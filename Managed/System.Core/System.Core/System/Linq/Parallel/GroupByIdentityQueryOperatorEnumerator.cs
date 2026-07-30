using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001BA RID: 442
	internal sealed class GroupByIdentityQueryOperatorEnumerator<TSource, TGroupKey, TOrderKey> : GroupByQueryOperatorEnumerator<TSource, TGroupKey, TSource, TOrderKey>
	{
		// Token: 0x06000BC3 RID: 3011 RVA: 0x000270E3 File Offset: 0x000252E3
		internal GroupByIdentityQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TSource, TGroupKey>, TOrderKey> source, IEqualityComparer<TGroupKey> keyComparer, CancellationToken cancellationToken)
			: base(source, keyComparer, cancellationToken)
		{
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x000270F0 File Offset: 0x000252F0
		protected override HashLookup<Wrapper<TGroupKey>, ListChunk<TSource>> BuildHashLookup()
		{
			HashLookup<Wrapper<TGroupKey>, ListChunk<TSource>> hashLookup = new HashLookup<Wrapper<TGroupKey>, ListChunk<TSource>>(new WrapperEqualityComparer<TGroupKey>(this._keyComparer));
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
				ListChunk<TSource> listChunk = null;
				if (!hashLookup.TryGetValue(wrapper, ref listChunk))
				{
					listChunk = new ListChunk<TSource>(2);
					hashLookup.Add(wrapper, listChunk);
				}
				listChunk.Add(pair.First);
			}
			return hashLookup;
		}
	}
}
