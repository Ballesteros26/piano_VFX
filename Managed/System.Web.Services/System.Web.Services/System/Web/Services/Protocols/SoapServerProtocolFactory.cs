using System;
using System.Security.Permissions;

namespace System.Web.Services.Protocols
{
	/// <summary>The .NET Framework creates an instance of the <see cref="T:System.Web.Services.Protocols.SoapServerProtocolFactory" /> class to process XML Web service requests.</summary>
	// Token: 0x0200007E RID: 126
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class SoapServerProtocolFactory : ServerProtocolFactory
	{
		/// <summary>Returns a <see cref="T:System.Web.Services.Protocols.ServerProtocol" /> that can be used to process the XML Web service request specified by <paramref name="request" />.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Protocols.ServerProtocol" /> that can be used to process the XML Web service request specified by <paramref name="request" />.</returns>
		/// <param name="request">The <see cref="T:System.Web.HttpRequest" /> that represents the Web service request.</param>
		// Token: 0x06000350 RID: 848 RVA: 0x0000F036 File Offset: 0x0000D236
		protected override ServerProtocol CreateIfRequestCompatible(HttpRequest request)
		{
			if (request.PathInfo.Length > 0)
			{
				return null;
			}
			if (request.HttpMethod != "POST")
			{
				return new UnsupportedRequestProtocol(405);
			}
			return new SoapServerProtocol();
		}
	}
}
