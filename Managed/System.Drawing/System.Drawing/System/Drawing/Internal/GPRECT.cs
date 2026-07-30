using System;

namespace System.Drawing.Internal
{
	// Token: 0x020000F0 RID: 240
	internal struct GPRECT
	{
		// Token: 0x06000BDC RID: 3036 RVA: 0x0001A5AC File Offset: 0x000187AC
		internal GPRECT(int x, int y, int width, int height)
		{
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0001A5CB File Offset: 0x000187CB
		internal GPRECT(Rectangle rect)
		{
			this.X = rect.X;
			this.Y = rect.Y;
			this.Width = rect.Width;
			this.Height = rect.Height;
		}

		// Token: 0x04000817 RID: 2071
		internal int X;

		// Token: 0x04000818 RID: 2072
		internal int Y;

		// Token: 0x04000819 RID: 2073
		internal int Width;

		// Token: 0x0400081A RID: 2074
		internal int Height;
	}
}
