using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000DB RID: 219
	[Guid("a6cf90c3-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMUIEvent : nsIDOMEvent
	{
		// Token: 0x06000732 RID: 1842
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getType(HandleRef ret);

		// Token: 0x06000733 RID: 1843
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTarget([MarshalAs(UnmanagedType.Interface)] out nsIDOMEventTarget ret);

		// Token: 0x06000734 RID: 1844
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCurrentTarget([MarshalAs(UnmanagedType.Interface)] out nsIDOMEventTarget ret);

		// Token: 0x06000735 RID: 1845
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getEventPhase(out ushort ret);

		// Token: 0x06000736 RID: 1846
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getBubbles(out bool ret);

		// Token: 0x06000737 RID: 1847
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCancelable(out bool ret);

		// Token: 0x06000738 RID: 1848
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTimeStamp(out int ret);

		// Token: 0x06000739 RID: 1849
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int stopPropagation();

		// Token: 0x0600073A RID: 1850
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int preventDefault();

		// Token: 0x0600073B RID: 1851
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int initEvent(HandleRef eventTypeArg, bool canBubbleArg, bool cancelableArg);

		// Token: 0x0600073C RID: 1852
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getView([MarshalAs(UnmanagedType.Interface)] out nsIDOMAbstractView ret);

		// Token: 0x0600073D RID: 1853
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getDetail(out int ret);

		// Token: 0x0600073E RID: 1854
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int initUIEvent(HandleRef typeArg, bool canBubbleArg, bool cancelableArg, [MarshalAs(UnmanagedType.Interface)] nsIDOMAbstractView viewArg, int detailArg);
	}
}
