using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x0200012D RID: 301
	[Guid("570F39D0-EFD0-11d3-B093-00A024FFC08C")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIWebProgress
	{
		// Token: 0x060008E0 RID: 2272
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int addProgressListener([MarshalAs(UnmanagedType.Interface)] nsIWebProgressListener aListener, uint aNotifyMask);

		// Token: 0x060008E1 RID: 2273
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int removeProgressListener([MarshalAs(UnmanagedType.Interface)] nsIWebProgressListener aListener);

		// Token: 0x060008E2 RID: 2274
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getDOMWindow([MarshalAs(UnmanagedType.Interface)] out nsIDOMWindow ret);

		// Token: 0x060008E3 RID: 2275
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getIsLoadingDocument(out bool ret);
	}
}
