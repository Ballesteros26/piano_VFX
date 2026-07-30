using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000115 RID: 277
	[Guid("436a83fa-b396-11d9-bcfa-00112478d626")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsITimer
	{
		// Token: 0x06000864 RID: 2148
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int init([MarshalAs(UnmanagedType.Interface)] nsIObserver aObserver, uint aDelay, uint aType);

		// Token: 0x06000865 RID: 2149
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int initWithFuncCallback(nsITimerCallbackDelegate aCallback, IntPtr aClosure, uint aDelay, uint aType);

		// Token: 0x06000866 RID: 2150
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int initWithCallback([MarshalAs(UnmanagedType.Interface)] nsITimerCallback aCallback, uint aDelay, uint aType);

		// Token: 0x06000867 RID: 2151
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int cancel();

		// Token: 0x06000868 RID: 2152
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getDelay(out uint ret);

		// Token: 0x06000869 RID: 2153
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setDelay(uint value);

		// Token: 0x0600086A RID: 2154
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getType(out uint ret);

		// Token: 0x0600086B RID: 2155
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setType(uint value);

		// Token: 0x0600086C RID: 2156
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getClosure(IntPtr ret);

		// Token: 0x0600086D RID: 2157
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCallback([MarshalAs(UnmanagedType.Interface)] out nsITimerCallback ret);
	}
}
