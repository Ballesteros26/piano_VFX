using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200006C RID: 108
	internal class nsAccessibilityService
	{
		// Token: 0x06000327 RID: 807 RVA: 0x00004D7E File Offset: 0x00002F7E
		public static nsIAccessibilityService GetProxy(IWebBrowser control, nsIAccessibilityService obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIAccessibilityService).GUID, obj) as nsIAccessibilityService;
		}
	}
}
