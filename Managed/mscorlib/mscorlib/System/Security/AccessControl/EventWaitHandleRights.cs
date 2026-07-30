using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the access control rights that can be applied to named system event objects.</summary>
	// Token: 0x020005E8 RID: 1512
	[Flags]
	public enum EventWaitHandleRights
	{
		/// <summary>The right to set or reset the signaled state of a named event.</summary>
		// Token: 0x0400219A RID: 8602
		Modify = 2,
		/// <summary>The right to delete a named event.</summary>
		// Token: 0x0400219B RID: 8603
		Delete = 65536,
		/// <summary>The right to open and copy the access rules and audit rules for a named event.</summary>
		// Token: 0x0400219C RID: 8604
		ReadPermissions = 131072,
		/// <summary>The right to change the security and audit rules associated with a named event.</summary>
		// Token: 0x0400219D RID: 8605
		ChangePermissions = 262144,
		/// <summary>The right to change the owner of a named event.</summary>
		// Token: 0x0400219E RID: 8606
		TakeOwnership = 524288,
		/// <summary>The right to wait on a named event.</summary>
		// Token: 0x0400219F RID: 8607
		Synchronize = 1048576,
		/// <summary>The right to exert full control over a named event, and to modify its access rules and audit rules.</summary>
		// Token: 0x040021A0 RID: 8608
		FullControl = 2031619
	}
}
