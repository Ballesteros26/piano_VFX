using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000DE RID: 222
	internal class nsDOMViewCSS
	{
		// Token: 0x06000743 RID: 1859 RVA: 0x000055BB File Offset: 0x000037BB
		public static nsIDOMViewCSS GetProxy(IWebBrowser control, nsIDOMViewCSS obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMViewCSS).GUID, obj) as nsIDOMViewCSS;
		}
	}
}
