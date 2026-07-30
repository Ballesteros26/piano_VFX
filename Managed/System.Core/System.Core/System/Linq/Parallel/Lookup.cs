using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x0200020B RID: 523
	internal class Lookup<TKey, TElement> : ILookup<TKey, TElement>, IEnumerable<IGrouping<TKey, TElement>>, IEnumerable
	{
		// Token: 0x06000D02 RID: 3330 RVA: 0x0002B59F File Offset: 0x0002979F
		internal Lookup(IEqualityComparer<TKey> comparer)
		{
			this._comparer = comparer;
			this._dict = new Dictionary<TKey, IGrouping<TKey, TElement>>(this._comparer);
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x0002B5C0 File Offset: 0x000297C0
		public int Count
		{
			get
			{
				int num = this._dict.Count;
				if (this._defaultKeyGrouping != null)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x170001AB RID: 427
		public IEnumerable<TElement> this[TKey key]
		{
			get
			{
				if (this._comparer.Equals(key, default(TKey)))
				{
					if (this._defaultKeyGrouping != null)
					{
						return this._defaultKeyGrouping;
					}
					return Enumerable.Empty<TElement>();
				}
				else
				{
					IGrouping<TKey, TElement> grouping;
					if (this._dict.TryGetValue(key, out grouping))
					{
						return grouping;
					}
					return Enumerable.Empty<TElement>();
				}
			}
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0002B638 File Offset: 0x00029838
		public bool Contains(TKey key)
		{
			if (this._comparer.Equals(key, default(TKey)))
			{
				return this._defaultKeyGrouping != null;
			}
			return this._dict.ContainsKey(key);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0002B674 File Offset: 0x00029874
		internal void Add(IGrouping<TKey, TElement> grouping)
		{
			if (this._comparer.Equals(grouping.Key, default(TKey)))
			{
				this._defaultKeyGrouping = grouping;
				return;
			}
			this._dict.Add(grouping.Key, grouping);
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0002B6B7 File Offset: 0x000298B7
		public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
		{
			foreach (IGrouping<TKey, TElement> grouping in this._dict.Values)
			{
				yield return grouping;
			}
			IEnumerator<IGrouping<TKey, TElement>> enumerator = null;
			if (this._defaultKeyGrouping != null)
			{
				yield return this._defaultKeyGrouping;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0002B6C6 File Offset: 0x000298C6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<IGrouping<TKey, TElement>>)this).GetEnumerator();
		}

		// Token: 0x0400081A RID: 2074
		private IDictionary<TKey, IGrouping<TKey, TElement>> _dict;

		// Token: 0x0400081B RID: 2075
		private IEqualityComparer<TKey> _comparer;

		// Token: 0x0400081C RID: 2076
		private IGrouping<TKey, TElement> _defaultKeyGrouping;
	}
}
