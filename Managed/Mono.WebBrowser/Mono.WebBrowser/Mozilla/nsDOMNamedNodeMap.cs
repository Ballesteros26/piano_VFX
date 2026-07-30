using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000C8 RID: 200
	internal class nsDOMNamedNodeMap
	{
		// Token: 0x0600069C RID: 1692 RVA: 0x00005424 File Offset: 0x00003624
		public static nsIDOMNamedNodeMap GetProxy(IWebBrowser control, nsIDOMNamedNodeMap obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMNamedNodeMap).GUID, obj) as nsIDOMNamedNodeMap;
		}
	}
}
