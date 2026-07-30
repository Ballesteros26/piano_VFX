using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000A9 RID: 169
	[Guid("a6cf9078-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMElement : nsIDOMNode
	{
		// Token: 0x060004EF RID: 1263
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNodeName(HandleRef ret);

		// Token: 0x060004F0 RID: 1264
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNodeValue(HandleRef ret);

		// Token: 0x060004F1 RID: 1265
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setNodeValue(HandleRef value);

		// Token: 0x060004F2 RID: 1266
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNodeType(out ushort ret);

		// Token: 0x060004F3 RID: 1267
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getParentNode([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060004F4 RID: 1268
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getChildNodes([MarshalAs(UnmanagedType.Interface)] out nsIDOMNodeList ret);

		// Token: 0x060004F5 RID: 1269
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getFirstChild([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060004F6 RID: 1270
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLastChild([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060004F7 RID: 1271
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPreviousSibling([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060004F8 RID: 1272
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNextSibling([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060004F9 RID: 1273
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAttributes([MarshalAs(UnmanagedType.Interface)] out nsIDOMNamedNodeMap ret);

		// Token: 0x060004FA RID: 1274
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getOwnerDocument([MarshalAs(UnmanagedType.Interface)] out nsIDOMDocument ret);

		// Token: 0x060004FB RID: 1275
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int insertBefore([MarshalAs(UnmanagedType.Interface)] nsIDOMNode newChild, [MarshalAs(UnmanagedType.Interface)] nsIDOMNode refChild, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060004FC RID: 1276
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int replaceChild([MarshalAs(UnmanagedType.Interface)] nsIDOMNode newChild, [MarshalAs(UnmanagedType.Interface)] nsIDOMNode oldChild, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060004FD RID: 1277
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int removeChild([MarshalAs(UnmanagedType.Interface)] nsIDOMNode oldChild, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060004FE RID: 1278
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int appendChild([MarshalAs(UnmanagedType.Interface)] nsIDOMNode newChild, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060004FF RID: 1279
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int hasChildNodes(out bool ret);

		// Token: 0x06000500 RID: 1280
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int cloneNode(bool deep, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x06000501 RID: 1281
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int normalize();

		// Token: 0x06000502 RID: 1282
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int isSupported(HandleRef feature, HandleRef version, out bool ret);

		// Token: 0x06000503 RID: 1283
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNamespaceURI(HandleRef ret);

		// Token: 0x06000504 RID: 1284
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPrefix(HandleRef ret);

		// Token: 0x06000505 RID: 1285
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setPrefix(HandleRef value);

		// Token: 0x06000506 RID: 1286
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLocalName(HandleRef ret);

		// Token: 0x06000507 RID: 1287
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int hasAttributes(out bool ret);

		// Token: 0x06000508 RID: 1288
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTagName(HandleRef ret);

		// Token: 0x06000509 RID: 1289
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAttribute(HandleRef name, HandleRef ret);

		// Token: 0x0600050A RID: 1290
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setAttribute(HandleRef name, HandleRef value);

		// Token: 0x0600050B RID: 1291
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int removeAttribute(HandleRef name);

		// Token: 0x0600050C RID: 1292
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAttributeNode(HandleRef name, [MarshalAs(UnmanagedType.Interface)] out nsIDOMAttr ret);

		// Token: 0x0600050D RID: 1293
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setAttributeNode([MarshalAs(UnmanagedType.Interface)] nsIDOMAttr newAttr, [MarshalAs(UnmanagedType.Interface)] out nsIDOMAttr ret);

		// Token: 0x0600050E RID: 1294
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int removeAttributeNode([MarshalAs(UnmanagedType.Interface)] nsIDOMAttr oldAttr, [MarshalAs(UnmanagedType.Interface)] out nsIDOMAttr ret);

		// Token: 0x0600050F RID: 1295
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getElementsByTagName(HandleRef name, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNodeList ret);

		// Token: 0x06000510 RID: 1296
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAttributeNS(HandleRef namespaceURI, HandleRef localName, HandleRef ret);

		// Token: 0x06000511 RID: 1297
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setAttributeNS(HandleRef namespaceURI, HandleRef qualifiedName, HandleRef value);

		// Token: 0x06000512 RID: 1298
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int removeAttributeNS(HandleRef namespaceURI, HandleRef localName);

		// Token: 0x06000513 RID: 1299
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAttributeNodeNS(HandleRef namespaceURI, HandleRef localName, [MarshalAs(UnmanagedType.Interface)] out nsIDOMAttr ret);

		// Token: 0x06000514 RID: 1300
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setAttributeNodeNS([MarshalAs(UnmanagedType.Interface)] nsIDOMAttr newAttr, [MarshalAs(UnmanagedType.Interface)] out nsIDOMAttr ret);

		// Token: 0x06000515 RID: 1301
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getElementsByTagNameNS(HandleRef namespaceURI, HandleRef localName, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNodeList ret);

		// Token: 0x06000516 RID: 1302
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int hasAttribute(HandleRef name, out bool ret);

		// Token: 0x06000517 RID: 1303
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int hasAttributeNS(HandleRef namespaceURI, HandleRef localName, out bool ret);
	}
}
