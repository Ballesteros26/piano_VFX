using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000FE RID: 254
	internal class nsPrefBranch
	{
		// Token: 0x06000804 RID: 2052 RVA: 0x0000580B File Offset: 0x00003A0B
		public static nsIPrefBranch GetProxy(IWebBrowser control, nsIPrefBranch obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIPrefBranch).GUID, obj) as nsIPrefBranch;
		}
	}
}
