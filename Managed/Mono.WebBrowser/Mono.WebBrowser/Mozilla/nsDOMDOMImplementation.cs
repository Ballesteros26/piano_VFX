using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000098 RID: 152
	internal class nsDOMDOMImplementation
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x000050AC File Offset: 0x000032AC
		public static nsIDOMDOMImplementation GetProxy(IWebBrowser control, nsIDOMDOMImplementation obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMDOMImplementation).GUID, obj) as nsIDOMDOMImplementation;
		}
	}
}
