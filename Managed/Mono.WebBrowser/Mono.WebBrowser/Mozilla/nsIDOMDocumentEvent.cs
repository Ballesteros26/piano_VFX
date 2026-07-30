using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x0200009D RID: 157
	[Guid("46b91d66-28e2-11d4-ab1e-0010830123b4")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMDocumentEvent
	{
		// Token: 0x060004A7 RID: 1191
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createEvent(HandleRef eventType, [MarshalAs(UnmanagedType.Interface)] out nsIDOMEvent ret);
	}
}
