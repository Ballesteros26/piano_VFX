using System;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000038 RID: 56
	internal class HttpPostLocalhostServerProtocolFactory : ServerProtocolFactory
	{
		// Token: 0x06000125 RID: 293 RVA: 0x000056E4 File Offset: 0x000038E4
		protected override ServerProtocol CreateIfRequestCompatible(HttpRequest request)
		{
			if (request.PathInfo.Length < 2)
			{
				return null;
			}
			if (request.HttpMethod != "POST")
			{
				return new UnsupportedRequestProtocol(405);
			}
			if (!request.Url.IsLoopback && !request.IsLocal)
			{
				return null;
			}
			return new HttpPostServerProtocol();
		}
	}
}
