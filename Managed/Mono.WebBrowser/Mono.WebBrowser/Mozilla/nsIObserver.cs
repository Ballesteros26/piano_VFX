using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000F7 RID: 247
	[Guid("DB242E01-E4D9-11d2-9DDE-000064657374")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIObserver
	{
		// Token: 0x060007DA RID: 2010
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int observe([MarshalAs(UnmanagedType.Interface)] IntPtr aSubject, [MarshalAs(UnmanagedType.LPStr)] string aTopic, [MarshalAs(UnmanagedType.LPWStr)] string aData);
	}
}
