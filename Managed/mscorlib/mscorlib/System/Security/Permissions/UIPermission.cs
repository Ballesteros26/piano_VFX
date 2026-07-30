using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Controls the permissions related to user interfaces and the Clipboard. This class cannot be inherited.</summary>
	// Token: 0x020005BA RID: 1466
	[ComVisible(true)]
	[Serializable]
	public sealed class UIPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UIPermission" /> class with either fully restricted or unrestricted access, as specified.</summary>
		/// <param name="state">One of the enumeration values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06004133 RID: 16691 RVA: 0x000E826D File Offset: 0x000E646D
		public UIPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._clipboard = UIPermissionClipboard.AllClipboard;
				this._window = UIPermissionWindow.AllWindows;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UIPermission" /> class with the permissions for the Clipboard, and no access to windows.</summary>
		/// <param name="clipboardFlag">One of the enumeration values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="clipboardFlag" /> parameter is not a valid <see cref="T:System.Security.Permissions.UIPermissionClipboard" /> value. </exception>
		// Token: 0x06004134 RID: 16692 RVA: 0x000E828D File Offset: 0x000E648D
		public UIPermission(UIPermissionClipboard clipboardFlag)
		{
			this.Clipboard = clipboardFlag;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UIPermission" /> class with the permissions for windows, and no access to the Clipboard.</summary>
		/// <param name="windowFlag">One of the enumeration values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="windowFlag" /> parameter is not a valid <see cref="T:System.Security.Permissions.UIPermissionWindow" /> value. </exception>
		// Token: 0x06004135 RID: 16693 RVA: 0x000E829C File Offset: 0x000E649C
		public UIPermission(UIPermissionWindow windowFlag)
		{
			this.Window = windowFlag;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UIPermission" /> class with the specified permissions for windows and the Clipboard.</summary>
		/// <param name="windowFlag">One of the enumeration values. </param>
		/// <param name="clipboardFlag">One of the enumeration values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="windowFlag" /> parameter is not a valid <see cref="T:System.Security.Permissions.UIPermissionWindow" /> value.-or- The <paramref name="clipboardFlag" /> parameter is not a valid <see cref="T:System.Security.Permissions.UIPermissionClipboard" /> value. </exception>
		// Token: 0x06004136 RID: 16694 RVA: 0x000E82AB File Offset: 0x000E64AB
		public UIPermission(UIPermissionWindow windowFlag, UIPermissionClipboard clipboardFlag)
		{
			this.Clipboard = clipboardFlag;
			this.Window = windowFlag;
		}

		/// <summary>Gets or sets the Clipboard access represented by the permission.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.UIPermissionClipboard" /> values.</returns>
		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06004137 RID: 16695 RVA: 0x000E82C1 File Offset: 0x000E64C1
		// (set) Token: 0x06004138 RID: 16696 RVA: 0x000E82C9 File Offset: 0x000E64C9
		public UIPermissionClipboard Clipboard
		{
			get
			{
				return this._clipboard;
			}
			set
			{
				if (!Enum.IsDefined(typeof(UIPermissionClipboard), value))
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "UIPermissionClipboard");
				}
				this._clipboard = value;
			}
		}

		/// <summary>Gets or sets the window access represented by the permission.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.UIPermissionWindow" /> values.</returns>
		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06004139 RID: 16697 RVA: 0x000E8309 File Offset: 0x000E6509
		// (set) Token: 0x0600413A RID: 16698 RVA: 0x000E8311 File Offset: 0x000E6511
		public UIPermissionWindow Window
		{
			get
			{
				return this._window;
			}
			set
			{
				if (!Enum.IsDefined(typeof(UIPermissionWindow), value))
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "UIPermissionWindow");
				}
				this._window = value;
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x0600413B RID: 16699 RVA: 0x000E8351 File Offset: 0x000E6551
		public override IPermission Copy()
		{
			return new UIPermission(this._window, this._clipboard);
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding used to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.-or- The <paramref name="esd" /> parameter's version number is not valid. </exception>
		// Token: 0x0600413C RID: 16700 RVA: 0x000E8364 File Offset: 0x000E6564
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this._window = UIPermissionWindow.AllWindows;
				this._clipboard = UIPermissionClipboard.AllClipboard;
				return;
			}
			string text = esd.Attribute("Window");
			if (text == null)
			{
				this._window = UIPermissionWindow.NoWindows;
			}
			else
			{
				this._window = (UIPermissionWindow)Enum.Parse(typeof(UIPermissionWindow), text);
			}
			string text2 = esd.Attribute("Clipboard");
			if (text2 == null)
			{
				this._clipboard = UIPermissionClipboard.NoClipboard;
				return;
			}
			this._clipboard = (UIPermissionClipboard)Enum.Parse(typeof(UIPermissionClipboard), text2);
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x0600413D RID: 16701 RVA: 0x000E83FC File Offset: 0x000E65FC
		public override IPermission Intersect(IPermission target)
		{
			UIPermission uipermission = this.Cast(target);
			if (uipermission == null)
			{
				return null;
			}
			UIPermissionWindow uipermissionWindow = ((this._window < uipermission._window) ? this._window : uipermission._window);
			UIPermissionClipboard uipermissionClipboard = ((this._clipboard < uipermission._clipboard) ? this._clipboard : uipermission._clipboard);
			if (this.IsEmpty(uipermissionWindow, uipermissionClipboard))
			{
				return null;
			}
			return new UIPermission(uipermissionWindow, uipermissionClipboard);
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission to test for the subset relationship. This permission must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x0600413E RID: 16702 RVA: 0x000E8464 File Offset: 0x000E6664
		public override bool IsSubsetOf(IPermission target)
		{
			UIPermission uipermission = this.Cast(target);
			if (uipermission == null)
			{
				return this.IsEmpty(this._window, this._clipboard);
			}
			return uipermission.IsUnrestricted() || (this._window <= uipermission._window && this._clipboard <= uipermission._clipboard);
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x0600413F RID: 16703 RVA: 0x000E84BA File Offset: 0x000E66BA
		public bool IsUnrestricted()
		{
			return this._window == UIPermissionWindow.AllWindows && this._clipboard == UIPermissionClipboard.AllClipboard;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06004140 RID: 16704 RVA: 0x000E84D0 File Offset: 0x000E66D0
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._window == UIPermissionWindow.AllWindows && this._clipboard == UIPermissionClipboard.AllClipboard)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				if (this._window != UIPermissionWindow.NoWindows)
				{
					securityElement.AddAttribute("Window", this._window.ToString());
				}
				if (this._clipboard != UIPermissionClipboard.NoClipboard)
				{
					securityElement.AddAttribute("Clipboard", this._clipboard.ToString());
				}
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06004141 RID: 16705 RVA: 0x000E8554 File Offset: 0x000E6754
		public override IPermission Union(IPermission target)
		{
			UIPermission uipermission = this.Cast(target);
			if (uipermission == null)
			{
				return this.Copy();
			}
			UIPermissionWindow uipermissionWindow = ((this._window > uipermission._window) ? this._window : uipermission._window);
			UIPermissionClipboard uipermissionClipboard = ((this._clipboard > uipermission._clipboard) ? this._clipboard : uipermission._clipboard);
			if (this.IsEmpty(uipermissionWindow, uipermissionClipboard))
			{
				return null;
			}
			return new UIPermission(uipermissionWindow, uipermissionClipboard);
		}

		// Token: 0x06004142 RID: 16706 RVA: 0x0002A81F File Offset: 0x00028A1F
		int IBuiltInPermission.GetTokenIndex()
		{
			return 7;
		}

		// Token: 0x06004143 RID: 16707 RVA: 0x000E85C0 File Offset: 0x000E67C0
		private bool IsEmpty(UIPermissionWindow w, UIPermissionClipboard c)
		{
			return w == UIPermissionWindow.NoWindows && c == UIPermissionClipboard.NoClipboard;
		}

		// Token: 0x06004144 RID: 16708 RVA: 0x000E85CB File Offset: 0x000E67CB
		private UIPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			UIPermission uipermission = target as UIPermission;
			if (uipermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(UIPermission));
			}
			return uipermission;
		}

		// Token: 0x040020F9 RID: 8441
		private UIPermissionWindow _window;

		// Token: 0x040020FA RID: 8442
		private UIPermissionClipboard _clipboard;

		// Token: 0x040020FB RID: 8443
		private const int version = 1;
	}
}
