using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the caching group within a configuration file. This class cannot be inherited. </summary>
	// Token: 0x02000579 RID: 1401
	public sealed class SystemWebCachingSectionGroup : ConfigurationSectionGroup
	{
		/// <summary>Gets the cache section contained within the configuration.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.CacheSection" /> object.</returns>
		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x06003B66 RID: 15206 RVA: 0x0009F13B File Offset: 0x0009D33B
		[ConfigurationProperty("cache")]
		public CacheSection Cache
		{
			get
			{
				return (CacheSection)base.Sections["cache"];
			}
		}

		/// <summary>Gets the outputCache section contained within the configuration.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.OutputCacheSection" /> object.</returns>
		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x06003B67 RID: 15207 RVA: 0x0009F152 File Offset: 0x0009D352
		[ConfigurationProperty("outputCache")]
		public OutputCacheSection OutputCache
		{
			get
			{
				return (OutputCacheSection)base.Sections["outputCache"];
			}
		}

		/// <summary>Gets the outputCacheSettings section contained within the configuration.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.OutputCacheSettingsSection" /> object.</returns>
		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x06003B68 RID: 15208 RVA: 0x0009F169 File Offset: 0x0009D369
		[ConfigurationProperty("outputCacheSettings")]
		public OutputCacheSettingsSection OutputCacheSettings
		{
			get
			{
				return (OutputCacheSettingsSection)base.Sections["outputCacheSettings"];
			}
		}

		/// <summary>Gets the sqlCacheDependency section contained within the configuration.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.SqlCacheDependencySection" /> object.</returns>
		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x06003B69 RID: 15209 RVA: 0x0009F180 File Offset: 0x0009D380
		[ConfigurationProperty("sqlCacheDependency")]
		public SqlCacheDependencySection SqlCacheDependency
		{
			get
			{
				return (SqlCacheDependencySection)base.Sections["sqlCacheDependency"];
			}
		}
	}
}
