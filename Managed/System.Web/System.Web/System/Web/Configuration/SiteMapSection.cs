using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Defines configuration settings that are used to support the infrastructure for configuring, storing, and rendering site navigation. This class cannot be inherited.</summary>
	// Token: 0x020005D9 RID: 1497
	public sealed class SiteMapSection : ConfigurationSection
	{
		// Token: 0x060040BF RID: 16575 RVA: 0x000AA6D4 File Offset: 0x000A88D4
		static SiteMapSection()
		{
			SiteMapSection.properties.Add(SiteMapSection.defaultProviderProp);
			SiteMapSection.properties.Add(SiteMapSection.enabledProp);
			SiteMapSection.properties.Add(SiteMapSection.providersProp);
		}

		/// <summary>Gets or sets the name of the default navigation provider. </summary>
		/// <returns>The name of a provider in the <see cref="P:System.Web.Configuration.SiteMapSection.Providers" /> property or a <see cref="F:System.String.Empty" /> field. The default is "AspNetXmlSiteMapProvider".</returns>
		// Token: 0x1700147B RID: 5243
		// (get) Token: 0x060040C0 RID: 16576 RVA: 0x000AA76E File Offset: 0x000A896E
		// (set) Token: 0x060040C1 RID: 16577 RVA: 0x000AA780 File Offset: 0x000A8980
		[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetXmlSiteMapProvider")]
		[StringValidator(MinLength = 1)]
		public string DefaultProvider
		{
			get
			{
				return (string)base["defaultProvider"];
			}
			set
			{
				base["defaultProvider"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the ASP.NET site map feature is enabled.</summary>
		/// <returns>true if the ASP.NET site map feature is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x1700147C RID: 5244
		// (get) Token: 0x060040C2 RID: 16578 RVA: 0x000AA78E File Offset: 0x000A898E
		// (set) Token: 0x060040C3 RID: 16579 RVA: 0x000AA7A0 File Offset: 0x000A89A0
		[ConfigurationProperty("enabled", DefaultValue = "True")]
		public bool Enabled
		{
			get
			{
				return (bool)base["enabled"];
			}
			set
			{
				base["enabled"] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection of <see cref="T:System.Configuration.ProviderSettings" /> objects.</summary>
		/// <returns>A <see cref="T:System.Configuration.ProviderSettingsCollection" /> that contains the providers settings defined within the providers subsection of the siteMap section of the configuration file.</returns>
		// Token: 0x1700147D RID: 5245
		// (get) Token: 0x060040C4 RID: 16580 RVA: 0x000AA7B3 File Offset: 0x000A89B3
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base["providers"];
			}
		}

		// Token: 0x1700147E RID: 5246
		// (get) Token: 0x060040C5 RID: 16581 RVA: 0x000AA7C8 File Offset: 0x000A89C8
		internal SiteMapProviderCollection ProvidersInternal
		{
			get
			{
				if (this.providers == null)
				{
					SiteMapProviderCollection siteMapProviderCollection = new SiteMapProviderCollection();
					ProvidersHelper.InstantiateProviders(this.Providers, siteMapProviderCollection, typeof(SiteMapProvider));
					this.providers = siteMapProviderCollection;
				}
				return this.providers;
			}
		}

		// Token: 0x1700147F RID: 5247
		// (get) Token: 0x060040C6 RID: 16582 RVA: 0x000AA806 File Offset: 0x000A8A06
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SiteMapSection.properties;
			}
		}

		// Token: 0x0400230F RID: 8975
		private static ConfigurationProperty defaultProviderProp = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetXmlSiteMapProvider");

		// Token: 0x04002310 RID: 8976
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);

		// Token: 0x04002311 RID: 8977
		private static ConfigurationProperty providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection));

		// Token: 0x04002312 RID: 8978
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002313 RID: 8979
		private SiteMapProviderCollection providers;
	}
}
