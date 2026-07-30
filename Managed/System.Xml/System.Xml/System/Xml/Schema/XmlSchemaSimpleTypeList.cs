using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the list element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to define a simpleType element as a list of values of a specified data type.</summary>
	// Token: 0x02000482 RID: 1154
	public class XmlSchemaSimpleTypeList : XmlSchemaSimpleTypeContent
	{
		/// <summary>Gets or sets the name of a built-in data type or simpleType element defined in this schema (or another schema indicated by the specified namespace).</summary>
		/// <returns>The type name of the simple type list.</returns>
		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06002D55 RID: 11605 RVA: 0x00109EBC File Offset: 0x001080BC
		// (set) Token: 0x06002D56 RID: 11606 RVA: 0x00109EC4 File Offset: 0x001080C4
		[XmlAttribute("itemType")]
		public XmlQualifiedName ItemTypeName
		{
			get
			{
				return this.itemTypeName;
			}
			set
			{
				this.itemTypeName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		/// <summary>Gets or sets the simpleType element that is derived from the type specified by the base value.</summary>
		/// <returns>The item type for the simple type element.</returns>
		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06002D57 RID: 11607 RVA: 0x00109EDD File Offset: 0x001080DD
		// (set) Token: 0x06002D58 RID: 11608 RVA: 0x00109EE5 File Offset: 0x001080E5
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		public XmlSchemaSimpleType ItemType
		{
			get
			{
				return this.itemType;
			}
			set
			{
				this.itemType = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> representing the type of the simpleType element based on the <see cref="P:System.Xml.Schema.XmlSchemaSimpleTypeList.ItemType" /> and <see cref="P:System.Xml.Schema.XmlSchemaSimpleTypeList.ItemTypeName" /> values of the simple type.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> representing the type of the simpleType element.</returns>
		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06002D59 RID: 11609 RVA: 0x00109EEE File Offset: 0x001080EE
		// (set) Token: 0x06002D5A RID: 11610 RVA: 0x00109EF6 File Offset: 0x001080F6
		[XmlIgnore]
		public XmlSchemaSimpleType BaseItemType
		{
			get
			{
				return this.baseItemType;
			}
			set
			{
				this.baseItemType = value;
			}
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x00109EFF File Offset: 0x001080FF
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = (XmlSchemaSimpleTypeList)base.MemberwiseClone();
			xmlSchemaSimpleTypeList.ItemTypeName = this.itemTypeName.Clone();
			return xmlSchemaSimpleTypeList;
		}

		// Token: 0x04001E1D RID: 7709
		private XmlQualifiedName itemTypeName = XmlQualifiedName.Empty;

		// Token: 0x04001E1E RID: 7710
		private XmlSchemaSimpleType itemType;

		// Token: 0x04001E1F RID: 7711
		private XmlSchemaSimpleType baseItemType;
	}
}
