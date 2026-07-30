using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000089 RID: 137
	[Guid("a6cf90c0-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMCSSRuleList
	{
		// Token: 0x06000403 RID: 1027
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLength(out uint ret);

		// Token: 0x06000404 RID: 1028
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int item(uint index, [MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSRule ret);
	}
}
