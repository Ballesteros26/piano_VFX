using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200007A RID: 122
	internal class nsChannel
	{
		// Token: 0x06000398 RID: 920 RVA: 0x00004E81 File Offset: 0x00003081
		public static nsIChannel GetProxy(IWebBrowser control, nsIChannel obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIChannel).GUID, obj) as nsIChannel;
		}
	}
}
