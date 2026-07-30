using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000208 RID: 520
	internal class DocumentXPathNodeIterator_AllElemChildren : DocumentXPathNodeIterator_ElemDescendants
	{
		// Token: 0x060012E0 RID: 4832 RVA: 0x00070BDE File Offset: 0x0006EDDE
		internal DocumentXPathNodeIterator_AllElemChildren(DocumentXPathNavigator nav)
			: base(nav)
		{
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x00070BE7 File Offset: 0x0006EDE7
		internal DocumentXPathNodeIterator_AllElemChildren(DocumentXPathNodeIterator_AllElemChildren other)
			: base(other)
		{
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x00070BF0 File Offset: 0x0006EDF0
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_AllElemChildren(this);
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00070BF8 File Offset: 0x0006EDF8
		protected override bool Match(XmlNode node)
		{
			return node.NodeType == XmlNodeType.Element;
		}
	}
}
