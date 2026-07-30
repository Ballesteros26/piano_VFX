using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x0200011F RID: 287
	[Guid("69E5DF00-7B8B-11d3-AF61-00A024FFC08C")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIWebBrowser
	{
		// Token: 0x0600089C RID: 2204
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int addWebBrowserListener([MarshalAs(UnmanagedType.Interface)] nsIWeakReference aListener, [MarshalAs(UnmanagedType.LPStruct)] Guid aIID);

		// Token: 0x0600089D RID: 2205
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int removeWebBrowserListener([MarshalAs(UnmanagedType.Interface)] nsIWeakReference aListener, [MarshalAs(UnmanagedType.LPStruct)] Guid aIID);

		// Token: 0x0600089E RID: 2206
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getContainerWindow([MarshalAs(UnmanagedType.Interface)] out nsIWebBrowserChrome ret);

		// Token: 0x0600089F RID: 2207
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setContainerWindow([MarshalAs(UnmanagedType.Interface)] nsIWebBrowserChrome value);

		// Token: 0x060008A0 RID: 2208
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getParentURIContentListener([MarshalAs(UnmanagedType.Interface)] out nsIURIContentListener ret);

		// Token: 0x060008A1 RID: 2209
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setParentURIContentListener([MarshalAs(UnmanagedType.Interface)] nsIURIContentListener value);

		// Token: 0x060008A2 RID: 2210
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getContentDOMWindow([MarshalAs(UnmanagedType.Interface)] out nsIDOMWindow ret);
	}
}
