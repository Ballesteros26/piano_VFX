using System;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Provides methods and fields to manage the input language. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E2 RID: 482
	public sealed class InputLanguage
	{
		// Token: 0x06001E85 RID: 7813 RVA: 0x00072C94 File Offset: 0x00070E94
		[MonoInternalNote("Pull Microsofts InputLanguages and enter them here")]
		internal InputLanguage()
		{
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x00072C9C File Offset: 0x00070E9C
		internal InputLanguage(IntPtr handle, CultureInfo culture, string layout_name)
			: this()
		{
			this.handle = handle;
			this.culture = culture;
			this.layout_name = layout_name;
		}

		/// <summary>Gets or sets the input language for the current thread.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.InputLanguage" /> that represents the input language for the current thread.</returns>
		/// <exception cref="T:System.ArgumentException">The input language is not recognized by the system.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x00072CBC File Offset: 0x00070EBC
		// (set) Token: 0x06001E88 RID: 7816 RVA: 0x00072CDC File Offset: 0x00070EDC
		public static InputLanguage CurrentInputLanguage
		{
			get
			{
				if (InputLanguage.current_input == null)
				{
					InputLanguage.current_input = InputLanguage.FromCulture(CultureInfo.CurrentUICulture);
				}
				return InputLanguage.current_input;
			}
			set
			{
				if (InputLanguage.InstalledInputLanguages.Contains(value))
				{
					InputLanguage.current_input = value;
				}
			}
		}

		/// <summary>Gets the default input language for the system.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.InputLanguage" /> representing the default input language for the system.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x00072CF4 File Offset: 0x00070EF4
		public static InputLanguage DefaultInputLanguage
		{
			get
			{
				if (InputLanguage.default_input == null)
				{
					InputLanguage.default_input = InputLanguage.FromCulture(CultureInfo.CurrentUICulture);
				}
				return InputLanguage.default_input;
			}
		}

		/// <summary>Gets a list of all installed input languages.</summary>
		/// <returns>An array of <see cref="T:System.Windows.Forms.InputLanguage" /> objects that represent the input languages installed on the computer.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001E8A RID: 7818 RVA: 0x00072D14 File Offset: 0x00070F14
		public static InputLanguageCollection InstalledInputLanguages
		{
			get
			{
				if (InputLanguage.all == null)
				{
					InputLanguage.all = new InputLanguageCollection(new InputLanguage[]
					{
						new InputLanguage(IntPtr.Zero, new CultureInfo(string.Empty), "US")
					});
				}
				return InputLanguage.all;
			}
		}

		/// <summary>Gets the culture of the current input language.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> that represents the culture of the current input language.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x00072D54 File Offset: 0x00070F54
		public CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
		}

		/// <summary>Gets the handle for the input language.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that represents the handle of this input language.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x00072D5C File Offset: 0x00070F5C
		public IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		/// <summary>Gets the name of the current keyboard layout as it appears in the regional settings of the operating system on the computer.</summary>
		/// <returns>The name of the layout.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001E8D RID: 7821 RVA: 0x00072D64 File Offset: 0x00070F64
		public string LayoutName
		{
			get
			{
				return this.layout_name;
			}
		}

		/// <summary>Returns the input language associated with the specified culture.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.InputLanguage" /> that represents the previously selected input language.</returns>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to convert from. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E8E RID: 7822 RVA: 0x00072D6C File Offset: 0x00070F6C
		public static InputLanguage FromCulture(CultureInfo culture)
		{
			foreach (object obj in InputLanguage.InstalledInputLanguages)
			{
				InputLanguage inputLanguage = (InputLanguage)obj;
				if (culture.EnglishName == inputLanguage.culture.EnglishName)
				{
					return new InputLanguage(inputLanguage.handle, inputLanguage.culture, inputLanguage.layout_name);
				}
			}
			return new InputLanguage(InputLanguage.InstalledInputLanguages[0].handle, InputLanguage.InstalledInputLanguages[0].culture, InputLanguage.InstalledInputLanguages[0].layout_name);
		}

		/// <summary>Specifies whether two input languages are equal.</summary>
		/// <returns>true if the two languages are equal; otherwise, false.</returns>
		/// <param name="value">The language to test for equality. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E8F RID: 7823 RVA: 0x00072E44 File Offset: 0x00071044
		public override bool Equals(object value)
		{
			return value is InputLanguage && ((InputLanguage)value).culture == this.culture && ((InputLanguage)value).handle == this.handle && ((InputLanguage)value).layout_name == this.layout_name;
		}

		/// <summary>Returns the hash code for this input language.</summary>
		/// <returns>The hash code for this input language.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E90 RID: 7824 RVA: 0x00072EAC File Offset: 0x000710AC
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000FFE RID: 4094
		private static InputLanguageCollection all;

		// Token: 0x04000FFF RID: 4095
		private IntPtr handle;

		// Token: 0x04001000 RID: 4096
		private CultureInfo culture;

		// Token: 0x04001001 RID: 4097
		private string layout_name;

		// Token: 0x04001002 RID: 4098
		private static InputLanguage current_input;

		// Token: 0x04001003 RID: 4099
		private static InputLanguage default_input;
	}
}
