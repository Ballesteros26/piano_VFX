using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000E4 RID: 228
	internal class nsDocCharset
	{
		// Token: 0x0600075F RID: 1887 RVA: 0x0000562A File Offset: 0x0000382A
		public static nsIDocCharset GetProxy(IWebBrowser control, nsIDocCharset obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDocCharset).GUID, obj) as nsIDocCharset;
		}
	}
}
