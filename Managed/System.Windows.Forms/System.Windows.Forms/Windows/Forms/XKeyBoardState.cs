using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000418 RID: 1048
	internal struct XKeyBoardState
	{
		// Token: 0x040020C8 RID: 8392
		public int key_click_percent;

		// Token: 0x040020C9 RID: 8393
		public int bell_percent;

		// Token: 0x040020CA RID: 8394
		public uint bell_pitch;

		// Token: 0x040020CB RID: 8395
		public uint bell_duration;

		// Token: 0x040020CC RID: 8396
		public IntPtr led_mask;

		// Token: 0x040020CD RID: 8397
		public int global_auto_repeat;

		// Token: 0x040020CE RID: 8398
		public XKeyBoardState.AutoRepeats auto_repeats;

		// Token: 0x02000419 RID: 1049
		[StructLayout(2)]
		public struct AutoRepeats
		{
			// Token: 0x040020CF RID: 8399
			[FieldOffset(0)]
			public byte first;

			// Token: 0x040020D0 RID: 8400
			[FieldOffset(31)]
			public byte last;
		}
	}
}
