using System;

namespace System.Windows.Forms
{
	// Token: 0x020003FE RID: 1022
	[Flags]
	internal enum SetWindowValuemask
	{
		// Token: 0x04001FA4 RID: 8100
		Nothing = 0,
		// Token: 0x04001FA5 RID: 8101
		BackPixmap = 1,
		// Token: 0x04001FA6 RID: 8102
		BackPixel = 2,
		// Token: 0x04001FA7 RID: 8103
		BorderPixmap = 4,
		// Token: 0x04001FA8 RID: 8104
		BorderPixel = 8,
		// Token: 0x04001FA9 RID: 8105
		BitGravity = 16,
		// Token: 0x04001FAA RID: 8106
		WinGravity = 32,
		// Token: 0x04001FAB RID: 8107
		BackingStore = 64,
		// Token: 0x04001FAC RID: 8108
		BackingPlanes = 128,
		// Token: 0x04001FAD RID: 8109
		BackingPixel = 256,
		// Token: 0x04001FAE RID: 8110
		OverrideRedirect = 512,
		// Token: 0x04001FAF RID: 8111
		SaveUnder = 1024,
		// Token: 0x04001FB0 RID: 8112
		EventMask = 2048,
		// Token: 0x04001FB1 RID: 8113
		DontPropagate = 4096,
		// Token: 0x04001FB2 RID: 8114
		ColorMap = 8192,
		// Token: 0x04001FB3 RID: 8115
		Cursor = 16384
	}
}
