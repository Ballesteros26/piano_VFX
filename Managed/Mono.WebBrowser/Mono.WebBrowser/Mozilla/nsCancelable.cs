using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000078 RID: 120
	internal class nsCancelable
	{
		// Token: 0x0600037C RID: 892 RVA: 0x00004E5C File Offset: 0x0000305C
		public static nsICancelable GetProxy(IWebBrowser control, nsICancelable obj)
		{
			return Base.GetProxyForObject(control, typeof(nsICancelable).GUID, obj) as nsICancelable;
		}
	}
}
