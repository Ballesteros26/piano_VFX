using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000DD RID: 221
	[Guid("0b9341f3-95d4-4fa4-adcd-e119e0db2889")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMViewCSS : nsIDOMAbstractView
	{
		// Token: 0x06000741 RID: 1857
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getDocument([MarshalAs(UnmanagedType.Interface)] out nsIDOMDocumentView ret);

		// Token: 0x06000742 RID: 1858
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getComputedStyle([MarshalAs(UnmanagedType.Interface)] nsIDOMElement elt, HandleRef pseudoElt, [MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSStyleDeclaration ret);
	}
}
