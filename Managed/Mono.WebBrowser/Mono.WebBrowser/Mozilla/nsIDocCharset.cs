using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000E3 RID: 227
	[Guid("9c18bb4e-1dd1-11b2-bf91-9cc82c275823")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDocCharset
	{
		// Token: 0x0600075D RID: 1885
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCharset(ref IntPtr ret);

		// Token: 0x0600075E RID: 1886
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setCharset([MarshalAs(UnmanagedType.LPStr)] string value);
	}
}
