using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Internal;
using System.Reflection;
using System.Threading;
using System.Web.Hosting;
using System.Web.Util;
using Mono.Web.Util;

namespace System.Web.Configuration
{
	/// <summary>Provides access to configuration files as they apply to Web applications.</summary>
	// Token: 0x020005F0 RID: 1520
	public static class WebConfigurationManager
	{
		// Token: 0x170014F7 RID: 5367
		// (get) Token: 0x06004202 RID: 16898 RVA: 0x000AC584 File Offset: 0x000AA784
		internal static ArrayList ExtraAssemblies
		{
			get
			{
				if (WebConfigurationManager.extra_assemblies == null)
				{
					WebConfigurationManager.extra_assemblies = new ArrayList();
				}
				return WebConfigurationManager.extra_assemblies;
			}
		}

		// Token: 0x06004203 RID: 16899 RVA: 0x000AC59C File Offset: 0x000AA79C
		static WebConfigurationManager()
		{
			int num = 100;
			bool flag = false;
			int num2;
			if (int.TryParse(Environment.GetEnvironmentVariable("MONO_ASPNET_WEBCONFIG_CACHESIZE"), out num2))
			{
				num = num2;
				flag = true;
				Console.WriteLine("WebConfigurationManager's LRUcache Size overriden to: {0} (via {1})", num2, "MONO_ASPNET_WEBCONFIG_CACHESIZE");
			}
			WebConfigurationManager.sectionCache = new LruCache<int, object>(num);
			string text = "WebConfigurationManager's LRUcache evictions count reached its max size";
			if (!flag)
			{
				text += string.Format("{0}Cache Size: {1} (overridable via {2})", Environment.NewLine, num, "MONO_ASPNET_WEBCONFIG_CACHESIZE");
			}
			WebConfigurationManager.sectionCache.EvictionWarning = text;
			WebConfigurationManager.configFactory = ConfigurationManager.ConfigurationFactory;
			Configuration.SaveStart += WebConfigurationManager.ConfigurationSaveHandler;
			Configuration.SaveEnd += WebConfigurationManager.ConfigurationSaveHandler;
			Type type = Type.GetType("System.Configuration.CustomizableFileSettingsProvider, System", false);
			if (type != null)
			{
				FieldInfo field = type.GetField("webConfigurationFileMapType", BindingFlags.Static | BindingFlags.NonPublic);
				if (field != null && field.FieldType == Type.GetType("System.Type"))
				{
					field.SetValue(null, typeof(ApplicationSettingsConfigurationFileMap));
				}
			}
			WebConfigurationManager.sectionCacheLock = new ReaderWriterLockSlim();
		}

		// Token: 0x06004204 RID: 16900 RVA: 0x000AC708 File Offset: 0x000AA908
		private static void ReenableWatcherOnConfigLocation(object state)
		{
			string text = state as string;
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			object obj = WebConfigurationManager.saveLocationsCacheLock;
			DateTime minValue;
			lock (obj)
			{
				if (!WebConfigurationManager.saveLocationsCache.TryGetValue(text, out minValue))
				{
					minValue = DateTime.MinValue;
				}
			}
			DateTime now = DateTime.Now;
			if (minValue == DateTime.MinValue || now.Subtract(minValue).TotalMilliseconds >= 6000.0)
			{
				WebConfigurationManager.saveLocationsTimer.Dispose();
				WebConfigurationManager.saveLocationsTimer = null;
				HttpApplicationFactory.EnableWatcher(VirtualPathUtility.RemoveTrailingSlash(HttpRuntime.AppDomainAppPath), "?eb.?onfig");
				return;
			}
			WebConfigurationManager.saveLocationsTimer.Change(6000, 6000);
		}

