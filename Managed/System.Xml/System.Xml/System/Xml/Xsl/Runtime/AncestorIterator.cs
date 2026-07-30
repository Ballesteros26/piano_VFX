using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005E8 RID: 1512
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct AncestorIterator
	{
		// Token: 0x06003B41 RID: 15169 RVA: 0x0014D964 File Offset: 0x0014BB64
		public void Create(XPathNavigator context, XmlNavigatorFilter filter, bool orSelf)
		{
			this.filter = filter;
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.haveCurrent = orSelf && !this.filter.IsFiltered(this.navCurrent);
		}

		// Token: 0x06003B42 RID: 15170 RVA: 0x0014D99F File Offset: 0x0014BB9F
		public bool MoveNext()
		{
			if (this.haveCurrent)
			{
				this.haveCurrent = false;
				return true;
			}
			while (this.navCurrent.MoveToParent())
			{
				if (!this.filter.IsFiltered(this.navCurrent))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06003B43 RID: 15171 RVA: 0x0014D9D5 File Offset: 0x0014BBD5
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x040026FA RID: 9978
		private XmlNavigatorFilter filter;

		// Token: 0x040026FB RID: 9979
		private XPathNavigator navCurrent;

		// Token: 0x040026FC RID: 9980
		private bool haveCurrent;
	}
}
