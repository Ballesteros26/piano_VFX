using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000F0 RID: 240
	internal class nsIOService
	{
		// Token: 0x060007B8 RID: 1976 RVA: 0x00005708 File Offset: 0x00003908
		public static nsIIOService GetProxy(IWebBrowser control, nsIIOService obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIIOService).GUID, obj) as nsIIOService;
		}
	}
}
