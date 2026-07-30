using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>The <see cref="T:System.Web.Services.Description.WebReferenceOptions" /> class represents code generation options specified in an XML text file.</summary>
	// Token: 0x02000135 RID: 309
	[XmlType("webReferenceOptions", Namespace = "http://microsoft.com/webReference/")]
	[XmlRoot("webReferenceOptions", Namespace = "http://microsoft.com/webReference/")]
	public class WebReferenceOptions
	{
		/// <summary>Gets or sets the <see cref="T:System.Xml.Serialization.CodeGenerationOptions" /> associated with this <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.CodeGenerationOptions" /> associated with this <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</returns>
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00040F28 File Offset: 0x0003F128
		// (set) Token: 0x06000966 RID: 2406 RVA: 0x00040F30 File Offset: 0x0003F130
		[XmlElement("codeGenerationOptions")]
		[DefaultValue(CodeGenerationOptions.GenerateOldAsync)]
		public CodeGenerationOptions CodeGenerationOptions
		{
			get
			{
				return this.codeGenerationOptions;
			}
			set
			{
				this.codeGenerationOptions = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Collections.Specialized.StringCollection" /> that represents the schema importer extensions associated with this <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> that represents the schema importer extensions associated with this <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</returns>
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00040F39 File Offset: 0x0003F139
		[XmlArray("schemaImporterExtensions")]
		[XmlArrayItem("type")]
		public StringCollection SchemaImporterExtensions
		{
			get
			{
				if (this.schemaImporterExtensions == null)
				{
					this.schemaImporterExtensions = new StringCollection();
				}
				return this.schemaImporterExtensions;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Services.Description.ServiceDescriptionImportStyle" /> associated with this <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Description.ServiceDescriptionImportStyle" /> associated with this <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</returns>
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x00040F54 File Offset: 0x0003F154
		// (set) Token: 0x06000969 RID: 2409 RVA: 0x00040F5C File Offset: 0x0003F15C
		[XmlElement("style")]
		[DefaultValue(ServiceDescriptionImportStyle.Client)]
		public ServiceDescriptionImportStyle Style
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

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that indicates whether verbose warning messages are to be generated during compilation of a client proxy or a server stub.</summary>
		/// <returns>true if verbose warning messages are to be generated during compilation of a client proxy or a server stub; otherwise, false.</returns>
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x00040F65 File Offset: 0x0003F165
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x00040F6D File Offset: 0x0003F16D
		[XmlElement("verbose")]
		public bool Verbose
		{
			get
			{
				return this.verbose;
			}
			set
			{
				this.verbose = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchema" /> associated with this <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchema" /> associated with this <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</returns>
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00040F76 File Offset: 0x0003F176
		public static XmlSchema Schema
		{
			get
			{
				if (WebReferenceOptions.schema == null)
				{
					WebReferenceOptions.schema = XmlSchema.Read(new StringReader("<?xml version='1.0' encoding='UTF-8' ?>\n<xs:schema xmlns:tns='http://microsoft.com/webReference/' elementFormDefault='qualified' targetNamespace='http://microsoft.com/webReference/' xmlns:xs='http://www.w3.org/2001/XMLSchema'>\n  <xs:simpleType name='options'>\n    <xs:list>\n      <xs:simpleType>\n        <xs:restriction base='xs:string'>\n          <xs:enumeration value='properties' />\n          <xs:enumeration value='newAsync' />\n          <xs:enumeration value='oldAsync' />\n          <xs:enumeration value='order' />\n          <xs:enumeration value='enableDataBinding' />\n        </xs:restriction>\n      </xs:simpleType>\n    </xs:list>\n  </xs:simpleType>\n  <xs:simpleType name='style'>\n    <xs:restriction base='xs:string'>\n      <xs:enumeration value='client' />\n      <xs:enumeration value='server' />\n      <xs:enumeration value='serverInterface' />\n    </xs:restriction>\n  </xs:simpleType>\n  <xs:complexType name='webReferenceOptions'>\n    <xs:all>\n      <xs:element minOccurs='0' default='oldAsync' name='codeGenerationOptions' type='tns:options' />\n      <xs:element minOccurs='0' default='client' name='style' type='tns:style' />\n      <xs:element minOccurs='0' default='false' name='verbose' type='xs:boolean' />\n      <xs:element minOccurs='0' name='schemaImporterExtensions'>\n        <xs:complexType>\n          <xs:sequence>\n            <xs:element minOccurs='0' maxOccurs='unbounded' name='type' type='xs:string' />\n          </xs:sequence>\n        </xs:complexType>\n      </xs:element>\n    </xs:all>\n  </xs:complexType>\n  <xs:element name='webReferenceOptions' type='tns:webReferenceOptions' />\n  <xs:complexType name='wsdlParameters'>\n    <xs:all>\n      <xs:element minOccurs='0' name='appSettingBaseUrl' type='xs:string' />\n      <xs:element minOccurs='0' name='appSettingUrlKey' type='xs:string' />\n      <xs:element minOccurs='0' name='domain' type='xs:string' />\n      <xs:element minOccurs='0' name='out' type='xs:string' />\n      <xs:element minOccurs='0' name='password' type='xs:string' />\n      <xs:element minOccurs='0' name='proxy' type='xs:string' />\n      <xs:element minOccurs='0' name='proxydomain' type='xs:string' />\n      <xs:element minOccurs='0' name='proxypassword' type='xs:string' />\n      <xs:element minOccurs='0' name='proxyusername' type='xs:string' />\n      <xs:element minOccurs='0' name='username' type='xs:string' />\n      <xs:element minOccurs='0' name='namespace' type='xs:string' />\n      <xs:element minOccurs='0' name='language' type='xs:string' />\n      <xs:element minOccurs='0' name='protocol' type='xs:string' />\n      <xs:element minOccurs='0' name='nologo' type='xs:boolean' />\n      <xs:element minOccurs='0' name='parsableerrors' type='xs:boolean' />\n      <xs:element minOccurs='0' name='sharetypes' type='xs:boolean' />\n      <xs:element minOccurs='0' name='webReferenceOptions' type='tns:webReferenceOptions' />\n      <xs:element minOccurs='0' name='documents'>\n        <xs:complexType>\n          <xs:sequence>\n            <xs:element minOccurs='0' maxOccurs='unbounded' name='document' type='xs:string' />\n          </xs:sequence>\n        </xs:complexType>\n      </xs:element>\n    </xs:all>\n  </xs:complexType>\n  <xs:element name='wsdlParameters' type='tns:wsdlParameters' />\n</xs:schema>"), null);
				}
				return WebReferenceOptions.schema;
			}
		}

		/// <summary>Returns a new instance of <see cref="T:System.Web.Services.Description.WebReferenceOptions" /> based on the code generation options described in the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <returns>A new instance of <see cref="T:System.Web.Services.Description.WebReferenceOptions" /> based on the code generation options described in the specified <see cref="T:System.IO.TextReader" />.</returns>
		/// <param name="reader">The <see cref="T:System.IO.TextReader" /> that contains the code generation options.</param>
		/// <param name="validationEventHandler">The <see cref="T:System.Xml.Schema.ValidationEventHandler" /> to be associated with the new instance of <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</param>
		// Token: 0x0600096D RID: 2413 RVA: 0x00040F99 File Offset: 0x0003F199
		public static WebReferenceOptions Read(TextReader reader, ValidationEventHandler validationEventHandler)
		{
			return WebReferenceOptions.Read(new XmlTextReader(reader)
			{
				XmlResolver = null,
				DtdProcessing = DtdProcessing.Prohibit
			}, validationEventHandler);
		}

		/// <summary>Returns a new instance of <see cref="T:System.Web.Services.Description.WebReferenceOptions" /> based on the code generation options described in the specified stream.</summary>
		/// <returns>A new instance of <see cref="T:System.Web.Services.Description.WebReferenceOptions" /> based on the code generation options described in the specified stream.</returns>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> that contains the code generation options.</param>
		/// <param name="validationEventHandler">The <see cref="T:System.Xml.Schema.ValidationEventHandler" /> to be associated with the new instance of <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</param>
		// Token: 0x0600096E RID: 2414 RVA: 0x00040FB5 File Offset: 0x0003F1B5
		public static WebReferenceOptions Read(Stream stream, ValidationEventHandler validationEventHandler)
		{
			return WebReferenceOptions.Read(new XmlTextReader(stream)
			{
				XmlResolver = null,
				DtdProcessing = DtdProcessing.Prohibit
			}, validationEventHandler);
		}

		/// <summary>Returns a new instance of <see cref="T:System.Web.Services.Description.WebReferenceOptions" /> based on the code generation options described in the specified <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>A new instance of <see cref="T:System.Web.Services.Description.WebReferenceOptions" /> based on the code generation options described in the specified <see cref="T:System.Xml.XmlReader" />.</returns>
		/// <param name="xmlReader">The <see cref="T:System.Xml.XmlReader" /> that contains the code generation options.</param>
		/// <param name="validationEventHandler">The <see cref="T:System.Xml.Schema.ValidationEventHandler" /> to be associated with the new instance of <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</param>
		// Token: 0x0600096F RID: 2415 RVA: 0x00040FD4 File Offset: 0x0003F1D4
		public static WebReferenceOptions Read(XmlReader xmlReader, ValidationEventHandler validationEventHandler)
		{
			XmlValidatingReader xmlValidatingReader = new XmlValidatingReader(xmlReader);
			xmlValidatingReader.ValidationType = ValidationType.Schema;
			if (validationEventHandler != null)
			{
				xmlValidatingReader.ValidationEventHandler += validationEventHandler;
			}
			else
			{
				xmlValidatingReader.ValidationEventHandler += WebReferenceOptions.SchemaValidationHandler;
			}
			xmlValidatingReader.Schemas.Add(WebReferenceOptions.Schema);
			webReferenceOptionsSerializer webReferenceOptionsSerializer = new webReferenceOptionsSerializer();
			WebReferenceOptions webReferenceOptions;
			try
			{
				webReferenceOptions = (WebReferenceOptions)webReferenceOptionsSerializer.Deserialize(xmlValidatingReader);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				xmlValidatingReader.Close();
			}
			return webReferenceOptions;
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00041058 File Offset: 0x0003F258
		private static void SchemaValidationHandler(object sender, ValidationEventArgs args)
		{
			if (args.Severity != XmlSeverityType.Error)
			{
				return;
			}
			throw new InvalidOperationException(Res.GetString("WsdlInstanceValidationDetails", new object[]
			{
				args.Message,
				args.Exception.LineNumber.ToString(CultureInfo.InvariantCulture),
				args.Exception.LinePosition.ToString(CultureInfo.InvariantCulture)
			}));
		}

		/// <summary>A <see cref="T:System.String" /> that contains the target namespace for the <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</summary>
		// Token: 0x04000581 RID: 1409
		public const string TargetNamespace = "http://microsoft.com/webReference/";

		// Token: 0x04000582 RID: 1410
		private static XmlSchema schema;

		// Token: 0x04000583 RID: 1411
		private CodeGenerationOptions codeGenerationOptions = CodeGenerationOptions.GenerateOldAsync;

		// Token: 0x04000584 RID: 1412
		private ServiceDescriptionImportStyle style;

		// Token: 0x04000585 RID: 1413
		private StringCollection schemaImporterExtensions;

		// Token: 0x04000586 RID: 1414
		private bool verbose;
	}
}
