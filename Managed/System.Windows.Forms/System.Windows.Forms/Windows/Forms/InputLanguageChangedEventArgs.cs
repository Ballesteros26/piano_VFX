using System;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Form.InputLanguageChanged" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E3 RID: 483
	public class InputLanguageChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.InputLanguageChangedEventArgs" /> class with the specified locale and character set.</summary>
		/// <param name="culture">The locale of the input language. </param>
		/// <param name="charSet">The character set associated with the new input language. </param>
		// Token: 0x06001E91 RID: 7825 RVA: 0x00072EB4 File Offset: 0x000710B4
		public InputLanguageChangedEventArgs(CultureInfo culture, byte charSet)
		{
			this.culture = culture;
			this.charset = charSet;
			this.input_language = InputLanguage.FromCulture(culture);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.InputLanguageChangedEventArgs" /> class with the specified input language and character set.</summary>
		/// <param name="inputLanguage">The input language. </param>
		/// <param name="charSet">The character set associated with the new input language. </param>
		// Token: 0x06001E92 RID: 7826 RVA: 0x00072EE4 File Offset: 0x000710E4
		public InputLanguageChangedEventArgs(InputLanguage inputLanguage, byte charSet)
		{
			this.culture = inputLanguage.Culture;
			this.charset = charSet;
			this.input_language = inputLanguage;
		}

		/// <summary>Gets the character set associated with the new input language.</summary>
		/// <returns>An 8-bit unsigned integer that corresponds to the character set, as shown in the following table.Character Set Value ANSI_CHARSET 0 DEFAULT_CHARSET 1 SYMBOL_CHARSET 2 MAC_CHARSET 77 SHIFTJI_CHARSET 128 HANGEUL_CHARSET 129 HANGUL_CHARSET 129 JOHAB_CHARSET 130 GB2312_CHARSET 134 CHINESEBIG5_CHARSET 136 GREEK_CHARSET 161 TURKISH_CHARSET 162 VIETNAMESE_CHARSET 163 HEBREW_CHARSET 177 ARABIC_CHARSET 178 BALTIC_CHARSET 186 RUSSIAN_CHARSET 204 THAI_CHARSET 222 EASTEUROPE_CHARSET 238 OEM_CHARSET 255 </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x00072F14 File Offset: 0x00071114
		public byte CharSet
		{
			get
			{
				return this.charset;
			}
		}

		/// <summary>Gets the locale of the input language.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> that specifies the locale of the input language.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001E94 RID: 7828 RVA: 0x00072F1C File Offset: 0x0007111C
		public CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
		}

		/// <summary>Gets a value indicating the input language.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.InputLanguage" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001E95 RID: 7829 RVA: 0x00072F24 File Offset: 0x00071124
		public InputLanguage InputLanguage
		{
			get
			{
				return this.input_language;
			}
		}

		// Token: 0x04001004 RID: 4100
		private CultureInfo culture;

		// Token: 0x04001005 RID: 4101
		private byte charset;

		// Token: 0x04001006 RID: 4102
		private InputLanguage input_language;
	}
}
