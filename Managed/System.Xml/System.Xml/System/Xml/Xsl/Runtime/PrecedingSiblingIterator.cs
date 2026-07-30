using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005E1 RID: 1505
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct PrecedingSiblingIterator
	{
		// Token: 0x06003B2B RID: 15147 RVA: 0x0014D4C2 File Offset: 0x0014B6C2
		public void Create(XPathNavigator context, XmlNavigatorFilter filter)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.filter = filter;
		}

		// Token: 0x06003B2C RID: 15148 RVA: 0x0014D4DD File Offset: 0x0014B6DD
		public bool MoveNext()
		{
			return this.filter.MoveToPreviousSibling(this.navCurrent);
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06003B2D RID: 15149 RVA: 0x0014D4F0 File Offset: 0x0014B6F0
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x040026DC RID: 9948
		private XmlNavigatorFilter filter;

		// Token: 0x040026DD RID: 9949
		private XPathNavigator navCurrent;
	}
}
