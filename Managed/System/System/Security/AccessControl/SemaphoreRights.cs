using System;
using System.Runtime.InteropServices;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the access control rights that can be applied to named system semaphore objects.</summary>
	// Token: 0x02000370 RID: 880
	[ComVisible(false)]
	[Flags]
	public enum SemaphoreRights
	{
		/// <summary>The right to release a named semaphore.</summary>
		// Token: 0x04001882 RID: 6274
		Modify = 2,
		/// <summary>The right to delete a named semaphore.</summary>
		// Token: 0x04001883 RID: 6275
		Delete = 65536,
		/// <summary>The right to open and copy the access rules and audit rules for a named semaphore.</summary>
		// Token: 0x04001884 RID: 6276
		ReadPermissions = 131072,
		/// <summary>The right to change the security and audit rules associated with a named semaphore.</summary>
		// Token: 0x04001885 RID: 6277
		ChangePermissions = 262144,
		/// <summary>The right to change the owner of a named semaphore.</summary>
		// Token: 0x04001886 RID: 6278
		TakeOwnership = 524288,
		/// <summary>The right to wait on a named semaphore.</summary>
		// Token: 0x04001887 RID: 6279
		Synchronize = 1048576,
		/// <summary>The right to exert full control over a named semaphore, and to modify its access rules and audit rules.</summary>
		// Token: 0x04001888 RID: 6280
		FullControl = 2031619
	}
}
