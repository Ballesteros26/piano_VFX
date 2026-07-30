using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000EA RID: 234
	internal sealed class GroupedResultEnumerable<TSource, TKey, TElement, TResult> : IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
	{
		// Token: 0x0600084F RID: 2127 RVA: 0x0001B258 File Offset: 0x00019458
		public GroupedResultEnumerable(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			this._source = source;
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			this._keySelector = keySelector;
			if (elementSelector == null)
			{
				throw Error.ArgumentNull("elementSelector");
			}
			this._elementSelector = elementSelector;
			this._comparer = comparer;
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			this._resultSelector = resultSelector;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001B2CC File Offset: 0x000194CC
		public IEnumerator<TResult> GetEnumerator()
		{
			return Lookup<TKey, TElement>.Create<TSource>(this._source, this._keySelector, this._elementSelector, this._comparer).ApplyResultSelector<TResult>(this._resultSelector).GetEnumerator();
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001B2FB File Offset: 0x000194FB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001B303 File Offset: 0x00019503
		public TResult[] ToArray()
		{
			return Lookup<TKey, TElement>.Create<TSource>(this._source, this._keySelector, this._elementSelector, this._comparer).ToArray<TResult>(this._resultSelector);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001B32D File Offset: 0x0001952D
		public List<TResult> ToList()
		{
			return Lookup<TKey, TElement>.Create<TSource>(this._source, this._keySelector, this._elementSelector, this._comparer).ToList<TResult>(this._resultSelector);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001B357 File Offset: 0x00019557
		public int GetCount(bool onlyIfCheap)
		{
			if (!onlyIfCheap)
			{
				return Lookup<TKey, TElement>.Create<TSource>(this._source, this._keySelector, this._elementSelector, this._comparer).Count;
			}
			return -1;
		}

		// Token: 0x040004EA RID: 1258
		private readonly IEnumerable<TSource> _source;

		// Token: 0x040004EB RID: 1259
		private readonly Func<TSource, TKey> _keySelector;

		// Token: 0x040004EC RID: 1260
		private readonly Func<TSource, TElement> _elementSelector;

		// Token: 0x040004ED RID: 1261
		private readonly IEqualityComparer<TKey> _comparer;

		// Token: 0x040004EE RID: 1262
		private readonly Func<TKey, IEnumerable<TElement>, TResult> _resultSelector;
	}
}
