using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000075 RID: 117
	[Guid("114744d9-c369-456e-b55a-52fe52880d2d")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIArray
	{
		// Token: 0x06000375 RID: 885
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLength(out uint ret);

		// Token: 0x06000376 RID: 886
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int queryElementAt(uint index, [MarshalAs(UnmanagedType.LPStruct)] Guid uuid, out IntPtr result);

		// Token: 0x06000377 RID: 887
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int indexOf(uint startIndex, [MarshalAs(UnmanagedType.Interface)] IntPtr element, out uint ret);

		// Token: 0x06000378 RID: 888
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int enumerate([MarshalAs(UnmanagedType.Interface)] out nsISimpleEnumerator ret);
	}
}
