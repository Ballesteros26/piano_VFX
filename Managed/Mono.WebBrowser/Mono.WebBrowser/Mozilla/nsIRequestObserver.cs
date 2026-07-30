using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000107 RID: 263
	[Guid("fd91e2e0-1481-11d3-9333-00104ba0fd40")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIRequestObserver
	{
		// Token: 0x06000829 RID: 2089
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int onStartRequest([MarshalAs(UnmanagedType.Interface)] nsIRequest aRequest, IntPtr aContext);

		// Token: 0x0600082A RID: 2090
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int onStopRequest([MarshalAs(UnmanagedType.Interface)] nsIRequest aRequest, IntPtr aContext, int aStatusCode);
	}
}
