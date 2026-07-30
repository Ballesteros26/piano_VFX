using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200009E RID: 158
	internal class nsDOMDocumentEvent
	{
		// Token: 0x060004A8 RID: 1192 RVA: 0x0000511B File Offset: 0x0000331B
		public static nsIDOMDocumentEvent GetProxy(IWebBrowser control, nsIDOMDocumentEvent obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMDocumentEvent).GUID, obj) as nsIDOMDocumentEvent;
		}
	}
}
