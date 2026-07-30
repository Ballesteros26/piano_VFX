using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000071 RID: 113
	[Guid("f42a1589-70ab-4704-877f-4a9162bbe188")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIAccessibleRelation
	{
		// Token: 0x06000361 RID: 865
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getRelationType(out uint ret);

		// Token: 0x06000362 RID: 866
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTargetsCount(out uint ret);

		// Token: 0x06000363 RID: 867
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTarget(uint index, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

		// Token: 0x06000364 RID: 868
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTargets([MarshalAs(UnmanagedType.Interface)] out nsIArray ret);
	}
}
