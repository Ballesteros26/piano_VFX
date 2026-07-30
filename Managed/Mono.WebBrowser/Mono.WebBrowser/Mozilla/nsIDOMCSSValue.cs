using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x0200008F RID: 143
	[Guid("009f7ea5-9e80-41be-b008-db62f10823f2")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMCSSValue
	{
		// Token: 0x06000421 RID: 1057
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCssText(HandleRef ret);

		// Token: 0x06000422 RID: 1058
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setCssText(HandleRef value);

		// Token: 0x06000423 RID: 1059
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCssValueType(out ushort ret);
	}
}
