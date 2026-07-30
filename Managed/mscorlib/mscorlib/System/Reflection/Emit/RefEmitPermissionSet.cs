using System;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x02000344 RID: 836
	internal struct RefEmitPermissionSet
	{
		// Token: 0x060024EF RID: 9455 RVA: 0x0008497A File Offset: 0x00082B7A
		public RefEmitPermissionSet(SecurityAction action, string pset)
		{
			this.action = action;
			this.pset = pset;
		}

		// Token: 0x04001388 RID: 5000
		public SecurityAction action;

		// Token: 0x04001389 RID: 5001
		public string pset;
	}
}
