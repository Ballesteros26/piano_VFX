using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000F2 RID: 242
	internal class nsInputStream
	{
		// Token: 0x060007BF RID: 1983 RVA: 0x0000572D File Offset: 0x0000392D
		public static nsIInputStream GetProxy(IWebBrowser control, nsIInputStream obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIInputStream).GUID, obj) as nsIInputStream;
		}
	}
}
