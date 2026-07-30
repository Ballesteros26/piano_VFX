using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000D0 RID: 208
	internal class nsDOMRGBColor
	{
		// Token: 0x060006DE RID: 1758 RVA: 0x000054B8 File Offset: 0x000036B8
		public static nsIDOMRGBColor GetProxy(IWebBrowser control, nsIDOMRGBColor obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMRGBColor).GUID, obj) as nsIDOMRGBColor;
		}
	}
}
