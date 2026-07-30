using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000077 RID: 119
	[Guid("d94ac0a0-bb18-46b8-844e-84159064b0bd")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsICancelable
	{
		// Token: 0x0600037B RID: 891
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int cancel(int aReason);
	}
}
