using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005DF RID: 1503
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct FollowingSiblingIterator
	{
		// Token: 0x06003B25 RID: 15141 RVA: 0x0014D462 File Offset: 0x0014B662
		public void Create(XPathNavigator context, XmlNavigatorFilter filter)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.filter = filter;
		}

		// Token: 0x06003B26 RID: 15142 RVA: 0x0014D47D File Offset: 0x0014B67D
		public bool MoveNext()
		{
			return this.filter.MoveToFollowingSibling(this.navCurrent);
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06003B27 RID: 15143 RVA: 0x0014D490 File Offset: 0x0014B690
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x040026D9 RID: 9945
		private XmlNavigatorFilter filter;

		// Token: 0x040026DA RID: 9946
		private XPathNavigator navCurrent;
	}
}
