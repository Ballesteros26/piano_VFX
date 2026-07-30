using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200010A RID: 266
	internal class nsSHistory
	{
		// Token: 0x06000836 RID: 2102 RVA: 0x000058E9 File Offset: 0x00003AE9
		public static nsISHistory GetProxy(IWebBrowser control, nsISHistory obj)
		{
			return Base.GetProxyForObject(control, typeof(nsISHistory).GUID, obj) as nsISHistory;
		}
	}
}
