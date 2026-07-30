using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x0200007D RID: 125
	[Guid("F51EBADE-8B1A-11D3-AAE7-0010830123B4")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMAbstractView
	{
		// Token: 0x060003A4 RID: 932
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getDocument([MarshalAs(UnmanagedType.Interface)] out nsIDOMDocumentView ret);
	}
}