		// Token: 0x06004205 RID: 16901 RVA: 0x000AC7D4 File Offset: 0x000AA9D4
		private static void ConfigurationSaveHandler(Configuration sender, ConfigurationSaveEventArgs args)
		{
			try
			{
				WebConfigurationManager.sectionCacheLock.EnterWriteLock();
				WebConfigurationManager.sectionCache.Clear();
			}
			finally
			{
				WebConfigurationManager.sectionCacheLock.ExitWriteLock();
			}
			object obj = WebConfigurationManager.suppressAppReloadLock;
			lock (obj)
			{
				string webConfigFileName = WebConfigurationHost.GetWebConfigFileName(HttpRuntime.AppDomainAppPath);
				if (string.Compare(args.StreamPath, webConfigFileName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					WebConfigurationManager.SuppressAppReload(args.Start);
					if (args.Start)
					{
						HttpApplicationFactory.DisableWatcher(VirtualPathUtility.RemoveTrailingSlash(HttpRuntime.AppDomainAppPath), "?eb.?onfig");
						object obj2 = WebConfigurationManager.saveLocationsCacheLock;
						lock (obj2)
						{
							if (WebConfigurationManager.saveLocationsCache == null)
							{
								WebConfigurationManager.saveLocationsCache = new Dictionary<string, DateTime>(StringComparer.Ordinal);
							}
							if (WebConfigurationManager.saveLocationsCache.ContainsKey(webConfigFileName))
							{
								WebConfigurationManager.saveLocationsCache[webConfigFileName] = DateTime.Now;
							}
							else
							{
								WebConfigurationManager.saveLocationsCache.Add(webConfigFileName, DateTime.Now);
							}
							if (WebConfigurationManager.saveLocationsTimer == null)
							{
								WebConfigurationManager.saveLocationsTimer = new Timer(new TimerCallback(WebConfigurationManager.ReenableWatcherOnConfigLocation), webConfigFileName, 6000, 6000);
							}
						}
					}
				}
			}
		}

		/// <summary>Opens the machine-configuration file on the current computer as a <see cref="T:System.Configuration.Configuration" /> object to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x06004206 RID: 16902 RVA: 0x000AC91C File Offset: 0x000AAB1C
		public static Configuration OpenMachineConfiguration()
		{
			return ConfigurationManager.OpenMachineConfiguration();
		}

		/// <summary>Opens the machine-configuration file on the current computer as a <see cref="T:System.Configuration.Configuration" /> object to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="locationSubPath">The application path to which the machine configuration applies.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x06004207 RID: 16903 RVA: 0x000AC923 File Offset: 0x000AAB23
		[global::System.MonoLimitation("locationSubPath is not handled")]
		public static Configuration OpenMachineConfiguration(string locationSubPath)
		{
			return WebConfigurationManager.OpenMachineConfiguration();
		}

		/// <summary>Opens the specified machine-configuration file on the specified server as a <see cref="T:System.Configuration.Configuration" /> object to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="locationSubPath">The application path to which the configuration applies.</param>
		/// <param name="server">The fully qualified name of the server to return the configuration for.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x06004208 RID: 16904 RVA: 0x000AC92A File Offset: 0x000AAB2A
		[global::System.MonoLimitation("Mono does not support remote configuration")]
		public static Configuration OpenMachineConfiguration(string locationSubPath, string server)
		{
			if (server == null)
			{
				return WebConfigurationManager.OpenMachineConfiguration(locationSubPath);
			}
			throw new NotSupportedException("Mono doesn't support remote configuration");
		}

		/// <summary>Opens the specified machine-configuration file on the specified server as a <see cref="T:System.Configuration.Configuration" /> object, using the specified security context to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="locationSubPath">The application path to which the configuration applies.</param>
		/// <param name="server">The fully qualified name of the server to return the configuration for.</param>
		/// <param name="userToken">An account token to use.</param>
		/// <exception cref="T:System.ArgumentException">Valid values were not supplied for the <paramref name="server" /> or <paramref name="userToken" /> parameters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x06004209 RID: 16905 RVA: 0x000AC92A File Offset: 0x000AAB2A
		[global::System.MonoLimitation("Mono does not support remote configuration")]
		public static Configuration OpenMachineConfiguration(string locationSubPath, string server, IntPtr userToken)
		{
			if (server == null)
			{
				return WebConfigurationManager.OpenMachineConfiguration(locationSubPath);
			}
			throw new NotSupportedException("Mono doesn't support remote configuration");
		}

		/// <summary>Opens the specified machine-configuration file on the specified server as a <see cref="T:System.Configuration.Configuration" /> object, using the specified security context to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="locationSubPath">The application path to which the configuration applies. </param>
		/// <param name="server">The fully qualified name of the server to return the configuration for.</param>
		/// <param name="userName">The full user name (Domain\User) to use when opening the file.</param>
		/// <param name="password">The password for the user name.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="server" /> or <paramref name="userName" /> and <paramref name="password" /> parameters were invalid.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x0600420A RID: 16906 RVA: 0x000AC92A File Offset: 0x000AAB2A
		[global::System.MonoLimitation("Mono does not support remote configuration")]
		public static Configuration OpenMachineConfiguration(string locationSubPath, string server, string userName, string password)
		{
			if (server == null)
			{
				return WebConfigurationManager.OpenMachineConfiguration(locationSubPath);
			}
			throw new NotSupportedException("Mono doesn't support remote configuration");
		}

		/// <summary>Opens the Web-application configuration file as a <see cref="T:System.Configuration.Configuration" /> object using the specified virtual path to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="path">The virtual path to the configuration file. If null, the root Web.config file is opened.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x0600420B RID: 16907 RVA: 0x000AC940 File Offset: 0x000AAB40
		public static Configuration OpenWebConfiguration(string path)
		{
			return WebConfigurationManager.OpenWebConfiguration(path, null, null, null, null, null);
		}

		/// <summary>Opens the Web-application configuration file as a <see cref="T:System.Configuration.Configuration" /> object using the specified virtual path and site name to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="path">The virtual path to the configuration file. </param>
		/// <param name="site">The name of the application Web site, as displayed in Internet Information Services (IIS) configuration.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x0600420C RID: 16908 RVA: 0x000AC94D File Offset: 0x000AAB4D
		public static Configuration OpenWebConfiguration(string path, string site)
		{
			return WebConfigurationManager.OpenWebConfiguration(path, site, null, null, null, null);
		}

		/// <summary>Opens the Web-application configuration file as a <see cref="T:System.Configuration.Configuration" /> object using the specified virtual path, site name, and location to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="path">The virtual path to the configuration file. </param>
		/// <param name="site">The name of the application Web site, as displayed in Internet Information Services (IIS) configuration.</param>
		/// <param name="locationSubPath">The specific resource to which the configuration applies.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x0600420D RID: 16909 RVA: 0x000AC95A File Offset: 0x000AAB5A
		public static Configuration OpenWebConfiguration(string path, string site, string locationSubPath)
		{
			return WebConfigurationManager.OpenWebConfiguration(path, site, locationSubPath, null, null, null);
		}

		/// <summary>Opens the Web-application configuration file as a <see cref="T:System.Configuration.Configuration" /> object using the specified virtual path, site name, location, and server to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="path">The virtual path to the configuration file. </param>
		/// <param name="site">The name of the application Web site, as displayed in Internet Information Services (IIS) configuration.</param>
		/// <param name="locationSubPath">The specific resource to which the configuration applies. </param>
		/// <param name="server">The network name of the server the Web application resides on.</param>
		/// <exception cref="T:System.ArgumentException">The server parameter was invalid.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x0600420E RID: 16910 RVA: 0x000AC967 File Offset: 0x000AAB67
		public static Configuration OpenWebConfiguration(string path, string site, string locationSubPath, string server)
		{
			return WebConfigurationManager.OpenWebConfiguration(path, site, locationSubPath, server, null, null);
		}

		/// <summary>Opens the Web-application configuration file as a <see cref="T:System.Configuration.Configuration" /> object using the specified virtual path, site name, location, server, and security context to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="path">The virtual path to the configuration file. </param>
		/// <param name="site">The name of the application Web site, as displayed in Internet Information Services (IIS) configuration.</param>
		/// <param name="locationSubPath">The specific resource to which the configuration applies.</param>
		/// <param name="server">The network name of the server the Web application resides on.</param>
		/// <param name="userToken">An account token to use.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="server" /> or <paramref name="userToken" /> parameters were invalid.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x0600420F RID: 16911 RVA: 0x000AC967 File Offset: 0x000AAB67
		public static Configuration OpenWebConfiguration(string path, string site, string locationSubPath, string server, IntPtr userToken)
		{
			return WebConfigurationManager.OpenWebConfiguration(path, site, locationSubPath, server, null, null);
		}

		/// <summary>Opens the Web-application configuration file as a <see cref="T:System.Configuration.Configuration" /> object using the specified virtual path, site name, location, server, and security context to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="path">The virtual path to the configuration file. </param>
		/// <param name="site">The name of the application Web site, as displayed in Internet Information Services (IIS) configuration.</param>
		/// <param name="locationSubPath">The specific resource to which the configuration applies. </param>
		/// <param name="server">The network name of the server the Web application resides on.</param>
		/// <param name="userName">The full user name (Domain\User) to use when opening the file.</param>
		/// <param name="password">The password for the user name.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="server" /> or <paramref name="userName" /> and <paramref name="password" /> parameters were invalid.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Could not load a valid configuration file.</exception>
		// Token: 0x06004210 RID: 16912 RVA: 0x000AC974 File Offset: 0x000AAB74
		public static Configuration OpenWebConfiguration(string path, string site, string locationSubPath, string server, string userName, string password)
		{
			return WebConfigurationManager.OpenWebConfiguration(path, site, locationSubPath, server, null, null, false);
		}

		// Token: 0x06004211 RID: 16913 RVA: 0x000AC984 File Offset: 0x000AAB84
		private static Configuration OpenWebConfiguration(string path, string site, string locationSubPath, string server, string userName, string password, bool fweb)
		{
			if (string.IsNullOrEmpty(path))
			{
				path = "/";
			}
			bool flag = false;
			if (!fweb && !string.IsNullOrEmpty(path))
			{
				path = WebConfigurationManager.FindWebConfig(path, out flag);
			}
			string text = string.Concat(new string[] { path, site, locationSubPath, server, userName, password });
			Configuration configuration = (Configuration)WebConfigurationManager.configurations[text];
			if (configuration == null)
			{
				configuration = WebConfigurationManager.ConfigurationFactory.Create(typeof(WebConfigurationHost), new object[] { null, path, site, locationSubPath, server, userName, password, flag });
				WebConfigurationManager.configurations[text] = configuration;
			}
			return configuration;
		}

		/// <summary>Opens the specified Web-application configuration file as a <see cref="T:System.Configuration.Configuration" /> object using the specified file mapping and virtual path to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="fileMap">The <see cref="T:System.Web.Configuration.WebConfigurationFileMap" /> object to use in place of a default Web-application configuration file.</param>
		/// <param name="path">The virtual path to the configuration file. </param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x06004212 RID: 16914 RVA: 0x000ACA3F File Offset: 0x000AAC3F
		public static Configuration OpenMappedWebConfiguration(WebConfigurationFileMap fileMap, string path)
		{
			return WebConfigurationManager.ConfigurationFactory.Create(typeof(WebConfigurationHost), new object[] { fileMap, path });
		}

		/// <summary>Opens the specified Web application configuration file as a <see cref="T:System.Configuration.Configuration" /> object using the specified file mapping, virtual path, and site name to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="fileMap">The <see cref="T:System.Web.Configuration.WebConfigurationFileMap" /> object to use in place of a default Web-application configuration-file mapping.</param>
		/// <param name="path">The virtual path to the configuration file.</param>
		/// <param name="site">The name of the application Web site, as displayed in Internet Information Services (IIS) configuration.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x06004213 RID: 16915 RVA: 0x000ACA63 File Offset: 0x000AAC63
		public static Configuration OpenMappedWebConfiguration(WebConfigurationFileMap fileMap, string path, string site)
		{
			return WebConfigurationManager.ConfigurationFactory.Create(typeof(WebConfigurationHost), new object[] { fileMap, path, site });
		}

		/// <summary>Opens the specified Web-application configuration file as a <see cref="T:System.Configuration.Configuration" /> object using the specified file mapping, virtual path, site name, and location to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="fileMap">The <see cref="T:System.Web.Configuration.WebConfigurationFileMap" /> object to use in place of a default Web-application configuration-file mapping.</param>
		/// <param name="path">The virtual path to the configuration file. </param>
		/// <param name="site">The name of the application Web site, as displayed in Internet Information Services (IIS) configuration.</param>
		/// <param name="locationSubPath">The specific resource to which the configuration applies.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x06004214 RID: 16916 RVA: 0x000ACA8B File Offset: 0x000AAC8B
		public static Configuration OpenMappedWebConfiguration(WebConfigurationFileMap fileMap, string path, string site, string locationSubPath)
		{
			return WebConfigurationManager.ConfigurationFactory.Create(typeof(WebConfigurationHost), new object[] { fileMap, path, site, locationSubPath });
		}

		/// <summary>Opens the machine-configuration file as a <see cref="T:System.Configuration.Configuration" /> object, using the specified file mapping to allow read or write operations. </summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="fileMap">The <see cref="T:System.Configuration.ConfigurationFileMap" /> object to use in place of the default machine-configuration file.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x06004215 RID: 16917 RVA: 0x000ACAB7 File Offset: 0x000AACB7
		public static Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap)
		{
			return WebConfigurationManager.ConfigurationFactory.Create(typeof(WebConfigurationHost), new object[] { fileMap });
		}

