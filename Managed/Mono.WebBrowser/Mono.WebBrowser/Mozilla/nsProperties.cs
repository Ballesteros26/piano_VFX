using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000102 RID: 258
	internal class nsProperties
	{
		// Token: 0x06000813 RID: 2067 RVA: 0x00005855 File Offset: 0x00003A55
		public static nsIProperties GetProxy(IWebBrowser control, nsIProperties obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIProperties).GUID, obj) as nsIProperties;
		}
	}
}
