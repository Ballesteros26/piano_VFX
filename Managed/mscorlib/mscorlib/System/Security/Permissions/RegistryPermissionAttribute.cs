using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.RegistryPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020005AE RID: 1454
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class RegistryPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.RegistryPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="action" /> parameter is not a valid <see cref="T:System.Security.Permissions.SecurityAction" />. </exception>
		// Token: 0x060040AF RID: 16559 RVA: 0x000E2D08 File Offset: 0x000E0F08
		public RegistryPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets full access for the specified registry keys.</summary>
		/// <returns>A semicolon-separated list of registry key paths, for full access. </returns>
		/// <exception cref="T:System.NotSupportedException">The get accessor is called; it is only provided for C# compiler compatibility.</exception>
		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x060040B0 RID: 16560 RVA: 0x000E2D11 File Offset: 0x000E0F11
		// (set) Token: 0x060040B1 RID: 16561 RVA: 0x000E6A90 File Offset: 0x000E4C90
		[Obsolete("use newer properties")]
		public string All
		{
			get
			{
				throw new NotSupportedException("All");
			}
			set
			{
				this.create = value;
				this.read = value;
				this.write = value;
			}
		}

		/// <summary>Gets or sets create-level access for the specified registry keys. </summary>
		/// <returns>A semicolon-separated list of registry key paths, for create-level access. </returns>
		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x060040B2 RID: 16562 RVA: 0x000E6AA7 File Offset: 0x000E4CA7
		// (set) Token: 0x060040B3 RID: 16563 RVA: 0x000E6AAF File Offset: 0x000E4CAF
		public string Create
		{
			get
			{
				return this.create;
			}
			set
			{
				this.create = value;
			}
		}

		/// <summary>Gets or sets read access for the specified registry keys.</summary>
		/// <returns>A semicolon-separated list of registry key paths, for read access. </returns>
		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x060040B4 RID: 16564 RVA: 0x000E6AB8 File Offset: 0x000E4CB8
		// (set) Token: 0x060040B5 RID: 16565 RVA: 0x000E6AC0 File Offset: 0x000E4CC0
		public string Read
		{
			get
			{
				return this.read;
			}
			set
			{
				this.read = value;
			}
		}

		/// <summary>Gets or sets write access for the specified registry keys.</summary>
		/// <returns>A semicolon-separated list of registry key paths, for write access. </returns>
		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x060040B6 RID: 16566 RVA: 0x000E6AC9 File Offset: 0x000E4CC9
		// (set) Token: 0x060040B7 RID: 16567 RVA: 0x000E6AD1 File Offset: 0x000E4CD1
		public string Write
		{
			get
			{
				return this.write;
			}
			set
			{
				this.write = value;
			}
		}

		/// <summary>Gets or sets change access control for the specified registry keys.</summary>
		/// <returns>A semicolon-separated list of registry key paths, for change access control. .</returns>
		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x060040B8 RID: 16568 RVA: 0x000E6ADA File Offset: 0x000E4CDA
		// (set) Token: 0x060040B9 RID: 16569 RVA: 0x000E6AE2 File Offset: 0x000E4CE2
		public string ChangeAccessControl
		{
			get
			{
				return this.changeAccessControl;
			}
			set
			{
				this.changeAccessControl = value;
			}
		}

		/// <summary>Gets or sets view access control for the specified registry keys.</summary>
		/// <returns>A semicolon-separated list of registry key paths, for view access control.</returns>
		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x060040BA RID: 16570 RVA: 0x000E6AEB File Offset: 0x000E4CEB
		// (set) Token: 0x060040BB RID: 16571 RVA: 0x000E6AF3 File Offset: 0x000E4CF3
		public string ViewAccessControl
		{
			get
			{
				return this.viewAccessControl;
			}
			set
			{
				this.viewAccessControl = value;
			}
		}

		/// <summary>Gets or sets a specified set of registry keys that can be viewed and modified.</summary>
		/// <returns>A semicolon-separated list of registry key paths, for create, read, and write access. </returns>
		/// <exception cref="T:System.NotSupportedException">The get accessor is called; it is only provided for C# compiler compatibility. </exception>
		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x060040BC RID: 16572 RVA: 0x00014B5A File Offset: 0x00012D5A
		// (set) Token: 0x060040BD RID: 16573 RVA: 0x000E6A90 File Offset: 0x000E4C90
		public string ViewAndModify
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				this.create = value;
				this.read = value;
				this.write = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.RegistryPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.RegistryPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x060040BE RID: 16574 RVA: 0x000E6AFC File Offset: 0x000E4CFC
		public override IPermission CreatePermission()
		{
			RegistryPermission registryPermission;
			if (base.Unrestricted)
			{
				registryPermission = new RegistryPermission(PermissionState.Unrestricted);
			}
			else
			{
				registryPermission = new RegistryPermission(PermissionState.None);
				if (this.create != null)
				{
					registryPermission.AddPathList(RegistryPermissionAccess.Create, this.create);
				}
				if (this.read != null)
				{
					registryPermission.AddPathList(RegistryPermissionAccess.Read, this.read);
				}
				if (this.write != null)
				{
					registryPermission.AddPathList(RegistryPermissionAccess.Write, this.write);
				}
			}
			return registryPermission;
		}

		// Token: 0x040020C5 RID: 8389
		private string create;

		// Token: 0x040020C6 RID: 8390
		private string read;

		// Token: 0x040020C7 RID: 8391
		private string write;

		// Token: 0x040020C8 RID: 8392
		private string changeAccessControl;

		// Token: 0x040020C9 RID: 8393
		private string viewAccessControl;
	}
}
