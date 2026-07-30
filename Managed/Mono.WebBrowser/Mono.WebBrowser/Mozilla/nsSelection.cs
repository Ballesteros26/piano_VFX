using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200010E RID: 270
	internal class nsSelection
	{
		// Token: 0x06000853 RID: 2131 RVA: 0x00005933 File Offset: 0x00003B33
		public static nsISelection GetProxy(IWebBrowser control, nsISelection obj)
		{
			return Base.GetProxyForObject(control, typeof(nsISelection).GUID, obj) as nsISelection;
		}
	}
}
