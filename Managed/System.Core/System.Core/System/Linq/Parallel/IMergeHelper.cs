using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x0200011B RID: 283
	internal interface IMergeHelper<TInputOutput>
	{
		// Token: 0x06000962 RID: 2402
		void Execute();

		// Token: 0x06000963 RID: 2403
		IEnumerator<TInputOutput> GetEnumerator();

		// Token: 0x06000964 RID: 2404
		TInputOutput[] GetResultsAsArray();
	}
}
