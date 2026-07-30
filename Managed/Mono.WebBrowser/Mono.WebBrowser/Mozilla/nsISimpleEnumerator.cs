using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000111 RID: 273
	[Guid("D1899240-F9D2-11D2-BDD6-000064657374")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsISimpleEnumerator
	{
		// Token: 0x0600085B RID: 2139
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int hasMoreElements(out bool ret);

		// Token: 0x0600085C RID: 2140
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNext(out IntPtr ret);
	}
}
