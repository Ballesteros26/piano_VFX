using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000D6 RID: 214
	internal class nsDOMStyleSheet
	{
		// Token: 0x06000708 RID: 1800 RVA: 0x00005527 File Offset: 0x00003727
		public static nsIDOMStyleSheet GetProxy(IWebBrowser control, nsIDOMStyleSheet obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMStyleSheet).GUID, obj) as nsIDOMStyleSheet;
		}
	}
}
