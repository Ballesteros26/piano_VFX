using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005E2 RID: 1506
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct PrecedingSiblingDocOrderIterator
	{
		// Token: 0x06003B2E RID: 15150 RVA: 0x0014D4F8 File Offset: 0x0014B6F8
		public void Create(XPathNavigator context, XmlNavigatorFilter filter)
		{
			this.filter = filter;
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.navEnd = XmlQueryRuntime.SyncToNavigator(this.navEnd, context);
			this.needFirst = true;
			this.useCompPos = this.filter.IsFiltered(context);
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x0014D54C File Offset: 0x0014B74C
		public bool MoveNext()
		{
			if (this.needFirst)
			{
				if (!this.navCurrent.MoveToParent())
				{
					return false;
				}
				if (!this.filter.MoveToContent(this.navCurrent))
				{
					return false;
				}
				this.needFirst = false;
			}
			else if (!this.filter.MoveToFollowingSibling(this.navCurrent))
			{
				return false;
			}
			if (this.useCompPos)
			{
				return this.navCurrent.ComparePosition(this.navEnd) == XmlNodeOrder.Before;
			}
			if (this.navCurrent.IsSamePosition(this.navEnd))
			{
				this.useCompPos = true;
				return false;
			}
			return true;
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06003B30 RID: 15152 RVA: 0x0014D5DD File Offset: 0x0014B7DD
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x040026DE RID: 9950
		private XmlNavigatorFilter filter;

		// Token: 0x040026DF RID: 9951
		private XPathNavigator navCurrent;

		// Token: 0x040026E0 RID: 9952
		private XPathNavigator navEnd;

		// Token: 0x040026E1 RID: 9953
		private bool needFirst;

		// Token: 0x040026E2 RID: 9954
		private bool useCompPos;
	}
}
