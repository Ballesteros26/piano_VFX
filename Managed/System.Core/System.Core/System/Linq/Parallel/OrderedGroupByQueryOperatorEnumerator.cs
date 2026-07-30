using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001BC RID: 444
	internal abstract class OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey> : QueryOperatorEnumerator<IGrouping<TGroupKey, TElement>, TOrderKey>
	{
		// Token: 0x06000BC7 RID: 3015 RVA: 0x00027247 File Offset: 0x00025447
		protected OrderedGroupByQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TSource, TGroupKey>, TOrderKey> source, Func<TSource, TGroupKey> keySelector, IEqualityComparer<TGroupKey> keyComparer, IComparer<TOrderKey> orderComparer, CancellationToken cancellationToken)
		{
			this._source = source;
			this._keySelector = keySelector;
			this._keyComparer = keyComparer;
			this._orderComparer = orderComparer;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00027274 File Offset: 0x00025474
		internal override bool MoveNext(ref IGrouping<TGroupKey, TElement> currentElement, ref TOrderKey currentKey)
		{
			OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables mutables = this._mutables;
			if (mutables == null)
			{
				mutables = (this._mutables = new OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables());
				mutables._hashLookup = this.BuildHashLookup();
				mutables._hashLookupIndex = -1;
			}
			OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables mutables2 = mutables;
			int num = mutables2._hashLookupIndex + 1;
			mutables2._hashLookupIndex = num;
			if (num < mutables._hashLookup.Count)
			{
				OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.GroupKeyData value = mutables._hashLookup[mutables._hashLookupIndex].Value;
				currentElement = value._grouping;
				currentKey = value._orderKey;
				return true;
			}
			return false;
		}

		// Token: 0x06000BC9 RID: 3017
		protected abstract HashLookup<Wrapper<TGroupKey>, OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.GroupKeyData> BuildHashLookup();

		// Token: 0x06000BCA RID: 3018 RVA: 0x000272FD File Offset: 0x000254FD
		protected override void Dispose(bool disposing)
		{
			this._source.Dispose();
		}

		// Token: 0x0400070E RID: 1806
		protected readonly QueryOperatorEnumerator<Pair<TSource, TGroupKey>, TOrderKey> _source;

		// Token: 0x0400070F RID: 1807
		private readonly Func<TSource, TGroupKey> _keySelector;

		// Token: 0x04000710 RID: 1808
		protected readonly IEqualityComparer<TGroupKey> _keyComparer;

		// Token: 0x04000711 RID: 1809
		protected readonly IComparer<TOrderKey> _orderComparer;

		// Token: 0x04000712 RID: 1810
		protected readonly CancellationToken _cancellationToken;

		// Token: 0x04000713 RID: 1811
		private OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables _mutables;

		// Token: 0x020001BD RID: 445
		private class Mutables
		{
			// Token: 0x04000714 RID: 1812
			internal HashLookup<Wrapper<TGroupKey>, OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.GroupKeyData> _hashLookup;

			// Token: 0x04000715 RID: 1813
			internal int _hashLookupIndex;
		}

		// Token: 0x020001BE RID: 446
		protected class GroupKeyData
		{
			// Token: 0x06000BCC RID: 3020 RVA: 0x0002730A File Offset: 0x0002550A
			internal GroupKeyData(TOrderKey orderKey, TGroupKey hashKey, IComparer<TOrderKey> orderComparer)
			{
				this._orderKey = orderKey;
				this._grouping = new OrderedGroupByGrouping<TGroupKey, TOrderKey, TElement>(hashKey, orderComparer);
			}

			// Token: 0x04000716 RID: 1814
			internal TOrderKey _orderKey;

			// Token: 0x04000717 RID: 1815
			internal OrderedGroupByGrouping<TGroupKey, TOrderKey, TElement> _grouping;
		}
	}
}
