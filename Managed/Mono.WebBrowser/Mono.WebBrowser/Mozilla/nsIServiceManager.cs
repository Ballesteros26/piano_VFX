using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x0200010F RID: 271
	[Guid("8bb35ed9-e332-462d-9155-4a002ab5c958")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIServiceManager
	{
		// Token: 0x06000855 RID: 2133
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		IntPtr getService([MarshalAs(UnmanagedType.LPStruct)] Guid aClass, [MarshalAs(UnmanagedType.LPStruct)] Guid aIID);

		// Token: 0x06000856 RID: 2134
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getServiceByContractID([MarshalAs(UnmanagedType.LPStr)] string aContractID, [MarshalAs(UnmanagedType.LPStruct)] Guid aIID, out IntPtr ret);

		// Token: 0x06000857 RID: 2135
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		bool isServiceInstantiated([MarshalAs(UnmanagedType.LPStruct)] Guid aClass, [MarshalAs(UnmanagedType.LPStruct)] Guid aIID);

		// Token: 0x06000858 RID: 2136
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		bool isServiceInstantiatedByContractID([MarshalAs(UnmanagedType.LPStr)] string aContractID, [MarshalAs(UnmanagedType.LPStruct)] Guid aIID);
	}
}
