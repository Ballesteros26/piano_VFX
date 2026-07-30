using System;
using System.Globalization;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Configuration;
using Unity;

namespace System.Web.Hosting
{
	/// <summary>Provides application-management functions and application services to a managed application within its application domain. This class cannot be inherited.</summary>
	// Token: 0x02000550 RID: 1360
	[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.High)]
	public sealed class HostingEnvironment : MarshalByRefObject
	{
		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x06003AD1 RID: 15057 RVA: 0x0009E7F6 File Offset: 0x0009C9F6
		// (set) Token: 0x06003AD2 RID: 15058 RVA: 0x0009E7FD File Offset: 0x0009C9FD
		internal static bool HaveCustomVPP { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.HostingEnvironment" /> class. </summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.Hosting.HostingEnvironment.#ctor" /> constructor is called more than once.</exception>
		// Token: 0x06003AD3 RID: 15059 RVA: 0x0009E805 File Offset: 0x0009CA05
		public HostingEnvironment()
		{
			throw new InvalidOperationException();
		}

		/// <summary>Gets the unique identifier of the application.</summary>
		/// <returns>The unique identifier of the application.</returns>
		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x06003AD4 RID: 15060 RVA: 0x0009E812 File Offset: 0x0009CA12
		public static string ApplicationID
		{
			get
			{
				return HttpRuntime.AppDomainAppId;
			}
		}

		/// <summary>Gets the physical path on disk to the application's directory.</summary>
		/// <returns>The physical path on disk to the application's directory.</returns>
		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x06003AD5 RID: 15061 RVA: 0x0001726E File Offset: 0x0001546E
		public static string ApplicationPhysicalPath
		{
			get
			{
				return HttpRuntime.AppDomainAppPath;
			}
		}

		/// <summary>Gets the root virtual path of the application.</summary>
		/// <returns>The root virtual path of the application with no trailing slash (/).</returns>
		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x06003AD6 RID: 15062 RVA: 0x00017275 File Offset: 0x00015475
		public static string ApplicationVirtualPath
		{
			get
			{
				return HttpRuntime.AppDomainAppVirtualPath;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Caching.Cache" /> instance for the current application.</summary>
		/// <returns>The current <see cref="T:System.Web.Caching.Cache" /> instance.</returns>
		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x06003AD7 RID: 15063 RVA: 0x0000F0BD File Offset: 0x0000D2BD
		public static Cache Cache
		{
			get
			{
				return HttpRuntime.Cache;
			}
		}

		/// <summary>Gets any exception thrown during initialization of the <see cref="T:System.Web.Hosting.HostingEnvironment" /> object.</summary>
		/// <returns>The exception thrown during initialization of the <see cref="T:System.Web.Hosting.HostingEnvironment" /> object. If no exception was thrown, returns null.</returns>
		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x06003AD8 RID: 15064 RVA: 0x0009E819 File Offset: 0x0009CA19
		public static Exception InitializationException
		{
			get
			{
				return HttpApplication.InitializationException;
			}
		}

		/// <summary>Gets a value indicating whether the current application domain is being hosted by an <see cref="T:System.Web.Hosting.ApplicationManager" /> object.</summary>
		/// <returns>true if the application domain is hosted by an <see cref="T:System.Web.Hosting.ApplicationManager" /> object; otherwise, false.</returns>
		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x06003AD9 RID: 15065 RVA: 0x0009E820 File Offset: 0x0009CA20
		// (set) Token: 0x06003ADA RID: 15066 RVA: 0x0009E827 File Offset: 0x0009CA27
		public static bool IsHosted
		{
			get
			{
				return HostingEnvironment.is_hosted;
			}
			internal set
			{
				HostingEnvironment.is_hosted = value;
			}
		}

		/// <summary>Returns an enumerated value that indicates why the application terminated.</summary>
		/// <returns>One of the <see cref="T:System.Web.ApplicationShutdownReason" /> values.</returns>
		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x06003ADB RID: 15067 RVA: 0x0009E82F File Offset: 0x0009CA2F
		public static ApplicationShutdownReason ShutdownReason
		{
			get
			{
				return HostingEnvironment.shutdown_reason;
			}
		}

		/// <summary>Gets the name of the site.</summary>
		/// <returns>The name of the site.</returns>
		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x06003ADC RID: 15068 RVA: 0x0009E836 File Offset: 0x0009CA36
		// (set) Token: 0x06003ADD RID: 15069 RVA: 0x0009E83D File Offset: 0x0009CA3D
		public static string SiteName
		{
			get
			{
				return HostingEnvironment.site_name;
			}
			internal set
			{
				HostingEnvironment.site_name = value;
			}
		}

		/// <summary>Gets the virtual path provider for this application.</summary>
		/// <returns>The <see cref="T:System.Web.Hosting.VirtualPathProvider" /> instance for this application.</returns>
		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x06003ADE RID: 15070 RVA: 0x0009E845 File Offset: 0x0009CA45
		public static VirtualPathProvider VirtualPathProvider
		{
			get
			{
				return HostingEnvironment.vpath_provider;
			}
		}

		/// <summary>Gets a value that indicates whether the hosting environment has access to the ASP.NET build system.</summary>
		/// <returns>true if the application domain is the ASP.NET hosted application domain used in ClientBuildManager scenarios; otherwise, false.</returns>
		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x06003ADF RID: 15071 RVA: 0x00008A69 File Offset: 0x00006C69
		public static bool InClientBuildManager
		{
			get
			{
				return false;
			}
		}

		/// <summary>Reduces the count of busy objects in the hosted environment by one.</summary>
		// Token: 0x06003AE0 RID: 15072 RVA: 0x0009E84C File Offset: 0x0009CA4C
		public static void DecrementBusyCount()
		{
			Interlocked.Decrement(ref HostingEnvironment.busy_count);
		}

		/// <summary>Impersonates the user represented by the application identity.</summary>
		/// <returns>An <see cref="T:System.IDisposable" /> object that represents the Windows user prior to impersonation; this object can be used to revert to the original user's context.</returns>
		/// <exception cref="T:System.Web.HttpException">The process cannot impersonate.</exception>
		// Token: 0x06003AE1 RID: 15073 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public static IDisposable Impersonate()
		{
			throw new NotImplementedException();
		}

		/// <summary>Impersonates the user represented by the specified user token.</summary>
		/// <returns>An <see cref="T:System.IDisposable" /> object that represents the Windows user prior to impersonation; this object can be used to revert to the original user's context.</returns>
		/// <param name="token">The handle of a Windows account token.</param>
		/// <exception cref="T:System.Web.HttpException">The process cannot impersonate.</exception>
		// Token: 0x06003AE2 RID: 15074 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public static IDisposable Impersonate(IntPtr token)
		{
			throw new NotImplementedException();
		}

		/// <summary>Impersonates the user specified by the configuration settings for the specified virtual path, or the specified user token.</summary>
		/// <returns>An <see cref="T:System.IDisposable" /> object that represents the Windows user prior to impersonation; this object can be used to revert to the original user's context.</returns>
		/// <param name="userToken">The handle of a Windows account token.</param>
		/// <param name="virtualPath">The path to the requested resource.</param>
		/// <exception cref="T:System.Web.HttpException">The process cannot impersonate.</exception>
		// Token: 0x06003AE3 RID: 15075 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public static IDisposable Impersonate(IntPtr userToken, string virtualPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Increases the count of busy objects in the hosted environment by one.</summary>
		// Token: 0x06003AE4 RID: 15076 RVA: 0x0009E859 File Offset: 0x0009CA59
		public static void IncrementBusyCount()
		{
			Interlocked.Increment(ref HostingEnvironment.busy_count);
		}

		/// <summary>Gives the <see cref="T:System.Web.Hosting.HostingEnvironment" /> object an infinite lifetime by preventing a lease from being created.</summary>
		/// <returns>Always null.</returns>
		// Token: 0x06003AE5 RID: 15077 RVA: 0x00003BEA File Offset: 0x00001DEA
		public override object InitializeLifetimeService()
		{
			return null;
		}

		/// <summary>Starts shutting down the web application associated with this host and removes registered objects from the system.</summary>
		// Token: 0x06003AE6 RID: 15078 RVA: 0x0009E866 File Offset: 0x0009CA66
		public static void InitiateShutdown()
		{
			HttpRuntime.UnloadAppDomain();
		}

		/// <summary>Maps a virtual path to a physical path on the server.</summary>
		/// <returns>The physical path on the server specified by <paramref name="virtualPath" />.</returns>
		/// <param name="virtualPath">The virtual path (absolute or relative).</param>
		// Token: 0x06003AE7 RID: 15079 RVA: 0x0009E870 File Offset: 0x0009CA70
		public static string MapPath(string virtualPath)
		{
			if (virtualPath == null || virtualPath == "")
			{
				throw new ArgumentNullException("virtualPath");
			}
			HttpContext httpContext = HttpContext.Current;
			HttpRequest httpRequest = ((httpContext == null) ? null : httpContext.Request);
			if (httpRequest == null)
			{
				return null;
			}
			return httpRequest.MapPath(virtualPath);
		}

		/// <summary>Places an object in the list of registered objects for the application.</summary>
		/// <param name="obj">The object to register.</param>
		// Token: 0x06003AE8 RID: 15080 RVA: 0x0009E8B7 File Offset: 0x0009CAB7
		public static void RegisterObject(IRegisteredObject obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (HostingEnvironment.Host != null)
			{
				HostingEnvironment.Host.RegisterObject(obj, false);
			}
		}

		/// <summary>Registers a new <see cref="T:System.Web.Hosting.VirtualPathProvider" /> instance with the ASP.NET compilation system.</summary>
		/// <param name="virtualPathProvider">The new <see cref="T:System.Web.Hosting.VirtualPathProvider" /> instance to add to the compilation system.</param>
		// Token: 0x06003AE9 RID: 15081 RVA: 0x0009E8DC File Offset: 0x0009CADC
		public static void RegisterVirtualPathProvider(VirtualPathProvider virtualPathProvider)
		{
			if (HttpRuntime.AppDomainAppVirtualPath == null)
			{
				throw new InvalidOperationException();
			}
			if (virtualPathProvider == null)
			{
				throw new ArgumentNullException("virtualPathProvider");
			}
			VirtualPathProvider virtualPathProvider2 = HostingEnvironment.vpath_provider;
			HostingEnvironment.vpath_provider = virtualPathProvider;
			HostingEnvironment.vpath_provider.InitializeAndSetPrevious(virtualPathProvider2);
			if (!(virtualPathProvider is DefaultVirtualPathProvider))
			{
				HostingEnvironment.HaveCustomVPP = true;
				return;
			}
			HostingEnvironment.HaveCustomVPP = false;
		}

		/// <summary>Sets the current thread to the culture of the specified virtual path.</summary>
		/// <returns>An <see cref="T:System.IDisposable" /> object that represents the culture prior to changing; this object can be used to revert to the previous culture.</returns>
		/// <param name="virtualPath">The path that contains the culture information.</param>
		// Token: 0x06003AEA RID: 15082 RVA: 0x0009E930 File Offset: 0x0009CB30
		public static IDisposable SetCultures(string virtualPath)
		{
			GlobalizationSection globalizationSection = WebConfigurationManager.GetSection("system.web/globalization", virtualPath) as GlobalizationSection;
			IDisposable disposable = Thread.CurrentThread.CurrentCulture as IDisposable;
			string culture = globalizationSection.Culture;
			if (string.IsNullOrEmpty(culture))
			{
				return disposable;
			}
			Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
			return disposable;
		}

		/// <summary>Sets the current thread to the culture specified in the application configuration file.</summary>
		/// <returns>An <see cref="T:System.IDisposable" /> object that represents the culture prior to changing; this object can be used to revert to the previous culture.</returns>
		// Token: 0x06003AEB RID: 15083 RVA: 0x0009E97E File Offset: 0x0009CB7E
		public static IDisposable SetCultures()
		{
			return HostingEnvironment.SetCultures("~/");
		}

		/// <summary>Removes an object from the list of registered objects for the application.</summary>
		/// <param name="obj">The object to remove.</param>
		// Token: 0x06003AEC RID: 15084 RVA: 0x0009E98A File Offset: 0x0009CB8A
		public static void UnregisterObject(IRegisteredObject obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (HostingEnvironment.Host != null)
			{
				HostingEnvironment.Host.UnregisterObject(obj);
			}
		}

		// Token: 0x06003AED RID: 15085 RVA: 0x0009E9AD File Offset: 0x0009CBAD
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public static void QueueBackgroundWorkItem(Action<CancellationToken> workItem)
		{
			if (workItem == null)
			{
				throw new ArgumentNullException("workItem");
			}
			HostingEnvironment.QueueBackgroundWorkItem(delegate(CancellationToken ct)
			{
				workItem(ct);
				return HostingEnvironment._completedTask;
			});
		}

		// Token: 0x06003AEE RID: 15086 RVA: 0x0009E9DE File Offset: 0x0009CBDE
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public static void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem)
		{
			if (workItem == null)
			{
				throw new ArgumentNullException("workItem");
			}
			if (HostingEnvironment.Host == null)
			{
				throw new InvalidOperationException();
			}
			HostingEnvironment.QueueBackgroundWorkItemInternal(workItem);
		}

		// Token: 0x06003AEF RID: 15087 RVA: 0x0009EA04 File Offset: 0x0009CC04
		private static void QueueBackgroundWorkItemInternal(Func<CancellationToken, Task> workItem)
		{
			BackgroundWorkScheduler backgroundWorkScheduler = Volatile.Read<BackgroundWorkScheduler>(ref HostingEnvironment._backgroundWorkScheduler);
			if (backgroundWorkScheduler == null)
			{
				BackgroundWorkScheduler backgroundWorkScheduler2 = new BackgroundWorkScheduler(new Action<BackgroundWorkScheduler>(HostingEnvironment.UnregisterObject), new Action<AppDomain, Exception>(HostingEnvironment.WriteUnhandledException), null);
				backgroundWorkScheduler = Interlocked.CompareExchange<BackgroundWorkScheduler>(ref HostingEnvironment._backgroundWorkScheduler, backgroundWorkScheduler2, null) ?? backgroundWorkScheduler2;
				if (backgroundWorkScheduler == backgroundWorkScheduler2)
				{
					HostingEnvironment.RegisterObject(backgroundWorkScheduler);
				}
			}
			backgroundWorkScheduler.ScheduleWorkItem(workItem);
		}

		// Token: 0x06003AF0 RID: 15088 RVA: 0x0009EA61 File Offset: 0x0009CC61
		private static void WriteUnhandledException(AppDomain appDomain, Exception exception)
		{
			Console.Error.WriteLine("Error in background work item: " + exception);
		}

		/// <summary>This property supports the ASP.NET infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An object that contains information about the application host.</returns>
		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x06003AF2 RID: 15090 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static IApplicationHost ApplicationHost
		{
			[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x06003AF3 RID: 15091 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static ApplicationMonitors ApplicationMonitors
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a value that indicats whether the current application is in a development environment.</summary>
		/// <returns>true if the application is in a development environment; otherwise, false.</returns>
		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x06003AF4 RID: 15092 RVA: 0x0009EAA0 File Offset: 0x0009CCA0
		public static bool IsDevelopmentEnvironment
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets or sets the maximum concurrent requests per CPU.</summary>
		/// <returns>The maximum concurrent requests per CPU.</returns>
		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x06003AF5 RID: 15093 RVA: 0x0009EABC File Offset: 0x0009CCBC
		// (set) Token: 0x06003AF6 RID: 15094 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static int MaxConcurrentRequestsPerCPU
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the maximum concurrent threads per CPU.</summary>
		/// <returns>The maximum concurrent threads per CPU.</returns>
		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x06003AF7 RID: 15095 RVA: 0x0009EAD8 File Offset: 0x0009CCD8
		// (set) Token: 0x06003AF8 RID: 15096 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static int MaxConcurrentThreadsPerCPU
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs when the worker process or application pool associated with this host has stopped listening for new requests and will eventually shut down.</summary>
		// Token: 0x1400010C RID: 268
		// (add) Token: 0x06003AF9 RID: 15097 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x06003AFA RID: 15098 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static event EventHandler StopListening
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>This method supports the ASP.NET infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x06003AFB RID: 15099 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void MessageReceived()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001FE4 RID: 8164
		private static bool is_hosted;

		// Token: 0x04001FE5 RID: 8165
		private static string site_name;

		// Token: 0x04001FE6 RID: 8166
		private static ApplicationShutdownReason shutdown_reason;

		// Token: 0x04001FE7 RID: 8167
		internal static BareApplicationHost Host;

		// Token: 0x04001FE8 RID: 8168
		private static VirtualPathProvider vpath_provider = ((HttpRuntime.AppDomainAppVirtualPath == null) ? null : new DefaultVirtualPathProvider());

		// Token: 0x04001FE9 RID: 8169
		private static int busy_count;

		// Token: 0x04001FEA RID: 8170
		private static BackgroundWorkScheduler _backgroundWorkScheduler = null;

		// Token: 0x04001FEB RID: 8171
		private static readonly Task<object> _completedTask = Task.FromResult<object>(null);
	}
}
