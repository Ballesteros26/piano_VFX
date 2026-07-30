using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000085 RID: 133
	[Guid("e249031f-8df9-4e7a-b644-18946dce0019")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMCSSPrimitiveValue : nsIDOMCSSValue
	{
		// Token: 0x060003EF RID: 1007
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCssText(HandleRef ret);

		// Token: 0x060003F0 RID: 1008
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setCssText(HandleRef value);

		// Token: 0x060003F1 RID: 1009
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCssValueType(out ushort ret);

		// Token: 0x060003F2 RID: 1010
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPrimitiveType(out ushort ret);

		// Token: 0x060003F3 RID: 1011
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setFloatValue(ushort unitType, float floatValue);

		// Token: 0x060003F4 RID: 1012
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getFloatValue(ushort unitType, out float ret);

		// Token: 0x060003F5 RID: 1013
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setStringValue(ushort stringType, HandleRef stringValue);

		// Token: 0x060003F6 RID: 1014
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getStringValue(HandleRef ret);

		// Token: 0x060003F7 RID: 1015
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCounterValue([MarshalAs(UnmanagedType.Interface)] out nsIDOMCounter ret);

		// Token: 0x060003F8 RID: 1016
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getRectValue([MarshalAs(UnmanagedType.Interface)] out nsIDOMRect ret);

		// Token: 0x060003F9 RID: 1017
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getRGBColorValue([MarshalAs(UnmanagedType.Interface)] out nsIDOMRGBColor ret);
	}
}
