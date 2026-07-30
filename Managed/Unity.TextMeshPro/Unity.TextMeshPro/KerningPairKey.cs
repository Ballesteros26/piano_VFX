using System;

namespace TMPro
{
	// Token: 0x0200001E RID: 30
	public struct KerningPairKey
	{
		// Token: 0x06000109 RID: 265 RVA: 0x00007504 File Offset: 0x00005704
		public KerningPairKey(uint ascii_left, uint ascii_right)
		{
			this.ascii_Left = ascii_left;
			this.ascii_Right = ascii_right;
			this.key = (ascii_right << 16) + ascii_left;
		}

		// Token: 0x040000DE RID: 222
		public uint ascii_Left;

		// Token: 0x040000DF RID: 223
		public uint ascii_Right;

		// Token: 0x040000E0 RID: 224
		public uint key;
	}
}
