using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005EA RID: 1514
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct XPathFollowingIterator
	{
		// Token: 0x06003B47 RID: 15175 RVA: 0x0014DA57 File Offset: 0x0014BC57
		public void Create(XPathNavigator input, XmlNavigatorFilter filter)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, input);
			this.filter = filter;
			this.needFirst = true;
		}

		// Token: 0x06003B48 RID: 15176 RVA: 0x0014DA79 File Offset: 0x0014BC79
		public bool MoveNext()
		{
			if (!this.needFirst)
			{
				return this.filter.MoveToFollowing(this.navCurrent, null);
			}
			if (!XPathFollowingIterator.MoveFirst(this.filter, this.navCurrent))
			{
				return false;
			}
			this.needFirst = false;
			return true;
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06003B49 RID: 15177 RVA: 0x0014DAB3 File Offset: 0x0014BCB3
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x06003B4A RID: 15178 RVA: 0x0014DABC File Offset: 0x0014BCBC
		internal static bool MoveFirst(XmlNavigatorFilter filter, XPathNavigator nav)
		{
			if (nav.NodeType == XPathNodeType.Attribute || nav.NodeType == XPathNodeType.Namespace)
			{
				if (!nav.MoveToParent())
				{
					return false;
				}
				if (!filter.MoveToFollowing(nav, null))
				{
					return false;
				}
			}
			else
			{
				if (!nav.MoveToNonDescendant())
				{
					return false;
				}
				if (filter.IsFiltered(nav) && !filter.MoveToFollowing(nav, null))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040026FF RID: 9983
		private XmlNavigatorFilter filter;

		// Token: 0x04002700 RID: 9984
		private XPathNavigator navCurrent;

		// Token: 0x04002701 RID: 9985
		private bool needFirst;
	}
}
