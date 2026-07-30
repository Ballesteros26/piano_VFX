using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x0200011D RID: 285
	[Guid("9188bc85-f92e-11d2-81ef-0060083a0bcf")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIWeakReference
	{
		// Token: 0x06000899 RID: 2201
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int QueryReferent([MarshalAs(UnmanagedType.LPStruct)] Guid uuid, out IntPtr result);
	}
}
