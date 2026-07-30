using System;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000028 RID: 40
	internal class DiscoveryServerProtocolFactory : ServerProtocolFactory
	{
		// Token: 0x060000DF RID: 223 RVA: 0x0000422C File Offset: 0x0000242C
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
			string text = request.QueryString[null];
			if (text == null)
			{
				text = "";
			}
			if (request.QueryString["schema"] == null && request.QueryString["wsdl"] == null && string.Compare(text, "wsdl", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(text, "disco", StringComparison.OrdinalIgnoreCase) != 0)
			{
				return null;
			}
			return new DiscoveryServerProtocol();
		}
	}
}
