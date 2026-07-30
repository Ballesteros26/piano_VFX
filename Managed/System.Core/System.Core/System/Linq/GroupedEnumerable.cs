using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000EC RID: 236
	internal sealed class GroupedEnumerable<TSource, TKey, TElement> : IIListProvider<IGrouping<TKey, TElement>>, IEnumerable<IGrouping<TKey, TElement>>, IEnumerable
	{
		// Token: 0x0600085B RID: 2139 RVA: 0x0001B47C File Offset: 0x0001967C
		public GroupedEnumerable(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
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
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001B4D9 File Offset: 0x000196D9
		public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
		{
			return Lookup<TKey, TElement>.Create<TSource>(this._source, this._keySelector, this._elementSelector, this._comparer).GetEnumerator();
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001B4FD File Offset: 0x000196FD
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001B505 File Offset: 0x00019705
		public IGrouping<TKey, TElement>[] ToArray()
		{
			return ((IIListProvider<IGrouping<TKey, TElement>>)Lookup<TKey, TElement>.Create<TSource>(this._source, this._keySelector, this._elementSelector, this._comparer)).ToArray();
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0001B529 File Offset: 0x00019729
		public List<IGrouping<TKey, TElement>> ToList()
		{
			return ((IIListProvider<IGrouping<TKey, TElement>>)Lookup<TKey, TElement>.Create<TSource>(this._source, this._keySelector, this._elementSelector, this._comparer)).ToList();
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0001B54D File Offset: 0x0001974D
		public int GetCount(bool onlyIfCheap)
		{
			if (!onlyIfCheap)
			{
				return Lookup<TKey, TElement>.Create<TSource>(this._source, this._keySelector, this._elementSelector, this._comparer).Count;
			}
			return -1;
		}

		// Token: 0x040004F3 RID: 1267
		private readonly IEnumerable<TSource> _source;

		// Token: 0x040004F4 RID: 1268
		private readonly Func<TSource, TKey> _keySelector;

		// Token: 0x040004F5 RID: 1269
		private readonly Func<TSource, TElement> _elementSelector;

		// Token: 0x040004F6 RID: 1270
		private readonly IEqualityComparer<TKey> _comparer;
	}
}
