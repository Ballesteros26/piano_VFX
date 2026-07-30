using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000076 RID: 118
	internal class nsArray
	{
		// Token: 0x06000379 RID: 889 RVA: 0x00004E37 File Offset: 0x00003037
		public static nsIArray GetProxy(IWebBrowser control, nsIArray obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIArray).GUID, obj) as nsIArray;
		}
	}
}
