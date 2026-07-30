using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000114 RID: 276
	internal class nsStreamListener
	{
		// Token: 0x06000862 RID: 2146 RVA: 0x000059A2 File Offset: 0x00003BA2
		public static nsIStreamListener GetProxy(IWebBrowser control, nsIStreamListener obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIStreamListener).GUID, obj) as nsIStreamListener;
		}
	}
}
