using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005F0 RID: 1520
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct XPathPrecedingMergeIterator
	{
		// Token: 0x06003B5A RID: 15194 RVA: 0x0014DE53 File Offset: 0x0014C053
		public void Create(XmlNavigatorFilter filter)
		{
			this.filter = filter;
			this.state = XPathPrecedingMergeIterator.IteratorState.NeedCandidateCurrent;
		}

		// Token: 0x06003B5B RID: 15195 RVA: 0x0014DE64 File Offset: 0x0014C064
		public IteratorResult MoveNext(XPathNavigator input)
		{
			XPathPrecedingMergeIterator.IteratorState iteratorState = this.state;
			if (iteratorState != XPathPrecedingMergeIterator.IteratorState.NeedCandidateCurrent)
			{
				if (iteratorState == XPathPrecedingMergeIterator.IteratorState.HaveCandidateCurrent)
				{
					if (input == null)
					{
						this.state = XPathPrecedingMergeIterator.IteratorState.HaveCurrentNoNext;
					}
					else
					{
						if (this.navCurrent.ComparePosition(input) != XmlNodeOrder.Unknown)
						{
							this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, input);
							return IteratorResult.NeedInputNode;
						}
						this.navNext = XmlQueryRuntime.SyncToNavigator(this.navNext, input);
						this.state = XPathPrecedingMergeIterator.IteratorState.HaveCurrentHaveNext;
					}
					this.PushAncestors();
				}
				if (!this.navStack.IsEmpty)
				{
					while (!this.filter.MoveToFollowing(this.navCurrent, this.navStack.Peek()))
					{
						this.navCurrent.MoveTo(this.navStack.Pop());
						if (this.navStack.IsEmpty)
						{
							goto IL_00CF;
						}
					}
					return IteratorResult.HaveCurrentNode;
				}
				IL_00CF:
				if (this.state == XPathPrecedingMergeIterator.IteratorState.HaveCurrentNoNext)
				{
					this.state = XPathPrecedingMergeIterator.IteratorState.NeedCandidateCurrent;
					return IteratorResult.NoMoreNodes;
				}
				this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, this.navNext);
				this.state = XPathPrecedingMergeIterator.IteratorState.HaveCandidateCurrent;
				return IteratorResult.HaveCurrentNode;
			}
			else
			{
				if (input == null)
				{
					return IteratorResult.NoMoreNodes;
				}
				this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, input);
				this.state = XPathPrecedingMergeIterator.IteratorState.HaveCandidateCurrent;
				return IteratorResult.NeedInputNode;
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06003B5C RID: 15196 RVA: 0x0014DF71 File Offset: 0x0014C171
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x06003B5D RID: 15197 RVA: 0x0014DF79 File Offset: 0x0014C179
		private void PushAncestors()
		{
			this.navStack.Reset();
			do
			{
				this.navStack.Push(this.navCurrent.Clone());
			}
			while (this.navCurrent.MoveToParent());
			this.navStack.Pop();
		}

		// Token: 0x04002713 RID: 10003
		private XmlNavigatorFilter filter;

		// Token: 0x04002714 RID: 10004
		private XPathPrecedingMergeIterator.IteratorState state;

		// Token: 0x04002715 RID: 10005
		private XPathNavigator navCurrent;

		// Token: 0x04002716 RID: 10006
		private XPathNavigator navNext;

		// Token: 0x04002717 RID: 10007
		private XmlNavigatorStack navStack;

		// Token: 0x020005F1 RID: 1521
		private enum IteratorState
		{
			// Token: 0x04002719 RID: 10009
			NeedCandidateCurrent,
			// Token: 0x0400271A RID: 10010
			HaveCandidateCurrent,
			// Token: 0x0400271B RID: 10011
			HaveCurrentHaveNext,
			// Token: 0x0400271C RID: 10012
			HaveCurrentNoNext
		}
	}
}
