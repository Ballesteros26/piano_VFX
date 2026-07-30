using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000206 RID: 518
	internal class HashLookup<TKey, TValue>
	{
		// Token: 0x06000CE9 RID: 3305 RVA: 0x0002B0EF File Offset: 0x000292EF
		internal HashLookup()
			: this(null)
		{
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0002B0F8 File Offset: 0x000292F8
		internal HashLookup(IEqualityComparer<TKey> comparer)
		{
			this.comparer = comparer;
			this.buckets = new int[7];
			this.slots = new HashLookup<TKey, TValue>.Slot[7];
			this.freeList = -1;
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0002B126 File Offset: 0x00029326
		internal bool Add(TKey key, TValue value)
		{
			return !this.Find(key, true, false, ref value);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0002B136 File Offset: 0x00029336
		internal bool TryGetValue(TKey key, ref TValue value)
		{
			return this.Find(key, false, false, ref value);
		}

		// Token: 0x170001A3 RID: 419
		internal TValue this[TKey key]
		{
			set
			{
				TValue tvalue = value;
				this.Find(key, false, true, ref tvalue);
			}
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0002B15F File Offset: 0x0002935F
		private int GetKeyHashCode(TKey key)
		{
			return int.MaxValue & ((this.comparer == null) ? ((key == null) ? 0 : key.GetHashCode()) : this.comparer.GetHashCode(key));
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x0002B198 File Offset: 0x00029398
		private bool AreKeysEqual(TKey key1, TKey key2)
		{
			if (this.comparer != null)
			{
				return this.comparer.Equals(key1, key2);
			}
			return (key1 == null && key2 == null) || (key1 != null && key1.Equals(key2));
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0002B1EC File Offset: 0x000293EC
		private bool Find(TKey key, bool add, bool set, ref TValue value)
		{
			int keyHashCode = this.GetKeyHashCode(key);
			int i = this.buckets[keyHashCode % this.buckets.Length] - 1;
			while (i >= 0)
			{
				if (this.slots[i].hashCode == keyHashCode && this.AreKeysEqual(this.slots[i].key, key))
				{
					if (set)
					{
						this.slots[i].value = value;
						return true;
					}
					value = this.slots[i].value;
					return true;
				}
				else
				{
					i = this.slots[i].next;
				}
			}
			if (add)
			{
				int num;
				if (this.freeList >= 0)
				{
					num = this.freeList;
					this.freeList = this.slots[num].next;
				}
				else
				{
					if (this.count == this.slots.Length)
					{
						this.Resize();
					}
					num = this.count;
					this.count++;
				}
				int num2 = keyHashCode % this.buckets.Length;
				this.slots[num].hashCode = keyHashCode;
				this.slots[num].key = key;
				this.slots[num].value = value;
				this.slots[num].next = this.buckets[num2] - 1;
				this.buckets[num2] = num + 1;
			}
			return false;
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0002B35C File Offset: 0x0002955C
		private void Resize()
		{
			int num = checked(this.count * 2 + 1);
			int[] array = new int[num];
			HashLookup<TKey, TValue>.Slot[] array2 = new HashLookup<TKey, TValue>.Slot[num];
			Array.Copy(this.slots, 0, array2, 0, this.count);
			for (int i = 0; i < this.count; i++)
			{
				int num2 = array2[i].hashCode % num;
				array2[i].next = array[num2] - 1;
				array[num2] = i + 1;
			}
			this.buckets = array;
			this.slots = array2;
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0002B3DE File Offset: 0x000295DE
		internal int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x170001A5 RID: 421
		internal KeyValuePair<TKey, TValue> this[int index]
		{
			get
			{
				return new KeyValuePair<TKey, TValue>(this.slots[index].key, this.slots[index].value);
			}
		}

		// Token: 0x04000806 RID: 2054
		private int[] buckets;

		// Token: 0x04000807 RID: 2055
		private HashLookup<TKey, TValue>.Slot[] slots;

		// Token: 0x04000808 RID: 2056
		private int count;

		// Token: 0x04000809 RID: 2057
		private int freeList;

		// Token: 0x0400080A RID: 2058
		private IEqualityComparer<TKey> comparer;

		// Token: 0x0400080B RID: 2059
		private const int HashCodeMask = 2147483647;

		// Token: 0x02000207 RID: 519
		internal struct Slot
		{
			// Token: 0x0400080C RID: 2060
			internal int hashCode;

			// Token: 0x0400080D RID: 2061
			internal int next;

			// Token: 0x0400080E RID: 2062
			internal TKey key;

			// Token: 0x0400080F RID: 2063
			internal TValue value;
		}
	}
}
