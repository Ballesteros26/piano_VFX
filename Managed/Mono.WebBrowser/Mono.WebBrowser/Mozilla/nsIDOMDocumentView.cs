using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000A7 RID: 167
	[Guid("1ACDB2BA-1DD2-11B2-95BC-9542495D2569")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMDocumentView
	{
		// Token: 0x060004EC RID: 1260
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getDefaultView([MarshalAs(UnmanagedType.Interface)] out nsIDOMAbstractView ret);
	}
}
