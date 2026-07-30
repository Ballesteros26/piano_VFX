using System;
using System.ComponentModel;
using System.Drawing.Text;

namespace System.Drawing
{
	/// <summary>Encapsulates text layout information (such as alignment, orientation and tab stops) display manipulations (such as ellipsis insertion and national digit substitution) and OpenType features. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000088 RID: 136
	public sealed class StringFormat : MarshalByRefObject, IDisposable, ICloneable
	{
		/// <summary>Initializes a new <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		// Token: 0x06000704 RID: 1796 RVA: 0x00014388 File Offset: 0x00012588
		public StringFormat()
			: this((StringFormatFlags)0, 0)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.StringFormat" /> object with the specified <see cref="T:System.Drawing.StringFormatFlags" /> enumeration and language.</summary>
		/// <param name="options">The <see cref="T:System.Drawing.StringFormatFlags" /> enumeration for the new <see cref="T:System.Drawing.StringFormat" /> object. </param>
		/// <param name="language">A value that indicates the language of the text. </param>
		// Token: 0x06000705 RID: 1797 RVA: 0x00014392 File Offset: 0x00012592
		public StringFormat(StringFormatFlags options, int language)
		{
			this.nativeStrFmt = IntPtr.Zero;
			base..ctor();
			GDIPlus.CheckStatus(GDIPlus.GdipCreateStringFormat(options, language, out this.nativeStrFmt));
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x000143B7 File Offset: 0x000125B7
		internal StringFormat(IntPtr native)
		{
			this.nativeStrFmt = IntPtr.Zero;
			base..ctor();
			this.nativeStrFmt = native;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x000143D4 File Offset: 0x000125D4
		~StringFormat()
		{
			this.Dispose(false);
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000708 RID: 1800 RVA: 0x00014404 File Offset: 0x00012604
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00014413 File Offset: 0x00012613
		private void Dispose(bool disposing)
		{
			if (this.nativeStrFmt != IntPtr.Zero)
			{
				Status status = GDIPlus.GdipDeleteStringFormat(this.nativeStrFmt);
				this.nativeStrFmt = IntPtr.Zero;
				GDIPlus.CheckStatus(status);
			}
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.StringFormat" /> object from the specified existing <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <param name="format">The <see cref="T:System.Drawing.StringFormat" /> object from which to initialize the new <see cref="T:System.Drawing.StringFormat" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null.</exception>
		// Token: 0x0600070A RID: 1802 RVA: 0x00014442 File Offset: 0x00012642
		public StringFormat(StringFormat format)
		{
			this.nativeStrFmt = IntPtr.Zero;
			base..ctor();
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCloneStringFormat(format.NativeObject, out this.nativeStrFmt));
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.StringFormat" /> object with the specified <see cref="T:System.Drawing.StringFormatFlags" /> enumeration.</summary>
		/// <param name="options">The <see cref="T:System.Drawing.StringFormatFlags" /> enumeration for the new <see cref="T:System.Drawing.StringFormat" /> object. </param>
		// Token: 0x0600070B RID: 1803 RVA: 0x00014479 File Offset: 0x00012679
		public StringFormat(StringFormatFlags options)
		{
			this.nativeStrFmt = IntPtr.Zero;
			base..ctor();
			GDIPlus.CheckStatus(GDIPlus.GdipCreateStringFormat(options, 0, out this.nativeStrFmt));
		}

		/// <summary>Gets or sets horizontal alignment of the string..</summary>
		/// <returns>A <see cref="T:System.Drawing.StringAlignment" /> enumeration that specifies the horizontal  alignment of the string.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x000144A0 File Offset: 0x000126A0
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x000144C0 File Offset: 0x000126C0
		public StringAlignment Alignment
		{
			get
			{
				StringAlignment stringAlignment;
				GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatAlign(this.nativeStrFmt, out stringAlignment));
				return stringAlignment;
			}
			set
			{
				if (value < StringAlignment.Near || value > StringAlignment.Far)
				{
					throw new InvalidEnumArgumentException("Alignment");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatAlign(this.nativeStrFmt, value));
			}
		}

		/// <summary>Gets or sets the vertical alignment of the string.</summary>
		/// <returns>A <see cref="T:System.Drawing.StringAlignment" /> enumeration that represents the vertical line alignment.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x000144E8 File Offset: 0x000126E8
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x00014508 File Offset: 0x00012708
		public StringAlignment LineAlignment
		{
			get
			{
				StringAlignment stringAlignment;
				GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatLineAlign(this.nativeStrFmt, out stringAlignment));
				return stringAlignment;
			}
			set
			{
				if (value < StringAlignment.Near || value > StringAlignment.Far)
				{
					throw new InvalidEnumArgumentException("Alignment");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatLineAlign(this.nativeStrFmt, value));
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.StringFormatFlags" /> enumeration that contains formatting information.</summary>
		/// <returns>A <see cref="T:System.Drawing.StringFormatFlags" /> enumeration that contains formatting information.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x00014530 File Offset: 0x00012730
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x00014550 File Offset: 0x00012750
		public StringFormatFlags FormatFlags
		{
			get
			{
				StringFormatFlags stringFormatFlags;
				GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatFlags(this.nativeStrFmt, out stringFormatFlags));
				return stringFormatFlags;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatFlags(this.nativeStrFmt, value));
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Drawing.Text.HotkeyPrefix" /> object for this <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <returns>The <see cref="T:System.Drawing.Text.HotkeyPrefix" /> object for this <see cref="T:System.Drawing.StringFormat" /> object, the default is <see cref="F:System.Drawing.Text.HotkeyPrefix.None" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00014564 File Offset: 0x00012764
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x00014584 File Offset: 0x00012784
		public HotkeyPrefix HotkeyPrefix
		{
			get
			{
				HotkeyPrefix hotkeyPrefix;
				GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatHotkeyPrefix(this.nativeStrFmt, out hotkeyPrefix));
				return hotkeyPrefix;
			}
			set
			{
				if (value < HotkeyPrefix.None || value > HotkeyPrefix.Hide)
				{
					throw new InvalidEnumArgumentException("HotkeyPrefix");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatHotkeyPrefix(this.nativeStrFmt, value));
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Drawing.StringTrimming" /> enumeration for this <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.StringTrimming" /> enumeration that indicates how text drawn with this <see cref="T:System.Drawing.StringFormat" /> object is trimmed when it exceeds the edges of the layout rectangle.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x000145AC File Offset: 0x000127AC
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x000145CC File Offset: 0x000127CC
		public StringTrimming Trimming
		{
			get
			{
				StringTrimming stringTrimming;
				GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatTrimming(this.nativeStrFmt, out stringTrimming));
				return stringTrimming;
			}
			set
			{
				if (value < StringTrimming.None || value > StringTrimming.EllipsisPath)
				{
					throw new InvalidEnumArgumentException("Trimming");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatTrimming(this.nativeStrFmt, value));
			}
		}

		/// <summary>Gets a generic default <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <returns>The generic default <see cref="T:System.Drawing.StringFormat" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x000145F4 File Offset: 0x000127F4
		public static StringFormat GenericDefault
		{
			get
			{
				IntPtr intPtr;
				GDIPlus.CheckStatus(GDIPlus.GdipStringFormatGetGenericDefault(out intPtr));
				return new StringFormat(intPtr);
			}
		}

		/// <summary>Gets the language that is used when local digits are substituted for western digits.</summary>
		/// <returns>A National Language Support (NLS) language identifier that identifies the language that will be used when local digits are substituted for western digits. You can pass the <see cref="P:System.Globalization.CultureInfo.LCID" /> property of a <see cref="T:System.Globalization.CultureInfo" /> object as the NLS language identifier. For example, suppose you create a <see cref="T:System.Globalization.CultureInfo" /> object by passing the string "ar-EG" to a <see cref="T:System.Globalization.CultureInfo" /> constructor. If you pass the <see cref="P:System.Globalization.CultureInfo.LCID" /> property of that <see cref="T:System.Globalization.CultureInfo" /> object along with.<see cref="F:System.Drawing.StringDigitSubstitute.Traditional" /> to the <see cref="M:System.Drawing.StringFormat.SetDigitSubstitution(System.Int32,System.Drawing.StringDigitSubstitute)" /> method, then Arabic-Indic digits will be substituted for western digits at display time.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00014613 File Offset: 0x00012813
		public int DigitSubstitutionLanguage
		{
			get
			{
				return this.language;
			}
		}

		/// <summary>Gets a generic typographic <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <returns>A generic typographic <see cref="T:System.Drawing.StringFormat" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0001461C File Offset: 0x0001281C
		public static StringFormat GenericTypographic
		{
			get
			{
				IntPtr intPtr;
				GDIPlus.CheckStatus(GDIPlus.GdipStringFormatGetGenericTypographic(out intPtr));
				return new StringFormat(intPtr);
			}
		}

		/// <summary>Gets the method to be used for digit substitution.</summary>
		/// <returns>A <see cref="T:System.Drawing.StringDigitSubstitute" /> enumeration value that specifies how to substitute characters in a string that cannot be displayed because they are not supported by the current font.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001463C File Offset: 0x0001283C
		public StringDigitSubstitute DigitSubstitutionMethod
		{
			get
			{
				StringDigitSubstitute stringDigitSubstitute;
				GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatDigitSubstitution(this.nativeStrFmt, this.language, out stringDigitSubstitute));
				return stringDigitSubstitute;
			}
		}

		/// <summary>Specifies an array of <see cref="T:System.Drawing.CharacterRange" /> structures that represent the ranges of characters measured by a call to the <see cref="M:System.Drawing.Graphics.MeasureCharacterRanges(System.String,System.Drawing.Font,System.Drawing.RectangleF,System.Drawing.StringFormat)" /> method.</summary>
		/// <param name="ranges">An array of <see cref="T:System.Drawing.CharacterRange" /> structures that specifies the ranges of characters measured by a call to the <see cref="M:System.Drawing.Graphics.MeasureCharacterRanges(System.String,System.Drawing.Font,System.Drawing.RectangleF,System.Drawing.StringFormat)" /> method. </param>
		/// <exception cref="T:System.OverflowException">More than 32 character ranges are set.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600071A RID: 1818 RVA: 0x00014662 File Offset: 0x00012862
		public void SetMeasurableCharacterRanges(CharacterRange[] ranges)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatMeasurableCharacterRanges(this.nativeStrFmt, ranges.Length, ranges));
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00014678 File Offset: 0x00012878
		internal int GetMeasurableCharacterRangeCount()
		{
			int num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatMeasurableCharacterRangeCount(this.nativeStrFmt, out num));
			return num;
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <returns>The <see cref="T:System.Drawing.StringFormat" /> object this method creates.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600071C RID: 1820 RVA: 0x00014698 File Offset: 0x00012898
		public object Clone()
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneStringFormat(this.nativeStrFmt, out intPtr));
			return new StringFormat(intPtr);
		}

