using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000088 RID: 136
	internal class nsDOMCSSRule
	{
		// Token: 0x06000401 RID: 1025 RVA: 0x00004F84 File Offset: 0x00003184
		public static nsIDOMCSSRule GetProxy(IWebBrowser control, nsIDOMCSSRule obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMCSSRule).GUID, obj) as nsIDOMCSSRule;
		}
	}
}
