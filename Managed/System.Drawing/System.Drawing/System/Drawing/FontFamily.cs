using System;
using System.Drawing.Text;
using System.Text;

namespace System.Drawing
{
	/// <summary>Defines a group of type faces having a similar basic design and certain variations in styles. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000064 RID: 100
	public sealed class FontFamily : MarshalByRefObject, IDisposable
	{
		// Token: 0x06000382 RID: 898 RVA: 0x0000917A File Offset: 0x0000737A
		internal FontFamily(IntPtr fntfamily)
		{
			this.nativeFontFamily = fntfamily;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00009194 File Offset: 0x00007394
		internal void refreshName()
		{
			if (this.nativeFontFamily == IntPtr.Zero)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(32);
			GDIPlus.CheckStatus(GDIPlus.GdipGetFamilyName(this.nativeFontFamily, stringBuilder, 0));
			this.name = stringBuilder.ToString();
		}

		// Token: 0x06000384 RID: 900 RVA: 0x000091DC File Offset: 0x000073DC
		~FontFamily()
		{
			this.Dispose();
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00009208 File Offset: 0x00007408
		internal IntPtr NativeObject
		{
			get
			{
				return this.nativeFontFamily;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00009208 File Offset: 0x00007408
		internal IntPtr NativeFamily
		{
			get
			{
				return this.nativeFontFamily;
			}
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.FontFamily" /> from the specified generic font family.</summary>
		/// <param name="genericFamily">The <see cref="T:System.Drawing.Text.GenericFontFamilies" /> from which to create the new <see cref="T:System.Drawing.FontFamily" />. </param>
		// Token: 0x06000387 RID: 903 RVA: 0x00009210 File Offset: 0x00007410
		public FontFamily(GenericFontFamilies genericFamily)
		{
			Status status;
			switch (genericFamily)
			{
			case GenericFontFamilies.Serif:
				status = GDIPlus.GdipGetGenericFontFamilySerif(out this.nativeFontFamily);
				goto IL_004D;
			case GenericFontFamilies.SansSerif:
				status = GDIPlus.GdipGetGenericFontFamilySansSerif(out this.nativeFontFamily);
				goto IL_004D;
			}
			status = GDIPlus.GdipGetGenericFontFamilyMonospace(out this.nativeFontFamily);
			IL_004D:
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.FontFamily" /> with the specified name.</summary>
		/// <param name="name">The name of the new <see cref="T:System.Drawing.FontFamily" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string ("").-or-<paramref name="name" /> specifies a font that is not installed on the computer running the application.-or-<paramref name="name" /> specifies a font that is not a TrueType font.</exception>
		// Token: 0x06000388 RID: 904 RVA: 0x00009270 File Offset: 0x00007470
		public FontFamily(string name)
			: this(name, null)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.FontFamily" /> in the specified <see cref="T:System.Drawing.Text.FontCollection" /> with the specified name.</summary>
		/// <param name="name">A <see cref="T:System.String" /> that represents the name of the new <see cref="T:System.Drawing.FontFamily" />. </param>
		/// <param name="fontCollection">The <see cref="T:System.Drawing.Text.FontCollection" /> that contains this <see cref="T:System.Drawing.FontFamily" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string ("").-or-<paramref name="name" /> specifies a font that is not installed on the computer running the application.-or-<paramref name="name" /> specifies a font that is not a TrueType font.</exception>
		// Token: 0x06000389 RID: 905 RVA: 0x0000927C File Offset: 0x0000747C
		public FontFamily(string name, FontCollection fontCollection)
		{
			IntPtr intPtr = ((fontCollection == null) ? IntPtr.Zero : fontCollection._nativeFontCollection);
			GDIPlus.CheckStatus(GDIPlus.GdipCreateFontFamilyFromName(name, intPtr, out this.nativeFontFamily));
		}

		/// <summary>Gets the name of this <see cref="T:System.Drawing.FontFamily" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the name of this <see cref="T:System.Drawing.FontFamily" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600038A RID: 906 RVA: 0x000092BD File Offset: 0x000074BD
		public string Name
		{
			get
			{
				if (this.nativeFontFamily == IntPtr.Zero)
				{
					throw new ArgumentException("Name", Locale.GetText("Object was disposed."));
				}
				if (this.name == null)
				{
					this.refreshName();
				}
				return this.name;
			}
		}

		/// <summary>Gets a generic monospace <see cref="T:System.Drawing.FontFamily" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.FontFamily" /> that represents a generic monospace font.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600038B RID: 907 RVA: 0x000092FA File Offset: 0x000074FA
		public static FontFamily GenericMonospace
		{
			get
			{
				return new FontFamily(GenericFontFamilies.Monospace);
			}
		}

		/// <summary>Gets a generic sans serif <see cref="T:System.Drawing.FontFamily" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.FontFamily" /> object that represents a generic sans serif font.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00009302 File Offset: 0x00007502
		public static FontFamily GenericSansSerif
		{
			get
			{
				return new FontFamily(GenericFontFamilies.SansSerif);
			}
		}

		/// <summary>Gets a generic serif <see cref="T:System.Drawing.FontFamily" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.FontFamily" /> that represents a generic serif font.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000930A File Offset: 0x0000750A
		public static FontFamily GenericSerif
		{
			get
			{
				return new FontFamily(GenericFontFamilies.Serif);
			}
		}

		/// <summary>Returns the cell ascent, in design units, of the <see cref="T:System.Drawing.FontFamily" /> of the specified style.</summary>
		/// <returns>The cell ascent for this <see cref="T:System.Drawing.FontFamily" /> that uses the specified <see cref="T:System.Drawing.FontStyle" />.</returns>
		/// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> that contains style information for the font. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600038E RID: 910 RVA: 0x00009314 File Offset: 0x00007514
		public int GetCellAscent(FontStyle style)
		{
			short num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetCellAscent(this.nativeFontFamily, (int)style, out num));
			return (int)num;
		}

		/// <summary>Returns the cell descent, in design units, of the <see cref="T:System.Drawing.FontFamily" /> of the specified style. </summary>
		/// <returns>The cell descent metric for this <see cref="T:System.Drawing.FontFamily" /> that uses the specified <see cref="T:System.Drawing.FontStyle" />.</returns>
		/// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> that contains style information for the font. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600038F RID: 911 RVA: 0x00009338 File Offset: 0x00007538
		public int GetCellDescent(FontStyle style)
		{
			short num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetCellDescent(this.nativeFontFamily, (int)style, out num));
			return (int)num;
		}

		/// <summary>Gets the height, in font design units, of the em square for the specified style.</summary>
		/// <returns>The height of the em square.</returns>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> for which to get the em height. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000390 RID: 912 RVA: 0x0000935C File Offset: 0x0000755C
		public int GetEmHeight(FontStyle style)
		{
			short num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetEmHeight(this.nativeFontFamily, (int)style, out num));
			return (int)num;
		}

		/// <summary>Returns the line spacing, in design units, of the <see cref="T:System.Drawing.FontFamily" /> of the specified style. The line spacing is the vertical distance between the base lines of two consecutive lines of text. </summary>
		/// <returns>The distance between two consecutive lines of text.</returns>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> to apply. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000391 RID: 913 RVA: 0x00009380 File Offset: 0x00007580
		public int GetLineSpacing(FontStyle style)
		{
			short num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetLineSpacing(this.nativeFontFamily, (int)style, out num));
			return (int)num;
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Drawing.FontStyle" /> enumeration is available.</summary>
		/// <returns>true if the specified <see cref="T:System.Drawing.FontStyle" /> is available; otherwise, false.</returns>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> to test. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000392 RID: 914 RVA: 0x000093A4 File Offset: 0x000075A4
		[MonoDocumentationNote("When used with libgdiplus this method always return true (styles are created on demand).")]
		public bool IsStyleAvailable(FontStyle style)
		{
			bool flag;
			GDIPlus.CheckStatus(GDIPlus.GdipIsStyleAvailable(this.nativeFontFamily, (int)style, out flag));
			return flag;
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.FontFamily" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000393 RID: 915 RVA: 0x000093C5 File Offset: 0x000075C5
		public void Dispose()
		{
			if (this.nativeFontFamily != IntPtr.Zero)
			{
				Status status = GDIPlus.GdipDeleteFontFamily(this.nativeFontFamily);
				this.nativeFontFamily = IntPtr.Zero;
				GC.SuppressFinalize(this);
				GDIPlus.CheckStatus(status);
			}
		}

		/// <summary>Indicates whether the specified object is a <see cref="T:System.Drawing.FontFamily" /> and is identical to this <see cref="T:System.Drawing.FontFamily" />.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.Drawing.FontFamily" /> and is identical to this <see cref="T:System.Drawing.FontFamily" />; otherwise, false.</returns>
		/// <param name="obj">The object to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000394 RID: 916 RVA: 0x000093FC File Offset: 0x000075FC
		public override bool Equals(object obj)
		{
			FontFamily fontFamily = obj as FontFamily;
			return fontFamily != null && this.Name == fontFamily.Name;
		}

		/// <summary>Gets a hash code for this <see cref="T:System.Drawing.FontFamily" />.</summary>
		/// <returns>The hash code for this <see cref="T:System.Drawing.FontFamily" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000395 RID: 917 RVA: 0x00009426 File Offset: 0x00007626
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		/// <summary>Returns an array that contains all the <see cref="T:System.Drawing.FontFamily" /> objects associated with the current graphics context.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.FontFamily" /> objects associated with the current graphics context.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00009433 File Offset: 0x00007633
		public static FontFamily[] Families
		{
			get
			{
				return new InstalledFontCollection().Families;
			}
		}

		/// <summary>Returns an array that contains all the <see cref="T:System.Drawing.FontFamily" /> objects available for the specified graphics context.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.FontFamily" /> objects available for the specified <see cref="T:System.Drawing.Graphics" /> object.</returns>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> object from which to return <see cref="T:System.Drawing.FontFamily" /> objects. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="graphics " />isnull.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000397 RID: 919 RVA: 0x0000943F File Offset: 0x0000763F
		public static FontFamily[] GetFamilies(Graphics graphics)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			return new InstalledFontCollection().Families;
		}

		/// <summary>Returns the name, in the specified language, of this <see cref="T:System.Drawing.FontFamily" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the name, in the specified language, of this <see cref="T:System.Drawing.FontFamily" />. </returns>
		/// <param name="language">The language in which the name is returned. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000398 RID: 920 RVA: 0x00009459 File Offset: 0x00007659
		[MonoLimitation("The language parameter is ignored. We always return the name using the default system language.")]
		public string GetName(int language)
		{
			return this.Name;
		}

		/// <summary>Converts this <see cref="T:System.Drawing.FontFamily" /> to a human-readable string representation.</summary>
		/// <returns>The string that represents this <see cref="T:System.Drawing.FontFamily" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000399 RID: 921 RVA: 0x00009461 File Offset: 0x00007661
		public override string ToString()
		{
			return "[FontFamily: Name=" + this.Name + "]";
		}

		// Token: 0x040003BA RID: 954
		private string name;

		// Token: 0x040003BB RID: 955
		private IntPtr nativeFontFamily = IntPtr.Zero;
	}
}
