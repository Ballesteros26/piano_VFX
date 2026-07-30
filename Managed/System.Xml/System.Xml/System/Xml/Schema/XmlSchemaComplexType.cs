using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the complexType element from XML Schema as specified by the World Wide Web Consortium (W3C). This class defines a complex type that determines the set of attributes and content of an element.</summary>
	// Token: 0x02000446 RID: 1094
	public class XmlSchemaComplexType : XmlSchemaType
	{
		// Token: 0x06002B70 RID: 11120 RVA: 0x00105810 File Offset: 0x00103A10
		static XmlSchemaComplexType()
		{
			XmlSchemaComplexType.untypedAnyType.SetQualifiedName(new XmlQualifiedName("untypedAny", "http://www.w3.org/2003/11/xpath-datatypes"));
			XmlSchemaComplexType.untypedAnyType.IsMixed = true;
			XmlSchemaComplexType.untypedAnyType.SetContentTypeParticle(XmlSchemaComplexType.anyTypeLax.ContentTypeParticle);
			XmlSchemaComplexType.untypedAnyType.SetContentType(XmlSchemaContentType.Mixed);
			XmlSchemaComplexType.untypedAnyType.ElementDecl = SchemaElementDecl.CreateAnyTypeElementDecl();
			XmlSchemaComplexType.untypedAnyType.ElementDecl.SchemaType = XmlSchemaComplexType.untypedAnyType;
			XmlSchemaComplexType.untypedAnyType.ElementDecl.ContentValidator = XmlSchemaComplexType.AnyTypeContentValidator;
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x001058B8 File Offset: 0x00103AB8
		private static XmlSchemaComplexType CreateAnyType(XmlSchemaContentProcessing processContents)
		{
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			xmlSchemaComplexType.SetQualifiedName(DatatypeImplementation.QnAnyType);
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.MinOccurs = 0m;
			xmlSchemaAny.MaxOccurs = decimal.MaxValue;
			xmlSchemaAny.ProcessContents = processContents;
			xmlSchemaAny.BuildNamespaceList(null);
			xmlSchemaComplexType.SetContentTypeParticle(new XmlSchemaSequence
			{
				Items = { xmlSchemaAny }
			});
			xmlSchemaComplexType.SetContentType(XmlSchemaContentType.Mixed);
			xmlSchemaComplexType.ElementDecl = SchemaElementDecl.CreateAnyTypeElementDecl();
			xmlSchemaComplexType.ElementDecl.SchemaType = xmlSchemaComplexType;
			ParticleContentValidator particleContentValidator = new ParticleContentValidator(XmlSchemaContentType.Mixed);
			particleContentValidator.Start();
			particleContentValidator.OpenGroup();
			particleContentValidator.AddNamespaceList(xmlSchemaAny.NamespaceList, xmlSchemaAny);
			particleContentValidator.AddStar();
			particleContentValidator.CloseGroup();
			ContentValidator contentValidator = particleContentValidator.Finish(true);
			xmlSchemaComplexType.ElementDecl.ContentValidator = contentValidator;
			XmlSchemaAnyAttribute xmlSchemaAnyAttribute = new XmlSchemaAnyAttribute();
			xmlSchemaAnyAttribute.ProcessContents = processContents;
			xmlSchemaAnyAttribute.BuildNamespaceList(null);
			xmlSchemaComplexType.SetAttributeWildcard(xmlSchemaAnyAttribute);
			xmlSchemaComplexType.ElementDecl.AnyAttribute = xmlSchemaAnyAttribute;
			return xmlSchemaComplexType;
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06002B73 RID: 11123 RVA: 0x001059C6 File Offset: 0x00103BC6
		[XmlIgnore]
		internal static XmlSchemaComplexType AnyType
		{
			get
			{
				return XmlSchemaComplexType.anyTypeLax;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06002B74 RID: 11124 RVA: 0x001059CD File Offset: 0x00103BCD
		[XmlIgnore]
		internal static XmlSchemaComplexType UntypedAnyType
		{
			get
			{
				return XmlSchemaComplexType.untypedAnyType;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06002B75 RID: 11125 RVA: 0x001059D4 File Offset: 0x00103BD4
		[XmlIgnore]
		internal static XmlSchemaComplexType AnyTypeSkip
		{
			get
			{
				return XmlSchemaComplexType.anyTypeSkip;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06002B76 RID: 11126 RVA: 0x001059DB File Offset: 0x00103BDB
		internal static ContentValidator AnyTypeContentValidator
		{
			get
			{
				return XmlSchemaComplexType.anyTypeLax.ElementDecl.ContentValidator;
			}
		}

		/// <summary>Gets or sets the information that determines if the complexType element can be used in the instance document.</summary>
		/// <returns>If true, an element cannot use this complexType element directly and must use a complex type that is derived from this complexType element. The default is false.Optional.</returns>
		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06002B77 RID: 11127 RVA: 0x001059EC File Offset: 0x00103BEC
		// (set) Token: 0x06002B78 RID: 11128 RVA: 0x001059F9 File Offset: 0x00103BF9
		[XmlAttribute("abstract")]
		[DefaultValue(false)]
		public bool IsAbstract
		{
			get
			{
				return (this.pvFlags & 4) > 0;
			}
			set
			{
				if (value)
				{
					this.pvFlags |= 4;
					return;
				}
				this.pvFlags = (byte)((int)this.pvFlags & -5);
			}
		}

		/// <summary>Gets or sets the block attribute.</summary>
		/// <returns>The block attribute prevents a complex type from being used in the specified type of derivation. The default is XmlSchemaDerivationMethod.None.Optional.</returns>
		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06002B79 RID: 11129 RVA: 0x00105A1E File Offset: 0x00103C1E
		// (set) Token: 0x06002B7A RID: 11130 RVA: 0x00105A26 File Offset: 0x00103C26
		[DefaultValue(XmlSchemaDerivationMethod.None)]
		[XmlAttribute("block")]
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

		/// <summary>Gets or sets information that determines if the complex type has a mixed content model (markup within the content).</summary>
		/// <returns>true, if character data can appear between child elements of this complex type; otherwise, false. The default is false.Optional.</returns>
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06002B7B RID: 11131 RVA: 0x00105A2F File Offset: 0x00103C2F
		// (set) Token: 0x06002B7C RID: 11132 RVA: 0x00105A3C File Offset: 0x00103C3C
		[DefaultValue(false)]
		[XmlAttribute("mixed")]
		public override bool IsMixed
		{
			get
			{
				return (this.pvFlags & 2) > 0;
			}
			set
			{
				if (value)
				{
					this.pvFlags |= 2;
					return;
				}
				this.pvFlags = (byte)((int)this.pvFlags & -3);
			}
		}

		/// <summary>Gets or sets the post-compilation <see cref="T:System.Xml.Schema.XmlSchemaContentModel" /> of this complex type.</summary>
		/// <returns>The content model type that is one of the <see cref="T:System.Xml.Schema.XmlSchemaSimpleContent" /> or <see cref="T:System.Xml.Schema.XmlSchemaComplexContent" /> classes.</returns>
		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06002B7D RID: 11133 RVA: 0x00105A61 File Offset: 0x00103C61
		// (set) Token: 0x06002B7E RID: 11134 RVA: 0x00105A69 File Offset: 0x00103C69
		[XmlElement("complexContent", typeof(XmlSchemaComplexContent))]
		[XmlElement("simpleContent", typeof(XmlSchemaSimpleContent))]
		public XmlSchemaContentModel ContentModel
		{
			get
			{
				return this.contentModel;
			}
			set
			{
				this.contentModel = value;
			}
		}

		/// <summary>Gets or sets the compositor type as one of the <see cref="T:System.Xml.Schema.XmlSchemaGroupRef" />, <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes.</summary>
		/// <returns>The compositor type.</returns>
		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06002B7F RID: 11135 RVA: 0x00105A72 File Offset: 0x00103C72
		// (set) Token: 0x06002B80 RID: 11136 RVA: 0x00105A7A File Offset: 0x00103C7A
		[XmlElement("group", typeof(XmlSchemaGroupRef))]
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		[XmlElement("all", typeof(XmlSchemaAll))]
		[XmlElement("choice", typeof(XmlSchemaChoice))]
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

		/// <summary>Gets the collection of attributes for the complex type.</summary>
		/// <returns>Contains <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> and <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroupRef" /> classes.</returns>
		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06002B81 RID: 11137 RVA: 0x00105A83 File Offset: 0x00103C83
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new XmlSchemaObjectCollection();
				}
				return this.attributes;
			}
		}

		/// <summary>Gets or sets the value for the <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> component of the complex type.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> component of the complex type.</returns>
		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06002B82 RID: 11138 RVA: 0x00105A9E File Offset: 0x00103C9E
		// (set) Token: 0x06002B83 RID: 11139 RVA: 0x00105AA6 File Offset: 0x00103CA6
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

		/// <summary>Gets the content model of the complex type which holds the post-compilation value.</summary>
		/// <returns>The post-compilation value of the content model for the complex type.</returns>
		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06002B84 RID: 11140 RVA: 0x00105AAF File Offset: 0x00103CAF
		[XmlIgnore]
		public XmlSchemaContentType ContentType
		{
			get
			{
				return base.SchemaContentType;
			}
		}

		/// <summary>Gets the particle that holds the post-compilation value of the <see cref="P:System.Xml.Schema.XmlSchemaComplexType.ContentType" /> particle.</summary>
		/// <returns>The particle for the content type. The post-compilation value of the <see cref="P:System.Xml.Schema.XmlSchemaComplexType.ContentType" /> particle.</returns>
		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06002B85 RID: 11141 RVA: 0x00105AB7 File Offset: 0x00103CB7
		[XmlIgnore]
		public XmlSchemaParticle ContentTypeParticle
		{
			get
			{
				return this.contentTypeParticle;
			}
		}

		/// <summary>Gets the value after the type has been compiled to the post-schema-validation information set (infoset). This value indicates how the type is enforced when xsi:type is used in the instance document.</summary>
		/// <returns>The post-schema-validated infoset value. The default is BlockDefault value on the schema element.</returns>
		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06002B86 RID: 11142 RVA: 0x00105ABF File Offset: 0x00103CBF
		[XmlIgnore]
		public XmlSchemaDerivationMethod BlockResolved
		{
			get
			{
				return this.blockResolved;
			}
		}

		/// <summary>Gets the collection of all the complied attributes of this complex type and its base types.</summary>
		/// <returns>The collection of all the attributes from this complex type and its base types. The post-compilation value of the AttributeUses property.</returns>
		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06002B87 RID: 11143 RVA: 0x00105AC7 File Offset: 0x00103CC7
		[XmlIgnore]
		public XmlSchemaObjectTable AttributeUses
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

		/// <summary>Gets the post-compilation value for anyAttribute for this complex type and its base type(s).</summary>
		/// <returns>The post-compilation value of the anyAttribute element.</returns>
		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06002B88 RID: 11144 RVA: 0x00105AE2 File Offset: 0x00103CE2
		[XmlIgnore]
		public XmlSchemaAnyAttribute AttributeWildcard
		{
			get
			{
				return this.attributeWildcard;
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06002B89 RID: 11145 RVA: 0x00105AEA File Offset: 0x00103CEA
		[XmlIgnore]
		internal XmlSchemaObjectTable LocalElements
		{
			get
			{
				if (this.localElements == null)
				{
					this.localElements = new XmlSchemaObjectTable();
				}
				return this.localElements;
			}
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x00105B05 File Offset: 0x00103D05
		internal void SetContentTypeParticle(XmlSchemaParticle value)
		{
			this.contentTypeParticle = value;
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x00105B0E File Offset: 0x00103D0E
		internal void SetBlockResolved(XmlSchemaDerivationMethod value)
		{
			this.blockResolved = value;
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x00105B17 File Offset: 0x00103D17
		internal void SetAttributeWildcard(XmlSchemaAnyAttribute value)
		{
			this.attributeWildcard = value;
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06002B8D RID: 11149 RVA: 0x00105B20 File Offset: 0x00103D20
		// (set) Token: 0x06002B8E RID: 11150 RVA: 0x00105B2D File Offset: 0x00103D2D
		internal bool HasWildCard
		{
			get
			{
				return (this.pvFlags & 1) > 0;
			}
			set
			{
				if (value)
				{
					this.pvFlags |= 1;
					return;
				}
				this.pvFlags = (byte)((int)this.pvFlags & -2);
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06002B8F RID: 11151 RVA: 0x00105B54 File Offset: 0x00103D54
		internal override XmlQualifiedName DerivedFrom
		{
			get
			{
				if (this.contentModel == null)
				{
					return XmlQualifiedName.Empty;
				}
				if (this.contentModel.Content is XmlSchemaComplexContentRestriction)
				{
					return ((XmlSchemaComplexContentRestriction)this.contentModel.Content).BaseTypeName;
				}
				if (this.contentModel.Content is XmlSchemaComplexContentExtension)
				{
					return ((XmlSchemaComplexContentExtension)this.contentModel.Content).BaseTypeName;
				}
				if (this.contentModel.Content is XmlSchemaSimpleContentRestriction)
				{
					return ((XmlSchemaSimpleContentRestriction)this.contentModel.Content).BaseTypeName;
				}
				if (this.contentModel.Content is XmlSchemaSimpleContentExtension)
				{
					return ((XmlSchemaSimpleContentExtension)this.contentModel.Content).BaseTypeName;
				}
				return XmlQualifiedName.Empty;
			}
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x00105C14 File Offset: 0x00103E14
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x00105C20 File Offset: 0x00103E20
		internal bool ContainsIdAttribute(bool findAll)
		{
			int num = 0;
			foreach (object obj in this.AttributeUses.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj;
				if (xmlSchemaAttribute.Use != XmlSchemaUse.Prohibited)
				{
					XmlSchemaDatatype datatype = xmlSchemaAttribute.Datatype;
					if (datatype != null && datatype.TypeCode == XmlTypeCode.Id)
					{
						num++;
						if (num > 1)
						{
							break;
						}
					}
				}
			}
			if (!findAll)
			{
				return num > 0;
			}
			return num > 1;
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x00105CB0 File Offset: 0x00103EB0
		internal override XmlSchemaObject Clone()
		{
			return this.Clone(null);
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x00105CBC File Offset: 0x00103EBC
		internal XmlSchemaObject Clone(XmlSchema parentSchema)
		{
			XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)base.MemberwiseClone();
			if (xmlSchemaComplexType.ContentModel != null)
			{
				XmlSchemaSimpleContent xmlSchemaSimpleContent = xmlSchemaComplexType.ContentModel as XmlSchemaSimpleContent;
				if (xmlSchemaSimpleContent != null)
				{
					XmlSchemaSimpleContent xmlSchemaSimpleContent2 = (XmlSchemaSimpleContent)xmlSchemaSimpleContent.Clone();
					XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = xmlSchemaSimpleContent.Content as XmlSchemaSimpleContentExtension;
					if (xmlSchemaSimpleContentExtension != null)
					{
						XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension2 = (XmlSchemaSimpleContentExtension)xmlSchemaSimpleContentExtension.Clone();
						xmlSchemaSimpleContentExtension2.BaseTypeName = xmlSchemaSimpleContentExtension.BaseTypeName.Clone();
						xmlSchemaSimpleContentExtension2.SetAttributes(XmlSchemaComplexType.CloneAttributes(xmlSchemaSimpleContentExtension.Attributes));
						xmlSchemaSimpleContent2.Content = xmlSchemaSimpleContentExtension2;
					}
					else
					{
						XmlSchemaSimpleContentRestriction xmlSchemaSimpleContentRestriction = (XmlSchemaSimpleContentRestriction)xmlSchemaSimpleContent.Content;
						XmlSchemaSimpleContentRestriction xmlSchemaSimpleContentRestriction2 = (XmlSchemaSimpleContentRestriction)xmlSchemaSimpleContentRestriction.Clone();
						xmlSchemaSimpleContentRestriction2.BaseTypeName = xmlSchemaSimpleContentRestriction.BaseTypeName.Clone();
						xmlSchemaSimpleContentRestriction2.SetAttributes(XmlSchemaComplexType.CloneAttributes(xmlSchemaSimpleContentRestriction.Attributes));
						xmlSchemaSimpleContent2.Content = xmlSchemaSimpleContentRestriction2;
					}
					xmlSchemaComplexType.ContentModel = xmlSchemaSimpleContent2;
				}
				else
				{
					XmlSchemaComplexContent xmlSchemaComplexContent = (XmlSchemaComplexContent)xmlSchemaComplexType.ContentModel;
					XmlSchemaComplexContent xmlSchemaComplexContent2 = (XmlSchemaComplexContent)xmlSchemaComplexContent.Clone();
					XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = xmlSchemaComplexContent.Content as XmlSchemaComplexContentExtension;
					if (xmlSchemaComplexContentExtension != null)
					{
						XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension2 = (XmlSchemaComplexContentExtension)xmlSchemaComplexContentExtension.Clone();
						xmlSchemaComplexContentExtension2.BaseTypeName = xmlSchemaComplexContentExtension.BaseTypeName.Clone();
						xmlSchemaComplexContentExtension2.SetAttributes(XmlSchemaComplexType.CloneAttributes(xmlSchemaComplexContentExtension.Attributes));
						if (XmlSchemaComplexType.HasParticleRef(xmlSchemaComplexContentExtension.Particle, parentSchema))
						{
							xmlSchemaComplexContentExtension2.Particle = XmlSchemaComplexType.CloneParticle(xmlSchemaComplexContentExtension.Particle, parentSchema);
						}
						xmlSchemaComplexContent2.Content = xmlSchemaComplexContentExtension2;
					}
					else
					{
						XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction = xmlSchemaComplexContent.Content as XmlSchemaComplexContentRestriction;
						XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction2 = (XmlSchemaComplexContentRestriction)xmlSchemaComplexContentRestriction.Clone();
						xmlSchemaComplexContentRestriction2.BaseTypeName = xmlSchemaComplexContentRestriction.BaseTypeName.Clone();
						xmlSchemaComplexContentRestriction2.SetAttributes(XmlSchemaComplexType.CloneAttributes(xmlSchemaComplexContentRestriction.Attributes));
						if (XmlSchemaComplexType.HasParticleRef(xmlSchemaComplexContentRestriction2.Particle, parentSchema))
						{
							xmlSchemaComplexContentRestriction2.Particle = XmlSchemaComplexType.CloneParticle(xmlSchemaComplexContentRestriction2.Particle, parentSchema);
						}
						xmlSchemaComplexContent2.Content = xmlSchemaComplexContentRestriction2;
					}
					xmlSchemaComplexType.ContentModel = xmlSchemaComplexContent2;
				}
			}
			else
			{
				if (XmlSchemaComplexType.HasParticleRef(xmlSchemaComplexType.Particle, parentSchema))
				{
					xmlSchemaComplexType.Particle = XmlSchemaComplexType.CloneParticle(xmlSchemaComplexType.Particle, parentSchema);
				}
				xmlSchemaComplexType.SetAttributes(XmlSchemaComplexType.CloneAttributes(xmlSchemaComplexType.Attributes));
			}
			xmlSchemaComplexType.ClearCompiledState();
			return xmlSchemaComplexType;
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x00105EDC File Offset: 0x001040DC
		private void ClearCompiledState()
		{
			this.attributeUses = null;
			this.localElements = null;
			this.attributeWildcard = null;
			this.contentTypeParticle = XmlSchemaParticle.Empty;
			this.blockResolved = XmlSchemaDerivationMethod.None;
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x00105F0C File Offset: 0x0010410C
		internal static XmlSchemaObjectCollection CloneAttributes(XmlSchemaObjectCollection attributes)
		{
			if (XmlSchemaComplexType.HasAttributeQNameRef(attributes))
			{
				XmlSchemaObjectCollection xmlSchemaObjectCollection = attributes.Clone();
				for (int i = 0; i < attributes.Count; i++)
				{
					XmlSchemaObject xmlSchemaObject = attributes[i];
					XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef = xmlSchemaObject as XmlSchemaAttributeGroupRef;
					if (xmlSchemaAttributeGroupRef != null)
					{
						XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef2 = (XmlSchemaAttributeGroupRef)xmlSchemaAttributeGroupRef.Clone();
						xmlSchemaAttributeGroupRef2.RefName = xmlSchemaAttributeGroupRef.RefName.Clone();
						xmlSchemaObjectCollection[i] = xmlSchemaAttributeGroupRef2;
					}
					else
					{
						XmlSchemaAttribute xmlSchemaAttribute = xmlSchemaObject as XmlSchemaAttribute;
						if (!xmlSchemaAttribute.RefName.IsEmpty || !xmlSchemaAttribute.SchemaTypeName.IsEmpty)
						{
							xmlSchemaObjectCollection[i] = xmlSchemaAttribute.Clone();
						}
					}
				}
				return xmlSchemaObjectCollection;
			}
			return attributes;
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x00105FB4 File Offset: 0x001041B4
		private static XmlSchemaObjectCollection CloneGroupBaseParticles(XmlSchemaObjectCollection groupBaseParticles, XmlSchema parentSchema)
		{
			XmlSchemaObjectCollection xmlSchemaObjectCollection = groupBaseParticles.Clone();
			for (int i = 0; i < groupBaseParticles.Count; i++)
			{
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)groupBaseParticles[i];
				xmlSchemaObjectCollection[i] = XmlSchemaComplexType.CloneParticle(xmlSchemaParticle, parentSchema);
			}
			return xmlSchemaObjectCollection;
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x00105FF8 File Offset: 0x001041F8
		internal static XmlSchemaParticle CloneParticle(XmlSchemaParticle particle, XmlSchema parentSchema)
		{
			XmlSchemaGroupBase xmlSchemaGroupBase = particle as XmlSchemaGroupBase;
			if (xmlSchemaGroupBase != null)
			{
				XmlSchemaObjectCollection xmlSchemaObjectCollection = XmlSchemaComplexType.CloneGroupBaseParticles(xmlSchemaGroupBase.Items, parentSchema);
				XmlSchemaGroupBase xmlSchemaGroupBase2 = (XmlSchemaGroupBase)xmlSchemaGroupBase.Clone();
				xmlSchemaGroupBase2.SetItems(xmlSchemaObjectCollection);
				return xmlSchemaGroupBase2;
			}
			if (particle is XmlSchemaGroupRef)
			{
				XmlSchemaGroupRef xmlSchemaGroupRef = (XmlSchemaGroupRef)particle.Clone();
				xmlSchemaGroupRef.RefName = xmlSchemaGroupRef.RefName.Clone();
				return xmlSchemaGroupRef;
			}
			XmlSchemaElement xmlSchemaElement = particle as XmlSchemaElement;
			if (xmlSchemaElement != null && (!xmlSchemaElement.RefName.IsEmpty || !xmlSchemaElement.SchemaTypeName.IsEmpty || XmlSchemaComplexType.GetResolvedElementForm(parentSchema, xmlSchemaElement) == XmlSchemaForm.Qualified))
			{
				return (XmlSchemaElement)xmlSchemaElement.Clone(parentSchema);
			}
			return particle;
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x00106090 File Offset: 0x00104290
		private static XmlSchemaForm GetResolvedElementForm(XmlSchema parentSchema, XmlSchemaElement element)
		{
			if (element.Form == XmlSchemaForm.None && parentSchema != null)
			{
				return parentSchema.ElementFormDefault;
			}
			return element.Form;
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x001060AC File Offset: 0x001042AC
		internal static bool HasParticleRef(XmlSchemaParticle particle, XmlSchema parentSchema)
		{
			XmlSchemaGroupBase xmlSchemaGroupBase = particle as XmlSchemaGroupBase;
			if (xmlSchemaGroupBase != null)
			{
				bool flag = false;
				int num = 0;
				while (num < xmlSchemaGroupBase.Items.Count && !flag)
				{
					XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)xmlSchemaGroupBase.Items[num++];
					if (xmlSchemaParticle is XmlSchemaGroupRef)
					{
						flag = true;
					}
					else
					{
						XmlSchemaElement xmlSchemaElement = xmlSchemaParticle as XmlSchemaElement;
						flag = (xmlSchemaElement != null && (!xmlSchemaElement.RefName.IsEmpty || !xmlSchemaElement.SchemaTypeName.IsEmpty || XmlSchemaComplexType.GetResolvedElementForm(parentSchema, xmlSchemaElement) == XmlSchemaForm.Qualified)) || XmlSchemaComplexType.HasParticleRef(xmlSchemaParticle, parentSchema);
					}
				}
				return flag;
			}
			return particle is XmlSchemaGroupRef;
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x00106148 File Offset: 0x00104348
		internal static bool HasAttributeQNameRef(XmlSchemaObjectCollection attributes)
		{
			for (int i = 0; i < attributes.Count; i++)
			{
				if (attributes[i] is XmlSchemaAttributeGroupRef)
				{
					return true;
				}
				XmlSchemaAttribute xmlSchemaAttribute = attributes[i] as XmlSchemaAttribute;
				if (!xmlSchemaAttribute.RefName.IsEmpty || !xmlSchemaAttribute.SchemaTypeName.IsEmpty)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001D5A RID: 7514
		private XmlSchemaDerivationMethod block = XmlSchemaDerivationMethod.None;

		// Token: 0x04001D5B RID: 7515
		private XmlSchemaContentModel contentModel;

		// Token: 0x04001D5C RID: 7516
		private XmlSchemaParticle particle;

		// Token: 0x04001D5D RID: 7517
		private XmlSchemaObjectCollection attributes;

		// Token: 0x04001D5E RID: 7518
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04001D5F RID: 7519
		private XmlSchemaParticle contentTypeParticle = XmlSchemaParticle.Empty;

		// Token: 0x04001D60 RID: 7520
		private XmlSchemaDerivationMethod blockResolved;

		// Token: 0x04001D61 RID: 7521
		private XmlSchemaObjectTable localElements;

		// Token: 0x04001D62 RID: 7522
		private XmlSchemaObjectTable attributeUses;

		// Token: 0x04001D63 RID: 7523
		private XmlSchemaAnyAttribute attributeWildcard;

		// Token: 0x04001D64 RID: 7524
		private static XmlSchemaComplexType anyTypeLax = XmlSchemaComplexType.CreateAnyType(XmlSchemaContentProcessing.Lax);

		// Token: 0x04001D65 RID: 7525
		private static XmlSchemaComplexType anyTypeSkip = XmlSchemaComplexType.CreateAnyType(XmlSchemaContentProcessing.Skip);

		// Token: 0x04001D66 RID: 7526
		private static XmlSchemaComplexType untypedAnyType = new XmlSchemaComplexType();

		// Token: 0x04001D67 RID: 7527
		private byte pvFlags;

		// Token: 0x04001D68 RID: 7528
		private const byte wildCardMask = 1;

		// Token: 0x04001D69 RID: 7529
		private const byte isMixedMask = 2;

		// Token: 0x04001D6A RID: 7530
		private const byte isAbstractMask = 4;
	}
}
