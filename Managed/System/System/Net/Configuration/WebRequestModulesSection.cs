using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for Web request modules. This class cannot be inherited.</summary>
	// Token: 0x020006B7 RID: 1719
	public sealed class WebRequestModulesSection : ConfigurationSection
	{
		// Token: 0x060035DE RID: 13790 RVA: 0x000C5CF4 File Offset: 0x000C3EF4
		static WebRequestModulesSection()
		{
			WebRequestModulesSection.properties.Add(WebRequestModulesSection.webRequestModulesProp);
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x060035DF RID: 13791 RVA: 0x000C5D2A File Offset: 0x000C3F2A
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebRequestModulesSection.properties;
			}
		}

		/// <summary>Gets the collection of Web request modules in the section.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.WebRequestModuleElementCollection" /> containing the registered Web request modules. </returns>
		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x060035E0 RID: 13792 RVA: 0x000C5D31 File Offset: 0x000C3F31
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public WebRequestModuleElementCollection WebRequestModules
		{
			get
			{
				return (WebRequestModuleElementCollection)base[WebRequestModulesSection.webRequestModulesProp];
			}
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x000027E8 File Offset: 0x000009E8
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x000027E8 File Offset: 0x000009E8
		[MonoTODO]
		protected override void InitializeDefault()
		{
		}

		// Token: 0x04002AA8 RID: 10920
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002AA9 RID: 10921
		private static ConfigurationProperty webRequestModulesProp = new ConfigurationProperty("", typeof(WebRequestModuleElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
