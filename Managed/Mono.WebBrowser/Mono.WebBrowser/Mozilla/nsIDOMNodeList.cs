using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000CB RID: 203
	[Guid("a6cf907d-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMNodeList
	{
		// Token: 0x060006B9 RID: 1721
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int item(uint index, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x060006BA RID: 1722
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLength(out uint ret);
	}
}
