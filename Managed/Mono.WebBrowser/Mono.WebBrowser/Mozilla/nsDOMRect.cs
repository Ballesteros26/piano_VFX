using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000D4 RID: 212
	internal class nsDOMRect
	{
		// Token: 0x060006FE RID: 1790 RVA: 0x00005502 File Offset: 0x00003702
		public static nsIDOMRect GetProxy(IWebBrowser control, nsIDOMRect obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMRect).GUID, obj) as nsIDOMRect;
		}
	}
}
