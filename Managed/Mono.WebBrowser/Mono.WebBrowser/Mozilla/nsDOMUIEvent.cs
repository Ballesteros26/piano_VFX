using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000DC RID: 220
	internal class nsDOMUIEvent
	{
		// Token: 0x0600073F RID: 1855 RVA: 0x00005596 File Offset: 0x00003796
		public static nsIDOMUIEvent GetProxy(IWebBrowser control, nsIDOMUIEvent obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMUIEvent).GUID, obj) as nsIDOMUIEvent;
		}
	}
}
