using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000C3 RID: 195
	[Guid("da83b2ec-8264-4410-8496-ada3acd2ae42")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMNSHTMLElement
	{
		// Token: 0x06000676 RID: 1654
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getOffsetTop(out int ret);

		// Token: 0x06000677 RID: 1655
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getOffsetLeft(out int ret);

		// Token: 0x06000678 RID: 1656
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getOffsetWidth(out int ret);

		// Token: 0x06000679 RID: 1657
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getOffsetHeight(out int ret);

		// Token: 0x0600067A RID: 1658
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getOffsetParent([MarshalAs(UnmanagedType.Interface)] out nsIDOMElement ret);

		// Token: 0x0600067B RID: 1659
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getInnerHTML(HandleRef ret);

		// Token: 0x0600067C RID: 1660
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setInnerHTML(HandleRef value);

		// Token: 0x0600067D RID: 1661
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getScrollTop(out int ret);

		// Token: 0x0600067E RID: 1662
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setScrollTop(int value);

		// Token: 0x0600067F RID: 1663
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getScrollLeft(out int ret);

		// Token: 0x06000680 RID: 1664
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setScrollLeft(int value);

		// Token: 0x06000681 RID: 1665
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getScrollHeight(out int ret);

		// Token: 0x06000682 RID: 1666
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getScrollWidth(out int ret);

		// Token: 0x06000683 RID: 1667
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getClientHeight(out int ret);

		// Token: 0x06000684 RID: 1668
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getClientWidth(out int ret);

		// Token: 0x06000685 RID: 1669
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTabIndex(out int ret);

		// Token: 0x06000686 RID: 1670
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setTabIndex(int value);

		// Token: 0x06000687 RID: 1671
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int blur();

		// Token: 0x06000688 RID: 1672
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int focus();

		// Token: 0x06000689 RID: 1673
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int scrollIntoView(bool top);
	}
}
