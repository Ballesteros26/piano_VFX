using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000121 RID: 289
	[Guid("BA434C60-9D52-11d3-AFB0-00A024FFC08C")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIWebBrowserChrome
	{
		// Token: 0x060008A5 RID: 2213
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setStatus(uint statusType, [MarshalAs(UnmanagedType.LPWStr)] string status);

		// Token: 0x060008A6 RID: 2214
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getWebBrowser([MarshalAs(UnmanagedType.Interface)] out nsIWebBrowser ret);

		// Token: 0x060008A7 RID: 2215
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setWebBrowser([MarshalAs(UnmanagedType.Interface)] nsIWebBrowser value);

		// Token: 0x060008A8 RID: 2216
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getChromeFlags(out uint ret);

		// Token: 0x060008A9 RID: 2217
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setChromeFlags(uint value);

		// Token: 0x060008AA RID: 2218
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int destroyBrowserWindow();

		// Token: 0x060008AB RID: 2219
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int sizeBrowserTo(int aCX, int aCY);

		// Token: 0x060008AC RID: 2220
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int showAsModal();

		// Token: 0x060008AD RID: 2221
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int isWindowModal(out bool ret);

		// Token: 0x060008AE RID: 2222
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int exitModalEventLoop(int aStatus);
	}
}
