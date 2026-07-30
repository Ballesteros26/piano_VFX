using System;
using System.Security.Principal;

namespace System.Web
{
	// Token: 0x0200004B RID: 75
	internal interface IPrincipalContainer
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060003CC RID: 972
		// (set) Token: 0x060003CD RID: 973
		IPrincipal Principal { get; set; }
	}
}
