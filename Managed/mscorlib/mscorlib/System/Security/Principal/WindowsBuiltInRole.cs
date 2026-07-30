using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Specifies common roles to be used with <see cref="M:System.Security.Principal.WindowsPrincipal.IsInRole(System.String)" />.</summary>
	// Token: 0x0200062D RID: 1581
	[ComVisible(true)]
	[Serializable]
	public enum WindowsBuiltInRole
	{
		/// <summary>Administrators have complete and unrestricted access to the computer or domain.</summary>
		// Token: 0x040022F5 RID: 8949
		Administrator = 544,
		/// <summary>Users are prevented from making accidental or intentional system-wide changes. Thus, users can run certified applications, but not most legacy applications.</summary>
		// Token: 0x040022F6 RID: 8950
		User,
		/// <summary>Guests are more restricted than users.</summary>
		// Token: 0x040022F7 RID: 8951
		Guest,
		/// <summary>Power users possess most administrative permissions with some restrictions. Thus, power users can run legacy applications, in addition to certified applications.</summary>
		// Token: 0x040022F8 RID: 8952
		PowerUser,
		/// <summary>Account operators manage the user accounts on a computer or domain.</summary>
		// Token: 0x040022F9 RID: 8953
		AccountOperator,
		/// <summary>System operators manage a particular computer.</summary>
		// Token: 0x040022FA RID: 8954
		SystemOperator,
		/// <summary>Print operators can take control of a printer.</summary>
		// Token: 0x040022FB RID: 8955
		PrintOperator,
		/// <summary>Backup operators can override security restrictions for the sole purpose of backing up or restoring files.</summary>
		// Token: 0x040022FC RID: 8956
		BackupOperator,
		/// <summary>Replicators support file replication in a domain.</summary>
		// Token: 0x040022FD RID: 8957
		Replicator
	}
}
