using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000A1 RID: 161
	[Guid("7b9badc6-c9bc-447a-8670-dbd195aed24b")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMDocumentRange
	{
		// Token: 0x060004C5 RID: 1221
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createRange([MarshalAs(UnmanagedType.Interface)] out nsIDOMRange ret);
	}
}
