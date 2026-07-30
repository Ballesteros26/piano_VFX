using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000B0 RID: 176
	internal class nsDOMEventListener
	{
		// Token: 0x06000542 RID: 1346 RVA: 0x00005268 File Offset: 0x00003468
		public static nsIDOMEventListener GetProxy(IWebBrowser control, nsIDOMEventListener obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMEventListener).GUID, obj) as nsIDOMEventListener;
		}
	}
}
