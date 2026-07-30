using System;
using System.Collections;
using System.IO;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x020004B0 RID: 1200
	internal sealed class XsdValidator : BaseValidator
	{
		// Token: 0x060030BD RID: 12477 RVA: 0x00119E65 File Offset: 0x00118065
		internal XsdValidator(BaseValidator validator)
			: base(validator)
		{
			this.Init();
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x00119E7B File Offset: 0x0011807B
		internal XsdValidator(XmlValidatingReaderImpl reader, XmlSchemaCollection schemaCollection, IValidationEventHandling eventHandling)
			: base(reader, schemaCollection, eventHandling)
		{
			this.Init();
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x00119E94 File Offset: 0x00118094
		private void Init()
		{
			this.nsManager = this.reader.NamespaceManager;
			if (this.nsManager == null)
			{
				this.nsManager = new XmlNamespaceManager(base.NameTable);
				this.bManageNamespaces = true;
			}
			this.validationStack = new HWStack(10);
			this.textValue = new StringBuilder();
			this.attPresence = new Hashtable();
			this.schemaInfo = new SchemaInfo();
			this.checkDatatype = false;
			this.processContents = XmlSchemaContentProcessing.Strict;
			this.Push(XmlQualifiedName.Empty);
			this.NsXmlNs = base.NameTable.Add("http://www.w3.org/2000/xmlns/");
			this.NsXs = base.NameTable.Add("http://www.w3.org/2001/XMLSchema");
			this.NsXsi = base.NameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
			this.XsiType = base.NameTable.Add("type");
			this.XsiNil = base.NameTable.Add("nil");
			this.XsiSchemaLocation = base.NameTable.Add("schemaLocation");
			this.XsiNoNamespaceSchemaLocation = base.NameTable.Add("noNamespaceSchemaLocation");
			this.XsdSchema = base.NameTable.Add("schema");
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x00119FCC File Offset: 0x001181CC
		public override void Validate()
		{
			if (this.IsInlineSchemaStarted)
			{
				this.ProcessInlineSchema();
				return;
			}
			XmlNodeType nodeType = this.reader.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType - XmlNodeType.Text > 1)
				{
					switch (nodeType)
					{
					case XmlNodeType.Whitespace:
						base.ValidateWhitespace();
						return;
					case XmlNodeType.SignificantWhitespace:
						break;
					case XmlNodeType.EndElement:
						goto IL_005E;
					default:
						return;
					}
				}
				base.ValidateText();
				return;
			}
			this.ValidateElement();
			if (!this.reader.IsEmptyElement)
			{
				return;
			}
			IL_005E:
			this.ValidateEndElement();
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x0011A03D File Offset: 0x0011823D
		public override void CompleteValidation()
		{
			this.CheckForwardRefs();
		}

		// Token: 0x17000A40 RID: 2624
		// (set) Token: 0x060030C2 RID: 12482 RVA: 0x0011A045 File Offset: 0x00118245
		public ValidationState Context
		{
			set
			{
				this.context = value;
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x060030C3 RID: 12483 RVA: 0x0011A04E File Offset: 0x0011824E
		public static XmlSchemaDatatype DtQName
		{
			get
			{
				return XsdValidator.dtQName;
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x060030C4 RID: 12484 RVA: 0x0011A055 File Offset: 0x00118255
		private bool IsInlineSchemaStarted
		{
			get
			{
				return this.inlineSchemaParser != null;
			}
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x0011A060 File Offset: 0x00118260
		private void ProcessInlineSchema()
		{
			if (!this.inlineSchemaParser.ParseReaderNode())
			{
				this.inlineSchemaParser.FinishParsing();
				XmlSchema xmlSchema = this.inlineSchemaParser.XmlSchema;
				if (xmlSchema != null && xmlSchema.ErrorCount == 0)
				{
					try
					{
						SchemaInfo schemaInfo = new SchemaInfo();
						schemaInfo.SchemaType = SchemaType.XSD;
						string text = ((xmlSchema.TargetNamespace == null) ? string.Empty : xmlSchema.TargetNamespace);
						if (!base.SchemaInfo.TargetNamespaces.ContainsKey(text) && base.SchemaCollection.Add(text, schemaInfo, xmlSchema, true) != null)
						{
							base.SchemaInfo.Add(schemaInfo, base.EventHandler);
						}
					}
					catch (XmlSchemaException ex)
					{
						base.SendValidationEvent("Cannot load the schema for the namespace '{0}' - {1}", new string[]
						{
							base.BaseUri.AbsoluteUri,
							ex.Message
						}, XmlSeverityType.Error);
					}
				}
				this.inlineSchemaParser = null;
			}
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x0011A148 File Offset: 0x00118348
		private void ValidateElement()
		{
			this.elementName.Init(this.reader.LocalName, this.reader.NamespaceURI);
			object obj = this.ValidateChildElement();
			if (this.IsXSDRoot(this.elementName.Name, this.elementName.Namespace) && this.reader.Depth > 0)
			{
				this.inlineSchemaParser = new Parser(SchemaType.XSD, base.NameTable, base.SchemaNames, base.EventHandler);
				this.inlineSchemaParser.StartParsing(this.reader, null);
				this.ProcessInlineSchema();
				return;
			}
			this.ProcessElement(obj);
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x0011A1E8 File Offset: 0x001183E8
		private object ValidateChildElement()
		{
			object obj = null;
			int num = 0;
			if (this.context.NeedValidateChildren)
			{
				if (this.context.IsNill)
				{
					base.SendValidationEvent("Element '{0}' must have no character or element children.", this.elementName.ToString());
					return null;
				}
				obj = this.context.ElementDecl.ContentValidator.ValidateElement(this.elementName, this.context, out num);
				if (obj == null)
				{
					this.processContents = (this.context.ProcessContents = XmlSchemaContentProcessing.Skip);
					if (num == -2)
					{
						base.SendValidationEvent("Element '{0}' cannot appear more than once if content model type is \"all\".", this.elementName.ToString());
					}
					XmlSchemaValidator.ElementValidationError(this.elementName, this.context, base.EventHandler, this.reader, this.reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition, null);
				}
			}
			return obj;
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x0011A2C8 File Offset: 0x001184C8
		private void ProcessElement(object particle)
		{
			SchemaElementDecl schemaElementDecl = this.FastGetElementDecl(particle);
			this.Push(this.elementName);
			if (this.bManageNamespaces)
			{
				this.nsManager.PushScope();
			}
			XmlQualifiedName xmlQualifiedName;
			string text;
			this.ProcessXsiAttributes(out xmlQualifiedName, out text);
			if (this.processContents != XmlSchemaContentProcessing.Skip)
			{
				if (schemaElementDecl == null || !xmlQualifiedName.IsEmpty || text != null)
				{
					schemaElementDecl = this.ThoroughGetElementDecl(schemaElementDecl, xmlQualifiedName, text);
				}
				if (schemaElementDecl == null)
				{
					if (this.HasSchema && this.processContents == XmlSchemaContentProcessing.Strict)
					{
						base.SendValidationEvent("The '{0}' element is not declared.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
					}
					else
					{
						base.SendValidationEvent("Could not find schema information for the element '{0}'.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace), XmlSeverityType.Warning);
					}
				}
			}
			this.context.ElementDecl = schemaElementDecl;
			this.ValidateStartElementIdentityConstraints();
			this.ValidateStartElement();
			if (this.context.ElementDecl != null)
			{
				this.ValidateEndStartElement();
				this.context.NeedValidateChildren = this.processContents != XmlSchemaContentProcessing.Skip;
				this.context.ElementDecl.ContentValidator.InitValidation(this.context);
			}
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x0011A3E8 File Offset: 0x001185E8
		private void ProcessXsiAttributes(out XmlQualifiedName xsiType, out string xsiNil)
		{
			string[] array = null;
			string text = null;
			xsiType = XmlQualifiedName.Empty;
			xsiNil = null;
			if (this.reader.Depth == 0)
			{
				this.LoadSchema(string.Empty, null);
				foreach (string text2 in this.nsManager.GetNamespacesInScope(XmlNamespaceScope.ExcludeXml).Values)
				{
					this.LoadSchema(text2, null);
				}
			}
			if (this.reader.MoveToFirstAttribute())
			{
				do
				{
					string namespaceURI = this.reader.NamespaceURI;
					string localName = this.reader.LocalName;
					if (Ref.Equal(namespaceURI, this.NsXmlNs))
					{
						this.LoadSchema(this.reader.Value, null);
						if (this.bManageNamespaces)
						{
							this.nsManager.AddNamespace((this.reader.Prefix.Length == 0) ? string.Empty : this.reader.LocalName, this.reader.Value);
						}
					}
					else if (Ref.Equal(namespaceURI, this.NsXsi))
					{
						if (Ref.Equal(localName, this.XsiSchemaLocation))
						{
							array = (string[])XsdValidator.dtStringArray.ParseValue(this.reader.Value, base.NameTable, this.nsManager);
						}
						else if (Ref.Equal(localName, this.XsiNoNamespaceSchemaLocation))
						{
							text = this.reader.Value;
						}
						else if (Ref.Equal(localName, this.XsiType))
						{
							xsiType = (XmlQualifiedName)XsdValidator.dtQName.ParseValue(this.reader.Value, base.NameTable, this.nsManager);
						}
						else if (Ref.Equal(localName, this.XsiNil))
						{
							xsiNil = this.reader.Value;
						}
					}
				}
				while (this.reader.MoveToNextAttribute());
				this.reader.MoveToElement();
			}
			if (text != null)
			{
				this.LoadSchema(string.Empty, text);
			}
			if (array != null)
			{
				for (int i = 0; i < array.Length - 1; i += 2)
				{
					this.LoadSchema(array[i], array[i + 1]);
				}
			}
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x0011A60C File Offset: 0x0011880C
		private void ValidateEndElement()
		{
			if (this.bManageNamespaces)
			{
				this.nsManager.PopScope();
			}
			if (this.context.ElementDecl != null)
			{
				if (!this.context.IsNill)
				{
					if (this.context.NeedValidateChildren && !this.context.ElementDecl.ContentValidator.CompleteValidation(this.context))
					{
						XmlSchemaValidator.CompleteValidationError(this.context, base.EventHandler, this.reader, this.reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition, null);
					}
					if (this.checkDatatype && !this.context.IsNill)
					{
						string text = ((!this.hasSibling) ? this.textString : this.textValue.ToString());
						if (text.Length != 0 || this.context.ElementDecl.DefaultValueTyped == null)
						{
							this.CheckValue(text, null);
							this.checkDatatype = false;
						}
					}
				}
				if (this.HasIdentityConstraints)
				{
					this.EndElementIdentityConstraints();
				}
			}
			this.Pop();
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x0011A720 File Offset: 0x00118920
		private SchemaElementDecl FastGetElementDecl(object particle)
		{
			SchemaElementDecl schemaElementDecl = null;
			if (particle != null)
			{
				XmlSchemaElement xmlSchemaElement = particle as XmlSchemaElement;
				if (xmlSchemaElement != null)
				{
					schemaElementDecl = xmlSchemaElement.ElementDecl;
				}
				else
				{
					XmlSchemaAny xmlSchemaAny = (XmlSchemaAny)particle;
					this.processContents = xmlSchemaAny.ProcessContentsCorrect;
				}
			}
			return schemaElementDecl;
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x0011A75C File Offset: 0x0011895C
		private SchemaElementDecl ThoroughGetElementDecl(SchemaElementDecl elementDecl, XmlQualifiedName xsiType, string xsiNil)
		{
			if (elementDecl == null)
			{
				elementDecl = this.schemaInfo.GetElementDecl(this.elementName);
			}
			if (elementDecl != null)
			{
				if (xsiType.IsEmpty)
				{
					if (elementDecl.IsAbstract)
					{
						base.SendValidationEvent("The element '{0}' is abstract or its type is abstract.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
						elementDecl = null;
					}
				}
				else if (xsiNil != null && xsiNil.Equals("true"))
				{
					base.SendValidationEvent("There can be no type value when attribute is 'xsi:nil' and has value 'true'.");
				}
				else
				{
					SchemaElementDecl elementDecl2;
					if (!this.schemaInfo.ElementDeclsByType.TryGetValue(xsiType, out elementDecl2) && xsiType.Namespace == this.NsXs)
					{
						XmlSchemaSimpleType simpleTypeFromXsdType = DatatypeImplementation.GetSimpleTypeFromXsdType(new XmlQualifiedName(xsiType.Name, this.NsXs));
						if (simpleTypeFromXsdType != null)
						{
							elementDecl2 = simpleTypeFromXsdType.ElementDecl;
						}
					}
					if (elementDecl2 == null)
					{
						base.SendValidationEvent("This is an invalid xsi:type '{0}'.", xsiType.ToString());
						elementDecl = null;
					}
					else if (!XmlSchemaType.IsDerivedFrom(elementDecl2.SchemaType, elementDecl.SchemaType, elementDecl.Block))
					{
						base.SendValidationEvent("The xsi:type attribute value '{0}' is not valid for the element '{1}', either because it is not a type validly derived from the type in the schema, or because it has xsi:type derivation blocked.", new string[]
						{
							xsiType.ToString(),
							XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace)
						});
						elementDecl = null;
					}
					else
					{
						elementDecl = elementDecl2;
					}
				}
				if (elementDecl != null && elementDecl.IsNillable)
				{
					if (xsiNil != null)
					{
						this.context.IsNill = XmlConvert.ToBoolean(xsiNil);
						if (this.context.IsNill && elementDecl.DefaultValueTyped != null)
						{
							base.SendValidationEvent("There must be no fixed value when an attribute is 'xsi:nil' and has a value of 'true'.");
						}
					}
				}
				else if (xsiNil != null)
				{
					base.SendValidationEvent("If the 'nillable' attribute is false in the schema, the 'xsi:nil' attribute must not be present in the instance.");
				}
			}
			return elementDecl;
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x0011A8F4 File Offset: 0x00118AF4
		private void ValidateStartElement()
		{
			if (this.context.ElementDecl != null)
			{
				if (this.context.ElementDecl.IsAbstract)
				{
					base.SendValidationEvent("The element '{0}' is abstract or its type is abstract.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
				}
				this.reader.SchemaTypeObject = this.context.ElementDecl.SchemaType;
				if (this.reader.IsEmptyElement && !this.context.IsNill && this.context.ElementDecl.DefaultValueTyped != null)
				{
					this.reader.TypedValueObject = this.UnWrapUnion(this.context.ElementDecl.DefaultValueTyped);
					this.context.IsNill = true;
				}
				else
				{
					this.reader.TypedValueObject = null;
				}
				if (this.context.ElementDecl.HasRequiredAttribute || this.HasIdentityConstraints)
				{
					this.attPresence.Clear();
				}
			}
			if (this.reader.MoveToFirstAttribute())
			{
				do
				{
					if (this.reader.NamespaceURI != this.NsXmlNs && this.reader.NamespaceURI != this.NsXsi)
					{
						try
						{
							this.reader.SchemaTypeObject = null;
							XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(this.reader.LocalName, this.reader.NamespaceURI);
							bool flag = this.processContents == XmlSchemaContentProcessing.Skip;
							SchemaAttDef attributeXsd = this.schemaInfo.GetAttributeXsd(this.context.ElementDecl, xmlQualifiedName, ref flag);
							if (attributeXsd != null)
							{
								if (this.context.ElementDecl != null && (this.context.ElementDecl.HasRequiredAttribute || this.startIDConstraint != -1))
								{
									this.attPresence.Add(attributeXsd.Name, attributeXsd);
								}
								this.reader.SchemaTypeObject = attributeXsd.SchemaType;
								if (attributeXsd.Datatype != null)
								{
									this.CheckValue(this.reader.Value, attributeXsd);
								}
								if (this.HasIdentityConstraints)
								{
									this.AttributeIdentityConstraints(this.reader.LocalName, this.reader.NamespaceURI, this.reader.TypedValueObject, this.reader.Value, attributeXsd);
								}
							}
							else if (!flag)
							{
								if (this.context.ElementDecl == null && this.processContents == XmlSchemaContentProcessing.Strict && xmlQualifiedName.Namespace.Length != 0 && this.schemaInfo.Contains(xmlQualifiedName.Namespace))
								{
									base.SendValidationEvent("The '{0}' attribute is not declared.", xmlQualifiedName.ToString());
								}
								else
								{
									base.SendValidationEvent("Could not find schema information for the attribute '{0}'.", xmlQualifiedName.ToString(), XmlSeverityType.Warning);
								}
							}
						}
						catch (XmlSchemaException ex)
						{
							ex.SetSource(this.reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
							base.SendValidationEvent(ex);
						}
					}
				}
				while (this.reader.MoveToNextAttribute());
				this.reader.MoveToElement();
			}
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x0011ABF0 File Offset: 0x00118DF0
		private void ValidateEndStartElement()
		{
			if (this.context.ElementDecl.HasDefaultAttribute)
			{
				for (int i = 0; i < this.context.ElementDecl.DefaultAttDefs.Count; i++)
				{
					SchemaAttDef schemaAttDef = (SchemaAttDef)this.context.ElementDecl.DefaultAttDefs[i];
					this.reader.AddDefaultAttribute(schemaAttDef);
					if (this.HasIdentityConstraints && !this.attPresence.Contains(schemaAttDef.Name))
					{
						this.AttributeIdentityConstraints(schemaAttDef.Name.Name, schemaAttDef.Name.Namespace, this.UnWrapUnion(schemaAttDef.DefaultValueTyped), schemaAttDef.DefaultValueRaw, schemaAttDef);
					}
				}
			}
			if (this.context.ElementDecl.HasRequiredAttribute)
			{
				try
				{
					this.context.ElementDecl.CheckAttributes(this.attPresence, this.reader.StandAlone);
				}
				catch (XmlSchemaException ex)
				{
					ex.SetSource(this.reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
					base.SendValidationEvent(ex);
				}
			}
			if (this.context.ElementDecl.Datatype != null)
			{
				this.checkDatatype = true;
				this.hasSibling = false;
				this.textString = string.Empty;
				this.textValue.Length = 0;
			}
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x0011AD58 File Offset: 0x00118F58
		private void LoadSchemaFromLocation(string uri, string url)
		{
			XmlReader xmlReader = null;
			try
			{
				Uri uri2 = base.XmlResolver.ResolveUri(base.BaseUri, url);
				Stream stream = (Stream)base.XmlResolver.GetEntity(uri2, null, null);
				xmlReader = new XmlTextReader(uri2.ToString(), stream, base.NameTable);
				Parser parser = new Parser(SchemaType.XSD, base.NameTable, base.SchemaNames, base.EventHandler);
				parser.XmlResolver = base.XmlResolver;
				SchemaType schemaType = parser.Parse(xmlReader, uri);
				SchemaInfo schemaInfo = new SchemaInfo();
				schemaInfo.SchemaType = schemaType;
				if (schemaType == SchemaType.XSD)
				{
					if (base.SchemaCollection.EventHandler == null)
					{
						base.SchemaCollection.EventHandler = base.EventHandler;
					}
					base.SchemaCollection.Add(uri, schemaInfo, parser.XmlSchema, true);
				}
				base.SchemaInfo.Add(schemaInfo, base.EventHandler);
				while (xmlReader.Read())
				{
				}
			}
			catch (XmlSchemaException ex)
			{
				base.SendValidationEvent("Cannot load the schema for the namespace '{0}' - {1}", new string[] { uri, ex.Message }, XmlSeverityType.Error);
			}
			catch (Exception ex2)
			{
				base.SendValidationEvent("Cannot load the schema for the namespace '{0}' - {1}", new string[] { uri, ex2.Message }, XmlSeverityType.Warning);
			}
			finally
			{
				if (xmlReader != null)
				{
					xmlReader.Close();
				}
			}
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x0011AEDC File Offset: 0x001190DC
		private void LoadSchema(string uri, string url)
		{
			if (base.XmlResolver == null)
			{
				return;
			}
			if (base.SchemaInfo.TargetNamespaces.ContainsKey(uri) && this.nsManager.LookupPrefix(uri) != null)
			{
				return;
			}
			SchemaInfo schemaInfo = null;
			if (base.SchemaCollection != null)
			{
				schemaInfo = base.SchemaCollection.GetSchemaInfo(uri);
			}
			if (schemaInfo == null)
			{
				if (url != null)
				{
					this.LoadSchemaFromLocation(uri, url);
				}
				return;
			}
			if (schemaInfo.SchemaType != SchemaType.XSD)
			{
				throw new XmlException("Unsupported combination of validation types.", string.Empty, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
			}
			base.SchemaInfo.Add(schemaInfo, base.EventHandler);
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x060030D1 RID: 12497 RVA: 0x00103225 File Offset: 0x00101425
		private bool HasSchema
		{
			get
			{
				return this.schemaInfo.SchemaType > SchemaType.None;
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x060030D2 RID: 12498 RVA: 0x000E5EA9 File Offset: 0x000E40A9
		public override bool PreserveWhitespace
		{
			get
			{
				return this.context.ElementDecl != null && this.context.ElementDecl.ContentValidator.PreserveWhitespace;
			}
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x0011AF7C File Offset: 0x0011917C
		private void ProcessTokenizedType(XmlTokenizedType ttype, string name)
		{
			switch (ttype)
			{
			case XmlTokenizedType.ID:
				if (this.FindId(name) != null)
				{
					base.SendValidationEvent("'{0}' is already used as an ID.", name);
					return;
				}
				this.AddID(name, this.context.LocalName);
				return;
			case XmlTokenizedType.IDREF:
				if (this.FindId(name) == null)
				{
					this.idRefListHead = new IdRefNode(this.idRefListHead, name, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
					return;
				}
				break;
			case XmlTokenizedType.IDREFS:
				break;
			case XmlTokenizedType.ENTITY:
				BaseValidator.ProcessEntity(this.schemaInfo, name, this, base.EventHandler, this.reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
				break;
			default:
				return;
			}
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x0011B034 File Offset: 0x00119234
		private void CheckValue(string value, SchemaAttDef attdef)
		{
			try
			{
				this.reader.TypedValueObject = null;
				bool flag = attdef != null;
				XmlSchemaDatatype xmlSchemaDatatype = (flag ? attdef.Datatype : this.context.ElementDecl.Datatype);
				if (xmlSchemaDatatype != null)
				{
					object obj = xmlSchemaDatatype.ParseValue(value, base.NameTable, this.nsManager, true);
					XmlTokenizedType tokenizedType = xmlSchemaDatatype.TokenizedType;
					if (tokenizedType == XmlTokenizedType.ENTITY || tokenizedType == XmlTokenizedType.ID || tokenizedType == XmlTokenizedType.IDREF)
					{
						if (xmlSchemaDatatype.Variety == XmlSchemaDatatypeVariety.List)
						{
							string[] array = (string[])obj;
							for (int i = 0; i < array.Length; i++)
							{
								this.ProcessTokenizedType(xmlSchemaDatatype.TokenizedType, array[i]);
							}
						}
						else
						{
							this.ProcessTokenizedType(xmlSchemaDatatype.TokenizedType, (string)obj);
						}
					}
					if (!(flag ? attdef : this.context.ElementDecl).CheckValue(obj))
					{
						if (flag)
						{
							base.SendValidationEvent("The value of the '{0}' attribute does not equal its fixed value.", attdef.Name.ToString());
						}
						else
						{
							base.SendValidationEvent("The value of the '{0}' element does not equal its fixed value.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
						}
					}
					if (xmlSchemaDatatype.Variety == XmlSchemaDatatypeVariety.Union)
					{
						obj = this.UnWrapUnion(obj);
					}
					this.reader.TypedValueObject = obj;
				}
			}
			catch (XmlSchemaException)
			{
				if (attdef != null)
				{
					base.SendValidationEvent("The '{0}' attribute has an invalid value according to its data type.", attdef.Name.ToString());
				}
				else
				{
					base.SendValidationEvent("The '{0}' element has an invalid value according to its data type.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
				}
			}
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x0011B1C0 File Offset: 0x001193C0
		internal void AddID(string name, object node)
		{
			if (this.IDs == null)
			{
				this.IDs = new Hashtable();
			}
			this.IDs.Add(name, node);
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x0011B1E2 File Offset: 0x001193E2
		public override object FindId(string name)
		{
			if (this.IDs != null)
			{
				return this.IDs[name];
			}
			return null;
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x0011B1FA File Offset: 0x001193FA
		public bool IsXSDRoot(string localName, string ns)
		{
			return Ref.Equal(ns, this.NsXs) && Ref.Equal(localName, this.XsdSchema);
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x0011B218 File Offset: 0x00119418
		private void Push(XmlQualifiedName elementName)
		{
			this.context = (ValidationState)this.validationStack.Push();
			if (this.context == null)
			{
				this.context = new ValidationState();
				this.validationStack.AddToTop(this.context);
			}
			this.context.LocalName = elementName.Name;
			this.context.Namespace = elementName.Namespace;
			this.context.HasMatched = false;
			this.context.IsNill = false;
			this.context.ProcessContents = this.processContents;
			this.context.NeedValidateChildren = false;
			this.context.Constr = null;
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x0011B2C4 File Offset: 0x001194C4
		private void Pop()
		{
			if (this.validationStack.Length > 1)
			{
				this.validationStack.Pop();
				if (this.startIDConstraint == this.validationStack.Length)
				{
					this.startIDConstraint = -1;
				}
				this.context = (ValidationState)this.validationStack.Peek();
				this.processContents = this.context.ProcessContents;
			}
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x0011B32C File Offset: 0x0011952C
		private void CheckForwardRefs()
		{
			IdRefNode next;
			for (IdRefNode idRefNode = this.idRefListHead; idRefNode != null; idRefNode = next)
			{
				if (this.FindId(idRefNode.Id) == null)
				{
					base.SendValidationEvent(new XmlSchemaException("Reference to undeclared ID is '{0}'.", idRefNode.Id, this.reader.BaseURI, idRefNode.LineNo, idRefNode.LinePos));
				}
				next = idRefNode.Next;
				idRefNode.Next = null;
			}
			this.idRefListHead = null;
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x0011B395 File Offset: 0x00119595
		private void ValidateStartElementIdentityConstraints()
		{
			if (this.context.ElementDecl != null)
			{
				if (this.context.ElementDecl.Constraints != null)
				{
					this.AddIdentityConstraints();
				}
				if (this.HasIdentityConstraints)
				{
					this.ElementIdentityConstraints();
				}
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x060030DC RID: 12508 RVA: 0x0011B3CA File Offset: 0x001195CA
		private bool HasIdentityConstraints
		{
			get
			{
				return this.startIDConstraint != -1;
			}
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x0011B3D8 File Offset: 0x001195D8
		private void AddIdentityConstraints()
		{
			this.context.Constr = new ConstraintStruct[this.context.ElementDecl.Constraints.Length];
			int num = 0;
			for (int i = 0; i < this.context.ElementDecl.Constraints.Length; i++)
			{
				this.context.Constr[num++] = new ConstraintStruct(this.context.ElementDecl.Constraints[i]);
			}
			for (int j = 0; j < this.context.Constr.Length; j++)
			{
				if (this.context.Constr[j].constraint.Role == CompiledIdentityConstraint.ConstraintRole.Keyref)
				{
					bool flag = false;
					for (int k = this.validationStack.Length - 1; k >= ((this.startIDConstraint >= 0) ? this.startIDConstraint : (this.validationStack.Length - 1)); k--)
					{
						if (((ValidationState)this.validationStack[k]).Constr != null)
						{
							ConstraintStruct[] constr = ((ValidationState)this.validationStack[k]).Constr;
							for (int l = 0; l < constr.Length; l++)
							{
								if (constr[l].constraint.name == this.context.Constr[j].constraint.refer)
								{
									flag = true;
									if (constr[l].keyrefTable == null)
									{
										constr[l].keyrefTable = new Hashtable();
									}
									this.context.Constr[j].qualifiedTable = constr[l].keyrefTable;
									break;
								}
							}
							if (flag)
							{
								break;
							}
						}
					}
					if (!flag)
					{
						base.SendValidationEvent("The Keyref '{0}' cannot find the referred key or unique in scope.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
					}
				}
			}
			if (this.startIDConstraint == -1)
			{
				this.startIDConstraint = this.validationStack.Length - 1;
			}
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x0011B5C4 File Offset: 0x001197C4
		private void ElementIdentityConstraints()
		{
			for (int i = this.startIDConstraint; i < this.validationStack.Length; i++)
			{
				if (((ValidationState)this.validationStack[i]).Constr != null)
				{
					ConstraintStruct[] constr = ((ValidationState)this.validationStack[i]).Constr;
					for (int j = 0; j < constr.Length; j++)
					{
						if (constr[j].axisSelector.MoveToStartElement(this.reader.LocalName, this.reader.NamespaceURI))
						{
							constr[j].axisSelector.PushKS(base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
						}
						for (int k = 0; k < constr[j].axisFields.Count; k++)
						{
							LocatedActiveAxis locatedActiveAxis = (LocatedActiveAxis)constr[j].axisFields[k];
							if (locatedActiveAxis.MoveToStartElement(this.reader.LocalName, this.reader.NamespaceURI) && this.context.ElementDecl != null)
							{
								if (this.context.ElementDecl.Datatype == null)
								{
									base.SendValidationEvent("The field '{0}' is expecting an element or attribute with simple type or simple content.", this.reader.LocalName);
								}
								else
								{
									locatedActiveAxis.isMatched = true;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x0011B710 File Offset: 0x00119910
		private void AttributeIdentityConstraints(string name, string ns, object obj, string sobj, SchemaAttDef attdef)
		{
			for (int i = this.startIDConstraint; i < this.validationStack.Length; i++)
			{
				if (((ValidationState)this.validationStack[i]).Constr != null)
				{
					ConstraintStruct[] constr = ((ValidationState)this.validationStack[i]).Constr;
					for (int j = 0; j < constr.Length; j++)
					{
						for (int k = 0; k < constr[j].axisFields.Count; k++)
						{
							LocatedActiveAxis locatedActiveAxis = (LocatedActiveAxis)constr[j].axisFields[k];
							if (locatedActiveAxis.MoveToAttribute(name, ns))
							{
								if (locatedActiveAxis.Ks[locatedActiveAxis.Column] != null)
								{
									base.SendValidationEvent("The field '{0}' is expecting at the most one value.", name);
								}
								else if (attdef != null && attdef.Datatype != null)
								{
									locatedActiveAxis.Ks[locatedActiveAxis.Column] = new TypedObject(obj, sobj, attdef.Datatype);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x0011B814 File Offset: 0x00119A14
		private object UnWrapUnion(object typedValue)
		{
			XsdSimpleValue xsdSimpleValue = typedValue as XsdSimpleValue;
			if (xsdSimpleValue != null)
			{
				typedValue = xsdSimpleValue.TypedValue;
			}
			return typedValue;
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x0011B834 File Offset: 0x00119A34
		private void EndElementIdentityConstraints()
		{
			for (int i = this.validationStack.Length - 1; i >= this.startIDConstraint; i--)
			{
				if (((ValidationState)this.validationStack[i]).Constr != null)
				{
					ConstraintStruct[] constr = ((ValidationState)this.validationStack[i]).Constr;
					for (int j = 0; j < constr.Length; j++)
					{
						for (int k = 0; k < constr[j].axisFields.Count; k++)
						{
							LocatedActiveAxis locatedActiveAxis = (LocatedActiveAxis)constr[j].axisFields[k];
							if (locatedActiveAxis.isMatched)
							{
								locatedActiveAxis.isMatched = false;
								if (locatedActiveAxis.Ks[locatedActiveAxis.Column] != null)
								{
									base.SendValidationEvent("The field '{0}' is expecting at the most one value.", this.reader.LocalName);
								}
								else
								{
									string text = ((!this.hasSibling) ? this.textString : this.textValue.ToString());
									if (this.reader.TypedValueObject != null && text.Length != 0)
									{
										locatedActiveAxis.Ks[locatedActiveAxis.Column] = new TypedObject(this.reader.TypedValueObject, text, this.context.ElementDecl.Datatype);
									}
								}
							}
							locatedActiveAxis.EndElement(this.reader.LocalName, this.reader.NamespaceURI);
						}
						if (constr[j].axisSelector.EndElement(this.reader.LocalName, this.reader.NamespaceURI))
						{
							KeySequence keySequence = constr[j].axisSelector.PopKS();
							switch (constr[j].constraint.Role)
							{
							case CompiledIdentityConstraint.ConstraintRole.Unique:
								if (keySequence.IsQualified())
								{
									if (constr[j].qualifiedTable.Contains(keySequence))
									{
										base.SendValidationEvent(new XmlSchemaException("There is a duplicate key sequence '{0}' for the '{1}' key or unique identity constraint.", new string[]
										{
											keySequence.ToString(),
											constr[j].constraint.name.ToString()
										}, this.reader.BaseURI, keySequence.PosLine, keySequence.PosCol));
									}
									else
									{
										constr[j].qualifiedTable.Add(keySequence, keySequence);
									}
								}
								break;
							case CompiledIdentityConstraint.ConstraintRole.Key:
								if (!keySequence.IsQualified())
								{
									base.SendValidationEvent(new XmlSchemaException("The identity constraint '{0}' validation has failed. Either a key is missing or the existing key has an empty node.", constr[j].constraint.name.ToString(), this.reader.BaseURI, keySequence.PosLine, keySequence.PosCol));
								}
								else if (constr[j].qualifiedTable.Contains(keySequence))
								{
									base.SendValidationEvent(new XmlSchemaException("There is a duplicate key sequence '{0}' for the '{1}' key or unique identity constraint.", new string[]
									{
										keySequence.ToString(),
										constr[j].constraint.name.ToString()
									}, this.reader.BaseURI, keySequence.PosLine, keySequence.PosCol));
								}
								else
								{
									constr[j].qualifiedTable.Add(keySequence, keySequence);
								}
								break;
							case CompiledIdentityConstraint.ConstraintRole.Keyref:
								if (constr[j].qualifiedTable != null && keySequence.IsQualified() && !constr[j].qualifiedTable.Contains(keySequence))
								{
									constr[j].qualifiedTable.Add(keySequence, keySequence);
								}
								break;
							}
						}
					}
				}
			}
			ConstraintStruct[] constr2 = ((ValidationState)this.validationStack[this.validationStack.Length - 1]).Constr;
			if (constr2 != null)
			{
				for (int l = 0; l < constr2.Length; l++)
				{
					if (constr2[l].constraint.Role != CompiledIdentityConstraint.ConstraintRole.Keyref && constr2[l].keyrefTable != null)
					{
						foreach (object obj in constr2[l].keyrefTable.Keys)
						{
							KeySequence keySequence2 = (KeySequence)obj;
							if (!constr2[l].qualifiedTable.Contains(keySequence2))
							{
								base.SendValidationEvent(new XmlSchemaException("The key sequence '{0}' in '{1}' Keyref fails to refer to some key.", new string[]
								{
									keySequence2.ToString(),
									constr2[l].constraint.name.ToString()
								}, this.reader.BaseURI, keySequence2.PosLine, keySequence2.PosCol));
							}
						}
					}
				}
			}
		}

		// Token: 0x04001FFE RID: 8190
		private int startIDConstraint = -1;

		// Token: 0x04001FFF RID: 8191
		private const int STACK_INCREMENT = 10;

		// Token: 0x04002000 RID: 8192
		private HWStack validationStack;

		// Token: 0x04002001 RID: 8193
		private Hashtable attPresence;

		// Token: 0x04002002 RID: 8194
		private XmlNamespaceManager nsManager;

		// Token: 0x04002003 RID: 8195
		private bool bManageNamespaces;

		// Token: 0x04002004 RID: 8196
		private Hashtable IDs;

		// Token: 0x04002005 RID: 8197
		private IdRefNode idRefListHead;

		// Token: 0x04002006 RID: 8198
		private Parser inlineSchemaParser;

		// Token: 0x04002007 RID: 8199
		private XmlSchemaContentProcessing processContents;

		// Token: 0x04002008 RID: 8200
		private static readonly XmlSchemaDatatype dtCDATA = XmlSchemaDatatype.FromXmlTokenizedType(XmlTokenizedType.CDATA);

		// Token: 0x04002009 RID: 8201
		private static readonly XmlSchemaDatatype dtQName = XmlSchemaDatatype.FromXmlTokenizedTypeXsd(XmlTokenizedType.QName);

		// Token: 0x0400200A RID: 8202
		private static readonly XmlSchemaDatatype dtStringArray = XsdValidator.dtCDATA.DeriveByList(null);

		// Token: 0x0400200B RID: 8203
		private string NsXmlNs;

		// Token: 0x0400200C RID: 8204
		private string NsXs;

		// Token: 0x0400200D RID: 8205
		private string NsXsi;

		// Token: 0x0400200E RID: 8206
		private string XsiType;

		// Token: 0x0400200F RID: 8207
		private string XsiNil;

		// Token: 0x04002010 RID: 8208
		private string XsiSchemaLocation;

		// Token: 0x04002011 RID: 8209
		private string XsiNoNamespaceSchemaLocation;

		// Token: 0x04002012 RID: 8210
		private string XsdSchema;
	}
}
