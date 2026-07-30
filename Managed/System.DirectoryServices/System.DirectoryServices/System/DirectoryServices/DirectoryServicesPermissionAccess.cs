using System;

namespace System.DirectoryServices
{
	/// <summary>The <see cref="T:System.DirectoryServices.DirectoryServicesPermissionAccess" /> enumeration defines access levels that are used by <see cref="N:System.DirectoryServices" /> permission classes. This enumeration has a <see cref="T:System.FlagsAttribute" /> attribute that allows a bitwise combination of its member values.</summary>
	// Token: 0x02000018 RID: 24
	[Flags]
	[Serializable]
	public enum DirectoryServicesPermissionAccess
	{
		/// <summary>No permissions are allowed.</summary>
		// Token: 0x04000080 RID: 128
		None = 0,
		/// <summary>Reading the Active Directory Domain Services tree is allowed.</summary>
		// Token: 0x04000081 RID: 129
		Browse = 2,
		/// <summary>Reading, writing, deleting, changing, and adding to the Active Directory Domain Srevices tree are allowed.</summary>
		// Token: 0x04000082 RID: 130
		Write = 6
	}
}
