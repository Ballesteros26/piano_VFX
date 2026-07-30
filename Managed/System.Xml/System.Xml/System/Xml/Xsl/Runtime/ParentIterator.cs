using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005E7 RID: 1511
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct ParentIterator
	{
		// Token: 0x06003B3E RID: 15166 RVA: 0x0014D90F File Offset: 0x0014BB0F
		public void Create(XPathNavigator context, XmlNavigatorFilter filter)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.haveCurrent = this.navCurrent.MoveToParent() && !filter.IsFiltered(this.navCurrent);
		}

		// Token: 0x06003B3F RID: 15167 RVA: 0x0014D948 File Offset: 0x0014BB48
		public bool MoveNext()
		{
			if (this.haveCurrent)
			{
				this.haveCurrent = false;
				return true;
			}
			return false;
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06003B40 RID: 15168 RVA: 0x0014D95C File Offset: 0x0014BB5C
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x040026F8 RID: 9976
		private XPathNavigator navCurrent;

		// Token: 0x040026F9 RID: 9977
		private bool haveCurrent;
	}
}
