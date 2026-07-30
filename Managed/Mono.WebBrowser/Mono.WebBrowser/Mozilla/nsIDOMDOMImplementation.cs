using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000097 RID: 151
	[Guid("a6cf9074-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMDOMImplementation
	{
		// Token: 0x06000471 RID: 1137
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int hasFeature(HandleRef feature, HandleRef version, out bool ret);

		// Token: 0x06000472 RID: 1138
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createDocumentType(HandleRef qualifiedName, HandleRef publicId, HandleRef systemId, [MarshalAs(UnmanagedType.Interface)] out nsIDOMDocumentType ret);

		// Token: 0x06000473 RID: 1139
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createDocument(HandleRef namespaceURI, HandleRef qualifiedName, [MarshalAs(UnmanagedType.Interface)] nsIDOMDocumentType doctype, [MarshalAs(UnmanagedType.Interface)] out nsIDOMDocument ret);
	}
}
