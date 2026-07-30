using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Gets the section group information for the networking namespaces. This class cannot be inherited.</summary>
	// Token: 0x020006A6 RID: 1702
	public sealed class NetSectionGroup : ConfigurationSectionGroup
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.NetSectionGroup" /> class. </summary>
		// Token: 0x0600354E RID: 13646 RVA: 0x0003947D File Offset: 0x0003767D
		[MonoTODO]
		public NetSectionGroup()
		{
		}

		/// <summary>Gets the configuration section containing the authentication modules registered for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.AuthenticationModulesSection" /> object.</returns>
		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x0600354F RID: 13647 RVA: 0x000C4EEC File Offset: 0x000C30EC
		[ConfigurationProperty("authenticationModules")]
		public AuthenticationModulesSection AuthenticationModules
		{
			get
			{
				return (AuthenticationModulesSection)base.Sections["authenticationModules"];
			}
		}

		/// <summary>Gets the configuration section containing the connection management settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ConnectionManagementSection" /> object.</returns>
		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06003550 RID: 13648 RVA: 0x000C4F03 File Offset: 0x000C3103
		[ConfigurationProperty("connectionManagement")]
		public ConnectionManagementSection ConnectionManagement
		{
			get
			{
				return (ConnectionManagementSection)base.Sections["connectionManagement"];
			}
		}

		/// <summary>Gets the configuration section containing the default Web proxy server settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.DefaultProxySection" /> object.</returns>
		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06003551 RID: 13649 RVA: 0x000C4F1A File Offset: 0x000C311A
		[ConfigurationProperty("defaultProxy")]
		public DefaultProxySection DefaultProxy
		{
			get
			{
				return (DefaultProxySection)base.Sections["defaultProxy"];
			}
		}

		/// <summary>Gets the configuration section containing the SMTP client e-mail settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.MailSettingsSectionGroup" /> object.</returns>
		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06003552 RID: 13650 RVA: 0x000C4F31 File Offset: 0x000C3131
		public MailSettingsSectionGroup MailSettings
		{
			get
			{
				return (MailSettingsSectionGroup)base.SectionGroups["mailSettings"];
			}
		}

		/// <summary>Gets the configuration section containing the cache configuration settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.RequestCachingSection" /> object.</returns>
		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06003553 RID: 13651 RVA: 0x000C4F48 File Offset: 0x000C3148
		[ConfigurationProperty("requestCaching")]
		public RequestCachingSection RequestCaching
		{
			get
			{
				return (RequestCachingSection)base.Sections["requestCaching"];
			}
		}

		/// <summary>Gets the configuration section containing the network settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SettingsSection" /> object.</returns>
		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06003554 RID: 13652 RVA: 0x000C4F5F File Offset: 0x000C315F
		[ConfigurationProperty("settings")]
		public SettingsSection Settings
		{
			get
			{
				return (SettingsSection)base.Sections["settings"];
			}
		}

		/// <summary>Gets the configuration section containing the modules registered for use with the <see cref="T:System.Net.WebRequest" /> class.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.WebRequestModulesSection" /> object.</returns>
		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06003555 RID: 13653 RVA: 0x000C4F76 File Offset: 0x000C3176
		[ConfigurationProperty("webRequestModules")]
		public WebRequestModulesSection WebRequestModules
		{
			get
			{
				return (WebRequestModulesSection)base.Sections["webRequestModules"];
			}
		}

		/// <summary>Gets the System.Net configuration section group from the specified configuration file.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.NetSectionGroup" /> that represents the System.Net settings in <paramref name="config" />.</returns>
		/// <param name="config">A <see cref="T:System.Configuration.Configuration" /> that represents a configuration file.</param>
		// Token: 0x06003556 RID: 13654 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO]
		public static NetSectionGroup GetSectionGroup(Configuration config)
		{
			throw new NotImplementedException();
		}
	}
}
