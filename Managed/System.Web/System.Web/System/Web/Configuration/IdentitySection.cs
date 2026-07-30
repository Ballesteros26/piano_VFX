using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the identity of a Web application. This class cannot be inherited.</summary>
	// Token: 0x020005B4 RID: 1460
	public sealed class IdentitySection : ConfigurationSection
	{
		// Token: 0x06003EA1 RID: 16033 RVA: 0x000A5D5C File Offset: 0x000A3F5C
		static IdentitySection()
		{
			IdentitySection.properties.Add(IdentitySection.impersonateProp);
			IdentitySection.properties.Add(IdentitySection.passwordProp);
			IdentitySection.properties.Add(IdentitySection.userNameProp);
		}

		// Token: 0x06003EA2 RID: 16034 RVA: 0x00002058 File Offset: 0x00000258
		[global::System.MonoTODO("why override this?")]
		protected internal override object GetRuntimeObject()
		{
			return this;
		}

		// Token: 0x06003EA3 RID: 16035 RVA: 0x0000393A File Offset: 0x00001B3A
		protected internal override void Reset(ConfigurationElement parentElement)
		{
		}

		// Token: 0x06003EA4 RID: 16036 RVA: 0x0000393A File Offset: 0x00001B3A
		protected internal override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
		}

		/// <summary>Gets or sets a value indicating whether client impersonation is used on each request.</summary>
		/// <returns>true if client impersonation is used; otherwise, false. The default value is false.</returns>
		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x06003EA5 RID: 16037 RVA: 0x000A5DFB File Offset: 0x000A3FFB
		// (set) Token: 0x06003EA6 RID: 16038 RVA: 0x000A5E0D File Offset: 0x000A400D
		[ConfigurationProperty("impersonate", DefaultValue = "False")]
		public bool Impersonate
		{
			get
			{
				return (bool)base[IdentitySection.impersonateProp];
			}
			set
			{
				base[IdentitySection.impersonateProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the password to use for impersonation. </summary>
		/// <returns>The password to use for impersonation. </returns>
		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x06003EA7 RID: 16039 RVA: 0x000A5E20 File Offset: 0x000A4020
		// (set) Token: 0x06003EA8 RID: 16040 RVA: 0x000A5E32 File Offset: 0x000A4032
		[ConfigurationProperty("password", DefaultValue = "")]
		public string Password
		{
			get
			{
				return (string)base[IdentitySection.passwordProp];
			}
			set
			{
				base[IdentitySection.passwordProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the user name to use for impersonation.</summary>
		/// <returns>The user name to use for impersonation. </returns>
		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x06003EA9 RID: 16041 RVA: 0x000A5E40 File Offset: 0x000A4040
		// (set) Token: 0x06003EAA RID: 16042 RVA: 0x000A5E52 File Offset: 0x000A4052
		[ConfigurationProperty("userName", DefaultValue = "")]
		public string UserName
		{
			get
			{
				return (string)base[IdentitySection.userNameProp];
			}
			set
			{
				base[IdentitySection.userNameProp] = value;
			}
		}

		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x06003EAB RID: 16043 RVA: 0x000A5E60 File Offset: 0x000A4060
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return IdentitySection.properties;
			}
		}

		// Token: 0x04002238 RID: 8760
		private static ConfigurationProperty impersonateProp = new ConfigurationProperty("impersonate", typeof(bool), false);

		// Token: 0x04002239 RID: 8761
		private static ConfigurationProperty passwordProp = new ConfigurationProperty("password", typeof(string), "");

		// Token: 0x0400223A RID: 8762
		private static ConfigurationProperty userNameProp = new ConfigurationProperty("userName", typeof(string), "");

		// Token: 0x0400223B RID: 8763
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
