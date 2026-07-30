using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000D7 RID: 215
	[Guid("a6cf9081-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMStyleSheetList
	{
		// Token: 0x0600070A RID: 1802
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLength(out uint ret);

		// Token: 0x0600070B RID: 1803
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int item(uint index, [MarshalAs(UnmanagedType.Interface)] out nsIDOMStyleSheet ret);
	}
}
