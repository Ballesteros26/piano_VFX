using System;

namespace System.Web.Services.Protocols
{
	// Token: 0x0200002B RID: 43
	internal class DocumentationServerProtocolFactory : ServerProtocolFactory
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00004893 File Offset: 0x00002A93
		protected override ServerProtocol CreateIfRequestCompatible(HttpRequest request)
		{
			if (request.PathInfo.Length > 0)
			{
				return null;
			}
			if (request.HttpMethod != "GET")
			{
				return new UnsupportedRequestProtocol(405);
			}
			return new DocumentationServerProtocol();
		}
	}
}
