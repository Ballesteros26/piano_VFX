using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000618 RID: 1560
	internal class XmlCachedSequenceWriter : XmlSequenceWriter
	{
		// Token: 0x06003D4C RID: 15692 RVA: 0x001533BF File Offset: 0x001515BF
		public XmlCachedSequenceWriter()
		{
			this.seqTyped = new XmlQueryItemSequence();
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06003D4D RID: 15693 RVA: 0x001533D2 File Offset: 0x001515D2
		public XmlQueryItemSequence ResultSequence
		{
			get
			{
				return this.seqTyped;
			}
		}

		// Token: 0x06003D4E RID: 15694 RVA: 0x001533DA File Offset: 0x001515DA
		public override XmlRawWriter StartTree(XPathNodeType rootType, IXmlNamespaceResolver nsResolver, XmlNameTable nameTable)
		{
			this.doc = new XPathDocument(nameTable);
			this.writer = this.doc.LoadFromWriter(XPathDocument.LoadFlags.AtomizeNames | ((rootType == XPathNodeType.Root) ? XPathDocument.LoadFlags.None : XPathDocument.LoadFlags.Fragment), string.Empty);
			this.writer.NamespaceResolver = nsResolver;
			return this.writer;
		}

		// Token: 0x06003D4F RID: 15695 RVA: 0x00153419 File Offset: 0x00151619
		public override void EndTree()
		{
			this.writer.Close();
			this.seqTyped.Add(this.doc.CreateNavigator());
		}

		// Token: 0x06003D50 RID: 15696 RVA: 0x0015343C File Offset: 0x0015163C
		public override void WriteItem(XPathItem item)
		{
			this.seqTyped.AddClone(item);
		}

		// Token: 0x040027C6 RID: 10182
		private XmlQueryItemSequence seqTyped;

		// Token: 0x040027C7 RID: 10183
		private XPathDocument doc;

		// Token: 0x040027C8 RID: 10184
		private XmlRawWriter writer;
	}
}
