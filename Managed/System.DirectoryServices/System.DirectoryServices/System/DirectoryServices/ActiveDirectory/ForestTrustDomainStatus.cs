using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Indicates the status of a forest trust relationship.</summary>
	// Token: 0x0200005E RID: 94
	public enum ForestTrustDomainStatus
	{
		/// <summary>The forest trust relationship is enabled.</summary>
		// Token: 0x04000104 RID: 260
		Enabled,
		/// <summary>The forest trust SID is disabled by administrative action.</summary>
		// Token: 0x04000105 RID: 261
		SidAdminDisabled,
		/// <summary>The forest trust SID is disabled due to a conflict with an existing SID.</summary>
		// Token: 0x04000106 RID: 262
		SidConflictDisabled,
		/// <summary>The forest trust NetBIOS record is disabled by administrative action.</summary>
		// Token: 0x04000107 RID: 263
		NetBiosNameAdminDisabled = 4,
		/// <summary>The forest trust NetBIOS record is disabled due to a conflict with an existing NetBIOS record.</summary>
		// Token: 0x04000108 RID: 264
		NetBiosNameConflictDisabled = 8
	}
}
