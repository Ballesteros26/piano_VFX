using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200009C RID: 156
	internal class nsDOMDocument
	{
		// Token: 0x060004A5 RID: 1189 RVA: 0x000050F6 File Offset: 0x000032F6
		public static nsIDOMDocument GetProxy(IWebBrowser control, nsIDOMDocument obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMDocument).GUID, obj) as nsIDOMDocument;
		}
	}
}
