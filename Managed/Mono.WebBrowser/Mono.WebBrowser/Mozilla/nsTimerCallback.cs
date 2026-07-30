using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000118 RID: 280
	internal class nsTimerCallback
	{
		// Token: 0x06000871 RID: 2161 RVA: 0x000059EC File Offset: 0x00003BEC
		public static nsITimerCallback GetProxy(IWebBrowser control, nsITimerCallback obj)
		{
			return Base.GetProxyForObject(control, typeof(nsITimerCallback).GUID, obj) as nsITimerCallback;
		}
	}
}
