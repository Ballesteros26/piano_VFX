using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures ASP.NET profiles that determine how health-monitoring events are sent to event providers. This class cannot be inherited.</summary>
	// Token: 0x020005A4 RID: 1444
	public sealed class HealthMonitoringSection : ConfigurationSection
	{
		// Token: 0x06003D57 RID: 15703 RVA: 0x000A2C54 File Offset: 0x000A0E54
		static HealthMonitoringSection()
		{
			HealthMonitoringSection.properties.Add(HealthMonitoringSection.bufferModesProp);
			HealthMonitoringSection.properties.Add(HealthMonitoringSection.enabledProp);
			HealthMonitoringSection.properties.Add(HealthMonitoringSection.eventMappingsProp);
			HealthMonitoringSection.properties.Add(HealthMonitoringSection.heartbeatIntervalProp);
			HealthMonitoringSection.properties.Add(HealthMonitoringSection.profilesProp);
			HealthMonitoringSection.properties.Add(HealthMonitoringSection.providersProp);
			HealthMonitoringSection.properties.Add(HealthMonitoringSection.rulesProp);
		}

		/// <summary>Gets a collection of objects that specify the settings for the buffer modes.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.BufferModesCollection" /> collection of <see cref="T:System.Web.Configuration.BufferModeSettings" /> objects.</returns>
		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x06003D58 RID: 15704 RVA: 0x000A2DE1 File Offset: 0x000A0FE1
		[ConfigurationProperty("bufferModes")]
		public BufferModesCollection BufferModes
		{
			get
			{
				return (BufferModesCollection)base[HealthMonitoringSection.bufferModesProp];
			}
		}

		/// <summary>Gets or sets a value indicating whether health monitoring is enabled.</summary>
		/// <returns>true if health monitoring is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x06003D59 RID: 15705 RVA: 0x000A2DF3 File Offset: 0x000A0FF3
		// (set) Token: 0x06003D5A RID: 15706 RVA: 0x000A2E05 File Offset: 0x000A1005
		[ConfigurationProperty("enabled", DefaultValue = "True")]
		public bool Enabled
		{
			get
			{
				return (bool)base[HealthMonitoringSection.enabledProp];
			}
			set
			{
				base[HealthMonitoringSection.enabledProp] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.Configuration.EventMappingSettingsCollection" /> collection of <see cref="T:System.Web.Configuration.EventMappingSettings" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.EventMappingSettingsCollection" /> collection of <see cref="T:System.Web.Configuration.EventMappingSettings" /> objects. The default is an empty <see cref="T:System.Web.Configuration.EventMappingSettingsCollection" /> collection.</returns>
		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x06003D5B RID: 15707 RVA: 0x000A2E18 File Offset: 0x000A1018
		[ConfigurationProperty("eventMappings")]
		public EventMappingSettingsCollection EventMappings
		{
			get
			{
				return (EventMappingSettingsCollection)base[HealthMonitoringSection.eventMappingsProp];
			}
		}

		/// <summary>Gets or sets the interval used by the application domain when it raises the <see cref="T:System.Web.Management.WebHeartbeatEvent" /> event.</summary>
		/// <returns>The interval used by the application domain when it raises the <see cref="T:System.Web.Management.WebHeartbeatEvent" /> event.</returns>
		// Token: 0x170012F1 RID: 4849
		// (get) Token: 0x06003D5C RID: 15708 RVA: 0x000A2E2A File Offset: 0x000A102A
		// (set) Token: 0x06003D5D RID: 15709 RVA: 0x000A2E3C File Offset: 0x000A103C
		[TypeConverter(typeof(TimeSpanSecondsConverter))]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "24.20:31:23")]
		[ConfigurationProperty("heartbeatInterval", DefaultValue = "00:00:00")]
		public TimeSpan HeartbeatInterval
		{
			get
			{
				return (TimeSpan)base[HealthMonitoringSection.heartbeatIntervalProp];
			}
			set
			{
				base[HealthMonitoringSection.heartbeatIntervalProp] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.Configuration.ProfileSettingsCollection" /> collection of <see cref="T:System.Web.Configuration.ProfileSettings" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.ProfileSettingsCollection" /> collection of <see cref="T:System.Web.Configuration.ProfileSettings" /> objects. The default is an empty <see cref="T:System.Web.Configuration.ProfileSettingsCollection" /> collection.</returns>
		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x06003D5E RID: 15710 RVA: 0x000A2E4F File Offset: 0x000A104F
		[ConfigurationProperty("profiles")]
		public ProfileSettingsCollection Profiles
		{
			get
			{
				return (ProfileSettingsCollection)base[HealthMonitoringSection.profilesProp];
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection of <see cref="T:System.Configuration.ProviderSettings" /> objects.</summary>
		/// <returns>A <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection. The default is an empty <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection.</returns>
		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x06003D5F RID: 15711 RVA: 0x000A2E61 File Offset: 0x000A1061
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[HealthMonitoringSection.providersProp];
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.Configuration.RuleSettingsCollection" /> collection of <see cref="T:System.Web.Configuration.RuleSettings" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.RuleSettingsCollection" /> collection. The default is an empty <see cref="T:System.Web.Configuration.RuleSettingsCollection" /> collection</returns>
		// Token: 0x170012F4 RID: 4852
		// (get) Token: 0x06003D60 RID: 15712 RVA: 0x000A2E73 File Offset: 0x000A1073
		[ConfigurationProperty("rules")]
		public RuleSettingsCollection Rules
		{
			get
			{
				return (RuleSettingsCollection)base[HealthMonitoringSection.rulesProp];
			}
		}

		// Token: 0x170012F5 RID: 4853
		// (get) Token: 0x06003D61 RID: 15713 RVA: 0x000A2E85 File Offset: 0x000A1085
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HealthMonitoringSection.properties;
			}
		}

		// Token: 0x04002111 RID: 8465
		private static ConfigurationProperty bufferModesProp = new ConfigurationProperty("bufferModes", typeof(BufferModesCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002112 RID: 8466
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);

		// Token: 0x04002113 RID: 8467
		private static ConfigurationProperty eventMappingsProp = new ConfigurationProperty("eventMappings", typeof(EventMappingSettingsCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002114 RID: 8468
		private static ConfigurationProperty heartbeatIntervalProp = new ConfigurationProperty("heartbeatInterval", typeof(TimeSpan), TimeSpan.FromSeconds(0.0), PropertyHelper.TimeSpanSecondsConverter, new TimeSpanValidator(TimeSpan.Zero, new TimeSpan(24, 30, 31, 23)), ConfigurationPropertyOptions.None);

		// Token: 0x04002115 RID: 8469
		private static ConfigurationProperty profilesProp = new ConfigurationProperty("profiles", typeof(ProfileSettingsCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002116 RID: 8470
		private static ConfigurationProperty providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002117 RID: 8471
		private static ConfigurationProperty rulesProp = new ConfigurationProperty("rules", typeof(RuleSettingsCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002118 RID: 8472
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
