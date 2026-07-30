using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Win32
{
	// Token: 0x020000BB RID: 187
	internal static class NativeMethods
	{
		// Token: 0x06000634 RID: 1588
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetCurrentProcessId();
	}
}
