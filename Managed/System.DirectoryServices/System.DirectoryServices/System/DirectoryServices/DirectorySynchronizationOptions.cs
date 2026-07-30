using System;

namespace System.DirectoryServices
{
	/// <summary>Contains flags that determine how directories within a domain will be synchronized.  These options can be set for the <see cref="P:System.DirectoryServices.DirectorySynchronization.Option" /> property.</summary>
	// Token: 0x0200001D RID: 29
	[Flags]
	public enum DirectorySynchronizationOptions : long
	{
		/// <summary>No flags are set.</summary>
		// Token: 0x04000089 RID: 137
		None = 0L,
		/// <summary>If this flag is not present, the caller must have the right to replicate changes. If this flag is present, the caller requires no rights, but is allowed to see only objects and attributes that are accessible to the caller.</summary>
		// Token: 0x0400008A RID: 138
		ObjectSecurity = 1L,
		/// <summary>Return parents before children, when parents would otherwise appear later in the replication stream.</summary>
		// Token: 0x0400008B RID: 139
		ParentsFirst = 2048L,
		/// <summary>Do not return private data in the search results.</summary>
		// Token: 0x0400008C RID: 140
		PublicDataOnly = 8192L,
		/// <summary>If this flag is not present, all of the values, up to a server-specified limit, in a multi-valued attribute are returned when any value changes. If this flag is present, only the changed values are returned.</summary>
		// Token: 0x0400008D RID: 141
		IncrementalValues = 2147483648L
	}
}
