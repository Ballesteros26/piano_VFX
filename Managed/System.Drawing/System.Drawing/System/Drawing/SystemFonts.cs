using System;

namespace System.Drawing
{
	/// <summary>Specifies the fonts used to display text in Windows display elements.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200008A RID: 138
	public sealed class SystemFonts
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x00002050 File Offset: 0x00000250
		private SystemFonts()
		{
		}

		/// <summary>Returns a font object that corresponds to the specified system font name.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> if the specified name matches a value in <see cref="T:System.Drawing.SystemFonts" />; otherwise, null.</returns>
		/// <param name="systemFontName">The name of the system font you need a font object for.</param>
		// Token: 0x06000748 RID: 1864 RVA: 0x000148B0 File Offset: 0x00012AB0
		public static Font GetFontByName(string systemFontName)
		{
			if (systemFontName == "CaptionFont")
			{
				return SystemFonts.CaptionFont;
			}
			if (systemFontName == "DefaultFont")
			{
				return SystemFonts.DefaultFont;
			}
			if (systemFontName == "DialogFont")
			{
				return SystemFonts.DialogFont;
			}
			if (systemFontName == "IconTitleFont")
			{
				return SystemFonts.IconTitleFont;
			}
			if (systemFontName == "MenuFont")
			{
				return SystemFonts.MenuFont;
			}
			if (systemFontName == "MessageBoxFont")
			{
				return SystemFonts.MessageBoxFont;
			}
			if (systemFontName == "SmallCaptionFont")
			{
				return SystemFonts.SmallCaptionFont;
			}
			if (systemFontName == "StatusFont")
			{
				return SystemFonts.StatusFont;
			}
			return null;
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used to display text in the title bars of windows.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that is used to display text in the title bars of windows.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x00014956 File Offset: 0x00012B56
		public static Font CaptionFont
		{
			get
			{
				return new Font("Microsoft Sans Serif", 11f, "CaptionFont");
			}
		}

		/// <summary>Gets the default font that applications can use for dialog boxes and forms.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Font" /> of the system. The value returned will vary depending on the user's operating system and the local culture setting of their system.</returns>
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x0001496C File Offset: 0x00012B6C
		public static Font DefaultFont
		{
			get
			{
				return new Font("Microsoft Sans Serif", 8.25f, "DefaultFont");
			}
		}

		/// <summary>Gets a font that applications can use for dialog boxes and forms.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that can be used for dialog boxes and forms, depending on the operating system and local culture setting of the system.</returns>
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x00014982 File Offset: 0x00012B82
		public static Font DialogFont
		{
			get
			{
				return new Font("Tahoma", 8f, "DialogFont");
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used for icon titles.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that is used for icon titles.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x00014998 File Offset: 0x00012B98
		public static Font IconTitleFont
		{
			get
			{
				return new Font("Microsoft Sans Serif", 11f, "IconTitleFont");
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used for menus.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that is used for menus.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x000149AE File Offset: 0x00012BAE
		public static Font MenuFont
		{
			get
			{
				return new Font("Microsoft Sans Serif", 11f, "MenuFont");
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used for message boxes.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that is used for message boxes</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x000149C4 File Offset: 0x00012BC4
		public static Font MessageBoxFont
		{
			get
			{
				return new Font("Microsoft Sans Serif", 11f, "MessageBoxFont");
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used to display text in the title bars of small windows, such as tool windows.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that is used to display text in the title bars of small windows, such as tool windows.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x000149DA File Offset: 0x00012BDA
		public static Font SmallCaptionFont
		{
			get
			{
				return new Font("Microsoft Sans Serif", 11f, "SmallCaptionFont");
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used to display text in the status bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that is used to display text in the status bar.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x000149F0 File Offset: 0x00012BF0
		public static Font StatusFont
		{
			get
			{
				return new Font("Microsoft Sans Serif", 11f, "StatusFont");
			}
		}
	}
}
