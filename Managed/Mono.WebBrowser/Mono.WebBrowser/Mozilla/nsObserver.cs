using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000F8 RID: 248
	internal class nsObserver
	{
		// Token: 0x060007DB RID: 2011 RVA: 0x0000579C File Offset: 0x0000399C
		public static nsIObserver GetProxy(IWebBrowser control, nsIObserver obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIObserver).GUID, obj) as nsIObserver;
		}
	}
}
