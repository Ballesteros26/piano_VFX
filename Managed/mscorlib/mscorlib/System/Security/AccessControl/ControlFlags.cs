using System;

namespace System.Security.AccessControl
{
	/// <summary>These flags affect the security descriptor behavior.</summary>
	// Token: 0x020005DD RID: 1501
	[Flags]
	public enum ControlFlags
	{
		/// <summary>No control flags.</summary>
		// Token: 0x04002175 RID: 8565
		None = 0,
		/// <summary>Specifies that the owner <see cref="T:System.Security.Principal.SecurityIdentifier" /> was obtained by a defaulting mechanism. Set by resource managers only; should not be set by callers.  </summary>
		// Token: 0x04002176 RID: 8566
		OwnerDefaulted = 1,
		/// <summary>Specifies that the group <see cref="T:System.Security.Principal.SecurityIdentifier" /> was obtained by a defaulting mechanism. Set by resource managers only; should not be set by callers.</summary>
		// Token: 0x04002177 RID: 8567
		GroupDefaulted = 2,
		/// <summary>Specifies that the DACL is not null. Set by resource managers or users.  </summary>
		// Token: 0x04002178 RID: 8568
		DiscretionaryAclPresent = 4,
		/// <summary>Specifies that the DACL was obtained by a defaulting mechanism. Set by resource managers only.</summary>
		// Token: 0x04002179 RID: 8569
		DiscretionaryAclDefaulted = 8,
		/// <summary>Specifies that the SACL is not null. Set by resource managers or users.</summary>
		// Token: 0x0400217A RID: 8570
		SystemAclPresent = 16,
		/// <summary>Specifies that the SACL was obtained by a defaulting mechanism. Set by resource managers only.</summary>
		// Token: 0x0400217B RID: 8571
		SystemAclDefaulted = 32,
		/// <summary>Ignored.</summary>
		// Token: 0x0400217C RID: 8572
		DiscretionaryAclUntrusted = 64,
		/// <summary>Ignored.</summary>
		// Token: 0x0400217D RID: 8573
		ServerSecurity = 128,
		/// <summary>Ignored.</summary>
		// Token: 0x0400217E RID: 8574
		DiscretionaryAclAutoInheritRequired = 256,
		/// <summary>Ignored.</summary>
		// Token: 0x0400217F RID: 8575
		SystemAclAutoInheritRequired = 512,
		/// <summary>Specifies that the Discretionary Access Control List (DACL) has been automatically inherited from the parent. Set by resource managers only.</summary>
		// Token: 0x04002180 RID: 8576
		DiscretionaryAclAutoInherited = 1024,
		/// <summary>Specifies that the System Access Control List (SACL) has been automatically inherited from the parent. Set by resource managers only.</summary>
		// Token: 0x04002181 RID: 8577
		SystemAclAutoInherited = 2048,
		/// <summary>Specifies that the resource manager prevents auto-inheritance. Set by resource managers or users.  </summary>
		// Token: 0x04002182 RID: 8578
		DiscretionaryAclProtected = 4096,
		/// <summary>Specifies that the resource manager prevents auto-inheritance. Set by resource managers or users.</summary>
		// Token: 0x04002183 RID: 8579
		SystemAclProtected = 8192,
		/// <summary>Specifies that the contents of the Reserved field are valid.</summary>
		// Token: 0x04002184 RID: 8580
		RMControlValid = 16384,
		/// <summary>Specifies that the security descriptor binary representation is in the self-relative format.  This flag is always set.</summary>
		// Token: 0x04002185 RID: 8581
		SelfRelative = 32768
	}
}
