using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the inheritance and auditing behavior of an access control entry (ACE).</summary>
	// Token: 0x020005C9 RID: 1481
	[Flags]
	public enum AceFlags : byte
	{
		/// <summary>No ACE flags are set.</summary>
		// Token: 0x04002126 RID: 8486
		None = 0,
		/// <summary>The access mask is propagated onto child leaf objects.</summary>
		// Token: 0x04002127 RID: 8487
		ObjectInherit = 1,
		/// <summary>The access mask is propagated to child container objects.</summary>
		// Token: 0x04002128 RID: 8488
		ContainerInherit = 2,
		/// <summary>The access checks do not apply to the object; they only apply to its children.</summary>
		// Token: 0x04002129 RID: 8489
		NoPropagateInherit = 4,
		/// <summary>The access mask is propagated only to child objects. This includes both container and leaf child objects.</summary>
		// Token: 0x0400212A RID: 8490
		InheritOnly = 8,
		/// <summary>A logical OR of <see cref="F:System.Security.AccessControl.AceFlags.ObjectInherit" />, <see cref="F:System.Security.AccessControl.AceFlags.ContainerInherit" />, <see cref="F:System.Security.AccessControl.AceFlags.NoPropagateInherit" />, and <see cref="F:System.Security.AccessControl.AceFlags.InheritOnly" />.</summary>
		// Token: 0x0400212B RID: 8491
		InheritanceFlags = 15,
		/// <summary>An ACE is inherited from a parent container rather than being explicitly set for an object.</summary>
		// Token: 0x0400212C RID: 8492
		Inherited = 16,
		/// <summary>Successful access attempts are audited.</summary>
		// Token: 0x0400212D RID: 8493
		SuccessfulAccess = 64,
		/// <summary>Failed access attempts are audited.</summary>
		// Token: 0x0400212E RID: 8494
		FailedAccess = 128,
		/// <summary>All access attempts are audited.</summary>
		// Token: 0x0400212F RID: 8495
		AuditFlags = 192
	}
}
