using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.FileDialogPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x0200058B RID: 1419
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class FileDialogPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileDialogPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06003F71 RID: 16241 RVA: 0x000E2D08 File Offset: 0x000E0F08
		public FileDialogPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets a value indicating whether permission to open files through the file dialog is declared.</summary>
		/// <returns>true if permission to open files through the file dialog is declared; otherwise, false.</returns>
		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06003F72 RID: 16242 RVA: 0x000E2FC8 File Offset: 0x000E11C8
		// (set) Token: 0x06003F73 RID: 16243 RVA: 0x000E2FD0 File Offset: 0x000E11D0
		public bool Open
		{
			get
			{
				return this.canOpen;
			}
			set
			{
				this.canOpen = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to save files through the file dialog is declared.</summary>
		/// <returns>true if permission to save files through the file dialog is declared; otherwise, false.</returns>
		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06003F74 RID: 16244 RVA: 0x000E2FD9 File Offset: 0x000E11D9
		// (set) Token: 0x06003F75 RID: 16245 RVA: 0x000E2FE1 File Offset: 0x000E11E1
		public bool Save
		{
			get
			{
				return this.canSave;
			}
			set
			{
				this.canSave = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.FileDialogPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.FileDialogPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06003F76 RID: 16246 RVA: 0x000E2FEC File Offset: 0x000E11EC
		public override IPermission CreatePermission()
		{
			FileDialogPermission fileDialogPermission;
			if (base.Unrestricted)
			{
				fileDialogPermission = new FileDialogPermission(PermissionState.Unrestricted);
			}
			else
			{
				FileDialogPermissionAccess fileDialogPermissionAccess = FileDialogPermissionAccess.None;
				if (this.canOpen)
				{
					fileDialogPermissionAccess |= FileDialogPermissionAccess.Open;
				}
				if (this.canSave)
				{
					fileDialogPermissionAccess |= FileDialogPermissionAccess.Save;
				}
				fileDialogPermission = new FileDialogPermission(fileDialogPermissionAccess);
			}
			return fileDialogPermission;
		}

		// Token: 0x04002029 RID: 8233
		private bool canOpen;

		// Token: 0x0400202A RID: 8234
		private bool canSave;
	}
}
