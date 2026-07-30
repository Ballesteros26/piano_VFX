using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Configuration.nBrowser;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x0200007D RID: 125
	internal sealed class HttpApplicationFactory
	{
		// Token: 0x0600055F RID: 1375 RVA: 0x0000CB24 File Offset: 0x0000AD24
		private bool IsEventHandler(MethodInfo m)
		{
			int num = m.Name.IndexOf('_');
			if (num == -1 || m.Name.Length - 1 <= num)
			{
				return false;
			}
			if (m.ReturnType != typeof(void))
			{
				return false;
			}
			ParameterInfo[] parameters = m.GetParameters();
			int num2 = parameters.Length;
			return num2 == 0 || (num2 == 2 && !(parameters[0].ParameterType != typeof(object)) && typeof(EventArgs).IsAssignableFrom(parameters[1].ParameterType));
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0000CBBC File Offset: 0x0000ADBC
		private void AddEvent(MethodInfo method, Hashtable appTypeEventHandlers)
		{
			string text = method.Name.Replace("_On", "_");
			if (appTypeEventHandlers[text] == null)
			{
				appTypeEventHandlers[text] = method;
				return;
			}
			MethodInfo methodInfo = appTypeEventHandlers[text] as MethodInfo;
			ArrayList arrayList;
			if (methodInfo != null)
			{
				arrayList = new ArrayList(4);
				arrayList.Add(methodInfo);
				appTypeEventHandlers[text] = arrayList;
			}
			else
			{
				arrayList = appTypeEventHandlers[text] as ArrayList;
			}
			arrayList.Add(method);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0000CC38 File Offset: 0x0000AE38
		private ArrayList GetMethodsDeep(Type type)
		{
			ArrayList arrayList = new ArrayList();
			MethodInfo[] array = type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			arrayList.AddRange(array);
			Type type2 = type.BaseType;
			while (type2 != null)
			{
				array = type2.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
				arrayList.AddRange(array);
				type2 = type2.BaseType;
			}
			return arrayList;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0000CC88 File Offset: 0x0000AE88
		private Hashtable GetApplicationTypeEvents(Type type)
		{
			if (this.have_app_events)
			{
				return this.app_event_handlers;
			}
			object obj = this.this_lock;
			lock (obj)
			{
				if (this.app_event_handlers != null)
				{
					return this.app_event_handlers;
				}
				this.app_event_handlers = new Hashtable();
				ArrayList methodsDeep = this.GetMethodsDeep(type);
				Hashtable hashtable = null;
				foreach (object obj2 in methodsDeep)
				{
					MethodInfo methodInfo = obj2 as MethodInfo;
					if (methodInfo.DeclaringType != typeof(HttpApplication) && this.IsEventHandler(methodInfo))
					{
						string text = methodInfo.ToString();
						if (hashtable == null)
						{
							hashtable = new Hashtable();
						}
						else if (hashtable.ContainsKey(text))
						{
							continue;
						}
						hashtable.Add(text, methodInfo);
						this.AddEvent(methodInfo, this.app_event_handlers);
					}
				}
				this.have_app_events = true;
			}
			return this.app_event_handlers;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0000CDA4 File Offset: 0x0000AFA4
		private Hashtable GetApplicationTypeEvents(HttpApplication app)
		{
			if (this.have_app_events)
			{
				return this.app_event_handlers;
			}
			return this.GetApplicationTypeEvents(app.GetType());
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0000CDC4 File Offset: 0x0000AFC4
		private bool FireEvent(string method_name, object target, object[] args)
		{
			MethodInfo methodInfo = this.GetApplicationTypeEvents((HttpApplication)target)[method_name] as MethodInfo;
			if (methodInfo == null)
			{
				return false;
			}
			if (methodInfo.GetParameters().Length == 0)
			{
				args = null;
			}
			methodInfo.Invoke(target, args);
			return true;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000CE0C File Offset: 0x0000B00C
		private HttpApplication FireOnAppStart(HttpContext context)
		{
			HttpApplication httpApplication = (HttpApplication)Activator.CreateInstance(this.app_type, true);
			context.ApplicationInstance = httpApplication;
			httpApplication.SetContext(context);
			object[] array = new object[]
			{
				httpApplication,
				EventArgs.Empty
			};
			httpApplication.InApplicationStart = true;
			this.FireEvent("Application_Start", httpApplication, array);
			httpApplication.InApplicationStart = false;
			return httpApplication;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0000CE6C File Offset: 0x0000B06C
		private void FireOnAppEnd()
		{
			if (this.app_type == null)
			{
				return;
			}
			HttpApplication httpApplication = (HttpApplication)Activator.CreateInstance(this.app_type, true);
			this.FireEvent("Application_End", httpApplication, new object[]
			{
				new object(),
				EventArgs.Empty
			});
			httpApplication.DisposeInternal();
			this.app_type = null;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0000CECA File Offset: 0x0000B0CA
		public static void Dispose()
		{
			HttpApplicationFactory.theFactory.FireOnAppEnd();
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0000CED8 File Offset: 0x0000B0D8
		private static FileSystemWatcher CreateWatcher(string file, FileSystemEventHandler hnd, RenamedEventHandler reh)
		{
			FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
			fileSystemWatcher.Path = Path.GetFullPath(Path.GetDirectoryName(file));
			fileSystemWatcher.Filter = Path.GetFileName(file);
			fileSystemWatcher.NotifyFilter |= NotifyFilters.Size;
			fileSystemWatcher.Changed += hnd;
			fileSystemWatcher.Created += hnd;
			fileSystemWatcher.Deleted += hnd;
			fileSystemWatcher.Renamed += reh;
			fileSystemWatcher.EnableRaisingEvents = true;
			return fileSystemWatcher;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0000CF38 File Offset: 0x0000B138
		internal static void AttachEvents(HttpApplication app)
		{
			HttpApplicationFactory httpApplicationFactory = HttpApplicationFactory.theFactory;
			Hashtable applicationTypeEvents = httpApplicationFactory.GetApplicationTypeEvents(app);
			foreach (object obj in applicationTypeEvents.Keys)
			{
				string text = (string)obj;
				int num = text.IndexOf('_');
				string text2 = text.Substring(0, num);
				object obj2;
				if (text2 == "Application")
				{
					obj2 = app;
				}
				else
				{
					obj2 = app.Modules[text2];
					if (obj2 == null)
					{
						continue;
					}
				}
				string text3 = text.Substring(num + 1);
				EventInfo @event = obj2.GetType().GetEvent(text3);
				if (!(@event == null))
				{
					string text4 = text2 + "_" + text3;
					object obj3 = applicationTypeEvents[text4];
					if (obj3 != null)
					{
						if (text3 == "End" && text2 == "Session")
						{
							Interlocked.CompareExchange(ref httpApplicationFactory.session_end, obj3, null);
						}
						else if (obj3 is MethodInfo)
						{
							httpApplicationFactory.AddHandler(@event, obj2, app, (MethodInfo)obj3);
						}
						else
						{
							foreach (object obj4 in ((ArrayList)obj3))
							{
								MethodInfo methodInfo = (MethodInfo)obj4;
								httpApplicationFactory.AddHandler(@event, obj2, app, methodInfo);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0000D0E4 File Offset: 0x0000B2E4
		private void AddHandler(EventInfo evt, object target, HttpApplication app, MethodInfo method)
		{
			if (method.GetParameters().Length == 0)
			{
				NoParamsInvoker noParamsInvoker = new NoParamsInvoker(app, method);
				evt.AddEventHandler(target, noParamsInvoker.FakeDelegate);
				return;
			}
			if (method.IsStatic)
			{
				evt.AddEventHandler(target, Delegate.CreateDelegate(evt.EventHandlerType, method));
				return;
			}
			evt.AddEventHandler(target, Delegate.CreateDelegate(evt.EventHandlerType, app, method));
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0000D146 File Offset: 0x0000B346
		internal static void InvokeSessionEnd(object state)
		{
			HttpApplicationFactory.InvokeSessionEnd(state, null, EventArgs.Empty);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0000D154 File Offset: 0x0000B354
		internal static void InvokeSessionEnd(object state, object source, EventArgs e)
		{
			HttpApplicationFactory httpApplicationFactory = HttpApplicationFactory.theFactory;
			MethodInfo methodInfo = null;
			HttpApplication httpApplication = null;
			Stack stack = httpApplicationFactory.available_for_end;
			lock (stack)
			{
				methodInfo = (MethodInfo)httpApplicationFactory.session_end;
				if (methodInfo == null)
				{
					return;
				}
				httpApplication = HttpApplicationFactory.GetApplicationForSessionEnd();
			}
			httpApplication.SetSession((HttpSessionState)state);
			try
			{
				methodInfo.Invoke(httpApplication, new object[]
				{
					(source == null) ? httpApplication : source,
					e
				});
			}
			catch (global::System.Exception)
			{
			}
			HttpApplicationFactory.RecycleForSessionEnd(httpApplication);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0000D1F8 File Offset: 0x0000B3F8
		private static HttpStaticObjectsCollection MakeStaticCollection(ArrayList list)
		{
			if (list == null || list.Count == 0)
			{
				return null;
			}
			HttpStaticObjectsCollection httpStaticObjectsCollection = new HttpStaticObjectsCollection();
			foreach (object obj in list)
			{
				ObjectTagBuilder objectTagBuilder = (ObjectTagBuilder)obj;
				httpStaticObjectsCollection.Add(objectTagBuilder);
			}
			return httpStaticObjectsCollection;
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x0000D260 File Offset: 0x0000B460
		internal static HttpApplicationState ApplicationState
		{
			get
			{
				if (HttpApplicationFactory.theFactory.app_state == null)
				{
					HttpStaticObjectsCollection httpStaticObjectsCollection = HttpApplicationFactory.MakeStaticCollection(GlobalAsaxCompiler.ApplicationObjects);
					HttpStaticObjectsCollection httpStaticObjectsCollection2 = HttpApplicationFactory.MakeStaticCollection(GlobalAsaxCompiler.SessionObjects);
					HttpApplicationFactory.theFactory.app_state = new HttpApplicationState(httpStaticObjectsCollection, httpStaticObjectsCollection2);
				}
				return HttpApplicationFactory.theFactory.app_state;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x0000D2AA File Offset: 0x0000B4AA
		internal static Type AppType
		{
			get
			{
				return HttpApplicationFactory.theFactory.app_type;
			}
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000D2B8 File Offset: 0x0000B4B8
		private void InitType(HttpContext context)
		{
			object obj = this.this_lock;
			lock (obj)
			{
				if (this.needs_init)
				{
					try
					{
						string appDomainAppPath = HttpRuntime.AppDomainAppPath;
						string text = Path.Combine(appDomainAppPath, "Global.asax");
						if (!global::System.IO.File.Exists(text))
						{
							text = Path.Combine(appDomainAppPath, "global.asax");
							if (!global::System.IO.File.Exists(text))
							{
								text = null;
							}
						}
						BuildManager.CallPreStartMethods();
						BuildManager.CompilingTopLevelAssemblies = true;
						new AppResourcesCompiler(context).Compile();
						new AppWebReferencesCompiler().Compile();
						new AppCodeCompiler().Compile();
						BuildManager.AllowReferencedAssembliesCaching = true;
						string text2 = Path.Combine(HttpRuntime.MachineConfigurationDirectory, "Browsers");
						HttpApplicationFactory.default_machine_browsers_files = new string[0];
						if (Directory.Exists(text2))
						{
							HttpApplicationFactory.default_machine_browsers_files = Directory.GetFiles(text2, "*.browser");
						}
						string text3 = Path.Combine(Path.Combine(appDomainAppPath, "App_Data"), "Mono_Machine_Browsers");
						HttpApplicationFactory.app_mono_machine_browsers_files = new string[0];
						if (Directory.Exists(text3))
						{
							HttpApplicationFactory.app_mono_machine_browsers_files = Directory.GetFiles(text3, "*.browser");
						}
						string text4 = Path.Combine(appDomainAppPath, "App_Browsers");
						HttpApplicationFactory.app_browsers_files = new string[0];
						if (Directory.Exists(text4))
						{
							HttpApplicationFactory.app_browsers_files = Directory.GetFiles(text4, "*.browser");
						}
						BuildManager.CompilingTopLevelAssemblies = false;
						this.app_type = BuildManager.GetPrecompiledApplicationType();
						if (this.app_type == null && text != null)
						{
							this.app_type = BuildManager.GetCompiledType("~/" + Path.GetFileName(text));
							if (this.app_type == null)
							{
								throw new ApplicationException(string.Format("Error compiling application file ({0}).", text));
							}
						}
						else if (this.app_type == null)
						{
							this.app_type = typeof(HttpApplication);
							this.app_state = new HttpApplicationState();
						}
						HttpApplicationFactory.WatchLocationForRestart("?lobal.asax");
						ThreadPool.QueueUserWorkItem(delegate
						{
							try
							{
								HttpApplicationFactory.WatchLocationForRestart(string.Empty, "?eb.?onfig", true);
							}
							catch (global::System.Exception ex)
							{
								Console.Error.WriteLine(ex);
							}
						}, null);
						this.needs_init = false;
					}
					catch (global::System.Exception)
					{
						if (BuildManager.CodeAssemblies != null)
						{
							BuildManager.CodeAssemblies.Clear();
						}
						if (BuildManager.TopLevelAssemblies != null)
						{
							BuildManager.TopLevelAssemblies.Clear();
						}
						if (WebConfigurationManager.ExtraAssemblies != null)
						{
							WebConfigurationManager.ExtraAssemblies.Clear();
						}
						throw;
					}
				}
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0000D520 File Offset: 0x0000B720
		internal static HttpApplication GetApplication(HttpContext context)
		{
			HttpApplicationFactory httpApplicationFactory = HttpApplicationFactory.theFactory;
			HttpApplication httpApplication;
			if (httpApplicationFactory.app_start_needed)
			{
				if (context == null)
				{
					return null;
				}
				httpApplicationFactory.InitType(context);
				HttpApplicationFactory httpApplicationFactory2 = httpApplicationFactory;
				lock (httpApplicationFactory2)
				{
					if (httpApplicationFactory.app_start_needed)
					{
						string[] binDirs = HttpApplication.BinDirs;
						for (int i = 0; i < binDirs.Length; i++)
						{
							HttpApplicationFactory.WatchLocationForRestart(binDirs[i], "*.dll");
						}
						HttpApplicationFactory.WatchLocationForRestart(".", "App_Code");
						HttpApplicationFactory.WatchLocationForRestart(".", "App_Browsers");
						HttpApplicationFactory.WatchLocationForRestart(".", "App_GlobalResources");
						HttpApplicationFactory.WatchLocationForRestart("App_Code", "*", true);
						HttpApplicationFactory.WatchLocationForRestart("App_Browsers", "*");
						HttpApplicationFactory.WatchLocationForRestart("App_GlobalResources", "*");
						httpApplication = httpApplicationFactory.FireOnAppStart(context);
						httpApplicationFactory.app_start_needed = false;
						return httpApplication;
					}
				}
			}
			httpApplication = (HttpApplication)Interlocked.Exchange(ref httpApplicationFactory.next_free, null);
			if (httpApplication != null)
			{
				httpApplication.RequestCompleted = false;
				return httpApplication;
			}
			Stack stack = httpApplicationFactory.available;
			lock (stack)
			{
				if (httpApplicationFactory.available.Count > 0)
				{
					httpApplication = (HttpApplication)httpApplicationFactory.available.Pop();
					httpApplication.RequestCompleted = false;
					return httpApplication;
				}
			}
			return (HttpApplication)Activator.CreateInstance(httpApplicationFactory.app_type, true);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000D6AC File Offset: 0x0000B8AC
		private static HttpApplication GetApplicationForSessionEnd()
		{
			HttpApplicationFactory httpApplicationFactory = HttpApplicationFactory.theFactory;
			if (httpApplicationFactory.available_for_end.Count > 0)
			{
				return (HttpApplication)httpApplicationFactory.available_for_end.Pop();
			}
			HttpApplication httpApplication = (HttpApplication)Activator.CreateInstance(httpApplicationFactory.app_type, true);
			httpApplication.InitOnce(false);
			return httpApplication;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000D6F8 File Offset: 0x0000B8F8
		internal static void RecycleForSessionEnd(HttpApplication app)
		{
			bool flag = false;
			HttpApplicationFactory httpApplicationFactory = HttpApplicationFactory.theFactory;
			Stack stack = httpApplicationFactory.available_for_end;
			lock (stack)
			{
				if (httpApplicationFactory.available_for_end.Count < 64)
				{
					httpApplicationFactory.available_for_end.Push(app);
				}
				else
				{
					flag = true;
				}
			}
			if (flag)
			{
				app.Dispose();
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0000D764 File Offset: 0x0000B964
		internal static void Recycle(HttpApplication app)
		{
			bool flag = false;
			HttpApplicationFactory httpApplicationFactory = HttpApplicationFactory.theFactory;
			if (Interlocked.CompareExchange(ref httpApplicationFactory.next_free, app, null) == null)
			{
				return;
			}
			Stack stack = httpApplicationFactory.available;
			lock (stack)
			{
				if (httpApplicationFactory.available.Count < 64)
				{
					httpApplicationFactory.available.Push(app);
				}
				else
				{
					flag = true;
				}
			}
			if (flag)
			{
				app.Dispose();
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0000D7E0 File Offset: 0x0000B9E0
		internal static bool ContextAvailable
		{
			get
			{
				return HttpApplicationFactory.theFactory != null && !HttpApplicationFactory.theFactory.app_start_needed;
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0000D7F8 File Offset: 0x0000B9F8
		internal static bool WatchLocationForRestart(string filter)
		{
			return HttpApplicationFactory.WatchLocationForRestart(string.Empty, filter, false);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000D806 File Offset: 0x0000BA06
		internal static bool WatchLocationForRestart(string virtualPath, string filter)
		{
			return HttpApplicationFactory.WatchLocationForRestart(virtualPath, filter, false);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0000D810 File Offset: 0x0000BA10
		internal static bool WatchLocationForRestart(string virtualPath, string filter, bool watchSubdirs)
		{
			string text = HttpRuntime.AppDomainAppPath;
			text = Path.Combine(text, virtualPath);
			bool flag = Directory.Exists(text);
			bool flag2 = !flag && global::System.IO.File.Exists(text);
			if (flag || flag2)
			{
				FileSystemEventHandler fileSystemEventHandler = new FileSystemEventHandler(HttpApplicationFactory.OnFileChanged);
				RenamedEventHandler renamedEventHandler = new RenamedEventHandler(HttpApplicationFactory.OnFileRenamed);
				FileSystemWatcher fileSystemWatcher = HttpApplicationFactory.CreateWatcher(Path.Combine(text, filter), fileSystemEventHandler, renamedEventHandler);
				if (flag)
				{
					fileSystemWatcher.IncludeSubdirectories = watchSubdirs;
				}
				object obj = HttpApplicationFactory.watchers_lock;
				lock (obj)
				{
					HttpApplicationFactory.watchers.Add(fileSystemWatcher);
				}
				return true;
			}
			return false;
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0000D8BC File Offset: 0x0000BABC
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x0000D8C3 File Offset: 0x0000BAC3
		internal static bool ApplicationDisabled
		{
			get
			{
				return HttpApplicationFactory.app_disabled;
			}
			set
			{
				HttpApplicationFactory.app_disabled = value;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0000D8CB File Offset: 0x0000BACB
		internal static string[] AppBrowsersFiles
		{
			get
			{
				return HttpApplicationFactory.app_browsers_files;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0000D8D4 File Offset: 0x0000BAD4
		internal static ICapabilitiesProcess CapabilitiesProcessor
		{
			get
			{
				object obj = HttpApplicationFactory.capabilities_processor_lock;
				lock (obj)
				{
					if (HttpApplicationFactory.capabilities_processor == null)
					{
						HttpApplicationFactory.capabilities_processor = new Build();
						string[] array = HttpApplicationFactory.app_mono_machine_browsers_files;
						if (array.Length == 0)
						{
							array = HttpApplicationFactory.default_machine_browsers_files;
						}
						foreach (string text in array)
						{
							HttpApplicationFactory.capabilities_processor.AddBrowserFile(text);
						}
						foreach (string text2 in HttpApplicationFactory.app_browsers_files)
						{
							HttpApplicationFactory.capabilities_processor.AddBrowserFile(text2);
						}
					}
				}
				return HttpApplicationFactory.capabilities_processor;
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0000D984 File Offset: 0x0000BB84
		internal static void DisableWatchers()
		{
			object obj = HttpApplicationFactory.watchers_lock;
			lock (obj)
			{
				foreach (object obj2 in HttpApplicationFactory.watchers)
				{
					((FileSystemWatcher)obj2).EnableRaisingEvents = false;
				}
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0000DA00 File Offset: 0x0000BC00
		internal static void DisableWatcher(string virtualPath, string filter)
		{
			HttpApplicationFactory.EnableWatcherEvents(virtualPath, filter, false);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000DA0A File Offset: 0x0000BC0A
		internal static void EnableWatcher(string virtualPath, string filter)
		{
			HttpApplicationFactory.EnableWatcherEvents(virtualPath, filter, true);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000DA14 File Offset: 0x0000BC14
		private static void EnableWatcherEvents(string virtualPath, string filter, bool enable)
		{
			object obj = HttpApplicationFactory.watchers_lock;
			lock (obj)
			{
				foreach (object obj2 in HttpApplicationFactory.watchers)
				{
					FileSystemWatcher fileSystemWatcher = (FileSystemWatcher)obj2;
					if (string.Compare(fileSystemWatcher.Path, virtualPath, StringComparison.Ordinal) == 0 && string.Compare(fileSystemWatcher.Filter, filter, StringComparison.Ordinal) == 0)
					{
						fileSystemWatcher.EnableRaisingEvents = enable;
					}
				}
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000DAB4 File Offset: 0x0000BCB4
		internal static void EnableWatchers()
		{
			object obj = HttpApplicationFactory.watchers_lock;
			lock (obj)
			{
				foreach (object obj2 in HttpApplicationFactory.watchers)
				{
					((FileSystemWatcher)obj2).EnableRaisingEvents = true;
				}
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0000DB30 File Offset: 0x0000BD30
		private static void OnFileRenamed(object sender, RenamedEventArgs args)
		{
			HttpApplicationFactory.OnFileChanged(sender, args);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0000DB3C File Offset: 0x0000BD3C
		private static void OnFileChanged(object sender, FileSystemEventArgs args)
		{
			if (HttpRuntime.DomainUnloading)
			{
				return;
			}
			string name = args.Name;
			bool flag = false;
			if (StrUtils.EndsWith(name, "onfig", true))
			{
				if (string.Compare(Path.GetFileName(name), "web.config", true, Helpers.InvariantCulture) != 0)
				{
					return;
				}
				flag = true;
			}
			else if (StrUtils.EndsWith(name, "lobal.asax", true) && string.Compare(name, "global.asax", true, Helpers.InvariantCulture) != 0)
			{
				return;
			}
			Console.WriteLine("Change: " + name);
			FileSystemWatcher fileSystemWatcher = sender as FileSystemWatcher;
			if (fileSystemWatcher != null && string.Compare(fileSystemWatcher.Filter, "?eb.?onfig", true, Helpers.InvariantCulture) == 0 && Directory.Exists(name))
			{
				return;
			}
			if (flag && WebConfigurationManager.SuppressAppReload(true))
			{
				return;
			}
			object obj = HttpApplicationFactory.watchers_lock;
			lock (obj)
			{
				if (!HttpApplicationFactory.app_shutdown)
				{
					HttpApplicationFactory.app_shutdown = true;
					HttpApplicationFactory.DisableWatchers();
					HttpRuntime.UnloadAppDomain();
				}
			}
		}

		// Token: 0x04000EE3 RID: 3811
		private object this_lock = new object();

		// Token: 0x04000EE4 RID: 3812
		private static HttpApplicationFactory theFactory = new HttpApplicationFactory();

		// Token: 0x04000EE5 RID: 3813
		private object session_end;

		// Token: 0x04000EE6 RID: 3814
		private bool needs_init = true;

		// Token: 0x04000EE7 RID: 3815
		private bool app_start_needed = true;

		// Token: 0x04000EE8 RID: 3816
		private bool have_app_events;

		// Token: 0x04000EE9 RID: 3817
		private Type app_type;

		// Token: 0x04000EEA RID: 3818
		private HttpApplicationState app_state;

		// Token: 0x04000EEB RID: 3819
		private Hashtable app_event_handlers;

		// Token: 0x04000EEC RID: 3820
		private static ArrayList watchers = new ArrayList();

		// Token: 0x04000EED RID: 3821
		private static object watchers_lock = new object();

		// Token: 0x04000EEE RID: 3822
		private static bool app_shutdown = false;

		// Token: 0x04000EEF RID: 3823
		private static bool app_disabled = false;

		// Token: 0x04000EF0 RID: 3824
		private static string[] app_browsers_files = new string[0];

		// Token: 0x04000EF1 RID: 3825
		private static string[] default_machine_browsers_files = new string[0];

		// Token: 0x04000EF2 RID: 3826
		private static string[] app_mono_machine_browsers_files = new string[0];

		// Token: 0x04000EF3 RID: 3827
		private Stack available = new Stack();

		// Token: 0x04000EF4 RID: 3828
		private object next_free;

		// Token: 0x04000EF5 RID: 3829
		private Stack available_for_end = new Stack();

		// Token: 0x04000EF6 RID: 3830
		private static Build capabilities_processor = null;

		// Token: 0x04000EF7 RID: 3831
		private static object capabilities_processor_lock = new object();
	}
}
