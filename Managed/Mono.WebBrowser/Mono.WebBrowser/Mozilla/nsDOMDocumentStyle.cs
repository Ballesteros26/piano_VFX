using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000A4 RID: 164
	internal class nsDOMDocumentStyle
	{
		// Token: 0x060004C9 RID: 1225 RVA: 0x0000518A File Offset: 0x0000338A
		public static nsIDOMDocumentStyle GetProxy(IWebBrowser control, nsIDOMDocumentStyle obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMDocumentStyle).GUID, obj) as nsIDOMDocumentStyle;
		}
	}
}
