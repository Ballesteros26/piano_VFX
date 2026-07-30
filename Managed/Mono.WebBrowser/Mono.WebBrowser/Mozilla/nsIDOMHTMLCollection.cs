using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000B5 RID: 181
	[Guid("a6cf9083-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMHTMLCollection
	{
		// Token: 0x0600058A RID: 1418
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLength(out uint ret);

		// Token: 0x0600058B RID: 1419
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int item(uint index, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x0600058C RID: 1420
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int namedItem(HandleRef name, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);
	}
}
