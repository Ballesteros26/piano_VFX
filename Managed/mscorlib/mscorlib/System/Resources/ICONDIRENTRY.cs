using System;

namespace System.Resources
{
	// Token: 0x020002BC RID: 700
	internal class ICONDIRENTRY
	{
		// Token: 0x06001FE6 RID: 8166 RVA: 0x0007D870 File Offset: 0x0007BA70
		public override string ToString()
		{
			return string.Concat(new object[] { "ICONDIRENTRY (", this.bWidth, "x", this.bHeight, " ", this.wBitCount, " bpp)" });
		}

		// Token: 0x04001156 RID: 4438
		public byte bWidth;

		// Token: 0x04001157 RID: 4439
		public byte bHeight;

		// Token: 0x04001158 RID: 4440
		public byte bColorCount;

		// Token: 0x04001159 RID: 4441
		public byte bReserved;

		// Token: 0x0400115A RID: 4442
		public short wPlanes;

		// Token: 0x0400115B RID: 4443
		public short wBitCount;

		// Token: 0x0400115C RID: 4444
		public int dwBytesInRes;

		// Token: 0x0400115D RID: 4445
		public int dwImageOffset;

		// Token: 0x0400115E RID: 4446
		public byte[] image;
	}
}
