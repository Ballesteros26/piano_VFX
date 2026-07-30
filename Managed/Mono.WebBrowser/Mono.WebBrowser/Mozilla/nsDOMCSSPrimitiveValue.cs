using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000086 RID: 134
	internal class nsDOMCSSPrimitiveValue
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x00004F5F File Offset: 0x0000315F
		public static nsIDOMCSSPrimitiveValue GetProxy(IWebBrowser control, nsIDOMCSSPrimitiveValue obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMCSSPrimitiveValue).GUID, obj) as nsIDOMCSSPrimitiveValue;
		}
	}
}
