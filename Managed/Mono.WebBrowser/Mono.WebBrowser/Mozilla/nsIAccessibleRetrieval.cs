using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000073 RID: 115
	[Guid("244e4c67-a1d3-44f2-9cab-cdaa31b68046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIAccessibleRetrieval
	{
		// Token: 0x06000367 RID: 871
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAccessibleFor([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000368 RID: 872
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAttachedAccessibleFor([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000369 RID: 873
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getRelevantContentNodeFor([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x0600036A RID: 874
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAccessibleInWindow([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aDOMWin, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x0600036B RID: 875
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAccessibleInWeakShell([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] nsIWeakReference aPresShell, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x0600036C RID: 876
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAccessibleInShell([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, IntPtr aPresShell, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x0600036D RID: 877
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCachedAccessNode([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] nsIWeakReference aShell, [MarshalAs(UnmanagedType.Interface)] out nsIAccessNode ret);

		// Token: 0x0600036E RID: 878
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCachedAccessible([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] nsIWeakReference aShell, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x0600036F RID: 879
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getStringRole(uint aRole, HandleRef ret);

		// Token: 0x06000370 RID: 880
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getStringStates(uint aStates, uint aExtraStates, [MarshalAs(UnmanagedType.Interface)] out nsIDOMDOMStringList ret);

		// Token: 0x06000371 RID: 881
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getStringEventType(uint aEventType, HandleRef ret);

		// Token: 0x06000372 RID: 882
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getStringRelationType(uint aRelationType, HandleRef ret);
	}
}
