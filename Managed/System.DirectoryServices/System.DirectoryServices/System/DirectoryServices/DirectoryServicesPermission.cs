using System;
using System.Security.Permissions;

namespace System.DirectoryServices
{
	/// <summary>The <see cref="T:System.DirectoryServices.DirectoryServicesPermission" /> class allows you to control code access security permissions for <see cref="N:System.DirectoryServices" />.</summary>
	// Token: 0x02000017 RID: 23
	[Serializable]
	public sealed class DirectoryServicesPermission : ResourcePermissionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryServicesPermission" /> class. </summary>
		// Token: 0x060000D6 RID: 214 RVA: 0x00003CC4 File Offset: 0x00001EC4
		public DirectoryServicesPermission()
		{
			this.SetUp();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryServicesPermission" /> class with the specified permission access level entries.</summary>
		/// <param name="permissionAccessEntries">An array of <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> objects. The <see cref="P:System.DirectoryServices.DirectoryServicesPermission.PermissionEntries" /> property is set to this value.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified value for the <paramref name="permissionAccessEntries" /> parameter is null.</exception>
		// Token: 0x060000D7 RID: 215 RVA: 0x00003CD2 File Offset: 0x00001ED2
		public DirectoryServicesPermission(DirectoryServicesPermissionEntry[] permissionAccessEntries)
		{
			this.SetUp();
			this.innerCollection = new DirectoryServicesPermissionEntryCollection(this);
			this.innerCollection.AddRange(permissionAccessEntries);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryServicesPermission" /> class with the specified permission state.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="State" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x060000D8 RID: 216 RVA: 0x00003CF8 File Offset: 0x00001EF8
		public DirectoryServicesPermission(PermissionState state)
			: base(state)
		{
			this.SetUp();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryServicesPermission" /> class with the specified access levels and specified path to an Active Directory Domain Services node.</summary>
		/// <param name="permissionAccess">One of the <see cref="T:System.DirectoryServices.DirectoryServicesPermissionAccess" /> values.</param>
		/// <param name="path">The path of the Active Directory Domain Services object, otherwise known as the ADsPath, to which the permissions apply.</param>
		// Token: 0x060000D9 RID: 217 RVA: 0x00003D07 File Offset: 0x00001F07
		public DirectoryServicesPermission(DirectoryServicesPermissionAccess permissionAccess, string path)
		{
			this.SetUp();
			this.innerCollection = new DirectoryServicesPermissionEntryCollection(this);
			this.innerCollection.Add(new DirectoryServicesPermissionEntry(permissionAccess, path));
		}

		/// <summary>Gets the collection of permission entries for this permission.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntryCollection" /> object that contains the permission entries for this permission.</returns>
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003D34 File Offset: 0x00001F34
		public DirectoryServicesPermissionEntryCollection PermissionEntries
		{
			get
			{
				if (this.innerCollection == null)
				{
					this.innerCollection = new DirectoryServicesPermissionEntryCollection(this);
				}
				return this.innerCollection;
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003D50 File Offset: 0x00001F50
		private void SetUp()
		{
			base.PermissionAccessType = typeof(DirectoryServicesPermissionAccess);
			base.TagNames = new string[] { "Path" };
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003D76 File Offset: 0x00001F76
		internal ResourcePermissionBaseEntry[] GetEntries()
		{
			return base.GetPermissionEntries();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003D7E File Offset: 0x00001F7E
		internal void ClearEntries()
		{
			base.Clear();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003D88 File Offset: 0x00001F88
		internal void Add(object obj)
		{
			DirectoryServicesPermissionEntry directoryServicesPermissionEntry = obj as DirectoryServicesPermissionEntry;
			base.AddPermissionAccess(directoryServicesPermissionEntry.GetBaseEntry());
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003DA8 File Offset: 0x00001FA8
		internal void Remove(object obj)
		{
			DirectoryServicesPermissionEntry directoryServicesPermissionEntry = obj as DirectoryServicesPermissionEntry;
			base.RemovePermissionAccess(directoryServicesPermissionEntry.GetBaseEntry());
		}

		// Token: 0x0400007E RID: 126
		private DirectoryServicesPermissionEntryCollection innerCollection;
	}
}
