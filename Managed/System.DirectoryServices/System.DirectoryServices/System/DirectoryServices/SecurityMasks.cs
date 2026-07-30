using System;

namespace System.DirectoryServices
{
	/// <summary>Specifies the available options for examining security information of a directory object. This enumeration is used with the <see cref="P:System.DirectoryServices.DirectorySearcher.SecurityMasks" /> and <see cref="P:System.DirectoryServices.DirectoryEntryConfiguration.SecurityMasks" /> properties.          </summary>
	// Token: 0x0200000C RID: 12
	[Flags]
	public enum SecurityMasks
	{
		/// <summary>Does not read or write security data.</summary>
		// Token: 0x0400002C RID: 44
		None = 0,
		/// <summary>Reads or writes the owner data.</summary>
		// Token: 0x0400002D RID: 45
		Owner = 1,
		/// <summary>Reads or writes the group data.</summary>
		// Token: 0x0400002E RID: 46
		Group = 2,
		/// <summary>Reads or writes the discretionary access-control list (DACL) data. </summary>
		// Token: 0x0400002F RID: 47
		Dacl = 4,
		/// <summary>Reads or writes the system access-control list (SACL) data.</summary>
		// Token: 0x04000030 RID: 48
		Sacl = 8
	}
}
