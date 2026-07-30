using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000D1 RID: 209
	[Guid("a6cf90ce-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMRange
	{
		// Token: 0x060006E0 RID: 1760
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getStartContainer([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060006E1 RID: 1761
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getStartOffset(out int ret);

		// Token: 0x060006E2 RID: 1762
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getEndContainer([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060006E3 RID: 1763
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getEndOffset(out int ret);

		// Token: 0x060006E4 RID: 1764
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCollapsed(out bool ret);

		// Token: 0x060006E5 RID: 1765
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCommonAncestorContainer([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060006E6 RID: 1766
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setStart([MarshalAs(UnmanagedType.Interface)] nsIDOMNode refNode, int offset);

		// Token: 0x060006E7 RID: 1767
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setEnd([MarshalAs(UnmanagedType.Interface)] nsIDOMNode refNode, int offset);

		// Token: 0x060006E8 RID: 1768
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setStartBefore([MarshalAs(UnmanagedType.Interface)] nsIDOMNode refNode);

		// Token: 0x060006E9 RID: 1769
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setStartAfter([MarshalAs(UnmanagedType.Interface)] nsIDOMNode refNode);

		// Token: 0x060006EA RID: 1770
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setEndBefore([MarshalAs(UnmanagedType.Interface)] nsIDOMNode refNode);

		// Token: 0x060006EB RID: 1771
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setEndAfter([MarshalAs(UnmanagedType.Interface)] nsIDOMNode refNode);

		// Token: 0x060006EC RID: 1772
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int collapse(bool toStart);

		// Token: 0x060006ED RID: 1773
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int selectNode([MarshalAs(UnmanagedType.Interface)] nsIDOMNode refNode);

		// Token: 0x060006EE RID: 1774
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int selectNodeContents([MarshalAs(UnmanagedType.Interface)] nsIDOMNode refNode);

		// Token: 0x060006EF RID: 1775
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int compareBoundaryPoints(ushort how, [MarshalAs(UnmanagedType.Interface)] nsIDOMRange sourceRange, out short ret);

		// Token: 0x060006F0 RID: 1776
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int deleteContents();

		// Token: 0x060006F1 RID: 1777
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int extractContents([MarshalAs(UnmanagedType.Interface)] out nsIDOMDocumentFragment ret);

		// Token: 0x060006F2 RID: 1778
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int cloneContents([MarshalAs(UnmanagedType.Interface)] out nsIDOMDocumentFragment ret);

		// Token: 0x060006F3 RID: 1779
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int insertNode([MarshalAs(UnmanagedType.Interface)] nsIDOMNode newNode);

		// Token: 0x060006F4 RID: 1780
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int surroundContents([MarshalAs(UnmanagedType.Interface)] nsIDOMNode newParent);

		// Token: 0x060006F5 RID: 1781
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int cloneRange([MarshalAs(UnmanagedType.Interface)] out nsIDOMRange ret);

		// Token: 0x060006F6 RID: 1782
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int toString(HandleRef ret);

		// Token: 0x060006F7 RID: 1783
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int detach();
	}
}
