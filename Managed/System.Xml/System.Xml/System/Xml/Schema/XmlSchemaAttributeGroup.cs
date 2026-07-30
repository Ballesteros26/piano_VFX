using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the attributeGroup element from the XML Schema as specified by the World Wide Web Consortium (W3C). AttributesGroups provides a mechanism to group a set of attribute declarations so that they can be incorporated as a group into complex type definitions.</summary>
	// Token: 0x0200043C RID: 1084
	public class XmlSchemaAttributeGroup : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the name of the attribute group.</summary>
		/// <returns>The name of the attribute group.</returns>
		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06002B0D RID: 11021 RVA: 0x00104F4A File Offset: 0x0010314A
		// (set) Token: 0x06002B0E RID: 11022 RVA: 0x00104F52 File Offset: 0x00103152
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

		/// <summary>Gets the collection of attributes for the attribute group. Contains XmlSchemaAttribute and XmlSchemaAttributeGroupRef elements.</summary>
		/// <returns>The collection of attributes for the attribute group.</returns>
		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06002B0F RID: 11023 RVA: 0x00104F5B File Offset: 0x0010315B
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> component of the attribute group.</summary>
		/// <returns>The World Wide Web Consortium (W3C) anyAttribute element.</returns>
		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06002B10 RID: 11024 RVA: 0x00104F63 File Offset: 0x00103163
		// (set) Token: 0x06002B11 RID: 11025 RVA: 0x00104F6B File Offset: 0x0010316B
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

		/// <summary>Gets the qualified name of the attribute group.</summary>
		/// <returns>The qualified name of the attribute group.</returns>
		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06002B12 RID: 11026 RVA: 0x00104F74 File Offset: 0x00103174
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qname;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06002B13 RID: 11027 RVA: 0x00104F7C File Offset: 0x0010317C
		[XmlIgnore]
		internal XmlSchemaObjectTable AttributeUses
		{
			get
			{
				if (this.attributeUses == null)
				{
					this.attributeUses = new XmlSchemaObjectTable();
				}
				return this.attributeUses;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06002B14 RID: 11028 RVA: 0x00104F97 File Offset: 0x00103197
		// (set) Token: 0x06002B15 RID: 11029 RVA: 0x00104F9F File Offset: 0x0010319F
		[XmlIgnore]
		internal XmlSchemaAnyAttribute AttributeWildcard
		{
			get
			{
				return this.attributeWildcard;
			}
			set
			{
				this.attributeWildcard = value;
			}
		}

		/// <summary>Gets the redefined attribute group property from the XML Schema.</summary>
		/// <returns>The redefined attribute group property.</returns>
		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06002B16 RID: 11030 RVA: 0x00104FA8 File Offset: 0x001031A8
		[XmlIgnore]
		public XmlSchemaAttributeGroup RedefinedAttributeGroup
		{
			get
			{
				return this.redefined;
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06002B17 RID: 11031 RVA: 0x00104FA8 File Offset: 0x001031A8
		// (set) Token: 0x06002B18 RID: 11032 RVA: 0x00104FB0 File Offset: 0x001031B0
		[XmlIgnore]
		internal XmlSchemaAttributeGroup Redefined
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

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06002B19 RID: 11033 RVA: 0x00104FB9 File Offset: 0x001031B9
		// (set) Token: 0x06002B1A RID: 11034 RVA: 0x00104FC1 File Offset: 0x001031C1
		[XmlIgnore]
		internal int SelfReferenceCount
		{
			get
			{
				return this.selfReferenceCount;
			}
			set
			{
				this.selfReferenceCount = value;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06002B1B RID: 11035 RVA: 0x00104FCA File Offset: 0x001031CA
		// (set) Token: 0x06002B1C RID: 11036 RVA: 0x00104FD2 File Offset: 0x001031D2
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

		// Token: 0x06002B1D RID: 11037 RVA: 0x00104FDB File Offset: 0x001031DB
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qname = value;
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x00104FE4 File Offset: 0x001031E4
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaAttributeGroup xmlSchemaAttributeGroup = (XmlSchemaAttributeGroup)base.MemberwiseClone();
			if (XmlSchemaComplexType.HasAttributeQNameRef(this.attributes))
			{
				xmlSchemaAttributeGroup.attributes = XmlSchemaComplexType.CloneAttributes(this.attributes);
				xmlSchemaAttributeGroup.attributeUses = null;
			}
			return xmlSchemaAttributeGroup;
		}

		// Token: 0x04001D38 RID: 7480
		private string name;

		// Token: 0x04001D39 RID: 7481
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x04001D3A RID: 7482
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04001D3B RID: 7483
		private XmlQualifiedName qname = XmlQualifiedName.Empty;

		// Token: 0x04001D3C RID: 7484
		private XmlSchemaAttributeGroup redefined;

		// Token: 0x04001D3D RID: 7485
		private XmlSchemaObjectTable attributeUses;

		// Token: 0x04001D3E RID: 7486
		private XmlSchemaAnyAttribute attributeWildcard;

		// Token: 0x04001D3F RID: 7487
		private int selfReferenceCount;
	}
}
