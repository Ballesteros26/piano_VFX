using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000607 RID: 1543
	internal class XmlNavTypeFilter : XmlNavigatorFilter
	{
		// Token: 0x06003C0A RID: 15370 RVA: 0x0014FE48 File Offset: 0x0014E048
		static XmlNavTypeFilter()
		{
			XmlNavTypeFilter.TypeFilters[1] = new XmlNavTypeFilter(XPathNodeType.Element);
			XmlNavTypeFilter.TypeFilters[4] = new XmlNavTypeFilter(XPathNodeType.Text);
			XmlNavTypeFilter.TypeFilters[7] = new XmlNavTypeFilter(XPathNodeType.ProcessingInstruction);
			XmlNavTypeFilter.TypeFilters[8] = new XmlNavTypeFilter(XPathNodeType.Comment);
		}

		// Token: 0x06003C0B RID: 15371 RVA: 0x0014FE95 File Offset: 0x0014E095
		public static XmlNavigatorFilter Create(XPathNodeType nodeType)
		{
			return XmlNavTypeFilter.TypeFilters[(int)nodeType];
		}

		// Token: 0x06003C0C RID: 15372 RVA: 0x0014FE9E File Offset: 0x0014E09E
		private XmlNavTypeFilter(XPathNodeType nodeType)
		{
			this.nodeType = nodeType;
			this.mask = XPathNavigator.GetContentKindMask(nodeType);
		}

		// Token: 0x06003C0D RID: 15373 RVA: 0x0014FEB9 File Offset: 0x0014E0B9
		public override bool MoveToContent(XPathNavigator navigator)
		{
			return navigator.MoveToChild(this.nodeType);
		}

		// Token: 0x06003C0E RID: 15374 RVA: 0x0014FEC7 File Offset: 0x0014E0C7
		public override bool MoveToNextContent(XPathNavigator navigator)
		{
			return navigator.MoveToNext(this.nodeType);
		}

		// Token: 0x06003C0F RID: 15375 RVA: 0x0014FEC7 File Offset: 0x0014E0C7
		public override bool MoveToFollowingSibling(XPathNavigator navigator)
		{
			return navigator.MoveToNext(this.nodeType);
		}

		// Token: 0x06003C10 RID: 15376 RVA: 0x0014FED5 File Offset: 0x0014E0D5
		public override bool MoveToPreviousSibling(XPathNavigator navigator)
		{
			return navigator.MoveToPrevious(this.nodeType);
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x0014FEE3 File Offset: 0x0014E0E3
		public override bool MoveToFollowing(XPathNavigator navigator, XPathNavigator navEnd)
		{
			return navigator.MoveToFollowing(this.nodeType, navEnd);
		}

		// Token: 0x06003C12 RID: 15378 RVA: 0x0014FEF2 File Offset: 0x0014E0F2
		public override bool IsFiltered(XPathNavigator navigator)
		{
			return ((1 << (int)navigator.NodeType) & this.mask) == 0;
		}

		// Token: 0x04002777 RID: 10103
		private static XmlNavigatorFilter[] TypeFilters = new XmlNavigatorFilter[9];

		// Token: 0x04002778 RID: 10104
		private XPathNodeType nodeType;

		// Token: 0x04002779 RID: 10105
		private int mask;
	}
}
