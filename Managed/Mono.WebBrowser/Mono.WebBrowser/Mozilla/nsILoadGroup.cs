using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000F5 RID: 245
	[Guid("3de0a31c-feaf-400f-9f1e-4ef71f8b20cc")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsILoadGroup : nsIRequest
	{
		// Token: 0x060007C4 RID: 1988
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getName(HandleRef ret);

		// Token: 0x060007C5 RID: 1989
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int isPending(out bool ret);

		// Token: 0x060007C6 RID: 1990
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getStatus(out int ret);

		// Token: 0x060007C7 RID: 1991
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int cancel(int aStatus);

		// Token: 0x060007C8 RID: 1992
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int suspend();

		// Token: 0x060007C9 RID: 1993
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int resume();

		// Token: 0x060007CA RID: 1994
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLoadGroup([MarshalAs(UnmanagedType.Interface)] out nsILoadGroup ret);

		// Token: 0x060007CB RID: 1995
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setLoadGroup([MarshalAs(UnmanagedType.Interface)] nsILoadGroup value);

		// Token: 0x060007CC RID: 1996
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLoadFlags(out ulong ret);

		// Token: 0x060007CD RID: 1997
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setLoadFlags(ulong value);

		// Token: 0x060007CE RID: 1998
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getGroupObserver([MarshalAs(UnmanagedType.Interface)] out nsIRequestObserver ret);

		// Token: 0x060007CF RID: 1999
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setGroupObserver([MarshalAs(UnmanagedType.Interface)] nsIRequestObserver value);

		// Token: 0x060007D0 RID: 2000
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getDefaultLoadRequest([MarshalAs(UnmanagedType.Interface)] out nsIRequest ret);

		// Token: 0x060007D1 RID: 2001
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setDefaultLoadRequest([MarshalAs(UnmanagedType.Interface)] nsIRequest value);

		// Token: 0x060007D2 RID: 2002
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int addRequest([MarshalAs(UnmanagedType.Interface)] nsIRequest aRequest, IntPtr aContext);

		// Token: 0x060007D3 RID: 2003
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int removeRequest([MarshalAs(UnmanagedType.Interface)] nsIRequest aRequest, IntPtr aContext, int aStatus);

		// Token: 0x060007D4 RID: 2004
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getRequests([MarshalAs(UnmanagedType.Interface)] out nsISimpleEnumerator ret);

		// Token: 0x060007D5 RID: 2005
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getActiveCount(out uint ret);

		// Token: 0x060007D6 RID: 2006
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNotificationCallbacks([MarshalAs(UnmanagedType.Interface)] out nsIInterfaceRequestor ret);

		// Token: 0x060007D7 RID: 2007
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setNotificationCallbacks([MarshalAs(UnmanagedType.Interface)] nsIInterfaceRequestor value);
	}
}
