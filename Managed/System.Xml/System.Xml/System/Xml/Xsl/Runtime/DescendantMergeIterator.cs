using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005E5 RID: 1509
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct DescendantMergeIterator
	{
		// Token: 0x06003B3B RID: 15163 RVA: 0x0014D837 File Offset: 0x0014BA37
		public void Create(XmlNavigatorFilter filter, bool orSelf)
		{
			this.filter = filter;
			this.state = DescendantMergeIterator.IteratorState.NoPrevious;
			this.orSelf = orSelf;
		}

		// Token: 0x06003B3C RID: 15164 RVA: 0x0014D850 File Offset: 0x0014BA50
		public IteratorResult MoveNext(XPathNavigator input)
		{
			if (this.state != DescendantMergeIterator.IteratorState.NeedDescendant)
			{
				if (input == null)
				{
					return IteratorResult.NoMoreNodes;
				}
				if (this.state != DescendantMergeIterator.IteratorState.NoPrevious && this.navRoot.IsDescendant(input))
				{
					return IteratorResult.NeedInputNode;
				}
				this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, input);
				this.navRoot = XmlQueryRuntime.SyncToNavigator(this.navRoot, input);
				this.navEnd = XmlQueryRuntime.SyncToNavigator(this.navEnd, input);
				this.navEnd.MoveToNonDescendant();
				this.state = DescendantMergeIterator.IteratorState.NeedDescendant;
				if (this.orSelf && !this.filter.IsFiltered(input))
				{
					return IteratorResult.HaveCurrentNode;
				}
			}
			if (this.filter.MoveToFollowing(this.navCurrent, this.navEnd))
			{
				return IteratorResult.HaveCurrentNode;
			}
			this.state = DescendantMergeIterator.IteratorState.NeedCurrent;
			return IteratorResult.NeedInputNode;
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06003B3D RID: 15165 RVA: 0x0014D907 File Offset: 0x0014BB07
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x040026EE RID: 9966
		private XmlNavigatorFilter filter;

		// Token: 0x040026EF RID: 9967
		private XPathNavigator navCurrent;

		// Token: 0x040026F0 RID: 9968
		private XPathNavigator navRoot;

		// Token: 0x040026F1 RID: 9969
		private XPathNavigator navEnd;

		// Token: 0x040026F2 RID: 9970
		private DescendantMergeIterator.IteratorState state;

		// Token: 0x040026F3 RID: 9971
		private bool orSelf;

		// Token: 0x020005E6 RID: 1510
		private enum IteratorState
		{
			// Token: 0x040026F5 RID: 9973
			NoPrevious,
			// Token: 0x040026F6 RID: 9974
			NeedCurrent,
			// Token: 0x040026F7 RID: 9975
			NeedDescendant
		}
	}
}
