using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x0200010D RID: 269
	[Guid("B2C7ED59-8634-4352-9E37-5484C8B6E4E1")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsISelection
	{
		// Token: 0x06000840 RID: 2112
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAnchorNode([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x06000841 RID: 2113
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAnchorOffset(out int ret);

		// Token: 0x06000842 RID: 2114
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getFocusNode([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x06000843 RID: 2115
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getFocusOffset(out int ret);

		// Token: 0x06000844 RID: 2116
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getIsCollapsed(out bool ret);

		// Token: 0x06000845 RID: 2117
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getRangeCount(out int ret);

		// Token: 0x06000846 RID: 2118
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getRangeAt(int index, [MarshalAs(UnmanagedType.Interface)] out nsIDOMRange ret);

		// Token: 0x06000847 RID: 2119
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int collapse([MarshalAs(UnmanagedType.Interface)] nsIDOMNode parentNode, int offset);

		// Token: 0x06000848 RID: 2120
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int extend([MarshalAs(UnmanagedType.Interface)] nsIDOMNode parentNode, int offset);

		// Token: 0x06000849 RID: 2121
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int collapseToStart();

		// Token: 0x0600084A RID: 2122
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int collapseToEnd();

		// Token: 0x0600084B RID: 2123
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int containsNode([MarshalAs(UnmanagedType.Interface)] nsIDOMNode node, bool entirelyContained, out bool ret);

		// Token: 0x0600084C RID: 2124
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int selectAllChildren([MarshalAs(UnmanagedType.Interface)] nsIDOMNode parentNode);

		// Token: 0x0600084D RID: 2125
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int addRange([MarshalAs(UnmanagedType.Interface)] nsIDOMRange range);

		// Token: 0x0600084E RID: 2126
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int removeRange([MarshalAs(UnmanagedType.Interface)] nsIDOMRange range);

		// Token: 0x0600084F RID: 2127
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int removeAllRanges();

		// Token: 0x06000850 RID: 2128
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int deleteFromDocument();

		// Token: 0x06000851 RID: 2129
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int selectionLanguageChange(bool langRTL);

		// Token: 0x06000852 RID: 2130
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int toString([MarshalAs(UnmanagedType.LPWStr)] string ret);
	}
}
