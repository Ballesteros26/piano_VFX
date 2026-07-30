using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000B3 RID: 179
	[Guid("a6cf908e-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMHTMLBodyElement : nsIDOMHTMLElement, nsIDOMElement, nsIDOMNode
	{
		// Token: 0x06000549 RID: 1353
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNodeName(HandleRef ret);

		// Token: 0x0600054A RID: 1354
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNodeValue(HandleRef ret);

		// Token: 0x0600054B RID: 1355
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setNodeValue(HandleRef value);

		// Token: 0x0600054C RID: 1356
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNodeType(out ushort ret);

		// Token: 0x0600054D RID: 1357
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getParentNode([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x0600054E RID: 1358
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getChildNodes([MarshalAs(UnmanagedType.Interface)] out nsIDOMNodeList ret);

		// Token: 0x0600054F RID: 1359
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getFirstChild([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x06000550 RID: 1360
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLastChild([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x06000551 RID: 1361
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPreviousSibling([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x06000552 RID: 1362
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNextSibling([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x06000553 RID: 1363
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAttributes([MarshalAs(UnmanagedType.Interface)] out nsIDOMNamedNodeMap ret);

		// Token: 0x06000554 RID: 1364
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getOwnerDocument([MarshalAs(UnmanagedType.Interface)] out nsIDOMDocument ret);

		// Token: 0x06000555 RID: 1365
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int insertBefore([MarshalAs(UnmanagedType.Interface)] nsIDOMNode newChild, [MarshalAs(UnmanagedType.Interface)] nsIDOMNode refChild, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x06000556 RID: 1366
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int replaceChild([MarshalAs(UnmanagedType.Interface)] nsIDOMNode newChild, [MarshalAs(UnmanagedType.Interface)] nsIDOMNode oldChild, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x06000557 RID: 1367
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int removeChild([MarshalAs(UnmanagedType.Interface)] nsIDOMNode oldChild, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x06000558 RID: 1368
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int appendChild([MarshalAs(UnmanagedType.Interface)] nsIDOMNode newChild, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x06000559 RID: 1369
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int hasChildNodes(out bool ret);

		// Token: 0x0600055A RID: 1370
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int cloneNode(bool deep, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x0600055B RID: 1371
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int normalize();

		// Token: 0x0600055C RID: 1372
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int isSupported(HandleRef feature, HandleRef version, out bool ret);

		// Token: 0x0600055D RID: 1373
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNamespaceURI(HandleRef ret);

		// Token: 0x0600055E RID: 1374
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPrefix(HandleRef ret);

		// Token: 0x0600055F RID: 1375
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setPrefix(HandleRef value);

		// Token: 0x06000560 RID: 1376
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLocalName(HandleRef ret);

		// Token: 0x06000561 RID: 1377
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int hasAttributes(out bool ret);

		// Token: 0x06000562 RID: 1378
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTagName(HandleRef ret);

		// Token: 0x06000563 RID: 1379
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAttribute(HandleRef name, HandleRef ret);

		// Token: 0x06000564 RID: 1380
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setAttribute(HandleRef name, HandleRef value);

		// Token: 0x06000565 RID: 1381
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int removeAttribute(HandleRef name);

		// Token: 0x06000566 RID: 1382
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAttributeNode(HandleRef name, [MarshalAs(UnmanagedType.Interface)] out nsIDOMAttr ret);

		// Token: 0x06000567 RID: 1383
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setAttributeNode([MarshalAs(UnmanagedType.Interface)] nsIDOMAttr newAttr, [MarshalAs(UnmanagedType.Interface)] out nsIDOMAttr ret);

		// Token: 0x06000568 RID: 1384
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int removeAttributeNode([MarshalAs(UnmanagedType.Interface)] nsIDOMAttr oldAttr, [MarshalAs(UnmanagedType.Interface)] out nsIDOMAttr ret);

		// Token: 0x06000569 RID: 1385
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getElementsByTagName(HandleRef name, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNodeList ret);

		// Token: 0x0600056A RID: 1386
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAttributeNS(HandleRef namespaceURI, HandleRef localName, HandleRef ret);

		// Token: 0x0600056B RID: 1387
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setAttributeNS(HandleRef namespaceURI, HandleRef qualifiedName, HandleRef value);

		// Token: 0x0600056C RID: 1388
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int removeAttributeNS(HandleRef namespaceURI, HandleRef localName);

		// Token: 0x0600056D RID: 1389
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAttributeNodeNS(HandleRef namespaceURI, HandleRef localName, [MarshalAs(UnmanagedType.Interface)] out nsIDOMAttr ret);

		// Token: 0x0600056E RID: 1390
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setAttributeNodeNS([MarshalAs(UnmanagedType.Interface)] nsIDOMAttr newAttr, [MarshalAs(UnmanagedType.Interface)] out nsIDOMAttr ret);

		// Token: 0x0600056F RID: 1391
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getElementsByTagNameNS(HandleRef namespaceURI, HandleRef localName, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNodeList ret);

		// Token: 0x06000570 RID: 1392
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int hasAttribute(HandleRef name, out bool ret);

		// Token: 0x06000571 RID: 1393
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int hasAttributeNS(HandleRef namespaceURI, HandleRef localName, out bool ret);

		// Token: 0x06000572 RID: 1394
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getId(HandleRef ret);

		// Token: 0x06000573 RID: 1395
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setId(HandleRef value);

		// Token: 0x06000574 RID: 1396
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTitle(HandleRef ret);

		// Token: 0x06000575 RID: 1397
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setTitle(HandleRef value);

		// Token: 0x06000576 RID: 1398
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLang(HandleRef ret);

		// Token: 0x06000577 RID: 1399
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setLang(HandleRef value);

		// Token: 0x06000578 RID: 1400
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getDir(HandleRef ret);

		// Token: 0x06000579 RID: 1401
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setDir(HandleRef value);

		// Token: 0x0600057A RID: 1402
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getClassName(HandleRef ret);

		// Token: 0x0600057B RID: 1403
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setClassName(HandleRef value);

		// Token: 0x0600057C RID: 1404
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getALink(HandleRef ret);

		// Token: 0x0600057D RID: 1405
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setALink(HandleRef value);

		// Token: 0x0600057E RID: 1406
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getBackground(HandleRef ret);

		// Token: 0x0600057F RID: 1407
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setBackground(HandleRef value);

		// Token: 0x06000580 RID: 1408
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getBgColor(HandleRef ret);

		// Token: 0x06000581 RID: 1409
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setBgColor(HandleRef value);

		// Token: 0x06000582 RID: 1410
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLink(HandleRef ret);

		// Token: 0x06000583 RID: 1411
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setLink(HandleRef value);

		// Token: 0x06000584 RID: 1412
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getText(HandleRef ret);

		// Token: 0x06000585 RID: 1413
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setText(HandleRef value);

		// Token: 0x06000586 RID: 1414
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getVLink(HandleRef ret);

		// Token: 0x06000587 RID: 1415
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setVLink(HandleRef value);
	}
}
