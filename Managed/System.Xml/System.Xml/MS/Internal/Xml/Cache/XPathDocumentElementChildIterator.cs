using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x0200005D RID: 93
	internal class XPathDocumentElementChildIterator : XPathDocumentBaseIterator
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x0000AD36 File Offset: 0x00008F36
		public XPathDocumentElementChildIterator(XPathDocumentNavigator parent, string name, string namespaceURI)
			: base(parent)
		{
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			this.localName = parent.NameTable.Get(name);
			this.namespaceUri = namespaceURI;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000AD66 File Offset: 0x00008F66
		public XPathDocumentElementChildIterator(XPathDocumentElementChildIterator iter)
			: base(iter)
		{
			this.localName = iter.localName;
			this.namespaceUri = iter.namespaceUri;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000AD87 File Offset: 0x00008F87
		public override XPathNodeIterator Clone()
		{
			return new XPathDocumentElementChildIterator(this);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000AD90 File Offset: 0x00008F90
		public override bool MoveNext()
		{
			if (this.pos == 0)
			{
				if (!this.ctxt.MoveToChild(this.localName, this.namespaceUri))
				{
					return false;
				}
			}
			else if (!this.ctxt.MoveToNext(this.localName, this.namespaceUri))
			{
				return false;
			}
			this.pos++;
			return true;
		}

		// Token: 0x04000181 RID: 385
		private string localName;

		// Token: 0x04000182 RID: 386
		private string namespaceUri;
	}
}
