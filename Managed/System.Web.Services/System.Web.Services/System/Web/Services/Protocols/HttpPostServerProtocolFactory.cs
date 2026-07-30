using System;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000039 RID: 57
	internal class HttpPostServerProtocolFactory : ServerProtocolFactory
	{
		// Token: 0x06000127 RID: 295 RVA: 0x0000573D File Offset: 0x0000393D
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
			return new HttpPostServerProtocol();
		}
	}
}
