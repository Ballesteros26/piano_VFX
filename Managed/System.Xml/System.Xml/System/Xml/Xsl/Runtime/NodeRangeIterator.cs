using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005F2 RID: 1522
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct NodeRangeIterator
	{
		// Token: 0x06003B5E RID: 15198 RVA: 0x0014DFB8 File Offset: 0x0014C1B8
		public void Create(XPathNavigator start, XmlNavigatorFilter filter, XPathNavigator end)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, start);
			this.navEnd = XmlQueryRuntime.SyncToNavigator(this.navEnd, end);
			this.filter = filter;
			if (start.IsSamePosition(end))
			{
				this.state = ((!filter.IsFiltered(start)) ? NodeRangeIterator.IteratorState.HaveCurrentNoNext : NodeRangeIterator.IteratorState.NoNext);
				return;
			}
			this.state = ((!filter.IsFiltered(start)) ? NodeRangeIterator.IteratorState.HaveCurrent : NodeRangeIterator.IteratorState.NeedCurrent);
		}

		// Token: 0x06003B5F RID: 15199 RVA: 0x0014E020 File Offset: 0x0014C220
		public bool MoveNext()
		{
			switch (this.state)
			{
			case NodeRangeIterator.IteratorState.HaveCurrent:
				this.state = NodeRangeIterator.IteratorState.NeedCurrent;
				return true;
			case NodeRangeIterator.IteratorState.NeedCurrent:
				if (!this.filter.MoveToFollowing(this.navCurrent, this.navEnd))
				{
					if (this.filter.IsFiltered(this.navEnd))
					{
						this.state = NodeRangeIterator.IteratorState.NoNext;
						return false;
					}
					this.navCurrent.MoveTo(this.navEnd);
					this.state = NodeRangeIterator.IteratorState.NoNext;
				}
				return true;
			case NodeRangeIterator.IteratorState.HaveCurrentNoNext:
				this.state = NodeRangeIterator.IteratorState.NoNext;
				return true;
			default:
				return false;
			}
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06003B60 RID: 15200 RVA: 0x0014E0AB File Offset: 0x0014C2AB
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x0400271D RID: 10013
		private XmlNavigatorFilter filter;

		// Token: 0x0400271E RID: 10014
		private XPathNavigator navCurrent;

		// Token: 0x0400271F RID: 10015
		private XPathNavigator navEnd;

		// Token: 0x04002720 RID: 10016
		private NodeRangeIterator.IteratorState state;

		// Token: 0x020005F3 RID: 1523
		private enum IteratorState
		{
			// Token: 0x04002722 RID: 10018
			HaveCurrent,
			// Token: 0x04002723 RID: 10019
			NeedCurrent,
			// Token: 0x04002724 RID: 10020
			HaveCurrentNoNext,
			// Token: 0x04002725 RID: 10021
			NoNext
		}
	}
}
