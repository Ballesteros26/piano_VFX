using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web.Services.Diagnostics;

namespace System.Web.Services.Discovery
{
	// Token: 0x020000B2 RID: 178
	internal class DynamicVirtualDiscoSearcher : DynamicDiscoSearcher
	{
		// Token: 0x060004AB RID: 1195 RVA: 0x00015B40 File Offset: 0x00013D40
		internal DynamicVirtualDiscoSearcher(string startDir, string[] excludedUrls, string rootUrl)
			: base(excludedUrls)
		{
			this.origUrl = rootUrl;
			this.entryPathPrefix = this.GetWebServerForUrl(rootUrl) + "/ROOT";
			this.startDir = startDir;
			string text = new Uri(rootUrl).LocalPath;
			if (text.Equals("/"))
			{
				text = "";
			}
			this.rootPathAsdi = this.entryPathPrefix + text;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00015BC0 File Offset: 0x00013DC0
		internal override void Search(string fileToSkipAtBegin)
		{
			this.SearchInit(fileToSkipAtBegin);
			base.ScanDirectory(this.rootPathAsdi);
			this.CleanupCache();
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00015BDC File Offset: 0x00013DDC
		protected override void SearchSubDirectories(string nameAdsiDir)
		{
			bool traceVerbose = CompModSwitches.DynamicDiscoverySearcher.TraceVerbose;
			DirectoryEntry directoryEntry = (DirectoryEntry)this.Adsi[nameAdsiDir];
			if (directoryEntry == null)
			{
				if (!DirectoryEntry.Exists(nameAdsiDir))
				{
					return;
				}
				directoryEntry = new DirectoryEntry(nameAdsiDir);
				this.Adsi[nameAdsiDir] = directoryEntry;
			}
			foreach (object obj in directoryEntry.Children)
			{
				DirectoryEntry directoryEntry2 = (DirectoryEntry)obj;
				DirectoryEntry directoryEntry3 = (DirectoryEntry)this.Adsi[directoryEntry2.Path];
				if (directoryEntry3 == null)
				{
					directoryEntry3 = directoryEntry2;
					this.Adsi[directoryEntry2.Path] = directoryEntry2;
				}
				else
				{
					directoryEntry2.Dispose();
				}
				if (this.GetAppSettings(directoryEntry3) != null)
				{
					base.ScanDirectory(directoryEntry3.Path);
				}
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00015CBC File Offset: 0x00013EBC
		protected override DirectoryInfo GetPhysicalDir(string dir)
		{
			DirectoryEntry directoryEntry = (DirectoryEntry)this.Adsi[dir];
			if (directoryEntry == null)
			{
				if (!DirectoryEntry.Exists(dir))
				{
					return null;
				}
				directoryEntry = new DirectoryEntry(dir);
				this.Adsi[dir] = directoryEntry;
			}
			try
			{
				DynamicVirtualDiscoSearcher.AppSettings appSettings = this.GetAppSettings(directoryEntry);
				if (appSettings == null)
				{
					return null;
				}
				DirectoryInfo directoryInfo;
				if (appSettings.VPath == null)
				{
					if (!dir.StartsWith(this.rootPathAsdi, StringComparison.Ordinal))
					{
						throw new ArgumentException(Res.GetString("WebVirtualDisoRoot", new object[] { dir, this.rootPathAsdi }), "dir");
					}
					string text = dir.Substring(this.rootPathAsdi.Length);
					text = text.Replace('/', '\\');
					directoryInfo = new DirectoryInfo(this.startDir + text);
				}
				else
				{
					directoryInfo = new DirectoryInfo(appSettings.VPath);
				}
				if (directoryInfo.Exists)
				{
					return directoryInfo;
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				bool traceVerbose = CompModSwitches.DynamicDiscoverySearcher.TraceVerbose;
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Warning, this, "GetPhysicalDir", ex);
				}
				return null;
			}
			return null;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00015DF8 File Offset: 0x00013FF8
		private string GetWebServerForUrl(string url)
		{
			Uri uri = new Uri(url);
			foreach (object obj in new DirectoryEntry("IIS://" + uri.Host + "/W3SVC").Children)
			{
				DirectoryEntry directoryEntry = (DirectoryEntry)obj;
				DirectoryEntry directoryEntry2 = (DirectoryEntry)this.Adsi[directoryEntry.Path];
				if (directoryEntry2 == null)
				{
					directoryEntry2 = directoryEntry;
					this.Adsi[directoryEntry.Path] = directoryEntry;
				}
				else
				{
					directoryEntry.Dispose();
				}
				DynamicVirtualDiscoSearcher.AppSettings appSettings = this.GetAppSettings(directoryEntry2);
				if (appSettings != null && appSettings.Bindings != null)
				{
					foreach (string text in appSettings.Bindings)
					{
						bool traceVerbose = CompModSwitches.DynamicDiscoverySearcher.TraceVerbose;
						string[] array = text.Split(new char[] { ':' });
						string text2 = array[0];
						string text3 = array[1];
						string text4 = array[2];
						if (Convert.ToInt32(text3, CultureInfo.InvariantCulture) == uri.Port)
						{
							if (uri.HostNameType == UriHostNameType.Dns)
							{
								if (text4.Length == 0 || string.Compare(text4, uri.Host, StringComparison.OrdinalIgnoreCase) == 0)
								{
									return directoryEntry2.Path;
								}
							}
							else if (text2.Length == 0 || string.Compare(text2, uri.Host, StringComparison.OrdinalIgnoreCase) == 0)
							{
								return directoryEntry2.Path;
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00015F88 File Offset: 0x00014188
		protected override string MakeResultPath(string dirName, string fileName)
		{
			return this.origUrl + dirName.Substring(this.rootPathAsdi.Length, dirName.Length - this.rootPathAsdi.Length) + "/" + fileName;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00015FBE File Offset: 0x000141BE
		protected override string MakeAbsExcludedPath(string pathRelativ)
		{
			return this.rootPathAsdi + "/" + pathRelativ.Replace('\\', '/');
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00002B54 File Offset: 0x00000D54
		protected override bool IsVirtualSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00015FDC File Offset: 0x000141DC
		private DynamicVirtualDiscoSearcher.AppSettings GetAppSettings(DirectoryEntry entry)
		{
			string path = entry.Path;
			DynamicVirtualDiscoSearcher.AppSettings appSettings = null;
			object obj = this.webApps[path];
			if (obj == null)
			{
				Hashtable hashtable = this.webApps;
				lock (hashtable)
				{
					if (this.webApps[path] == null)
					{
						appSettings = new DynamicVirtualDiscoSearcher.AppSettings(entry);
						this.webApps[path] = appSettings;
					}
					goto IL_0063;
				}
			}
			appSettings = (DynamicVirtualDiscoSearcher.AppSettings)obj;
			IL_0063:
			if (!appSettings.AccessRead)
			{
				return null;
			}
			return appSettings;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00016068 File Offset: 0x00014268
		private void CleanupCache()
		{
			foreach (object obj in this.Adsi)
			{
				((DirectoryEntry)((DictionaryEntry)obj).Value).Dispose();
			}
			this.rootPathAsdi = null;
			this.entryPathPrefix = null;
			this.startDir = null;
			this.Adsi = null;
			this.webApps = null;
		}

		// Token: 0x04000353 RID: 851
		private string rootPathAsdi;

		// Token: 0x04000354 RID: 852
		private string entryPathPrefix;

		// Token: 0x04000355 RID: 853
		private string startDir;

		// Token: 0x04000356 RID: 854
		private Hashtable webApps = new Hashtable();

		// Token: 0x04000357 RID: 855
		private Hashtable Adsi = new Hashtable();

		// Token: 0x020000B3 RID: 179
		private class AppSettings
		{
			// Token: 0x060004B5 RID: 1205 RVA: 0x000160F0 File Offset: 0x000142F0
			internal AppSettings(DirectoryEntry entry)
			{
				string schemaClassName = entry.SchemaClassName;
				this.AccessRead = true;
				if (schemaClassName == "IIsWebVirtualDir" || schemaClassName == "IIsWebDirectory")
				{
					if (!(bool)entry.Properties["AccessRead"][0])
					{
						this.AccessRead = false;
						return;
					}
					if (schemaClassName == "IIsWebVirtualDir")
					{
						this.VPath = (string)entry.Properties["Path"][0];
						return;
					}
				}
				else
				{
					if (schemaClassName == "IIsWebServer")
					{
						this.Bindings = new string[entry.Properties["ServerBindings"].Count];
						for (int i = 0; i < this.Bindings.Length; i++)
						{
							this.Bindings[i] = (string)entry.Properties["ServerBindings"][i];
						}
						return;
					}
					this.AccessRead = false;
				}
			}

			// Token: 0x04000358 RID: 856
			internal readonly bool AccessRead;

			// Token: 0x04000359 RID: 857
			internal readonly string[] Bindings;

			// Token: 0x0400035A RID: 858
			internal readonly string VPath;
		}
	}
}
