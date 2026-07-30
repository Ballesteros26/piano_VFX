using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000096 RID: 150
	internal class nsDOMCounter
	{
		// Token: 0x0600046F RID: 1135 RVA: 0x00005087 File Offset: 0x00003287
		public static nsIDOMCounter GetProxy(IWebBrowser control, nsIDOMCounter obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMCounter).GUID, obj) as nsIDOMCounter;
		}
	}
}
