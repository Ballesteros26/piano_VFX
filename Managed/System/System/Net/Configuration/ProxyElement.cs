using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Identifies the configuration settings for Web proxy server. This class cannot be inherited.</summary>
	// Token: 0x020006A8 RID: 1704
	public sealed class ProxyElement : ConfigurationElement
	{
		// Token: 0x0600355C RID: 13660 RVA: 0x000C4FF4 File Offset: 0x000C31F4
		static ProxyElement()
		{
			ProxyElement.properties.Add(ProxyElement.autoDetectProp);
			ProxyElement.properties.Add(ProxyElement.bypassOnLocalProp);
			ProxyElement.properties.Add(ProxyElement.proxyAddressProp);
			ProxyElement.properties.Add(ProxyElement.scriptLocationProp);
			ProxyElement.properties.Add(ProxyElement.useSystemDefaultProp);
		}

		/// <summary>Gets or sets an <see cref="T:System.Net.Configuration.ProxyElement.AutoDetectValues" /> value that controls whether the Web proxy is automatically detected.</summary>
		/// <returns>
		///   <see cref="F:System.Net.Configuration.ProxyElement.AutoDetectValues.True" /> if the <see cref="T:System.Net.WebProxy" /> is automatically detected; <see cref="F:System.Net.Configuration.ProxyElement.AutoDetectValues.False" /> if the <see cref="T:System.Net.WebProxy" /> is not automatically detected; or <see cref="F:System.Net.Configuration.ProxyElement.AutoDetectValues.Unspecified" />.</returns>
		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x0600355E RID: 13662 RVA: 0x000C50E7 File Offset: 0x000C32E7
		// (set) Token: 0x0600355F RID: 13663 RVA: 0x000C50F9 File Offset: 0x000C32F9
		[ConfigurationProperty("autoDetect", DefaultValue = "Unspecified")]
		public ProxyElement.AutoDetectValues AutoDetect
		{
			get
			{
				return (ProxyElement.AutoDetectValues)base[ProxyElement.autoDetectProp];
			}
			set
			{
				base[ProxyElement.autoDetectProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether local resources are retrieved by using a Web proxy server.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ProxyElement.BypassOnLocalValues" />.Avalue that indicates whether local resources are retrieved by using a Web proxy server.</returns>
		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06003560 RID: 13664 RVA: 0x000C510C File Offset: 0x000C330C
		// (set) Token: 0x06003561 RID: 13665 RVA: 0x000C511E File Offset: 0x000C331E
		[ConfigurationProperty("bypassonlocal", DefaultValue = "Unspecified")]
		public ProxyElement.BypassOnLocalValues BypassOnLocal
		{
			get
			{
				return (ProxyElement.BypassOnLocalValues)base[ProxyElement.bypassOnLocalProp];
			}
			set
			{
				base[ProxyElement.bypassOnLocalProp] = value;
			}
		}

		/// <summary>Gets or sets the URI that identifies the Web proxy server to use.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a URI.</returns>
		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06003562 RID: 13666 RVA: 0x000C5131 File Offset: 0x000C3331
		// (set) Token: 0x06003563 RID: 13667 RVA: 0x000C5143 File Offset: 0x000C3343
		[ConfigurationProperty("proxyaddress")]
		public Uri ProxyAddress
		{
			get
			{
				return (Uri)base[ProxyElement.proxyAddressProp];
			}
			set
			{
				base[ProxyElement.proxyAddressProp] = value;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Uri" /> value that specifies the location of the automatic proxy detection script.</summary>
		/// <returns>A <see cref="T:System.Uri" /> specifying the location of the automatic proxy detection script.</returns>
		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06003564 RID: 13668 RVA: 0x000C5151 File Offset: 0x000C3351
		// (set) Token: 0x06003565 RID: 13669 RVA: 0x000C5163 File Offset: 0x000C3363
		[ConfigurationProperty("scriptLocation")]
		public Uri ScriptLocation
		{
			get
			{
				return (Uri)base[ProxyElement.scriptLocationProp];
			}
			set
			{
				base[ProxyElement.scriptLocationProp] = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether the Internet Explorer Web proxy settings are used.</summary>
		/// <returns>true if the Internet Explorer LAN settings are used to detect and configure the default <see cref="T:System.Net.WebProxy" /> used for requests; otherwise, false.</returns>
		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06003566 RID: 13670 RVA: 0x000C5171 File Offset: 0x000C3371
		// (set) Token: 0x06003567 RID: 13671 RVA: 0x000C5183 File Offset: 0x000C3383
		[ConfigurationProperty("usesystemdefault", DefaultValue = "Unspecified")]
		public ProxyElement.UseSystemDefaultValues UseSystemDefault
		{
			get
			{
				return (ProxyElement.UseSystemDefaultValues)base[ProxyElement.useSystemDefaultProp];
			}
			set
			{
				base[ProxyElement.useSystemDefaultProp] = value;
			}
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06003568 RID: 13672 RVA: 0x000C5196 File Offset: 0x000C3396
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProxyElement.properties;
			}
		}

		// Token: 0x04002A77 RID: 10871
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002A78 RID: 10872
		private static ConfigurationProperty autoDetectProp = new ConfigurationProperty("autoDetect", typeof(ProxyElement.AutoDetectValues), ProxyElement.AutoDetectValues.Unspecified);

		// Token: 0x04002A79 RID: 10873
		private static ConfigurationProperty bypassOnLocalProp = new ConfigurationProperty("bypassonlocal", typeof(ProxyElement.BypassOnLocalValues), ProxyElement.BypassOnLocalValues.Unspecified);

		// Token: 0x04002A7A RID: 10874
		private static ConfigurationProperty proxyAddressProp = new ConfigurationProperty("proxyaddress", typeof(Uri), null);

		// Token: 0x04002A7B RID: 10875
		private static ConfigurationProperty scriptLocationProp = new ConfigurationProperty("scriptLocation", typeof(Uri), null);

		// Token: 0x04002A7C RID: 10876
		private static ConfigurationProperty useSystemDefaultProp = new ConfigurationProperty("usesystemdefault", typeof(ProxyElement.UseSystemDefaultValues), ProxyElement.UseSystemDefaultValues.Unspecified);

		/// <summary>Specifies whether the proxy is bypassed for local resources.</summary>
		// Token: 0x020006A9 RID: 1705
		public enum BypassOnLocalValues
		{
			/// <summary>Unspecified.</summary>
			// Token: 0x04002A7E RID: 10878
			Unspecified = -1,
			/// <summary>Access local resources directly.</summary>
			// Token: 0x04002A7F RID: 10879
			True = 1,
			/// <summary>All requests for local resources should go through the proxy</summary>
			// Token: 0x04002A80 RID: 10880
			False = 0
		}

		/// <summary>Specifies whether to use the local system proxy settings to determine whether the proxy is bypassed for local resources.</summary>
		// Token: 0x020006AA RID: 1706
		public enum UseSystemDefaultValues
		{
			/// <summary>The system default proxy setting is unspecified.</summary>
			// Token: 0x04002A82 RID: 10882
			Unspecified = -1,
			/// <summary>Use system default proxy setting values.</summary>
			// Token: 0x04002A83 RID: 10883
			True = 1,
			/// <summary>Do not use system default proxy setting values</summary>
			// Token: 0x04002A84 RID: 10884
			False = 0
		}

		/// <summary>Specifies whether the proxy is automatically detected.</summary>
		// Token: 0x020006AB RID: 1707
		public enum AutoDetectValues
		{
			/// <summary>Unspecified.</summary>
			// Token: 0x04002A86 RID: 10886
			Unspecified = -1,
			/// <summary>The proxy is automatically detected.</summary>
			// Token: 0x04002A87 RID: 10887
			True = 1,
			/// <summary>The proxy is not automatically detected.</summary>
			// Token: 0x04002A88 RID: 10888
			False = 0
		}
	}
}
