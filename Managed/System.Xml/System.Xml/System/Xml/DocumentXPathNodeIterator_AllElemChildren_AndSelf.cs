using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000209 RID: 521
	internal sealed class DocumentXPathNodeIterator_AllElemChildren_AndSelf : DocumentXPathNodeIterator_AllElemChildren
	{
		// Token: 0x060012E4 RID: 4836 RVA: 0x00070C03 File Offset: 0x0006EE03
		internal DocumentXPathNodeIterator_AllElemChildren_AndSelf(DocumentXPathNavigator nav)
			: base(nav)
		{
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00070C0C File Offset: 0x0006EE0C
		internal DocumentXPathNodeIterator_AllElemChildren_AndSelf(DocumentXPathNodeIterator_AllElemChildren_AndSelf other)
			: base(other)
		{
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x00070C15 File Offset: 0x0006EE15
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_AllElemChildren_AndSelf(this);
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00070C20 File Offset: 0x0006EE20
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
