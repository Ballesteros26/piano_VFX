using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000090 RID: 144
	internal class nsDOMCSSValue
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x00005018 File Offset: 0x00003218
		public static nsIDOMCSSValue GetProxy(IWebBrowser control, nsIDOMCSSValue obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMCSSValue).GUID, obj) as nsIDOMCSSValue;
		}
	}
}
