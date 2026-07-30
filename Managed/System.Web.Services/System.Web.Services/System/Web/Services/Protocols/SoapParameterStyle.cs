using System;

namespace System.Web.Services.Protocols
{
	/// <summary>Specifies how parameters are formatted in a SOAP message.</summary>
	// Token: 0x02000070 RID: 112
	public enum SoapParameterStyle
	{
		/// <summary>Specifies using the default <see cref="T:System.Web.Services.Protocols.SoapParameterStyle" /> for the XML Web service. The default for an XML Web service can be set by applying a <see cref="T:System.Web.Services.Protocols.SoapDocumentServiceAttribute" /> to the class implementing the XML Web service. If a <see cref="T:System.Web.Services.Protocols.SoapDocumentServiceAttribute" /> is not applied to the class implementing the XML Web service, the default is <see cref="F:System.Web.Services.Protocols.SoapParameterStyle.Wrapped" />.</summary>
		// Token: 0x040002A3 RID: 675
		Default,
		/// <summary>Parameters sent to and from an XML Web service method are placed in XML elements directly following the Body element of a SOAP request or SOAP response.</summary>
		// Token: 0x040002A4 RID: 676
		Bare,
		/// <summary>Parameters sent to and from an XML Web service method are encapsulated within a single XML element followig the Body element of the XML portion of a SOAP request or SOAP response.</summary>
		// Token: 0x040002A5 RID: 677
		Wrapped
	}
}
