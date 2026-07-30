using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the extension element from XML Schema as specified by the World Wide Web Consortium (W3C). This class is for complex types with complex content model derived by extension. It extends the complex type by adding attributes or elements.</summary>
	// Token: 0x02000444 RID: 1092
	public class XmlSchemaComplexContentExtension : XmlSchemaContent
	{
		/// <summary>Gets or sets the name of the complex type from which this type is derived by extension.</summary>
		/// <returns>The name of the complex type from which this type is derived by extension.</returns>
		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06002B5E RID: 11102 RVA: 0x0010572C File Offset: 0x0010392C
		// (set) Token: 0x06002B5F RID: 11103 RVA: 0x00105734 File Offset: 0x00103934
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

		/// <summary>Gets or sets one of the <see cref="T:System.Xml.Schema.XmlSchemaGroupRef" />, <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlSchemaGroupRef" />, <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes.</returns>
		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06002B60 RID: 11104 RVA: 0x0010574D File Offset: 0x0010394D
		// (set) Token: 0x06002B61 RID: 11105 RVA: 0x00105755 File Offset: 0x00103955
		[XmlElement("all", typeof(XmlSchemaAll))]
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		[XmlElement("group", typeof(XmlSchemaGroupRef))]
		public XmlSchemaParticle Particle
		{
			get
			{
				return this.particle;
			}
			set
			{
				this.particle = value;
			}
		}

		/// <summary>Gets the collection of attributes for the complex content. Contains <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> and <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroupRef" /> elements.</summary>
		/// <returns>The collection of attributes for the complex content.</returns>
		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06002B62 RID: 11106 RVA: 0x0010575E File Offset: 0x0010395E
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> component of the complex content model.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> component of the complex content model.</returns>
		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06002B63 RID: 11107 RVA: 0x00105766 File Offset: 0x00103966
		// (set) Token: 0x06002B64 RID: 11108 RVA: 0x0010576E File Offset: 0x0010396E
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

		// Token: 0x06002B65 RID: 11109 RVA: 0x00105777 File Offset: 0x00103977
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		// Token: 0x04001D52 RID: 7506
		private XmlSchemaParticle particle;

		// Token: 0x04001D53 RID: 7507
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x04001D54 RID: 7508
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04001D55 RID: 7509
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;
	}
}
