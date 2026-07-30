using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the ASP.NET event rules. This class cannot be inherited.</summary>
	// Token: 0x020005D4 RID: 1492
	public sealed class RuleSettings : ConfigurationElement
	{
		// Token: 0x06004067 RID: 16487 RVA: 0x000A9B50 File Offset: 0x000A7D50
		static RuleSettings()
		{
			RuleSettings.properties.Add(RuleSettings.customProp);
			RuleSettings.properties.Add(RuleSettings.eventNameProp);
			RuleSettings.properties.Add(RuleSettings.maxLimitProp);
			RuleSettings.properties.Add(RuleSettings.minInstancesProp);
			RuleSettings.properties.Add(RuleSettings.minIntervalProp);
			RuleSettings.properties.Add(RuleSettings.nameProp);
			RuleSettings.properties.Add(RuleSettings.profileProp);
			RuleSettings.properties.Add(RuleSettings.providerProp);
		}

		// Token: 0x06004068 RID: 16488 RVA: 0x0009F629 File Offset: 0x0009D829
		internal RuleSettings()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.BufferModeSettings" /> class where all values are specified.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.RuleSettings" /> object to create.</param>
		/// <param name="eventName">The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object this rule applies to.</param>
		/// <param name="provider">The name of the <see cref="T:System.Configuration.ProviderSettings" /> object this rule applies to.</param>
		/// <param name="profile">The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object this rule applies to.</param>
		/// <param name="minInstances">The minimum number of occurrences of an event of the same type before the event is fired to the provider. </param>
		/// <param name="maxLimit">The maximum number of times events of the same type are fired.</param>
		/// <param name="minInterval">The minimum time interval between two events of the same type.</param>
		/// <param name="custom">The fully qualified type of a custom class that implements <see cref="T:System.Web.Management.IWebEventCustomEvaluator" />.</param>
		// Token: 0x06004069 RID: 16489 RVA: 0x000A9D28 File Offset: 0x000A7F28
		public RuleSettings(string name, string eventName, string provider, string profile, int minInstances, int maxLimit, TimeSpan minInterval, string custom)
		{
			this.Name = name;
			this.EventName = eventName;
			this.Provider = provider;
			this.Profile = profile;
			this.MinInstances = minInstances;
			this.MaxLimit = maxLimit;
			this.MinInterval = minInterval;
			this.Custom = custom;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.RuleSettings" /> class where all values except those of the <see cref="P:System.Web.Configuration.RuleSettings.Custom" /> class are specified.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.RuleSettings" /> object to create.</param>
		/// <param name="eventName">The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object this rule applies to.</param>
		/// <param name="provider">The name of the <see cref="T:System.Configuration.ProviderSettings" /> object this rule applies to.</param>
		/// <param name="profile">The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object this rule applies to.</param>
		/// <param name="minInstances">The minimum number of occurrences of the same type of event that can occur before the event is raised to the provider. </param>
		/// <param name="maxLimit">The maximum number of times events of the same type can be raised.</param>
		/// <param name="minInterval">The minimum time interval between two events of the same type.</param>
		// Token: 0x0600406A RID: 16490 RVA: 0x000A9D78 File Offset: 0x000A7F78
		public RuleSettings(string name, string eventName, string provider, string profile, int minInstances, int maxLimit, TimeSpan minInterval)
		{
			this.Name = name;
			this.EventName = eventName;
			this.Provider = provider;
			this.Profile = profile;
			this.MinInstances = minInstances;
			this.MaxLimit = maxLimit;
			this.MinInterval = minInterval;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.RuleSettings" /> class using default settings; however, the name, event name, and provider are specified.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.RuleSettings" /> object to create.</param>
		/// <param name="eventName">The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object this rule applies to.</param>
		/// <param name="provider">The name of the <see cref="T:System.Configuration.ProviderSettings" /> object this rule applies to.</param>
		// Token: 0x0600406B RID: 16491 RVA: 0x000A9DB5 File Offset: 0x000A7FB5
		public RuleSettings(string name, string eventName, string provider)
		{
			this.Name = name;
			this.EventName = eventName;
			this.Provider = provider;
		}

		/// <summary>Gets or sets the fully qualified type of a custom class that implements <see cref="T:System.Web.Management.IWebEventCustomEvaluator" />.</summary>
		/// <returns>The fully qualified type of a custom class that implements <see cref="T:System.Web.Management.IWebEventCustomEvaluator" />.</returns>
		// Token: 0x17001457 RID: 5207
		// (get) Token: 0x0600406C RID: 16492 RVA: 0x000A9DD2 File Offset: 0x000A7FD2
		// (set) Token: 0x0600406D RID: 16493 RVA: 0x000A9DE4 File Offset: 0x000A7FE4
		[ConfigurationProperty("custom", DefaultValue = "")]
		public string Custom
		{
			get
			{
				return (string)base[RuleSettings.customProp];
			}
			set
			{
				base[RuleSettings.customProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object this rule applies to.</summary>
		/// <returns>The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object this rule applies to.</returns>
		// Token: 0x17001458 RID: 5208
		// (get) Token: 0x0600406E RID: 16494 RVA: 0x000A9DF2 File Offset: 0x000A7FF2
		// (set) Token: 0x0600406F RID: 16495 RVA: 0x000A9E04 File Offset: 0x000A8004
		[ConfigurationProperty("eventName", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string EventName
		{
			get
			{
				return (string)base[RuleSettings.eventNameProp];
			}
			set
			{
				base[RuleSettings.eventNameProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum number of times events of the same type are raised.</summary>
		/// <returns>The maximum number of times events of the same type are raised. The default value is <see cref="F:System.Int32.MaxValue" />.</returns>
		// Token: 0x17001459 RID: 5209
		// (get) Token: 0x06004070 RID: 16496 RVA: 0x000A9E12 File Offset: 0x000A8012
		// (set) Token: 0x06004071 RID: 16497 RVA: 0x000A9E24 File Offset: 0x000A8024
		[TypeConverter(typeof(InfiniteIntConverter))]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		[ConfigurationProperty("maxLimit", DefaultValue = "2147483647")]
		public int MaxLimit
		{
			get
			{
				return (int)base[RuleSettings.maxLimitProp];
			}
			set
			{
				base[RuleSettings.maxLimitProp] = value;
			}
		}

		/// <summary>Gets or sets the minimum number of occurrences of the same type of event before the event is raised to the provider.</summary>
		/// <returns>The minimum number of occurrences of the same type of event before the event is raised to the provider. The default value is 1.</returns>
		// Token: 0x1700145A RID: 5210
		// (get) Token: 0x06004072 RID: 16498 RVA: 0x000A9E37 File Offset: 0x000A8037
		// (set) Token: 0x06004073 RID: 16499 RVA: 0x000A9E49 File Offset: 0x000A8049
		[ConfigurationProperty("minInstances", DefaultValue = "1")]
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647)]
		public int MinInstances
		{
			get
			{
				return (int)base[RuleSettings.minInstancesProp];
			}
			set
			{
				base[RuleSettings.minInstancesProp] = value;
			}
		}

		/// <summary>Gets or sets the minimum time interval between two events of the same type.</summary>
		/// <returns>The minimum time interval between two events of the same type. The default value is 0 ticks.</returns>
		// Token: 0x1700145B RID: 5211
		// (get) Token: 0x06004074 RID: 16500 RVA: 0x000A9E5C File Offset: 0x000A805C
		// (set) Token: 0x06004075 RID: 16501 RVA: 0x000A9E6E File Offset: 0x000A806E
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		[ConfigurationProperty("minInterval", DefaultValue = "00:00:00")]
		public TimeSpan MinInterval
		{
			get
			{
				return (TimeSpan)base[RuleSettings.minIntervalProp];
			}
			set
			{
				base[RuleSettings.minIntervalProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Web.Configuration.RuleSettings" /> object.</summary>
		/// <returns>The name of the <see cref="T:System.Web.Configuration.RuleSettings" /> object. The default value is an empty string ("").</returns>
		// Token: 0x1700145C RID: 5212
		// (get) Token: 0x06004076 RID: 16502 RVA: 0x000A9E81 File Offset: 0x000A8081
		// (set) Token: 0x06004077 RID: 16503 RVA: 0x000A9E93 File Offset: 0x000A8093
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Name
		{
			get
			{
				return (string)base[RuleSettings.nameProp];
			}
			set
			{
				base[RuleSettings.nameProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object this rule applies to.</summary>
		/// <returns>The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object this rule applies to.</returns>
		// Token: 0x1700145D RID: 5213
		// (get) Token: 0x06004078 RID: 16504 RVA: 0x000A9EA1 File Offset: 0x000A80A1
		// (set) Token: 0x06004079 RID: 16505 RVA: 0x000A9EB3 File Offset: 0x000A80B3
		[ConfigurationProperty("profile", DefaultValue = "")]
		public string Profile
		{
			get
			{
				return (string)base[RuleSettings.profileProp];
			}
			set
			{
				base[RuleSettings.profileProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Configuration.ProviderSettings" /> object this rule applies to.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.ProviderSettings" /> object this rule applies to.</returns>
		// Token: 0x1700145E RID: 5214
		// (get) Token: 0x0600407A RID: 16506 RVA: 0x000A9EC1 File Offset: 0x000A80C1
		// (set) Token: 0x0600407B RID: 16507 RVA: 0x000A9ED3 File Offset: 0x000A80D3
		[ConfigurationProperty("provider", DefaultValue = "")]
		public string Provider
		{
			get
			{
				return (string)base[RuleSettings.providerProp];
			}
			set
			{
				base[RuleSettings.providerProp] = value;
			}
		}

		// Token: 0x1700145F RID: 5215
		// (get) Token: 0x0600407C RID: 16508 RVA: 0x000A9EE1 File Offset: 0x000A80E1
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return RuleSettings.properties;
			}
		}

		// Token: 0x040022EC RID: 8940
		private static ConfigurationProperty customProp = new ConfigurationProperty("custom", typeof(string), "");

		// Token: 0x040022ED RID: 8941
		private static ConfigurationProperty eventNameProp = new ConfigurationProperty("eventName", typeof(string), "", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040022EE RID: 8942
		private static ConfigurationProperty maxLimitProp = new ConfigurationProperty("maxLimit", typeof(int), int.MaxValue, PropertyHelper.InfiniteIntConverter, PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022EF RID: 8943
		private static ConfigurationProperty minInstancesProp = new ConfigurationProperty("minInstances", typeof(int), 1, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(1, int.MaxValue), ConfigurationPropertyOptions.None);

		// Token: 0x040022F0 RID: 8944
		private static ConfigurationProperty minIntervalProp = new ConfigurationProperty("minInterval", typeof(TimeSpan), TimeSpan.FromSeconds(0.0), PropertyHelper.InfiniteTimeSpanConverter, null, ConfigurationPropertyOptions.None);

		// Token: 0x040022F1 RID: 8945
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040022F2 RID: 8946
		private static ConfigurationProperty profileProp = new ConfigurationProperty("profile", typeof(string), "");

		// Token: 0x040022F3 RID: 8947
		private static ConfigurationProperty providerProp = new ConfigurationProperty("provider", typeof(string), "");

		// Token: 0x040022F4 RID: 8948
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
