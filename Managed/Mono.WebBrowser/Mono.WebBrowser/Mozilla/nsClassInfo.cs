using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200007C RID: 124
	internal class nsClassInfo
	{
		// Token: 0x060003A2 RID: 930 RVA: 0x00004EA6 File Offset: 0x000030A6
		public static nsIClassInfo GetProxy(IWebBrowser control, nsIClassInfo obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIClassInfo).GUID, obj) as nsIClassInfo;
		}
	}
}
