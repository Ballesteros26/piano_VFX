using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000FD RID: 253
	internal interface IPartition<TElement> : IIListProvider<TElement>, IEnumerable<TElement>, IEnumerable
	{
		// Token: 0x060008D2 RID: 2258
		IPartition<TElement> Skip(int count);

		// Token: 0x060008D3 RID: 2259
		IPartition<TElement> Take(int count);

		// Token: 0x060008D4 RID: 2260
		TElement TryGetElementAt(int index, out bool found);

		// Token: 0x060008D5 RID: 2261
		TElement TryGetFirst(out bool found);

		// Token: 0x060008D6 RID: 2262
		TElement TryGetLast(out bool found);
	}
}
