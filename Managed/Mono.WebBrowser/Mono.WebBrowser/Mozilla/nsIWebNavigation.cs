using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x0200012B RID: 299
	[Guid("F5D9E7B0-D930-11d3-B057-00A024FFC08C")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIWebNavigation
	{
		// Token: 0x060008D1 RID: 2257
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCanGoBack(out bool ret);

		// Token: 0x060008D2 RID: 2258
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCanGoForward(out bool ret);

		// Token: 0x060008D3 RID: 2259
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int goBack();

		// Token: 0x060008D4 RID: 2260
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int goForward();

		// Token: 0x060008D5 RID: 2261
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int gotoIndex(int index);

		// Token: 0x060008D6 RID: 2262
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int loadURI([MarshalAs(UnmanagedType.LPWStr)] string aURI, uint aLoadFlags, [MarshalAs(UnmanagedType.Interface)] nsIURI aReferrer, [MarshalAs(UnmanagedType.Interface)] nsIInputStream aPostData, [MarshalAs(UnmanagedType.Interface)] nsIInputStream aHeaders);

		// Token: 0x060008D7 RID: 2263
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int reload(uint aReloadFlags);

		// Token: 0x060008D8 RID: 2264
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int stop(uint aStopFlags);

		// Token: 0x060008D9 RID: 2265
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getDocument([MarshalAs(UnmanagedType.Interface)] out nsIDOMDocument ret);

		// Token: 0x060008DA RID: 2266
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCurrentURI([MarshalAs(UnmanagedType.Interface)] out nsIURI ret);

		// Token: 0x060008DB RID: 2267
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getReferringURI([MarshalAs(UnmanagedType.Interface)] out nsIURI ret);

		// Token: 0x060008DC RID: 2268
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getSessionHistory([MarshalAs(UnmanagedType.Interface)] out nsISHistory ret);

		// Token: 0x060008DD RID: 2269
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setSessionHistory([MarshalAs(UnmanagedType.Interface)] nsISHistory value);
	}
}
