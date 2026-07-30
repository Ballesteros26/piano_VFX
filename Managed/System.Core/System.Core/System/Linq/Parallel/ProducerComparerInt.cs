using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000123 RID: 291
	internal class ProducerComparerInt : IComparer<Producer<int>>
	{
		// Token: 0x06000985 RID: 2437 RVA: 0x0001E9C4 File Offset: 0x0001CBC4
		public int Compare(Producer<int> x, Producer<int> y)
		{
			return y.MaxKey - x.MaxKey;
		}
	}
}
