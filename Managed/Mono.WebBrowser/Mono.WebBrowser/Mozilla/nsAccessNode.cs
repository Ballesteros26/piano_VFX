using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200006A RID: 106
	internal class nsAccessNode
	{
		// Token: 0x060002FB RID: 763 RVA: 0x00004D59 File Offset: 0x00002F59
		public static nsIAccessNode GetProxy(IWebBrowser control, nsIAccessNode obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIAccessNode).GUID, obj) as nsIAccessNode;
		}
	}
}
