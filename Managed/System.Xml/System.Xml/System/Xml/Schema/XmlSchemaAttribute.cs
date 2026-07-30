using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the attribute element from the XML Schema as specified by the World Wide Web Consortium (W3C). Attributes provide additional information for other document elements. The attribute tag is nested between the tags of a document's element for the schema. The XML document displays attributes as named items in the opening tag of an element.</summary>
	// Token: 0x0200043B RID: 1083
	public class XmlSchemaAttribute : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the default value for the attribute.</summary>
		/// <returns>The default value for the attribute. The default is a null reference.Optional.</returns>
		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06002AEF RID: 10991 RVA: 0x00104D5F File Offset: 0x00102F5F
		// (set) Token: 0x06002AF0 RID: 10992 RVA: 0x00104D67 File Offset: 0x00102F67
		[DefaultValue(null)]
		[XmlAttribute("default")]
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.defaultValue = value;
			}
		}

		/// <summary>Gets or sets the fixed value for the attribute.</summary>
		/// <returns>The fixed value for the attribute. The default is null.Optional.</returns>
		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06002AF1 RID: 10993 RVA: 0x00104D70 File Offset: 0x00102F70
		// (set) Token: 0x06002AF2 RID: 10994 RVA: 0x00104D78 File Offset: 0x00102F78
		[XmlAttribute("fixed")]
		[DefaultValue(null)]
		public string FixedValue
		{
			get
			{
				return this.fixedValue;
			}
			set
			{
				this.fixedValue = value;
			}
		}

		/// <summary>Gets or sets the form for the attribute.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlSchemaForm" /> values. The default is the value of the <see cref="P:System.Xml.Schema.XmlSchema.AttributeFormDefault" /> of the schema element containing the attribute.Optional.</returns>
		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06002AF3 RID: 10995 RVA: 0x00104D81 File Offset: 0x00102F81
		// (set) Token: 0x06002AF4 RID: 10996 RVA: 0x00104D89 File Offset: 0x00102F89
		[XmlAttribute("form")]
		[DefaultValue(XmlSchemaForm.None)]
		public XmlSchemaForm Form
		{
			get
			{
				return this.form;
			}
			set
			{
				this.form = value;
			}
		}

		/// <summary>Gets or sets the name of the attribute.</summary>
		/// <returns>The name of the attribute.</returns>
		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06002AF5 RID: 10997 RVA: 0x00104D92 File Offset: 0x00102F92
		// (set) Token: 0x06002AF6 RID: 10998 RVA: 0x00104D9A File Offset: 0x00102F9A
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

		/// <summary>Gets or sets the name of an attribute declared in this schema (or another schema indicated by the specified namespace).</summary>
		/// <returns>The name of the attribute declared.</returns>
		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06002AF7 RID: 10999 RVA: 0x00104DA3 File Offset: 0x00102FA3
		// (set) Token: 0x06002AF8 RID: 11000 RVA: 0x00104DAB File Offset: 0x00102FAB
		[XmlAttribute("ref")]
		public XmlQualifiedName RefName
		{
			get
			{
				return this.refName;
			}
			set
			{
				this.refName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		/// <summary>Gets or sets the name of the simple type defined in this schema (or another schema indicated by the specified namespace).</summary>
		/// <returns>The name of the simple type.</returns>
		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06002AF9 RID: 11001 RVA: 0x00104DC4 File Offset: 0x00102FC4
		// (set) Token: 0x06002AFA RID: 11002 RVA: 0x00104DCC File Offset: 0x00102FCC
		[XmlAttribute("type")]
		public XmlQualifiedName SchemaTypeName
		{
			get
			{
				return this.typeName;
			}
			set
			{
				this.typeName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		/// <summary>Gets or sets the attribute type to a simple type.</summary>
		/// <returns>The simple type defined in this schema.</returns>
		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06002AFB RID: 11003 RVA: 0x00104DE5 File Offset: 0x00102FE5
		// (set) Token: 0x06002AFC RID: 11004 RVA: 0x00104DED File Offset: 0x00102FED
		[XmlElement("simpleType")]
		public XmlSchemaSimpleType SchemaType
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		/// <summary>Gets or sets information about how the attribute is used.</summary>
		/// <returns>One of the following values: None, Prohibited, Optional, or Required. The default is Optional.Optional.</returns>
		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06002AFD RID: 11005 RVA: 0x00104DF6 File Offset: 0x00102FF6
		// (set) Token: 0x06002AFE RID: 11006 RVA: 0x00104DFE File Offset: 0x00102FFE
		[XmlAttribute("use")]
		[DefaultValue(XmlSchemaUse.None)]
		public XmlSchemaUse Use
		{
			get
			{
				return this.use;
			}
			set
			{
				this.use = value;
			}
		}

		/// <summary>Gets the qualified name for the attribute.</summary>
		/// <returns>The post-compilation value of the QualifiedName property.</returns>
		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06002AFF RID: 11007 RVA: 0x00104E07 File Offset: 0x00103007
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		/// <summary>Gets the common language runtime (CLR) object based on the <see cref="P:System.Xml.Schema.XmlSchemaAttribute.SchemaType" /> or <see cref="P:System.Xml.Schema.XmlSchemaAttribute.SchemaTypeName" /> of the attribute that holds the post-compilation value of the AttributeType property.</summary>
		/// <returns>The common runtime library (CLR) object that holds the post-compilation value of the AttributeType property.</returns>
		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06002B00 RID: 11008 RVA: 0x00104E0F File Offset: 0x0010300F
		[Obsolete("This property has been deprecated. Please use AttributeSchemaType property that returns a strongly typed attribute type. http://go.microsoft.com/fwlink/?linkid=14202")]
		[XmlIgnore]
		public object AttributeType
		{
			get
			{
				if (this.attributeType == null)
				{
					return null;
				}
				if (this.attributeType.QualifiedName.Namespace == "http://www.w3.org/2001/XMLSchema")
				{
					return this.attributeType.Datatype;
				}
				return this.attributeType;
			}
		}

		/// <summary>Gets an <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> object representing the type of the attribute based on the <see cref="P:System.Xml.Schema.XmlSchemaAttribute.SchemaType" /> or <see cref="P:System.Xml.Schema.XmlSchemaAttribute.SchemaTypeName" /> of the attribute.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> object.</returns>
		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06002B01 RID: 11009 RVA: 0x00104E49 File Offset: 0x00103049
		[XmlIgnore]
		public XmlSchemaSimpleType AttributeSchemaType
		{
			get
			{
				return this.attributeType;
			}
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x00104E54 File Offset: 0x00103054
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

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06002B03 RID: 11011 RVA: 0x00104E8B File Offset: 0x0010308B
		[XmlIgnore]
		internal XmlSchemaDatatype Datatype
		{
			get
			{
				if (this.attributeType != null)
				{
					return this.attributeType.Datatype;
				}
				return null;
			}
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x00104EA2 File Offset: 0x001030A2
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qualifiedName = value;
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x00104EAB File Offset: 0x001030AB
		internal void SetAttributeType(XmlSchemaSimpleType value)
		{
			this.attributeType = value;
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06002B06 RID: 11014 RVA: 0x00104EB4 File Offset: 0x001030B4
		// (set) Token: 0x06002B07 RID: 11015 RVA: 0x00104EBC File Offset: 0x001030BC
		internal SchemaAttDef AttDef
		{
			get
			{
				return this.attDef;
			}
			set
			{
				this.attDef = value;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06002B08 RID: 11016 RVA: 0x00104EC5 File Offset: 0x001030C5
		internal bool HasDefault
		{
			get
			{
				return this.defaultValue != null;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06002B09 RID: 11017 RVA: 0x00104ED0 File Offset: 0x001030D0
		// (set) Token: 0x06002B0A RID: 11018 RVA: 0x00104ED8 File Offset: 0x001030D8
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

		// Token: 0x06002B0B RID: 11019 RVA: 0x00104EE1 File Offset: 0x001030E1
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)base.MemberwiseClone();
			xmlSchemaAttribute.refName = this.refName.Clone();
			xmlSchemaAttribute.typeName = this.typeName.Clone();
			xmlSchemaAttribute.qualifiedName = this.qualifiedName.Clone();
			return xmlSchemaAttribute;
		}

		// Token: 0x04001D2D RID: 7469
		private string defaultValue;

		// Token: 0x04001D2E RID: 7470
		private string fixedValue;

		// Token: 0x04001D2F RID: 7471
		private string name;

		// Token: 0x04001D30 RID: 7472
		private XmlSchemaForm form;

		// Token: 0x04001D31 RID: 7473
		private XmlSchemaUse use;

		// Token: 0x04001D32 RID: 7474
		private XmlQualifiedName refName = XmlQualifiedName.Empty;

		// Token: 0x04001D33 RID: 7475
		private XmlQualifiedName typeName = XmlQualifiedName.Empty;

		// Token: 0x04001D34 RID: 7476
		private XmlQualifiedName qualifiedName = XmlQualifiedName.Empty;

		// Token: 0x04001D35 RID: 7477
		private XmlSchemaSimpleType type;

		// Token: 0x04001D36 RID: 7478
		private XmlSchemaSimpleType attributeType;

		// Token: 0x04001D37 RID: 7479
		private SchemaAttDef attDef;
	}
}
