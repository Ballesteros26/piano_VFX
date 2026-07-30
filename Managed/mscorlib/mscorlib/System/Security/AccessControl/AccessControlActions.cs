using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the actions that are permitted for securable objects.</summary>
	// Token: 0x020005C2 RID: 1474
	[Flags]
	public enum AccessControlActions
	{
		/// <summary>Specifies no access.</summary>
		// Token: 0x0400210E RID: 8462
		None = 0,
		/// <summary>Specifies read-only access.</summary>
		// Token: 0x0400210F RID: 8463
		View = 1,
		/// <summary>Specifies write-only access.</summary>
		// Token: 0x04002110 RID: 8464
		Change = 2
	}
}
