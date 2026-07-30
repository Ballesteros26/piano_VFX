using System;

namespace System.Xml.Schema
{
	// Token: 0x020003A3 RID: 931
	internal sealed class QmarkNode : InteriorNode
	{
		// Token: 0x0600254E RID: 9550 RVA: 0x000E0A7F File Offset: 0x000DEC7F
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			base.LeftChild.ConstructPos(firstpos, lastpos, followpos);
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x0600254F RID: 9551 RVA: 0x00003242 File Offset: 0x00001442
		public override bool IsNullable
		{
			get
			{
				return true;
			}
		}
	}
}
