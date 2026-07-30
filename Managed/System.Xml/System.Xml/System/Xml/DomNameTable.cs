using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x02000212 RID: 530
	internal class DomNameTable
	{
		// Token: 0x06001328 RID: 4904 RVA: 0x00071798 File Offset: 0x0006F998
		public DomNameTable(XmlDocument document)
		{
			this.ownerDocument = document;
			this.nameTable = document.NameTable;
			this.entries = new XmlName[64];
			this.mask = 63;
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x000717C8 File Offset: 0x0006F9C8
		public XmlName GetName(string prefix, string localName, string ns, IXmlSchemaInfo schemaInfo)
		{
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			if (ns == null)
			{
				ns = string.Empty;
			}
			int hashCode = XmlName.GetHashCode(localName);
			for (XmlName xmlName = this.entries[hashCode & this.mask]; xmlName != null; xmlName = xmlName.next)
			{
				if (xmlName.HashCode == hashCode && (xmlName.LocalName == localName || xmlName.LocalName.Equals(localName)) && (xmlName.Prefix == prefix || xmlName.Prefix.Equals(prefix)) && (xmlName.NamespaceURI == ns || xmlName.NamespaceURI.Equals(ns)) && xmlName.Equals(schemaInfo))
				{
					return xmlName;
				}
			}
			return null;
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00071868 File Offset: 0x0006FA68
		public XmlName AddName(string prefix, string localName, string ns, IXmlSchemaInfo schemaInfo)
		{
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			if (ns == null)
			{
				ns = string.Empty;
			}
			int hashCode = XmlName.GetHashCode(localName);
			for (XmlName xmlName = this.entries[hashCode & this.mask]; xmlName != null; xmlName = xmlName.next)
			{
				if (xmlName.HashCode == hashCode && (xmlName.LocalName == localName || xmlName.LocalName.Equals(localName)) && (xmlName.Prefix == prefix || xmlName.Prefix.Equals(prefix)) && (xmlName.NamespaceURI == ns || xmlName.NamespaceURI.Equals(ns)) && xmlName.Equals(schemaInfo))
				{
					return xmlName;
				}
			}
			prefix = this.nameTable.Add(prefix);
			localName = this.nameTable.Add(localName);
			ns = this.nameTable.Add(ns);
			int num = hashCode & this.mask;
			XmlName xmlName2 = XmlName.Create(prefix, localName, ns, hashCode, this.ownerDocument, this.entries[num], schemaInfo);
			this.entries[num] = xmlName2;
			int num2 = this.count;
			this.count = num2 + 1;
			if (num2 == this.mask)
			{
				this.Grow();
			}
			return xmlName2;
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00071980 File Offset: 0x0006FB80
		private void Grow()
		{
			int num = this.mask * 2 + 1;
			XmlName[] array = this.entries;
			XmlName[] array2 = new XmlName[num + 1];
			foreach (XmlName xmlName in array)
			{
				while (xmlName != null)
				{
					int num2 = xmlName.HashCode & num;
					XmlName next = xmlName.next;
					xmlName.next = array2[num2];
					array2[num2] = xmlName;
					xmlName = next;
				}
			}
			this.entries = array2;
			this.mask = num;
		}

		// Token: 0x04000D72 RID: 3442
		private XmlName[] entries;

		// Token: 0x04000D73 RID: 3443
		private int count;

		// Token: 0x04000D74 RID: 3444
		private int mask;

		// Token: 0x04000D75 RID: 3445
		private XmlDocument ownerDocument;

		// Token: 0x04000D76 RID: 3446
		private XmlNameTable nameTable;

		// Token: 0x04000D77 RID: 3447
		private const int InitialSize = 64;
	}
}
