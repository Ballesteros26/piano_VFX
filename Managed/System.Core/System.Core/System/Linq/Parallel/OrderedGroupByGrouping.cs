using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001C2 RID: 450
	internal class OrderedGroupByGrouping<TGroupKey, TOrderKey, TElement> : IGrouping<TGroupKey, TElement>, IEnumerable<TElement>, IEnumerable
	{
		// Token: 0x06000BD5 RID: 3029 RVA: 0x0002759F File Offset: 0x0002579F
		internal OrderedGroupByGrouping(TGroupKey groupKey, IComparer<TOrderKey> orderComparer)
		{
			this._groupKey = groupKey;
			this._values = new GrowingArray<TElement>();
			this._orderKeys = new GrowingArray<TOrderKey>();
			this._orderComparer = orderComparer;
			this._wrappedComparer = new OrderedGroupByGrouping<TGroupKey, TOrderKey, TElement>.KeyAndValuesComparer(this._orderComparer);
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x000275DC File Offset: 0x000257DC
		TGroupKey IGrouping<TGroupKey, TElement>.Key
		{
			get
			{
				return this._groupKey;
			}
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x000275E4 File Offset: 0x000257E4
		IEnumerator<TElement> IEnumerable<TElement>.GetEnumerator()
		{
			int valueCount = this._values.Count;
			TElement[] valueArray = this._values.InternalArray;
			int num;
			for (int i = 0; i < valueCount; i = num + 1)
			{
				yield return valueArray[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x000275F3 File Offset: 0x000257F3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<TElement>)this).GetEnumerator();
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x000275FB File Offset: 0x000257FB
		internal void Add(TElement value, TOrderKey orderKey)
		{
			this._values.Add(value);
			this._orderKeys.Add(orderKey);
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00027618 File Offset: 0x00025818
		internal void DoneAdding()
		{
			List<KeyValuePair<TOrderKey, TElement>> list = new List<KeyValuePair<TOrderKey, TElement>>();
			for (int i = 0; i < this._orderKeys.InternalArray.Length; i++)
			{
				list.Add(new KeyValuePair<TOrderKey, TElement>(this._orderKeys.InternalArray[i], this._values.InternalArray[i]));
			}
			list.Sort(0, this._values.Count, this._wrappedComparer);
			for (int j = 0; j < this._values.InternalArray.Length; j++)
			{
				this._orderKeys.InternalArray[j] = list[j].Key;
				this._values.InternalArray[j] = list[j].Value;
			}
		}

		// Token: 0x0400071A RID: 1818
		private TGroupKey _groupKey;

		// Token: 0x0400071B RID: 1819
		private GrowingArray<TElement> _values;

		// Token: 0x0400071C RID: 1820
		private GrowingArray<TOrderKey> _orderKeys;

		// Token: 0x0400071D RID: 1821
		private IComparer<TOrderKey> _orderComparer;

		// Token: 0x0400071E RID: 1822
		private OrderedGroupByGrouping<TGroupKey, TOrderKey, TElement>.KeyAndValuesComparer _wrappedComparer;

		// Token: 0x020001C3 RID: 451
		private class KeyAndValuesComparer : IComparer<KeyValuePair<TOrderKey, TElement>>
		{
			// Token: 0x06000BDB RID: 3035 RVA: 0x000276E0 File Offset: 0x000258E0
			public KeyAndValuesComparer(IComparer<TOrderKey> comparer)
			{
				this.myComparer = comparer;
			}

			// Token: 0x06000BDC RID: 3036 RVA: 0x000276EF File Offset: 0x000258EF
			public int Compare(KeyValuePair<TOrderKey, TElement> x, KeyValuePair<TOrderKey, TElement> y)
			{
				return this.myComparer.Compare(x.Key, y.Key);
			}

			// Token: 0x0400071F RID: 1823
			private IComparer<TOrderKey> myComparer;
		}
	}
}
