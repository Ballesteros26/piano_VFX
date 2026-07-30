using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000BC RID: 188
	internal class nsDOMHTMLStyleElement
	{
		// Token: 0x0600063C RID: 1596 RVA: 0x00005346 File Offset: 0x00003546
		public static nsIDOMHTMLStyleElement GetProxy(IWebBrowser control, nsIDOMHTMLStyleElement obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMHTMLStyleElement).GUID, obj) as nsIDOMHTMLStyleElement;
		}
	}
}
