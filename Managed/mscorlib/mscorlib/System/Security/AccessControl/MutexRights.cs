using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the access control rights that can be applied to named system mutex objects.</summary>
	// Token: 0x020005F6 RID: 1526
	[Flags]
	public enum MutexRights
	{
		/// <summary>The right to release a named mutex.</summary>
		// Token: 0x040021C5 RID: 8645
		Modify = 1,
		/// <summary>The right to delete a named mutex.</summary>
		// Token: 0x040021C6 RID: 8646
		Delete = 65536,
		/// <summary>The right to open and copy the access rules and audit rules for a named mutex.</summary>
		// Token: 0x040021C7 RID: 8647
		ReadPermissions = 131072,
		/// <summary>The right to change the security and audit rules associated with a named mutex.</summary>
		// Token: 0x040021C8 RID: 8648
		ChangePermissions = 262144,
		/// <summary>The right to change the owner of a named mutex.</summary>
		// Token: 0x040021C9 RID: 8649
		TakeOwnership = 524288,
		/// <summary>The right to wait on a named mutex.</summary>
		// Token: 0x040021CA RID: 8650
		Synchronize = 1048576,
		/// <summary>The right to exert full control over a named mutex, and to modify its access rules and audit rules.</summary>
		// Token: 0x040021CB RID: 8651
		FullControl = 2031617
	}
}
