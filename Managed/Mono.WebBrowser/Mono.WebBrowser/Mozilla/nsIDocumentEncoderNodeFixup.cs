using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000E7 RID: 231
	[Guid("e770c650-b3d3-11da-a94d-0800200c9a66")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDocumentEncoderNodeFixup
	{
		// Token: 0x0600076F RID: 1903
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[return: MarshalAs(UnmanagedType.Interface)]
		nsIDOMNode fixupNode([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode);
	}
}
