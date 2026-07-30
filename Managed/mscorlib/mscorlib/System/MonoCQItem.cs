using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200021C RID: 540
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class MonoCQItem
	{
		// Token: 0x04000CC5 RID: 3269
		private object[] array;

		// Token: 0x04000CC6 RID: 3270
		private byte[] array_state;

		// Token: 0x04000CC7 RID: 3271
		private int head;

		// Token: 0x04000CC8 RID: 3272
		private int tail;
	}
}
