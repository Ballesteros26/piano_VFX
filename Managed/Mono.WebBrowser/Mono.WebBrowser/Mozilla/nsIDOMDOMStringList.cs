using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000099 RID: 153
	[Guid("0bbae65c-1dde-11d9-8c46-000a95dc234c")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMDOMStringList
	{
		// Token: 0x06000476 RID: 1142
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int item(uint index, HandleRef ret);

		// Token: 0x06000477 RID: 1143
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLength(out uint ret);

		// Token: 0x06000478 RID: 1144
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int contains(HandleRef str, out bool ret);
	}
}
