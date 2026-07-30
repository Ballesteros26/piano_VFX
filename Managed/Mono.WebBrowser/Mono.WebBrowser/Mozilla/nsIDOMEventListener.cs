using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000AF RID: 175
	[Guid("df31c120-ded6-11d1-bd85-00805f8ae3f4")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMEventListener
	{
		// Token: 0x06000541 RID: 1345
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int handleEvent([MarshalAs(UnmanagedType.Interface)] nsIDOMEvent _event);
	}
}
