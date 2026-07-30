using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000059 RID: 89
	internal sealed class XPathDocumentBuilder : XmlRawWriter
	{
		// Token: 0x06000290 RID: 656 RVA: 0x00009EF2 File Offset: 0x000080F2
		public XPathDocumentBuilder(XPathDocument doc, IXmlLineInfo lineInfo, string baseUri, XPathDocument.LoadFlags flags)
		{
			this.nodePageFact.Init(256);
			this.nmspPageFact.Init(16);
			this.stkNmsp = new Stack<XPathNodeRef>();
			this.Initialize(doc, lineInfo, baseUri, flags);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00009F30 File Offset: 0x00008130
		public void Initialize(XPathDocument doc, IXmlLineInfo lineInfo, string baseUri, XPathDocument.LoadFlags flags)
		{
			this.doc = doc;
			this.nameTable = doc.NameTable;
			this.atomizeNames = (flags & XPathDocument.LoadFlags.AtomizeNames) > XPathDocument.LoadFlags.None;
			this.idxParent = (this.idxSibling = 0);
			this.elemNameIndex = new XPathNodeRef[64];
			this.textBldr.Initialize(lineInfo);
			this.lineInfo = lineInfo;
			this.lineNumBase = 0;
			this.linePosBase = 0;
			this.infoTable = new XPathNodeInfoTable();
			XPathNode[] array;
			int num = this.NewNode(out array, XPathNodeType.Text, string.Empty, string.Empty, string.Empty, string.Empty);
			this.doc.SetCollapsedTextNode(array, num);
			this.idxNmsp = this.NewNamespaceNode(out this.pageNmsp, this.nameTable.Add("xml"), this.nameTable.Add("http://www.w3.org/XML/1998/namespace"), null, 0);
			this.doc.SetXmlNamespaceNode(this.pageNmsp, this.idxNmsp);
			if ((flags & XPathDocument.LoadFlags.Fragment) == XPathDocument.LoadFlags.None)
			{
				this.idxParent = this.NewNode(out this.pageParent, XPathNodeType.Root, string.Empty, string.Empty, string.Empty, baseUri);
				this.doc.SetRootNode(this.pageParent, this.idxParent);
				return;
			}
			this.doc.SetRootNode(this.nodePageFact.NextNodePage, this.nodePageFact.NextNodeIndex);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000A07E File Offset: 0x0000827E
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.WriteStartElement(prefix, localName, ns, string.Empty);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000A090 File Offset: 0x00008290
		public void WriteStartElement(string prefix, string localName, string ns, string baseUri)
		{
			if (this.atomizeNames)
			{
				prefix = this.nameTable.Add(prefix);
				localName = this.nameTable.Add(localName);
				ns = this.nameTable.Add(ns);
			}
			this.AddSibling(XPathNodeType.Element, localName, ns, prefix, baseUri);
			this.pageParent = this.pageSibling;
			this.idxParent = this.idxSibling;
			this.idxSibling = 0;
			int num = this.pageParent[this.idxParent].LocalNameHashCode & 63;
			this.elemNameIndex[num] = this.LinkSimilarElements(this.elemNameIndex[num].Page, this.elemNameIndex[num].Index, this.pageParent, this.idxParent);
			if (this.elemIdMap != null)
			{
				this.idAttrName = (XmlQualifiedName)this.elemIdMap[new XmlQualifiedName(localName, prefix)];
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000A179 File Offset: 0x00008379
		public override void WriteEndElement()
		{
			this.WriteEndElement(true);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000A182 File Offset: 0x00008382
		public override void WriteFullEndElement()
		{
			this.WriteEndElement(false);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000A179 File Offset: 0x00008379
		internal override void WriteEndElement(string prefix, string localName, string namespaceName)
		{
			this.WriteEndElement(true);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000A182 File Offset: 0x00008382
		internal override void WriteFullEndElement(string prefix, string localName, string namespaceName)
		{
			this.WriteEndElement(false);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000A18C File Offset: 0x0000838C
		public void WriteEndElement(bool allowShortcutTag)
		{
			if (!this.pageParent[this.idxParent].HasContentChild)
			{
				TextBlockType textType = this.textBldr.TextType;
				if (textType == TextBlockType.Text)
				{
					if (this.lineInfo != null)
					{
						if (this.textBldr.LineNumber != this.pageParent[this.idxParent].LineNumber)
						{
							goto IL_00CD;
						}
						int num = this.textBldr.LinePosition - this.pageParent[this.idxParent].LinePosition;
						if (num < 0 || num > 255)
						{
							goto IL_00CD;
						}
						this.pageParent[this.idxParent].SetCollapsedLineInfoOffset(num);
					}
					this.pageParent[this.idxParent].SetCollapsedValue(this.textBldr.ReadText());
					goto IL_012D;
				}
				if (textType - TextBlockType.SignificantWhitespace > 1)
				{
					this.pageParent[this.idxParent].SetEmptyValue(allowShortcutTag);
					goto IL_012D;
				}
				IL_00CD:
				this.CachedTextNode();
				this.pageParent[this.idxParent].SetValue(this.pageSibling[this.idxSibling].Value);
			}
			else if (this.textBldr.HasText)
			{
				this.CachedTextNode();
			}
			IL_012D:
			if (this.pageParent[this.idxParent].HasNamespaceDecls)
			{
				this.doc.AddNamespace(this.pageParent, this.idxParent, this.pageNmsp, this.idxNmsp);
				XPathNodeRef xpathNodeRef = this.stkNmsp.Pop();
				this.pageNmsp = xpathNodeRef.Page;
				this.idxNmsp = xpathNodeRef.Index;
			}
			this.pageSibling = this.pageParent;
			this.idxSibling = this.idxParent;
			this.idxParent = this.pageParent[this.idxParent].GetParent(out this.pageParent);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000A364 File Offset: 0x00008564
		public override void WriteStartAttribute(string prefix, string localName, string namespaceName)
		{
			if (this.atomizeNames)
			{
				prefix = this.nameTable.Add(prefix);
				localName = this.nameTable.Add(localName);
				namespaceName = this.nameTable.Add(namespaceName);
			}
			this.AddSibling(XPathNodeType.Attribute, localName, namespaceName, prefix, string.Empty);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000A3B4 File Offset: 0x000085B4
		public override void WriteEndAttribute()
		{
			this.pageSibling[this.idxSibling].SetValue(this.textBldr.ReadText());
			if (this.idAttrName != null && this.pageSibling[this.idxSibling].LocalName == this.idAttrName.Name && this.pageSibling[this.idxSibling].Prefix == this.idAttrName.Namespace)
			{
				this.doc.AddIdElement(this.pageSibling[this.idxSibling].Value, this.pageParent, this.idxParent);
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000A46D File Offset: 0x0000866D
		public override void WriteCData(string text)
		{
			this.WriteString(text, TextBlockType.Text);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000A477 File Offset: 0x00008677
		public override void WriteComment(string text)
		{
			this.AddSibling(XPathNodeType.Comment, string.Empty, string.Empty, string.Empty, string.Empty);
			this.pageSibling[this.idxSibling].SetValue(text);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000A4AB File Offset: 0x000086AB
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.WriteProcessingInstruction(name, text, string.Empty);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000A4BC File Offset: 0x000086BC
		public void WriteProcessingInstruction(string name, string text, string baseUri)
		{
			if (this.atomizeNames)
			{
				name = this.nameTable.Add(name);
			}
			this.AddSibling(XPathNodeType.ProcessingInstruction, name, string.Empty, string.Empty, baseUri);
			this.pageSibling[this.idxSibling].SetValue(text);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000A509 File Offset: 0x00008709
		public override void WriteWhitespace(string ws)
		{
			this.WriteString(ws, TextBlockType.Whitespace);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000A46D File Offset: 0x0000866D
		public override void WriteString(string text)
		{
			this.WriteString(text, TextBlockType.Text);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000A513 File Offset: 0x00008713
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count), TextBlockType.Text);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000A46D File Offset: 0x0000866D
		public override void WriteRaw(string data)
		{
			this.WriteString(data, TextBlockType.Text);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000A513 File Offset: 0x00008713
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count), TextBlockType.Text);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000A524 File Offset: 0x00008724
		public void WriteString(string text, TextBlockType textType)
		{
			this.textBldr.WriteTextBlock(text, textType);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000A533 File Offset: 0x00008733
		public override void WriteEntityRef(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000A53C File Offset: 0x0000873C
		public override void WriteCharEntity(char ch)
		{
			char[] array = new char[] { ch };
			this.WriteString(new string(array), TextBlockType.Text);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000A564 File Offset: 0x00008764
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			char[] array = new char[] { highChar, lowChar };
			this.WriteString(new string(array), TextBlockType.Text);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000A590 File Offset: 0x00008790
		public override void Close()
		{
			if (this.textBldr.HasText)
			{
				this.CachedTextNode();
			}
			XPathNode[] array;
			if (this.doc.GetRootNode(out array) == this.nodePageFact.NextNodeIndex && array == this.nodePageFact.NextNodePage)
			{
				this.AddSibling(XPathNodeType.Text, string.Empty, string.Empty, string.Empty, string.Empty);
				this.pageSibling[this.idxSibling].SetValue(string.Empty);
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00002F50 File Offset: 0x00001150
		public override void Flush()
		{
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void StartElementContent()
		{
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000A610 File Offset: 0x00008810
		internal override void WriteNamespaceDeclaration(string prefix, string namespaceName)
		{
			if (this.atomizeNames)
			{
				prefix = this.nameTable.Add(prefix);
			}
			namespaceName = this.nameTable.Add(namespaceName);
			XPathNode[] array = this.pageNmsp;
			int num = this.idxNmsp;
			while (num != 0 && array[num].LocalName != prefix)
			{
				num = array[num].GetSibling(out array);
			}
			XPathNode[] array2;
			int num2 = this.NewNamespaceNode(out array2, prefix, namespaceName, this.pageParent, this.idxParent);
			if (num != 0)
			{
				XPathNode[] array3 = this.pageNmsp;
				int sibling = this.idxNmsp;
				XPathNode[] array4 = array2;
				int num3 = num2;
				while (sibling != num || array3 != array)
				{
					XPathNode[] array5;
					int num4 = array3[sibling].GetParent(out array5);
					num4 = this.NewNamespaceNode(out array5, array3[sibling].LocalName, array3[sibling].Value, array5, num4);
					array4[num3].SetSibling(this.infoTable, array5, num4);
					array4 = array5;
					num3 = num4;
					sibling = array3[sibling].GetSibling(out array3);
				}
				num = array[num].GetSibling(out array);
				if (num != 0)
				{
					array4[num3].SetSibling(this.infoTable, array, num);
				}
			}
			else if (this.idxParent != 0)
			{
				array2[num2].SetSibling(this.infoTable, this.pageNmsp, this.idxNmsp);
			}
			else
			{
				this.doc.SetRootNode(array2, num2);
			}
			if (this.idxParent != 0)
			{
				if (!this.pageParent[this.idxParent].HasNamespaceDecls)
				{
					this.stkNmsp.Push(new XPathNodeRef(this.pageNmsp, this.idxNmsp));
					this.pageParent[this.idxParent].HasNamespaceDecls = true;
				}
				this.pageNmsp = array2;
				this.idxNmsp = num2;
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000A7E8 File Offset: 0x000089E8
		public void CreateIdTables(IDtdInfo dtdInfo)
		{
			foreach (IDtdAttributeListInfo dtdAttributeListInfo in dtdInfo.GetAttributeLists())
			{
				IDtdAttributeInfo dtdAttributeInfo = dtdAttributeListInfo.LookupIdAttribute();
				if (dtdAttributeInfo != null)
				{
					if (this.elemIdMap == null)
					{
						this.elemIdMap = new Hashtable();
					}
					this.elemIdMap.Add(new XmlQualifiedName(dtdAttributeListInfo.LocalName, dtdAttributeListInfo.Prefix), new XmlQualifiedName(dtdAttributeInfo.LocalName, dtdAttributeInfo.Prefix));
				}
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000A878 File Offset: 0x00008A78
		private XPathNodeRef LinkSimilarElements(XPathNode[] pagePrev, int idxPrev, XPathNode[] pageNext, int idxNext)
		{
			if (pagePrev != null)
			{
				pagePrev[idxPrev].SetSimilarElement(this.infoTable, pageNext, idxNext);
			}
			return new XPathNodeRef(pageNext, idxNext);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000A89C File Offset: 0x00008A9C
		private int NewNamespaceNode(out XPathNode[] page, string prefix, string namespaceUri, XPathNode[] pageElem, int idxElem)
		{
			XPathNode[] array;
			int num;
			this.nmspPageFact.AllocateSlot(out array, out num);
			int num2;
			int num3;
			this.ComputeLineInfo(false, out num2, out num3);
			XPathNodeInfoAtom xpathNodeInfoAtom = this.infoTable.Create(prefix, string.Empty, string.Empty, string.Empty, pageElem, array, null, this.doc, this.lineNumBase, this.linePosBase);
			array[num].Create(xpathNodeInfoAtom, XPathNodeType.Namespace, idxElem);
			array[num].SetValue(namespaceUri);
			array[num].SetLineInfoOffsets(num2, num3);
			page = array;
			return num;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000A928 File Offset: 0x00008B28
		private int NewNode(out XPathNode[] page, XPathNodeType xptyp, string localName, string namespaceUri, string prefix, string baseUri)
		{
			XPathNode[] array;
			int num;
			this.nodePageFact.AllocateSlot(out array, out num);
			int num2;
			int num3;
			this.ComputeLineInfo(XPathNavigator.IsText(xptyp), out num2, out num3);
			XPathNodeInfoAtom xpathNodeInfoAtom = this.infoTable.Create(localName, namespaceUri, prefix, baseUri, this.pageParent, array, array, this.doc, this.lineNumBase, this.linePosBase);
			array[num].Create(xpathNodeInfoAtom, xptyp, this.idxParent);
			array[num].SetLineInfoOffsets(num2, num3);
			page = array;
			return num;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000A9AC File Offset: 0x00008BAC
		private void ComputeLineInfo(bool isTextNode, out int lineNumOffset, out int linePosOffset)
		{
			if (this.lineInfo == null)
			{
				lineNumOffset = 0;
				linePosOffset = 0;
				return;
			}
			int num;
			int num2;
			if (isTextNode)
			{
				num = this.textBldr.LineNumber;
				num2 = this.textBldr.LinePosition;
			}
			else
			{
				num = this.lineInfo.LineNumber;
				num2 = this.lineInfo.LinePosition;
			}
			lineNumOffset = num - this.lineNumBase;
			if (lineNumOffset < 0 || lineNumOffset > 16383)
			{
				this.lineNumBase = num;
				lineNumOffset = 0;
			}
			linePosOffset = num2 - this.linePosBase;
			if (linePosOffset < 0 || linePosOffset > 65535)
			{
				this.linePosBase = num2;
				linePosOffset = 0;
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000AA44 File Offset: 0x00008C44
		private void AddSibling(XPathNodeType xptyp, string localName, string namespaceUri, string prefix, string baseUri)
		{
			if (this.textBldr.HasText)
			{
				this.CachedTextNode();
			}
			XPathNode[] array;
			int num = this.NewNode(out array, xptyp, localName, namespaceUri, prefix, baseUri);
			if (this.idxParent != 0)
			{
				this.pageParent[this.idxParent].SetParentProperties(xptyp);
				if (this.idxSibling != 0)
				{
					this.pageSibling[this.idxSibling].SetSibling(this.infoTable, array, num);
				}
			}
			this.pageSibling = array;
			this.idxSibling = num;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000AAC8 File Offset: 0x00008CC8
		private void CachedTextNode()
		{
			TextBlockType textType = this.textBldr.TextType;
			string text = this.textBldr.ReadText();
			this.AddSibling((XPathNodeType)textType, string.Empty, string.Empty, string.Empty, string.Empty);
			this.pageSibling[this.idxSibling].SetValue(text);
		}

		// Token: 0x04000162 RID: 354
		private XPathDocumentBuilder.NodePageFactory nodePageFact;

		// Token: 0x04000163 RID: 355
		private XPathDocumentBuilder.NodePageFactory nmspPageFact;

		// Token: 0x04000164 RID: 356
		private XPathDocumentBuilder.TextBlockBuilder textBldr;

		// Token: 0x04000165 RID: 357
		private Stack<XPathNodeRef> stkNmsp;

		// Token: 0x04000166 RID: 358
		private XPathNodeInfoTable infoTable;

		// Token: 0x04000167 RID: 359
		private XPathDocument doc;

		// Token: 0x04000168 RID: 360
		private IXmlLineInfo lineInfo;

		// Token: 0x04000169 RID: 361
		private XmlNameTable nameTable;

		// Token: 0x0400016A RID: 362
		private bool atomizeNames;

		// Token: 0x0400016B RID: 363
		private XPathNode[] pageNmsp;

		// Token: 0x0400016C RID: 364
		private int idxNmsp;

		// Token: 0x0400016D RID: 365
		private XPathNode[] pageParent;

		// Token: 0x0400016E RID: 366
		private int idxParent;

		// Token: 0x0400016F RID: 367
		private XPathNode[] pageSibling;

		// Token: 0x04000170 RID: 368
		private int idxSibling;

		// Token: 0x04000171 RID: 369
		private int lineNumBase;

		// Token: 0x04000172 RID: 370
		private int linePosBase;

		// Token: 0x04000173 RID: 371
		private XmlQualifiedName idAttrName;

		// Token: 0x04000174 RID: 372
		private Hashtable elemIdMap;

		// Token: 0x04000175 RID: 373
		private XPathNodeRef[] elemNameIndex;

		// Token: 0x04000176 RID: 374
		private const int ElementIndexSize = 64;

		// Token: 0x0200005A RID: 90
		private struct NodePageFactory
		{
			// Token: 0x060002B6 RID: 694 RVA: 0x0000AB1F File Offset: 0x00008D1F
			public void Init(int initialPageSize)
			{
				this.pageSize = initialPageSize;
				this.page = new XPathNode[this.pageSize];
				this.pageInfo = new XPathNodePageInfo(null, 1);
				this.page[0].Create(this.pageInfo);
			}

			// Token: 0x17000098 RID: 152
			// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000AB5D File Offset: 0x00008D5D
			public XPathNode[] NextNodePage
			{
				get
				{
					return this.page;
				}
			}

			// Token: 0x17000099 RID: 153
			// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000AB65 File Offset: 0x00008D65
			public int NextNodeIndex
			{
				get
				{
					return this.pageInfo.NodeCount;
				}
			}

			// Token: 0x060002B9 RID: 697 RVA: 0x0000AB74 File Offset: 0x00008D74
			public void AllocateSlot(out XPathNode[] page, out int idx)
			{
				page = this.page;
				idx = this.pageInfo.NodeCount;
				XPathNodePageInfo xpathNodePageInfo = this.pageInfo;
				int num = xpathNodePageInfo.NodeCount + 1;
				xpathNodePageInfo.NodeCount = num;
				if (num >= this.page.Length)
				{
					if (this.pageSize < 65536)
					{
						this.pageSize *= 2;
					}
					this.page = new XPathNode[this.pageSize];
					this.pageInfo.NextPage = this.page;
					this.pageInfo = new XPathNodePageInfo(page, this.pageInfo.PageNumber + 1);
					this.page[0].Create(this.pageInfo);
				}
			}

			// Token: 0x04000177 RID: 375
			private XPathNode[] page;

			// Token: 0x04000178 RID: 376
			private XPathNodePageInfo pageInfo;

			// Token: 0x04000179 RID: 377
			private int pageSize;
		}

		// Token: 0x0200005B RID: 91
		private struct TextBlockBuilder
		{
			// Token: 0x060002BA RID: 698 RVA: 0x0000AC24 File Offset: 0x00008E24
			public void Initialize(IXmlLineInfo lineInfo)
			{
				this.lineInfo = lineInfo;
				this.textType = TextBlockType.None;
			}

			// Token: 0x1700009A RID: 154
			// (get) Token: 0x060002BB RID: 699 RVA: 0x0000AC34 File Offset: 0x00008E34
			public TextBlockType TextType
			{
				get
				{
					return this.textType;
				}
			}

			// Token: 0x1700009B RID: 155
			// (get) Token: 0x060002BC RID: 700 RVA: 0x0000AC3C File Offset: 0x00008E3C
			public bool HasText
			{
				get
				{
					return this.textType > TextBlockType.None;
				}
			}

			// Token: 0x1700009C RID: 156
			// (get) Token: 0x060002BD RID: 701 RVA: 0x0000AC47 File Offset: 0x00008E47
			public int LineNumber
			{
				get
				{
					return this.lineNum;
				}
			}

			// Token: 0x1700009D RID: 157
			// (get) Token: 0x060002BE RID: 702 RVA: 0x0000AC4F File Offset: 0x00008E4F
			public int LinePosition
			{
				get
				{
					return this.linePos;
				}
			}

			// Token: 0x060002BF RID: 703 RVA: 0x0000AC58 File Offset: 0x00008E58
			public void WriteTextBlock(string text, TextBlockType textType)
			{
				if (text.Length != 0)
				{
					if (this.textType == TextBlockType.None)
					{
						this.text = text;
						this.textType = textType;
						if (this.lineInfo != null)
						{
							this.lineNum = this.lineInfo.LineNumber;
							this.linePos = this.lineInfo.LinePosition;
							return;
						}
					}
					else
					{
						this.text += text;
						if (textType < this.textType)
						{
							this.textType = textType;
						}
					}
				}
			}

			// Token: 0x060002C0 RID: 704 RVA: 0x0000ACD0 File Offset: 0x00008ED0
			public string ReadText()
			{
				if (this.textType == TextBlockType.None)
				{
					return string.Empty;
				}
				this.textType = TextBlockType.None;
				return this.text;
			}

			// Token: 0x0400017A RID: 378
			private IXmlLineInfo lineInfo;

			// Token: 0x0400017B RID: 379
			private TextBlockType textType;

			// Token: 0x0400017C RID: 380
			private string text;

			// Token: 0x0400017D RID: 381
			private int lineNum;

			// Token: 0x0400017E RID: 382
			private int linePos;
		}
	}
}
