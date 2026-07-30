using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005DB RID: 1499
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct IntersectIterator
	{
		// Token: 0x06003B1F RID: 15135 RVA: 0x0014D2C3 File Offset: 0x0014B4C3
		public void Create(XmlQueryRuntime runtime)
		{
			this.runtime = runtime;
			this.state = IntersectIterator.IteratorState.InitLeft;
		}

		// Token: 0x06003B20 RID: 15136 RVA: 0x0014D2D4 File Offset: 0x0014B4D4
		public SetIteratorResult MoveNext(XPathNavigator nestedNavigator)
		{
			switch (this.state)
			{
			case IntersectIterator.IteratorState.InitLeft:
				this.navLeft = nestedNavigator;
				this.state = IntersectIterator.IteratorState.NeedRight;
				return SetIteratorResult.InitRightIterator;
			case IntersectIterator.IteratorState.NeedLeft:
				this.navLeft = nestedNavigator;
				break;
			case IntersectIterator.IteratorState.NeedRight:
				this.navRight = nestedNavigator;
				break;
			case IntersectIterator.IteratorState.NeedLeftAndRight:
				this.navLeft = nestedNavigator;
				this.state = IntersectIterator.IteratorState.NeedRight;
				return SetIteratorResult.NeedRightNode;
			case IntersectIterator.IteratorState.HaveCurrent:
				this.state = IntersectIterator.IteratorState.NeedLeftAndRight;
				return SetIteratorResult.NeedLeftNode;
			}
			if (this.navLeft == null || this.navRight == null)
			{
				return SetIteratorResult.NoMoreNodes;
			}
			int num = this.runtime.ComparePosition(this.navLeft, this.navRight);
			if (num < 0)
			{
				this.state = IntersectIterator.IteratorState.NeedLeft;
				return SetIteratorResult.NeedLeftNode;
			}
			if (num > 0)
			{
				this.state = IntersectIterator.IteratorState.NeedRight;
				return SetIteratorResult.NeedRightNode;
			}
			this.state = IntersectIterator.IteratorState.HaveCurrent;
			return SetIteratorResult.HaveCurrentNode;
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06003B21 RID: 15137 RVA: 0x0014D38B File Offset: 0x0014B58B
		public XPathNavigator Current
		{
			get
			{
				return this.navLeft;
			}
		}

		// Token: 0x040026C5 RID: 9925
		private XmlQueryRuntime runtime;

		// Token: 0x040026C6 RID: 9926
		private XPathNavigator navLeft;

		// Token: 0x040026C7 RID: 9927
		private XPathNavigator navRight;

		// Token: 0x040026C8 RID: 9928
		private IntersectIterator.IteratorState state;

		// Token: 0x020005DC RID: 1500
		private enum IteratorState
		{
			// Token: 0x040026CA RID: 9930
			InitLeft,
			// Token: 0x040026CB RID: 9931
			NeedLeft,
			// Token: 0x040026CC RID: 9932
			NeedRight,
			// Token: 0x040026CD RID: 9933
			NeedLeftAndRight,
			// Token: 0x040026CE RID: 9934
			HaveCurrent
		}
	}
}
