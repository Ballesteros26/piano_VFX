using System;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000031 RID: 49
	[Flags]
	internal enum ProgressDialogFlags : uint
	{
		// Token: 0x040000E2 RID: 226
		Normal = 0U,
		// Token: 0x040000E3 RID: 227
		Modal = 1U,
		// Token: 0x040000E4 RID: 228
		AutoTime = 2U,
		// Token: 0x040000E5 RID: 229
		NoTime = 4U,
		// Token: 0x040000E6 RID: 230
		NoMinimize = 8U,
		// Token: 0x040000E7 RID: 231
		NoProgressBar = 16U,
		// Token: 0x040000E8 RID: 232
		MarqueeProgress = 32U,
		// Token: 0x040000E9 RID: 233
		NoCancel = 64U
	}
}
