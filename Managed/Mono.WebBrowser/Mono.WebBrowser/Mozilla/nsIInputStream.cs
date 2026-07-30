using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000F1 RID: 241
	[Guid("fa9c7f6c-61b3-11d4-9877-00c04fa0cf4a")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIInputStream
	{
		// Token: 0x060007BA RID: 1978
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int close();

		// Token: 0x060007BB RID: 1979
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int available(out uint ret);

		// Token: 0x060007BC RID: 1980
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int read(HandleRef aBuf, uint aCount, out uint ret);

		// Token: 0x060007BD RID: 1981
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int readSegments(nsIWriteSegmentFunDelegate aWriter, IntPtr aClosure, uint aCount, out uint ret);

		// Token: 0x060007BE RID: 1982
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int isNonBlocking(out bool ret);
	}
}
