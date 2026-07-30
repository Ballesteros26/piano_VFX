using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the extension element for simple content from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to derive simple types by extension. Such derivations are used to extend the simple type content of the element by adding attributes.</summary>
	// Token: 0x0200047E RID: 1150
	public class XmlSchemaSimpleContentExtension : XmlSchemaContent
	{
		/// <summary>Gets or sets the name of a built-in data type or simple type from which this type is extended.</summary>
		/// <returns>The base type name.</returns>
		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06002D3E RID: 11582 RVA: 0x00109D52 File Offset: 0x00107F52
		// (set) Token: 0x06002D3F RID: 11583 RVA: 0x00109D5A File Offset: 0x00107F5A
		[XmlAttribute("base")]
		public XmlQualifiedName BaseTypeName
		{
			get
			{
				return this.baseTypeName;
			}
			set
			{
				this.baseTypeName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> and <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroupRef" />.</summary>
		/// <returns>The collection of attributes for the simpleType element.</returns>
		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06002D40 RID: 11584 RVA: 0x00109D73 File Offset: 0x00107F73
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>Gets or sets the XmlSchemaAnyAttribute to be used for the attribute value.</summary>
		/// <returns>The XmlSchemaAnyAttribute.Optional.</returns>
		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06002D41 RID: 11585 RVA: 0x00109D7B File Offset: 0x00107F7B
		// (set) Token: 0x06002D42 RID: 11586 RVA: 0x00109D83 File Offset: 0x00107F83
		[XmlElement("anyAttribute")]
		public XmlSchemaAnyAttribute AnyAttribute
		{
			get
			{
				return this.anyAttribute;
			}
			set
			{
				this.anyAttribute = value;
			}
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x00109D8C File Offset: 0x00107F8C
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		// Token: 0x04001E14 RID: 7700
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x04001E15 RID: 7701
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04001E16 RID: 7702
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;
	}
}
