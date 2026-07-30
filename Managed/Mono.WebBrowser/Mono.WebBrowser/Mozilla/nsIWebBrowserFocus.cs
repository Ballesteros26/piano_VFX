using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000125 RID: 293
	[Guid("9c5d3c58-1dd1-11b2-a1c9-f3699284657a")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIWebBrowserFocus
	{
		// Token: 0x060008B5 RID: 2229
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int activate();

		// Token: 0x060008B6 RID: 2230
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int deactivate();

		// Token: 0x060008B7 RID: 2231
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setFocusAtFirstElement();

		// Token: 0x060008B8 RID: 2232
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setFocusAtLastElement();

		// Token: 0x060008B9 RID: 2233
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getFocusedWindow([MarshalAs(UnmanagedType.Interface)] out nsIDOMWindow ret);

		// Token: 0x060008BA RID: 2234
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setFocusedWindow([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow value);

		// Token: 0x060008BB RID: 2235
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getFocusedElement([MarshalAs(UnmanagedType.Interface)] out nsIDOMElement ret);

		// Token: 0x060008BC RID: 2236
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setFocusedElement([MarshalAs(UnmanagedType.Interface)] nsIDOMElement value);
	}
}
