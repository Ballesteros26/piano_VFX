using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000CC RID: 204
	internal class nsDOMNodeList
	{
		// Token: 0x060006BB RID: 1723 RVA: 0x0000546E File Offset: 0x0000366E
		public static nsIDOMNodeList GetProxy(IWebBrowser control, nsIDOMNodeList obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMNodeList).GUID, obj) as nsIDOMNodeList;
		}
	}
}
