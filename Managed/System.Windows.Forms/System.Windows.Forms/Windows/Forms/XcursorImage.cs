using System;

namespace System.Windows.Forms
{
	// Token: 0x02000438 RID: 1080
	internal struct XcursorImage
	{
		// Token: 0x06004628 RID: 17960 RVA: 0x00114698 File Offset: 0x00112898
		public override string ToString()
		{
			return string.Format("XCursorImage (version: {0}, size: {1}, width: {2}, height: {3}, xhot: {4}, yhot: {5}, delay: {6}, pixels: {7}", new object[] { this.version, this.size, this.width, this.height, this.xhot, this.yhot, this.delay, this.pixels });
		}

		// Token: 0x0400227B RID: 8827
		private int version;

		// Token: 0x0400227C RID: 8828
		public int size;

		// Token: 0x0400227D RID: 8829
		public int width;

		// Token: 0x0400227E RID: 8830
		public int height;

		// Token: 0x0400227F RID: 8831
		public int xhot;

		// Token: 0x04002280 RID: 8832
		public int yhot;

		// Token: 0x04002281 RID: 8833
		public int delay;

		// Token: 0x04002282 RID: 8834
		public IntPtr pixels;
	}
}
