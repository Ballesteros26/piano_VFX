using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000100 RID: 256
	internal class nsPrefService
	{
		// Token: 0x0600080C RID: 2060 RVA: 0x00005830 File Offset: 0x00003A30
		public static nsIPrefService GetProxy(IWebBrowser control, nsIPrefService obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIPrefService).GUID, obj) as nsIPrefService;
		}
	}
}