		/// <summary>Opens the machine-configuration file as a <see cref="T:System.Configuration.Configuration" /> object using the specified file mapping and location to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="fileMap">The <see cref="T:System.Configuration.ConfigurationFileMap" /> object to use in place of a default machine-configuration file.</param>
		/// <param name="locationSubPath">The specific resource to which the configuration applies.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x06004216 RID: 16918 RVA: 0x000ACAD7 File Offset: 0x000AACD7
		public static Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap, string locationSubPath)
		{
			return WebConfigurationManager.OpenMappedMachineConfiguration(fileMap);
		}

		// Token: 0x06004217 RID: 16919 RVA: 0x000ACAE0 File Offset: 0x000AACE0
		internal static object SafeGetSection(string sectionName, Type configSectionType)
		{
			object obj;
			try
			{
				obj = WebConfigurationManager.GetSection(sectionName);
			}
			catch (Exception)
			{
				if (configSectionType != null)
				{
					obj = Activator.CreateInstance(configSectionType);
				}
				else
				{
					obj = null;
				}
			}
			return obj;
		}

		// Token: 0x06004218 RID: 16920 RVA: 0x000ACB20 File Offset: 0x000AAD20
		internal static object SafeGetSection(string sectionName, string path, Type configSectionType)
		{
			object obj;
			try
			{
				obj = WebConfigurationManager.GetSection(sectionName, path);
			}
			catch (Exception)
			{
				if (configSectionType != null)
				{
					obj = Activator.CreateInstance(configSectionType);
				}
				else
				{
					obj = null;
				}
			}
			return obj;
		}

