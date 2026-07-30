using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000E2 RID: 226
	internal class nsDOMWindowCollection
	{
		// Token: 0x0600075B RID: 1883 RVA: 0x00005605 File Offset: 0x00003805
		public static nsIDOMWindowCollection GetProxy(IWebBrowser control, nsIDOMWindowCollection obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMWindowCollection).GUID, obj) as nsIDOMWindowCollection;
		}
	}
}
