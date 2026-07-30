using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Specifies the context type for an <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object.</summary>
	// Token: 0x0200004F RID: 79
	public enum DirectoryContextType
	{
		/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object represents a domain.</summary>
		// Token: 0x040000E1 RID: 225
		Domain,
		/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object represents a forest.</summary>
		// Token: 0x040000E2 RID: 226
		Forest,
		/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object represents a directory server.</summary>
		// Token: 0x040000E3 RID: 227
		DirectoryServer,
		/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object represents an AD LDS configuration set.</summary>
		// Token: 0x040000E4 RID: 228
		ConfigurationSet,
		/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object represents an application partition.</summary>
		// Token: 0x040000E5 RID: 229
		ApplicationPartition
	}
}
