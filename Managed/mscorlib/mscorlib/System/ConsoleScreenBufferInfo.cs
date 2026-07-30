using System;

namespace System
{
	// Token: 0x0200025A RID: 602
	internal struct ConsoleScreenBufferInfo
	{
		// Token: 0x04000F98 RID: 3992
		public Coord Size;

		// Token: 0x04000F99 RID: 3993
		public Coord CursorPosition;

		// Token: 0x04000F9A RID: 3994
		public short Attribute;

		// Token: 0x04000F9B RID: 3995
		public SmallRect Window;

		// Token: 0x04000F9C RID: 3996
		public Coord MaxWindowSize;
	}
}
