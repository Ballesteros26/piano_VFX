using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000B4 RID: 180
	internal class nsDOMHTMLBodyElement
	{
		// Token: 0x06000588 RID: 1416 RVA: 0x000052B2 File Offset: 0x000034B2
		public static nsIDOMHTMLBodyElement GetProxy(IWebBrowser control, nsIDOMHTMLBodyElement obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMHTMLBodyElement).GUID, obj) as nsIDOMHTMLBodyElement;
		}
	}
}
