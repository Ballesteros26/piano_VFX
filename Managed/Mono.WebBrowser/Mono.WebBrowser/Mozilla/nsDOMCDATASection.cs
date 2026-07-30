using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000084 RID: 132
	internal class nsDOMCDATASection
	{
		// Token: 0x060003ED RID: 1005 RVA: 0x00004F3A File Offset: 0x0000313A
		public static nsIDOMCDATASection GetProxy(IWebBrowser control, nsIDOMCDATASection obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMCDATASection).GUID, obj) as nsIDOMCDATASection;
		}
	}
}
