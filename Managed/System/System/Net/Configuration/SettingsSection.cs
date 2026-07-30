using System;
using System.Configuration;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for sockets, IPv6, response headers, and service points. This class cannot be inherited.</summary>
	// Token: 0x020006AE RID: 1710
	public sealed class SettingsSection : ConfigurationSection
	{
		// Token: 0x0600358A RID: 13706 RVA: 0x000C55C0 File Offset: 0x000C37C0
		static SettingsSection()
		{
			SettingsSection.properties.Add(SettingsSection.httpWebRequestProp);
			SettingsSection.properties.Add(SettingsSection.ipv6Prop);
			SettingsSection.properties.Add(SettingsSection.performanceCountersProp);
			SettingsSection.properties.Add(SettingsSection.servicePointManagerProp);
			SettingsSection.properties.Add(SettingsSection.socketProp);
			SettingsSection.properties.Add(SettingsSection.webProxyScriptProp);
		}

		/// <summary>Gets the configuration element that controls the settings used by an <see cref="T:System.Net.HttpWebRequest" /> object.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.HttpWebRequestElement" /> object.The configuration element that controls the maximum response header length and other settings used by an <see cref="T:System.Net.HttpWebRequest" /> object.</returns>
		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x0600358C RID: 13708 RVA: 0x000C56C7 File Offset: 0x000C38C7
		[ConfigurationProperty("httpWebRequest")]
		public HttpWebRequestElement HttpWebRequest
		{
			get
			{
				return (HttpWebRequestElement)base[SettingsSection.httpWebRequestProp];
			}
		}

		/// <summary>Gets the configuration element that enables Internet Protocol version 6 (IPv6).</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.Ipv6Element" />.The configuration element that controls setting used by IPv6.</returns>
		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x0600358D RID: 13709 RVA: 0x000C56D9 File Offset: 0x000C38D9
		[ConfigurationProperty("ipv6")]
		public Ipv6Element Ipv6
		{
			get
			{
				return (Ipv6Element)base[SettingsSection.ipv6Prop];
			}
		}

		/// <summary>Gets the configuration element that controls whether network performance counters are enabled.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.PerformanceCountersElement" />.The configuration element that controls setting used network performance counters.</returns>
		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x0600358E RID: 13710 RVA: 0x000C56EB File Offset: 0x000C38EB
		[ConfigurationProperty("performanceCounters")]
		public PerformanceCountersElement PerformanceCounters
		{
			get
			{
				return (PerformanceCountersElement)base[SettingsSection.performanceCountersProp];
			}
		}

		/// <summary>Gets the configuration element that controls settings for connections to remote host computers.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ServicePointManagerElement" /> object.The configuration element that that controls setting used network performance counters for connections to remote host computers.</returns>
		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x0600358F RID: 13711 RVA: 0x000C56FD File Offset: 0x000C38FD
		[ConfigurationProperty("servicePointManager")]
		public ServicePointManagerElement ServicePointManager
		{
			get
			{
				return (ServicePointManagerElement)base[SettingsSection.servicePointManagerProp];
			}
		}

		/// <summary>Gets the configuration element that controls settings for sockets.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SocketElement" /> object.The configuration element that controls settings for sockets.</returns>
		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06003590 RID: 13712 RVA: 0x000C570F File Offset: 0x000C390F
		[ConfigurationProperty("socket")]
		public SocketElement Socket
		{
			get
			{
				return (SocketElement)base[SettingsSection.socketProp];
			}
		}

		/// <summary>Gets the configuration element that controls the execution timeout and download timeout of Web proxy scripts.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.WebProxyScriptElement" /> object.The configuration element that controls settings for the execution timeout and download timeout used by the Web proxy scripts.</returns>
		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06003591 RID: 13713 RVA: 0x000C5721 File Offset: 0x000C3921
		[ConfigurationProperty("webProxyScript")]
		public WebProxyScriptElement WebProxyScript
		{
			get
			{
				return (WebProxyScriptElement)base[SettingsSection.webProxyScriptProp];
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06003592 RID: 13714 RVA: 0x000C5733 File Offset: 0x000C3933
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SettingsSection.properties;
			}
		}

		/// <summary>Gets the configuration element that controls the settings used by an <see cref="T:System.Net.HttpListener" /> object.</summary>
		/// <returns>An <see cref="T:System.Net.Configuration.HttpListenerElement" /> object.The configuration element that controls the settings used by an <see cref="T:System.Net.HttpListener" /> object.</returns>
		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06003593 RID: 13715 RVA: 0x0003D2D0 File Offset: 0x0003B4D0
		public HttpListenerElement HttpListener
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the configuration element that controls the settings used by an <see cref="T:System.Net.WebUtility" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Net.Configuration.WebUtilityElement" />.The configuration element that controls the settings used by an <see cref="T:System.Net.WebUtility" /> object.</returns>
		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06003594 RID: 13716 RVA: 0x0003D2D0 File Offset: 0x0003B4D0
		public WebUtilityElement WebUtility
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x04002A97 RID: 10903
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002A98 RID: 10904
		private static ConfigurationProperty httpWebRequestProp = new ConfigurationProperty("httpWebRequest", typeof(HttpWebRequestElement));

		// Token: 0x04002A99 RID: 10905
		private static ConfigurationProperty ipv6Prop = new ConfigurationProperty("ipv6", typeof(Ipv6Element));

		// Token: 0x04002A9A RID: 10906
		private static ConfigurationProperty performanceCountersProp = new ConfigurationProperty("performanceCounters", typeof(PerformanceCountersElement));

		// Token: 0x04002A9B RID: 10907
		private static ConfigurationProperty servicePointManagerProp = new ConfigurationProperty("servicePointManager", typeof(ServicePointManagerElement));

		// Token: 0x04002A9C RID: 10908
		private static ConfigurationProperty webProxyScriptProp = new ConfigurationProperty("webProxyScript", typeof(WebProxyScriptElement));

		// Token: 0x04002A9D RID: 10909
		private static ConfigurationProperty socketProp = new ConfigurationProperty("socket", typeof(SocketElement));
	}
}
