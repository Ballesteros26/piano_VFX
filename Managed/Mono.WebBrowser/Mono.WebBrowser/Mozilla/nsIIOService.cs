using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000EF RID: 239
	[Guid("bddeda3f-9020-4d12-8c70-984ee9f7935e")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIIOService
	{
		// Token: 0x060007AE RID: 1966
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getProtocolHandler([MarshalAs(UnmanagedType.LPStr)] string aScheme, [MarshalAs(UnmanagedType.Interface)] out nsIProtocolHandler ret);

		// Token: 0x060007AF RID: 1967
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getProtocolFlags([MarshalAs(UnmanagedType.LPStr)] string aScheme, out uint ret);

		// Token: 0x060007B0 RID: 1968
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int newURI(HandleRef aSpec, [MarshalAs(UnmanagedType.LPStr)] string aOriginCharset, [MarshalAs(UnmanagedType.Interface)] nsIURI aBaseURI, [MarshalAs(UnmanagedType.Interface)] out nsIURI ret);

		// Token: 0x060007B1 RID: 1969
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int newFileURI([MarshalAs(UnmanagedType.Interface)] nsIFile aFile, [MarshalAs(UnmanagedType.Interface)] out nsIURI ret);

		// Token: 0x060007B2 RID: 1970
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int newChannelFromURI([MarshalAs(UnmanagedType.Interface)] nsIURI aURI, [MarshalAs(UnmanagedType.Interface)] out nsIChannel ret);

		// Token: 0x060007B3 RID: 1971
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int newChannel(HandleRef aSpec, [MarshalAs(UnmanagedType.LPStr)] string aOriginCharset, [MarshalAs(UnmanagedType.Interface)] nsIURI aBaseURI, [MarshalAs(UnmanagedType.Interface)] out nsIChannel ret);

		// Token: 0x060007B4 RID: 1972
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getOffline(out bool ret);

		// Token: 0x060007B5 RID: 1973
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setOffline(bool value);

		// Token: 0x060007B6 RID: 1974
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int allowPort(int aPort, [MarshalAs(UnmanagedType.LPStr)] string aScheme, out bool ret);

		// Token: 0x060007B7 RID: 1975
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int extractScheme(HandleRef urlString, HandleRef ret);
	}
}
