using System;
using System.Web.Services.Configuration;

namespace System.Web.Services.Description
{
	/// <summary>Represents a service description format extension applied to an <see cref="T:System.Web.Services.Description.InputBinding" /> or an <see cref="T:System.Web.Services.Description.OutputBinding" /> when an XML Web service supports the SOAP protocol version 1.2. This class cannot be inherited.</summary>
	// Token: 0x02000116 RID: 278
	[XmlFormatExtension("body", "http://schemas.xmlsoap.org/wsdl/soap12/", typeof(InputBinding), typeof(OutputBinding), typeof(MimePart))]
	public sealed class Soap12BodyBinding : SoapBodyBinding
	{
	}
}
