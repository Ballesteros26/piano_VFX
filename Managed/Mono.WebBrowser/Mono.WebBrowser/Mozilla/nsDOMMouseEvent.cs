using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000C2 RID: 194
	internal class nsDOMMouseEvent
	{
		// Token: 0x06000674 RID: 1652 RVA: 0x000053B5 File Offset: 0x000035B5
		public static nsIDOMMouseEvent GetProxy(IWebBrowser control, nsIDOMMouseEvent obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMMouseEvent).GUID, obj) as nsIDOMMouseEvent;
		}
	}
}
