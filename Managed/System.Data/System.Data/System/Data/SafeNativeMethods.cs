using System;
using System.Runtime.InteropServices;

namespace System.Data
{
	// Token: 0x02000116 RID: 278
	internal static class SafeNativeMethods
	{
		// Token: 0x06000E4D RID: 3661 RVA: 0x0004B8E9 File Offset: 0x00049AE9
		internal static IntPtr LocalAlloc(IntPtr initialSize)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(initialSize);
			SafeNativeMethods.ZeroMemory(intPtr, (int)initialSize);
			return intPtr;
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0004B8FD File Offset: 0x00049AFD
		internal static void LocalFree(IntPtr ptr)
		{
			Marshal.FreeHGlobal(ptr);
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0004B905 File Offset: 0x00049B05
		internal static void ZeroMemory(IntPtr ptr, int length)
		{
			Marshal.Copy(new byte[length], 0, ptr, length);
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0004B915 File Offset: 0x00049B15
		internal static IntPtr GetProcAddress(IntPtr HModule, string funcName)
		{
			throw new PlatformNotSupportedException("SafeNativeMethods.GetProcAddress is not supported on non-Windows platforms");
		}
	}
}
