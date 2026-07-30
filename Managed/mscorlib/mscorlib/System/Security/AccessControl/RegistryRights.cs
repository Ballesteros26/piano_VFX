using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the access control rights that can be applied to registry objects.</summary>
	// Token: 0x0200060D RID: 1549
	[Flags]
	public enum RegistryRights
	{
		/// <summary>The right to query the name/value pairs in a registry key.</summary>
		// Token: 0x040021F2 RID: 8690
		QueryValues = 1,
		/// <summary>The right to create, delete, or set name/value pairs in a registry key.</summary>
		// Token: 0x040021F3 RID: 8691
		SetValue = 2,
		/// <summary>The right to create subkeys of a registry key.</summary>
		// Token: 0x040021F4 RID: 8692
		CreateSubKey = 4,
		/// <summary>The right to list the subkeys of a registry key.</summary>
		// Token: 0x040021F5 RID: 8693
		EnumerateSubKeys = 8,
		/// <summary>The right to request notification of changes on a registry key.</summary>
		// Token: 0x040021F6 RID: 8694
		Notify = 16,
		/// <summary>Reserved for system use.</summary>
		// Token: 0x040021F7 RID: 8695
		CreateLink = 32,
		/// <summary>The right to delete a registry key.</summary>
		// Token: 0x040021F8 RID: 8696
		Delete = 65536,
		/// <summary>The right to open and copy the access rules and audit rules for a registry key.</summary>
		// Token: 0x040021F9 RID: 8697
		ReadPermissions = 131072,
		/// <summary>The right to create, delete, and set the name/value pairs in a registry key, to create or delete subkeys, to request notification of changes, to enumerate its subkeys, and to read its access rules and audit rules.</summary>
		// Token: 0x040021FA RID: 8698
		WriteKey = 131078,
		/// <summary>The right to query the name/value pairs in a registry key, to request notification of changes, to enumerate its subkeys, and to read its access rules and audit rules.</summary>
		// Token: 0x040021FB RID: 8699
		ReadKey = 131097,
		/// <summary>Same as <see cref="F:System.Security.AccessControl.RegistryRights.ReadKey" />.</summary>
		// Token: 0x040021FC RID: 8700
		ExecuteKey = 131097,
		/// <summary>The right to change the access rules and audit rules associated with a registry key.</summary>
		// Token: 0x040021FD RID: 8701
		ChangePermissions = 262144,
		/// <summary>The right to change the owner of a registry key.</summary>
		// Token: 0x040021FE RID: 8702
		TakeOwnership = 524288,
		/// <summary>The right to exert full control over a registry key, and to modify its access rules and audit rules.</summary>
		// Token: 0x040021FF RID: 8703
		FullControl = 983103
	}
}
