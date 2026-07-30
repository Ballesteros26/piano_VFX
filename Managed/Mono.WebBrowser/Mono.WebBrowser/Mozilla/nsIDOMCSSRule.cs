using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000087 RID: 135
	[Guid("a6cf90c1-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMCSSRule
	{
		// Token: 0x060003FC RID: 1020
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getType(out ushort ret);

		// Token: 0x060003FD RID: 1021
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCssText(HandleRef ret);

		// Token: 0x060003FE RID: 1022
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setCssText(HandleRef value);

		// Token: 0x060003FF RID: 1023
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getParentStyleSheet([MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSStyleSheet ret);

		// Token: 0x06000400 RID: 1024
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getParentRule([MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSRule ret);
	}
}
