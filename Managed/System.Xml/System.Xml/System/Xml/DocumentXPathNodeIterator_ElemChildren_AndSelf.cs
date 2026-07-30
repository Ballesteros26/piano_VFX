using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x0200020D RID: 525
	internal sealed class DocumentXPathNodeIterator_ElemChildren_AndSelf : DocumentXPathNodeIterator_ElemChildren
	{
		// Token: 0x060012F4 RID: 4852 RVA: 0x00070D7C File Offset: 0x0006EF7C
		internal DocumentXPathNodeIterator_ElemChildren_AndSelf(DocumentXPathNavigator nav, string localNameAtom, string nsAtom)
			: base(nav, localNameAtom, nsAtom)
		{
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x00070D87 File Offset: 0x0006EF87
		internal DocumentXPathNodeIterator_ElemChildren_AndSelf(DocumentXPathNodeIterator_ElemChildren_AndSelf other)
			: base(other)
		{
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00070D90 File Offset: 0x0006EF90
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_ElemChildren_AndSelf(this);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x00070D98 File Offset: 0x0006EF98
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
