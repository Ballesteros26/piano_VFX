using System;

namespace System.Xml.Schema
{
	// Token: 0x020003A4 RID: 932
	internal sealed class StarNode : InteriorNode
	{
		// Token: 0x06002551 RID: 9553 RVA: 0x000E0A90 File Offset: 0x000DEC90
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			base.LeftChild.ConstructPos(firstpos, lastpos, followpos);
			for (int num = lastpos.NextSet(-1); num != -1; num = lastpos.NextSet(num))
			{
				followpos[num].Or(firstpos);
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06002552 RID: 9554 RVA: 0x00003242 File Offset: 0x00001442
		public override bool IsNullable
		{
			get
			{
				return true;
			}
		}
	}
}
