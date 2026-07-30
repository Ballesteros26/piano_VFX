using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000ED RID: 237
	[Guid("A41661D4-1417-11D5-9882-00C04FA02F40")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIHistoryEntry
	{
		// Token: 0x060007A9 RID: 1961
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getURI([MarshalAs(UnmanagedType.Interface)] out nsIURI ret);

		// Token: 0x060007AA RID: 1962
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTitle([MarshalAs(UnmanagedType.LPWStr)] string ret);

		// Token: 0x060007AB RID: 1963
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getIsSubFrame(out bool ret);
	}
}
