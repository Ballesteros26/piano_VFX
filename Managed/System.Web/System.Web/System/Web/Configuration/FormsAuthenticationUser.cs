using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the user's credentials for Web applications that use forms-based authentication. </summary>
	// Token: 0x020005A0 RID: 1440
	public sealed class FormsAuthenticationUser : ConfigurationElement
	{
		// Token: 0x06003D17 RID: 15639 RVA: 0x000A2234 File Offset: 0x000A0434
		static FormsAuthenticationUser()
		{
			FormsAuthenticationUser.properties.Add(FormsAuthenticationUser.nameProp);
			FormsAuthenticationUser.properties.Add(FormsAuthenticationUser.passwordProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> class using the passed parameters.</summary>
		/// <param name="name">User's name.</param>
		/// <param name="password">User's password.</param>
		// Token: 0x06003D18 RID: 15640 RVA: 0x000A22B1 File Offset: 0x000A04B1
		public FormsAuthenticationUser(string name, string password)
		{
			this.Name = name;
			this.Password = password;
		}

		/// <summary>Gets or sets the logon user name.</summary>
		/// <returns>The logon user name required by the application.</returns>
		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x06003D19 RID: 15641 RVA: 0x000A22C7 File Offset: 0x000A04C7
		// (set) Token: 0x06003D1A RID: 15642 RVA: 0x000A22D9 File Offset: 0x000A04D9
		[StringValidator]
		[TypeConverter(typeof(LowerCaseStringConverter))]
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Name
		{
			get
			{
				return (string)base[FormsAuthenticationUser.nameProp];
			}
			set
			{
				base[FormsAuthenticationUser.nameProp] = value;
			}
		}

		/// <summary>Gets or sets the user's password.</summary>
		/// <returns>The user's password required by the application.</returns>
		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x06003D1B RID: 15643 RVA: 0x000A22E7 File Offset: 0x000A04E7
		// (set) Token: 0x06003D1C RID: 15644 RVA: 0x000A22F9 File Offset: 0x000A04F9
		[StringValidator]
		[ConfigurationProperty("password", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Password
		{
			get
			{
				return (string)base[FormsAuthenticationUser.passwordProp];
			}
			set
			{
				base[FormsAuthenticationUser.passwordProp] = value;
			}
		}

		// Token: 0x170012DA RID: 4826
		// (get) Token: 0x06003D1D RID: 15645 RVA: 0x000A2307 File Offset: 0x000A0507
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return FormsAuthenticationUser.properties;
			}
		}

		// Token: 0x040020F6 RID: 8438
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), "", new LowerCaseStringConverter(), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040020F7 RID: 8439
		private static ConfigurationProperty passwordProp = new ConfigurationProperty("password", typeof(string), "", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040020F8 RID: 8440
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
