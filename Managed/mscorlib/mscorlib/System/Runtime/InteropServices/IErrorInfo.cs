using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000917 RID: 2327
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("1CF2B120-547D-101B-8E65-08002B2BD119")]
	[SuppressUnmanagedCodeSecurity]
	[ComImport]
	internal interface IErrorInfo
	{
		// Token: 0x060055F1 RID: 22001
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int GetGUID(out Guid pGuid);

		// Token: 0x060055F2 RID: 22002
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int GetSource([MarshalAs(UnmanagedType.BStr)] out string pBstrSource);

		// Token: 0x060055F3 RID: 22003
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int GetDescription([MarshalAs(UnmanagedType.BStr)] out string pbstrDescription);

		// Token: 0x060055F4 RID: 22004
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int GetHelpFile([MarshalAs(UnmanagedType.BStr)] out string pBstrHelpFile);

		// Token: 0x060055F5 RID: 22005
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int GetHelpContext(out uint pdwHelpContext);
	}
}
