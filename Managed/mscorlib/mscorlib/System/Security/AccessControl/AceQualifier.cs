using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the function of an access control entry (ACE).</summary>
	// Token: 0x020005CA RID: 1482
	public enum AceQualifier
	{
		/// <summary>Allow access.</summary>
		// Token: 0x04002131 RID: 8497
		AccessAllowed,
		/// <summary>Deny access.</summary>
		// Token: 0x04002132 RID: 8498
		AccessDenied,
		/// <summary>Cause a system audit.</summary>
		// Token: 0x04002133 RID: 8499
		SystemAudit,
		/// <summary>Cause a system alarm.</summary>
		// Token: 0x04002134 RID: 8500
		SystemAlarm
	}
}
