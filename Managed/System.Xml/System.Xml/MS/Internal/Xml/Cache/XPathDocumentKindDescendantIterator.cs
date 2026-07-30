using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000060 RID: 96
	internal class XPathDocumentKindDescendantIterator : XPathDocumentBaseIterator
	{
		// Token: 0x060002D1 RID: 721 RVA: 0x0000AF81 File Offset: 0x00009181
		public XPathDocumentKindDescendantIterator(XPathDocumentNavigator root, XPathNodeType typ, bool matchSelf)
			: base(root)
		{
			this.typ = typ;
			this.matchSelf = matchSelf;
			if (root.NodeType != XPathNodeType.Root)
			{
				this.end = new XPathDocumentNavigator(root);
				this.end.MoveToNonDescendant();
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000AFB8 File Offset: 0x000091B8
		public XPathDocumentKindDescendantIterator(XPathDocumentKindDescendantIterator iter)
			: base(iter)
		{
			this.end = iter.end;
			this.typ = iter.typ;
			this.matchSelf = iter.matchSelf;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000AFE5 File Offset: 0x000091E5
		public override XPathNodeIterator Clone()
		{
			return new XPathDocumentKindDescendantIterator(this);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000AFF0 File Offset: 0x000091F0
		public override bool MoveNext()
		{
			if (this.matchSelf)
			{
				this.matchSelf = false;
				if (this.ctxt.IsKindMatch(this.typ))
				{
					this.pos++;
					return true;
				}
			}
			if (!this.ctxt.MoveToFollowing(this.typ, this.end))
			{
				return false;
			}
			this.pos++;
			return true;
		}

		// Token: 0x04000188 RID: 392
		private XPathDocumentNavigator end;

		// Token: 0x04000189 RID: 393
		private XPathNodeType typ;

		// Token: 0x0400018A RID: 394
		private bool matchSelf;
	}
}
