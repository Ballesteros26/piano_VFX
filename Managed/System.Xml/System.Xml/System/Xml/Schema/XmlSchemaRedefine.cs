using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the redefine element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to allow simple and complex types, groups and attribute groups from external schema files to be redefined in the current schema. This class can also be used to provide versioning for the schema elements.</summary>
	// Token: 0x0200047A RID: 1146
	public class XmlSchemaRedefine : XmlSchemaExternal
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaRedefine" /> class.</summary>
		// Token: 0x06002CF7 RID: 11511 RVA: 0x00107DC7 File Offset: 0x00105FC7
		public XmlSchemaRedefine()
		{
			base.Compositor = Compositor.Redefine;
		}

		/// <summary>Gets the collection of the following classes: <see cref="T:System.Xml.Schema.XmlSchemaAnnotation" />, <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroup" />, <see cref="T:System.Xml.Schema.XmlSchemaComplexType" />, <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" />, and <see cref="T:System.Xml.Schema.XmlSchemaGroup" />.</summary>
		/// <returns>The elements contained within the redefine element.</returns>
		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06002CF8 RID: 11512 RVA: 0x00107E02 File Offset: 0x00106002
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroup))]
		[XmlElement("complexType", typeof(XmlSchemaComplexType))]
		[XmlElement("annotation", typeof(XmlSchemaAnnotation))]
		[XmlElement("group", typeof(XmlSchemaGroup))]
		public XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> , for all attributes in the schema, which holds the post-compilation value of the AttributeGroups property.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> for all attributes in the schema. The post-compilation value of the AttributeGroups property.</returns>
		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06002CF9 RID: 11513 RVA: 0x00107E0A File Offset: 0x0010600A
		[XmlIgnore]
		public XmlSchemaObjectTable AttributeGroups
		{
			get
			{
				return this.attributeGroups;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />, for all simple and complex types in the schema, which holds the post-compilation value of the SchemaTypes property.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> for all schema types in the schema. The post-compilation value of the SchemaTypes property.</returns>
		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06002CFA RID: 11514 RVA: 0x00107E12 File Offset: 0x00106012
		[XmlIgnore]
		public XmlSchemaObjectTable SchemaTypes
		{
			get
			{
				return this.types;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />, for all groups in the schema, which holds the post-compilation value of the Groups property.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> for all groups in the schema. The post-compilation value of the Groups property.</returns>
		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06002CFB RID: 11515 RVA: 0x00107E1A File Offset: 0x0010601A
		[XmlIgnore]
		public XmlSchemaObjectTable Groups
		{
			get
			{
				return this.groups;
			}
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x00107E22 File Offset: 0x00106022
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.items.Add(annotation);
		}

		// Token: 0x04001DFA RID: 7674
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();

		// Token: 0x04001DFB RID: 7675
		private XmlSchemaObjectTable attributeGroups = new XmlSchemaObjectTable();

		// Token: 0x04001DFC RID: 7676
		private XmlSchemaObjectTable types = new XmlSchemaObjectTable();

		// Token: 0x04001DFD RID: 7677
		private XmlSchemaObjectTable groups = new XmlSchemaObjectTable();
	}
}
