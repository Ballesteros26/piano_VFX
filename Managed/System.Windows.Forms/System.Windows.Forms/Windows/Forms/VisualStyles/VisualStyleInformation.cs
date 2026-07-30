using System;
using System.Drawing;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Provides information about the current visual style of the operating system.</summary>
	// Token: 0x02000626 RID: 1574
	public static class VisualStyleInformation
	{
		/// <summary>Gets the author of the current visual style.</summary>
		/// <returns>A string that specifies the author of the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
		// Token: 0x1700153C RID: 5436
		// (get) Token: 0x06004FC1 RID: 20417 RVA: 0x001376D8 File Offset: 0x001358D8
		public static string Author
		{
			get
			{
				if (!VisualStyleRenderer.IsSupported)
				{
					return string.Empty;
				}
				return VisualStyleInformation.VisualStyles.VisualStyleInformationAuthor;
			}
		}

		/// <summary>Gets the color scheme of the current visual style.</summary>
		/// <returns>A string that specifies the color scheme of the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
		// Token: 0x1700153D RID: 5437
		// (get) Token: 0x06004FC2 RID: 20418 RVA: 0x001376F4 File Offset: 0x001358F4
		public static string ColorScheme
		{
			get
			{
				if (!VisualStyleRenderer.IsSupported)
				{
					return string.Empty;
				}
				return VisualStyleInformation.VisualStyles.VisualStyleInformationColorScheme;
			}
		}

		/// <summary>Gets the company that created the current visual style.</summary>
		/// <returns>A string that specifies the company that created the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
		// Token: 0x1700153E RID: 5438
		// (get) Token: 0x06004FC3 RID: 20419 RVA: 0x00137710 File Offset: 0x00135910
		public static string Company
		{
			get
			{
				if (!VisualStyleRenderer.IsSupported)
				{
					return string.Empty;
				}
				return VisualStyleInformation.VisualStyles.VisualStyleInformationCompany;
			}
		}

		/// <summary>Gets the color that the current visual style uses to indicate the hot state of a control.</summary>
		/// <returns>If visual styles are enabled, the <see cref="T:System.Drawing.Color" /> used to paint a highlight on a control in the hot state; otherwise, <see cref="P:System.Drawing.SystemColors.ButtonHighlight" />.</returns>
		// Token: 0x1700153F RID: 5439
		// (get) Token: 0x06004FC4 RID: 20420 RVA: 0x0013772C File Offset: 0x0013592C
		[MonoTODO("Cannot get this to return the same as MS's...")]
		public static Color ControlHighlightHot
		{
			get
			{
				if (!VisualStyleRenderer.IsSupported)
				{
					return SystemColors.ButtonHighlight;
				}
				return VisualStyleInformation.VisualStyles.VisualStyleInformationControlHighlightHot;
			}
		}

		/// <summary>Gets the copyright of the current visual style.</summary>
		/// <returns>A string that specifies the copyright of the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
		// Token: 0x17001540 RID: 5440
		// (get) Token: 0x06004FC5 RID: 20421 RVA: 0x00137748 File Offset: 0x00135948
		public static string Copyright
		{
			get
			{
				if (!VisualStyleRenderer.IsSupported)
				{
					return string.Empty;
				}
				return VisualStyleInformation.VisualStyles.VisualStyleInformationCopyright;
			}
		}

		/// <summary>Gets a description of the current visual style.</summary>
		/// <returns>A string that describes the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
		// Token: 0x17001541 RID: 5441
		// (get) Token: 0x06004FC6 RID: 20422 RVA: 0x00137764 File Offset: 0x00135964
		public static string Description
		{
			get
			{
				if (!VisualStyleRenderer.IsSupported)
				{
					return string.Empty;
				}
				return VisualStyleInformation.VisualStyles.VisualStyleInformationDescription;
			}
		}

		/// <summary>Gets the display name of the current visual style.</summary>
		/// <returns>A string that specifies the display name of the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
		// Token: 0x17001542 RID: 5442
		// (get) Token: 0x06004FC7 RID: 20423 RVA: 0x00137780 File Offset: 0x00135980
		public static string DisplayName
		{
			get
			{
				if (!VisualStyleRenderer.IsSupported)
				{
					return string.Empty;
				}
				return VisualStyleInformation.VisualStyles.VisualStyleInformationDisplayName;
			}
		}

		/// <summary>Gets a value indicating whether the user has enabled visual styles in the operating system.</summary>
		/// <returns>true if the user has enabled visual styles in an operating system that supports them; otherwise, false.</returns>
		// Token: 0x17001543 RID: 5443
		// (get) Token: 0x06004FC8 RID: 20424 RVA: 0x0013779C File Offset: 0x0013599C
		public static bool IsEnabledByUser
		{
			get
			{
				return VisualStyleInformation.IsSupportedByOS && VisualStyleInformation.VisualStyles.UxThemeIsAppThemed() && VisualStyleInformation.VisualStyles.UxThemeIsThemeActive();
			}
		}

		/// <summary>Gets a value indicating whether the operating system supports visual styles.</summary>
		/// <returns>true if the operating system supports visual styles; otherwise, false.</returns>
		// Token: 0x17001544 RID: 5444
		// (get) Token: 0x06004FC9 RID: 20425 RVA: 0x001377D4 File Offset: 0x001359D4
		public static bool IsSupportedByOS
		{
			get
			{
				return VisualStyleInformation.VisualStyles.VisualStyleInformationIsSupportedByOS;
			}
		}

		/// <summary>Gets the minimum color depth for the current visual style.</summary>
		/// <returns>The minimum color depth for the current visual style if visual styles are enabled; otherwise, 0.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001545 RID: 5445
		// (get) Token: 0x06004FCA RID: 20426 RVA: 0x001377E0 File Offset: 0x001359E0
		public static int MinimumColorDepth
		{
			get
			{
				if (!VisualStyleRenderer.IsSupported)
				{
					return 0;
				}
				return VisualStyleInformation.VisualStyles.VisualStyleInformationMinimumColorDepth;
			}
		}

		/// <summary>Gets a string that describes the size of the current visual style.</summary>
		/// <returns>A string that describes the size of the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
		// Token: 0x17001546 RID: 5446
		// (get) Token: 0x06004FCB RID: 20427 RVA: 0x001377F8 File Offset: 0x001359F8
		public static string Size
		{
			get
			{
				if (!VisualStyleRenderer.IsSupported)
				{
					return string.Empty;
				}
				return VisualStyleInformation.VisualStyles.VisualStyleInformationSize;
			}
		}

		/// <summary>Gets a value indicating whether the current visual style supports flat menus.</summary>
		/// <returns>true if visual styles are enabled and the current visual style supports flat menus; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001547 RID: 5447
		// (get) Token: 0x06004FCC RID: 20428 RVA: 0x00137814 File Offset: 0x00135A14
		public static bool SupportsFlatMenus
		{
			get
			{
				return VisualStyleRenderer.IsSupported && VisualStyleInformation.VisualStyles.VisualStyleInformationSupportsFlatMenus;
			}
		}

		/// <summary>Gets the color that the current visual style uses to paint the borders of controls that contain text.</summary>
		/// <returns>If visual styles are enabled, the <see cref="T:System.Drawing.Color" /> that the current visual style uses to paint the borders of controls that contain text; otherwise, <see cref="P:System.Drawing.SystemColors.ControlDarkDark" />.</returns>
		// Token: 0x17001548 RID: 5448
		// (get) Token: 0x06004FCD RID: 20429 RVA: 0x0013782C File Offset: 0x00135A2C
		[MonoTODO("Cannot get this to return the same as MS's...")]
		public static Color TextControlBorder
		{
			get
			{
				if (!VisualStyleRenderer.IsSupported)
				{
					return SystemColors.ControlDarkDark;
				}
				return VisualStyleInformation.VisualStyles.VisualStyleInformationTextControlBorder;
			}
		}

		/// <summary>Gets a URL provided by the author of the current visual style.</summary>
		/// <returns>A string that specifies a URL provided by the author of the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
		// Token: 0x17001549 RID: 5449
		// (get) Token: 0x06004FCE RID: 20430 RVA: 0x00137848 File Offset: 0x00135A48
		public static string Url
		{
			get
			{
				if (!VisualStyleRenderer.IsSupported)
				{
					return string.Empty;
				}
				return VisualStyleInformation.VisualStyles.VisualStyleInformationUrl;
			}
		}

		/// <summary>Gets the version of the current visual style.</summary>
		/// <returns>A string that indicates the version of the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
		// Token: 0x1700154A RID: 5450
		// (get) Token: 0x06004FCF RID: 20431 RVA: 0x00137864 File Offset: 0x00135A64
		public static string Version
		{
			get
			{
				if (!VisualStyleRenderer.IsSupported)
				{
					return string.Empty;
				}
				return VisualStyleInformation.VisualStyles.VisualStyleInformationVersion;
			}
		}

		// Token: 0x1700154B RID: 5451
		// (get) Token: 0x06004FD0 RID: 20432 RVA: 0x00137880 File Offset: 0x00135A80
		private static IVisualStyles VisualStyles
		{
			get
			{
				return VisualStylesEngine.Instance;
			}
		}
	}
}
