using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000094 RID: 148
	internal class nsDOMComment
	{
		// Token: 0x0600046A RID: 1130 RVA: 0x00005062 File Offset: 0x00003262
		public static nsIDOMComment GetProxy(IWebBrowser control, nsIDOMComment obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMComment).GUID, obj) as nsIDOMComment;
		}
	}
}
