using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000105 RID: 261
	[Guid("ef6bfbd2-fd46-48d8-96b7-9f8f0fd387fe")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIRequest
	{
		// Token: 0x0600081D RID: 2077
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getName(HandleRef ret);

		// Token: 0x0600081E RID: 2078
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int isPending(out bool ret);

		// Token: 0x0600081F RID: 2079
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getStatus(out uint ret);

		// Token: 0x06000820 RID: 2080
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int cancel(uint aStatus);

		// Token: 0x06000821 RID: 2081
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int suspend();

		// Token: 0x06000822 RID: 2082
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int resume();

		// Token: 0x06000823 RID: 2083
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLoadGroup([MarshalAs(UnmanagedType.Interface)] out nsILoadGroup ret);

		// Token: 0x06000824 RID: 2084
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setLoadGroup([MarshalAs(UnmanagedType.Interface)] nsILoadGroup value);

		// Token: 0x06000825 RID: 2085
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLoadFlags(out ulong ret);

		// Token: 0x06000826 RID: 2086
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setLoadFlags(ulong value);
	}
}
