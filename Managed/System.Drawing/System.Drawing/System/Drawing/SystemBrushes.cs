using System;
using Unity;

namespace System.Drawing
{
	/// <summary>Each property of the <see cref="T:System.Drawing.SystemBrushes" /> class is a <see cref="T:System.Drawing.SolidBrush" /> that is the color of a Windows display element.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000038 RID: 56
	public static class SystemBrushes
	{
		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the active window's border.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the active window's border.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003959 File Offset: 0x00001B59
		public static Brush ActiveBorder
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ActiveBorder);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of the active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of the active window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003965 File Offset: 0x00001B65
		public static Brush ActiveCaption
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ActiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text in the active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of the active window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003971 File Offset: 0x00001B71
		public static Brush ActiveCaptionText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ActiveCaptionText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the application workspace. </summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the application workspace.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000397D File Offset: 0x00001B7D
		public static Brush AppWorkspace
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.AppWorkspace);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the face color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the face color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003989 File Offset: 0x00001B89
		public static Brush ButtonFace
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ButtonFace);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the highlight color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the highlight color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003995 File Offset: 0x00001B95
		public static Brush ButtonHighlight
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ButtonHighlight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the shadow color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the shadow color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000039A1 File Offset: 0x00001BA1
		public static Brush ButtonShadow
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ButtonShadow);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the face color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the face color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000039AD File Offset: 0x00001BAD
		public static Brush Control
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.Control);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the highlight color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the highlight color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000039B9 File Offset: 0x00001BB9
		public static Brush ControlLightLight
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ControlLightLight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the light color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the light color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000039C5 File Offset: 0x00001BC5
		public static Brush ControlLight
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ControlLight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the shadow color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the shadow color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000039D1 File Offset: 0x00001BD1
		public static Brush ControlDark
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ControlDark);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the dark shadow color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the dark shadow color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000039DD File Offset: 0x00001BDD
		public static Brush ControlDarkDark
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ControlDarkDark);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of text in a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of text in a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x000039E9 File Offset: 0x00001BE9
		public static Brush ControlText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ControlText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the desktop.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the desktop.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000039F5 File Offset: 0x00001BF5
		public static Brush Desktop
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.Desktop);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the lightest color in the color gradient of an active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the lightest color in the color gradient of an active window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00003A01 File Offset: 0x00001C01
		public static Brush GradientActiveCaption
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.GradientActiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the lightest color in the color gradient of an inactive window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the lightest color in the color gradient of an inactive window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003A0D File Offset: 0x00001C0D
		public static Brush GradientInactiveCaption
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.GradientInactiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of dimmed text.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of dimmed text.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00003A19 File Offset: 0x00001C19
		public static Brush GrayText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.GrayText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of selected items. </summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of selected items.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003A25 File Offset: 0x00001C25
		public static Brush Highlight
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.Highlight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text of selected items. </summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text of selected items.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00003A31 File Offset: 0x00001C31
		public static Brush HighlightText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.HighlightText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color used to designate a hot-tracked item. </summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color used to designate a hot-tracked item.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003A3D File Offset: 0x00001C3D
		public static Brush HotTrack
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.HotTrack);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of an inactive window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of an inactive window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00003A49 File Offset: 0x00001C49
		public static Brush InactiveCaption
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.InactiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of an inactive window's border.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of an inactive window's border.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00003A55 File Offset: 0x00001C55
		public static Brush InactiveBorder
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.InactiveBorder);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text in an inactive window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text in an inactive window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00003A61 File Offset: 0x00001C61
		public static Brush InactiveCaptionText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.InactiveCaptionText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a ToolTip.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a ToolTip.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003A6D File Offset: 0x00001C6D
		public static Brush Info
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.Info);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text of a ToolTip.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> is the color of the text of a ToolTip.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00003A79 File Offset: 0x00001C79
		public static Brush InfoText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.InfoText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of a menu's background.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of a menu's background.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00003A85 File Offset: 0x00001C85
		public static Brush Menu
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.Menu);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a menu bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a menu bar.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00003A91 File Offset: 0x00001C91
		public static Brush MenuBar
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.MenuBar);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color used to highlight menu items when the menu appears as a flat menu.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color used to highlight menu items when the menu appears as a flat menu.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00003A9D File Offset: 0x00001C9D
		public static Brush MenuHighlight
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.MenuHighlight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of a menu's text.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of a menu's text.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00003AA9 File Offset: 0x00001CA9
		public static Brush MenuText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.MenuText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a scroll bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a scroll bar.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003AB5 File Offset: 0x00001CB5
		public static Brush ScrollBar
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ScrollBar);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background in the client area of a window.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background in the client area of a window.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00003AC1 File Offset: 0x00001CC1
		public static Brush Window
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.Window);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of a window frame.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of a window frame.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00003ACD File Offset: 0x00001CCD
		public static Brush WindowFrame
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.WindowFrame);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text in the client area of a window.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text in the client area of a window.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00003AD9 File Offset: 0x00001CD9
		public static Brush WindowText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.WindowText);
			}
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Brush" /> from the specified <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The <see cref="T:System.Drawing.Brush" /> this method creates.</returns>
		/// <param name="c">The <see cref="T:System.Drawing.Color" /> structure from which to create the <see cref="T:System.Drawing.Brush" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060000EA RID: 234 RVA: 0x00003AE8 File Offset: 0x00001CE8
		public static Brush FromSystemColor(Color c)
		{
			if (!c.IsSystemColor())
			{
				throw new ArgumentException(SR.Format("The color {0} is not a system color.", new object[] { c.ToString() }));
			}
			Brush[] array = (Brush[])SafeNativeMethods.Gdip.ThreadData[SystemBrushes.s_systemBrushesKey];
			if (array == null)
			{
				array = new Brush[33];
				SafeNativeMethods.Gdip.ThreadData[SystemBrushes.s_systemBrushesKey] = array;
			}
			int num = (int)c.ToKnownColor();
			if (num > 167)
			{
				num -= 141;
			}
			num--;
			if (array[num] == null)
			{
				array[num] = new SolidBrush(c, true);
			}
			return array[num];
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003B8D File Offset: 0x00001D8D
		internal SystemBrushes()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040002B9 RID: 697
		private static readonly object s_systemBrushesKey = new object();
	}
}
