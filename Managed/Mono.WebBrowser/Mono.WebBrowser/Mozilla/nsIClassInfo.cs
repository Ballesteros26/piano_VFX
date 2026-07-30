using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x0200007B RID: 123
	[Guid("986c11d0-f340-11d4-9075-0010a4e73d9a")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIClassInfo
	{
		// Token: 0x0600039A RID: 922
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getInterfaces(out uint count, out IntPtr array);

		// Token: 0x0600039B RID: 923
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getHelperForLanguage(uint language, [MarshalAs(UnmanagedType.Interface)] out IntPtr ret);

		// Token: 0x0600039C RID: 924
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getContractID(ref IntPtr ret);

		// Token: 0x0600039D RID: 925
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getClassDescription(ref IntPtr ret);

		// Token: 0x0600039E RID: 926
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getClassID([MarshalAs(UnmanagedType.LPStruct)] out Guid ret);

		// Token: 0x0600039F RID: 927
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getImplementationLanguage(out uint ret);

		// Token: 0x060003A0 RID: 928
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getFlags(out uint ret);

		// Token: 0x060003A1 RID: 929
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getClassIDNoAlloc([MarshalAs(UnmanagedType.LPStruct)] out Guid ret);
	}
}
