using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000122 RID: 290
	internal class nsWebBrowserChrome
	{
		// Token: 0x060008AF RID: 2223 RVA: 0x00005AA5 File Offset: 0x00003CA5
		public static nsIWebBrowserChrome GetProxy(IWebBrowser control, nsIWebBrowserChrome obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIWebBrowserChrome).GUID, obj) as nsIWebBrowserChrome;
		}
	}
}
