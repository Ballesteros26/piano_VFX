using System;

namespace UnityEngine.TextCore
{
	// Token: 0x0200000E RID: 14
	internal struct KerningPairKey
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00005125 File Offset: 0x00003325
		public KerningPairKey(uint ascii_left, uint ascii_right)
		{
			this.ascii_Left = ascii_left;
			this.ascii_Right = ascii_right;
			this.key = (ascii_right << 16) + ascii_left;
		}

		// Token: 0x04000063 RID: 99
		public uint ascii_Left;

		// Token: 0x04000064 RID: 100
		public uint ascii_Right;

		// Token: 0x04000065 RID: 101
		public uint key;
	}
}
