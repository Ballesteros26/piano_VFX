using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;

namespace Mono.Net
{
	// Token: 0x0200005B RID: 91
	internal static class CFNetwork
	{
		// Token: 0x06000199 RID: 409
		[DllImport("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork", EntryPoint = "CFNetworkCopyProxiesForAutoConfigurationScript")]
		private static extern IntPtr CFNetworkCopyProxiesForAutoConfigurationScriptSequential(IntPtr proxyAutoConfigurationScript, IntPtr targetURL, out IntPtr error);

		// Token: 0x0600019A RID: 410
		[DllImport("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork")]
		private static extern IntPtr CFNetworkExecuteProxyAutoConfigurationURL(IntPtr proxyAutoConfigURL, IntPtr targetURL, CFNetwork.CFProxyAutoConfigurationResultCallback cb, ref CFStreamClientContext clientContext);

		// Token: 0x0600019B RID: 411 RVA: 0x00004EC0 File Offset: 0x000030C0
		private static void CFNetworkCopyProxiesForAutoConfigurationScriptThread()
		{
			bool flag = true;
			for (;;)
			{
				CFNetwork.proxy_event.WaitOne();
				do
				{
					object obj = CFNetwork.lock_obj;
					CFNetwork.GetProxyData getProxyData;
					lock (obj)
					{
						if (CFNetwork.get_proxy_queue.Count == 0)
						{
							break;
						}
						getProxyData = CFNetwork.get_proxy_queue.Dequeue();
						flag = CFNetwork.get_proxy_queue.Count > 0;
					}
					getProxyData.result = CFNetwork.CFNetworkCopyProxiesForAutoConfigurationScriptSequential(getProxyData.script, getProxyData.targetUri, out getProxyData.error);
					getProxyData.evt.Set();
				}
				while (flag);
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00004F5C File Offset: 0x0000315C
		private static IntPtr CFNetworkCopyProxiesForAutoConfigurationScript(IntPtr proxyAutoConfigurationScript, IntPtr targetURL, out IntPtr error)
		{
			IntPtr result;
			using (CFNetwork.GetProxyData getProxyData = new CFNetwork.GetProxyData())
			{
				getProxyData.script = proxyAutoConfigurationScript;
				getProxyData.targetUri = targetURL;
				object obj = CFNetwork.lock_obj;
				lock (obj)
				{
					if (CFNetwork.get_proxy_queue == null)
					{
						CFNetwork.get_proxy_queue = new Queue<CFNetwork.GetProxyData>();
						CFNetwork.proxy_event = new AutoResetEvent(false);
						new Thread(new ThreadStart(CFNetwork.CFNetworkCopyProxiesForAutoConfigurationScriptThread))
						{
							IsBackground = true
						}.Start();
					}
					CFNetwork.get_proxy_queue.Enqueue(getProxyData);
					CFNetwork.proxy_event.Set();
				}
				getProxyData.evt.WaitOne();
				error = getProxyData.error;
				result = getProxyData.result;
			}
			return result;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000502C File Offset: 0x0000322C
		private static CFArray CopyProxiesForAutoConfigurationScript(IntPtr proxyAutoConfigurationScript, CFUrl targetURL)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr = CFNetwork.CFNetworkCopyProxiesForAutoConfigurationScript(proxyAutoConfigurationScript, targetURL.Handle, out zero);
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new CFArray(intPtr, true);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00005064 File Offset: 0x00003264
		public static CFProxy[] GetProxiesForAutoConfigurationScript(IntPtr proxyAutoConfigurationScript, CFUrl targetURL)
		{
			if (proxyAutoConfigurationScript == IntPtr.Zero)
			{
				throw new ArgumentNullException("proxyAutoConfigurationScript");
			}
			if (targetURL == null)
			{
				throw new ArgumentNullException("targetURL");
			}
			CFArray cfarray = CFNetwork.CopyProxiesForAutoConfigurationScript(proxyAutoConfigurationScript, targetURL);
			if (cfarray == null)
			{
				return null;
			}
			CFProxy[] array = new CFProxy[cfarray.Count];
			for (int i = 0; i < array.Length; i++)
			{
				CFDictionary cfdictionary = new CFDictionary(cfarray[i], false);
				array[i] = new CFProxy(cfdictionary);
			}
			cfarray.Dispose();
			return array;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000050DC File Offset: 0x000032DC
		public static CFProxy[] GetProxiesForAutoConfigurationScript(IntPtr proxyAutoConfigurationScript, Uri targetUri)
		{
			if (proxyAutoConfigurationScript == IntPtr.Zero)
			{
				throw new ArgumentNullException("proxyAutoConfigurationScript");
			}
			if (targetUri == null)
			{
				throw new ArgumentNullException("targetUri");
			}
			CFUrl cfurl = CFUrl.Create(targetUri.AbsoluteUri);
			CFProxy[] proxiesForAutoConfigurationScript = CFNetwork.GetProxiesForAutoConfigurationScript(proxyAutoConfigurationScript, cfurl);
			cfurl.Dispose();
			return proxiesForAutoConfigurationScript;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00005130 File Offset: 0x00003330
		public static CFProxy[] ExecuteProxyAutoConfigurationURL(IntPtr proxyAutoConfigURL, Uri targetURL)
		{
			CFUrl cfurl = CFUrl.Create(targetURL.AbsoluteUri);
			if (cfurl == null)
			{
				return null;
			}
			CFProxy[] proxies = null;
			CFRunLoop runLoop = CFRunLoop.CurrentRunLoop;
			CFNetwork.CFProxyAutoConfigurationResultCallback cfproxyAutoConfigurationResultCallback = delegate(IntPtr client, IntPtr proxyList, IntPtr error)
			{
				if (proxyList != IntPtr.Zero)
				{
					CFArray cfarray = new CFArray(proxyList, false);
					proxies = new CFProxy[cfarray.Count];
					for (int i = 0; i < proxies.Length; i++)
					{
						CFDictionary cfdictionary = new CFDictionary(cfarray[i], false);
						proxies[i] = new CFProxy(cfdictionary);
					}
					cfarray.Dispose();
				}
				runLoop.Stop();
			};
			CFStreamClientContext cfstreamClientContext = default(CFStreamClientContext);
			IntPtr intPtr = CFNetwork.CFNetworkExecuteProxyAutoConfigurationURL(proxyAutoConfigURL, cfurl.Handle, cfproxyAutoConfigurationResultCallback, ref cfstreamClientContext);
			CFString cfstring = CFString.Create("Mono.MacProxy");
			runLoop.AddSource(intPtr, cfstring);
			runLoop.RunInMode(cfstring, double.MaxValue, false);
			runLoop.RemoveSource(intPtr, cfstring);
			return proxies;
		}

		// Token: 0x060001A1 RID: 417
		[DllImport("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork")]
		private static extern IntPtr CFNetworkCopyProxiesForURL(IntPtr url, IntPtr proxySettings);

		// Token: 0x060001A2 RID: 418 RVA: 0x000051D4 File Offset: 0x000033D4
		private static CFArray CopyProxiesForURL(CFUrl url, CFDictionary proxySettings)
		{
			IntPtr intPtr = CFNetwork.CFNetworkCopyProxiesForURL(url.Handle, (proxySettings != null) ? proxySettings.Handle : IntPtr.Zero);
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new CFArray(intPtr, true);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00005214 File Offset: 0x00003414
		public static CFProxy[] GetProxiesForURL(CFUrl url, CFProxySettings proxySettings)
		{
			if (url == null || url.Handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("url");
			}
			if (proxySettings == null)
			{
				proxySettings = CFNetwork.GetSystemProxySettings();
			}
			CFArray cfarray = CFNetwork.CopyProxiesForURL(url, proxySettings.Dictionary);
			if (cfarray == null)
			{
				return null;
			}
			CFProxy[] array = new CFProxy[cfarray.Count];
			for (int i = 0; i < array.Length; i++)
			{
				CFDictionary cfdictionary = new CFDictionary(cfarray[i], false);
				array[i] = new CFProxy(cfdictionary);
			}
			cfarray.Dispose();
			return array;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00005298 File Offset: 0x00003498
		public static CFProxy[] GetProxiesForUri(Uri uri, CFProxySettings proxySettings)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			CFUrl cfurl = CFUrl.Create(uri.AbsoluteUri);
			if (cfurl == null)
			{
				return null;
			}
			CFProxy[] proxiesForURL = CFNetwork.GetProxiesForURL(cfurl, proxySettings);
			cfurl.Dispose();
			return proxiesForURL;
		}

		// Token: 0x060001A5 RID: 421
		[DllImport("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork")]
		private static extern IntPtr CFNetworkCopySystemProxySettings();

		// Token: 0x060001A6 RID: 422 RVA: 0x000052DC File Offset: 0x000034DC
		public static CFProxySettings GetSystemProxySettings()
		{
			IntPtr intPtr = CFNetwork.CFNetworkCopySystemProxySettings();
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new CFProxySettings(new CFDictionary(intPtr, true));
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000530A File Offset: 0x0000350A
		public static IWebProxy GetDefaultProxy()
		{
			return new CFNetwork.CFWebProxy();
		}

		// Token: 0x0400076D RID: 1901
		public const string CFNetworkLibrary = "/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork";

		// Token: 0x0400076E RID: 1902
		private static object lock_obj = new object();

		// Token: 0x0400076F RID: 1903
		private static Queue<CFNetwork.GetProxyData> get_proxy_queue;

		// Token: 0x04000770 RID: 1904
		private static AutoResetEvent proxy_event;

		// Token: 0x0200005C RID: 92
		private class GetProxyData : IDisposable
		{
			// Token: 0x060001A9 RID: 425 RVA: 0x0000531D File Offset: 0x0000351D
			public void Dispose()
			{
				this.evt.Close();
			}

			// Token: 0x04000771 RID: 1905
			public IntPtr script;

			// Token: 0x04000772 RID: 1906
			public IntPtr targetUri;

			// Token: 0x04000773 RID: 1907
			public IntPtr error;

			// Token: 0x04000774 RID: 1908
			public IntPtr result;

			// Token: 0x04000775 RID: 1909
			public ManualResetEvent evt = new ManualResetEvent(false);
		}

		// Token: 0x0200005D RID: 93
		// (Invoke) Token: 0x060001AC RID: 428
		private delegate void CFProxyAutoConfigurationResultCallback(IntPtr client, IntPtr proxyList, IntPtr error);

		// Token: 0x0200005E RID: 94
		private class CFWebProxy : IWebProxy
		{
			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000533E File Offset: 0x0000353E
			// (set) Token: 0x060001B1 RID: 433 RVA: 0x00005346 File Offset: 0x00003546
			public ICredentials Credentials
			{
				get
				{
					return this.credentials;
				}
				set
				{
					this.userSpecified = true;
					this.credentials = value;
				}
			}

			// Token: 0x060001B2 RID: 434 RVA: 0x00005358 File Offset: 0x00003558
			private static Uri GetProxyUri(CFProxy proxy, out NetworkCredential credentials)
			{
				CFProxyType proxyType = proxy.ProxyType;
				string text;
				if (proxyType != CFProxyType.FTP)
				{
					if (proxyType - CFProxyType.HTTP > 1)
					{
						credentials = null;
						return null;
					}
					text = "http://";
				}
				else
				{
					text = "ftp://";
				}
				string username = proxy.Username;
				string password = proxy.Password;
				string hostName = proxy.HostName;
				int port = proxy.Port;
				if (username != null)
				{
					credentials = new NetworkCredential(username, password);
				}
				else
				{
					credentials = null;
				}
				return new Uri(text + hostName + ((port != 0) ? (":" + port.ToString()) : string.Empty), UriKind.Absolute);
			}

			// Token: 0x060001B3 RID: 435 RVA: 0x000053E7 File Offset: 0x000035E7
			private static Uri GetProxyUriFromScript(IntPtr script, Uri targetUri, out NetworkCredential credentials)
			{
				return CFNetwork.CFWebProxy.SelectProxy(CFNetwork.GetProxiesForAutoConfigurationScript(script, targetUri), targetUri, out credentials);
			}

			// Token: 0x060001B4 RID: 436 RVA: 0x000053F7 File Offset: 0x000035F7
			private static Uri ExecuteProxyAutoConfigurationURL(IntPtr proxyAutoConfigURL, Uri targetUri, out NetworkCredential credentials)
			{
				return CFNetwork.CFWebProxy.SelectProxy(CFNetwork.ExecuteProxyAutoConfigurationURL(proxyAutoConfigURL, targetUri), targetUri, out credentials);
			}

			// Token: 0x060001B5 RID: 437 RVA: 0x00005408 File Offset: 0x00003608
			private static Uri SelectProxy(CFProxy[] proxies, Uri targetUri, out NetworkCredential credentials)
			{
				if (proxies == null)
				{
					credentials = null;
					return targetUri;
				}
				for (int i = 0; i < proxies.Length; i++)
				{
					switch (proxies[i].ProxyType)
					{
					case CFProxyType.None:
						credentials = null;
						return targetUri;
					case CFProxyType.FTP:
					case CFProxyType.HTTP:
					case CFProxyType.HTTPS:
						return CFNetwork.CFWebProxy.GetProxyUri(proxies[i], out credentials);
					}
				}
				credentials = null;
				return null;
			}

			// Token: 0x060001B6 RID: 438 RVA: 0x0000546C File Offset: 0x0000366C
			public Uri GetProxy(Uri targetUri)
			{
				NetworkCredential networkCredential = null;
				Uri uri = null;
				if (targetUri == null)
				{
					throw new ArgumentNullException("targetUri");
				}
				try
				{
					CFProxySettings systemProxySettings = CFNetwork.GetSystemProxySettings();
					CFProxy[] proxiesForUri = CFNetwork.GetProxiesForUri(targetUri, systemProxySettings);
					if (proxiesForUri != null)
					{
						int num = 0;
						while (num < proxiesForUri.Length && uri == null)
						{
							switch (proxiesForUri[num].ProxyType)
							{
							case CFProxyType.None:
								uri = targetUri;
								break;
							case CFProxyType.AutoConfigurationUrl:
								uri = CFNetwork.CFWebProxy.ExecuteProxyAutoConfigurationURL(proxiesForUri[num].AutoConfigurationUrl, targetUri, out networkCredential);
								break;
							case CFProxyType.AutoConfigurationJavaScript:
								uri = CFNetwork.CFWebProxy.GetProxyUriFromScript(proxiesForUri[num].AutoConfigurationJavaScript, targetUri, out networkCredential);
								break;
							case CFProxyType.FTP:
							case CFProxyType.HTTP:
							case CFProxyType.HTTPS:
								uri = CFNetwork.CFWebProxy.GetProxyUri(proxiesForUri[num], out networkCredential);
								break;
							}
							num++;
						}
						if (uri == null)
						{
							uri = targetUri;
						}
					}
					else
					{
						uri = targetUri;
					}
				}
				catch
				{
					uri = targetUri;
				}
				if (!this.userSpecified)
				{
					this.credentials = networkCredential;
				}
				return uri;
			}

			// Token: 0x060001B7 RID: 439 RVA: 0x00005560 File Offset: 0x00003760
			public bool IsBypassed(Uri targetUri)
			{
				if (targetUri == null)
				{
					throw new ArgumentNullException("targetUri");
				}
				return this.GetProxy(targetUri) == targetUri;
			}

			// Token: 0x04000776 RID: 1910
			private ICredentials credentials;

			// Token: 0x04000777 RID: 1911
			private bool userSpecified;
		}
	}
}
