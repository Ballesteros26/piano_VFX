using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200021B RID: 539
	[StructLayout(LayoutKind.Sequential)]
	internal class MonoAsyncCall
	{
		// Token: 0x04000CBF RID: 3263
		private object msg;

		// Token: 0x04000CC0 RID: 3264
		private IntPtr cb_method;

		// Token: 0x04000CC1 RID: 3265
		private object cb_target;

		// Token: 0x04000CC2 RID: 3266
		private object state;

		// Token: 0x04000CC3 RID: 3267
		private object res;

		// Token: 0x04000CC4 RID: 3268
		private object out_args;
	}
}
