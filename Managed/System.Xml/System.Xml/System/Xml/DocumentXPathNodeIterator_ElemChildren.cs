using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x0200020C RID: 524
	internal class DocumentXPathNodeIterator_ElemChildren : DocumentXPathNodeIterator_ElemDescendants
	{
		// Token: 0x060012F0 RID: 4848 RVA: 0x00070D14 File Offset: 0x0006EF14
		internal DocumentXPathNodeIterator_ElemChildren(DocumentXPathNavigator nav, string localNameAtom, string nsAtom)
			: base(nav)
		{
			this.localNameAtom = localNameAtom;
			this.nsAtom = nsAtom;
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00070D2B File Offset: 0x0006EF2B
		internal DocumentXPathNodeIterator_ElemChildren(DocumentXPathNodeIterator_ElemChildren other)
			: base(other)
		{
			this.localNameAtom = other.localNameAtom;
			this.nsAtom = other.nsAtom;
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00070D4C File Offset: 0x0006EF4C
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_ElemChildren(this);
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x00070D54 File Offset: 0x0006EF54
		protected override bool Match(XmlNode node)
		{
			return Ref.Equal(node.LocalName, this.localNameAtom) && Ref.Equal(node.NamespaceURI, this.nsAtom);
		}

		// Token: 0x04000D45 RID: 3397
		protected string localNameAtom;

		// Token: 0x04000D46 RID: 3398
		protected string nsAtom;
	}
}
