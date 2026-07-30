using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Identifies specific roles within a domain.</summary>
	// Token: 0x02000037 RID: 55
	public enum ActiveDirectoryRole
	{
		/// <summary>Identifies the schema master role.</summary>
		// Token: 0x040000B3 RID: 179
		SchemaRole,
		/// <summary>Identifies the domain naming master role.</summary>
		// Token: 0x040000B4 RID: 180
		NamingRole,
		/// <summary>Identifies the primary domain controller (PDC) emulator role.</summary>
		// Token: 0x040000B5 RID: 181
		PdcRole,
		/// <summary>Identifies the relative identifier (RID) master role.</summary>
		// Token: 0x040000B6 RID: 182
		RidRole,
		/// <summary>Identifies the infrastructure role.</summary>
		// Token: 0x040000B7 RID: 183
		InfrastructureRole
	}
}
