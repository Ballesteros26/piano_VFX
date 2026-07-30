using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000095 RID: 149
	[Guid("31adb439-0055-402d-9b1d-d5ca94f3f55b")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMCounter
	{
		// Token: 0x0600046C RID: 1132
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getIdentifier(HandleRef ret);

		// Token: 0x0600046D RID: 1133
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getListStyle(HandleRef ret);

		// Token: 0x0600046E RID: 1134
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getSeparator(HandleRef ret);
	}
}
