using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020001ED RID: 493
	internal class XsdValidatingReader : XmlReader, IXmlSchemaInfo, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x060011A6 RID: 4518 RVA: 0x00067E38 File Offset: 0x00066038
		internal XsdValidatingReader(XmlReader reader, XmlResolver xmlResolver, XmlReaderSettings readerSettings, XmlSchemaObject partialValidationType)
		{
			this.coreReader = reader;
			this.coreReaderNSResolver = reader as IXmlNamespaceResolver;
			this.lineInfo = reader as IXmlLineInfo;
			this.coreReaderNameTable = this.coreReader.NameTable;
			if (this.coreReaderNSResolver == null)
			{
				this.nsManager = new XmlNamespaceManager(this.coreReaderNameTable);
				this.manageNamespaces = true;
			}
			this.thisNSResolver = this;
			this.xmlResolver = xmlResolver;
			this.processInlineSchema = (readerSettings.ValidationFlags & XmlSchemaValidationFlags.ProcessInlineSchema) > XmlSchemaValidationFlags.None;
			this.Init();
			this.SetupValidator(readerSettings, reader, partialValidationType);
			this.validationEvent = readerSettings.GetEventHandler();
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00067EE1 File Offset: 0x000660E1
		internal XsdValidatingReader(XmlReader reader, XmlResolver xmlResolver, XmlReaderSettings readerSettings)
			: this(reader, xmlResolver, readerSettings, null)
		{
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00067EF0 File Offset: 0x000660F0
		private void Init()
		{
			this.validationState = XsdValidatingReader.ValidatingReaderState.Init;
			this.defaultAttributes = new ArrayList();
			this.currentAttrIndex = -1;
			this.attributePSVINodes = new AttributePSVIInfo[8];
			this.valueGetter = new XmlValueGetter(this.GetStringValue);
			XsdValidatingReader.TypeOfString = typeof(string);
			this.xmlSchemaInfo = new XmlSchemaInfo();
			this.NsXmlNs = this.coreReaderNameTable.Add("http://www.w3.org/2000/xmlns/");
			this.NsXs = this.coreReaderNameTable.Add("http://www.w3.org/2001/XMLSchema");
			this.NsXsi = this.coreReaderNameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
			this.XsiType = this.coreReaderNameTable.Add("type");
			this.XsiNil = this.coreReaderNameTable.Add("nil");
			this.XsiSchemaLocation = this.coreReaderNameTable.Add("schemaLocation");
			this.XsiNoNamespaceSchemaLocation = this.coreReaderNameTable.Add("noNamespaceSchemaLocation");
			this.XsdSchema = this.coreReaderNameTable.Add("schema");
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00068000 File Offset: 0x00066200
		private void SetupValidator(XmlReaderSettings readerSettings, XmlReader reader, XmlSchemaObject partialValidationType)
		{
			this.validator = new XmlSchemaValidator(this.coreReaderNameTable, readerSettings.Schemas, this.thisNSResolver, readerSettings.ValidationFlags);
			this.validator.XmlResolver = this.xmlResolver;
			this.validator.SourceUri = XmlConvert.ToUri(reader.BaseURI);
			this.validator.ValidationEventSender = this;
			this.validator.ValidationEventHandler += readerSettings.GetEventHandler();
			this.validator.LineInfoProvider = this.lineInfo;
			if (this.validator.ProcessSchemaHints)
			{
				this.validator.SchemaSet.ReaderSettings.DtdProcessing = readerSettings.DtdProcessing;
			}
			this.validator.SetDtdSchemaInfo(reader.DtdInfo);
			if (partialValidationType != null)
			{
				this.validator.Initialize(partialValidationType);
				return;
			}
			this.validator.Initialize();
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x000680DC File Offset: 0x000662DC
		public override XmlReaderSettings Settings
		{
			get
			{
				XmlReaderSettings xmlReaderSettings = this.coreReader.Settings;
				if (xmlReaderSettings != null)
				{
					xmlReaderSettings = xmlReaderSettings.Clone();
				}
				if (xmlReaderSettings == null)
				{
					xmlReaderSettings = new XmlReaderSettings();
				}
				xmlReaderSettings.Schemas = this.validator.SchemaSet;
				xmlReaderSettings.ValidationType = ValidationType.Schema;
				xmlReaderSettings.ValidationFlags = this.validator.ValidationFlags;
				xmlReaderSettings.ReadOnly = true;
				return xmlReaderSettings;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x0006813C File Offset: 0x0006633C
		public override XmlNodeType NodeType
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.NodeType;
				}
				XmlNodeType nodeType = this.coreReader.NodeType;
				if (nodeType == XmlNodeType.Whitespace && (this.validator.CurrentContentType == XmlSchemaContentType.TextOnly || this.validator.CurrentContentType == XmlSchemaContentType.Mixed))
				{
					return XmlNodeType.SignificantWhitespace;
				}
				return nodeType;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x00068190 File Offset: 0x00066390
		public override string Name
		{
			get
			{
				if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute)
				{
					return this.coreReader.Name;
				}
				string defaultAttributePrefix = this.validator.GetDefaultAttributePrefix(this.cachedNode.Namespace);
				if (defaultAttributePrefix != null && defaultAttributePrefix.Length != 0)
				{
					return string.Concat(new string[] { defaultAttributePrefix + ":" + this.cachedNode.LocalName });
				}
				return this.cachedNode.LocalName;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x00068204 File Offset: 0x00066404
		public override string LocalName
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.LocalName;
				}
				return this.coreReader.LocalName;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x00068226 File Offset: 0x00066426
		public override string NamespaceURI
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.Namespace;
				}
				return this.coreReader.NamespaceURI;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x00068248 File Offset: 0x00066448
		public override string Prefix
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.Prefix;
				}
				return this.coreReader.Prefix;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0006826A File Offset: 0x0006646A
		public override bool HasValue
		{
			get
			{
				return this.validationState < XsdValidatingReader.ValidatingReaderState.None || this.coreReader.HasValue;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x00068282 File Offset: 0x00066482
		public override string Value
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.RawValue;
				}
				return this.coreReader.Value;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x000682A4 File Offset: 0x000664A4
		public override int Depth
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.Depth;
				}
				return this.coreReader.Depth;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x000682C6 File Offset: 0x000664C6
		public override string BaseURI
		{
			get
			{
				return this.coreReader.BaseURI;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x000682D3 File Offset: 0x000664D3
		public override bool IsEmptyElement
		{
			get
			{
				return this.coreReader.IsEmptyElement;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x000682E0 File Offset: 0x000664E0
		public override bool IsDefault
		{
			get
			{
				return this.validationState == XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute || this.coreReader.IsDefault;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x000682F8 File Offset: 0x000664F8
		public override char QuoteChar
		{
			get
			{
				return this.coreReader.QuoteChar;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x00068305 File Offset: 0x00066505
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.coreReader.XmlSpace;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x00068312 File Offset: 0x00066512
		public override string XmlLang
		{
			get
			{
				return this.coreReader.XmlLang;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x00002068 File Offset: 0x00000268
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x00068320 File Offset: 0x00066520
		public override Type ValueType
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						if (nodeType != XmlNodeType.EndElement)
						{
							goto IL_0062;
						}
					}
					else
					{
						if (this.attributePSVI != null && this.AttributeSchemaInfo.ContentType == XmlSchemaContentType.TextOnly)
						{
							return this.AttributeSchemaInfo.SchemaType.Datatype.ValueType;
						}
						goto IL_0062;
					}
				}
				if (this.xmlSchemaInfo.ContentType == XmlSchemaContentType.TextOnly)
				{
					return this.xmlSchemaInfo.SchemaType.Datatype.ValueType;
				}
				IL_0062:
				return XsdValidatingReader.TypeOfString;
			}
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00068396 File Offset: 0x00066596
		public override object ReadContentAsObject()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsObject");
			}
			return this.InternalReadContentAsObject(true);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x000683B8 File Offset: 0x000665B8
		public override bool ReadContentAsBoolean()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsBoolean");
			}
			object obj = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = ((this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType);
			bool flag;
			try
			{
				if (xmlSchemaType != null)
				{
					flag = xmlSchemaType.ValueConverter.ToBoolean(obj);
				}
				else
				{
					flag = XmlUntypedConverter.Untyped.ToBoolean(obj);
				}
			}
			catch (InvalidCastException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Boolean", ex, this);
			}
			catch (FormatException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Boolean", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Boolean", ex3, this);
			}
			return flag;
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00068484 File Offset: 0x00066684
		public override DateTime ReadContentAsDateTime()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsDateTime");
			}
			object obj = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = ((this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType);
			DateTime dateTime;
			try
			{
				if (xmlSchemaType != null)
				{
					dateTime = xmlSchemaType.ValueConverter.ToDateTime(obj);
				}
				else
				{
					dateTime = XmlUntypedConverter.Untyped.ToDateTime(obj);
				}
			}
			catch (InvalidCastException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "DateTime", ex, this);
			}
			catch (FormatException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "DateTime", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "DateTime", ex3, this);
			}
			return dateTime;
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00068550 File Offset: 0x00066750
		public override double ReadContentAsDouble()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsDouble");
			}
			object obj = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = ((this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType);
			double num;
			try
			{
				if (xmlSchemaType != null)
				{
					num = xmlSchemaType.ValueConverter.ToDouble(obj);
				}
				else
				{
					num = XmlUntypedConverter.Untyped.ToDouble(obj);
				}
			}
			catch (InvalidCastException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Double", ex, this);
			}
			catch (FormatException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Double", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Double", ex3, this);
			}
			return num;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0006861C File Offset: 0x0006681C
		public override float ReadContentAsFloat()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsFloat");
			}
			object obj = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = ((this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType);
			float num;
			try
			{
				if (xmlSchemaType != null)
				{
					num = xmlSchemaType.ValueConverter.ToSingle(obj);
				}
				else
				{
					num = XmlUntypedConverter.Untyped.ToSingle(obj);
				}
			}
			catch (InvalidCastException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Float", ex, this);
			}
			catch (FormatException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Float", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Float", ex3, this);
			}
			return num;
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x000686E8 File Offset: 0x000668E8
		public override decimal ReadContentAsDecimal()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsDecimal");
			}
			object obj = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = ((this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType);
			decimal num;
			try
			{
				if (xmlSchemaType != null)
				{
					num = xmlSchemaType.ValueConverter.ToDecimal(obj);
				}
				else
				{
					num = XmlUntypedConverter.Untyped.ToDecimal(obj);
				}
			}
			catch (InvalidCastException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Decimal", ex, this);
			}
			catch (FormatException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Decimal", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Decimal", ex3, this);
			}
			return num;
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x000687B4 File Offset: 0x000669B4
		public override int ReadContentAsInt()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsInt");
			}
			object obj = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = ((this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType);
			int num;
			try
			{
				if (xmlSchemaType != null)
				{
					num = xmlSchemaType.ValueConverter.ToInt32(obj);
				}
				else
				{
					num = XmlUntypedConverter.Untyped.ToInt32(obj);
				}
			}
			catch (InvalidCastException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Int", ex, this);
			}
			catch (FormatException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Int", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Int", ex3, this);
			}
			return num;
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00068880 File Offset: 0x00066A80
		public override long ReadContentAsLong()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsLong");
			}
			object obj = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = ((this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType);
			long num;
			try
			{
				if (xmlSchemaType != null)
				{
					num = xmlSchemaType.ValueConverter.ToInt64(obj);
				}
				else
				{
					num = XmlUntypedConverter.Untyped.ToInt64(obj);
				}
			}
			catch (InvalidCastException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Long", ex, this);
			}
			catch (FormatException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Long", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Long", ex3, this);
			}
			return num;
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0006894C File Offset: 0x00066B4C
		public override string ReadContentAsString()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsString");
			}
			object obj = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = ((this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType);
			string text;
			try
			{
				if (xmlSchemaType != null)
				{
					text = xmlSchemaType.ValueConverter.ToString(obj);
				}
				else
				{
					text = obj as string;
				}
			}
			catch (InvalidCastException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", ex, this);
			}
			catch (FormatException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", ex3, this);
			}
			return text;
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00068A14 File Offset: 0x00066C14
		public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAs");
			}
			string text;
			object obj = this.InternalReadContentAsObject(false, out text);
			XmlSchemaType xmlSchemaType = ((this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType);
			object obj2;
			try
			{
				if (xmlSchemaType != null)
				{
					if (returnType == typeof(DateTimeOffset) && xmlSchemaType.Datatype is Datatype_dateTimeBase)
					{
						obj = text;
					}
					obj2 = xmlSchemaType.ValueConverter.ChangeType(obj, returnType);
				}
				else
				{
					obj2 = XmlUntypedConverter.Untyped.ChangeType(obj, returnType, namespaceResolver);
				}
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), ex, this);
			}
			catch (InvalidCastException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), ex3, this);
			}
			return obj2;
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00068B0C File Offset: 0x00066D0C
		public override object ReadElementContentAsObject()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsObject");
			}
			XmlSchemaType xmlSchemaType;
			return this.InternalReadElementContentAsObject(out xmlSchemaType, true);
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00068B38 File Offset: 0x00066D38
		public override bool ReadElementContentAsBoolean()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsBoolean");
			}
			XmlSchemaType xmlSchemaType;
			object obj = this.InternalReadElementContentAsObject(out xmlSchemaType);
			bool flag;
			try
			{
				if (xmlSchemaType != null)
				{
					flag = xmlSchemaType.ValueConverter.ToBoolean(obj);
				}
				else
				{
					flag = XmlUntypedConverter.Untyped.ToBoolean(obj);
				}
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Boolean", ex, this);
			}
			catch (InvalidCastException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Boolean", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Boolean", ex3, this);
			}
			return flag;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00068BEC File Offset: 0x00066DEC
		public override DateTime ReadElementContentAsDateTime()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsDateTime");
			}
			XmlSchemaType xmlSchemaType;
			object obj = this.InternalReadElementContentAsObject(out xmlSchemaType);
			DateTime dateTime;
			try
			{
				if (xmlSchemaType != null)
				{
					dateTime = xmlSchemaType.ValueConverter.ToDateTime(obj);
				}
				else
				{
					dateTime = XmlUntypedConverter.Untyped.ToDateTime(obj);
				}
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "DateTime", ex, this);
			}
			catch (InvalidCastException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "DateTime", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "DateTime", ex3, this);
			}
			return dateTime;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00068CA0 File Offset: 0x00066EA0
		public override double ReadElementContentAsDouble()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsDouble");
			}
			XmlSchemaType xmlSchemaType;
			object obj = this.InternalReadElementContentAsObject(out xmlSchemaType);
			double num;
			try
			{
				if (xmlSchemaType != null)
				{
					num = xmlSchemaType.ValueConverter.ToDouble(obj);
				}
				else
				{
					num = XmlUntypedConverter.Untyped.ToDouble(obj);
				}
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Double", ex, this);
			}
			catch (InvalidCastException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Double", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Double", ex3, this);
			}
			return num;
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00068D54 File Offset: 0x00066F54
		public override float ReadElementContentAsFloat()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsFloat");
			}
			XmlSchemaType xmlSchemaType;
			object obj = this.InternalReadElementContentAsObject(out xmlSchemaType);
			float num;
			try
			{
				if (xmlSchemaType != null)
				{
					num = xmlSchemaType.ValueConverter.ToSingle(obj);
				}
				else
				{
					num = XmlUntypedConverter.Untyped.ToSingle(obj);
				}
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Float", ex, this);
			}
			catch (InvalidCastException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Float", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Float", ex3, this);
			}
			return num;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00068E08 File Offset: 0x00067008
		public override decimal ReadElementContentAsDecimal()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsDecimal");
			}
			XmlSchemaType xmlSchemaType;
			object obj = this.InternalReadElementContentAsObject(out xmlSchemaType);
			decimal num;
			try
			{
				if (xmlSchemaType != null)
				{
					num = xmlSchemaType.ValueConverter.ToDecimal(obj);
				}
				else
				{
					num = XmlUntypedConverter.Untyped.ToDecimal(obj);
				}
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Decimal", ex, this);
			}
			catch (InvalidCastException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Decimal", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Decimal", ex3, this);
			}
			return num;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00068EBC File Offset: 0x000670BC
		public override int ReadElementContentAsInt()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsInt");
			}
			XmlSchemaType xmlSchemaType;
			object obj = this.InternalReadElementContentAsObject(out xmlSchemaType);
			int num;
			try
			{
				if (xmlSchemaType != null)
				{
					num = xmlSchemaType.ValueConverter.ToInt32(obj);
				}
				else
				{
					num = XmlUntypedConverter.Untyped.ToInt32(obj);
				}
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Int", ex, this);
			}
			catch (InvalidCastException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Int", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Int", ex3, this);
			}
			return num;
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00068F70 File Offset: 0x00067170
		public override long ReadElementContentAsLong()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsLong");
			}
			XmlSchemaType xmlSchemaType;
			object obj = this.InternalReadElementContentAsObject(out xmlSchemaType);
			long num;
			try
			{
				if (xmlSchemaType != null)
				{
					num = xmlSchemaType.ValueConverter.ToInt64(obj);
				}
				else
				{
					num = XmlUntypedConverter.Untyped.ToInt64(obj);
				}
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Long", ex, this);
			}
			catch (InvalidCastException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Long", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Long", ex3, this);
			}
			return num;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00069024 File Offset: 0x00067224
		public override string ReadElementContentAsString()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsString");
			}
			XmlSchemaType xmlSchemaType;
			object obj = this.InternalReadElementContentAsObject(out xmlSchemaType);
			string text;
			try
			{
				if (xmlSchemaType != null)
				{
					text = xmlSchemaType.ValueConverter.ToString(obj);
				}
				else
				{
					text = obj as string;
				}
			}
			catch (InvalidCastException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", ex, this);
			}
			catch (FormatException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", ex3, this);
			}
			return text;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x000690D0 File Offset: 0x000672D0
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAs");
			}
			XmlSchemaType xmlSchemaType;
			string text;
			object obj = this.InternalReadElementContentAsObject(out xmlSchemaType, false, out text);
			object obj2;
			try
			{
				if (xmlSchemaType != null)
				{
					if (returnType == typeof(DateTimeOffset) && xmlSchemaType.Datatype is Datatype_dateTimeBase)
					{
						obj = text;
					}
					obj2 = xmlSchemaType.ValueConverter.ChangeType(obj, returnType, namespaceResolver);
				}
				else
				{
					obj2 = XmlUntypedConverter.Untyped.ChangeType(obj, returnType, namespaceResolver);
				}
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), ex, this);
			}
			catch (InvalidCastException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), ex3, this);
			}
			return obj2;
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x000691B0 File Offset: 0x000673B0
		public override int AttributeCount
		{
			get
			{
				return this.attributeCount;
			}
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x000691B8 File Offset: 0x000673B8
		public override string GetAttribute(string name)
		{
			string text = this.coreReader.GetAttribute(name);
			if (text == null && this.attributeCount > 0)
			{
				ValidatingReaderNodeData defaultAttribute = this.GetDefaultAttribute(name, false);
				if (defaultAttribute != null)
				{
					text = defaultAttribute.RawValue;
				}
			}
			return text;
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x000691F4 File Offset: 0x000673F4
		public override string GetAttribute(string name, string namespaceURI)
		{
			string attribute = this.coreReader.GetAttribute(name, namespaceURI);
			if (attribute == null && this.attributeCount > 0)
			{
				namespaceURI = ((namespaceURI == null) ? string.Empty : this.coreReaderNameTable.Get(namespaceURI));
				name = this.coreReaderNameTable.Get(name);
				if (name == null || namespaceURI == null)
				{
					return null;
				}
				ValidatingReaderNodeData defaultAttribute = this.GetDefaultAttribute(name, namespaceURI, false);
				if (defaultAttribute != null)
				{
					return defaultAttribute.RawValue;
				}
			}
			return attribute;
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00069260 File Offset: 0x00067460
		public override string GetAttribute(int i)
		{
			if (this.attributeCount == 0)
			{
				return null;
			}
			if (i < this.coreReaderAttributeCount)
			{
				return this.coreReader.GetAttribute(i);
			}
			int num = i - this.coreReaderAttributeCount;
			return ((ValidatingReaderNodeData)this.defaultAttributes[num]).RawValue;
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x000692AC File Offset: 0x000674AC
		public override bool MoveToAttribute(string name)
		{
			if (!this.coreReader.MoveToAttribute(name))
			{
				if (this.attributeCount > 0)
				{
					ValidatingReaderNodeData defaultAttribute = this.GetDefaultAttribute(name, true);
					if (defaultAttribute != null)
					{
						this.validationState = XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute;
						this.attributePSVI = defaultAttribute.AttInfo;
						this.cachedNode = defaultAttribute;
						goto IL_0057;
					}
				}
				return false;
			}
			this.validationState = XsdValidatingReader.ValidatingReaderState.OnAttribute;
			this.attributePSVI = this.GetAttributePSVI(name);
			IL_0057:
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
			return true;
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00069334 File Offset: 0x00067534
		public override bool MoveToAttribute(string name, string ns)
		{
			name = this.coreReaderNameTable.Get(name);
			ns = ((ns != null) ? this.coreReaderNameTable.Get(ns) : string.Empty);
			if (name == null || ns == null)
			{
				return false;
			}
			if (this.coreReader.MoveToAttribute(name, ns))
			{
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnAttribute;
				if (this.inlineSchemaParser == null)
				{
					this.attributePSVI = this.GetAttributePSVI(name, ns);
				}
				else
				{
					this.attributePSVI = null;
				}
			}
			else
			{
				ValidatingReaderNodeData defaultAttribute = this.GetDefaultAttribute(name, ns, true);
				if (defaultAttribute == null)
				{
					return false;
				}
				this.attributePSVI = defaultAttribute.AttInfo;
				this.cachedNode = defaultAttribute;
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute;
			}
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
			return true;
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x000693F4 File Offset: 0x000675F4
		public override void MoveToAttribute(int i)
		{
			if (i < 0 || i >= this.attributeCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			this.currentAttrIndex = i;
			if (i < this.coreReaderAttributeCount)
			{
				this.coreReader.MoveToAttribute(i);
				if (this.inlineSchemaParser == null)
				{
					this.attributePSVI = this.attributePSVINodes[i];
				}
				else
				{
					this.attributePSVI = null;
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnAttribute;
			}
			else
			{
				int num = i - this.coreReaderAttributeCount;
				this.cachedNode = (ValidatingReaderNodeData)this.defaultAttributes[num];
				this.attributePSVI = this.cachedNode.AttInfo;
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute;
			}
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x000694B8 File Offset: 0x000676B8
		public override bool MoveToFirstAttribute()
		{
			if (this.coreReader.MoveToFirstAttribute())
			{
				this.currentAttrIndex = 0;
				if (this.inlineSchemaParser == null)
				{
					this.attributePSVI = this.attributePSVINodes[0];
				}
				else
				{
					this.attributePSVI = null;
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnAttribute;
			}
			else
			{
				if (this.defaultAttributes.Count <= 0)
				{
					return false;
				}
				this.cachedNode = (ValidatingReaderNodeData)this.defaultAttributes[0];
				this.attributePSVI = this.cachedNode.AttInfo;
				this.currentAttrIndex = 0;
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute;
			}
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
			return true;
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0006956C File Offset: 0x0006776C
		public override bool MoveToNextAttribute()
		{
			if (this.currentAttrIndex + 1 < this.coreReaderAttributeCount)
			{
				this.coreReader.MoveToNextAttribute();
				this.currentAttrIndex++;
				if (this.inlineSchemaParser == null)
				{
					this.attributePSVI = this.attributePSVINodes[this.currentAttrIndex];
				}
				else
				{
					this.attributePSVI = null;
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnAttribute;
			}
			else
			{
				if (this.currentAttrIndex + 1 >= this.attributeCount)
				{
					return false;
				}
				int num = this.currentAttrIndex + 1;
				this.currentAttrIndex = num;
				int num2 = num - this.coreReaderAttributeCount;
				this.cachedNode = (ValidatingReaderNodeData)this.defaultAttributes[num2];
				this.attributePSVI = this.cachedNode.AttInfo;
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute;
			}
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
			return true;
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0006964D File Offset: 0x0006784D
		public override bool MoveToElement()
		{
			if (this.coreReader.MoveToElement() || this.validationState < XsdValidatingReader.ValidatingReaderState.None)
			{
				this.currentAttrIndex = -1;
				this.validationState = XsdValidatingReader.ValidatingReaderState.ClearAttributes;
				return true;
			}
			return false;
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00069678 File Offset: 0x00067878
		public override bool Read()
		{
			switch (this.validationState)
			{
			case XsdValidatingReader.ValidatingReaderState.OnReadAttributeValue:
			case XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute:
			case XsdValidatingReader.ValidatingReaderState.OnAttribute:
			case XsdValidatingReader.ValidatingReaderState.ClearAttributes:
				this.ClearAttributesInfo();
				if (this.inlineSchemaParser != null)
				{
					this.validationState = XsdValidatingReader.ValidatingReaderState.ParseInlineSchema;
					goto IL_007C;
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				break;
			case XsdValidatingReader.ValidatingReaderState.None:
				return false;
			case XsdValidatingReader.ValidatingReaderState.Init:
				this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				if (this.coreReader.ReadState == ReadState.Interactive)
				{
					this.ProcessReaderEvent();
					return true;
				}
				break;
			case XsdValidatingReader.ValidatingReaderState.Read:
				break;
			case XsdValidatingReader.ValidatingReaderState.ParseInlineSchema:
				goto IL_007C;
			case XsdValidatingReader.ValidatingReaderState.ReadAhead:
				this.ClearAttributesInfo();
				this.ProcessReaderEvent();
				this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				return true;
			case XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent:
				this.validationState = this.savedState;
				this.readBinaryHelper.Finish();
				return this.Read();
			case XsdValidatingReader.ValidatingReaderState.ReaderClosed:
			case XsdValidatingReader.ValidatingReaderState.EOF:
				return false;
			default:
				return false;
			}
			if (this.coreReader.Read())
			{
				this.ProcessReaderEvent();
				return true;
			}
			this.validator.EndValidation();
			if (this.coreReader.EOF)
			{
				this.validationState = XsdValidatingReader.ValidatingReaderState.EOF;
			}
			return false;
			IL_007C:
			this.ProcessInlineSchema();
			return true;
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x0006977F File Offset: 0x0006797F
		public override bool EOF
		{
			get
			{
				return this.coreReader.EOF;
			}
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0006978C File Offset: 0x0006798C
		public override void Close()
		{
			this.coreReader.Close();
			this.validationState = XsdValidatingReader.ValidatingReaderState.ReaderClosed;
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x000697A0 File Offset: 0x000679A0
		public override ReadState ReadState
		{
			get
			{
				if (this.validationState != XsdValidatingReader.ValidatingReaderState.Init)
				{
					return this.coreReader.ReadState;
				}
				return ReadState.Initial;
			}
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x000697B8 File Offset: 0x000679B8
		public override void Skip()
		{
			int depth = this.Depth;
			XmlNodeType nodeType = this.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Attribute)
				{
					goto IL_0081;
				}
				this.MoveToElement();
			}
			if (!this.coreReader.IsEmptyElement)
			{
				bool flag = true;
				if ((this.xmlSchemaInfo.IsUnionType || this.xmlSchemaInfo.IsDefault) && this.coreReader is XsdCachingReader)
				{
					flag = false;
				}
				this.coreReader.Skip();
				this.validationState = XsdValidatingReader.ValidatingReaderState.ReadAhead;
				if (flag)
				{
					this.validator.SkipToEndElement(this.xmlSchemaInfo);
				}
			}
			IL_0081:
			this.Read();
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x0006984D File Offset: 0x00067A4D
		public override XmlNameTable NameTable
		{
			get
			{
				return this.coreReaderNameTable;
			}
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00069855 File Offset: 0x00067A55
		public override string LookupNamespace(string prefix)
		{
			return this.thisNSResolver.LookupNamespace(prefix);
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00007944 File Offset: 0x00005B44
		public override void ResolveEntity()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x00069864 File Offset: 0x00067A64
		public override bool ReadAttributeValue()
		{
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
			if (this.NodeType != XmlNodeType.Attribute)
			{
				return false;
			}
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute)
			{
				this.cachedNode = this.CreateDummyTextNode(this.cachedNode.RawValue, this.cachedNode.Depth + 1);
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadAttributeValue;
				return true;
			}
			return this.coreReader.ReadAttributeValue();
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x00003242 File Offset: 0x00001442
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x000698E0 File Offset: 0x00067AE0
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.validationState;
			}
			this.validationState = this.savedState;
			int num = this.readBinaryHelper.ReadContentAsBase64(buffer, index, count);
			this.savedState = this.validationState;
			this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
			return num;
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0006994C File Offset: 0x00067B4C
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.validationState;
			}
			this.validationState = this.savedState;
			int num = this.readBinaryHelper.ReadContentAsBinHex(buffer, index, count);
			this.savedState = this.validationState;
			this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
			return num;
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x000699B8 File Offset: 0x00067BB8
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.validationState;
			}
			this.validationState = this.savedState;
			int num = this.readBinaryHelper.ReadElementContentAsBase64(buffer, index, count);
			this.savedState = this.validationState;
			this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
			return num;
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00069A24 File Offset: 0x00067C24
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.validationState;
			}
			this.validationState = this.savedState;
			int num = this.readBinaryHelper.ReadElementContentAsBinHex(buffer, index, count);
			this.savedState = this.validationState;
			this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
			return num;
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x00069A90 File Offset: 0x00067C90
		bool IXmlSchemaInfo.IsDefault
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							return this.xmlSchemaInfo.IsDefault;
						}
					}
					else if (this.attributePSVI != null)
					{
						return this.AttributeSchemaInfo.IsDefault;
					}
					return false;
				}
				if (!this.coreReader.IsEmptyElement)
				{
					this.GetIsDefault();
				}
				return this.xmlSchemaInfo.IsDefault;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x00069AF4 File Offset: 0x00067CF4
		bool IXmlSchemaInfo.IsNil
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				return (nodeType == XmlNodeType.Element || nodeType == XmlNodeType.EndElement) && this.xmlSchemaInfo.IsNil;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x00069B20 File Offset: 0x00067D20
		XmlSchemaValidity IXmlSchemaInfo.Validity
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							return this.xmlSchemaInfo.Validity;
						}
					}
					else if (this.attributePSVI != null)
					{
						return this.AttributeSchemaInfo.Validity;
					}
					return XmlSchemaValidity.NotKnown;
				}
				if (this.coreReader.IsEmptyElement)
				{
					return this.xmlSchemaInfo.Validity;
				}
				if (this.xmlSchemaInfo.Validity == XmlSchemaValidity.Valid)
				{
					return XmlSchemaValidity.NotKnown;
				}
				return this.xmlSchemaInfo.Validity;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x00069B9C File Offset: 0x00067D9C
		XmlSchemaSimpleType IXmlSchemaInfo.MemberType
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType == XmlNodeType.Element)
				{
					if (!this.coreReader.IsEmptyElement)
					{
						this.GetMemberType();
					}
					return this.xmlSchemaInfo.MemberType;
				}
				if (nodeType != XmlNodeType.Attribute)
				{
					if (nodeType != XmlNodeType.EndElement)
					{
						return null;
					}
					return this.xmlSchemaInfo.MemberType;
				}
				else
				{
					if (this.attributePSVI != null)
					{
						return this.AttributeSchemaInfo.MemberType;
					}
					return null;
				}
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x00069C04 File Offset: 0x00067E04
		XmlSchemaType IXmlSchemaInfo.SchemaType
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						if (nodeType != XmlNodeType.EndElement)
						{
							return null;
						}
					}
					else
					{
						if (this.attributePSVI != null)
						{
							return this.AttributeSchemaInfo.SchemaType;
						}
						return null;
					}
				}
				return this.xmlSchemaInfo.SchemaType;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x00069C48 File Offset: 0x00067E48
		XmlSchemaElement IXmlSchemaInfo.SchemaElement
		{
			get
			{
				if (this.NodeType == XmlNodeType.Element || this.NodeType == XmlNodeType.EndElement)
				{
					return this.xmlSchemaInfo.SchemaElement;
				}
				return null;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x00069C6A File Offset: 0x00067E6A
		XmlSchemaAttribute IXmlSchemaInfo.SchemaAttribute
		{
			get
			{
				if (this.NodeType == XmlNodeType.Attribute && this.attributePSVI != null)
				{
					return this.AttributeSchemaInfo.SchemaAttribute;
				}
				return null;
			}
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x00003242 File Offset: 0x00001442
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x00069C8A File Offset: 0x00067E8A
		public int LineNumber
		{
			get
			{
				if (this.lineInfo != null)
				{
					return this.lineInfo.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x00069CA1 File Offset: 0x00067EA1
		public int LinePosition
		{
			get
			{
				if (this.lineInfo != null)
				{
					return this.lineInfo.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00069CB8 File Offset: 0x00067EB8
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			if (this.coreReaderNSResolver != null)
			{
				return this.coreReaderNSResolver.GetNamespacesInScope(scope);
			}
			return this.nsManager.GetNamespacesInScope(scope);
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x00069CDB File Offset: 0x00067EDB
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			if (this.coreReaderNSResolver != null)
			{
				return this.coreReaderNSResolver.LookupNamespace(prefix);
			}
			return this.nsManager.LookupNamespace(prefix);
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00069CFE File Offset: 0x00067EFE
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			if (this.coreReaderNSResolver != null)
			{
				return this.coreReaderNSResolver.LookupPrefix(namespaceName);
			}
			return this.nsManager.LookupPrefix(namespaceName);
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00069D21 File Offset: 0x00067F21
		private object GetStringValue()
		{
			return this.coreReader.Value;
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x00069D2E File Offset: 0x00067F2E
		private XmlSchemaType ElementXmlType
		{
			get
			{
				return this.xmlSchemaInfo.XmlType;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x00069D3B File Offset: 0x00067F3B
		private XmlSchemaType AttributeXmlType
		{
			get
			{
				if (this.attributePSVI != null)
				{
					return this.AttributeSchemaInfo.XmlType;
				}
				return null;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00069D52 File Offset: 0x00067F52
		private XmlSchemaInfo AttributeSchemaInfo
		{
			get
			{
				return this.attributePSVI.attributeSchemaInfo;
			}
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x00069D60 File Offset: 0x00067F60
		private void ProcessReaderEvent()
		{
			if (this.replayCache)
			{
				return;
			}
			switch (this.coreReader.NodeType)
			{
			case XmlNodeType.Element:
				this.ProcessElementEvent();
				return;
			case XmlNodeType.Attribute:
			case XmlNodeType.Entity:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Comment:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
			case XmlNodeType.Notation:
				break;
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
				this.validator.ValidateText(new XmlValueGetter(this.GetStringValue));
				return;
			case XmlNodeType.EntityReference:
				throw new InvalidOperationException();
			case XmlNodeType.DocumentType:
				this.validator.SetDtdSchemaInfo(this.coreReader.DtdInfo);
				break;
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				this.validator.ValidateWhitespace(new XmlValueGetter(this.GetStringValue));
				return;
			case XmlNodeType.EndElement:
				this.ProcessEndElementEvent();
				return;
			default:
				return;
			}
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x00069E24 File Offset: 0x00068024
		private void ProcessElementEvent()
		{
			if (!this.processInlineSchema || !this.IsXSDRoot(this.coreReader.LocalName, this.coreReader.NamespaceURI) || this.coreReader.Depth <= 0)
			{
				this.atomicValue = null;
				this.originalAtomicValueString = null;
				this.xmlSchemaInfo.Clear();
				if (this.manageNamespaces)
				{
					this.nsManager.PushScope();
				}
				string text = null;
				string text2 = null;
				string text3 = null;
				string text4 = null;
				if (this.coreReader.MoveToFirstAttribute())
				{
					do
					{
						string namespaceURI = this.coreReader.NamespaceURI;
						string localName = this.coreReader.LocalName;
						if (Ref.Equal(namespaceURI, this.NsXsi))
						{
							if (Ref.Equal(localName, this.XsiSchemaLocation))
							{
								text = this.coreReader.Value;
							}
							else if (Ref.Equal(localName, this.XsiNoNamespaceSchemaLocation))
							{
								text2 = this.coreReader.Value;
							}
							else if (Ref.Equal(localName, this.XsiType))
							{
								text4 = this.coreReader.Value;
							}
							else if (Ref.Equal(localName, this.XsiNil))
							{
								text3 = this.coreReader.Value;
							}
						}
						if (this.manageNamespaces && Ref.Equal(this.coreReader.NamespaceURI, this.NsXmlNs))
						{
							this.nsManager.AddNamespace((this.coreReader.Prefix.Length == 0) ? string.Empty : this.coreReader.LocalName, this.coreReader.Value);
						}
					}
					while (this.coreReader.MoveToNextAttribute());
					this.coreReader.MoveToElement();
				}
				this.validator.ValidateElement(this.coreReader.LocalName, this.coreReader.NamespaceURI, this.xmlSchemaInfo, text4, text3, text, text2);
				this.ValidateAttributes();
				this.validator.ValidateEndOfAttributes(this.xmlSchemaInfo);
				if (this.coreReader.IsEmptyElement)
				{
					this.ProcessEndElementEvent();
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.ClearAttributes;
				return;
			}
			this.xmlSchemaInfo.Clear();
			this.attributeCount = (this.coreReaderAttributeCount = this.coreReader.AttributeCount);
			if (!this.coreReader.IsEmptyElement)
			{
				this.inlineSchemaParser = new Parser(SchemaType.XSD, this.coreReaderNameTable, this.validator.SchemaSet.GetSchemaNames(this.coreReaderNameTable), this.validationEvent);
				this.inlineSchemaParser.StartParsing(this.coreReader, null);
				this.inlineSchemaParser.ParseReaderNode();
				this.validationState = XsdValidatingReader.ValidatingReaderState.ParseInlineSchema;
				return;
			}
			this.validationState = XsdValidatingReader.ValidatingReaderState.ClearAttributes;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0006A0AC File Offset: 0x000682AC
		private void ProcessEndElementEvent()
		{
			this.atomicValue = this.validator.ValidateEndElement(this.xmlSchemaInfo);
			this.originalAtomicValueString = this.GetOriginalAtomicValueStringOfElement();
			if (this.xmlSchemaInfo.IsDefault)
			{
				int depth = this.coreReader.Depth;
				this.coreReader = this.GetCachingReader();
				this.cachingReader.RecordTextNode(this.xmlSchemaInfo.XmlType.ValueConverter.ToString(this.atomicValue), this.originalAtomicValueString, depth + 1, 0, 0);
				this.cachingReader.RecordEndElementNode();
				this.cachingReader.SetToReplayMode();
				this.replayCache = true;
				return;
			}
			if (this.manageNamespaces)
			{
				this.nsManager.PopScope();
			}
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x0006A168 File Offset: 0x00068368
		private void ValidateAttributes()
		{
			this.attributeCount = (this.coreReaderAttributeCount = this.coreReader.AttributeCount);
			int num = 0;
			bool flag = false;
			if (this.coreReader.MoveToFirstAttribute())
			{
				do
				{
					string localName = this.coreReader.LocalName;
					string namespaceURI = this.coreReader.NamespaceURI;
					AttributePSVIInfo attributePSVIInfo = this.AddAttributePSVI(num);
					attributePSVIInfo.localName = localName;
					attributePSVIInfo.namespaceUri = namespaceURI;
					if (namespaceURI == this.NsXmlNs)
					{
						num++;
					}
					else
					{
						attributePSVIInfo.typedAttributeValue = this.validator.ValidateAttribute(localName, namespaceURI, this.valueGetter, attributePSVIInfo.attributeSchemaInfo);
						if (!flag)
						{
							flag = attributePSVIInfo.attributeSchemaInfo.Validity == XmlSchemaValidity.Invalid;
						}
						num++;
					}
				}
				while (this.coreReader.MoveToNextAttribute());
			}
			this.coreReader.MoveToElement();
			if (flag)
			{
				this.xmlSchemaInfo.Validity = XmlSchemaValidity.Invalid;
			}
			this.validator.GetUnspecifiedDefaultAttributes(this.defaultAttributes, true);
			this.attributeCount += this.defaultAttributes.Count;
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x0006A271 File Offset: 0x00068471
		private void ClearAttributesInfo()
		{
			this.attributeCount = 0;
			this.coreReaderAttributeCount = 0;
			this.currentAttrIndex = -1;
			this.defaultAttributes.Clear();
			this.attributePSVI = null;
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0006A29C File Offset: 0x0006849C
		private AttributePSVIInfo GetAttributePSVI(string name)
		{
			if (this.inlineSchemaParser != null)
			{
				return null;
			}
			string text;
			string text2;
			ValidateNames.SplitQName(name, out text, out text2);
			text = this.coreReaderNameTable.Add(text);
			text2 = this.coreReaderNameTable.Add(text2);
			string text3;
			if (text.Length == 0)
			{
				text3 = string.Empty;
			}
			else
			{
				text3 = this.thisNSResolver.LookupNamespace(text);
			}
			return this.GetAttributePSVI(text2, text3);
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0006A2FC File Offset: 0x000684FC
		private AttributePSVIInfo GetAttributePSVI(string localName, string ns)
		{
			for (int i = 0; i < this.coreReaderAttributeCount; i++)
			{
				AttributePSVIInfo attributePSVIInfo = this.attributePSVINodes[i];
				if (attributePSVIInfo != null && Ref.Equal(localName, attributePSVIInfo.localName) && Ref.Equal(ns, attributePSVIInfo.namespaceUri))
				{
					this.currentAttrIndex = i;
					return attributePSVIInfo;
				}
			}
			return null;
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0006A350 File Offset: 0x00068550
		private ValidatingReaderNodeData GetDefaultAttribute(string name, bool updatePosition)
		{
			string text;
			string text2;
			ValidateNames.SplitQName(name, out text, out text2);
			text = this.coreReaderNameTable.Add(text);
			text2 = this.coreReaderNameTable.Add(text2);
			string text3;
			if (text.Length == 0)
			{
				text3 = string.Empty;
			}
			else
			{
				text3 = this.thisNSResolver.LookupNamespace(text);
			}
			return this.GetDefaultAttribute(text2, text3, updatePosition);
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x0006A3A8 File Offset: 0x000685A8
		private ValidatingReaderNodeData GetDefaultAttribute(string attrLocalName, string ns, bool updatePosition)
		{
			for (int i = 0; i < this.defaultAttributes.Count; i++)
			{
				ValidatingReaderNodeData validatingReaderNodeData = (ValidatingReaderNodeData)this.defaultAttributes[i];
				if (Ref.Equal(validatingReaderNodeData.LocalName, attrLocalName) && Ref.Equal(validatingReaderNodeData.Namespace, ns))
				{
					if (updatePosition)
					{
						this.currentAttrIndex = this.coreReader.AttributeCount + i;
					}
					return validatingReaderNodeData;
				}
			}
			return null;
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0006A414 File Offset: 0x00068614
		private AttributePSVIInfo AddAttributePSVI(int attIndex)
		{
			AttributePSVIInfo attributePSVIInfo = this.attributePSVINodes[attIndex];
			if (attributePSVIInfo != null)
			{
				attributePSVIInfo.Reset();
				return attributePSVIInfo;
			}
			if (attIndex >= this.attributePSVINodes.Length - 1)
			{
				AttributePSVIInfo[] array = new AttributePSVIInfo[this.attributePSVINodes.Length * 2];
				Array.Copy(this.attributePSVINodes, 0, array, 0, this.attributePSVINodes.Length);
				this.attributePSVINodes = array;
			}
			attributePSVIInfo = this.attributePSVINodes[attIndex];
			if (attributePSVIInfo == null)
			{
				attributePSVIInfo = new AttributePSVIInfo();
				this.attributePSVINodes[attIndex] = attributePSVIInfo;
			}
			return attributePSVIInfo;
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0006A48B File Offset: 0x0006868B
		private bool IsXSDRoot(string localName, string ns)
		{
			return Ref.Equal(ns, this.NsXs) && Ref.Equal(localName, this.XsdSchema);
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0006A4AC File Offset: 0x000686AC
		private void ProcessInlineSchema()
		{
			if (this.coreReader.Read())
			{
				if (this.coreReader.NodeType == XmlNodeType.Element)
				{
					this.attributeCount = (this.coreReaderAttributeCount = this.coreReader.AttributeCount);
				}
				else
				{
					this.ClearAttributesInfo();
				}
				if (!this.inlineSchemaParser.ParseReaderNode())
				{
					this.inlineSchemaParser.FinishParsing();
					XmlSchema xmlSchema = this.inlineSchemaParser.XmlSchema;
					this.validator.AddSchema(xmlSchema);
					this.inlineSchemaParser = null;
					this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				}
			}
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x0006A535 File Offset: 0x00068735
		private object InternalReadContentAsObject()
		{
			return this.InternalReadContentAsObject(false);
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0006A540 File Offset: 0x00068740
		private object InternalReadContentAsObject(bool unwrapTypedValue)
		{
			string text;
			return this.InternalReadContentAsObject(unwrapTypedValue, out text);
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0006A558 File Offset: 0x00068758
		private object InternalReadContentAsObject(bool unwrapTypedValue, out string originalStringValue)
		{
			XmlNodeType nodeType = this.NodeType;
			if (nodeType == XmlNodeType.Attribute)
			{
				originalStringValue = this.Value;
				if (this.attributePSVI != null && this.attributePSVI.typedAttributeValue != null)
				{
					if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute)
					{
						XmlSchemaAttribute schemaAttribute = this.attributePSVI.attributeSchemaInfo.SchemaAttribute;
						originalStringValue = ((schemaAttribute.DefaultValue != null) ? schemaAttribute.DefaultValue : schemaAttribute.FixedValue);
					}
					return this.ReturnBoxedValue(this.attributePSVI.typedAttributeValue, this.AttributeSchemaInfo.XmlType, unwrapTypedValue);
				}
				return this.Value;
			}
			else if (nodeType == XmlNodeType.EndElement)
			{
				if (this.atomicValue != null)
				{
					originalStringValue = this.originalAtomicValueString;
					return this.atomicValue;
				}
				originalStringValue = string.Empty;
				return string.Empty;
			}
			else
			{
				if (this.validator.CurrentContentType == XmlSchemaContentType.TextOnly)
				{
					object obj = this.ReturnBoxedValue(this.ReadTillEndElement(), this.xmlSchemaInfo.XmlType, unwrapTypedValue);
					originalStringValue = this.originalAtomicValueString;
					return obj;
				}
				XsdCachingReader xsdCachingReader = this.coreReader as XsdCachingReader;
				if (xsdCachingReader != null)
				{
					originalStringValue = xsdCachingReader.ReadOriginalContentAsString();
				}
				else
				{
					originalStringValue = base.InternalReadContentAsString();
				}
				return originalStringValue;
			}
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0006A65E File Offset: 0x0006885E
		private object InternalReadElementContentAsObject(out XmlSchemaType xmlType)
		{
			return this.InternalReadElementContentAsObject(out xmlType, false);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x0006A668 File Offset: 0x00068868
		private object InternalReadElementContentAsObject(out XmlSchemaType xmlType, bool unwrapTypedValue)
		{
			string text;
			return this.InternalReadElementContentAsObject(out xmlType, unwrapTypedValue, out text);
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x0006A680 File Offset: 0x00068880
		private object InternalReadElementContentAsObject(out XmlSchemaType xmlType, bool unwrapTypedValue, out string originalString)
		{
			xmlType = null;
			object obj;
			if (this.IsEmptyElement)
			{
				if (this.xmlSchemaInfo.ContentType == XmlSchemaContentType.TextOnly)
				{
					obj = this.ReturnBoxedValue(this.atomicValue, this.xmlSchemaInfo.XmlType, unwrapTypedValue);
				}
				else
				{
					obj = this.atomicValue;
				}
				originalString = this.originalAtomicValueString;
				xmlType = this.ElementXmlType;
				this.Read();
				return obj;
			}
			this.Read();
			if (this.NodeType == XmlNodeType.EndElement)
			{
				if (this.xmlSchemaInfo.IsDefault)
				{
					if (this.xmlSchemaInfo.ContentType == XmlSchemaContentType.TextOnly)
					{
						obj = this.ReturnBoxedValue(this.atomicValue, this.xmlSchemaInfo.XmlType, unwrapTypedValue);
					}
					else
					{
						obj = this.atomicValue;
					}
					originalString = this.originalAtomicValueString;
				}
				else
				{
					obj = string.Empty;
					originalString = string.Empty;
				}
			}
			else
			{
				if (this.NodeType == XmlNodeType.Element)
				{
					throw new XmlException("ReadElementContentAs() methods cannot be called on an element that has child elements.", string.Empty, this);
				}
				obj = this.InternalReadContentAsObject(unwrapTypedValue, out originalString);
				if (this.NodeType != XmlNodeType.EndElement)
				{
					throw new XmlException("ReadElementContentAs() methods cannot be called on an element that has child elements.", string.Empty, this);
				}
			}
			xmlType = this.ElementXmlType;
			this.Read();
			return obj;
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x0006A798 File Offset: 0x00068998
		private object ReadTillEndElement()
		{
			if (this.atomicValue == null)
			{
				while (this.coreReader.Read())
				{
					if (!this.replayCache)
					{
						switch (this.coreReader.NodeType)
						{
						case XmlNodeType.Element:
							this.ProcessReaderEvent();
							goto IL_010B;
						case XmlNodeType.Text:
						case XmlNodeType.CDATA:
							this.validator.ValidateText(new XmlValueGetter(this.GetStringValue));
							break;
						case XmlNodeType.Whitespace:
						case XmlNodeType.SignificantWhitespace:
							this.validator.ValidateWhitespace(new XmlValueGetter(this.GetStringValue));
							break;
						case XmlNodeType.EndElement:
							this.atomicValue = this.validator.ValidateEndElement(this.xmlSchemaInfo);
							this.originalAtomicValueString = this.GetOriginalAtomicValueStringOfElement();
							if (this.manageNamespaces)
							{
								this.nsManager.PopScope();
								goto IL_010B;
							}
							goto IL_010B;
						}
					}
				}
			}
			else
			{
				if (this.atomicValue == this)
				{
					this.atomicValue = null;
				}
				this.SwitchReader();
			}
			IL_010B:
			return this.atomicValue;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x0006A8B8 File Offset: 0x00068AB8
		private void SwitchReader()
		{
			XsdCachingReader xsdCachingReader = this.coreReader as XsdCachingReader;
			if (xsdCachingReader != null)
			{
				this.coreReader = xsdCachingReader.GetCoreReader();
			}
			this.replayCache = false;
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0006A8E8 File Offset: 0x00068AE8
		private void ReadAheadForMemberType()
		{
			while (this.coreReader.Read())
			{
				switch (this.coreReader.NodeType)
				{
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
					this.validator.ValidateText(new XmlValueGetter(this.GetStringValue));
					break;
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					this.validator.ValidateWhitespace(new XmlValueGetter(this.GetStringValue));
					break;
				case XmlNodeType.EndElement:
					this.atomicValue = this.validator.ValidateEndElement(this.xmlSchemaInfo);
					this.originalAtomicValueString = this.GetOriginalAtomicValueStringOfElement();
					if (this.atomicValue == null)
					{
						this.atomicValue = this;
						return;
					}
					if (this.xmlSchemaInfo.IsDefault)
					{
						this.cachingReader.SwitchTextNodeAndEndElement(this.xmlSchemaInfo.XmlType.ValueConverter.ToString(this.atomicValue), this.originalAtomicValueString);
						return;
					}
					return;
				}
			}
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0006AA04 File Offset: 0x00068C04
		private void GetIsDefault()
		{
			if (!(this.coreReader is XsdCachingReader) && this.xmlSchemaInfo.HasDefaultValue)
			{
				this.coreReader = this.GetCachingReader();
				if (this.xmlSchemaInfo.IsUnionType && !this.xmlSchemaInfo.IsNil)
				{
					this.ReadAheadForMemberType();
				}
				else if (this.coreReader.Read())
				{
					switch (this.coreReader.NodeType)
					{
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
						this.validator.ValidateText(new XmlValueGetter(this.GetStringValue));
						break;
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
						this.validator.ValidateWhitespace(new XmlValueGetter(this.GetStringValue));
						break;
					case XmlNodeType.EndElement:
						this.atomicValue = this.validator.ValidateEndElement(this.xmlSchemaInfo);
						this.originalAtomicValueString = this.GetOriginalAtomicValueStringOfElement();
						if (this.xmlSchemaInfo.IsDefault)
						{
							this.cachingReader.SwitchTextNodeAndEndElement(this.xmlSchemaInfo.XmlType.ValueConverter.ToString(this.atomicValue), this.originalAtomicValueString);
						}
						break;
					}
				}
				this.cachingReader.SetToReplayMode();
				this.replayCache = true;
			}
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0006AB68 File Offset: 0x00068D68
		private void GetMemberType()
		{
			if (this.xmlSchemaInfo.MemberType != null || this.atomicValue == this)
			{
				return;
			}
			if (!(this.coreReader is XsdCachingReader) && this.xmlSchemaInfo.IsUnionType && !this.xmlSchemaInfo.IsNil)
			{
				this.coreReader = this.GetCachingReader();
				this.ReadAheadForMemberType();
				this.cachingReader.SetToReplayMode();
				this.replayCache = true;
			}
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0006ABD8 File Offset: 0x00068DD8
		private object ReturnBoxedValue(object typedValue, XmlSchemaType xmlType, bool unWrap)
		{
			if (typedValue != null)
			{
				if (unWrap && xmlType.Datatype.Variety == XmlSchemaDatatypeVariety.List && (xmlType.Datatype as Datatype_List).ItemType.Variety == XmlSchemaDatatypeVariety.Union)
				{
					typedValue = xmlType.ValueConverter.ChangeType(typedValue, xmlType.Datatype.ValueType, this.thisNSResolver);
				}
				return typedValue;
			}
			typedValue = this.validator.GetConcatenatedValue();
			return typedValue;
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0006AC40 File Offset: 0x00068E40
		private XsdCachingReader GetCachingReader()
		{
			if (this.cachingReader == null)
			{
				this.cachingReader = new XsdCachingReader(this.coreReader, this.lineInfo, new CachingEventHandler(this.CachingCallBack));
			}
			else
			{
				this.cachingReader.Reset(this.coreReader);
			}
			this.lineInfo = this.cachingReader;
			return this.cachingReader;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0006AC9D File Offset: 0x00068E9D
		internal ValidatingReaderNodeData CreateDummyTextNode(string attributeValue, int depth)
		{
			if (this.textNode == null)
			{
				this.textNode = new ValidatingReaderNodeData(XmlNodeType.Text);
			}
			this.textNode.Depth = depth;
			this.textNode.RawValue = attributeValue;
			return this.textNode;
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0006ACD1 File Offset: 0x00068ED1
		internal void CachingCallBack(XsdCachingReader cachingReader)
		{
			this.coreReader = cachingReader.GetCoreReader();
			this.lineInfo = cachingReader.GetLineInfo();
			this.replayCache = false;
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0006ACF4 File Offset: 0x00068EF4
		private string GetOriginalAtomicValueStringOfElement()
		{
			if (!this.xmlSchemaInfo.IsDefault)
			{
				return this.validator.GetConcatenatedValue();
			}
			XmlSchemaElement schemaElement = this.xmlSchemaInfo.SchemaElement;
			if (schemaElement == null)
			{
				return string.Empty;
			}
			if (schemaElement.DefaultValue == null)
			{
				return schemaElement.FixedValue;
			}
			return schemaElement.DefaultValue;
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0006AD44 File Offset: 0x00068F44
		public override Task<string> GetValueAsync()
		{
			if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
			{
				return Task.FromResult<string>(this.cachedNode.RawValue);
			}
			return this.coreReader.GetValueAsync();
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0006AD6B File Offset: 0x00068F6B
		public override Task<object> ReadContentAsObjectAsync()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsObject");
			}
			return this.InternalReadContentAsObjectAsync(true);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0006AD90 File Offset: 0x00068F90
		public override async Task<string> ReadContentAsStringAsync()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsString");
			}
			object obj = await this.InternalReadContentAsObjectAsync().ConfigureAwait(false);
			XmlSchemaType xmlSchemaType = ((this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType);
			string text;
			try
			{
				if (xmlSchemaType != null)
				{
					text = xmlSchemaType.ValueConverter.ToString(obj);
				}
				else
				{
					text = obj as string;
				}
			}
			catch (InvalidCastException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", ex, this);
			}
			catch (FormatException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", ex3, this);
			}
			return text;
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x0006ADD8 File Offset: 0x00068FD8
		public override async Task<object> ReadContentAsAsync(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAs");
			}
			object obj = await this.InternalReadContentAsObjectTupleAsync(false).ConfigureAwait(false);
			string item = obj.Item1;
			object obj2 = obj.Item2;
			XmlSchemaType xmlSchemaType = ((this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType);
			object obj3;
			try
			{
				if (xmlSchemaType != null)
				{
					if (returnType == typeof(DateTimeOffset) && xmlSchemaType.Datatype is Datatype_dateTimeBase)
					{
						obj2 = item;
					}
					obj3 = xmlSchemaType.ValueConverter.ChangeType(obj2, returnType);
				}
				else
				{
					obj3 = XmlUntypedConverter.Untyped.ChangeType(obj2, returnType, namespaceResolver);
				}
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), ex, this);
			}
			catch (InvalidCastException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), ex3, this);
			}
			return obj3;
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0006AE30 File Offset: 0x00069030
		public override async Task<object> ReadElementContentAsObjectAsync()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsObject");
			}
			return (await this.InternalReadElementContentAsObjectAsync(true).ConfigureAwait(false)).Item2;
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x0006AE78 File Offset: 0x00069078
		public override async Task<string> ReadElementContentAsStringAsync()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsString");
			}
			object obj = await this.InternalReadElementContentAsObjectAsync().ConfigureAwait(false);
			XmlSchemaType item = obj.Item1;
			object item2 = obj.Item2;
			string text;
			try
			{
				if (item != null)
				{
					text = item.ValueConverter.ToString(item2);
				}
				else
				{
					text = item2 as string;
				}
			}
			catch (InvalidCastException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", ex, this);
			}
			catch (FormatException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", ex3, this);
			}
			return text;
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0006AEC0 File Offset: 0x000690C0
		public override async Task<object> ReadElementContentAsAsync(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAs");
			}
			object obj = await this.InternalReadElementContentAsObjectTupleAsync(false).ConfigureAwait(false);
			XmlSchemaType item = obj.Item1;
			string item2 = obj.Item2;
			object obj2 = obj.Item3;
			object obj3;
			try
			{
				if (item != null)
				{
					if (returnType == typeof(DateTimeOffset) && item.Datatype is Datatype_dateTimeBase)
					{
						obj2 = item2;
					}
					obj3 = item.ValueConverter.ChangeType(obj2, returnType, namespaceResolver);
				}
				else
				{
					obj3 = XmlUntypedConverter.Untyped.ChangeType(obj2, returnType, namespaceResolver);
				}
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), ex, this);
			}
			catch (InvalidCastException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), ex3, this);
			}
			return obj3;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x0006AF18 File Offset: 0x00069118
		private Task<bool> ReadAsync_Read(Task<bool> task)
		{
			if (!task.IsSuccess())
			{
				return this._ReadAsync_Read(task);
			}
			if (task.Result)
			{
				return this.ProcessReaderEventAsync().ReturnTaskBoolWhenFinish(true);
			}
			this.validator.EndValidation();
			if (this.coreReader.EOF)
			{
				this.validationState = XsdValidatingReader.ValidatingReaderState.EOF;
			}
			return AsyncHelper.DoneTaskFalse;
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x0006AF70 File Offset: 0x00069170
		private async Task<bool> _ReadAsync_Read(Task<bool> task)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = task.ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			bool flag;
			if (configuredTaskAwaiter.GetResult())
			{
				await this.ProcessReaderEventAsync().ConfigureAwait(false);
				flag = true;
			}
			else
			{
				this.validator.EndValidation();
				if (this.coreReader.EOF)
				{
					this.validationState = XsdValidatingReader.ValidatingReaderState.EOF;
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x0006AFBD File Offset: 0x000691BD
		private Task<bool> ReadAsync_ReadAhead(Task task)
		{
			if (task.IsSuccess())
			{
				this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				return AsyncHelper.DoneTaskTrue;
			}
			return this._ReadAsync_ReadAhead(task);
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x0006AFDC File Offset: 0x000691DC
		private async Task<bool> _ReadAsync_ReadAhead(Task task)
		{
			await task.ConfigureAwait(false);
			this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
			return true;
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0006B02C File Offset: 0x0006922C
		public override Task<bool> ReadAsync()
		{
			switch (this.validationState)
			{
			case XsdValidatingReader.ValidatingReaderState.OnReadAttributeValue:
			case XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute:
			case XsdValidatingReader.ValidatingReaderState.OnAttribute:
			case XsdValidatingReader.ValidatingReaderState.ClearAttributes:
				this.ClearAttributesInfo();
				if (this.inlineSchemaParser != null)
				{
					this.validationState = XsdValidatingReader.ValidatingReaderState.ParseInlineSchema;
					goto IL_0059;
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				break;
			case XsdValidatingReader.ValidatingReaderState.None:
				goto IL_00F0;
			case XsdValidatingReader.ValidatingReaderState.Init:
				this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				if (this.coreReader.ReadState == ReadState.Interactive)
				{
					return this.ProcessReaderEventAsync().ReturnTaskBoolWhenFinish(true);
				}
				break;
			case XsdValidatingReader.ValidatingReaderState.Read:
				break;
			case XsdValidatingReader.ValidatingReaderState.ParseInlineSchema:
				goto IL_0059;
			case XsdValidatingReader.ValidatingReaderState.ReadAhead:
			{
				this.ClearAttributesInfo();
				Task task = this.ProcessReaderEventAsync();
				return this.ReadAsync_ReadAhead(task);
			}
			case XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent:
				this.validationState = this.savedState;
				return this.readBinaryHelper.FinishAsync().CallBoolTaskFuncWhenFinish(new Func<Task<bool>>(this.ReadAsync));
			case XsdValidatingReader.ValidatingReaderState.ReaderClosed:
			case XsdValidatingReader.ValidatingReaderState.EOF:
				return AsyncHelper.DoneTaskFalse;
			default:
				goto IL_00F0;
			}
			Task<bool> task2 = this.coreReader.ReadAsync();
			return this.ReadAsync_Read(task2);
			IL_0059:
			return this.ProcessInlineSchemaAsync().ReturnTaskBoolWhenFinish(true);
			IL_00F0:
			return AsyncHelper.DoneTaskFalse;
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0006B130 File Offset: 0x00069330
		public override async Task SkipAsync()
		{
			int depth = this.Depth;
			XmlNodeType nodeType = this.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Attribute)
				{
					goto IL_0116;
				}
				this.MoveToElement();
			}
			if (!this.coreReader.IsEmptyElement)
			{
				bool callSkipToEndElem = true;
				if ((this.xmlSchemaInfo.IsUnionType || this.xmlSchemaInfo.IsDefault) && this.coreReader is XsdCachingReader)
				{
					callSkipToEndElem = false;
				}
				await this.coreReader.SkipAsync().ConfigureAwait(false);
				this.validationState = XsdValidatingReader.ValidatingReaderState.ReadAhead;
				if (callSkipToEndElem)
				{
					this.validator.SkipToEndElement(this.xmlSchemaInfo);
				}
			}
			IL_0116:
			await this.ReadAsync().ConfigureAwait(false);
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0006B178 File Offset: 0x00069378
		public override async Task<int> ReadContentAsBase64Async(byte[] buffer, int index, int count)
		{
			int num;
			if (this.ReadState != ReadState.Interactive)
			{
				num = 0;
			}
			else
			{
				if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
				{
					this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
					this.savedState = this.validationState;
				}
				this.validationState = this.savedState;
				int num2 = await this.readBinaryHelper.ReadContentAsBase64Async(buffer, index, count).ConfigureAwait(false);
				this.savedState = this.validationState;
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
				num = num2;
			}
			return num;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0006B1D8 File Offset: 0x000693D8
		public override async Task<int> ReadContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			int num;
			if (this.ReadState != ReadState.Interactive)
			{
				num = 0;
			}
			else
			{
				if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
				{
					this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
					this.savedState = this.validationState;
				}
				this.validationState = this.savedState;
				int num2 = await this.readBinaryHelper.ReadContentAsBinHexAsync(buffer, index, count).ConfigureAwait(false);
				this.savedState = this.validationState;
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
				num = num2;
			}
			return num;
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x0006B238 File Offset: 0x00069438
		public override async Task<int> ReadElementContentAsBase64Async(byte[] buffer, int index, int count)
		{
			int num;
			if (this.ReadState != ReadState.Interactive)
			{
				num = 0;
			}
			else
			{
				if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
				{
					this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
					this.savedState = this.validationState;
				}
				this.validationState = this.savedState;
				int num2 = await this.readBinaryHelper.ReadElementContentAsBase64Async(buffer, index, count).ConfigureAwait(false);
				this.savedState = this.validationState;
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
				num = num2;
			}
			return num;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x0006B298 File Offset: 0x00069498
		public override async Task<int> ReadElementContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			int num;
			if (this.ReadState != ReadState.Interactive)
			{
				num = 0;
			}
			else
			{
				if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
				{
					this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
					this.savedState = this.validationState;
				}
				this.validationState = this.savedState;
				int num2 = await this.readBinaryHelper.ReadElementContentAsBinHexAsync(buffer, index, count).ConfigureAwait(false);
				this.savedState = this.validationState;
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
				num = num2;
			}
			return num;
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0006B2F8 File Offset: 0x000694F8
		private Task ProcessReaderEventAsync()
		{
			if (this.replayCache)
			{
				return AsyncHelper.DoneTask;
			}
			switch (this.coreReader.NodeType)
			{
			case XmlNodeType.Element:
				return this.ProcessElementEventAsync();
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
				this.validator.ValidateText(new XmlValueGetter(this.GetStringValue));
				break;
			case XmlNodeType.EntityReference:
				throw new InvalidOperationException();
			case XmlNodeType.DocumentType:
				this.validator.SetDtdSchemaInfo(this.coreReader.DtdInfo);
				break;
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				this.validator.ValidateWhitespace(new XmlValueGetter(this.GetStringValue));
				break;
			case XmlNodeType.EndElement:
				return this.ProcessEndElementEventAsync();
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0006B3C8 File Offset: 0x000695C8
		private async Task ProcessElementEventAsync()
		{
			if (this.processInlineSchema && this.IsXSDRoot(this.coreReader.LocalName, this.coreReader.NamespaceURI) && this.coreReader.Depth > 0)
			{
				this.xmlSchemaInfo.Clear();
				this.attributeCount = (this.coreReaderAttributeCount = this.coreReader.AttributeCount);
				if (!this.coreReader.IsEmptyElement)
				{
					this.inlineSchemaParser = new Parser(SchemaType.XSD, this.coreReaderNameTable, this.validator.SchemaSet.GetSchemaNames(this.coreReaderNameTable), this.validationEvent);
					await this.inlineSchemaParser.StartParsingAsync(this.coreReader, null).ConfigureAwait(false);
					this.inlineSchemaParser.ParseReaderNode();
					this.validationState = XsdValidatingReader.ValidatingReaderState.ParseInlineSchema;
				}
				else
				{
					this.validationState = XsdValidatingReader.ValidatingReaderState.ClearAttributes;
				}
			}
			else
			{
				this.atomicValue = null;
				this.originalAtomicValueString = null;
				this.xmlSchemaInfo.Clear();
				if (this.manageNamespaces)
				{
					this.nsManager.PushScope();
				}
				string text = null;
				string text2 = null;
				string text3 = null;
				string text4 = null;
				if (this.coreReader.MoveToFirstAttribute())
				{
					do
					{
						string namespaceURI = this.coreReader.NamespaceURI;
						string localName = this.coreReader.LocalName;
						if (Ref.Equal(namespaceURI, this.NsXsi))
						{
							if (Ref.Equal(localName, this.XsiSchemaLocation))
							{
								text = this.coreReader.Value;
							}
							else if (Ref.Equal(localName, this.XsiNoNamespaceSchemaLocation))
							{
								text2 = this.coreReader.Value;
							}
							else if (Ref.Equal(localName, this.XsiType))
							{
								text4 = this.coreReader.Value;
							}
							else if (Ref.Equal(localName, this.XsiNil))
							{
								text3 = this.coreReader.Value;
							}
						}
						if (this.manageNamespaces && Ref.Equal(this.coreReader.NamespaceURI, this.NsXmlNs))
						{
							this.nsManager.AddNamespace((this.coreReader.Prefix.Length == 0) ? string.Empty : this.coreReader.LocalName, this.coreReader.Value);
						}
					}
					while (this.coreReader.MoveToNextAttribute());
					this.coreReader.MoveToElement();
				}
				this.validator.ValidateElement(this.coreReader.LocalName, this.coreReader.NamespaceURI, this.xmlSchemaInfo, text4, text3, text, text2);
				this.ValidateAttributes();
				this.validator.ValidateEndOfAttributes(this.xmlSchemaInfo);
				if (this.coreReader.IsEmptyElement)
				{
					await this.ProcessEndElementEventAsync().ConfigureAwait(false);
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.ClearAttributes;
			}
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0006B410 File Offset: 0x00069610
		private async Task ProcessEndElementEventAsync()
		{
			this.atomicValue = this.validator.ValidateEndElement(this.xmlSchemaInfo);
			this.originalAtomicValueString = this.GetOriginalAtomicValueStringOfElement();
			if (this.xmlSchemaInfo.IsDefault)
			{
				int depth = this.coreReader.Depth;
				this.coreReader = this.GetCachingReader();
				this.cachingReader.RecordTextNode(this.xmlSchemaInfo.XmlType.ValueConverter.ToString(this.atomicValue), this.originalAtomicValueString, depth + 1, 0, 0);
				this.cachingReader.RecordEndElementNode();
				await this.cachingReader.SetToReplayModeAsync().ConfigureAwait(false);
				this.replayCache = true;
			}
			else if (this.manageNamespaces)
			{
				this.nsManager.PopScope();
			}
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0006B458 File Offset: 0x00069658
		private async Task ProcessInlineSchemaAsync()
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.coreReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (configuredTaskAwaiter.GetResult())
			{
				if (this.coreReader.NodeType == XmlNodeType.Element)
				{
					int num = this.coreReader.AttributeCount;
					this.coreReaderAttributeCount = num;
					this.attributeCount = num;
				}
				else
				{
					this.ClearAttributesInfo();
				}
				if (!this.inlineSchemaParser.ParseReaderNode())
				{
					this.inlineSchemaParser.FinishParsing();
					XmlSchema xmlSchema = this.inlineSchemaParser.XmlSchema;
					this.validator.AddSchema(xmlSchema);
					this.inlineSchemaParser = null;
					this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				}
			}
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0006B49D File Offset: 0x0006969D
		private Task<object> InternalReadContentAsObjectAsync()
		{
			return this.InternalReadContentAsObjectAsync(false);
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0006B4A8 File Offset: 0x000696A8
		private async Task<object> InternalReadContentAsObjectAsync(bool unwrapTypedValue)
		{
			return (await this.InternalReadContentAsObjectTupleAsync(unwrapTypedValue).ConfigureAwait(false)).Item2;
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0006B4F8 File Offset: 0x000696F8
		private async Task<Tuple<string, object>> InternalReadContentAsObjectTupleAsync(bool unwrapTypedValue)
		{
			XmlNodeType nodeType = this.NodeType;
			Tuple<string, object> tuple;
			if (nodeType == XmlNodeType.Attribute)
			{
				string text = this.Value;
				if (this.attributePSVI != null && this.attributePSVI.typedAttributeValue != null)
				{
					if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute)
					{
						XmlSchemaAttribute schemaAttribute = this.attributePSVI.attributeSchemaInfo.SchemaAttribute;
						text = ((schemaAttribute.DefaultValue != null) ? schemaAttribute.DefaultValue : schemaAttribute.FixedValue);
					}
					tuple = new Tuple<string, object>(text, this.ReturnBoxedValue(this.attributePSVI.typedAttributeValue, this.AttributeSchemaInfo.XmlType, unwrapTypedValue));
				}
				else
				{
					tuple = new Tuple<string, object>(text, this.Value);
				}
			}
			else if (nodeType == XmlNodeType.EndElement)
			{
				if (this.atomicValue != null)
				{
					string text = this.originalAtomicValueString;
					tuple = new Tuple<string, object>(text, this.atomicValue);
				}
				else
				{
					string text = string.Empty;
					tuple = new Tuple<string, object>(text, string.Empty);
				}
			}
			else if (this.validator.CurrentContentType == XmlSchemaContentType.TextOnly)
			{
				object obj = await this.ReadTillEndElementAsync().ConfigureAwait(false);
				object obj2 = this.ReturnBoxedValue(obj, this.xmlSchemaInfo.XmlType, unwrapTypedValue);
				string text = this.originalAtomicValueString;
				tuple = new Tuple<string, object>(text, obj2);
			}
			else
			{
				XsdCachingReader xsdCachingReader = this.coreReader as XsdCachingReader;
				string text;
				if (xsdCachingReader != null)
				{
					text = xsdCachingReader.ReadOriginalContentAsString();
				}
				else
				{
					text = await base.InternalReadContentAsStringAsync().ConfigureAwait(false);
				}
				tuple = new Tuple<string, object>(text, text);
			}
			return tuple;
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0006B545 File Offset: 0x00069745
		private Task<Tuple<XmlSchemaType, object>> InternalReadElementContentAsObjectAsync()
		{
			return this.InternalReadElementContentAsObjectAsync(false);
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0006B550 File Offset: 0x00069750
		private async Task<Tuple<XmlSchemaType, object>> InternalReadElementContentAsObjectAsync(bool unwrapTypedValue)
		{
			Tuple<XmlSchemaType, string, object> tuple = await this.InternalReadElementContentAsObjectTupleAsync(unwrapTypedValue).ConfigureAwait(false);
			return new Tuple<XmlSchemaType, object>(tuple.Item1, tuple.Item3);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0006B5A0 File Offset: 0x000697A0
		private async Task<Tuple<XmlSchemaType, string, object>> InternalReadElementContentAsObjectTupleAsync(bool unwrapTypedValue)
		{
			object typedValue = null;
			XmlSchemaType xmlType = null;
			Tuple<XmlSchemaType, string, object> tuple;
			if (this.IsEmptyElement)
			{
				if (this.xmlSchemaInfo.ContentType == XmlSchemaContentType.TextOnly)
				{
					typedValue = this.ReturnBoxedValue(this.atomicValue, this.xmlSchemaInfo.XmlType, unwrapTypedValue);
				}
				else
				{
					typedValue = this.atomicValue;
				}
				string originalString = this.originalAtomicValueString;
				xmlType = this.ElementXmlType;
				await this.ReadAsync().ConfigureAwait(false);
				tuple = new Tuple<XmlSchemaType, string, object>(xmlType, originalString, typedValue);
			}
			else
			{
				await this.ReadAsync().ConfigureAwait(false);
				string originalString;
				if (this.NodeType == XmlNodeType.EndElement)
				{
					if (this.xmlSchemaInfo.IsDefault)
					{
						if (this.xmlSchemaInfo.ContentType == XmlSchemaContentType.TextOnly)
						{
							typedValue = this.ReturnBoxedValue(this.atomicValue, this.xmlSchemaInfo.XmlType, unwrapTypedValue);
						}
						else
						{
							typedValue = this.atomicValue;
						}
						originalString = this.originalAtomicValueString;
					}
					else
					{
						typedValue = string.Empty;
						originalString = string.Empty;
					}
				}
				else
				{
					if (this.NodeType == XmlNodeType.Element)
					{
						throw new XmlException("ReadElementContentAs() methods cannot be called on an element that has child elements.", string.Empty, this);
					}
					Tuple<string, object> tuple2 = await this.InternalReadContentAsObjectTupleAsync(unwrapTypedValue).ConfigureAwait(false);
					originalString = tuple2.Item1;
					typedValue = tuple2.Item2;
					if (this.NodeType != XmlNodeType.EndElement)
					{
						throw new XmlException("ReadElementContentAs() methods cannot be called on an element that has child elements.", string.Empty, this);
					}
				}
				xmlType = this.ElementXmlType;
				await this.ReadAsync().ConfigureAwait(false);
				tuple = new Tuple<XmlSchemaType, string, object>(xmlType, originalString, typedValue);
			}
			return tuple;
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0006B5F0 File Offset: 0x000697F0
		private async Task<object> ReadTillEndElementAsync()
		{
			if (this.atomicValue == null)
			{
				for (;;)
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.coreReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
					}
					if (!configuredTaskAwaiter.GetResult())
					{
						goto Block_6;
					}
					if (!this.replayCache)
					{
						switch (this.coreReader.NodeType)
						{
						case XmlNodeType.Element:
							goto IL_008B;
						case XmlNodeType.Text:
						case XmlNodeType.CDATA:
							this.validator.ValidateText(new XmlValueGetter(this.GetStringValue));
							break;
						case XmlNodeType.Whitespace:
						case XmlNodeType.SignificantWhitespace:
							this.validator.ValidateWhitespace(new XmlValueGetter(this.GetStringValue));
							break;
						case XmlNodeType.EndElement:
							goto IL_012A;
						}
					}
				}
				IL_008B:
				await this.ProcessReaderEventAsync().ConfigureAwait(false);
				goto IL_01F0;
				IL_012A:
				this.atomicValue = this.validator.ValidateEndElement(this.xmlSchemaInfo);
				this.originalAtomicValueString = this.GetOriginalAtomicValueStringOfElement();
				if (this.manageNamespaces)
				{
					this.nsManager.PopScope();
				}
				Block_6:;
			}
			else
			{
				if (this.atomicValue == this)
				{
					this.atomicValue = null;
				}
				this.SwitchReader();
			}
			IL_01F0:
			return this.atomicValue;
		}

		// Token: 0x04000C7B RID: 3195
		private XmlReader coreReader;

		// Token: 0x04000C7C RID: 3196
		private IXmlNamespaceResolver coreReaderNSResolver;

		// Token: 0x04000C7D RID: 3197
		private IXmlNamespaceResolver thisNSResolver;

		// Token: 0x04000C7E RID: 3198
		private XmlSchemaValidator validator;

		// Token: 0x04000C7F RID: 3199
		private XmlResolver xmlResolver;

		// Token: 0x04000C80 RID: 3200
		private ValidationEventHandler validationEvent;

		// Token: 0x04000C81 RID: 3201
		private XsdValidatingReader.ValidatingReaderState validationState;

		// Token: 0x04000C82 RID: 3202
		private XmlValueGetter valueGetter;

		// Token: 0x04000C83 RID: 3203
		private XmlNamespaceManager nsManager;

		// Token: 0x04000C84 RID: 3204
		private bool manageNamespaces;

		// Token: 0x04000C85 RID: 3205
		private bool processInlineSchema;

		// Token: 0x04000C86 RID: 3206
		private bool replayCache;

		// Token: 0x04000C87 RID: 3207
		private ValidatingReaderNodeData cachedNode;

		// Token: 0x04000C88 RID: 3208
		private AttributePSVIInfo attributePSVI;

		// Token: 0x04000C89 RID: 3209
		private int attributeCount;

		// Token: 0x04000C8A RID: 3210
		private int coreReaderAttributeCount;

		// Token: 0x04000C8B RID: 3211
		private int currentAttrIndex;

		// Token: 0x04000C8C RID: 3212
		private AttributePSVIInfo[] attributePSVINodes;

		// Token: 0x04000C8D RID: 3213
		private ArrayList defaultAttributes;

		// Token: 0x04000C8E RID: 3214
		private Parser inlineSchemaParser;

		// Token: 0x04000C8F RID: 3215
		private object atomicValue;

		// Token: 0x04000C90 RID: 3216
		private XmlSchemaInfo xmlSchemaInfo;

		// Token: 0x04000C91 RID: 3217
		private string originalAtomicValueString;

		// Token: 0x04000C92 RID: 3218
		private XmlNameTable coreReaderNameTable;

		// Token: 0x04000C93 RID: 3219
		private XsdCachingReader cachingReader;

		// Token: 0x04000C94 RID: 3220
		private ValidatingReaderNodeData textNode;

		// Token: 0x04000C95 RID: 3221
		private string NsXmlNs;

		// Token: 0x04000C96 RID: 3222
		private string NsXs;

		// Token: 0x04000C97 RID: 3223
		private string NsXsi;

		// Token: 0x04000C98 RID: 3224
		private string XsiType;

		// Token: 0x04000C99 RID: 3225
		private string XsiNil;

		// Token: 0x04000C9A RID: 3226
		private string XsdSchema;

		// Token: 0x04000C9B RID: 3227
		private string XsiSchemaLocation;

		// Token: 0x04000C9C RID: 3228
		private string XsiNoNamespaceSchemaLocation;

		// Token: 0x04000C9D RID: 3229
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x04000C9E RID: 3230
		private IXmlLineInfo lineInfo;

		// Token: 0x04000C9F RID: 3231
		private ReadContentAsBinaryHelper readBinaryHelper;

		// Token: 0x04000CA0 RID: 3232
		private XsdValidatingReader.ValidatingReaderState savedState;

		// Token: 0x04000CA1 RID: 3233
		private const int InitialAttributeCount = 8;

		// Token: 0x04000CA2 RID: 3234
		private static volatile Type TypeOfString;

		// Token: 0x020001EE RID: 494
		private enum ValidatingReaderState
		{
			// Token: 0x04000CA4 RID: 3236
			None,
			// Token: 0x04000CA5 RID: 3237
			Init,
			// Token: 0x04000CA6 RID: 3238
			Read,
			// Token: 0x04000CA7 RID: 3239
			OnDefaultAttribute = -1,
			// Token: 0x04000CA8 RID: 3240
			OnReadAttributeValue = -2,
			// Token: 0x04000CA9 RID: 3241
			OnAttribute = 3,
			// Token: 0x04000CAA RID: 3242
			ClearAttributes,
			// Token: 0x04000CAB RID: 3243
			ParseInlineSchema,
			// Token: 0x04000CAC RID: 3244
			ReadAhead,
			// Token: 0x04000CAD RID: 3245
			OnReadBinaryContent,
			// Token: 0x04000CAE RID: 3246
			ReaderClosed,
			// Token: 0x04000CAF RID: 3247
			EOF,
			// Token: 0x04000CB0 RID: 3248
			Error
		}
	}
}
