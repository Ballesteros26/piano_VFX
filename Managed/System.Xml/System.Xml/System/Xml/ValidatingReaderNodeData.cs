using System;

namespace System.Xml
{
	// Token: 0x020000BF RID: 191
	internal class ValidatingReaderNodeData
	{
		// Token: 0x0600062A RID: 1578 RVA: 0x0001B0AF File Offset: 0x000192AF
		public ValidatingReaderNodeData()
		{
			this.Clear(XmlNodeType.None);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001B0BE File Offset: 0x000192BE
		public ValidatingReaderNodeData(XmlNodeType nodeType)
		{
			this.Clear(nodeType);
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0001B0CD File Offset: 0x000192CD
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x0001B0D5 File Offset: 0x000192D5
		public string LocalName
		{
			get
			{
				return this.localName;
			}
			set
			{
				this.localName = value;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0001B0DE File Offset: 0x000192DE
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x0001B0E6 File Offset: 0x000192E6
		public string Namespace
		{
			get
			{
				return this.namespaceUri;
			}
			set
			{
				this.namespaceUri = value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0001B0EF File Offset: 0x000192EF
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x0001B0F7 File Offset: 0x000192F7
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
			set
			{
				this.prefix = value;
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001B100 File Offset: 0x00019300
		public string GetAtomizedNameWPrefix(XmlNameTable nameTable)
		{
			if (this.nameWPrefix == null)
			{
				if (this.prefix.Length == 0)
				{
					this.nameWPrefix = this.localName;
				}
				else
				{
					this.nameWPrefix = nameTable.Add(this.prefix + ":" + this.localName);
				}
			}
			return this.nameWPrefix;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x0001B158 File Offset: 0x00019358
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x0001B160 File Offset: 0x00019360
		public int Depth
		{
			get
			{
				return this.depth;
			}
			set
			{
				this.depth = value;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0001B169 File Offset: 0x00019369
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x0001B171 File Offset: 0x00019371
		public string RawValue
		{
			get
			{
				return this.rawValue;
			}
			set
			{
				this.rawValue = value;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0001B17A File Offset: 0x0001937A
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x0001B182 File Offset: 0x00019382
		public string OriginalStringValue
		{
			get
			{
				return this.originalStringValue;
			}
			set
			{
				this.originalStringValue = value;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0001B18B File Offset: 0x0001938B
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x0001B193 File Offset: 0x00019393
		public XmlNodeType NodeType
		{
			get
			{
				return this.nodeType;
			}
			set
			{
				this.nodeType = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0001B19C File Offset: 0x0001939C
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x0001B1A4 File Offset: 0x000193A4
		public AttributePSVIInfo AttInfo
		{
			get
			{
				return this.attributePSVIInfo;
			}
			set
			{
				this.attributePSVIInfo = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0001B1AD File Offset: 0x000193AD
		public int LineNumber
		{
			get
			{
				return this.lineNo;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x0001B1B5 File Offset: 0x000193B5
		public int LinePosition
		{
			get
			{
				return this.linePos;
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001B1C0 File Offset: 0x000193C0
		internal void Clear(XmlNodeType nodeType)
		{
			this.nodeType = nodeType;
			this.localName = string.Empty;
			this.prefix = string.Empty;
			this.namespaceUri = string.Empty;
			this.rawValue = string.Empty;
			if (this.attributePSVIInfo != null)
			{
				this.attributePSVIInfo.Reset();
			}
			this.nameWPrefix = null;
			this.lineNo = 0;
			this.linePos = 0;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001B228 File Offset: 0x00019428
		internal void ClearName()
		{
			this.localName = string.Empty;
			this.prefix = string.Empty;
			this.namespaceUri = string.Empty;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001B24B File Offset: 0x0001944B
		internal void SetLineInfo(int lineNo, int linePos)
		{
			this.lineNo = lineNo;
			this.linePos = linePos;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001B25B File Offset: 0x0001945B
		internal void SetLineInfo(IXmlLineInfo lineInfo)
		{
			if (lineInfo != null)
			{
				this.lineNo = lineInfo.LineNumber;
				this.linePos = lineInfo.LinePosition;
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001B278 File Offset: 0x00019478
		internal void SetItemData(string localName, string prefix, string ns, string value)
		{
			this.localName = localName;
			this.prefix = prefix;
			this.namespaceUri = ns;
			this.rawValue = value;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001B297 File Offset: 0x00019497
		internal void SetItemData(string localName, string prefix, string ns, int depth)
		{
			this.localName = localName;
			this.prefix = prefix;
			this.namespaceUri = ns;
			this.depth = depth;
			this.rawValue = string.Empty;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001B2C1 File Offset: 0x000194C1
		internal void SetItemData(string value)
		{
			this.SetItemData(value, value);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001B2CB File Offset: 0x000194CB
		internal void SetItemData(string value, string originalStringValue)
		{
			this.rawValue = value;
			this.originalStringValue = originalStringValue;
		}

		// Token: 0x040003C6 RID: 966
		private string localName;

		// Token: 0x040003C7 RID: 967
		private string namespaceUri;

		// Token: 0x040003C8 RID: 968
		private string prefix;

		// Token: 0x040003C9 RID: 969
		private string nameWPrefix;

		// Token: 0x040003CA RID: 970
		private string rawValue;

		// Token: 0x040003CB RID: 971
		private string originalStringValue;

		// Token: 0x040003CC RID: 972
		private int depth;

		// Token: 0x040003CD RID: 973
		private AttributePSVIInfo attributePSVIInfo;

		// Token: 0x040003CE RID: 974
		private XmlNodeType nodeType;

		// Token: 0x040003CF RID: 975
		private int lineNo;

		// Token: 0x040003D0 RID: 976
		private int linePos;
	}
}
