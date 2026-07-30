using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000C1 RID: 193
	[Guid("ff751edc-8b02-aae7-0010-8301838a3123")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMMouseEvent : nsIDOMUIEvent, nsIDOMEvent
	{
		// Token: 0x0600065C RID: 1628
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getType(HandleRef ret);

		// Token: 0x0600065D RID: 1629
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTarget([MarshalAs(UnmanagedType.Interface)] out nsIDOMEventTarget ret);

		// Token: 0x0600065E RID: 1630
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCurrentTarget([MarshalAs(UnmanagedType.Interface)] out nsIDOMEventTarget ret);

		// Token: 0x0600065F RID: 1631
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getEventPhase(out ushort ret);

		// Token: 0x06000660 RID: 1632
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getBubbles(out bool ret);

		// Token: 0x06000661 RID: 1633
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCancelable(out bool ret);

		// Token: 0x06000662 RID: 1634
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTimeStamp(out int ret);

		// Token: 0x06000663 RID: 1635
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int stopPropagation();

		// Token: 0x06000664 RID: 1636
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int preventDefault();

		// Token: 0x06000665 RID: 1637
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int initEvent(HandleRef eventTypeArg, bool canBubbleArg, bool cancelableArg);

		// Token: 0x06000666 RID: 1638
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getView([MarshalAs(UnmanagedType.Interface)] out nsIDOMAbstractView ret);

		// Token: 0x06000667 RID: 1639
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getDetail(out int ret);

		// Token: 0x06000668 RID: 1640
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int initUIEvent(HandleRef typeArg, bool canBubbleArg, bool cancelableArg, [MarshalAs(UnmanagedType.Interface)] nsIDOMAbstractView viewArg, int detailArg);

		// Token: 0x06000669 RID: 1641
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getScreenX(out int ret);

		// Token: 0x0600066A RID: 1642
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getScreenY(out int ret);

		// Token: 0x0600066B RID: 1643
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getClientX(out int ret);

		// Token: 0x0600066C RID: 1644
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getClientY(out int ret);

		// Token: 0x0600066D RID: 1645
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCtrlKey(out bool ret);

		// Token: 0x0600066E RID: 1646
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getShiftKey(out bool ret);

		// Token: 0x0600066F RID: 1647
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAltKey(out bool ret);

		// Token: 0x06000670 RID: 1648
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getMetaKey(out bool ret);

		// Token: 0x06000671 RID: 1649
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getButton(out ushort ret);

		// Token: 0x06000672 RID: 1650
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getRelatedTarget([MarshalAs(UnmanagedType.Interface)] out nsIDOMEventTarget ret);

		// Token: 0x06000673 RID: 1651
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int initMouseEvent(HandleRef typeArg, bool canBubbleArg, bool cancelableArg, [MarshalAs(UnmanagedType.Interface)] nsIDOMAbstractView viewArg, int detailArg, int screenXArg, int screenYArg, int clientXArg, int clientYArg, bool ctrlKeyArg, bool altKeyArg, bool shiftKeyArg, bool metaKeyArg, ushort buttonArg, [MarshalAs(UnmanagedType.Interface)] nsIDOMEventTarget relatedTargetArg);
	}
}
