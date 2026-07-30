using System;
using System.Configuration;
using System.Web.Configuration;

namespace System.Web
{
	/// <summary>The <see cref="T:System.Web.SiteMap" /> class is an in-memory representation of the navigation structure for a site, which is provided by one or more site map providers. This class cannot be inherited. </summary>
	// Token: 0x020000D2 RID: 210
	public static class SiteMap
	{
		// Token: 0x06000B2A RID: 2858 RVA: 0x0001E2AC File Offset: 0x0001C4AC
		private static void Init()
		{
			object obj = SiteMap.locker;
			lock (obj)
			{
				if (SiteMap.provider == null)
				{
					SiteMapSection siteMapSection = (SiteMapSection)WebConfigurationManager.GetSection("system.web/siteMap");
					if (!siteMapSection.Enabled)
					{
						throw new InvalidOperationException("This feature is currently disabled.  Please enable it in the system.web/siteMap section in the web.config file.");
					}
					SiteMap.providers = siteMapSection.ProvidersInternal;
					SiteMap.providers.SetReadOnly();
					SiteMap.provider = SiteMap.providers[siteMapSection.DefaultProvider];
					if (SiteMap.provider == null)
					{
						throw new ConfigurationErrorsException(string.Format("The default sitemap provider '{0}' does not exist in the provider collection.", siteMapSection.DefaultProvider));
					}
				}
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.SiteMapNode" /> control that represents the currently requested page.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> instance that represents the currently requested page; otherwise, null, if no representative node exists in the site map information. </returns>
		/// <exception cref="T:System.InvalidOperationException">The site map feature is not enabled.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The default provider specified in the configuration does not exist.</exception>
		/// <exception cref="T:System.Web.HttpException">The feature is supported only when running in Low trust or higher.</exception>
		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0001E358 File Offset: 0x0001C558
		public static SiteMapNode CurrentNode
		{
			get
			{
				return SiteMap.Provider.CurrentNode;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.SiteMapNode" /> object that represents the top-level page of the navigation structure for the site.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the top-level page of the site's navigation structure; otherwise, null, if security trimming is enabled and the node cannot be returned to the current user.</returns>
		/// <exception cref="T:System.InvalidOperationException">The site map feature is not enabled.- or -The <see cref="P:System.Web.SiteMap.RootNode" /> resolves to null, which occurs if security trimming is enabled and the root node is not visible to the current user. </exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The default provider specified in the configuration does not exist.</exception>
		/// <exception cref="T:System.Web.HttpException">The feature is supported only when running in Low trust or higher.</exception>
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0001E364 File Offset: 0x0001C564
		public static SiteMapNode RootNode
		{
			get
			{
				return SiteMap.Provider.RootNode;
			}
		}

		/// <summary>Gets the default <see cref="T:System.Web.SiteMapProvider" /> object for the current site map.</summary>
		/// <returns>The default site map provider for the <see cref="T:System.Web.SiteMap" />. </returns>
		/// <exception cref="T:System.InvalidOperationException">The site map feature is not enabled.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The default provider specified in the configuration does not exist.</exception>
		/// <exception cref="T:System.Web.HttpException">The feature is supported only when running in Low trust or higher.</exception>
		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0001E370 File Offset: 0x0001C570
		public static SiteMapProvider Provider
		{
			get
			{
				SiteMap.Init();
				return SiteMap.provider;
			}
		}

		/// <summary>Gets a read-only collection of named <see cref="T:System.Web.SiteMapProvider" /> objects that are available to the <see cref="T:System.Web.SiteMap" /> class.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapProviderCollection" /> of named <see cref="T:System.Web.SiteMapProvider" /> objects.</returns>
		/// <exception cref="T:System.InvalidOperationException">The site map feature is not enabled.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The default provider specified in the configuration does not exist.</exception>
		/// <exception cref="T:System.Web.HttpException">The feature is supported only when running in Low trust or higher.</exception>
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x0001E37C File Offset: 0x0001C57C
		public static SiteMapProviderCollection Providers
		{
			get
			{
				SiteMap.Init();
				return SiteMap.providers;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Web.SiteMap.CurrentNode" /> property is accessed. </summary>
		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000B2F RID: 2863 RVA: 0x0001E388 File Offset: 0x0001C588
		// (remove) Token: 0x06000B30 RID: 2864 RVA: 0x0001E395 File Offset: 0x0001C595
		public static event SiteMapResolveEventHandler SiteMapResolve
		{
			add
			{
				SiteMap.Provider.SiteMapResolve += value;
			}
			remove
			{
				SiteMap.Provider.SiteMapResolve -= value;
			}
		}

		/// <summary>Gets a Boolean value indicating if a site map provider is specified in the Web.config file and if the site map provider is enabled.</summary>
		/// <returns>true if a site map provider is configured and enabled; otherwise, false.</returns>
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0001E3A2 File Offset: 0x0001C5A2
		public static bool Enabled
		{
			get
			{
				return ((SiteMapSection)WebConfigurationManager.GetSection("system.web/siteMap")).Enabled;
			}
		}

		// Token: 0x04001093 RID: 4243
		private static SiteMapProvider provider;

		// Token: 0x04001094 RID: 4244
		private static SiteMapProviderCollection providers;

		// Token: 0x04001095 RID: 4245
		private static object locker = new object();
	}
}
