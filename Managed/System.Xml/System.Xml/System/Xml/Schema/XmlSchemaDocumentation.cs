using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the documentation element from XML Schema as specified by the World Wide Web Consortium (W3C). This class specifies information to be read or used by humans within an annotation.</summary>
	// Token: 0x0200044D RID: 1101
	public class XmlSchemaDocumentation : XmlSchemaObject
	{
		/// <summary>Gets or sets the Uniform Resource Identifier (URI) source of the information.</summary>
		/// <returns>A URI reference. The default is String.Empty.Optional.</returns>
		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06002BC1 RID: 11201 RVA: 0x001066A4 File Offset: 0x001048A4
		// (set) Token: 0x06002BC2 RID: 11202 RVA: 0x001066AC File Offset: 0x001048AC
		[XmlAttribute("source", DataType = "anyURI")]
		public string Source
		{
			get
			{
				return this.source;
			}
			set
			{
				this.source = value;
			}
		}

		/// <summary>Gets or sets the xml:lang attribute. This serves as an indicator of the language used in the contents.</summary>
		/// <returns>The xml:lang attribute.Optional.</returns>
		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06002BC3 RID: 11203 RVA: 0x001066B5 File Offset: 0x001048B5
		// (set) Token: 0x06002BC4 RID: 11204 RVA: 0x001066BD File Offset: 0x001048BD
		[XmlAttribute("xml:lang")]
		public string Language
		{
			get
			{
				return this.language;
			}
			set
			{
				this.language = (string)XmlSchemaDocumentation.languageType.Datatype.ParseValue(value, null, null);
			}
		}

		/// <summary>Gets or sets an array of XmlNodes that represents the documentation child nodes.</summary>
		/// <returns>The array that represents the documentation child nodes.</returns>
		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06002BC5 RID: 11205 RVA: 0x001066DC File Offset: 0x001048DC
		// (set) Token: 0x06002BC6 RID: 11206 RVA: 0x001066E4 File Offset: 0x001048E4
		[XmlAnyElement]
		[XmlText]
		public XmlNode[] Markup
		{
			get
			{
				return this.markup;
			}
			set
			{
				this.markup = value;
			}
		}

		// Token: 0x04001D7E RID: 7550
		private string source;

		// Token: 0x04001D7F RID: 7551
		private string language;

		// Token: 0x04001D80 RID: 7552
		private XmlNode[] markup;

		// Token: 0x04001D81 RID: 7553
		private static XmlSchemaSimpleType languageType = DatatypeImplementation.GetSimpleTypeFromXsdType(new XmlQualifiedName("language", "http://www.w3.org/2001/XMLSchema"));
	}
}
