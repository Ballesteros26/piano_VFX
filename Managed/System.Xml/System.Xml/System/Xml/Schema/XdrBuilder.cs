using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.XmlConfiguration;

namespace System.Xml.Schema
{
	// Token: 0x02000425 RID: 1061
	internal sealed class XdrBuilder : SchemaBuilder
	{
		// Token: 0x060029CE RID: 10702 RVA: 0x0010016C File Offset: 0x000FE36C
		internal XdrBuilder(XmlReader reader, XmlNamespaceManager curmgr, SchemaInfo sinfo, string targetNamspace, XmlNameTable nameTable, SchemaNames schemaNames, ValidationEventHandler eventhandler)
		{
			this._SchemaInfo = sinfo;
			this._TargetNamespace = targetNamspace;
			this._reader = reader;
			this._CurNsMgr = curmgr;
			this.validationEventHandler = eventhandler;
			this._StateHistory = new HWStack(10);
			this._ElementDef = new XdrBuilder.ElementContent();
			this._AttributeDef = new XdrBuilder.AttributeContent();
			this._GroupStack = new HWStack(10);
			this._GroupDef = new XdrBuilder.GroupContent();
			this._NameTable = nameTable;
			this._SchemaNames = schemaNames;
			this._CurState = XdrBuilder.S_SchemaEntries[0];
			this.positionInfo = PositionInfo.GetPositionInfo(this._reader);
			this.xmlResolver = XmlReaderSection.CreateDefaultResolver();
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x00100224 File Offset: 0x000FE424
		internal override bool ProcessElement(string prefix, string name, string ns)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name, XmlSchemaDatatype.XdrCanonizeUri(ns, this._NameTable, this._SchemaNames));
			if (this.GetNextState(xmlQualifiedName))
			{
				this.Push();
				if (this._CurState._InitFunc != null)
				{
					this._CurState._InitFunc(this, xmlQualifiedName);
				}
				return true;
			}
			if (!this.IsSkipableElement(xmlQualifiedName))
			{
				this.SendValidationEvent("The '{0}' element is not supported in this context.", XmlQualifiedName.ToString(name, prefix));
			}
			return false;
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x00100298 File Offset: 0x000FE498
		internal override void ProcessAttribute(string prefix, string name, string ns, string value)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name, XmlSchemaDatatype.XdrCanonizeUri(ns, this._NameTable, this._SchemaNames));
			int i = 0;
			while (i < this._CurState._Attributes.Length)
			{
				XdrBuilder.XdrAttributeEntry xdrAttributeEntry = this._CurState._Attributes[i];
				if (this._SchemaNames.TokenToQName[(int)xdrAttributeEntry._Attribute].Equals(xmlQualifiedName))
				{
					XdrBuilder.XdrBuildFunction buildFunc = xdrAttributeEntry._BuildFunc;
					if (xdrAttributeEntry._Datatype.TokenizedType == XmlTokenizedType.QName)
					{
						string text;
						XmlQualifiedName xmlQualifiedName2 = XmlQualifiedName.Parse(value, this._CurNsMgr, out text);
						xmlQualifiedName2.Atomize(this._NameTable);
						if (text.Length != 0)
						{
							if (xdrAttributeEntry._Attribute != SchemaNames.Token.SchemaType)
							{
								throw new XmlException("This is an unexpected token. The expected token is '{0}'.", "NAME");
							}
						}
						else if (this.IsGlobal(xdrAttributeEntry._SchemaFlags))
						{
							xmlQualifiedName2 = new XmlQualifiedName(xmlQualifiedName2.Name, this._TargetNamespace);
						}
						else
						{
							xmlQualifiedName2 = new XmlQualifiedName(xmlQualifiedName2.Name);
						}
						buildFunc(this, xmlQualifiedName2, text);
						return;
					}
					buildFunc(this, xdrAttributeEntry._Datatype.ParseValue(value, this._NameTable, this._CurNsMgr), string.Empty);
					return;
				}
				else
				{
					i++;
				}
			}
			if (ns == this._SchemaNames.NsXmlNs && XdrBuilder.IsXdrSchema(value))
			{
				this.LoadSchema(value);
				return;
			}
			if (!this.IsSkipableAttribute(xmlQualifiedName))
			{
				this.SendValidationEvent("The '{0}' attribute is not supported in this context.", XmlQualifiedName.ToString(xmlQualifiedName.Name, prefix));
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (set) Token: 0x060029D1 RID: 10705 RVA: 0x00100402 File Offset: 0x000FE602
		internal XmlResolver XmlResolver
		{
			set
			{
				this.xmlResolver = value;
			}
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x0010040C File Offset: 0x000FE60C
		private bool LoadSchema(string uri)
		{
			if (this.xmlResolver == null)
			{
				return false;
			}
			uri = this._NameTable.Add(uri);
			if (this._SchemaInfo.TargetNamespaces.ContainsKey(uri))
			{
				return false;
			}
			SchemaInfo schemaInfo = null;
			Uri uri2 = this.xmlResolver.ResolveUri(null, this._reader.BaseURI);
			XmlReader xmlReader = null;
			try
			{
				Uri uri3 = this.xmlResolver.ResolveUri(uri2, uri.Substring("x-schema:".Length));
				Stream stream = (Stream)this.xmlResolver.GetEntity(uri3, null, null);
				xmlReader = new XmlTextReader(uri3.ToString(), stream, this._NameTable);
				schemaInfo = new SchemaInfo();
				Parser parser = new Parser(SchemaType.XDR, this._NameTable, this._SchemaNames, this.validationEventHandler);
				parser.XmlResolver = this.xmlResolver;
				parser.Parse(xmlReader, uri);
				schemaInfo = parser.XdrSchema;
			}
			catch (XmlException ex)
			{
				this.SendValidationEvent("Cannot load the schema for the namespace '{0}' - {1}", new string[] { uri, ex.Message }, XmlSeverityType.Warning);
				schemaInfo = null;
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
				this._SchemaInfo.Add(schemaInfo, this.validationEventHandler);
				return true;
			}
			return false;
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x00100550 File Offset: 0x000FE750
		internal static bool IsXdrSchema(string uri)
		{
			return uri.Length >= "x-schema:".Length && string.Compare(uri, 0, "x-schema:", 0, "x-schema:".Length, StringComparison.Ordinal) == 0 && !uri.StartsWith("x-schema:#", StringComparison.Ordinal);
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x00003242 File Offset: 0x00001442
		internal override bool IsContentParsed()
		{
			return true;
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x00016C08 File Offset: 0x00014E08
		internal override void ProcessMarkup(XmlNode[] markup)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x0010058F File Offset: 0x000FE78F
		internal override void ProcessCData(string value)
		{
			if (this._CurState._AllowText)
			{
				this._Text = value;
				return;
			}
			this.SendValidationEvent("The following text is not allowed in this context: '{0}'.", value);
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x001005B2 File Offset: 0x000FE7B2
		internal override void StartChildren()
		{
			if (this._CurState._BeginChildFunc != null)
			{
				this._CurState._BeginChildFunc(this);
			}
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x001005D2 File Offset: 0x000FE7D2
		internal override void EndChildren()
		{
			if (this._CurState._EndChildFunc != null)
			{
				this._CurState._EndChildFunc(this);
			}
			this.Pop();
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x001005F8 File Offset: 0x000FE7F8
		private void Push()
		{
			this._StateHistory.Push();
			this._StateHistory[this._StateHistory.Length - 1] = this._CurState;
			this._CurState = this._NextState;
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x00100630 File Offset: 0x000FE830
		private void Pop()
		{
			this._CurState = (XdrBuilder.XdrEntry)this._StateHistory.Pop();
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x00100648 File Offset: 0x000FE848
		private void PushGroupInfo()
		{
			this._GroupStack.Push();
			this._GroupStack[this._GroupStack.Length - 1] = XdrBuilder.GroupContent.Copy(this._GroupDef);
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x00100679 File Offset: 0x000FE879
		private void PopGroupInfo()
		{
			this._GroupDef = (XdrBuilder.GroupContent)this._GroupStack.Pop();
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x00100691 File Offset: 0x000FE891
		private static void XDR_InitRoot(XdrBuilder builder, object obj)
		{
			builder._SchemaInfo.SchemaType = SchemaType.XDR;
			builder._ElementDef._ElementDecl = null;
			builder._ElementDef._AttDefList = null;
			builder._AttributeDef._AttDef = null;
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x001006C3 File Offset: 0x000FE8C3
		private static void XDR_BuildRoot_Name(XdrBuilder builder, object obj, string prefix)
		{
			builder._XdrName = (string)obj;
			builder._XdrPrefix = prefix;
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x00002F50 File Offset: 0x00001150
		private static void XDR_BuildRoot_ID(XdrBuilder builder, object obj, string prefix)
		{
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x001006D8 File Offset: 0x000FE8D8
		private static void XDR_BeginRoot(XdrBuilder builder)
		{
			if (builder._TargetNamespace == null)
			{
				if (builder._XdrName != null)
				{
					builder._TargetNamespace = builder._NameTable.Add("x-schema:#" + builder._XdrName);
				}
				else
				{
					builder._TargetNamespace = string.Empty;
				}
			}
			builder._SchemaInfo.TargetNamespaces.Add(builder._TargetNamespace, true);
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x0010073C File Offset: 0x000FE93C
		private static void XDR_EndRoot(XdrBuilder builder)
		{
			while (builder._UndefinedAttributeTypes != null)
			{
				XmlQualifiedName xmlQualifiedName = builder._UndefinedAttributeTypes._TypeName;
				if (xmlQualifiedName.Namespace.Length == 0)
				{
					xmlQualifiedName = new XmlQualifiedName(xmlQualifiedName.Name, builder._TargetNamespace);
				}
				SchemaAttDef schemaAttDef;
				if (builder._SchemaInfo.AttributeDecls.TryGetValue(xmlQualifiedName, out schemaAttDef))
				{
					builder._UndefinedAttributeTypes._Attdef = schemaAttDef.Clone();
					builder._UndefinedAttributeTypes._Attdef.Name = xmlQualifiedName;
					builder.XDR_CheckAttributeDefault(builder._UndefinedAttributeTypes, builder._UndefinedAttributeTypes._Attdef);
				}
				else
				{
					builder.SendValidationEvent("The '{0}' attribute is not declared.", xmlQualifiedName.Name);
				}
				builder._UndefinedAttributeTypes = builder._UndefinedAttributeTypes._Next;
			}
			foreach (object obj in builder._UndeclaredElements.Values)
			{
				SchemaElementDecl schemaElementDecl = (SchemaElementDecl)obj;
				builder.SendValidationEvent("The '{0}' element is not declared.", XmlQualifiedName.ToString(schemaElementDecl.Name.Name, schemaElementDecl.Prefix));
			}
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x00100864 File Offset: 0x000FEA64
		private static void XDR_InitElementType(XdrBuilder builder, object obj)
		{
			builder._ElementDef._ElementDecl = new SchemaElementDecl();
			builder._contentValidator = new ParticleContentValidator(XmlSchemaContentType.Mixed);
			builder._contentValidator.IsOpen = true;
			builder._ElementDef._ContentAttr = 0;
			builder._ElementDef._OrderAttr = 0;
			builder._ElementDef._MasterGroupRequired = false;
			builder._ElementDef._ExistTerminal = false;
			builder._ElementDef._AllowDataType = true;
			builder._ElementDef._HasDataType = false;
			builder._ElementDef._EnumerationRequired = false;
			builder._ElementDef._AttDefList = new Hashtable();
			builder._ElementDef._MaxLength = uint.MaxValue;
			builder._ElementDef._MinLength = uint.MaxValue;
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x00100918 File Offset: 0x000FEB18
		private static void XDR_BuildElementType_Name(XdrBuilder builder, object obj, string prefix)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
			if (builder._SchemaInfo.ElementDecls.ContainsKey(xmlQualifiedName))
			{
				builder.SendValidationEvent("The '{0}' element has already been declared.", XmlQualifiedName.ToString(xmlQualifiedName.Name, prefix));
			}
			builder._ElementDef._ElementDecl.Name = xmlQualifiedName;
			builder._ElementDef._ElementDecl.Prefix = prefix;
			builder._SchemaInfo.ElementDecls.Add(xmlQualifiedName, builder._ElementDef._ElementDecl);
			if (builder._UndeclaredElements[xmlQualifiedName] != null)
			{
				builder._UndeclaredElements.Remove(xmlQualifiedName);
			}
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x001009AE File Offset: 0x000FEBAE
		private static void XDR_BuildElementType_Content(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._ContentAttr = builder.GetContent((XmlQualifiedName)obj);
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x001009C7 File Offset: 0x000FEBC7
		private static void XDR_BuildElementType_Model(XdrBuilder builder, object obj, string prefix)
		{
			builder._contentValidator.IsOpen = builder.GetModel((XmlQualifiedName)obj);
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x001009E0 File Offset: 0x000FEBE0
		private static void XDR_BuildElementType_Order(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._OrderAttr = (builder._GroupDef._Order = builder.GetOrder((XmlQualifiedName)obj));
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x00100A14 File Offset: 0x000FEC14
		private static void XDR_BuildElementType_DtType(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._HasDataType = true;
			string text = ((string)obj).Trim();
			if (text.Length == 0)
			{
				builder.SendValidationEvent("The DataType value cannot be empty.");
				return;
			}
			XmlSchemaDatatype xmlSchemaDatatype = XmlSchemaDatatype.FromXdrName(text);
			if (xmlSchemaDatatype == null)
			{
				builder.SendValidationEvent("Reference to an unknown data type, '{0}'.", text);
			}
			builder._ElementDef._ElementDecl.Datatype = xmlSchemaDatatype;
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x00100A74 File Offset: 0x000FEC74
		private static void XDR_BuildElementType_DtValues(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._EnumerationRequired = true;
			builder._ElementDef._ElementDecl.Values = new List<string>((string[])obj);
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x00100A9D File Offset: 0x000FEC9D
		private static void XDR_BuildElementType_DtMaxLength(XdrBuilder builder, object obj, string prefix)
		{
			XdrBuilder.ParseDtMaxLength(ref builder._ElementDef._MaxLength, obj, builder);
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x00100AB1 File Offset: 0x000FECB1
		private static void XDR_BuildElementType_DtMinLength(XdrBuilder builder, object obj, string prefix)
		{
			XdrBuilder.ParseDtMinLength(ref builder._ElementDef._MinLength, obj, builder);
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x00100AC8 File Offset: 0x000FECC8
		private static void XDR_BeginElementType(XdrBuilder builder)
		{
			string text = null;
			string text2 = null;
			if (builder._ElementDef._ElementDecl.Name.IsEmpty)
			{
				text = "The '{0}' attribute is either invalid or missing.";
				text2 = "name";
			}
			else
			{
				if (builder._ElementDef._HasDataType)
				{
					if (!builder._ElementDef._AllowDataType)
					{
						text = "Content must be \"textOnly\" when using DataType on an ElementType.";
						goto IL_01F4;
					}
					builder._ElementDef._ContentAttr = 2;
				}
				else if (builder._ElementDef._ContentAttr == 0)
				{
					switch (builder._ElementDef._OrderAttr)
					{
					case 0:
						builder._ElementDef._ContentAttr = 3;
						builder._ElementDef._OrderAttr = 1;
						break;
					case 1:
						builder._ElementDef._ContentAttr = 3;
						break;
					case 2:
						builder._ElementDef._ContentAttr = 4;
						break;
					case 3:
						builder._ElementDef._ContentAttr = 4;
						break;
					}
				}
				bool isOpen = builder._contentValidator.IsOpen;
				XdrBuilder.ElementContent elementDef = builder._ElementDef;
				switch (builder._ElementDef._ContentAttr)
				{
				case 1:
					builder._ElementDef._ElementDecl.ContentValidator = ContentValidator.Empty;
					builder._contentValidator = null;
					break;
				case 2:
					builder._ElementDef._ElementDecl.ContentValidator = ContentValidator.TextOnly;
					builder._GroupDef._Order = 1;
					builder._contentValidator = null;
					break;
				case 3:
					if (elementDef._OrderAttr != 0 && elementDef._OrderAttr != 1)
					{
						text = "The order must be many when content is mixed.";
						goto IL_01F4;
					}
					builder._GroupDef._Order = 1;
					elementDef._MasterGroupRequired = true;
					builder._contentValidator.IsOpen = isOpen;
					break;
				case 4:
					builder._contentValidator = new ParticleContentValidator(XmlSchemaContentType.ElementOnly);
					if (elementDef._OrderAttr == 0)
					{
						builder._GroupDef._Order = 2;
					}
					elementDef._MasterGroupRequired = true;
					builder._contentValidator.IsOpen = isOpen;
					break;
				}
				if (elementDef._ContentAttr == 3 || elementDef._ContentAttr == 4)
				{
					builder._contentValidator.Start();
					builder._contentValidator.OpenGroup();
				}
			}
			IL_01F4:
			if (text != null)
			{
				builder.SendValidationEvent(text, text2);
			}
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x00100CD4 File Offset: 0x000FEED4
		private static void XDR_EndElementType(XdrBuilder builder)
		{
			SchemaElementDecl elementDecl = builder._ElementDef._ElementDecl;
			if (builder._UndefinedAttributeTypes != null && builder._ElementDef._AttDefList != null)
			{
				XdrBuilder.DeclBaseInfo declBaseInfo = builder._UndefinedAttributeTypes;
				XdrBuilder.DeclBaseInfo declBaseInfo2 = declBaseInfo;
				while (declBaseInfo != null)
				{
					SchemaAttDef schemaAttDef = null;
					if (declBaseInfo._ElementDecl == elementDecl)
					{
						XmlQualifiedName typeName = declBaseInfo._TypeName;
						schemaAttDef = (SchemaAttDef)builder._ElementDef._AttDefList[typeName];
						if (schemaAttDef != null)
						{
							declBaseInfo._Attdef = schemaAttDef.Clone();
							declBaseInfo._Attdef.Name = typeName;
							builder.XDR_CheckAttributeDefault(declBaseInfo, schemaAttDef);
							if (declBaseInfo == builder._UndefinedAttributeTypes)
							{
								declBaseInfo = (builder._UndefinedAttributeTypes = declBaseInfo._Next);
								declBaseInfo2 = declBaseInfo;
							}
							else
							{
								declBaseInfo2._Next = declBaseInfo._Next;
								declBaseInfo = declBaseInfo2._Next;
							}
						}
					}
					if (schemaAttDef == null)
					{
						if (declBaseInfo != builder._UndefinedAttributeTypes)
						{
							declBaseInfo2 = declBaseInfo2._Next;
						}
						declBaseInfo = declBaseInfo._Next;
					}
				}
			}
			if (builder._ElementDef._MasterGroupRequired)
			{
				builder._contentValidator.CloseGroup();
				if (!builder._ElementDef._ExistTerminal)
				{
					if (builder._contentValidator.IsOpen)
					{
						builder._ElementDef._ElementDecl.ContentValidator = ContentValidator.Any;
						builder._contentValidator = null;
					}
					else if (builder._ElementDef._ContentAttr != 3)
					{
						builder.SendValidationEvent("There is a missing element.");
					}
				}
				else if (builder._GroupDef._Order == 1)
				{
					builder._contentValidator.AddStar();
				}
			}
			if (elementDecl.Datatype != null)
			{
				XmlTokenizedType tokenizedType = elementDecl.Datatype.TokenizedType;
				if (tokenizedType == XmlTokenizedType.ENUMERATION && !builder._ElementDef._EnumerationRequired)
				{
					builder.SendValidationEvent("The dt:values attribute is missing.");
				}
				if (tokenizedType != XmlTokenizedType.ENUMERATION && builder._ElementDef._EnumerationRequired)
				{
					builder.SendValidationEvent("Data type should be enumeration when the values attribute is present.");
				}
			}
			XdrBuilder.CompareMinMaxLength(builder._ElementDef._MinLength, builder._ElementDef._MaxLength, builder);
			elementDecl.MaxLength = (long)((ulong)builder._ElementDef._MaxLength);
			elementDecl.MinLength = (long)((ulong)builder._ElementDef._MinLength);
			if (builder._contentValidator != null)
			{
				builder._ElementDef._ElementDecl.ContentValidator = builder._contentValidator.Finish(true);
				builder._contentValidator = null;
			}
			builder._ElementDef._ElementDecl = null;
			builder._ElementDef._AttDefList = null;
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x00100F0C File Offset: 0x000FF10C
		private static void XDR_InitAttributeType(XdrBuilder builder, object obj)
		{
			XdrBuilder.AttributeContent attributeDef = builder._AttributeDef;
			attributeDef._AttDef = new SchemaAttDef(XmlQualifiedName.Empty, null);
			attributeDef._Required = false;
			attributeDef._Prefix = null;
			attributeDef._Default = null;
			attributeDef._MinVal = 0U;
			attributeDef._MaxVal = 1U;
			attributeDef._EnumerationRequired = false;
			attributeDef._HasDataType = false;
			attributeDef._Global = builder._StateHistory.Length == 2;
			attributeDef._MaxLength = uint.MaxValue;
			attributeDef._MinLength = uint.MaxValue;
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x00100F84 File Offset: 0x000FF184
		private static void XDR_BuildAttributeType_Name(XdrBuilder builder, object obj, string prefix)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
			builder._AttributeDef._Name = xmlQualifiedName;
			builder._AttributeDef._Prefix = prefix;
			builder._AttributeDef._AttDef.Name = xmlQualifiedName;
			if (builder._ElementDef._ElementDecl != null)
			{
				if (builder._ElementDef._AttDefList[xmlQualifiedName] == null)
				{
					builder._ElementDef._AttDefList.Add(xmlQualifiedName, builder._AttributeDef._AttDef);
					return;
				}
				builder.SendValidationEvent("The '{0}' attribute has already been declared for this ElementType.", XmlQualifiedName.ToString(xmlQualifiedName.Name, prefix));
				return;
			}
			else
			{
				xmlQualifiedName = new XmlQualifiedName(xmlQualifiedName.Name, builder._TargetNamespace);
				builder._AttributeDef._AttDef.Name = xmlQualifiedName;
				if (!builder._SchemaInfo.AttributeDecls.ContainsKey(xmlQualifiedName))
				{
					builder._SchemaInfo.AttributeDecls.Add(xmlQualifiedName, builder._AttributeDef._AttDef);
					return;
				}
				builder.SendValidationEvent("The '{0}' attribute has already been declared for this ElementType.", XmlQualifiedName.ToString(xmlQualifiedName.Name, prefix));
				return;
			}
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x00101080 File Offset: 0x000FF280
		private static void XDR_BuildAttributeType_Required(XdrBuilder builder, object obj, string prefix)
		{
			builder._AttributeDef._Required = XdrBuilder.IsYes(obj, builder);
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x00101094 File Offset: 0x000FF294
		private static void XDR_BuildAttributeType_Default(XdrBuilder builder, object obj, string prefix)
		{
			builder._AttributeDef._Default = obj;
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x001010A4 File Offset: 0x000FF2A4
		private static void XDR_BuildAttributeType_DtType(XdrBuilder builder, object obj, string prefix)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
			builder._AttributeDef._HasDataType = true;
			builder._AttributeDef._AttDef.Datatype = builder.CheckDatatype(xmlQualifiedName.Name);
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x001010E0 File Offset: 0x000FF2E0
		private static void XDR_BuildAttributeType_DtValues(XdrBuilder builder, object obj, string prefix)
		{
			builder._AttributeDef._EnumerationRequired = true;
			builder._AttributeDef._AttDef.Values = new List<string>((string[])obj);
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x00101109 File Offset: 0x000FF309
		private static void XDR_BuildAttributeType_DtMaxLength(XdrBuilder builder, object obj, string prefix)
		{
			XdrBuilder.ParseDtMaxLength(ref builder._AttributeDef._MaxLength, obj, builder);
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x0010111D File Offset: 0x000FF31D
		private static void XDR_BuildAttributeType_DtMinLength(XdrBuilder builder, object obj, string prefix)
		{
			XdrBuilder.ParseDtMinLength(ref builder._AttributeDef._MinLength, obj, builder);
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x00101131 File Offset: 0x000FF331
		private static void XDR_BeginAttributeType(XdrBuilder builder)
		{
			if (builder._AttributeDef._Name.IsEmpty)
			{
				builder.SendValidationEvent("The '{0}' attribute is either invalid or missing.");
			}
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x00101150 File Offset: 0x000FF350
		private static void XDR_EndAttributeType(XdrBuilder builder)
		{
			string text = null;
			if (builder._AttributeDef._HasDataType && builder._AttributeDef._AttDef.Datatype != null)
			{
				XmlTokenizedType tokenizedType = builder._AttributeDef._AttDef.Datatype.TokenizedType;
				if (tokenizedType == XmlTokenizedType.ENUMERATION && !builder._AttributeDef._EnumerationRequired)
				{
					text = "The dt:values attribute is missing.";
					goto IL_0164;
				}
				if (tokenizedType != XmlTokenizedType.ENUMERATION && builder._AttributeDef._EnumerationRequired)
				{
					text = "Data type should be enumeration when the values attribute is present.";
					goto IL_0164;
				}
				if (builder._AttributeDef._Default != null && tokenizedType == XmlTokenizedType.ID)
				{
					text = "An attribute or element of type xs:ID or derived from xs:ID, should not have a value constraint.";
					goto IL_0164;
				}
			}
			else
			{
				builder._AttributeDef._AttDef.Datatype = XmlSchemaDatatype.FromXmlTokenizedType(XmlTokenizedType.CDATA);
			}
			XdrBuilder.CompareMinMaxLength(builder._AttributeDef._MinLength, builder._AttributeDef._MaxLength, builder);
			builder._AttributeDef._AttDef.MaxLength = (long)((ulong)builder._AttributeDef._MaxLength);
			builder._AttributeDef._AttDef.MinLength = (long)((ulong)builder._AttributeDef._MinLength);
			if (builder._AttributeDef._Default != null)
			{
				builder._AttributeDef._AttDef.DefaultValueRaw = (builder._AttributeDef._AttDef.DefaultValueExpanded = (string)builder._AttributeDef._Default);
				builder.CheckDefaultAttValue(builder._AttributeDef._AttDef);
			}
			builder.SetAttributePresence(builder._AttributeDef._AttDef, builder._AttributeDef._Required);
			IL_0164:
			if (text != null)
			{
				builder.SendValidationEvent(text);
			}
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x001012CC File Offset: 0x000FF4CC
		private static void XDR_InitElement(XdrBuilder builder, object obj)
		{
			if (builder._ElementDef._HasDataType || builder._ElementDef._ContentAttr == 1 || builder._ElementDef._ContentAttr == 2)
			{
				builder.SendValidationEvent("Element is not allowed when the content is empty or textOnly.");
			}
			builder._ElementDef._AllowDataType = false;
			builder._ElementDef._HasType = false;
			builder._ElementDef._MinVal = 1U;
			builder._ElementDef._MaxVal = 1U;
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x00101340 File Offset: 0x000FF540
		private static void XDR_BuildElement_Type(XdrBuilder builder, object obj, string prefix)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
			if (!builder._SchemaInfo.ElementDecls.ContainsKey(xmlQualifiedName) && (SchemaElementDecl)builder._UndeclaredElements[xmlQualifiedName] == null)
			{
				SchemaElementDecl schemaElementDecl = new SchemaElementDecl(xmlQualifiedName, prefix);
				builder._UndeclaredElements.Add(xmlQualifiedName, schemaElementDecl);
			}
			builder._ElementDef._HasType = true;
			if (builder._ElementDef._ExistTerminal)
			{
				builder.AddOrder();
			}
			else
			{
				builder._ElementDef._ExistTerminal = true;
			}
			builder._contentValidator.AddName(xmlQualifiedName, null);
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x001013CB File Offset: 0x000FF5CB
		private static void XDR_BuildElement_MinOccurs(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._MinVal = XdrBuilder.ParseMinOccurs(obj, builder);
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x001013DF File Offset: 0x000FF5DF
		private static void XDR_BuildElement_MaxOccurs(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._MaxVal = XdrBuilder.ParseMaxOccurs(obj, builder);
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x001013F3 File Offset: 0x000FF5F3
		private static void XDR_EndElement(XdrBuilder builder)
		{
			if (builder._ElementDef._HasType)
			{
				XdrBuilder.HandleMinMax(builder._contentValidator, builder._ElementDef._MinVal, builder._ElementDef._MaxVal);
				return;
			}
			builder.SendValidationEvent("The '{0}' attribute is either invalid or missing.");
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x0010142F File Offset: 0x000FF62F
		private static void XDR_InitAttribute(XdrBuilder builder, object obj)
		{
			if (builder._BaseDecl == null)
			{
				builder._BaseDecl = new XdrBuilder.DeclBaseInfo();
			}
			builder._BaseDecl._MinOccurs = 0U;
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x00101450 File Offset: 0x000FF650
		private static void XDR_BuildAttribute_Type(XdrBuilder builder, object obj, string prefix)
		{
			builder._BaseDecl._TypeName = (XmlQualifiedName)obj;
			builder._BaseDecl._Prefix = prefix;
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x0010146F File Offset: 0x000FF66F
		private static void XDR_BuildAttribute_Required(XdrBuilder builder, object obj, string prefix)
		{
			if (XdrBuilder.IsYes(obj, builder))
			{
				builder._BaseDecl._MinOccurs = 1U;
			}
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x00101486 File Offset: 0x000FF686
		private static void XDR_BuildAttribute_Default(XdrBuilder builder, object obj, string prefix)
		{
			builder._BaseDecl._Default = obj;
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x00101494 File Offset: 0x000FF694
		private static void XDR_BeginAttribute(XdrBuilder builder)
		{
			if (builder._BaseDecl._TypeName.IsEmpty)
			{
				builder.SendValidationEvent("The '{0}' attribute is either invalid or missing.");
			}
			SchemaAttDef schemaAttDef = null;
			XmlQualifiedName typeName = builder._BaseDecl._TypeName;
			string prefix = builder._BaseDecl._Prefix;
			if (builder._ElementDef._AttDefList != null)
			{
				schemaAttDef = (SchemaAttDef)builder._ElementDef._AttDefList[typeName];
			}
			if (schemaAttDef == null)
			{
				XmlQualifiedName xmlQualifiedName = typeName;
				if (prefix.Length == 0)
				{
					xmlQualifiedName = new XmlQualifiedName(typeName.Name, builder._TargetNamespace);
				}
				SchemaAttDef schemaAttDef2;
				if (builder._SchemaInfo.AttributeDecls.TryGetValue(xmlQualifiedName, out schemaAttDef2))
				{
					schemaAttDef = schemaAttDef2.Clone();
					schemaAttDef.Name = typeName;
				}
				else if (prefix.Length != 0)
				{
					builder.SendValidationEvent("The '{0}' attribute is not declared.", XmlQualifiedName.ToString(typeName.Name, prefix));
				}
			}
			if (schemaAttDef != null)
			{
				builder.XDR_CheckAttributeDefault(builder._BaseDecl, schemaAttDef);
			}
			else
			{
				schemaAttDef = new SchemaAttDef(typeName, prefix);
				builder._UndefinedAttributeTypes = new XdrBuilder.DeclBaseInfo
				{
					_Checking = true,
					_Attdef = schemaAttDef,
					_TypeName = builder._BaseDecl._TypeName,
					_ElementDecl = builder._ElementDef._ElementDecl,
					_MinOccurs = builder._BaseDecl._MinOccurs,
					_Default = builder._BaseDecl._Default,
					_Next = builder._UndefinedAttributeTypes
				};
			}
			builder._ElementDef._ElementDecl.AddAttDef(schemaAttDef);
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x001015FF File Offset: 0x000FF7FF
		private static void XDR_EndAttribute(XdrBuilder builder)
		{
			builder._BaseDecl.Reset();
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x0010160C File Offset: 0x000FF80C
		private static void XDR_InitGroup(XdrBuilder builder, object obj)
		{
			if (builder._ElementDef._ContentAttr == 1 || builder._ElementDef._ContentAttr == 2)
			{
				builder.SendValidationEvent("The group is not allowed when ElementType has empty or textOnly content.");
			}
			builder.PushGroupInfo();
			builder._GroupDef._MinVal = 1U;
			builder._GroupDef._MaxVal = 1U;
			builder._GroupDef._HasMaxAttr = false;
			builder._GroupDef._HasMinAttr = false;
			if (builder._ElementDef._ExistTerminal)
			{
				builder.AddOrder();
			}
			builder._ElementDef._ExistTerminal = false;
			builder._contentValidator.OpenGroup();
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x001016A0 File Offset: 0x000FF8A0
		private static void XDR_BuildGroup_Order(XdrBuilder builder, object obj, string prefix)
		{
			builder._GroupDef._Order = builder.GetOrder((XmlQualifiedName)obj);
			if (builder._ElementDef._ContentAttr == 3 && builder._GroupDef._Order != 1)
			{
				builder.SendValidationEvent("The order must be many when content is mixed.");
			}
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x001016E0 File Offset: 0x000FF8E0
		private static void XDR_BuildGroup_MinOccurs(XdrBuilder builder, object obj, string prefix)
		{
			builder._GroupDef._MinVal = XdrBuilder.ParseMinOccurs(obj, builder);
			builder._GroupDef._HasMinAttr = true;
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x00101700 File Offset: 0x000FF900
		private static void XDR_BuildGroup_MaxOccurs(XdrBuilder builder, object obj, string prefix)
		{
			builder._GroupDef._MaxVal = XdrBuilder.ParseMaxOccurs(obj, builder);
			builder._GroupDef._HasMaxAttr = true;
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x00101720 File Offset: 0x000FF920
		private static void XDR_EndGroup(XdrBuilder builder)
		{
			if (!builder._ElementDef._ExistTerminal)
			{
				builder.SendValidationEvent("There is a missing element.");
			}
			builder._contentValidator.CloseGroup();
			if (builder._GroupDef._Order == 1)
			{
				builder._contentValidator.AddStar();
			}
			if (1 == builder._GroupDef._Order && builder._GroupDef._HasMaxAttr && builder._GroupDef._MaxVal != 4294967295U)
			{
				builder.SendValidationEvent("When the order is many, the maxOccurs attribute must have a value of '*'.");
			}
			XdrBuilder.HandleMinMax(builder._contentValidator, builder._GroupDef._MinVal, builder._GroupDef._MaxVal);
			builder.PopGroupInfo();
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x001017C4 File Offset: 0x000FF9C4
		private static void XDR_InitElementDtType(XdrBuilder builder, object obj)
		{
			if (builder._ElementDef._HasDataType)
			{
				builder.SendValidationEvent("Data type has already been declared.");
			}
			if (!builder._ElementDef._AllowDataType)
			{
				builder.SendValidationEvent("Content must be \"textOnly\" when using DataType on an ElementType.");
			}
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x001017F8 File Offset: 0x000FF9F8
		private static void XDR_EndElementDtType(XdrBuilder builder)
		{
			if (!builder._ElementDef._HasDataType)
			{
				builder.SendValidationEvent("The '{0}' attribute is either invalid or missing.");
			}
			builder._ElementDef._ElementDecl.ContentValidator = ContentValidator.TextOnly;
			builder._ElementDef._ContentAttr = 2;
			builder._ElementDef._MasterGroupRequired = false;
			builder._contentValidator = null;
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x00101851 File Offset: 0x000FFA51
		private static void XDR_InitAttributeDtType(XdrBuilder builder, object obj)
		{
			if (builder._AttributeDef._HasDataType)
			{
				builder.SendValidationEvent("Data type has already been declared.");
			}
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x0010186C File Offset: 0x000FFA6C
		private static void XDR_EndAttributeDtType(XdrBuilder builder)
		{
			string text = null;
			if (!builder._AttributeDef._HasDataType)
			{
				text = "The '{0}' attribute is either invalid or missing.";
			}
			else if (builder._AttributeDef._AttDef.Datatype != null)
			{
				XmlTokenizedType tokenizedType = builder._AttributeDef._AttDef.Datatype.TokenizedType;
				if (tokenizedType == XmlTokenizedType.ENUMERATION && !builder._AttributeDef._EnumerationRequired)
				{
					text = "The dt:values attribute is missing.";
				}
				else if (tokenizedType != XmlTokenizedType.ENUMERATION && builder._AttributeDef._EnumerationRequired)
				{
					text = "Data type should be enumeration when the values attribute is present.";
				}
			}
			if (text != null)
			{
				builder.SendValidationEvent(text);
			}
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x001018F4 File Offset: 0x000FFAF4
		private bool GetNextState(XmlQualifiedName qname)
		{
			if (this._CurState._NextStates != null)
			{
				for (int i = 0; i < this._CurState._NextStates.Length; i++)
				{
					if (this._SchemaNames.TokenToQName[(int)XdrBuilder.S_SchemaEntries[this._CurState._NextStates[i]]._Name].Equals(qname))
					{
						this._NextState = XdrBuilder.S_SchemaEntries[this._CurState._NextStates[i]];
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x00101970 File Offset: 0x000FFB70
		private bool IsSkipableElement(XmlQualifiedName qname)
		{
			string @namespace = qname.Namespace;
			return (@namespace != null && !Ref.Equal(@namespace, this._SchemaNames.NsXdr)) || (this._SchemaNames.TokenToQName[38].Equals(qname) || this._SchemaNames.TokenToQName[39].Equals(qname));
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x001019CC File Offset: 0x000FFBCC
		private bool IsSkipableAttribute(XmlQualifiedName qname)
		{
			string @namespace = qname.Namespace;
			return (@namespace.Length != 0 && !Ref.Equal(@namespace, this._SchemaNames.NsXdr) && !Ref.Equal(@namespace, this._SchemaNames.NsDataType)) || (Ref.Equal(@namespace, this._SchemaNames.NsDataType) && this._CurState._Name == SchemaNames.Token.XdrDatatype && (this._SchemaNames.QnDtMax.Equals(qname) || this._SchemaNames.QnDtMin.Equals(qname) || this._SchemaNames.QnDtMaxExclusive.Equals(qname) || this._SchemaNames.QnDtMinExclusive.Equals(qname)));
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x00101A84 File Offset: 0x000FFC84
		private int GetOrder(XmlQualifiedName qname)
		{
			int num = 0;
			if (this._SchemaNames.TokenToQName[15].Equals(qname))
			{
				num = 2;
			}
			else if (this._SchemaNames.TokenToQName[16].Equals(qname))
			{
				num = 3;
			}
			else if (this._SchemaNames.TokenToQName[17].Equals(qname))
			{
				num = 1;
			}
			else
			{
				this.SendValidationEvent("The order attribute must have a value of 'seq', 'one', or 'many', not '{0}'.", qname.Name);
			}
			return num;
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x00101AF4 File Offset: 0x000FFCF4
		private void AddOrder()
		{
			switch (this._GroupDef._Order)
			{
			case 1:
			case 3:
				this._contentValidator.AddChoice();
				return;
			case 2:
				this._contentValidator.AddSequence();
				return;
			}
			throw new XmlException("This is an unexpected token. The expected token is '{0}'.", "NAME");
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x00101B50 File Offset: 0x000FFD50
		private static bool IsYes(object obj, XdrBuilder builder)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
			bool flag = false;
			if (xmlQualifiedName.Name == "yes")
			{
				flag = true;
			}
			else if (xmlQualifiedName.Name != "no")
			{
				builder.SendValidationEvent("The required attribute must have a value of yes or no.");
			}
			return flag;
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x00101B9C File Offset: 0x000FFD9C
		private static uint ParseMinOccurs(object obj, XdrBuilder builder)
		{
			uint num = 1U;
			if (!XdrBuilder.ParseInteger((string)obj, ref num) || (num != 0U && num != 1U))
			{
				builder.SendValidationEvent("The minOccurs attribute must have a value of 0 or 1.");
			}
			return num;
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x00101BD0 File Offset: 0x000FFDD0
		private static uint ParseMaxOccurs(object obj, XdrBuilder builder)
		{
			uint maxValue = uint.MaxValue;
			string text = (string)obj;
			if (!text.Equals("*") && (!XdrBuilder.ParseInteger(text, ref maxValue) || (maxValue != 4294967295U && maxValue != 1U)))
			{
				builder.SendValidationEvent("The maxOccurs attribute must have a value of 1 or *.");
			}
			return maxValue;
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x00101C11 File Offset: 0x000FFE11
		private static void HandleMinMax(ParticleContentValidator pContent, uint cMin, uint cMax)
		{
			if (pContent != null)
			{
				if (cMax == 4294967295U)
				{
					if (cMin == 0U)
					{
						pContent.AddStar();
						return;
					}
					pContent.AddPlus();
					return;
				}
				else if (cMin == 0U)
				{
					pContent.AddQMark();
				}
			}
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x00101C34 File Offset: 0x000FFE34
		private static void ParseDtMaxLength(ref uint cVal, object obj, XdrBuilder builder)
		{
			if (4294967295U != cVal)
			{
				builder.SendValidationEvent("The value of maxLength has already been declared.");
			}
			if (!XdrBuilder.ParseInteger((string)obj, ref cVal) || cVal < 0U)
			{
				builder.SendValidationEvent("The value '{0}' is invalid for dt:maxLength.", obj.ToString());
			}
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x00101C6A File Offset: 0x000FFE6A
		private static void ParseDtMinLength(ref uint cVal, object obj, XdrBuilder builder)
		{
			if (4294967295U != cVal)
			{
				builder.SendValidationEvent("The value of minLength has already been declared.");
			}
			if (!XdrBuilder.ParseInteger((string)obj, ref cVal) || cVal < 0U)
			{
				builder.SendValidationEvent("The value '{0}' is invalid for dt:minLength.", obj.ToString());
			}
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x00101CA0 File Offset: 0x000FFEA0
		private static void CompareMinMaxLength(uint cMin, uint cMax, XdrBuilder builder)
		{
			if (cMin != 4294967295U && cMax != 4294967295U && cMin > cMax)
			{
				builder.SendValidationEvent("The maxLength value must be equal to or greater than the minLength value.");
			}
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x00101CB9 File Offset: 0x000FFEB9
		private static bool ParseInteger(string str, ref uint n)
		{
			return uint.TryParse(str, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo, out n);
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x00101CC8 File Offset: 0x000FFEC8
		private void XDR_CheckAttributeDefault(XdrBuilder.DeclBaseInfo decl, SchemaAttDef pAttdef)
		{
			if ((decl._Default != null || pAttdef.DefaultValueTyped != null) && decl._Default != null)
			{
				pAttdef.DefaultValueRaw = (pAttdef.DefaultValueExpanded = (string)decl._Default);
				this.CheckDefaultAttValue(pAttdef);
			}
			this.SetAttributePresence(pAttdef, 1U == decl._MinOccurs);
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x00101D20 File Offset: 0x000FFF20
		private void SetAttributePresence(SchemaAttDef pAttdef, bool fRequired)
		{
			if (SchemaDeclBase.Use.Fixed != pAttdef.Presence)
			{
				if (fRequired || SchemaDeclBase.Use.Required == pAttdef.Presence)
				{
					if (pAttdef.DefaultValueTyped != null)
					{
						pAttdef.Presence = SchemaDeclBase.Use.Fixed;
						return;
					}
					pAttdef.Presence = SchemaDeclBase.Use.Required;
					return;
				}
				else
				{
					if (pAttdef.DefaultValueTyped != null)
					{
						pAttdef.Presence = SchemaDeclBase.Use.Default;
						return;
					}
					pAttdef.Presence = SchemaDeclBase.Use.Implied;
				}
			}
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x00101D74 File Offset: 0x000FFF74
		private int GetContent(XmlQualifiedName qname)
		{
			int num = 0;
			if (this._SchemaNames.TokenToQName[11].Equals(qname))
			{
				num = 1;
				this._ElementDef._AllowDataType = false;
			}
			else if (this._SchemaNames.TokenToQName[12].Equals(qname))
			{
				num = 4;
				this._ElementDef._AllowDataType = false;
			}
			else if (this._SchemaNames.TokenToQName[10].Equals(qname))
			{
				num = 3;
				this._ElementDef._AllowDataType = false;
			}
			else if (this._SchemaNames.TokenToQName[13].Equals(qname))
			{
				num = 2;
			}
			else
			{
				this.SendValidationEvent("The content attribute must have a value of 'textOnly', 'eltOnly', 'mixed', or 'empty', not '{0}'.", qname.Name);
			}
			return num;
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x00101E24 File Offset: 0x00100024
		private bool GetModel(XmlQualifiedName qname)
		{
			bool flag = false;
			if (this._SchemaNames.TokenToQName[7].Equals(qname))
			{
				flag = true;
			}
			else if (this._SchemaNames.TokenToQName[8].Equals(qname))
			{
				flag = false;
			}
			else
			{
				this.SendValidationEvent("The model attribute must have a value of open or closed, not '{0}'.", qname.Name);
			}
			return flag;
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x00101E78 File Offset: 0x00100078
		private XmlSchemaDatatype CheckDatatype(string str)
		{
			XmlSchemaDatatype xmlSchemaDatatype = XmlSchemaDatatype.FromXdrName(str);
			if (xmlSchemaDatatype == null)
			{
				this.SendValidationEvent("Reference to an unknown data type, '{0}'.", str);
			}
			else if (xmlSchemaDatatype.TokenizedType == XmlTokenizedType.ID && !this._AttributeDef._Global)
			{
				if (this._ElementDef._ElementDecl.IsIdDeclared)
				{
					this.SendValidationEvent("The attribute of type ID is already declared on the '{0}' element.", XmlQualifiedName.ToString(this._ElementDef._ElementDecl.Name.Name, this._ElementDef._ElementDecl.Prefix));
				}
				this._ElementDef._ElementDecl.IsIdDeclared = true;
			}
			return xmlSchemaDatatype;
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x00101F0C File Offset: 0x0010010C
		private void CheckDefaultAttValue(SchemaAttDef attDef)
		{
			XdrValidator.CheckDefaultValue(attDef.DefaultValueRaw.Trim(), attDef, this._SchemaInfo, this._CurNsMgr, this._NameTable, null, this.validationEventHandler, this._reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition);
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x00101F64 File Offset: 0x00100164
		private bool IsGlobal(int flags)
		{
			return flags == 256;
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x00101F6E File Offset: 0x0010016E
		private void SendValidationEvent(string code, string[] args, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, args, this._reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x00101F9F File Offset: 0x0010019F
		private void SendValidationEvent(string code)
		{
			this.SendValidationEvent(code, string.Empty);
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x00101FAD File Offset: 0x001001AD
		private void SendValidationEvent(string code, string msg)
		{
			this.SendValidationEvent(new XmlSchemaException(code, msg, this._reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition), XmlSeverityType.Error);
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x00101FE0 File Offset: 0x001001E0
		private void SendValidationEvent(XmlSchemaException e, XmlSeverityType severity)
		{
			SchemaInfo schemaInfo = this._SchemaInfo;
			int errorCount = schemaInfo.ErrorCount;
			schemaInfo.ErrorCount = errorCount + 1;
			if (this.validationEventHandler != null)
			{
				this.validationEventHandler(this, new ValidationEventArgs(e, severity));
				return;
			}
			if (severity == XmlSeverityType.Error)
			{
				throw e;
			}
		}

		// Token: 0x04001C77 RID: 7287
		private const int XdrSchema = 1;

		// Token: 0x04001C78 RID: 7288
		private const int XdrElementType = 2;

		// Token: 0x04001C79 RID: 7289
		private const int XdrAttributeType = 3;

		// Token: 0x04001C7A RID: 7290
		private const int XdrElement = 4;

		// Token: 0x04001C7B RID: 7291
		private const int XdrAttribute = 5;

		// Token: 0x04001C7C RID: 7292
		private const int XdrGroup = 6;

		// Token: 0x04001C7D RID: 7293
		private const int XdrElementDatatype = 7;

		// Token: 0x04001C7E RID: 7294
		private const int XdrAttributeDatatype = 8;

		// Token: 0x04001C7F RID: 7295
		private const int SchemaFlagsNs = 256;

		// Token: 0x04001C80 RID: 7296
		private const int StackIncrement = 10;

		// Token: 0x04001C81 RID: 7297
		private const int SchemaOrderNone = 0;

		// Token: 0x04001C82 RID: 7298
		private const int SchemaOrderMany = 1;

		// Token: 0x04001C83 RID: 7299
		private const int SchemaOrderSequence = 2;

		// Token: 0x04001C84 RID: 7300
		private const int SchemaOrderChoice = 3;

		// Token: 0x04001C85 RID: 7301
		private const int SchemaOrderAll = 4;

		// Token: 0x04001C86 RID: 7302
		private const int SchemaContentNone = 0;

		// Token: 0x04001C87 RID: 7303
		private const int SchemaContentEmpty = 1;

		// Token: 0x04001C88 RID: 7304
		private const int SchemaContentText = 2;

		// Token: 0x04001C89 RID: 7305
		private const int SchemaContentMixed = 3;

		// Token: 0x04001C8A RID: 7306
		private const int SchemaContentElement = 4;

		// Token: 0x04001C8B RID: 7307
		private static readonly int[] S_XDR_Root_Element = new int[] { 1 };

		// Token: 0x04001C8C RID: 7308
		private static readonly int[] S_XDR_Root_SubElements = new int[] { 2, 3 };

		// Token: 0x04001C8D RID: 7309
		private static readonly int[] S_XDR_ElementType_SubElements = new int[] { 4, 6, 3, 5, 7 };

		// Token: 0x04001C8E RID: 7310
		private static readonly int[] S_XDR_AttributeType_SubElements = new int[] { 8 };

		// Token: 0x04001C8F RID: 7311
		private static readonly int[] S_XDR_Group_SubElements = new int[] { 4, 6 };

		// Token: 0x04001C90 RID: 7312
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_Root_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaName, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildRoot_Name)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaId, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildRoot_ID))
		};

		// Token: 0x04001C91 RID: 7313
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_ElementType_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaName, XmlTokenizedType.QName, 256, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_Name)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaContent, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_Content)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaModel, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_Model)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaOrder, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_Order)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtType, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtType)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtValues, XmlTokenizedType.NMTOKENS, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtValues)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMaxLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtMaxLength)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMinLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtMinLength))
		};

		// Token: 0x04001C92 RID: 7314
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_AttributeType_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaName, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_Name)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaRequired, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_Required)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDefault, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_Default)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtType, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtType)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtValues, XmlTokenizedType.NMTOKENS, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtValues)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMaxLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtMaxLength)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMinLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtMinLength))
		};

		// Token: 0x04001C93 RID: 7315
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_Element_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaType, XmlTokenizedType.QName, 256, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElement_Type)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaMinOccurs, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElement_MinOccurs)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElement_MaxOccurs))
		};

		// Token: 0x04001C94 RID: 7316
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_Attribute_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaType, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttribute_Type)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaRequired, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttribute_Required)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDefault, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttribute_Default))
		};

		// Token: 0x04001C95 RID: 7317
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_Group_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaOrder, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildGroup_Order)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaMinOccurs, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildGroup_MinOccurs)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildGroup_MaxOccurs))
		};

		// Token: 0x04001C96 RID: 7318
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_ElementDataType_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtType, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtType)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtValues, XmlTokenizedType.NMTOKENS, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtValues)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMaxLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtMaxLength)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMinLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtMinLength))
		};

		// Token: 0x04001C97 RID: 7319
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_AttributeDataType_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtType, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtType)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtValues, XmlTokenizedType.NMTOKENS, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtValues)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMaxLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtMaxLength)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMinLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtMinLength))
		};

		// Token: 0x04001C98 RID: 7320
		private static readonly XdrBuilder.XdrEntry[] S_SchemaEntries = new XdrBuilder.XdrEntry[]
		{
			new XdrBuilder.XdrEntry(SchemaNames.Token.Empty, XdrBuilder.S_XDR_Root_Element, null, null, null, null, false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrRoot, XdrBuilder.S_XDR_Root_SubElements, XdrBuilder.S_XDR_Root_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitRoot), new XdrBuilder.XdrBeginChildFunction(XdrBuilder.XDR_BeginRoot), new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndRoot), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrElementType, XdrBuilder.S_XDR_ElementType_SubElements, XdrBuilder.S_XDR_ElementType_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitElementType), new XdrBuilder.XdrBeginChildFunction(XdrBuilder.XDR_BeginElementType), new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndElementType), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrAttributeType, XdrBuilder.S_XDR_AttributeType_SubElements, XdrBuilder.S_XDR_AttributeType_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitAttributeType), new XdrBuilder.XdrBeginChildFunction(XdrBuilder.XDR_BeginAttributeType), new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndAttributeType), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrElement, null, XdrBuilder.S_XDR_Element_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitElement), null, new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndElement), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrAttribute, null, XdrBuilder.S_XDR_Attribute_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitAttribute), new XdrBuilder.XdrBeginChildFunction(XdrBuilder.XDR_BeginAttribute), new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndAttribute), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrGroup, XdrBuilder.S_XDR_Group_SubElements, XdrBuilder.S_XDR_Group_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitGroup), null, new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndGroup), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrDatatype, null, XdrBuilder.S_XDR_ElementDataType_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitElementDtType), null, new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndElementDtType), true),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrDatatype, null, XdrBuilder.S_XDR_AttributeDataType_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitAttributeDtType), null, new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndAttributeDtType), true)
		};

		// Token: 0x04001C99 RID: 7321
		private SchemaInfo _SchemaInfo;

		// Token: 0x04001C9A RID: 7322
		private string _TargetNamespace;

		// Token: 0x04001C9B RID: 7323
		private XmlReader _reader;

		// Token: 0x04001C9C RID: 7324
		private PositionInfo positionInfo;

		// Token: 0x04001C9D RID: 7325
		private ParticleContentValidator _contentValidator;

		// Token: 0x04001C9E RID: 7326
		private XdrBuilder.XdrEntry _CurState;

		// Token: 0x04001C9F RID: 7327
		private XdrBuilder.XdrEntry _NextState;

		// Token: 0x04001CA0 RID: 7328
		private HWStack _StateHistory;

		// Token: 0x04001CA1 RID: 7329
		private HWStack _GroupStack;

		// Token: 0x04001CA2 RID: 7330
		private string _XdrName;

		// Token: 0x04001CA3 RID: 7331
		private string _XdrPrefix;

		// Token: 0x04001CA4 RID: 7332
		private XdrBuilder.ElementContent _ElementDef;

		// Token: 0x04001CA5 RID: 7333
		private XdrBuilder.GroupContent _GroupDef;

		// Token: 0x04001CA6 RID: 7334
		private XdrBuilder.AttributeContent _AttributeDef;

		// Token: 0x04001CA7 RID: 7335
		private XdrBuilder.DeclBaseInfo _UndefinedAttributeTypes;

		// Token: 0x04001CA8 RID: 7336
		private XdrBuilder.DeclBaseInfo _BaseDecl;

		// Token: 0x04001CA9 RID: 7337
		private XmlNameTable _NameTable;

		// Token: 0x04001CAA RID: 7338
		private SchemaNames _SchemaNames;

		// Token: 0x04001CAB RID: 7339
		private XmlNamespaceManager _CurNsMgr;

		// Token: 0x04001CAC RID: 7340
		private string _Text;

		// Token: 0x04001CAD RID: 7341
		private ValidationEventHandler validationEventHandler;

		// Token: 0x04001CAE RID: 7342
		private Hashtable _UndeclaredElements = new Hashtable();

		// Token: 0x04001CAF RID: 7343
		private const string x_schema = "x-schema:";

		// Token: 0x04001CB0 RID: 7344
		private XmlResolver xmlResolver;

		// Token: 0x02000426 RID: 1062
		private sealed class DeclBaseInfo
		{
			// Token: 0x06002A24 RID: 10788 RVA: 0x001025A9 File Offset: 0x001007A9
			internal DeclBaseInfo()
			{
				this.Reset();
			}

			// Token: 0x06002A25 RID: 10789 RVA: 0x001025B8 File Offset: 0x001007B8
			internal void Reset()
			{
				this._Name = XmlQualifiedName.Empty;
				this._Prefix = null;
				this._TypeName = XmlQualifiedName.Empty;
				this._TypePrefix = null;
				this._Default = null;
				this._Revises = null;
				this._MaxOccurs = 1U;
				this._MinOccurs = 1U;
				this._Checking = false;
				this._ElementDecl = null;
				this._Next = null;
				this._Attdef = null;
			}

			// Token: 0x04001CB1 RID: 7345
			internal XmlQualifiedName _Name;

			// Token: 0x04001CB2 RID: 7346
			internal string _Prefix;

			// Token: 0x04001CB3 RID: 7347
			internal XmlQualifiedName _TypeName;

			// Token: 0x04001CB4 RID: 7348
			internal string _TypePrefix;

			// Token: 0x04001CB5 RID: 7349
			internal object _Default;

			// Token: 0x04001CB6 RID: 7350
			internal object _Revises;

			// Token: 0x04001CB7 RID: 7351
			internal uint _MaxOccurs;

			// Token: 0x04001CB8 RID: 7352
			internal uint _MinOccurs;

			// Token: 0x04001CB9 RID: 7353
			internal bool _Checking;

			// Token: 0x04001CBA RID: 7354
			internal SchemaElementDecl _ElementDecl;

			// Token: 0x04001CBB RID: 7355
			internal SchemaAttDef _Attdef;

			// Token: 0x04001CBC RID: 7356
			internal XdrBuilder.DeclBaseInfo _Next;
		}

		// Token: 0x02000427 RID: 1063
		private sealed class GroupContent
		{
			// Token: 0x06002A26 RID: 10790 RVA: 0x00102621 File Offset: 0x00100821
			internal static void Copy(XdrBuilder.GroupContent from, XdrBuilder.GroupContent to)
			{
				to._MinVal = from._MinVal;
				to._MaxVal = from._MaxVal;
				to._Order = from._Order;
			}

			// Token: 0x06002A27 RID: 10791 RVA: 0x00102648 File Offset: 0x00100848
			internal static XdrBuilder.GroupContent Copy(XdrBuilder.GroupContent other)
			{
				XdrBuilder.GroupContent groupContent = new XdrBuilder.GroupContent();
				XdrBuilder.GroupContent.Copy(other, groupContent);
				return groupContent;
			}

			// Token: 0x04001CBD RID: 7357
			internal uint _MinVal;

			// Token: 0x04001CBE RID: 7358
			internal uint _MaxVal;

			// Token: 0x04001CBF RID: 7359
			internal bool _HasMaxAttr;

			// Token: 0x04001CC0 RID: 7360
			internal bool _HasMinAttr;

			// Token: 0x04001CC1 RID: 7361
			internal int _Order;
		}

		// Token: 0x02000428 RID: 1064
		private sealed class ElementContent
		{
			// Token: 0x04001CC2 RID: 7362
			internal SchemaElementDecl _ElementDecl;

			// Token: 0x04001CC3 RID: 7363
			internal int _ContentAttr;

			// Token: 0x04001CC4 RID: 7364
			internal int _OrderAttr;

			// Token: 0x04001CC5 RID: 7365
			internal bool _MasterGroupRequired;

			// Token: 0x04001CC6 RID: 7366
			internal bool _ExistTerminal;

			// Token: 0x04001CC7 RID: 7367
			internal bool _AllowDataType;

			// Token: 0x04001CC8 RID: 7368
			internal bool _HasDataType;

			// Token: 0x04001CC9 RID: 7369
			internal bool _HasType;

			// Token: 0x04001CCA RID: 7370
			internal bool _EnumerationRequired;

			// Token: 0x04001CCB RID: 7371
			internal uint _MinVal;

			// Token: 0x04001CCC RID: 7372
			internal uint _MaxVal;

			// Token: 0x04001CCD RID: 7373
			internal uint _MaxLength;

			// Token: 0x04001CCE RID: 7374
			internal uint _MinLength;

			// Token: 0x04001CCF RID: 7375
			internal Hashtable _AttDefList;
		}

		// Token: 0x02000429 RID: 1065
		private sealed class AttributeContent
		{
			// Token: 0x04001CD0 RID: 7376
			internal SchemaAttDef _AttDef;

			// Token: 0x04001CD1 RID: 7377
			internal XmlQualifiedName _Name;

			// Token: 0x04001CD2 RID: 7378
			internal string _Prefix;

			// Token: 0x04001CD3 RID: 7379
			internal bool _Required;

			// Token: 0x04001CD4 RID: 7380
			internal uint _MinVal;

			// Token: 0x04001CD5 RID: 7381
			internal uint _MaxVal;

			// Token: 0x04001CD6 RID: 7382
			internal uint _MaxLength;

			// Token: 0x04001CD7 RID: 7383
			internal uint _MinLength;

			// Token: 0x04001CD8 RID: 7384
			internal bool _EnumerationRequired;

			// Token: 0x04001CD9 RID: 7385
			internal bool _HasDataType;

			// Token: 0x04001CDA RID: 7386
			internal bool _Global;

			// Token: 0x04001CDB RID: 7387
			internal object _Default;
		}

		// Token: 0x0200042A RID: 1066
		// (Invoke) Token: 0x06002A2C RID: 10796
		private delegate void XdrBuildFunction(XdrBuilder builder, object obj, string prefix);

		// Token: 0x0200042B RID: 1067
		// (Invoke) Token: 0x06002A30 RID: 10800
		private delegate void XdrInitFunction(XdrBuilder builder, object obj);

		// Token: 0x0200042C RID: 1068
		// (Invoke) Token: 0x06002A34 RID: 10804
		private delegate void XdrBeginChildFunction(XdrBuilder builder);

		// Token: 0x0200042D RID: 1069
		// (Invoke) Token: 0x06002A38 RID: 10808
		private delegate void XdrEndChildFunction(XdrBuilder builder);

		// Token: 0x0200042E RID: 1070
		private sealed class XdrAttributeEntry
		{
			// Token: 0x06002A3B RID: 10811 RVA: 0x00102663 File Offset: 0x00100863
			internal XdrAttributeEntry(SchemaNames.Token a, XmlTokenizedType ttype, XdrBuilder.XdrBuildFunction build)
			{
				this._Attribute = a;
				this._Datatype = XmlSchemaDatatype.FromXmlTokenizedType(ttype);
				this._SchemaFlags = 0;
				this._BuildFunc = build;
			}

			// Token: 0x06002A3C RID: 10812 RVA: 0x0010268C File Offset: 0x0010088C
			internal XdrAttributeEntry(SchemaNames.Token a, XmlTokenizedType ttype, int schemaFlags, XdrBuilder.XdrBuildFunction build)
			{
				this._Attribute = a;
				this._Datatype = XmlSchemaDatatype.FromXmlTokenizedType(ttype);
				this._SchemaFlags = schemaFlags;
				this._BuildFunc = build;
			}

			// Token: 0x04001CDC RID: 7388
			internal SchemaNames.Token _Attribute;

			// Token: 0x04001CDD RID: 7389
			internal int _SchemaFlags;

			// Token: 0x04001CDE RID: 7390
			internal XmlSchemaDatatype _Datatype;

			// Token: 0x04001CDF RID: 7391
			internal XdrBuilder.XdrBuildFunction _BuildFunc;
		}

		// Token: 0x0200042F RID: 1071
		private sealed class XdrEntry
		{
			// Token: 0x06002A3D RID: 10813 RVA: 0x001026B6 File Offset: 0x001008B6
			internal XdrEntry(SchemaNames.Token n, int[] states, XdrBuilder.XdrAttributeEntry[] attributes, XdrBuilder.XdrInitFunction init, XdrBuilder.XdrBeginChildFunction begin, XdrBuilder.XdrEndChildFunction end, bool fText)
			{
				this._Name = n;
				this._NextStates = states;
				this._Attributes = attributes;
				this._InitFunc = init;
				this._BeginChildFunc = begin;
				this._EndChildFunc = end;
				this._AllowText = fText;
			}

			// Token: 0x04001CE0 RID: 7392
			internal SchemaNames.Token _Name;

			// Token: 0x04001CE1 RID: 7393
			internal int[] _NextStates;

			// Token: 0x04001CE2 RID: 7394
			internal XdrBuilder.XdrAttributeEntry[] _Attributes;

			// Token: 0x04001CE3 RID: 7395
			internal XdrBuilder.XdrInitFunction _InitFunc;

			// Token: 0x04001CE4 RID: 7396
			internal XdrBuilder.XdrBeginChildFunction _BeginChildFunc;

			// Token: 0x04001CE5 RID: 7397
			internal XdrBuilder.XdrEndChildFunction _EndChildFunc;

			// Token: 0x04001CE6 RID: 7398
			internal bool _AllowText;
		}
	}
}
