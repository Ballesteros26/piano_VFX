using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the ASP.NET process model settings on an Internet Information Services (IIS) Web server. This class cannot be inherited.</summary>
	// Token: 0x020005C6 RID: 1478
	public sealed class ProcessModelSection : ConfigurationSection
	{
		// Token: 0x06003F84 RID: 16260 RVA: 0x000A7E48 File Offset: 0x000A6048
		static ProcessModelSection()
		{
			ProcessModelSection.properties.Add(ProcessModelSection.autoConfigProp);
			ProcessModelSection.properties.Add(ProcessModelSection.clientConnectedCheckProp);
			ProcessModelSection.properties.Add(ProcessModelSection.comAuthenticationLevelProp);
			ProcessModelSection.properties.Add(ProcessModelSection.comImpersonationLevelProp);
			ProcessModelSection.properties.Add(ProcessModelSection.cpuMaskProp);
			ProcessModelSection.properties.Add(ProcessModelSection.enableProp);
			ProcessModelSection.properties.Add(ProcessModelSection.idleTimeoutProp);
			ProcessModelSection.properties.Add(ProcessModelSection.logLevelProp);
			ProcessModelSection.properties.Add(ProcessModelSection.maxAppDomainsProp);
			ProcessModelSection.properties.Add(ProcessModelSection.maxIoThreadsProp);
			ProcessModelSection.properties.Add(ProcessModelSection.maxWorkerThreadsProp);
			ProcessModelSection.properties.Add(ProcessModelSection.memoryLimitProp);
			ProcessModelSection.properties.Add(ProcessModelSection.minIoThreadsProp);
			ProcessModelSection.properties.Add(ProcessModelSection.minWorkerThreadsProp);
			ProcessModelSection.properties.Add(ProcessModelSection.passwordProp);
			ProcessModelSection.properties.Add(ProcessModelSection.pingFrequencyProp);
			ProcessModelSection.properties.Add(ProcessModelSection.pingTimeoutProp);
			ProcessModelSection.properties.Add(ProcessModelSection.requestLimitProp);
			ProcessModelSection.properties.Add(ProcessModelSection.requestQueueLimitProp);
			ProcessModelSection.properties.Add(ProcessModelSection.responseDeadlockIntervalProp);
			ProcessModelSection.properties.Add(ProcessModelSection.responseRestartDeadlockIntervalProp);
			ProcessModelSection.properties.Add(ProcessModelSection.restartQueueLimitProp);
			ProcessModelSection.properties.Add(ProcessModelSection.serverErrorMessageFileProp);
			ProcessModelSection.properties.Add(ProcessModelSection.shutdownTimeoutProp);
			ProcessModelSection.properties.Add(ProcessModelSection.timeoutProp);
			ProcessModelSection.properties.Add(ProcessModelSection.userNameProp);
			ProcessModelSection.properties.Add(ProcessModelSection.webGardenProp);
			ProcessModelSection.elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(ProcessModelSection), new ValidatorCallback(ProcessModelSection.ValidateElement)));
		}

		// Token: 0x06003F85 RID: 16261 RVA: 0x0000393A File Offset: 0x00001B3A
		private static void ValidateElement(object o)
		{
		}

		// Token: 0x17001401 RID: 5121
		// (get) Token: 0x06003F86 RID: 16262 RVA: 0x000A84D4 File Offset: 0x000A66D4
		protected internal override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return ProcessModelSection.elementProperty;
			}
		}

		/// <summary>Gets or sets a value indicating whether ASP.NET performance settings are automatically configured for ASP.NET applications. </summary>
		/// <returns>true if performance settings are automatically configured for ASP.NET applications; otherwise, false. The default value is false.</returns>
		// Token: 0x17001402 RID: 5122
		// (get) Token: 0x06003F87 RID: 16263 RVA: 0x000A84DB File Offset: 0x000A66DB
		// (set) Token: 0x06003F88 RID: 16264 RVA: 0x000A84ED File Offset: 0x000A66ED
		[ConfigurationProperty("autoConfig", DefaultValue = "False")]
		public bool AutoConfig
		{
			get
			{
				return (bool)base[ProcessModelSection.autoConfigProp];
			}
			set
			{
				base[ProcessModelSection.autoConfigProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating how long a request is left in the queue. </summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value indicating the queuing time. The default value is 5 seconds.</returns>
		// Token: 0x17001403 RID: 5123
		// (get) Token: 0x06003F89 RID: 16265 RVA: 0x000A8500 File Offset: 0x000A6700
		// (set) Token: 0x06003F8A RID: 16266 RVA: 0x000A8512 File Offset: 0x000A6712
		[ConfigurationProperty("clientConnectedCheck", DefaultValue = "00:00:05")]
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		public TimeSpan ClientConnectedCheck
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection.clientConnectedCheckProp];
			}
			set
			{
				base[ProcessModelSection.clientConnectedCheckProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the level of authentication for DCOM security.</summary>
		/// <returns>One of the <see cref="T:System.Web.Configuration.ProcessModelComAuthenticationLevel" /> values. The default value is <see cref="F:System.Web.Configuration.ProcessModelComAuthenticationLevel.Connect" />.</returns>
		// Token: 0x17001404 RID: 5124
		// (get) Token: 0x06003F8B RID: 16267 RVA: 0x000A8525 File Offset: 0x000A6725
		// (set) Token: 0x06003F8C RID: 16268 RVA: 0x000A8537 File Offset: 0x000A6737
		[ConfigurationProperty("comAuthenticationLevel", DefaultValue = "Connect")]
		public ProcessModelComAuthenticationLevel ComAuthenticationLevel
		{
			get
			{
				return (ProcessModelComAuthenticationLevel)base[ProcessModelSection.comAuthenticationLevelProp];
			}
			set
			{
				base[ProcessModelSection.comAuthenticationLevelProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the level of authentication for COM security.</summary>
		/// <returns>One of the <see cref="T:System.Web.Configuration.ProcessModelComImpersonationLevel" /> values. The default value is <see cref="F:System.Web.Configuration.ProcessModelComImpersonationLevel.Impersonate" />. </returns>
		// Token: 0x17001405 RID: 5125
		// (get) Token: 0x06003F8D RID: 16269 RVA: 0x000A854A File Offset: 0x000A674A
		// (set) Token: 0x06003F8E RID: 16270 RVA: 0x000A855C File Offset: 0x000A675C
		[ConfigurationProperty("comImpersonationLevel", DefaultValue = "Impersonate")]
		public ProcessModelComImpersonationLevel ComImpersonationLevel
		{
			get
			{
				return (ProcessModelComImpersonationLevel)base[ProcessModelSection.comImpersonationLevelProp];
			}
			set
			{
				base[ProcessModelSection.comImpersonationLevelProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating which processors on a multiprocessor server are eligible to run ASP.NET processes. </summary>
		/// <returns>The number representing the bit pattern to apply. The default value is 0xFFFFFFFF.</returns>
		// Token: 0x17001406 RID: 5126
		// (get) Token: 0x06003F8F RID: 16271 RVA: 0x000A856F File Offset: 0x000A676F
		// (set) Token: 0x06003F90 RID: 16272 RVA: 0x000A8581 File Offset: 0x000A6781
		[ConfigurationProperty("cpuMask", DefaultValue = "0xffffffff")]
		public int CpuMask
		{
			get
			{
				return (int)base[ProcessModelSection.cpuMaskProp];
			}
			set
			{
				base[ProcessModelSection.cpuMaskProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the process model is enabled.</summary>
		/// <returns>true if the process model is enabled; otherwise, false. The default value is true.</returns>
		// Token: 0x17001407 RID: 5127
		// (get) Token: 0x06003F91 RID: 16273 RVA: 0x000A8594 File Offset: 0x000A6794
		// (set) Token: 0x06003F92 RID: 16274 RVA: 0x000A85A6 File Offset: 0x000A67A6
		[ConfigurationProperty("enable", DefaultValue = "True")]
		public bool Enable
		{
			get
			{
				return (bool)base[ProcessModelSection.enableProp];
			}
			set
			{
				base[ProcessModelSection.enableProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the period of inactivity after which ASP.NET automatically ends the worker process.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value indicating the idle time. The default value is Infinite, which corresponds to <see cref="F:System.TimeSpan.MaxValue" />. </returns>
		// Token: 0x17001408 RID: 5128
		// (get) Token: 0x06003F93 RID: 16275 RVA: 0x000A85B9 File Offset: 0x000A67B9
		// (set) Token: 0x06003F94 RID: 16276 RVA: 0x000A85CB File Offset: 0x000A67CB
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		[ConfigurationProperty("idleTimeout", DefaultValue = "10675199.02:48:05.4775807")]
		public TimeSpan IdleTimeout
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection.idleTimeoutProp];
			}
			set
			{
				base[ProcessModelSection.idleTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the event types to be logged to the event log.</summary>
		/// <returns>One of the <see cref="T:System.Web.Configuration.ProcessModelLogLevel" /> values. The default value is <see cref="F:System.Web.Configuration.ProcessModelLogLevel.Errors" />. </returns>
		// Token: 0x17001409 RID: 5129
		// (get) Token: 0x06003F95 RID: 16277 RVA: 0x000A85DE File Offset: 0x000A67DE
		// (set) Token: 0x06003F96 RID: 16278 RVA: 0x000A85F0 File Offset: 0x000A67F0
		[ConfigurationProperty("logLevel", DefaultValue = "Errors")]
		public ProcessModelLogLevel LogLevel
		{
			get
			{
				return (ProcessModelLogLevel)base[ProcessModelSection.logLevelProp];
			}
			set
			{
				base[ProcessModelSection.logLevelProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum allowed number of application domains in one process.</summary>
		/// <returns>The maximum allowed number of application domains in one process.</returns>
		// Token: 0x1700140A RID: 5130
		// (get) Token: 0x06003F97 RID: 16279 RVA: 0x000A8603 File Offset: 0x000A6803
		// (set) Token: 0x06003F98 RID: 16280 RVA: 0x000A8615 File Offset: 0x000A6815
		[ConfigurationProperty("maxAppDomains", DefaultValue = "2000")]
		[IntegerValidator(MinValue = 1, MaxValue = 2147483646)]
		public int MaxAppDomains
		{
			get
			{
				return (int)base[ProcessModelSection.maxAppDomainsProp];
			}
			set
			{
				base[ProcessModelSection.maxAppDomainsProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the maximum number of I/O threads per CPU in the CLR thread pool. </summary>
		/// <returns>The maximum number of threads. The default is 20.</returns>
		// Token: 0x1700140B RID: 5131
		// (get) Token: 0x06003F99 RID: 16281 RVA: 0x000A8628 File Offset: 0x000A6828
		// (set) Token: 0x06003F9A RID: 16282 RVA: 0x000A863A File Offset: 0x000A683A
		[ConfigurationProperty("maxIoThreads", DefaultValue = "20")]
		[IntegerValidator(MinValue = 1, MaxValue = 2147483646)]
		public int MaxIOThreads
		{
			get
			{
				return (int)base[ProcessModelSection.maxIoThreadsProp];
			}
			set
			{
				base[ProcessModelSection.maxIoThreadsProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the maximum amount of worker threads per CPU in the CLR thread pool. </summary>
		/// <returns>The maximum number of threads. The default is 20.</returns>
		// Token: 0x1700140C RID: 5132
		// (get) Token: 0x06003F9B RID: 16283 RVA: 0x000A864D File Offset: 0x000A684D
		// (set) Token: 0x06003F9C RID: 16284 RVA: 0x000A865F File Offset: 0x000A685F
		[IntegerValidator(MinValue = 1, MaxValue = 2147483646)]
		[ConfigurationProperty("maxWorkerThreads", DefaultValue = "20")]
		public int MaxWorkerThreads
		{
			get
			{
				return (int)base[ProcessModelSection.maxWorkerThreadsProp];
			}
			set
			{
				base[ProcessModelSection.maxWorkerThreadsProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the maximum allowed memory size.</summary>
		/// <returns>The percentage of the total system memory. The default is 60 percent. </returns>
		// Token: 0x1700140D RID: 5133
		// (get) Token: 0x06003F9D RID: 16285 RVA: 0x000A8672 File Offset: 0x000A6872
		// (set) Token: 0x06003F9E RID: 16286 RVA: 0x000A8684 File Offset: 0x000A6884
		[ConfigurationProperty("memoryLimit", DefaultValue = "60")]
		public int MemoryLimit
		{
			get
			{
				return (int)base[ProcessModelSection.memoryLimitProp];
			}
			set
			{
				base[ProcessModelSection.memoryLimitProp] = value;
			}
		}

		/// <summary>Gets or sets the minimum number of I/O threads per CPU in the CLR thread pool.</summary>
		/// <returns>The minimum number of I/O threads per CPU in the CLR thread pool.</returns>
		// Token: 0x1700140E RID: 5134
		// (get) Token: 0x06003F9F RID: 16287 RVA: 0x000A8697 File Offset: 0x000A6897
		// (set) Token: 0x06003FA0 RID: 16288 RVA: 0x000A86A9 File Offset: 0x000A68A9
		[IntegerValidator(MinValue = 1, MaxValue = 2147483646)]
		[ConfigurationProperty("minIoThreads", DefaultValue = "1")]
		public int MinIOThreads
		{
			get
			{
				return (int)base[ProcessModelSection.minIoThreadsProp];
			}
			set
			{
				base[ProcessModelSection.minIoThreadsProp] = value;
			}
		}

		/// <summary>Gets or sets the minimum number of worker threads per CPU in the CLR thread pool.</summary>
		/// <returns>The minimum number of worker threads per CPU in the CLR thread pool</returns>
		// Token: 0x1700140F RID: 5135
		// (get) Token: 0x06003FA1 RID: 16289 RVA: 0x000A86BC File Offset: 0x000A68BC
		// (set) Token: 0x06003FA2 RID: 16290 RVA: 0x000A86CE File Offset: 0x000A68CE
		[IntegerValidator(MinValue = 1, MaxValue = 2147483646)]
		[ConfigurationProperty("minWorkerThreads", DefaultValue = "1")]
		public int MinWorkerThreads
		{
			get
			{
				return (int)base[ProcessModelSection.minWorkerThreadsProp];
			}
			set
			{
				base[ProcessModelSection.minWorkerThreadsProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the password to use for the Windows identity.</summary>
		/// <returns>The password to use. The default value is AutoGenerate.</returns>
		// Token: 0x17001410 RID: 5136
		// (get) Token: 0x06003FA3 RID: 16291 RVA: 0x000A86E1 File Offset: 0x000A68E1
		// (set) Token: 0x06003FA4 RID: 16292 RVA: 0x000A86F3 File Offset: 0x000A68F3
		[ConfigurationProperty("password", DefaultValue = "AutoGenerate")]
		public string Password
		{
			get
			{
				return (string)base[ProcessModelSection.passwordProp];
			}
			set
			{
				base[ProcessModelSection.passwordProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the time interval at which the ISAPI extension pings the worker process to determine whether it is running.</summary>
		/// <returns>The <see cref="T:System.TimeSpan" /> defining the time interval. The default is 30 seconds.</returns>
		// Token: 0x17001411 RID: 5137
		// (get) Token: 0x06003FA5 RID: 16293 RVA: 0x000A8701 File Offset: 0x000A6901
		// (set) Token: 0x06003FA6 RID: 16294 RVA: 0x000A8713 File Offset: 0x000A6913
		[ConfigurationProperty("pingFrequency", DefaultValue = "10675199.02:48:05.4775807")]
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		public TimeSpan PingFrequency
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection.pingFrequencyProp];
			}
			set
			{
				base[ProcessModelSection.pingFrequencyProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the time interval after which a non-responsive worker process is restarted.</summary>
		/// <returns>The <see cref="T:System.TimeSpan" /> defining the time interval. The default is 5 seconds.</returns>
		// Token: 0x17001412 RID: 5138
		// (get) Token: 0x06003FA7 RID: 16295 RVA: 0x000A8726 File Offset: 0x000A6926
		// (set) Token: 0x06003FA8 RID: 16296 RVA: 0x000A8738 File Offset: 0x000A6938
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		[ConfigurationProperty("pingTimeout", DefaultValue = "10675199.02:48:05.4775807")]
		public TimeSpan PingTimeout
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection.pingTimeoutProp];
			}
			set
			{
				base[ProcessModelSection.pingTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the number of requests allowed before a worker process is recycled.</summary>
		/// <returns>The number of allowed requests. The default is Infinite.</returns>
		// Token: 0x17001413 RID: 5139
		// (get) Token: 0x06003FA9 RID: 16297 RVA: 0x000A874B File Offset: 0x000A694B
		// (set) Token: 0x06003FAA RID: 16298 RVA: 0x000A875D File Offset: 0x000A695D
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		[ConfigurationProperty("requestLimit", DefaultValue = "2147483647")]
		[TypeConverter(typeof(InfiniteIntConverter))]
		public int RequestLimit
		{
			get
			{
				return (int)base[ProcessModelSection.requestLimitProp];
			}
			set
			{
				base[ProcessModelSection.requestLimitProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the number of requests allowed in the queue.</summary>
		/// <returns>The number of requests allowed to be queued. The default is 5000.</returns>
		// Token: 0x17001414 RID: 5140
		// (get) Token: 0x06003FAB RID: 16299 RVA: 0x000A8770 File Offset: 0x000A6970
		// (set) Token: 0x06003FAC RID: 16300 RVA: 0x000A8782 File Offset: 0x000A6982
		[ConfigurationProperty("requestQueueLimit", DefaultValue = "5000")]
		[TypeConverter(typeof(InfiniteIntConverter))]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		public int RequestQueueLimit
		{
			get
			{
				return (int)base[ProcessModelSection.requestQueueLimitProp];
			}
			set
			{
				base[ProcessModelSection.requestQueueLimitProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the time interval for the worker process to respond.</summary>
		/// <returns>The <see cref="T:System.TimeSpan" /> defining the interval. The default is 3 minutes.</returns>
		// Token: 0x17001415 RID: 5141
		// (get) Token: 0x06003FAD RID: 16301 RVA: 0x000A8795 File Offset: 0x000A6995
		// (set) Token: 0x06003FAE RID: 16302 RVA: 0x000A87A7 File Offset: 0x000A69A7
		[ConfigurationProperty("responseDeadlockInterval", DefaultValue = "00:03:00")]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		public TimeSpan ResponseDeadlockInterval
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection.responseDeadlockIntervalProp];
			}
			set
			{
				base[ProcessModelSection.responseDeadlockIntervalProp] = value;
			}
		}

		/// <summary>No longer used.</summary>
		/// <returns>Not applicable.</returns>
		// Token: 0x17001416 RID: 5142
		// (get) Token: 0x06003FAF RID: 16303 RVA: 0x000A87BA File Offset: 0x000A69BA
		// (set) Token: 0x06003FB0 RID: 16304 RVA: 0x000A87CC File Offset: 0x000A69CC
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		[ConfigurationProperty("responseRestartDeadlockInterval", DefaultValue = "00:03:00")]
		public TimeSpan ResponseRestartDeadlockInterval
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection.responseRestartDeadlockIntervalProp];
			}
			set
			{
				base[ProcessModelSection.responseRestartDeadlockIntervalProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the maximum number of requests queued by the ISAPI while waiting for a new worker process to start handling the requests.</summary>
		/// <returns>The number of requests queued. The default is 10. </returns>
		// Token: 0x17001417 RID: 5143
		// (get) Token: 0x06003FB1 RID: 16305 RVA: 0x000A87DF File Offset: 0x000A69DF
		// (set) Token: 0x06003FB2 RID: 16306 RVA: 0x000A87F1 File Offset: 0x000A69F1
		[TypeConverter(typeof(InfiniteIntConverter))]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		[ConfigurationProperty("restartQueueLimit", DefaultValue = "10")]
		public int RestartQueueLimit
		{
			get
			{
				return (int)base[ProcessModelSection.restartQueueLimitProp];
			}
			set
			{
				base[ProcessModelSection.restartQueueLimitProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the file whose content must be used when a fatal error occurs.</summary>
		/// <returns>The path of the file used when a fatal error occurs.</returns>
		// Token: 0x17001418 RID: 5144
		// (get) Token: 0x06003FB3 RID: 16307 RVA: 0x000A8804 File Offset: 0x000A6A04
		// (set) Token: 0x06003FB4 RID: 16308 RVA: 0x000A8816 File Offset: 0x000A6A16
		[ConfigurationProperty("serverErrorMessageFile", DefaultValue = "")]
		public string ServerErrorMessageFile
		{
			get
			{
				return (string)base[ProcessModelSection.serverErrorMessageFileProp];
			}
			set
			{
				base[ProcessModelSection.serverErrorMessageFileProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the time allowed for the worker process to shut down. </summary>
		/// <returns>The <see cref="T:System.TimeSpan" /> defining the interval. The default is 5 seconds.</returns>
		// Token: 0x17001419 RID: 5145
		// (get) Token: 0x06003FB5 RID: 16309 RVA: 0x000A8824 File Offset: 0x000A6A24
		// (set) Token: 0x06003FB6 RID: 16310 RVA: 0x000A8836 File Offset: 0x000A6A36
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		[ConfigurationProperty("shutdownTimeout", DefaultValue = "00:00:05")]
		public TimeSpan ShutdownTimeout
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection.shutdownTimeoutProp];
			}
			set
			{
				base[ProcessModelSection.shutdownTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the number of minutes until ASP.NET launches a new worker process.</summary>
		/// <returns>The <see cref="T:System.TimeSpan" /> defining the interval. The default is Infinite.</returns>
		// Token: 0x1700141A RID: 5146
		// (get) Token: 0x06003FB7 RID: 16311 RVA: 0x000A8849 File Offset: 0x000A6A49
		// (set) Token: 0x06003FB8 RID: 16312 RVA: 0x000A885B File Offset: 0x000A6A5B
		[ConfigurationProperty("timeout", DefaultValue = "10675199.02:48:05.4775807")]
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		public TimeSpan Timeout
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection.timeoutProp];
			}
			set
			{
				base[ProcessModelSection.timeoutProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the user name for a Windows identity.</summary>
		/// <returns>The user name. The default value is Machine. </returns>
		// Token: 0x1700141B RID: 5147
		// (get) Token: 0x06003FB9 RID: 16313 RVA: 0x000A886E File Offset: 0x000A6A6E
		// (set) Token: 0x06003FBA RID: 16314 RVA: 0x000A8880 File Offset: 0x000A6A80
		[ConfigurationProperty("userName", DefaultValue = "machine")]
		public string UserName
		{
			get
			{
				return (string)base[ProcessModelSection.userNameProp];
			}
			set
			{
				base[ProcessModelSection.userNameProp] = value;
			}
		}

		/// <summary>Gets or sets a value enabling the available CPUs to run the worker processes.</summary>
		/// <returns>true, if <see cref="P:System.Web.Configuration.ProcessModelSection.CpuMask" /> is used to map the worker processes to the number of eligible CPUs; false if <see cref="P:System.Web.Configuration.ProcessModelSection.CpuMask" /> is ignored.</returns>
		// Token: 0x1700141C RID: 5148
		// (get) Token: 0x06003FBB RID: 16315 RVA: 0x000A888E File Offset: 0x000A6A8E
		// (set) Token: 0x06003FBC RID: 16316 RVA: 0x000A88A0 File Offset: 0x000A6AA0
		[ConfigurationProperty("webGarden", DefaultValue = "False")]
		public bool WebGarden
		{
			get
			{
				return (bool)base[ProcessModelSection.webGardenProp];
			}
			set
			{
				base[ProcessModelSection.webGardenProp] = value;
			}
		}

		// Token: 0x1700141D RID: 5149
		// (get) Token: 0x06003FBD RID: 16317 RVA: 0x000A88B3 File Offset: 0x000A6AB3
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProcessModelSection.properties;
			}
		}

		// Token: 0x04002295 RID: 8853
		private static ConfigurationProperty autoConfigProp = new ConfigurationProperty("autoConfig", typeof(bool), false);

		// Token: 0x04002296 RID: 8854
		private static ConfigurationProperty clientConnectedCheckProp = new ConfigurationProperty("clientConnectedCheck", typeof(TimeSpan), TimeSpan.FromSeconds(5.0), PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002297 RID: 8855
		private static ConfigurationProperty comAuthenticationLevelProp = new ConfigurationProperty("comAuthenticationLevel", typeof(ProcessModelComAuthenticationLevel), ProcessModelComAuthenticationLevel.Connect, new GenericEnumConverter(typeof(ProcessModelComAuthenticationLevel)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002298 RID: 8856
		private static ConfigurationProperty comImpersonationLevelProp = new ConfigurationProperty("comImpersonationLevel", typeof(ProcessModelComImpersonationLevel), ProcessModelComImpersonationLevel.Impersonate, new GenericEnumConverter(typeof(ProcessModelComImpersonationLevel)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002299 RID: 8857
		private static ConfigurationProperty cpuMaskProp = new ConfigurationProperty("cpuMask", typeof(int), 268435455);

		// Token: 0x0400229A RID: 8858
		private static ConfigurationProperty enableProp = new ConfigurationProperty("enable", typeof(bool), true);

		// Token: 0x0400229B RID: 8859
		private static ConfigurationProperty idleTimeoutProp = new ConfigurationProperty("idleTimeout", typeof(TimeSpan), TimeSpan.MaxValue, PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400229C RID: 8860
		private static ConfigurationProperty logLevelProp = new ConfigurationProperty("logLevel", typeof(ProcessModelLogLevel), ProcessModelLogLevel.Errors, new GenericEnumConverter(typeof(ProcessModelLogLevel)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400229D RID: 8861
		private static ConfigurationProperty maxAppDomainsProp = new ConfigurationProperty("maxAppDomains", typeof(int), 2000, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromOneToMax_1Validator, ConfigurationPropertyOptions.None);

		// Token: 0x0400229E RID: 8862
		private static ConfigurationProperty maxIoThreadsProp = new ConfigurationProperty("maxIoThreads", typeof(int), 20, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromOneToMax_1Validator, ConfigurationPropertyOptions.None);

		// Token: 0x0400229F RID: 8863
		private static ConfigurationProperty maxWorkerThreadsProp = new ConfigurationProperty("maxWorkerThreads", typeof(int), 20, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromOneToMax_1Validator, ConfigurationPropertyOptions.None);

		// Token: 0x040022A0 RID: 8864
		private static ConfigurationProperty memoryLimitProp = new ConfigurationProperty("memoryLimit", typeof(int), 60);

		// Token: 0x040022A1 RID: 8865
		private static ConfigurationProperty minIoThreadsProp = new ConfigurationProperty("minIoThreads", typeof(int), 1, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromOneToMax_1Validator, ConfigurationPropertyOptions.None);

		// Token: 0x040022A2 RID: 8866
		private static ConfigurationProperty minWorkerThreadsProp = new ConfigurationProperty("minWorkerThreads", typeof(int), 1, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromOneToMax_1Validator, ConfigurationPropertyOptions.None);

		// Token: 0x040022A3 RID: 8867
		private static ConfigurationProperty passwordProp = new ConfigurationProperty("password", typeof(string), "AutoGenerate");

		// Token: 0x040022A4 RID: 8868
		private static ConfigurationProperty pingFrequencyProp = new ConfigurationProperty("pingFrequency", typeof(TimeSpan), TimeSpan.MaxValue, PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022A5 RID: 8869
		private static ConfigurationProperty pingTimeoutProp = new ConfigurationProperty("pingTimeout", typeof(TimeSpan), TimeSpan.MaxValue, PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022A6 RID: 8870
		private static ConfigurationProperty requestLimitProp = new ConfigurationProperty("requestLimit", typeof(int), int.MaxValue, PropertyHelper.InfiniteIntConverter, PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022A7 RID: 8871
		private static ConfigurationProperty requestQueueLimitProp = new ConfigurationProperty("requestQueueLimit", typeof(int), 5000, PropertyHelper.InfiniteIntConverter, PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022A8 RID: 8872
		private static ConfigurationProperty responseDeadlockIntervalProp = new ConfigurationProperty("responseDeadlockInterval", typeof(TimeSpan), TimeSpan.FromMinutes(3.0), PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022A9 RID: 8873
		private static ConfigurationProperty responseRestartDeadlockIntervalProp = new ConfigurationProperty("responseRestartDeadlockInterval", typeof(TimeSpan), TimeSpan.FromMinutes(3.0), PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022AA RID: 8874
		private static ConfigurationProperty restartQueueLimitProp = new ConfigurationProperty("restartQueueLimit", typeof(int), 10, PropertyHelper.InfiniteIntConverter, PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022AB RID: 8875
		private static ConfigurationProperty serverErrorMessageFileProp = new ConfigurationProperty("serverErrorMessageFile", typeof(string), "");

		// Token: 0x040022AC RID: 8876
		private static ConfigurationProperty shutdownTimeoutProp = new ConfigurationProperty("shutdownTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(5.0), PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022AD RID: 8877
		private static ConfigurationProperty timeoutProp = new ConfigurationProperty("timeout", typeof(TimeSpan), TimeSpan.MaxValue, PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022AE RID: 8878
		private static ConfigurationProperty userNameProp = new ConfigurationProperty("userName", typeof(string), "machine");

		// Token: 0x040022AF RID: 8879
		private static ConfigurationProperty webGardenProp = new ConfigurationProperty("webGarden", typeof(bool), false);

		// Token: 0x040022B0 RID: 8880
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040022B1 RID: 8881
		private static ConfigurationElementProperty elementProperty;
	}
}
