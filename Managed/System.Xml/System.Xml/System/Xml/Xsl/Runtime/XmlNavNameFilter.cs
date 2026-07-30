using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000606 RID: 1542
	internal class XmlNavNameFilter : XmlNavigatorFilter
	{
		// Token: 0x06003C02 RID: 15362 RVA: 0x0014FDAD File Offset: 0x0014DFAD
		public static XmlNavigatorFilter Create(string localName, string namespaceUri)
		{
			return new XmlNavNameFilter(localName, namespaceUri);
		}

		// Token: 0x06003C03 RID: 15363 RVA: 0x0014FDB6 File Offset: 0x0014DFB6
		private XmlNavNameFilter(string localName, string namespaceUri)
		{
			this.localName = localName;
			this.namespaceUri = namespaceUri;
		}

		// Token: 0x06003C04 RID: 15364 RVA: 0x0014FDCC File Offset: 0x0014DFCC
		public override bool MoveToContent(XPathNavigator navigator)
		{
			return navigator.MoveToChild(this.localName, this.namespaceUri);
		}

		// Token: 0x06003C05 RID: 15365 RVA: 0x0014FDE0 File Offset: 0x0014DFE0
		public override bool MoveToNextContent(XPathNavigator navigator)
		{
			return navigator.MoveToNext(this.localName, this.namespaceUri);
		}

		// Token: 0x06003C06 RID: 15366 RVA: 0x0014FDE0 File Offset: 0x0014DFE0
		public override bool MoveToFollowingSibling(XPathNavigator navigator)
		{
			return navigator.MoveToNext(this.localName, this.namespaceUri);
		}

		// Token: 0x06003C07 RID: 15367 RVA: 0x0014FDF4 File Offset: 0x0014DFF4
		public override bool MoveToPreviousSibling(XPathNavigator navigator)
		{
			return navigator.MoveToPrevious(this.localName, this.namespaceUri);
		}

		// Token: 0x06003C08 RID: 15368 RVA: 0x0014FE08 File Offset: 0x0014E008
		public override bool MoveToFollowing(XPathNavigator navigator, XPathNavigator navEnd)
		{
			return navigator.MoveToFollowing(this.localName, this.namespaceUri, navEnd);
		}

		// Token: 0x06003C09 RID: 15369 RVA: 0x0014FE1D File Offset: 0x0014E01D
		public override bool IsFiltered(XPathNavigator navigator)
		{
			return navigator.LocalName != this.localName || navigator.NamespaceURI != this.namespaceUri;
		}

		// Token: 0x04002775 RID: 10101
		private string localName;

		// Token: 0x04002776 RID: 10102
		private string namespaceUri;
	}
}
