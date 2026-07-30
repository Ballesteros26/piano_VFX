using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the authentication for a Web application. This class cannot be inherited.</summary>
	// Token: 0x02000583 RID: 1411
	public sealed class AuthenticationSection : ConfigurationSection
	{
		// Token: 0x06003B92 RID: 15250 RVA: 0x0009F668 File Offset: 0x0009D868
		static AuthenticationSection()
		{
			AuthenticationSection.properties.Add(AuthenticationSection.formsProp);
			AuthenticationSection.properties.Add(AuthenticationSection.passportProp);
			AuthenticationSection.properties.Add(AuthenticationSection.modeProp);
		}

		// Token: 0x06003B94 RID: 15252 RVA: 0x0009F722 File Offset: 0x0009D922
		protected internal override void Reset(ConfigurationElement parentElement)
		{
			base.Reset(parentElement);
		}

		/// <summary>Gets the <see cref="P:System.Web.Configuration.AuthenticationSection.Forms" /> element property.</summary>
		/// <returns>A <see cref="P:System.Web.Configuration.AuthenticationSection.Forms" /> element property that contains information used during forms-based authentication.</returns>
		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x06003B95 RID: 15253 RVA: 0x0009F72B File Offset: 0x0009D92B
		[ConfigurationProperty("forms")]
		public FormsAuthenticationConfiguration Forms
		{
			get
			{
				return (FormsAuthenticationConfiguration)base[AuthenticationSection.formsProp];
			}
		}

		/// <summary>Gets the <see cref="P:System.Web.Configuration.AuthenticationSection.Passport" /> element property.</summary>
		/// <returns>A <see cref="P:System.Web.Configuration.AuthenticationSection.Passport" /> element property that contains information used during passport-based authentication.</returns>
		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x06003B96 RID: 15254 RVA: 0x0009F73D File Offset: 0x0009D93D
		[Obsolete("This property is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
		[ConfigurationProperty("passport")]
		public PassportAuthentication Passport
		{
			get
			{
				return (PassportAuthentication)base[AuthenticationSection.passportProp];
			}
		}

		/// <summary>Gets or sets the authentication modality.</summary>
		/// <returns>One of the <see cref="T:System.Web.Configuration.AuthenticationMode" /> values.</returns>
		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x06003B97 RID: 15255 RVA: 0x0009F74F File Offset: 0x0009D94F
		// (set) Token: 0x06003B98 RID: 15256 RVA: 0x0009F761 File Offset: 0x0009D961
		[ConfigurationProperty("mode", DefaultValue = "Windows")]
		public AuthenticationMode Mode
		{
			get
			{
				return (AuthenticationMode)base[AuthenticationSection.modeProp];
			}
			set
			{
				base[AuthenticationSection.modeProp] = value;
			}
		}

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x06003B99 RID: 15257 RVA: 0x0009F774 File Offset: 0x0009D974
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AuthenticationSection.properties;
			}
		}

		// Token: 0x0400208B RID: 8331
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400208C RID: 8332
		private static ConfigurationProperty formsProp = new ConfigurationProperty("forms", typeof(FormsAuthenticationConfiguration), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400208D RID: 8333
		private static ConfigurationProperty passportProp = new ConfigurationProperty("passport", typeof(PassportAuthentication), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400208E RID: 8334
		private static ConfigurationProperty modeProp = new ConfigurationProperty("mode", typeof(AuthenticationMode), AuthenticationMode.Windows, new GenericEnumConverter(typeof(AuthenticationMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
	}
}
