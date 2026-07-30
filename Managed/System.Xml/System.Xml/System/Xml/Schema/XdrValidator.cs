using System;
using System.Collections;
using System.IO;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x02000430 RID: 1072
	internal sealed class XdrValidator : BaseValidator
	{
		// Token: 0x06002A3E RID: 10814 RVA: 0x001026F3 File Offset: 0x001008F3
		internal XdrValidator(BaseValidator validator)
			: base(validator)
		{
			this.Init();
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x0010270D File Offset: 0x0010090D
		internal XdrValidator(XmlValidatingReaderImpl reader, XmlSchemaCollection schemaCollection, IValidationEventHandling eventHandling)
			: base(reader, schemaCollection, eventHandling)
		{
			this.Init();
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x0010272C File Offset: 0x0010092C
		private void Init()
		{
			this.nsManager = this.reader.NamespaceManager;
			if (this.nsManager == null)
			{
				this.nsManager = new XmlNamespaceManager(base.NameTable);
				this.isProcessContents = true;
			}
			this.validationStack = new HWStack(10);
			this.textValue = new StringBuilder();
			this.name = XmlQualifiedName.Empty;
			this.attPresence = new Hashtable();
			this.Push(XmlQualifiedName.Empty);
			this.schemaInfo = new SchemaInfo();
			this.checkDatatype = false;
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x001027B8 File Offset: 0x001009B8
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

		// Token: 0x06002A42 RID: 10818 RVA: 0x0010282C File Offset: 0x00100A2C
		private void ValidateElement()
		{
			this.elementName.Init(this.reader.LocalName, XmlSchemaDatatype.XdrCanonizeUri(this.reader.NamespaceURI, base.NameTable, base.SchemaNames));
			this.ValidateChildElement();
			if (base.SchemaNames.IsXDRRoot(this.elementName.Name, this.elementName.Namespace) && this.reader.Depth > 0)
			{
				this.inlineSchemaParser = new Parser(SchemaType.XDR, base.NameTable, base.SchemaNames, base.EventHandler);
				this.inlineSchemaParser.StartParsing(this.reader, null);
				this.inlineSchemaParser.ParseReaderNode();
				return;
			}
			this.ProcessElement();
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x001028E8 File Offset: 0x00100AE8
		private void ValidateChildElement()
		{
			if (this.context.NeedValidateChildren)
			{
				int num = 0;
				this.context.ElementDecl.ContentValidator.ValidateElement(this.elementName, this.context, out num);
				if (num < 0)
				{
					XmlSchemaValidator.ElementValidationError(this.elementName, this.context, base.EventHandler, this.reader, this.reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition, null);
				}
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06002A44 RID: 10820 RVA: 0x0010296B File Offset: 0x00100B6B
		private bool IsInlineSchemaStarted
		{
			get
			{
				return this.inlineSchemaParser != null;
			}
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x00102978 File Offset: 0x00100B78
		private void ProcessInlineSchema()
		{
			if (!this.inlineSchemaParser.ParseReaderNode())
			{
				this.inlineSchemaParser.FinishParsing();
				SchemaInfo xdrSchema = this.inlineSchemaParser.XdrSchema;
				if (xdrSchema != null && xdrSchema.ErrorCount == 0)
				{
					foreach (string text in xdrSchema.TargetNamespaces.Keys)
					{
						if (!this.schemaInfo.HasSchema(text))
						{
							this.schemaInfo.Add(xdrSchema, base.EventHandler);
							base.SchemaCollection.Add(text, xdrSchema, null, false);
							break;
						}
					}
				}
				this.inlineSchemaParser = null;
			}
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x00102A38 File Offset: 0x00100C38
		private void ProcessElement()
		{
			this.Push(this.elementName);
			if (this.isProcessContents)
			{
				this.nsManager.PopScope();
			}
			this.context.ElementDecl = this.ThoroughGetElementDecl();
			if (this.context.ElementDecl != null)
			{
				this.ValidateStartElement();
				this.ValidateEndStartElement();
				this.context.NeedValidateChildren = true;
				this.context.ElementDecl.ContentValidator.InitValidation(this.context);
			}
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x00102AB8 File Offset: 0x00100CB8
		private void ValidateEndElement()
		{
			if (this.isProcessContents)
			{
				this.nsManager.PopScope();
			}
			if (this.context.ElementDecl != null)
			{
				if (this.context.NeedValidateChildren && !this.context.ElementDecl.ContentValidator.CompleteValidation(this.context))
				{
					XmlSchemaValidator.CompleteValidationError(this.context, base.EventHandler, this.reader, this.reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition, null);
				}
				if (this.checkDatatype)
				{
					string text = ((!this.hasSibling) ? this.textString : this.textValue.ToString());
					this.CheckValue(text, null);
					this.checkDatatype = false;
					this.textValue.Length = 0;
					this.textString = string.Empty;
				}
			}
			this.Pop();
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x00102B9C File Offset: 0x00100D9C
		private SchemaElementDecl ThoroughGetElementDecl()
		{
			if (this.reader.Depth == 0)
			{
				this.LoadSchema(string.Empty);
			}
			if (this.reader.MoveToFirstAttribute())
			{
				do
				{
					string namespaceURI = this.reader.NamespaceURI;
					string localName = this.reader.LocalName;
					if (Ref.Equal(namespaceURI, base.SchemaNames.NsXmlNs))
					{
						this.LoadSchema(this.reader.Value);
						if (this.isProcessContents)
						{
							this.nsManager.AddNamespace((this.reader.Prefix.Length == 0) ? string.Empty : this.reader.LocalName, this.reader.Value);
						}
					}
					if (Ref.Equal(namespaceURI, base.SchemaNames.QnDtDt.Namespace) && Ref.Equal(localName, base.SchemaNames.QnDtDt.Name))
					{
						this.reader.SchemaTypeObject = XmlSchemaDatatype.FromXdrName(this.reader.Value);
					}
				}
				while (this.reader.MoveToNextAttribute());
				this.reader.MoveToElement();
			}
			SchemaElementDecl elementDecl = this.schemaInfo.GetElementDecl(this.elementName);
			if (elementDecl == null && this.schemaInfo.TargetNamespaces.ContainsKey(this.context.Namespace))
			{
				base.SendValidationEvent("The '{0}' element is not declared.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
			}
			return elementDecl;
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x00102D0C File Offset: 0x00100F0C
		private void ValidateStartElement()
		{
			if (this.context.ElementDecl != null)
			{
				if (this.context.ElementDecl.SchemaType != null)
				{
					this.reader.SchemaTypeObject = this.context.ElementDecl.SchemaType;
				}
				else
				{
					this.reader.SchemaTypeObject = this.context.ElementDecl.Datatype;
				}
				if (this.reader.IsEmptyElement && !this.context.IsNill && this.context.ElementDecl.DefaultValueTyped != null)
				{
					this.reader.TypedValueObject = this.context.ElementDecl.DefaultValueTyped;
					this.context.IsNill = true;
				}
				if (this.context.ElementDecl.HasRequiredAttribute)
				{
					this.attPresence.Clear();
				}
			}
			if (this.reader.MoveToFirstAttribute())
			{
				do
				{
					if (this.reader.NamespaceURI != base.SchemaNames.NsXmlNs)
					{
						try
						{
							this.reader.SchemaTypeObject = null;
							SchemaAttDef attributeXdr = this.schemaInfo.GetAttributeXdr(this.context.ElementDecl, this.QualifiedName(this.reader.LocalName, this.reader.NamespaceURI));
							if (attributeXdr != null)
							{
								if (this.context.ElementDecl != null && this.context.ElementDecl.HasRequiredAttribute)
								{
									this.attPresence.Add(attributeXdr.Name, attributeXdr);
								}
								this.reader.SchemaTypeObject = ((attributeXdr.SchemaType != null) ? attributeXdr.SchemaType : attributeXdr.Datatype);
								if (attributeXdr.Datatype != null)
								{
									string value = this.reader.Value;
									this.CheckValue(value, attributeXdr);
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

		// Token: 0x06002A4A RID: 10826 RVA: 0x00102F20 File Offset: 0x00101120
		private void ValidateEndStartElement()
		{
			if (this.context.ElementDecl.HasDefaultAttribute)
			{
				for (int i = 0; i < this.context.ElementDecl.DefaultAttDefs.Count; i++)
				{
					this.reader.AddDefaultAttribute((SchemaAttDef)this.context.ElementDecl.DefaultAttDefs[i]);
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

		// Token: 0x06002A4B RID: 10827 RVA: 0x00103034 File Offset: 0x00101234
		private void LoadSchemaFromLocation(string uri)
		{
			if (!XdrBuilder.IsXdrSchema(uri))
			{
				return;
			}
			string text = uri.Substring("x-schema:".Length);
			XmlReader xmlReader = null;
			SchemaInfo schemaInfo = null;
			try
			{
				Uri uri2 = base.XmlResolver.ResolveUri(base.BaseUri, text);
				Stream stream = (Stream)base.XmlResolver.GetEntity(uri2, null, null);
				xmlReader = new XmlTextReader(uri2.ToString(), stream, base.NameTable);
				((XmlTextReader)xmlReader).XmlResolver = base.XmlResolver;
				Parser parser = new Parser(SchemaType.XDR, base.NameTable, base.SchemaNames, base.EventHandler);
				parser.XmlResolver = base.XmlResolver;
				parser.Parse(xmlReader, uri);
				while (xmlReader.Read())
				{
				}
				schemaInfo = parser.XdrSchema;
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
			if (schemaInfo != null && schemaInfo.ErrorCount == 0)
			{
				this.schemaInfo.Add(schemaInfo, base.EventHandler);
				base.SchemaCollection.Add(uri, schemaInfo, null, false);
			}
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x00103198 File Offset: 0x00101398
		private void LoadSchema(string uri)
		{
			if (this.schemaInfo.TargetNamespaces.ContainsKey(uri))
			{
				return;
			}
			if (base.XmlResolver == null)
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
				this.LoadSchemaFromLocation(uri);
				return;
			}
			if (schemaInfo.SchemaType != SchemaType.XDR)
			{
				throw new XmlException("Unsupported combination of validation types.", string.Empty, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
			}
			this.schemaInfo.Add(schemaInfo, base.EventHandler);
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06002A4D RID: 10829 RVA: 0x00103225 File Offset: 0x00101425
		private bool HasSchema
		{
			get
			{
				return this.schemaInfo.SchemaType > SchemaType.None;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06002A4E RID: 10830 RVA: 0x000E5EA9 File Offset: 0x000E40A9
		public override bool PreserveWhitespace
		{
			get
			{
				return this.context.ElementDecl != null && this.context.ElementDecl.ContentValidator.PreserveWhitespace;
			}
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x00103238 File Offset: 0x00101438
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

		// Token: 0x06002A50 RID: 10832 RVA: 0x001032ED File Offset: 0x001014ED
		public override void CompleteValidation()
		{
			if (this.HasSchema)
			{
				this.CheckForwardRefs();
				return;
			}
			base.SendValidationEvent(new XmlSchemaException("No validation occurred.", string.Empty), XmlSeverityType.Warning);
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x00103314 File Offset: 0x00101514
		private void CheckValue(string value, SchemaAttDef attdef)
		{
			try
			{
				this.reader.TypedValueObject = null;
				bool flag = attdef != null;
				XmlSchemaDatatype xmlSchemaDatatype = (flag ? attdef.Datatype : this.context.ElementDecl.Datatype);
				if (xmlSchemaDatatype != null)
				{
					if (xmlSchemaDatatype.TokenizedType != XmlTokenizedType.CDATA)
					{
						value = value.Trim();
					}
					if (value.Length != 0)
					{
						object obj = xmlSchemaDatatype.ParseValue(value, base.NameTable, this.nsManager);
						this.reader.TypedValueObject = obj;
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
						SchemaDeclBase schemaDeclBase = (flag ? attdef : this.context.ElementDecl);
						if (schemaDeclBase.MaxLength != (long)((ulong)(-1)) && (long)value.Length > schemaDeclBase.MaxLength)
						{
							base.SendValidationEvent("The actual length is greater than the MaxLength value.", value);
						}
						if (schemaDeclBase.MinLength != (long)((ulong)(-1)) && (long)value.Length < schemaDeclBase.MinLength)
						{
							base.SendValidationEvent("The actual length is less than the MinLength value.", value);
						}
						if (schemaDeclBase.Values != null && !schemaDeclBase.CheckEnumeration(obj))
						{
							if (xmlSchemaDatatype.TokenizedType == XmlTokenizedType.NOTATION)
							{
								base.SendValidationEvent("'{0}' is not in the notation list.", obj.ToString());
							}
							else
							{
								base.SendValidationEvent("'{0}' is not in the enumeration list.", obj.ToString());
							}
						}
						if (!schemaDeclBase.CheckValue(obj))
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
					}
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

		// Token: 0x06002A52 RID: 10834 RVA: 0x00103540 File Offset: 0x00101740
		public static void CheckDefaultValue(string value, SchemaAttDef attdef, SchemaInfo sinfo, XmlNamespaceManager nsManager, XmlNameTable NameTable, object sender, ValidationEventHandler eventhandler, string baseUri, int lineNo, int linePos)
		{
			try
			{
				XmlSchemaDatatype datatype = attdef.Datatype;
				if (datatype != null)
				{
					if (datatype.TokenizedType != XmlTokenizedType.CDATA)
					{
						value = value.Trim();
					}
					if (value.Length != 0)
					{
						object obj = datatype.ParseValue(value, NameTable, nsManager);
						XmlTokenizedType tokenizedType = datatype.TokenizedType;
						if (tokenizedType == XmlTokenizedType.ENTITY)
						{
							if (datatype.Variety == XmlSchemaDatatypeVariety.List)
							{
								string[] array = (string[])obj;
								for (int i = 0; i < array.Length; i++)
								{
									BaseValidator.ProcessEntity(sinfo, array[i], sender, eventhandler, baseUri, lineNo, linePos);
								}
							}
							else
							{
								BaseValidator.ProcessEntity(sinfo, (string)obj, sender, eventhandler, baseUri, lineNo, linePos);
							}
						}
						else if (tokenizedType == XmlTokenizedType.ENUMERATION && !attdef.CheckEnumeration(obj))
						{
							XmlSchemaException ex = new XmlSchemaException("'{0}' is not in the enumeration list.", obj.ToString(), baseUri, lineNo, linePos);
							if (eventhandler == null)
							{
								throw ex;
							}
							eventhandler(sender, new ValidationEventArgs(ex));
						}
						attdef.DefaultValueTyped = obj;
					}
				}
			}
			catch
			{
				XmlSchemaException ex2 = new XmlSchemaException("The default value of '{0}' attribute is invalid according to its datatype.", attdef.Name.ToString(), baseUri, lineNo, linePos);
				if (eventhandler == null)
				{
					throw ex2;
				}
				eventhandler(sender, new ValidationEventArgs(ex2));
			}
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x00103670 File Offset: 0x00101870
		internal void AddID(string name, object node)
		{
			if (this.IDs == null)
			{
				this.IDs = new Hashtable();
			}
			this.IDs.Add(name, node);
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x00103692 File Offset: 0x00101892
		public override object FindId(string name)
		{
			if (this.IDs != null)
			{
				return this.IDs[name];
			}
			return null;
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x001036AC File Offset: 0x001018AC
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
			this.context.NeedValidateChildren = false;
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x00103739 File Offset: 0x00101939
		private void Pop()
		{
			if (this.validationStack.Length > 1)
			{
				this.validationStack.Pop();
				this.context = (ValidationState)this.validationStack.Peek();
			}
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x0010376C File Offset: 0x0010196C
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

		// Token: 0x06002A58 RID: 10840 RVA: 0x001037D5 File Offset: 0x001019D5
		private XmlQualifiedName QualifiedName(string name, string ns)
		{
			return new XmlQualifiedName(name, XmlSchemaDatatype.XdrCanonizeUri(ns, base.NameTable, base.SchemaNames));
		}

		// Token: 0x04001CE7 RID: 7399
		private const int STACK_INCREMENT = 10;

		// Token: 0x04001CE8 RID: 7400
		private HWStack validationStack;

		// Token: 0x04001CE9 RID: 7401
		private Hashtable attPresence;

		// Token: 0x04001CEA RID: 7402
		private XmlQualifiedName name = XmlQualifiedName.Empty;

		// Token: 0x04001CEB RID: 7403
		private XmlNamespaceManager nsManager;

		// Token: 0x04001CEC RID: 7404
		private bool isProcessContents;

		// Token: 0x04001CED RID: 7405
		private Hashtable IDs;

		// Token: 0x04001CEE RID: 7406
		private IdRefNode idRefListHead;

		// Token: 0x04001CEF RID: 7407
		private Parser inlineSchemaParser;

		// Token: 0x04001CF0 RID: 7408
		private const string x_schema = "x-schema:";
	}
}
