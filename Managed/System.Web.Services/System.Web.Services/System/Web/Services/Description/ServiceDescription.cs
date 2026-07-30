using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Services.Configuration;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Provides a means of creating and formatting a valid Web Services Description Language (WSDL) document file, complete with appropriate namespaces, elements, and attributes, for describing an XML Web service. This class cannot be inherited.</summary>
	// Token: 0x020000E6 RID: 230
	[XmlFormatExtensionPoint("Extensions")]
	[XmlRoot("definitions", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
	public sealed class ServiceDescription : NamedItem
	{
		// Token: 0x06000610 RID: 1552 RVA: 0x0001BB18 File Offset: 0x00019D18
		private static void InstanceValidation(object sender, ValidationEventArgs args)
		{
			ServiceDescription.warnings.Add(Res.GetString("WsdlInstanceValidationDetails", new object[]
			{
				args.Message,
				args.Exception.LineNumber.ToString(CultureInfo.InvariantCulture),
				args.Exception.LinePosition.ToString(CultureInfo.InvariantCulture)
			}));
		}

		/// <summary>Gets or sets the URL of the XML Web service to which the <see cref="T:System.Web.Services.Description.ServiceDescription" /> instance applies.</summary>
		/// <returns>The URL of the XML Web service. The default value is an empty string ("").</returns>
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001BB7F File Offset: 0x00019D7F
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x0001BB95 File Offset: 0x00019D95
		[XmlIgnore]
		public string RetrievalUrl
		{
			get
			{
				if (this.retrievalUrl != null)
				{
					return this.retrievalUrl;
				}
				return string.Empty;
			}
			set
			{
				this.retrievalUrl = value;
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001BB9E File Offset: 0x00019D9E
		internal void SetParent(ServiceDescriptionCollection parent)
		{
			this.parent = parent;
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> instance of which the <see cref="T:System.Web.Services.Description.ServiceDescription" /> is a member.</summary>
		/// <returns>A collection of service description.</returns>
		/// <exception cref="T:System.NullReferenceException">The <see cref="T:System.Web.Services.Description.ServiceDescription" /> has not been assigned to a parent collection. </exception>
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0001BBA7 File Offset: 0x00019DA7
		[XmlIgnore]
		public ServiceDescriptionCollection ServiceDescriptions
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Gets the collection of extensibility elements contained in the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</summary>
		/// <returns>The collection of extensibility elements contained in the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0001BBAF File Offset: 0x00019DAF
		[XmlIgnore]
		public override ServiceDescriptionFormatExtensionCollection Extensions
		{
			get
			{
				if (this.extensions == null)
				{
					this.extensions = new ServiceDescriptionFormatExtensionCollection(this);
				}
				return this.extensions;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Web.Services.Description.Import" /> elements contained in the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</summary>
		/// <returns>A collection of import elements contained in the service description.</returns>
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0001BBCB File Offset: 0x00019DCB
		[XmlElement("import")]
		public ImportCollection Imports
		{
			get
			{
				if (this.imports == null)
				{
					this.imports = new ImportCollection(this);
				}
				return this.imports;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Services.Description.Types" /> contained by the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.Types" /> instance that represents the data types of both the parameters and return values of the methods exposed by the XML Web service.</returns>
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0001BBE7 File Offset: 0x00019DE7
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x0001BC02 File Offset: 0x00019E02
		[XmlElement("types")]
		public Types Types
		{
			get
			{
				if (this.types == null)
				{
					this.types = new Types();
				}
				return this.types;
			}
			set
			{
				this.types = value;
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001BC0B File Offset: 0x00019E0B
		private bool ShouldSerializeTypes()
		{
			return this.Types.HasItems();
		}

		/// <summary>Gets the collection of <see cref="T:System.Web.Services.Description.Message" /> elements contained in the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</summary>
		/// <returns>A collection of message elements contained in the service description.</returns>
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0001BC18 File Offset: 0x00019E18
		[XmlElement("message")]
		public MessageCollection Messages
		{
			get
			{
				if (this.messages == null)
				{
					this.messages = new MessageCollection(this);
				}
				return this.messages;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Web.Services.Description.PortType" /> elements contained in the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</summary>
		/// <returns>A collection of <see cref="T:System.Web.Services.Description.PortType" /> elements contained in the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0001BC34 File Offset: 0x00019E34
		[XmlElement("portType")]
		public PortTypeCollection PortTypes
		{
			get
			{
				if (this.portTypes == null)
				{
					this.portTypes = new PortTypeCollection(this);
				}
				return this.portTypes;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Web.Services.Description.Binding" /> elements contained in the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</summary>
		/// <returns>A collection of binding elements contained in the service description.</returns>
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x0001BC50 File Offset: 0x00019E50
		[XmlElement("binding")]
		public BindingCollection Bindings
		{
			get
			{
				if (this.bindings == null)
				{
					this.bindings = new BindingCollection(this);
				}
				return this.bindings;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Web.Services.Description.Service" /> instances contained in the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</summary>
		/// <returns>A collection of service instances contained in the service description.</returns>
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0001BC6C File Offset: 0x00019E6C
		[XmlElement("service")]
		public ServiceCollection Services
		{
			get
			{
				if (this.services == null)
				{
					this.services = new ServiceCollection(this);
				}
				return this.services;
			}
		}

		/// <summary>Gets or sets the XML targetNamespace attribute of the descriptions tag enclosing a Web Services Description Language (WSDL) file.</summary>
		/// <returns>The URL of the XML Web service described by the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x0001BC88 File Offset: 0x00019E88
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x0001BC90 File Offset: 0x00019E90
		[XmlAttribute("targetNamespace")]
		public string TargetNamespace
		{
			get
			{
				return this.targetNamespace;
			}
			set
			{
				this.targetNamespace = value;
			}
		}

		/// <summary>Gets the schema associated with this <see cref="T:System.Web.Services.Description.ServiceDescription" />.</summary>
		/// <returns>The schema associated with this <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0001BC99 File Offset: 0x00019E99
		public static XmlSchema Schema
		{
			get
			{
				if (ServiceDescription.schema == null)
				{
					ServiceDescription.schema = XmlSchema.Read(new StringReader("<?xml version='1.0' encoding='UTF-8' ?> \n<xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema'\n           xmlns:wsdl='http://schemas.xmlsoap.org/wsdl/'\n           targetNamespace='http://schemas.xmlsoap.org/wsdl/'\n           elementFormDefault='qualified' >\n   \n  <xs:complexType mixed='true' name='tDocumentation' >\n    <xs:sequence>\n      <xs:any minOccurs='0' maxOccurs='unbounded' processContents='lax' />\n    </xs:sequence>\n  </xs:complexType>\n\n  <xs:complexType name='tDocumented' >\n    <xs:annotation>\n      <xs:documentation>\n      This type is extended by  component types to allow them to be documented\n      </xs:documentation>\n    </xs:annotation>\n    <xs:sequence>\n      <xs:element name='documentation' type='wsdl:tDocumentation' minOccurs='0' />\n    </xs:sequence>\n  </xs:complexType>\n <!-- allow extensibility via elements and attributes on all elements swa124 -->\n <xs:complexType name='tExtensibleAttributesDocumented' abstract='true' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tDocumented' >\n        <xs:annotation>\n          <xs:documentation>\n          This type is extended by component types to allow attributes from other namespaces to be added.\n          </xs:documentation>\n        </xs:annotation>\n        <xs:sequence>\n          <xs:any namespace='##other' minOccurs='0' maxOccurs='unbounded' processContents='lax' />\n        </xs:sequence>\n        <xs:anyAttribute namespace='##other' processContents='lax' />   \n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n  <xs:complexType name='tExtensibleDocumented' abstract='true' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tDocumented' >\n        <xs:annotation>\n          <xs:documentation>\n          This type is extended by component types to allow elements from other namespaces to be added.\n          </xs:documentation>\n        </xs:annotation>\n        <xs:sequence>\n          <xs:any namespace='##other' minOccurs='0' maxOccurs='unbounded' processContents='lax' />\n        </xs:sequence>\n        <xs:anyAttribute namespace='##other' processContents='lax' />   \n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n  <!-- original wsdl removed as part of swa124 resolution\n  <xs:complexType name='tExtensibleAttributesDocumented' abstract='true' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tDocumented' >\n        <xs:annotation>\n          <xs:documentation>\n          This type is extended by component types to allow attributes from other namespaces to be added.\n          </xs:documentation>\n        </xs:annotation>\n        <xs:anyAttribute namespace='##other' processContents='lax' />    \n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n\n  <xs:complexType name='tExtensibleDocumented' abstract='true' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tDocumented' >\n        <xs:annotation>\n          <xs:documentation>\n          This type is extended by component types to allow elements from other namespaces to be added.\n          </xs:documentation>\n        </xs:annotation>\n        <xs:sequence>\n          <xs:any namespace='##other' minOccurs='0' maxOccurs='unbounded' processContents='lax' />\n        </xs:sequence>\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n -->\n  <xs:element name='definitions' type='wsdl:tDefinitions' >\n    <xs:key name='message' >\n      <xs:selector xpath='wsdl:message' />\n      <xs:field xpath='@name' />\n    </xs:key>\n    <xs:key name='portType' >\n      <xs:selector xpath='wsdl:portType' />\n      <xs:field xpath='@name' />\n    </xs:key>\n    <xs:key name='binding' >\n      <xs:selector xpath='wsdl:binding' />\n      <xs:field xpath='@name' />\n    </xs:key>\n    <xs:key name='service' >\n      <xs:selector xpath='wsdl:service' />\n      <xs:field xpath='@name' />\n    </xs:key>\n    <xs:key name='import' >\n      <xs:selector xpath='wsdl:import' />\n      <xs:field xpath='@namespace' />\n    </xs:key>\n  </xs:element>\n\n  <xs:group name='anyTopLevelOptionalElement' >\n    <xs:annotation>\n      <xs:documentation>\n      Any top level optional element allowed to appear more then once - any child of definitions element except wsdl:types. Any extensibility element is allowed in any place.\n      </xs:documentation>\n    </xs:annotation>\n    <xs:choice>\n      <xs:element name='import' type='wsdl:tImport' />\n      <xs:element name='types' type='wsdl:tTypes' />                     \n      <xs:element name='message'  type='wsdl:tMessage' >\n        <xs:unique name='part' >\n          <xs:selector xpath='wsdl:part' />\n          <xs:field xpath='@name' />\n        </xs:unique>\n      </xs:element>\n      <xs:element name='portType' type='wsdl:tPortType' />\n      <xs:element name='binding'  type='wsdl:tBinding' />\n      <xs:element name='service'  type='wsdl:tService' >\n        <xs:unique name='port' >\n          <xs:selector xpath='wsdl:port' />\n          <xs:field xpath='@name' />\n        </xs:unique>\n      </xs:element>\n    </xs:choice>\n  </xs:group>\n\n  <xs:complexType name='tDefinitions' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tExtensibleDocumented' >\n        <xs:sequence>\n          <xs:group ref='wsdl:anyTopLevelOptionalElement'  minOccurs='0'   maxOccurs='unbounded' />\n        </xs:sequence>\n        <xs:attribute name='targetNamespace' type='xs:anyURI' use='optional' />\n        <xs:attribute name='name' type='xs:NCName' use='optional' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n   \n  <xs:complexType name='tImport' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tExtensibleAttributesDocumented' >\n        <xs:attribute name='namespace' type='xs:anyURI' use='required' />\n        <xs:attribute name='location' type='xs:anyURI' use='required' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n   \n  <xs:complexType name='tTypes' >\n    <xs:complexContent>   \n      <xs:extension base='wsdl:tExtensibleDocumented' />\n    </xs:complexContent>   \n  </xs:complexType>\n     \n  <xs:complexType name='tMessage' >\n    <xs:complexContent>   \n      <xs:extension base='wsdl:tExtensibleDocumented' >\n        <xs:sequence>\n          <xs:element name='part' type='wsdl:tPart' minOccurs='0' maxOccurs='unbounded' />\n        </xs:sequence>\n        <xs:attribute name='name' type='xs:NCName' use='required' />\n      </xs:extension>\n    </xs:complexContent>   \n  </xs:complexType>\n\n  <xs:complexType name='tPart' >\n    <xs:complexContent>   \n      <xs:extension base='wsdl:tExtensibleAttributesDocumented' >\n        <xs:attribute name='name' type='xs:NCName' use='required' />\n        <xs:attribute name='element' type='xs:QName' use='optional' />\n        <xs:attribute name='type' type='xs:QName' use='optional' />    \n      </xs:extension>\n    </xs:complexContent>   \n  </xs:complexType>\n\n  <xs:complexType name='tPortType' >\n    <xs:complexContent>   \n      <xs:extension base='wsdl:tExtensibleAttributesDocumented' >\n        <xs:sequence>\n          <xs:element name='operation' type='wsdl:tOperation' minOccurs='0' maxOccurs='unbounded' />\n        </xs:sequence>\n        <xs:attribute name='name' type='xs:NCName' use='required' />\n      </xs:extension>\n    </xs:complexContent>   \n  </xs:complexType>\n   \n  <xs:complexType name='tOperation' >\n    <xs:complexContent>   \n      <xs:extension base='wsdl:tExtensibleDocumented' >\n        <xs:sequence>\n          <xs:choice>\n            <xs:group ref='wsdl:request-response-or-one-way-operation' />\n            <xs:group ref='wsdl:solicit-response-or-notification-operation' />\n          </xs:choice>\n        </xs:sequence>\n        <xs:attribute name='name' type='xs:NCName' use='required' />\n        <xs:attribute name='parameterOrder' type='xs:NMTOKENS' use='optional' />\n      </xs:extension>\n    </xs:complexContent>   \n  </xs:complexType>\n    \n  <xs:group name='request-response-or-one-way-operation' >\n    <xs:sequence>\n      <xs:element name='input' type='wsdl:tParam' />\n      <xs:sequence minOccurs='0' >\n        <xs:element name='output' type='wsdl:tParam' />\n        <xs:element name='fault' type='wsdl:tFault' minOccurs='0' maxOccurs='unbounded' />\n      </xs:sequence>\n    </xs:sequence>\n  </xs:group>\n\n  <xs:group name='solicit-response-or-notification-operation' >\n    <xs:sequence>\n      <xs:element name='output' type='wsdl:tParam' />\n      <xs:sequence minOccurs='0' >\n        <xs:element name='input' type='wsdl:tParam' />\n        <xs:element name='fault' type='wsdl:tFault' minOccurs='0' maxOccurs='unbounded' />\n      </xs:sequence>\n    </xs:sequence>\n  </xs:group>\n        \n  <xs:complexType name='tParam' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tExtensibleAttributesDocumented' >\n        <xs:attribute name='name' type='xs:NCName' use='optional' />\n        <xs:attribute name='message' type='xs:QName' use='required' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n\n  <xs:complexType name='tFault' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tExtensibleAttributesDocumented' >\n        <xs:attribute name='name' type='xs:NCName'  use='required' />\n        <xs:attribute name='message' type='xs:QName' use='required' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n     \n  <xs:complexType name='tBinding' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tExtensibleDocumented' >\n        <xs:sequence>\n          <xs:element name='operation' type='wsdl:tBindingOperation' minOccurs='0' maxOccurs='unbounded' />\n        </xs:sequence>\n        <xs:attribute name='name' type='xs:NCName' use='required' />\n        <xs:attribute name='type' type='xs:QName' use='required' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n    \n  <xs:complexType name='tBindingOperationMessage' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tExtensibleDocumented' >\n        <xs:attribute name='name' type='xs:NCName' use='optional' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n  \n  <xs:complexType name='tBindingOperationFault' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tExtensibleDocumented' >\n        <xs:attribute name='name' type='xs:NCName' use='required' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n\n  <xs:complexType name='tBindingOperation' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tExtensibleDocumented' >\n        <xs:sequence>\n          <xs:element name='input' type='wsdl:tBindingOperationMessage' minOccurs='0' />\n          <xs:element name='output' type='wsdl:tBindingOperationMessage' minOccurs='0' />\n          <xs:element name='fault' type='wsdl:tBindingOperationFault' minOccurs='0' maxOccurs='unbounded' />\n        </xs:sequence>\n        <xs:attribute name='name' type='xs:NCName' use='required' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n     \n  <xs:complexType name='tService' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tExtensibleDocumented' >\n        <xs:sequence>\n          <xs:element name='port' type='wsdl:tPort' minOccurs='0' maxOccurs='unbounded' />\n        </xs:sequence>\n        <xs:attribute name='name' type='xs:NCName' use='required' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n     \n  <xs:complexType name='tPort' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tExtensibleDocumented' >\n        <xs:attribute name='name' type='xs:NCName' use='required' />\n        <xs:attribute name='binding' type='xs:QName' use='required' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n\n  <xs:attribute name='arrayType' type='xs:string' />\n  <xs:attribute name='required' type='xs:boolean' />\n  <xs:complexType name='tExtensibilityElement' abstract='true' >\n    <xs:attribute ref='wsdl:required' use='optional' />\n  </xs:complexType>\n\n</xs:schema>"), null);
				}
				return ServiceDescription.schema;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x0001BCBC File Offset: 0x00019EBC
		internal static XmlSchema SoapEncodingSchema
		{
			get
			{
				if (ServiceDescription.soapEncodingSchema == null)
				{
					ServiceDescription.soapEncodingSchema = XmlSchema.Read(new StringReader("<?xml version='1.0' encoding='UTF-8' ?>\n<xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema'\n           xmlns:tns='http://schemas.xmlsoap.org/soap/encoding/'\n           targetNamespace='http://schemas.xmlsoap.org/soap/encoding/' >\n        \n <xs:attribute name='root' >\n   <xs:simpleType>\n     <xs:restriction base='xs:boolean'>\n       <xs:pattern value='0|1' />\n     </xs:restriction>\n   </xs:simpleType>\n </xs:attribute>\n\n  <xs:attributeGroup name='commonAttributes' >\n    <xs:attribute name='id' type='xs:ID' />\n    <xs:attribute name='href' type='xs:anyURI' />\n    <xs:anyAttribute namespace='##other' processContents='lax' />\n  </xs:attributeGroup>\n   \n  <xs:simpleType name='arrayCoordinate' >\n    <xs:restriction base='xs:string' />\n  </xs:simpleType>\n          \n  <xs:attribute name='arrayType' type='xs:string' />\n  <xs:attribute name='offset' type='tns:arrayCoordinate' />\n  \n  <xs:attributeGroup name='arrayAttributes' >\n    <xs:attribute ref='tns:arrayType' />\n    <xs:attribute ref='tns:offset' />\n  </xs:attributeGroup>    \n  \n  <xs:attribute name='position' type='tns:arrayCoordinate' /> \n  \n  <xs:attributeGroup name='arrayMemberAttributes' >\n    <xs:attribute ref='tns:position' />\n  </xs:attributeGroup>    \n\n  <xs:group name='Array' >\n    <xs:sequence>\n      <xs:any namespace='##any' minOccurs='0' maxOccurs='unbounded' processContents='lax' />\n    </xs:sequence>\n  </xs:group>\n\n  <xs:element name='Array' type='tns:Array' />\n  <xs:complexType name='Array' >\n    <xs:group ref='tns:Array' minOccurs='0' />\n    <xs:attributeGroup ref='tns:arrayAttributes' />\n    <xs:attributeGroup ref='tns:commonAttributes' />\n  </xs:complexType> \n  <xs:element name='Struct' type='tns:Struct' />\n  <xs:group name='Struct' >\n    <xs:sequence>\n      <xs:any namespace='##any' minOccurs='0' maxOccurs='unbounded' processContents='lax' />\n    </xs:sequence>\n  </xs:group>\n\n  <xs:complexType name='Struct' >\n    <xs:group ref='tns:Struct' minOccurs='0' />\n    <xs:attributeGroup ref='tns:commonAttributes'/>\n  </xs:complexType> \n  \n  <xs:simpleType name='base64' >\n    <xs:restriction base='xs:base64Binary' />\n  </xs:simpleType>\n\n  <xs:element name='duration' type='tns:duration' />\n  <xs:complexType name='duration' >\n    <xs:simpleContent>\n      <xs:extension base='xs:duration' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='dateTime' type='tns:dateTime' />\n  <xs:complexType name='dateTime' >\n    <xs:simpleContent>\n      <xs:extension base='xs:dateTime' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n\n\n  <xs:element name='NOTATION' type='tns:NOTATION' />\n  <xs:complexType name='NOTATION' >\n    <xs:simpleContent>\n      <xs:extension base='xs:QName' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n  \n\n  <xs:element name='time' type='tns:time' />\n  <xs:complexType name='time' >\n    <xs:simpleContent>\n      <xs:extension base='xs:time' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='date' type='tns:date' />\n  <xs:complexType name='date' >\n    <xs:simpleContent>\n      <xs:extension base='xs:date' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='gYearMonth' type='tns:gYearMonth' />\n  <xs:complexType name='gYearMonth' >\n    <xs:simpleContent>\n      <xs:extension base='xs:gYearMonth' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='gYear' type='tns:gYear' />\n  <xs:complexType name='gYear' >\n    <xs:simpleContent>\n      <xs:extension base='xs:gYear' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='gMonthDay' type='tns:gMonthDay' />\n  <xs:complexType name='gMonthDay' >\n    <xs:simpleContent>\n      <xs:extension base='xs:gMonthDay' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='gDay' type='tns:gDay' />\n  <xs:complexType name='gDay' >\n    <xs:simpleContent>\n      <xs:extension base='xs:gDay' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='gMonth' type='tns:gMonth' />\n  <xs:complexType name='gMonth' >\n    <xs:simpleContent>\n      <xs:extension base='xs:gMonth' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n  \n  <xs:element name='boolean' type='tns:boolean' />\n  <xs:complexType name='boolean' >\n    <xs:simpleContent>\n      <xs:extension base='xs:boolean' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='base64Binary' type='tns:base64Binary' />\n  <xs:complexType name='base64Binary' >\n    <xs:simpleContent>\n      <xs:extension base='xs:base64Binary' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='hexBinary' type='tns:hexBinary' />\n  <xs:complexType name='hexBinary' >\n    <xs:simpleContent>\n     <xs:extension base='xs:hexBinary' >\n       <xs:attributeGroup ref='tns:commonAttributes' />\n     </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='float' type='tns:float' />\n  <xs:complexType name='float' >\n    <xs:simpleContent>\n      <xs:extension base='xs:float' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='double' type='tns:double' />\n  <xs:complexType name='double' >\n    <xs:simpleContent>\n      <xs:extension base='xs:double' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='anyURI' type='tns:anyURI' />\n  <xs:complexType name='anyURI' >\n    <xs:simpleContent>\n      <xs:extension base='xs:anyURI' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='QName' type='tns:QName' />\n  <xs:complexType name='QName' >\n    <xs:simpleContent>\n      <xs:extension base='xs:QName' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  \n  <xs:element name='string' type='tns:string' />\n  <xs:complexType name='string' >\n    <xs:simpleContent>\n      <xs:extension base='xs:string' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='normalizedString' type='tns:normalizedString' />\n  <xs:complexType name='normalizedString' >\n    <xs:simpleContent>\n      <xs:extension base='xs:normalizedString' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='token' type='tns:token' />\n  <xs:complexType name='token' >\n    <xs:simpleContent>\n      <xs:extension base='xs:token' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='language' type='tns:language' />\n  <xs:complexType name='language' >\n    <xs:simpleContent>\n      <xs:extension base='xs:language' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='Name' type='tns:Name' />\n  <xs:complexType name='Name' >\n    <xs:simpleContent>\n      <xs:extension base='xs:Name' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='NMTOKEN' type='tns:NMTOKEN' />\n  <xs:complexType name='NMTOKEN' >\n    <xs:simpleContent>\n      <xs:extension base='xs:NMTOKEN' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='NCName' type='tns:NCName' />\n  <xs:complexType name='NCName' >\n    <xs:simpleContent>\n      <xs:extension base='xs:NCName' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='NMTOKENS' type='tns:NMTOKENS' />\n  <xs:complexType name='NMTOKENS' >\n    <xs:simpleContent>\n      <xs:extension base='xs:NMTOKENS' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='ID' type='tns:ID' />\n  <xs:complexType name='ID' >\n    <xs:simpleContent>\n      <xs:extension base='xs:ID' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='IDREF' type='tns:IDREF' />\n  <xs:complexType name='IDREF' >\n    <xs:simpleContent>\n      <xs:extension base='xs:IDREF' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='ENTITY' type='tns:ENTITY' />\n  <xs:complexType name='ENTITY' >\n    <xs:simpleContent>\n      <xs:extension base='xs:ENTITY' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='IDREFS' type='tns:IDREFS' />\n  <xs:complexType name='IDREFS' >\n    <xs:simpleContent>\n      <xs:extension base='xs:IDREFS' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='ENTITIES' type='tns:ENTITIES' />\n  <xs:complexType name='ENTITIES' >\n    <xs:simpleContent>\n      <xs:extension base='xs:ENTITIES' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='decimal' type='tns:decimal' />\n  <xs:complexType name='decimal' >\n    <xs:simpleContent>\n      <xs:extension base='xs:decimal' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='integer' type='tns:integer' />\n  <xs:complexType name='integer' >\n    <xs:simpleContent>\n      <xs:extension base='xs:integer' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='nonPositiveInteger' type='tns:nonPositiveInteger' />\n  <xs:complexType name='nonPositiveInteger' >\n    <xs:simpleContent>\n      <xs:extension base='xs:nonPositiveInteger' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='negativeInteger' type='tns:negativeInteger' />\n  <xs:complexType name='negativeInteger' >\n    <xs:simpleContent>\n      <xs:extension base='xs:negativeInteger' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='long' type='tns:long' />\n  <xs:complexType name='long' >\n    <xs:simpleContent>\n      <xs:extension base='xs:long' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='int' type='tns:int' />\n  <xs:complexType name='int' >\n    <xs:simpleContent>\n      <xs:extension base='xs:int' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='short' type='tns:short' />\n  <xs:complexType name='short' >\n    <xs:simpleContent>\n      <xs:extension base='xs:short' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='byte' type='tns:byte' />\n  <xs:complexType name='byte' >\n    <xs:simpleContent>\n      <xs:extension base='xs:byte' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='nonNegativeInteger' type='tns:nonNegativeInteger' />\n  <xs:complexType name='nonNegativeInteger' >\n    <xs:simpleContent>\n      <xs:extension base='xs:nonNegativeInteger' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='unsignedLong' type='tns:unsignedLong' />\n  <xs:complexType name='unsignedLong' >\n    <xs:simpleContent>\n      <xs:extension base='xs:unsignedLong' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='unsignedInt' type='tns:unsignedInt' />\n  <xs:complexType name='unsignedInt' >\n    <xs:simpleContent>\n      <xs:extension base='xs:unsignedInt' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='unsignedShort' type='tns:unsignedShort' />\n  <xs:complexType name='unsignedShort' >\n    <xs:simpleContent>\n      <xs:extension base='xs:unsignedShort' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='unsignedByte' type='tns:unsignedByte' />\n  <xs:complexType name='unsignedByte' >\n    <xs:simpleContent>\n      <xs:extension base='xs:unsignedByte' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='positiveInteger' type='tns:positiveInteger' />\n  <xs:complexType name='positiveInteger' >\n    <xs:simpleContent>\n      <xs:extension base='xs:positiveInteger' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='anyType' />\n</xs:schema>"), null);
				}
				return ServiceDescription.soapEncodingSchema;
			}
		}

		/// <summary>Gets a <see cref="T:System.Collections.Specialized.StringCollection" /> that contains any validation warnings that were generated during a call to <see cref="M:System.Web.Services.Description.ServiceDescription.Read(System.IO.Stream,System.Boolean)" />, <see cref="M:System.Web.Services.Description.ServiceDescription.Read(System.IO.TextReader,System.Boolean)" />, <see cref="M:System.Web.Services.Description.ServiceDescription.Read(System.String,System.Boolean)" />, or <see cref="M:System.Web.Services.Description.ServiceDescription.Read(System.Xml.XmlReader,System.Boolean)" /> with the <paramref name="validate" /> parameter set to true.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> that contains any validation warnings that were generated during a call to <see cref="M:System.Web.Services.Description.ServiceDescription.Read(System.IO.Stream,System.Boolean)" />, <see cref="M:System.Web.Services.Description.ServiceDescription.Read(System.IO.TextReader,System.Boolean)" />, <see cref="M:System.Web.Services.Description.ServiceDescription.Read(System.String,System.Boolean)" />, or <see cref="M:System.Web.Services.Description.ServiceDescription.Read(System.Xml.XmlReader,System.Boolean)" /> with the <paramref name="validate" /> parameter set to true.</returns>
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x0001BCDF File Offset: 0x00019EDF
		[XmlIgnore]
		public StringCollection ValidationWarnings
		{
			get
			{
				if (this.validationWarnings == null)
				{
					this.validationWarnings = new StringCollection();
				}
				return this.validationWarnings;
			}
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001BCFA File Offset: 0x00019EFA
		internal void SetWarnings(StringCollection warnings)
		{
			this.validationWarnings = warnings;
		}

		/// <summary>Gets the XML serializer used to serialize and deserialize between a <see cref="T:System.Web.Services.Description.ServiceDescription" /> object and a Web Services Description Language (WSDL) document.</summary>
		/// <returns>The XML serializer used to serialize and deserialize between a <see cref="T:System.Web.Services.Description.ServiceDescription" /> object and a Web Services Description Language (WSDL) document.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x0001BD04 File Offset: 0x00019F04
		[XmlIgnore]
		public static XmlSerializer Serializer
		{
			get
			{
				if (ServiceDescription.serializer == null)
				{
					WebServicesSection webServicesSection = WebServicesSection.Current;
					XmlAttributeOverrides xmlAttributeOverrides = new XmlAttributeOverrides();
					XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
					xmlSerializerNamespaces.Add("s", "http://www.w3.org/2001/XMLSchema");
					WebServicesSection.LoadXmlFormatExtensions(webServicesSection.GetAllFormatExtensionTypes(), xmlAttributeOverrides, xmlSerializerNamespaces);
					ServiceDescription.namespaces = xmlSerializerNamespaces;
					if (webServicesSection.ServiceDescriptionExtended)
					{
						ServiceDescription.serializer = new XmlSerializer(typeof(ServiceDescription), xmlAttributeOverrides);
					}
					else
					{
						ServiceDescription.serializer = new ServiceDescription.ServiceDescriptionSerializer();
					}
					ServiceDescription.serializer.UnknownElement += RuntimeUtils.OnUnknownElement;
				}
				return ServiceDescription.serializer;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0001BD8F File Offset: 0x00019F8F
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x0001BD97 File Offset: 0x00019F97
		internal string AppSettingBaseUrl
		{
			get
			{
				return this.appSettingBaseUrl;
			}
			set
			{
				this.appSettingBaseUrl = value;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0001BDA0 File Offset: 0x00019FA0
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x0001BDA8 File Offset: 0x00019FA8
		internal string AppSettingUrlKey
		{
			get
			{
				return this.appSettingUrlKey;
			}
			set
			{
				this.appSettingUrlKey = value;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0001BDB1 File Offset: 0x00019FB1
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x0001BDB9 File Offset: 0x00019FB9
		internal ServiceDescription Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> class by directly loading the XML from a <see cref="T:System.IO.TextReader" />.</summary>
		/// <returns>An instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
		/// <param name="textReader">A <see cref="T:System.IO.TextReader" /> instance, passed by reference, which contains the text to be read. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600062B RID: 1579 RVA: 0x0001BDC2 File Offset: 0x00019FC2
		public static ServiceDescription Read(TextReader textReader)
		{
			return ServiceDescription.Read(textReader, false);
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> class by directly loading the XML from a <see cref="T:System.IO.Stream" /> instance.</summary>
		/// <returns>An instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
		/// <param name="stream">A <see cref="T:System.IO.Stream" />, passed by reference, which contains the bytes to be read.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600062C RID: 1580 RVA: 0x0001BDCB File Offset: 0x00019FCB
		public static ServiceDescription Read(Stream stream)
		{
			return ServiceDescription.Read(stream, false);
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> class by directly loading the XML from an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>An instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
		/// <param name="reader">An <see cref="T:System.Xml.XmlReader" />, passed by reference, which contains the XML data to be read. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600062D RID: 1581 RVA: 0x0001BDD4 File Offset: 0x00019FD4
		public static ServiceDescription Read(XmlReader reader)
		{
			return ServiceDescription.Read(reader, false);
		}

		/// <summary>Initializes an instance of a <see cref="T:System.Web.Services.Description.ServiceDescription" /> object by directly loading the XML from the specified file.</summary>
		/// <returns>An instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
		/// <param name="fileName">The path to the file to be read. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600062E RID: 1582 RVA: 0x0001BDDD File Offset: 0x00019FDD
		public static ServiceDescription Read(string fileName)
		{
			return ServiceDescription.Read(fileName, false);
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> class by directly loading the XML from a <see cref="T:System.IO.TextReader" />.</summary>
		/// <returns>An instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
		/// <param name="textReader">A <see cref="T:System.IO.TextReader" /> instance, passed by reference, which contains the text to be read. </param>
		/// <param name="validate">A <see cref="T:System.Boolean" /> that indicates whether the XML should be validated against the schema specified by <see cref="P:System.Web.Services.Description.ServiceDescription.Schema" />.</param>
		// Token: 0x0600062F RID: 1583 RVA: 0x0001BDE6 File Offset: 0x00019FE6
		public static ServiceDescription Read(TextReader textReader, bool validate)
		{
			return ServiceDescription.Read(new XmlTextReader(textReader)
			{
				WhitespaceHandling = WhitespaceHandling.Significant,
				XmlResolver = null,
				DtdProcessing = DtdProcessing.Prohibit
			}, validate);
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> class by directly loading the XML from a <see cref="T:System.IO.Stream" /> instance.</summary>
		/// <returns>An instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
		/// <param name="stream">A <see cref="T:System.IO.Stream" />, passed by reference, which contains the bytes to be read. </param>
		/// <param name="validate">A <see cref="T:System.Boolean" /> that indicates whether the XML should be validated against the schema specified by <see cref="P:System.Web.Services.Description.ServiceDescription.Schema" />.</param>
		// Token: 0x06000630 RID: 1584 RVA: 0x0001BE09 File Offset: 0x0001A009
		public static ServiceDescription Read(Stream stream, bool validate)
		{
			return ServiceDescription.Read(new XmlTextReader(stream)
			{
				WhitespaceHandling = WhitespaceHandling.Significant,
				XmlResolver = null,
				DtdProcessing = DtdProcessing.Prohibit
			}, validate);
		}

		/// <summary>Initializes an instance of a <see cref="T:System.Web.Services.Description.ServiceDescription" /> object by directly loading the XML from the specified file.</summary>
		/// <returns>An instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
		/// <param name="fileName">The path to the file to be read. </param>
		/// <param name="validate">A <see cref="T:System.Boolean" /> that indicates whether the XML should be validated against the schema specified by <see cref="P:System.Web.Services.Description.ServiceDescription.Schema" />.</param>
		// Token: 0x06000631 RID: 1585 RVA: 0x0001BE2C File Offset: 0x0001A02C
		public static ServiceDescription Read(string fileName, bool validate)
		{
			StreamReader streamReader = new StreamReader(fileName, Encoding.Default, true);
			ServiceDescription serviceDescription;
			try
			{
				serviceDescription = ServiceDescription.Read(streamReader, validate);
			}
			finally
			{
				streamReader.Close();
			}
			return serviceDescription;
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> class by directly loading the XML from an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>An instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
		/// <param name="reader">An <see cref="T:System.Xml.XmlReader" />, passed by reference, which contains the XML data to be read. </param>
		/// <param name="validate">A <see cref="T:System.Boolean" /> that indicates whether the XML should be validated against the schema specified by <see cref="P:System.Web.Services.Description.ServiceDescription.Schema" />.</param>
		// Token: 0x06000632 RID: 1586 RVA: 0x0001BE68 File Offset: 0x0001A068
		public static ServiceDescription Read(XmlReader reader, bool validate)
		{
			if (validate)
			{
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				xmlReaderSettings.ValidationType = ValidationType.Schema;
				xmlReaderSettings.ValidationFlags = XmlSchemaValidationFlags.ProcessIdentityConstraints;
				xmlReaderSettings.Schemas.Add(ServiceDescription.Schema);
				xmlReaderSettings.Schemas.Add(SoapBinding.Schema);
				xmlReaderSettings.ValidationEventHandler += ServiceDescription.InstanceValidation;
				ServiceDescription.warnings.Clear();
				XmlReader xmlReader = XmlReader.Create(reader, xmlReaderSettings);
				if (reader.ReadState != ReadState.Initial)
				{
					xmlReader.Read();
				}
				ServiceDescription serviceDescription = (ServiceDescription)ServiceDescription.Serializer.Deserialize(xmlReader);
				serviceDescription.SetWarnings(ServiceDescription.warnings);
				return serviceDescription;
			}
			return (ServiceDescription)ServiceDescription.Serializer.Deserialize(reader);
		}

		/// <summary>Gets a value that indicates whether an <see cref="T:System.Xml.XmlReader" /> represents a valid Web Services Description Language (WSDL) file that can be parsed.</summary>
		/// <returns>true if the <see cref="T:System.Xml.Serialization.XmlSerializer" /> can recognize the node on which the <see cref="T:System.Xml.XmlReader" /> is positioned; otherwise false.</returns>
		/// <param name="reader">An <see cref="T:System.Xml.XmlReader" /></param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000633 RID: 1587 RVA: 0x0001BF10 File Offset: 0x0001A110
		public static bool CanRead(XmlReader reader)
		{
			return ServiceDescription.Serializer.CanDeserialize(reader);
		}

		/// <summary>Writes out the <see cref="T:System.Web.Services.Description.ServiceDescription" /> as a Web Services Description Language (WSDL) file to the specified path.</summary>
		/// <param name="fileName">The path to which the WSDL file is written. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000634 RID: 1588 RVA: 0x0001BF20 File Offset: 0x0001A120
		public void Write(string fileName)
		{
			StreamWriter streamWriter = new StreamWriter(fileName);
			try
			{
				this.Write(streamWriter);
			}
			finally
			{
				streamWriter.Close();
			}
		}

		/// <summary>Writes out the <see cref="T:System.Web.Services.Description.ServiceDescription" /> as a Web Services Description Language (WSDL) file to the <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> that contains the WSDL file produced. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000635 RID: 1589 RVA: 0x0001BF54 File Offset: 0x0001A154
		public void Write(TextWriter writer)
		{
			this.Write(new XmlTextWriter(writer)
			{
				Formatting = Formatting.Indented,
				Indentation = 2
			});
		}

		/// <summary>Writes out the <see cref="T:System.Web.Services.Description.ServiceDescription" /> to the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" />, passed by reference, which contains the Web Services Description Language (WSDL) file produced. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000636 RID: 1590 RVA: 0x0001BF80 File Offset: 0x0001A180
		public void Write(Stream stream)
		{
			TextWriter textWriter = new StreamWriter(stream);
			this.Write(textWriter);
			textWriter.Flush();
		}

		/// <summary>Writes out the <see cref="T:System.Web.Services.Description.ServiceDescription" /> to the <see cref="T:System.Xml.XmlWriter" /> as a Web Services Description Language (WSDL) file.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" />, passed by reference, which contains the WSDL file produced. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000637 RID: 1591 RVA: 0x0001BFA4 File Offset: 0x0001A1A4
		public void Write(XmlWriter writer)
		{
			XmlSerializer xmlSerializer = ServiceDescription.Serializer;
			XmlSerializerNamespaces xmlSerializerNamespaces;
			if (base.Namespaces == null || base.Namespaces.Count == 0)
			{
				xmlSerializerNamespaces = new XmlSerializerNamespaces(ServiceDescription.namespaces);
				xmlSerializerNamespaces.Add("wsdl", "http://schemas.xmlsoap.org/wsdl/");
				if (this.TargetNamespace != null && this.TargetNamespace.Length != 0)
				{
					xmlSerializerNamespaces.Add("tns", this.TargetNamespace);
				}
				for (int i = 0; i < this.Types.Schemas.Count; i++)
				{
					string text = this.Types.Schemas[i].TargetNamespace;
					if (text != null && text.Length > 0 && text != this.TargetNamespace && text != "http://schemas.xmlsoap.org/wsdl/")
					{
						xmlSerializerNamespaces.Add("s" + i.ToString(CultureInfo.InvariantCulture), text);
					}
				}
				for (int j = 0; j < this.Imports.Count; j++)
				{
					Import import = this.Imports[j];
					if (import.Namespace.Length > 0)
					{
						xmlSerializerNamespaces.Add("i" + j.ToString(CultureInfo.InvariantCulture), import.Namespace);
					}
				}
			}
			else
			{
				xmlSerializerNamespaces = base.Namespaces;
			}
			xmlSerializer.Serialize(writer, this, xmlSerializerNamespaces);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001C0F4 File Offset: 0x0001A2F4
		internal static WsiProfiles GetConformanceClaims(XmlElement documentation)
		{
			if (documentation == null)
			{
				return WsiProfiles.None;
			}
			WsiProfiles wsiProfiles = WsiProfiles.None;
			XmlNode nextSibling;
			for (XmlNode xmlNode = documentation.FirstChild; xmlNode != null; xmlNode = nextSibling)
			{
				nextSibling = xmlNode.NextSibling;
				if (xmlNode is XmlElement)
				{
					XmlElement xmlElement = (XmlElement)xmlNode;
					if (xmlElement.LocalName == "Claim" && xmlElement.NamespaceURI == "http://ws-i.org/schemas/conformanceClaim/" && "http://ws-i.org/profiles/basic/1.1" == xmlElement.GetAttribute("conformsTo"))
					{
						wsiProfiles |= WsiProfiles.BasicProfile1_1;
					}
				}
			}
			return wsiProfiles;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001C16C File Offset: 0x0001A36C
		internal static void AddConformanceClaims(XmlElement documentation, WsiProfiles claims)
		{
			claims &= WsiProfiles.BasicProfile1_1;
			if (claims == WsiProfiles.None)
			{
				return;
			}
			WsiProfiles conformanceClaims = ServiceDescription.GetConformanceClaims(documentation);
			claims &= ~conformanceClaims;
			if (claims == WsiProfiles.None)
			{
				return;
			}
			XmlDocument ownerDocument = documentation.OwnerDocument;
			if ((claims & WsiProfiles.BasicProfile1_1) != WsiProfiles.None)
			{
				XmlElement xmlElement = ownerDocument.CreateElement("wsi", "Claim", "http://ws-i.org/schemas/conformanceClaim/");
				xmlElement.SetAttribute("conformsTo", "http://ws-i.org/profiles/basic/1.1");
				documentation.InsertBefore(xmlElement, null);
			}
		}

		/// <summary>The XML namespace in which the <see cref="T:System.Web.Services.Description.ServiceDescription" /> class is defined ("http://schemas.xmlsoap.org/wsdl/"). This field is constant.</summary>
		// Token: 0x040003C9 RID: 969
		public const string Namespace = "http://schemas.xmlsoap.org/wsdl/";

		// Token: 0x040003CA RID: 970
		internal const string Prefix = "wsdl";

		// Token: 0x040003CB RID: 971
		private Types types;

		// Token: 0x040003CC RID: 972
		private ImportCollection imports;

		// Token: 0x040003CD RID: 973
		private MessageCollection messages;

		// Token: 0x040003CE RID: 974
		private PortTypeCollection portTypes;

		// Token: 0x040003CF RID: 975
		private BindingCollection bindings;

		// Token: 0x040003D0 RID: 976
		private ServiceCollection services;

		// Token: 0x040003D1 RID: 977
		private string targetNamespace;

		// Token: 0x040003D2 RID: 978
		private ServiceDescriptionFormatExtensionCollection extensions;

		// Token: 0x040003D3 RID: 979
		private ServiceDescriptionCollection parent;

		// Token: 0x040003D4 RID: 980
		private string appSettingUrlKey;

		// Token: 0x040003D5 RID: 981
		private string appSettingBaseUrl;

		// Token: 0x040003D6 RID: 982
		private string retrievalUrl;

		// Token: 0x040003D7 RID: 983
		private static XmlSerializer serializer;

		// Token: 0x040003D8 RID: 984
		private static XmlSerializerNamespaces namespaces;

		// Token: 0x040003D9 RID: 985
		private const WsiProfiles SupportedClaims = WsiProfiles.BasicProfile1_1;

		// Token: 0x040003DA RID: 986
		private static XmlSchema schema = null;

		// Token: 0x040003DB RID: 987
		private static XmlSchema soapEncodingSchema = null;

		// Token: 0x040003DC RID: 988
		private StringCollection validationWarnings;

		// Token: 0x040003DD RID: 989
		private static StringCollection warnings = new StringCollection();

		// Token: 0x040003DE RID: 990
		private ServiceDescription next;

		// Token: 0x020000E7 RID: 231
		internal class ServiceDescriptionSerializer : XmlSerializer
		{
			// Token: 0x0600063C RID: 1596 RVA: 0x0001C1EE File Offset: 0x0001A3EE
			protected override XmlSerializationReader CreateReader()
			{
				return new ServiceDescriptionSerializationReader();
			}

			// Token: 0x0600063D RID: 1597 RVA: 0x0001C1F5 File Offset: 0x0001A3F5
			protected override XmlSerializationWriter CreateWriter()
			{
				return new ServiceDescriptionSerializationWriter();
			}

			// Token: 0x0600063E RID: 1598 RVA: 0x0001C1FC File Offset: 0x0001A3FC
			public override bool CanDeserialize(XmlReader xmlReader)
			{
				return xmlReader.IsStartElement("definitions", "http://schemas.xmlsoap.org/wsdl/");
			}

			// Token: 0x0600063F RID: 1599 RVA: 0x0001C20E File Offset: 0x0001A40E
			protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
			{
				((ServiceDescriptionSerializationWriter)writer).Write125_definitions(objectToSerialize);
			}

			// Token: 0x06000640 RID: 1600 RVA: 0x0001C21C File Offset: 0x0001A41C
			protected override object Deserialize(XmlSerializationReader reader)
			{
				return ((ServiceDescriptionSerializationReader)reader).Read125_definitions();
			}
		}
	}
}
