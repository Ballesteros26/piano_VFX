using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the simpleType element for simple content from XML Schema as specified by the World Wide Web Consortium (W3C). This class defines a simple type. Simple types can specify information and constraints for the value of attributes or elements with text-only content.</summary>
	// Token: 0x02000480 RID: 1152
	public class XmlSchemaSimpleType : XmlSchemaType
	{
		/// <summary>Gets or sets one of <see cref="T:System.Xml.Schema.XmlSchemaSimpleTypeUnion" />, <see cref="T:System.Xml.Schema.XmlSchemaSimpleTypeList" />, or <see cref="T:System.Xml.Schema.XmlSchemaSimpleTypeRestriction" />.</summary>
		/// <returns>One of XmlSchemaSimpleTypeUnion, XmlSchemaSimpleTypeList, or XmlSchemaSimpleTypeRestriction.</returns>
		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06002D50 RID: 11600 RVA: 0x00109E40 File Offset: 0x00108040
		// (set) Token: 0x06002D51 RID: 11601 RVA: 0x00109E48 File Offset: 0x00108048
		[XmlElement("union", typeof(XmlSchemaSimpleTypeUnion))]
		[XmlElement("restriction", typeof(XmlSchemaSimpleTypeRestriction))]
		[XmlElement("list", typeof(XmlSchemaSimpleTypeList))]
		public XmlSchemaSimpleTypeContent Content
		{
			get
			{
				return this.content;
			}
			set
			{
				this.content = value;
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06002D52 RID: 11602 RVA: 0x00109E51 File Offset: 0x00108051
		internal override XmlQualifiedName DerivedFrom
		{
			get
			{
				if (this.content == null)
				{
					return XmlQualifiedName.Empty;
				}
				if (this.content is XmlSchemaSimpleTypeRestriction)
				{
					return ((XmlSchemaSimpleTypeRestriction)this.content).BaseTypeName;
				}
				return XmlQualifiedName.Empty;
			}
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x00109E84 File Offset: 0x00108084
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = (XmlSchemaSimpleType)base.MemberwiseClone();
			if (this.content != null)
			{
				xmlSchemaSimpleType.Content = (XmlSchemaSimpleTypeContent)this.content.Clone();
			}
			return xmlSchemaSimpleType;
		}

		// Token: 0x04001E1C RID: 7708
		private XmlSchemaSimpleTypeContent content;
	}
}
