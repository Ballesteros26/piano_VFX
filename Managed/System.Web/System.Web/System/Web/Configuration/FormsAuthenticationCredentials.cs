using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures user credentials for ASP.NET applications that use form-based authentication.</summary>
	// Token: 0x0200059F RID: 1439
	public sealed class FormsAuthenticationCredentials : ConfigurationElement
	{
		// Token: 0x06003D11 RID: 15633 RVA: 0x000A216C File Offset: 0x000A036C
		static FormsAuthenticationCredentials()
		{
			FormsAuthenticationCredentials.properties.Add(FormsAuthenticationCredentials.passwordFormatProp);
			FormsAuthenticationCredentials.properties.Add(FormsAuthenticationCredentials.usersProp);
		}

		/// <summary>Gets or sets the password format.</summary>
		/// <returns>One of the <see cref="T:System.Web.Configuration.FormsAuthPasswordFormat" /> values.</returns>
		// Token: 0x170012D5 RID: 4821
		// (get) Token: 0x06003D13 RID: 15635 RVA: 0x000A21F6 File Offset: 0x000A03F6
		// (set) Token: 0x06003D14 RID: 15636 RVA: 0x000A2208 File Offset: 0x000A0408
		[ConfigurationProperty("passwordFormat", DefaultValue = "SHA1")]
		public FormsAuthPasswordFormat PasswordFormat
		{
			get
			{
				return (FormsAuthPasswordFormat)base[FormsAuthenticationCredentials.passwordFormatProp];
			}
			set
			{
				base[FormsAuthenticationCredentials.passwordFormatProp] = value;
			}
		}

		/// <summary>Gets the users' names and password credentials.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.FormsAuthenticationUserCollection" /> that contains the users' names and password credentials.</returns>
		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x06003D15 RID: 15637 RVA: 0x000A221B File Offset: 0x000A041B
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public FormsAuthenticationUserCollection Users
		{
			get
			{
				return (FormsAuthenticationUserCollection)base[FormsAuthenticationCredentials.usersProp];
			}
		}

		// Token: 0x170012D7 RID: 4823
		// (get) Token: 0x06003D16 RID: 15638 RVA: 0x000A222D File Offset: 0x000A042D
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return FormsAuthenticationCredentials.properties;
			}
		}

		// Token: 0x040020F3 RID: 8435
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040020F4 RID: 8436
		private static ConfigurationProperty passwordFormatProp = new ConfigurationProperty("passwordFormat", typeof(FormsAuthPasswordFormat), FormsAuthPasswordFormat.SHA1, new GenericEnumConverter(typeof(FormsAuthPasswordFormat)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020F5 RID: 8437
		private static ConfigurationProperty usersProp = new ConfigurationProperty("", typeof(FormsAuthenticationUserCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
