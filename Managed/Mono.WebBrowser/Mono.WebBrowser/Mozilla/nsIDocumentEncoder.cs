using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000E5 RID: 229
	[Guid("f85c5a20-258d-11db-a98b-0800200c9a66")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDocumentEncoder
	{
		// Token: 0x06000761 RID: 1889
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void init([MarshalAs(UnmanagedType.Interface)] nsIDOMDocument aDocument, HandleRef aMimeType, uint aFlags);

		// Token: 0x06000762 RID: 1890
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void setSelection([MarshalAs(UnmanagedType.Interface)] nsISelection aSelection);

		// Token: 0x06000763 RID: 1891
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void setRange([MarshalAs(UnmanagedType.Interface)] nsIDOMRange aRange);

		// Token: 0x06000764 RID: 1892
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void setNode([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode);

		// Token: 0x06000765 RID: 1893
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void setContainerNode([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aContainer);

		// Token: 0x06000766 RID: 1894
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void setCharset(HandleRef aCharset);

		// Token: 0x06000767 RID: 1895
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void setWrapColumn(uint aWrapColumn);

		// Token: 0x06000768 RID: 1896
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getMimeType(HandleRef ret);

		// Token: 0x06000769 RID: 1897
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void encodeToStream([MarshalAs(UnmanagedType.Interface)] nsIOutputStream aStream);

		// Token: 0x0600076A RID: 1898
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int encodeToString(HandleRef ret);

		// Token: 0x0600076B RID: 1899
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int encodeToStringWithContext(HandleRef aContextString, HandleRef aInfoString, HandleRef ret);

		// Token: 0x0600076C RID: 1900
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void setNodeFixup([MarshalAs(UnmanagedType.Interface)] nsIDocumentEncoderNodeFixup aFixup);
	}
}
