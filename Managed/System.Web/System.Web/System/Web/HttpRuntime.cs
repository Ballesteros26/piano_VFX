using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Caching;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Util;
using Mono.Web.Util;

namespace System.Web
{
	/// <summary>Provides a set of ASP.NET run-time services for the current application. </summary>
	// Token: 0x020000B2 RID: 178
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpRuntime
	{
		// Token: 0x06000991 RID: 2449 RVA: 0x000170D8 File Offset: 0x000152D8
		static HttpRuntime()
		{
			try
			{
				WebConfigurationManager.Init();
				SettingsMappingManager.Init();
				HttpRuntime.runtime_section = (HttpRuntimeSection)WebConfigurationManager.GetSection("system.web/httpRuntime");
			}
			catch (Exception ex)
			{
				HttpRuntime.initialException = ex;
			}
			HttpRuntime.queue_manager = new QueueManager();
			if (HttpRuntime.queue_manager.HasException)
			{
				if (HttpRuntime.initialException == null)
				{
					HttpRuntime.initialException = HttpRuntime.queue_manager.InitialException;
				}
				else
				{
					Console.Error.WriteLine("Exception during QueueManager initialization:");
					Console.Error.WriteLine(HttpRuntime.queue_manager.InitialException);
				}
			}
			HttpRuntime.trace_manager = new TraceManager();
			if (HttpRuntime.trace_manager.HasException)
			{
				if (HttpRuntime.initialException == null)
				{
					HttpRuntime.initialException = HttpRuntime.trace_manager.InitialException;
				}
				else
				{
					Console.Error.WriteLine("Exception during TraceManager initialization:");
					Console.Error.WriteLine(HttpRuntime.trace_manager.InitialException);
				}
			}
			HttpRuntime.registeredAssemblies = new SplitOrderedList<string, string>(StringComparer.Ordinal);
			HttpRuntime.cache = new Cache();
			HttpRuntime.internalCache = new Cache();
			HttpRuntime.internalCache.DependencyCache = HttpRuntime.internalCache;
			HttpRuntime.do_RealProcessRequest = delegate(object state)
			{
				try
				{
					HttpRuntime.RealProcessRequest(state);
				}
				catch
				{
				}
			};
			HttpRuntime.end_of_send_cb = new HttpWorkerRequest.EndOfSendNotification(HttpRuntime.EndOfSend);
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x00017260 File Offset: 0x00015460
		internal static SplitOrderedList<string, string> RegisteredAssemblies
		{
			get
			{
				return HttpRuntime.registeredAssemblies;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x00017267 File Offset: 0x00015467
		internal static bool DomainUnloading
		{
			get
			{
				return HttpRuntime.domainUnloading;
			}
		}

		/// <summary>Gets the folder path for the ASP.NET client script files.</summary>
		/// <returns>The folder path for the ASP.NET client script files.</returns>
		/// <exception cref="T:System.Web.HttpException">ASP.NET is not installed.</exception>
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0001726E File Offset: 0x0001546E
		[global::System.MonoDocumentationNote("Currently returns path to the application root")]
		public static string AspClientScriptPhysicalPath
		{
			get
			{
				return HttpRuntime.AppDomainAppPath;
			}
		}

		/// <summary>Gets the virtual path for the ASP.NET client script files.</summary>
		/// <returns>The virtual path for the ASP.NET client script files.</returns>
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x00017275 File Offset: 0x00015475
		[global::System.MonoDocumentationNote("Currently returns path to the application root")]
		public static string AspClientScriptVirtualPath
		{
			get
			{
				return HttpRuntime.AppDomainAppVirtualPath;
			}
		}

		/// <summary>Gets the application identification of the application domain where the <see cref="T:System.Web.HttpRuntime" /> exists.</summary>
		/// <returns>The application identification of the application domain where the <see cref="T:System.Web.HttpRuntime" /> exists.</returns>
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x0001727C File Offset: 0x0001547C
		public static string AppDomainAppId
		{
			[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
			get
			{
				string text = (string)AppDomain.CurrentDomain.GetData(".appId");
				if (text != null && text.Length > 0 && SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text).Demand();
				}
				return text;
			}
		}

		/// <summary>Gets the physical drive path of the application directory for the application hosted in the current application domain.</summary>
		/// <returns>The physical drive path of the application directory for the application hosted in the current application domain.</returns>
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x000172C0 File Offset: 0x000154C0
		public static string AppDomainAppPath
		{
			get
			{
				string text = (string)AppDomain.CurrentDomain.GetData(".appPath");
				if (SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text).Demand();
				}
				return text;
			}
		}

		/// <summary>Gets the virtual path of the directory that contains the application hosted in the current application domain.</summary>
		/// <returns>The virtual path of the directory that contains the application hosted in the current application domain.</returns>
		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x000172F6 File Offset: 0x000154F6
		public static string AppDomainAppVirtualPath
		{
			get
			{
				return (string)AppDomain.CurrentDomain.GetData(".appVPath");
			}
		}

		/// <summary>Gets the domain identification of the application domain where the <see cref="T:System.Web.HttpRuntime" /> instance exists.</summary>
		/// <returns>The unique application domain identifier.</returns>
		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0001730C File Offset: 0x0001550C
		public static string AppDomainId
		{
			[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
			get
			{
				return (string)AppDomain.CurrentDomain.GetData(".domainId");
			}
		}

		/// <summary>Gets the physical path of the directory where the ASP.NET executable files are installed.</summary>
		/// <returns>The physical path to the ASP.NET executable files.</returns>
		/// <exception cref="T:System.Web.HttpException">ASP.NET is not installed on this computer.</exception>
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x00017324 File Offset: 0x00015524
		public static string AspInstallDirectory
		{
			get
			{
				string text = (string)AppDomain.CurrentDomain.GetData(".hostingInstallDir");
				if (text != null && text.Length > 0 && SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text).Demand();
				}
				return text;
			}
		}

		/// <summary>Gets the physical path to the /bin directory for the current application.</summary>
		/// <returns>The path to the current application's /bin directory.</returns>
		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x00017368 File Offset: 0x00015568
		public static string BinDirectory
		{
			get
			{
				if (HttpRuntime._actual_bin_directory == null)
				{
					string[] array = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath.Split(new char[] { ';' });
					string appDomainAppPath = HttpRuntime.AppDomainAppPath;
					foreach (string text in array)
					{
						string text2 = Path.Combine(appDomainAppPath, text);
						if (Directory.Exists(text2))
						{
							HttpRuntime._actual_bin_directory = text2;
							break;
						}
					}
					if (HttpRuntime._actual_bin_directory == null)
					{
						HttpRuntime._actual_bin_directory = Path.Combine(appDomainAppPath, "bin");
					}
					if (HttpRuntime._actual_bin_directory[HttpRuntime._actual_bin_directory.Length - 1] != Path.DirectorySeparatorChar)
					{
						HttpRuntime._actual_bin_directory += Path.DirectorySeparatorChar.ToString();
					}
				}
				if (SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, HttpRuntime._actual_bin_directory).Demand();
				}
				return HttpRuntime._actual_bin_directory;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Caching.Cache" /> for the current application.</summary>
		/// <returns>The current <see cref="T:System.Web.Caching.Cache" />.</returns>
		/// <exception cref="T:System.Web.HttpException">ASP.NET is not installed.</exception>
		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x0001743E File Offset: 0x0001563E
		public static Cache Cache
		{
			get
			{
				return HttpRuntime.cache;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x00017445 File Offset: 0x00015645
		internal static Cache InternalCache
		{
			get
			{
				return HttpRuntime.internalCache;
			}
		}

		/// <summary>Gets the physical path to the directory where the common language runtime executable files are installed.</summary>
		/// <returns>The physical path to the common language runtime executable files.</returns>
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0001744C File Offset: 0x0001564C
		public static string ClrInstallDirectory
		{
			get
			{
				string directoryName = Path.GetDirectoryName(typeof(object).Assembly.Location);
				if (directoryName != null && directoryName.Length > 0 && SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, directoryName).Demand();
				}
				return directoryName;
			}
		}

		/// <summary>Gets the physical path to the directory where ASP.NET stores temporary files (generated sources, compiled assemblies, and so on) for the current application.</summary>
		/// <returns>The physical path to the application's temporary file storage directory.</returns>
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x00017494 File Offset: 0x00015694
		public static string CodegenDir
		{
			get
			{
				string dynamicBase = AppDomain.CurrentDomain.SetupInformation.DynamicBase;
				if (SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, dynamicBase).Demand();
				}
				return dynamicBase;
			}
		}

		/// <summary>Gets a value that indicates whether the application is mapped to a universal naming convention (UNC) share.</summary>
		/// <returns>true if the application is mapped to a UNC share; otherwise, false.</returns>
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x000174C5 File Offset: 0x000156C5
		public static bool IsOnUNCShare
		{
			[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Low)]
			get
			{
				return RuntimeHelpers.IsUncShare;
			}
		}

		/// <summary>Gets the physical path to the directory where the Machine.config file for the current application is located.</summary>
		/// <returns>The physical path to the Machine.config file for the current application.</returns>
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x000174CC File Offset: 0x000156CC
		public static string MachineConfigurationDirectory
		{
			get
			{
				string directoryName = Path.GetDirectoryName(ICalls.GetMachineConfigPath());
				if (directoryName != null && directoryName.Length > 0 && SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, directoryName).Demand();
				}
				return directoryName;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x00017504 File Offset: 0x00015704
		internal static HttpRuntimeSection Section
		{
			get
			{
				return HttpRuntime.runtime_section;
			}
		}

		/// <summary>Gets a value that indicates whether the current application is running in the integrated-pipeline mode of IIS 7.0.</summary>
		/// <returns>true if the application is running in integrated-pipeline mode; otherwise, false.</returns>
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x00008A69 File Offset: 0x00006C69
		public static bool UsingIntegratedPipeline
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the version of IIS that is hosting this application.</summary>
		/// <returns>The version of IIS that is hosting this application, or null if this application is not hosted by IIS.</returns>
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x00003BEA File Offset: 0x00001DEA
		public static Version IISVersion
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the version of the .NET Framework that the current web application targets.</summary>
		/// <returns>The version of the .NET Framework that the current web application targets.</returns>
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0001750B File Offset: 0x0001570B
		public static Version TargetFramework
		{
			get
			{
				return HttpRuntime.runtime_section.TargetFramework;
			}
		}

		/// <summary>Shuts down the <see cref="T:System.Web.HttpRuntime" /> instance.</summary>
		// Token: 0x060009A6 RID: 2470 RVA: 0x0000393A File Offset: 0x00001B3A
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static void Close()
		{
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00017518 File Offset: 0x00015718
		internal static HttpWorkerRequest QueuePendingRequest(bool started_internally)
		{
			HttpWorkerRequest nextRequest = HttpRuntime.queue_manager.GetNextRequest(null);
			if (nextRequest == null)
			{
				return null;
			}
			if (!started_internally)
			{
				nextRequest.StartedInternally = true;
				ThreadPool.QueueUserWorkItem(HttpRuntime.do_RealProcessRequest, nextRequest);
				return null;
			}
			return nextRequest;
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00017550 File Offset: 0x00015750
		private static bool AppIsOffline(HttpContext context)
		{
			if (!HttpApplicationFactory.ApplicationDisabled || HttpRuntime.app_offline_file == null)
			{
				return false;
			}
			HttpResponse response = context.Response;
			response.Clear();
			response.ContentType = "text/html";
			response.ExpiresAbsolute = DateTime.UtcNow;
			response.StatusCode = 503;
			response.TransmitFile(HttpRuntime.app_offline_file, true);
			context.Request.ReleaseResources();
			context.Response.ReleaseResources();
			HttpContext.Current = null;
			HttpApplication.requests_total_counter.Increment();
			return true;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x000175CD File Offset: 0x000157CD
		private static void AppOfflineFileRenamed(object sender, RenamedEventArgs args)
		{
			HttpRuntime.AppOfflineFileChanged(sender, args);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x000175D8 File Offset: 0x000157D8
		private static void AppOfflineFileChanged(object sender, FileSystemEventArgs args)
		{
			object obj = HttpRuntime.appOfflineLock;
			lock (obj)
			{
				WatcherChangeTypes changeType = args.ChangeType;
				bool flag2;
				switch (changeType)
				{
				case WatcherChangeTypes.Created:
				case WatcherChangeTypes.Changed:
					flag2 = true;
					goto IL_0067;
				case WatcherChangeTypes.Deleted:
					flag2 = false;
					goto IL_0067;
				case WatcherChangeTypes.Created | WatcherChangeTypes.Deleted:
					break;
				default:
					if (changeType == WatcherChangeTypes.Renamed)
					{
						RenamedEventArgs renamedEventArgs = args as RenamedEventArgs;
						flag2 = renamedEventArgs != null && string.Compare(renamedEventArgs.Name, "app_offline.htm", StringComparison.OrdinalIgnoreCase) == 0;
						goto IL_0067;
					}
					break;
				}
				flag2 = false;
				IL_0067:
				HttpRuntime.SetOfflineMode(flag2, args.FullPath);
			}
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00017674 File Offset: 0x00015874
		private static void SetOfflineMode(bool offline, string filePath)
		{
			if (!offline)
			{
				HttpRuntime.app_offline_file = null;
				if (HttpApplicationFactory.ApplicationDisabled)
				{
					HttpRuntime.UnloadAppDomain();
					return;
				}
			}
			else
			{
				HttpRuntime.app_offline_file = filePath;
				HttpApplicationFactory.DisableWatchers();
				HttpApplicationFactory.ApplicationDisabled = true;
				HttpRuntime.InternalCache.InvokePrivateCallbacks();
				HttpApplicationFactory.Dispose();
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x000176AC File Offset: 0x000158AC
		private static void SetupOfflineWatch()
		{
			object obj = HttpRuntime.appOfflineLock;
			lock (obj)
			{
				FileSystemEventHandler fileSystemEventHandler = new FileSystemEventHandler(HttpRuntime.AppOfflineFileChanged);
				RenamedEventHandler renamedEventHandler = new RenamedEventHandler(HttpRuntime.AppOfflineFileRenamed);
				string appDomainAppPath = HttpRuntime.AppDomainAppPath;
				string text = null;
				foreach (string text2 in HttpRuntime.app_offline_files)
				{
					FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
					fileSystemWatcher.Path = Path.GetDirectoryName(appDomainAppPath);
					fileSystemWatcher.Filter = Path.GetFileName(text2);
					fileSystemWatcher.NotifyFilter |= NotifyFilters.Size;
					fileSystemWatcher.Deleted += fileSystemEventHandler;
					fileSystemWatcher.Changed += fileSystemEventHandler;
					fileSystemWatcher.Created += fileSystemEventHandler;
					fileSystemWatcher.Renamed += renamedEventHandler;
					fileSystemWatcher.EnableRaisingEvents = true;
					string text3 = Path.Combine(appDomainAppPath, text2);
					if (File.Exists(text3))
					{
						text = text3;
					}
				}
				if (text != null)
				{
					HttpRuntime.SetOfflineMode(true, text);
				}
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x000177A0 File Offset: 0x000159A0
		private static void RealProcessRequest(object o)
		{
			if (HttpRuntime.domainUnloading)
			{
				Console.Error.WriteLine("Domain is unloading, not processing the request.");
				return;
			}
			HttpWorkerRequest httpWorkerRequest = (HttpWorkerRequest)o;
			bool startedInternally = httpWorkerRequest.StartedInternally;
			do
			{
				HttpRuntime.Process(httpWorkerRequest);
				httpWorkerRequest = HttpRuntime.QueuePendingRequest(startedInternally);
			}
			while (startedInternally && httpWorkerRequest != null);
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x000177E8 File Offset: 0x000159E8
		private static void Process(HttpWorkerRequest req)
		{
			bool flag = false;
			if (HttpRuntime.firstRun)
			{
				HttpRuntime.firstRun = false;
				if (HttpRuntime.initialException != null)
				{
					HttpRuntime.FinishWithException(req, HttpException.NewWithCode("Initial exception", HttpRuntime.initialException, 3001));
					flag = true;
				}
				HttpRuntime.SetupOfflineWatch();
			}
			HttpContext httpContext = new HttpContext(req);
			HttpContext.Current = httpContext;
			if (HttpRuntime.AppIsOffline(httpContext))
			{
				return;
			}
			HttpApplication httpApplication = null;
			if (!flag)
			{
				try
				{
					httpApplication = HttpApplicationFactory.GetApplication(httpContext);
				}
				catch (Exception ex)
				{
					HttpRuntime.FinishWithException(req, HttpException.NewWithCode(string.Empty, ex, 3001));
					flag = true;
				}
			}
			if (flag)
			{
				httpContext.Request.ReleaseResources();
				httpContext.Response.ReleaseResources();
				HttpContext.Current = null;
				return;
			}
			httpContext.ApplicationInstance = httpApplication;
			req.SetEndOfSendNotification(HttpRuntime.end_of_send_cb, httpContext);
			((IHttpHandler)httpApplication).ProcessRequest(httpContext);
			HttpApplicationFactory.Recycle(httpApplication);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0000393A File Offset: 0x00001B3A
		private static void EndOfSend(HttpWorkerRequest ignored1, object ignored2)
		{
		}

		/// <summary>Drives all ASP.NET Web processing execution.</summary>
		/// <param name="wr">An <see cref="T:System.Web.HttpWorkerRequest" /> for the current application. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="wr" /> parameter is null. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The Web application is running under IIS 7 in Integrated mode.</exception>
		// Token: 0x060009B0 RID: 2480 RVA: 0x000178BC File Offset: 0x00015ABC
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
		public static void ProcessRequest(HttpWorkerRequest wr)
		{
			if (wr == null)
			{
				throw new ArgumentNullException("wr");
			}
			HttpWorkerRequest nextRequest = HttpRuntime.queue_manager.GetNextRequest(wr);
			if (nextRequest == null)
			{
				return;
			}
			HttpRuntime.QueuePendingRequest(false);
			HttpRuntime.RealProcessRequest(nextRequest);
		}

		/// <summary>Terminates the current application. The application restarts the next time a request is received for it.</summary>
		// Token: 0x060009B1 RID: 2481 RVA: 0x000178F4 File Offset: 0x00015AF4
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static void UnloadAppDomain()
		{
			HttpRuntime.domainUnloading = true;
			HttpApplicationFactory.DisableWatchers();
			ThreadPool.QueueUserWorkItem(delegate
			{
				try
				{
					HttpRuntime.ShutdownAppDomain();
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine(ex);
				}
			});
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00017926 File Offset: 0x00015B26
		private static void ShutdownAppDomain()
		{
			HttpRuntime.queue_manager.Dispose();
			HttpRuntime.InternalCache.InvokePrivateCallbacks();
			HttpApplicationFactory.Dispose();
			ThreadPool.QueueUserWorkItem(delegate
			{
				try
				{
					HttpRuntime.DoUnload();
				}
				catch
				{
				}
			});
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00017966 File Offset: 0x00015B66
		private static void DoUnload()
		{
			AppDomain.Unload(AppDomain.CurrentDomain);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00017974 File Offset: 0x00015B74
		private static void FinishWithException(HttpWorkerRequest wr, HttpException e)
		{
			int httpCode = e.GetHttpCode();
			wr.SendStatus(httpCode, HttpWorkerRequest.GetStatusDescription(httpCode));
			wr.SendUnknownResponseHeader("Connection", "close");
			Encoding ascii = Encoding.ASCII;
			wr.SendUnknownResponseHeader("Content-Type", "text/html; charset=" + ascii.WebName);
			string htmlErrorMessage = e.GetHtmlErrorMessage();
			byte[] bytes = ascii.GetBytes(htmlErrorMessage);
			wr.SendUnknownResponseHeader("Content-Length", bytes.Length.ToString());
			wr.SendResponseFromMemory(bytes, bytes.Length);
			wr.FlushResponse(true);
			wr.CloseConnection();
			HttpApplication.requests_total_counter.Increment();
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00017A10 File Offset: 0x00015C10
		internal static void FinishUnavailable(HttpWorkerRequest wr)
		{
			wr.SendStatus(503, "Service unavailable");
			wr.SendUnknownResponseHeader("Connection", "close");
			Encoding ascii = Encoding.ASCII;
			wr.SendUnknownResponseHeader("Content-Type", "text/html; charset=" + ascii.WebName);
			byte[] bytes = ascii.GetBytes(HttpRuntime.content503);
			wr.SendUnknownResponseHeader("Content-Length", bytes.Length.ToString());
			wr.SendResponseFromMemory(bytes, bytes.Length);
			wr.FlushResponse(true);
			wr.CloseConnection();
			HttpApplication.requests_total_counter.Increment();
		}

		/// <summary>Returns the set of permissions associated with code groups.</summary>
		/// <returns>A <see cref="T:System.Security.NamedPermissionSet" /> object containing the names and descriptions of permissions, or null if none exists.</returns>
		// Token: 0x060009B6 RID: 2486 RVA: 0x00003BEA File Offset: 0x00001DEA
		[global::System.MonoDocumentationNote("Always returns null on Mono")]
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Unrestricted)]
		public static NamedPermissionSet GetNamedPermissionSet()
		{
			return null;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00017AA4 File Offset: 0x00015CA4
		internal static void WritePreservationFile(Assembly asm, string genericNameBase)
		{
			if (asm == null)
			{
				throw new ArgumentNullException("asm");
			}
			if (string.IsNullOrEmpty(genericNameBase))
			{
				throw new ArgumentNullException("genericNameBase");
			}
			string text = Path.Combine(AppDomain.CurrentDomain.SetupInformation.DynamicBase, genericNameBase + ".compiled");
			PreservationFile preservationFile = new PreservationFile();
			try
			{
				preservationFile.VirtualPath = "/" + genericNameBase + "/";
				AssemblyName name = asm.GetName();
				preservationFile.Assembly = name.Name;
				preservationFile.ResultType = BuildResultTypeCode.TopLevelAssembly;
				preservationFile.Save(text);
			}
			catch (Exception ex)
			{
				throw new HttpException(string.Format("Failed to write preservation file {0}", genericNameBase + ".compiled"), ex);
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00017B68 File Offset: 0x00015D68
		private static Assembly ResolveAssemblyHandler(object sender, ResolveEventArgs e)
		{
			AssemblyName assemblyName = new AssemblyName(e.Name);
			string dynamicBase = AppDomain.CurrentDomain.SetupInformation.DynamicBase;
			string text = Path.Combine(dynamicBase, assemblyName.Name + ".compiled");
			string text2;
			if (!File.Exists(text))
			{
				string fullName = assemblyName.FullName;
				if (!HttpRuntime.RegisteredAssemblies.Find((uint)fullName.GetHashCode(), fullName, out text2))
				{
					return null;
				}
			}
			else
			{
				PreservationFile preservationFile;
				try
				{
					preservationFile = new PreservationFile(text);
				}
				catch (Exception ex)
				{
					throw new HttpException(string.Format("Failed to read preservation file {0}", assemblyName.Name + ".compiled"), ex);
				}
				text2 = Path.Combine(dynamicBase, preservationFile.Assembly + ".dll");
			}
			if (string.IsNullOrEmpty(text2))
			{
				return null;
			}
			Assembly assembly = null;
			try
			{
				assembly = Assembly.LoadFrom(text2);
			}
			catch (Exception)
			{
			}
			return assembly;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00017C50 File Offset: 0x00015E50
		internal static void EnableAssemblyMapping(bool enable)
		{
			object obj = HttpRuntime.assemblyMappingLock;
			lock (obj)
			{
				if (HttpRuntime.assemblyMappingEnabled != enable)
				{
					if (enable)
					{
						AppDomain.CurrentDomain.AssemblyResolve += HttpRuntime.ResolveAssemblyHandler;
					}
					else
					{
						AppDomain.CurrentDomain.AssemblyResolve -= HttpRuntime.ResolveAssemblyHandler;
					}
					HttpRuntime.assemblyMappingEnabled = enable;
				}
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x00017CCC File Offset: 0x00015ECC
		internal static TraceManager TraceManager
		{
			get
			{
				return HttpRuntime.trace_manager;
			}
		}

		// Token: 0x04001003 RID: 4099
		private static bool domainUnloading;

		// Token: 0x04001004 RID: 4100
		private static SplitOrderedList<string, string> registeredAssemblies;

		// Token: 0x04001005 RID: 4101
		private static QueueManager queue_manager;

		// Token: 0x04001006 RID: 4102
		private static TraceManager trace_manager;

		// Token: 0x04001007 RID: 4103
		private static Cache cache;

		// Token: 0x04001008 RID: 4104
		private static Cache internalCache;

		// Token: 0x04001009 RID: 4105
		private static WaitCallback do_RealProcessRequest;

		// Token: 0x0400100A RID: 4106
		private static HttpWorkerRequest.EndOfSendNotification end_of_send_cb;

		// Token: 0x0400100B RID: 4107
		private static Exception initialException;

		// Token: 0x0400100C RID: 4108
		private static bool firstRun = true;

		// Token: 0x0400100D RID: 4109
		private static bool assemblyMappingEnabled;

		// Token: 0x0400100E RID: 4110
		private static object assemblyMappingLock = new object();

		// Token: 0x0400100F RID: 4111
		private static object appOfflineLock = new object();

		// Token: 0x04001010 RID: 4112
		private static HttpRuntimeSection runtime_section;

		// Token: 0x04001011 RID: 4113
		private static string _actual_bin_directory;

		// Token: 0x04001012 RID: 4114
		private static readonly string[] app_offline_files = new string[] { "app_offline.htm", "App_Offline.htm", "APP_OFFLINE.HTM" };

		// Token: 0x04001013 RID: 4115
		private static string app_offline_file;

		// Token: 0x04001014 RID: 4116
		private static string content503 = "<!DOCTYPE HTML PUBLIC \"-//IETF//DTD HTML 2.0//EN\">\n<html><head>\n<title>503 Server Unavailable</title>\n</head><body>\n<h1>Server Unavailable</h1>\n</body></html>\n";
	}
}
