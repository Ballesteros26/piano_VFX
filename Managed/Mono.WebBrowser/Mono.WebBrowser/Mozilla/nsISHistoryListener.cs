using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x0200010B RID: 267
	[Guid("3b07f591-e8e1-11d4-9882-00c04fa02f40")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsISHistoryListener
	{
		// Token: 0x06000838 RID: 2104
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int OnHistoryNewEntry([MarshalAs(UnmanagedType.Interface)] nsIURI aNewURI);

		// Token: 0x06000839 RID: 2105
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int OnHistoryGoBack([MarshalAs(UnmanagedType.Interface)] nsIURI aBackURI, out bool ret);

		// Token: 0x0600083A RID: 2106
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int OnHistoryGoForward([MarshalAs(UnmanagedType.Interface)] nsIURI aForwardURI, out bool ret);

		// Token: 0x0600083B RID: 2107
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int OnHistoryReload([MarshalAs(UnmanagedType.Interface)] nsIURI aReloadURI, uint aReloadFlags, out bool ret);

		// Token: 0x0600083C RID: 2108
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int OnHistoryGotoIndex(int aIndex, [MarshalAs(UnmanagedType.Interface)] nsIURI aGotoURI, out bool ret);

		// Token: 0x0600083D RID: 2109
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int OnHistoryPurge(int aNumEntries, out bool ret);
	}
}
