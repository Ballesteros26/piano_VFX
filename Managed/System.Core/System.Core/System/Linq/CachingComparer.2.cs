using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000F8 RID: 248
	internal class CachingComparer<TElement, TKey> : CachingComparer<TElement>
	{
		// Token: 0x060008B8 RID: 2232 RVA: 0x0001C5D4 File Offset: 0x0001A7D4
		public CachingComparer(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
		{
			this._keySelector = keySelector;
			this._comparer = comparer;
			this._descending = descending;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0001C5F4 File Offset: 0x0001A7F4
		internal override int Compare(TElement element, bool cacheLower)
		{
			TKey tkey = this._keySelector(element);
			int num = (this._descending ? this._comparer.Compare(this._lastKey, tkey) : this._comparer.Compare(tkey, this._lastKey));
			if (cacheLower == num < 0)
			{
				this._lastKey = tkey;
			}
			return num;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001C64C File Offset: 0x0001A84C
		internal override void SetElement(TElement element)
		{
			this._lastKey = this._keySelector(element);
		}

		// Token: 0x0400051B RID: 1307
		protected readonly Func<TElement, TKey> _keySelector;

		// Token: 0x0400051C RID: 1308
		protected readonly IComparer<TKey> _comparer;

		// Token: 0x0400051D RID: 1309
		protected readonly bool _descending;

		// Token: 0x0400051E RID: 1310
		protected TKey _lastKey;
	}
}
