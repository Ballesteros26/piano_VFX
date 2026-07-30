using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005CE RID: 1486
	internal class DocumentOrderComparer : IComparer<XPathNavigator>
	{
		// Token: 0x06003AE0 RID: 15072 RVA: 0x0014CAF8 File Offset: 0x0014ACF8
		public int Compare(XPathNavigator navThis, XPathNavigator navThat)
		{
			switch (navThis.ComparePosition(navThat))
			{
			case XmlNodeOrder.Before:
				return -1;
			case XmlNodeOrder.After:
				return 1;
			case XmlNodeOrder.Same:
				return 0;
			default:
				if (this.roots == null)
				{
					this.roots = new List<XPathNavigator>();
				}
				if (this.GetDocumentIndex(navThis) >= this.GetDocumentIndex(navThat))
				{
					return 1;
				}
				return -1;
			}
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x0014CB50 File Offset: 0x0014AD50
		public int GetDocumentIndex(XPathNavigator nav)
		{
			if (this.roots == null)
			{
				this.roots = new List<XPathNavigator>();
			}
			XPathNavigator xpathNavigator = nav.Clone();
			xpathNavigator.MoveToRoot();
			for (int i = 0; i < this.roots.Count; i++)
			{
				if (xpathNavigator.IsSamePosition(this.roots[i]))
				{
					return i;
				}
			}
			this.roots.Add(xpathNavigator);
			return this.roots.Count - 1;
		}

		// Token: 0x04002675 RID: 9845
		private List<XPathNavigator> roots;
	}
}