		/// <summary>Retrieves the specified configuration section from the current Web application's configuration file.</summary>
		/// <returns>The specified configuration section object, or null if the section does not exist. Remember that security restrictions exist on the use of <see cref="M:System.Web.Configuration.WebConfigurationManager.GetSection(System.String)" /> as a runtime operation. You might not be able to access a section at run time for modifications, for example.</returns>
		/// <param name="sectionName">The configuration section name.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x06004219 RID: 16921 RVA: 0x000ACB60 File Offset: 0x000AAD60
		public static object GetSection(string sectionName)
		{
			HttpContext httpContext = HttpContext.Current;
			return WebConfigurationManager.GetSection(sectionName, WebConfigurationManager.GetCurrentPath(httpContext), httpContext);
		}

		/// <summary>Retrieves the specified configuration section from the Web application's configuration file at the specified location.</summary>
		/// <returns>The specified configuration section object, or null if the section does not exist. Remember that security restrictions exist on the use of <see cref="M:System.Web.Configuration.WebConfigurationManager.GetSection(System.String,System.String)" /> as a run-time operation. You might not be able to access a section at run time for modifications, for instance.</returns>
		/// <param name="sectionName">The configuration section name.</param>
		/// <param name="path">The virtual configuration file path.</param>
		/// <exception cref="T:System.InvalidOperationException">The method is called from outside a Web application.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x0600421A RID: 16922 RVA: 0x000ACB80 File Offset: 0x000AAD80
		public static object GetSection(string sectionName, string path)
		{
			return WebConfigurationManager.GetSection(sectionName, path, HttpContext.Current);
		}

