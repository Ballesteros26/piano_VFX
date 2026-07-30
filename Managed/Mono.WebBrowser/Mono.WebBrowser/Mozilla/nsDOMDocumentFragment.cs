using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000A0 RID: 160
	internal class nsDOMDocumentFragment
	{
		// Token: 0x060004C3 RID: 1219 RVA: 0x00005140 File Offset: 0x00003340
		public static nsIDOMDocumentFragment GetProxy(IWebBrowser control, nsIDOMDocumentFragment obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMDocumentFragment).GUID, obj) as nsIDOMDocumentFragment;
		}
	}
}
