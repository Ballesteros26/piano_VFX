using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Form.InputLanguageChanging" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E4 RID: 484
	public class InputLanguageChangingEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.InputLanguageChangingEventArgs" /> class with the specified locale, character set, and acceptance.</summary>
		/// <param name="culture">The locale of the requested input language. </param>
		/// <param name="sysCharSet">true if the system default font supports the character set required for the requested input language; otherwise, false. </param>
		// Token: 0x06001E96 RID: 7830 RVA: 0x00072F2C File Offset: 0x0007112C
		public InputLanguageChangingEventArgs(CultureInfo culture, bool sysCharSet)
		{
			this.culture = culture;
			this.system_charset = sysCharSet;
			this.input_language = InputLanguage.FromCulture(culture);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.InputLanguageChangingEventArgs" /> class with the specified input language, character set, and acceptance of a language change.</summary>
		/// <param name="inputLanguage">The requested input language. </param>
		/// <param name="sysCharSet">true if the system default font supports the character set required for the requested input language; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inputLanguage" /> is null. </exception>
		// Token: 0x06001E97 RID: 7831 RVA: 0x00072F5C File Offset: 0x0007115C
		public InputLanguageChangingEventArgs(InputLanguage inputLanguage, bool sysCharSet)
		{
			this.culture = inputLanguage.Culture;
			this.system_charset = sysCharSet;
			this.input_language = inputLanguage;
		}

		/// <summary>Gets a value indicating whether the system default font supports the character set required for the requested input language.</summary>
		/// <returns>true if the system default font supports the character set required for the requested input language; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001E98 RID: 7832 RVA: 0x00072F8C File Offset: 0x0007118C
		public bool SysCharSet
		{
			get
			{
				return this.system_charset;
			}
		}

		/// <summary>Gets the locale of the requested input language.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> that specifies the locale of the requested input language.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001E99 RID: 7833 RVA: 0x00072F94 File Offset: 0x00071194
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
		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001E9A RID: 7834 RVA: 0x00072F9C File Offset: 0x0007119C
		public InputLanguage InputLanguage
		{
			get
			{
				return this.input_language;
			}
		}

		// Token: 0x04001007 RID: 4103
		private CultureInfo culture;

		// Token: 0x04001008 RID: 4104
		private bool system_charset;

		// Token: 0x04001009 RID: 4105
		private InputLanguage input_language;
	}
}
