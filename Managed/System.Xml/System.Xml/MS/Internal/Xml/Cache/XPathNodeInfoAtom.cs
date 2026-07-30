using System;
using System.Text;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000066 RID: 102
	internal sealed class XPathNodeInfoAtom
	{
		// Token: 0x06000354 RID: 852 RVA: 0x0000CBF2 File Offset: 0x0000ADF2
		public XPathNodeInfoAtom(XPathNodePageInfo pageInfo)
		{
			this.pageInfo = pageInfo;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000CC04 File Offset: 0x0000AE04
		public XPathNodeInfoAtom(string localName, string namespaceUri, string prefix, string baseUri, XPathNode[] pageParent, XPathNode[] pageSibling, XPathNode[] pageSimilar, XPathDocument doc, int lineNumBase, int linePosBase)
		{
			this.Init(localName, namespaceUri, prefix, baseUri, pageParent, pageSibling, pageSimilar, doc, lineNumBase, linePosBase);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000CC30 File Offset: 0x0000AE30
		public void Init(string localName, string namespaceUri, string prefix, string baseUri, XPathNode[] pageParent, XPathNode[] pageSibling, XPathNode[] pageSimilar, XPathDocument doc, int lineNumBase, int linePosBase)
		{
			this.localName = localName;
			this.namespaceUri = namespaceUri;
			this.prefix = prefix;
			this.baseUri = baseUri;
			this.pageParent = pageParent;
			this.pageSibling = pageSibling;
			this.pageSimilar = pageSimilar;
			this.doc = doc;
			this.lineNumBase = lineNumBase;
			this.linePosBase = linePosBase;
			this.next = null;
			this.pageInfo = null;
			this.hashCode = 0;
			this.localNameHash = 0;
			for (int i = 0; i < this.localName.Length; i++)
			{
				this.localNameHash += (this.localNameHash << 7) ^ (int)this.localName[i];
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000CCDE File Offset: 0x0000AEDE
		public XPathNodePageInfo PageInfo
		{
			get
			{
				return this.pageInfo;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000CCE6 File Offset: 0x0000AEE6
		public string LocalName
		{
			get
			{
				return this.localName;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000CCEE File Offset: 0x0000AEEE
		public string NamespaceUri
		{
			get
			{
				return this.namespaceUri;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000CCF6 File Offset: 0x0000AEF6
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000CCFE File Offset: 0x0000AEFE
		public string BaseUri
		{
			get
			{
				return this.baseUri;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000CD06 File Offset: 0x0000AF06
		public XPathNode[] SiblingPage
		{
			get
			{
				return this.pageSibling;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000CD0E File Offset: 0x0000AF0E
		public XPathNode[] SimilarElementPage
		{
			get
			{
				return this.pageSimilar;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000CD16 File Offset: 0x0000AF16
		public XPathNode[] ParentPage
		{
			get
			{
				return this.pageParent;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000CD1E File Offset: 0x0000AF1E
		public XPathDocument Document
		{
			get
			{
				return this.doc;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000CD26 File Offset: 0x0000AF26
		public int LineNumberBase
		{
			get
			{
				return this.lineNumBase;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000CD2E File Offset: 0x0000AF2E
		public int LinePositionBase
		{
			get
			{
				return this.linePosBase;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000CD36 File Offset: 0x0000AF36
		public int LocalNameHashCode
		{
			get
			{
				return this.localNameHash;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000CD3E File Offset: 0x0000AF3E
		// (set) Token: 0x06000364 RID: 868 RVA: 0x0000CD46 File Offset: 0x0000AF46
		public XPathNodeInfoAtom Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000CD50 File Offset: 0x0000AF50
		public override int GetHashCode()
		{
			if (this.hashCode == 0)
			{
				int num = this.localNameHash;
				if (this.pageSibling != null)
				{
					num += (num << 7) ^ this.pageSibling[0].PageInfo.PageNumber;
				}
				if (this.pageParent != null)
				{
					num += (num << 7) ^ this.pageParent[0].PageInfo.PageNumber;
				}
				if (this.pageSimilar != null)
				{
					num += (num << 7) ^ this.pageSimilar[0].PageInfo.PageNumber;
				}
				this.hashCode = ((num == 0) ? 1 : num);
			}
			return this.hashCode;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000CDF4 File Offset: 0x0000AFF4
		public override bool Equals(object other)
		{
			XPathNodeInfoAtom xpathNodeInfoAtom = other as XPathNodeInfoAtom;
			return this.GetHashCode() == xpathNodeInfoAtom.GetHashCode() && this.localName == xpathNodeInfoAtom.localName && this.pageSibling == xpathNodeInfoAtom.pageSibling && this.namespaceUri == xpathNodeInfoAtom.namespaceUri && this.pageParent == xpathNodeInfoAtom.pageParent && this.pageSimilar == xpathNodeInfoAtom.pageSimilar && this.prefix == xpathNodeInfoAtom.prefix && this.baseUri == xpathNodeInfoAtom.baseUri && this.lineNumBase == xpathNodeInfoAtom.lineNumBase && this.linePosBase == xpathNodeInfoAtom.linePosBase;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000CE9C File Offset: 0x0000B09C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("hash=");
			stringBuilder.Append(this.GetHashCode());
			stringBuilder.Append(", ");
			if (this.localName.Length != 0)
			{
				stringBuilder.Append('{');
				stringBuilder.Append(this.namespaceUri);
				stringBuilder.Append('}');
				if (this.prefix.Length != 0)
				{
					stringBuilder.Append(this.prefix);
					stringBuilder.Append(':');
				}
				stringBuilder.Append(this.localName);
				stringBuilder.Append(", ");
			}
			if (this.pageParent != null)
			{
				stringBuilder.Append("parent=");
				stringBuilder.Append(this.pageParent[0].PageInfo.PageNumber);
				stringBuilder.Append(", ");
			}
			if (this.pageSibling != null)
			{
				stringBuilder.Append("sibling=");
				stringBuilder.Append(this.pageSibling[0].PageInfo.PageNumber);
				stringBuilder.Append(", ");
			}
			if (this.pageSimilar != null)
			{
				stringBuilder.Append("similar=");
				stringBuilder.Append(this.pageSimilar[0].PageInfo.PageNumber);
				stringBuilder.Append(", ");
			}
			stringBuilder.Append("lineNum=");
			stringBuilder.Append(this.lineNumBase);
			stringBuilder.Append(", ");
			stringBuilder.Append("linePos=");
			stringBuilder.Append(this.linePosBase);
			return stringBuilder.ToString();
		}

		// Token: 0x040001AA RID: 426
		private string localName;

		// Token: 0x040001AB RID: 427
		private string namespaceUri;

		// Token: 0x040001AC RID: 428
		private string prefix;

		// Token: 0x040001AD RID: 429
		private string baseUri;

		// Token: 0x040001AE RID: 430
		private XPathNode[] pageParent;

		// Token: 0x040001AF RID: 431
		private XPathNode[] pageSibling;

		// Token: 0x040001B0 RID: 432
		private XPathNode[] pageSimilar;

		// Token: 0x040001B1 RID: 433
		private XPathDocument doc;

		// Token: 0x040001B2 RID: 434
		private int lineNumBase;

		// Token: 0x040001B3 RID: 435
		private int linePosBase;

		// Token: 0x040001B4 RID: 436
		private int hashCode;

		// Token: 0x040001B5 RID: 437
		private int localNameHash;

		// Token: 0x040001B6 RID: 438
		private XPathNodeInfoAtom next;

		// Token: 0x040001B7 RID: 439
		private XPathNodePageInfo pageInfo;
	}
}
