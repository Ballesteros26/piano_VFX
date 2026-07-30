using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000EC RID: 236
	internal class nsFile
	{
		// Token: 0x060007A7 RID: 1959 RVA: 0x000056BE File Offset: 0x000038BE
		public static nsIFile GetProxy(IWebBrowser control, nsIFile obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIFile).GUID, obj) as nsIFile;
		}
	}
}
