using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the ASP.NET event-buffering settings for event providers. This class cannot be inherited.</summary>
	// Token: 0x02000587 RID: 1415
	public sealed class BufferModeSettings : ConfigurationElement
	{
		// Token: 0x06003BC8 RID: 15304 RVA: 0x0009FF48 File Offset: 0x0009E148
		static BufferModeSettings()
		{
			IntegerValidator integerValidator = new IntegerValidator(1, int.MaxValue);
			BufferModeSettings.maxBufferSizeProp = new ConfigurationProperty("maxBufferSize", typeof(int), int.MaxValue, PropertyHelper.InfiniteIntConverter, integerValidator, ConfigurationPropertyOptions.IsRequired);
			BufferModeSettings.maxBufferThreadsProp = new ConfigurationProperty("maxBufferThreads", typeof(int), 1, PropertyHelper.InfiniteIntConverter, integerValidator, ConfigurationPropertyOptions.None);
			BufferModeSettings.maxFlushSizeProp = new ConfigurationProperty("maxFlushSize", typeof(int), int.MaxValue, PropertyHelper.InfiniteIntConverter, integerValidator, ConfigurationPropertyOptions.IsRequired);
			BufferModeSettings.nameProp = new ConfigurationProperty("name", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
			BufferModeSettings.regularFlushIntervalProp = new ConfigurationProperty("regularFlushInterval", typeof(TimeSpan), TimeSpan.FromSeconds(1.0), PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.IsRequired);
			BufferModeSettings.urgentFlushIntervalProp = new ConfigurationProperty("urgentFlushInterval", typeof(TimeSpan), TimeSpan.FromSeconds(0.0), PropertyHelper.InfiniteTimeSpanConverter, null, ConfigurationPropertyOptions.IsRequired);
			BufferModeSettings.urgentFlushThresholdProp = new ConfigurationProperty("urgentFlushThreshold", typeof(int), int.MaxValue, PropertyHelper.InfiniteIntConverter, integerValidator, ConfigurationPropertyOptions.IsRequired);
			BufferModeSettings.properties = new ConfigurationPropertyCollection();
			BufferModeSettings.properties.Add(BufferModeSettings.nameProp);
			BufferModeSettings.properties.Add(BufferModeSettings.maxBufferSizeProp);
			BufferModeSettings.properties.Add(BufferModeSettings.maxBufferThreadsProp);
			BufferModeSettings.properties.Add(BufferModeSettings.maxFlushSizeProp);
			BufferModeSettings.properties.Add(BufferModeSettings.regularFlushIntervalProp);
			BufferModeSettings.properties.Add(BufferModeSettings.urgentFlushIntervalProp);
			BufferModeSettings.properties.Add(BufferModeSettings.urgentFlushThresholdProp);
			BufferModeSettings.elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(BufferModeSettings), new ValidatorCallback(BufferModeSettings.ValidateElement)));
		}

		// Token: 0x06003BC9 RID: 15305 RVA: 0x0009F629 File Offset: 0x0009D829
		internal BufferModeSettings()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.BufferModeSettings" /> class using specified settings.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.BufferModeSettings" /> object being created.</param>
		/// <param name="maxBufferSize">The maximum number of events buffered at one time. The value must be greater than zero.</param>
		/// <param name="maxFlushSize">The maximum number of events per buffer flush. Must be greater than zero.</param>
		/// <param name="urgentFlushThreshold">The number of events buffered before a buffer flush is triggered. The value must be greater than zero and less than or equal to <paramref name="maxBufferSize" />.</param>
		/// <param name="regularFlushInterval">The standard amount of time between buffer flushes. The value can be made infinite by setting it to <see cref="F:System.Int32.MaxValue" /> ticks.</param>
		/// <param name="urgentFlushInterval">The minimum length of time that can pass between buffer flushes. The value must be less than or equal to <paramref name="regularFlushInterval" />.</param>
		/// <param name="maxBufferThreads">The maximum number of buffer-flushing threads that can be active at one time.</param>
		// Token: 0x06003BCA RID: 15306 RVA: 0x000A013A File Offset: 0x0009E33A
		public BufferModeSettings(string name, int maxBufferSize, int maxFlushSize, int urgentFlushThreshold, TimeSpan regularFlushInterval, TimeSpan urgentFlushInterval, int maxBufferThreads)
		{
			this.Name = name;
			this.MaxBufferSize = maxBufferSize;
			this.MaxFlushSize = maxFlushSize;
			this.UrgentFlushThreshold = urgentFlushThreshold;
			this.RegularFlushInterval = regularFlushInterval;
			this.UrgentFlushInterval = urgentFlushInterval;
			this.MaxBufferThreads = maxBufferThreads;
		}

		// Token: 0x06003BCB RID: 15307 RVA: 0x0000393A File Offset: 0x00001B3A
		[global::System.MonoTODO("Should do some validation here")]
		private static void ValidateElement(object o)
		{
		}

		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x06003BCC RID: 15308 RVA: 0x000A0177 File Offset: 0x0009E377
		protected internal override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return BufferModeSettings.elementProperty;
			}
		}

		/// <summary>Gets or sets the maximum number of events that can be buffered at one time.</summary>
		/// <returns>The maximum number of events that can be buffered at one time.</returns>
		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x06003BCD RID: 15309 RVA: 0x000A017E File Offset: 0x0009E37E
		// (set) Token: 0x06003BCE RID: 15310 RVA: 0x000A0190 File Offset: 0x0009E390
		[TypeConverter(typeof(InfiniteIntConverter))]
		[ConfigurationProperty("maxBufferSize", DefaultValue = "2147483647", Options = ConfigurationPropertyOptions.IsRequired)]
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647)]
		public int MaxBufferSize
		{
			get
			{
				return (int)base[BufferModeSettings.maxBufferSizeProp];
			}
			set
			{
				base[BufferModeSettings.maxBufferSizeProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum number of flushing threads that can be active at one time.</summary>
		/// <returns>The maximum number of flushing threads that can be active at one time. The default is 1.</returns>
		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x06003BCF RID: 15311 RVA: 0x000A01A3 File Offset: 0x0009E3A3
		// (set) Token: 0x06003BD0 RID: 15312 RVA: 0x000A01B5 File Offset: 0x0009E3B5
		[ConfigurationProperty("maxBufferThreads", DefaultValue = "1")]
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647)]
		[TypeConverter(typeof(InfiniteIntConverter))]
		public int MaxBufferThreads
		{
			get
			{
				return (int)base[BufferModeSettings.maxBufferThreadsProp];
			}
			set
			{
				base[BufferModeSettings.maxBufferThreadsProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum number of events per flush.</summary>
		/// <returns>The maximum number of events per flush.</returns>
		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x06003BD1 RID: 15313 RVA: 0x000A01C8 File Offset: 0x0009E3C8
		// (set) Token: 0x06003BD2 RID: 15314 RVA: 0x000A01DA File Offset: 0x0009E3DA
		[ConfigurationProperty("maxFlushSize", DefaultValue = "2147483647", Options = ConfigurationPropertyOptions.IsRequired)]
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647)]
		[TypeConverter(typeof(InfiniteIntConverter))]
		public int MaxFlushSize
		{
			get
			{
				return (int)base[BufferModeSettings.maxFlushSizeProp];
			}
			set
			{
				base[BufferModeSettings.maxFlushSizeProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Web.Configuration.BufferModeSettings" /> object.</summary>
		/// <returns>The name of the <see cref="T:System.Web.Configuration.BufferModeSettings" /> object. The default value is an empty string.</returns>
		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x06003BD3 RID: 15315 RVA: 0x000A01ED File Offset: 0x0009E3ED
		// (set) Token: 0x06003BD4 RID: 15316 RVA: 0x000A01FF File Offset: 0x0009E3FF
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Name
		{
			get
			{
				return (string)base[BufferModeSettings.nameProp];
			}
			set
			{
				base[BufferModeSettings.nameProp] = value;
			}
		}

		/// <summary>Gets or sets the amount of time between buffer flushes.</summary>
		/// <returns>The regular amount of time between buffer flushes.</returns>
		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x06003BD5 RID: 15317 RVA: 0x000A020D File Offset: 0x0009E40D
		// (set) Token: 0x06003BD6 RID: 15318 RVA: 0x000A021F File Offset: 0x0009E41F
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		[ConfigurationProperty("regularFlushInterval", DefaultValue = "00:00:01", Options = ConfigurationPropertyOptions.IsRequired)]
		public TimeSpan RegularFlushInterval
		{
			get
			{
				return (TimeSpan)base[BufferModeSettings.regularFlushIntervalProp];
			}
			set
			{
				base[BufferModeSettings.regularFlushIntervalProp] = value;
			}
		}

		/// <summary>Gets or sets the minimum amount of time that can pass between buffer flushes. </summary>
		/// <returns>The minimum amount of time that can pass between buffer flushes.</returns>
		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x06003BD7 RID: 15319 RVA: 0x000A0232 File Offset: 0x0009E432
		// (set) Token: 0x06003BD8 RID: 15320 RVA: 0x000A0244 File Offset: 0x0009E444
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		[ConfigurationProperty("urgentFlushInterval", DefaultValue = "00:00:00", Options = ConfigurationPropertyOptions.IsRequired)]
		public TimeSpan UrgentFlushInterval
		{
			get
			{
				return (TimeSpan)base[BufferModeSettings.urgentFlushIntervalProp];
			}
			set
			{
				base[BufferModeSettings.urgentFlushIntervalProp] = value;
			}
		}

		/// <summary>Gets or sets the number of events that can be buffered before a flush is triggered.</summary>
		/// <returns>The number of events that can be buffered before a flush is triggered.</returns>
		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x06003BD9 RID: 15321 RVA: 0x000A0257 File Offset: 0x0009E457
		// (set) Token: 0x06003BDA RID: 15322 RVA: 0x000A0269 File Offset: 0x0009E469
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647)]
		[ConfigurationProperty("urgentFlushThreshold", DefaultValue = "2147483647", Options = ConfigurationPropertyOptions.IsRequired)]
		[TypeConverter(typeof(InfiniteIntConverter))]
		public int UrgentFlushThreshold
		{
			get
			{
				return (int)base[BufferModeSettings.urgentFlushThresholdProp];
			}
			set
			{
				base[BufferModeSettings.urgentFlushThresholdProp] = value;
			}
		}

		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x06003BDB RID: 15323 RVA: 0x000A027C File Offset: 0x0009E47C
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return BufferModeSettings.properties;
			}
		}

		// Token: 0x04002098 RID: 8344
		private static ConfigurationProperty maxBufferSizeProp;

		// Token: 0x04002099 RID: 8345
		private static ConfigurationProperty maxBufferThreadsProp;

		// Token: 0x0400209A RID: 8346
		private static ConfigurationProperty maxFlushSizeProp;

		// Token: 0x0400209B RID: 8347
		private static ConfigurationProperty nameProp;

		// Token: 0x0400209C RID: 8348
		private static ConfigurationProperty regularFlushIntervalProp;

		// Token: 0x0400209D RID: 8349
		private static ConfigurationProperty urgentFlushIntervalProp;

		// Token: 0x0400209E RID: 8350
		private static ConfigurationProperty urgentFlushThresholdProp;

		// Token: 0x0400209F RID: 8351
		private static ConfigurationPropertyCollection properties;

		// Token: 0x040020A0 RID: 8352
		private static ConfigurationElementProperty elementProperty;
	}
}
