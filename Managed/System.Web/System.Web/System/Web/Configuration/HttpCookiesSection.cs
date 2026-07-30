using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures properties for cookies used by a Web application.</summary>
	// Token: 0x020005AB RID: 1451
	public sealed class HttpCookiesSection : ConfigurationSection
	{
		// Token: 0x06003E11 RID: 15889 RVA: 0x000A4864 File Offset: 0x000A2A64
		static HttpCookiesSection()
		{
			HttpCookiesSection.properties.Add(HttpCookiesSection.domainProp);
			HttpCookiesSection.properties.Add(HttpCookiesSection.httpOnlyCookiesProp);
			HttpCookiesSection.properties.Add(HttpCookiesSection.requireSSLProp);
		}

		/// <summary>Gets or sets the cookie domain name.</summary>
		/// <returns>The cookie domain name. </returns>
		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x06003E12 RID: 15890 RVA: 0x000A4904 File Offset: 0x000A2B04
		// (set) Token: 0x06003E13 RID: 15891 RVA: 0x000A4916 File Offset: 0x000A2B16
		[ConfigurationProperty("domain", DefaultValue = "")]
		public string Domain
		{
			get
			{
				return (string)base[HttpCookiesSection.domainProp];
			}
			set
			{
				base[HttpCookiesSection.domainProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the support for the browser's HttpOnly cookie is enabled.</summary>
		/// <returns>true if support for the HttpOnly cookie is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x06003E14 RID: 15892 RVA: 0x000A4924 File Offset: 0x000A2B24
		// (set) Token: 0x06003E15 RID: 15893 RVA: 0x000A4936 File Offset: 0x000A2B36
		[ConfigurationProperty("httpOnlyCookies", DefaultValue = "False")]
		public bool HttpOnlyCookies
		{
			get
			{
				return (bool)base[HttpCookiesSection.httpOnlyCookiesProp];
			}
			set
			{
				base[HttpCookiesSection.httpOnlyCookiesProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether Secure Sockets Layer (SSL) communication is required.</summary>
		/// <returns>true if SSL is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x17001375 RID: 4981
		// (get) Token: 0x06003E16 RID: 15894 RVA: 0x000A4949 File Offset: 0x000A2B49
		// (set) Token: 0x06003E17 RID: 15895 RVA: 0x000A495B File Offset: 0x000A2B5B
		[ConfigurationProperty("requireSSL", DefaultValue = "False")]
		public bool RequireSSL
		{
			get
			{
				return (bool)base[HttpCookiesSection.requireSSLProp];
			}
			set
			{
				base[HttpCookiesSection.requireSSLProp] = value;
			}
		}

		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x06003E18 RID: 15896 RVA: 0x000A496E File Offset: 0x000A2B6E
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpCookiesSection.properties;
			}
		}

		// Token: 0x04002205 RID: 8709
		private static ConfigurationProperty domainProp = new ConfigurationProperty("domain", typeof(string), "");

		// Token: 0x04002206 RID: 8710
		private static ConfigurationProperty httpOnlyCookiesProp = new ConfigurationProperty("httpOnlyCookies", typeof(bool), false);

		// Token: 0x04002207 RID: 8711
		private static ConfigurationProperty requireSSLProp = new ConfigurationProperty("requireSSL", typeof(bool), false);

		// Token: 0x04002208 RID: 8712
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
