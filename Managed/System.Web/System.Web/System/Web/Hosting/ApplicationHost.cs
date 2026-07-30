using System;
using System.IO;
using System.Security.Permissions;
using System.Security.Policy;
using System.Web.Configuration;

namespace System.Web.Hosting
{
	/// <summary>Enables hosting of ASP.NET pages outside the Internet Information Services (IIS) application. This class enables the host to create application domains for processing ASP.NET requests.</summary>
	// Token: 0x02000548 RID: 1352
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ApplicationHost
	{
		// Token: 0x06003A8F RID: 14991 RVA: 0x00002050 File Offset: 0x00000250
		private ApplicationHost()
		{
		}

		// Token: 0x06003A90 RID: 14992 RVA: 0x0009DAE8 File Offset: 0x0009BCE8
		internal static string FindWebConfig(string basedir)
		{
			if (string.IsNullOrEmpty(basedir) || !Directory.Exists(basedir))
			{
				return null;
			}
			string[] fileSystemEntries = Directory.GetFileSystemEntries(basedir, "?eb.?onfig");
			if (fileSystemEntries == null || fileSystemEntries.Length == 0)
			{
				return null;
			}
			return fileSystemEntries[0];
		}

		// Token: 0x06003A91 RID: 14993 RVA: 0x0009DB20 File Offset: 0x0009BD20
		internal static bool ClearDynamicBaseDirectory(string directory)
		{
			string[] array = null;
			try
			{
				array = Directory.GetDirectories(directory);
			}
			catch
			{
			}
			bool flag = true;
			if (array != null && array.Length != 0)
			{
				foreach (string text in array)
				{
					if (ApplicationHost.ClearDynamicBaseDirectory(text))
					{
						try
						{
							Directory.Delete(text);
						}
						catch
						{
							flag = false;
						}
					}
				}
			}
			try
			{
				array = Directory.GetFiles(directory);
			}
			catch
			{
				array = null;
			}
			if (array != null && array.Length != 0)
			{
				foreach (string text2 in array)
				{
					try
					{
						File.Delete(text2);
					}
					catch
					{
						flag = false;
					}
				}
			}
			return flag;
		}

		// Token: 0x06003A92 RID: 14994 RVA: 0x0009DBE0 File Offset: 0x0009BDE0
		private static bool CreateDirectory(string directory)
		{
			object obj = ApplicationHost.create_dir;
			bool flag2;
			lock (obj)
			{
				if (!Directory.Exists(directory))
				{
					Directory.CreateDirectory(directory);
					flag2 = false;
				}
				else
				{
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06003A93 RID: 14995 RVA: 0x0009DC30 File Offset: 0x0009BE30
		private static string BuildPrivateBinPath(string physicalPath, string[] dirs)
		{
			int num = dirs.Length;
			string[] array = new string[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = Path.Combine(physicalPath, dirs[i]);
			}
			return string.Join(";", array);
		}

		/// <summary>Creates and configures an application domain for hosting ASP.NET.</summary>
		/// <returns>An instance of a user-supplied class used to marshal calls into the newly created application domain.</returns>
		/// <param name="hostType">The name of a user-supplied class to be created in the new application domain.</param>
		/// <param name="virtualDir">The virtual directory for the application domain; for example, /myapp.</param>
		/// <param name="physicalDir">The physical directory for the application domain where ASP.NET pages are located; for example, c:\mypages.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">The Web host computer is not running the Windows NT platform or a Coriolis environment.</exception>
		// Token: 0x06003A94 RID: 14996 RVA: 0x0009DC6C File Offset: 0x0009BE6C
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static object CreateApplicationHost(Type hostType, string virtualDir, string physicalDir)
		{
			if (physicalDir == null)
			{
				throw new NullReferenceException();
			}
			physicalDir = Path.GetFullPath(physicalDir);
			if (hostType == null)
			{
				throw new ArgumentException("hostType can't be null");
			}
			if (virtualDir == null)
			{
				throw new ArgumentNullException("virtualDir");
			}
			Evidence evidence = new Evidence(AppDomain.CurrentDomain.Evidence);
			AppDomainSetup appDomainSetup = new AppDomainSetup();
			appDomainSetup.ApplicationBase = physicalDir;
			string text = ApplicationHost.FindWebConfig(physicalDir);
			if (text == null)
			{
				text = Path.Combine(physicalDir, "web.config");
			}
			appDomainSetup.ConfigurationFile = text;
			appDomainSetup.DisallowCodeDownload = true;
			string[] array = new string[] { Path.Combine(physicalDir, "bin") };
			foreach (string text2 in HttpApplication.BinDirs)
			{
				string text3 = Path.Combine(physicalDir, text2);
				if (Directory.Exists(text3))
				{
					array[0] = text3;
					break;
				}
			}
			appDomainSetup.PrivateBinPath = ApplicationHost.BuildPrivateBinPath(physicalDir, array);
			appDomainSetup.PrivateBinPathProbe = "*";
			string text4 = null;
			string userName = Environment.UserName;
			int num = 0;
			string text5 = userName + "-temp-aspnet-";
			int num2 = 0;
			for (;;)
			{
				string text6 = Path.Combine(Path.GetTempPath(), text5 + num2.ToString("x"));
				try
				{
					ApplicationHost.CreateDirectory(text6);
					string text7 = Path.Combine(text6, "stamp");
					ApplicationHost.CreateDirectory(text7);
					text4 = text6;
					try
					{
						Directory.Delete(text7);
					}
					catch (Exception)
					{
					}
					num = num2.GetHashCode();
					break;
				}
				catch (UnauthorizedAccessException)
				{
				}
				num2++;
			}
			string text8 = ((virtualDir.GetHashCode() + 1) ^ (physicalDir.GetHashCode() + 2) ^ num).ToString("x");
			string environmentVariable = Environment.GetEnvironmentVariable("__MONO_DOMAIN_ID_SUFFIX");
			if (environmentVariable != null && environmentVariable.Length > 0)
			{
				text8 += environmentVariable;
			}
			appDomainSetup.ApplicationName = text8;
			appDomainSetup.DynamicBase = text4;
			appDomainSetup.CachePath = text4;
			string dynamicBase = appDomainSetup.DynamicBase;
			if (ApplicationHost.CreateDirectory(dynamicBase) && Environment.GetEnvironmentVariable("MONO_ASPNET_NODELETE") == null)
			{
				ApplicationHost.ClearDynamicBaseDirectory(dynamicBase);
			}
			AppDomain appDomain = AppDomain.CreateDomain(text8, evidence, appDomainSetup);
			appDomain.SetData(".appDomain", "*");
			int length = physicalDir.Length;
			if (physicalDir[length - 1] != Path.DirectorySeparatorChar)
			{
				physicalDir += Path.DirectorySeparatorChar.ToString();
			}
			appDomain.SetData(".appPath", physicalDir);
			appDomain.SetData(".appVPath", virtualDir);
			appDomain.SetData(".appId", text8);
			appDomain.SetData(".domainId", text8);
			appDomain.SetData(".hostingVirtualPath", virtualDir);
			appDomain.SetData(".hostingInstallDir", Path.GetDirectoryName(typeof(object).Assembly.CodeBase));
			appDomain.SetData("DataDirectory", Path.Combine(physicalDir, "App_Data"));
			appDomain.SetData(".:!MonoAspNetHostedApp!:.", "yes");
			appDomain.DoCallBack(new CrossAppDomainDelegate(ApplicationHost.SetHostingEnvironment));
			return appDomain.CreateInstanceAndUnwrap(hostType.Module.Assembly.FullName, hostType.FullName);
		}

		// Token: 0x06003A95 RID: 14997 RVA: 0x0009DF74 File Offset: 0x0009C174
		private static void SetHostingEnvironment()
		{
			bool flag = true;
			HostingEnvironmentSection hostingEnvironmentSection = WebConfigurationManager.GetWebApplicationSection("system.web/hostingEnvironment") as HostingEnvironmentSection;
			if (hostingEnvironmentSection != null)
			{
				flag = hostingEnvironmentSection.ShadowCopyBinAssemblies;
			}
			if (flag)
			{
				AppDomain currentDomain = AppDomain.CurrentDomain;
				currentDomain.SetShadowCopyFiles();
				currentDomain.SetShadowCopyPath(currentDomain.SetupInformation.PrivateBinPath);
			}
			HostingEnvironment.IsHosted = true;
			HostingEnvironment.SiteName = HostingEnvironment.ApplicationID;
		}

		// Token: 0x04001FD2 RID: 8146
		private const string DEFAULT_WEB_CONFIG_NAME = "web.config";

		// Token: 0x04001FD3 RID: 8147
		internal const string MonoHostedDataKey = ".:!MonoAspNetHostedApp!:.";

		// Token: 0x04001FD4 RID: 8148
		private static object create_dir = new object();
	}
}
