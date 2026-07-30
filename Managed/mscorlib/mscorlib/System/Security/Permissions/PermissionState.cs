using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies whether a permission should have all or no access to resources at creation.</summary>
	// Token: 0x020005A3 RID: 1443
	[ComVisible(true)]
	[Serializable]
	public enum PermissionState
	{
		/// <summary>Full access to the resource protected by the permission.</summary>
		// Token: 0x0400209E RID: 8350
		Unrestricted = 1,
		/// <summary>No access to the resource protected by the permission.</summary>
		// Token: 0x0400209F RID: 8351
		None = 0
	}
}
