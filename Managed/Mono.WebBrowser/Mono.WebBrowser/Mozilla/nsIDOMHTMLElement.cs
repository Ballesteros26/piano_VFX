using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000B9 RID: 185
	[Guid("a6cf9085-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMHTMLElement : nsIDOMElement, nsIDOMNode
	{
		// Token: 0x060005CE RID: 1486
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNodeName(HandleRef ret);

		// Token: 0x060005CF RID: 1487
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNodeValue(HandleRef ret);

		// Token: 0x060005D0 RID: 1488
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setNodeValue(HandleRef value);

		// Token: 0x060005D1 RID: 1489
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNodeType(out ushort ret);

		// Token: 0x060005D2 RID: 1490
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getParentNode([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060005D3 RID: 1491
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getChildNodes([MarshalAs(UnmanagedType.Interface)] out nsIDOMNodeList ret);

		// Token: 0x060005D4 RID: 1492
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getFirstChild([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060005D5 RID: 1493
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLastChild([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060005D6 RID: 1494
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPreviousSibling([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060005D7 RID: 1495
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNextSibling([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060005D8 RID: 1496
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAttributes([MarshalAs(UnmanagedType.Interface)] out nsIDOMNamedNodeMap ret);

		// Token: 0x060005D9 RID: 1497
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getOwnerDocument([MarshalAs(UnmanagedType.Interface)] out nsIDOMDocument ret);

		// Token: 0x060005DA RID: 1498
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int insertBefore([MarshalAs(UnmanagedType.Interface)] nsIDOMNode newChild, [MarshalAs(UnmanagedType.Interface)] nsIDOMNode refChild, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060005DB RID: 1499
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int replaceChild([MarshalAs(UnmanagedType.Interface)] nsIDOMNode newChild, [MarshalAs(UnmanagedType.Interface)] nsIDOMNode oldChild, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060005DC RID: 1500
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int removeChild([MarshalAs(UnmanagedType.Interface)] nsIDOMNode oldChild, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060005DD RID: 1501
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int appendChild([MarshalAs(UnmanagedType.Interface)] nsIDOMNode newChild, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060005DE RID: 1502
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int hasChildNodes(out bool ret);

		// Token: 0x060005DF RID: 1503
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int cloneNode(bool deep, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060005E0 RID: 1504
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int normalize();

		// Token: 0x060005E1 RID: 1505
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int isSupported(HandleRef feature, HandleRef version, out bool ret);

		// Token: 0x060005E2 RID: 1506
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNamespaceURI(HandleRef ret);

		// Token: 0x060005E3 RID: 1507
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPrefix(HandleRef ret);

		// Token: 0x060005E4 RID: 1508
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setPrefix(HandleRef value);

		// Token: 0x060005E5 RID: 1509
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLocalName(HandleRef ret);

		// Token: 0x060005E6 RID: 1510
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int hasAttributes(out bool ret);

		// Token: 0x060005E7 RID: 1511
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTagName(HandleRef ret);

		// Token: 0x060005E8 RID: 1512
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAttribute(HandleRef name, HandleRef ret);

		// Token: 0x060005E9 RID: 1513
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setAttribute(HandleRef name, HandleRef value);

		// Token: 0x060005EA RID: 1514
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int removeAttribute(HandleRef name);

		// Token: 0x060005EB RID: 1515
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAttributeNode(HandleRef name, [MarshalAs(UnmanagedType.Interface)] out nsIDOMAttr ret);

		// Token: 0x060005EC RID: 1516
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setAttributeNode([MarshalAs(UnmanagedType.Interface)] nsIDOMAttr newAttr, [MarshalAs(UnmanagedType.Interface)] out nsIDOMAttr ret);

		// Token: 0x060005ED RID: 1517
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int removeAttributeNode([MarshalAs(UnmanagedType.Interface)] nsIDOMAttr oldAttr, [MarshalAs(UnmanagedType.Interface)] out nsIDOMAttr ret);

		// Token: 0x060005EE RID: 1518
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getElementsByTagName(HandleRef name, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNodeList ret);

		// Token: 0x060005EF RID: 1519
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAttributeNS(HandleRef namespaceURI, HandleRef localName, HandleRef ret);

		// Token: 0x060005F0 RID: 1520
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setAttributeNS(HandleRef namespaceURI, HandleRef qualifiedName, HandleRef value);

		// Token: 0x060005F1 RID: 1521
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int removeAttributeNS(HandleRef namespaceURI, HandleRef localName);

		// Token: 0x060005F2 RID: 1522
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAttributeNodeNS(HandleRef namespaceURI, HandleRef localName, [MarshalAs(UnmanagedType.Interface)] out nsIDOMAttr ret);

		// Token: 0x060005F3 RID: 1523
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setAttributeNodeNS([MarshalAs(UnmanagedType.Interface)] nsIDOMAttr newAttr, [MarshalAs(UnmanagedType.Interface)] out nsIDOMAttr ret);

		// Token: 0x060005F4 RID: 1524
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getElementsByTagNameNS(HandleRef namespaceURI, HandleRef localName, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNodeList ret);

		// Token: 0x060005F5 RID: 1525
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int hasAttribute(HandleRef name, out bool ret);

		// Token: 0x060005F6 RID: 1526
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int hasAttributeNS(HandleRef namespaceURI, HandleRef localName, out bool ret);

		// Token: 0x060005F7 RID: 1527
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getId(HandleRef ret);

		// Token: 0x060005F8 RID: 1528
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setId(HandleRef value);

		// Token: 0x060005F9 RID: 1529
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTitle(HandleRef ret);

		// Token: 0x060005FA RID: 1530
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setTitle(HandleRef value);

		// Token: 0x060005FB RID: 1531
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLang(HandleRef ret);

		// Token: 0x060005FC RID: 1532
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setLang(HandleRef value);

		// Token: 0x060005FD RID: 1533
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getDir(HandleRef ret);

		// Token: 0x060005FE RID: 1534
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setDir(HandleRef value);

		// Token: 0x060005FF RID: 1535
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getClassName(HandleRef ret);

		// Token: 0x06000600 RID: 1536
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setClassName(HandleRef value);
	}
}
