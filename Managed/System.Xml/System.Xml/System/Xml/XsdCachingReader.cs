using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x020001E7 RID: 487
	internal class XsdCachingReader : XmlReader, IXmlLineInfo
	{
		// Token: 0x0600115F RID: 4447 RVA: 0x00066C20 File Offset: 0x00064E20
		internal XsdCachingReader(XmlReader reader, IXmlLineInfo lineInfo, CachingEventHandler handlerMethod)
		{
			this.coreReader = reader;
			this.lineInfo = lineInfo;
			this.cacheHandler = handlerMethod;
			this.attributeEvents = new ValidatingReaderNodeData[8];
			this.contentEvents = new ValidatingReaderNodeData[4];
			this.Init();
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00066C5C File Offset: 0x00064E5C
		private void Init()
		{
			this.coreReaderNameTable = this.coreReader.NameTable;
			this.cacheState = XsdCachingReader.CachingReaderState.Init;
			this.contentIndex = 0;
			this.currentAttrIndex = -1;
			this.currentContentIndex = -1;
			this.attributeCount = 0;
			this.cachedNode = null;
			this.readAhead = false;
			if (this.coreReader.NodeType == XmlNodeType.Element)
			{
				ValidatingReaderNodeData validatingReaderNodeData = this.AddContent(this.coreReader.NodeType);
				validatingReaderNodeData.SetItemData(this.coreReader.LocalName, this.coreReader.Prefix, this.coreReader.NamespaceURI, this.coreReader.Depth);
				validatingReaderNodeData.SetLineInfo(this.lineInfo);
				this.RecordAttributes();
			}
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00066D0D File Offset: 0x00064F0D
		internal void Reset(XmlReader reader)
		{
			this.coreReader = reader;
			this.Init();
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x00066D1C File Offset: 0x00064F1C
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.coreReader.Settings;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x00066D29 File Offset: 0x00064F29
		public override XmlNodeType NodeType
		{
			get
			{
				return this.cachedNode.NodeType;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x00066D36 File Offset: 0x00064F36
		public override string Name
		{
			get
			{
				return this.cachedNode.GetAtomizedNameWPrefix(this.coreReaderNameTable);
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x00066D49 File Offset: 0x00064F49
		public override string LocalName
		{
			get
			{
				return this.cachedNode.LocalName;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x00066D56 File Offset: 0x00064F56
		public override string NamespaceURI
		{
			get
			{
				return this.cachedNode.Namespace;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x00066D63 File Offset: 0x00064F63
		public override string Prefix
		{
			get
			{
				return this.cachedNode.Prefix;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x00066D70 File Offset: 0x00064F70
		public override bool HasValue
		{
			get
			{
				return XmlReader.HasValueInternal(this.cachedNode.NodeType);
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x00066D82 File Offset: 0x00064F82
		public override string Value
		{
			get
			{
				if (!this.returnOriginalStringValues)
				{
					return this.cachedNode.RawValue;
				}
				return this.cachedNode.OriginalStringValue;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x00066DA3 File Offset: 0x00064FA3
		public override int Depth
		{
			get
			{
				return this.cachedNode.Depth;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x00066DB0 File Offset: 0x00064FB0
		public override string BaseURI
		{
			get
			{
				return this.coreReader.BaseURI;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x0600116C RID: 4460 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool IsEmptyElement
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool IsDefault
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600116E RID: 4462 RVA: 0x00066DBD File Offset: 0x00064FBD
		public override char QuoteChar
		{
			get
			{
				return this.coreReader.QuoteChar;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x0600116F RID: 4463 RVA: 0x00066DCA File Offset: 0x00064FCA
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.coreReader.XmlSpace;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x00066DD7 File Offset: 0x00064FD7
		public override string XmlLang
		{
			get
			{
				return this.coreReader.XmlLang;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001171 RID: 4465 RVA: 0x00066DE4 File Offset: 0x00064FE4
		public override int AttributeCount
		{
			get
			{
				return this.attributeCount;
			}
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00066DEC File Offset: 0x00064FEC
		public override string GetAttribute(string name)
		{
			int num;
			if (name.IndexOf(':') == -1)
			{
				num = this.GetAttributeIndexWithoutPrefix(name);
			}
			else
			{
				num = this.GetAttributeIndexWithPrefix(name);
			}
			if (num < 0)
			{
				return null;
			}
			return this.attributeEvents[num].RawValue;
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00066E2C File Offset: 0x0006502C
		public override string GetAttribute(string name, string namespaceURI)
		{
			namespaceURI = ((namespaceURI == null) ? string.Empty : this.coreReaderNameTable.Get(namespaceURI));
			name = this.coreReaderNameTable.Get(name);
			for (int i = 0; i < this.attributeCount; i++)
			{
				ValidatingReaderNodeData validatingReaderNodeData = this.attributeEvents[i];
				if (Ref.Equal(validatingReaderNodeData.LocalName, name) && Ref.Equal(validatingReaderNodeData.Namespace, namespaceURI))
				{
					return validatingReaderNodeData.RawValue;
				}
			}
			return null;
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00066E9D File Offset: 0x0006509D
		public override string GetAttribute(int i)
		{
			if (i < 0 || i >= this.attributeCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			return this.attributeEvents[i].RawValue;
		}

		// Token: 0x170002FF RID: 767
		public override string this[int i]
		{
			get
			{
				return this.GetAttribute(i);
			}
		}

		// Token: 0x17000300 RID: 768
		public override string this[string name]
		{
			get
			{
				return this.GetAttribute(name);
			}
		}

		// Token: 0x17000301 RID: 769
		public override string this[string name, string namespaceURI]
		{
			get
			{
				return this.GetAttribute(name, namespaceURI);
			}
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00066EC4 File Offset: 0x000650C4
		public override bool MoveToAttribute(string name)
		{
			int num;
			if (name.IndexOf(':') == -1)
			{
				num = this.GetAttributeIndexWithoutPrefix(name);
			}
			else
			{
				num = this.GetAttributeIndexWithPrefix(name);
			}
			if (num >= 0)
			{
				this.currentAttrIndex = num;
				this.cachedNode = this.attributeEvents[num];
				return true;
			}
			return false;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00066F0C File Offset: 0x0006510C
		public override bool MoveToAttribute(string name, string ns)
		{
			ns = ((ns == null) ? string.Empty : this.coreReaderNameTable.Get(ns));
			name = this.coreReaderNameTable.Get(name);
			for (int i = 0; i < this.attributeCount; i++)
			{
				ValidatingReaderNodeData validatingReaderNodeData = this.attributeEvents[i];
				if (Ref.Equal(validatingReaderNodeData.LocalName, name) && Ref.Equal(validatingReaderNodeData.Namespace, ns))
				{
					this.currentAttrIndex = i;
					this.cachedNode = this.attributeEvents[i];
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00066F8D File Offset: 0x0006518D
		public override void MoveToAttribute(int i)
		{
			if (i < 0 || i >= this.attributeCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			this.currentAttrIndex = i;
			this.cachedNode = this.attributeEvents[i];
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00066FBC File Offset: 0x000651BC
		public override bool MoveToFirstAttribute()
		{
			if (this.attributeCount == 0)
			{
				return false;
			}
			this.currentAttrIndex = 0;
			this.cachedNode = this.attributeEvents[0];
			return true;
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00066FE0 File Offset: 0x000651E0
		public override bool MoveToNextAttribute()
		{
			if (this.currentAttrIndex + 1 < this.attributeCount)
			{
				ValidatingReaderNodeData[] array = this.attributeEvents;
				int num = this.currentAttrIndex + 1;
				this.currentAttrIndex = num;
				this.cachedNode = array[num];
				return true;
			}
			return false;
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0006701E File Offset: 0x0006521E
		public override bool MoveToElement()
		{
			if (this.cacheState != XsdCachingReader.CachingReaderState.Replay || this.cachedNode.NodeType != XmlNodeType.Attribute)
			{
				return false;
			}
			this.currentContentIndex = 0;
			this.currentAttrIndex = -1;
			this.Read();
			return true;
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00067050 File Offset: 0x00065250
		public override bool Read()
		{
			switch (this.cacheState)
			{
			case XsdCachingReader.CachingReaderState.Init:
				this.cacheState = XsdCachingReader.CachingReaderState.Record;
				break;
			case XsdCachingReader.CachingReaderState.Record:
				break;
			case XsdCachingReader.CachingReaderState.Replay:
				if (this.currentContentIndex >= this.contentIndex)
				{
					this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
					this.cacheHandler(this);
					return (this.coreReader.NodeType == XmlNodeType.Element && !this.readAhead) || this.coreReader.Read();
				}
				this.cachedNode = this.contentEvents[this.currentContentIndex];
				if (this.currentContentIndex > 0)
				{
					this.ClearAttributesInfo();
				}
				this.currentContentIndex++;
				return true;
			default:
				return false;
			}
			ValidatingReaderNodeData validatingReaderNodeData = null;
			if (this.coreReader.Read())
			{
				switch (this.coreReader.NodeType)
				{
				case XmlNodeType.Element:
					this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
					return false;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					validatingReaderNodeData = this.AddContent(this.coreReader.NodeType);
					validatingReaderNodeData.SetItemData(this.coreReader.Value);
					validatingReaderNodeData.SetLineInfo(this.lineInfo);
					validatingReaderNodeData.Depth = this.coreReader.Depth;
					break;
				case XmlNodeType.EndElement:
					validatingReaderNodeData = this.AddContent(this.coreReader.NodeType);
					validatingReaderNodeData.SetItemData(this.coreReader.LocalName, this.coreReader.Prefix, this.coreReader.NamespaceURI, this.coreReader.Depth);
					validatingReaderNodeData.SetLineInfo(this.lineInfo);
					break;
				}
				this.cachedNode = validatingReaderNodeData;
				return true;
			}
			this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
			return false;
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00067210 File Offset: 0x00065410
		internal ValidatingReaderNodeData RecordTextNode(string textValue, string originalStringValue, int depth, int lineNo, int linePos)
		{
			ValidatingReaderNodeData validatingReaderNodeData = this.AddContent(XmlNodeType.Text);
			validatingReaderNodeData.SetItemData(textValue, originalStringValue);
			validatingReaderNodeData.SetLineInfo(lineNo, linePos);
			validatingReaderNodeData.Depth = depth;
			return validatingReaderNodeData;
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00067234 File Offset: 0x00065434
		internal void SwitchTextNodeAndEndElement(string textValue, string originalStringValue)
		{
			ValidatingReaderNodeData validatingReaderNodeData = this.RecordTextNode(textValue, originalStringValue, this.coreReader.Depth + 1, 0, 0);
			int num = this.contentIndex - 2;
			ValidatingReaderNodeData validatingReaderNodeData2 = this.contentEvents[num];
			this.contentEvents[num] = validatingReaderNodeData;
			this.contentEvents[this.contentIndex - 1] = validatingReaderNodeData2;
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00067284 File Offset: 0x00065484
		internal void RecordEndElementNode()
		{
			ValidatingReaderNodeData validatingReaderNodeData = this.AddContent(XmlNodeType.EndElement);
			validatingReaderNodeData.SetItemData(this.coreReader.LocalName, this.coreReader.Prefix, this.coreReader.NamespaceURI, this.coreReader.Depth);
			validatingReaderNodeData.SetLineInfo(this.coreReader as IXmlLineInfo);
			if (this.coreReader.IsEmptyElement)
			{
				this.readAhead = true;
			}
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x000672EF File Offset: 0x000654EF
		internal string ReadOriginalContentAsString()
		{
			this.returnOriginalStringValues = true;
			string text = base.InternalReadContentAsString();
			this.returnOriginalStringValues = false;
			return text;
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x00067305 File Offset: 0x00065505
		public override bool EOF
		{
			get
			{
				return this.cacheState == XsdCachingReader.CachingReaderState.ReaderClosed && this.coreReader.EOF;
			}
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0006731D File Offset: 0x0006551D
		public override void Close()
		{
			this.coreReader.Close();
			this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x00067331 File Offset: 0x00065531
		public override ReadState ReadState
		{
			get
			{
				return this.coreReader.ReadState;
			}
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00067340 File Offset: 0x00065540
		public override void Skip()
		{
			XmlNodeType nodeType = this.cachedNode.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Attribute)
				{
					this.Read();
					return;
				}
				this.MoveToElement();
			}
			if (this.coreReader.NodeType != XmlNodeType.EndElement && !this.readAhead)
			{
				int num = this.coreReader.Depth - 1;
				while (this.coreReader.Read() && this.coreReader.Depth > num)
				{
				}
			}
			this.coreReader.Read();
			this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
			this.cacheHandler(this);
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x000673D3 File Offset: 0x000655D3
		public override XmlNameTable NameTable
		{
			get
			{
				return this.coreReaderNameTable;
			}
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x000673DB File Offset: 0x000655DB
		public override string LookupNamespace(string prefix)
		{
			return this.coreReader.LookupNamespace(prefix);
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x00007944 File Offset: 0x00005B44
		public override void ResolveEntity()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x000673E9 File Offset: 0x000655E9
		public override bool ReadAttributeValue()
		{
			if (this.cachedNode.NodeType != XmlNodeType.Attribute)
			{
				return false;
			}
			this.cachedNode = this.CreateDummyTextNode(this.cachedNode.RawValue, this.cachedNode.Depth + 1);
			return true;
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00003242 File Offset: 0x00001442
		bool IXmlLineInfo.HasLineInfo()
		{
			return true;
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x00067420 File Offset: 0x00065620
		int IXmlLineInfo.LineNumber
		{
			get
			{
				return this.cachedNode.LineNumber;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x0006742D File Offset: 0x0006562D
		int IXmlLineInfo.LinePosition
		{
			get
			{
				return this.cachedNode.LinePosition;
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0006743A File Offset: 0x0006563A
		internal void SetToReplayMode()
		{
			this.cacheState = XsdCachingReader.CachingReaderState.Replay;
			this.currentContentIndex = 0;
			this.currentAttrIndex = -1;
			this.Read();
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00067458 File Offset: 0x00065658
		internal XmlReader GetCoreReader()
		{
			return this.coreReader;
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00067460 File Offset: 0x00065660
		internal IXmlLineInfo GetLineInfo()
		{
			return this.lineInfo;
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00067468 File Offset: 0x00065668
		private void ClearAttributesInfo()
		{
			this.attributeCount = 0;
			this.currentAttrIndex = -1;
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00067478 File Offset: 0x00065678
		private ValidatingReaderNodeData AddAttribute(int attIndex)
		{
			ValidatingReaderNodeData validatingReaderNodeData = this.attributeEvents[attIndex];
			if (validatingReaderNodeData != null)
			{
				validatingReaderNodeData.Clear(XmlNodeType.Attribute);
				return validatingReaderNodeData;
			}
			if (attIndex >= this.attributeEvents.Length - 1)
			{
				ValidatingReaderNodeData[] array = new ValidatingReaderNodeData[this.attributeEvents.Length * 2];
				Array.Copy(this.attributeEvents, 0, array, 0, this.attributeEvents.Length);
				this.attributeEvents = array;
			}
			validatingReaderNodeData = this.attributeEvents[attIndex];
			if (validatingReaderNodeData == null)
			{
				validatingReaderNodeData = new ValidatingReaderNodeData(XmlNodeType.Attribute);
				this.attributeEvents[attIndex] = validatingReaderNodeData;
			}
			return validatingReaderNodeData;
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x000674F4 File Offset: 0x000656F4
		private ValidatingReaderNodeData AddContent(XmlNodeType nodeType)
		{
			ValidatingReaderNodeData validatingReaderNodeData = this.contentEvents[this.contentIndex];
			if (validatingReaderNodeData != null)
			{
				validatingReaderNodeData.Clear(nodeType);
				this.contentIndex++;
				return validatingReaderNodeData;
			}
			if (this.contentIndex >= this.contentEvents.Length - 1)
			{
				ValidatingReaderNodeData[] array = new ValidatingReaderNodeData[this.contentEvents.Length * 2];
				Array.Copy(this.contentEvents, 0, array, 0, this.contentEvents.Length);
				this.contentEvents = array;
			}
			validatingReaderNodeData = this.contentEvents[this.contentIndex];
			if (validatingReaderNodeData == null)
			{
				validatingReaderNodeData = new ValidatingReaderNodeData(nodeType);
				this.contentEvents[this.contentIndex] = validatingReaderNodeData;
			}
			this.contentIndex++;
			return validatingReaderNodeData;
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x000675A0 File Offset: 0x000657A0
		private void RecordAttributes()
		{
			this.attributeCount = this.coreReader.AttributeCount;
			if (this.coreReader.MoveToFirstAttribute())
			{
				int num = 0;
				do
				{
					ValidatingReaderNodeData validatingReaderNodeData = this.AddAttribute(num);
					validatingReaderNodeData.SetItemData(this.coreReader.LocalName, this.coreReader.Prefix, this.coreReader.NamespaceURI, this.coreReader.Depth);
					validatingReaderNodeData.SetLineInfo(this.lineInfo);
					validatingReaderNodeData.RawValue = this.coreReader.Value;
					num++;
				}
				while (this.coreReader.MoveToNextAttribute());
				this.coreReader.MoveToElement();
			}
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x00067640 File Offset: 0x00065840
		private int GetAttributeIndexWithoutPrefix(string name)
		{
			name = this.coreReaderNameTable.Get(name);
			if (name == null)
			{
				return -1;
			}
			for (int i = 0; i < this.attributeCount; i++)
			{
				ValidatingReaderNodeData validatingReaderNodeData = this.attributeEvents[i];
				if (Ref.Equal(validatingReaderNodeData.LocalName, name) && validatingReaderNodeData.Prefix.Length == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00067698 File Offset: 0x00065898
		private int GetAttributeIndexWithPrefix(string name)
		{
			name = this.coreReaderNameTable.Get(name);
			if (name == null)
			{
				return -1;
			}
			for (int i = 0; i < this.attributeCount; i++)
			{
				if (Ref.Equal(this.attributeEvents[i].GetAtomizedNameWPrefix(this.coreReaderNameTable), name))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x000676E7 File Offset: 0x000658E7
		private ValidatingReaderNodeData CreateDummyTextNode(string attributeValue, int depth)
		{
			if (this.textNode == null)
			{
				this.textNode = new ValidatingReaderNodeData(XmlNodeType.Text);
			}
			this.textNode.Depth = depth;
			this.textNode.RawValue = attributeValue;
			return this.textNode;
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x0006771B File Offset: 0x0006591B
		public override Task<string> GetValueAsync()
		{
			if (this.returnOriginalStringValues)
			{
				return Task.FromResult<string>(this.cachedNode.OriginalStringValue);
			}
			return Task.FromResult<string>(this.cachedNode.RawValue);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00067748 File Offset: 0x00065948
		public override async Task<bool> ReadAsync()
		{
			switch (this.cacheState)
			{
			case XsdCachingReader.CachingReaderState.Init:
				this.cacheState = XsdCachingReader.CachingReaderState.Record;
				break;
			case XsdCachingReader.CachingReaderState.Record:
				break;
			case XsdCachingReader.CachingReaderState.Replay:
				if (this.currentContentIndex < this.contentIndex)
				{
					this.cachedNode = this.contentEvents[this.currentContentIndex];
					if (this.currentContentIndex > 0)
					{
						this.ClearAttributesInfo();
					}
					this.currentContentIndex++;
					return true;
				}
				this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
				this.cacheHandler(this);
				if (this.coreReader.NodeType != XmlNodeType.Element || this.readAhead)
				{
					return await this.coreReader.ReadAsync().ConfigureAwait(false);
				}
				return true;
			default:
				return false;
			}
			ValidatingReaderNodeData recordedNode = null;
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.coreReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
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
				switch (this.coreReader.NodeType)
				{
				case XmlNodeType.Element:
					this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
					return false;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
				{
					recordedNode = this.AddContent(this.coreReader.NodeType);
					ValidatingReaderNodeData validatingReaderNodeData = recordedNode;
					validatingReaderNodeData.SetItemData(await this.coreReader.GetValueAsync().ConfigureAwait(false));
					validatingReaderNodeData = null;
					recordedNode.SetLineInfo(this.lineInfo);
					recordedNode.Depth = this.coreReader.Depth;
					break;
				}
				case XmlNodeType.EndElement:
					recordedNode = this.AddContent(this.coreReader.NodeType);
					recordedNode.SetItemData(this.coreReader.LocalName, this.coreReader.Prefix, this.coreReader.NamespaceURI, this.coreReader.Depth);
					recordedNode.SetLineInfo(this.lineInfo);
					break;
				}
				this.cachedNode = recordedNode;
				flag = true;
			}
			else
			{
				this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00067790 File Offset: 0x00065990
		public override async Task SkipAsync()
		{
			XmlNodeType nodeType = this.cachedNode.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Attribute)
				{
					await this.ReadAsync().ConfigureAwait(false);
					return;
				}
				this.MoveToElement();
			}
			if (this.coreReader.NodeType != XmlNodeType.EndElement && !this.readAhead)
			{
				int startDepth = this.coreReader.Depth - 1;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter;
				do
				{
					configuredTaskAwaiter = this.coreReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
					}
				}
				while (configuredTaskAwaiter.GetResult() && this.coreReader.Depth > startDepth);
			}
			await this.coreReader.ReadAsync().ConfigureAwait(false);
			this.cacheState = XsdCachingReader.CachingReaderState.ReaderClosed;
			this.cacheHandler(this);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x000677D5 File Offset: 0x000659D5
		internal Task SetToReplayModeAsync()
		{
			this.cacheState = XsdCachingReader.CachingReaderState.Replay;
			this.currentContentIndex = 0;
			this.currentAttrIndex = -1;
			return this.ReadAsync();
		}

		// Token: 0x04000C53 RID: 3155
		private XmlReader coreReader;

		// Token: 0x04000C54 RID: 3156
		private XmlNameTable coreReaderNameTable;

		// Token: 0x04000C55 RID: 3157
		private ValidatingReaderNodeData[] contentEvents;

		// Token: 0x04000C56 RID: 3158
		private ValidatingReaderNodeData[] attributeEvents;

		// Token: 0x04000C57 RID: 3159
		private ValidatingReaderNodeData cachedNode;

		// Token: 0x04000C58 RID: 3160
		private XsdCachingReader.CachingReaderState cacheState;

		// Token: 0x04000C59 RID: 3161
		private int contentIndex;

		// Token: 0x04000C5A RID: 3162
		private int attributeCount;

		// Token: 0x04000C5B RID: 3163
		private bool returnOriginalStringValues;

		// Token: 0x04000C5C RID: 3164
		private CachingEventHandler cacheHandler;

		// Token: 0x04000C5D RID: 3165
		private int currentAttrIndex;

		// Token: 0x04000C5E RID: 3166
		private int currentContentIndex;

		// Token: 0x04000C5F RID: 3167
		private bool readAhead;

		// Token: 0x04000C60 RID: 3168
		private IXmlLineInfo lineInfo;

		// Token: 0x04000C61 RID: 3169
		private ValidatingReaderNodeData textNode;

		// Token: 0x04000C62 RID: 3170
		private const int InitialAttributeCount = 8;

		// Token: 0x04000C63 RID: 3171
		private const int InitialContentCount = 4;

		// Token: 0x020001E8 RID: 488
		private enum CachingReaderState
		{
			// Token: 0x04000C65 RID: 3173
			None,
			// Token: 0x04000C66 RID: 3174
			Init,
			// Token: 0x04000C67 RID: 3175
			Record,
			// Token: 0x04000C68 RID: 3176
			Replay,
			// Token: 0x04000C69 RID: 3177
			ReaderClosed,
			// Token: 0x04000C6A RID: 3178
			Error
		}
	}
}
