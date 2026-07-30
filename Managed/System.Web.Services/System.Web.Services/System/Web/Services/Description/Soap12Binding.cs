using System;
using System.Web.Services.Configuration;

namespace System.Web.Services.Description
{
	/// <summary>Represents a binding in a Web Services Description Language (WSDL) document to the SOAP version 1.2 protocol. This class cannot be inherited.</summary>
	// Token: 0x02000114 RID: 276
	[XmlFormatExtension("binding", "http://schemas.xmlsoap.org/wsdl/soap12/", typeof(Binding))]
	[XmlFormatExtensionPrefix("soap12", "http://schemas.xmlsoap.org/wsdl/soap12/")]
	public sealed class Soap12Binding : SoapBinding
	{
		/// <summary>Represents the XML namespace of a binding to the SOAP protocol version 1.2. This field is constant.</summary>
		// Token: 0x0400051D RID: 1309
		public new const string Namespace = "http://schemas.xmlsoap.org/wsdl/soap12/";

		/// <summary>Represents the transport protocol for the SOAP message is HTTP. This field is constant.</summary>
		// Token: 0x0400051E RID: 1310
		public new const string HttpTransport = "http://schemas.xmlsoap.org/soap/http";
	}
}
