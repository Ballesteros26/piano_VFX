using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Specifies search flags for finding a domain controller in a domain.</summary>
	// Token: 0x02000065 RID: 101
	[Flags]
	public enum LocatorOptions : long
	{
		/// <summary>Forces cached domain controller data to be ignored when searching for domain controllers.</summary>
		// Token: 0x04000123 RID: 291
		ForceRediscovery = 1L,
		/// <summary>Search only for domain controllers that are currently running the Kerberos Key Distribution Center service.</summary>
		// Token: 0x04000124 RID: 292
		KdcRequired = 1024L,
		/// <summary>Search only for domain controllers that are currently running the Windows Time service.</summary>
		// Token: 0x04000125 RID: 293
		TimeServerRequired = 2048L,
		/// <summary>Search only for writeable domain controllers.</summary>
		// Token: 0x04000126 RID: 294
		WriteableRequired = 4096L,
		/// <summary>When searching for domain controllers from a domain controller, exclude this domain controller from the search. If the current computer is not a domain controller, this flag is ignored.</summary>
		// Token: 0x04000127 RID: 295
		AvoidSelf = 16384L
	}
}
