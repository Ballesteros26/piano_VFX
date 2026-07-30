using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200006E RID: 110
	internal class nsAccessible
	{
		// Token: 0x06000353 RID: 851 RVA: 0x00004DA3 File Offset: 0x00002FA3
		public static nsIAccessible GetProxy(IWebBrowser control, nsIAccessible obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIAccessible).GUID, obj) as nsIAccessible;
		}
	}
}
