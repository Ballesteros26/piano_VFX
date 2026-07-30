using System;

namespace System.Windows.Forms
{
	// Token: 0x0200042C RID: 1068
	[Flags]
	internal enum XWMHintsFlags
	{
		// Token: 0x040021AE RID: 8622
		InputHint = 1,
		// Token: 0x040021AF RID: 8623
		StateHint = 2,
		// Token: 0x040021B0 RID: 8624
		IconPixmapHint = 4,
		// Token: 0x040021B1 RID: 8625
		IconWindowHint = 8,
		// Token: 0x040021B2 RID: 8626
		IconPositionHint = 16,
		// Token: 0x040021B3 RID: 8627
		IconMaskHint = 32,
		// Token: 0x040021B4 RID: 8628
		WindowGroupHint = 64,
		// Token: 0x040021B5 RID: 8629
		AllHints = 127
	}
}
