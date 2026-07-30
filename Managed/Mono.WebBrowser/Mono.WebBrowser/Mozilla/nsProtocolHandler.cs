using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000104 RID: 260
	internal class nsProtocolHandler
	{
		// Token: 0x0600081B RID: 2075 RVA: 0x0000587A File Offset: 0x00003A7A
		public static nsIProtocolHandler GetProxy(IWebBrowser control, nsIProtocolHandler obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIProtocolHandler).GUID, obj) as nsIProtocolHandler;
		}
	}
}
