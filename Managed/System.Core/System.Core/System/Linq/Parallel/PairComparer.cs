using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000210 RID: 528
	internal sealed class PairComparer<T, U> : IComparer<Pair<T, U>>
	{
		// Token: 0x06000D21 RID: 3361 RVA: 0x0002B9B2 File Offset: 0x00029BB2
		public PairComparer(IComparer<T> comparer1, IComparer<U> comparer2)
		{
			this._comparer1 = comparer1;
			this._comparer2 = comparer2;
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0002B9C8 File Offset: 0x00029BC8
		public int Compare(Pair<T, U> x, Pair<T, U> y)
		{
			int num = this._comparer1.Compare(x.First, y.First);
			if (num != 0)
			{
				return num;
			}
			return this._comparer2.Compare(x.Second, y.Second);
		}

		// Token: 0x0400082D RID: 2093
		private readonly IComparer<T> _comparer1;

		// Token: 0x0400082E RID: 2094
		private readonly IComparer<U> _comparer2;
	}
}
