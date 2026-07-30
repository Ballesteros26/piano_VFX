using System;
using System.Configuration;
using System.Configuration.Internal;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x020005EF RID: 1519
	internal class WebConfigurationHost : IInternalConfigHost
	{
		// Token: 0x060041D3 RID: 16851 RVA: 0x000ABF0C File Offset: 0x000AA10C
		public virtual object CreateConfigurationContext(string configPath, string locationSubPath)
		{
			return new WebContext(WebApplicationLevel.AtApplication, "", "", configPath, locationSubPath);
		}

		// Token: 0x060041D4 RID: 16852 RVA: 0x000ABF21 File Offset: 0x000AA121
		public virtual object CreateDeprecatedConfigContext(string configPath)
		{
			return new HttpConfigurationContext(configPath);
		}

		// Token: 0x060041D5 RID: 16853 RVA: 0x000ABF29 File Offset: 0x000AA129
		public virtual string DecryptSection(string encryptedXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedSection)
		{
			if (protectedSection == null)
			{
				throw new ArgumentNullException("protectedSection");
			}
			return protectedSection.EncryptSection(encryptedXml, protectionProvider);
		}

		// Token: 0x060041D6 RID: 16854 RVA: 0x000ABF41 File Offset: 0x000AA141
		public virtual void DeleteStream(string streamName)
		{
			File.Delete(streamName);
		}

		// Token: 0x060041D7 RID: 16855 RVA: 0x000ABF29 File Offset: 0x000AA129
		public virtual string EncryptSection(string clearXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedSection)
		{
			if (protectedSection == null)
			{
				throw new ArgumentNullException("protectedSection");
			}
			return protectedSection.EncryptSection(clearXml, protectionProvider);
		}

		// Token: 0x060041D8 RID: 16856 RVA: 0x000ABF4C File Offset: 0x000AA14C
		public virtual string GetConfigPathFromLocationSubPath(string configPath, string locationSubPath)
		{
			if (!string.IsNullOrEmpty(locationSubPath) && !string.IsNullOrEmpty(configPath))
			{
				string text = ((configPath.Length == 1) ? null : (configPath.Substring(1) + "/"));
				if (text != null && locationSubPath.StartsWith(text, StringComparison.Ordinal))
				{
					locationSubPath = locationSubPath.Substring(text.Length);
				}
			}
			string text2 = configPath + "/" + locationSubPath;
			if (!string.IsNullOrEmpty(text2) && text2[0] == '/')
			{
				return text2.Substring(1);
			}
			return text2;
		}

		// Token: 0x060041D9 RID: 16857 RVA: 0x000ABFCB File Offset: 0x000AA1CB
		public virtual Type GetConfigType(string typeName, bool throwOnError)
		{
			Type type = HttpApplication.LoadType(typeName);
			if (type == null && throwOnError)
			{
				throw new ConfigurationErrorsException("Type not found: '" + typeName + "'");
			}
			return type;
		}

		// Token: 0x060041DA RID: 16858 RVA: 0x000ABFF4 File Offset: 0x000AA1F4
		public virtual string GetConfigTypeName(Type t)
		{
			return t.AssemblyQualifiedName;
		}

		// Token: 0x060041DB RID: 16859 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void GetRestrictedPermissions(IInternalConfigRecord configRecord, out PermissionSet permissionSet, out bool isHostReady)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041DC RID: 16860 RVA: 0x000ABFFC File Offset: 0x000AA1FC
		public virtual string GetStreamName(string configPath)
		{
			if (configPath == ":machine:")
			{
				if (this.map == null)
				{
					return RuntimeEnvironment.SystemConfigurationFile;
				}
				return this.map.MachineConfigFilename;
			}
			else
			{
				if (configPath == ":web:")
				{
					string text;
					if (this.map == null)
					{
						text = Path.GetDirectoryName(RuntimeEnvironment.SystemConfigurationFile);
					}
					else
					{
						text = Path.GetDirectoryName(this.map.MachineConfigFilename);
					}
					return WebConfigurationHost.GetWebConfigFileName(text);
				}
				return WebConfigurationHost.GetWebConfigFileName(this.MapPath(configPath));
			}
		}

		// Token: 0x060041DD RID: 16861 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string GetStreamNameForConfigSource(string streamName, string configSource)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041DE RID: 16862 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual object GetStreamVersion(string streamName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041DF RID: 16863 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual IDisposable Impersonate()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041E0 RID: 16864 RVA: 0x0000393A File Offset: 0x00001B3A
		public virtual void Init(IInternalConfigRoot root, params object[] hostInitParams)
		{
		}

		// Token: 0x060041E1 RID: 16865 RVA: 0x000AC078 File Offset: 0x000AA278
		public virtual void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot root, params object[] hostInitConfigurationParams)
		{
			string text = (string)hostInitConfigurationParams[1];
			this.map = (WebConfigurationFileMap)hostInitConfigurationParams[0];
			bool flag = false;
			if (hostInitConfigurationParams.Length > 7 && hostInitConfigurationParams[7] is bool)
			{
				flag = (bool)hostInitConfigurationParams[7];
			}
			if (flag)
			{
				this.appVirtualPath = text;
			}
			else
			{
				this.appVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
			}
			if (locationSubPath == ":web:")
			{
				locationSubPath = ":machine:";
				configPath = ":web:";
				locationConfigPath = null;
				return;
			}
			if (locationSubPath == ":machine:")
			{
				locationSubPath = null;
				configPath = ":machine:";
				locationConfigPath = null;
				return;
			}
			if (locationSubPath == null)
			{
				configPath = text;
				if (configPath.Length > 1)
				{
					configPath = VirtualPathUtility.RemoveTrailingSlash(configPath);
				}
			}
			else
			{
				configPath = locationSubPath;
			}
			int num;
			if (configPath == HttpRuntime.AppDomainAppVirtualPath || configPath == "/")
			{
				num = -1;
			}
			else
			{
				num = configPath.LastIndexOf("/");
			}
			if (num == -1)
			{
				locationSubPath = ":web:";
				locationConfigPath = null;
				return;
			}
			locationConfigPath = configPath.Substring(num + 1);
			if (num == 0)
			{
				locationSubPath = "/";
				return;
			}
			locationSubPath = text.Substring(0, num);
		}

		// Token: 0x060041E2 RID: 16866 RVA: 0x000AC18C File Offset: 0x000AA38C
		public string MapPath(string virtualPath)
		{
			if (!string.IsNullOrEmpty(virtualPath) && virtualPath.StartsWith("/@@MonoFakeVirtualPath@@", StringComparison.Ordinal))
			{
				return HttpRuntime.AppDomainAppPath;
			}
			if (this.map != null)
			{
				return this.MapPathFromMapper(virtualPath);
			}
			if (HttpContext.Current != null && HttpContext.Current.Request != null)
			{
				return HttpContext.Current.Request.MapPath(virtualPath);
			}
			if (HttpRuntime.AppDomainAppVirtualPath == null || !virtualPath.StartsWith(HttpRuntime.AppDomainAppVirtualPath))
			{
				return virtualPath;
			}
			if (virtualPath == HttpRuntime.AppDomainAppVirtualPath)
			{
				return HttpRuntime.AppDomainAppPath;
			}
			return UrlUtils.Combine(HttpRuntime.AppDomainAppPath, virtualPath.Substring(HttpRuntime.AppDomainAppVirtualPath.Length));
		}

		// Token: 0x060041E3 RID: 16867 RVA: 0x000AC22C File Offset: 0x000AA42C
		public string NormalizeVirtualPath(string virtualPath)
		{
			if (virtualPath == null || virtualPath.Length == 0)
			{
				virtualPath = ".";
			}
			else
			{
				virtualPath = virtualPath.Trim();
			}
			if (virtualPath[0] == '~' && virtualPath.Length > 2 && virtualPath[1] == '/')
			{
				virtualPath = virtualPath.Substring(1);
			}
			if (Path.DirectorySeparatorChar != '/')
			{
				virtualPath = virtualPath.Replace(Path.DirectorySeparatorChar, '/');
			}
			if (UrlUtils.IsRooted(virtualPath))
			{
				virtualPath = UrlUtils.Canonic(virtualPath);
			}
			else if (this.map.VirtualDirectories.Count > 0)
			{
				virtualPath = UrlUtils.Combine(this.map.VirtualDirectories[0].VirtualDirectory, virtualPath);
				virtualPath = UrlUtils.Canonic(virtualPath);
			}
			return virtualPath;
		}

		// Token: 0x060041E4 RID: 16868 RVA: 0x000AC2E4 File Offset: 0x000AA4E4
		public string MapPathFromMapper(string virtualPath)
		{
			string text = this.NormalizeVirtualPath(virtualPath);
			foreach (object obj in this.map.VirtualDirectories)
			{
				VirtualDirectoryMapping virtualDirectoryMapping = (VirtualDirectoryMapping)obj;
				if (text.StartsWith(virtualDirectoryMapping.VirtualDirectory))
				{
					int length = virtualDirectoryMapping.VirtualDirectory.Length;
					if (text.Length == length)
					{
						return virtualDirectoryMapping.PhysicalDirectory;
					}
					if (text[length] == '/')
					{
						string text2 = text.Substring(length + 1).Replace('/', Path.DirectorySeparatorChar);
						return Path.Combine(virtualDirectoryMapping.PhysicalDirectory, text2);
					}
				}
			}
			throw new HttpException("Invalid virtual directory: " + virtualPath);
		}

		// Token: 0x060041E5 RID: 16869 RVA: 0x000AC3BC File Offset: 0x000AA5BC
		internal static string GetWebConfigFileName(string dir)
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			if (currentDomain.GetData(".:!MonoAspNetHostedApp!:.") as string == "yes")
			{
				return ApplicationHost.FindWebConfig(dir);
			}
			string fileName = Path.GetFileName((Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly()).Location);
			string[] array = new string[]
			{
				fileName + ".config",
				fileName + ".Config"
			};
			string baseDirectory = currentDomain.BaseDirectory;
			foreach (string text in array)
			{
				string text2 = Path.Combine(baseDirectory, text);
				if (File.Exists(text2))
				{
					return text2;
				}
			}
			return null;
		}

		// Token: 0x060041E6 RID: 16870 RVA: 0x000AC464 File Offset: 0x000AA664
		public virtual bool IsAboveApplication(string configPath)
		{
			return !configPath.Contains(HttpRuntime.AppDomainAppPath);
		}

		// Token: 0x060041E7 RID: 16871 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool IsConfigRecordRequired(string configPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x000AC474 File Offset: 0x000AA674
		public virtual bool IsDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition)
		{
			if (allowDefinition == ConfigurationAllowDefinition.MachineOnly)
			{
				return configPath == ":machine:" || configPath == ":web:";
			}
			if (allowDefinition != ConfigurationAllowDefinition.MachineToWebRoot && allowDefinition != ConfigurationAllowDefinition.MachineToApplication)
			{
				return true;
			}
			if (string.IsNullOrEmpty(configPath))
			{
				return true;
			}
			string text;
			if (VirtualPathUtility.IsRooted(configPath))
			{
				text = VirtualPathUtility.Normalize(configPath);
			}
			else
			{
				text = configPath;
			}
			return string.Compare(text, ":machine:", StringComparison.Ordinal) == 0 || string.Compare(text, ":web:", StringComparison.Ordinal) == 0 || string.Compare(text, this.appVirtualPath) == 0 || this.IsApplication(text);
		}

		// Token: 0x060041E9 RID: 16873 RVA: 0x00008B66 File Offset: 0x00006D66
		[global::System.MonoTODO("Should return false in case strPath points to the root of an application.")]
		internal bool IsApplication(string strPath)
		{
			return true;
		}

		// Token: 0x060041EA RID: 16874 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool IsFile(string streamName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041EB RID: 16875 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool IsLocationApplicable(string configPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041EC RID: 16876 RVA: 0x000AC502 File Offset: 0x000AA702
		public virtual Stream OpenStreamForRead(string streamName)
		{
			if (!File.Exists(streamName))
			{
				return null;
			}
			return new FileStream(streamName, FileMode.Open, FileAccess.Read);
		}

		// Token: 0x060041ED RID: 16877 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public virtual Stream OpenStreamForRead(string streamName, bool assertPermissions)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041EE RID: 16878 RVA: 0x000AC516 File Offset: 0x000AA716
		public virtual Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext)
		{
			if (!this.IsAboveApplication(streamName))
			{
				WebConfigurationManager.SuppressAppReload(true);
			}
			return new FileStream(streamName, FileMode.Create, FileAccess.Write);
		}

		// Token: 0x060041EF RID: 16879 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public virtual Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext, bool assertPermissions)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool PrefetchAll(string configPath, string streamName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041F1 RID: 16881 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool PrefetchSection(string sectionGroupName, string sectionName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041F2 RID: 16882 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public virtual void RequireCompleteInit(IInternalConfigRecord configRecord)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041F3 RID: 16883 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual object StartMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041F4 RID: 16884 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void StopMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041F5 RID: 16885 RVA: 0x000AC530 File Offset: 0x000AA730
		public virtual void VerifyDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition, IConfigErrorInfo errorInfo)
		{
			if (!this.IsDefinitionAllowed(configPath, allowDefinition, allowExeDefinition))
			{
				throw new ConfigurationErrorsException("The section can't be defined in this file (the allowed definition context is '" + allowDefinition + "').", errorInfo.Filename, errorInfo.LineNumber);
			}
		}

		// Token: 0x060041F6 RID: 16886 RVA: 0x000AC566 File Offset: 0x000AA766
		public virtual void WriteCompleted(string streamName, bool success, object writeContext)
		{
			this.WriteCompleted(streamName, success, writeContext, false);
		}

		// Token: 0x060041F7 RID: 16887 RVA: 0x000AC572 File Offset: 0x000AA772
		public virtual void WriteCompleted(string streamName, bool success, object writeContext, bool assertPermissions)
		{
			if (!this.IsAboveApplication(streamName))
			{
				WebConfigurationManager.SuppressAppReload(true);
			}
		}

		// Token: 0x170014F2 RID: 5362
		// (get) Token: 0x060041F8 RID: 16888 RVA: 0x00008A69 File Offset: 0x00006C69
		public virtual bool SupportsChangeNotifications
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170014F3 RID: 5363
		// (get) Token: 0x060041F9 RID: 16889 RVA: 0x00008A69 File Offset: 0x00006C69
		public virtual bool SupportsLocation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170014F4 RID: 5364
		// (get) Token: 0x060041FA RID: 16890 RVA: 0x00008A69 File Offset: 0x00006C69
		public virtual bool SupportsPath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170014F5 RID: 5365
		// (get) Token: 0x060041FB RID: 16891 RVA: 0x00008A69 File Offset: 0x00006C69
		public virtual bool SupportsRefresh
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170014F6 RID: 5366
		// (get) Token: 0x060041FC RID: 16892 RVA: 0x00008A69 File Offset: 0x00006C69
		[global::System.MonoTODO("Always returns false")]
		public virtual bool IsRemote
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060041FD RID: 16893 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public virtual bool IsFullTrustSectionWithoutAptcaAllowed(IInternalConfigRecord configRecord)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041FE RID: 16894 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public virtual bool IsInitDelayed(IInternalConfigRecord configRecord)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041FF RID: 16895 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public virtual bool IsSecondaryRoot(string configPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004200 RID: 16896 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public virtual bool IsTrustedConfigPath(string configPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400234C RID: 9036
		private WebConfigurationFileMap map;

		// Token: 0x0400234D RID: 9037
		private const string MachinePath = ":machine:";

		// Token: 0x0400234E RID: 9038
		private const string MachineWebPath = ":web:";

		// Token: 0x0400234F RID: 9039
		private string appVirtualPath;
	}
}