		// Token: 0x0600421B RID: 16923 RVA: 0x000ACB90 File Offset: 0x000AAD90
		private static bool LookUpLocation(string relativePath, ref Configuration defaultConfiguration)
		{
			if (string.IsNullOrEmpty(relativePath))
			{
				return false;
			}
			Configuration configuration = defaultConfiguration.FindLocationConfiguration(relativePath, defaultConfiguration);
			if (configuration == defaultConfiguration)
			{
				return false;
			}
			defaultConfiguration = configuration;
			return true;
		}

		// Token: 0x0600421C RID: 16924 RVA: 0x000ACBC0 File Offset: 0x000AADC0
		internal static object GetSection(string sectionName, string path, HttpContext context)
		{
			if (string.IsNullOrEmpty(sectionName))
			{
				return null;
			}
			Configuration configuration = WebConfigurationManager.OpenWebConfiguration(path, null, null, null, null, null, false);
			string configPath = configuration.ConfigPath;
			int num = 0;
			bool flag = !string.IsNullOrEmpty(path);
			string text = null;
			if (flag)
			{
				text = "location_" + path;
			}
			num = sectionName.GetHashCode();
			if (configPath != null)
			{
				num ^= configPath.GetHashCode();
			}
			int num2;
			try
			{
				WebConfigurationManager.sectionCacheLock.EnterWriteLock();
				object obj;
				if (flag)
				{
					num2 = num ^ text.GetHashCode();
					if (WebConfigurationManager.sectionCache.TryGetValue(num2, out obj))
					{
						return obj;
					}
					num2 = num ^ path.GetHashCode();
					if (WebConfigurationManager.sectionCache.TryGetValue(num2, out obj))
					{
						return obj;
					}
				}
				if (WebConfigurationManager.sectionCache.TryGetValue(num, out obj))
				{
					return obj;
				}
			}
			finally
			{
				WebConfigurationManager.sectionCacheLock.ExitWriteLock();
			}
			string text2 = null;
			if (flag)
			{
				string text3;
				if (VirtualPathUtility.IsRooted(path))
				{
					if (path[0] == '~')
					{
						text3 = ((path.Length > 1) ? path.Substring(2) : string.Empty);
					}
					else if (path[0] == '/')
					{
						text3 = path.Substring(1);
					}
					else
					{
						text3 = path;
					}
				}
				else
				{
					text3 = path;
				}
				HttpRequest httpRequest = ((context != null) ? context.Request : null);
				if (httpRequest != null)
				{
					string text4 = VirtualPathUtility.GetDirectory(httpRequest.PathNoValidation);
					if (text4 != null)
					{
						text4 = text4.TrimEnd(WebConfigurationManager.pathTrimChars);
						if (string.Compare(configuration.ConfigPath, text4, StringComparison.Ordinal) != 0 && WebConfigurationManager.LookUpLocation(text4.Trim(WebConfigurationManager.pathTrimChars), ref configuration))
						{
							text2 = path;
						}
					}
				}
				if (WebConfigurationManager.LookUpLocation(text3, ref configuration))
				{
					text2 = text;
				}
				else
				{
					text2 = path;
				}
			}
			object obj2 = WebConfigurationManager.getSectionLock;
			ConfigurationSection section;
			lock (obj2)
			{
				section = configuration.GetSection(sectionName);
			}
			if (section == null)
			{
				return null;
			}
			object obj3 = SettingsMappingManager.MapSection(section.GetRuntimeObject());
			if (text2 != null)
			{
				num2 = num ^ text2.GetHashCode();
			}
			else
			{
				num2 = num;
			}
			WebConfigurationManager.AddSectionToCache(num2, obj3);
			return obj3;
		}

