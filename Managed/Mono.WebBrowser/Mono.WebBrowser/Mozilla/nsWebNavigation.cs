using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200012C RID: 300
	internal class nsWebNavigation
	{
		// Token: 0x060008DE RID: 2270 RVA: 0x00005B5E File Offset: 0x00003D5E
		public static nsIWebNavigation GetProxy(IWebBrowser control, nsIWebNavigation obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIWebNavigation).GUID, obj) as nsIWebNavigation;
		}
	}
}
