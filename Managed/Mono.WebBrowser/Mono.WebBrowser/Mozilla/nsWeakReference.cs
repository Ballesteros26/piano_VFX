using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200011E RID: 286
	internal class nsWeakReference
	{
		// Token: 0x0600089A RID: 2202 RVA: 0x00005A5B File Offset: 0x00003C5B
		public static nsIWeakReference GetProxy(IWebBrowser control, nsIWeakReference obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIWeakReference).GUID, obj) as nsIWeakReference;
		}
	}
}
