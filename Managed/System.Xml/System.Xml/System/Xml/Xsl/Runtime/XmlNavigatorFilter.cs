using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000605 RID: 1541
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class XmlNavigatorFilter
	{
		// Token: 0x06003BFB RID: 15355
		public abstract bool MoveToContent(XPathNavigator navigator);

		// Token: 0x06003BFC RID: 15356
		public abstract bool MoveToNextContent(XPathNavigator navigator);

		// Token: 0x06003BFD RID: 15357
		public abstract bool MoveToFollowingSibling(XPathNavigator navigator);

		// Token: 0x06003BFE RID: 15358
		public abstract bool MoveToPreviousSibling(XPathNavigator navigator);

		// Token: 0x06003BFF RID: 15359
		public abstract bool MoveToFollowing(XPathNavigator navigator, XPathNavigator navigatorEnd);

		// Token: 0x06003C00 RID: 15360
		public abstract bool IsFiltered(XPathNavigator navigator);
	}
}
