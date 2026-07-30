using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000A8 RID: 168
	internal class nsDOMDocumentView
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x000051D4 File Offset: 0x000033D4
		public static nsIDOMDocumentView GetProxy(IWebBrowser control, nsIDOMDocumentView obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMDocumentView).GUID, obj) as nsIDOMDocumentView;
		}
	}
}
