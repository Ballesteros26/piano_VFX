using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000116 RID: 278
	internal class nsTimer
	{
		// Token: 0x0600086E RID: 2158 RVA: 0x000059C7 File Offset: 0x00003BC7
		public static nsITimer GetProxy(IWebBrowser control, nsITimer obj)
		{
			return Base.GetProxyForObject(control, typeof(nsITimer).GUID, obj) as nsITimer;
		}
	}
}
