using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x0200007F RID: 127
	[Guid("a6cf9070-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMAttr : nsIDOMNode
	{
		// Token: 0x060003A7 RID: 935
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNodeName(HandleRef ret);

		// Token: 0x060003A8 RID: 936
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNodeValue(HandleRef ret);

		// Token: 0x060003A9 RID: 937
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setNodeValue(HandleRef value);

		// Token: 0x060003AA RID: 938
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNodeType(out ushort ret);

		// Token: 0x060003AB RID: 939
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getParentNode([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060003AC RID: 940
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getChildNodes([MarshalAs(UnmanagedType.Interface)] out nsIDOMNodeList ret);

		// Token: 0x060003AD RID: 941
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getFirstChild([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060003AE RID: 942
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLastChild([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060003AF RID: 943
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPreviousSibling([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060003B0 RID: 944
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNextSibling([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060003B1 RID: 945
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAttributes([MarshalAs(UnmanagedType.Interface)] out nsIDOMNamedNodeMap ret);

		// Token: 0x060003B2 RID: 946
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getOwnerDocument([MarshalAs(UnmanagedType.Interface)] out nsIDOMDocument ret);

		// Token: 0x060003B3 RID: 947
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int insertBefore([MarshalAs(UnmanagedType.Interface)] nsIDOMNode newChild, [MarshalAs(UnmanagedType.Interface)] nsIDOMNode refChild, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060003B4 RID: 948
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int replaceChild([MarshalAs(UnmanagedType.Interface)] nsIDOMNode newChild, [MarshalAs(UnmanagedType.Interface)] nsIDOMNode oldChild, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060003B5 RID: 949
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int removeChild([MarshalAs(UnmanagedType.Interface)] nsIDOMNode oldChild, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060003B6 RID: 950
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int appendChild([MarshalAs(UnmanagedType.Interface)] nsIDOMNode newChild, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060003B7 RID: 951
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int hasChildNodes(out bool ret);

		// Token: 0x060003B8 RID: 952
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int cloneNode(bool deep, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060003B9 RID: 953
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int normalize();

		// Token: 0x060003BA RID: 954
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int isSupported(HandleRef feature, HandleRef version, out bool ret);

		// Token: 0x060003BB RID: 955
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNamespaceURI(HandleRef ret);

		// Token: 0x060003BC RID: 956
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPrefix(HandleRef ret);

		// Token: 0x060003BD RID: 957
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setPrefix(HandleRef value);

		// Token: 0x060003BE RID: 958
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLocalName(HandleRef ret);

		// Token: 0x060003BF RID: 959
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int hasAttributes(out bool ret);

		// Token: 0x060003C0 RID: 960
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getName(HandleRef ret);

		// Token: 0x060003C1 RID: 961
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getSpecified(out bool ret);

		// Token: 0x060003C2 RID: 962
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getValue(HandleRef ret);

		// Token: 0x060003C3 RID: 963
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setValue(HandleRef value);

		// Token: 0x060003C4 RID: 964
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getOwnerElement([MarshalAs(UnmanagedType.Interface)] out nsIDOMElement ret);
	}
}
