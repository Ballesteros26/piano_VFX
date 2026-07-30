using System;
using System.ComponentModel;
using System.Configuration;
using System.Web.SessionState;

namespace System.Web.Configuration
{
	/// <summary>Configures the session state for a Web application.</summary>
	// Token: 0x020005D8 RID: 1496
	public sealed class SessionStateSection : ConfigurationSection
	{
		// Token: 0x06004095 RID: 16533 RVA: 0x000AA034 File Offset: 0x000A8234
		static SessionStateSection()
		{
			SessionStateSection.properties.Add(SessionStateSection.allowCustomSqlDatabaseProp);
			SessionStateSection.properties.Add(SessionStateSection.cookielessProp);
			SessionStateSection.properties.Add(SessionStateSection.cookieNameProp);
			SessionStateSection.properties.Add(SessionStateSection.customProviderProp);
			SessionStateSection.properties.Add(SessionStateSection.modeProp);
			SessionStateSection.properties.Add(SessionStateSection.partitionResolverTypeProp);
			SessionStateSection.properties.Add(SessionStateSection.providersProp);
			SessionStateSection.properties.Add(SessionStateSection.regenerateExpiredSessionIdProp);
			SessionStateSection.properties.Add(SessionStateSection.sessionIDManagerTypeProp);
			SessionStateSection.properties.Add(SessionStateSection.sqlCommandTimeoutProp);
			SessionStateSection.properties.Add(SessionStateSection.sqlConnectionStringProp);
			SessionStateSection.properties.Add(SessionStateSection.stateConnectionStringProp);
			SessionStateSection.properties.Add(SessionStateSection.stateNetworkTimeoutProp);
			SessionStateSection.properties.Add(SessionStateSection.timeoutProp);
			SessionStateSection.properties.Add(SessionStateSection.useHostingIdentityProp);
			SessionStateSection.properties.Add(SessionStateSection.compressionEnabledProp);
			SessionStateSection.properties.Add(SessionStateSection.sqlConnectionRetryIntervalProp);
			SessionStateSection.elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(SessionStateSection), new ValidatorCallback(SessionStateSection.ValidateElement)));
		}

		// Token: 0x06004096 RID: 16534 RVA: 0x0009FE7D File Offset: 0x0009E07D
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
		}

		/// <summary>Gets or sets a value indicating whether the user can specify the initial catalog value in the <see cref="P:System.Web.Configuration.SessionStateSection.SqlConnectionString" /> property.</summary>
		/// <returns>true if the user is allowed to specify the catalog; otherwise, false. The default value is false.</returns>
		// Token: 0x17001467 RID: 5223
		// (get) Token: 0x06004097 RID: 16535 RVA: 0x000AA3F5 File Offset: 0x000A85F5
		// (set) Token: 0x06004098 RID: 16536 RVA: 0x000AA407 File Offset: 0x000A8607
		[ConfigurationProperty("allowCustomSqlDatabase", DefaultValue = "False")]
		public bool AllowCustomSqlDatabase
		{
			get
			{
				return (bool)base[SessionStateSection.allowCustomSqlDatabaseProp];
			}
			set
			{
				base[SessionStateSection.allowCustomSqlDatabaseProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether cookies are used to identify client sessions.</summary>
		/// <returns>true if all requests are treated as cookieless, or false if no requests are treated as cookieless, or one of the <see cref="T:System.Web.HttpCookieMode" /> values. The default value in ASP.NET version 2.0 is <see cref="F:System.Web.HttpCookieMode.AutoDetect" />. In earlier versions, the default value was false.</returns>
		// Token: 0x17001468 RID: 5224
		// (get) Token: 0x06004099 RID: 16537 RVA: 0x000AA41A File Offset: 0x000A861A
		// (set) Token: 0x0600409A RID: 16538 RVA: 0x000AA432 File Offset: 0x000A8632
		[ConfigurationProperty("cookieless")]
		public HttpCookieMode Cookieless
		{
			get
			{
				return this.ParseCookieMode((string)base[SessionStateSection.cookielessProp]);
			}
			set
			{
				base[SessionStateSection.cookielessProp] = value.ToString();
			}
		}

		/// <summary>Gets or sets the cookie name.</summary>
		/// <returns>The name of the HTTP cookie to use for session identification.</returns>
		// Token: 0x17001469 RID: 5225
		// (get) Token: 0x0600409B RID: 16539 RVA: 0x000AA44C File Offset: 0x000A864C
		// (set) Token: 0x0600409C RID: 16540 RVA: 0x000AA45E File Offset: 0x000A865E
		[ConfigurationProperty("cookieName", DefaultValue = "ASP.NET_SessionId")]
		public string CookieName
		{
			get
			{
				return (string)base[SessionStateSection.cookieNameProp];
			}
			set
			{
				base[SessionStateSection.cookieNameProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the custom provider from the <see cref="P:System.Web.Configuration.SessionStateSection.Providers" /> collection.</summary>
		/// <returns>The custom provider name.</returns>
		// Token: 0x1700146A RID: 5226
		// (get) Token: 0x0600409D RID: 16541 RVA: 0x000AA46C File Offset: 0x000A866C
		// (set) Token: 0x0600409E RID: 16542 RVA: 0x000AA47E File Offset: 0x000A867E
		[ConfigurationProperty("customProvider", DefaultValue = "")]
		public string CustomProvider
		{
			get
			{
				return (string)base[SessionStateSection.customProviderProp];
			}
			set
			{
				base[SessionStateSection.customProviderProp] = value;
			}
		}

		/// <summary>Gets or sets a value specifying where to store the session state.</summary>
		/// <returns>One of the <see cref="T:System.Web.SessionState.SessionStateMode" /> values. The default value is <see cref="F:System.Web.SessionState.SessionStateMode.InProc" />.</returns>
		// Token: 0x1700146B RID: 5227
		// (get) Token: 0x0600409F RID: 16543 RVA: 0x000AA48C File Offset: 0x000A868C
		// (set) Token: 0x060040A0 RID: 16544 RVA: 0x000AA49E File Offset: 0x000A869E
		[ConfigurationProperty("mode", DefaultValue = "InProc")]
		public SessionStateMode Mode
		{
			get
			{
				return (SessionStateMode)base[SessionStateSection.modeProp];
			}
			set
			{
				base[SessionStateSection.modeProp] = value;
			}
		}

		/// <summary>Gets or sets a value specifying where to store the session state.</summary>
		/// <returns>A value specifying where to store the session state, or an empty string ("").</returns>
		// Token: 0x1700146C RID: 5228
		// (get) Token: 0x060040A1 RID: 16545 RVA: 0x000AA4B1 File Offset: 0x000A86B1
		// (set) Token: 0x060040A2 RID: 16546 RVA: 0x000AA4C3 File Offset: 0x000A86C3
		[ConfigurationProperty("partitionResolverType", DefaultValue = "")]
		public string PartitionResolverType
		{
			get
			{
				return (string)base[SessionStateSection.partitionResolverTypeProp];
			}
			set
			{
				base[SessionStateSection.partitionResolverTypeProp] = value;
			}
		}

		/// <summary>Gets the current <see cref="T:System.Configuration.ProviderSettingsCollection" /> providers.</summary>
		/// <returns>The collection containing the <see cref="T:System.Web.Configuration.SessionStateSection" /> providers.</returns>
		// Token: 0x1700146D RID: 5229
		// (get) Token: 0x060040A3 RID: 16547 RVA: 0x000AA4D1 File Offset: 0x000A86D1
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[SessionStateSection.providersProp];
			}
		}

		/// <summary>Gets or sets a value indicating whether the session Id will be re-issued when an expired session ID is specified by the client.</summary>
		/// <returns>true if the session ID must be regenerated; otherwise, false. The default value is true.</returns>
		// Token: 0x1700146E RID: 5230
		// (get) Token: 0x060040A4 RID: 16548 RVA: 0x000AA4E3 File Offset: 0x000A86E3
		// (set) Token: 0x060040A5 RID: 16549 RVA: 0x000AA4F5 File Offset: 0x000A86F5
		[ConfigurationProperty("regenerateExpiredSessionId", DefaultValue = "True")]
		public bool RegenerateExpiredSessionId
		{
			get
			{
				return (bool)base[SessionStateSection.regenerateExpiredSessionIdProp];
			}
			set
			{
				base[SessionStateSection.regenerateExpiredSessionIdProp] = value;
			}
		}

		/// <summary>Gets or sets a value specifying the fully qualified type of session ID Manager.</summary>
		/// <returns>A fully qualified type of session ID Manager.</returns>
		// Token: 0x1700146F RID: 5231
		// (get) Token: 0x060040A6 RID: 16550 RVA: 0x000AA508 File Offset: 0x000A8708
		// (set) Token: 0x060040A7 RID: 16551 RVA: 0x000AA51A File Offset: 0x000A871A
		[ConfigurationProperty("sessionIDManagerType", DefaultValue = "")]
		public string SessionIDManagerType
		{
			get
			{
				return (string)base[SessionStateSection.sessionIDManagerTypeProp];
			}
			set
			{
				base[SessionStateSection.sessionIDManagerTypeProp] = value;
			}
		}

		/// <summary>Gets or sets the duration time-out for the SQL commands using the SQL Server session state mode.</summary>
		/// <returns>The amount of time, in seconds, after which a SQL command will time out. The default is 30 seconds.</returns>
		// Token: 0x17001470 RID: 5232
		// (get) Token: 0x060040A8 RID: 16552 RVA: 0x000AA528 File Offset: 0x000A8728
		// (set) Token: 0x060040A9 RID: 16553 RVA: 0x000AA53A File Offset: 0x000A873A
		[ConfigurationProperty("sqlCommandTimeout", DefaultValue = "00:00:30")]
		[TypeConverter(typeof(TimeSpanSecondsOrInfiniteConverter))]
		public TimeSpan SqlCommandTimeout
		{
			get
			{
				return (TimeSpan)base[SessionStateSection.sqlCommandTimeoutProp];
			}
			set
			{
				base[SessionStateSection.sqlCommandTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets the SQL connection string.</summary>
		/// <returns>The SQL connection string. Its default value is the generic string: "data source=127.0.0.1;Integrated Security=SSPI"</returns>
		// Token: 0x17001471 RID: 5233
		// (get) Token: 0x060040AA RID: 16554 RVA: 0x000AA54D File Offset: 0x000A874D
		// (set) Token: 0x060040AB RID: 16555 RVA: 0x000AA55F File Offset: 0x000A875F
		[ConfigurationProperty("sqlConnectionString", DefaultValue = "data source=localhost;Integrated Security=SSPI")]
		public string SqlConnectionString
		{
			get
			{
				return (string)base[SessionStateSection.sqlConnectionStringProp];
			}
			set
			{
				base[SessionStateSection.sqlConnectionStringProp] = value;
			}
		}

		/// <summary>Gets or sets the state server connection string.</summary>
		/// <returns>The state server connection string.</returns>
		// Token: 0x17001472 RID: 5234
		// (get) Token: 0x060040AC RID: 16556 RVA: 0x000AA56D File Offset: 0x000A876D
		// (set) Token: 0x060040AD RID: 16557 RVA: 0x000AA57F File Offset: 0x000A877F
		[ConfigurationProperty("stateConnectionString", DefaultValue = "tcpip=loopback:42424")]
		public string StateConnectionString
		{
			get
			{
				return (string)base[SessionStateSection.stateConnectionStringProp];
			}
			set
			{
				base[SessionStateSection.stateConnectionStringProp] = value;
			}
		}

		/// <summary>Gets or sets the amount of time the network connection between the Web server and the state server can remain idle. </summary>
		/// <returns>The time, in seconds, that the network connection between the Web server and the state server can remain idle before the session is abandoned. The default value is 10 seconds.</returns>
		// Token: 0x17001473 RID: 5235
		// (get) Token: 0x060040AE RID: 16558 RVA: 0x000AA58D File Offset: 0x000A878D
		// (set) Token: 0x060040AF RID: 16559 RVA: 0x000AA59F File Offset: 0x000A879F
		[ConfigurationProperty("stateNetworkTimeout", DefaultValue = "00:00:10")]
		[TypeConverter(typeof(TimeSpanSecondsOrInfiniteConverter))]
		public TimeSpan StateNetworkTimeout
		{
			get
			{
				return (TimeSpan)base[SessionStateSection.stateNetworkTimeoutProp];
			}
			set
			{
				base[SessionStateSection.stateNetworkTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets the session time-out</summary>
		/// <returns>The session time-out, in minutes. The default value is 20 minutes.</returns>
		// Token: 0x17001474 RID: 5236
		// (get) Token: 0x060040B0 RID: 16560 RVA: 0x000AA5B2 File Offset: 0x000A87B2
		// (set) Token: 0x060040B1 RID: 16561 RVA: 0x000AA5C4 File Offset: 0x000A87C4
		[TypeConverter(typeof(TimeSpanMinutesOrInfiniteConverter))]
		[TimeSpanValidator(MinValueString = "00:01:00", MaxValueString = "10675199.02:48:05.4775807")]
		[ConfigurationProperty("timeout", DefaultValue = "00:20:00")]
		public TimeSpan Timeout
		{
			get
			{
				return (TimeSpan)base[SessionStateSection.timeoutProp];
			}
			set
			{
				base[SessionStateSection.timeoutProp] = value;
			}
		}

		/// <summary>Gets or sets a value specifying the whether the session state will use client impersonation when available, or will always revert to the hosting identity.</summary>
		/// <returns>true if Web application should revert to hosting identity; otherwise, false. The default value is true.</returns>
		// Token: 0x17001475 RID: 5237
		// (get) Token: 0x060040B2 RID: 16562 RVA: 0x000AA5D7 File Offset: 0x000A87D7
		// (set) Token: 0x060040B3 RID: 16563 RVA: 0x000AA5E9 File Offset: 0x000A87E9
		[ConfigurationProperty("useHostingIdentity", DefaultValue = "True")]
		public bool UseHostingIdentity
		{
			get
			{
				return (bool)base[SessionStateSection.useHostingIdentityProp];
			}
			set
			{
				base[SessionStateSection.useHostingIdentityProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether compression is enabled for session-state data.</summary>
		/// <returns>true if compression is enabled; otherwise false. The default is false. </returns>
		// Token: 0x17001476 RID: 5238
		// (get) Token: 0x060040B4 RID: 16564 RVA: 0x000AA5FC File Offset: 0x000A87FC
		// (set) Token: 0x060040B5 RID: 16565 RVA: 0x000AA60E File Offset: 0x000A880E
		[ConfigurationProperty("compressionEnabled", DefaultValue = false)]
		public bool CompressionEnabled
		{
			get
			{
				return (bool)base[SessionStateSection.compressionEnabledProp];
			}
			set
			{
				base[SessionStateSection.compressionEnabledProp] = value;
			}
		}

		/// <summary>Gets or sets the time interval that should elapse before ASP.NET reconnects to the database.</summary>
		/// <returns>The time interval that should elapse before ASP.NET reconnects to the database.</returns>
		// Token: 0x17001477 RID: 5239
		// (get) Token: 0x060040B6 RID: 16566 RVA: 0x000AA621 File Offset: 0x000A8821
		// (set) Token: 0x060040B7 RID: 16567 RVA: 0x000AA633 File Offset: 0x000A8833
		[TypeConverter(typeof(TimeSpanSecondsOrInfiniteConverter))]
		[ConfigurationProperty("sqlConnectionRetryInterval", DefaultValue = "00:00:00")]
		public TimeSpan SqlConnectionRetryInterval
		{
			get
			{
				return (TimeSpan)base[SessionStateSection.sqlConnectionRetryIntervalProp];
			}
			set
			{
				base[SessionStateSection.sqlConnectionRetryIntervalProp] = value;
			}
		}

		// Token: 0x060040B8 RID: 16568 RVA: 0x0000393A File Offset: 0x00001B3A
		private static void ValidateElement(object o)
		{
		}

		// Token: 0x17001478 RID: 5240
		// (get) Token: 0x060040B9 RID: 16569 RVA: 0x000AA646 File Offset: 0x000A8846
		protected internal override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return SessionStateSection.elementProperty;
			}
		}

		// Token: 0x17001479 RID: 5241
		// (get) Token: 0x060040BA RID: 16570 RVA: 0x000AA64D File Offset: 0x000A884D
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SessionStateSection.properties;
			}
		}

		// Token: 0x060040BB RID: 16571 RVA: 0x000AA654 File Offset: 0x000A8854
		private HttpCookieMode ParseCookieMode(string s)
		{
			if (s == "true")
			{
				return HttpCookieMode.UseUri;
			}
			if (s == "false" || s == null)
			{
				return HttpCookieMode.UseCookies;
			}
			HttpCookieMode httpCookieMode;
			try
			{
				httpCookieMode = (HttpCookieMode)Enum.Parse(typeof(HttpCookieMode), s);
			}
			catch
			{
				httpCookieMode = HttpCookieMode.UseCookies;
			}
			return httpCookieMode;
		}

		// Token: 0x1700147A RID: 5242
		// (get) Token: 0x060040BC RID: 16572 RVA: 0x000AA6B4 File Offset: 0x000A88B4
		// (set) Token: 0x060040BD RID: 16573 RVA: 0x000AA6C2 File Offset: 0x000A88C2
		internal bool CookieLess
		{
			get
			{
				return this.Cookieless != HttpCookieMode.UseCookies;
			}
			set
			{
				this.Cookieless = (value ? HttpCookieMode.UseUri : HttpCookieMode.UseCookies);
			}
		}

		// Token: 0x040022FB RID: 8955
		internal static readonly string DefaultSqlConnectionString = "data source=localhost;Integrated Security=SSPI";

		// Token: 0x040022FC RID: 8956
		private static ConfigurationProperty allowCustomSqlDatabaseProp = new ConfigurationProperty("allowCustomSqlDatabase", typeof(bool), false);

		// Token: 0x040022FD RID: 8957
		private static ConfigurationProperty cookielessProp = new ConfigurationProperty("cookieless", typeof(string), null);

		// Token: 0x040022FE RID: 8958
		private static ConfigurationProperty cookieNameProp = new ConfigurationProperty("cookieName", typeof(string), "ASP.NET_SessionId");

		// Token: 0x040022FF RID: 8959
		private static ConfigurationProperty customProviderProp = new ConfigurationProperty("customProvider", typeof(string), "");

		// Token: 0x04002300 RID: 8960
		private static ConfigurationProperty modeProp = new ConfigurationProperty("mode", typeof(SessionStateMode), SessionStateMode.InProc, new GenericEnumConverter(typeof(SessionStateMode)), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002301 RID: 8961
		private static ConfigurationProperty partitionResolverTypeProp = new ConfigurationProperty("partitionResolverType", typeof(string), "");

		// Token: 0x04002302 RID: 8962
		private static ConfigurationProperty providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, null, null, ConfigurationPropertyOptions.None);

		// Token: 0x04002303 RID: 8963
		private static ConfigurationProperty regenerateExpiredSessionIdProp = new ConfigurationProperty("regenerateExpiredSessionId", typeof(bool), true);

		// Token: 0x04002304 RID: 8964
		private static ConfigurationProperty sessionIDManagerTypeProp = new ConfigurationProperty("sessionIDManagerType", typeof(string), "");

		// Token: 0x04002305 RID: 8965
		private static ConfigurationProperty sqlCommandTimeoutProp = new ConfigurationProperty("sqlCommandTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(30.0), PropertyHelper.TimeSpanSecondsOrInfiniteConverter, null, ConfigurationPropertyOptions.None);

		// Token: 0x04002306 RID: 8966
		private static ConfigurationProperty sqlConnectionStringProp = new ConfigurationProperty("sqlConnectionString", typeof(string), SessionStateSection.DefaultSqlConnectionString);

		// Token: 0x04002307 RID: 8967
		private static ConfigurationProperty stateConnectionStringProp = new ConfigurationProperty("stateConnectionString", typeof(string), "tcpip=loopback:42424");

		// Token: 0x04002308 RID: 8968
		private static ConfigurationProperty stateNetworkTimeoutProp = new ConfigurationProperty("stateNetworkTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(10.0), PropertyHelper.TimeSpanSecondsOrInfiniteConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002309 RID: 8969
		private static ConfigurationProperty timeoutProp = new ConfigurationProperty("timeout", typeof(TimeSpan), TimeSpan.FromMinutes(20.0), PropertyHelper.TimeSpanMinutesOrInfiniteConverter, new TimeSpanValidator(new TimeSpan(0, 1, 0), TimeSpan.MaxValue), ConfigurationPropertyOptions.None);

		// Token: 0x0400230A RID: 8970
		private static ConfigurationProperty useHostingIdentityProp = new ConfigurationProperty("useHostingIdentity", typeof(bool), true);

		// Token: 0x0400230B RID: 8971
		private static ConfigurationProperty compressionEnabledProp = new ConfigurationProperty("compressionEnabled", typeof(bool), false);

		// Token: 0x0400230C RID: 8972
		private static ConfigurationProperty sqlConnectionRetryIntervalProp = new ConfigurationProperty("sqlConnectionRetryIntervalProp", typeof(TimeSpan), TimeSpan.FromSeconds(0.0), PropertyHelper.TimeSpanSecondsOrInfiniteConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400230D RID: 8973
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400230E RID: 8974
		private static ConfigurationElementProperty elementProperty;
	}
}
