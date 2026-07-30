using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200009A RID: 154
	internal class nsDOMDOMStringList
	{
		// Token: 0x06000479 RID: 1145 RVA: 0x000050D1 File Offset: 0x000032D1
		public static nsIDOMDOMStringList GetProxy(IWebBrowser control, nsIDOMDOMStringList obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMDOMStringList).GUID, obj) as nsIDOMDOMStringList;
		}
	}
}
