using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000C4 RID: 196
	internal class nsDOMNSHTMLElement
	{
		// Token: 0x0600068A RID: 1674 RVA: 0x000053DA File Offset: 0x000035DA
		public static nsIDOMNSHTMLElement GetProxy(IWebBrowser control, nsIDOMNSHTMLElement obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMNSHTMLElement).GUID, obj) as nsIDOMNSHTMLElement;
		}
	}
}
