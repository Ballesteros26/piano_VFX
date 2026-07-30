using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.UIPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020005BB RID: 1467
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class UIPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UIPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06004145 RID: 16709 RVA: 0x000E2D08 File Offset: 0x000E0F08
		public UIPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the type of access to the clipboard that is permitted.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.UIPermissionClipboard" /> values.</returns>
		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06004146 RID: 16710 RVA: 0x000E85EB File Offset: 0x000E67EB
		// (set) Token: 0x06004147 RID: 16711 RVA: 0x000E85F3 File Offset: 0x000E67F3
		public UIPermissionClipboard Clipboard
		{
			get
			{
				return this.clipboard;
			}
			set
			{
				this.clipboard = value;
			}
		}

		/// <summary>Gets or sets the type of access to the window resources that is permitted.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.UIPermissionWindow" /> values.</returns>
		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06004148 RID: 16712 RVA: 0x000E85FC File Offset: 0x000E67FC
		// (set) Token: 0x06004149 RID: 16713 RVA: 0x000E8604 File Offset: 0x000E6804
		public UIPermissionWindow Window
		{
			get
			{
				return this.window;
			}
			set
			{
				this.window = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.UIPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.UIPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x0600414A RID: 16714 RVA: 0x000E8610 File Offset: 0x000E6810
		public override IPermission CreatePermission()
		{
			UIPermission uipermission;
			if (base.Unrestricted)
			{
				uipermission = new UIPermission(PermissionState.Unrestricted);
			}
			else
			{
				uipermission = new UIPermission(this.window, this.clipboard);
			}
			return uipermission;
		}

		// Token: 0x040020FC RID: 8444
		private UIPermissionClipboard clipboard;

		// Token: 0x040020FD RID: 8445
		private UIPermissionWindow window;
	}
}
