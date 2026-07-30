using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000069 RID: 105
	[Guid("71a3b4e7-e83d-45cf-a20e-9ce292bcf19f")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIAccessNode
	{
		// Token: 0x060002EA RID: 746
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getDOMNode([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060002EB RID: 747
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNumChildren(out int ret);

		// Token: 0x060002EC RID: 748
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getChildNodeAt(int childNum, [MarshalAs(UnmanagedType.Interface)] out nsIAccessNode ret);

		// Token: 0x060002ED RID: 749
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getParentNode([MarshalAs(UnmanagedType.Interface)] out nsIAccessNode ret);

		// Token: 0x060002EE RID: 750
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getFirstChildNode([MarshalAs(UnmanagedType.Interface)] out nsIAccessNode ret);

		// Token: 0x060002EF RID: 751
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLastChildNode([MarshalAs(UnmanagedType.Interface)] out nsIAccessNode ret);

		// Token: 0x060002F0 RID: 752
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPreviousSiblingNode([MarshalAs(UnmanagedType.Interface)] out nsIAccessNode ret);

		// Token: 0x060002F1 RID: 753
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNextSiblingNode([MarshalAs(UnmanagedType.Interface)] out nsIAccessNode ret);

		// Token: 0x060002F2 RID: 754
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAccessibleDocument([MarshalAs(UnmanagedType.Interface)] out nsIAccessibleDocument ret);

		// Token: 0x060002F3 RID: 755
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getInnerHTML(HandleRef ret);

		// Token: 0x060002F4 RID: 756
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int scrollTo(uint aScrollType);

		// Token: 0x060002F5 RID: 757
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int scrollToPoint(uint aCoordinateType, int aX, int aY);

		// Token: 0x060002F6 RID: 758
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getOwnerWindow(IntPtr ret);

		// Token: 0x060002F7 RID: 759
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getUniqueID(IntPtr ret);

		// Token: 0x060002F8 RID: 760
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getComputedStyleValue(HandleRef pseudoElt, HandleRef propertyName, HandleRef ret);

		// Token: 0x060002F9 RID: 761
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getComputedStyleCSSValue(HandleRef pseudoElt, HandleRef propertyName, [MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSPrimitiveValue ret);

		// Token: 0x060002FA RID: 762
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLanguage(HandleRef ret);
	}
}
