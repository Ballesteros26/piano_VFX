using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005CA RID: 1482
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct ContentMergeIterator
	{
		// Token: 0x06003AD6 RID: 15062 RVA: 0x0014C439 File Offset: 0x0014A639
		public void Create(XmlNavigatorFilter filter)
		{
			this.filter = filter;
			this.navStack.Reset();
			this.state = ContentMergeIterator.IteratorState.NeedCurrent;
		}

		// Token: 0x06003AD7 RID: 15063 RVA: 0x0014C454 File Offset: 0x0014A654
		public IteratorResult MoveNext(XPathNavigator input)
		{
			return this.MoveNext(input, true);
		}

		// Token: 0x06003AD8 RID: 15064 RVA: 0x0014C460 File Offset: 0x0014A660
		internal IteratorResult MoveNext(XPathNavigator input, bool isContent)
		{
			switch (this.state)
			{
			case ContentMergeIterator.IteratorState.NeedCurrent:
				if (input == null)
				{
					return IteratorResult.NoMoreNodes;
				}
				this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, input);
				if (isContent ? this.filter.MoveToContent(this.navCurrent) : this.filter.MoveToFollowingSibling(this.navCurrent))
				{
					this.state = ContentMergeIterator.IteratorState.HaveCurrentNeedNext;
				}
				return IteratorResult.NeedInputNode;
			case ContentMergeIterator.IteratorState.HaveCurrentNeedNext:
				if (input == null)
				{
					this.state = ContentMergeIterator.IteratorState.HaveCurrentNoNext;
					return IteratorResult.HaveCurrentNode;
				}
				this.navNext = XmlQueryRuntime.SyncToNavigator(this.navNext, input);
				if (isContent ? this.filter.MoveToContent(this.navNext) : this.filter.MoveToFollowingSibling(this.navNext))
				{
					this.state = ContentMergeIterator.IteratorState.HaveCurrentHaveNext;
					return this.DocOrderMerge();
				}
				return IteratorResult.NeedInputNode;
			case ContentMergeIterator.IteratorState.HaveCurrentNoNext:
			case ContentMergeIterator.IteratorState.HaveCurrentHaveNext:
				if (isContent ? (!this.filter.MoveToNextContent(this.navCurrent)) : (!this.filter.MoveToFollowingSibling(this.navCurrent)))
				{
					if (this.navStack.IsEmpty)
					{
						if (this.state == ContentMergeIterator.IteratorState.HaveCurrentNoNext)
						{
							return IteratorResult.NoMoreNodes;
						}
						this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, this.navNext);
						this.state = ContentMergeIterator.IteratorState.HaveCurrentNeedNext;
						return IteratorResult.NeedInputNode;
					}
					else
					{
						this.navCurrent = this.navStack.Pop();
					}
				}
				if (this.state == ContentMergeIterator.IteratorState.HaveCurrentNoNext)
				{
					return IteratorResult.HaveCurrentNode;
				}
				return this.DocOrderMerge();
			default:
				return IteratorResult.NoMoreNodes;
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06003AD9 RID: 15065 RVA: 0x0014C5BA File Offset: 0x0014A7BA
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x0014C5C4 File Offset: 0x0014A7C4
		private IteratorResult DocOrderMerge()
		{
			XmlNodeOrder xmlNodeOrder = this.navCurrent.ComparePosition(this.navNext);
			if (xmlNodeOrder == XmlNodeOrder.Before || xmlNodeOrder == XmlNodeOrder.Unknown)
			{
				return IteratorResult.HaveCurrentNode;
			}
			if (xmlNodeOrder == XmlNodeOrder.After)
			{
				this.navStack.Push(this.navCurrent);
				this.navCurrent = this.navNext;
				this.navNext = null;
			}
			this.state = ContentMergeIterator.IteratorState.HaveCurrentNeedNext;
			return IteratorResult.NeedInputNode;
		}

		// Token: 0x04002660 RID: 9824
		private XmlNavigatorFilter filter;

		// Token: 0x04002661 RID: 9825
		private XPathNavigator navCurrent;

		// Token: 0x04002662 RID: 9826
		private XPathNavigator navNext;

		// Token: 0x04002663 RID: 9827
		private XmlNavigatorStack navStack;

		// Token: 0x04002664 RID: 9828
		private ContentMergeIterator.IteratorState state;

		// Token: 0x020005CB RID: 1483
		private enum IteratorState
		{
			// Token: 0x04002666 RID: 9830
			NeedCurrent,
			// Token: 0x04002667 RID: 9831
			HaveCurrentNeedNext,
			// Token: 0x04002668 RID: 9832
			HaveCurrentNoNext,
			// Token: 0x04002669 RID: 9833
			HaveCurrentHaveNext
		}
	}
}
