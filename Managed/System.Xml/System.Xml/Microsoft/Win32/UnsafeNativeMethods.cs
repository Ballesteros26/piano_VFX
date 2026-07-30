using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32
{
	// Token: 0x02000068 RID: 104
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNativeMethods
	{
		// Token: 0x0600036D RID: 877
		[SecurityCritical]
		[DllImport("kernel32.dll", EntryPoint = "GetCurrentPackageId")]
		[return: MarshalAs(UnmanagedType.I4)]
		private static extern int _GetCurrentPackageId(ref int pBufferLength, byte[] pBuffer);

		// Token: 0x0600036E RID: 878
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string methodName);

		// Token: 0x0600036F RID: 879
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string moduleName);

		// Token: 0x06000370 RID: 880 RVA: 0x0000D218 File Offset: 0x0000B418
		[SecurityCritical]
		private static bool DoesWin32MethodExist(string moduleName, string methodName)
		{
			IntPtr moduleHandle = UnsafeNativeMethods.GetModuleHandle(moduleName);
			return !(moduleHandle == IntPtr.Zero) && UnsafeNativeMethods.GetProcAddress(moduleHandle, methodName) != IntPtr.Zero;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000D24C File Offset: 0x0000B44C
		[SecuritySafeCritical]
		private static bool _IsPackagedProcess()
		{
			OperatingSystem osversion = Environment.OSVersion;
			if (osversion.Platform == PlatformID.Win32NT && osversion.Version >= new Version(6, 2, 0, 0) && UnsafeNativeMethods.DoesWin32MethodExist("kernel32.dll", "GetCurrentPackageId"))
			{
				int num = 0;
				return UnsafeNativeMethods._GetCurrentPackageId(ref num, null) == 122;
			}
			return false;
		}

		// Token: 0x040001BC RID: 444
		internal const string KERNEL32 = "kernel32.dll";

		// Token: 0x040001BD RID: 445
		internal const int ERROR_INSUFFICIENT_BUFFER = 122;

		// Token: 0x040001BE RID: 446
		internal const int ERROR_NO_PACKAGE_IDENTITY = 15700;

		// Token: 0x040001BF RID: 447
		[SecuritySafeCritical]
		internal static Lazy<bool> IsPackagedProcess = new Lazy<bool>(() => UnsafeNativeMethods._IsPackagedProcess());
	}
}
