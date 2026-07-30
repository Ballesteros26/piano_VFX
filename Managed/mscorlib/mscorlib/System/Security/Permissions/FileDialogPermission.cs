using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Controls the ability to access files or folders through a File dialog box. This class cannot be inherited.</summary>
	// Token: 0x02000589 RID: 1417
	[ComVisible(true)]
	[Serializable]
	public sealed class FileDialogPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileDialogPermission" /> class with either restricted or unrestricted permission, as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values (Unrestricted or None). </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06003F64 RID: 16228 RVA: 0x000E2DA2 File Offset: 0x000E0FA2
		public FileDialogPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._access = FileDialogPermissionAccess.OpenSave;
				return;
			}
			this._access = FileDialogPermissionAccess.None;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileDialogPermission" /> class with the specified access.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileDialogPermissionAccess" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid combination of the <see cref="T:System.Security.Permissions.FileDialogPermissionAccess" /> values. </exception>
		// Token: 0x06003F65 RID: 16229 RVA: 0x000E2DC3 File Offset: 0x000E0FC3
		public FileDialogPermission(FileDialogPermissionAccess access)
		{
			this.Access = access;
		}

		/// <summary>Gets or sets the permitted access to files.</summary>
		/// <returns>The permitted access to files.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set the <paramref name="access" /> parameter to a value that is not a valid combination of the <see cref="T:System.Security.Permissions.FileDialogPermissionAccess" /> values. </exception>
		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06003F66 RID: 16230 RVA: 0x000E2DD2 File Offset: 0x000E0FD2
		// (set) Token: 0x06003F67 RID: 16231 RVA: 0x000E2DDA File Offset: 0x000E0FDA
		public FileDialogPermissionAccess Access
		{
			get
			{
				return this._access;
			}
			set
			{
				if (!Enum.IsDefined(typeof(FileDialogPermissionAccess), value))
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "FileDialogPermissionAccess");
				}
				this._access = value;
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06003F68 RID: 16232 RVA: 0x000E2E1A File Offset: 0x000E101A
		public override IPermission Copy()
		{
			return new FileDialogPermission(this._access);
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding used to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.-or- The version number of the <paramref name="esd" /> parameter is not supported. </exception>
		// Token: 0x06003F69 RID: 16233 RVA: 0x000E2E28 File Offset: 0x000E1028
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this._access = FileDialogPermissionAccess.OpenSave;
				return;
			}
			string text = esd.Attribute("Access");
			if (text == null)
			{
				this._access = FileDialogPermissionAccess.None;
				return;
			}
			this._access = (FileDialogPermissionAccess)Enum.Parse(typeof(FileDialogPermissionAccess), text);
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003F6A RID: 16234 RVA: 0x000E2E88 File Offset: 0x000E1088
		public override IPermission Intersect(IPermission target)
		{
			FileDialogPermission fileDialogPermission = this.Cast(target);
			if (fileDialogPermission == null)
			{
				return null;
			}
			FileDialogPermissionAccess fileDialogPermissionAccess = this._access & fileDialogPermission._access;
			if (fileDialogPermissionAccess != FileDialogPermissionAccess.None)
			{
				return new FileDialogPermission(fileDialogPermissionAccess);
			}
			return null;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003F6B RID: 16235 RVA: 0x000E2EBC File Offset: 0x000E10BC
		public override bool IsSubsetOf(IPermission target)
		{
			FileDialogPermission fileDialogPermission = this.Cast(target);
			return fileDialogPermission != null && (this._access & fileDialogPermission._access) == this._access;
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x06003F6C RID: 16236 RVA: 0x000E2EEB File Offset: 0x000E10EB
		public bool IsUnrestricted()
		{
			return this._access == FileDialogPermissionAccess.OpenSave;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including state information.</returns>
		// Token: 0x06003F6D RID: 16237 RVA: 0x000E2EF8 File Offset: 0x000E10F8
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			switch (this._access)
			{
			case FileDialogPermissionAccess.Open:
				securityElement.AddAttribute("Access", "Open");
				break;
			case FileDialogPermissionAccess.Save:
				securityElement.AddAttribute("Access", "Save");
				break;
			case FileDialogPermissionAccess.OpenSave:
				securityElement.AddAttribute("Unrestricted", "true");
				break;
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003F6E RID: 16238 RVA: 0x000E2F60 File Offset: 0x000E1160
		public override IPermission Union(IPermission target)
		{
			FileDialogPermission fileDialogPermission = this.Cast(target);
			if (fileDialogPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || fileDialogPermission.IsUnrestricted())
			{
				return new FileDialogPermission(PermissionState.Unrestricted);
			}
			return new FileDialogPermission(this._access | fileDialogPermission._access);
		}

		// Token: 0x06003F6F RID: 16239 RVA: 0x00003B29 File Offset: 0x00001D29
		int IBuiltInPermission.GetTokenIndex()
		{
			return 1;
		}

		// Token: 0x06003F70 RID: 16240 RVA: 0x000E2FA8 File Offset: 0x000E11A8
		private FileDialogPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			FileDialogPermission fileDialogPermission = target as FileDialogPermission;
			if (fileDialogPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(FileDialogPermission));
			}
			return fileDialogPermission;
		}

		// Token: 0x04002022 RID: 8226
		private const int version = 1;

		// Token: 0x04002023 RID: 8227
		private FileDialogPermissionAccess _access;
	}
}
