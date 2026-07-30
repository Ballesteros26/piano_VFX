using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000FD RID: 253
	[Guid("56c35506-f14b-11d3-99d3-ddbfac2ccf65")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIPrefBranch
	{
		// Token: 0x060007F2 RID: 2034
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getRoot(ref IntPtr ret);

		// Token: 0x060007F3 RID: 2035
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPrefType([MarshalAs(UnmanagedType.LPStr)] string aPrefName, out int ret);

		// Token: 0x060007F4 RID: 2036
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getBoolPref([MarshalAs(UnmanagedType.LPStr)] string aPrefName, out bool ret);

		// Token: 0x060007F5 RID: 2037
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setBoolPref([MarshalAs(UnmanagedType.LPStr)] string aPrefName, int aValue);

		// Token: 0x060007F6 RID: 2038
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCharPref([MarshalAs(UnmanagedType.LPStr)] string aPrefName, [MarshalAs(UnmanagedType.LPStr)] ref string ret);

		// Token: 0x060007F7 RID: 2039
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setCharPref([MarshalAs(UnmanagedType.LPStr)] string aPrefName, [MarshalAs(UnmanagedType.LPStr)] string aValue);

		// Token: 0x060007F8 RID: 2040
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getIntPref([MarshalAs(UnmanagedType.LPStr)] string aPrefName, out int ret);

		// Token: 0x060007F9 RID: 2041
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setIntPref([MarshalAs(UnmanagedType.LPStr)] string aPrefName, int aValue);

		// Token: 0x060007FA RID: 2042
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getComplexValue([MarshalAs(UnmanagedType.LPStr)] string aPrefName, [MarshalAs(UnmanagedType.LPStruct)] Guid aType, out IntPtr aValue);

		// Token: 0x060007FB RID: 2043
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setComplexValue([MarshalAs(UnmanagedType.LPStr)] string aPrefName, [MarshalAs(UnmanagedType.LPStruct)] Guid aType, IntPtr aValue);

		// Token: 0x060007FC RID: 2044
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int clearUserPref([MarshalAs(UnmanagedType.LPStr)] string aPrefName);

		// Token: 0x060007FD RID: 2045
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int lockPref([MarshalAs(UnmanagedType.LPStr)] string aPrefName);

		// Token: 0x060007FE RID: 2046
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int prefHasUserValue([MarshalAs(UnmanagedType.LPStr)] string aPrefName, out bool ret);

		// Token: 0x060007FF RID: 2047
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int prefIsLocked([MarshalAs(UnmanagedType.LPStr)] string aPrefName, out bool ret);

		// Token: 0x06000800 RID: 2048
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int unlockPref([MarshalAs(UnmanagedType.LPStr)] string aPrefName);

		// Token: 0x06000801 RID: 2049
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int deleteBranch([MarshalAs(UnmanagedType.LPStr)] string aStartingAt);

		// Token: 0x06000802 RID: 2050
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getChildList([MarshalAs(UnmanagedType.LPStr)] string aStartingAt, out uint aCount, [MarshalAs(UnmanagedType.LPStr)] out string[] aChildArray);

		// Token: 0x06000803 RID: 2051
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int resetBranch([MarshalAs(UnmanagedType.LPStr)] string aStartingAt);
	}
}
