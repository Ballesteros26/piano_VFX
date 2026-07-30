using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000D3 RID: 211
	[Guid("71735f62-ac5c-4236-9a1f-5ffb280d531c")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMRect
	{
		// Token: 0x060006FA RID: 1786
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTop([MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSPrimitiveValue ret);

		// Token: 0x060006FB RID: 1787
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getRight([MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSPrimitiveValue ret);

		// Token: 0x060006FC RID: 1788
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getBottom([MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSPrimitiveValue ret);

		// Token: 0x060006FD RID: 1789
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLeft([MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSPrimitiveValue ret);
	}
}
