using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005E4 RID: 1508
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct DescendantIterator
	{
		// Token: 0x06003B38 RID: 15160 RVA: 0x0014D790 File Offset: 0x0014B990
		public void Create(XPathNavigator input, XmlNavigatorFilter filter, bool orSelf)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, input);
			this.filter = filter;
			if (input.NodeType == XPathNodeType.Root)
			{
				this.navEnd = null;
			}
			else
			{
				this.navEnd = XmlQueryRuntime.SyncToNavigator(this.navEnd, input);
				this.navEnd.MoveToNonDescendant();
			}
			this.hasFirst = orSelf && !this.filter.IsFiltered(this.navCurrent);
		}

		// Token: 0x06003B39 RID: 15161 RVA: 0x0014D805 File Offset: 0x0014BA05
		public bool MoveNext()
		{
			if (this.hasFirst)
			{
				this.hasFirst = false;
				return true;
			}
			return this.filter.MoveToFollowing(this.navCurrent, this.navEnd);
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06003B3A RID: 15162 RVA: 0x0014D82F File Offset: 0x0014BA2F
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x040026EA RID: 9962
		private XmlNavigatorFilter filter;

		// Token: 0x040026EB RID: 9963
		private XPathNavigator navCurrent;

		// Token: 0x040026EC RID: 9964
		private XPathNavigator navEnd;

		// Token: 0x040026ED RID: 9965
		private bool hasFirst;
	}
}
