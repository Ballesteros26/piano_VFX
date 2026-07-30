using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200010C RID: 268
	internal class nsSHistoryListener
	{
		// Token: 0x0600083E RID: 2110 RVA: 0x0000590E File Offset: 0x00003B0E
		public static nsISHistoryListener GetProxy(IWebBrowser control, nsISHistoryListener obj)
		{
			return Base.GetProxyForObject(control, typeof(nsISHistoryListener).GUID, obj) as nsISHistoryListener;
		}
	}
}
