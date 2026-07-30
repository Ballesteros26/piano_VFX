using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000FA RID: 250
	internal class nsOutputStream
	{
		// Token: 0x060007E3 RID: 2019 RVA: 0x000057C1 File Offset: 0x000039C1
		public static nsIOutputStream GetProxy(IWebBrowser control, nsIOutputStream obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIOutputStream).GUID, obj) as nsIOutputStream;
		}
	}
}
