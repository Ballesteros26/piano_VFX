using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000106 RID: 262
	internal class nsRequest
	{
		// Token: 0x06000827 RID: 2087 RVA: 0x0000589F File Offset: 0x00003A9F
		public static nsIRequest GetProxy(IWebBrowser control, nsIRequest obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIRequest).GUID, obj) as nsIRequest;
		}
	}
}
