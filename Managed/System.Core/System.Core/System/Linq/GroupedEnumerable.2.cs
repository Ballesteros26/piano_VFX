using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000ED RID: 237
	internal sealed class GroupedEnumerable<TSource, TKey> : IIListProvider<IGrouping<TKey, TSource>>, IEnumerable<IGrouping<TKey, TSource>>, IEnumerable
	{
		// Token: 0x06000861 RID: 2145 RVA: 0x0001B576 File Offset: 0x00019776
		public GroupedEnumerable(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
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
			this._comparer = comparer;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0001B5B1 File Offset: 0x000197B1
		public IEnumerator<IGrouping<TKey, TSource>> GetEnumerator()
		{
			return Lookup<TKey, TSource>.Create(this._source, this._keySelector, this._comparer).GetEnumerator();
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001B5CF File Offset: 0x000197CF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0001B5D7 File Offset: 0x000197D7
		public IGrouping<TKey, TSource>[] ToArray()
		{
			return ((IIListProvider<IGrouping<TKey, TSource>>)Lookup<TKey, TSource>.Create(this._source, this._keySelector, this._comparer)).ToArray();
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0001B5F5 File Offset: 0x000197F5
		public List<IGrouping<TKey, TSource>> ToList()
		{
			return ((IIListProvider<IGrouping<TKey, TSource>>)Lookup<TKey, TSource>.Create(this._source, this._keySelector, this._comparer)).ToList();
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0001B613 File Offset: 0x00019813
		public int GetCount(bool onlyIfCheap)
		{
			if (!onlyIfCheap)
			{
				return Lookup<TKey, TSource>.Create(this._source, this._keySelector, this._comparer).Count;
			}
			return -1;
		}

		// Token: 0x040004F7 RID: 1271
		private readonly IEnumerable<TSource> _source;

		// Token: 0x040004F8 RID: 1272
		private readonly Func<TSource, TKey> _keySelector;

		// Token: 0x040004F9 RID: 1273
		private readonly IEqualityComparer<TKey> _comparer;
	}
}
