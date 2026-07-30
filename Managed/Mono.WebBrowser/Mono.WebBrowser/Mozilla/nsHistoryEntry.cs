using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000EE RID: 238
	internal class nsHistoryEntry
	{
		// Token: 0x060007AC RID: 1964 RVA: 0x000056E3 File Offset: 0x000038E3
		public static nsIHistoryEntry GetProxy(IWebBrowser control, nsIHistoryEntry obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIHistoryEntry).GUID, obj) as nsIHistoryEntry;
		}
	}
}
