using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the union element for simple types from XML Schema as specified by the World Wide Web Consortium (W3C). A union datatype can be used to specify the content of a simpleType. The value of the simpleType element must be any one of a set of alternative datatypes specified in the union. Union types are always derived types and must comprise at least two alternative datatypes.</summary>
	// Token: 0x02000484 RID: 1156
	public class XmlSchemaSimpleTypeUnion : XmlSchemaSimpleTypeContent
	{
		/// <summary>Gets the collection of base types.</summary>
		/// <returns>The collection of simple type base values.</returns>
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06002D64 RID: 11620 RVA: 0x00109FA6 File Offset: 0x001081A6
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		public XmlSchemaObjectCollection BaseTypes
		{
			get
			{
				return this.baseTypes;
			}
		}

		/// <summary>Gets or sets the array of qualified member names of built-in data types or simpleType elements defined in this schema (or another schema indicated by the specified namespace).</summary>
		/// <returns>An array that consists of a list of members of built-in data types or simple types.</returns>
		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06002D65 RID: 11621 RVA: 0x00109FAE File Offset: 0x001081AE
		// (set) Token: 0x06002D66 RID: 11622 RVA: 0x00109FB6 File Offset: 0x001081B6
		[XmlAttribute("memberTypes")]
		public XmlQualifiedName[] MemberTypes
		{
			get
			{
				return this.memberTypes;
			}
			set
			{
				this.memberTypes = value;
			}
		}

		/// <summary>Gets an array of <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> objects representing the type of the simpleType element based on the <see cref="P:System.Xml.Schema.XmlSchemaSimpleTypeUnion.BaseTypes" /> and <see cref="P:System.Xml.Schema.XmlSchemaSimpleTypeUnion.MemberTypes" /> values of the simple type.</summary>
		/// <returns>An array of <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> objects representing the type of the simpleType element.</returns>
		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06002D67 RID: 11623 RVA: 0x00109FBF File Offset: 0x001081BF
		[XmlIgnore]
		public XmlSchemaSimpleType[] BaseMemberTypes
		{
			get
			{
				return this.baseMemberTypes;
			}
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x00109FC7 File Offset: 0x001081C7
		internal void SetBaseMemberTypes(XmlSchemaSimpleType[] baseMemberTypes)
		{
			this.baseMemberTypes = baseMemberTypes;
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x00109FD0 File Offset: 0x001081D0
		internal override XmlSchemaObject Clone()
		{
			if (this.memberTypes != null && this.memberTypes.Length != 0)
			{
				XmlSchemaSimpleTypeUnion xmlSchemaSimpleTypeUnion = (XmlSchemaSimpleTypeUnion)base.MemberwiseClone();
				XmlQualifiedName[] array = new XmlQualifiedName[this.memberTypes.Length];
				for (int i = 0; i < this.memberTypes.Length; i++)
				{
					array[i] = this.memberTypes[i].Clone();
				}
				xmlSchemaSimpleTypeUnion.MemberTypes = array;
				return xmlSchemaSimpleTypeUnion;
			}
			return this;
		}

		// Token: 0x04001E23 RID: 7715
		private XmlSchemaObjectCollection baseTypes = new XmlSchemaObjectCollection();

		// Token: 0x04001E24 RID: 7716
		private XmlQualifiedName[] memberTypes;

		// Token: 0x04001E25 RID: 7717
		private XmlSchemaSimpleType[] baseMemberTypes;
	}
}
