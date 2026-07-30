using System;

namespace System.Web.Services.Protocols
{
	/// <summary>Specifies how a SOAP message is routed to the Web server hosting the XML Web service.</summary>
	// Token: 0x02000082 RID: 130
	public enum SoapServiceRoutingStyle
	{
		/// <summary>The SOAP message is routed based on the SOAPAction HTTP header.</summary>
		// Token: 0x040002FE RID: 766
		SoapAction,
		/// <summary>The SOAP Message is routed based on the first child element following the &lt;Body&gt; XML element of the SOAP message.</summary>
		// Token: 0x040002FF RID: 767
		RequestElement
	}
}
