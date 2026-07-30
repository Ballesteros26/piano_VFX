using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x0200005F RID: 95
	internal class XPathDocumentElementDescendantIterator : XPathDocumentBaseIterator
	{
		// Token: 0x060002CD RID: 717 RVA: 0x0000AE68 File Offset: 0x00009068
		public XPathDocumentElementDescendantIterator(XPathDocumentNavigator root, string name, string namespaceURI, bool matchSelf)
			: base(root)
		{
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			this.localName = root.NameTable.Get(name);
			this.namespaceUri = namespaceURI;
			this.matchSelf = matchSelf;
			if (root.NodeType != XPathNodeType.Root)
			{
				this.end = new XPathDocumentNavigator(root);
				this.end.MoveToNonDescendant();
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000AECB File Offset: 0x000090CB
		public XPathDocumentElementDescendantIterator(XPathDocumentElementDescendantIterator iter)
			: base(iter)
		{
			this.end = iter.end;
			this.localName = iter.localName;
			this.namespaceUri = iter.namespaceUri;
			this.matchSelf = iter.matchSelf;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000AF04 File Offset: 0x00009104
		public override XPathNodeIterator Clone()
		{
			return new XPathDocumentElementDescendantIterator(this);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000AF0C File Offset: 0x0000910C
		public override bool MoveNext()
		{
			if (this.matchSelf)
			{
				this.matchSelf = false;
				if (this.ctxt.IsElementMatch(this.localName, this.namespaceUri))
				{
					this.pos++;
					return true;
				}
			}
			if (!this.ctxt.MoveToFollowing(this.localName, this.namespaceUri, this.end))
			{
				return false;
			}
			this.pos++;
			return true;
		}

		// Token: 0x04000184 RID: 388
		private XPathDocumentNavigator end;

		// Token: 0x04000185 RID: 389
		private string localName;

		// Token: 0x04000186 RID: 390
		private string namespaceUri;

		// Token: 0x04000187 RID: 391
		private bool matchSelf;
	}
}
