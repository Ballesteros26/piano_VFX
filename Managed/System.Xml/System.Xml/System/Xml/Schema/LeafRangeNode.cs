using System;

namespace System.Xml.Schema
{
	// Token: 0x020003A5 RID: 933
	internal sealed class LeafRangeNode : LeafNode
	{
		// Token: 0x06002554 RID: 9556 RVA: 0x000E0ACA File Offset: 0x000DECCA
		public LeafRangeNode(decimal min, decimal max)
			: this(-1, min, max)
		{
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x000E0AD5 File Offset: 0x000DECD5
		public LeafRangeNode(int pos, decimal min, decimal max)
			: base(pos)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06002556 RID: 9558 RVA: 0x000E0AEC File Offset: 0x000DECEC
		public decimal Max
		{
			get
			{
				return this.max;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x000E0AF4 File Offset: 0x000DECF4
		public decimal Min
		{
			get
			{
				return this.min;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06002558 RID: 9560 RVA: 0x000E0AFC File Offset: 0x000DECFC
		// (set) Token: 0x06002559 RID: 9561 RVA: 0x000E0B04 File Offset: 0x000DED04
		public BitSet NextIteration
		{
			get
			{
				return this.nextIteration;
			}
			set
			{
				this.nextIteration = value;
			}
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x000E0B0D File Offset: 0x000DED0D
		public override SyntaxTreeNode Clone(Positions positions)
		{
			return new LeafRangeNode(base.Pos, this.min, this.max);
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x0600255B RID: 9563 RVA: 0x00003242 File Offset: 0x00001442
		public override bool IsRangeNode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x000E0B26 File Offset: 0x000DED26
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			if (parent.LeftChild.IsNullable)
			{
				this.min = 0m;
			}
		}

		// Token: 0x04001935 RID: 6453
		private decimal min;

		// Token: 0x04001936 RID: 6454
		private decimal max;

		// Token: 0x04001937 RID: 6455
		private BitSet nextIteration;
	}
}
