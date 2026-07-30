using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000129 RID: 297
	[Guid("86d02f0e-219b-4cfc-9c88-bd98d2cce0b8")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIWebBrowserStream
	{
		// Token: 0x060008CC RID: 2252
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int openStream([MarshalAs(UnmanagedType.Interface)] nsIURI aBaseURI, HandleRef aContentType);

		// Token: 0x060008CD RID: 2253
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int appendToStream(IntPtr aData, uint aLen);

		// Token: 0x060008CE RID: 2254
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int closeStream();
	}
}
