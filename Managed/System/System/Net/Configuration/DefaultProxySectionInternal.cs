using System;
using System.Configuration;
using System.Threading;

namespace System.Net.Configuration
{
	// Token: 0x0200068D RID: 1677
	internal sealed class DefaultProxySectionInternal
	{
		// Token: 0x060034A3 RID: 13475 RVA: 0x000C38F0 File Offset: 0x000C1AF0
		private static IWebProxy GetDefaultProxy_UsingOldMonoCode()
		{
			DefaultProxySection defaultProxySection = ConfigurationManager.GetSection("system.net/defaultProxy") as DefaultProxySection;
			if (defaultProxySection == null)
			{
				return DefaultProxySectionInternal.GetSystemWebProxy();
			}
			ProxyElement proxy = defaultProxySection.Proxy;
			WebProxy webProxy;
			if (proxy.UseSystemDefault != ProxyElement.UseSystemDefaultValues.False && proxy.ProxyAddress == null)
			{
				IWebProxy systemWebProxy = DefaultProxySectionInternal.GetSystemWebProxy();
				if (!(systemWebProxy is WebProxy))
				{
					return systemWebProxy;
				}
				webProxy = (WebProxy)systemWebProxy;
			}
			else
			{
				webProxy = new WebProxy();
			}
			if (proxy.ProxyAddress != null)
			{
				webProxy.Address = proxy.ProxyAddress;
			}
			if (proxy.BypassOnLocal != ProxyElement.BypassOnLocalValues.Unspecified)
			{
				webProxy.BypassProxyOnLocal = proxy.BypassOnLocal == ProxyElement.BypassOnLocalValues.True;
			}
			foreach (object obj in defaultProxySection.BypassList)
			{
				BypassElement bypassElement = (BypassElement)obj;
				webProxy.BypassArrayList.Add(bypassElement.Address);
			}
			return webProxy;
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x000841D2 File Offset: 0x000823D2
		private static IWebProxy GetSystemWebProxy()
		{
			return global::System.Net.WebProxy.CreateDefaultProxy();
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x060034A5 RID: 13477 RVA: 0x000C39E8 File Offset: 0x000C1BE8
		internal static object ClassSyncObject
		{
			get
			{
				if (DefaultProxySectionInternal.classSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref DefaultProxySectionInternal.classSyncObject, obj, null);
				}
				return DefaultProxySectionInternal.classSyncObject;
			}
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x000C3A14 File Offset: 0x000C1C14
		internal static DefaultProxySectionInternal GetSection()
		{
			object obj = DefaultProxySectionInternal.ClassSyncObject;
			DefaultProxySectionInternal defaultProxySectionInternal;
			lock (obj)
			{
				defaultProxySectionInternal = new DefaultProxySectionInternal
				{
					webProxy = DefaultProxySectionInternal.GetDefaultProxy_UsingOldMonoCode()
				};
			}
			return defaultProxySectionInternal;
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x060034A7 RID: 13479 RVA: 0x000C3A60 File Offset: 0x000C1C60
		internal IWebProxy WebProxy
		{
			get
			{
				return this.webProxy;
			}
		}

		// Token: 0x04002A3B RID: 10811
		private IWebProxy webProxy;

		// Token: 0x04002A3C RID: 10812
		private static object classSyncObject;
	}
}
