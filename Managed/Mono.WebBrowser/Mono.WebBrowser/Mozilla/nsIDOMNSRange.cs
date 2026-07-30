using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000C5 RID: 197
	[Guid("a6cf90f2-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMNSRange
	{
		// Token: 0x0600068C RID: 1676
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createContextualFragment(HandleRef fragment, [MarshalAs(UnmanagedType.Interface)] out nsIDOMDocumentFragment ret);

		// Token: 0x0600068D RID: 1677
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int isPointInRange([MarshalAs(UnmanagedType.Interface)] nsIDOMNode parent, int offset, out bool ret);

		// Token: 0x0600068E RID: 1678
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int comparePoint([MarshalAs(UnmanagedType.Interface)] nsIDOMNode parent, int offset, out short ret);

		// Token: 0x0600068F RID: 1679
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int intersectsNode([MarshalAs(UnmanagedType.Interface)] nsIDOMNode n, out bool ret);

		// Token: 0x06000690 RID: 1680
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int compareNode([MarshalAs(UnmanagedType.Interface)] nsIDOMNode n, out ushort ret);

		// Token: 0x06000691 RID: 1681
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int nSDetach();
	}
}
