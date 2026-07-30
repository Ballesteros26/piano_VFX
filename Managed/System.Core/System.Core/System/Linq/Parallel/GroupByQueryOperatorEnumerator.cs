using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001B8 RID: 440
	internal abstract class GroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey> : QueryOperatorEnumerator<IGrouping<TGroupKey, TElement>, TOrderKey>
	{
		// Token: 0x06000BBE RID: 3006 RVA: 0x00027044 File Offset: 0x00025244
		protected GroupByQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TSource, TGroupKey>, TOrderKey> source, IEqualityComparer<TGroupKey> keyComparer, CancellationToken cancellationToken)
		{
			this._source = source;
			this._keyComparer = keyComparer;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00027064 File Offset: 0x00025264
		internal override bool MoveNext(ref IGrouping<TGroupKey, TElement> currentElement, ref TOrderKey currentKey)
		{
			GroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables mutables = this._mutables;
			if (mutables == null)
			{
				mutables = (this._mutables = new GroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables());
				mutables._hashLookup = this.BuildHashLookup();
				mutables._hashLookupIndex = -1;
			}
			GroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables mutables2 = mutables;
			int num = mutables2._hashLookupIndex + 1;
			mutables2._hashLookupIndex = num;
			if (num < mutables._hashLookup.Count)
			{
				currentElement = new GroupByGrouping<TGroupKey, TElement>(mutables._hashLookup[mutables._hashLookupIndex]);
				return true;
			}
			return false;
		}

		// Token: 0x06000BC0 RID: 3008
		protected abstract HashLookup<Wrapper<TGroupKey>, ListChunk<TElement>> BuildHashLookup();

		// Token: 0x06000BC1 RID: 3009 RVA: 0x000270D6 File Offset: 0x000252D6
		protected override void Dispose(bool disposing)
		{
			this._source.Dispose();
		}

		// Token: 0x04000707 RID: 1799
		protected readonly QueryOperatorEnumerator<Pair<TSource, TGroupKey>, TOrderKey> _source;

		// Token: 0x04000708 RID: 1800
		protected readonly IEqualityComparer<TGroupKey> _keyComparer;

		// Token: 0x04000709 RID: 1801
		protected readonly CancellationToken _cancellationToken;

		// Token: 0x0400070A RID: 1802
		private GroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables _mutables;

		// Token: 0x020001B9 RID: 441
		private class Mutables
		{
			// Token: 0x0400070B RID: 1803
			internal HashLookup<Wrapper<TGroupKey>, ListChunk<TElement>> _hashLookup;

			// Token: 0x0400070C RID: 1804
			internal int _hashLookupIndex;
		}
	}
}
