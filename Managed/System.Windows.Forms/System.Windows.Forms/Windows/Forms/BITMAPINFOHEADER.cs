using System;

namespace System.Windows.Forms
{
	// Token: 0x02000460 RID: 1120
	internal struct BITMAPINFOHEADER
	{
		// Token: 0x04002562 RID: 9570
		internal uint biSize;

		// Token: 0x04002563 RID: 9571
		internal int biWidth;

		// Token: 0x04002564 RID: 9572
		internal int biHeight;

		// Token: 0x04002565 RID: 9573
		internal ushort biPlanes;

		// Token: 0x04002566 RID: 9574
		internal ushort biBitCount;

		// Token: 0x04002567 RID: 9575
		internal uint biCompression;

		// Token: 0x04002568 RID: 9576
		internal uint biSizeImage;

		// Token: 0x04002569 RID: 9577
		internal int biXPelsPerMeter;

		// Token: 0x0400256A RID: 9578
		internal int biYPelsPerMeter;

		// Token: 0x0400256B RID: 9579
		internal uint biClrUsed;

		// Token: 0x0400256C RID: 9580
		internal uint biClrImportant;
	}
}
