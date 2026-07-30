using System;

namespace System.Xml.Schema
{
	// Token: 0x0200039C RID: 924
	internal class LeafNode : SyntaxTreeNode
	{
		// Token: 0x0600252C RID: 9516 RVA: 0x000E04FC File Offset: 0x000DE6FC
		public LeafNode(int pos)
		{
			this.pos = pos;
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x0600252D RID: 9517 RVA: 0x000E050B File Offset: 0x000DE70B
		// (set) Token: 0x0600252E RID: 9518 RVA: 0x000E0513 File Offset: 0x000DE713
		public int Pos
		{
			get
			{
				return this.pos;
			}
			set
			{
				this.pos = value;
			}
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x00002F50 File Offset: 0x00001150
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x000E051C File Offset: 0x000DE71C
		public override SyntaxTreeNode Clone(Positions positions)
		{
			return new LeafNode(positions.Add(positions[this.pos].symbol, positions[this.pos].particle));
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x000E054B File Offset: 0x000DE74B
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			firstpos.Set(this.pos);
			lastpos.Set(this.pos);
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06002532 RID: 9522 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool IsNullable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400192B RID: 6443
		private int pos;
	}
}
