using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000AC RID: 172
	internal class nsDOMEntityReference
	{
		// Token: 0x06000533 RID: 1331 RVA: 0x0000521E File Offset: 0x0000341E
		public static nsIDOMEntityReference GetProxy(IWebBrowser control, nsIDOMEntityReference obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMEntityReference).GUID, obj) as nsIDOMEntityReference;
		}
	}
}
