using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the restriction element for simple content from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to derive simple types by restriction. Such derivations can be used to restrict the range of values for the element to a subset of the values specified in the inherited simple type.</summary>
	// Token: 0x0200047F RID: 1151
	public class XmlSchemaSimpleContentRestriction : XmlSchemaContent
	{
		/// <summary>Gets or sets the name of the built-in data type or simple type from which this type is derived.</summary>
		/// <returns>The name of the base type.</returns>
		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06002D45 RID: 11589 RVA: 0x00109DB3 File Offset: 0x00107FB3
		// (set) Token: 0x06002D46 RID: 11590 RVA: 0x00109DBB File Offset: 0x00107FBB
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

		/// <summary>Gets or sets the simple type base value.</summary>
		/// <returns>The simple type base value.</returns>
		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06002D47 RID: 11591 RVA: 0x00109DD4 File Offset: 0x00107FD4
		// (set) Token: 0x06002D48 RID: 11592 RVA: 0x00109DDC File Offset: 0x00107FDC
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
		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06002D49 RID: 11593 RVA: 0x00109DE5 File Offset: 0x00107FE5
		[XmlElement("fractionDigits", typeof(XmlSchemaFractionDigitsFacet))]
		[XmlElement("minLength", typeof(XmlSchemaMinLengthFacet))]
		[XmlElement("length", typeof(XmlSchemaLengthFacet))]
		[XmlElement("minExclusive", typeof(XmlSchemaMinExclusiveFacet))]
		[XmlElement("totalDigits", typeof(XmlSchemaTotalDigitsFacet))]
		[XmlElement("maxLength", typeof(XmlSchemaMaxLengthFacet))]
		[XmlElement("pattern", typeof(XmlSchemaPatternFacet))]
		[XmlElement("enumeration", typeof(XmlSchemaEnumerationFacet))]
		[XmlElement("maxInclusive", typeof(XmlSchemaMaxInclusiveFacet))]
		[XmlElement("minInclusive", typeof(XmlSchemaMinInclusiveFacet))]
		[XmlElement("maxExclusive", typeof(XmlSchemaMaxExclusiveFacet))]
		[XmlElement("whiteSpace", typeof(XmlSchemaWhiteSpaceFacet))]
		public XmlSchemaObjectCollection Facets
		{
			get
			{
				return this.facets;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> and <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroupRef" /> collection of attributes for the simple type.</summary>
		/// <returns>The collection of attributes for a simple type.</returns>
		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06002D4A RID: 11594 RVA: 0x00109DED File Offset: 0x00107FED
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> to be used for the attribute value.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> for the attribute value.Optional.</returns>
		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06002D4B RID: 11595 RVA: 0x00109DF5 File Offset: 0x00107FF5
		// (set) Token: 0x06002D4C RID: 11596 RVA: 0x00109DFD File Offset: 0x00107FFD
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

		// Token: 0x06002D4D RID: 11597 RVA: 0x00109E06 File Offset: 0x00108006
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		// Token: 0x04001E17 RID: 7703
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;

		// Token: 0x04001E18 RID: 7704
		private XmlSchemaSimpleType baseType;

		// Token: 0x04001E19 RID: 7705
		private XmlSchemaObjectCollection facets = new XmlSchemaObjectCollection();

		// Token: 0x04001E1A RID: 7706
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x04001E1B RID: 7707
		private XmlSchemaAnyAttribute anyAttribute;
	}
}
