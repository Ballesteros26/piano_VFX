using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x0200020B RID: 523
	internal sealed class DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName : DocumentXPathNodeIterator_ElemChildren_NoLocalName
	{
		// Token: 0x060012EC RID: 4844 RVA: 0x00070CAC File Offset: 0x0006EEAC
		internal DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName(DocumentXPathNavigator nav, string nsAtom)
			: base(nav, nsAtom)
		{
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00070CB6 File Offset: 0x0006EEB6
		internal DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName(DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName other)
			: base(other)
		{
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x00070CBF File Offset: 0x0006EEBF
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName(this);
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00070CC8 File Offset: 0x0006EEC8
		public override bool MoveNext()
		{
			if (this.CurrentPosition == 0)
			{
				XmlNode xmlNode = (XmlNode)((DocumentXPathNavigator)this.Current).UnderlyingObject;
				if (xmlNode.NodeType == XmlNodeType.Element && this.Match(xmlNode))
				{
					base.SetPosition(1);
					return true;
				}
			}
			return base.MoveNext();
		}
	}
}
