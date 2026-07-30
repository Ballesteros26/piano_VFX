using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x0200012F RID: 303
	[Guid("570F39D1-EFD0-11d3-B093-00A024FFC08C")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIWebProgressListener
	{
		// Token: 0x060008E6 RID: 2278
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int onStateChange([MarshalAs(UnmanagedType.Interface)] nsIWebProgress aWebProgress, [MarshalAs(UnmanagedType.Interface)] nsIRequest aRequest, uint aStateFlags, int aStatus);

		// Token: 0x060008E7 RID: 2279
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int onProgressChange([MarshalAs(UnmanagedType.Interface)] nsIWebProgress aWebProgress, [MarshalAs(UnmanagedType.Interface)] nsIRequest aRequest, int aCurSelfProgress, int aMaxSelfProgress, int aCurTotalProgress, int aMaxTotalProgress);

		// Token: 0x060008E8 RID: 2280
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int onLocationChange([MarshalAs(UnmanagedType.Interface)] nsIWebProgress aWebProgress, [MarshalAs(UnmanagedType.Interface)] nsIRequest aRequest, [MarshalAs(UnmanagedType.Interface)] nsIURI aLocation);

		// Token: 0x060008E9 RID: 2281
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int onStatusChange([MarshalAs(UnmanagedType.Interface)] nsIWebProgress aWebProgress, [MarshalAs(UnmanagedType.Interface)] nsIRequest aRequest, int aStatus, [MarshalAs(UnmanagedType.LPWStr)] string aMessage);

		// Token: 0x060008EA RID: 2282
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int onSecurityChange([MarshalAs(UnmanagedType.Interface)] nsIWebProgress aWebProgress, [MarshalAs(UnmanagedType.Interface)] nsIRequest aRequest, uint aState);
	}
}
