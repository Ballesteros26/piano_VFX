using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Collections.Concurrent
{
	// Token: 0x02000018 RID: 24
	internal class SplitOrderedList<TKey, T>
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00003088 File Offset: 0x00001288
		public SplitOrderedList(IEqualityComparer<TKey> comparer)
		{
			this.comparer = comparer;
			this.head = new SplitOrderedList<TKey, T>.Node().Init(0UL);
			this.tail = new SplitOrderedList<TKey, T>.Node().Init(ulong.MaxValue);
			this.head.Next = this.tail;
			this.SetBucket(0U, this.head);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000030FC File Offset: 0x000012FC
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003104 File Offset: 0x00001304
		public T InsertOrUpdate(uint key, TKey subKey, Func<T> addGetter, Func<T, T> updateGetter)
		{
			SplitOrderedList<TKey, T>.Node node;
			if (this.InsertInternal(key, subKey, default(T), addGetter, out node))
			{
				return node.Data;
			}
			return node.Data = updateGetter(node.Data);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003144 File Offset: 0x00001344
		public T InsertOrUpdate(uint key, TKey subKey, T addValue, T updateValue)
		{
			SplitOrderedList<TKey, T>.Node node;
			if (this.InsertInternal(key, subKey, addValue, null, out node))
			{
				return node.Data;
			}
			node.Data = updateValue;
			return updateValue;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003174 File Offset: 0x00001374
		public bool Insert(uint key, TKey subKey, T data)
		{
			SplitOrderedList<TKey, T>.Node node;
			return this.InsertInternal(key, subKey, data, null, out node);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003190 File Offset: 0x00001390
		public T InsertOrGet(uint key, TKey subKey, T data, Func<T> dataCreator)
		{
			SplitOrderedList<TKey, T>.Node node;
			this.InsertInternal(key, subKey, data, dataCreator, out node);
			return node.Data;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000031B4 File Offset: 0x000013B4
		private bool InsertInternal(uint key, TKey subKey, T data, Func<T> dataCreator, out SplitOrderedList<TKey, T>.Node current)
		{
			SplitOrderedList<TKey, T>.Node node = new SplitOrderedList<TKey, T>.Node().Init(SplitOrderedList<TKey, T>.ComputeRegularKey(key), subKey, data);
			uint num = key % (uint)this.size;
			SplitOrderedList<TKey, T>.Node node2 = this.GetBucket(num) ?? this.InitializeBucket(num);
			if (!this.ListInsert(node, node2, out current, dataCreator))
			{
				return false;
			}
			int num2 = this.size;
			if (Interlocked.Increment(ref this.count) / num2 > 5 && (num2 & 1073741824) == 0)
			{
				Interlocked.CompareExchange(ref this.size, 2 * num2, num2);
			}
			current = node;
			return true;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003238 File Offset: 0x00001438
		public bool Find(uint key, TKey subKey, out T data)
		{
			uint num = key % (uint)this.size;
			data = default(T);
			SplitOrderedList<TKey, T>.Node node = this.GetBucket(num) ?? this.InitializeBucket(num);
			SplitOrderedList<TKey, T>.Node node2;
			if (!this.ListFind(SplitOrderedList<TKey, T>.ComputeRegularKey(key), subKey, node, out node2))
			{
				return false;
			}
			data = node2.Data;
			return !node2.Marked;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003294 File Offset: 0x00001494
		public bool CompareExchange(uint key, TKey subKey, T data, Func<T, bool> check)
		{
			uint num = key % (uint)this.size;
			SplitOrderedList<TKey, T>.Node node = this.GetBucket(num) ?? this.InitializeBucket(num);
			SplitOrderedList<TKey, T>.Node node2;
			if (!this.ListFind(SplitOrderedList<TKey, T>.ComputeRegularKey(key), subKey, node, out node2))
			{
				return false;
			}
			if (!check(node2.Data))
			{
				return false;
			}
			node2.Data = data;
			return true;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000032EC File Offset: 0x000014EC
		public bool Delete(uint key, TKey subKey, out T data)
		{
			uint num = key % (uint)this.size;
			SplitOrderedList<TKey, T>.Node node = this.GetBucket(num) ?? this.InitializeBucket(num);
			if (!this.ListDelete(node, SplitOrderedList<TKey, T>.ComputeRegularKey(key), subKey, out data))
			{
				return false;
			}
			Interlocked.Decrement(ref this.count);
			return true;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003335 File Offset: 0x00001535
		public IEnumerator<T> GetEnumerator()
		{
			for (SplitOrderedList<TKey, T>.Node node = this.head.Next; node != this.tail; node = node.Next)
			{
				while (node.Marked || (node.Key & 1UL) == 0UL)
				{
					node = node.Next;
					if (node == this.tail)
					{
						yield break;
					}
				}
				yield return node.Data;
			}
			yield break;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003344 File Offset: 0x00001544
		private SplitOrderedList<TKey, T>.Node InitializeBucket(uint b)
		{
			uint parent = SplitOrderedList<TKey, T>.GetParent(b);
			SplitOrderedList<TKey, T>.Node node = this.GetBucket(parent) ?? this.InitializeBucket(parent);
			SplitOrderedList<TKey, T>.Node node2 = new SplitOrderedList<TKey, T>.Node().Init(SplitOrderedList<TKey, T>.ComputeDummyKey(b));
			SplitOrderedList<TKey, T>.Node node3;
			if (!this.ListInsert(node2, node, out node3, null))
			{
				return node3;
			}
			return this.SetBucket(b, node2);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003394 File Offset: 0x00001594
		private static uint GetParent(uint v)
		{
			uint num2;
			uint num3;
			int num = (int)(((num2 = v >> 16) > 0U) ? (((num3 = num2 >> 8) > 0U) ? (24 + SplitOrderedList<TKey, T>.logTable[(int)num3]) : (16 + SplitOrderedList<TKey, T>.logTable[(int)num2])) : (((num3 = v >> 8) > 0U) ? (8 + SplitOrderedList<TKey, T>.logTable[(int)num3]) : SplitOrderedList<TKey, T>.logTable[(int)v]));
			return (uint)((ulong)v & (ulong)(~(1L << (num & 31))));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000033F1 File Offset: 0x000015F1
		private static ulong ComputeRegularKey(uint key)
		{
			return SplitOrderedList<TKey, T>.ComputeDummyKey(key) | 1UL;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000033FC File Offset: 0x000015FC
		private static ulong ComputeDummyKey(uint key)
		{
			return (ulong)(((int)SplitOrderedList<TKey, T>.reverseTable[(int)(key & 255U)] << 24) | ((int)SplitOrderedList<TKey, T>.reverseTable[(int)((key >> 8) & 255U)] << 16) | ((int)SplitOrderedList<TKey, T>.reverseTable[(int)((key >> 16) & 255U)] << 8) | (int)SplitOrderedList<TKey, T>.reverseTable[(int)((key >> 24) & 255U)]) << 1;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003453 File Offset: 0x00001653
		private SplitOrderedList<TKey, T>.Node GetBucket(uint index)
		{
			if ((ulong)index >= (ulong)((long)this.buckets.Length))
			{
				return null;
			}
			return this.buckets[(int)index];
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000346C File Offset: 0x0000166C
		private SplitOrderedList<TKey, T>.Node SetBucket(uint index, SplitOrderedList<TKey, T>.Node node)
		{
			SplitOrderedList<TKey, T>.Node node2;
			try
			{
				this.slim.EnterReadLock();
				this.CheckSegment(index, true);
				Interlocked.CompareExchange<SplitOrderedList<TKey, T>.Node>(ref this.buckets[(int)index], node, null);
				node2 = this.buckets[(int)index];
			}
			finally
			{
				this.slim.ExitReadLock();
			}
			return node2;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000034C8 File Offset: 0x000016C8
		private void CheckSegment(uint segment, bool readLockTaken)
		{
			if ((ulong)segment < (ulong)((long)this.buckets.Length))
			{
				return;
			}
			if (readLockTaken)
			{
				this.slim.ExitReadLock();
			}
			try
			{
				this.slim.EnterWriteLock();
				while ((ulong)segment >= (ulong)((long)this.buckets.Length))
				{
					Array.Resize<SplitOrderedList<TKey, T>.Node>(ref this.buckets, this.buckets.Length * 2);
				}
			}
			finally
			{
				this.slim.ExitWriteLock();
			}
			if (readLockTaken)
			{
				this.slim.EnterReadLock();
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003550 File Offset: 0x00001750
		private SplitOrderedList<TKey, T>.Node ListSearch(ulong key, TKey subKey, ref SplitOrderedList<TKey, T>.Node left, SplitOrderedList<TKey, T>.Node h)
		{
			SplitOrderedList<TKey, T>.Node node = null;
			SplitOrderedList<TKey, T>.Node node4;
			for (;;)
			{
				SplitOrderedList<TKey, T>.Node node2 = h;
				SplitOrderedList<TKey, T>.Node node3 = node2.Next;
				do
				{
					if (!node3.Marked)
					{
						left = node2;
						node = node3;
					}
					node2 = (node3.Marked ? node3.Next : node3);
					if (node2 == this.tail)
					{
						break;
					}
					node3 = node2.Next;
				}
				while (node3.Marked || node2.Key < key || (node3.Key == key && !this.comparer.Equals(subKey, node2.SubKey)));
				node4 = node2;
				if (node == node4)
				{
					if (node4 == this.tail || !node4.Next.Marked)
					{
						break;
					}
				}
				else if (Interlocked.CompareExchange<SplitOrderedList<TKey, T>.Node>(ref left.Next, node4, node) == node && (node4 == this.tail || !node4.Next.Marked))
				{
					return node4;
				}
			}
			return node4;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003618 File Offset: 0x00001818
		private bool ListDelete(SplitOrderedList<TKey, T>.Node startPoint, ulong key, TKey subKey, out T data)
		{
			SplitOrderedList<TKey, T>.Node node = null;
			data = default(T);
			SplitOrderedList<TKey, T>.Node node2 = null;
			SplitOrderedList<TKey, T>.Node node3;
			SplitOrderedList<TKey, T>.Node next;
			for (;;)
			{
				node3 = this.ListSearch(key, subKey, ref node, startPoint);
				if (node3 == this.tail || node3.Key != key || !this.comparer.Equals(subKey, node3.SubKey))
				{
					break;
				}
				data = node3.Data;
				next = node3.Next;
				if (!next.Marked)
				{
					if (node2 == null)
					{
						node2 = new SplitOrderedList<TKey, T>.Node();
					}
					node2.Init(next);
					if (Interlocked.CompareExchange<SplitOrderedList<TKey, T>.Node>(ref node3.Next, node2, next) == next)
					{
						goto Block_5;
					}
				}
			}
			return false;
			Block_5:
			if (Interlocked.CompareExchange<SplitOrderedList<TKey, T>.Node>(ref node.Next, next, node3) != node3)
			{
				this.ListSearch(node3.Key, subKey, ref node, startPoint);
			}
			return true;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000036C8 File Offset: 0x000018C8
		private bool ListInsert(SplitOrderedList<TKey, T>.Node newNode, SplitOrderedList<TKey, T>.Node startPoint, out SplitOrderedList<TKey, T>.Node current, Func<T> dataCreator)
		{
			ulong key = newNode.Key;
			SplitOrderedList<TKey, T>.Node node = null;
			for (;;)
			{
				SplitOrderedList<TKey, T>.Node node2;
				current = (node2 = this.ListSearch(key, newNode.SubKey, ref node, startPoint));
				SplitOrderedList<TKey, T>.Node node3 = node2;
				if (node3 != this.tail && node3.Key == key && this.comparer.Equals(newNode.SubKey, node3.SubKey))
				{
					break;
				}
				newNode.Next = node3;
				if (dataCreator != null)
				{
					newNode.Data = dataCreator();
				}
				if (Interlocked.CompareExchange<SplitOrderedList<TKey, T>.Node>(ref node.Next, newNode, node3) == node3)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000374C File Offset: 0x0000194C
		private bool ListFind(ulong key, TKey subKey, SplitOrderedList<TKey, T>.Node startPoint, out SplitOrderedList<TKey, T>.Node data)
		{
			SplitOrderedList<TKey, T>.Node node = null;
			data = null;
			SplitOrderedList<TKey, T>.Node node2 = this.ListSearch(key, subKey, ref node, startPoint);
			data = node2;
			return node2 != this.tail && node2.Key == key && this.comparer.Equals(subKey, node2.SubKey);
		}

		// Token: 0x04000D4D RID: 3405
		private const int MaxLoad = 5;

		// Token: 0x04000D4E RID: 3406
		private const uint BucketSize = 512U;

		// Token: 0x04000D4F RID: 3407
		private SplitOrderedList<TKey, T>.Node head;

		// Token: 0x04000D50 RID: 3408
		private SplitOrderedList<TKey, T>.Node tail;

		// Token: 0x04000D51 RID: 3409
		private SplitOrderedList<TKey, T>.Node[] buckets = new SplitOrderedList<TKey, T>.Node[512];

		// Token: 0x04000D52 RID: 3410
		private int count;

		// Token: 0x04000D53 RID: 3411
		private int size = 2;

		// Token: 0x04000D54 RID: 3412
		private SplitOrderedList<TKey, T>.SimpleRwLock slim;

		// Token: 0x04000D55 RID: 3413
		private readonly IEqualityComparer<TKey> comparer;

		// Token: 0x04000D56 RID: 3414
		private static readonly byte[] reverseTable = new byte[]
		{
			0, 128, 64, 192, 32, 160, 96, 224, 16, 144,
			80, 208, 48, 176, 112, 240, 8, 136, 72, 200,
			40, 168, 104, 232, 24, 152, 88, 216, 56, 184,
			120, 248, 4, 132, 68, 196, 36, 164, 100, 228,
			20, 148, 84, 212, 52, 180, 116, 244, 12, 140,
			76, 204, 44, 172, 108, 236, 28, 156, 92, 220,
			60, 188, 124, 252, 2, 130, 66, 194, 34, 162,
			98, 226, 18, 146, 82, 210, 50, 178, 114, 242,
			10, 138, 74, 202, 42, 170, 106, 234, 26, 154,
			90, 218, 58, 186, 122, 250, 6, 134, 70, 198,
			38, 166, 102, 230, 22, 150, 86, 214, 54, 182,
			118, 246, 14, 142, 78, 206, 46, 174, 110, 238,
			30, 158, 94, 222, 62, 190, 126, 254, 1, 129,
			65, 193, 33, 161, 97, 225, 17, 145, 81, 209,
			49, 177, 113, 241, 9, 137, 73, 201, 41, 169,
			105, 233, 25, 153, 89, 217, 57, 185, 121, 249,
			5, 133, 69, 197, 37, 165, 101, 229, 21, 149,
			85, 213, 53, 181, 117, 245, 13, 141, 77, 205,
			45, 173, 109, 237, 29, 157, 93, 221, 61, 189,
			125, 253, 3, 131, 67, 195, 35, 163, 99, 227,
			19, 147, 83, 211, 51, 179, 115, 243, 11, 139,
			75, 203, 43, 171, 107, 235, 27, 155, 91, 219,
			59, 187, 123, 251, 7, 135, 71, 199, 39, 167,
			103, 231, 23, 151, 87, 215, 55, 183, 119, 247,
			15, 143, 79, 207, 47, 175, 111, 239, 31, 159,
			95, 223, 63, 191, 127, byte.MaxValue
		};

		// Token: 0x04000D57 RID: 3415
		private static readonly byte[] logTable = new byte[]
		{
			byte.MaxValue, 0, 1, 1, 2, 2, 2, 2, 3, 3,
			3, 3, 3, 3, 3, 3, 4, 4, 4, 4,
			4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
			4, 4, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 6, 6, 6, 6, 6, 6,
			6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
			6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
			6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
			6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
			6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
			6, 6, 6, 6, 6, 6, 6, 6, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7
		};

		// Token: 0x02000019 RID: 25
		private class Node
		{
			// Token: 0x0600005C RID: 92 RVA: 0x000037CD File Offset: 0x000019CD
			public SplitOrderedList<TKey, T>.Node Init(ulong key, TKey subKey, T data)
			{
				this.Key = key;
				this.SubKey = subKey;
				this.Data = data;
				this.Marked = false;
				this.Next = null;
				return this;
			}

			// Token: 0x0600005D RID: 93 RVA: 0x000037F3 File Offset: 0x000019F3
			public SplitOrderedList<TKey, T>.Node Init(ulong key)
			{
				this.Key = key;
				this.Data = default(T);
				this.Next = null;
				this.Marked = false;
				this.SubKey = default(TKey);
				return this;
			}

			// Token: 0x0600005E RID: 94 RVA: 0x00003823 File Offset: 0x00001A23
			public SplitOrderedList<TKey, T>.Node Init(SplitOrderedList<TKey, T>.Node wrapped)
			{
				this.Marked = true;
				this.Next = wrapped;
				this.Key = 0UL;
				this.Data = default(T);
				this.SubKey = default(TKey);
				return this;
			}

			// Token: 0x04000D58 RID: 3416
			public bool Marked;

			// Token: 0x04000D59 RID: 3417
			public ulong Key;

			// Token: 0x04000D5A RID: 3418
			public TKey SubKey;

			// Token: 0x04000D5B RID: 3419
			public T Data;

			// Token: 0x04000D5C RID: 3420
			public SplitOrderedList<TKey, T>.Node Next;
		}

		// Token: 0x0200001A RID: 26
		private struct SimpleRwLock
		{
			// Token: 0x06000060 RID: 96 RVA: 0x00003854 File Offset: 0x00001A54
			public void EnterReadLock()
			{
				SpinWait spinWait = default(SpinWait);
				for (;;)
				{
					if ((this.rwlock & 3) <= 0)
					{
						if ((Interlocked.Add(ref this.rwlock, 4) & 1) == 0)
						{
							break;
						}
						Interlocked.Add(ref this.rwlock, -4);
					}
					else
					{
						spinWait.SpinOnce();
					}
				}
			}

			// Token: 0x06000061 RID: 97 RVA: 0x0000389D File Offset: 0x00001A9D
			public void ExitReadLock()
			{
				Interlocked.Add(ref this.rwlock, -4);
			}

			// Token: 0x06000062 RID: 98 RVA: 0x000038B0 File Offset: 0x00001AB0
			public void EnterWriteLock()
			{
				SpinWait spinWait = default(SpinWait);
				for (;;)
				{
					int num = this.rwlock;
					if (num < 2)
					{
						if (Interlocked.CompareExchange(ref this.rwlock, 2, num) == num)
						{
							break;
						}
						num = this.rwlock;
					}
					while ((num & 1) == 0)
					{
						if (Interlocked.CompareExchange(ref this.rwlock, num | 1, num) == num)
						{
							break;
						}
						num = this.rwlock;
					}
					while (this.rwlock > 1)
					{
						spinWait.SpinOnce();
					}
				}
			}

			// Token: 0x06000063 RID: 99 RVA: 0x0000391B File Offset: 0x00001B1B
			public void ExitWriteLock()
			{
				Interlocked.Add(ref this.rwlock, -2);
			}

			// Token: 0x04000D5D RID: 3421
			private const int RwWait = 1;

			// Token: 0x04000D5E RID: 3422
			private const int RwWrite = 2;

			// Token: 0x04000D5F RID: 3423
			private const int RwRead = 4;

			// Token: 0x04000D60 RID: 3424
			private int rwlock;
		}
	}
}
