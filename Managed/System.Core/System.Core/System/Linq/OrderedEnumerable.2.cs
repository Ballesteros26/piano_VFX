using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000F6 RID: 246
	internal sealed class OrderedEnumerable<TElement, TKey> : OrderedEnumerable<TElement>
	{
		// Token: 0x060008B2 RID: 2226 RVA: 0x0001C4E0 File Offset: 0x0001A6E0
		internal OrderedEnumerable(IEnumerable<TElement> source, Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending, OrderedEnumerable<TElement> parent)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			this._source = source;
			this._parent = parent;
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			this._keySelector = keySelector;
			this._comparer = comparer ?? Comparer<TKey>.Default;
			this._descending = descending;
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0001C540 File Offset: 0x0001A740
		internal override EnumerableSorter<TElement> GetEnumerableSorter(EnumerableSorter<TElement> next)
		{
			EnumerableSorter<TElement> enumerableSorter = new EnumerableSorter<TElement, TKey>(this._keySelector, this._comparer, this._descending, next);
			if (this._parent != null)
			{
				enumerableSorter = this._parent.GetEnumerableSorter(enumerableSorter);
			}
			return enumerableSorter;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0001C57C File Offset: 0x0001A77C
		internal override CachingComparer<TElement> GetComparer(CachingComparer<TElement> childComparer)
		{
			CachingComparer<TElement> cachingComparer = ((childComparer == null) ? new CachingComparer<TElement, TKey>(this._keySelector, this._comparer, this._descending) : new CachingComparerWithChild<TElement, TKey>(this._keySelector, this._comparer, this._descending, childComparer));
			if (this._parent == null)
			{
				return cachingComparer;
			}
			return this._parent.GetComparer(cachingComparer);
		}

		// Token: 0x04000517 RID: 1303
		private readonly OrderedEnumerable<TElement> _parent;

		// Token: 0x04000518 RID: 1304
		private readonly Func<TElement, TKey> _keySelector;

		// Token: 0x04000519 RID: 1305
		private readonly IComparer<TKey> _comparer;

		// Token: 0x0400051A RID: 1306
		private readonly bool _descending;
	}
}
