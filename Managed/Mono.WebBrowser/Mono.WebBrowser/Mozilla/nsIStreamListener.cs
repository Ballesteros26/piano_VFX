using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000113 RID: 275
	[Guid("1a637020-1482-11d3-9333-00104ba0fd40")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIStreamListener : nsIRequestObserver
	{
		// Token: 0x0600085F RID: 2143
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int onStartRequest([MarshalAs(UnmanagedType.Interface)] nsIRequest aRequest, IntPtr aContext);

		// Token: 0x06000860 RID: 2144
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int onStopRequest([MarshalAs(UnmanagedType.Interface)] nsIRequest aRequest, IntPtr aContext, int aStatusCode);

		// Token: 0x06000861 RID: 2145
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int onDataAvailable([MarshalAs(UnmanagedType.Interface)] nsIRequest aRequest, IntPtr aContext, [MarshalAs(UnmanagedType.Interface)] nsIInputStream aInputStream, uint aOffset, uint aCount);
	}
}
