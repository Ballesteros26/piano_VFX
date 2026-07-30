using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000100 RID: 256
	internal sealed class Set<TElement>
	{
		// Token: 0x060008F3 RID: 2291 RVA: 0x0001CC10 File Offset: 0x0001AE10
		public Set(IEqualityComparer<TElement> comparer)
		{
			this._comparer = comparer ?? EqualityComparer<TElement>.Default;
			this._buckets = new int[7];
			this._slots = new Set<TElement>.Slot[7];
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0001CC40 File Offset: 0x0001AE40
		public bool Add(TElement value)
		{
			int num = this.InternalGetHashCode(value);
			for (int i = this._buckets[num % this._buckets.Length] - 1; i >= 0; i = this._slots[i]._next)
			{
				if (this._slots[i]._hashCode == num && this._comparer.Equals(this._slots[i]._value, value))
				{
					return false;
				}
			}
			if (this._count == this._slots.Length)
			{
				this.Resize();
			}
			int count = this._count;
			this._count++;
			int num2 = num % this._buckets.Length;
			this._slots[count]._hashCode = num;
			this._slots[count]._value = value;
			this._slots[count]._next = this._buckets[num2] - 1;
			this._buckets[num2] = count + 1;
			return true;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0001CD38 File Offset: 0x0001AF38
		public bool Remove(TElement value)
		{
			int num = this.InternalGetHashCode(value);
			int num2 = num % this._buckets.Length;
			int num3 = -1;
			for (int i = this._buckets[num2] - 1; i >= 0; i = this._slots[i]._next)
			{
				if (this._slots[i]._hashCode == num && this._comparer.Equals(this._slots[i]._value, value))
				{
					if (num3 < 0)
					{
						this._buckets[num2] = this._slots[i]._next + 1;
					}
					else
					{
						this._slots[num3]._next = this._slots[i]._next;
					}
					this._slots[i]._hashCode = -1;
					this._slots[i]._value = default(TElement);
					this._slots[i]._next = -1;
					return true;
				}
				num3 = i;
			}
			return false;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0001CE40 File Offset: 0x0001B040
		private void Resize()
		{
			int num = checked(this._count * 2 + 1);
			int[] array = new int[num];
			Set<TElement>.Slot[] array2 = new Set<TElement>.Slot[num];
			Array.Copy(this._slots, 0, array2, 0, this._count);
			for (int i = 0; i < this._count; i++)
			{
				int num2 = array2[i]._hashCode % num;
				array2[i]._next = array[num2] - 1;
				array[num2] = i + 1;
			}
			this._buckets = array;
			this._slots = array2;
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0001CEC4 File Offset: 0x0001B0C4
		public TElement[] ToArray()
		{
			TElement[] array = new TElement[this._count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = this._slots[num]._value;
			}
			return array;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001CF04 File Offset: 0x0001B104
		public List<TElement> ToList()
		{
			int count = this._count;
			List<TElement> list = new List<TElement>(count);
			for (int num = 0; num != count; num++)
			{
				list.Add(this._slots[num]._value);
			}
			return list;
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x0001CF43 File Offset: 0x0001B143
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0001CF4C File Offset: 0x0001B14C
		public void UnionWith(IEnumerable<TElement> other)
		{
			foreach (TElement telement in other)
			{
				this.Add(telement);
			}
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0001CF98 File Offset: 0x0001B198
		private int InternalGetHashCode(TElement value)
		{
			if (value != null)
			{
				return this._comparer.GetHashCode(value) & int.MaxValue;
			}
			return 0;
		}

		// Token: 0x04000529 RID: 1321
		private readonly IEqualityComparer<TElement> _comparer;

		// Token: 0x0400052A RID: 1322
		private int[] _buckets;

		// Token: 0x0400052B RID: 1323
		private Set<TElement>.Slot[] _slots;

		// Token: 0x0400052C RID: 1324
		private int _count;

		// Token: 0x02000101 RID: 257
		private struct Slot
		{
			// Token: 0x0400052D RID: 1325
			internal int _hashCode;

			// Token: 0x0400052E RID: 1326
			internal int _next;

			// Token: 0x0400052F RID: 1327
			internal TElement _value;
		}
	}
}
