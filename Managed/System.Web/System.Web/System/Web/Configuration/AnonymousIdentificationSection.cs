using System;
using System.ComponentModel;
using System.Configuration;
using System.Web.Security;

namespace System.Web.Configuration
{
	/// <summary>Configures anonymous identification for users that are not authenticated. This class cannot be inherited.</summary>
	// Token: 0x0200057F RID: 1407
	public sealed class AnonymousIdentificationSection : ConfigurationSection
	{
		// Token: 0x06003B6A RID: 15210 RVA: 0x0009F198 File Offset: 0x0009D398
		static AnonymousIdentificationSection()
		{
			AnonymousIdentificationSection.properties.Add(AnonymousIdentificationSection.enabledProp);
			AnonymousIdentificationSection.properties.Add(AnonymousIdentificationSection.cookielessProp);
			AnonymousIdentificationSection.properties.Add(AnonymousIdentificationSection.cookieNameProp);
			AnonymousIdentificationSection.properties.Add(AnonymousIdentificationSection.cookieTimeoutProp);
			AnonymousIdentificationSection.properties.Add(AnonymousIdentificationSection.cookiePathProp);
			AnonymousIdentificationSection.properties.Add(AnonymousIdentificationSection.cookieRequireSSLProp);
			AnonymousIdentificationSection.properties.Add(AnonymousIdentificationSection.cookieSlidingExpirationProp);
			AnonymousIdentificationSection.properties.Add(AnonymousIdentificationSection.cookieProtectionProp);
			AnonymousIdentificationSection.properties.Add(AnonymousIdentificationSection.domainProp);
		}

		/// <summary>Gets or sets a value indicating whether to use cookies.</summary>
		/// <returns>One of the <see cref="T:System.Web.HttpCookieMode" /> values. The default value is <see cref="F:System.Web.HttpCookieMode.UseDeviceProfile" />. </returns>
		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x06003B6B RID: 15211 RVA: 0x0009F3AC File Offset: 0x0009D5AC
		// (set) Token: 0x06003B6C RID: 15212 RVA: 0x0009F3BE File Offset: 0x0009D5BE
		[ConfigurationProperty("cookieless", DefaultValue = "UseCookies")]
		public HttpCookieMode Cookieless
		{
			get
			{
				return (HttpCookieMode)base[AnonymousIdentificationSection.cookielessProp];
			}
			set
			{
				base[AnonymousIdentificationSection.cookielessProp] = value;
			}
		}

		/// <summary>Gets or sets the cookie name.</summary>
		/// <returns>The name of the cookie. The default value is ".ASPXANONYMOUS".</returns>
		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x06003B6D RID: 15213 RVA: 0x0009F3D1 File Offset: 0x0009D5D1
		// (set) Token: 0x06003B6E RID: 15214 RVA: 0x0009F3E3 File Offset: 0x0009D5E3
		[ConfigurationProperty("cookieName", DefaultValue = ".ASPXANONYMOUS")]
		[StringValidator(MinLength = 1)]
		public string CookieName
		{
			get
			{
				return (string)base[AnonymousIdentificationSection.cookieNameProp];
			}
			set
			{
				base[AnonymousIdentificationSection.cookieNameProp] = value;
			}
		}

		/// <summary>Gets or sets the path where the cookie is stored.</summary>
		/// <returns>The path of the HTTP cookie to use for the user's anonymous identification. The default value is a slash (/), which represents the Web application root.</returns>
		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x06003B6F RID: 15215 RVA: 0x0009F3F1 File Offset: 0x0009D5F1
		// (set) Token: 0x06003B70 RID: 15216 RVA: 0x0009F403 File Offset: 0x0009D603
		[ConfigurationProperty("cookiePath", DefaultValue = "/")]
		[StringValidator(MinLength = 1)]
		public string CookiePath
		{
			get
			{
				return (string)base[AnonymousIdentificationSection.cookiePathProp];
			}
			set
			{
				base[AnonymousIdentificationSection.cookiePathProp] = value;
			}
		}

