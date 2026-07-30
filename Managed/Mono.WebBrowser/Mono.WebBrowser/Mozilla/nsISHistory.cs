using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000109 RID: 265
	[Guid("7294FE9B-14D8-11D5-9882-00C04FA02F40")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsISHistory
	{
		// Token: 0x0600082D RID: 2093
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCount(out int ret);

		// Token: 0x0600082E RID: 2094
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getIndex(out int ret);

		// Token: 0x0600082F RID: 2095
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getMaxLength(out int ret);

		// Token: 0x06000830 RID: 2096
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setMaxLength(int value);

		// Token: 0x06000831 RID: 2097
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getEntryAtIndex(int index, bool modifyIndex, [MarshalAs(UnmanagedType.Interface)] out nsIHistoryEntry ret);

		// Token: 0x06000832 RID: 2098
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int PurgeHistory(int numEntries);

		// Token: 0x06000833 RID: 2099
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int addSHistoryListener([MarshalAs(UnmanagedType.Interface)] nsISHistoryListener aListener);

		// Token: 0x06000834 RID: 2100
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int removeSHistoryListener([MarshalAs(UnmanagedType.Interface)] nsISHistoryListener aListener);

		// Token: 0x06000835 RID: 2101
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getSHistoryEnumerator([MarshalAs(UnmanagedType.Interface)] out nsISimpleEnumerator ret);
	}
}
