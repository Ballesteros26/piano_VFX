using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the restriction element for simple types from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used restricting simpleType element.</summary>
	// Token: 0x02000483 RID: 1155
	public class XmlSchemaSimpleTypeRestriction : XmlSchemaSimpleTypeContent
	{
		/// <summary>Gets or sets the name of the qualified base type.</summary>
		/// <returns>The qualified name of the simple type restriction base type.</returns>
		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06002D5D RID: 11613 RVA: 0x00109F30 File Offset: 0x00108130
		// (set) Token: 0x06002D5E RID: 11614 RVA: 0x00109F38 File Offset: 0x00108138
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

		/// <summary>Gets or sets information on the base type.</summary>
		/// <returns>The base type for the simpleType element.</returns>
		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06002D5F RID: 11615 RVA: 0x00109F51 File Offset: 0x00108151
		// (set) Token: 0x06002D60 RID: 11616 RVA: 0x00109F59 File Offset: 0x00108159
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		public XmlSchemaSimpleType BaseType
		{
			get
			{
				return this.baseType;
			}
			set
			{
				this.baseType = value;
			}
		}

		/// <summary>Gets or sets an Xml Schema facet. </summary>
		/// <returns>One of the following facet classes:<see cref="T:System.Xml.Schema.XmlSchemaLengthFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMinLengthFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMaxLengthFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaPatternFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaEnumerationFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMaxInclusiveFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMaxExclusiveFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMinInclusiveFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMinExclusiveFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaFractionDigitsFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaTotalDigitsFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaWhiteSpaceFacet" />.</returns>
		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06002D61 RID: 11617 RVA: 0x00109F62 File Offset: 0x00108162
		[XmlElement("length", typeof(XmlSchemaLengthFacet))]
		[XmlElement("minLength", typeof(XmlSchemaMinLengthFacet))]
		[XmlElement("maxLength", typeof(XmlSchemaMaxLengthFacet))]
		[XmlElement("pattern", typeof(XmlSchemaPatternFacet))]
		[XmlElement("enumeration", typeof(XmlSchemaEnumerationFacet))]
		[XmlElement("maxExclusive", typeof(XmlSchemaMaxExclusiveFacet))]
		[XmlElement("minInclusive", typeof(XmlSchemaMinInclusiveFacet))]
		[XmlElement("minExclusive", typeof(XmlSchemaMinExclusiveFacet))]
		[XmlElement("totalDigits", typeof(XmlSchemaTotalDigitsFacet))]
		[XmlElement("fractionDigits", typeof(XmlSchemaFractionDigitsFacet))]
		[XmlElement("whiteSpace", typeof(XmlSchemaWhiteSpaceFacet))]
		[XmlElement("maxInclusive", typeof(XmlSchemaMaxInclusiveFacet))]
		public XmlSchemaObjectCollection Facets
		{
			get
			{
				return this.facets;
			}
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x00109F6A File Offset: 0x0010816A
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = (XmlSchemaSimpleTypeRestriction)base.MemberwiseClone();
			xmlSchemaSimpleTypeRestriction.BaseTypeName = this.baseTypeName.Clone();
			return xmlSchemaSimpleTypeRestriction;
		}

		// Token: 0x04001E20 RID: 7712
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;

		// Token: 0x04001E21 RID: 7713
		private XmlSchemaSimpleType baseType;

		// Token: 0x04001E22 RID: 7714
		private XmlSchemaObjectCollection facets = new XmlSchemaObjectCollection();
	}
}
