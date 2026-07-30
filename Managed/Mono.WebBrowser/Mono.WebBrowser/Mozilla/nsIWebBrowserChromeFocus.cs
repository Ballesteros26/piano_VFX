using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000123 RID: 291
	[Guid("d2206418-1dd1-11b2-8e55-acddcd2bcfb8")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIWebBrowserChromeFocus
	{
		// Token: 0x060008B1 RID: 2225
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int focusNextElement();

		// Token: 0x060008B2 RID: 2226
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int focusPrevElement();
	}
}
