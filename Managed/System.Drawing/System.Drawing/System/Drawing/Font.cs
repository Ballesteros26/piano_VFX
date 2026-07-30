using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Drawing
{
	/// <summary>Defines a particular format for text, including font face, size, and style attributes. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000060 RID: 96
	[Editor("System.Drawing.Design.FontEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[ComVisible(true)]
	[TypeConverter(typeof(FontConverter))]
	[Serializable]
	public sealed class Font : MarshalByRefObject, ISerializable, ICloneable, IDisposable
	{
		// Token: 0x06000338 RID: 824 RVA: 0x00007E94 File Offset: 0x00006094
		private void CreateFont(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte charSet, bool isVertical)
		{
			this.originalFontName = familyName;
			FontFamily fontFamily;
			try
			{
				fontFamily = new FontFamily(familyName);
			}
			catch (Exception)
			{
				fontFamily = FontFamily.GenericSansSerif;
			}
			this.setProperties(fontFamily, emSize, style, unit, charSet, isVertical);
			Status status = GDIPlus.GdipCreateFont(fontFamily.NativeFamily, emSize, style, unit, out this.fontObject);
			if (status == Status.FontStyleNotFound)
			{
				throw new ArgumentException(Locale.GetText("Style {0} isn't supported by font {1}.", new object[]
				{
					style.ToString(),
					familyName
				}));
			}
			GDIPlus.CheckStatus(status);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00007F24 File Offset: 0x00006124
		private Font(SerializationInfo info, StreamingContext context)
		{
			string text = (string)info.GetValue("Name", typeof(string));
			float num = (float)info.GetValue("Size", typeof(float));
			FontStyle fontStyle = (FontStyle)info.GetValue("Style", typeof(FontStyle));
			GraphicsUnit graphicsUnit = (GraphicsUnit)info.GetValue("Unit", typeof(GraphicsUnit));
			this.CreateFont(text, num, fontStyle, graphicsUnit, 1, false);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x0600033A RID: 826 RVA: 0x00007FBC File Offset: 0x000061BC
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			si.AddValue("Name", this.Name);
			si.AddValue("Size", this.Size);
			si.AddValue("Style", this.Style);
			si.AddValue("Unit", this.Unit);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00008018 File Offset: 0x00006218
		~Font()
		{
			this.Dispose();
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Font" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600033C RID: 828 RVA: 0x00008044 File Offset: 0x00006244
		public void Dispose()
		{
			if (this.fontObject != IntPtr.Zero)
			{
				Status status = GDIPlus.GdipDeleteFont(this.fontObject);
				this.fontObject = IntPtr.Zero;
				GC.SuppressFinalize(this);
				GDIPlus.CheckStatus(status);
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00008079 File Offset: 0x00006279
		internal void SetSystemFontName(string newSystemFontName)
		{
			this.systemFontName = newSystemFontName;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00008084 File Offset: 0x00006284
		internal void unitConversion(GraphicsUnit fromUnit, GraphicsUnit toUnit, float nSrc, out float nTrg)
		{
			nTrg = 0f;
			float num;
			switch (fromUnit)
			{
			case GraphicsUnit.World:
			case GraphicsUnit.Pixel:
				num = nSrc / Graphics.systemDpiX;
				break;
			case GraphicsUnit.Display:
				num = nSrc / 75f;
				break;
			case GraphicsUnit.Point:
				num = nSrc / 72f;
				break;
			case GraphicsUnit.Inch:
				num = nSrc;
				break;
			case GraphicsUnit.Document:
				num = nSrc / 300f;
				break;
			case GraphicsUnit.Millimeter:
				num = nSrc / 25.4f;
				break;
			default:
				throw new ArgumentException("Invalid GraphicsUnit");
			}
			switch (toUnit)
			{
			case GraphicsUnit.World:
			case GraphicsUnit.Pixel:
				nTrg = num * Graphics.systemDpiX;
				return;
			case GraphicsUnit.Display:
				nTrg = num * 75f;
				return;
			case GraphicsUnit.Point:
				nTrg = num * 72f;
				return;
			case GraphicsUnit.Inch:
				nTrg = num;
				return;
			case GraphicsUnit.Document:
				nTrg = num * 300f;
				return;
			case GraphicsUnit.Millimeter:
				nTrg = num * 25.4f;
				return;
			default:
				throw new ArgumentException("Invalid GraphicsUnit");
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00008170 File Offset: 0x00006370
		private void setProperties(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte charSet, bool isVertical)
		{
			this._name = family.Name;
			this._fontFamily = family;
			this._size = emSize;
			this._unit = unit;
			this._style = style;
			this._gdiCharSet = charSet;
			this._gdiVerticalFont = isVertical;
			this.unitConversion(unit, GraphicsUnit.Point, emSize, out this._sizeInPoints);
			this._bold = (this._italic = (this._strikeout = (this._underline = false)));
			if ((style & FontStyle.Bold) == FontStyle.Bold)
			{
				this._bold = true;
			}
			if ((style & FontStyle.Italic) == FontStyle.Italic)
			{
				this._italic = true;
			}
			if ((style & FontStyle.Strikeout) == FontStyle.Strikeout)
			{
				this._strikeout = true;
			}
			if ((style & FontStyle.Underline) == FontStyle.Underline)
			{
				this._underline = true;
			}
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Font" /> from the specified Windows handle.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> this method creates.</returns>
		/// <param name="hfont">A Windows handle to a GDI font. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hfont" /> points to an object that is not a TrueType font.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000340 RID: 832 RVA: 0x0000821C File Offset: 0x0000641C
		public static Font FromHfont(IntPtr hfont)
		{
			FontStyle fontStyle = FontStyle.Regular;
			LOGFONT logfont = default(LOGFONT);
			if (hfont == IntPtr.Zero)
			{
				return new Font("Arial", 10f, FontStyle.Regular);
			}
			IntPtr intPtr;
			if (GDIPlus.RunningOnUnix())
			{
				GDIPlus.CheckStatus(GDIPlus.GdipCreateFontFromHfont(hfont, out intPtr, ref logfont));
			}
			else
			{
				IntPtr dc = GDIPlus.GetDC(IntPtr.Zero);
				try
				{
					return Font.FromLogFont(logfont, dc);
				}
				finally
				{
					GDIPlus.ReleaseDC(IntPtr.Zero, dc);
				}
			}
			if (logfont.lfItalic != 0)
			{
				fontStyle |= FontStyle.Italic;
			}
			if (logfont.lfUnderline != 0)
			{
				fontStyle |= FontStyle.Underline;
			}
			if (logfont.lfStrikeOut != 0)
			{
				fontStyle |= FontStyle.Strikeout;
			}
			if (logfont.lfWeight > 400U)
			{
				fontStyle |= FontStyle.Bold;
			}
			float num;
			if (logfont.lfHeight < 0)
			{
				num = (float)(logfont.lfHeight * -1);
			}
			else
			{
				num = (float)logfont.lfHeight;
			}
			return new Font(intPtr, logfont.lfFaceName, fontStyle, num);
		}

		/// <summary>Returns a handle to this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>A Windows handle to this <see cref="T:System.Drawing.Font" />.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operation was unsuccessful.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000341 RID: 833 RVA: 0x00008310 File Offset: 0x00006510
		public IntPtr ToHfont()
		{
			if (this.fontObject == IntPtr.Zero)
			{
				throw new ArgumentException(Locale.GetText("Object has been disposed."));
			}
			if (GDIPlus.RunningOnUnix())
			{
				return this.fontObject;
			}
			if (this.olf == null)
			{
				this.olf = default(LOGFONT);
				this.ToLogFont(this.olf);
			}
			LOGFONT logfont = (LOGFONT)this.olf;
			return GDIPlus.CreateFontIndirect(ref logfont);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00008388 File Offset: 0x00006588
		internal Font(IntPtr newFontObject, string familyName, FontStyle style, float size)
		{
			FontFamily fontFamily;
			try
			{
				fontFamily = new FontFamily(familyName);
			}
			catch (Exception)
			{
				fontFamily = FontFamily.GenericSansSerif;
			}
			this.setProperties(fontFamily, size, style, GraphicsUnit.Pixel, 0, false);
			this.fontObject = newFontObject;
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> that uses the specified existing <see cref="T:System.Drawing.Font" /> and <see cref="T:System.Drawing.FontStyle" /> enumeration.</summary>
		/// <param name="prototype">The existing <see cref="T:System.Drawing.Font" /> from which to create the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="newStyle">The <see cref="T:System.Drawing.FontStyle" /> to apply to the new <see cref="T:System.Drawing.Font" />. Multiple values of the <see cref="T:System.Drawing.FontStyle" /> enumeration can be combined with the OR operator. </param>
		// Token: 0x06000343 RID: 835 RVA: 0x000083DC File Offset: 0x000065DC
		public Font(Font prototype, FontStyle newStyle)
		{
			this.setProperties(prototype.FontFamily, prototype.Size, newStyle, prototype.Unit, prototype.GdiCharSet, prototype.GdiVerticalFont);
			GDIPlus.CheckStatus(GDIPlus.GdipCreateFont(this._fontFamily.NativeFamily, this.Size, this.Style, this.Unit, out this.fontObject));
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size and unit. Sets the style to <see cref="F:System.Drawing.FontStyle.Regular" />.</summary>
		/// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="family" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number. </exception>
		// Token: 0x06000344 RID: 836 RVA: 0x0000844C File Offset: 0x0000664C
		public Font(FontFamily family, float emSize, GraphicsUnit unit)
			: this(family, emSize, FontStyle.Regular, unit, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size and unit. The style is set to <see cref="F:System.Drawing.FontStyle.Regular" />.</summary>
		/// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number. </exception>
		// Token: 0x06000345 RID: 837 RVA: 0x0000845A File Offset: 0x0000665A
		public Font(string familyName, float emSize, GraphicsUnit unit)
			: this(new FontFamily(familyName), emSize, FontStyle.Regular, unit, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size. </summary>
		/// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size, in points, of the new font. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number. </exception>
		// Token: 0x06000346 RID: 838 RVA: 0x0000846D File Offset: 0x0000666D
		public Font(FontFamily family, float emSize)
			: this(family, emSize, FontStyle.Regular, GraphicsUnit.Point, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size and style. </summary>
		/// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size, in points, of the new font. </param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="family" /> is null.</exception>
		// Token: 0x06000347 RID: 839 RVA: 0x0000847B File Offset: 0x0000667B
		public Font(FontFamily family, float emSize, FontStyle style)
			: this(family, emSize, style, GraphicsUnit.Point, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size, style, and unit.</summary>
		/// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font. </param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="family" /> is null.</exception>
		// Token: 0x06000348 RID: 840 RVA: 0x00008489 File Offset: 0x00006689
		public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit)
			: this(family, emSize, style, unit, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size, style, unit, and character set.</summary>
		/// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font. </param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font. </param>
		/// <param name="gdiCharSet">A <see cref="T:System.Byte" /> that specifies a GDI character set to use for the new font. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="family" /> is null.</exception>
		// Token: 0x06000349 RID: 841 RVA: 0x00008498 File Offset: 0x00006698
		public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet)
			: this(family, emSize, style, unit, gdiCharSet, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size, style, unit, and character set.</summary>
		/// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font. </param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font. </param>
		/// <param name="gdiCharSet">A <see cref="T:System.Byte" /> that specifies a GDI character set to use for this font. </param>
		/// <param name="gdiVerticalFont">A Boolean value indicating whether the new font is derived from a GDI vertical font. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="family" /> is null </exception>
		// Token: 0x0600034A RID: 842 RVA: 0x000084A8 File Offset: 0x000066A8
		public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont)
		{
			if (family == null)
			{
				throw new ArgumentNullException("family");
			}
			this.setProperties(family, emSize, style, unit, gdiCharSet, gdiVerticalFont);
			GDIPlus.CheckStatus(GDIPlus.GdipCreateFont(family.NativeFamily, emSize, style, unit, out this.fontObject));
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size. </summary>
		/// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size, in points, of the new font. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity or is not a valid number. </exception>
		// Token: 0x0600034B RID: 843 RVA: 0x000084FD File Offset: 0x000066FD
		public Font(string familyName, float emSize)
			: this(familyName, emSize, FontStyle.Regular, GraphicsUnit.Point, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size and style. </summary>
		/// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size, in points, of the new font. </param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number. </exception>
		// Token: 0x0600034C RID: 844 RVA: 0x0000850B File Offset: 0x0000670B
		public Font(string familyName, float emSize, FontStyle style)
			: this(familyName, emSize, style, GraphicsUnit.Point, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size, style, and unit.</summary>
		/// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font. </param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity or is not a valid number. </exception>
		// Token: 0x0600034D RID: 845 RVA: 0x00008519 File Offset: 0x00006719
		public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit)
			: this(familyName, emSize, style, unit, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size, style, unit, and character set.</summary>
		/// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font. </param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font. </param>
		/// <param name="gdiCharSet">A <see cref="T:System.Byte" /> that specifies a GDI character set to use for this font. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number. </exception>
		// Token: 0x0600034E RID: 846 RVA: 0x00008528 File Offset: 0x00006728
		public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet)
			: this(familyName, emSize, style, unit, gdiCharSet, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using the specified size, style, unit, and character set.</summary>
		/// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter. </param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font. </param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font. </param>
		/// <param name="gdiCharSet">A <see cref="T:System.Byte" /> that specifies a GDI character set to use for this font. </param>
		/// <param name="gdiVerticalFont">A Boolean value indicating whether the new <see cref="T:System.Drawing.Font" /> is derived from a GDI vertical font. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number. </exception>
		// Token: 0x0600034F RID: 847 RVA: 0x00008538 File Offset: 0x00006738
		public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont)
		{
			this.CreateFont(familyName, emSize, style, unit, gdiCharSet, gdiVerticalFont);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000855A File Offset: 0x0000675A
		internal Font(string familyName, float emSize, string systemName)
			: this(familyName, emSize, FontStyle.Regular, GraphicsUnit.Point, 1, false)
		{
			this.systemFontName = systemName;
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> this method creates, cast as an <see cref="T:System.Object" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000351 RID: 849 RVA: 0x0000856F File Offset: 0x0000676F
		public object Clone()
		{
			return new Font(this, this.Style);
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000857D File Offset: 0x0000677D
		internal IntPtr NativeObject
		{
			get
			{
				return this.fontObject;
			}
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Drawing.Font" /> is bold.</summary>
		/// <returns>true if this <see cref="T:System.Drawing.Font" /> is bold; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00008585 File Offset: 0x00006785
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Bold
		{
			get
			{
				return this._bold;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.FontFamily" /> associated with this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.FontFamily" /> associated with this <see cref="T:System.Drawing.Font" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000858D File Offset: 0x0000678D
		[Browsable(false)]
		public FontFamily FontFamily
		{
			get
			{
				return this._fontFamily;
			}
		}

		/// <summary>Gets a byte value that specifies the GDI character set that this <see cref="T:System.Drawing.Font" /> uses.</summary>
		/// <returns>A byte value that specifies the GDI character set that this <see cref="T:System.Drawing.Font" /> uses. The default is 1.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00008595 File Offset: 0x00006795
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public byte GdiCharSet
		{
			get
			{
				return this._gdiCharSet;
			}
		}

		/// <summary>Gets a Boolean value that indicates whether this <see cref="T:System.Drawing.Font" /> is derived from a GDI vertical font.</summary>
		/// <returns>true if this <see cref="T:System.Drawing.Font" /> is derived from a GDI vertical font; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000859D File Offset: 0x0000679D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool GdiVerticalFont
		{
			get
			{
				return this._gdiVerticalFont;
			}
		}

		/// <summary>Gets the line spacing of this font.</summary>
		/// <returns>The line spacing, in pixels, of this font. </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000357 RID: 855 RVA: 0x000085A5 File Offset: 0x000067A5
		[Browsable(false)]
		public int Height
		{
			get
			{
				return (int)Math.Ceiling((double)this.GetHeight());
			}
		}

		/// <summary>Gets a value indicating whether the font is a member of <see cref="T:System.Drawing.SystemFonts" />. </summary>
		/// <returns>true if the font is a member of <see cref="T:System.Drawing.SystemFonts" />; otherwise, false. The default is false.</returns>
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000358 RID: 856 RVA: 0x000085B4 File Offset: 0x000067B4
		[Browsable(false)]
		public bool IsSystemFont
		{
			get
			{
				return !string.IsNullOrEmpty(this.systemFontName);
			}
		}

		/// <summary>Gets a value that indicates whether this font has the italic style applied.</summary>
		/// <returns>true to indicate this font has the italic style applied; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000359 RID: 857 RVA: 0x000085C4 File Offset: 0x000067C4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Italic
		{
			get
			{
				return this._italic;
			}
		}

		/// <summary>Gets the face name of this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>A string representation of the face name of this <see cref="T:System.Drawing.Font" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600035A RID: 858 RVA: 0x000085CC File Offset: 0x000067CC
		[TypeConverter(typeof(FontConverter.FontNameConverter))]
		[Editor("System.Drawing.Design.FontNameEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Gets the em-size of this <see cref="T:System.Drawing.Font" /> measured in the units specified by the <see cref="P:System.Drawing.Font.Unit" /> property.</summary>
		/// <returns>The em-size of this <see cref="T:System.Drawing.Font" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600035B RID: 859 RVA: 0x000085D4 File Offset: 0x000067D4
		public float Size
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets the em-size, in points, of this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>The em-size, in points, of this <see cref="T:System.Drawing.Font" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600035C RID: 860 RVA: 0x000085DC File Offset: 0x000067DC
		[Browsable(false)]
		public float SizeInPoints
		{
			get
			{
				return this._sizeInPoints;
			}
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Drawing.Font" /> specifies a horizontal line through the font.</summary>
		/// <returns>true if this <see cref="T:System.Drawing.Font" /> has a horizontal line through it; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600035D RID: 861 RVA: 0x000085E4 File Offset: 0x000067E4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Strikeout
		{
			get
			{
				return this._strikeout;
			}
		}

		/// <summary>Gets style information for this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.FontStyle" /> enumeration that contains style information for this <see cref="T:System.Drawing.Font" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600035E RID: 862 RVA: 0x000085EC File Offset: 0x000067EC
		[Browsable(false)]
		public FontStyle Style
		{
			get
			{
				return this._style;
			}
		}

		/// <summary>Gets the name of the system font if the <see cref="P:System.Drawing.Font.IsSystemFont" /> property returns true.</summary>
		/// <returns>The name of the system font, if <see cref="P:System.Drawing.Font.IsSystemFont" /> returns true; otherwise, an empty string ("").</returns>
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600035F RID: 863 RVA: 0x000085F4 File Offset: 0x000067F4
		[Browsable(false)]
		public string SystemFontName
		{
			get
			{
				return this.systemFontName;
			}
		}

		/// <summary>Gets the name of the font originally specified.</summary>
		/// <returns>The string representing the name of the font originally specified.</returns>
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000360 RID: 864 RVA: 0x000085FC File Offset: 0x000067FC
		[Browsable(false)]
		public string OriginalFontName
		{
			get
			{
				return this.originalFontName;
			}
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Drawing.Font" /> is underlined.</summary>
		/// <returns>true if this <see cref="T:System.Drawing.Font" /> is underlined; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00008604 File Offset: 0x00006804
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Underline
		{
			get
			{
				return this._underline;
			}
		}

		/// <summary>Gets the unit of measure for this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.GraphicsUnit" /> that represents the unit of measure for this <see cref="T:System.Drawing.Font" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000860C File Offset: 0x0000680C
		[TypeConverter(typeof(FontConverter.FontUnitConverter))]
		public GraphicsUnit Unit
		{
			get
			{
				return this._unit;
			}
		}

		/// <summary>Indicates whether the specified object is a <see cref="T:System.Drawing.Font" /> and has the same <see cref="P:System.Drawing.Font.FontFamily" />, <see cref="P:System.Drawing.Font.GdiVerticalFont" />, <see cref="P:System.Drawing.Font.GdiCharSet" />, <see cref="P:System.Drawing.Font.Style" />, <see cref="P:System.Drawing.Font.Size" />, and <see cref="P:System.Drawing.Font.Unit" /> property values as this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>true if the <paramref name="obj" /> parameter is a <see cref="T:System.Drawing.Font" /> and has the same <see cref="P:System.Drawing.Font.FontFamily" />, <see cref="P:System.Drawing.Font.GdiVerticalFont" />, <see cref="P:System.Drawing.Font.GdiCharSet" />, <see cref="P:System.Drawing.Font.Style" />, <see cref="P:System.Drawing.Font.Size" />, and <see cref="P:System.Drawing.Font.Unit" /> property values as this <see cref="T:System.Drawing.Font" />; otherwise, false.</returns>
		/// <param name="obj">The object to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000363 RID: 867 RVA: 0x00008614 File Offset: 0x00006814
		public override bool Equals(object obj)
		{
			Font font = obj as Font;
			return font != null && (font.FontFamily.Equals(this.FontFamily) && font.Size == this.Size && font.Style == this.Style && font.Unit == this.Unit && font.GdiCharSet == this.GdiCharSet && font.GdiVerticalFont == this.GdiVerticalFont);
		}

		/// <summary>Gets the hash code for this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>The hash code for this <see cref="T:System.Drawing.Font" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000364 RID: 868 RVA: 0x0000868C File Offset: 0x0000688C
		public override int GetHashCode()
		{
			if (this._hashCode == 0)
			{
				this._hashCode = 17;
				this._hashCode = this._hashCode * 23 + this._name.GetHashCode();
				this._hashCode = this._hashCode * 23 + this.FontFamily.GetHashCode();
				this._hashCode = this._hashCode * 23 + this._size.GetHashCode();
				this._hashCode = this._hashCode * 23 + this._unit.GetHashCode();
				this._hashCode = this._hashCode * 23 + this._style.GetHashCode();
				this._hashCode = this._hashCode * 23 + (int)this._gdiCharSet;
				this._hashCode = this._hashCode * 23 + this._gdiVerticalFont.GetHashCode();
			}
			return this._hashCode;
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Font" /> from the specified Windows handle to a device context.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> this method creates.</returns>
		/// <param name="hdc">A handle to a device context. </param>
		/// <exception cref="T:System.ArgumentException">The font for the specified device context is not a TrueType font.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000365 RID: 869 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("The hdc parameter has no direct equivalent in libgdiplus.")]
		public static Font FromHdc(IntPtr hdc)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Font" /> from the specified GDI logical font (LOGFONT) structure.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> that this method creates.</returns>
		/// <param name="lf">An <see cref="T:System.Object" /> that represents the GDI LOGFONT structure from which to create the <see cref="T:System.Drawing.Font" />. </param>
		/// <param name="hdc">A handle to a device context that contains additional information about the <paramref name="lf" /> structure. </param>
		/// <exception cref="T:System.ArgumentException">The font is not a TrueType font.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000366 RID: 870 RVA: 0x00008778 File Offset: 0x00006978
		[MonoTODO("The returned font may not have all it's properties initialized correctly.")]
		public static Font FromLogFont(object lf, IntPtr hdc)
		{
			LOGFONT logfont = (LOGFONT)lf;
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateFontFromLogfont(hdc, ref logfont, out intPtr));
			return new Font(intPtr, "Microsoft Sans Serif", FontStyle.Regular, 10f);
		}

		/// <summary>Returns the line spacing, in pixels, of this font. </summary>
		/// <returns>The line spacing, in pixels, of this font.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000367 RID: 871 RVA: 0x000087AC File Offset: 0x000069AC
		public float GetHeight()
		{
			return this.GetHeight(Graphics.systemDpiY);
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Font" /> from the specified GDI logical font (LOGFONT) structure.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> that this method creates.</returns>
		/// <param name="lf">An <see cref="T:System.Object" /> that represents the GDI LOGFONT structure from which to create the <see cref="T:System.Drawing.Font" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000368 RID: 872 RVA: 0x000087BC File Offset: 0x000069BC
		public static Font FromLogFont(object lf)
		{
			if (GDIPlus.RunningOnUnix())
			{
				return Font.FromLogFont(lf, IntPtr.Zero);
			}
			IntPtr intPtr = IntPtr.Zero;
			Font font;
			try
			{
				intPtr = GDIPlus.GetDC(IntPtr.Zero);
				font = Font.FromLogFont(lf, intPtr);
			}
			finally
			{
				GDIPlus.ReleaseDC(IntPtr.Zero, intPtr);
			}
			return font;
		}

		/// <summary>Creates a GDI logical font (LOGFONT) structure from this <see cref="T:System.Drawing.Font" />.</summary>
		/// <param name="logFont">An <see cref="T:System.Object" /> that represents the LOGFONT structure that this method creates. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000369 RID: 873 RVA: 0x00008818 File Offset: 0x00006A18
		public void ToLogFont(object logFont)
		{
			if (GDIPlus.RunningOnUnix())
			{
				using (Bitmap bitmap = new Bitmap(1, 1, PixelFormat.Format32bppArgb))
				{
					using (Graphics graphics = Graphics.FromImage(bitmap))
					{
						this.ToLogFont(logFont, graphics);
						return;
					}
				}
			}
			IntPtr dc = GDIPlus.GetDC(IntPtr.Zero);
			try
			{
				using (Graphics graphics2 = Graphics.FromHdc(dc))
				{
					this.ToLogFont(logFont, graphics2);
				}
			}
			finally
			{
				GDIPlus.ReleaseDC(IntPtr.Zero, dc);
			}
		}

		/// <summary>Creates a GDI logical font (LOGFONT) structure from this <see cref="T:System.Drawing.Font" />.</summary>
		/// <param name="logFont">An <see cref="T:System.Object" /> that represents the LOGFONT structure that this method creates. </param>
		/// <param name="graphics">A <see cref="T:System.Drawing.Graphics" /> that provides additional information for the LOGFONT structure. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="graphics" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600036A RID: 874 RVA: 0x000088C8 File Offset: 0x00006AC8
		public void ToLogFont(object logFont, Graphics graphics)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			if (logFont == null)
			{
				throw new AccessViolationException("logFont");
			}
			if (!logFont.GetType().GetTypeInfo().IsLayoutSequential)
			{
				throw new ArgumentException("logFont", Locale.GetText("Layout must be sequential."));
			}
			Type typeFromHandle = typeof(LOGFONT);
			int num = Marshal.SizeOf(logFont);
			if (num >= Marshal.SizeOf(typeFromHandle))
			{
				IntPtr intPtr = Marshal.AllocHGlobal(num);
				Status status;
				try
				{
					Marshal.StructureToPtr(logFont, intPtr, false);
					status = GDIPlus.GdipGetLogFont(this.NativeObject, graphics.NativeObject, logFont);
					if (status != Status.Ok)
					{
						Marshal.PtrToStructure(intPtr, logFont);
					}
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (Font.CharSetOffset == -1)
				{
					Font.CharSetOffset = (int)Marshal.OffsetOf(typeFromHandle, "lfCharSet");
				}
				GCHandle gchandle = GCHandle.Alloc(logFont, GCHandleType.Pinned);
				try
				{
					IntPtr intPtr2 = gchandle.AddrOfPinnedObject();
					if (Marshal.ReadByte(intPtr2, Font.CharSetOffset) == 0)
					{
						Marshal.WriteByte(intPtr2, Font.CharSetOffset, 1);
					}
				}
				finally
				{
					gchandle.Free();
				}
				GDIPlus.CheckStatus(status);
			}
		}

		/// <summary>Returns the line spacing, in the current unit of a specified <see cref="T:System.Drawing.Graphics" />, of this font. </summary>
		/// <returns>The line spacing, in pixels, of this font.</returns>
		/// <param name="graphics">A <see cref="T:System.Drawing.Graphics" /> that holds the vertical resolution, in dots per inch, of the display device as well as settings for page unit and page scale. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="graphics" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600036B RID: 875 RVA: 0x000089E4 File Offset: 0x00006BE4
		public float GetHeight(Graphics graphics)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			float num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetFontHeight(this.fontObject, graphics.NativeObject, out num));
			return num;
		}

		/// <summary>Returns the height, in pixels, of this <see cref="T:System.Drawing.Font" /> when drawn to a device with the specified vertical resolution.</summary>
		/// <returns>The height, in pixels, of this <see cref="T:System.Drawing.Font" />.</returns>
		/// <param name="dpi">The vertical resolution, in dots per inch, used to calculate the height of the font. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600036C RID: 876 RVA: 0x00008A18 File Offset: 0x00006C18
		public float GetHeight(float dpi)
		{
			float num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetFontHeightGivenDPI(this.fontObject, dpi, out num));
			return num;
		}

		/// <summary>Returns a human-readable string representation of this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>A string that represents this <see cref="T:System.Drawing.Font" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600036D RID: 877 RVA: 0x00008A3C File Offset: 0x00006C3C
		public override string ToString()
		{
			return string.Format("[Font: Name={0}, Size={1}, Units={2}, GdiCharSet={3}, GdiVerticalFont={4}]", new object[]
			{
				this._name,
				this.Size,
				(int)this._unit,
				this._gdiCharSet,
				this._gdiVerticalFont
			});
		}

		// Token: 0x040003A6 RID: 934
		private IntPtr fontObject = IntPtr.Zero;

		// Token: 0x040003A7 RID: 935
		private string systemFontName;

		// Token: 0x040003A8 RID: 936
		private string originalFontName;

		// Token: 0x040003A9 RID: 937
		private float _size;

		// Token: 0x040003AA RID: 938
		private object olf;

		// Token: 0x040003AB RID: 939
		private const byte DefaultCharSet = 1;

		// Token: 0x040003AC RID: 940
		private static int CharSetOffset = -1;

		// Token: 0x040003AD RID: 941
		private bool _bold;

		// Token: 0x040003AE RID: 942
		private FontFamily _fontFamily;

		// Token: 0x040003AF RID: 943
		private byte _gdiCharSet;

		// Token: 0x040003B0 RID: 944
		private bool _gdiVerticalFont;

		// Token: 0x040003B1 RID: 945
		private bool _italic;

		// Token: 0x040003B2 RID: 946
		private string _name;

		// Token: 0x040003B3 RID: 947
		private float _sizeInPoints;

		// Token: 0x040003B4 RID: 948
		private bool _strikeout;

		// Token: 0x040003B5 RID: 949
		private FontStyle _style;

		// Token: 0x040003B6 RID: 950
		private bool _underline;

		// Token: 0x040003B7 RID: 951
		private GraphicsUnit _unit;

		// Token: 0x040003B8 RID: 952
		private int _hashCode;
	}
}
