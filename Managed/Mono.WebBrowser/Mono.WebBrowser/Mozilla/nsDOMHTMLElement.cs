using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000BA RID: 186
	internal class nsDOMHTMLElement
	{
		// Token: 0x06000601 RID: 1537 RVA: 0x00005321 File Offset: 0x00003521
		public static nsIDOMHTMLElement GetProxy(IWebBrowser control, nsIDOMHTMLElement obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMHTMLElement).GUID, obj) as nsIDOMHTMLElement;
		}
	}
}
