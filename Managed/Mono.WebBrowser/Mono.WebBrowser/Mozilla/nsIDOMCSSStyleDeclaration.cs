using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x0200008B RID: 139
	[Guid("a6cf90be-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMCSSStyleDeclaration
	{
		// Token: 0x06000407 RID: 1031
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCssText(HandleRef ret);

		// Token: 0x06000408 RID: 1032
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setCssText(HandleRef value);

		// Token: 0x06000409 RID: 1033
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPropertyValue(HandleRef propertyName, HandleRef ret);

		// Token: 0x0600040A RID: 1034
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPropertyCSSValue(HandleRef propertyName, [MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSValue ret);

		// Token: 0x0600040B RID: 1035
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int removeProperty(HandleRef propertyName, HandleRef ret);

		// Token: 0x0600040C RID: 1036
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPropertyPriority(HandleRef propertyName, HandleRef ret);

		// Token: 0x0600040D RID: 1037
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setProperty(HandleRef propertyName, HandleRef value, HandleRef priority);

		// Token: 0x0600040E RID: 1038
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLength(out uint ret);

		// Token: 0x0600040F RID: 1039
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int item(uint index, HandleRef ret);

		// Token: 0x06000410 RID: 1040
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getParentRule([MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSRule ret);
	}
}
