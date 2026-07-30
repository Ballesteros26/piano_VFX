using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the output cache settings for application pages . This class cannot be inherited.</summary>
	// Token: 0x020005C2 RID: 1474
	public sealed class OutputCacheSettingsSection : ConfigurationSection
	{
		// Token: 0x06003F42 RID: 16194 RVA: 0x000A7488 File Offset: 0x000A5688
		static OutputCacheSettingsSection()
		{
			OutputCacheSettingsSection.properties.Add(OutputCacheSettingsSection.outputCacheProfilesProp);
		}

		/// <summary>Gets a <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" />.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> of <see cref="T:System.Web.Configuration.OutputCacheProfile" /> objects</returns>
		// Token: 0x170013E1 RID: 5089
		// (get) Token: 0x06003F43 RID: 16195 RVA: 0x000A74C4 File Offset: 0x000A56C4
		[ConfigurationProperty("outputCacheProfiles")]
		public OutputCacheProfileCollection OutputCacheProfiles
		{
			get
			{
				return (OutputCacheProfileCollection)base[OutputCacheSettingsSection.outputCacheProfilesProp];
			}
		}

		// Token: 0x170013E2 RID: 5090
		// (get) Token: 0x06003F44 RID: 16196 RVA: 0x000A74D6 File Offset: 0x000A56D6
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return OutputCacheSettingsSection.properties;
			}
		}

		// Token: 0x04002277 RID: 8823
		private static ConfigurationProperty outputCacheProfilesProp = new ConfigurationProperty("outputCacheProfiles", typeof(OutputCacheProfileCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002278 RID: 8824
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
