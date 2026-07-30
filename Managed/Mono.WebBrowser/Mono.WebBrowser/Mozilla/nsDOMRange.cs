using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000D2 RID: 210
	internal class nsDOMRange
	{
		// Token: 0x060006F8 RID: 1784 RVA: 0x000054DD File Offset: 0x000036DD
		public static nsIDOMRange GetProxy(IWebBrowser control, nsIDOMRange obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMRange).GUID, obj) as nsIDOMRange;
		}
	}
}
