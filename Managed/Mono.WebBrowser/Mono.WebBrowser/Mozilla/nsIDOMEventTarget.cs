using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000B1 RID: 177
	[Guid("1c773b30-d1cf-11d2-bd95-00805f8ae3f4")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMEventTarget
	{
		// Token: 0x06000544 RID: 1348
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int addEventListener(HandleRef type, [MarshalAs(UnmanagedType.Interface)] nsIDOMEventListener listener, bool useCapture);

		// Token: 0x06000545 RID: 1349
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int removeEventListener(HandleRef type, [MarshalAs(UnmanagedType.Interface)] nsIDOMEventListener listener, bool useCapture);

		// Token: 0x06000546 RID: 1350
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int dispatchEvent([MarshalAs(UnmanagedType.Interface)] nsIDOMEvent evt, out bool ret);
	}
}
