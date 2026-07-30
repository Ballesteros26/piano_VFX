using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000AE RID: 174
	internal class nsDOMEvent
	{
		// Token: 0x0600053F RID: 1343 RVA: 0x00005243 File Offset: 0x00003443
		public static nsIDOMEvent GetProxy(IWebBrowser control, nsIDOMEvent obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMEvent).GUID, obj) as nsIDOMEvent;
		}
	}
}
