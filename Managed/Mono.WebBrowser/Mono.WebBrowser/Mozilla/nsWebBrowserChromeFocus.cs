using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000124 RID: 292
	internal class nsWebBrowserChromeFocus
	{
		// Token: 0x060008B3 RID: 2227 RVA: 0x00005ACA File Offset: 0x00003CCA
		public static nsIWebBrowserChromeFocus GetProxy(IWebBrowser control, nsIWebBrowserChromeFocus obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIWebBrowserChromeFocus).GUID, obj) as nsIWebBrowserChromeFocus;
		}
	}
}
