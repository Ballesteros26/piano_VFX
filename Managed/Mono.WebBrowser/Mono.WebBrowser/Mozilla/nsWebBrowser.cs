using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000120 RID: 288
	internal class nsWebBrowser
	{
		// Token: 0x060008A3 RID: 2211 RVA: 0x00005A80 File Offset: 0x00003C80
		public static nsIWebBrowser GetProxy(IWebBrowser control, nsIWebBrowser obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIWebBrowser).GUID, obj) as nsIWebBrowser;
		}
	}
}
