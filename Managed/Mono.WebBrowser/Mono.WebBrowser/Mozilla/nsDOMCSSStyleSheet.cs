using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200008E RID: 142
	internal class nsDOMCSSStyleSheet
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x00004FF3 File Offset: 0x000031F3
		public static nsIDOMCSSStyleSheet GetProxy(IWebBrowser control, nsIDOMCSSStyleSheet obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMCSSStyleSheet).GUID, obj) as nsIDOMCSSStyleSheet;
		}
	}
}
