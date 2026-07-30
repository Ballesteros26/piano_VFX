using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005D9 RID: 1497
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct UnionIterator
	{
		// Token: 0x06003B1B RID: 15131 RVA: 0x0014D193 File Offset: 0x0014B393
		public void Create(XmlQueryRuntime runtime)
		{
			this.runtime = runtime;
			this.state = UnionIterator.IteratorState.InitLeft;
		}

		// Token: 0x06003B1C RID: 15132 RVA: 0x0014D1A4 File Offset: 0x0014B3A4
		public SetIteratorResult MoveNext(XPathNavigator nestedNavigator)
		{
			switch (this.state)
			{
			case UnionIterator.IteratorState.InitLeft:
				this.navOther = nestedNavigator;
				this.state = UnionIterator.IteratorState.NeedRight;
				return SetIteratorResult.InitRightIterator;
			case UnionIterator.IteratorState.NeedLeft:
				this.navCurr = nestedNavigator;
				this.state = UnionIterator.IteratorState.LeftIsCurrent;
				break;
			case UnionIterator.IteratorState.NeedRight:
				this.navCurr = nestedNavigator;
				this.state = UnionIterator.IteratorState.RightIsCurrent;
				break;
			case UnionIterator.IteratorState.LeftIsCurrent:
				this.state = UnionIterator.IteratorState.NeedLeft;
				return SetIteratorResult.NeedLeftNode;
			case UnionIterator.IteratorState.RightIsCurrent:
				this.state = UnionIterator.IteratorState.NeedRight;
				return SetIteratorResult.NeedRightNode;
			}
			if (this.navCurr == null)
			{
				if (this.navOther == null)
				{
					return SetIteratorResult.NoMoreNodes;
				}
				this.Swap();
			}
			else if (this.navOther != null)
			{
				int num = this.runtime.ComparePosition(this.navOther, this.navCurr);
				if (num == 0)
				{
					if (this.state == UnionIterator.IteratorState.LeftIsCurrent)
					{
						this.state = UnionIterator.IteratorState.NeedLeft;
						return SetIteratorResult.NeedLeftNode;
					}
					this.state = UnionIterator.IteratorState.NeedRight;
					return SetIteratorResult.NeedRightNode;
				}
				else if (num < 0)
				{
					this.Swap();
				}
			}
			return SetIteratorResult.HaveCurrentNode;
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06003B1D RID: 15133 RVA: 0x0014D279 File Offset: 0x0014B479
		public XPathNavigator Current
		{
			get
			{
				return this.navCurr;
			}
		}

		// Token: 0x06003B1E RID: 15134 RVA: 0x0014D284 File Offset: 0x0014B484
		private void Swap()
		{
			XPathNavigator xpathNavigator = this.navCurr;
			this.navCurr = this.navOther;
			this.navOther = xpathNavigator;
			if (this.state == UnionIterator.IteratorState.LeftIsCurrent)
			{
				this.state = UnionIterator.IteratorState.RightIsCurrent;
				return;
			}
			this.state = UnionIterator.IteratorState.LeftIsCurrent;
		}

		// Token: 0x040026BB RID: 9915
		private XmlQueryRuntime runtime;

		// Token: 0x040026BC RID: 9916
		private XPathNavigator navCurr;

		// Token: 0x040026BD RID: 9917
		private XPathNavigator navOther;

		// Token: 0x040026BE RID: 9918
		private UnionIterator.IteratorState state;

		// Token: 0x020005DA RID: 1498
		private enum IteratorState
		{
			// Token: 0x040026C0 RID: 9920
			InitLeft,
			// Token: 0x040026C1 RID: 9921
			NeedLeft,
			// Token: 0x040026C2 RID: 9922
			NeedRight,
			// Token: 0x040026C3 RID: 9923
			LeftIsCurrent,
			// Token: 0x040026C4 RID: 9924
			RightIsCurrent
		}
	}
}
