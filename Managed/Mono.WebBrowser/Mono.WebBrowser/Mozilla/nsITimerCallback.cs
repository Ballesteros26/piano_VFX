using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000117 RID: 279
	[Guid("a796816d-7d47-4348-9ab8-c7aeb3216a7d")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsITimerCallback
	{
		// Token: 0x06000870 RID: 2160
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int notify([MarshalAs(UnmanagedType.Interface)] nsITimer timer);
	}
}
