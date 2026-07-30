using System;
using System.ComponentModel;
using System.IO;
using System.Web.Services.Configuration;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Represents an extension added to a <see cref="T:System.Web.Services.Description.Binding" /> within an XML Web service. This class cannot be inherited.</summary>
	// Token: 0x0200011F RID: 287
	[XmlFormatExtension("binding", "http://schemas.xmlsoap.org/wsdl/soap/", typeof(Binding))]
	[XmlFormatExtensionPrefix("soapenc", "http://schemas.xmlsoap.org/soap/encoding/")]
	[XmlFormatExtensionPrefix("soap", "http://schemas.xmlsoap.org/wsdl/soap/")]
	public class SoapBinding : ServiceDescriptionFormatExtension
	{
		/// <summary>Gets or sets the URI with the specification for HTTP transmission of SOAP data.</summary>
		/// <returns>A string value representing the URI for the specification for data transmission by means of SOAP. The default is an empty string ("").</returns>
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x0003C40D File Offset: 0x0003A60D
		// (set) Token: 0x060008A4 RID: 2212 RVA: 0x0003C423 File Offset: 0x0003A623
		[XmlAttribute("transport")]
		public string Transport
		{
			get
			{
				if (this.transport != null)
				{
					return this.transport;
				}
				return string.Empty;
			}
			set
			{
				this.transport = value;
			}
		}

		/// <summary>Specifies the type of SOAP binding used by the current <see cref="T:System.Web.Services.Description.SoapBinding" />.</summary>
		/// <returns>One of the <see cref="T:System.Web.Services.Description.SoapBindingStyle" /> values. The default is Document.</returns>
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0003C42C File Offset: 0x0003A62C
		// (set) Token: 0x060008A6 RID: 2214 RVA: 0x0003C434 File Offset: 0x0003A634
		[XmlAttribute("style")]
		[DefaultValue(SoapBindingStyle.Document)]
		public SoapBindingStyle Style
		{
			get
			{
				return this.style;
			}
			set
			{
				this.style = value;
			}
		}

		/// <summary>Gets the binding schema that is to be used to transmit data by using the SOAP protocol version 1.1.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchema" /> object that represents the binding schema that is to be used to transmit data by using the SOAP protocol version 1.1.</returns>
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x0003C43D File Offset: 0x0003A63D
		public static XmlSchema Schema
		{
			get
			{
				if (SoapBinding.schema == null)
				{
					SoapBinding.schema = XmlSchema.Read(new StringReader("<?xml version='1.0' encoding='UTF-8' ?> \n<xs:schema xmlns:soap='http://schemas.xmlsoap.org/wsdl/soap/' xmlns:wsdl='http://schemas.xmlsoap.org/wsdl/' targetNamespace='http://schemas.xmlsoap.org/wsdl/soap/' xmlns:xs='http://www.w3.org/2001/XMLSchema'>\n  <xs:import namespace='http://schemas.xmlsoap.org/wsdl/' />\n  <xs:simpleType name='encodingStyle'>\n    <xs:annotation>\n      <xs:documentation>\n      'encodingStyle' indicates any canonicalization conventions followed in the contents of the containing element.  For example, the value 'http://schemas.xmlsoap.org/soap/encoding/' indicates the pattern described in SOAP specification\n      </xs:documentation>\n    </xs:annotation>\n    <xs:list itemType='xs:anyURI' />\n  </xs:simpleType>\n  <xs:element name='binding' type='soap:tBinding' />\n  <xs:complexType name='tBinding'>\n    <xs:complexContent mixed='false'>\n      <xs:extension base='wsdl:tExtensibilityElement'>\n        <xs:attribute name='transport' type='xs:anyURI' use='required' />\n        <xs:attribute name='style' type='soap:tStyleChoice' use='optional' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n  <xs:simpleType name='tStyleChoice'>\n    <xs:restriction base='xs:string'>\n      <xs:enumeration value='rpc' />\n      <xs:enumeration value='document' />\n    </xs:restriction>\n  </xs:simpleType>\n  <xs:element name='operation' type='soap:tOperation' />\n  <xs:complexType name='tOperation'>\n    <xs:complexContent mixed='false'>\n      <xs:extension base='wsdl:tExtensibilityElement'>\n        <xs:attribute name='soapAction' type='xs:anyURI' use='optional' />\n        <xs:attribute name='style' type='soap:tStyleChoice' use='optional' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n  <xs:element name='body' type='soap:tBody' />\n  <xs:attributeGroup name='tBodyAttributes'>\n    <xs:attribute name='encodingStyle' type='soap:encodingStyle' use='optional' />\n    <xs:attribute name='use' type='soap:useChoice' use='optional' />\n    <xs:attribute name='namespace' type='xs:anyURI' use='optional' />\n  </xs:attributeGroup>\n  <xs:complexType name='tBody'>\n    <xs:complexContent mixed='false'>\n      <xs:extension base='wsdl:tExtensibilityElement'>\n        <xs:attribute name='parts' type='xs:NMTOKENS' use='optional' />\n        <xs:attributeGroup ref='soap:tBodyAttributes' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n  <xs:simpleType name='useChoice'>\n    <xs:restriction base='xs:string'>\n      <xs:enumeration value='literal' />\n      <xs:enumeration value='encoded' />\n    </xs:restriction>\n  </xs:simpleType>\n  <xs:element name='fault' type='soap:tFault' />\n  <xs:complexType name='tFaultRes' abstract='true'>\n    <xs:complexContent mixed='false'>\n      <xs:restriction base='soap:tBody'>\n        <xs:attribute ref='wsdl:required' use='optional' />\n        <xs:attribute name='parts' type='xs:NMTOKENS' use='prohibited' />\n        <xs:attributeGroup ref='soap:tBodyAttributes' />\n      </xs:restriction>\n    </xs:complexContent>\n  </xs:complexType>\n  <xs:complexType name='tFault'>\n    <xs:complexContent mixed='false'>\n      <xs:extension base='soap:tFaultRes'>\n        <xs:attribute name='name' type='xs:NCName' use='required' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n  <xs:element name='header' type='soap:tHeader' />\n  <xs:attributeGroup name='tHeaderAttributes'>\n    <xs:attribute name='message' type='xs:QName' use='required' />\n    <xs:attribute name='part' type='xs:NMTOKEN' use='required' />\n    <xs:attribute name='use' type='soap:useChoice' use='required' />\n    <xs:attribute name='encodingStyle' type='soap:encodingStyle' use='optional' />\n    <xs:attribute name='namespace' type='xs:anyURI' use='optional' />\n  </xs:attributeGroup>\n  <xs:complexType name='tHeader'>\n    <xs:complexContent mixed='false'>\n      <xs:extension base='wsdl:tExtensibilityElement'>\n        <xs:sequence>\n          <xs:element minOccurs='0' maxOccurs='unbounded' ref='soap:headerfault' />\n        </xs:sequence>\n        <xs:attributeGroup ref='soap:tHeaderAttributes' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n  <xs:element name='headerfault' type='soap:tHeaderFault' />\n  <xs:complexType name='tHeaderFault'>\n    <xs:attributeGroup ref='soap:tHeaderAttributes' />\n  </xs:complexType>\n  <xs:element name='address' type='soap:tAddress' />\n  <xs:complexType name='tAddress'>\n    <xs:complexContent mixed='false'>\n      <xs:extension base='wsdl:tExtensibilityElement'>\n        <xs:attribute name='location' type='xs:anyURI' use='required' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n</xs:schema>"), null);
				}
				return SoapBinding.schema;
			}
		}

		// Token: 0x04000529 RID: 1321
		private SoapBindingStyle style = SoapBindingStyle.Document;

		// Token: 0x0400052A RID: 1322
		private string transport;

		// Token: 0x0400052B RID: 1323
		private static XmlSchema schema;

		/// <summary>Gets the URI for the XML namespace of the <see cref="T:System.Web.Services.Description.SoapBinding" /> class. This field is constant.</summary>
		// Token: 0x0400052C RID: 1324
		public const string Namespace = "http://schemas.xmlsoap.org/wsdl/soap/";

		/// <summary>Gets the URI for the standard protocol specifying HTTP transmission of SOAP data. This field is constant.</summary>
		// Token: 0x0400052D RID: 1325
		public const string HttpTransport = "http://schemas.xmlsoap.org/soap/http";
	}
}
