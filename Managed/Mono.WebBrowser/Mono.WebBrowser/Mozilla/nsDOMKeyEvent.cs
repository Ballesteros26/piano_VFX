using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000BE RID: 190
	internal class nsDOMKeyEvent
	{
		// Token: 0x06000652 RID: 1618 RVA: 0x0000536B File Offset: 0x0000356B
		public static nsIDOMKeyEvent GetProxy(IWebBrowser control, nsIDOMKeyEvent obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMKeyEvent).GUID, obj) as nsIDOMKeyEvent;
		}
	}
}
