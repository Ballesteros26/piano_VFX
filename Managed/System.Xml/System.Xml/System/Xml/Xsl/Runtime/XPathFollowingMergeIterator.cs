using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005EB RID: 1515
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct XPathFollowingMergeIterator
	{
		// Token: 0x06003B4B RID: 15179 RVA: 0x0014DB11 File Offset: 0x0014BD11
		public void Create(XmlNavigatorFilter filter)
		{
			this.filter = filter;
			this.state = XPathFollowingMergeIterator.IteratorState.NeedCandidateCurrent;
		}

		// Token: 0x06003B4C RID: 15180 RVA: 0x0014DB24 File Offset: 0x0014BD24
		public IteratorResult MoveNext(XPathNavigator input)
		{
			switch (this.state)
			{
			case XPathFollowingMergeIterator.IteratorState.NeedCandidateCurrent:
				break;
			case XPathFollowingMergeIterator.IteratorState.HaveCandidateCurrent:
				if (input == null)
				{
					this.state = XPathFollowingMergeIterator.IteratorState.HaveCurrentNoNext;
					return this.MoveFirst();
				}
				if (!this.navCurrent.IsDescendant(input))
				{
					this.state = XPathFollowingMergeIterator.IteratorState.HaveCurrentNeedNext;
					goto IL_0064;
				}
				break;
			case XPathFollowingMergeIterator.IteratorState.HaveCurrentNeedNext:
				goto IL_0064;
			default:
				if (!this.filter.MoveToFollowing(this.navCurrent, null))
				{
					return this.MoveFailed();
				}
				return IteratorResult.HaveCurrentNode;
			}
			if (input == null)
			{
				return IteratorResult.NoMoreNodes;
			}
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, input);
			this.state = XPathFollowingMergeIterator.IteratorState.HaveCandidateCurrent;
			return IteratorResult.NeedInputNode;
			IL_0064:
			if (input == null)
			{
				this.state = XPathFollowingMergeIterator.IteratorState.HaveCurrentNoNext;
				return this.MoveFirst();
			}
			if (this.navCurrent.ComparePosition(input) != XmlNodeOrder.Unknown)
			{
				return IteratorResult.NeedInputNode;
			}
			this.navNext = XmlQueryRuntime.SyncToNavigator(this.navNext, input);
			this.state = XPathFollowingMergeIterator.IteratorState.HaveCurrentHaveNext;
			return this.MoveFirst();
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06003B4D RID: 15181 RVA: 0x0014DBF3 File Offset: 0x0014BDF3
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x0014DBFC File Offset: 0x0014BDFC
		private IteratorResult MoveFailed()
		{
			if (this.state == XPathFollowingMergeIterator.IteratorState.HaveCurrentNoNext)
			{
				this.state = XPathFollowingMergeIterator.IteratorState.NeedCandidateCurrent;
				return IteratorResult.NoMoreNodes;
			}
			this.state = XPathFollowingMergeIterator.IteratorState.HaveCandidateCurrent;
			XPathNavigator xpathNavigator = this.navCurrent;
			this.navCurrent = this.navNext;
			this.navNext = xpathNavigator;
			return IteratorResult.NeedInputNode;
		}

		// Token: 0x06003B4F RID: 15183 RVA: 0x0014DC3D File Offset: 0x0014BE3D
		private IteratorResult MoveFirst()
		{
			if (!XPathFollowingIterator.MoveFirst(this.filter, this.navCurrent))
			{
				return this.MoveFailed();
			}
			return IteratorResult.HaveCurrentNode;
		}

		// Token: 0x04002702 RID: 9986
		private XmlNavigatorFilter filter;

		// Token: 0x04002703 RID: 9987
		private XPathFollowingMergeIterator.IteratorState state;

		// Token: 0x04002704 RID: 9988
		private XPathNavigator navCurrent;

		// Token: 0x04002705 RID: 9989
		private XPathNavigator navNext;

		// Token: 0x020005EC RID: 1516
		private enum IteratorState
		{
			// Token: 0x04002707 RID: 9991
			NeedCandidateCurrent,
			// Token: 0x04002708 RID: 9992
			HaveCandidateCurrent,
			// Token: 0x04002709 RID: 9993
			HaveCurrentNeedNext,
			// Token: 0x0400270A RID: 9994
			HaveCurrentHaveNext,
			// Token: 0x0400270B RID: 9995
			HaveCurrentNoNext
		}
	}
}
