using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000E0 RID: 224
	internal class nsDOMWindow
	{
		// Token: 0x06000756 RID: 1878 RVA: 0x000055E0 File Offset: 0x000037E0
		public static nsIDOMWindow GetProxy(IWebBrowser control, nsIDOMWindow obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMWindow).GUID, obj) as nsIDOMWindow;
		}
	}
}
