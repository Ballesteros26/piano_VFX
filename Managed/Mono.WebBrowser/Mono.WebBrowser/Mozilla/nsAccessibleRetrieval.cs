using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000074 RID: 116
	internal class nsAccessibleRetrieval
	{
		// Token: 0x06000373 RID: 883 RVA: 0x00004E12 File Offset: 0x00003012
		public static nsIAccessibleRetrieval GetProxy(IWebBrowser control, nsIAccessibleRetrieval obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIAccessibleRetrieval).GUID, obj) as nsIAccessibleRetrieval;
		}
	}
}
