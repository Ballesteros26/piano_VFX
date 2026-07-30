using System;

namespace System.Threading
{
	// Token: 0x0200044A RID: 1098
	internal interface IDeferredDisposable
	{
		// Token: 0x060034A0 RID: 13472
		void OnFinalRelease(bool disposed);
	}
}
