using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000079 RID: 121
	[Guid("c63a055a-a676-4e71-bf3c-6cfa11082018")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIChannel : nsIRequest
	{
		// Token: 0x0600037E RID: 894
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getName(HandleRef ret);

		// Token: 0x0600037F RID: 895
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int isPending(out bool ret);

		// Token: 0x06000380 RID: 896
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getStatus(out int ret);

		// Token: 0x06000381 RID: 897
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int cancel(int aStatus);

		// Token: 0x06000382 RID: 898
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int suspend();

		// Token: 0x06000383 RID: 899
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int resume();

		// Token: 0x06000384 RID: 900
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLoadGroup([MarshalAs(UnmanagedType.Interface)] out nsILoadGroup ret);

		// Token: 0x06000385 RID: 901
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setLoadGroup([MarshalAs(UnmanagedType.Interface)] nsILoadGroup value);

		// Token: 0x06000386 RID: 902
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLoadFlags(out ulong ret);

		// Token: 0x06000387 RID: 903
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setLoadFlags(ulong value);

		// Token: 0x06000388 RID: 904
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getOriginalURI([MarshalAs(UnmanagedType.Interface)] out nsIURI ret);

		// Token: 0x06000389 RID: 905
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setOriginalURI([MarshalAs(UnmanagedType.Interface)] nsIURI value);

		// Token: 0x0600038A RID: 906
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getURI([MarshalAs(UnmanagedType.Interface)] out nsIURI ret);

		// Token: 0x0600038B RID: 907
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getOwner(out IntPtr ret);

		// Token: 0x0600038C RID: 908
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setOwner(IntPtr value);

		// Token: 0x0600038D RID: 909
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNotificationCallbacks([MarshalAs(UnmanagedType.Interface)] out nsIInterfaceRequestor ret);

		// Token: 0x0600038E RID: 910
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setNotificationCallbacks([MarshalAs(UnmanagedType.Interface)] nsIInterfaceRequestor value);

		// Token: 0x0600038F RID: 911
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getSecurityInfo(out IntPtr ret);

		// Token: 0x06000390 RID: 912
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getContentType(HandleRef ret);

		// Token: 0x06000391 RID: 913
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setContentType(HandleRef value);

		// Token: 0x06000392 RID: 914
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getContentCharset(HandleRef ret);

		// Token: 0x06000393 RID: 915
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setContentCharset(HandleRef value);

		// Token: 0x06000394 RID: 916
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getContentLength(out int ret);

		// Token: 0x06000395 RID: 917
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setContentLength(int value);

		// Token: 0x06000396 RID: 918
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int open([MarshalAs(UnmanagedType.Interface)] out nsIInputStream ret);

		// Token: 0x06000397 RID: 919
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int asyncOpen([MarshalAs(UnmanagedType.Interface)] nsIStreamListener aListener, IntPtr aContext);
	}
}
