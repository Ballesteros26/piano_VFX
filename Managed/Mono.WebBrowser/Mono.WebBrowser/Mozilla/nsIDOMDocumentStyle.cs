using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000A3 RID: 163
	[Guid("3d9f4973-dd2e-48f5-b5f7-2634e09eadd9")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMDocumentStyle
	{
		// Token: 0x060004C8 RID: 1224
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getStyleSheets([MarshalAs(UnmanagedType.Interface)] out nsIDOMStyleSheetList ret);
	}
}
