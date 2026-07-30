using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Allows you to specify the personalization provider and set personalization authorizations. This class cannot be inherited.</summary>
	// Token: 0x020005F5 RID: 1525
	public sealed class WebPartsPersonalization : ConfigurationElement
	{
		// Token: 0x0600423C RID: 16956 RVA: 0x000AD3B4 File Offset: 0x000AB5B4
		static WebPartsPersonalization()
		{
			WebPartsPersonalization.properties.Add(WebPartsPersonalization.authorizationProp);
			WebPartsPersonalization.properties.Add(WebPartsPersonalization.defaultProviderProp);
			WebPartsPersonalization.properties.Add(WebPartsPersonalization.providersProp);
		}

		/// <summary>Gets an <see cref="T:System.Web.Configuration.AuthorizationSection" /> object containing the Web Parts personalization authorizations for the current Web application.</summary>
		/// <returns>An <see cref="T:System.Web.Configuration.AuthorizationSection" /> object containing the Web Parts personalization authorizations for the current Web application.</returns>
		// Token: 0x17001502 RID: 5378
		// (get) Token: 0x0600423D RID: 16957 RVA: 0x000AD46D File Offset: 0x000AB66D
		[ConfigurationProperty("authorization")]
		public WebPartsPersonalizationAuthorization Authorization
		{
			get
			{
				return (WebPartsPersonalizationAuthorization)base[WebPartsPersonalization.authorizationProp];
			}
		}

		/// <summary>Gets or sets the name of the default Web Parts personalization provider.</summary>
		/// <returns>The name of the default Web Parts personalization provider.</returns>
		// Token: 0x17001503 RID: 5379
		// (get) Token: 0x0600423E RID: 16958 RVA: 0x000AD47F File Offset: 0x000AB67F
		// (set) Token: 0x0600423F RID: 16959 RVA: 0x000AD491 File Offset: 0x000AB691
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetSqlPersonalizationProvider")]
		public string DefaultProvider
		{
			get
			{
				return (string)base[WebPartsPersonalization.defaultProviderProp];
			}
			set
			{
				base[WebPartsPersonalization.defaultProviderProp] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection that contains the Web Parts personalization providers for the current Web application.</summary>
		/// <returns>A <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection that contains the Web Parts personalization providers for the current Web application.</returns>
		// Token: 0x17001504 RID: 5380
		// (get) Token: 0x06004240 RID: 16960 RVA: 0x000AD49F File Offset: 0x000AB69F
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[WebPartsPersonalization.providersProp];
			}
		}

		// Token: 0x17001505 RID: 5381
		// (get) Token: 0x06004241 RID: 16961 RVA: 0x000AD4B1 File Offset: 0x000AB6B1
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebPartsPersonalization.properties;
			}
		}

		// Token: 0x0400236F RID: 9071
		private static ConfigurationProperty authorizationProp = new ConfigurationProperty("authorization", typeof(WebPartsPersonalizationAuthorization), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002370 RID: 9072
		private static ConfigurationProperty defaultProviderProp = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetSqlPersonalizationProvider", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002371 RID: 9073
		private static ConfigurationProperty providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002372 RID: 9074
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
