using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Indicates specific roles of a <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> object.</summary>
	// Token: 0x02000098 RID: 152
	public enum AdamRole
	{
		/// <summary>The AD LDS server holds the domain naming master role.</summary>
		// Token: 0x04000183 RID: 387
		NamingRole = 1,
		/// <summary>The AD LDS server holds the schema operations master role.</summary>
		// Token: 0x04000184 RID: 388
		SchemaRole = 0
	}
}
