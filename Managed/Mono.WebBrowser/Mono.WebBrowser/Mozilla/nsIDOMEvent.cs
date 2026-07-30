using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000AD RID: 173
	[Guid("a66b7b80-ff46-bd97-0080-5f8ae38add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMEvent
	{
		// Token: 0x06000535 RID: 1333
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getType(HandleRef ret);

		// Token: 0x06000536 RID: 1334
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTarget([MarshalAs(UnmanagedType.Interface)] out nsIDOMEventTarget ret);

		// Token: 0x06000537 RID: 1335
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCurrentTarget([MarshalAs(UnmanagedType.Interface)] out nsIDOMEventTarget ret);

		// Token: 0x06000538 RID: 1336
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getEventPhase(out ushort ret);

		// Token: 0x06000539 RID: 1337
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getBubbles(out bool ret);

		// Token: 0x0600053A RID: 1338
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCancelable(out bool ret);

		// Token: 0x0600053B RID: 1339
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTimeStamp(out int ret);

		// Token: 0x0600053C RID: 1340
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int stopPropagation();

		// Token: 0x0600053D RID: 1341
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int preventDefault();

		// Token: 0x0600053E RID: 1342
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int initEvent(HandleRef eventTypeArg, bool canBubbleArg, bool cancelableArg);
	}
}
