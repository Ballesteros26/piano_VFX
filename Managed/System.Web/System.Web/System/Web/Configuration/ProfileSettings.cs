using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the ASP.NET event profiles. This class cannot be inherited.</summary>
	// Token: 0x020005CD RID: 1485
	public sealed class ProfileSettings : ConfigurationElement
	{
		// Token: 0x06004018 RID: 16408 RVA: 0x000A9104 File Offset: 0x000A7304
		static ProfileSettings()
		{
			ProfileSettings.properties.Add(ProfileSettings.customProp);
			ProfileSettings.properties.Add(ProfileSettings.maxLimitProp);
			ProfileSettings.properties.Add(ProfileSettings.minInstancesProp);
			ProfileSettings.properties.Add(ProfileSettings.minIntervalProp);
			ProfileSettings.properties.Add(ProfileSettings.nameProp);
		}

		// Token: 0x06004019 RID: 16409 RVA: 0x0009F629 File Offset: 0x0009D829
		internal ProfileSettings()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ProfileSettings" /> class. using the specified name for the new instance of the class.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object to create.</param>
		// Token: 0x0600401A RID: 16410 RVA: 0x000A9256 File Offset: 0x000A7456
		public ProfileSettings(string name)
		{
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.BufferModeSettings" /> class, using the specified settings for the new instance of the class.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object to create.</param>
		/// <param name="minInstances">The minimum number of event occurrences before the event is raised to the provider. </param>
		/// <param name="maxLimit">The maximum number of times events of the same type are raised.</param>
		/// <param name="minInterval">A <see cref="T:System.TimeSpan" /> that specifies the minimum interval between two events of the same type.</param>
		/// <param name="custom">The fully qualified type of a custom class that implements <see cref="T:System.Web.Management.IWebEventCustomEvaluator" />.</param>
		// Token: 0x0600401B RID: 16411 RVA: 0x000A9265 File Offset: 0x000A7465
		public ProfileSettings(string name, int minInstances, int maxLimit, TimeSpan minInterval, string custom)
		{
			this.Name = name;
			this.MinInstances = minInstances;
			this.MaxLimit = maxLimit;
			this.MinInterval = this.MinInterval;
			this.Custom = custom;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ProfileSettings" /> class, using specified settings for the new instance of the class.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object to create.</param>
		/// <param name="minInstances">The minimum number of event occurrences before the event is raised to the provider. </param>
		/// <param name="maxLimit">The maximum number of times events of the same type are raised.</param>
		/// <param name="minInterval">A <see cref="T:System.TimeSpan" /> that specifies the minimum length of the interval between the times when two events of the same type are raised.</param>
		// Token: 0x0600401C RID: 16412 RVA: 0x000A9296 File Offset: 0x000A7496
		public ProfileSettings(string name, int minInstances, int maxLimit, TimeSpan minInterval)
		{
			this.Name = name;
			this.MinInstances = minInstances;
			this.MaxLimit = maxLimit;
			this.MinInterval = this.MinInterval;
		}

		/// <summary>Gets or sets the fully qualified type of a custom class that implements the <see cref="T:System.Web.Management.IWebEventCustomEvaluator" /> interface.</summary>
		/// <returns>The fully qualified type of a custom class that implements the <see cref="T:System.Web.Management.IWebEventCustomEvaluator" /> interface. The default is an empty string ("").</returns>
		// Token: 0x1700143B RID: 5179
		// (get) Token: 0x0600401D RID: 16413 RVA: 0x000A92BF File Offset: 0x000A74BF
		// (set) Token: 0x0600401E RID: 16414 RVA: 0x000A92D1 File Offset: 0x000A74D1
		[ConfigurationProperty("custom", DefaultValue = "")]
		public string Custom
		{
			get
			{
				return (string)base[ProfileSettings.customProp];
			}
			set
			{
				base[ProfileSettings.customProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum number of times events of the same type are raised.</summary>
		/// <returns>The maximum number of times events of the same type are raised. The default is <see cref="F:System.Int32.MaxValue" />.</returns>
		// Token: 0x1700143C RID: 5180
		// (get) Token: 0x0600401F RID: 16415 RVA: 0x000A92DF File Offset: 0x000A74DF
		// (set) Token: 0x06004020 RID: 16416 RVA: 0x000A92F1 File Offset: 0x000A74F1
		[TypeConverter(typeof(InfiniteIntConverter))]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		[ConfigurationProperty("maxLimit", DefaultValue = 2147483647)]
		public int MaxLimit
		{
			get
			{
				return (int)base[ProfileSettings.maxLimitProp];
			}
			set
			{
				base[ProfileSettings.maxLimitProp] = value;
			}
		}

		/// <summary>Gets or sets the minimum number of event occurrences before the event is raised to the provider.</summary>
		/// <returns>The minimum number of event occurrences before the event is fired to the provider. The default is 1.</returns>
		// Token: 0x1700143D RID: 5181
		// (get) Token: 0x06004021 RID: 16417 RVA: 0x000A9304 File Offset: 0x000A7504
		// (set) Token: 0x06004022 RID: 16418 RVA: 0x000A9316 File Offset: 0x000A7516
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647)]
		[ConfigurationProperty("minInstances", DefaultValue = "1")]
		public int MinInstances
		{
			get
			{
				return (int)base[ProfileSettings.minInstancesProp];
			}
			set
			{
				base[ProfileSettings.minInstancesProp] = value;
			}
		}

		/// <summary>Gets or sets the minimum interval between two events of the same type.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that specifies the minimum interval between two events of the same type. The default is <see cref="F:System.TimeSpan.Zero" />.</returns>
		// Token: 0x1700143E RID: 5182
		// (get) Token: 0x06004023 RID: 16419 RVA: 0x000A9329 File Offset: 0x000A7529
		// (set) Token: 0x06004024 RID: 16420 RVA: 0x000A933B File Offset: 0x000A753B
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		[ConfigurationProperty("minInterval", DefaultValue = "00:00:00")]
		public TimeSpan MinInterval
		{
			get
			{
				return (TimeSpan)base[ProfileSettings.minIntervalProp];
			}
			set
			{
				base[ProfileSettings.minIntervalProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object.</summary>
		/// <returns>The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object. The default is an empty string("").</returns>
		// Token: 0x1700143F RID: 5183
		// (get) Token: 0x06004025 RID: 16421 RVA: 0x000A934E File Offset: 0x000A754E
		// (set) Token: 0x06004026 RID: 16422 RVA: 0x000A9360 File Offset: 0x000A7560
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		[StringValidator(MinLength = 1)]
		public string Name
		{
			get
			{
				return (string)base[ProfileSettings.nameProp];
			}
			set
			{
				base[ProfileSettings.nameProp] = value;
			}
		}

		// Token: 0x17001440 RID: 5184
		// (get) Token: 0x06004027 RID: 16423 RVA: 0x000A936E File Offset: 0x000A756E
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProfileSettings.properties;
			}
		}

		// Token: 0x040022C7 RID: 8903
		private static ConfigurationProperty customProp = new ConfigurationProperty("custom", typeof(string), "");

		// Token: 0x040022C8 RID: 8904
		private static ConfigurationProperty maxLimitProp = new ConfigurationProperty("maxLimit", typeof(int), int.MaxValue, PropertyHelper.InfiniteIntConverter, PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022C9 RID: 8905
		private static ConfigurationProperty minInstancesProp = new ConfigurationProperty("minInstances", typeof(int), 1, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(1, int.MaxValue), ConfigurationPropertyOptions.None);

		// Token: 0x040022CA RID: 8906
		private static ConfigurationProperty minIntervalProp = new ConfigurationProperty("minInterval", typeof(TimeSpan), TimeSpan.FromSeconds(0.0), PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022CB RID: 8907
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040022CC RID: 8908
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
