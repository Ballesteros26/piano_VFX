using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000126 RID: 294
	internal class nsWebBrowserFocus
	{
		// Token: 0x060008BD RID: 2237 RVA: 0x00005AEF File Offset: 0x00003CEF
		public static nsIWebBrowserFocus GetProxy(IWebBrowser control, nsIWebBrowserFocus obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIWebBrowserFocus).GUID, obj) as nsIWebBrowserFocus;
		}
	}
}
