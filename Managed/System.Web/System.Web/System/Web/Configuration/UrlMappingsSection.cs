using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Provides programmatic access to the urlMappings configuration section. This class cannot be inherited.</summary>
	// Token: 0x020005EA RID: 1514
	public sealed class UrlMappingsSection : ConfigurationSection
	{
		// Token: 0x060041AF RID: 16815 RVA: 0x000ABBF0 File Offset: 0x000A9DF0
		static UrlMappingsSection()
		{
			UrlMappingsSection.properties.Add(UrlMappingsSection.enabledProp);
			UrlMappingsSection.properties.Add(UrlMappingsSection.urlMappingsProp);
		}

		/// <summary>Gets or sets a value indicating whether the mapping is enabled.</summary>
		/// <returns>true if the mapping is enabled; otherwise, false. The default value is true.</returns>
		// Token: 0x170014E7 RID: 5351
		// (get) Token: 0x060041B0 RID: 16816 RVA: 0x000ABC61 File Offset: 0x000A9E61
		// (set) Token: 0x060041B1 RID: 16817 RVA: 0x000ABC73 File Offset: 0x000A9E73
		[ConfigurationProperty("enabled", DefaultValue = "True")]
		public bool IsEnabled
		{
			get
			{
				return (bool)base[UrlMappingsSection.enabledProp];
			}
			set
			{
				base[UrlMappingsSection.enabledProp] = value;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.Configuration.UrlMapping" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.UrlMappingCollection" /> that contains <see cref="T:System.Web.Configuration.UrlMapping" /> objects.</returns>
		// Token: 0x170014E8 RID: 5352
		// (get) Token: 0x060041B2 RID: 16818 RVA: 0x000ABC86 File Offset: 0x000A9E86
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public UrlMappingCollection UrlMappings
		{
			get
			{
				return (UrlMappingCollection)base[UrlMappingsSection.urlMappingsProp];
			}
		}

		// Token: 0x170014E9 RID: 5353
		// (get) Token: 0x060041B3 RID: 16819 RVA: 0x000ABC98 File Offset: 0x000A9E98
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return UrlMappingsSection.properties;
			}
		}

		// Token: 0x04002342 RID: 9026
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);

		// Token: 0x04002343 RID: 9027
		private static ConfigurationProperty urlMappingsProp = new ConfigurationProperty("", typeof(UrlMappingCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04002344 RID: 9028
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
