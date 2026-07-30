using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000FB RID: 251
	[Guid("1A180F60-93B2-11d2-9B8B-00805F8A16D9")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIPersistentProperties : nsIProperties
	{
		// Token: 0x060007E5 RID: 2021
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int get([MarshalAs(UnmanagedType.LPStr)] string prop, [MarshalAs(UnmanagedType.LPStruct)] Guid iid, out IntPtr result);

		// Token: 0x060007E6 RID: 2022
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int set([MarshalAs(UnmanagedType.LPStr)] string prop, [MarshalAs(UnmanagedType.Interface)] IntPtr value);

		// Token: 0x060007E7 RID: 2023
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int has([MarshalAs(UnmanagedType.LPStr)] string prop, out bool ret);

		// Token: 0x060007E8 RID: 2024
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int undefine([MarshalAs(UnmanagedType.LPStr)] string prop);

		// Token: 0x060007E9 RID: 2025
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getKeys(out uint count, [MarshalAs(UnmanagedType.LPStr)] out string[] keys);

		// Token: 0x060007EA RID: 2026
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int load([MarshalAs(UnmanagedType.Interface)] nsIInputStream input);

		// Token: 0x060007EB RID: 2027
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int save([MarshalAs(UnmanagedType.Interface)] nsIOutputStream output, HandleRef header);

		// Token: 0x060007EC RID: 2028
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int subclass([MarshalAs(UnmanagedType.Interface)] nsIPersistentProperties superclass);

		// Token: 0x060007ED RID: 2029
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int enumerate([MarshalAs(UnmanagedType.Interface)] out nsISimpleEnumerator ret);

		// Token: 0x060007EE RID: 2030
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getStringProperty(HandleRef key, HandleRef ret);

		// Token: 0x060007EF RID: 2031
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setStringProperty(HandleRef key, HandleRef value, HandleRef ret);
	}
}
