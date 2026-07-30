using System;

namespace System
{
	// Token: 0x02000259 RID: 601
	internal struct SmallRect
	{
		// Token: 0x06001BAD RID: 7085 RVA: 0x000689EE File Offset: 0x00066BEE
		public SmallRect(int left, int top, int right, int bottom)
		{
			this.Left = (short)left;
			this.Top = (short)top;
			this.Right = (short)right;
			this.Bottom = (short)bottom;
		}

		// Token: 0x04000F94 RID: 3988
		public short Left;

		// Token: 0x04000F95 RID: 3989
		public short Top;

		// Token: 0x04000F96 RID: 3990
		public short Right;

		// Token: 0x04000F97 RID: 3991
		public short Bottom;
	}
}
