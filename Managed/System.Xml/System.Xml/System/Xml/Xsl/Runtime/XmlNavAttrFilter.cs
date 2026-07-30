using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000608 RID: 1544
	internal class XmlNavAttrFilter : XmlNavigatorFilter
	{
		// Token: 0x06003C13 RID: 15379 RVA: 0x0014FF09 File Offset: 0x0014E109
		public static XmlNavigatorFilter Create()
		{
			return XmlNavAttrFilter.Singleton;
		}

		// Token: 0x06003C14 RID: 15380 RVA: 0x0014FF10 File Offset: 0x0014E110
		private XmlNavAttrFilter()
		{
		}

		// Token: 0x06003C15 RID: 15381 RVA: 0x0014FF18 File Offset: 0x0014E118
		public override bool MoveToContent(XPathNavigator navigator)
		{
			return navigator.MoveToFirstChild();
		}

		// Token: 0x06003C16 RID: 15382 RVA: 0x0014FF20 File Offset: 0x0014E120
		public override bool MoveToNextContent(XPathNavigator navigator)
		{
			return navigator.MoveToNext();
		}

		// Token: 0x06003C17 RID: 15383 RVA: 0x0014FF20 File Offset: 0x0014E120
		public override bool MoveToFollowingSibling(XPathNavigator navigator)
		{
			return navigator.MoveToNext();
		}

		// Token: 0x06003C18 RID: 15384 RVA: 0x0014FF28 File Offset: 0x0014E128
		public override bool MoveToPreviousSibling(XPathNavigator navigator)
		{
			return navigator.MoveToPrevious();
		}

		// Token: 0x06003C19 RID: 15385 RVA: 0x0014FF30 File Offset: 0x0014E130
		public override bool MoveToFollowing(XPathNavigator navigator, XPathNavigator navEnd)
		{
			return navigator.MoveToFollowing(XPathNodeType.All, navEnd);
		}

		// Token: 0x06003C1A RID: 15386 RVA: 0x0014FF3B File Offset: 0x0014E13B
		public override bool IsFiltered(XPathNavigator navigator)
		{
			return navigator.NodeType == XPathNodeType.Attribute;
		}

		// Token: 0x0400277A RID: 10106
		private static XmlNavigatorFilter Singleton = new XmlNavAttrFilter();
	}
}
