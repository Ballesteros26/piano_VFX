using System;
using System.ComponentModel;
using System.Configuration;
using System.Web.Security;

namespace System.Web.Configuration
{
	/// <summary>Defines configuration settings that are used to support the role management infrastructure of Web applications. This class cannot be inherited.</summary>
	// Token: 0x020005D2 RID: 1490
	public sealed class RoleManagerSection : ConfigurationSection
	{
		// Token: 0x0600403E RID: 16446 RVA: 0x000A95D4 File Offset: 0x000A77D4
		static RoleManagerSection()
		{
			RoleManagerSection.properties.Add(RoleManagerSection.cacheRolesInCookieProp);
			RoleManagerSection.properties.Add(RoleManagerSection.cookieNameProp);
			RoleManagerSection.properties.Add(RoleManagerSection.cookiePathProp);
			RoleManagerSection.properties.Add(RoleManagerSection.cookieProtectionProp);
			RoleManagerSection.properties.Add(RoleManagerSection.cookieRequireSSLProp);
			RoleManagerSection.properties.Add(RoleManagerSection.cookieSlidingExpirationProp);
			RoleManagerSection.properties.Add(RoleManagerSection.cookieTimeoutProp);
			RoleManagerSection.properties.Add(RoleManagerSection.createPersistentCookieProp);
			RoleManagerSection.properties.Add(RoleManagerSection.defaultProviderProp);
			RoleManagerSection.properties.Add(RoleManagerSection.domainProp);
			RoleManagerSection.properties.Add(RoleManagerSection.enabledProp);
			RoleManagerSection.properties.Add(RoleManagerSection.maxCachedResultsProp);
			RoleManagerSection.properties.Add(RoleManagerSection.providersProp);
		}

