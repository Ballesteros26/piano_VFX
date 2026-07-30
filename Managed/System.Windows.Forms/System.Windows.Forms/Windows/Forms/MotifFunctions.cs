using System;

namespace System.Windows.Forms
{
	// Token: 0x02000411 RID: 1041
	[Flags]
	internal enum MotifFunctions
	{
		// Token: 0x0400209F RID: 8351
		All = 1,
		// Token: 0x040020A0 RID: 8352
		Resize = 2,
		// Token: 0x040020A1 RID: 8353
		Move = 4,
		// Token: 0x040020A2 RID: 8354
		Minimize = 8,
		// Token: 0x040020A3 RID: 8355
		Maximize = 16,
		// Token: 0x040020A4 RID: 8356
		Close = 32
	}
}
