using System;

namespace System.Security.Permissions
{
	/// <summary>Defines the smallest unit of a code access security permission set.</summary>
	// Token: 0x02000376 RID: 886
	[Serializable]
	public class ResourcePermissionBaseEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> class.</summary>
		// Token: 0x06001B26 RID: 6950 RVA: 0x0006CD2F File Offset: 0x0006AF2F
		public ResourcePermissionBaseEntry()
		{
			this.permissionAccessPath = new string[0];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> class with the specified permission access and permission access path.</summary>
		/// <param name="permissionAccess">The integer representation of the permission access level enumeration value. The <see cref="P:System.Security.Permissions.ResourcePermissionBaseEntry.PermissionAccess" /> property is set to this value. </param>
		/// <param name="permissionAccessPath">The array of strings that identify the resource you are protecting. The <see cref="P:System.Security.Permissions.ResourcePermissionBaseEntry.PermissionAccessPath" /> property is set to this value. </param>
		/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="permissionAccessPath" /> is null. </exception>
		// Token: 0x06001B27 RID: 6951 RVA: 0x0006CD43 File Offset: 0x0006AF43
		public ResourcePermissionBaseEntry(int permissionAccess, string[] permissionAccessPath)
		{
			if (permissionAccessPath == null)
			{
				throw new ArgumentNullException("permissionAccessPath");
			}
			this.permissionAccess = permissionAccess;
			this.permissionAccessPath = permissionAccessPath;
		}

		/// <summary>Gets an integer representation of the access level enumeration value.</summary>
		/// <returns>The access level enumeration value.</returns>
		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001B28 RID: 6952 RVA: 0x0006CD67 File Offset: 0x0006AF67
		public int PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
		}

		/// <summary>Gets an array of strings that identify the resource you are protecting.</summary>
		/// <returns>An array of strings that identify the resource you are protecting.</returns>
		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x0006CD6F File Offset: 0x0006AF6F
		public string[] PermissionAccessPath
		{
			get
			{
				return this.permissionAccessPath;
			}
		}

		// Token: 0x04001895 RID: 6293
		private int permissionAccess;

		// Token: 0x04001896 RID: 6294
		private string[] permissionAccessPath;
	}
}
