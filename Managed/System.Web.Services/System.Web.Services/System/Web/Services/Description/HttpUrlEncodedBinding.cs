using System;
using System.Web.Services.Configuration;

namespace System.Web.Services.Description
{
	/// <summary>Represents an extensibility element added to an <see cref="T:System.Web.Services.Description.InputBinding" /> within an XML Web service. This class cannot be inherited.</summary>
	// Token: 0x020000C0 RID: 192
	[XmlFormatExtension("urlEncoded", "http://schemas.xmlsoap.org/wsdl/http/", typeof(InputBinding))]
	public sealed class HttpUrlEncodedBinding : ServiceDescriptionFormatExtension
	{
	}
}