		// Token: 0x0600421D RID: 16925 RVA: 0x000ACDD4 File Offset: 0x000AAFD4
		private static string MapPath(HttpRequest req, string virtualPath)
		{
			if (req != null)
			{
				return req.MapPath(virtualPath);
			}
			string appDomainAppVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
			if (string.IsNullOrEmpty(appDomainAppVirtualPath) || !virtualPath.StartsWith(appDomainAppVirtualPath, StringComparison.Ordinal))
			{
				return null;
			}
			if (string.Compare(virtualPath, appDomainAppVirtualPath, StringComparison.Ordinal) == 0)
			{
				return HttpRuntime.AppDomainAppPath;
			}
			return UrlUtils.Combine(HttpRuntime.AppDomainAppPath, virtualPath.Substring(appDomainAppVirtualPath.Length));
		}

		// Token: 0x0600421E RID: 16926 RVA: 0x000ACE2C File Offset: 0x000AB02C
		private static string GetParentDir(string rootPath, string curPath)
		{
			int num = curPath.Length - 1;
			if (num > 0 && curPath[num] == '/')
			{
				curPath = curPath.Substring(0, num);
			}
			if (string.Compare(curPath, rootPath, StringComparison.Ordinal) == 0)
			{
				return null;
			}
			int num2 = curPath.LastIndexOf('/');
			if (num2 == -1)
			{
				return curPath;
			}
			if (num2 == 0)
			{
				return "/";
			}
			return curPath.Substring(0, num2);
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x000ACE88 File Offset: 0x000AB088
		internal static string FindWebConfig(string path)
		{
			bool flag;
			return WebConfigurationManager.FindWebConfig(path, out flag);
		}

		// Token: 0x06004220 RID: 16928 RVA: 0x000ACEA0 File Offset: 0x000AB0A0
		internal static string FindWebConfig(string path, out bool inAnotherApp)
		{
			inAnotherApp = false;
			if (string.IsNullOrEmpty(path))
			{
				return path;
			}
			if (HostingEnvironment.VirtualPathProvider != null && HostingEnvironment.VirtualPathProvider.DirectoryExists(path))
			{
				path = VirtualPathUtility.AppendTrailingSlash(path);
			}
			string appDomainAppVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
			WebConfigurationManager.ConfigPath configPath = WebConfigurationManager.configPaths[path] as WebConfigurationManager.ConfigPath;
			if (configPath != null)
			{
				inAnotherApp = configPath.InAnotherApp;
				return configPath.Path;
			}
			HttpContext httpContext = HttpContext.Current;
			HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
			string text = ((httpRequest != null) ? VirtualPathUtility.AppendTrailingSlash(WebConfigurationManager.MapPath(httpRequest, path)) : null);
			string appDomainAppPath = HttpRuntime.AppDomainAppPath;
			if (text != null && appDomainAppPath != null && !text.StartsWith(appDomainAppPath, StringComparison.Ordinal))
			{
				inAnotherApp = true;
			}
			string text2;
			if (inAnotherApp || path[path.Length - 1] == '/')
			{
				text2 = path;
			}
			else
			{
				text2 = VirtualPathUtility.GetDirectory(path, false);
				if (text2 == null)
				{
					return path;
				}
			}
			configPath = WebConfigurationManager.configPaths[text2] as WebConfigurationManager.ConfigPath;
			if (configPath != null)
			{
				inAnotherApp = configPath.InAnotherApp;
				return configPath.Path;
			}
			if (httpRequest == null)
			{
				return path;
			}
			configPath = new WebConfigurationManager.ConfigPath(path, inAnotherApp);
			while (string.Compare(configPath.Path, appDomainAppVirtualPath, StringComparison.Ordinal) != 0)
			{
				text = WebConfigurationManager.MapPath(httpRequest, configPath.Path);
				if (text == null)
				{
					configPath.Path = appDomainAppVirtualPath;
					break;
				}
				if (WebConfigurationHost.GetWebConfigFileName(text) != null)
				{
					break;
				}
				configPath.Path = WebConfigurationManager.GetParentDir(appDomainAppVirtualPath, configPath.Path);
				if (configPath.Path == null || configPath.Path == "~")
				{
					configPath.Path = appDomainAppVirtualPath;
					break;
				}
			}
			if (string.Compare(configPath.Path, path, StringComparison.Ordinal) != 0)
			{
				WebConfigurationManager.configPaths[path] = configPath;
			}
			else
			{
				WebConfigurationManager.configPaths[text2] = configPath;
			}
			return configPath.Path;
		}

		// Token: 0x06004221 RID: 16929 RVA: 0x000AD03C File Offset: 0x000AB23C
		private static string GetCurrentPath(HttpContext ctx)
		{
			HttpRequest httpRequest = ((ctx != null) ? ctx.Request : null);
			if (httpRequest == null)
			{
				return HttpRuntime.AppDomainAppVirtualPath;
			}
			return httpRequest.PathNoValidation;
		}

		// Token: 0x06004222 RID: 16930 RVA: 0x000AD068 File Offset: 0x000AB268
		internal static bool SuppressAppReload(bool newValue)
		{
			object obj = WebConfigurationManager.suppressAppReloadLock;
			bool flag2;
			lock (obj)
			{
				flag2 = WebConfigurationManager.suppressAppReload;
				WebConfigurationManager.suppressAppReload = newValue;
			}
			return flag2;
		}

		// Token: 0x06004223 RID: 16931 RVA: 0x000AD0B0 File Offset: 0x000AB2B0
		internal static void RemoveConfigurationFromCache(HttpContext ctx)
		{
			WebConfigurationManager.configurations.Remove(WebConfigurationManager.GetCurrentPath(ctx));
		}

		/// <summary>Retrieves the specified configuration section from the current Web application's configuration file.</summary>
		/// <returns>The specified configuration section object, or null if the section does not exist, or an internal object if the section is not accessible at run time.</returns>
		/// <param name="sectionName">The configuration section name.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x06004224 RID: 16932 RVA: 0x000AD0C4 File Offset: 0x000AB2C4
		public static object GetWebApplicationSection(string sectionName)
		{
			HttpContext httpContext = HttpContext.Current;
			HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
			string text = ((httpRequest != null) ? httpRequest.ApplicationPath : null);
			return WebConfigurationManager.GetSection(sectionName, string.IsNullOrEmpty(text) ? string.Empty : text);
		}

		/// <summary>Gets the Web site's application settings.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> object that contains the <see cref="T:System.Configuration.AppSettingsSection" /> object for the current Web application's default configuration. </returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid <see cref="T:System.Collections.Specialized.NameValueCollection" /> object could not be retrieved with the application settings data.</exception>
		// Token: 0x170014F8 RID: 5368
		// (get) Token: 0x06004225 RID: 16933 RVA: 0x000AD107 File Offset: 0x000AB307
		public static NameValueCollection AppSettings
		{
			get
			{
				return ConfigurationManager.AppSettings;
			}
		}

		/// <summary>Gets the Web site's connection strings.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConnectionStringSettingsCollection" /> object that contains the contents of the <see cref="T:System.Configuration.ConnectionStringsSection" /> object for the current Web application's default configuration. </returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid <see cref="T:System.Configuration.ConnectionStringSettingsCollection" /> object could not be retrieved.</exception>
		// Token: 0x170014F9 RID: 5369
		// (get) Token: 0x06004226 RID: 16934 RVA: 0x000AD10E File Offset: 0x000AB30E
		public static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return ConfigurationManager.ConnectionStrings;
			}
		}

		// Token: 0x170014FA RID: 5370
		// (get) Token: 0x06004227 RID: 16935 RVA: 0x000AD115 File Offset: 0x000AB315
		internal static IInternalConfigConfigurationFactory ConfigurationFactory
		{
			get
			{
				return WebConfigurationManager.configFactory;
			}
		}

		// Token: 0x06004228 RID: 16936 RVA: 0x000AD11C File Offset: 0x000AB31C
		private static void AddSectionToCache(int key, object section)
		{
			bool flag = false;
			try
			{
				if (WebConfigurationManager.sectionCacheLock.TryEnterWriteLock(200))
				{
					flag = true;
					object obj;
					if (!WebConfigurationManager.sectionCache.TryGetValue(key, out obj) || obj == null)
					{
						WebConfigurationManager.sectionCache.Add(key, section);
					}
				}
			}
			finally
			{
				if (flag)
				{
					WebConfigurationManager.sectionCacheLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x000AD180 File Offset: 0x000AB380
		internal static void Init()
		{
			object obj = WebConfigurationManager.lockobj;
			lock (obj)
			{
				if (WebConfigurationManager.config == null)
				{
					Web20DefaultConfig instance = Web20DefaultConfig.GetInstance();
					MethodInfo method = typeof(ConfigurationSettings).GetMethod("ChangeConfigurationSystem", BindingFlags.Static | BindingFlags.NonPublic);
					if (method == null)
					{
						throw new ConfigurationException("Cannot find method CCS");
					}
					object[] array = new object[] { instance };
					WebConfigurationManager.oldConfig = (IConfigurationSystem)method.Invoke(null, array);
					WebConfigurationManager.config = instance;
					WebConfigurationManager.config.Init();
					HttpConfigurationSystem httpConfigurationSystem = new HttpConfigurationSystem();
					MethodInfo method2 = typeof(ConfigurationManager).GetMethod("ChangeConfigurationSystem", BindingFlags.Static | BindingFlags.NonPublic);
					if (method2 == null)
					{
						throw new ConfigurationException("Cannot find method CCS");
					}
					object[] array2 = new object[] { httpConfigurationSystem };
					method2.Invoke(null, array2);
				}
			}
		}

		// Token: 0x04002350 RID: 9040
		private const int SAVE_LOCATIONS_CHECK_INTERVAL = 6000;

		// Token: 0x04002351 RID: 9041
		private const int SECTION_CACHE_LOCK_TIMEOUT = 200;

		// Token: 0x04002352 RID: 9042
		private static readonly char[] pathTrimChars = new char[] { '/' };

		// Token: 0x04002353 RID: 9043
		private static readonly object suppressAppReloadLock = new object();

		// Token: 0x04002354 RID: 9044
		private static readonly object saveLocationsCacheLock = new object();

		// Token: 0x04002355 RID: 9045
		private static readonly object getSectionLock = new object();

		// Token: 0x04002356 RID: 9046
		private static readonly ReaderWriterLockSlim sectionCacheLock;

		// Token: 0x04002357 RID: 9047
		private static IInternalConfigConfigurationFactory configFactory;

		// Token: 0x04002358 RID: 9048
		private static Hashtable configurations = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04002359 RID: 9049
		private static Hashtable configPaths = Hashtable.Synchronized(new Hashtable());

		// Token: 0x0400235A RID: 9050
		private static bool suppressAppReload;

		// Token: 0x0400235B RID: 9051
		private static Dictionary<string, DateTime> saveLocationsCache;

		// Token: 0x0400235C RID: 9052
		private static Timer saveLocationsTimer;

		// Token: 0x0400235D RID: 9053
		private static ArrayList extra_assemblies = null;

		// Token: 0x0400235E RID: 9054
		private const int DEFAULT_SECTION_CACHE_SIZE = 100;

		// Token: 0x0400235F RID: 9055
		private const string CACHE_SIZE_OVERRIDING_KEY = "MONO_ASPNET_WEBCONFIG_CACHESIZE";

		// Token: 0x04002360 RID: 9056
		private static LruCache<int, object> sectionCache;

		// Token: 0x04002361 RID: 9057
		internal static IConfigurationSystem oldConfig;

		// Token: 0x04002362 RID: 9058
		private static Web20DefaultConfig config;

		// Token: 0x04002363 RID: 9059
		private const BindingFlags privStatic = BindingFlags.Static | BindingFlags.NonPublic;

		// Token: 0x04002364 RID: 9060
		private static readonly object lockobj = new object();

		// Token: 0x020005F1 RID: 1521
		private sealed class ConfigPath
		{
			// Token: 0x0600422A RID: 16938 RVA: 0x000AD274 File Offset: 0x000AB474
			public ConfigPath(string path, bool inAnotherApp)
			{
				this.Path = path;
				this.InAnotherApp = inAnotherApp;
			}

			// Token: 0x04002365 RID: 9061
			public string Path;

			// Token: 0x04002366 RID: 9062
			public bool InAnotherApp;
		}
	}
}
