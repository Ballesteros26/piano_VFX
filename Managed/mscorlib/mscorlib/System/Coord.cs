using System;

namespace System
{
	// Token: 0x02000258 RID: 600
	internal struct Coord
	{
		// Token: 0x06001BAC RID: 7084 RVA: 0x000689DC File Offset: 0x00066BDC
		public Coord(int x, int y)
		{
			this.X = (short)x;
			this.Y = (short)y;
		}

		// Token: 0x04000F92 RID: 3986
		public short X;

		// Token: 0x04000F93 RID: 3987
		public short Y;
	}
}
