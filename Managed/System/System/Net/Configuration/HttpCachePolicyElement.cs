using System;
using System.Configuration;
using System.Net.Cache;
using System.Xml;

namespace System.Net.Configuration
{
	/// <summary>Represents the default HTTP cache policy for network resources. This class cannot be inherited.</summary>
	// Token: 0x0200069F RID: 1695
	public sealed class HttpCachePolicyElement : ConfigurationElement
	{
		// Token: 0x06003524 RID: 13604 RVA: 0x000C47B4 File Offset: 0x000C29B4
		static HttpCachePolicyElement()
		{
			HttpCachePolicyElement.properties.Add(HttpCachePolicyElement.maximumAgeProp);
			HttpCachePolicyElement.properties.Add(HttpCachePolicyElement.maximumStaleProp);
			HttpCachePolicyElement.properties.Add(HttpCachePolicyElement.minimumFreshProp);
			HttpCachePolicyElement.properties.Add(HttpCachePolicyElement.policyLevelProp);
		}

		/// <summary>Gets or sets the maximum age permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the maximum age for cached resources specified in the configuration file.</returns>
		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06003526 RID: 13606 RVA: 0x000C4890 File Offset: 0x000C2A90
		// (set) Token: 0x06003527 RID: 13607 RVA: 0x000C48A2 File Offset: 0x000C2AA2
		[ConfigurationProperty("maximumAge", DefaultValue = "10675199.02:48:05.4775807")]
		public TimeSpan MaximumAge
		{
			get
			{
				return (TimeSpan)base[HttpCachePolicyElement.maximumAgeProp];
			}
			set
			{
				base[HttpCachePolicyElement.maximumAgeProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum staleness value permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that is set to the maximum staleness value specified in the configuration file.</returns>
		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06003528 RID: 13608 RVA: 0x000C48B5 File Offset: 0x000C2AB5
		// (set) Token: 0x06003529 RID: 13609 RVA: 0x000C48C7 File Offset: 0x000C2AC7
		[ConfigurationProperty("maximumStale", DefaultValue = "-10675199.02:48:05.4775808")]
		public TimeSpan MaximumStale
		{
			get
			{
				return (TimeSpan)base[HttpCachePolicyElement.maximumStaleProp];
			}
			set
			{
				base[HttpCachePolicyElement.maximumStaleProp] = value;
			}
		}

		/// <summary>Gets or sets the minimum freshness permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the minimum freshness specified in the configuration file.</returns>
		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x0600352A RID: 13610 RVA: 0x000C48DA File Offset: 0x000C2ADA
		// (set) Token: 0x0600352B RID: 13611 RVA: 0x000C48EC File Offset: 0x000C2AEC
		[ConfigurationProperty("minimumFresh", DefaultValue = "-10675199.02:48:05.4775808")]
		public TimeSpan MinimumFresh
		{
			get
			{
				return (TimeSpan)base[HttpCachePolicyElement.minimumFreshProp];
			}
			set
			{
				base[HttpCachePolicyElement.minimumFreshProp] = value;
			}
		}

		/// <summary>Gets or sets HTTP caching behavior for the local machine.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.HttpRequestCacheLevel" /> value that specifies the cache behavior.</returns>
		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x0600352C RID: 13612 RVA: 0x000C48FF File Offset: 0x000C2AFF
		// (set) Token: 0x0600352D RID: 13613 RVA: 0x000C4911 File Offset: 0x000C2B11
		[ConfigurationProperty("policyLevel", DefaultValue = "Default", Options = ConfigurationPropertyOptions.IsRequired)]
		public HttpRequestCacheLevel PolicyLevel
		{
			get
			{
				return (HttpRequestCacheLevel)base[HttpCachePolicyElement.policyLevelProp];
			}
			set
			{
				base[HttpCachePolicyElement.policyLevelProp] = value;
			}
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x0600352E RID: 13614 RVA: 0x000C4924 File Offset: 0x000C2B24
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpCachePolicyElement.properties;
			}
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO]
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO]
		protected override void Reset(ConfigurationElement parentElement)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04002A67 RID: 10855
		private static ConfigurationProperty maximumAgeProp = new ConfigurationProperty("maximumAge", typeof(TimeSpan), TimeSpan.MaxValue);

		// Token: 0x04002A68 RID: 10856
		private static ConfigurationProperty maximumStaleProp = new ConfigurationProperty("maximumStale", typeof(TimeSpan), TimeSpan.MinValue);

		// Token: 0x04002A69 RID: 10857
		private static ConfigurationProperty minimumFreshProp = new ConfigurationProperty("minimumFresh", typeof(TimeSpan), TimeSpan.MinValue);

		// Token: 0x04002A6A RID: 10858
		private static ConfigurationProperty policyLevelProp = new ConfigurationProperty("policyLevel", typeof(HttpRequestCacheLevel), HttpRequestCacheLevel.Default, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04002A6B RID: 10859
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
