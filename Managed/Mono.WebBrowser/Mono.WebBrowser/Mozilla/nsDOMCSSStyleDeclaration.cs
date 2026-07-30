using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200008C RID: 140
	internal class nsDOMCSSStyleDeclaration
	{
		// Token: 0x06000411 RID: 1041 RVA: 0x00004FCE File Offset: 0x000031CE
		public static nsIDOMCSSStyleDeclaration GetProxy(IWebBrowser control, nsIDOMCSSStyleDeclaration obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMCSSStyleDeclaration).GUID, obj) as nsIDOMCSSStyleDeclaration;
		}
	}
}
