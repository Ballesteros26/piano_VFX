using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the element element from XML Schema as specified by the World Wide Web Consortium (W3C). This class is the base class for all particle types and is used to describe an element in an XML document.</summary>
	// Token: 0x0200044E RID: 1102
	public class XmlSchemaElement : XmlSchemaParticle
	{
		/// <summary>Gets or sets information to indicate if the element can be used in an instance document.</summary>
		/// <returns>If true, the element cannot appear in the instance document. The default is false.Optional.</returns>
		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06002BC9 RID: 11209 RVA: 0x00106708 File Offset: 0x00104908
		// (set) Token: 0x06002BCA RID: 11210 RVA: 0x00106710 File Offset: 0x00104910
		[DefaultValue(false)]
		[XmlAttribute("abstract")]
		public bool IsAbstract
		{
			get
			{
				return this.isAbstract;
			}
			set
			{
				this.isAbstract = value;
				this.hasAbstractAttribute = true;
			}
		}

		/// <summary>Gets or sets a Block derivation.</summary>
		/// <returns>The attribute used to block a type derivation. Default value is XmlSchemaDerivationMethod.None.Optional.</returns>
		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06002BCB RID: 11211 RVA: 0x00106720 File Offset: 0x00104920
		// (set) Token: 0x06002BCC RID: 11212 RVA: 0x00106728 File Offset: 0x00104928
		[XmlAttribute("block")]
		[DefaultValue(XmlSchemaDerivationMethod.None)]
		public XmlSchemaDerivationMethod Block
		{
			get
			{
				return this.block;
			}
			set
			{
				this.block = value;
			}
		}

		/// <summary>Gets or sets the default value of the element if its content is a simple type or content of the element is textOnly.</summary>
		/// <returns>The default value for the element. The default is a null reference.Optional.</returns>
		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06002BCD RID: 11213 RVA: 0x00106731 File Offset: 0x00104931
		// (set) Token: 0x06002BCE RID: 11214 RVA: 0x00106739 File Offset: 0x00104939
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

		/// <summary>Gets or sets the Final property to indicate that no further derivations are allowed.</summary>
		/// <returns>The Final property. The default is XmlSchemaDerivationMethod.None.Optional.</returns>
		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06002BCF RID: 11215 RVA: 0x00106742 File Offset: 0x00104942
		// (set) Token: 0x06002BD0 RID: 11216 RVA: 0x0010674A File Offset: 0x0010494A
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

		/// <summary>Gets or sets the fixed value.</summary>
		/// <returns>The fixed value that is predetermined and unchangeable. The default is a null reference.Optional.</returns>
		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06002BD1 RID: 11217 RVA: 0x00106753 File Offset: 0x00104953
		// (set) Token: 0x06002BD2 RID: 11218 RVA: 0x0010675B File Offset: 0x0010495B
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

		/// <summary>Gets or sets the form for the element.</summary>
		/// <returns>The form for the element. The default is the <see cref="P:System.Xml.Schema.XmlSchema.ElementFormDefault" /> value.Optional.</returns>
		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06002BD3 RID: 11219 RVA: 0x00106764 File Offset: 0x00104964
		// (set) Token: 0x06002BD4 RID: 11220 RVA: 0x0010676C File Offset: 0x0010496C
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

		/// <summary>Gets or sets the name of the element.</summary>
		/// <returns>The name of the element. The default is String.Empty.</returns>
		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06002BD5 RID: 11221 RVA: 0x00106775 File Offset: 0x00104975
		// (set) Token: 0x06002BD6 RID: 11222 RVA: 0x0010677D File Offset: 0x0010497D
		[XmlAttribute("name")]
		[DefaultValue("")]
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

		/// <summary>Gets or sets information that indicates if xsi:nil can occur in the instance data. Indicates if an explicit nil value can be assigned to the element.</summary>
		/// <returns>If nillable is true, this enables an instance of the element to have the nil attribute set to true. The nil attribute is defined as part of the XML Schema namespace for instances. The default is false.Optional.</returns>
		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06002BD7 RID: 11223 RVA: 0x00106786 File Offset: 0x00104986
		// (set) Token: 0x06002BD8 RID: 11224 RVA: 0x0010678E File Offset: 0x0010498E
		[DefaultValue(false)]
		[XmlAttribute("nillable")]
		public bool IsNillable
		{
			get
			{
				return this.isNillable;
			}
			set
			{
				this.isNillable = value;
				this.hasNillableAttribute = true;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06002BD9 RID: 11225 RVA: 0x0010679E File Offset: 0x0010499E
		[XmlIgnore]
		internal bool HasNillableAttribute
		{
			get
			{
				return this.hasNillableAttribute;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06002BDA RID: 11226 RVA: 0x001067A6 File Offset: 0x001049A6
		[XmlIgnore]
		internal bool HasAbstractAttribute
		{
			get
			{
				return this.hasAbstractAttribute;
			}
		}

		/// <summary>Gets or sets the reference name of an element declared in this schema (or another schema indicated by the specified namespace).</summary>
		/// <returns>The reference name of the element.</returns>
		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06002BDB RID: 11227 RVA: 0x001067AE File Offset: 0x001049AE
		// (set) Token: 0x06002BDC RID: 11228 RVA: 0x001067B6 File Offset: 0x001049B6
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

		/// <summary>Gets or sets the name of an element that is being substituted by this element.</summary>
		/// <returns>The qualified name of an element that is being substituted by this element.Optional.</returns>
		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002BDD RID: 11229 RVA: 0x001067CF File Offset: 0x001049CF
		// (set) Token: 0x06002BDE RID: 11230 RVA: 0x001067D7 File Offset: 0x001049D7
		[XmlAttribute("substitutionGroup")]
		public XmlQualifiedName SubstitutionGroup
		{
			get
			{
				return this.substitutionGroup;
			}
			set
			{
				this.substitutionGroup = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		/// <summary>Gets or sets the name of a built-in data type defined in this schema or another schema indicated by the specified namespace.</summary>
		/// <returns>The name of the built-in data type.</returns>
		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06002BDF RID: 11231 RVA: 0x001067F0 File Offset: 0x001049F0
		// (set) Token: 0x06002BE0 RID: 11232 RVA: 0x001067F8 File Offset: 0x001049F8
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

		/// <summary>Gets or sets the type of the element. This can either be a complex type or a simple type.</summary>
		/// <returns>The type of the element.</returns>
		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06002BE1 RID: 11233 RVA: 0x00106811 File Offset: 0x00104A11
		// (set) Token: 0x06002BE2 RID: 11234 RVA: 0x00106819 File Offset: 0x00104A19
		[XmlElement("complexType", typeof(XmlSchemaComplexType))]
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		public XmlSchemaType SchemaType
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

		/// <summary>Gets the collection of constraints on the element.</summary>
		/// <returns>The collection of constraints.</returns>
		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06002BE3 RID: 11235 RVA: 0x00106822 File Offset: 0x00104A22
		[XmlElement("unique", typeof(XmlSchemaUnique))]
		[XmlElement("keyref", typeof(XmlSchemaKeyref))]
		[XmlElement("key", typeof(XmlSchemaKey))]
		public XmlSchemaObjectCollection Constraints
		{
			get
			{
				if (this.constraints == null)
				{
					this.constraints = new XmlSchemaObjectCollection();
				}
				return this.constraints;
			}
		}

		/// <summary>Gets the actual qualified name for the given element. </summary>
		/// <returns>The qualified name of the element. The post-compilation value of the QualifiedName property.</returns>
		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06002BE4 RID: 11236 RVA: 0x0010683D File Offset: 0x00104A3D
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		/// <summary>Gets a common language runtime (CLR) object based on the <see cref="T:System.Xml.Schema.XmlSchemaElement" /> or <see cref="T:System.Xml.Schema.XmlSchemaElement" /> of the element, which holds the post-compilation value of the ElementType property.</summary>
		/// <returns>The common language runtime object. The post-compilation value of the ElementType property.</returns>
		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06002BE5 RID: 11237 RVA: 0x00106845 File Offset: 0x00104A45
		[XmlIgnore]
		[Obsolete("This property has been deprecated. Please use ElementSchemaType property that returns a strongly typed element type. http://go.microsoft.com/fwlink/?linkid=14202")]
		public object ElementType
		{
			get
			{
				if (this.elementType == null)
				{
					return null;
				}
				if (this.elementType.QualifiedName.Namespace == "http://www.w3.org/2001/XMLSchema")
				{
					return this.elementType.Datatype;
				}
				return this.elementType;
			}
		}

		/// <summary>Gets an <see cref="T:System.Xml.Schema.XmlSchemaType" /> object representing the type of the element based on the <see cref="P:System.Xml.Schema.XmlSchemaElement.SchemaType" /> or <see cref="P:System.Xml.Schema.XmlSchemaElement.SchemaTypeName" /> values of the element.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaType" /> object.</returns>
		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x0010687F File Offset: 0x00104A7F
		[XmlIgnore]
		public XmlSchemaType ElementSchemaType
		{
			get
			{
				return this.elementType;
			}
		}

		/// <summary>Gets the post-compilation value of the Block property.</summary>
		/// <returns>The post-compilation value of the Block property. The default is the BlockDefault value on the schema element.</returns>
		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06002BE7 RID: 11239 RVA: 0x00106887 File Offset: 0x00104A87
		[XmlIgnore]
		public XmlSchemaDerivationMethod BlockResolved
		{
			get
			{
				return this.blockResolved;
			}
		}

		/// <summary>Gets the post-compilation value of the Final property.</summary>
		/// <returns>The post-compilation value of the Final property. Default value is the FinalDefault value on the schema element.</returns>
		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06002BE8 RID: 11240 RVA: 0x0010688F File Offset: 0x00104A8F
		[XmlIgnore]
		public XmlSchemaDerivationMethod FinalResolved
		{
			get
			{
				return this.finalResolved;
			}
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x00106898 File Offset: 0x00104A98
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

		// Token: 0x06002BEA RID: 11242 RVA: 0x001068CF File Offset: 0x00104ACF
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qualifiedName = value;
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x001068D8 File Offset: 0x00104AD8
		internal void SetElementType(XmlSchemaType value)
		{
			this.elementType = value;
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x001068E1 File Offset: 0x00104AE1
		internal void SetBlockResolved(XmlSchemaDerivationMethod value)
		{
			this.blockResolved = value;
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x001068EA File Offset: 0x00104AEA
		internal void SetFinalResolved(XmlSchemaDerivationMethod value)
		{
			this.finalResolved = value;
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06002BEE RID: 11246 RVA: 0x001068F3 File Offset: 0x00104AF3
		[XmlIgnore]
		internal bool HasDefault
		{
			get
			{
				return this.defaultValue != null && this.defaultValue.Length > 0;
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06002BEF RID: 11247 RVA: 0x0010690D File Offset: 0x00104B0D
		internal bool HasConstraints
		{
			get
			{
				return this.constraints != null && this.constraints.Count > 0;
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06002BF0 RID: 11248 RVA: 0x00106927 File Offset: 0x00104B27
		// (set) Token: 0x06002BF1 RID: 11249 RVA: 0x0010692F File Offset: 0x00104B2F
		internal bool IsLocalTypeDerivationChecked
		{
			get
			{
				return this.isLocalTypeDerivationChecked;
			}
			set
			{
				this.isLocalTypeDerivationChecked = value;
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06002BF2 RID: 11250 RVA: 0x00106938 File Offset: 0x00104B38
		// (set) Token: 0x06002BF3 RID: 11251 RVA: 0x00106940 File Offset: 0x00104B40
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

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06002BF4 RID: 11252 RVA: 0x00106949 File Offset: 0x00104B49
		// (set) Token: 0x06002BF5 RID: 11253 RVA: 0x00106951 File Offset: 0x00104B51
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

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06002BF6 RID: 11254 RVA: 0x0010695A File Offset: 0x00104B5A
		[XmlIgnore]
		internal override string NameString
		{
			get
			{
				return this.qualifiedName.ToString();
			}
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x00106967 File Offset: 0x00104B67
		internal override XmlSchemaObject Clone()
		{
			return this.Clone(null);
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x00106970 File Offset: 0x00104B70
		internal XmlSchemaObject Clone(XmlSchema parentSchema)
		{
			XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)base.MemberwiseClone();
			xmlSchemaElement.refName = this.refName.Clone();
			xmlSchemaElement.substitutionGroup = this.substitutionGroup.Clone();
			xmlSchemaElement.typeName = this.typeName.Clone();
			xmlSchemaElement.qualifiedName = this.qualifiedName.Clone();
			XmlSchemaComplexType xmlSchemaComplexType = this.type as XmlSchemaComplexType;
			if (xmlSchemaComplexType != null && xmlSchemaComplexType.QualifiedName.IsEmpty)
			{
				xmlSchemaElement.type = (XmlSchemaType)xmlSchemaComplexType.Clone(parentSchema);
			}
			xmlSchemaElement.constraints = null;
			return xmlSchemaElement;
		}

		// Token: 0x04001D82 RID: 7554
		private bool isAbstract;

		// Token: 0x04001D83 RID: 7555
		private bool hasAbstractAttribute;

		// Token: 0x04001D84 RID: 7556
		private bool isNillable;

		// Token: 0x04001D85 RID: 7557
		private bool hasNillableAttribute;

		// Token: 0x04001D86 RID: 7558
		private bool isLocalTypeDerivationChecked;

		// Token: 0x04001D87 RID: 7559
		private XmlSchemaDerivationMethod block = XmlSchemaDerivationMethod.None;

		// Token: 0x04001D88 RID: 7560
		private XmlSchemaDerivationMethod final = XmlSchemaDerivationMethod.None;

		// Token: 0x04001D89 RID: 7561
		private XmlSchemaForm form;

		// Token: 0x04001D8A RID: 7562
		private string defaultValue;

		// Token: 0x04001D8B RID: 7563
		private string fixedValue;

		// Token: 0x04001D8C RID: 7564
		private string name;

		// Token: 0x04001D8D RID: 7565
		private XmlQualifiedName refName = XmlQualifiedName.Empty;

		// Token: 0x04001D8E RID: 7566
		private XmlQualifiedName substitutionGroup = XmlQualifiedName.Empty;

		// Token: 0x04001D8F RID: 7567
		private XmlQualifiedName typeName = XmlQualifiedName.Empty;

		// Token: 0x04001D90 RID: 7568
		private XmlSchemaType type;

		// Token: 0x04001D91 RID: 7569
		private XmlQualifiedName qualifiedName = XmlQualifiedName.Empty;

		// Token: 0x04001D92 RID: 7570
		private XmlSchemaType elementType;

		// Token: 0x04001D93 RID: 7571
		private XmlSchemaDerivationMethod blockResolved;

		// Token: 0x04001D94 RID: 7572
		private XmlSchemaDerivationMethod finalResolved;

		// Token: 0x04001D95 RID: 7573
		private XmlSchemaObjectCollection constraints;

		// Token: 0x04001D96 RID: 7574
		private SchemaElementDecl elementDecl;
	}
}
