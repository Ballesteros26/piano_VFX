using System;
using System.Diagnostics;
using System.Threading;

namespace System.Buffers
{
	// Token: 0x02000410 RID: 1040
	internal sealed class DefaultArrayPool<T> : ArrayPool<T>
	{
		// Token: 0x06001FBA RID: 8122 RVA: 0x0007B9E9 File Offset: 0x00079BE9
		internal DefaultArrayPool()
			: this(1048576, 50)
		{
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x0007B9F8 File Offset: 0x00079BF8
		internal DefaultArrayPool(int maxArrayLength, int maxArraysPerBucket)
		{
			if (maxArrayLength <= 0)
			{
				throw new ArgumentOutOfRangeException("maxArrayLength");
			}
			if (maxArraysPerBucket <= 0)
			{
				throw new ArgumentOutOfRangeException("maxArraysPerBucket");
			}
			if (maxArrayLength > 1073741824)
			{
				maxArrayLength = 1073741824;
			}
			else if (maxArrayLength < 16)
			{
				maxArrayLength = 16;
			}
			int id = this.Id;
			DefaultArrayPool<T>.Bucket[] array = new DefaultArrayPool<T>.Bucket[Utilities.SelectBucketIndex(maxArrayLength) + 1];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new DefaultArrayPool<T>.Bucket(Utilities.GetMaxSizeForBucket(i), maxArraysPerBucket, id);
			}
			this._buckets = array;
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001FBC RID: 8124 RVA: 0x0007BA7D File Offset: 0x00079C7D
		private int Id
		{
			get
			{
				return this.GetHashCode();
			}
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x0007BA88 File Offset: 0x00079C88
		public override T[] Rent(int minimumLength)
		{
			if (minimumLength < 0)
			{
				throw new ArgumentOutOfRangeException("minimumLength");
			}
			if (minimumLength == 0)
			{
				T[] array;
				if ((array = DefaultArrayPool<T>.s_emptyArray) == null)
				{
					array = (DefaultArrayPool<T>.s_emptyArray = new T[0]);
				}
				return array;
			}
			ArrayPoolEventSource log = ArrayPoolEventSource.Log;
			int num = Utilities.SelectBucketIndex(minimumLength);
			T[] array2;
			if (num < this._buckets.Length)
			{
				int num2 = num;
				for (;;)
				{
					array2 = this._buckets[num2].Rent();
					if (array2 != null)
					{
						break;
					}
					if (++num2 >= this._buckets.Length || num2 == num + 2)
					{
						goto IL_0096;
					}
				}
				if (log.IsEnabled())
				{
					log.BufferRented(array2.GetHashCode(), array2.Length, this.Id, this._buckets[num2].Id);
				}
				return array2;
				IL_0096:
				array2 = new T[this._buckets[num]._bufferLength];
			}
			else
			{
				array2 = new T[minimumLength];
			}
			if (log.IsEnabled())
			{
				int hashCode = array2.GetHashCode();
				int num3 = -1;
				log.BufferRented(hashCode, array2.Length, this.Id, num3);
				log.BufferAllocated(hashCode, array2.Length, this.Id, num3, (num >= this._buckets.Length) ? ArrayPoolEventSource.BufferAllocatedReason.OverMaximumSize : ArrayPoolEventSource.BufferAllocatedReason.PoolExhausted);
			}
			return array2;
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x0007BB90 File Offset: 0x00079D90
		public override void Return(T[] array, bool clearArray = false)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Length == 0)
			{
				return;
			}
			int num = Utilities.SelectBucketIndex(array.Length);
			if (num < this._buckets.Length)
			{
				if (clearArray)
				{
					Array.Clear(array, 0, array.Length);
				}
				this._buckets[num].Return(array);
			}
			ArrayPoolEventSource log = ArrayPoolEventSource.Log;
			if (log.IsEnabled())
			{
				log.BufferReturned(array.GetHashCode(), array.Length, this.Id);
			}
		}

		// Token: 0x04001B8D RID: 7053
		private const int DefaultMaxArrayLength = 1048576;

		// Token: 0x04001B8E RID: 7054
		private const int DefaultMaxNumberOfArraysPerBucket = 50;

		// Token: 0x04001B8F RID: 7055
		private static T[] s_emptyArray;

		// Token: 0x04001B90 RID: 7056
		private readonly DefaultArrayPool<T>.Bucket[] _buckets;

		// Token: 0x02000411 RID: 1041
		private sealed class Bucket
		{
			// Token: 0x06001FBF RID: 8127 RVA: 0x0007BC02 File Offset: 0x00079E02
			internal Bucket(int bufferLength, int numberOfBuffers, int poolId)
			{
				this._lock = new SpinLock(Debugger.IsAttached);
				this._buffers = new T[numberOfBuffers][];
				this._bufferLength = bufferLength;
				this._poolId = poolId;
			}

			// Token: 0x1700067D RID: 1661
			// (get) Token: 0x06001FC0 RID: 8128 RVA: 0x0007BA7D File Offset: 0x00079C7D
			internal int Id
			{
				get
				{
					return this.GetHashCode();
				}
			}

			// Token: 0x06001FC1 RID: 8129 RVA: 0x0007BC34 File Offset: 0x00079E34
			internal T[] Rent()
			{
				T[][] buffers = this._buffers;
				T[] array = null;
				bool flag = false;
				bool flag2 = false;
				try
				{
					this._lock.Enter(ref flag);
					if (this._index < buffers.Length)
					{
						array = buffers[this._index];
						T[][] array2 = buffers;
						int index = this._index;
						this._index = index + 1;
						array2[index] = null;
						flag2 = array == null;
					}
				}
				finally
				{
					if (flag)
					{
						this._lock.Exit(false);
					}
				}
				if (flag2)
				{
					array = new T[this._bufferLength];
					ArrayPoolEventSource log = ArrayPoolEventSource.Log;
					if (log.IsEnabled())
					{
						log.BufferAllocated(array.GetHashCode(), this._bufferLength, this._poolId, this.Id, ArrayPoolEventSource.BufferAllocatedReason.Pooled);
					}
				}
				return array;
			}

			// Token: 0x06001FC2 RID: 8130 RVA: 0x0007BCF0 File Offset: 0x00079EF0
			internal void Return(T[] array)
			{
				if (array.Length != this._bufferLength)
				{
					throw new ArgumentException("The buffer is not associated with this pool and may not be returned to it.", "array");
				}
				bool flag = false;
				try
				{
					this._lock.Enter(ref flag);
					if (this._index != 0)
					{
						T[][] buffers = this._buffers;
						int num = this._index - 1;
						this._index = num;
						buffers[num] = array;
					}
				}
				finally
				{
					if (flag)
					{
						this._lock.Exit(false);
					}
				}
			}

			// Token: 0x04001B91 RID: 7057
			internal readonly int _bufferLength;

			// Token: 0x04001B92 RID: 7058
			private readonly T[][] _buffers;

			// Token: 0x04001B93 RID: 7059
			private readonly int _poolId;

			// Token: 0x04001B94 RID: 7060
			private SpinLock _lock;

			// Token: 0x04001B95 RID: 7061
			private int _index;
		}
	}
}
