using System;

namespace System.Drawing
{
	/// <summary>Each property of the <see cref="T:System.Drawing.SystemPens" /> class is a <see cref="T:System.Drawing.Pen" /> that is the color of a Windows display element and that has a width of 1 pixel.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200008C RID: 140
	public sealed class SystemPens
	{
		// Token: 0x0600075D RID: 1885 RVA: 0x00002050 File Offset: 0x00000250
		private SystemPens()
		{
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the text in the active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the text in the active window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x00014AC2 File Offset: 0x00012CC2
		public static Pen ActiveCaptionText
		{
			get
			{
				if (SystemPens.active_caption_text == null)
				{
					SystemPens.active_caption_text = new Pen(SystemColors.ActiveCaptionText);
					SystemPens.active_caption_text.isModifiable = false;
				}
				return SystemPens.active_caption_text;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the face color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the face color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x00014AEA File Offset: 0x00012CEA
		public static Pen Control
		{
			get
			{
				if (SystemPens.control == null)
				{
					SystemPens.control = new Pen(SystemColors.Control);
					SystemPens.control.isModifiable = false;
				}
				return SystemPens.control;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the shadow color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the shadow color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x00014B12 File Offset: 0x00012D12
		public static Pen ControlDark
		{
			get
			{
				if (SystemPens.control_dark == null)
				{
					SystemPens.control_dark = new Pen(SystemColors.ControlDark);
					SystemPens.control_dark.isModifiable = false;
				}
				return SystemPens.control_dark;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the dark shadow color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the dark shadow color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00014B3A File Offset: 0x00012D3A
		public static Pen ControlDarkDark
		{
			get
			{
				if (SystemPens.control_dark_dark == null)
				{
					SystemPens.control_dark_dark = new Pen(SystemColors.ControlDarkDark);
					SystemPens.control_dark_dark.isModifiable = false;
				}
				return SystemPens.control_dark_dark;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the light color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the light color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x00014B62 File Offset: 0x00012D62
		public static Pen ControlLight
		{
			get
			{
				if (SystemPens.control_light == null)
				{
					SystemPens.control_light = new Pen(SystemColors.ControlLight);
					SystemPens.control_light.isModifiable = false;
				}
				return SystemPens.control_light;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the highlight color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the highlight color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x00014B8A File Offset: 0x00012D8A
		public static Pen ControlLightLight
		{
			get
			{
				if (SystemPens.control_light_light == null)
				{
					SystemPens.control_light_light = new Pen(SystemColors.ControlLightLight);
					SystemPens.control_light_light.isModifiable = false;
				}
				return SystemPens.control_light_light;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of text in a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of text in a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x00014BB2 File Offset: 0x00012DB2
		public static Pen ControlText
		{
			get
			{
				if (SystemPens.control_text == null)
				{
					SystemPens.control_text = new Pen(SystemColors.ControlText);
					SystemPens.control_text.isModifiable = false;
				}
				return SystemPens.control_text;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of dimmed text. </summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of dimmed text.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x00014BDA File Offset: 0x00012DDA
		public static Pen GrayText
		{
			get
			{
				if (SystemPens.gray_text == null)
				{
					SystemPens.gray_text = new Pen(SystemColors.GrayText);
					SystemPens.gray_text.isModifiable = false;
				}
				return SystemPens.gray_text;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the background of selected items. </summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the background of selected items.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x00014C02 File Offset: 0x00012E02
		public static Pen Highlight
		{
			get
			{
				if (SystemPens.highlight == null)
				{
					SystemPens.highlight = new Pen(SystemColors.Highlight);
					SystemPens.highlight.isModifiable = false;
				}
				return SystemPens.highlight;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the text of selected items. </summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the text of selected items.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x00014C2A File Offset: 0x00012E2A
		public static Pen HighlightText
		{
			get
			{
				if (SystemPens.highlight_text == null)
				{
					SystemPens.highlight_text = new Pen(SystemColors.HighlightText);
					SystemPens.highlight_text.isModifiable = false;
				}
				return SystemPens.highlight_text;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the text in an inactive window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the text in an inactive window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x00014C52 File Offset: 0x00012E52
		public static Pen InactiveCaptionText
		{
			get
			{
				if (SystemPens.inactive_caption_text == null)
				{
					SystemPens.inactive_caption_text = new Pen(SystemColors.InactiveCaptionText);
					SystemPens.inactive_caption_text.isModifiable = false;
				}
				return SystemPens.inactive_caption_text;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the text of a ToolTip.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the text of a ToolTip.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x00014C7A File Offset: 0x00012E7A
		public static Pen InfoText
		{
			get
			{
				if (SystemPens.info_text == null)
				{
					SystemPens.info_text = new Pen(SystemColors.InfoText);
					SystemPens.info_text.isModifiable = false;
				}
				return SystemPens.info_text;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of a menu's text.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of a menu's text.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x00014CA2 File Offset: 0x00012EA2
		public static Pen MenuText
		{
			get
			{
				if (SystemPens.menu_text == null)
				{
					SystemPens.menu_text = new Pen(SystemColors.MenuText);
					SystemPens.menu_text.isModifiable = false;
				}
				return SystemPens.menu_text;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of a window frame.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of a window frame.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x00014CCA File Offset: 0x00012ECA
		public static Pen WindowFrame
		{
			get
			{
				if (SystemPens.window_frame == null)
				{
					SystemPens.window_frame = new Pen(SystemColors.WindowFrame);
					SystemPens.window_frame.isModifiable = false;
				}
				return SystemPens.window_frame;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the text in the client area of a window.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the text in the client area of a window.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x00014CF2 File Offset: 0x00012EF2
		public static Pen WindowText
		{
			get
			{
				if (SystemPens.window_text == null)
				{
					SystemPens.window_text = new Pen(SystemColors.WindowText);
					SystemPens.window_text.isModifiable = false;
				}
				return SystemPens.window_text;
			}
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Pen" /> from the specified <see cref="T:System.Drawing.Color" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Pen" /> this method creates.</returns>
		/// <param name="c">The <see cref="T:System.Drawing.Color" /> for the new <see cref="T:System.Drawing.Pen" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600076D RID: 1901 RVA: 0x00014D1A File Offset: 0x00012F1A
		public static Pen FromSystemColor(Color c)
		{
			if (c.IsSystemColor)
			{
				return new Pen(c)
				{
					isModifiable = false
				};
			}
			throw new ArgumentException(string.Format("The color {0} is not a system color.", c));
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the active window's border.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the active window's border.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x00014D48 File Offset: 0x00012F48
		public static Pen ActiveBorder
		{
			get
			{
				if (SystemPens.active_border == null)
				{
					SystemPens.active_border = new Pen(SystemColors.ActiveBorder);
					SystemPens.active_border.isModifiable = false;
				}
				return SystemPens.active_border;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the background of the active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the background of the active window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x00014D70 File Offset: 0x00012F70
		public static Pen ActiveCaption
		{
			get
			{
				if (SystemPens.active_caption == null)
				{
					SystemPens.active_caption = new Pen(SystemColors.ActiveCaption);
					SystemPens.active_caption.isModifiable = false;
				}
				return SystemPens.active_caption;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the application workspace.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the application workspace.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x00014D98 File Offset: 0x00012F98
		public static Pen AppWorkspace
		{
			get
			{
				if (SystemPens.app_workspace == null)
				{
					SystemPens.app_workspace = new Pen(SystemColors.AppWorkspace);
					SystemPens.app_workspace.isModifiable = false;
				}
				return SystemPens.app_workspace;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the face color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the face color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x00014DC0 File Offset: 0x00012FC0
		public static Pen ButtonFace
		{
			get
			{
				if (SystemPens.button_face == null)
				{
					SystemPens.button_face = new Pen(SystemColors.ButtonFace);
					SystemPens.button_face.isModifiable = false;
				}
				return SystemPens.button_face;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the highlight color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the highlight color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x00014DE8 File Offset: 0x00012FE8
		public static Pen ButtonHighlight
		{
			get
			{
				if (SystemPens.button_highlight == null)
				{
					SystemPens.button_highlight = new Pen(SystemColors.ButtonHighlight);
					SystemPens.button_highlight.isModifiable = false;
				}
				return SystemPens.button_highlight;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the shadow color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the shadow color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x00014E10 File Offset: 0x00013010
		public static Pen ButtonShadow
		{
			get
			{
				if (SystemPens.button_shadow == null)
				{
					SystemPens.button_shadow = new Pen(SystemColors.ButtonShadow);
					SystemPens.button_shadow.isModifiable = false;
				}
				return SystemPens.button_shadow;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the Windows desktop.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the Windows desktop.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x00014E38 File Offset: 0x00013038
		public static Pen Desktop
		{
			get
			{
				if (SystemPens.desktop == null)
				{
					SystemPens.desktop = new Pen(SystemColors.Desktop);
					SystemPens.desktop.isModifiable = false;
				}
				return SystemPens.desktop;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the lightest color in the color gradient of an active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the lightest color in the color gradient of an active window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x00014E60 File Offset: 0x00013060
		public static Pen GradientActiveCaption
		{
			get
			{
				if (SystemPens.gradient_activecaption == null)
				{
					SystemPens.gradient_activecaption = new Pen(SystemColors.GradientActiveCaption);
					SystemPens.gradient_activecaption.isModifiable = false;
				}
				return SystemPens.gradient_activecaption;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the lightest color in the color gradient of an inactive window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the lightest color in the color gradient of an inactive window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x00014E88 File Offset: 0x00013088
		public static Pen GradientInactiveCaption
		{
			get
			{
				if (SystemPens.gradient_inactivecaption == null)
				{
					SystemPens.gradient_inactivecaption = new Pen(SystemColors.GradientInactiveCaption);
					SystemPens.gradient_inactivecaption.isModifiable = false;
				}
				return SystemPens.gradient_inactivecaption;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color used to designate a hot-tracked item.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color used to designate a hot-tracked item.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x00014EB0 File Offset: 0x000130B0
		public static Pen HotTrack
		{
			get
			{
				if (SystemPens.hot_track == null)
				{
					SystemPens.hot_track = new Pen(SystemColors.HotTrack);
					SystemPens.hot_track.isModifiable = false;
				}
				return SystemPens.hot_track;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> is the color of the border of an inactive window.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the border of an inactive window.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x00014ED8 File Offset: 0x000130D8
		public static Pen InactiveBorder
		{
			get
			{
				if (SystemPens.inactive_border == null)
				{
					SystemPens.inactive_border = new Pen(SystemColors.InactiveBorder);
					SystemPens.inactive_border.isModifiable = false;
				}
				return SystemPens.inactive_border;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the title bar caption of an inactive window.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the title bar caption of an inactive window.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x00014F00 File Offset: 0x00013100
		public static Pen InactiveCaption
		{
			get
			{
				if (SystemPens.inactive_caption == null)
				{
					SystemPens.inactive_caption = new Pen(SystemColors.InactiveCaption);
					SystemPens.inactive_caption.isModifiable = false;
				}
				return SystemPens.inactive_caption;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the background of a ToolTip.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the background of a ToolTip.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x00014F28 File Offset: 0x00013128
		public static Pen Info
		{
			get
			{
				if (SystemPens.info == null)
				{
					SystemPens.info = new Pen(SystemColors.Info);
					SystemPens.info.isModifiable = false;
				}
				return SystemPens.info;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of a menu's background.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of a menu's background.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x00014F50 File Offset: 0x00013150
		public static Pen Menu
		{
			get
			{
				if (SystemPens.menu == null)
				{
					SystemPens.menu = new Pen(SystemColors.Menu);
					SystemPens.menu.isModifiable = false;
				}
				return SystemPens.menu;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the background of a menu bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the background of a menu bar.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x00014F78 File Offset: 0x00013178
		public static Pen MenuBar
		{
			get
			{
				if (SystemPens.menu_bar == null)
				{
					SystemPens.menu_bar = new Pen(SystemColors.MenuBar);
					SystemPens.menu_bar.isModifiable = false;
				}
				return SystemPens.menu_bar;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color used to highlight menu items when the menu appears as a flat menu.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color used to highlight menu items when the menu appears as a flat menu.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x00014FA0 File Offset: 0x000131A0
		public static Pen MenuHighlight
		{
			get
			{
				if (SystemPens.menu_highlight == null)
				{
					SystemPens.menu_highlight = new Pen(SystemColors.MenuHighlight);
					SystemPens.menu_highlight.isModifiable = false;
				}
				return SystemPens.menu_highlight;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the background of a scroll bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the background of a scroll bar.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x00014FC8 File Offset: 0x000131C8
		public static Pen ScrollBar
		{
			get
			{
				if (SystemPens.scroll_bar == null)
				{
					SystemPens.scroll_bar = new Pen(SystemColors.ScrollBar);
					SystemPens.scroll_bar.isModifiable = false;
				}
				return SystemPens.scroll_bar;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of the background in the client area of a window.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of the background in the client area of a window.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x00014FF0 File Offset: 0x000131F0
		public static Pen Window
		{
			get
			{
				if (SystemPens.window == null)
				{
					SystemPens.window = new Pen(SystemColors.Window);
					SystemPens.window.isModifiable = false;
				}
				return SystemPens.window;
			}
		}

		// Token: 0x0400055D RID: 1373
		private static Pen active_caption_text;

		// Token: 0x0400055E RID: 1374
		private static Pen control;

		// Token: 0x0400055F RID: 1375
		private static Pen control_dark;

		// Token: 0x04000560 RID: 1376
		private static Pen control_dark_dark;

		// Token: 0x04000561 RID: 1377
		private static Pen control_light;

		// Token: 0x04000562 RID: 1378
		private static Pen control_light_light;

		// Token: 0x04000563 RID: 1379
		private static Pen control_text;

		// Token: 0x04000564 RID: 1380
		private static Pen gray_text;

		// Token: 0x04000565 RID: 1381
		private static Pen highlight;

		// Token: 0x04000566 RID: 1382
		private static Pen highlight_text;

		// Token: 0x04000567 RID: 1383
		private static Pen inactive_caption_text;

		// Token: 0x04000568 RID: 1384
		private static Pen info_text;

		// Token: 0x04000569 RID: 1385
		private static Pen menu_text;

		// Token: 0x0400056A RID: 1386
		private static Pen window_frame;

		// Token: 0x0400056B RID: 1387
		private static Pen window_text;

		// Token: 0x0400056C RID: 1388
		private static Pen active_border;

		// Token: 0x0400056D RID: 1389
		private static Pen active_caption;

		// Token: 0x0400056E RID: 1390
		private static Pen app_workspace;

		// Token: 0x0400056F RID: 1391
		private static Pen button_face;

		// Token: 0x04000570 RID: 1392
		private static Pen button_highlight;

		// Token: 0x04000571 RID: 1393
		private static Pen button_shadow;

		// Token: 0x04000572 RID: 1394
		private static Pen desktop;

		// Token: 0x04000573 RID: 1395
		private static Pen gradient_activecaption;

		// Token: 0x04000574 RID: 1396
		private static Pen gradient_inactivecaption;

		// Token: 0x04000575 RID: 1397
		private static Pen hot_track;

		// Token: 0x04000576 RID: 1398
		private static Pen inactive_border;

		// Token: 0x04000577 RID: 1399
		private static Pen inactive_caption;

		// Token: 0x04000578 RID: 1400
		private static Pen info;

		// Token: 0x04000579 RID: 1401
		private static Pen menu;

		// Token: 0x0400057A RID: 1402
		private static Pen menu_bar;

		// Token: 0x0400057B RID: 1403
		private static Pen menu_highlight;

		// Token: 0x0400057C RID: 1404
		private static Pen scroll_bar;

		// Token: 0x0400057D RID: 1405
		private static Pen window;
	}
}
