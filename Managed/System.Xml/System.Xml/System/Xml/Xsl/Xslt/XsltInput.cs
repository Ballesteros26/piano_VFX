using System;
using System.Collections.Generic;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020005AB RID: 1451
	internal class XsltInput : IErrorHelper
	{
		// Token: 0x06003959 RID: 14681 RVA: 0x0014130C File Offset: 0x0013F50C
		public XsltInput(XmlReader reader, Compiler compiler, KeywordsTable atoms)
		{
			XsltInput.EnsureExpandEntities(reader);
			IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
			this.atoms = atoms;
			this.reader = reader;
			this.reatomize = reader.NameTable != atoms.NameTable;
			this.readerLineInfo = ((xmlLineInfo != null && xmlLineInfo.HasLineInfo()) ? xmlLineInfo : null);
			this.topLevelReader = reader.ReadState == ReadState.Initial;
			this.scopeManager = new CompilerScopeManager<VarPar>(atoms);
			this.compiler = compiler;
			this.nodeType = XmlNodeType.Document;
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x0600395A RID: 14682 RVA: 0x001413AA File Offset: 0x0013F5AA
		public XmlNodeType NodeType
		{
			get
			{
				if (this.nodeType != XmlNodeType.Element || 0 >= this.currentRecord)
				{
					return this.nodeType;
				}
				return XmlNodeType.Attribute;
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x0600395B RID: 14683 RVA: 0x001413C6 File Offset: 0x0013F5C6
		public string LocalName
		{
			get
			{
				return this.records[this.currentRecord].localName;
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x0600395C RID: 14684 RVA: 0x001413DE File Offset: 0x0013F5DE
		public string NamespaceUri
		{
			get
			{
				return this.records[this.currentRecord].nsUri;
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x0600395D RID: 14685 RVA: 0x001413F6 File Offset: 0x0013F5F6
		public string Prefix
		{
			get
			{
				return this.records[this.currentRecord].prefix;
			}
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x0600395E RID: 14686 RVA: 0x0014140E File Offset: 0x0013F60E
		public string Value
		{
			get
			{
				return this.records[this.currentRecord].value;
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x0600395F RID: 14687 RVA: 0x00141426 File Offset: 0x0013F626
		public string BaseUri
		{
			get
			{
				return this.records[this.currentRecord].baseUri;
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06003960 RID: 14688 RVA: 0x0014143E File Offset: 0x0013F63E
		public string QualifiedName
		{
			get
			{
				return this.records[this.currentRecord].QualifiedName;
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06003961 RID: 14689 RVA: 0x00141456 File Offset: 0x0013F656
		public bool IsEmptyElement
		{
			get
			{
				return this.isEmptyElement;
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06003962 RID: 14690 RVA: 0x00141426 File Offset: 0x0013F626
		public string Uri
		{
			get
			{
				return this.records[this.currentRecord].baseUri;
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06003963 RID: 14691 RVA: 0x0014145E File Offset: 0x0013F65E
		public Location Start
		{
			get
			{
				return this.records[this.currentRecord].start;
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06003964 RID: 14692 RVA: 0x00141476 File Offset: 0x0013F676
		public Location End
		{
			get
			{
				return this.records[this.currentRecord].end;
			}
		}

		// Token: 0x06003965 RID: 14693 RVA: 0x00141490 File Offset: 0x0013F690
		private static void EnsureExpandEntities(XmlReader reader)
		{
			XmlTextReader xmlTextReader = reader as XmlTextReader;
			if (xmlTextReader != null && xmlTextReader.EntityHandling != EntityHandling.ExpandEntities)
			{
				xmlTextReader.EntityHandling = EntityHandling.ExpandEntities;
			}
		}

		// Token: 0x06003966 RID: 14694 RVA: 0x001414B8 File Offset: 0x0013F6B8
		private void ExtendRecordBuffer(int position)
		{
			if (this.records.Length <= position)
			{
				int num = this.records.Length * 2;
				if (num <= position)
				{
					num = position + 1;
				}
				XsltInput.Record[] array = new XsltInput.Record[num];
				Array.Copy(this.records, array, this.records.Length);
				this.records = array;
			}
		}

		// Token: 0x06003967 RID: 14695 RVA: 0x00141508 File Offset: 0x0013F708
		public bool FindStylesheetElement()
		{
			if (!this.topLevelReader && this.reader.ReadState != ReadState.Interactive)
			{
				return false;
			}
			IDictionary<string, string> dictionary = null;
			if (this.reader.ReadState == ReadState.Interactive)
			{
				IXmlNamespaceResolver xmlNamespaceResolver = this.reader as IXmlNamespaceResolver;
				if (xmlNamespaceResolver != null)
				{
					dictionary = xmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope.ExcludeXml);
				}
			}
			while (this.MoveToNextSibling() && this.nodeType == XmlNodeType.Whitespace)
			{
			}
			if (this.nodeType == XmlNodeType.Element)
			{
				if (dictionary != null)
				{
					foreach (KeyValuePair<string, string> keyValuePair in dictionary)
					{
						if (this.scopeManager.LookupNamespace(keyValuePair.Key) == null)
						{
							string text = this.atoms.NameTable.Add(keyValuePair.Value);
							this.scopeManager.AddNsDeclaration(keyValuePair.Key, text);
							this.ctxInfo.AddNamespace(keyValuePair.Key, text);
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06003968 RID: 14696 RVA: 0x00141600 File Offset: 0x0013F800
		public void Finish()
		{
			if (this.topLevelReader)
			{
				while (this.reader.ReadState == ReadState.Interactive)
				{
					this.reader.Skip();
				}
			}
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x00141628 File Offset: 0x0013F828
		private void FillupRecord(ref XsltInput.Record rec)
		{
			rec.localName = this.reader.LocalName;
			rec.nsUri = this.reader.NamespaceURI;
			rec.prefix = this.reader.Prefix;
			rec.value = this.reader.Value;
			rec.baseUri = this.reader.BaseURI;
			if (this.reatomize)
			{
				rec.localName = this.atoms.NameTable.Add(rec.localName);
				rec.nsUri = this.atoms.NameTable.Add(rec.nsUri);
				rec.prefix = this.atoms.NameTable.Add(rec.prefix);
			}
			if (this.readerLineInfo != null)
			{
				rec.start = new Location(this.readerLineInfo.LineNumber, this.readerLineInfo.LinePosition - XsltInput.PositionAdjustment(this.reader.NodeType));
			}
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x00141720 File Offset: 0x0013F920
		private void SetRecordEnd(ref XsltInput.Record rec)
		{
			if (this.readerLineInfo != null)
			{
				rec.end = new Location(this.readerLineInfo.LineNumber, this.readerLineInfo.LinePosition - XsltInput.PositionAdjustment(this.reader.NodeType));
				if (this.reader.BaseURI != rec.baseUri || rec.end.LessOrEqual(rec.start))
				{
					rec.end = new Location(rec.start.Line, int.MaxValue);
				}
			}
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x001417B0 File Offset: 0x0013F9B0
		private void FillupTextRecord(ref XsltInput.Record rec)
		{
			rec.localName = string.Empty;
			rec.nsUri = string.Empty;
			rec.prefix = string.Empty;
			rec.value = this.reader.Value;
			rec.baseUri = this.reader.BaseURI;
			if (this.readerLineInfo != null)
			{
				bool flag = this.reader.NodeType == XmlNodeType.CDATA;
				int num = this.readerLineInfo.LineNumber;
				int num2 = this.readerLineInfo.LinePosition;
				rec.start = new Location(num, num2 - (flag ? 9 : 0));
				char c = ' ';
				string value = rec.value;
				int i = 0;
				while (i < value.Length)
				{
					char c2 = value[i];
					if (c2 != '\n')
					{
						if (c2 == '\r')
						{
							goto IL_00B9;
						}
						num2++;
					}
					else if (c != '\r')
					{
						goto IL_00B9;
					}
					IL_00C5:
					c = c2;
					i++;
					continue;
					IL_00B9:
					num++;
					num2 = 1;
					goto IL_00C5;
				}
				rec.end = new Location(num, num2 + (flag ? 3 : 0));
			}
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x001418AC File Offset: 0x0013FAAC
		private void FillupCharacterEntityRecord(ref XsltInput.Record rec)
		{
			string localName = this.reader.LocalName;
			rec.localName = string.Empty;
			rec.nsUri = string.Empty;
			rec.prefix = string.Empty;
			rec.baseUri = this.reader.BaseURI;
			if (this.readerLineInfo != null)
			{
				rec.start = new Location(this.readerLineInfo.LineNumber, this.readerLineInfo.LinePosition - 1);
			}
			this.reader.ResolveEntity();
			this.reader.Read();
			rec.value = this.reader.Value;
			this.reader.Read();
			if (this.readerLineInfo != null)
			{
				int lineNumber = this.readerLineInfo.LineNumber;
				int linePosition = this.readerLineInfo.LinePosition;
				rec.end = new Location(this.readerLineInfo.LineNumber, this.readerLineInfo.LinePosition + 1);
			}
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x0014199C File Offset: 0x0013FB9C
		private bool ReadAttribute(ref XsltInput.Record rec)
		{
			this.FillupRecord(ref rec);
			if (Ref.Equal(rec.prefix, this.atoms.Xmlns))
			{
				string text = this.atoms.NameTable.Add(this.reader.Value);
				if (!Ref.Equal(rec.localName, this.atoms.Xml))
				{
					this.scopeManager.AddNsDeclaration(rec.localName, text);
					this.ctxInfo.AddNamespace(rec.localName, text);
				}
				return false;
			}
			if (rec.prefix.Length == 0 && Ref.Equal(rec.localName, this.atoms.Xmlns))
			{
				string text2 = this.atoms.NameTable.Add(this.reader.Value);
				this.scopeManager.AddNsDeclaration(string.Empty, text2);
				this.ctxInfo.AddNamespace(string.Empty, text2);
				return false;
			}
			if (!this.reader.ReadAttributeValue())
			{
				rec.value = string.Empty;
				this.SetRecordEnd(ref rec);
				return true;
			}
			if (this.readerLineInfo != null)
			{
				int num = ((this.reader.NodeType == XmlNodeType.EntityReference) ? (-2) : (-1));
				rec.valueStart = new Location(this.readerLineInfo.LineNumber, this.readerLineInfo.LinePosition + num);
				if (this.reader.BaseURI != rec.baseUri || rec.valueStart.LessOrEqual(rec.start))
				{
					int num2 = ((rec.prefix.Length != 0) ? (rec.prefix.Length + 1) : 0) + rec.localName.Length;
					rec.end = new Location(rec.start.Line, rec.start.Pos + num2 + 1);
				}
			}
			string text3 = string.Empty;
			this.strConcat.Clear();
			do
			{
				XmlNodeType xmlNodeType = this.reader.NodeType;
				if (xmlNodeType != XmlNodeType.EntityReference)
				{
					if (xmlNodeType != XmlNodeType.EndEntity)
					{
						text3 = this.reader.Value;
						this.strConcat.Concat(text3);
					}
				}
				else
				{
					this.reader.ResolveEntity();
				}
			}
			while (this.reader.ReadAttributeValue());
			rec.value = this.strConcat.GetResult();
			if (this.readerLineInfo != null)
			{
				int num3 = ((this.reader.NodeType == XmlNodeType.EndEntity) ? 1 : text3.Length) + 1;
				rec.end = new Location(this.readerLineInfo.LineNumber, this.readerLineInfo.LinePosition + num3);
				if (this.reader.BaseURI != rec.baseUri || rec.end.LessOrEqual(rec.valueStart))
				{
					rec.end = new Location(rec.start.Line, int.MaxValue);
				}
			}
			return true;
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x00141C66 File Offset: 0x0013FE66
		public bool MoveToFirstChild()
		{
			return !this.IsEmptyElement && this.ReadNextSibling();
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x00141C78 File Offset: 0x0013FE78
		public bool MoveToNextSibling()
		{
			if (this.nodeType == XmlNodeType.Element || this.nodeType == XmlNodeType.EndElement)
			{
				this.scopeManager.ExitScope();
			}
			return this.ReadNextSibling();
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x00141C9E File Offset: 0x0013FE9E
		public void SkipNode()
		{
			if (this.nodeType == XmlNodeType.Element && this.MoveToFirstChild())
			{
				do
				{
					this.SkipNode();
				}
				while (this.MoveToNextSibling());
			}
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x00141CC0 File Offset: 0x0013FEC0
		private int ReadTextNodes()
		{
			bool flag = this.reader.XmlSpace == XmlSpace.Preserve;
			bool flag2 = true;
			int num = 0;
			for (;;)
			{
				XmlNodeType xmlNodeType = this.reader.NodeType;
				if (xmlNodeType <= XmlNodeType.EntityReference)
				{
					if (xmlNodeType - XmlNodeType.Text > 1)
					{
						if (xmlNodeType != XmlNodeType.EntityReference)
						{
							break;
						}
						string localName = this.reader.LocalName;
						if (localName.Length > 0 && (localName[0] == '#' || localName == "lt" || localName == "gt" || localName == "quot" || localName == "apos"))
						{
							this.ExtendRecordBuffer(num);
							this.FillupCharacterEntityRecord(ref this.records[num]);
							if (flag2 && !XmlCharType.Instance.IsOnlyWhitespace(this.records[num].value))
							{
								flag2 = false;
							}
							num++;
							continue;
						}
						this.reader.ResolveEntity();
						this.reader.Read();
						continue;
					}
					else if (flag2 && !XmlCharType.Instance.IsOnlyWhitespace(this.reader.Value))
					{
						flag2 = false;
					}
				}
				else if (xmlNodeType - XmlNodeType.Whitespace > 1)
				{
					if (xmlNodeType != XmlNodeType.EndEntity)
					{
						break;
					}
					this.reader.Read();
					continue;
				}
				this.ExtendRecordBuffer(num);
				this.FillupTextRecord(ref this.records[num]);
				this.reader.Read();
				num++;
			}
			this.nodeType = ((!flag2) ? XmlNodeType.Text : (flag ? XmlNodeType.SignificantWhitespace : XmlNodeType.Whitespace));
			return num;
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x00141E48 File Offset: 0x00140048
		private bool ReadNextSibling()
		{
			if (this.currentRecord < this.lastTextNode)
			{
				this.currentRecord++;
				if (this.currentRecord == this.lastTextNode)
				{
					this.lastTextNode = 0;
				}
				return true;
			}
			this.currentRecord = 0;
			while (!this.reader.EOF)
			{
				XmlNodeType xmlNodeType = this.reader.NodeType;
				if (xmlNodeType <= XmlNodeType.EntityReference)
				{
					if (xmlNodeType == XmlNodeType.Element)
					{
						this.scopeManager.EnterScope();
						this.numAttributes = this.ReadElement();
						return true;
					}
					if (xmlNodeType - XmlNodeType.Text > 2)
					{
						goto IL_00D8;
					}
				}
				else if (xmlNodeType - XmlNodeType.Whitespace > 1)
				{
					if (xmlNodeType != XmlNodeType.EndElement)
					{
						goto IL_00D8;
					}
					this.nodeType = XmlNodeType.EndElement;
					this.isEmptyElement = false;
					this.FillupRecord(ref this.records[0]);
					this.reader.Read();
					this.SetRecordEnd(ref this.records[0]);
					return false;
				}
				int num = this.ReadTextNodes();
				if (num != 0)
				{
					this.lastTextNode = num - 1;
					return true;
				}
				continue;
				IL_00D8:
				this.reader.Read();
			}
			return false;
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x00141F4C File Offset: 0x0014014C
		private int ReadElement()
		{
			this.attributesRead = false;
			this.FillupRecord(ref this.records[0]);
			this.nodeType = XmlNodeType.Element;
			this.isEmptyElement = this.reader.IsEmptyElement;
			this.ctxInfo = new XsltInput.ContextInfo(this);
			int num = 1;
			if (this.reader.MoveToFirstAttribute())
			{
				do
				{
					this.ExtendRecordBuffer(num);
					if (this.ReadAttribute(ref this.records[num]))
					{
						num++;
					}
				}
				while (this.reader.MoveToNextAttribute());
				this.reader.MoveToElement();
			}
			this.reader.Read();
			this.SetRecordEnd(ref this.records[0]);
			this.ctxInfo.lineInfo = this.BuildLineInfo();
			this.attributes = null;
			return num - 1;
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x00142016 File Offset: 0x00140216
		public void MoveToElement()
		{
			this.currentRecord = 0;
		}

		// Token: 0x06003975 RID: 14709 RVA: 0x0014201F File Offset: 0x0014021F
		private bool MoveToAttributeBase(int attNum)
		{
			if (0 < attNum && attNum <= this.numAttributes)
			{
				this.currentRecord = attNum;
				return true;
			}
			this.currentRecord = 0;
			return false;
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x0014201F File Offset: 0x0014021F
		public bool MoveToLiteralAttribute(int attNum)
		{
			if (0 < attNum && attNum <= this.numAttributes)
			{
				this.currentRecord = attNum;
				return true;
			}
			this.currentRecord = 0;
			return false;
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x0014203F File Offset: 0x0014023F
		public bool MoveToXsltAttribute(int attNum, string attName)
		{
			this.currentRecord = this.xsltAttributeNumber[attNum];
			return this.currentRecord != 0;
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x00142058 File Offset: 0x00140258
		public bool IsRequiredAttribute(int attNum)
		{
			return (this.attributes[attNum].flags & ((this.compiler.Version == 2) ? XsltLoader.V2Req : XsltLoader.V1Req)) != 0;
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x00142089 File Offset: 0x00140289
		public bool AttributeExists(int attNum, string attName)
		{
			return this.xsltAttributeNumber[attNum] != 0;
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x0600397A RID: 14714 RVA: 0x00142096 File Offset: 0x00140296
		public XsltInput.DelayedQName ElementName
		{
			get
			{
				return new XsltInput.DelayedQName(ref this.records[0]);
			}
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x001420A9 File Offset: 0x001402A9
		public bool IsNs(string ns)
		{
			return Ref.Equal(ns, this.NamespaceUri);
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x001420B7 File Offset: 0x001402B7
		public bool IsKeyword(string kwd)
		{
			return Ref.Equal(kwd, this.LocalName);
		}

		// Token: 0x0600397D RID: 14717 RVA: 0x001420C5 File Offset: 0x001402C5
		public bool IsXsltNamespace()
		{
			return this.IsNs(this.atoms.UriXsl);
		}

		// Token: 0x0600397E RID: 14718 RVA: 0x001420D8 File Offset: 0x001402D8
		public bool IsNullNamespace()
		{
			return this.IsNs(string.Empty);
		}

		// Token: 0x0600397F RID: 14719 RVA: 0x001420E5 File Offset: 0x001402E5
		public bool IsXsltAttribute(string kwd)
		{
			return this.IsKeyword(kwd) && this.IsNullNamespace();
		}

		// Token: 0x06003980 RID: 14720 RVA: 0x001420F8 File Offset: 0x001402F8
		public bool IsXsltKeyword(string kwd)
		{
			return this.IsKeyword(kwd) && this.IsXsltNamespace();
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06003981 RID: 14721 RVA: 0x0014210B File Offset: 0x0014030B
		// (set) Token: 0x06003982 RID: 14722 RVA: 0x00142118 File Offset: 0x00140318
		public bool CanHaveApplyImports
		{
			get
			{
				return this.scopeManager.CanHaveApplyImports;
			}
			set
			{
				this.scopeManager.CanHaveApplyImports = value;
			}
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x00142126 File Offset: 0x00140326
		public bool IsExtensionNamespace(string uri)
		{
			return this.scopeManager.IsExNamespace(uri);
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06003984 RID: 14724 RVA: 0x00142134 File Offset: 0x00140334
		public bool ForwardCompatibility
		{
			get
			{
				return this.scopeManager.ForwardCompatibility;
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06003985 RID: 14725 RVA: 0x00142141 File Offset: 0x00140341
		public bool BackwardCompatibility
		{
			get
			{
				return this.scopeManager.BackwardCompatibility;
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06003986 RID: 14726 RVA: 0x0014214E File Offset: 0x0014034E
		public XslVersion XslVersion
		{
			get
			{
				if (!this.scopeManager.ForwardCompatibility)
				{
					return XslVersion.Version10;
				}
				return XslVersion.ForwardsCompatible;
			}
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x00142160 File Offset: 0x00140360
		private void SetVersion(int attVersion)
		{
			this.MoveToLiteralAttribute(attVersion);
			double num = XPathConvert.StringToDouble(this.Value);
			if (double.IsNaN(num))
			{
				this.ReportError("'{1}' is an invalid value for the '{0}' attribute.", new string[]
				{
					this.atoms.Version,
					this.Value
				});
				num = 1.0;
			}
			this.SetVersion(num);
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x001421C4 File Offset: 0x001403C4
		private void SetVersion(double version)
		{
			if (this.compiler.Version == 0)
			{
				this.compiler.Version = 1;
			}
			if (this.compiler.Version == 1)
			{
				this.scopeManager.BackwardCompatibility = false;
				this.scopeManager.ForwardCompatibility = version != 1.0;
				return;
			}
			this.scopeManager.BackwardCompatibility = version < 2.0;
			this.scopeManager.ForwardCompatibility = 2.0 < version;
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x0014224D File Offset: 0x0014044D
		public XsltInput.ContextInfo GetAttributes()
		{
			return this.GetAttributes(XsltInput.noAttributes);
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x0014225C File Offset: 0x0014045C
		public XsltInput.ContextInfo GetAttributes(XsltInput.XsltAttribute[] attributes)
		{
			this.attributes = attributes;
			this.records[0].value = null;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			bool flag = this.IsXsltNamespace() && this.IsKeyword(this.atoms.Output);
			bool flag2 = this.IsXsltNamespace() && (this.IsKeyword(this.atoms.Stylesheet) || this.IsKeyword(this.atoms.Transform));
			bool flag3 = this.compiler.Version == 2;
			for (int i = 0; i < attributes.Length; i++)
			{
				this.xsltAttributeNumber[i] = 0;
			}
			this.compiler.EnterForwardsCompatible();
			if (flag2 || (flag3 && !flag))
			{
				int num6 = 1;
				while (this.MoveToAttributeBase(num6))
				{
					if (this.IsNullNamespace() && this.IsKeyword(this.atoms.Version))
					{
						this.SetVersion(num6);
						break;
					}
					num6++;
				}
			}
			if (this.compiler.Version == 0)
			{
				this.SetVersion(1.0);
			}
			flag3 = this.compiler.Version == 2;
			int num7 = (flag3 ? (XsltLoader.V2Opt | XsltLoader.V2Req) : (XsltLoader.V1Opt | XsltLoader.V1Req));
			int num8 = 1;
			while (this.MoveToAttributeBase(num8))
			{
				if (this.IsNullNamespace())
				{
					string localName = this.LocalName;
					int j;
					for (j = 0; j < attributes.Length; j++)
					{
						if (Ref.Equal(localName, attributes[j].name) && (attributes[j].flags & num7) != 0)
						{
							this.xsltAttributeNumber[j] = num8;
							break;
						}
					}
					if (j == attributes.Length)
					{
						if (Ref.Equal(localName, this.atoms.ExcludeResultPrefixes) && (flag2 || flag3))
						{
							num2 = num8;
						}
						else if (Ref.Equal(localName, this.atoms.ExtensionElementPrefixes) && (flag2 || flag3))
						{
							num = num8;
						}
						else if (Ref.Equal(localName, this.atoms.XPathDefaultNamespace) && flag3)
						{
							num3 = num8;
						}
						else if (Ref.Equal(localName, this.atoms.DefaultCollation) && flag3)
						{
							num4 = num8;
						}
						else if (Ref.Equal(localName, this.atoms.UseWhen) && flag3)
						{
							num5 = num8;
						}
						else
						{
							this.ReportError("'{0}' is an invalid attribute for the '{1}' element.", new string[]
							{
								this.QualifiedName,
								this.records[0].QualifiedName
							});
						}
					}
				}
				else if (this.IsXsltNamespace())
				{
					this.ReportError("'{0}' is an invalid attribute for the '{1}' element.", new string[]
					{
						this.QualifiedName,
						this.records[0].QualifiedName
					});
				}
				num8++;
			}
			this.attributesRead = true;
			this.compiler.ExitForwardsCompatible(this.ForwardCompatibility);
			this.InsertExNamespaces(num, this.ctxInfo, true);
			this.InsertExNamespaces(num2, this.ctxInfo, false);
			this.SetXPathDefaultNamespace(num3);
			this.SetDefaultCollation(num4);
			if (num5 != 0)
			{
				this.ReportNYI(this.atoms.UseWhen);
			}
			this.MoveToElement();
			for (int k = 0; k < attributes.Length; k++)
			{
				if (this.xsltAttributeNumber[k] == 0)
				{
					int flags = attributes[k].flags;
					if ((this.compiler.Version == 2 && (flags & XsltLoader.V2Req) != 0) || (this.compiler.Version == 1 && (flags & XsltLoader.V1Req) != 0 && (!this.ForwardCompatibility || (flags & XsltLoader.V2Req) != 0)))
					{
						this.ReportError("Missing mandatory attribute '{0}'.", new string[] { attributes[k].name });
					}
				}
			}
			return this.ctxInfo;
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x00142628 File Offset: 0x00140828
		public XsltInput.ContextInfo GetLiteralAttributes(bool asStylesheet)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 1;
			while (this.MoveToLiteralAttribute(num7))
			{
				if (this.IsXsltNamespace())
				{
					string localName = this.LocalName;
					if (Ref.Equal(localName, this.atoms.Version))
					{
						num = num7;
					}
					else if (Ref.Equal(localName, this.atoms.ExtensionElementPrefixes))
					{
						num2 = num7;
					}
					else if (Ref.Equal(localName, this.atoms.ExcludeResultPrefixes))
					{
						num3 = num7;
					}
					else if (Ref.Equal(localName, this.atoms.XPathDefaultNamespace))
					{
						num4 = num7;
					}
					else if (Ref.Equal(localName, this.atoms.DefaultCollation))
					{
						num5 = num7;
					}
					else if (Ref.Equal(localName, this.atoms.UseWhen))
					{
						num6 = num7;
					}
				}
				num7++;
			}
			this.attributesRead = true;
			this.MoveToElement();
			if (num != 0)
			{
				this.SetVersion(num);
			}
			else if (asStylesheet)
			{
				this.ReportError((Ref.Equal(this.NamespaceUri, this.atoms.UriWdXsl) && Ref.Equal(this.LocalName, this.atoms.Stylesheet)) ? "The 'http://www.w3.org/TR/WD-xsl' namespace is no longer supported." : "Stylesheet must start either with an 'xsl:stylesheet' or an 'xsl:transform' element, or with a literal result element that has an 'xsl:version' attribute, where prefix 'xsl' denotes the 'http://www.w3.org/1999/XSL/Transform' namespace.", Array.Empty<string>());
				this.SetVersion(1.0);
			}
			this.InsertExNamespaces(num2, this.ctxInfo, true);
			if (!this.IsExtensionNamespace(this.records[0].nsUri))
			{
				if (this.compiler.Version == 2)
				{
					this.SetXPathDefaultNamespace(num4);
					this.SetDefaultCollation(num5);
					if (num6 != 0)
					{
						this.ReportNYI(this.atoms.UseWhen);
					}
				}
				this.InsertExNamespaces(num3, this.ctxInfo, false);
			}
			return this.ctxInfo;
		}

		// Token: 0x0600398C RID: 14732 RVA: 0x001427E8 File Offset: 0x001409E8
		public void GetVersionAttribute()
		{
			if (this.compiler.Version == 2)
			{
				int num = 1;
				while (this.MoveToAttributeBase(num))
				{
					if (this.IsNullNamespace() && this.IsKeyword(this.atoms.Version))
					{
						this.SetVersion(num);
						break;
					}
					num++;
				}
			}
			this.attributesRead = true;
		}

		// Token: 0x0600398D RID: 14733 RVA: 0x00142844 File Offset: 0x00140A44
		private void InsertExNamespaces(int attExPrefixes, XsltInput.ContextInfo ctxInfo, bool extensions)
		{
			if (this.MoveToLiteralAttribute(attExPrefixes))
			{
				string value = this.Value;
				if (value.Length != 0)
				{
					if (!extensions && this.compiler.Version != 1 && value == "#all")
					{
						ctxInfo.nsList = new NsDecl(ctxInfo.nsList, null, null);
						return;
					}
					this.compiler.EnterForwardsCompatible();
					string[] array = XmlConvert.SplitString(value);
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] == "#default")
						{
							array[i] = this.LookupXmlNamespace(string.Empty);
							if (array[i].Length == 0 && this.compiler.Version != 1 && !this.BackwardCompatibility)
							{
								this.ReportError("Value '#default' is used within the 'exclude-result-prefixes' attribute and the parent element of this attribute has no default namespace.", Array.Empty<string>());
							}
						}
						else
						{
							array[i] = this.LookupXmlNamespace(array[i]);
						}
					}
					if (!this.compiler.ExitForwardsCompatible(this.ForwardCompatibility))
					{
						return;
					}
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j] != null)
						{
							ctxInfo.nsList = new NsDecl(ctxInfo.nsList, null, array[j]);
							if (extensions)
							{
								this.scopeManager.AddExNamespace(array[j]);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600398E RID: 14734 RVA: 0x00142969 File Offset: 0x00140B69
		private void SetXPathDefaultNamespace(int attNamespace)
		{
			if (this.MoveToLiteralAttribute(attNamespace) && this.Value.Length != 0)
			{
				this.ReportNYI(this.atoms.XPathDefaultNamespace);
			}
		}

		// Token: 0x0600398F RID: 14735 RVA: 0x00142994 File Offset: 0x00140B94
		private void SetDefaultCollation(int attCollation)
		{
			if (this.MoveToLiteralAttribute(attCollation))
			{
				string[] array = XmlConvert.SplitString(this.Value);
				int num = 0;
				while (num < array.Length && XmlCollation.Create(array[num], false) == null)
				{
					num++;
				}
				if (num == array.Length)
				{
					this.ReportErrorFC("The value of an 'default-collation' attribute contains no recognized collation URI.", Array.Empty<string>());
					return;
				}
				if (array[num] != "http://www.w3.org/2004/10/xpath-functions/collation/codepoint")
				{
					this.ReportNYI(this.atoms.DefaultCollation);
				}
			}
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x00142A06 File Offset: 0x00140C06
		private static int PositionAdjustment(XmlNodeType nt)
		{
			switch (nt)
			{
			case XmlNodeType.Element:
				return 1;
			case XmlNodeType.Attribute:
			case XmlNodeType.Text:
			case XmlNodeType.Entity:
				break;
			case XmlNodeType.CDATA:
				return 9;
			case XmlNodeType.EntityReference:
				return 1;
			case XmlNodeType.ProcessingInstruction:
				return 2;
			case XmlNodeType.Comment:
				return 4;
			default:
				if (nt == XmlNodeType.EndElement)
				{
					return 2;
				}
				break;
			}
			return 0;
		}

		// Token: 0x06003991 RID: 14737 RVA: 0x00142A45 File Offset: 0x00140C45
		public ISourceLineInfo BuildLineInfo()
		{
			return new SourceLineInfo(this.Uri, this.Start, this.End);
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x00142A60 File Offset: 0x00140C60
		public ISourceLineInfo BuildNameLineInfo()
		{
			if (this.readerLineInfo == null)
			{
				return this.BuildLineInfo();
			}
			if (this.LocalName == null)
			{
				this.FillupRecord(ref this.records[this.currentRecord]);
			}
			Location start = this.Start;
			int line = start.Line;
			int num = start.Pos + XsltInput.PositionAdjustment(this.NodeType);
			return new SourceLineInfo(this.Uri, new Location(line, num), new Location(line, num + this.QualifiedName.Length));
		}

		// Token: 0x06003993 RID: 14739 RVA: 0x00142AE4 File Offset: 0x00140CE4
		public ISourceLineInfo BuildReaderLineInfo()
		{
			Location location;
			if (this.readerLineInfo != null)
			{
				location = new Location(this.readerLineInfo.LineNumber, this.readerLineInfo.LinePosition);
			}
			else
			{
				location = new Location(0, 0);
			}
			return new SourceLineInfo(this.reader.BaseURI, location, location);
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x00142B34 File Offset: 0x00140D34
		public string LookupXmlNamespace(string prefix)
		{
			string text = this.scopeManager.LookupNamespace(prefix);
			if (text != null)
			{
				return text;
			}
			if (prefix.Length == 0)
			{
				return string.Empty;
			}
			this.ReportError("Prefix '{0}' is not defined.", new string[] { prefix });
			return null;
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x00142B77 File Offset: 0x00140D77
		public void ReportError(string res, params string[] args)
		{
			this.compiler.ReportError(this.BuildNameLineInfo(), res, args);
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x00142B8C File Offset: 0x00140D8C
		public void ReportErrorFC(string res, params string[] args)
		{
			if (!this.ForwardCompatibility)
			{
				this.compiler.ReportError(this.BuildNameLineInfo(), res, args);
			}
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x00142BA9 File Offset: 0x00140DA9
		public void ReportWarning(string res, params string[] args)
		{
			this.compiler.ReportWarning(this.BuildNameLineInfo(), res, args);
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x00142BBE File Offset: 0x00140DBE
		private void ReportNYI(string arg)
		{
			this.ReportErrorFC("'{0}' is not yet implemented.", new string[] { arg });
		}

		// Token: 0x0400253A RID: 9530
		private const int InitRecordsSize = 22;

		// Token: 0x0400253B RID: 9531
		private XmlReader reader;

		// Token: 0x0400253C RID: 9532
		private IXmlLineInfo readerLineInfo;

		// Token: 0x0400253D RID: 9533
		private bool topLevelReader;

		// Token: 0x0400253E RID: 9534
		private CompilerScopeManager<VarPar> scopeManager;

		// Token: 0x0400253F RID: 9535
		private KeywordsTable atoms;

		// Token: 0x04002540 RID: 9536
		private Compiler compiler;

		// Token: 0x04002541 RID: 9537
		private bool reatomize;

		// Token: 0x04002542 RID: 9538
		private XmlNodeType nodeType;

		// Token: 0x04002543 RID: 9539
		private XsltInput.Record[] records = new XsltInput.Record[22];

		// Token: 0x04002544 RID: 9540
		private int currentRecord;

		// Token: 0x04002545 RID: 9541
		private bool isEmptyElement;

		// Token: 0x04002546 RID: 9542
		private int lastTextNode;

		// Token: 0x04002547 RID: 9543
		private int numAttributes;

		// Token: 0x04002548 RID: 9544
		private XsltInput.ContextInfo ctxInfo;

		// Token: 0x04002549 RID: 9545
		private bool attributesRead;

		// Token: 0x0400254A RID: 9546
		private StringConcat strConcat;

		// Token: 0x0400254B RID: 9547
		private XsltInput.XsltAttribute[] attributes;

		// Token: 0x0400254C RID: 9548
		private int[] xsltAttributeNumber = new int[21];

		// Token: 0x0400254D RID: 9549
		private static XsltInput.XsltAttribute[] noAttributes = new XsltInput.XsltAttribute[0];

		// Token: 0x020005AC RID: 1452
		public struct DelayedQName
		{
			// Token: 0x0600399A RID: 14746 RVA: 0x00142BE2 File Offset: 0x00140DE2
			public DelayedQName(ref XsltInput.Record rec)
			{
				this.prefix = rec.prefix;
				this.localName = rec.localName;
			}

			// Token: 0x0600399B RID: 14747 RVA: 0x00142BFC File Offset: 0x00140DFC
			public static implicit operator string(XsltInput.DelayedQName qn)
			{
				if (qn.prefix.Length != 0)
				{
					return qn.prefix + ":" + qn.localName;
				}
				return qn.localName;
			}

			// Token: 0x0400254E RID: 9550
			private string prefix;

			// Token: 0x0400254F RID: 9551
			private string localName;
		}

		// Token: 0x020005AD RID: 1453
		public struct XsltAttribute
		{
			// Token: 0x0600399C RID: 14748 RVA: 0x00142C28 File Offset: 0x00140E28
			public XsltAttribute(string name, int flags)
			{
				this.name = name;
				this.flags = flags;
			}

			// Token: 0x04002550 RID: 9552
			public string name;

			// Token: 0x04002551 RID: 9553
			public int flags;
		}

		// Token: 0x020005AE RID: 1454
		internal class ContextInfo
		{
			// Token: 0x0600399D RID: 14749 RVA: 0x00142C38 File Offset: 0x00140E38
			internal ContextInfo(ISourceLineInfo lineinfo)
			{
				this.elemNameLi = lineinfo;
				this.endTagLi = lineinfo;
				this.lineInfo = lineinfo;
			}

			// Token: 0x0600399E RID: 14750 RVA: 0x00142C55 File Offset: 0x00140E55
			public ContextInfo(XsltInput input)
			{
				this.elemNameLength = input.QualifiedName.Length;
			}

			// Token: 0x0600399F RID: 14751 RVA: 0x00142C6E File Offset: 0x00140E6E
			public void AddNamespace(string prefix, string nsUri)
			{
				this.nsList = new NsDecl(this.nsList, prefix, nsUri);
			}

			// Token: 0x060039A0 RID: 14752 RVA: 0x00142C84 File Offset: 0x00140E84
			public void SaveExtendedLineInfo(XsltInput input)
			{
				if (this.lineInfo.Start.Line == 0)
				{
					this.elemNameLi = (this.endTagLi = null);
					return;
				}
				this.elemNameLi = new SourceLineInfo(this.lineInfo.Uri, this.lineInfo.Start.Line, this.lineInfo.Start.Pos + 1, this.lineInfo.Start.Line, this.lineInfo.Start.Pos + 1 + this.elemNameLength);
				if (!input.IsEmptyElement)
				{
					this.endTagLi = input.BuildLineInfo();
					return;
				}
				this.endTagLi = new XsltInput.ContextInfo.EmptyElementEndTag(this.lineInfo);
			}

			// Token: 0x04002552 RID: 9554
			public NsDecl nsList;

			// Token: 0x04002553 RID: 9555
			public ISourceLineInfo lineInfo;

			// Token: 0x04002554 RID: 9556
			public ISourceLineInfo elemNameLi;

			// Token: 0x04002555 RID: 9557
			public ISourceLineInfo endTagLi;

			// Token: 0x04002556 RID: 9558
			private int elemNameLength;

			// Token: 0x020005AF RID: 1455
			internal class EmptyElementEndTag : ISourceLineInfo
			{
				// Token: 0x060039A1 RID: 14753 RVA: 0x00142D4A File Offset: 0x00140F4A
				public EmptyElementEndTag(ISourceLineInfo elementTagLi)
				{
					this.elementTagLi = elementTagLi;
				}

				// Token: 0x17000BDB RID: 3035
				// (get) Token: 0x060039A2 RID: 14754 RVA: 0x00142D59 File Offset: 0x00140F59
				public string Uri
				{
					get
					{
						return this.elementTagLi.Uri;
					}
				}

				// Token: 0x17000BDC RID: 3036
				// (get) Token: 0x060039A3 RID: 14755 RVA: 0x00142D66 File Offset: 0x00140F66
				public bool IsNoSource
				{
					get
					{
						return this.elementTagLi.IsNoSource;
					}
				}

				// Token: 0x17000BDD RID: 3037
				// (get) Token: 0x060039A4 RID: 14756 RVA: 0x00142D74 File Offset: 0x00140F74
				public Location Start
				{
					get
					{
						return new Location(this.elementTagLi.End.Line, this.elementTagLi.End.Pos - 2);
					}
				}

				// Token: 0x17000BDE RID: 3038
				// (get) Token: 0x060039A5 RID: 14757 RVA: 0x00142DAE File Offset: 0x00140FAE
				public Location End
				{
					get
					{
						return this.elementTagLi.End;
					}
				}

				// Token: 0x04002557 RID: 9559
				private ISourceLineInfo elementTagLi;
			}
		}

		// Token: 0x020005B0 RID: 1456
		internal struct Record
		{
			// Token: 0x17000BDF RID: 3039
			// (get) Token: 0x060039A6 RID: 14758 RVA: 0x00142DBB File Offset: 0x00140FBB
			public string QualifiedName
			{
				get
				{
					if (this.prefix.Length != 0)
					{
						return this.prefix + ":" + this.localName;
					}
					return this.localName;
				}
			}

			// Token: 0x04002558 RID: 9560
			public string localName;

			// Token: 0x04002559 RID: 9561
			public string nsUri;

			// Token: 0x0400255A RID: 9562
			public string prefix;

			// Token: 0x0400255B RID: 9563
			public string value;

			// Token: 0x0400255C RID: 9564
			public string baseUri;

			// Token: 0x0400255D RID: 9565
			public Location start;

			// Token: 0x0400255E RID: 9566
			public Location valueStart;

			// Token: 0x0400255F RID: 9567
			public Location end;
		}
	}
}
