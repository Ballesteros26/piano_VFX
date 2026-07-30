using System;

namespace System.Xml.XPath
{
	// Token: 0x020002C0 RID: 704
	internal class XmlEmptyNavigator : XPathNavigator
	{
		// Token: 0x06001A35 RID: 6709 RVA: 0x00093AF9 File Offset: 0x00091CF9
		private XmlEmptyNavigator()
		{
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001A36 RID: 6710 RVA: 0x00093B01 File Offset: 0x00091D01
		public static XmlEmptyNavigator Singleton
		{
			get
			{
				if (XmlEmptyNavigator.singleton == null)
				{
					XmlEmptyNavigator.singleton = new XmlEmptyNavigator();
				}
				return XmlEmptyNavigator.singleton;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001A37 RID: 6711 RVA: 0x000735E6 File Offset: 0x000717E6
		public override XPathNodeType NodeType
		{
			get
			{
				return XPathNodeType.All;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001A38 RID: 6712 RVA: 0x00003065 File Offset: 0x00001265
		public override string NamespaceURI
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x00003065 File Offset: 0x00001265
		public override string LocalName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x00003065 File Offset: 0x00001265
		public override string Name
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x00003065 File Offset: 0x00001265
		public override string Prefix
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001A3C RID: 6716 RVA: 0x00003065 File Offset: 0x00001265
		public override string BaseURI
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x00003065 File Offset: 0x00001265
		public override string Value
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool IsEmptyElement
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x00003065 File Offset: 0x00001265
		public override string XmlLang
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool HasAttributes
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool HasChildren
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x00093B1F File Offset: 0x00091D1F
		public override XmlNameTable NameTable
		{
			get
			{
				return new NameTable();
			}
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool MoveToFirstChild()
		{
			return false;
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00002F50 File Offset: 0x00001150
		public override void MoveToRoot()
		{
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool MoveToNext()
		{
			return false;
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool MoveToPrevious()
		{
			return false;
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool MoveToFirst()
		{
			return false;
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool MoveToFirstAttribute()
		{
			return false;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool MoveToNextAttribute()
		{
			return false;
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool MoveToId(string id)
		{
			return false;
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x0000365F File Offset: 0x0000185F
		public override string GetAttribute(string localName, string namespaceName)
		{
			return null;
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool MoveToAttribute(string localName, string namespaceName)
		{
			return false;
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x0000365F File Offset: 0x0000185F
		public override string GetNamespace(string name)
		{
			return null;
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool MoveToNamespace(string prefix)
		{
			return false;
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool MoveToFirstNamespace(XPathNamespaceScope scope)
		{
			return false;
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool MoveToNextNamespace(XPathNamespaceScope scope)
		{
			return false;
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool MoveToParent()
		{
			return false;
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x0007C6A3 File Offset: 0x0007A8A3
		public override bool MoveTo(XPathNavigator other)
		{
			return this == other;
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x00093B26 File Offset: 0x00091D26
		public override XmlNodeOrder ComparePosition(XPathNavigator other)
		{
			if (this != other)
			{
				return XmlNodeOrder.Unknown;
			}
			return XmlNodeOrder.Same;
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x0007C6A3 File Offset: 0x0007A8A3
		public override bool IsSamePosition(XPathNavigator other)
		{
			return this == other;
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x00002068 File Offset: 0x00000268
		public override XPathNavigator Clone()
		{
			return this;
		}

		// Token: 0x0400156B RID: 5483
		private static volatile XmlEmptyNavigator singleton;
	}
}
