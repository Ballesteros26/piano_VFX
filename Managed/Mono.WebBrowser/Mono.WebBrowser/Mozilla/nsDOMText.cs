using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000DA RID: 218
	internal class nsDOMText
	{
		// Token: 0x06000730 RID: 1840 RVA: 0x00005571 File Offset: 0x00003771
		public static nsIDOMText GetProxy(IWebBrowser control, nsIDOMText obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMText).GUID, obj) as nsIDOMText;
		}
	}
}
