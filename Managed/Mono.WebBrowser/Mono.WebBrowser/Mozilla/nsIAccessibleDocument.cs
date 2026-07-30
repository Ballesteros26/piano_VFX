using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x0200006F RID: 111
	[Guid("b7ae45bd-21e9-4ed5-a67e-86448b25d56b")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIAccessibleDocument
	{
		// Token: 0x06000355 RID: 853
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getURL(HandleRef ret);

		// Token: 0x06000356 RID: 854
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTitle(HandleRef ret);

		// Token: 0x06000357 RID: 855
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getMimeType(HandleRef ret);

		// Token: 0x06000358 RID: 856
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getDocType(HandleRef ret);

		// Token: 0x06000359 RID: 857
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getDocument([MarshalAs(UnmanagedType.Interface)] out nsIDOMDocument ret);

		// Token: 0x0600035A RID: 858
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getWindow([MarshalAs(UnmanagedType.Interface)] out nsIDOMWindow ret);

		// Token: 0x0600035B RID: 859
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNameSpaceURIForID(short nameSpaceID, HandleRef ret);

		// Token: 0x0600035C RID: 860
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getWindowHandle(IntPtr ret);

		// Token: 0x0600035D RID: 861
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCachedAccessNode(IntPtr aUniqueID, [MarshalAs(UnmanagedType.Interface)] out nsIAccessNode ret);

		// Token: 0x0600035E RID: 862
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAccessibleInParentChain([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aDOMNode, bool aCanCreate, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);
	}
}
