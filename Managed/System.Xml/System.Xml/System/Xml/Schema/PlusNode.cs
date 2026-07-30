using System;

namespace System.Xml.Schema
{
	// Token: 0x020003A2 RID: 930
	internal sealed class PlusNode : InteriorNode
	{
		// Token: 0x0600254B RID: 9547 RVA: 0x000E0A38 File Offset: 0x000DEC38
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			base.LeftChild.ConstructPos(firstpos, lastpos, followpos);
			for (int num = lastpos.NextSet(-1); num != -1; num = lastpos.NextSet(num))
			{
				followpos[num].Or(firstpos);
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x0600254C RID: 9548 RVA: 0x000E0A72 File Offset: 0x000DEC72
		public override bool IsNullable
		{
			get
			{
				return base.LeftChild.IsNullable;
			}
		}
	}
}
