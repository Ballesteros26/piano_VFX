using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001BB RID: 443
	internal sealed class GroupByElementSelectorQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey> : GroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>
	{
		// Token: 0x06000BC5 RID: 3013 RVA: 0x0002718C File Offset: 0x0002538C
		internal GroupByElementSelectorQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TSource, TGroupKey>, TOrderKey> source, IEqualityComparer<TGroupKey> keyComparer, Func<TSource, TElement> elementSelector, CancellationToken cancellationToken)
			: base(source, keyComparer, cancellationToken)
		{
			this._elementSelector = elementSelector;
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x000271A0 File Offset: 0x000253A0
		protected override HashLookup<Wrapper<TGroupKey>, ListChunk<TElement>> BuildHashLookup()
		{
			HashLookup<Wrapper<TGroupKey>, ListChunk<TElement>> hashLookup = new HashLookup<Wrapper<TGroupKey>, ListChunk<TElement>>(new WrapperEqualityComparer<TGroupKey>(this._keyComparer));
			Pair<TSource, TGroupKey> pair = default(Pair<TSource, TGroupKey>);
			TOrderKey torderKey = default(TOrderKey);
			int num = 0;
			while (this._source.MoveNext(ref pair, ref torderKey))
			{
				if ((num++ & 63) == 0)
				{
					CancellationState.ThrowIfCanceled(this._cancellationToken);
				}
				Wrapper<TGroupKey> wrapper = new Wrapper<TGroupKey>(pair.Second);
				ListChunk<TElement> listChunk = null;
				if (!hashLookup.TryGetValue(wrapper, ref listChunk))
				{
					listChunk = new ListChunk<TElement>(2);
					hashLookup.Add(wrapper, listChunk);
				}
				listChunk.Add(this._elementSelector(pair.First));
			}
			return hashLookup;
		}

		// Token: 0x0400070D RID: 1805
		private readonly Func<TSource, TElement> _elementSelector;
	}
}
