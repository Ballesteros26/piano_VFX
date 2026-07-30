using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000609 RID: 1545
	internal class XmlNavNeverFilter : XmlNavigatorFilter
	{
		// Token: 0x06003C1C RID: 15388 RVA: 0x0014FF52 File Offset: 0x0014E152
		public static XmlNavigatorFilter Create()
		{
			return XmlNavNeverFilter.Singleton;
		}

		// Token: 0x06003C1D RID: 15389 RVA: 0x0014FF10 File Offset: 0x0014E110
		private XmlNavNeverFilter()
		{
		}

		// Token: 0x06003C1E RID: 15390 RVA: 0x0014FF59 File Offset: 0x0014E159
		public override bool MoveToContent(XPathNavigator navigator)
		{
			return XmlNavNeverFilter.MoveToFirstAttributeContent(navigator);
		}

		// Token: 0x06003C1F RID: 15391 RVA: 0x0014FF61 File Offset: 0x0014E161
		public override bool MoveToNextContent(XPathNavigator navigator)
		{
			return XmlNavNeverFilter.MoveToNextAttributeContent(navigator);
		}

		// Token: 0x06003C20 RID: 15392 RVA: 0x0014FF20 File Offset: 0x0014E120
		public override bool MoveToFollowingSibling(XPathNavigator navigator)
		{
			return navigator.MoveToNext();
		}

		// Token: 0x06003C21 RID: 15393 RVA: 0x0014FF28 File Offset: 0x0014E128
		public override bool MoveToPreviousSibling(XPathNavigator navigator)
		{
			return navigator.MoveToPrevious();
		}

		// Token: 0x06003C22 RID: 15394 RVA: 0x0014FF30 File Offset: 0x0014E130
		public override bool MoveToFollowing(XPathNavigator navigator, XPathNavigator navEnd)
		{
			return navigator.MoveToFollowing(XPathNodeType.All, navEnd);
		}

		// Token: 0x06003C23 RID: 15395 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool IsFiltered(XPathNavigator navigator)
		{
			return false;
		}

		// Token: 0x06003C24 RID: 15396 RVA: 0x0014FF69 File Offset: 0x0014E169
		public static bool MoveToFirstAttributeContent(XPathNavigator navigator)
		{
			return navigator.MoveToFirstAttribute() || navigator.MoveToFirstChild();
		}

		// Token: 0x06003C25 RID: 15397 RVA: 0x0014FF7B File Offset: 0x0014E17B
		public static bool MoveToNextAttributeContent(XPathNavigator navigator)
		{
			if (navigator.NodeType == XPathNodeType.Attribute)
			{
				if (!navigator.MoveToNextAttribute())
				{
					navigator.MoveToParent();
					if (!navigator.MoveToFirstChild())
					{
						navigator.MoveToFirstAttribute();
						while (navigator.MoveToNextAttribute())
						{
						}
						return false;
					}
				}
				return true;
			}
			return navigator.MoveToNext();
		}

		// Token: 0x0400277B RID: 10107
		private static XmlNavigatorFilter Singleton = new XmlNavNeverFilter();
	}
}
