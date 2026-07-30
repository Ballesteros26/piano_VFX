using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono.Mozilla
{
	// Token: 0x020000E9 RID: 233
	[Guid("e72f94b2-5f85-11d4-9877-00c04fa0cf4a")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIErrorService
	{
		// Token: 0x06000772 RID: 1906
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int registerErrorStringBundle(short errorModule, [MarshalAs(UnmanagedType.LPStr)] string stringBundleURL);

		// Token: 0x06000773 RID: 1907
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int unregisterErrorStringBundle(short errorModule);

		// Token: 0x06000774 RID: 1908
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getErrorStringBundle(short errorModule, [MarshalAs(UnmanagedType.LPStr)] ref string ret);

		// Token: 0x06000775 RID: 1909
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int registerErrorStringBundleKey(int error, [MarshalAs(UnmanagedType.LPStr)] string stringBundleKey);

		// Token: 0x06000776 RID: 1910
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int unregisterErrorStringBundleKey(int error);

		// Token: 0x06000777 RID: 1911
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getErrorStringBundleKey(int error, StringBuilder ret);
	}
}
