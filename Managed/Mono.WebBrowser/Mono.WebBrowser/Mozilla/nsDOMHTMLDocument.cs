using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000B8 RID: 184
	internal class nsDOMHTMLDocument
	{
		// Token: 0x060005CC RID: 1484 RVA: 0x000052FC File Offset: 0x000034FC
		public static nsIDOMHTMLDocument GetProxy(IWebBrowser control, nsIDOMHTMLDocument obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMHTMLDocument).GUID, obj) as nsIDOMHTMLDocument;
		}
	}
}
