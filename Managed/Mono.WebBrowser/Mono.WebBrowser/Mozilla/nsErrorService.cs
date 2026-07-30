using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000EA RID: 234
	internal class nsErrorService
	{
		// Token: 0x06000778 RID: 1912 RVA: 0x00005699 File Offset: 0x00003899
		public static nsIErrorService GetProxy(IWebBrowser control, nsIErrorService obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIErrorService).GUID, obj) as nsIErrorService;
		}
	}
}
