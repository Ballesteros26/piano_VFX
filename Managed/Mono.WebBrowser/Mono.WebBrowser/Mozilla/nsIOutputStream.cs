using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000F9 RID: 249
	[Guid("0d0acd2a-61b4-11d4-9877-00c04fa0cf4a")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIOutputStream
	{
		// Token: 0x060007DD RID: 2013
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int close();

		// Token: 0x060007DE RID: 2014
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int flush();

		// Token: 0x060007DF RID: 2015
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int write([MarshalAs(UnmanagedType.LPStr)] string aBuf, uint aCount, out uint ret);

		// Token: 0x060007E0 RID: 2016
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int writeFrom([MarshalAs(UnmanagedType.Interface)] nsIInputStream aFromStream, uint aCount, out uint ret);

		// Token: 0x060007E1 RID: 2017
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int writeSegments(nsIReadSegmentFunDelegate aReader, IntPtr aClosure, uint aCount, out uint ret);

		// Token: 0x060007E2 RID: 2018
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int isNonBlocking(out bool ret);
	}
}
