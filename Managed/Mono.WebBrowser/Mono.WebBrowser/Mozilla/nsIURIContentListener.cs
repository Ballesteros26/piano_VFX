using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x0200011B RID: 283
	[Guid("94928AB3-8B63-11d3-989D-001083010E9B")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIURIContentListener
	{
		// Token: 0x0600088F RID: 2191
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		bool onStartURIOpen([MarshalAs(UnmanagedType.Interface)] nsIURI aURI);

		// Token: 0x06000890 RID: 2192
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		bool doContent([MarshalAs(UnmanagedType.LPStr)] string aContentType, bool aIsContentPreferred, [MarshalAs(UnmanagedType.Interface)] nsIRequest aRequest, [MarshalAs(UnmanagedType.Interface)] out nsIStreamListener aContentHandler);

		// Token: 0x06000891 RID: 2193
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		bool isPreferred([MarshalAs(UnmanagedType.LPStr)] string aContentType, [MarshalAs(UnmanagedType.LPStr)] ref string aDesiredContentType);

		// Token: 0x06000892 RID: 2194
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		bool canHandleContent([MarshalAs(UnmanagedType.LPStr)] string aContentType, bool aIsContentPreferred, [MarshalAs(UnmanagedType.LPStr)] ref string aDesiredContentType);

		// Token: 0x06000893 RID: 2195
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IntPtr getLoadCookie();

		// Token: 0x06000894 RID: 2196
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void setLoadCookie([MarshalAs(UnmanagedType.Interface)] IntPtr value);

		// Token: 0x06000895 RID: 2197
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[return: MarshalAs(UnmanagedType.Interface)]
		nsIURIContentListener getParentContentListener();

		// Token: 0x06000896 RID: 2198
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void setParentContentListener([MarshalAs(UnmanagedType.Interface)] nsIURIContentListener value);
	}
}