		/// <summary>Gets or sets the encryption type used to encrypt the cookie.</summary>
		/// <returns>One of the <see cref="T:System.Web.Security.CookieProtection" /> values. The default value is <see cref="F:System.Web.Security.CookieProtection.All" />.</returns>
		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x06003B71 RID: 15217 RVA: 0x0009F411 File Offset: 0x0009D611
		// (set) Token: 0x06003B72 RID: 15218 RVA: 0x0009F423 File Offset: 0x0009D623
		[ConfigurationProperty("cookieProtection", DefaultValue = "Validation")]
		public CookieProtection CookieProtection
		{
			get
			{
				return (CookieProtection)base[AnonymousIdentificationSection.cookieProtectionProp];
			}
			set
			{
				base[AnonymousIdentificationSection.cookieProtectionProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a Secure Sockets Layer (SSL) connection is required when transmitting authentication information.</summary>
		/// <returns>true if an SSL connection is required; otherwise, false. The default is false.</returns>
		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x06003B73 RID: 15219 RVA: 0x0009F436 File Offset: 0x0009D636
		// (set) Token: 0x06003B74 RID: 15220 RVA: 0x0009F448 File Offset: 0x0009D648
		[ConfigurationProperty("cookieRequireSSL", DefaultValue = "False")]
		public bool CookieRequireSSL
		{
			get
			{
				return (bool)base[AnonymousIdentificationSection.cookieRequireSSLProp];
			}
			set
			{
				base[AnonymousIdentificationSection.cookieRequireSSLProp] = value;
			}
		}

		/// <summary>Gets or sets whether the cookie time-out value is reset on each request.</summary>
		/// <returns>true if the sliding expiration is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x06003B75 RID: 15221 RVA: 0x0009F45B File Offset: 0x0009D65B
		// (set) Token: 0x06003B76 RID: 15222 RVA: 0x0009F46D File Offset: 0x0009D66D
		[ConfigurationProperty("cookieSlidingExpiration", DefaultValue = "True")]
		public bool CookieSlidingExpiration
		{
			get
			{
				return (bool)base[AnonymousIdentificationSection.cookieSlidingExpirationProp];
			}
			set
			{
				base[AnonymousIdentificationSection.cookieSlidingExpirationProp] = value;
			}
		}

		/// <summary>Gets or sets the amount of time, in minutes, after which the authentication expires.</summary>
		/// <returns>The amount of time, in minutes, after which the authentication expires. The default value is 100000.</returns>
		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x06003B77 RID: 15223 RVA: 0x0009F480 File Offset: 0x0009D680
		// (set) Token: 0x06003B78 RID: 15224 RVA: 0x0009F492 File Offset: 0x0009D692
		[TypeConverter(typeof(TimeSpanMinutesOrInfiniteConverter))]
		[ConfigurationProperty("cookieTimeout", DefaultValue = "69.10:40:00")]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		public TimeSpan CookieTimeout
		{
			get
			{
				return (TimeSpan)base[AnonymousIdentificationSection.cookieTimeoutProp];
			}
			set
			{
				base[AnonymousIdentificationSection.cookieTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets the cookie domain.</summary>
		/// <returns>The name of the cookie domain. The default is an empty string ("").</returns>
		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x06003B79 RID: 15225 RVA: 0x0009F4A5 File Offset: 0x0009D6A5
		// (set) Token: 0x06003B7A RID: 15226 RVA: 0x0009F4B7 File Offset: 0x0009D6B7
		[ConfigurationProperty("domain")]
		public string Domain
		{
			get
			{
				return (string)base[AnonymousIdentificationSection.domainProp];
			}
			set
			{
				base[AnonymousIdentificationSection.domainProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether anonymous identification is enabled. </summary>
		/// <returns>true if anonymous identification is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x06003B7B RID: 15227 RVA: 0x0009F4C5 File Offset: 0x0009D6C5
		// (set) Token: 0x06003B7C RID: 15228 RVA: 0x0009F4D7 File Offset: 0x0009D6D7
		[ConfigurationProperty("enabled", DefaultValue = "False")]
		public bool Enabled
		{
			get
			{
				return (bool)base[AnonymousIdentificationSection.enabledProp];
			}
			set
			{
				base[AnonymousIdentificationSection.enabledProp] = value;
			}
		}

		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x06003B7D RID: 15229 RVA: 0x0009F4EA File Offset: 0x0009D6EA
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AnonymousIdentificationSection.properties;
			}
		}

		// Token: 0x0400207E RID: 8318
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400207F RID: 8319
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), false);

		// Token: 0x04002080 RID: 8320
		private static ConfigurationProperty cookielessProp = new ConfigurationProperty("cookieless", typeof(HttpCookieMode), HttpCookieMode.UseCookies, new GenericEnumConverter(typeof(HttpCookieMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002081 RID: 8321
		private static ConfigurationProperty cookieNameProp = new ConfigurationProperty("cookieName", typeof(string), ".ASPXANONYMOUS", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002082 RID: 8322
		private static ConfigurationProperty cookieTimeoutProp = new ConfigurationProperty("cookieTimeout", typeof(TimeSpan), new TimeSpan(69, 10, 40, 0), new TimeSpanMinutesOrInfiniteConverter(), PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002083 RID: 8323
		private static ConfigurationProperty cookiePathProp = new ConfigurationProperty("cookiePath", typeof(string), "/", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002084 RID: 8324
		private static ConfigurationProperty cookieRequireSSLProp = new ConfigurationProperty("cookieRequireSSL", typeof(bool), false);

		// Token: 0x04002085 RID: 8325
		private static ConfigurationProperty cookieSlidingExpirationProp = new ConfigurationProperty("cookieSlidingExpiration", typeof(bool), true);

		// Token: 0x04002086 RID: 8326
		private static ConfigurationProperty cookieProtectionProp = new ConfigurationProperty("cookieProtection", typeof(CookieProtection), CookieProtection.Validation, new GenericEnumConverter(typeof(CookieProtection)), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002087 RID: 8327
		private static ConfigurationProperty domainProp = new ConfigurationProperty("domain", typeof(string), null);
	}
}
