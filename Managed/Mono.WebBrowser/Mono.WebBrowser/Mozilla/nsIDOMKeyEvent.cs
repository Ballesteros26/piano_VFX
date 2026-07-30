using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000BD RID: 189
	[Guid("028e0e6e-8b01-11d3-aae7-0010838a3123")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMKeyEvent : nsIDOMUIEvent, nsIDOMEvent
	{
		// Token: 0x0600063E RID: 1598
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getType(HandleRef ret);

		// Token: 0x0600063F RID: 1599
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTarget([MarshalAs(UnmanagedType.Interface)] out nsIDOMEventTarget ret);

		// Token: 0x06000640 RID: 1600
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCurrentTarget([MarshalAs(UnmanagedType.Interface)] out nsIDOMEventTarget ret);

		// Token: 0x06000641 RID: 1601
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getEventPhase(out ushort ret);

		// Token: 0x06000642 RID: 1602
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getBubbles(out bool ret);

		// Token: 0x06000643 RID: 1603
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCancelable(out bool ret);

		// Token: 0x06000644 RID: 1604
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTimeStamp(out int ret);

		// Token: 0x06000645 RID: 1605
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int stopPropagation();

		// Token: 0x06000646 RID: 1606
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int preventDefault();

		// Token: 0x06000647 RID: 1607
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int initEvent(HandleRef eventTypeArg, bool canBubbleArg, bool cancelableArg);

		// Token: 0x06000648 RID: 1608
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getView([MarshalAs(UnmanagedType.Interface)] out nsIDOMAbstractView ret);

		// Token: 0x06000649 RID: 1609
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getDetail(out int ret);

		// Token: 0x0600064A RID: 1610
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int initUIEvent(HandleRef typeArg, bool canBubbleArg, bool cancelableArg, [MarshalAs(UnmanagedType.Interface)] nsIDOMAbstractView viewArg, int detailArg);

		// Token: 0x0600064B RID: 1611
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCharCode(out uint ret);

		// Token: 0x0600064C RID: 1612
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getKeyCode(out uint ret);

		// Token: 0x0600064D RID: 1613
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAltKey(out bool ret);

		// Token: 0x0600064E RID: 1614
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCtrlKey(out bool ret);

		// Token: 0x0600064F RID: 1615
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getShiftKey(out bool ret);

		// Token: 0x06000650 RID: 1616
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getMetaKey(out bool ret);

		// Token: 0x06000651 RID: 1617
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int initKeyEvent(HandleRef typeArg, bool canBubbleArg, bool cancelableArg, [MarshalAs(UnmanagedType.Interface)] nsIDOMAbstractView viewArg, bool ctrlKeyArg, bool altKeyArg, bool shiftKeyArg, bool metaKeyArg, uint keyCodeArg, uint charCodeArg);
	}
}
