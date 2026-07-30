using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for Web proxy server usage. This class cannot be inherited.</summary>
	// Token: 0x0200069D RID: 1693
	public sealed class DefaultProxySection : ConfigurationSection
	{
		// Token: 0x06003511 RID: 13585 RVA: 0x000C45D8 File Offset: 0x000C27D8
		static DefaultProxySection()
		{
			DefaultProxySection.properties.Add(DefaultProxySection.bypassListProp);
			DefaultProxySection.properties.Add(DefaultProxySection.enabledProp);
			DefaultProxySection.properties.Add(DefaultProxySection.moduleProp);
			DefaultProxySection.properties.Add(DefaultProxySection.proxyProp);
			DefaultProxySection.properties.Add(DefaultProxySection.useDefaultCredentialsProp);
		}

		/// <summary>Gets the collection of resources that are not obtained using the Web proxy server.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.BypassElementCollection" /> that contains the addresses of resources that bypass the Web proxy server. </returns>
		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06003513 RID: 13587 RVA: 0x000C46C6 File Offset: 0x000C28C6
		[ConfigurationProperty("bypasslist")]
		public BypassElementCollection BypassList
		{
			get
			{
				return (BypassElementCollection)base[DefaultProxySection.bypassListProp];
			}
		}

		/// <summary>Gets or sets whether a Web proxy is used.</summary>
		/// <returns>true if a Web proxy will be used; otherwise, false.</returns>
		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06003514 RID: 13588 RVA: 0x000C46D8 File Offset: 0x000C28D8
		// (set) Token: 0x06003515 RID: 13589 RVA: 0x000C46EA File Offset: 0x000C28EA
		[ConfigurationProperty("enabled", DefaultValue = "True")]
		public bool Enabled
		{
			get
			{
				return (bool)base[DefaultProxySection.enabledProp];
			}
			set
			{
				base[DefaultProxySection.enabledProp] = value;
			}
		}

		/// <summary>Gets the type information for a custom Web proxy implementation.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ModuleElement" />. The type information for a custom Web proxy implementation.</returns>
		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06003516 RID: 13590 RVA: 0x000C46FD File Offset: 0x000C28FD
		[ConfigurationProperty("module")]
		public ModuleElement Module
		{
			get
			{
				return (ModuleElement)base[DefaultProxySection.moduleProp];
			}
		}

		/// <summary>Gets the URI that identifies the Web proxy server to use.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ProxyElement" />. The URI that identifies the Web proxy server.</returns>
		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06003517 RID: 13591 RVA: 0x000C470F File Offset: 0x000C290F
		[ConfigurationProperty("proxy")]
		public ProxyElement Proxy
		{
			get
			{
				return (ProxyElement)base[DefaultProxySection.proxyProp];
			}
		}

		/// <summary>Gets or sets whether default credentials are to be used to access a Web proxy server.</summary>
		/// <returns>true if default credentials are to be used; otherwise, false.</returns>
		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06003518 RID: 13592 RVA: 0x000C4721 File Offset: 0x000C2921
		// (set) Token: 0x06003519 RID: 13593 RVA: 0x000C4733 File Offset: 0x000C2933
		[ConfigurationProperty("useDefaultCredentials", DefaultValue = "False")]
		public bool UseDefaultCredentials
		{
			get
			{
				return (bool)base[DefaultProxySection.useDefaultCredentialsProp];
			}
			set
			{
				base[DefaultProxySection.useDefaultCredentialsProp] = value;
			}
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x0600351A RID: 13594 RVA: 0x000C4746 File Offset: 0x000C2946
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return DefaultProxySection.properties;
			}
		}

		// Token: 0x0600351B RID: 13595 RVA: 0x000027E8 File Offset: 0x000009E8
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x000027E8 File Offset: 0x000009E8
		[MonoTODO]
		protected override void Reset(ConfigurationElement parentElement)
		{
		}

		// Token: 0x04002A5F RID: 10847
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002A60 RID: 10848
		private static ConfigurationProperty bypassListProp = new ConfigurationProperty("bypasslist", typeof(BypassElementCollection), null);

		// Token: 0x04002A61 RID: 10849
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);

		// Token: 0x04002A62 RID: 10850
		private static ConfigurationProperty moduleProp = new ConfigurationProperty("module", typeof(ModuleElement), null);

		// Token: 0x04002A63 RID: 10851
		private static ConfigurationProperty proxyProp = new ConfigurationProperty("proxy", typeof(ProxyElement), null);

		// Token: 0x04002A64 RID: 10852
		private static ConfigurationProperty useDefaultCredentialsProp = new ConfigurationProperty("useDefaultCredentials", typeof(bool), false);
	}
}
