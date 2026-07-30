using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000108 RID: 264
	internal class nsRequestObserver
	{
		// Token: 0x0600082B RID: 2091 RVA: 0x000058C4 File Offset: 0x00003AC4
		public static nsIRequestObserver GetProxy(IWebBrowser control, nsIRequestObserver obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIRequestObserver).GUID, obj) as nsIRequestObserver;
		}
	}
}
