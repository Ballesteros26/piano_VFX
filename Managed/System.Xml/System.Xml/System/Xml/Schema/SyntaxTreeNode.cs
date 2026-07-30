using System;

namespace System.Xml.Schema
{
	// Token: 0x0200039B RID: 923
	internal abstract class SyntaxTreeNode
	{
		// Token: 0x06002526 RID: 9510
		public abstract void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions);

		// Token: 0x06002527 RID: 9511
		public abstract SyntaxTreeNode Clone(Positions positions);

		// Token: 0x06002528 RID: 9512
		public abstract void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos);

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06002529 RID: 9513
		public abstract bool IsNullable { get; }

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x0600252A RID: 9514 RVA: 0x0000226C File Offset: 0x0000046C
		public virtual bool IsRangeNode
		{
			get
			{
				return false;
			}
		}
	}
}
