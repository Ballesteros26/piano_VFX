using System;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000034 RID: 52
	internal class HttpGetServerProtocolFactory : ServerProtocolFactory
	{
		// Token: 0x0600011A RID: 282 RVA: 0x00005643 File Offset: 0x00003843
		protected override ServerProtocol CreateIfRequestCompatible(HttpRequest request)
		{
			if (request.PathInfo.Length < 2)
			{
				return null;
			}
			if (request.HttpMethod != "GET")
			{
				return new UnsupportedRequestProtocol(405);
			}
			return new HttpGetServerProtocol();
		}
	}
}
