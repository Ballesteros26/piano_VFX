using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005ED RID: 1517
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct PrecedingIterator
	{
		// Token: 0x06003B50 RID: 15184 RVA: 0x0014DC5C File Offset: 0x0014BE5C
		public void Create(XPathNavigator context, XmlNavigatorFilter filter)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.navCurrent.MoveToRoot();
			this.stack.Reset();
			if (!this.navCurrent.IsSamePosition(context))
			{
				if (!filter.IsFiltered(this.navCurrent))
				{
					this.stack.Push(this.navCurrent.Clone());
				}
				while (filter.MoveToFollowing(this.navCurrent, context))
				{
					this.stack.Push(this.navCurrent.Clone());
				}
			}
		}

		// Token: 0x06003B51 RID: 15185 RVA: 0x0014DCEA File Offset: 0x0014BEEA
		public bool MoveNext()
		{
			if (this.stack.IsEmpty)
			{
				return false;
			}
			this.navCurrent = this.stack.Pop();
			return true;
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06003B52 RID: 15186 RVA: 0x0014DD0D File Offset: 0x0014BF0D
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x0400270C RID: 9996
		private XmlNavigatorStack stack;

		// Token: 0x0400270D RID: 9997
		private XPathNavigator navCurrent;
	}
}
