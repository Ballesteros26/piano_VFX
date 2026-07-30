using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200008A RID: 138
	internal class nsDOMCSSRuleList
	{
		// Token: 0x06000405 RID: 1029 RVA: 0x00004FA9 File Offset: 0x000031A9
		public static nsIDOMCSSRuleList GetProxy(IWebBrowser control, nsIDOMCSSRuleList obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMCSSRuleList).GUID, obj) as nsIDOMCSSRuleList;
		}
	}
}
