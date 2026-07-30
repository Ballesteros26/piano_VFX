using System;
using System.ComponentModel;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Configures the ASP.NET HTTP runtime. This class cannot be inherited.</summary>
	// Token: 0x020005B2 RID: 1458
	public sealed class HttpRuntimeSection : ConfigurationSection
	{
		// Token: 0x06003E5E RID: 15966 RVA: 0x000A52E0 File Offset: 0x000A34E0
		static HttpRuntimeSection()
		{
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.apartmentThreadingProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.appRequestQueueLimitProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.delayNotificationTimeoutProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.enableProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.enableHeaderCheckingProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.enableKernelOutputCacheProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.enableVersionHeaderProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.executionTimeoutProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.maxRequestLengthProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.maxWaitChangeNotificationProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.minFreeThreadsProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.minLocalRequestFreeThreadsProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.requestLengthDiskThresholdProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.requireRootedSaveAsPathProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.sendCacheControlHeaderProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.shutdownTimeoutProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.useFullyQualifiedRedirectUrlProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.waitChangeNotificationProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.requestPathInvalidCharactersProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.requestValidationTypeProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.requestValidationModeProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.maxQueryStringLengthProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.maxUrlLengthProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.encoderTypeProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.relaxedUrlToFileSystemMappingProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.targetFrameworkProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.allowDynamicModuleRegistrationProp);
		}

		/// <summary>Gets or sets a value that indicates whether application apartment threading is enabled.</summary>
		/// <returns>true if application apartment threading is enabled; otherwise, false. </returns>
		// Token: 0x1700138B RID: 5003
		// (get) Token: 0x06003E60 RID: 15968 RVA: 0x000A5933 File Offset: 0x000A3B33
		// (set) Token: 0x06003E61 RID: 15969 RVA: 0x000A5945 File Offset: 0x000A3B45
		[ConfigurationProperty("apartmentThreading", DefaultValue = "False")]
		public bool ApartmentThreading
		{
			get
			{
				return (bool)base[HttpRuntimeSection.apartmentThreadingProp];
			}
			set
			{
				base[HttpRuntimeSection.apartmentThreadingProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates the maximum number of requests that ASP.NET queues for the application.</summary>
		/// <returns>The maximum number of requests that can be queued.</returns>
		// Token: 0x1700138C RID: 5004
		// (get) Token: 0x06003E62 RID: 15970 RVA: 0x000A5958 File Offset: 0x000A3B58
		// (set) Token: 0x06003E63 RID: 15971 RVA: 0x000A596A File Offset: 0x000A3B6A
		[ConfigurationProperty("appRequestQueueLimit", DefaultValue = "5000")]
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647)]
		public int AppRequestQueueLimit
		{
			get
			{
				return (int)base[HttpRuntimeSection.appRequestQueueLimitProp];
			}
			set
			{
				base[HttpRuntimeSection.appRequestQueueLimitProp] = value;
			}
		}

		/// <summary>Gets or sets the change notification delay.</summary>
		/// <returns>The time, in seconds, that specifies the change notification delay.</returns>
		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x06003E64 RID: 15972 RVA: 0x000A597D File Offset: 0x000A3B7D
		// (set) Token: 0x06003E65 RID: 15973 RVA: 0x000A598F File Offset: 0x000A3B8F
		[ConfigurationProperty("delayNotificationTimeout", DefaultValue = "00:00:05")]
		[TypeConverter(typeof(TimeSpanSecondsConverter))]
		public TimeSpan DelayNotificationTimeout
		{
			get
			{
				return (TimeSpan)base[HttpRuntimeSection.delayNotificationTimeoutProp];
			}
			set
			{
				base[HttpRuntimeSection.delayNotificationTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the application domain is enabled.</summary>
		/// <returns>true if the application domain is enabled; otherwise, false. The default value is true.</returns>
		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x06003E66 RID: 15974 RVA: 0x000A59A2 File Offset: 0x000A3BA2
		// (set) Token: 0x06003E67 RID: 15975 RVA: 0x000A59B4 File Offset: 0x000A3BB4
		[ConfigurationProperty("enable", DefaultValue = "True")]
		public bool Enable
		{
			get
			{
				return (bool)base[HttpRuntimeSection.enableProp];
			}
			set
			{
				base[HttpRuntimeSection.enableProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the header checking is enabled.</summary>
		/// <returns>true if the header checking is enabled; otherwise, false. The default value is true. </returns>
		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x06003E68 RID: 15976 RVA: 0x000A59C7 File Offset: 0x000A3BC7
		// (set) Token: 0x06003E69 RID: 15977 RVA: 0x000A59D9 File Offset: 0x000A3BD9
		[ConfigurationProperty("enableHeaderChecking", DefaultValue = "True")]
		public bool EnableHeaderChecking
		{
			get
			{
				return (bool)base[HttpRuntimeSection.enableHeaderCheckingProp];
			}
			set
			{
				base[HttpRuntimeSection.enableHeaderCheckingProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether output caching is enabled.</summary>
		/// <returns>true if output caching is enabled; otherwise, false. The default value is true. </returns>
		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x06003E6A RID: 15978 RVA: 0x000A59EC File Offset: 0x000A3BEC
		// (set) Token: 0x06003E6B RID: 15979 RVA: 0x000A59FE File Offset: 0x000A3BFE
		[ConfigurationProperty("enableKernelOutputCache", DefaultValue = "True")]
		public bool EnableKernelOutputCache
		{
			get
			{
				return (bool)base[HttpRuntimeSection.enableKernelOutputCacheProp];
			}
			set
			{
				base[HttpRuntimeSection.enableKernelOutputCacheProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether ASP.NET should output a version header.</summary>
		/// <returns>true if the output of the version header is enabled; otherwise, false. The default value is true.</returns>
		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x06003E6C RID: 15980 RVA: 0x000A5A11 File Offset: 0x000A3C11
		// (set) Token: 0x06003E6D RID: 15981 RVA: 0x000A5A23 File Offset: 0x000A3C23
		[ConfigurationProperty("enableVersionHeader", DefaultValue = "True")]
		public bool EnableVersionHeader
		{
			get
			{
				return (bool)base[HttpRuntimeSection.enableVersionHeaderProp];
			}
			set
			{
				base[HttpRuntimeSection.enableVersionHeaderProp] = value;
			}
		}

		/// <summary>Gets or sets the allowed execution time for the request.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that indicates the allowed execution time for the request.</returns>
		/// <exception cref="T:System.Web.HttpException">The request execution time exceeded the limit set by the execution time-out.</exception>
		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x06003E6E RID: 15982 RVA: 0x000A5A36 File Offset: 0x000A3C36
		// (set) Token: 0x06003E6F RID: 15983 RVA: 0x000A5A48 File Offset: 0x000A3C48
		[TimeSpanValidator(MinValueString = "00:00:00")]
		[ConfigurationProperty("executionTimeout", DefaultValue = "00:01:50")]
		[TypeConverter(typeof(TimeSpanSecondsConverter))]
		public TimeSpan ExecutionTimeout
		{
			get
			{
				return (TimeSpan)base[HttpRuntimeSection.executionTimeoutProp];
			}
			set
			{
				base[HttpRuntimeSection.executionTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum request size.</summary>
		/// <returns>The maximum request size in kilobytes. The default size is 4096 KB (4 MB).</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value is less than <see cref="P:System.Web.Configuration.HttpRuntimeSection.RequestLengthDiskThreshold" />.</exception>
		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x06003E70 RID: 15984 RVA: 0x000A5A5B File Offset: 0x000A3C5B
		// (set) Token: 0x06003E71 RID: 15985 RVA: 0x000A5A6D File Offset: 0x000A3C6D
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		[ConfigurationProperty("maxRequestLength", DefaultValue = "4096")]
		public int MaxRequestLength
		{
			get
			{
				return (int)base[HttpRuntimeSection.maxRequestLengthProp];
			}
			set
			{
				base[HttpRuntimeSection.maxRequestLengthProp] = value;
			}
		}

		/// <summary>Gets or sets the time interval between the first change notification and the time at which the application domain is restarted.</summary>
		/// <returns>The maximum time interval, in seconds, from the first change notification and the time when the application domain is restarted.</returns>
		// Token: 0x17001394 RID: 5012
		// (get) Token: 0x06003E72 RID: 15986 RVA: 0x000A5A80 File Offset: 0x000A3C80
		// (set) Token: 0x06003E73 RID: 15987 RVA: 0x000A5A92 File Offset: 0x000A3C92
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		[ConfigurationProperty("maxWaitChangeNotification", DefaultValue = "0")]
		public int MaxWaitChangeNotification
		{
			get
			{
				return (int)base[HttpRuntimeSection.maxWaitChangeNotificationProp];
			}
			set
			{
				base[HttpRuntimeSection.maxWaitChangeNotificationProp] = value;
			}
		}

		/// <summary>Gets or sets the minimum number of threads that must be free before a request for resources in this configuration scope can be serviced.</summary>
		/// <returns>The minimum number of free threads in the common language runtime (CLR) thread pool before a request in this configuration scope will be serviced. The default value is 8.</returns>
		// Token: 0x17001395 RID: 5013
		// (get) Token: 0x06003E74 RID: 15988 RVA: 0x000A5AA5 File Offset: 0x000A3CA5
		// (set) Token: 0x06003E75 RID: 15989 RVA: 0x000A5AB7 File Offset: 0x000A3CB7
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		[ConfigurationProperty("minFreeThreads", DefaultValue = "8")]
		public int MinFreeThreads
		{
			get
			{
				return (int)base[HttpRuntimeSection.minFreeThreadsProp];
			}
			set
			{
				base[HttpRuntimeSection.minFreeThreadsProp] = value;
			}
		}

		/// <summary>Gets or sets the minimum number of free threads required to service a local request.</summary>
		/// <returns>The minimum number of free threads assigned to local requests. The default value is 4.</returns>
		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x06003E76 RID: 15990 RVA: 0x000A5ACA File Offset: 0x000A3CCA
		// (set) Token: 0x06003E77 RID: 15991 RVA: 0x000A5ADC File Offset: 0x000A3CDC
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		[ConfigurationProperty("minLocalRequestFreeThreads", DefaultValue = "4")]
		public int MinLocalRequestFreeThreads
		{
			get
			{
				return (int)base[HttpRuntimeSection.minLocalRequestFreeThreadsProp];
			}
			set
			{
				base[HttpRuntimeSection.minLocalRequestFreeThreadsProp] = value;
			}
		}

		/// <summary>Gets or sets the input-stream buffering threshold.</summary>
		/// <returns>The number of bytes that indicate the input-stream buffering threshold. The default is 80 kilobytes.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value is greater than <see cref="P:System.Web.Configuration.HttpRuntimeSection.MaxRequestLength" />.</exception>
		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x06003E78 RID: 15992 RVA: 0x000A5AEF File Offset: 0x000A3CEF
		// (set) Token: 0x06003E79 RID: 15993 RVA: 0x000A5B01 File Offset: 0x000A3D01
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647)]
		[ConfigurationProperty("requestLengthDiskThreshold", DefaultValue = "80")]
		public int RequestLengthDiskThreshold
		{
			get
			{
				return (int)base[HttpRuntimeSection.requestLengthDiskThresholdProp];
			}
			set
			{
				base[HttpRuntimeSection.requestLengthDiskThresholdProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the file name must be a fully qualified physical file path.</summary>
		/// <returns>true if the file name must be a fully qualified physical file path; otherwise, false. The default value is true.</returns>
		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x06003E7A RID: 15994 RVA: 0x000A5B14 File Offset: 0x000A3D14
		// (set) Token: 0x06003E7B RID: 15995 RVA: 0x000A5B26 File Offset: 0x000A3D26
		[ConfigurationProperty("requireRootedSaveAsPath", DefaultValue = "True")]
		public bool RequireRootedSaveAsPath
		{
			get
			{
				return (bool)base[HttpRuntimeSection.requireRootedSaveAsPathProp];
			}
			set
			{
				base[HttpRuntimeSection.requireRootedSaveAsPathProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the cache-control:private header is sent as part of the HTTP response.</summary>
		/// <returns>true if the cache-control:private header is to be sent; otherwise, false. The default value is false.</returns>
		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x06003E7C RID: 15996 RVA: 0x000A5B39 File Offset: 0x000A3D39
		// (set) Token: 0x06003E7D RID: 15997 RVA: 0x000A5B4B File Offset: 0x000A3D4B
		[ConfigurationProperty("sendCacheControlHeader", DefaultValue = "True")]
		public bool SendCacheControlHeader
		{
			get
			{
				return (bool)base[HttpRuntimeSection.sendCacheControlHeaderProp];
			}
			set
			{
				base[HttpRuntimeSection.sendCacheControlHeaderProp] = value;
			}
		}

		/// <summary>Gets or sets the length of time the application is allowed to idle before it is terminated.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that indicates the length of time the application is allowed to idle before it is terminated.</returns>
		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x06003E7E RID: 15998 RVA: 0x000A5B5E File Offset: 0x000A3D5E
		// (set) Token: 0x06003E7F RID: 15999 RVA: 0x000A5B70 File Offset: 0x000A3D70
		[ConfigurationProperty("shutdownTimeout", DefaultValue = "00:01:30")]
		[TypeConverter(typeof(TimeSpanSecondsConverter))]
		public TimeSpan ShutdownTimeout
		{
			get
			{
				return (TimeSpan)base[HttpRuntimeSection.shutdownTimeoutProp];
			}
			set
			{
				base[HttpRuntimeSection.shutdownTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the client-side redirects are fully qualified.</summary>
		/// <returns>true if client-side redirects are fully qualified; otherwise, false. The default value is false.</returns>
		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x06003E80 RID: 16000 RVA: 0x000A5B83 File Offset: 0x000A3D83
		// (set) Token: 0x06003E81 RID: 16001 RVA: 0x000A5B95 File Offset: 0x000A3D95
		[ConfigurationProperty("useFullyQualifiedRedirectUrl", DefaultValue = "False")]
		public bool UseFullyQualifiedRedirectUrl
		{
			get
			{
				return (bool)base[HttpRuntimeSection.useFullyQualifiedRedirectUrlProp];
			}
			set
			{
				base[HttpRuntimeSection.useFullyQualifiedRedirectUrlProp] = value;
			}
		}

		/// <summary>Gets or sets the waiting time before the next change notification.</summary>
		/// <returns>The waiting time, in seconds, before the next change notification that triggers an application domain to restart. The default value is 0.</returns>
		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x06003E82 RID: 16002 RVA: 0x000A5BA8 File Offset: 0x000A3DA8
		// (set) Token: 0x06003E83 RID: 16003 RVA: 0x000A5BBA File Offset: 0x000A3DBA
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		[ConfigurationProperty("waitChangeNotification", DefaultValue = "0")]
		public int WaitChangeNotification
		{
			get
			{
				return (int)base[HttpRuntimeSection.waitChangeNotificationProp];
			}
			set
			{
				base[HttpRuntimeSection.waitChangeNotificationProp] = value;
			}
		}

		/// <summary>Gets or sets a list of characters that are specified as invalid in a path that is part of an HTTP request.</summary>
		/// <returns>A comma-separated list of invalid characters. The following list contains the default set of invalid characters: &lt;,&gt;,*,%,&amp;,:,\\</returns>
		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x06003E84 RID: 16004 RVA: 0x000A5BCD File Offset: 0x000A3DCD
		// (set) Token: 0x06003E85 RID: 16005 RVA: 0x000A5BDF File Offset: 0x000A3DDF
		[ConfigurationProperty("requestPathInvalidCharacters", DefaultValue = ",*,%,&,:,\\,?")]
		public string RequestPathInvalidCharacters
		{
			get
			{
				return (string)base[HttpRuntimeSection.requestPathInvalidCharactersProp];
			}
			set
			{
				base[HttpRuntimeSection.requestPathInvalidCharactersProp] = value;
			}
		}

		/// <summary>Gets or sets the name of a type that is used to validate HTTP requests.</summary>
		/// <returns>The name of a type that handles request validation tasks. The default is the fully qualified name of the <see cref="T:System.Web.Util.RequestValidator" /> type that ASP.NET uses for validation.</returns>
		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x06003E86 RID: 16006 RVA: 0x000A5BED File Offset: 0x000A3DED
		// (set) Token: 0x06003E87 RID: 16007 RVA: 0x000A5BFF File Offset: 0x000A3DFF
		[ConfigurationProperty("requestValidationType", DefaultValue = "System.Web.Util.RequestValidator")]
		[StringValidator(MinLength = 1)]
		public string RequestValidationType
		{
			get
			{
				return (string)base[HttpRuntimeSection.requestValidationTypeProp];
			}
			set
			{
				base[HttpRuntimeSection.requestValidationTypeProp] = value;
			}
		}

		/// <summary>Gets or sets a version number that indicates which ASP.NET version-specific approach to validation will be used.</summary>
		/// <returns>A value that indicates which ASP.NET version-specific approach to validation will be used. The default is 4.0. </returns>
		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x06003E88 RID: 16008 RVA: 0x000A5C0D File Offset: 0x000A3E0D
		// (set) Token: 0x06003E89 RID: 16009 RVA: 0x000A5C1F File Offset: 0x000A3E1F
		[ConfigurationProperty("requestValidationMode", DefaultValue = "4.0")]
		[TypeConverter("System.Web.Configuration.VersionConverter")]
		public Version RequestValidationMode
		{
			get
			{
				return (Version)base[HttpRuntimeSection.requestValidationModeProp];
			}
			set
			{
				base[HttpRuntimeSection.requestValidationModeProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum possible length, in number of characters, of a query string in an HTTP request.</summary>
		/// <returns>The maximum length of the query string, in number of characters. The default is 2048. </returns>
		// Token: 0x170013A0 RID: 5024
		// (get) Token: 0x06003E8A RID: 16010 RVA: 0x000A5C2D File Offset: 0x000A3E2D
		// (set) Token: 0x06003E8B RID: 16011 RVA: 0x000A5C3F File Offset: 0x000A3E3F
		[ConfigurationProperty("maxQueryStringLength", DefaultValue = "2048")]
		[IntegerValidator(MinValue = 0)]
		public int MaxQueryStringLength
		{
			get
			{
				return (int)base[HttpRuntimeSection.maxQueryStringLengthProp];
			}
			set
			{
				base[HttpRuntimeSection.maxQueryStringLengthProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum possible length, in number of characters, of the URL in an HTTP request.</summary>
		/// <returns>The length of the URL, in number of characters. The default is 260.</returns>
		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x06003E8C RID: 16012 RVA: 0x000A5C52 File Offset: 0x000A3E52
		// (set) Token: 0x06003E8D RID: 16013 RVA: 0x000A5C64 File Offset: 0x000A3E64
		[IntegerValidator(MinValue = 0)]
		[ConfigurationProperty("maxUrlLength", DefaultValue = "260")]
		public int MaxUrlLength
		{
			get
			{
				return (int)base[HttpRuntimeSection.maxUrlLengthProp];
			}
			set
			{
				base[HttpRuntimeSection.maxUrlLengthProp] = value;
			}
		}

		/// <summary>Gets or sets the name of a custom type that can be used to handle HTML and URL encoding. </summary>
		/// <returns>The name of a type that can be used to handle HTML and URL encoding. </returns>
		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x06003E8E RID: 16014 RVA: 0x000A5C77 File Offset: 0x000A3E77
		// (set) Token: 0x06003E8F RID: 16015 RVA: 0x000A5C89 File Offset: 0x000A3E89
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("encoderType", DefaultValue = "System.Web.Util.HttpEncoder")]
		public string EncoderType
		{
			get
			{
				return (string)base[HttpRuntimeSection.encoderTypeProp];
			}
			set
			{
				base[HttpRuntimeSection.encoderTypeProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the URL in an HTTP request is required to be a valid Windows file path.</summary>
		/// <returns>true if the URL does not have to comply with Windows path rules; otherwise false. The default is false. </returns>
		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x06003E90 RID: 16016 RVA: 0x000A5C97 File Offset: 0x000A3E97
		// (set) Token: 0x06003E91 RID: 16017 RVA: 0x000A5CA9 File Offset: 0x000A3EA9
		[ConfigurationProperty("relaxedUrlToFileSystemMapping", DefaultValue = "False")]
		public bool RelaxedUrlToFileSystemMapping
		{
			get
			{
				return (bool)base[HttpRuntimeSection.relaxedUrlToFileSystemMappingProp];
			}
			set
			{
				base[HttpRuntimeSection.relaxedUrlToFileSystemMappingProp] = value;
			}
		}

		/// <summary>Gets or sets the target .NET framework.</summary>
		/// <returns>The target .NET framework.</returns>
		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x06003E92 RID: 16018 RVA: 0x000A5CBC File Offset: 0x000A3EBC
		// (set) Token: 0x06003E93 RID: 16019 RVA: 0x000A5CCE File Offset: 0x000A3ECE
		[TypeConverter("System.Web.Configuration.VersionConverter")]
		[ConfigurationProperty("targetFramework", DefaultValue = "4.0")]
		public Version TargetFramework
		{
			get
			{
				return (Version)base[HttpRuntimeSection.targetFrameworkProp];
			}
			set
			{
				base[HttpRuntimeSection.targetFrameworkProp] = value;
			}
		}

		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x06003E94 RID: 16020 RVA: 0x000A5CDC File Offset: 0x000A3EDC
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpRuntimeSection.properties;
			}
		}

		/// <summary>Gets or sets a value that indicates whether <see cref="M:System.Web.HttpApplication.RegisterModule(System.Type)" /> method calls are allowed. The default is true.</summary>
		/// <returns>true if <see cref="M:System.Web.HttpApplication.RegisterModule(System.Type)" /> method calls are allowed; otherwise, false.</returns>
		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x06003E95 RID: 16021 RVA: 0x000A5CE3 File Offset: 0x000A3EE3
		// (set) Token: 0x06003E96 RID: 16022 RVA: 0x000A5CF5 File Offset: 0x000A3EF5
		[ConfigurationProperty("allowDynamicModuleRegistration", DefaultValue = "True")]
		public bool AllowDynamicModuleRegistration
		{
			get
			{
				return (bool)base[HttpRuntimeSection.allowDynamicModuleRegistrationProp];
			}
			set
			{
				base[HttpRuntimeSection.allowDynamicModuleRegistrationProp] = value;
			}
		}

		/// <summary>Gets or sets the mode of the request entity that is asynchronously preloaded.</summary>
		/// <returns>The mode of the request entity.</returns>
		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x06003E97 RID: 16023 RVA: 0x000A5D08 File Offset: 0x000A3F08
		// (set) Token: 0x06003E98 RID: 16024 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public AsyncPreloadModeFlags AsyncPreloadMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return AsyncPreloadModeFlags.None;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the amount of time that a regular expression is allowed in order to perform a match.</summary>
		/// <returns>The time that is allowed for regular-expression matching.</returns>
		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x06003E99 RID: 16025 RVA: 0x000A5D24 File Offset: 0x000A3F24
		// (set) Token: 0x06003E9A RID: 16026 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public TimeSpan DefaultRegexMatchTimeout
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(TimeSpan);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets an enumeration value specifying the file change notifications mode.</summary>
		/// <returns>An <see cref="T:System.Web.Configuration.FcnMode" /> enumeration value.</returns>
		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x06003E9B RID: 16027 RVA: 0x000A5D40 File Offset: 0x000A3F40
		// (set) Token: 0x06003E9C RID: 16028 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public FcnMode FcnMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return FcnMode.NotSet;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x06003E9D RID: 16029 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void set_TargetFramework(string value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400221C RID: 8732
		private static ConfigurationProperty apartmentThreadingProp = new ConfigurationProperty("apartmentThreading", typeof(bool), false);

		// Token: 0x0400221D RID: 8733
		private static ConfigurationProperty appRequestQueueLimitProp = new ConfigurationProperty("appRequestQueueLimit", typeof(int), 5000, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(1, int.MaxValue), ConfigurationPropertyOptions.None);

		// Token: 0x0400221E RID: 8734
		private static ConfigurationProperty delayNotificationTimeoutProp = new ConfigurationProperty("delayNotificationTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(5.0), PropertyHelper.TimeSpanSecondsConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400221F RID: 8735
		private static ConfigurationProperty enableProp = new ConfigurationProperty("enable", typeof(bool), true);

		// Token: 0x04002220 RID: 8736
		private static ConfigurationProperty enableHeaderCheckingProp = new ConfigurationProperty("enableHeaderChecking", typeof(bool), true);

		// Token: 0x04002221 RID: 8737
		private static ConfigurationProperty enableKernelOutputCacheProp = new ConfigurationProperty("enableKernelOutputCache", typeof(bool), true);

		// Token: 0x04002222 RID: 8738
		private static ConfigurationProperty enableVersionHeaderProp = new ConfigurationProperty("enableVersionHeader", typeof(bool), true);

		// Token: 0x04002223 RID: 8739
		private static ConfigurationProperty executionTimeoutProp = new ConfigurationProperty("executionTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(110.0), PropertyHelper.TimeSpanSecondsConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002224 RID: 8740
		private static ConfigurationProperty maxRequestLengthProp = new ConfigurationProperty("maxRequestLength", typeof(int), 4096, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002225 RID: 8741
		private static ConfigurationProperty maxWaitChangeNotificationProp = new ConfigurationProperty("maxWaitChangeNotification", typeof(int), 0, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002226 RID: 8742
		private static ConfigurationProperty minFreeThreadsProp = new ConfigurationProperty("minFreeThreads", typeof(int), 8, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002227 RID: 8743
		private static ConfigurationProperty minLocalRequestFreeThreadsProp = new ConfigurationProperty("minLocalRequestFreeThreads", typeof(int), 4, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002228 RID: 8744
		private static ConfigurationProperty requestLengthDiskThresholdProp = new ConfigurationProperty("requestLengthDiskThreshold", typeof(int), 80, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(1, int.MaxValue), ConfigurationPropertyOptions.None);

		// Token: 0x04002229 RID: 8745
		private static ConfigurationProperty requireRootedSaveAsPathProp = new ConfigurationProperty("requireRootedSaveAsPath", typeof(bool), true);

		// Token: 0x0400222A RID: 8746
		private static ConfigurationProperty sendCacheControlHeaderProp = new ConfigurationProperty("sendCacheControlHeader", typeof(bool), true);

		// Token: 0x0400222B RID: 8747
		private static ConfigurationProperty shutdownTimeoutProp = new ConfigurationProperty("shutdownTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(90.0), PropertyHelper.TimeSpanSecondsConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400222C RID: 8748
		private static ConfigurationProperty useFullyQualifiedRedirectUrlProp = new ConfigurationProperty("useFullyQualifiedRedirectUrl", typeof(bool), false);

		// Token: 0x0400222D RID: 8749
		private static ConfigurationProperty waitChangeNotificationProp = new ConfigurationProperty("waitChangeNotification", typeof(int), 0, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400222E RID: 8750
		private static ConfigurationProperty requestPathInvalidCharactersProp = new ConfigurationProperty("requestPathInvalidCharacters", typeof(string), "<,>,*,%,&,:,\\,?");

		// Token: 0x0400222F RID: 8751
		private static ConfigurationProperty requestValidationTypeProp = new ConfigurationProperty("requestValidationType", typeof(string), "System.Web.Util.RequestValidator", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002230 RID: 8752
		private static ConfigurationProperty requestValidationModeProp = new ConfigurationProperty("requestValidationMode", typeof(Version), new Version(4, 0), PropertyHelper.VersionConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002231 RID: 8753
		private static ConfigurationProperty maxQueryStringLengthProp = new ConfigurationProperty("maxQueryStringLength", typeof(int), 2048, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002232 RID: 8754
		private static ConfigurationProperty maxUrlLengthProp = new ConfigurationProperty("maxUrlLength", typeof(int), 260, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002233 RID: 8755
		private static ConfigurationProperty encoderTypeProp = new ConfigurationProperty("encoderType", typeof(string), "System.Web.Util.HttpEncoder", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002234 RID: 8756
		private static ConfigurationProperty relaxedUrlToFileSystemMappingProp = new ConfigurationProperty("relaxedUrlToFileSystemMapping", typeof(bool), false);

		// Token: 0x04002235 RID: 8757
		private static ConfigurationProperty targetFrameworkProp = new ConfigurationProperty("targetFramework", typeof(Version), new Version(4, 0), PropertyHelper.VersionConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002236 RID: 8758
		private static ConfigurationProperty allowDynamicModuleRegistrationProp = new ConfigurationProperty("allowDynamicModuleRegistration", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002237 RID: 8759
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
