using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000FC RID: 252
	internal interface IIListProvider<TElement> : IEnumerable<TElement>, IEnumerable
	{
		// Token: 0x060008CF RID: 2255
		TElement[] ToArray();

		// Token: 0x060008D0 RID: 2256
		List<TElement> ToList();

		// Token: 0x060008D1 RID: 2257
		int GetCount(bool onlyIfCheap);
	}
}
