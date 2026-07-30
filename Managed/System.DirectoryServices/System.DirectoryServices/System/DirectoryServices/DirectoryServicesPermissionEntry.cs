using System;
using System.Security.Permissions;

namespace System.DirectoryServices
{
	/// <summary>The <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> class defines the smallest unit of a code access security permission set for <see cref="N:System.DirectoryServices" />.</summary>
	// Token: 0x0200001A RID: 26
	[Serializable]
	public class DirectoryServicesPermissionEntry
	{
		/// <summary>The <see cref="M:System.DirectoryServices.DirectoryServicesPermissionEntry.#ctor(System.DirectoryServices.DirectoryServicesPermissionAccess,System.String)" /> constructor initializes a new instance of the <see cref="M:System.DirectoryServices.DirectoryServicesPermissionEntry.#ctor(System.DirectoryServices.DirectoryServicesPermissionAccess,System.String)" /> class.</summary>
		/// <param name="permissionAccess">One of the <see cref="T:System.DirectoryServices.DirectoryServicesPermissionAccess" /> values.</param>
		/// <param name="path">The path of the Active Directory Domain Services node to which the permissions apply.</param>
		// Token: 0x060000E6 RID: 230 RVA: 0x00003E35 File Offset: 0x00002035
		public DirectoryServicesPermissionEntry(DirectoryServicesPermissionAccess permissionAccess, string path)
		{
			this.permissionAccess = permissionAccess;
			this.path = path;
		}

		/// <summary>The <see cref="P:System.DirectoryServices.DirectoryServicesPermissionEntry.Path" /> property gets a path to an Active Directory Domain Services node to which the permissions apply.</summary>
		/// <returns>The path to an Active Directory Domain Services node.</returns>
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00003E4B File Offset: 0x0000204B
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		/// <summary>The <see cref="P:System.DirectoryServices.DirectoryServicesPermissionEntry.PermissionAccess" /> property gets the access levels used in creating permissions.</summary>
		/// <returns>One of the <see cref="T:System.DirectoryServices.DirectoryServicesPermissionAccess" /> values.</returns>
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00003E53 File Offset: 0x00002053
		public DirectoryServicesPermissionAccess PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003E5B File Offset: 0x0000205B
		internal ResourcePermissionBaseEntry GetBaseEntry()
		{
			return new ResourcePermissionBaseEntry((int)this.permissionAccess, new string[] { this.path });
		}

		// Token: 0x04000085 RID: 133
		private DirectoryServicesPermissionAccess permissionAccess;

		// Token: 0x04000086 RID: 134
		private string path;
	}
}
