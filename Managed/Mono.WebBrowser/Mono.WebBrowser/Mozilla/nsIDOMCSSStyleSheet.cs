using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x0200008D RID: 141
	[Guid("a6cf90c2-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMCSSStyleSheet : nsIDOMStyleSheet
	{
		// Token: 0x06000413 RID: 1043
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getType(HandleRef ret);

		// Token: 0x06000414 RID: 1044
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getDisabled(out bool ret);

		// Token: 0x06000415 RID: 1045
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setDisabled(bool value);

		// Token: 0x06000416 RID: 1046
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getOwnerNode([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

		// Token: 0x06000417 RID: 1047
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getParentStyleSheet([MarshalAs(UnmanagedType.Interface)] out nsIDOMStyleSheet ret);

		// Token: 0x06000418 RID: 1048
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getHref(HandleRef ret);

		// Token: 0x06000419 RID: 1049
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTitle(HandleRef ret);

		// Token: 0x0600041A RID: 1050
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getMedia([MarshalAs(UnmanagedType.Interface)] out nsIDOMMediaList ret);

		// Token: 0x0600041B RID: 1051
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getOwnerRule([MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSRule ret);

		// Token: 0x0600041C RID: 1052
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCssRules([MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSRuleList ret);

		// Token: 0x0600041D RID: 1053
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int insertRule(HandleRef rule, uint index, out uint ret);

		// Token: 0x0600041E RID: 1054
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int deleteRule(uint index);
	}
}
