using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000083 RID: 131
	[Guid("a6cf9071-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMCDATASection : nsIDOMText, nsIDOMCharacterData, nsIDOMNode
	{
		// Token: 0x060003CB RID: 971
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNodeName(HandleRef ret);

		// Token: 0x060003CC RID: 972
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNodeValue(HandleRef ret);

		// Token: 0x060003CD RID: 973
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setNodeValue(HandleRef value);

		// Token: 0x060003CE RID: 974
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNodeType(out ushort ret);

		// Token: 0x060003CF RID: 975
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getParentNode([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060003D0 RID: 976
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getChildNodes([MarshalAs(UnmanagedType.Interface)] out nsIDOMNodeList ret);

		// Token: 0x060003D1 RID: 977
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getFirstChild([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060003D2 RID: 978
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLastChild([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060003D3 RID: 979
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPreviousSibling([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060003D4 RID: 980
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNextSibling([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060003D5 RID: 981
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAttributes([MarshalAs(UnmanagedType.Interface)] out nsIDOMNamedNodeMap ret);

		// Token: 0x060003D6 RID: 982
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getOwnerDocument([MarshalAs(UnmanagedType.Interface)] out nsIDOMDocument ret);

		// Token: 0x060003D7 RID: 983
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int insertBefore([MarshalAs(UnmanagedType.Interface)] nsIDOMNode newChild, [MarshalAs(UnmanagedType.Interface)] nsIDOMNode refChild, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060003D8 RID: 984
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int replaceChild([MarshalAs(UnmanagedType.Interface)] nsIDOMNode newChild, [MarshalAs(UnmanagedType.Interface)] nsIDOMNode oldChild, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060003D9 RID: 985
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int removeChild([MarshalAs(UnmanagedType.Interface)] nsIDOMNode oldChild, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060003DA RID: 986
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int appendChild([MarshalAs(UnmanagedType.Interface)] nsIDOMNode newChild, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060003DB RID: 987
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int hasChildNodes(out bool ret);

		// Token: 0x060003DC RID: 988
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int cloneNode(bool deep, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060003DD RID: 989
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int normalize();

		// Token: 0x060003DE RID: 990
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int isSupported(HandleRef feature, HandleRef version, out bool ret);

		// Token: 0x060003DF RID: 991
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNamespaceURI(HandleRef ret);

		// Token: 0x060003E0 RID: 992
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPrefix(HandleRef ret);

		// Token: 0x060003E1 RID: 993
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setPrefix(HandleRef value);

		// Token: 0x060003E2 RID: 994
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLocalName(HandleRef ret);

		// Token: 0x060003E3 RID: 995
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int hasAttributes(out bool ret);

		// Token: 0x060003E4 RID: 996
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getData(HandleRef ret);

		// Token: 0x060003E5 RID: 997
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setData(HandleRef value);

		// Token: 0x060003E6 RID: 998
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLength(out uint ret);

		// Token: 0x060003E7 RID: 999
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int substringData(uint offset, uint count, HandleRef ret);

		// Token: 0x060003E8 RID: 1000
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int appendData(HandleRef arg);

		// Token: 0x060003E9 RID: 1001
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int insertData(uint offset, HandleRef arg);

		// Token: 0x060003EA RID: 1002
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int deleteData(uint offset, uint count);

		// Token: 0x060003EB RID: 1003
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int replaceData(uint offset, uint count, HandleRef arg);

		// Token: 0x060003EC RID: 1004
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int splitText(uint offset, [MarshalAs(UnmanagedType.Interface)] out nsIDOMText ret);
	}
}
