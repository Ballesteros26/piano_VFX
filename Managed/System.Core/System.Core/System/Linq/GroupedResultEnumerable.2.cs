using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000EB RID: 235
	internal sealed class GroupedResultEnumerable<TSource, TKey, TResult> : IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
	{
		// Token: 0x06000855 RID: 2133 RVA: 0x0001B380 File Offset: 0x00019580
		public GroupedResultEnumerable(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
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
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			this._resultSelector = resultSelector;
			this._comparer = comparer;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001B3DD File Offset: 0x000195DD
		public IEnumerator<TResult> GetEnumerator()
		{
			return Lookup<TKey, TSource>.Create(this._source, this._keySelector, this._comparer).ApplyResultSelector<TResult>(this._resultSelector).GetEnumerator();
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001B406 File Offset: 0x00019606
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0001B40E File Offset: 0x0001960E
		public TResult[] ToArray()
		{
			return Lookup<TKey, TSource>.Create(this._source, this._keySelector, this._comparer).ToArray<TResult>(this._resultSelector);
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001B432 File Offset: 0x00019632
		public List<TResult> ToList()
		{
			return Lookup<TKey, TSource>.Create(this._source, this._keySelector, this._comparer).ToList<TResult>(this._resultSelector);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001B456 File Offset: 0x00019656
		public int GetCount(bool onlyIfCheap)
		{
			if (!onlyIfCheap)
			{
				return Lookup<TKey, TSource>.Create(this._source, this._keySelector, this._comparer).Count;
			}
			return -1;
		}

		// Token: 0x040004EF RID: 1263
		private readonly IEnumerable<TSource> _source;

		// Token: 0x040004F0 RID: 1264
		private readonly Func<TSource, TKey> _keySelector;

		// Token: 0x040004F1 RID: 1265
		private readonly IEqualityComparer<TKey> _comparer;

		// Token: 0x040004F2 RID: 1266
		private readonly Func<TKey, IEnumerable<TSource>, TResult> _resultSelector;
	}
}