		/// <summary>Gets or sets a value indicating whether the current user's roles are cached in a cookie.</summary>
		/// <returns>true if the current user's roles are cached in a cookie; otherwise, false. The default is false.</returns>
		// Token: 0x17001445 RID: 5189
		// (get) Token: 0x0600403F RID: 16447 RVA: 0x000A9850 File Offset: 0x000A7A50
		// (set) Token: 0x06004040 RID: 16448 RVA: 0x000A9862 File Offset: 0x000A7A62
		[ConfigurationProperty("cacheRolesInCookie", DefaultValue = false)]
		public bool CacheRolesInCookie
		{
			get
			{
				return (bool)base[RoleManagerSection.cacheRolesInCookieProp];
			}
			set
			{
				base[RoleManagerSection.cacheRolesInCookieProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the cookie that is used to cache role names.</summary>
		/// <returns>The name of the cookie used to cache role names. The default is ".ASPXROLES".</returns>
		// Token: 0x17001446 RID: 5190
		// (get) Token: 0x06004041 RID: 16449 RVA: 0x000A9875 File Offset: 0x000A7A75
		// (set) Token: 0x06004042 RID: 16450 RVA: 0x000A9887 File Offset: 0x000A7A87
		[ConfigurationProperty("cookieName", DefaultValue = ".ASPXROLES")]
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[StringValidator(MinLength = 1)]
		public string CookieName
		{
			get
			{
				return (string)base[RoleManagerSection.cookieNameProp];
			}
			set
			{
				base[RoleManagerSection.cookieNameProp] = value;
			}
		}

		/// <summary>Gets or sets the virtual path of the cookie that is used to cache role names.</summary>
		/// <returns>The path of the cookie used to store role names. The default is "/".</returns>
		// Token: 0x17001447 RID: 5191
		// (get) Token: 0x06004043 RID: 16451 RVA: 0x000A9895 File Offset: 0x000A7A95
		// (set) Token: 0x06004044 RID: 16452 RVA: 0x000A98A7 File Offset: 0x000A7AA7
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("cookiePath", DefaultValue = "/")]
		public string CookiePath
		{
			get
			{
				return (string)base[RoleManagerSection.cookiePathProp];
			}
			set
			{
				base[RoleManagerSection.cookiePathProp] = value;
			}
		}

		/// <summary>Gets or sets the type of security that is used to protect the cookie that caches role names.</summary>
		/// <returns>The type of security protection used within the cookie where role names are cached. The default is All.</returns>
		// Token: 0x17001448 RID: 5192
		// (get) Token: 0x06004045 RID: 16453 RVA: 0x000A98B5 File Offset: 0x000A7AB5
		// (set) Token: 0x06004046 RID: 16454 RVA: 0x000A98C7 File Offset: 0x000A7AC7
		[ConfigurationProperty("cookieProtection", DefaultValue = "All")]
		public CookieProtection CookieProtection
		{
			get
			{
				return (CookieProtection)base[RoleManagerSection.cookieProtectionProp];
			}
			set
			{
				base[RoleManagerSection.cookieProtectionProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the cookie that is used to cache role names requires a Secure Sockets Layer (SSL) connection in order to be returned to the server.</summary>
		/// <returns>true if an SSL connection is needed in order to return to the server the cookie where role names are cached; otherwise, false. The default is false.</returns>
		// Token: 0x17001449 RID: 5193
		// (get) Token: 0x06004047 RID: 16455 RVA: 0x000A98DA File Offset: 0x000A7ADA
		// (set) Token: 0x06004048 RID: 16456 RVA: 0x000A98EC File Offset: 0x000A7AEC
		[ConfigurationProperty("cookieRequireSSL", DefaultValue = false)]
		public bool CookieRequireSSL
		{
			get
			{
				return (bool)base[RoleManagerSection.cookieRequireSSLProp];
			}
			set
			{
				base[RoleManagerSection.cookieRequireSSLProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the cookie that is used to cache role names will be reset periodically. </summary>
		/// <returns>true if the role names cookie expiration date and time will be reset periodically; otherwise, false. The default is true.</returns>
		// Token: 0x1700144A RID: 5194
		// (get) Token: 0x06004049 RID: 16457 RVA: 0x000A98FF File Offset: 0x000A7AFF
		// (set) Token: 0x0600404A RID: 16458 RVA: 0x000A9911 File Offset: 0x000A7B11
		[ConfigurationProperty("cookieSlidingExpiration", DefaultValue = true)]
		public bool CookieSlidingExpiration
		{
			get
			{
				return (bool)base[RoleManagerSection.cookieSlidingExpirationProp];
			}
			set
			{
				base[RoleManagerSection.cookieSlidingExpirationProp] = value;
			}
		}

		/// <summary>Gets or sets the number of minutes before the cookie that is used to cache role names expires.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that is the number of minutes before the cookie used to cache role names expires. The default is 30, in minutes.</returns>
		// Token: 0x1700144B RID: 5195
		// (get) Token: 0x0600404B RID: 16459 RVA: 0x000A9924 File Offset: 0x000A7B24
		// (set) Token: 0x0600404C RID: 16460 RVA: 0x000A9936 File Offset: 0x000A7B36
		[TypeConverter(typeof(TimeSpanMinutesOrInfiniteConverter))]
		[ConfigurationProperty("cookieTimeout", DefaultValue = "00:30:00")]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		public TimeSpan CookieTimeout
		{
			get
			{
				return (TimeSpan)base[RoleManagerSection.cookieTimeoutProp];
			}
			set
			{
				base[RoleManagerSection.cookieTimeoutProp] = value;
			}
		}

		/// <summary>Indicates whether a session-based cookie or a persistent cookie is used to cache role names. </summary>
		/// <returns>true to make the role names cookie persistent across browser sessions; otherwise, false. The default is false.</returns>
		// Token: 0x1700144C RID: 5196
		// (get) Token: 0x0600404D RID: 16461 RVA: 0x000A9949 File Offset: 0x000A7B49
		// (set) Token: 0x0600404E RID: 16462 RVA: 0x000A995B File Offset: 0x000A7B5B
		[ConfigurationProperty("createPersistentCookie", DefaultValue = false)]
		public bool CreatePersistentCookie
		{
			get
			{
				return (bool)base[RoleManagerSection.createPersistentCookieProp];
			}
			set
			{
				base[RoleManagerSection.createPersistentCookieProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the default provider that is used to manage roles. </summary>
		/// <returns>The name of a provider in the <see cref="P:System.Web.Configuration.RoleManagerSection.Providers" />. The default is "AspNetSqlRoleProvider".</returns>
		// Token: 0x1700144D RID: 5197
		// (get) Token: 0x0600404F RID: 16463 RVA: 0x000A996E File Offset: 0x000A7B6E
		// (set) Token: 0x06004050 RID: 16464 RVA: 0x000A9980 File Offset: 0x000A7B80
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetSqlRoleProvider")]
		public string DefaultProvider
		{
			get
			{
				return (string)base[RoleManagerSection.defaultProviderProp];
			}
			set
			{
				base[RoleManagerSection.defaultProviderProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the domain that is associated with the cookie that is used to cache role names. </summary>
		/// <returns>The <see cref="P:System.Web.HttpCookie.Domain" /> of the cookie used to cache role names. The default is an empty string ("").</returns>
		// Token: 0x1700144E RID: 5198
		// (get) Token: 0x06004051 RID: 16465 RVA: 0x000A998E File Offset: 0x000A7B8E
		// (set) Token: 0x06004052 RID: 16466 RVA: 0x000A99A0 File Offset: 0x000A7BA0
		[ConfigurationProperty("domain")]
		public string Domain
		{
			get
			{
				return (string)base[RoleManagerSection.domainProp];
			}
			set
			{
				base[RoleManagerSection.domainProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the ASP.NET role management feature is enabled. </summary>
		/// <returns>true if the ASP.NET role management feature is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x1700144F RID: 5199
		// (get) Token: 0x06004053 RID: 16467 RVA: 0x000A99AE File Offset: 0x000A7BAE
		// (set) Token: 0x06004054 RID: 16468 RVA: 0x000A99C0 File Offset: 0x000A7BC0
		[ConfigurationProperty("enabled", DefaultValue = false)]
		public bool Enabled
		{
			get
			{
				return (bool)base[RoleManagerSection.enabledProp];
			}
			set
			{
				base[RoleManagerSection.enabledProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum number of roles that ASP.NET caches in the role cookie. </summary>
		/// <returns>A value indicating the maximum number of roles ASP.NET caches in the role cookie. The default is 25.</returns>
		// Token: 0x17001450 RID: 5200
		// (get) Token: 0x06004055 RID: 16469 RVA: 0x000A99D3 File Offset: 0x000A7BD3
		// (set) Token: 0x06004056 RID: 16470 RVA: 0x000A99E5 File Offset: 0x000A7BE5
		[ConfigurationProperty("maxCachedResults", DefaultValue = 25)]
		public int MaxCachedResults
		{
			get
			{
				return (int)base[RoleManagerSection.maxCachedResultsProp];
			}
			set
			{
				base[RoleManagerSection.maxCachedResultsProp] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ProviderSettingsCollection" /> object of <see cref="T:System.Configuration.ProviderSettings" /> elements.</summary>
		/// <returns>A <see cref="T:System.Configuration.ProviderSettingsCollection" /> that contains the providers settings defined within the providers subsection of the roleManager section of the configuration file.</returns>
		// Token: 0x17001451 RID: 5201
		// (get) Token: 0x06004057 RID: 16471 RVA: 0x000A99F8 File Offset: 0x000A7BF8
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[RoleManagerSection.providersProp];
			}
		}

		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x06004058 RID: 16472 RVA: 0x000A9A0A File Offset: 0x000A7C0A
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return RoleManagerSection.properties;
			}
		}

		// Token: 0x040022DC RID: 8924
		private static ConfigurationProperty cacheRolesInCookieProp = new ConfigurationProperty("cacheRolesInCookie", typeof(bool), false);

		// Token: 0x040022DD RID: 8925
		private static ConfigurationProperty cookieNameProp = new ConfigurationProperty("cookieName", typeof(string), ".ASPXROLES");

		// Token: 0x040022DE RID: 8926
		private static ConfigurationProperty cookiePathProp = new ConfigurationProperty("cookiePath", typeof(string), "/");

		// Token: 0x040022DF RID: 8927
		private static ConfigurationProperty cookieProtectionProp = new ConfigurationProperty("cookieProtection", typeof(CookieProtection), CookieProtection.All);

		// Token: 0x040022E0 RID: 8928
		private static ConfigurationProperty cookieRequireSSLProp = new ConfigurationProperty("cookieRequireSSL", typeof(bool), false);

		// Token: 0x040022E1 RID: 8929
		private static ConfigurationProperty cookieSlidingExpirationProp = new ConfigurationProperty("cookieSlidingExpiration", typeof(bool), true);

		// Token: 0x040022E2 RID: 8930
		private static ConfigurationProperty cookieTimeoutProp = new ConfigurationProperty("cookieTimeout", typeof(TimeSpan), TimeSpan.FromMinutes(30.0), PropertyHelper.TimeSpanMinutesOrInfiniteConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022E3 RID: 8931
		private static ConfigurationProperty createPersistentCookieProp = new ConfigurationProperty("createPersistentCookie", typeof(bool), false);

		// Token: 0x040022E4 RID: 8932
		private static ConfigurationProperty defaultProviderProp = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetSqlRoleProvider");

		// Token: 0x040022E5 RID: 8933
		private static ConfigurationProperty domainProp = new ConfigurationProperty("domain", typeof(string), "");

		// Token: 0x040022E6 RID: 8934
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), false);

		// Token: 0x040022E7 RID: 8935
		private static ConfigurationProperty maxCachedResultsProp = new ConfigurationProperty("maxCachedResults", typeof(int), 25);

		// Token: 0x040022E8 RID: 8936
		private static ConfigurationProperty providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection));

		// Token: 0x040022E9 RID: 8937
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
