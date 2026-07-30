using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000081 RID: 129
	[Guid("9eb2c150-1d56-11d3-8221-0060083a0bcf")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMBarProp
	{
		// Token: 0x060003C7 RID: 967
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getVisible(out bool ret);

		// Token: 0x060003C8 RID: 968
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setVisible(bool value);
	}
}