		/// <summary>Converts this <see cref="T:System.Drawing.StringFormat" /> object to a human-readable string.</summary>
		/// <returns>A string representation of this <see cref="T:System.Drawing.StringFormat" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600071D RID: 1821 RVA: 0x000146C0 File Offset: 0x000128C0
		public override string ToString()
		{
			return "[StringFormat, FormatFlags=" + this.FormatFlags.ToString() + "]";
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x000146F0 File Offset: 0x000128F0
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x000146F8 File Offset: 0x000128F8
		internal IntPtr NativeObject
		{
			get
			{
				return this.nativeStrFmt;
			}
			set
			{
				this.nativeStrFmt = value;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x000146F0 File Offset: 0x000128F0
		internal IntPtr nativeFormat
		{
			get
			{
				return this.nativeStrFmt;
			}
		}

		/// <summary>Sets tab stops for this <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <param name="firstTabOffset">The number of spaces between the beginning of a line of text and the first tab stop. </param>
		/// <param name="tabStops">An array of distances between tab stops in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000721 RID: 1825 RVA: 0x00014701 File Offset: 0x00012901
		public void SetTabStops(float firstTabOffset, float[] tabStops)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatTabStops(this.nativeStrFmt, firstTabOffset, tabStops.Length, tabStops));
		}

		/// <summary>Specifies the language and method to be used when local digits are substituted for western digits.</summary>
		/// <param name="language">A National Language Support (NLS) language identifier that identifies the language that will be used when local digits are substituted for western digits. You can pass the <see cref="P:System.Globalization.CultureInfo.LCID" /> property of a <see cref="T:System.Globalization.CultureInfo" /> object as the NLS language identifier. For example, suppose you create a <see cref="T:System.Globalization.CultureInfo" /> object by passing the string "ar-EG" to a <see cref="T:System.Globalization.CultureInfo" /> constructor. If you pass the <see cref="P:System.Globalization.CultureInfo.LCID" /> property of that <see cref="T:System.Globalization.CultureInfo" /> object along with <see cref="F:System.Drawing.StringDigitSubstitute.Traditional" /> to the <see cref="M:System.Drawing.StringFormat.SetDigitSubstitution(System.Int32,System.Drawing.StringDigitSubstitute)" /> method, then Arabic-Indic digits will be substituted for western digits at display time. </param>
		/// <param name="substitute">An element of the <see cref="T:System.Drawing.StringDigitSubstitute" /> enumeration that specifies how digits are displayed. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000722 RID: 1826 RVA: 0x00014718 File Offset: 0x00012918
		public void SetDigitSubstitution(int language, StringDigitSubstitute substitute)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatDigitSubstitution(this.nativeStrFmt, this.language, substitute));
		}

		/// <summary>Gets the tab stops for this <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <returns>An array of distances (in number of spaces) between tab stops.</returns>
		/// <param name="firstTabOffset">The number of spaces between the beginning of a text line and the first tab stop. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000723 RID: 1827 RVA: 0x00014734 File Offset: 0x00012934
		public float[] GetTabStops(out float firstTabOffset)
		{
			int num = 0;
			firstTabOffset = 0f;
			GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatTabStopCount(this.nativeStrFmt, out num));
			float[] array = new float[num];
			if (num != 0)
			{
				GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatTabStops(this.nativeStrFmt, num, out firstTabOffset, array));
			}
			return array;
		}

		// Token: 0x04000554 RID: 1364
		private IntPtr nativeStrFmt;

		// Token: 0x04000555 RID: 1365
		private int language;
	}
}
