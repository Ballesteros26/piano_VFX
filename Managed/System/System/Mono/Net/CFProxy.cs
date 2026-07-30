using System;

namespace Mono.Net
{
	// Token: 0x02000059 RID: 89
	internal class CFProxy
	{
		// Token: 0x06000186 RID: 390 RVA: 0x00004AA8 File Offset: 0x00002CA8
		static CFProxy()
		{
			IntPtr intPtr = CFObject.dlopen("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork", 0);
			CFProxy.kCFProxyAutoConfigurationJavaScriptKey = CFObject.GetCFObjectHandle(intPtr, "kCFProxyAutoConfigurationJavaScriptKey");
			CFProxy.kCFProxyAutoConfigurationURLKey = CFObject.GetCFObjectHandle(intPtr, "kCFProxyAutoConfigurationURLKey");
			CFProxy.kCFProxyHostNameKey = CFObject.GetCFObjectHandle(intPtr, "kCFProxyHostNameKey");
			CFProxy.kCFProxyPasswordKey = CFObject.GetCFObjectHandle(intPtr, "kCFProxyPasswordKey");
			CFProxy.kCFProxyPortNumberKey = CFObject.GetCFObjectHandle(intPtr, "kCFProxyPortNumberKey");
			CFProxy.kCFProxyTypeKey = CFObject.GetCFObjectHandle(intPtr, "kCFProxyTypeKey");
			CFProxy.kCFProxyUsernameKey = CFObject.GetCFObjectHandle(intPtr, "kCFProxyUsernameKey");
			CFProxy.kCFProxyTypeAutoConfigurationURL = CFObject.GetCFObjectHandle(intPtr, "kCFProxyTypeAutoConfigurationURL");
			CFProxy.kCFProxyTypeAutoConfigurationJavaScript = CFObject.GetCFObjectHandle(intPtr, "kCFProxyTypeAutoConfigurationJavaScript");
			CFProxy.kCFProxyTypeFTP = CFObject.GetCFObjectHandle(intPtr, "kCFProxyTypeFTP");
			CFProxy.kCFProxyTypeHTTP = CFObject.GetCFObjectHandle(intPtr, "kCFProxyTypeHTTP");
			CFProxy.kCFProxyTypeHTTPS = CFObject.GetCFObjectHandle(intPtr, "kCFProxyTypeHTTPS");
			CFProxy.kCFProxyTypeSOCKS = CFObject.GetCFObjectHandle(intPtr, "kCFProxyTypeSOCKS");
			CFObject.dlclose(intPtr);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00004B95 File Offset: 0x00002D95
		internal CFProxy(CFDictionary settings)
		{
			this.settings = settings;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00004BA4 File Offset: 0x00002DA4
		private static CFProxyType CFProxyTypeToEnum(IntPtr type)
		{
			if (type == CFProxy.kCFProxyTypeAutoConfigurationJavaScript)
			{
				return CFProxyType.AutoConfigurationJavaScript;
			}
			if (type == CFProxy.kCFProxyTypeAutoConfigurationURL)
			{
				return CFProxyType.AutoConfigurationUrl;
			}
			if (type == CFProxy.kCFProxyTypeFTP)
			{
				return CFProxyType.FTP;
			}
			if (type == CFProxy.kCFProxyTypeHTTP)
			{
				return CFProxyType.HTTP;
			}
			if (type == CFProxy.kCFProxyTypeHTTPS)
			{
				return CFProxyType.HTTPS;
			}
			if (type == CFProxy.kCFProxyTypeSOCKS)
			{
				return CFProxyType.SOCKS;
			}
			return CFProxyType.None;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00004C0C File Offset: 0x00002E0C
		public IntPtr AutoConfigurationJavaScript
		{
			get
			{
				if (CFProxy.kCFProxyAutoConfigurationJavaScriptKey == IntPtr.Zero)
				{
					return IntPtr.Zero;
				}
				return this.settings[CFProxy.kCFProxyAutoConfigurationJavaScriptKey];
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00004C35 File Offset: 0x00002E35
		public IntPtr AutoConfigurationUrl
		{
			get
			{
				if (CFProxy.kCFProxyAutoConfigurationURLKey == IntPtr.Zero)
				{
					return IntPtr.Zero;
				}
				return this.settings[CFProxy.kCFProxyAutoConfigurationURLKey];
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00004C5E File Offset: 0x00002E5E
		public string HostName
		{
			get
			{
				if (CFProxy.kCFProxyHostNameKey == IntPtr.Zero)
				{
					return null;
				}
				return CFString.AsString(this.settings[CFProxy.kCFProxyHostNameKey]);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00004C88 File Offset: 0x00002E88
		public string Password
		{
			get
			{
				if (CFProxy.kCFProxyPasswordKey == IntPtr.Zero)
				{
					return null;
				}
				return CFString.AsString(this.settings[CFProxy.kCFProxyPasswordKey]);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00004CB2 File Offset: 0x00002EB2
		public int Port
		{
			get
			{
				if (CFProxy.kCFProxyPortNumberKey == IntPtr.Zero)
				{
					return 0;
				}
				return CFNumber.AsInt32(this.settings[CFProxy.kCFProxyPortNumberKey]);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00004CDC File Offset: 0x00002EDC
		public CFProxyType ProxyType
		{
			get
			{
				if (CFProxy.kCFProxyTypeKey == IntPtr.Zero)
				{
					return CFProxyType.None;
				}
				return CFProxy.CFProxyTypeToEnum(this.settings[CFProxy.kCFProxyTypeKey]);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00004D06 File Offset: 0x00002F06
		public string Username
		{
			get
			{
				if (CFProxy.kCFProxyUsernameKey == IntPtr.Zero)
				{
					return null;
				}
				return CFString.AsString(this.settings[CFProxy.kCFProxyUsernameKey]);
			}
		}

		// Token: 0x04000758 RID: 1880
		private static IntPtr kCFProxyAutoConfigurationJavaScriptKey;

		// Token: 0x04000759 RID: 1881
		private static IntPtr kCFProxyAutoConfigurationURLKey;

		// Token: 0x0400075A RID: 1882
		private static IntPtr kCFProxyHostNameKey;

		// Token: 0x0400075B RID: 1883
		private static IntPtr kCFProxyPasswordKey;

		// Token: 0x0400075C RID: 1884
		private static IntPtr kCFProxyPortNumberKey;

		// Token: 0x0400075D RID: 1885
		private static IntPtr kCFProxyTypeKey;

		// Token: 0x0400075E RID: 1886
		private static IntPtr kCFProxyUsernameKey;

		// Token: 0x0400075F RID: 1887
		private static IntPtr kCFProxyTypeAutoConfigurationURL;

		// Token: 0x04000760 RID: 1888
		private static IntPtr kCFProxyTypeAutoConfigurationJavaScript;

		// Token: 0x04000761 RID: 1889
		private static IntPtr kCFProxyTypeFTP;

		// Token: 0x04000762 RID: 1890
		private static IntPtr kCFProxyTypeHTTP;

		// Token: 0x04000763 RID: 1891
		private static IntPtr kCFProxyTypeHTTPS;

		// Token: 0x04000764 RID: 1892
		private static IntPtr kCFProxyTypeSOCKS;

		// Token: 0x04000765 RID: 1893
		private CFDictionary settings;
	}
}
