using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200011C RID: 284
	internal static class IOSelector
	{
		// Token: 0x060007AB RID: 1963
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Add(IntPtr handle, IOSelectorJob job);

		// Token: 0x060007AC RID: 1964
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Remove(IntPtr handle);
	}
}
