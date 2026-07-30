using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200011C RID: 284
	internal class nsURIContentListener
	{
		// Token: 0x06000897 RID: 2199 RVA: 0x00005A36 File Offset: 0x00003C36
		public static nsIURIContentListener GetProxy(IWebBrowser control, nsIURIContentListener obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIURIContentListener).GUID, obj) as nsIURIContentListener;
		}
	}
}
