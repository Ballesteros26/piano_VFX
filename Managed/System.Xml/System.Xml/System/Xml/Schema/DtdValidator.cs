using System;
using System.Collections;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x020003F2 RID: 1010
	internal sealed class DtdValidator : BaseValidator
	{
		// Token: 0x0600273C RID: 10044 RVA: 0x000E5781 File Offset: 0x000E3981
		internal DtdValidator(XmlValidatingReaderImpl reader, IValidationEventHandling eventHandling, bool processIdentityConstraints)
			: base(reader, null, eventHandling)
		{
			this.processIdentityConstraints = processIdentityConstraints;
			this.Init();
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x000E57A4 File Offset: 0x000E39A4
		private void Init()
		{
			this.validationStack = new HWStack(10);
			this.textValue = new StringBuilder();
			this.name = XmlQualifiedName.Empty;
			this.attPresence = new Hashtable();
			this.schemaInfo = new SchemaInfo();
			this.checkDatatype = false;
			this.Push(this.name);
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x000E5800 File Offset: 0x000E3A00
		public override void Validate()
		{
			if (this.schemaInfo.SchemaType == SchemaType.DTD)
			{
				switch (this.reader.NodeType)
				{
				case XmlNodeType.Element:
					this.ValidateElement();
					if (!this.reader.IsEmptyElement)
					{
						return;
					}
					break;
				case XmlNodeType.Attribute:
				case XmlNodeType.Entity:
				case XmlNodeType.Document:
				case XmlNodeType.DocumentType:
				case XmlNodeType.DocumentFragment:
				case XmlNodeType.Notation:
					return;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
					base.ValidateText();
					return;
				case XmlNodeType.EntityReference:
					if (!this.GenEntity(new XmlQualifiedName(this.reader.LocalName, this.reader.Prefix)))
					{
						base.ValidateText();
						return;
					}
					return;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
					this.ValidatePIComment();
					return;
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					if (this.MeetsStandAloneConstraint())
					{
						base.ValidateWhitespace();
						return;
					}
					return;
				case XmlNodeType.EndElement:
					break;
				default:
					return;
				}
				this.ValidateEndElement();
				return;
			}
			if (this.reader.Depth == 0 && this.reader.NodeType == XmlNodeType.Element)
			{
				base.SendValidationEvent("No DTD found.", this.name.ToString(), XmlSeverityType.Warning);
			}
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x000E5908 File Offset: 0x000E3B08
		private bool MeetsStandAloneConstraint()
		{
			if (this.reader.StandAlone && this.context.ElementDecl != null && this.context.ElementDecl.IsDeclaredInExternal && this.context.ElementDecl.ContentValidator.ContentType == XmlSchemaContentType.ElementOnly)
			{
				base.SendValidationEvent("The standalone document declaration must have a value of 'no'.");
				return false;
			}
			return true;
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x000E5967 File Offset: 0x000E3B67
		private void ValidatePIComment()
		{
			if (this.context.NeedValidateChildren && this.context.ElementDecl.ContentValidator == ContentValidator.Empty)
			{
				base.SendValidationEvent("The element cannot contain comment or processing instruction. Content model is empty.");
			}
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x000E5998 File Offset: 0x000E3B98
		private void ValidateElement()
		{
			this.elementName.Init(this.reader.LocalName, this.reader.Prefix);
			if (this.reader.Depth == 0 && !this.schemaInfo.DocTypeName.IsEmpty && !this.schemaInfo.DocTypeName.Equals(this.elementName))
			{
				base.SendValidationEvent("Root element name must match the DocType name.");
			}
			else
			{
				this.ValidateChildElement();
			}
			this.ProcessElement();
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x000E5A18 File Offset: 0x000E3C18
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

		// Token: 0x06002743 RID: 10051 RVA: 0x000E5A9C File Offset: 0x000E3C9C
		private void ValidateStartElement()
		{
			if (this.context.ElementDecl != null)
			{
				base.Reader.SchemaTypeObject = this.context.ElementDecl.SchemaType;
				if (base.Reader.IsEmptyElement && this.context.ElementDecl.DefaultValueTyped != null)
				{
					base.Reader.TypedValueObject = this.context.ElementDecl.DefaultValueTyped;
					this.context.IsNill = true;
				}
				if (this.context.ElementDecl.HasRequiredAttribute)
				{
					this.attPresence.Clear();
				}
			}
			if (base.Reader.MoveToFirstAttribute())
			{
				do
				{
					try
					{
						this.reader.SchemaTypeObject = null;
						SchemaAttDef attDef = this.context.ElementDecl.GetAttDef(new XmlQualifiedName(this.reader.LocalName, this.reader.Prefix));
						if (attDef != null)
						{
							if (this.context.ElementDecl != null && this.context.ElementDecl.HasRequiredAttribute)
							{
								this.attPresence.Add(attDef.Name, attDef);
							}
							base.Reader.SchemaTypeObject = attDef.SchemaType;
							if (attDef.Datatype != null && !this.reader.IsDefault)
							{
								this.CheckValue(base.Reader.Value, attDef);
							}
						}
						else
						{
							base.SendValidationEvent("The '{0}' attribute is not declared.", this.reader.Name);
						}
					}
					catch (XmlSchemaException ex)
					{
						ex.SetSource(base.Reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
						base.SendValidationEvent(ex);
					}
				}
				while (base.Reader.MoveToNextAttribute());
				base.Reader.MoveToElement();
			}
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x000E5C60 File Offset: 0x000E3E60
		private void ValidateEndStartElement()
		{
			if (this.context.ElementDecl.HasRequiredAttribute)
			{
				try
				{
					this.context.ElementDecl.CheckAttributes(this.attPresence, base.Reader.StandAlone);
				}
				catch (XmlSchemaException ex)
				{
					ex.SetSource(base.Reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
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

		// Token: 0x06002745 RID: 10053 RVA: 0x000E5D1C File Offset: 0x000E3F1C
		private void ProcessElement()
		{
			SchemaElementDecl elementDecl = this.schemaInfo.GetElementDecl(this.elementName);
			this.Push(this.elementName);
			if (elementDecl != null)
			{
				this.context.ElementDecl = elementDecl;
				this.ValidateStartElement();
				this.ValidateEndStartElement();
				this.context.NeedValidateChildren = true;
				elementDecl.ContentValidator.InitValidation(this.context);
				return;
			}
			base.SendValidationEvent("The '{0}' element is not declared.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
			this.context.ElementDecl = null;
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x000E5DB2 File Offset: 0x000E3FB2
		public override void CompleteValidation()
		{
			if (this.schemaInfo.SchemaType == SchemaType.DTD)
			{
				do
				{
					this.ValidateEndElement();
				}
				while (this.Pop());
				this.CheckForwardRefs();
			}
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x000E5DD8 File Offset: 0x000E3FD8
		private void ValidateEndElement()
		{
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

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06002748 RID: 10056 RVA: 0x000E5EA9 File Offset: 0x000E40A9
		public override bool PreserveWhitespace
		{
			get
			{
				return this.context.ElementDecl != null && this.context.ElementDecl.ContentValidator.PreserveWhitespace;
			}
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x000E5ED0 File Offset: 0x000E40D0
		private void ProcessTokenizedType(XmlTokenizedType ttype, string name)
		{
			switch (ttype)
			{
			case XmlTokenizedType.ID:
				if (this.processIdentityConstraints)
				{
					if (this.FindId(name) != null)
					{
						base.SendValidationEvent("'{0}' is already used as an ID.", name);
						return;
					}
					this.AddID(name, this.context.LocalName);
					return;
				}
				break;
			case XmlTokenizedType.IDREF:
				if (this.processIdentityConstraints && this.FindId(name) == null)
				{
					this.idRefListHead = new IdRefNode(this.idRefListHead, name, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
					return;
				}
				break;
			case XmlTokenizedType.IDREFS:
				break;
			case XmlTokenizedType.ENTITY:
				BaseValidator.ProcessEntity(this.schemaInfo, name, this, base.EventHandler, base.Reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
				break;
			default:
				return;
			}
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x000E5F98 File Offset: 0x000E4198
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
					object obj = xmlSchemaDatatype.ParseValue(value, base.NameTable, DtdValidator.namespaceManager);
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

		// Token: 0x0600274B RID: 10059 RVA: 0x000E6168 File Offset: 0x000E4368
		internal void AddID(string name, object node)
		{
			if (this.IDs == null)
			{
				this.IDs = new Hashtable();
			}
			this.IDs.Add(name, node);
		}

		// Token: 0x0600274C RID: 10060 RVA: 0x000E618A File Offset: 0x000E438A
		public override object FindId(string name)
		{
			if (this.IDs != null)
			{
				return this.IDs[name];
			}
			return null;
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x000E61A4 File Offset: 0x000E43A4
		private bool GenEntity(XmlQualifiedName qname)
		{
			string text = qname.Name;
			if (text[0] == '#')
			{
				return false;
			}
			if (SchemaEntity.IsPredefinedEntity(text))
			{
				return false;
			}
			SchemaEntity entity = this.GetEntity(qname, false);
			if (entity == null)
			{
				throw new XmlException("Reference to undeclared entity '{0}'.", text);
			}
			if (!entity.NData.IsEmpty)
			{
				throw new XmlException("Reference to unparsed entity '{0}'.", text);
			}
			if (this.reader.StandAlone && entity.DeclaredInExternal)
			{
				base.SendValidationEvent("The standalone document declaration must have a value of 'no'.");
			}
			return true;
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x000E6224 File Offset: 0x000E4424
		private SchemaEntity GetEntity(XmlQualifiedName qname, bool fParameterEntity)
		{
			SchemaEntity schemaEntity;
			if (fParameterEntity)
			{
				if (this.schemaInfo.ParameterEntities.TryGetValue(qname, out schemaEntity))
				{
					return schemaEntity;
				}
			}
			else if (this.schemaInfo.GeneralEntities.TryGetValue(qname, out schemaEntity))
			{
				return schemaEntity;
			}
			return null;
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x000E6264 File Offset: 0x000E4464
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

		// Token: 0x06002750 RID: 10064 RVA: 0x000E62D0 File Offset: 0x000E44D0
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

		// Token: 0x06002751 RID: 10065 RVA: 0x000E635D File Offset: 0x000E455D
		private bool Pop()
		{
			if (this.validationStack.Length > 1)
			{
				this.validationStack.Pop();
				this.context = (ValidationState)this.validationStack.Peek();
				return true;
			}
			return false;
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x000E6394 File Offset: 0x000E4594
		public static void SetDefaultTypedValue(SchemaAttDef attdef, IDtdParserAdapter readerAdapter)
		{
			try
			{
				string text = attdef.DefaultValueExpanded;
				XmlSchemaDatatype datatype = attdef.Datatype;
				if (datatype != null)
				{
					if (datatype.TokenizedType != XmlTokenizedType.CDATA)
					{
						text = text.Trim();
					}
					attdef.DefaultValueTyped = datatype.ParseValue(text, readerAdapter.NameTable, readerAdapter.NamespaceResolver);
				}
			}
			catch (Exception)
			{
				IValidationEventHandling validationEventHandling = ((IDtdParserAdapterWithValidation)readerAdapter).ValidationEventHandling;
				if (validationEventHandling != null)
				{
					XmlSchemaException ex = new XmlSchemaException("The default value of '{0}' attribute is invalid according to its datatype.", attdef.Name.ToString());
					validationEventHandling.SendEvent(ex, XmlSeverityType.Error);
				}
			}
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x000E6420 File Offset: 0x000E4620
		public static void CheckDefaultValue(SchemaAttDef attdef, SchemaInfo sinfo, IValidationEventHandling eventHandling, string baseUriStr)
		{
			try
			{
				if (baseUriStr == null)
				{
					baseUriStr = string.Empty;
				}
				XmlSchemaDatatype datatype = attdef.Datatype;
				if (datatype != null)
				{
					object defaultValueTyped = attdef.DefaultValueTyped;
					XmlTokenizedType tokenizedType = datatype.TokenizedType;
					if (tokenizedType == XmlTokenizedType.ENTITY)
					{
						if (datatype.Variety == XmlSchemaDatatypeVariety.List)
						{
							string[] array = (string[])defaultValueTyped;
							for (int i = 0; i < array.Length; i++)
							{
								BaseValidator.ProcessEntity(sinfo, array[i], eventHandling, baseUriStr, attdef.ValueLineNumber, attdef.ValueLinePosition);
							}
						}
						else
						{
							BaseValidator.ProcessEntity(sinfo, (string)defaultValueTyped, eventHandling, baseUriStr, attdef.ValueLineNumber, attdef.ValueLinePosition);
						}
					}
					else if (tokenizedType == XmlTokenizedType.ENUMERATION && !attdef.CheckEnumeration(defaultValueTyped) && eventHandling != null)
					{
						XmlSchemaException ex = new XmlSchemaException("'{0}' is not in the enumeration list.", defaultValueTyped.ToString(), baseUriStr, attdef.ValueLineNumber, attdef.ValueLinePosition);
						eventHandling.SendEvent(ex, XmlSeverityType.Error);
					}
				}
			}
			catch (Exception)
			{
				if (eventHandling != null)
				{
					XmlSchemaException ex2 = new XmlSchemaException("The default value of '{0}' attribute is invalid according to its datatype.", attdef.Name.ToString());
					eventHandling.SendEvent(ex2, XmlSeverityType.Error);
				}
			}
		}

		// Token: 0x04001A1F RID: 6687
		private static DtdValidator.NamespaceManager namespaceManager = new DtdValidator.NamespaceManager();

		// Token: 0x04001A20 RID: 6688
		private const int STACK_INCREMENT = 10;

		// Token: 0x04001A21 RID: 6689
		private HWStack validationStack;

		// Token: 0x04001A22 RID: 6690
		private Hashtable attPresence;

		// Token: 0x04001A23 RID: 6691
		private XmlQualifiedName name = XmlQualifiedName.Empty;

		// Token: 0x04001A24 RID: 6692
		private Hashtable IDs;

		// Token: 0x04001A25 RID: 6693
		private IdRefNode idRefListHead;

		// Token: 0x04001A26 RID: 6694
		private bool processIdentityConstraints;

		// Token: 0x020003F3 RID: 1011
		private class NamespaceManager : XmlNamespaceManager
		{
			// Token: 0x06002755 RID: 10069 RVA: 0x0000206B File Offset: 0x0000026B
			public override string LookupNamespace(string prefix)
			{
				return prefix;
			}
		}
	}
}
