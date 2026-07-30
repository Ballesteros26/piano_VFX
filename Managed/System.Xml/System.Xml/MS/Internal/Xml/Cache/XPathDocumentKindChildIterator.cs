using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x0200005E RID: 94
	internal class XPathDocumentKindChildIterator : XPathDocumentBaseIterator
	{
		// Token: 0x060002C9 RID: 713 RVA: 0x0000ADEA File Offset: 0x00008FEA
		public XPathDocumentKindChildIterator(XPathDocumentNavigator parent, XPathNodeType typ)
			: base(parent)
		{
			this.typ = typ;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000ADFA File Offset: 0x00008FFA
		public XPathDocumentKindChildIterator(XPathDocumentKindChildIterator iter)
			: base(iter)
		{
			this.typ = iter.typ;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000AE0F File Offset: 0x0000900F
		public override XPathNodeIterator Clone()
		{
			return new XPathDocumentKindChildIterator(this);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000AE18 File Offset: 0x00009018
		public override bool MoveNext()
		{
			if (this.pos == 0)
			{
				if (!this.ctxt.MoveToChild(this.typ))
				{
					return false;
				}
			}
			else if (!this.ctxt.MoveToNext(this.typ))
			{
				return false;
			}
			this.pos++;
			return true;
		}

		// Token: 0x04000183 RID: 387
		private XPathNodeType typ;
	}
}
