using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000B6 RID: 182
	internal class nsDOMHTMLCollection
	{
		// Token: 0x0600058D RID: 1421 RVA: 0x000052D7 File Offset: 0x000034D7
		public static nsIDOMHTMLCollection GetProxy(IWebBrowser control, nsIDOMHTMLCollection obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMHTMLCollection).GUID, obj) as nsIDOMHTMLCollection;
		}
	}
}
