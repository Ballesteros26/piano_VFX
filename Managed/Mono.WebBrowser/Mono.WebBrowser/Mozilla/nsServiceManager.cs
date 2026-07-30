using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000110 RID: 272
	internal class nsServiceManager
	{
		// Token: 0x06000859 RID: 2137 RVA: 0x00005958 File Offset: 0x00003B58
		public static nsIServiceManager GetProxy(IWebBrowser control, nsIServiceManager obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIServiceManager).GUID, obj) as nsIServiceManager;
		}
	}
}
