using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the output cache for a Web application. This class cannot be inherited.</summary>
	// Token: 0x020005C1 RID: 1473
	public sealed class OutputCacheSection : ConfigurationSection
	{
		// Token: 0x06003F32 RID: 16178 RVA: 0x000A7244 File Offset: 0x000A5444
		static OutputCacheSection()
		{
			OutputCacheSection.properties.Add(OutputCacheSection.enableFragmentCacheProp);
			OutputCacheSection.properties.Add(OutputCacheSection.enableOutputCacheProp);
			OutputCacheSection.properties.Add(OutputCacheSection.omitVaryStarProp);
			OutputCacheSection.properties.Add(OutputCacheSection.sendCacheControlHeaderProp);
			OutputCacheSection.properties.Add(OutputCacheSection.enableKernelCacheForVaryByStarProp);
			OutputCacheSection.properties.Add(OutputCacheSection.providersProp);
			OutputCacheSection.properties.Add(OutputCacheSection.defaultProviderNameProp);
		}

		/// <summary>Gets or sets a value indicating whether the fragment cache is enabled.</summary>
		/// <returns>true if the fragment cache is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x06003F33 RID: 16179 RVA: 0x000A7396 File Offset: 0x000A5596
		// (set) Token: 0x06003F34 RID: 16180 RVA: 0x000A73A8 File Offset: 0x000A55A8
		[ConfigurationProperty("enableFragmentCache", DefaultValue = "True")]
		public bool EnableFragmentCache
		{
			get
			{
				return (bool)base[OutputCacheSection.enableFragmentCacheProp];
			}
			set
			{
				base[OutputCacheSection.enableFragmentCacheProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the output cache is enabled.</summary>
		/// <returns>true if the output cache is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x06003F35 RID: 16181 RVA: 0x000A73BB File Offset: 0x000A55BB
		// (set) Token: 0x06003F36 RID: 16182 RVA: 0x000A73CD File Offset: 0x000A55CD
		[ConfigurationProperty("enableOutputCache", DefaultValue = "True")]
		public bool EnableOutputCache
		{
			get
			{
				return (bool)base[OutputCacheSection.enableOutputCacheProp];
			}
			set
			{
				base[OutputCacheSection.enableOutputCacheProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether kernel caching is enabled.</summary>
		/// <returns>true if kernel caching is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x170013DB RID: 5083
		// (get) Token: 0x06003F37 RID: 16183 RVA: 0x000A73E0 File Offset: 0x000A55E0
		// (set) Token: 0x06003F38 RID: 16184 RVA: 0x000A73F2 File Offset: 0x000A55F2
		[ConfigurationProperty("enableKernelCacheForVaryByStar", DefaultValue = "False")]
		public bool EnableKernelCacheForVaryByStar
		{
			get
			{
				return (bool)base[OutputCacheSection.enableKernelCacheForVaryByStarProp];
			}
			set
			{
				base[OutputCacheSection.enableKernelCacheForVaryByStarProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the vary header is enabled.</summary>
		/// <returns>true if the vary header is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x06003F39 RID: 16185 RVA: 0x000A7405 File Offset: 0x000A5605
		// (set) Token: 0x06003F3A RID: 16186 RVA: 0x000A7417 File Offset: 0x000A5617
		[ConfigurationProperty("omitVaryStar", DefaultValue = "False")]
		public bool OmitVaryStar
		{
			get
			{
				return (bool)base[OutputCacheSection.omitVaryStarProp];
			}
			set
			{
				base[OutputCacheSection.omitVaryStarProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the cache-control:private header is sent by the output cache module by default.</summary>
		/// <returns>true if the sending of cache-control:private header is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x06003F3B RID: 16187 RVA: 0x000A742A File Offset: 0x000A562A
		// (set) Token: 0x06003F3C RID: 16188 RVA: 0x000A743C File Offset: 0x000A563C
		[ConfigurationProperty("sendCacheControlHeader", DefaultValue = "True")]
		public bool SendCacheControlHeader
		{
			get
			{
				return (bool)base[OutputCacheSection.sendCacheControlHeaderProp];
			}
			set
			{
				base[OutputCacheSection.sendCacheControlHeaderProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the ASP.NET default output-cache provider that is stored in the <see cref="T:System.Web.Configuration.OutputCacheSection" /> element of a configuration file.</summary>
		/// <returns>The name of the default provider.</returns>
		// Token: 0x170013DE RID: 5086
		// (get) Token: 0x06003F3D RID: 16189 RVA: 0x000A744F File Offset: 0x000A564F
		// (set) Token: 0x06003F3E RID: 16190 RVA: 0x000A7461 File Offset: 0x000A5661
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetInternalProvider")]
		public string DefaultProviderName
		{
			get
			{
				return base[OutputCacheSection.defaultProviderNameProp] as string;
			}
			set
			{
				base[OutputCacheSection.defaultProviderNameProp] = value;
			}
		}

		/// <summary>Gets or sets the collection of output-cache providers that are stored in the <see cref="T:System.Web.Configuration.OutputCacheSection" /> element of a configuration file. </summary>
		/// <returns>The collection of providers.</returns>
		// Token: 0x170013DF RID: 5087
		// (get) Token: 0x06003F3F RID: 16191 RVA: 0x000A746F File Offset: 0x000A566F
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return base[OutputCacheSection.providersProp] as ProviderSettingsCollection;
			}
		}

		// Token: 0x170013E0 RID: 5088
		// (get) Token: 0x06003F40 RID: 16192 RVA: 0x000A7481 File Offset: 0x000A5681
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return OutputCacheSection.properties;
			}
		}

		// Token: 0x0400226F RID: 8815
		private static ConfigurationProperty enableFragmentCacheProp = new ConfigurationProperty("enableFragmentCache", typeof(bool), true);

		// Token: 0x04002270 RID: 8816
		private static ConfigurationProperty enableOutputCacheProp = new ConfigurationProperty("enableOutputCache", typeof(bool), true);

		// Token: 0x04002271 RID: 8817
		private static ConfigurationProperty omitVaryStarProp = new ConfigurationProperty("omitVaryStar", typeof(bool), false);

		// Token: 0x04002272 RID: 8818
		private static ConfigurationProperty sendCacheControlHeaderProp = new ConfigurationProperty("sendCacheControlHeader", typeof(bool), true);

		// Token: 0x04002273 RID: 8819
		private static ConfigurationProperty enableKernelCacheForVaryByStarProp = new ConfigurationProperty("enableKernelCacheForVaryByStar", typeof(bool), false);

		// Token: 0x04002274 RID: 8820
		private static ConfigurationProperty providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection));

		// Token: 0x04002275 RID: 8821
		private static ConfigurationProperty defaultProviderNameProp = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetInternalProvider");

		// Token: 0x04002276 RID: 8822
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
