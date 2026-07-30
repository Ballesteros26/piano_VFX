using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000E1 RID: 225
	[Guid("a6cf906f-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMWindowCollection
	{
		// Token: 0x06000758 RID: 1880
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLength(out uint ret);

		// Token: 0x06000759 RID: 1881
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int item(uint index, [MarshalAs(UnmanagedType.Interface)] out nsIDOMWindow ret);

		// Token: 0x0600075A RID: 1882
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int namedItem(HandleRef name, [MarshalAs(UnmanagedType.Interface)] out nsIDOMWindow ret);
	}
}
