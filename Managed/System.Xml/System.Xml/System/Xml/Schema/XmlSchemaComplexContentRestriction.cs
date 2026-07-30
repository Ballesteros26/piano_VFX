using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the restriction element from XML Schema as specified by the World Wide Web Consortium (W3C). This class is for complex types with a complex content model derived by restriction. It restricts the contents of the complex type to a subset of the inherited complex type.</summary>
	// Token: 0x02000445 RID: 1093
	public class XmlSchemaComplexContentRestriction : XmlSchemaContent
	{
		/// <summary>Gets or sets the name of a complex type from which this type is derived by restriction.</summary>
		/// <returns>The name of the complex type from which this type is derived by restriction.</returns>
		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06002B67 RID: 11111 RVA: 0x0010579E File Offset: 0x0010399E
		// (set) Token: 0x06002B68 RID: 11112 RVA: 0x001057A6 File Offset: 0x001039A6
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
		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06002B69 RID: 11113 RVA: 0x001057BF File Offset: 0x001039BF
		// (set) Token: 0x06002B6A RID: 11114 RVA: 0x001057C7 File Offset: 0x001039C7
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		[XmlElement("group", typeof(XmlSchemaGroupRef))]
		[XmlElement("all", typeof(XmlSchemaAll))]
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

		/// <summary>Gets the collection of attributes for the complex type. Contains the <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> and <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroupRef" /> elements.</summary>
		/// <returns>The collection of attributes for the complex type.</returns>
		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06002B6B RID: 11115 RVA: 0x001057D0 File Offset: 0x001039D0
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> component of the complex content model.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> component of the complex content model.</returns>
		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06002B6C RID: 11116 RVA: 0x001057D8 File Offset: 0x001039D8
		// (set) Token: 0x06002B6D RID: 11117 RVA: 0x001057E0 File Offset: 0x001039E0
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

		// Token: 0x06002B6E RID: 11118 RVA: 0x001057E9 File Offset: 0x001039E9
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		// Token: 0x04001D56 RID: 7510
		private XmlSchemaParticle particle;

		// Token: 0x04001D57 RID: 7511
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x04001D58 RID: 7512
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04001D59 RID: 7513
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;
	}
}
