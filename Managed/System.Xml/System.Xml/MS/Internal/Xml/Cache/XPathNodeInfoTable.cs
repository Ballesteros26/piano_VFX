using System;
using System.Text;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000067 RID: 103
	internal sealed class XPathNodeInfoTable
	{
		// Token: 0x06000368 RID: 872 RVA: 0x0000D037 File Offset: 0x0000B237
		public XPathNodeInfoTable()
		{
			this.hashTable = new XPathNodeInfoAtom[32];
			this.sizeTable = 0;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000D054 File Offset: 0x0000B254
		public XPathNodeInfoAtom Create(string localName, string namespaceUri, string prefix, string baseUri, XPathNode[] pageParent, XPathNode[] pageSibling, XPathNode[] pageSimilar, XPathDocument doc, int lineNumBase, int linePosBase)
		{
			XPathNodeInfoAtom xpathNodeInfoAtom;
			if (this.infoCached == null)
			{
				xpathNodeInfoAtom = new XPathNodeInfoAtom(localName, namespaceUri, prefix, baseUri, pageParent, pageSibling, pageSimilar, doc, lineNumBase, linePosBase);
			}
			else
			{
				xpathNodeInfoAtom = this.infoCached;
				this.infoCached = xpathNodeInfoAtom.Next;
				xpathNodeInfoAtom.Init(localName, namespaceUri, prefix, baseUri, pageParent, pageSibling, pageSimilar, doc, lineNumBase, linePosBase);
			}
			return this.Atomize(xpathNodeInfoAtom);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000D0B4 File Offset: 0x0000B2B4
		private XPathNodeInfoAtom Atomize(XPathNodeInfoAtom info)
		{
			for (XPathNodeInfoAtom xpathNodeInfoAtom = this.hashTable[info.GetHashCode() & (this.hashTable.Length - 1)]; xpathNodeInfoAtom != null; xpathNodeInfoAtom = xpathNodeInfoAtom.Next)
			{
				if (info.Equals(xpathNodeInfoAtom))
				{
					info.Next = this.infoCached;
					this.infoCached = info;
					return xpathNodeInfoAtom;
				}
			}
			if (this.sizeTable >= this.hashTable.Length)
			{
				XPathNodeInfoAtom[] array = this.hashTable;
				this.hashTable = new XPathNodeInfoAtom[array.Length * 2];
				foreach (XPathNodeInfoAtom xpathNodeInfoAtom in array)
				{
					while (xpathNodeInfoAtom != null)
					{
						XPathNodeInfoAtom next = xpathNodeInfoAtom.Next;
						this.AddInfo(xpathNodeInfoAtom);
						xpathNodeInfoAtom = next;
					}
				}
			}
			this.AddInfo(info);
			return info;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000D158 File Offset: 0x0000B358
		private void AddInfo(XPathNodeInfoAtom info)
		{
			int num = info.GetHashCode() & (this.hashTable.Length - 1);
			info.Next = this.hashTable[num];
			this.hashTable[num] = info;
			this.sizeTable++;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000D19C File Offset: 0x0000B39C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.hashTable.Length; i++)
			{
				stringBuilder.AppendFormat("{0,4}: ", i);
				for (XPathNodeInfoAtom xpathNodeInfoAtom = this.hashTable[i]; xpathNodeInfoAtom != null; xpathNodeInfoAtom = xpathNodeInfoAtom.Next)
				{
					if (xpathNodeInfoAtom != this.hashTable[i])
					{
						stringBuilder.Append("\n      ");
					}
					stringBuilder.Append(xpathNodeInfoAtom);
				}
				stringBuilder.Append('\n');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040001B8 RID: 440
		private XPathNodeInfoAtom[] hashTable;

		// Token: 0x040001B9 RID: 441
		private int sizeTable;

		// Token: 0x040001BA RID: 442
		private XPathNodeInfoAtom infoCached;

		// Token: 0x040001BB RID: 443
		private const int DefaultTableSize = 32;
	}
}
