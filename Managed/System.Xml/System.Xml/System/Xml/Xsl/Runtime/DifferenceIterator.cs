using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005DD RID: 1501
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct DifferenceIterator
	{
		// Token: 0x06003B22 RID: 15138 RVA: 0x0014D393 File Offset: 0x0014B593
		public void Create(XmlQueryRuntime runtime)
		{
			this.runtime = runtime;
			this.state = DifferenceIterator.IteratorState.InitLeft;
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x0014D3A4 File Offset: 0x0014B5A4
		public SetIteratorResult MoveNext(XPathNavigator nestedNavigator)
		{
			switch (this.state)
			{
			case DifferenceIterator.IteratorState.InitLeft:
				this.navLeft = nestedNavigator;
				this.state = DifferenceIterator.IteratorState.NeedRight;
				return SetIteratorResult.InitRightIterator;
			case DifferenceIterator.IteratorState.NeedLeft:
				this.navLeft = nestedNavigator;
				break;
			case DifferenceIterator.IteratorState.NeedRight:
				this.navRight = nestedNavigator;
				break;
			case DifferenceIterator.IteratorState.NeedLeftAndRight:
				this.navLeft = nestedNavigator;
				this.state = DifferenceIterator.IteratorState.NeedRight;
				return SetIteratorResult.NeedRightNode;
			case DifferenceIterator.IteratorState.HaveCurrent:
				this.state = DifferenceIterator.IteratorState.NeedLeft;
				return SetIteratorResult.NeedLeftNode;
			}
			if (this.navLeft == null)
			{
				return SetIteratorResult.NoMoreNodes;
			}
			if (this.navRight != null)
			{
				int num = this.runtime.ComparePosition(this.navLeft, this.navRight);
				if (num == 0)
				{
					this.state = DifferenceIterator.IteratorState.NeedLeftAndRight;
					return SetIteratorResult.NeedLeftNode;
				}
				if (num > 0)
				{
					this.state = DifferenceIterator.IteratorState.NeedRight;
					return SetIteratorResult.NeedRightNode;
				}
			}
			this.state = DifferenceIterator.IteratorState.HaveCurrent;
			return SetIteratorResult.HaveCurrentNode;
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06003B24 RID: 15140 RVA: 0x0014D45A File Offset: 0x0014B65A
		public XPathNavigator Current
		{
			get
			{
				return this.navLeft;
			}
		}

		// Token: 0x040026CF RID: 9935
		private XmlQueryRuntime runtime;

		// Token: 0x040026D0 RID: 9936
		private XPathNavigator navLeft;

		// Token: 0x040026D1 RID: 9937
		private XPathNavigator navRight;

		// Token: 0x040026D2 RID: 9938
		private DifferenceIterator.IteratorState state;

		// Token: 0x020005DE RID: 1502
		private enum IteratorState
		{
			// Token: 0x040026D4 RID: 9940
			InitLeft,
			// Token: 0x040026D5 RID: 9941
			NeedLeft,
			// Token: 0x040026D6 RID: 9942
			NeedRight,
			// Token: 0x040026D7 RID: 9943
			NeedLeftAndRight,
			// Token: 0x040026D8 RID: 9944
			HaveCurrent
		}
	}
}
