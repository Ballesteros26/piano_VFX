using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000F9 RID: 249
	internal sealed class CachingComparerWithChild<TElement, TKey> : CachingComparer<TElement, TKey>
	{
		// Token: 0x060008BB RID: 2235 RVA: 0x0001C660 File Offset: 0x0001A860
		public CachingComparerWithChild(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending, CachingComparer<TElement> child)
			: base(keySelector, comparer, descending)
		{
			this._child = child;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0001C674 File Offset: 0x0001A874
		internal override int Compare(TElement element, bool cacheLower)
		{
			TKey tkey = this._keySelector(element);
			int num = (this._descending ? this._comparer.Compare(this._lastKey, tkey) : this._comparer.Compare(tkey, this._lastKey));
			if (num == 0)
			{
				return this._child.Compare(element, cacheLower);
			}
			if (cacheLower == num < 0)
			{
				this._lastKey = tkey;
				this._child.SetElement(element);
			}
			return num;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0001C6E9 File Offset: 0x0001A8E9
		internal override void SetElement(TElement element)
		{
			base.SetElement(element);
			this._child.SetElement(element);
		}

		// Token: 0x0400051F RID: 1311
		private readonly CachingComparer<TElement> _child;
	}
}
