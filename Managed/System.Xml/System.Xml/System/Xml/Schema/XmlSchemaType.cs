using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>The base class for all simple types and complex types.</summary>
	// Token: 0x02000487 RID: 1159
	public class XmlSchemaType : XmlSchemaAnnotated
	{
		/// <summary>Returns an <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> that represents the built-in simple type of the simple type that is specified by the qualified name.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> that represents the built-in simple type.</returns>
		/// <param name="qualifiedName">The <see cref="T:System.Xml.XmlQualifiedName" /> of the simple type.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlQualifiedName" /> parameter is null.</exception>
		// Token: 0x06002D71 RID: 11633 RVA: 0x0010A09A File Offset: 0x0010829A
		public static XmlSchemaSimpleType GetBuiltInSimpleType(XmlQualifiedName qualifiedName)
		{
			if (qualifiedName == null)
			{
				throw new ArgumentNullException("qualifiedName");
			}
			return DatatypeImplementation.GetSimpleTypeFromXsdType(qualifiedName);
		}

		/// <summary>Returns an <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> that represents the built-in simple type of the specified simple type.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> that represents the built-in simple type.</returns>
		/// <param name="typeCode">One of the <see cref="T:System.Xml.Schema.XmlTypeCode" /> values representing the simple type.</param>
		// Token: 0x06002D72 RID: 11634 RVA: 0x0010A0B6 File Offset: 0x001082B6
		public static XmlSchemaSimpleType GetBuiltInSimpleType(XmlTypeCode typeCode)
		{
			return DatatypeImplementation.GetSimpleTypeFromTypeCode(typeCode);
		}

		/// <summary>Returns an <see cref="T:System.Xml.Schema.XmlSchemaComplexType" /> that represents the built-in complex type of the complex type specified.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaComplexType" /> that represents the built-in complex type.</returns>
		/// <param name="typeCode">One of the <see cref="T:System.Xml.Schema.XmlTypeCode" /> values representing the complex type.</param>
		// Token: 0x06002D73 RID: 11635 RVA: 0x0010A0BE File Offset: 0x001082BE
		public static XmlSchemaComplexType GetBuiltInComplexType(XmlTypeCode typeCode)
		{
			if (typeCode == XmlTypeCode.Item)
			{
				return XmlSchemaComplexType.AnyType;
			}
			return null;
		}

		/// <summary>Returns an <see cref="T:System.Xml.Schema.XmlSchemaComplexType" /> that represents the built-in complex type of the complex type specified by qualified name.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaComplexType" /> that represents the built-in complex type.</returns>
		/// <param name="qualifiedName">The <see cref="T:System.Xml.XmlQualifiedName" /> of the complex type.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlQualifiedName" /> parameter is null.</exception>
		// Token: 0x06002D74 RID: 11636 RVA: 0x0010A0CC File Offset: 0x001082CC
		public static XmlSchemaComplexType GetBuiltInComplexType(XmlQualifiedName qualifiedName)
		{
			if (qualifiedName == null)
			{
				throw new ArgumentNullException("qualifiedName");
			}
			if (qualifiedName.Equals(XmlSchemaComplexType.AnyType.QualifiedName))
			{
				return XmlSchemaComplexType.AnyType;
			}
			if (qualifiedName.Equals(XmlSchemaComplexType.UntypedAnyType.QualifiedName))
			{
				return XmlSchemaComplexType.UntypedAnyType;
			}
			return null;
		}

		/// <summary>Gets or sets the name of the type.</summary>
		/// <returns>The name of the type.</returns>
		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06002D75 RID: 11637 RVA: 0x0010A11E File Offset: 0x0010831E
		// (set) Token: 0x06002D76 RID: 11638 RVA: 0x0010A126 File Offset: 0x00108326
		[XmlAttribute("name")]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the final attribute of the type derivation that indicates if further derivations are allowed.</summary>
		/// <returns>One of the valid <see cref="T:System.Xml.Schema.XmlSchemaDerivationMethod" /> values. The default is <see cref="F:System.Xml.Schema.XmlSchemaDerivationMethod.None" />.</returns>
		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06002D77 RID: 11639 RVA: 0x0010A12F File Offset: 0x0010832F
		// (set) Token: 0x06002D78 RID: 11640 RVA: 0x0010A137 File Offset: 0x00108337
		[DefaultValue(XmlSchemaDerivationMethod.None)]
		[XmlAttribute("final")]
		public XmlSchemaDerivationMethod Final
		{
			get
			{
				return this.final;
			}
			set
			{
				this.final = value;
			}
		}

		/// <summary>Gets the qualified name for the type built from the Name attribute of this type. This is a post-schema-compilation property.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlQualifiedName" /> for the type built from the Name attribute of this type.</returns>
		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06002D79 RID: 11641 RVA: 0x0010A140 File Offset: 0x00108340
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qname;
			}
		}

		/// <summary>Gets the post-compilation value of the <see cref="P:System.Xml.Schema.XmlSchemaType.Final" /> property.</summary>
		/// <returns>The post-compilation value of the <see cref="P:System.Xml.Schema.XmlSchemaType.Final" /> property. The default is the finalDefault attribute value of the schema element.</returns>
		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06002D7A RID: 11642 RVA: 0x0010A14A File Offset: 0x0010834A
		[XmlIgnore]
		public XmlSchemaDerivationMethod FinalResolved
		{
			get
			{
				return this.finalResolved;
			}
		}

		/// <summary>Gets the post-compilation object type or the built-in XML Schema Definition Language (XSD) data type, simpleType element, or complexType element. This is a post-schema-compilation infoset property.</summary>
		/// <returns>The built-in XSD data type, simpleType element, or complexType element.</returns>
		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06002D7B RID: 11643 RVA: 0x0010A152 File Offset: 0x00108352
		[Obsolete("This property has been deprecated. Please use BaseXmlSchemaType property that returns a strongly typed base schema type. http://go.microsoft.com/fwlink/?linkid=14202")]
		[XmlIgnore]
		public object BaseSchemaType
		{
			get
			{
				if (this.baseSchemaType == null)
				{
					return null;
				}
				if (this.baseSchemaType.QualifiedName.Namespace == "http://www.w3.org/2001/XMLSchema")
				{
					return this.baseSchemaType.Datatype;
				}
				return this.baseSchemaType;
			}
		}

		/// <summary>Gets the post-compilation value for the base type of this schema type.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaType" /> object representing the base type of this schema type.</returns>
		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06002D7C RID: 11644 RVA: 0x0010A18C File Offset: 0x0010838C
		[XmlIgnore]
		public XmlSchemaType BaseXmlSchemaType
		{
			get
			{
				return this.baseSchemaType;
			}
		}

		/// <summary>Gets the post-compilation information on how this element was derived from its base type.</summary>
		/// <returns>One of the valid <see cref="T:System.Xml.Schema.XmlSchemaDerivationMethod" /> values.</returns>
		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06002D7D RID: 11645 RVA: 0x0010A194 File Offset: 0x00108394
		[XmlIgnore]
		public XmlSchemaDerivationMethod DerivedBy
		{
			get
			{
				return this.derivedBy;
			}
		}

		/// <summary>Gets the post-compilation value for the data type of the complex type.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaDatatype" /> post-schema-compilation value.</returns>
		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06002D7E RID: 11646 RVA: 0x0010A19C File Offset: 0x0010839C
		[XmlIgnore]
		public XmlSchemaDatatype Datatype
		{
			get
			{
				return this.datatype;
			}
		}

		/// <summary>Gets or sets a value indicating if this type has a mixed content model. This property is only valid in a complex type.</summary>
		/// <returns>true if the type has a mixed content model; otherwise, false. The default is false.</returns>
		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06002D7F RID: 11647 RVA: 0x0000226C File Offset: 0x0000046C
		// (set) Token: 0x06002D80 RID: 11648 RVA: 0x00002F50 File Offset: 0x00001150
		[XmlIgnore]
		public virtual bool IsMixed
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlTypeCode" /> of the type.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlTypeCode" /> values.</returns>
		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06002D81 RID: 11649 RVA: 0x0010A1A4 File Offset: 0x001083A4
		[XmlIgnore]
		public XmlTypeCode TypeCode
		{
			get
			{
				if (this == XmlSchemaComplexType.AnyType)
				{
					return XmlTypeCode.Item;
				}
				if (this.datatype == null)
				{
					return XmlTypeCode.None;
				}
				return this.datatype.TypeCode;
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06002D82 RID: 11650 RVA: 0x0010A1C5 File Offset: 0x001083C5
		[XmlIgnore]
		internal XmlValueConverter ValueConverter
		{
			get
			{
				if (this.datatype == null)
				{
					return XmlUntypedConverter.Untyped;
				}
				return this.datatype.ValueConverter;
			}
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x0010A1E0 File Offset: 0x001083E0
		internal XmlReader Validate(XmlReader reader, XmlResolver resolver, XmlSchemaSet schemaSet, ValidationEventHandler valEventHandler)
		{
			if (schemaSet != null)
			{
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				xmlReaderSettings.ValidationType = ValidationType.Schema;
				xmlReaderSettings.Schemas = schemaSet;
				xmlReaderSettings.ValidationEventHandler += valEventHandler;
				return new XsdValidatingReader(reader, resolver, xmlReaderSettings, this);
			}
			return null;
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06002D84 RID: 11652 RVA: 0x0010A217 File Offset: 0x00108417
		internal XmlSchemaContentType SchemaContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x0010A21F File Offset: 0x0010841F
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qname = value;
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x0010A22A File Offset: 0x0010842A
		internal void SetFinalResolved(XmlSchemaDerivationMethod value)
		{
			this.finalResolved = value;
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x0010A233 File Offset: 0x00108433
		internal void SetBaseSchemaType(XmlSchemaType value)
		{
			this.baseSchemaType = value;
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x0010A23C File Offset: 0x0010843C
		internal void SetDerivedBy(XmlSchemaDerivationMethod value)
		{
			this.derivedBy = value;
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x0010A245 File Offset: 0x00108445
		internal void SetDatatype(XmlSchemaDatatype value)
		{
			this.datatype = value;
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06002D8A RID: 11658 RVA: 0x0010A24E File Offset: 0x0010844E
		// (set) Token: 0x06002D8B RID: 11659 RVA: 0x0010A258 File Offset: 0x00108458
		internal SchemaElementDecl ElementDecl
		{
			get
			{
				return this.elementDecl;
			}
			set
			{
				this.elementDecl = value;
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06002D8C RID: 11660 RVA: 0x0010A263 File Offset: 0x00108463
		// (set) Token: 0x06002D8D RID: 11661 RVA: 0x0010A26B File Offset: 0x0010846B
		[XmlIgnore]
		internal XmlSchemaType Redefined
		{
			get
			{
				return this.redefined;
			}
			set
			{
				this.redefined = value;
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06002D8E RID: 11662 RVA: 0x0010A274 File Offset: 0x00108474
		internal virtual XmlQualifiedName DerivedFrom
		{
			get
			{
				return XmlQualifiedName.Empty;
			}
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x0010A27B File Offset: 0x0010847B
		internal void SetContentType(XmlSchemaContentType value)
		{
			this.contentType = value;
		}

		/// <summary>Returns a value indicating if the derived schema type specified is derived from the base schema type specified</summary>
		/// <returns>true if the derived type is derived from the base type; otherwise, false.</returns>
		/// <param name="derivedType">The derived <see cref="T:System.Xml.Schema.XmlSchemaType" /> to test.</param>
		/// <param name="baseType">The base <see cref="T:System.Xml.Schema.XmlSchemaType" /> to test the derived <see cref="T:System.Xml.Schema.XmlSchemaType" /> against.</param>
		/// <param name="except">One of the <see cref="T:System.Xml.Schema.XmlSchemaDerivationMethod" /> values representing a type derivation method to exclude from testing.</param>
		// Token: 0x06002D90 RID: 11664 RVA: 0x0010A284 File Offset: 0x00108484
		public static bool IsDerivedFrom(XmlSchemaType derivedType, XmlSchemaType baseType, XmlSchemaDerivationMethod except)
		{
			if (derivedType == null || baseType == null)
			{
				return false;
			}
			if (derivedType == baseType)
			{
				return true;
			}
			if (baseType == XmlSchemaComplexType.AnyType)
			{
				return true;
			}
			XmlSchemaSimpleType xmlSchemaSimpleType;
			XmlSchemaSimpleType xmlSchemaSimpleType2;
			for (;;)
			{
				xmlSchemaSimpleType = derivedType as XmlSchemaSimpleType;
				xmlSchemaSimpleType2 = baseType as XmlSchemaSimpleType;
				if (xmlSchemaSimpleType2 != null && xmlSchemaSimpleType != null)
				{
					break;
				}
				if ((except & derivedType.DerivedBy) != XmlSchemaDerivationMethod.Empty)
				{
					return false;
				}
				derivedType = derivedType.BaseXmlSchemaType;
				if (derivedType == baseType)
				{
					return true;
				}
				if (derivedType == null)
				{
					return false;
				}
			}
			return xmlSchemaSimpleType2 == DatatypeImplementation.AnySimpleType || ((except & derivedType.DerivedBy) == XmlSchemaDerivationMethod.Empty && xmlSchemaSimpleType.Datatype.IsDerivedFrom(xmlSchemaSimpleType2.Datatype));
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x0010A306 File Offset: 0x00108506
		internal static bool IsDerivedFromDatatype(XmlSchemaDatatype derivedDataType, XmlSchemaDatatype baseDataType, XmlSchemaDerivationMethod except)
		{
			return DatatypeImplementation.AnySimpleType.Datatype == baseDataType || derivedDataType.IsDerivedFrom(baseDataType);
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06002D92 RID: 11666 RVA: 0x0010A31E File Offset: 0x0010851E
		// (set) Token: 0x06002D93 RID: 11667 RVA: 0x0010A326 File Offset: 0x00108526
		[XmlIgnore]
		internal override string NameAttribute
		{
			get
			{
				return this.Name;
			}
			set
			{
				this.Name = value;
			}
		}

		// Token: 0x04001E29 RID: 7721
		private string name;

		// Token: 0x04001E2A RID: 7722
		private XmlSchemaDerivationMethod final = XmlSchemaDerivationMethod.None;

		// Token: 0x04001E2B RID: 7723
		private XmlSchemaDerivationMethod derivedBy;

		// Token: 0x04001E2C RID: 7724
		private XmlSchemaType baseSchemaType;

		// Token: 0x04001E2D RID: 7725
		private XmlSchemaDatatype datatype;

		// Token: 0x04001E2E RID: 7726
		private XmlSchemaDerivationMethod finalResolved;

		// Token: 0x04001E2F RID: 7727
		private volatile SchemaElementDecl elementDecl;

		// Token: 0x04001E30 RID: 7728
		private volatile XmlQualifiedName qname = XmlQualifiedName.Empty;

		// Token: 0x04001E31 RID: 7729
		private XmlSchemaType redefined;

		// Token: 0x04001E32 RID: 7730
		private XmlSchemaContentType contentType;
	}
}
