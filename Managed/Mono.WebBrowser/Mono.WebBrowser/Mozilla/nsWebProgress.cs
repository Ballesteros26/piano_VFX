using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200012E RID: 302
	internal class nsWebProgress
	{
		// Token: 0x060008E4 RID: 2276 RVA: 0x00005B83 File Offset: 0x00003D83
		public static nsIWebProgress GetProxy(IWebBrowser control, nsIWebProgress obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIWebProgress).GUID, obj) as nsIWebProgress;
		}
	}
}
