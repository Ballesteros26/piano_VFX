using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000F4 RID: 244
	internal class nsInterfaceRequestor
	{
		// Token: 0x060007C2 RID: 1986 RVA: 0x00005752 File Offset: 0x00003952
		public static nsIInterfaceRequestor GetProxy(IWebBrowser control, nsIInterfaceRequestor obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIInterfaceRequestor).GUID, obj) as nsIInterfaceRequestor;
		}
	}
}
