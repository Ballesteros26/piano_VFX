using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000116 RID: 278
	internal class Set<TElement>
	{
		// Token: 0x0600094D RID: 2381 RVA: 0x0001DA26 File Offset: 0x0001BC26
		public Set(IEqualityComparer<TElement> comparer)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<TElement>.Default;
			}
			this._comparer = comparer;
			this._buckets = new int[7];
			this._slots = new Set<TElement>.Slot[7];
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0001DA57 File Offset: 0x0001BC57
		public bool Add(TElement value)
		{
			return !this.Find(value, true);
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0001DA64 File Offset: 0x0001BC64
		public bool Contains(TElement value)
		{
			return this.Find(value, false);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0001DA70 File Offset: 0x0001BC70
		public bool Remove(TElement value)
		{
			int num = this.InternalGetHashCode(value);
			int num2 = num % this._buckets.Length;
			int num3 = -1;
			for (int i = this._buckets[num2] - 1; i >= 0; i = this._slots[i].next)
			{
				if (this._slots[i].hashCode == num && this._comparer.Equals(this._slots[i].value, value))
				{
					if (num3 < 0)
					{
						this._buckets[num2] = this._slots[i].next + 1;
					}
					else
					{
						this._slots[num3].next = this._slots[i].next;
					}
					this._slots[i].hashCode = -1;
					this._slots[i].value = default(TElement);
					this._slots[i].next = -1;
					return true;
				}
				num3 = i;
			}
			return false;
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0001DB78 File Offset: 0x0001BD78
		private bool Find(TElement value, bool add)
		{
			int num = this.InternalGetHashCode(value);
			for (int i = this._buckets[num % this._buckets.Length] - 1; i >= 0; i = this._slots[i].next)
			{
				if (this._slots[i].hashCode == num && this._comparer.Equals(this._slots[i].value, value))
				{
					return true;
				}
			}
			if (add)
			{
				if (this._count == this._slots.Length)
				{
					this.Resize();
				}
				int count = this._count;
				this._count++;
				int num2 = num % this._buckets.Length;
				this._slots[count].hashCode = num;
				this._slots[count].value = value;
				this._slots[count].next = this._buckets[num2] - 1;
				this._buckets[num2] = count + 1;
			}
			return false;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0001DC78 File Offset: 0x0001BE78
		private void Resize()
		{
			int num = checked(this._count * 2 + 1);
			int[] array = new int[num];
			Set<TElement>.Slot[] array2 = new Set<TElement>.Slot[num];
			Array.Copy(this._slots, 0, array2, 0, this._count);
			for (int i = 0; i < this._count; i++)
			{
				int num2 = array2[i].hashCode % num;
				array2[i].next = array[num2] - 1;
				array[num2] = i + 1;
			}
			this._buckets = array;
			this._slots = array2;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0001DCFA File Offset: 0x0001BEFA
		internal int InternalGetHashCode(TElement value)
		{
			if (value != null)
			{
				return this._comparer.GetHashCode(value) & int.MaxValue;
			}
			return 0;
		}

		// Token: 0x0400055D RID: 1373
		private int[] _buckets;

		// Token: 0x0400055E RID: 1374
		private Set<TElement>.Slot[] _slots;

		// Token: 0x0400055F RID: 1375
		private int _count;

		// Token: 0x04000560 RID: 1376
		private readonly IEqualityComparer<TElement> _comparer;

		// Token: 0x04000561 RID: 1377
		private const int InitialSize = 7;

		// Token: 0x04000562 RID: 1378
		private const int HashCodeMask = 2147483647;

		// Token: 0x02000117 RID: 279
		internal struct Slot
		{
			// Token: 0x04000563 RID: 1379
			internal int hashCode;

			// Token: 0x04000564 RID: 1380
			internal int next;

			// Token: 0x04000565 RID: 1381
			internal TElement value;
		}
	}
}
