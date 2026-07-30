using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000B2 RID: 178
	internal class nsDOMEventTarget
	{
		// Token: 0x06000547 RID: 1351 RVA: 0x0000528D File Offset: 0x0000348D
		public static nsIDOMEventTarget GetProxy(IWebBrowser control, nsIDOMEventTarget obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMEventTarget).GUID, obj) as nsIDOMEventTarget;
		}
	}
}
