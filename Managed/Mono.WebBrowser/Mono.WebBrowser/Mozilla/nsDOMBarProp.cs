using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000082 RID: 130
	internal class nsDOMBarProp
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x00004F15 File Offset: 0x00003115
		public static nsIDOMBarProp GetProxy(IWebBrowser control, nsIDOMBarProp obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMBarProp).GUID, obj) as nsIDOMBarProp;
		}
	}
}
