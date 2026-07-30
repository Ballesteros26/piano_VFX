using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020004A0 RID: 1184
	internal sealed class XsdBuilder : SchemaBuilder
	{
		// Token: 0x06002FDD RID: 12253 RVA: 0x00114388 File Offset: 0x00112588
		internal XsdBuilder(XmlReader reader, XmlNamespaceManager curmgr, XmlSchema schema, XmlNameTable nameTable, SchemaNames schemaNames, ValidationEventHandler eventhandler)
		{
			this.reader = reader;
			this.schema = schema;
			this.xso = schema;
			this.namespaceManager = new XsdBuilder.BuilderNamespaceManager(curmgr, reader);
			this.validationEventHandler = eventhandler;
			this.nameTable = nameTable;
			this.schemaNames = schemaNames;
			this.stateHistory = new HWStack(10);
			this.currentEntry = XsdBuilder.SchemaEntries[0];
			this.positionInfo = PositionInfo.GetPositionInfo(reader);
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x00114420 File Offset: 0x00112620
		internal override bool ProcessElement(string prefix, string name, string ns)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name, ns);
			if (this.GetNextState(xmlQualifiedName))
			{
				this.Push();
				this.xso = null;
				this.currentEntry.InitFunc(this, null);
				this.RecordPosition();
				return true;
			}
			if (!this.IsSkipableElement(xmlQualifiedName))
			{
				this.SendValidationEvent("The '{0}' element is not supported in this context.", xmlQualifiedName.ToString());
			}
			return false;
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x00114484 File Offset: 0x00112684
		internal override void ProcessAttribute(string prefix, string name, string ns, string value)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name, ns);
			if (this.currentEntry.Attributes != null)
			{
				for (int i = 0; i < this.currentEntry.Attributes.Length; i++)
				{
					XsdBuilder.XsdAttributeEntry xsdAttributeEntry = this.currentEntry.Attributes[i];
					if (this.schemaNames.TokenToQName[(int)xsdAttributeEntry.Attribute].Equals(xmlQualifiedName))
					{
						try
						{
							xsdAttributeEntry.BuildFunc(this, value);
						}
						catch (XmlSchemaException ex)
						{
							ex.SetSource(this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition);
							this.SendValidationEvent("The value for the '{0}' attribute is invalid - {1}", new string[] { name, ex.Message }, XmlSeverityType.Error);
						}
						return;
					}
				}
			}
			if (!(ns != this.schemaNames.NsXs) || ns.Length == 0)
			{
				this.SendValidationEvent("The '{0}' attribute is not supported in this context.", xmlQualifiedName.ToString());
				return;
			}
			if (ns == this.schemaNames.NsXmlNs)
			{
				if (this.namespaces == null)
				{
					this.namespaces = new Hashtable();
				}
				this.namespaces.Add((name == this.schemaNames.QnXmlNs.Name) ? string.Empty : name, value);
				return;
			}
			XmlAttribute xmlAttribute = new XmlAttribute(prefix, name, ns, this.schema.Document);
			xmlAttribute.Value = value;
			this.unhandledAttributes.Add(xmlAttribute);
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x0011460C File Offset: 0x0011280C
		internal override bool IsContentParsed()
		{
			return this.currentEntry.ParseContent;
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x00114619 File Offset: 0x00112819
		internal override void ProcessMarkup(XmlNode[] markup)
		{
			this.markup = markup;
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x00114622 File Offset: 0x00112822
		internal override void ProcessCData(string value)
		{
			this.SendValidationEvent("The following text is not allowed in this context: '{0}'.", value);
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x00114630 File Offset: 0x00112830
		internal override void StartChildren()
		{
			if (this.xso != null)
			{
				if (this.namespaces != null && this.namespaces.Count > 0)
				{
					this.xso.Namespaces.Namespaces = this.namespaces;
					this.namespaces = null;
				}
				if (this.unhandledAttributes.Count != 0)
				{
					this.xso.SetUnhandledAttributes((XmlAttribute[])this.unhandledAttributes.ToArray(typeof(XmlAttribute)));
					this.unhandledAttributes.Clear();
				}
			}
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x001146B5 File Offset: 0x001128B5
		internal override void EndChildren()
		{
			if (this.currentEntry.EndChildFunc != null)
			{
				this.currentEntry.EndChildFunc(this);
			}
			this.Pop();
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x001146DC File Offset: 0x001128DC
		private void Push()
		{
			this.stateHistory.Push();
			this.stateHistory[this.stateHistory.Length - 1] = this.currentEntry;
			this.containerStack.Push(this.GetContainer(this.currentEntry.CurrentState));
			this.currentEntry = this.nextEntry;
			if (this.currentEntry.Name != SchemaNames.Token.XsdAnnotation)
			{
				this.hasChild = false;
			}
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x00114751 File Offset: 0x00112951
		private void Pop()
		{
			this.currentEntry = (XsdBuilder.XsdEntry)this.stateHistory.Pop();
			this.SetContainer(this.currentEntry.CurrentState, this.containerStack.Pop());
			this.hasChild = true;
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06002FE7 RID: 12263 RVA: 0x0011478C File Offset: 0x0011298C
		private SchemaNames.Token CurrentElement
		{
			get
			{
				return this.currentEntry.Name;
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06002FE8 RID: 12264 RVA: 0x00114799 File Offset: 0x00112999
		private SchemaNames.Token ParentElement
		{
			get
			{
				return ((XsdBuilder.XsdEntry)this.stateHistory[this.stateHistory.Length - 1]).Name;
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06002FE9 RID: 12265 RVA: 0x001147BD File Offset: 0x001129BD
		private XmlSchemaObject ParentContainer
		{
			get
			{
				return (XmlSchemaObject)this.containerStack.Peek();
			}
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x001147D0 File Offset: 0x001129D0
		private XmlSchemaObject GetContainer(XsdBuilder.State state)
		{
			XmlSchemaObject xmlSchemaObject = null;
			switch (state)
			{
			case XsdBuilder.State.Schema:
				xmlSchemaObject = this.schema;
				break;
			case XsdBuilder.State.Annotation:
				xmlSchemaObject = this.annotation;
				break;
			case XsdBuilder.State.Include:
				xmlSchemaObject = this.include;
				break;
			case XsdBuilder.State.Import:
				xmlSchemaObject = this.import;
				break;
			case XsdBuilder.State.Element:
				xmlSchemaObject = this.element;
				break;
			case XsdBuilder.State.Attribute:
				xmlSchemaObject = this.attribute;
				break;
			case XsdBuilder.State.AttributeGroup:
				xmlSchemaObject = this.attributeGroup;
				break;
			case XsdBuilder.State.AttributeGroupRef:
				xmlSchemaObject = this.attributeGroupRef;
				break;
			case XsdBuilder.State.AnyAttribute:
				xmlSchemaObject = this.anyAttribute;
				break;
			case XsdBuilder.State.Group:
				xmlSchemaObject = this.group;
				break;
			case XsdBuilder.State.GroupRef:
				xmlSchemaObject = this.groupRef;
				break;
			case XsdBuilder.State.All:
				xmlSchemaObject = this.all;
				break;
			case XsdBuilder.State.Choice:
				xmlSchemaObject = this.choice;
				break;
			case XsdBuilder.State.Sequence:
				xmlSchemaObject = this.sequence;
				break;
			case XsdBuilder.State.Any:
				xmlSchemaObject = this.anyElement;
				break;
			case XsdBuilder.State.Notation:
				xmlSchemaObject = this.notation;
				break;
			case XsdBuilder.State.SimpleType:
				xmlSchemaObject = this.simpleType;
				break;
			case XsdBuilder.State.ComplexType:
				xmlSchemaObject = this.complexType;
				break;
			case XsdBuilder.State.ComplexContent:
				xmlSchemaObject = this.complexContent;
				break;
			case XsdBuilder.State.ComplexContentRestriction:
				xmlSchemaObject = this.complexContentRestriction;
				break;
			case XsdBuilder.State.ComplexContentExtension:
				xmlSchemaObject = this.complexContentExtension;
				break;
			case XsdBuilder.State.SimpleContent:
				xmlSchemaObject = this.simpleContent;
				break;
			case XsdBuilder.State.SimpleContentExtension:
				xmlSchemaObject = this.simpleContentExtension;
				break;
			case XsdBuilder.State.SimpleContentRestriction:
				xmlSchemaObject = this.simpleContentRestriction;
				break;
			case XsdBuilder.State.SimpleTypeUnion:
				xmlSchemaObject = this.simpleTypeUnion;
				break;
			case XsdBuilder.State.SimpleTypeList:
				xmlSchemaObject = this.simpleTypeList;
				break;
			case XsdBuilder.State.SimpleTypeRestriction:
				xmlSchemaObject = this.simpleTypeRestriction;
				break;
			case XsdBuilder.State.Unique:
			case XsdBuilder.State.Key:
			case XsdBuilder.State.KeyRef:
				xmlSchemaObject = this.identityConstraint;
				break;
			case XsdBuilder.State.Selector:
			case XsdBuilder.State.Field:
				xmlSchemaObject = this.xpath;
				break;
			case XsdBuilder.State.MinExclusive:
			case XsdBuilder.State.MinInclusive:
			case XsdBuilder.State.MaxExclusive:
			case XsdBuilder.State.MaxInclusive:
			case XsdBuilder.State.TotalDigits:
			case XsdBuilder.State.FractionDigits:
			case XsdBuilder.State.Length:
			case XsdBuilder.State.MinLength:
			case XsdBuilder.State.MaxLength:
			case XsdBuilder.State.Enumeration:
			case XsdBuilder.State.Pattern:
			case XsdBuilder.State.WhiteSpace:
				xmlSchemaObject = this.facet;
				break;
			case XsdBuilder.State.AppInfo:
				xmlSchemaObject = this.appInfo;
				break;
			case XsdBuilder.State.Documentation:
				xmlSchemaObject = this.documentation;
				break;
			case XsdBuilder.State.Redefine:
				xmlSchemaObject = this.redefine;
				break;
			}
			return xmlSchemaObject;
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x00114A08 File Offset: 0x00112C08
		private void SetContainer(XsdBuilder.State state, object container)
		{
			switch (state)
			{
			case XsdBuilder.State.Root:
			case XsdBuilder.State.Schema:
				break;
			case XsdBuilder.State.Annotation:
				this.annotation = (XmlSchemaAnnotation)container;
				return;
			case XsdBuilder.State.Include:
				this.include = (XmlSchemaInclude)container;
				return;
			case XsdBuilder.State.Import:
				this.import = (XmlSchemaImport)container;
				return;
			case XsdBuilder.State.Element:
				this.element = (XmlSchemaElement)container;
				return;
			case XsdBuilder.State.Attribute:
				this.attribute = (XmlSchemaAttribute)container;
				return;
			case XsdBuilder.State.AttributeGroup:
				this.attributeGroup = (XmlSchemaAttributeGroup)container;
				return;
			case XsdBuilder.State.AttributeGroupRef:
				this.attributeGroupRef = (XmlSchemaAttributeGroupRef)container;
				return;
			case XsdBuilder.State.AnyAttribute:
				this.anyAttribute = (XmlSchemaAnyAttribute)container;
				return;
			case XsdBuilder.State.Group:
				this.group = (XmlSchemaGroup)container;
				return;
			case XsdBuilder.State.GroupRef:
				this.groupRef = (XmlSchemaGroupRef)container;
				return;
			case XsdBuilder.State.All:
				this.all = (XmlSchemaAll)container;
				return;
			case XsdBuilder.State.Choice:
				this.choice = (XmlSchemaChoice)container;
				return;
			case XsdBuilder.State.Sequence:
				this.sequence = (XmlSchemaSequence)container;
				return;
			case XsdBuilder.State.Any:
				this.anyElement = (XmlSchemaAny)container;
				return;
			case XsdBuilder.State.Notation:
				this.notation = (XmlSchemaNotation)container;
				return;
			case XsdBuilder.State.SimpleType:
				this.simpleType = (XmlSchemaSimpleType)container;
				return;
			case XsdBuilder.State.ComplexType:
				this.complexType = (XmlSchemaComplexType)container;
				return;
			case XsdBuilder.State.ComplexContent:
				this.complexContent = (XmlSchemaComplexContent)container;
				return;
			case XsdBuilder.State.ComplexContentRestriction:
				this.complexContentRestriction = (XmlSchemaComplexContentRestriction)container;
				return;
			case XsdBuilder.State.ComplexContentExtension:
				this.complexContentExtension = (XmlSchemaComplexContentExtension)container;
				return;
			case XsdBuilder.State.SimpleContent:
				this.simpleContent = (XmlSchemaSimpleContent)container;
				return;
			case XsdBuilder.State.SimpleContentExtension:
				this.simpleContentExtension = (XmlSchemaSimpleContentExtension)container;
				return;
			case XsdBuilder.State.SimpleContentRestriction:
				this.simpleContentRestriction = (XmlSchemaSimpleContentRestriction)container;
				return;
			case XsdBuilder.State.SimpleTypeUnion:
				this.simpleTypeUnion = (XmlSchemaSimpleTypeUnion)container;
				return;
			case XsdBuilder.State.SimpleTypeList:
				this.simpleTypeList = (XmlSchemaSimpleTypeList)container;
				return;
			case XsdBuilder.State.SimpleTypeRestriction:
				this.simpleTypeRestriction = (XmlSchemaSimpleTypeRestriction)container;
				return;
			case XsdBuilder.State.Unique:
			case XsdBuilder.State.Key:
			case XsdBuilder.State.KeyRef:
				this.identityConstraint = (XmlSchemaIdentityConstraint)container;
				return;
			case XsdBuilder.State.Selector:
			case XsdBuilder.State.Field:
				this.xpath = (XmlSchemaXPath)container;
				return;
			case XsdBuilder.State.MinExclusive:
			case XsdBuilder.State.MinInclusive:
			case XsdBuilder.State.MaxExclusive:
			case XsdBuilder.State.MaxInclusive:
			case XsdBuilder.State.TotalDigits:
			case XsdBuilder.State.FractionDigits:
			case XsdBuilder.State.Length:
			case XsdBuilder.State.MinLength:
			case XsdBuilder.State.MaxLength:
			case XsdBuilder.State.Enumeration:
			case XsdBuilder.State.Pattern:
			case XsdBuilder.State.WhiteSpace:
				this.facet = (XmlSchemaFacet)container;
				return;
			case XsdBuilder.State.AppInfo:
				this.appInfo = (XmlSchemaAppInfo)container;
				return;
			case XsdBuilder.State.Documentation:
				this.documentation = (XmlSchemaDocumentation)container;
				return;
			case XsdBuilder.State.Redefine:
				this.redefine = (XmlSchemaRedefine)container;
				break;
			default:
				return;
			}
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x00114C7B File Offset: 0x00112E7B
		private static void BuildAnnotated_Id(XsdBuilder builder, string value)
		{
			builder.xso.IdAttribute = value;
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x00114C89 File Offset: 0x00112E89
		private static void BuildSchema_AttributeFormDefault(XsdBuilder builder, string value)
		{
			builder.schema.AttributeFormDefault = (XmlSchemaForm)builder.ParseEnum(value, "attributeFormDefault", XsdBuilder.FormStringValues);
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x00114CA7 File Offset: 0x00112EA7
		private static void BuildSchema_ElementFormDefault(XsdBuilder builder, string value)
		{
			builder.schema.ElementFormDefault = (XmlSchemaForm)builder.ParseEnum(value, "elementFormDefault", XsdBuilder.FormStringValues);
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x00114CC5 File Offset: 0x00112EC5
		private static void BuildSchema_TargetNamespace(XsdBuilder builder, string value)
		{
			builder.schema.TargetNamespace = value;
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x00114CD3 File Offset: 0x00112ED3
		private static void BuildSchema_Version(XsdBuilder builder, string value)
		{
			builder.schema.Version = value;
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x00114CE1 File Offset: 0x00112EE1
		private static void BuildSchema_FinalDefault(XsdBuilder builder, string value)
		{
			builder.schema.FinalDefault = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "finalDefault");
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x00114CFA File Offset: 0x00112EFA
		private static void BuildSchema_BlockDefault(XsdBuilder builder, string value)
		{
			builder.schema.BlockDefault = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "blockDefault");
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x00114D13 File Offset: 0x00112F13
		private static void InitSchema(XsdBuilder builder, string value)
		{
			builder.canIncludeImport = true;
			builder.xso = builder.schema;
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x00114D28 File Offset: 0x00112F28
		private static void InitInclude(XsdBuilder builder, string value)
		{
			if (!builder.canIncludeImport)
			{
				builder.SendValidationEvent("The 'include' element cannot appear at this location.", null);
			}
			builder.xso = (builder.include = new XmlSchemaInclude());
			builder.schema.Includes.Add(builder.include);
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x00114D74 File Offset: 0x00112F74
		private static void BuildInclude_SchemaLocation(XsdBuilder builder, string value)
		{
			builder.include.SchemaLocation = value;
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x00114D84 File Offset: 0x00112F84
		private static void InitImport(XsdBuilder builder, string value)
		{
			if (!builder.canIncludeImport)
			{
				builder.SendValidationEvent("The 'import' element cannot appear at this location.", null);
			}
			builder.xso = (builder.import = new XmlSchemaImport());
			builder.schema.Includes.Add(builder.import);
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x00114DD0 File Offset: 0x00112FD0
		private static void BuildImport_Namespace(XsdBuilder builder, string value)
		{
			builder.import.Namespace = value;
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x00114DDE File Offset: 0x00112FDE
		private static void BuildImport_SchemaLocation(XsdBuilder builder, string value)
		{
			builder.import.SchemaLocation = value;
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x00114DEC File Offset: 0x00112FEC
		private static void InitRedefine(XsdBuilder builder, string value)
		{
			if (!builder.canIncludeImport)
			{
				builder.SendValidationEvent("The 'redefine' element cannot appear at this location.", null);
			}
			builder.xso = (builder.redefine = new XmlSchemaRedefine());
			builder.schema.Includes.Add(builder.redefine);
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x00114E38 File Offset: 0x00113038
		private static void BuildRedefine_SchemaLocation(XsdBuilder builder, string value)
		{
			builder.redefine.SchemaLocation = value;
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x00114E46 File Offset: 0x00113046
		private static void EndRedefine(XsdBuilder builder)
		{
			builder.canIncludeImport = true;
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x00114E50 File Offset: 0x00113050
		private static void InitAttribute(XsdBuilder builder, string value)
		{
			builder.xso = (builder.attribute = new XmlSchemaAttribute());
			if (builder.ParentElement == SchemaNames.Token.XsdSchema)
			{
				builder.schema.Items.Add(builder.attribute);
			}
			else
			{
				builder.AddAttribute(builder.attribute);
			}
			builder.canIncludeImport = false;
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x00114EA7 File Offset: 0x001130A7
		private static void BuildAttribute_Default(XsdBuilder builder, string value)
		{
			builder.attribute.DefaultValue = value;
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x00114EB5 File Offset: 0x001130B5
		private static void BuildAttribute_Fixed(XsdBuilder builder, string value)
		{
			builder.attribute.FixedValue = value;
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x00114EC3 File Offset: 0x001130C3
		private static void BuildAttribute_Form(XsdBuilder builder, string value)
		{
			builder.attribute.Form = (XmlSchemaForm)builder.ParseEnum(value, "form", XsdBuilder.FormStringValues);
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x00114EE1 File Offset: 0x001130E1
		private static void BuildAttribute_Use(XsdBuilder builder, string value)
		{
			builder.attribute.Use = (XmlSchemaUse)builder.ParseEnum(value, "use", XsdBuilder.UseStringValues);
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x00114EFF File Offset: 0x001130FF
		private static void BuildAttribute_Ref(XsdBuilder builder, string value)
		{
			builder.attribute.RefName = builder.ParseQName(value, "ref");
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x00114F18 File Offset: 0x00113118
		private static void BuildAttribute_Name(XsdBuilder builder, string value)
		{
			builder.attribute.Name = value;
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x00114F26 File Offset: 0x00113126
		private static void BuildAttribute_Type(XsdBuilder builder, string value)
		{
			builder.attribute.SchemaTypeName = builder.ParseQName(value, "type");
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x00114F40 File Offset: 0x00113140
		private static void InitElement(XsdBuilder builder, string value)
		{
			builder.xso = (builder.element = new XmlSchemaElement());
			builder.canIncludeImport = false;
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement == SchemaNames.Token.XsdSchema)
			{
				builder.schema.Items.Add(builder.element);
				return;
			}
			switch (parentElement)
			{
			case SchemaNames.Token.XsdAll:
				builder.all.Items.Add(builder.element);
				return;
			case SchemaNames.Token.XsdChoice:
				builder.choice.Items.Add(builder.element);
				return;
			case SchemaNames.Token.XsdSequence:
				builder.sequence.Items.Add(builder.element);
				return;
			default:
				return;
			}
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x00114FE9 File Offset: 0x001131E9
		private static void BuildElement_Abstract(XsdBuilder builder, string value)
		{
			builder.element.IsAbstract = builder.ParseBoolean(value, "abstract");
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x00115002 File Offset: 0x00113202
		private static void BuildElement_Block(XsdBuilder builder, string value)
		{
			builder.element.Block = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "block");
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x0011501B File Offset: 0x0011321B
		private static void BuildElement_Default(XsdBuilder builder, string value)
		{
			builder.element.DefaultValue = value;
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x00115029 File Offset: 0x00113229
		private static void BuildElement_Form(XsdBuilder builder, string value)
		{
			builder.element.Form = (XmlSchemaForm)builder.ParseEnum(value, "form", XsdBuilder.FormStringValues);
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x00115047 File Offset: 0x00113247
		private static void BuildElement_SubstitutionGroup(XsdBuilder builder, string value)
		{
			builder.element.SubstitutionGroup = builder.ParseQName(value, "substitutionGroup");
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x00115060 File Offset: 0x00113260
		private static void BuildElement_Final(XsdBuilder builder, string value)
		{
			builder.element.Final = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "final");
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x00115079 File Offset: 0x00113279
		private static void BuildElement_Fixed(XsdBuilder builder, string value)
		{
			builder.element.FixedValue = value;
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x00115087 File Offset: 0x00113287
		private static void BuildElement_MaxOccurs(XsdBuilder builder, string value)
		{
			builder.SetMaxOccurs(builder.element, value);
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x00115096 File Offset: 0x00113296
		private static void BuildElement_MinOccurs(XsdBuilder builder, string value)
		{
			builder.SetMinOccurs(builder.element, value);
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x001150A5 File Offset: 0x001132A5
		private static void BuildElement_Name(XsdBuilder builder, string value)
		{
			builder.element.Name = value;
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x001150B3 File Offset: 0x001132B3
		private static void BuildElement_Nillable(XsdBuilder builder, string value)
		{
			builder.element.IsNillable = builder.ParseBoolean(value, "nillable");
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x001150CC File Offset: 0x001132CC
		private static void BuildElement_Ref(XsdBuilder builder, string value)
		{
			builder.element.RefName = builder.ParseQName(value, "ref");
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x001150E5 File Offset: 0x001132E5
		private static void BuildElement_Type(XsdBuilder builder, string value)
		{
			builder.element.SchemaTypeName = builder.ParseQName(value, "type");
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x00115100 File Offset: 0x00113300
		private static void InitSimpleType(XsdBuilder builder, string value)
		{
			builder.xso = (builder.simpleType = new XmlSchemaSimpleType());
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement <= SchemaNames.Token.XsdElement)
			{
				if (parentElement == SchemaNames.Token.XsdSchema)
				{
					builder.canIncludeImport = false;
					builder.schema.Items.Add(builder.simpleType);
					return;
				}
				if (parentElement != SchemaNames.Token.XsdElement)
				{
					return;
				}
				if (builder.element.SchemaType != null)
				{
					builder.SendValidationEvent("'{0}' is a duplicate XSD element.", "simpleType");
				}
				if (builder.element.Constraints.Count != 0)
				{
					builder.SendValidationEvent("'simpleType' or 'complexType' cannot follow 'unique', 'key' or 'keyref'.", null);
				}
				builder.element.SchemaType = builder.simpleType;
				return;
			}
			else
			{
				if (parentElement != SchemaNames.Token.XsdAttribute)
				{
					switch (parentElement)
					{
					case SchemaNames.Token.XsdSimpleContentRestriction:
						if (builder.simpleContentRestriction.BaseType != null)
						{
							builder.SendValidationEvent("'{0}' is a duplicate XSD element.", "simpleType");
						}
						if (builder.simpleContentRestriction.Attributes.Count != 0 || builder.simpleContentRestriction.AnyAttribute != null || builder.simpleContentRestriction.Facets.Count != 0)
						{
							builder.SendValidationEvent("'simpleType' should be the first child of restriction.", null);
						}
						builder.simpleContentRestriction.BaseType = builder.simpleType;
						return;
					case SchemaNames.Token.XsdSimpleTypeList:
						if (builder.simpleTypeList.ItemType != null)
						{
							builder.SendValidationEvent("'{0}' is a duplicate XSD element.", "simpleType");
						}
						builder.simpleTypeList.ItemType = builder.simpleType;
						return;
					case SchemaNames.Token.XsdSimpleTypeRestriction:
						if (builder.simpleTypeRestriction.BaseType != null)
						{
							builder.SendValidationEvent("'{0}' is a duplicate XSD element.", "simpleType");
						}
						builder.simpleTypeRestriction.BaseType = builder.simpleType;
						return;
					case SchemaNames.Token.XsdSimpleTypeUnion:
						builder.simpleTypeUnion.BaseTypes.Add(builder.simpleType);
						break;
					case SchemaNames.Token.XsdWhitespace:
						break;
					case SchemaNames.Token.XsdRedefine:
						builder.redefine.Items.Add(builder.simpleType);
						return;
					default:
						return;
					}
					return;
				}
				if (builder.attribute.SchemaType != null)
				{
					builder.SendValidationEvent("'{0}' is a duplicate XSD element.", "simpleType");
				}
				builder.attribute.SchemaType = builder.simpleType;
				return;
			}
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x001152F6 File Offset: 0x001134F6
		private static void BuildSimpleType_Name(XsdBuilder builder, string value)
		{
			builder.simpleType.Name = value;
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x00115304 File Offset: 0x00113504
		private static void BuildSimpleType_Final(XsdBuilder builder, string value)
		{
			builder.simpleType.Final = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "final");
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x00115320 File Offset: 0x00113520
		private static void InitSimpleTypeUnion(XsdBuilder builder, string value)
		{
			if (builder.simpleType.Content != null)
			{
				builder.SendValidationEvent("'simpleType' should have only one child 'union', 'list', or 'restriction'.", null);
			}
			builder.xso = (builder.simpleTypeUnion = new XmlSchemaSimpleTypeUnion());
			builder.simpleType.Content = builder.simpleTypeUnion;
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x0011536C File Offset: 0x0011356C
		private static void BuildSimpleTypeUnion_MemberTypes(XsdBuilder builder, string value)
		{
			XmlSchemaDatatype xmlSchemaDatatype = XmlSchemaDatatype.FromXmlTokenizedTypeXsd(XmlTokenizedType.QName).DeriveByList(null);
			try
			{
				builder.simpleTypeUnion.MemberTypes = (XmlQualifiedName[])xmlSchemaDatatype.ParseValue(value, builder.nameTable, builder.namespaceManager);
			}
			catch (XmlSchemaException ex)
			{
				ex.SetSource(builder.reader.BaseURI, builder.positionInfo.LineNumber, builder.positionInfo.LinePosition);
				builder.SendValidationEvent(ex);
			}
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x001153F0 File Offset: 0x001135F0
		private static void InitSimpleTypeList(XsdBuilder builder, string value)
		{
			if (builder.simpleType.Content != null)
			{
				builder.SendValidationEvent("'simpleType' should have only one child 'union', 'list', or 'restriction'.", null);
			}
			builder.xso = (builder.simpleTypeList = new XmlSchemaSimpleTypeList());
			builder.simpleType.Content = builder.simpleTypeList;
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x0011543B File Offset: 0x0011363B
		private static void BuildSimpleTypeList_ItemType(XsdBuilder builder, string value)
		{
			builder.simpleTypeList.ItemTypeName = builder.ParseQName(value, "itemType");
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x00115454 File Offset: 0x00113654
		private static void InitSimpleTypeRestriction(XsdBuilder builder, string value)
		{
			if (builder.simpleType.Content != null)
			{
				builder.SendValidationEvent("'simpleType' should have only one child 'union', 'list', or 'restriction'.", null);
			}
			builder.xso = (builder.simpleTypeRestriction = new XmlSchemaSimpleTypeRestriction());
			builder.simpleType.Content = builder.simpleTypeRestriction;
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x0011549F File Offset: 0x0011369F
		private static void BuildSimpleTypeRestriction_Base(XsdBuilder builder, string value)
		{
			builder.simpleTypeRestriction.BaseTypeName = builder.ParseQName(value, "base");
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x001154B8 File Offset: 0x001136B8
		private static void InitComplexType(XsdBuilder builder, string value)
		{
			builder.xso = (builder.complexType = new XmlSchemaComplexType());
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement == SchemaNames.Token.XsdSchema)
			{
				builder.canIncludeImport = false;
				builder.schema.Items.Add(builder.complexType);
				return;
			}
			if (parentElement == SchemaNames.Token.XsdElement)
			{
				if (builder.element.SchemaType != null)
				{
					builder.SendValidationEvent("The '{0}' element already exists in the content model.", "complexType");
				}
				if (builder.element.Constraints.Count != 0)
				{
					builder.SendValidationEvent("'simpleType' or 'complexType' cannot follow 'unique', 'key' or 'keyref'.", null);
				}
				builder.element.SchemaType = builder.complexType;
				return;
			}
			if (parentElement != SchemaNames.Token.XsdRedefine)
			{
				return;
			}
			builder.redefine.Items.Add(builder.complexType);
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x00115573 File Offset: 0x00113773
		private static void BuildComplexType_Abstract(XsdBuilder builder, string value)
		{
			builder.complexType.IsAbstract = builder.ParseBoolean(value, "abstract");
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x0011558C File Offset: 0x0011378C
		private static void BuildComplexType_Block(XsdBuilder builder, string value)
		{
			builder.complexType.Block = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "block");
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x001155A5 File Offset: 0x001137A5
		private static void BuildComplexType_Final(XsdBuilder builder, string value)
		{
			builder.complexType.Final = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "final");
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x001155BE File Offset: 0x001137BE
		private static void BuildComplexType_Mixed(XsdBuilder builder, string value)
		{
			builder.complexType.IsMixed = builder.ParseBoolean(value, "mixed");
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x001155D7 File Offset: 0x001137D7
		private static void BuildComplexType_Name(XsdBuilder builder, string value)
		{
			builder.complexType.Name = value;
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x001155E8 File Offset: 0x001137E8
		private static void InitComplexContent(XsdBuilder builder, string value)
		{
			if (builder.complexType.ContentModel != null || builder.complexType.Particle != null || builder.complexType.Attributes.Count != 0 || builder.complexType.AnyAttribute != null)
			{
				builder.SendValidationEvent("The content model of a complex type must consist of 'annotation' (if present); followed by zero or one of the following: 'simpleContent', 'complexContent', 'group', 'choice', 'sequence', or 'all'; followed by zero or more 'attribute' or 'attributeGroup'; followed by zero or one 'anyAttribute'.", "complexContent");
			}
			builder.xso = (builder.complexContent = new XmlSchemaComplexContent());
			builder.complexType.ContentModel = builder.complexContent;
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x00115663 File Offset: 0x00113863
		private static void BuildComplexContent_Mixed(XsdBuilder builder, string value)
		{
			builder.complexContent.IsMixed = builder.ParseBoolean(value, "mixed");
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x0011567C File Offset: 0x0011387C
		private static void InitComplexContentExtension(XsdBuilder builder, string value)
		{
			if (builder.complexContent.Content != null)
			{
				builder.SendValidationEvent("Complex content restriction or extension should consist of zero or one of 'group', 'choice', 'sequence', or 'all'; followed by zero or more 'attribute' or 'attributeGroup'; followed by zero or one 'anyAttribute'.", "extension");
			}
			builder.xso = (builder.complexContentExtension = new XmlSchemaComplexContentExtension());
			builder.complexContent.Content = builder.complexContentExtension;
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x001156CB File Offset: 0x001138CB
		private static void BuildComplexContentExtension_Base(XsdBuilder builder, string value)
		{
			builder.complexContentExtension.BaseTypeName = builder.ParseQName(value, "base");
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x001156E4 File Offset: 0x001138E4
		private static void InitComplexContentRestriction(XsdBuilder builder, string value)
		{
			builder.xso = (builder.complexContentRestriction = new XmlSchemaComplexContentRestriction());
			builder.complexContent.Content = builder.complexContentRestriction;
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x00115716 File Offset: 0x00113916
		private static void BuildComplexContentRestriction_Base(XsdBuilder builder, string value)
		{
			builder.complexContentRestriction.BaseTypeName = builder.ParseQName(value, "base");
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x00115730 File Offset: 0x00113930
		private static void InitSimpleContent(XsdBuilder builder, string value)
		{
			if (builder.complexType.ContentModel != null || builder.complexType.Particle != null || builder.complexType.Attributes.Count != 0 || builder.complexType.AnyAttribute != null)
			{
				builder.SendValidationEvent("The content model of a complex type must consist of 'annotation' (if present); followed by zero or one of the following: 'simpleContent', 'complexContent', 'group', 'choice', 'sequence', or 'all'; followed by zero or more 'attribute' or 'attributeGroup'; followed by zero or one 'anyAttribute'.", "simpleContent");
			}
			builder.xso = (builder.simpleContent = new XmlSchemaSimpleContent());
			builder.complexType.ContentModel = builder.simpleContent;
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x001157AC File Offset: 0x001139AC
		private static void InitSimpleContentExtension(XsdBuilder builder, string value)
		{
			if (builder.simpleContent.Content != null)
			{
				builder.SendValidationEvent("The '{0}' element already exists in the content model.", "extension");
			}
			builder.xso = (builder.simpleContentExtension = new XmlSchemaSimpleContentExtension());
			builder.simpleContent.Content = builder.simpleContentExtension;
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x001157FB File Offset: 0x001139FB
		private static void BuildSimpleContentExtension_Base(XsdBuilder builder, string value)
		{
			builder.simpleContentExtension.BaseTypeName = builder.ParseQName(value, "base");
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x00115814 File Offset: 0x00113A14
		private static void InitSimpleContentRestriction(XsdBuilder builder, string value)
		{
			if (builder.simpleContent.Content != null)
			{
				builder.SendValidationEvent("The '{0}' element already exists in the content model.", "restriction");
			}
			builder.xso = (builder.simpleContentRestriction = new XmlSchemaSimpleContentRestriction());
			builder.simpleContent.Content = builder.simpleContentRestriction;
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x00115863 File Offset: 0x00113A63
		private static void BuildSimpleContentRestriction_Base(XsdBuilder builder, string value)
		{
			builder.simpleContentRestriction.BaseTypeName = builder.ParseQName(value, "base");
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x0011587C File Offset: 0x00113A7C
		private static void InitAttributeGroup(XsdBuilder builder, string value)
		{
			builder.canIncludeImport = false;
			builder.xso = (builder.attributeGroup = new XmlSchemaAttributeGroup());
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement == SchemaNames.Token.XsdSchema)
			{
				builder.schema.Items.Add(builder.attributeGroup);
				return;
			}
			if (parentElement != SchemaNames.Token.XsdRedefine)
			{
				return;
			}
			builder.redefine.Items.Add(builder.attributeGroup);
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x001158E5 File Offset: 0x00113AE5
		private static void BuildAttributeGroup_Name(XsdBuilder builder, string value)
		{
			builder.attributeGroup.Name = value;
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x001158F4 File Offset: 0x00113AF4
		private static void InitAttributeGroupRef(XsdBuilder builder, string value)
		{
			builder.xso = (builder.attributeGroupRef = new XmlSchemaAttributeGroupRef());
			builder.AddAttribute(builder.attributeGroupRef);
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x00115921 File Offset: 0x00113B21
		private static void BuildAttributeGroupRef_Ref(XsdBuilder builder, string value)
		{
			builder.attributeGroupRef.RefName = builder.ParseQName(value, "ref");
		}

		// Token: 0x06003030 RID: 12336 RVA: 0x0011593C File Offset: 0x00113B3C
		private static void InitAnyAttribute(XsdBuilder builder, string value)
		{
			builder.xso = (builder.anyAttribute = new XmlSchemaAnyAttribute());
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement != SchemaNames.Token.xsdAttributeGroup)
			{
				if (parentElement == SchemaNames.Token.XsdComplexType)
				{
					if (builder.complexType.ContentModel != null)
					{
						builder.SendValidationEvent("'{0}' and content model are mutually exclusive.", "anyAttribute");
					}
					if (builder.complexType.AnyAttribute != null)
					{
						builder.SendValidationEvent("The '{0}' element already exists in the content model.", "anyAttribute");
					}
					builder.complexType.AnyAttribute = builder.anyAttribute;
					return;
				}
				switch (parentElement)
				{
				case SchemaNames.Token.XsdComplexContentExtension:
					if (builder.complexContentExtension.AnyAttribute != null)
					{
						builder.SendValidationEvent("The '{0}' element already exists in the content model.", "anyAttribute");
					}
					builder.complexContentExtension.AnyAttribute = builder.anyAttribute;
					return;
				case SchemaNames.Token.XsdComplexContentRestriction:
					if (builder.complexContentRestriction.AnyAttribute != null)
					{
						builder.SendValidationEvent("The '{0}' element already exists in the content model.", "anyAttribute");
					}
					builder.complexContentRestriction.AnyAttribute = builder.anyAttribute;
					return;
				case SchemaNames.Token.XsdSimpleContent:
					break;
				case SchemaNames.Token.XsdSimpleContentExtension:
					if (builder.simpleContentExtension.AnyAttribute != null)
					{
						builder.SendValidationEvent("The '{0}' element already exists in the content model.", "anyAttribute");
					}
					builder.simpleContentExtension.AnyAttribute = builder.anyAttribute;
					return;
				case SchemaNames.Token.XsdSimpleContentRestriction:
					if (builder.simpleContentRestriction.AnyAttribute != null)
					{
						builder.SendValidationEvent("The '{0}' element already exists in the content model.", "anyAttribute");
					}
					builder.simpleContentRestriction.AnyAttribute = builder.anyAttribute;
					return;
				default:
					return;
				}
			}
			else
			{
				if (builder.attributeGroup.AnyAttribute != null)
				{
					builder.SendValidationEvent("The '{0}' element already exists in the content model.", "anyAttribute");
				}
				builder.attributeGroup.AnyAttribute = builder.anyAttribute;
			}
		}

		// Token: 0x06003031 RID: 12337 RVA: 0x00115AC5 File Offset: 0x00113CC5
		private static void BuildAnyAttribute_Namespace(XsdBuilder builder, string value)
		{
			builder.anyAttribute.Namespace = value;
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x00115AD3 File Offset: 0x00113CD3
		private static void BuildAnyAttribute_ProcessContents(XsdBuilder builder, string value)
		{
			builder.anyAttribute.ProcessContents = (XmlSchemaContentProcessing)builder.ParseEnum(value, "processContents", XsdBuilder.ProcessContentsStringValues);
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x00115AF4 File Offset: 0x00113CF4
		private static void InitGroup(XsdBuilder builder, string value)
		{
			builder.xso = (builder.group = new XmlSchemaGroup());
			builder.canIncludeImport = false;
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement == SchemaNames.Token.XsdSchema)
			{
				builder.schema.Items.Add(builder.group);
				return;
			}
			if (parentElement != SchemaNames.Token.XsdRedefine)
			{
				return;
			}
			builder.redefine.Items.Add(builder.group);
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x00115B5D File Offset: 0x00113D5D
		private static void BuildGroup_Name(XsdBuilder builder, string value)
		{
			builder.group.Name = value;
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x00115B6C File Offset: 0x00113D6C
		private static void InitGroupRef(XsdBuilder builder, string value)
		{
			builder.xso = (builder.particle = (builder.groupRef = new XmlSchemaGroupRef()));
			builder.AddParticle(builder.groupRef);
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x00115BA2 File Offset: 0x00113DA2
		private static void BuildParticle_MaxOccurs(XsdBuilder builder, string value)
		{
			builder.SetMaxOccurs(builder.particle, value);
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x00115BB1 File Offset: 0x00113DB1
		private static void BuildParticle_MinOccurs(XsdBuilder builder, string value)
		{
			builder.SetMinOccurs(builder.particle, value);
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x00115BC0 File Offset: 0x00113DC0
		private static void BuildGroupRef_Ref(XsdBuilder builder, string value)
		{
			builder.groupRef.RefName = builder.ParseQName(value, "ref");
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x00115BDC File Offset: 0x00113DDC
		private static void InitAll(XsdBuilder builder, string value)
		{
			builder.xso = (builder.particle = (builder.all = new XmlSchemaAll()));
			builder.AddParticle(builder.all);
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x00115C14 File Offset: 0x00113E14
		private static void InitChoice(XsdBuilder builder, string value)
		{
			builder.xso = (builder.particle = (builder.choice = new XmlSchemaChoice()));
			builder.AddParticle(builder.choice);
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x00115C4C File Offset: 0x00113E4C
		private static void InitSequence(XsdBuilder builder, string value)
		{
			builder.xso = (builder.particle = (builder.sequence = new XmlSchemaSequence()));
			builder.AddParticle(builder.sequence);
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x00115C84 File Offset: 0x00113E84
		private static void InitAny(XsdBuilder builder, string value)
		{
			builder.xso = (builder.particle = (builder.anyElement = new XmlSchemaAny()));
			builder.AddParticle(builder.anyElement);
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x00115CBA File Offset: 0x00113EBA
		private static void BuildAny_Namespace(XsdBuilder builder, string value)
		{
			builder.anyElement.Namespace = value;
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x00115CC8 File Offset: 0x00113EC8
		private static void BuildAny_ProcessContents(XsdBuilder builder, string value)
		{
			builder.anyElement.ProcessContents = (XmlSchemaContentProcessing)builder.ParseEnum(value, "processContents", XsdBuilder.ProcessContentsStringValues);
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x00115CE8 File Offset: 0x00113EE8
		private static void InitNotation(XsdBuilder builder, string value)
		{
			builder.xso = (builder.notation = new XmlSchemaNotation());
			builder.canIncludeImport = false;
			builder.schema.Items.Add(builder.notation);
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x00115D27 File Offset: 0x00113F27
		private static void BuildNotation_Name(XsdBuilder builder, string value)
		{
			builder.notation.Name = value;
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x00115D35 File Offset: 0x00113F35
		private static void BuildNotation_Public(XsdBuilder builder, string value)
		{
			builder.notation.Public = value;
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x00115D43 File Offset: 0x00113F43
		private static void BuildNotation_System(XsdBuilder builder, string value)
		{
			builder.notation.System = value;
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x00115D54 File Offset: 0x00113F54
		private static void InitFacet(XsdBuilder builder, string value)
		{
			switch (builder.CurrentElement)
			{
			case SchemaNames.Token.XsdMinExclusive:
				builder.facet = new XmlSchemaMinExclusiveFacet();
				break;
			case SchemaNames.Token.XsdMinInclusive:
				builder.facet = new XmlSchemaMinInclusiveFacet();
				break;
			case SchemaNames.Token.XsdMaxExclusive:
				builder.facet = new XmlSchemaMaxExclusiveFacet();
				break;
			case SchemaNames.Token.XsdMaxInclusive:
				builder.facet = new XmlSchemaMaxInclusiveFacet();
				break;
			case SchemaNames.Token.XsdTotalDigits:
				builder.facet = new XmlSchemaTotalDigitsFacet();
				break;
			case SchemaNames.Token.XsdFractionDigits:
				builder.facet = new XmlSchemaFractionDigitsFacet();
				break;
			case SchemaNames.Token.XsdLength:
				builder.facet = new XmlSchemaLengthFacet();
				break;
			case SchemaNames.Token.XsdMinLength:
				builder.facet = new XmlSchemaMinLengthFacet();
				break;
			case SchemaNames.Token.XsdMaxLength:
				builder.facet = new XmlSchemaMaxLengthFacet();
				break;
			case SchemaNames.Token.XsdEnumeration:
				builder.facet = new XmlSchemaEnumerationFacet();
				break;
			case SchemaNames.Token.XsdPattern:
				builder.facet = new XmlSchemaPatternFacet();
				break;
			case SchemaNames.Token.XsdWhitespace:
				builder.facet = new XmlSchemaWhiteSpaceFacet();
				break;
			}
			builder.xso = builder.facet;
			if (SchemaNames.Token.XsdSimpleTypeRestriction == builder.ParentElement)
			{
				builder.simpleTypeRestriction.Facets.Add(builder.facet);
				return;
			}
			if (builder.simpleContentRestriction.Attributes.Count != 0 || builder.simpleContentRestriction.AnyAttribute != null)
			{
				builder.SendValidationEvent("Facet should go before 'attribute', 'attributeGroup', or 'anyAttribute'.", null);
			}
			builder.simpleContentRestriction.Facets.Add(builder.facet);
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x00115EE2 File Offset: 0x001140E2
		private static void BuildFacet_Fixed(XsdBuilder builder, string value)
		{
			builder.facet.IsFixed = builder.ParseBoolean(value, "fixed");
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x00115EFB File Offset: 0x001140FB
		private static void BuildFacet_Value(XsdBuilder builder, string value)
		{
			builder.facet.Value = value;
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x00115F0C File Offset: 0x0011410C
		private static void InitIdentityConstraint(XsdBuilder builder, string value)
		{
			if (!builder.element.RefName.IsEmpty)
			{
				builder.SendValidationEvent("When the ref attribute is present, the type attribute and complexType, simpleType, key, keyref, and unique elements cannot be present.", null);
			}
			switch (builder.CurrentElement)
			{
			case SchemaNames.Token.XsdUnique:
				builder.xso = (builder.identityConstraint = new XmlSchemaUnique());
				break;
			case SchemaNames.Token.XsdKey:
				builder.xso = (builder.identityConstraint = new XmlSchemaKey());
				break;
			case SchemaNames.Token.XsdKeyref:
				builder.xso = (builder.identityConstraint = new XmlSchemaKeyref());
				break;
			}
			builder.element.Constraints.Add(builder.identityConstraint);
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x00115FAC File Offset: 0x001141AC
		private static void BuildIdentityConstraint_Name(XsdBuilder builder, string value)
		{
			builder.identityConstraint.Name = value;
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x00115FBA File Offset: 0x001141BA
		private static void BuildIdentityConstraint_Refer(XsdBuilder builder, string value)
		{
			if (builder.identityConstraint is XmlSchemaKeyref)
			{
				((XmlSchemaKeyref)builder.identityConstraint).Refer = builder.ParseQName(value, "refer");
				return;
			}
			builder.SendValidationEvent("The '{0}' attribute is not supported in this context.", "refer");
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x00115FF8 File Offset: 0x001141F8
		private static void InitSelector(XsdBuilder builder, string value)
		{
			builder.xso = (builder.xpath = new XmlSchemaXPath());
			if (builder.identityConstraint.Selector == null)
			{
				builder.identityConstraint.Selector = builder.xpath;
				return;
			}
			builder.SendValidationEvent("Selector cannot appear twice in one identity constraint.", builder.identityConstraint.Name);
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x0011604E File Offset: 0x0011424E
		private static void BuildSelector_XPath(XsdBuilder builder, string value)
		{
			builder.xpath.XPath = value;
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x0011605C File Offset: 0x0011425C
		private static void InitField(XsdBuilder builder, string value)
		{
			builder.xso = (builder.xpath = new XmlSchemaXPath());
			if (builder.identityConstraint.Selector == null)
			{
				builder.SendValidationEvent("Cannot define fields before selector.", builder.identityConstraint.Name);
			}
			builder.identityConstraint.Fields.Add(builder.xpath);
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x0011604E File Offset: 0x0011424E
		private static void BuildField_XPath(XsdBuilder builder, string value)
		{
			builder.xpath.XPath = value;
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x001160B8 File Offset: 0x001142B8
		private static void InitAnnotation(XsdBuilder builder, string value)
		{
			if (builder.hasChild && builder.ParentElement != SchemaNames.Token.XsdSchema && builder.ParentElement != SchemaNames.Token.XsdRedefine)
			{
				builder.SendValidationEvent("The 'annotation' element cannot appear at this location.", null);
			}
			builder.xso = (builder.annotation = new XmlSchemaAnnotation());
			builder.ParentContainer.AddAnnotation(builder.annotation);
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x00116114 File Offset: 0x00114314
		private static void InitAppinfo(XsdBuilder builder, string value)
		{
			builder.xso = (builder.appInfo = new XmlSchemaAppInfo());
			builder.annotation.Items.Add(builder.appInfo);
			builder.markup = new XmlNode[0];
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x00116158 File Offset: 0x00114358
		private static void BuildAppinfo_Source(XsdBuilder builder, string value)
		{
			builder.appInfo.Source = XsdBuilder.ParseUriReference(value);
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x0011616B File Offset: 0x0011436B
		private static void EndAppinfo(XsdBuilder builder)
		{
			builder.appInfo.Markup = builder.markup;
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x00116180 File Offset: 0x00114380
		private static void InitDocumentation(XsdBuilder builder, string value)
		{
			builder.xso = (builder.documentation = new XmlSchemaDocumentation());
			builder.annotation.Items.Add(builder.documentation);
			builder.markup = new XmlNode[0];
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x001161C4 File Offset: 0x001143C4
		private static void BuildDocumentation_Source(XsdBuilder builder, string value)
		{
			builder.documentation.Source = XsdBuilder.ParseUriReference(value);
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x001161D8 File Offset: 0x001143D8
		private static void BuildDocumentation_XmlLang(XsdBuilder builder, string value)
		{
			try
			{
				builder.documentation.Language = value;
			}
			catch (XmlSchemaException ex)
			{
				ex.SetSource(builder.reader.BaseURI, builder.positionInfo.LineNumber, builder.positionInfo.LinePosition);
				builder.SendValidationEvent(ex);
			}
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x00116234 File Offset: 0x00114434
		private static void EndDocumentation(XsdBuilder builder)
		{
			builder.documentation.Markup = builder.markup;
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x00116248 File Offset: 0x00114448
		private void AddAttribute(XmlSchemaObject value)
		{
			SchemaNames.Token parentElement = this.ParentElement;
			if (parentElement != SchemaNames.Token.xsdAttributeGroup)
			{
				if (parentElement == SchemaNames.Token.XsdComplexType)
				{
					if (this.complexType.ContentModel != null)
					{
						this.SendValidationEvent("'{0}' and content model are mutually exclusive.", "attribute");
					}
					if (this.complexType.AnyAttribute != null)
					{
						this.SendValidationEvent("'anyAttribute' must be the last child.", null);
					}
					this.complexType.Attributes.Add(value);
					return;
				}
				switch (parentElement)
				{
				case SchemaNames.Token.XsdComplexContentExtension:
					if (this.complexContentExtension.AnyAttribute != null)
					{
						this.SendValidationEvent("'anyAttribute' must be the last child.", null);
					}
					this.complexContentExtension.Attributes.Add(value);
					return;
				case SchemaNames.Token.XsdComplexContentRestriction:
					if (this.complexContentRestriction.AnyAttribute != null)
					{
						this.SendValidationEvent("'anyAttribute' must be the last child.", null);
					}
					this.complexContentRestriction.Attributes.Add(value);
					return;
				case SchemaNames.Token.XsdSimpleContent:
					break;
				case SchemaNames.Token.XsdSimpleContentExtension:
					if (this.simpleContentExtension.AnyAttribute != null)
					{
						this.SendValidationEvent("'anyAttribute' must be the last child.", null);
					}
					this.simpleContentExtension.Attributes.Add(value);
					return;
				case SchemaNames.Token.XsdSimpleContentRestriction:
					if (this.simpleContentRestriction.AnyAttribute != null)
					{
						this.SendValidationEvent("'anyAttribute' must be the last child.", null);
					}
					this.simpleContentRestriction.Attributes.Add(value);
					return;
				default:
					return;
				}
			}
			else
			{
				if (this.attributeGroup.AnyAttribute != null)
				{
					this.SendValidationEvent("'anyAttribute' must be the last child.", null);
				}
				this.attributeGroup.Attributes.Add(value);
			}
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x001163AC File Offset: 0x001145AC
		private void AddParticle(XmlSchemaParticle particle)
		{
			SchemaNames.Token parentElement = this.ParentElement;
			if (parentElement <= SchemaNames.Token.XsdSequence)
			{
				if (parentElement == SchemaNames.Token.XsdGroup)
				{
					if (this.group.Particle != null)
					{
						this.SendValidationEvent("The content model can only have one of the following; 'all', 'choice', or 'sequence'.", "particle");
					}
					this.group.Particle = (XmlSchemaGroupBase)particle;
					return;
				}
				if (parentElement - SchemaNames.Token.XsdChoice > 1)
				{
					return;
				}
				((XmlSchemaGroupBase)this.ParentContainer).Items.Add(particle);
				return;
			}
			else
			{
				if (parentElement == SchemaNames.Token.XsdComplexType)
				{
					if (this.complexType.ContentModel != null || this.complexType.Attributes.Count != 0 || this.complexType.AnyAttribute != null || this.complexType.Particle != null)
					{
						this.SendValidationEvent("The content model of a complex type must consist of 'annotation' (if present); followed by zero or one of the following: 'simpleContent', 'complexContent', 'group', 'choice', 'sequence', or 'all'; followed by zero or more 'attribute' or 'attributeGroup'; followed by zero or one 'anyAttribute'.", "complexType");
					}
					this.complexType.Particle = particle;
					return;
				}
				if (parentElement == SchemaNames.Token.XsdComplexContentExtension)
				{
					if (this.complexContentExtension.Particle != null || this.complexContentExtension.Attributes.Count != 0 || this.complexContentExtension.AnyAttribute != null)
					{
						this.SendValidationEvent("Complex content restriction or extension should consist of zero or one of 'group', 'choice', 'sequence', or 'all'; followed by zero or more 'attribute' or 'attributeGroup'; followed by zero or one 'anyAttribute'.", "ComplexContentExtension");
					}
					this.complexContentExtension.Particle = particle;
					return;
				}
				if (parentElement != SchemaNames.Token.XsdComplexContentRestriction)
				{
					return;
				}
				if (this.complexContentRestriction.Particle != null || this.complexContentRestriction.Attributes.Count != 0 || this.complexContentRestriction.AnyAttribute != null)
				{
					this.SendValidationEvent("Complex content restriction or extension should consist of zero or one of 'group', 'choice', 'sequence', or 'all'; followed by zero or more 'attribute' or 'attributeGroup'; followed by zero or one 'anyAttribute'.", "ComplexContentExtension");
				}
				this.complexContentRestriction.Particle = particle;
				return;
			}
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x0011651C File Offset: 0x0011471C
		private bool GetNextState(XmlQualifiedName qname)
		{
			if (this.currentEntry.NextStates != null)
			{
				for (int i = 0; i < this.currentEntry.NextStates.Length; i++)
				{
					int num = (int)this.currentEntry.NextStates[i];
					if (this.schemaNames.TokenToQName[(int)XsdBuilder.SchemaEntries[num].Name].Equals(qname))
					{
						this.nextEntry = XsdBuilder.SchemaEntries[num];
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x0011658C File Offset: 0x0011478C
		private bool IsSkipableElement(XmlQualifiedName qname)
		{
			return this.CurrentElement == SchemaNames.Token.XsdDocumentation || this.CurrentElement == SchemaNames.Token.XsdAppInfo;
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x001165A4 File Offset: 0x001147A4
		private void SetMinOccurs(XmlSchemaParticle particle, string value)
		{
			try
			{
				particle.MinOccursString = value;
			}
			catch (Exception)
			{
				this.SendValidationEvent("The value for the 'minOccurs' attribute must be xsd:nonNegativeInteger.", null);
			}
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x001165DC File Offset: 0x001147DC
		private void SetMaxOccurs(XmlSchemaParticle particle, string value)
		{
			try
			{
				particle.MaxOccursString = value;
			}
			catch (Exception)
			{
				this.SendValidationEvent("The value for the 'maxOccurs' attribute must be xsd:nonNegativeInteger or 'unbounded'.", null);
			}
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x00116614 File Offset: 0x00114814
		private bool ParseBoolean(string value, string attributeName)
		{
			bool flag;
			try
			{
				flag = XmlConvert.ToBoolean(value);
			}
			catch (Exception)
			{
				this.SendValidationEvent("'{1}' is an invalid value for the '{0}' attribute.", attributeName, value, null);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x00116650 File Offset: 0x00114850
		private int ParseEnum(string value, string attributeName, string[] values)
		{
			string text = value.Trim();
			for (int i = 0; i < values.Length; i++)
			{
				if (values[i] == text)
				{
					return i + 1;
				}
			}
			this.SendValidationEvent("'{1}' is an invalid value for the '{0}' attribute.", attributeName, text, null);
			return 0;
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x00116690 File Offset: 0x00114890
		private XmlQualifiedName ParseQName(string value, string attributeName)
		{
			XmlQualifiedName xmlQualifiedName;
			try
			{
				value = XmlComplianceUtil.NonCDataNormalize(value);
				string text;
				xmlQualifiedName = XmlQualifiedName.Parse(value, this.namespaceManager, out text);
			}
			catch (Exception)
			{
				this.SendValidationEvent("'{1}' is an invalid value for the '{0}' attribute.", attributeName, value, null);
				xmlQualifiedName = XmlQualifiedName.Empty;
			}
			return xmlQualifiedName;
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x001166E0 File Offset: 0x001148E0
		private int ParseBlockFinalEnum(string value, string attributeName)
		{
			int num = 0;
			string[] array = XmlConvert.SplitString(value);
			for (int i = 0; i < array.Length; i++)
			{
				bool flag = false;
				int j = 0;
				while (j < XsdBuilder.DerivationMethodStrings.Length)
				{
					if (array[i] == XsdBuilder.DerivationMethodStrings[j])
					{
						if ((num & XsdBuilder.DerivationMethodValues[j]) != 0 && (num & XsdBuilder.DerivationMethodValues[j]) != XsdBuilder.DerivationMethodValues[j])
						{
							this.SendValidationEvent("'{1}' is an invalid value for the '{0}' attribute.", attributeName, value, null);
							return 0;
						}
						num |= XsdBuilder.DerivationMethodValues[j];
						flag = true;
						break;
					}
					else
					{
						j++;
					}
				}
				if (!flag)
				{
					this.SendValidationEvent("'{1}' is an invalid value for the '{0}' attribute.", attributeName, value, null);
					return 0;
				}
				if (num == 255 && value.Length > 4)
				{
					this.SendValidationEvent("'{1}' is an invalid value for the '{0}' attribute.", attributeName, value, null);
					return 0;
				}
			}
			return num;
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x00002068 File Offset: 0x00000268
		private static string ParseUriReference(string s)
		{
			return s;
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x001167A8 File Offset: 0x001149A8
		private void SendValidationEvent(string code, string arg0, string arg1, string arg2)
		{
			this.SendValidationEvent(new XmlSchemaException(code, new string[] { arg0, arg1, arg2 }, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x001167F5 File Offset: 0x001149F5
		private void SendValidationEvent(string code, string msg)
		{
			this.SendValidationEvent(new XmlSchemaException(code, msg, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x00116825 File Offset: 0x00114A25
		private void SendValidationEvent(string code, string[] args, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, args, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x00116858 File Offset: 0x00114A58
		private void SendValidationEvent(XmlSchemaException e, XmlSeverityType severity)
		{
			XmlSchema xmlSchema = this.schema;
			int errorCount = xmlSchema.ErrorCount;
			xmlSchema.ErrorCount = errorCount + 1;
			e.SetSchemaObject(this.schema);
			if (this.validationEventHandler != null)
			{
				this.validationEventHandler(null, new ValidationEventArgs(e, severity));
				return;
			}
			if (severity == XmlSeverityType.Error)
			{
				throw e;
			}
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x001168A7 File Offset: 0x00114AA7
		private void SendValidationEvent(XmlSchemaException e)
		{
			this.SendValidationEvent(e, XmlSeverityType.Error);
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x001168B4 File Offset: 0x00114AB4
		private void RecordPosition()
		{
			this.xso.SourceUri = this.reader.BaseURI;
			this.xso.LineNumber = this.positionInfo.LineNumber;
			this.xso.LinePosition = this.positionInfo.LinePosition;
			if (this.xso != this.schema)
			{
				this.xso.Parent = this.ParentContainer;
			}
		}

		// Token: 0x04001EF0 RID: 7920
		private const int STACK_INCREMENT = 10;

		// Token: 0x04001EF1 RID: 7921
		private static readonly XsdBuilder.State[] SchemaElement = new XsdBuilder.State[] { XsdBuilder.State.Schema };

		// Token: 0x04001EF2 RID: 7922
		private static readonly XsdBuilder.State[] SchemaSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Include,
			XsdBuilder.State.Import,
			XsdBuilder.State.Redefine,
			XsdBuilder.State.ComplexType,
			XsdBuilder.State.SimpleType,
			XsdBuilder.State.Element,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroup,
			XsdBuilder.State.Group,
			XsdBuilder.State.Notation
		};

		// Token: 0x04001EF3 RID: 7923
		private static readonly XsdBuilder.State[] AttributeSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType
		};

		// Token: 0x04001EF4 RID: 7924
		private static readonly XsdBuilder.State[] ElementSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType,
			XsdBuilder.State.ComplexType,
			XsdBuilder.State.Unique,
			XsdBuilder.State.Key,
			XsdBuilder.State.KeyRef
		};

		// Token: 0x04001EF5 RID: 7925
		private static readonly XsdBuilder.State[] ComplexTypeSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleContent,
			XsdBuilder.State.ComplexContent,
			XsdBuilder.State.GroupRef,
			XsdBuilder.State.All,
			XsdBuilder.State.Choice,
			XsdBuilder.State.Sequence,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x04001EF6 RID: 7926
		private static readonly XsdBuilder.State[] SimpleContentSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleContentRestriction,
			XsdBuilder.State.SimpleContentExtension
		};

		// Token: 0x04001EF7 RID: 7927
		private static readonly XsdBuilder.State[] SimpleContentExtensionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x04001EF8 RID: 7928
		private static readonly XsdBuilder.State[] SimpleContentRestrictionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType,
			XsdBuilder.State.Enumeration,
			XsdBuilder.State.Length,
			XsdBuilder.State.MaxExclusive,
			XsdBuilder.State.MaxInclusive,
			XsdBuilder.State.MaxLength,
			XsdBuilder.State.MinExclusive,
			XsdBuilder.State.MinInclusive,
			XsdBuilder.State.MinLength,
			XsdBuilder.State.Pattern,
			XsdBuilder.State.TotalDigits,
			XsdBuilder.State.FractionDigits,
			XsdBuilder.State.WhiteSpace,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x04001EF9 RID: 7929
		private static readonly XsdBuilder.State[] ComplexContentSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.ComplexContentRestriction,
			XsdBuilder.State.ComplexContentExtension
		};

		// Token: 0x04001EFA RID: 7930
		private static readonly XsdBuilder.State[] ComplexContentExtensionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.GroupRef,
			XsdBuilder.State.All,
			XsdBuilder.State.Choice,
			XsdBuilder.State.Sequence,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x04001EFB RID: 7931
		private static readonly XsdBuilder.State[] ComplexContentRestrictionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.GroupRef,
			XsdBuilder.State.All,
			XsdBuilder.State.Choice,
			XsdBuilder.State.Sequence,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x04001EFC RID: 7932
		private static readonly XsdBuilder.State[] SimpleTypeSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleTypeList,
			XsdBuilder.State.SimpleTypeRestriction,
			XsdBuilder.State.SimpleTypeUnion
		};

		// Token: 0x04001EFD RID: 7933
		private static readonly XsdBuilder.State[] SimpleTypeRestrictionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType,
			XsdBuilder.State.Enumeration,
			XsdBuilder.State.Length,
			XsdBuilder.State.MaxExclusive,
			XsdBuilder.State.MaxInclusive,
			XsdBuilder.State.MaxLength,
			XsdBuilder.State.MinExclusive,
			XsdBuilder.State.MinInclusive,
			XsdBuilder.State.MinLength,
			XsdBuilder.State.Pattern,
			XsdBuilder.State.TotalDigits,
			XsdBuilder.State.FractionDigits,
			XsdBuilder.State.WhiteSpace
		};

		// Token: 0x04001EFE RID: 7934
		private static readonly XsdBuilder.State[] SimpleTypeListSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType
		};

		// Token: 0x04001EFF RID: 7935
		private static readonly XsdBuilder.State[] SimpleTypeUnionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType
		};

		// Token: 0x04001F00 RID: 7936
		private static readonly XsdBuilder.State[] RedefineSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.AttributeGroup,
			XsdBuilder.State.ComplexType,
			XsdBuilder.State.Group,
			XsdBuilder.State.SimpleType
		};

		// Token: 0x04001F01 RID: 7937
		private static readonly XsdBuilder.State[] AttributeGroupSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x04001F02 RID: 7938
		private static readonly XsdBuilder.State[] GroupSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.All,
			XsdBuilder.State.Choice,
			XsdBuilder.State.Sequence
		};

		// Token: 0x04001F03 RID: 7939
		private static readonly XsdBuilder.State[] AllSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Element
		};

		// Token: 0x04001F04 RID: 7940
		private static readonly XsdBuilder.State[] ChoiceSequenceSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Element,
			XsdBuilder.State.GroupRef,
			XsdBuilder.State.Choice,
			XsdBuilder.State.Sequence,
			XsdBuilder.State.Any
		};

		// Token: 0x04001F05 RID: 7941
		private static readonly XsdBuilder.State[] IdentityConstraintSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Selector,
			XsdBuilder.State.Field
		};

		// Token: 0x04001F06 RID: 7942
		private static readonly XsdBuilder.State[] AnnotationSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.AppInfo,
			XsdBuilder.State.Documentation
		};

		// Token: 0x04001F07 RID: 7943
		private static readonly XsdBuilder.State[] AnnotatedSubelements = new XsdBuilder.State[] { XsdBuilder.State.Annotation };

		// Token: 0x04001F08 RID: 7944
		private static readonly XsdBuilder.XsdAttributeEntry[] SchemaAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaAttributeFormDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_AttributeFormDefault)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaElementFormDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_ElementFormDefault)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaTargetNamespace, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_TargetNamespace)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaVersion, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_Version)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFinalDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_FinalDefault)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBlockDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_BlockDefault))
		};

		// Token: 0x04001F09 RID: 7945
		private static readonly XsdBuilder.XsdAttributeEntry[] AttributeAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Default)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFixed, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Fixed)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaForm, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Form)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Name)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaRef, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Ref)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaType, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Type)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaUse, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Use))
		};

		// Token: 0x04001F0A RID: 7946
		private static readonly XsdBuilder.XsdAttributeEntry[] ElementAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaAbstract, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Abstract)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBlock, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Block)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Default)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFinal, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Final)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFixed, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Fixed)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaForm, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Form)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_MaxOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMinOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_MinOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Name)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaNillable, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Nillable)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaRef, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Ref)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSubstitutionGroup, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_SubstitutionGroup)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaType, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Type))
		};

		// Token: 0x04001F0B RID: 7947
		private static readonly XsdBuilder.XsdAttributeEntry[] ComplexTypeAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaAbstract, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexType_Abstract)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBlock, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexType_Block)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFinal, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexType_Final)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMixed, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexType_Mixed)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexType_Name))
		};

		// Token: 0x04001F0C RID: 7948
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleContentAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x04001F0D RID: 7949
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleContentExtensionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBase, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleContentExtension_Base)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x04001F0E RID: 7950
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleContentRestrictionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBase, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleContentRestriction_Base)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x04001F0F RID: 7951
		private static readonly XsdBuilder.XsdAttributeEntry[] ComplexContentAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMixed, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexContent_Mixed))
		};

		// Token: 0x04001F10 RID: 7952
		private static readonly XsdBuilder.XsdAttributeEntry[] ComplexContentExtensionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBase, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexContentExtension_Base)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x04001F11 RID: 7953
		private static readonly XsdBuilder.XsdAttributeEntry[] ComplexContentRestrictionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBase, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexContentRestriction_Base)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x04001F12 RID: 7954
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleTypeAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFinal, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleType_Final)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleType_Name))
		};

		// Token: 0x04001F13 RID: 7955
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleTypeRestrictionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBase, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleTypeRestriction_Base)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x04001F14 RID: 7956
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleTypeUnionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMemberTypes, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleTypeUnion_MemberTypes))
		};

		// Token: 0x04001F15 RID: 7957
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleTypeListAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaItemType, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleTypeList_ItemType))
		};

		// Token: 0x04001F16 RID: 7958
		private static readonly XsdBuilder.XsdAttributeEntry[] AttributeGroupAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttributeGroup_Name))
		};

		// Token: 0x04001F17 RID: 7959
		private static readonly XsdBuilder.XsdAttributeEntry[] AttributeGroupRefAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaRef, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttributeGroupRef_Ref))
		};

		// Token: 0x04001F18 RID: 7960
		private static readonly XsdBuilder.XsdAttributeEntry[] GroupAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildGroup_Name))
		};

		// Token: 0x04001F19 RID: 7961
		private static readonly XsdBuilder.XsdAttributeEntry[] GroupRefAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MaxOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMinOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MinOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaRef, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildGroupRef_Ref))
		};

		// Token: 0x04001F1A RID: 7962
		private static readonly XsdBuilder.XsdAttributeEntry[] ParticleAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MaxOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMinOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MinOccurs))
		};

		// Token: 0x04001F1B RID: 7963
		private static readonly XsdBuilder.XsdAttributeEntry[] AnyAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MaxOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMinOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MinOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaNamespace, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAny_Namespace)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaProcessContents, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAny_ProcessContents))
		};

		// Token: 0x04001F1C RID: 7964
		private static readonly XsdBuilder.XsdAttributeEntry[] IdentityConstraintAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildIdentityConstraint_Name)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaRefer, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildIdentityConstraint_Refer))
		};

		// Token: 0x04001F1D RID: 7965
		private static readonly XsdBuilder.XsdAttributeEntry[] SelectorAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaXPath, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSelector_XPath))
		};

		// Token: 0x04001F1E RID: 7966
		private static readonly XsdBuilder.XsdAttributeEntry[] FieldAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaXPath, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildField_XPath))
		};

		// Token: 0x04001F1F RID: 7967
		private static readonly XsdBuilder.XsdAttributeEntry[] NotationAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildNotation_Name)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaPublic, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildNotation_Public)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSystem, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildNotation_System))
		};

		// Token: 0x04001F20 RID: 7968
		private static readonly XsdBuilder.XsdAttributeEntry[] IncludeAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSchemaLocation, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildInclude_SchemaLocation))
		};

		// Token: 0x04001F21 RID: 7969
		private static readonly XsdBuilder.XsdAttributeEntry[] ImportAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaNamespace, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildImport_Namespace)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSchemaLocation, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildImport_SchemaLocation))
		};

		// Token: 0x04001F22 RID: 7970
		private static readonly XsdBuilder.XsdAttributeEntry[] FacetAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFixed, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildFacet_Fixed)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaValue, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildFacet_Value))
		};

		// Token: 0x04001F23 RID: 7971
		private static readonly XsdBuilder.XsdAttributeEntry[] AnyAttributeAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaNamespace, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnyAttribute_Namespace)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaProcessContents, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnyAttribute_ProcessContents))
		};

		// Token: 0x04001F24 RID: 7972
		private static readonly XsdBuilder.XsdAttributeEntry[] DocumentationAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSource, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildDocumentation_Source)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.XmlLang, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildDocumentation_XmlLang))
		};

		// Token: 0x04001F25 RID: 7973
		private static readonly XsdBuilder.XsdAttributeEntry[] AppinfoAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSource, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAppinfo_Source))
		};

		// Token: 0x04001F26 RID: 7974
		private static readonly XsdBuilder.XsdAttributeEntry[] RedefineAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSchemaLocation, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildRedefine_SchemaLocation))
		};

		// Token: 0x04001F27 RID: 7975
		private static readonly XsdBuilder.XsdAttributeEntry[] AnnotationAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x04001F28 RID: 7976
		private static readonly XsdBuilder.XsdEntry[] SchemaEntries = new XsdBuilder.XsdEntry[]
		{
			new XsdBuilder.XsdEntry(SchemaNames.Token.Empty, XsdBuilder.State.Root, XsdBuilder.SchemaElement, null, null, null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSchema, XsdBuilder.State.Schema, XsdBuilder.SchemaSubelements, XsdBuilder.SchemaAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSchema), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAnnotation, XsdBuilder.State.Annotation, XsdBuilder.AnnotationSubelements, XsdBuilder.AnnotationAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAnnotation), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdInclude, XsdBuilder.State.Include, XsdBuilder.AnnotatedSubelements, XsdBuilder.IncludeAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitInclude), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdImport, XsdBuilder.State.Import, XsdBuilder.AnnotatedSubelements, XsdBuilder.ImportAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitImport), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdElement, XsdBuilder.State.Element, XsdBuilder.ElementSubelements, XsdBuilder.ElementAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitElement), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAttribute, XsdBuilder.State.Attribute, XsdBuilder.AttributeSubelements, XsdBuilder.AttributeAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAttribute), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.xsdAttributeGroup, XsdBuilder.State.AttributeGroup, XsdBuilder.AttributeGroupSubelements, XsdBuilder.AttributeGroupAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAttributeGroup), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.xsdAttributeGroup, XsdBuilder.State.AttributeGroupRef, XsdBuilder.AnnotatedSubelements, XsdBuilder.AttributeGroupRefAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAttributeGroupRef), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAnyAttribute, XsdBuilder.State.AnyAttribute, XsdBuilder.AnnotatedSubelements, XsdBuilder.AnyAttributeAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAnyAttribute), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdGroup, XsdBuilder.State.Group, XsdBuilder.GroupSubelements, XsdBuilder.GroupAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitGroup), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdGroup, XsdBuilder.State.GroupRef, XsdBuilder.AnnotatedSubelements, XsdBuilder.GroupRefAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitGroupRef), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAll, XsdBuilder.State.All, XsdBuilder.AllSubelements, XsdBuilder.ParticleAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAll), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdChoice, XsdBuilder.State.Choice, XsdBuilder.ChoiceSequenceSubelements, XsdBuilder.ParticleAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitChoice), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSequence, XsdBuilder.State.Sequence, XsdBuilder.ChoiceSequenceSubelements, XsdBuilder.ParticleAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSequence), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAny, XsdBuilder.State.Any, XsdBuilder.AnnotatedSubelements, XsdBuilder.AnyAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAny), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdNotation, XsdBuilder.State.Notation, XsdBuilder.AnnotatedSubelements, XsdBuilder.NotationAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitNotation), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleType, XsdBuilder.State.SimpleType, XsdBuilder.SimpleTypeSubelements, XsdBuilder.SimpleTypeAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleType), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdComplexType, XsdBuilder.State.ComplexType, XsdBuilder.ComplexTypeSubelements, XsdBuilder.ComplexTypeAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitComplexType), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdComplexContent, XsdBuilder.State.ComplexContent, XsdBuilder.ComplexContentSubelements, XsdBuilder.ComplexContentAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitComplexContent), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdComplexContentRestriction, XsdBuilder.State.ComplexContentRestriction, XsdBuilder.ComplexContentRestrictionSubelements, XsdBuilder.ComplexContentRestrictionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitComplexContentRestriction), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdComplexContentExtension, XsdBuilder.State.ComplexContentExtension, XsdBuilder.ComplexContentExtensionSubelements, XsdBuilder.ComplexContentExtensionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitComplexContentExtension), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleContent, XsdBuilder.State.SimpleContent, XsdBuilder.SimpleContentSubelements, XsdBuilder.SimpleContentAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleContent), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleContentExtension, XsdBuilder.State.SimpleContentExtension, XsdBuilder.SimpleContentExtensionSubelements, XsdBuilder.SimpleContentExtensionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleContentExtension), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleContentRestriction, XsdBuilder.State.SimpleContentRestriction, XsdBuilder.SimpleContentRestrictionSubelements, XsdBuilder.SimpleContentRestrictionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleContentRestriction), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleTypeUnion, XsdBuilder.State.SimpleTypeUnion, XsdBuilder.SimpleTypeUnionSubelements, XsdBuilder.SimpleTypeUnionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleTypeUnion), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleTypeList, XsdBuilder.State.SimpleTypeList, XsdBuilder.SimpleTypeListSubelements, XsdBuilder.SimpleTypeListAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleTypeList), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleTypeRestriction, XsdBuilder.State.SimpleTypeRestriction, XsdBuilder.SimpleTypeRestrictionSubelements, XsdBuilder.SimpleTypeRestrictionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleTypeRestriction), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdUnique, XsdBuilder.State.Unique, XsdBuilder.IdentityConstraintSubelements, XsdBuilder.IdentityConstraintAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitIdentityConstraint), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdKey, XsdBuilder.State.Key, XsdBuilder.IdentityConstraintSubelements, XsdBuilder.IdentityConstraintAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitIdentityConstraint), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdKeyref, XsdBuilder.State.KeyRef, XsdBuilder.IdentityConstraintSubelements, XsdBuilder.IdentityConstraintAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitIdentityConstraint), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSelector, XsdBuilder.State.Selector, XsdBuilder.AnnotatedSubelements, XsdBuilder.SelectorAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSelector), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdField, XsdBuilder.State.Field, XsdBuilder.AnnotatedSubelements, XsdBuilder.FieldAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitField), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMinExclusive, XsdBuilder.State.MinExclusive, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMinInclusive, XsdBuilder.State.MinInclusive, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMaxExclusive, XsdBuilder.State.MaxExclusive, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMaxInclusive, XsdBuilder.State.MaxInclusive, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdTotalDigits, XsdBuilder.State.TotalDigits, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdFractionDigits, XsdBuilder.State.FractionDigits, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdLength, XsdBuilder.State.Length, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMinLength, XsdBuilder.State.MinLength, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMaxLength, XsdBuilder.State.MaxLength, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdEnumeration, XsdBuilder.State.Enumeration, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdPattern, XsdBuilder.State.Pattern, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdWhitespace, XsdBuilder.State.WhiteSpace, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAppInfo, XsdBuilder.State.AppInfo, null, XsdBuilder.AppinfoAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAppinfo), new XsdBuilder.XsdEndChildFunction(XsdBuilder.EndAppinfo), false),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdDocumentation, XsdBuilder.State.Documentation, null, XsdBuilder.DocumentationAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitDocumentation), new XsdBuilder.XsdEndChildFunction(XsdBuilder.EndDocumentation), false),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdRedefine, XsdBuilder.State.Redefine, XsdBuilder.RedefineSubelements, XsdBuilder.RedefineAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitRedefine), new XsdBuilder.XsdEndChildFunction(XsdBuilder.EndRedefine), true)
		};

		// Token: 0x04001F29 RID: 7977
		private static readonly int[] DerivationMethodValues = new int[] { 1, 2, 4, 8, 16, 255 };

		// Token: 0x04001F2A RID: 7978
		private static readonly string[] DerivationMethodStrings = new string[] { "substitution", "extension", "restriction", "list", "union", "#all" };

		// Token: 0x04001F2B RID: 7979
		private static readonly string[] FormStringValues = new string[] { "qualified", "unqualified" };

		// Token: 0x04001F2C RID: 7980
		private static readonly string[] UseStringValues = new string[] { "optional", "prohibited", "required" };

		// Token: 0x04001F2D RID: 7981
		private static readonly string[] ProcessContentsStringValues = new string[] { "skip", "lax", "strict" };

		// Token: 0x04001F2E RID: 7982
		private XmlReader reader;

		// Token: 0x04001F2F RID: 7983
		private PositionInfo positionInfo;

		// Token: 0x04001F30 RID: 7984
		private XsdBuilder.XsdEntry currentEntry;

		// Token: 0x04001F31 RID: 7985
		private XsdBuilder.XsdEntry nextEntry;

		// Token: 0x04001F32 RID: 7986
		private bool hasChild;

		// Token: 0x04001F33 RID: 7987
		private HWStack stateHistory = new HWStack(10);

		// Token: 0x04001F34 RID: 7988
		private Stack containerStack = new Stack();

		// Token: 0x04001F35 RID: 7989
		private XmlNameTable nameTable;

		// Token: 0x04001F36 RID: 7990
		private SchemaNames schemaNames;

		// Token: 0x04001F37 RID: 7991
		private XmlNamespaceManager namespaceManager;

		// Token: 0x04001F38 RID: 7992
		private bool canIncludeImport;

		// Token: 0x04001F39 RID: 7993
		private XmlSchema schema;

		// Token: 0x04001F3A RID: 7994
		private XmlSchemaObject xso;

		// Token: 0x04001F3B RID: 7995
		private XmlSchemaElement element;

		// Token: 0x04001F3C RID: 7996
		private XmlSchemaAny anyElement;

		// Token: 0x04001F3D RID: 7997
		private XmlSchemaAttribute attribute;

		// Token: 0x04001F3E RID: 7998
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04001F3F RID: 7999
		private XmlSchemaComplexType complexType;

		// Token: 0x04001F40 RID: 8000
		private XmlSchemaSimpleType simpleType;

		// Token: 0x04001F41 RID: 8001
		private XmlSchemaComplexContent complexContent;

		// Token: 0x04001F42 RID: 8002
		private XmlSchemaComplexContentExtension complexContentExtension;

		// Token: 0x04001F43 RID: 8003
		private XmlSchemaComplexContentRestriction complexContentRestriction;

		// Token: 0x04001F44 RID: 8004
		private XmlSchemaSimpleContent simpleContent;

		// Token: 0x04001F45 RID: 8005
		private XmlSchemaSimpleContentExtension simpleContentExtension;

		// Token: 0x04001F46 RID: 8006
		private XmlSchemaSimpleContentRestriction simpleContentRestriction;

		// Token: 0x04001F47 RID: 8007
		private XmlSchemaSimpleTypeUnion simpleTypeUnion;

		// Token: 0x04001F48 RID: 8008
		private XmlSchemaSimpleTypeList simpleTypeList;

		// Token: 0x04001F49 RID: 8009
		private XmlSchemaSimpleTypeRestriction simpleTypeRestriction;

		// Token: 0x04001F4A RID: 8010
		private XmlSchemaGroup group;

		// Token: 0x04001F4B RID: 8011
		private XmlSchemaGroupRef groupRef;

		// Token: 0x04001F4C RID: 8012
		private XmlSchemaAll all;

		// Token: 0x04001F4D RID: 8013
		private XmlSchemaChoice choice;

		// Token: 0x04001F4E RID: 8014
		private XmlSchemaSequence sequence;

		// Token: 0x04001F4F RID: 8015
		private XmlSchemaParticle particle;

		// Token: 0x04001F50 RID: 8016
		private XmlSchemaAttributeGroup attributeGroup;

		// Token: 0x04001F51 RID: 8017
		private XmlSchemaAttributeGroupRef attributeGroupRef;

		// Token: 0x04001F52 RID: 8018
		private XmlSchemaNotation notation;

		// Token: 0x04001F53 RID: 8019
		private XmlSchemaIdentityConstraint identityConstraint;

		// Token: 0x04001F54 RID: 8020
		private XmlSchemaXPath xpath;

		// Token: 0x04001F55 RID: 8021
		private XmlSchemaInclude include;

		// Token: 0x04001F56 RID: 8022
		private XmlSchemaImport import;

		// Token: 0x04001F57 RID: 8023
		private XmlSchemaAnnotation annotation;

		// Token: 0x04001F58 RID: 8024
		private XmlSchemaAppInfo appInfo;

		// Token: 0x04001F59 RID: 8025
		private XmlSchemaDocumentation documentation;

		// Token: 0x04001F5A RID: 8026
		private XmlSchemaFacet facet;

		// Token: 0x04001F5B RID: 8027
		private XmlNode[] markup;

		// Token: 0x04001F5C RID: 8028
		private XmlSchemaRedefine redefine;

		// Token: 0x04001F5D RID: 8029
		private ValidationEventHandler validationEventHandler;

		// Token: 0x04001F5E RID: 8030
		private ArrayList unhandledAttributes = new ArrayList();

		// Token: 0x04001F5F RID: 8031
		private Hashtable namespaces;

		// Token: 0x020004A1 RID: 1185
		private enum State
		{
			// Token: 0x04001F61 RID: 8033
			Root,
			// Token: 0x04001F62 RID: 8034
			Schema,
			// Token: 0x04001F63 RID: 8035
			Annotation,
			// Token: 0x04001F64 RID: 8036
			Include,
			// Token: 0x04001F65 RID: 8037
			Import,
			// Token: 0x04001F66 RID: 8038
			Element,
			// Token: 0x04001F67 RID: 8039
			Attribute,
			// Token: 0x04001F68 RID: 8040
			AttributeGroup,
			// Token: 0x04001F69 RID: 8041
			AttributeGroupRef,
			// Token: 0x04001F6A RID: 8042
			AnyAttribute,
			// Token: 0x04001F6B RID: 8043
			Group,
			// Token: 0x04001F6C RID: 8044
			GroupRef,
			// Token: 0x04001F6D RID: 8045
			All,
			// Token: 0x04001F6E RID: 8046
			Choice,
			// Token: 0x04001F6F RID: 8047
			Sequence,
			// Token: 0x04001F70 RID: 8048
			Any,
			// Token: 0x04001F71 RID: 8049
			Notation,
			// Token: 0x04001F72 RID: 8050
			SimpleType,
			// Token: 0x04001F73 RID: 8051
			ComplexType,
			// Token: 0x04001F74 RID: 8052
			ComplexContent,
			// Token: 0x04001F75 RID: 8053
			ComplexContentRestriction,
			// Token: 0x04001F76 RID: 8054
			ComplexContentExtension,
			// Token: 0x04001F77 RID: 8055
			SimpleContent,
			// Token: 0x04001F78 RID: 8056
			SimpleContentExtension,
			// Token: 0x04001F79 RID: 8057
			SimpleContentRestriction,
			// Token: 0x04001F7A RID: 8058
			SimpleTypeUnion,
			// Token: 0x04001F7B RID: 8059
			SimpleTypeList,
			// Token: 0x04001F7C RID: 8060
			SimpleTypeRestriction,
			// Token: 0x04001F7D RID: 8061
			Unique,
			// Token: 0x04001F7E RID: 8062
			Key,
			// Token: 0x04001F7F RID: 8063
			KeyRef,
			// Token: 0x04001F80 RID: 8064
			Selector,
			// Token: 0x04001F81 RID: 8065
			Field,
			// Token: 0x04001F82 RID: 8066
			MinExclusive,
			// Token: 0x04001F83 RID: 8067
			MinInclusive,
			// Token: 0x04001F84 RID: 8068
			MaxExclusive,
			// Token: 0x04001F85 RID: 8069
			MaxInclusive,
			// Token: 0x04001F86 RID: 8070
			TotalDigits,
			// Token: 0x04001F87 RID: 8071
			FractionDigits,
			// Token: 0x04001F88 RID: 8072
			Length,
			// Token: 0x04001F89 RID: 8073
			MinLength,
			// Token: 0x04001F8A RID: 8074
			MaxLength,
			// Token: 0x04001F8B RID: 8075
			Enumeration,
			// Token: 0x04001F8C RID: 8076
			Pattern,
			// Token: 0x04001F8D RID: 8077
			WhiteSpace,
			// Token: 0x04001F8E RID: 8078
			AppInfo,
			// Token: 0x04001F8F RID: 8079
			Documentation,
			// Token: 0x04001F90 RID: 8080
			Redefine
		}

		// Token: 0x020004A2 RID: 1186
		// (Invoke) Token: 0x06003068 RID: 12392
		private delegate void XsdBuildFunction(XsdBuilder builder, string value);

		// Token: 0x020004A3 RID: 1187
		// (Invoke) Token: 0x0600306C RID: 12396
		private delegate void XsdInitFunction(XsdBuilder builder, string value);

		// Token: 0x020004A4 RID: 1188
		// (Invoke) Token: 0x06003070 RID: 12400
		private delegate void XsdEndChildFunction(XsdBuilder builder);

		// Token: 0x020004A5 RID: 1189
		private sealed class XsdAttributeEntry
		{
			// Token: 0x06003073 RID: 12403 RVA: 0x00117CBE File Offset: 0x00115EBE
			public XsdAttributeEntry(SchemaNames.Token a, XsdBuilder.XsdBuildFunction build)
			{
				this.Attribute = a;
				this.BuildFunc = build;
			}

			// Token: 0x04001F91 RID: 8081
			public SchemaNames.Token Attribute;

			// Token: 0x04001F92 RID: 8082
			public XsdBuilder.XsdBuildFunction BuildFunc;
		}

		// Token: 0x020004A6 RID: 1190
		private sealed class XsdEntry
		{
			// Token: 0x06003074 RID: 12404 RVA: 0x00117CD4 File Offset: 0x00115ED4
			public XsdEntry(SchemaNames.Token n, XsdBuilder.State state, XsdBuilder.State[] nextStates, XsdBuilder.XsdAttributeEntry[] attributes, XsdBuilder.XsdInitFunction init, XsdBuilder.XsdEndChildFunction end, bool parseContent)
			{
				this.Name = n;
				this.CurrentState = state;
				this.NextStates = nextStates;
				this.Attributes = attributes;
				this.InitFunc = init;
				this.EndChildFunc = end;
				this.ParseContent = parseContent;
			}

			// Token: 0x04001F93 RID: 8083
			public SchemaNames.Token Name;

			// Token: 0x04001F94 RID: 8084
			public XsdBuilder.State CurrentState;

			// Token: 0x04001F95 RID: 8085
			public XsdBuilder.State[] NextStates;

			// Token: 0x04001F96 RID: 8086
			public XsdBuilder.XsdAttributeEntry[] Attributes;

			// Token: 0x04001F97 RID: 8087
			public XsdBuilder.XsdInitFunction InitFunc;

			// Token: 0x04001F98 RID: 8088
			public XsdBuilder.XsdEndChildFunction EndChildFunc;

			// Token: 0x04001F99 RID: 8089
			public bool ParseContent;
		}

		// Token: 0x020004A7 RID: 1191
		private class BuilderNamespaceManager : XmlNamespaceManager
		{
			// Token: 0x06003075 RID: 12405 RVA: 0x00117D11 File Offset: 0x00115F11
			public BuilderNamespaceManager(XmlNamespaceManager nsMgr, XmlReader reader)
			{
				this.nsMgr = nsMgr;
				this.reader = reader;
			}

			// Token: 0x06003076 RID: 12406 RVA: 0x00117D28 File Offset: 0x00115F28
			public override string LookupNamespace(string prefix)
			{
				string text = this.nsMgr.LookupNamespace(prefix);
				if (text == null)
				{
					text = this.reader.LookupNamespace(prefix);
				}
				return text;
			}

			// Token: 0x04001F9A RID: 8090
			private XmlNamespaceManager nsMgr;

			// Token: 0x04001F9B RID: 8091
			private XmlReader reader;
		}
	}
}
