using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000128 RID: 296
	internal class nsWebBrowserPersist
	{
		// Token: 0x060008CA RID: 2250 RVA: 0x00005B14 File Offset: 0x00003D14
		public static nsIWebBrowserPersist GetProxy(IWebBrowser control, nsIWebBrowserPersist obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIWebBrowserPersist).GUID, obj) as nsIWebBrowserPersist;
		}
	}
}
