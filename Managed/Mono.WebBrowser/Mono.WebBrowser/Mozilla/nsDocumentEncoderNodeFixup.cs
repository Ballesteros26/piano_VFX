using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000E8 RID: 232
	internal class nsDocumentEncoderNodeFixup
	{
		// Token: 0x06000770 RID: 1904 RVA: 0x00005674 File Offset: 0x00003874
		public static nsIDocumentEncoderNodeFixup GetProxy(IWebBrowser control, nsIDocumentEncoderNodeFixup obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDocumentEncoderNodeFixup).GUID, obj) as nsIDocumentEncoderNodeFixup;
		}
	}
}
