using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000F3 RID: 243
	[Guid("033A1470-8B2A-11d3-AF88-00A024FFC08C")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIInterfaceRequestor
	{
		// Token: 0x060007C1 RID: 1985
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getInterface([MarshalAs(UnmanagedType.LPStruct)] Guid uuid, out IntPtr result);
	}
}
