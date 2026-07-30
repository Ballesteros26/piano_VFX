using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x0200020A RID: 522
	internal class DocumentXPathNodeIterator_ElemChildren_NoLocalName : DocumentXPathNodeIterator_ElemDescendants
	{
		// Token: 0x060012E8 RID: 4840 RVA: 0x00070C6C File Offset: 0x0006EE6C
		internal DocumentXPathNodeIterator_ElemChildren_NoLocalName(DocumentXPathNavigator nav, string nsAtom)
			: base(nav)
		{
			this.nsAtom = nsAtom;
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00070C7C File Offset: 0x0006EE7C
		internal DocumentXPathNodeIterator_ElemChildren_NoLocalName(DocumentXPathNodeIterator_ElemChildren_NoLocalName other)
			: base(other)
		{
			this.nsAtom = other.nsAtom;
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00070C91 File Offset: 0x0006EE91
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_ElemChildren_NoLocalName(this);
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00070C99 File Offset: 0x0006EE99
		protected override bool Match(XmlNode node)
		{
			return Ref.Equal(node.NamespaceURI, this.nsAtom);
		}

		// Token: 0x04000D44 RID: 3396
		private string nsAtom;
	}
}
