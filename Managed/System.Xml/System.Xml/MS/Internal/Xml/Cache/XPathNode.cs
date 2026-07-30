using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000062 RID: 98
	internal struct XPathNode
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000BEC4 File Offset: 0x0000A0C4
		public XPathNodeType NodeType
		{
			get
			{
				return (XPathNodeType)(this.props & 15U);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000BECF File Offset: 0x0000A0CF
		public string Prefix
		{
			get
			{
				return this.info.Prefix;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		public string LocalName
		{
			get
			{
				return this.info.LocalName;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000BEE9 File Offset: 0x0000A0E9
		public string Name
		{
			get
			{
				if (this.Prefix.Length == 0)
				{
					return this.LocalName;
				}
				return this.Prefix + ":" + this.LocalName;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000BF15 File Offset: 0x0000A115
		public string NamespaceUri
		{
			get
			{
				return this.info.NamespaceUri;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000BF22 File Offset: 0x0000A122
		public XPathDocument Document
		{
			get
			{
				return this.info.Document;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000BF2F File Offset: 0x0000A12F
		public string BaseUri
		{
			get
			{
				return this.info.BaseUri;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000BF3C File Offset: 0x0000A13C
		public int LineNumber
		{
			get
			{
				return this.info.LineNumberBase + (int)((this.props & 16776192U) >> 10);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000BF59 File Offset: 0x0000A159
		public int LinePosition
		{
			get
			{
				return this.info.LinePositionBase + (int)this.posOffset;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000BF6D File Offset: 0x0000A16D
		public int CollapsedLinePosition
		{
			get
			{
				return this.LinePosition + (int)(this.props >> 24);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000BF7F File Offset: 0x0000A17F
		public XPathNodePageInfo PageInfo
		{
			get
			{
				return this.info.PageInfo;
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000BF8C File Offset: 0x0000A18C
		public int GetRoot(out XPathNode[] pageNode)
		{
			return this.info.Document.GetRootNode(out pageNode);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000BF9F File Offset: 0x0000A19F
		public int GetParent(out XPathNode[] pageNode)
		{
			pageNode = this.info.ParentPage;
			return (int)this.idxParent;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000BFB4 File Offset: 0x0000A1B4
		public int GetSibling(out XPathNode[] pageNode)
		{
			pageNode = this.info.SiblingPage;
			return (int)this.idxSibling;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000BFC9 File Offset: 0x0000A1C9
		public int GetSimilarElement(out XPathNode[] pageNode)
		{
			pageNode = this.info.SimilarElementPage;
			return (int)this.idxSimilar;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000BFDE File Offset: 0x0000A1DE
		public bool NameMatch(string localName, string namespaceName)
		{
			return this.info.LocalName == localName && this.info.NamespaceUri == namespaceName;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000C001 File Offset: 0x0000A201
		public bool ElementMatch(string localName, string namespaceName)
		{
			return this.NodeType == XPathNodeType.Element && this.info.LocalName == localName && this.info.NamespaceUri == namespaceName;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000C030 File Offset: 0x0000A230
		public bool IsXmlNamespaceNode
		{
			get
			{
				string localName = this.info.LocalName;
				return this.NodeType == XPathNodeType.Namespace && localName.Length == 3 && localName == "xml";
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000C068 File Offset: 0x0000A268
		public bool HasSibling
		{
			get
			{
				return this.idxSibling > 0;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000C073 File Offset: 0x0000A273
		public bool HasCollapsedText
		{
			get
			{
				return (this.props & 128U) > 0U;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000C084 File Offset: 0x0000A284
		public bool HasAttribute
		{
			get
			{
				return (this.props & 16U) > 0U;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000C092 File Offset: 0x0000A292
		public bool HasContentChild
		{
			get
			{
				return (this.props & 32U) > 0U;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000C0A0 File Offset: 0x0000A2A0
		public bool HasElementChild
		{
			get
			{
				return (this.props & 64U) > 0U;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000C0B0 File Offset: 0x0000A2B0
		public bool IsAttrNmsp
		{
			get
			{
				XPathNodeType nodeType = this.NodeType;
				return nodeType == XPathNodeType.Attribute || nodeType == XPathNodeType.Namespace;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000C0CE File Offset: 0x0000A2CE
		public bool IsText
		{
			get
			{
				return XPathNavigator.IsText(this.NodeType);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000C0DB File Offset: 0x0000A2DB
		// (set) Token: 0x06000322 RID: 802 RVA: 0x0000C0EC File Offset: 0x0000A2EC
		public bool HasNamespaceDecls
		{
			get
			{
				return (this.props & 512U) > 0U;
			}
			set
			{
				if (value)
				{
					this.props |= 512U;
					return;
				}
				this.props &= 255U;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000C116 File Offset: 0x0000A316
		public bool AllowShortcutTag
		{
			get
			{
				return (this.props & 256U) > 0U;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000C127 File Offset: 0x0000A327
		public int LocalNameHashCode
		{
			get
			{
				return this.info.LocalNameHashCode;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000C134 File Offset: 0x0000A334
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000C13C File Offset: 0x0000A33C
		public void Create(XPathNodePageInfo pageInfo)
		{
			this.info = new XPathNodeInfoAtom(pageInfo);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000C14A File Offset: 0x0000A34A
		public void Create(XPathNodeInfoAtom info, XPathNodeType xptyp, int idxParent)
		{
			this.info = info;
			this.props = (uint)xptyp;
			this.idxParent = (ushort)idxParent;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000C162 File Offset: 0x0000A362
		public void SetLineInfoOffsets(int lineNumOffset, int linePosOffset)
		{
			this.props |= (uint)((uint)lineNumOffset << 10);
			this.posOffset = (ushort)linePosOffset;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000C17D File Offset: 0x0000A37D
		public void SetCollapsedLineInfoOffset(int posOffset)
		{
			this.props |= (uint)((uint)posOffset << 24);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000C190 File Offset: 0x0000A390
		public void SetValue(string value)
		{
			this.value = value;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000C199 File Offset: 0x0000A399
		public void SetEmptyValue(bool allowShortcutTag)
		{
			this.value = string.Empty;
			if (allowShortcutTag)
			{
				this.props |= 256U;
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000C1BB File Offset: 0x0000A3BB
		public void SetCollapsedValue(string value)
		{
			this.value = value;
			this.props |= 160U;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000C1D6 File Offset: 0x0000A3D6
		public void SetParentProperties(XPathNodeType xptyp)
		{
			if (xptyp == XPathNodeType.Attribute)
			{
				this.props |= 16U;
				return;
			}
			this.props |= 32U;
			if (xptyp == XPathNodeType.Element)
			{
				this.props |= 64U;
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000C210 File Offset: 0x0000A410
		public void SetSibling(XPathNodeInfoTable infoTable, XPathNode[] pageSibling, int idxSibling)
		{
			this.idxSibling = (ushort)idxSibling;
			if (pageSibling != this.info.SiblingPage)
			{
				this.info = infoTable.Create(this.info.LocalName, this.info.NamespaceUri, this.info.Prefix, this.info.BaseUri, this.info.ParentPage, pageSibling, this.info.SimilarElementPage, this.info.Document, this.info.LineNumberBase, this.info.LinePositionBase);
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000C2A4 File Offset: 0x0000A4A4
		public void SetSimilarElement(XPathNodeInfoTable infoTable, XPathNode[] pageSimilar, int idxSimilar)
		{
			this.idxSimilar = (ushort)idxSimilar;
			if (pageSimilar != this.info.SimilarElementPage)
			{
				this.info = infoTable.Create(this.info.LocalName, this.info.NamespaceUri, this.info.Prefix, this.info.BaseUri, this.info.ParentPage, this.info.SiblingPage, pageSimilar, this.info.Document, this.info.LineNumberBase, this.info.LinePositionBase);
			}
		}

		// Token: 0x04000190 RID: 400
		private XPathNodeInfoAtom info;

		// Token: 0x04000191 RID: 401
		private ushort idxSibling;

		// Token: 0x04000192 RID: 402
		private ushort idxParent;

		// Token: 0x04000193 RID: 403
		private ushort idxSimilar;

		// Token: 0x04000194 RID: 404
		private ushort posOffset;

		// Token: 0x04000195 RID: 405
		private uint props;

		// Token: 0x04000196 RID: 406
		private string value;

		// Token: 0x04000197 RID: 407
		private const uint NodeTypeMask = 15U;

		// Token: 0x04000198 RID: 408
		private const uint HasAttributeBit = 16U;

		// Token: 0x04000199 RID: 409
		private const uint HasContentChildBit = 32U;

		// Token: 0x0400019A RID: 410
		private const uint HasElementChildBit = 64U;

		// Token: 0x0400019B RID: 411
		private const uint HasCollapsedTextBit = 128U;

		// Token: 0x0400019C RID: 412
		private const uint AllowShortcutTagBit = 256U;

		// Token: 0x0400019D RID: 413
		private const uint HasNmspDeclsBit = 512U;

		// Token: 0x0400019E RID: 414
		private const uint LineNumberMask = 16776192U;

		// Token: 0x0400019F RID: 415
		private const int LineNumberShift = 10;

		// Token: 0x040001A0 RID: 416
		private const int CollapsedPositionShift = 24;

		// Token: 0x040001A1 RID: 417
		public const int MaxLineNumberOffset = 16383;

		// Token: 0x040001A2 RID: 418
		public const int MaxLinePositionOffset = 65535;

		// Token: 0x040001A3 RID: 419
		public const int MaxCollapsedPositionOffset = 255;
	}
}
