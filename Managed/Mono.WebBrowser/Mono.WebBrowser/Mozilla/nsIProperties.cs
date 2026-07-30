using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000101 RID: 257
	[Guid("78650582-4e93-4b60-8e85-26ebd3eb14ca")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIProperties
	{
		// Token: 0x0600080E RID: 2062
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int get([MarshalAs(UnmanagedType.LPStr)] string prop, [MarshalAs(UnmanagedType.LPStruct)] Guid iid, out IntPtr result);

		// Token: 0x0600080F RID: 2063
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int set([MarshalAs(UnmanagedType.LPStr)] string prop, [MarshalAs(UnmanagedType.Interface)] IntPtr value);

		// Token: 0x06000810 RID: 2064
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int has([MarshalAs(UnmanagedType.LPStr)] string prop, out bool ret);

		// Token: 0x06000811 RID: 2065
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int undefine([MarshalAs(UnmanagedType.LPStr)] string prop);

		// Token: 0x06000812 RID: 2066
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getKeys(out uint count, [MarshalAs(UnmanagedType.LPStr)] out string[] keys);
	}
}
