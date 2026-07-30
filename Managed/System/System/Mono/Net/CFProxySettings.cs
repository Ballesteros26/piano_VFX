using System;

namespace Mono.Net
{
	// Token: 0x0200005A RID: 90
	internal class CFProxySettings
	{
		// Token: 0x06000190 RID: 400 RVA: 0x00004D30 File Offset: 0x00002F30
		static CFProxySettings()
		{
			IntPtr intPtr = CFObject.dlopen("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork", 0);
			CFProxySettings.kCFNetworkProxiesHTTPEnable = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesHTTPEnable");
			CFProxySettings.kCFNetworkProxiesHTTPPort = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesHTTPPort");
			CFProxySettings.kCFNetworkProxiesHTTPProxy = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesHTTPProxy");
			CFProxySettings.kCFNetworkProxiesProxyAutoConfigEnable = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesProxyAutoConfigEnable");
			CFProxySettings.kCFNetworkProxiesProxyAutoConfigJavaScript = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesProxyAutoConfigJavaScript");
			CFProxySettings.kCFNetworkProxiesProxyAutoConfigURLString = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesProxyAutoConfigURLString");
			CFObject.dlclose(intPtr);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00004DAD File Offset: 0x00002FAD
		public CFProxySettings(CFDictionary settings)
		{
			this.settings = settings;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00004DBC File Offset: 0x00002FBC
		public CFDictionary Dictionary
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00004DC4 File Offset: 0x00002FC4
		public bool HTTPEnable
		{
			get
			{
				return !(CFProxySettings.kCFNetworkProxiesHTTPEnable == IntPtr.Zero) && CFNumber.AsBool(this.settings[CFProxySettings.kCFNetworkProxiesHTTPEnable]);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00004DEE File Offset: 0x00002FEE
		public int HTTPPort
		{
			get
			{
				if (CFProxySettings.kCFNetworkProxiesHTTPPort == IntPtr.Zero)
				{
					return 0;
				}
				return CFNumber.AsInt32(this.settings[CFProxySettings.kCFNetworkProxiesHTTPPort]);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00004E18 File Offset: 0x00003018
		public string HTTPProxy
		{
			get
			{
				if (CFProxySettings.kCFNetworkProxiesHTTPProxy == IntPtr.Zero)
				{
					return null;
				}
				return CFString.AsString(this.settings[CFProxySettings.kCFNetworkProxiesHTTPProxy]);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00004E42 File Offset: 0x00003042
		public bool ProxyAutoConfigEnable
		{
			get
			{
				return !(CFProxySettings.kCFNetworkProxiesProxyAutoConfigEnable == IntPtr.Zero) && CFNumber.AsBool(this.settings[CFProxySettings.kCFNetworkProxiesProxyAutoConfigEnable]);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00004E6C File Offset: 0x0000306C
		public string ProxyAutoConfigJavaScript
		{
			get
			{
				if (CFProxySettings.kCFNetworkProxiesProxyAutoConfigJavaScript == IntPtr.Zero)
				{
					return null;
				}
				return CFString.AsString(this.settings[CFProxySettings.kCFNetworkProxiesProxyAutoConfigJavaScript]);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00004E96 File Offset: 0x00003096
		public string ProxyAutoConfigURLString
		{
			get
			{
				if (CFProxySettings.kCFNetworkProxiesProxyAutoConfigURLString == IntPtr.Zero)
				{
					return null;
				}
				return CFString.AsString(this.settings[CFProxySettings.kCFNetworkProxiesProxyAutoConfigURLString]);
			}
		}

		// Token: 0x04000766 RID: 1894
		private static IntPtr kCFNetworkProxiesHTTPEnable;

		// Token: 0x04000767 RID: 1895
		private static IntPtr kCFNetworkProxiesHTTPPort;

		// Token: 0x04000768 RID: 1896
		private static IntPtr kCFNetworkProxiesHTTPProxy;

		// Token: 0x04000769 RID: 1897
		private static IntPtr kCFNetworkProxiesProxyAutoConfigEnable;

		// Token: 0x0400076A RID: 1898
		private static IntPtr kCFNetworkProxiesProxyAutoConfigJavaScript;

		// Token: 0x0400076B RID: 1899
		private static IntPtr kCFNetworkProxiesProxyAutoConfigURLString;

		// Token: 0x0400076C RID: 1900
		private CFDictionary settings;
	}
}
