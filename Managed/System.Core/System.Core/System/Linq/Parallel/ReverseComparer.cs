using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000211 RID: 529
	internal class ReverseComparer<T> : IComparer<T>
	{
		// Token: 0x06000D23 RID: 3363 RVA: 0x0002BA0D File Offset: 0x00029C0D
		internal ReverseComparer(IComparer<T> comparer)
		{
			this._comparer = comparer;
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0002BA1C File Offset: 0x00029C1C
		public int Compare(T x, T y)
		{
			return this._comparer.Compare(y, x);
		}

		// Token: 0x0400082F RID: 2095
		private IComparer<T> _comparer;
	}
}
