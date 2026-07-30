using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000CF RID: 207
	[Guid("6aff3102-320d-4986-9790-12316bb87cf9")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMRGBColor
	{
		// Token: 0x060006DB RID: 1755
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getRed([MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSPrimitiveValue ret);

		// Token: 0x060006DC RID: 1756
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getGreen([MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSPrimitiveValue ret);

		// Token: 0x060006DD RID: 1757
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getBlue([MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSPrimitiveValue ret);
	}
}
