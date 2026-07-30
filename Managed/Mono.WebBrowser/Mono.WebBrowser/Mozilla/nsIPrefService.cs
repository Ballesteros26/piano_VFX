using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000FF RID: 255
	[Guid("decb9cc7-c08f-4ea5-be91-a8fc637ce2d2")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIPrefService
	{
		// Token: 0x06000806 RID: 2054
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int readUserPrefs([MarshalAs(UnmanagedType.Interface)] nsIFile aFile);

		// Token: 0x06000807 RID: 2055
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int resetPrefs();

		// Token: 0x06000808 RID: 2056
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int resetUserPrefs();

		// Token: 0x06000809 RID: 2057
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int savePrefFile([MarshalAs(UnmanagedType.Interface)] nsIFile aFile);

		// Token: 0x0600080A RID: 2058
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getBranch([MarshalAs(UnmanagedType.LPStr)] string aPrefRoot, [MarshalAs(UnmanagedType.Interface)] out nsIPrefBranch ret);

		// Token: 0x0600080B RID: 2059
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getDefaultBranch([MarshalAs(UnmanagedType.LPStr)] string aPrefRoot, [MarshalAs(UnmanagedType.Interface)] out nsIPrefBranch ret);
	}
}
