using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x0200005C RID: 92
	internal abstract class XPathDocumentBaseIterator : XPathNodeIterator
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x0000ACED File Offset: 0x00008EED
		protected XPathDocumentBaseIterator(XPathDocumentNavigator ctxt)
		{
			this.ctxt = new XPathDocumentNavigator(ctxt);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000AD01 File Offset: 0x00008F01
		protected XPathDocumentBaseIterator(XPathDocumentBaseIterator iter)
		{
			this.ctxt = new XPathDocumentNavigator(iter.ctxt);
			this.pos = iter.pos;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000AD26 File Offset: 0x00008F26
		public override XPathNavigator Current
		{
			get
			{
				return this.ctxt;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000AD2E File Offset: 0x00008F2E
		public override int CurrentPosition
		{
			get
			{
				return this.pos;
			}
		}

		// Token: 0x0400017F RID: 383
		protected XPathDocumentNavigator ctxt;

		// Token: 0x04000180 RID: 384
		protected int pos;
	}
}
