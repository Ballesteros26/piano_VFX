using System;

namespace System.Web.Util
{
	// Token: 0x0200011E RID: 286
	internal interface ISyncContext
	{
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06000E1A RID: 3610
		HttpContext HttpContext { get; }

		// Token: 0x06000E1B RID: 3611
		ISyncContextLock Enter();
	}
}
