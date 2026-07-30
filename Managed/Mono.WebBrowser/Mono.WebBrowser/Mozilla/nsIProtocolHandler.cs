using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000103 RID: 259
	[Guid("15fd6940-8ea7-11d3-93ad-00104ba0fd40")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIProtocolHandler
	{
		// Token: 0x06000815 RID: 2069
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getScheme(HandleRef ret);

		// Token: 0x06000816 RID: 2070
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getDefaultPort(out int ret);

		// Token: 0x06000817 RID: 2071
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getProtocolFlags(out uint ret);

		// Token: 0x06000818 RID: 2072
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int newURI(HandleRef aSpec, [MarshalAs(UnmanagedType.LPStr)] string aOriginCharset, [MarshalAs(UnmanagedType.Interface)] nsIURI aBaseURI, [MarshalAs(UnmanagedType.Interface)] out nsIURI ret);

		// Token: 0x06000819 RID: 2073
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int newChannel([MarshalAs(UnmanagedType.Interface)] nsIURI aURI, [MarshalAs(UnmanagedType.Interface)] out nsIChannel ret);

		// Token: 0x0600081A RID: 2074
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int allowPort(int port, [MarshalAs(UnmanagedType.LPStr)] string scheme, out bool ret);
	}
}
