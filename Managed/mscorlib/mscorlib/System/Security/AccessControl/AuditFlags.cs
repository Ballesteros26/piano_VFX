using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the conditions for auditing attempts to access a securable object.</summary>
	// Token: 0x020005CC RID: 1484
	[Flags]
	public enum AuditFlags
	{
		/// <summary>No access attempts are to be audited.</summary>
		// Token: 0x04002149 RID: 8521
		None = 0,
		/// <summary>Successful access attempts are to be audited.</summary>
		// Token: 0x0400214A RID: 8522
		Success = 1,
		/// <summary>Failed access attempts are to be audited.</summary>
		// Token: 0x0400214B RID: 8523
		Failure = 2
	}
}
