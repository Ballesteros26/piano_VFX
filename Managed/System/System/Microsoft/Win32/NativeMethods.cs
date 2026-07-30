using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Win32
{
	// Token: 0x020000DF RID: 223
	internal static class NativeMethods
	{
		// Token: 0x060004EF RID: 1263 RVA: 0x0000ED90 File Offset: 0x0000CF90
		public static bool DuplicateHandle(HandleRef hSourceProcessHandle, SafeHandle hSourceHandle, HandleRef hTargetProcess, out SafeWaitHandle targetHandle, int dwDesiredAccess, bool bInheritHandle, int dwOptions)
		{
			bool flag = false;
			bool flag3;
			try
			{
				hSourceHandle.DangerousAddRef(ref flag);
				IntPtr intPtr;
				MonoIOError monoIOError;
				bool flag2 = MonoIO.DuplicateHandle(hSourceProcessHandle.Handle, hSourceHandle.DangerousGetHandle(), hTargetProcess.Handle, out intPtr, dwDesiredAccess, bInheritHandle ? 1 : 0, dwOptions, out monoIOError);
				if (monoIOError != MonoIOError.ERROR_SUCCESS)
				{
					throw MonoIO.GetException(monoIOError);
				}
				targetHandle = new SafeWaitHandle(intPtr, true);
				flag3 = flag2;
			}
			finally
			{
				if (flag)
				{
					hSourceHandle.DangerousRelease();
				}
			}
			return flag3;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000EE04 File Offset: 0x0000D004
		public static bool DuplicateHandle(HandleRef hSourceProcessHandle, HandleRef hSourceHandle, HandleRef hTargetProcess, out SafeProcessHandle targetHandle, int dwDesiredAccess, bool bInheritHandle, int dwOptions)
		{
			IntPtr intPtr;
			MonoIOError monoIOError;
			bool flag = MonoIO.DuplicateHandle(hSourceProcessHandle.Handle, hSourceHandle.Handle, hTargetProcess.Handle, out intPtr, dwDesiredAccess, bInheritHandle ? 1 : 0, dwOptions, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(monoIOError);
			}
			targetHandle = new SafeProcessHandle(intPtr, true);
			return flag;
		}

		// Token: 0x060004F1 RID: 1265
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetCurrentProcess();

		// Token: 0x060004F2 RID: 1266
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetExitCodeProcess(IntPtr processHandle, out int exitCode);

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000EE50 File Offset: 0x0000D050
		public static bool GetExitCodeProcess(SafeProcessHandle processHandle, out int exitCode)
		{
			bool flag = false;
			bool exitCodeProcess;
			try
			{
				processHandle.DangerousAddRef(ref flag);
				exitCodeProcess = NativeMethods.GetExitCodeProcess(processHandle.DangerousGetHandle(), out exitCode);
			}
			finally
			{
				if (flag)
				{
					processHandle.DangerousRelease();
				}
			}
			return exitCodeProcess;
		}

		// Token: 0x060004F4 RID: 1268
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool TerminateProcess(IntPtr processHandle, int exitCode);

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000EE94 File Offset: 0x0000D094
		public static bool TerminateProcess(SafeProcessHandle processHandle, int exitCode)
		{
			bool flag = false;
			bool flag2;
			try
			{
				processHandle.DangerousAddRef(ref flag);
				flag2 = NativeMethods.TerminateProcess(processHandle.DangerousGetHandle(), exitCode);
			}
			finally
			{
				if (flag)
				{
					processHandle.DangerousRelease();
				}
			}
			return flag2;
		}

		// Token: 0x060004F6 RID: 1270
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int WaitForInputIdle(IntPtr handle, int milliseconds);

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000EED8 File Offset: 0x0000D0D8
		public static int WaitForInputIdle(SafeProcessHandle handle, int milliseconds)
		{
			bool flag = false;
			int num;
			try
			{
				handle.DangerousAddRef(ref flag);
				num = NativeMethods.WaitForInputIdle(handle.DangerousGetHandle(), milliseconds);
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x060004F8 RID: 1272
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetProcessWorkingSetSize(IntPtr handle, out IntPtr min, out IntPtr max);

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000EF1C File Offset: 0x0000D11C
		public static bool GetProcessWorkingSetSize(SafeProcessHandle handle, out IntPtr min, out IntPtr max)
		{
			bool flag = false;
			bool processWorkingSetSize;
			try
			{
				handle.DangerousAddRef(ref flag);
				processWorkingSetSize = NativeMethods.GetProcessWorkingSetSize(handle.DangerousGetHandle(), out min, out max);
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return processWorkingSetSize;
		}

		// Token: 0x060004FA RID: 1274
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SetProcessWorkingSetSize(IntPtr handle, IntPtr min, IntPtr max);

		// Token: 0x060004FB RID: 1275 RVA: 0x0000EF60 File Offset: 0x0000D160
		public static bool SetProcessWorkingSetSize(SafeProcessHandle handle, IntPtr min, IntPtr max)
		{
			bool flag = false;
			bool flag2;
			try
			{
				handle.DangerousAddRef(ref flag);
				flag2 = NativeMethods.SetProcessWorkingSetSize(handle.DangerousGetHandle(), min, max);
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return flag2;
		}

		// Token: 0x060004FC RID: 1276
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetProcessTimes(IntPtr handle, out long creation, out long exit, out long kernel, out long user);

		// Token: 0x060004FD RID: 1277 RVA: 0x0000EFA4 File Offset: 0x0000D1A4
		public static bool GetProcessTimes(SafeProcessHandle handle, out long creation, out long exit, out long kernel, out long user)
		{
			bool flag = false;
			bool processTimes;
			try
			{
				handle.DangerousAddRef(ref flag);
				processTimes = NativeMethods.GetProcessTimes(handle.DangerousGetHandle(), out creation, out exit, out kernel, out user);
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return processTimes;
		}

		// Token: 0x060004FE RID: 1278
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetCurrentProcessId();

		// Token: 0x060004FF RID: 1279
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetPriorityClass(IntPtr handle);

		// Token: 0x06000500 RID: 1280 RVA: 0x0000EFEC File Offset: 0x0000D1EC
		public static int GetPriorityClass(SafeProcessHandle handle)
		{
			bool flag = false;
			int priorityClass;
			try
			{
				handle.DangerousAddRef(ref flag);
				priorityClass = NativeMethods.GetPriorityClass(handle.DangerousGetHandle());
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return priorityClass;
		}

		// Token: 0x06000501 RID: 1281
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SetPriorityClass(IntPtr handle, int priorityClass);

		// Token: 0x06000502 RID: 1282 RVA: 0x0000F02C File Offset: 0x0000D22C
		public static bool SetPriorityClass(SafeProcessHandle handle, int priorityClass)
		{
			bool flag = false;
			bool flag2;
			try
			{
				handle.DangerousAddRef(ref flag);
				flag2 = NativeMethods.SetPriorityClass(handle.DangerousGetHandle(), priorityClass);
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return flag2;
		}

		// Token: 0x06000503 RID: 1283
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CloseProcess(IntPtr handle);

		// Token: 0x04000BAF RID: 2991
		public const int E_ABORT = -2147467260;

		// Token: 0x04000BB0 RID: 2992
		public const int PROCESS_TERMINATE = 1;

		// Token: 0x04000BB1 RID: 2993
		public const int PROCESS_CREATE_THREAD = 2;

		// Token: 0x04000BB2 RID: 2994
		public const int PROCESS_SET_SESSIONID = 4;

		// Token: 0x04000BB3 RID: 2995
		public const int PROCESS_VM_OPERATION = 8;

		// Token: 0x04000BB4 RID: 2996
		public const int PROCESS_VM_READ = 16;

		// Token: 0x04000BB5 RID: 2997
		public const int PROCESS_VM_WRITE = 32;

		// Token: 0x04000BB6 RID: 2998
		public const int PROCESS_DUP_HANDLE = 64;

		// Token: 0x04000BB7 RID: 2999
		public const int PROCESS_CREATE_PROCESS = 128;

		// Token: 0x04000BB8 RID: 3000
		public const int PROCESS_SET_QUOTA = 256;

		// Token: 0x04000BB9 RID: 3001
		public const int PROCESS_SET_INFORMATION = 512;

		// Token: 0x04000BBA RID: 3002
		public const int PROCESS_QUERY_INFORMATION = 1024;

		// Token: 0x04000BBB RID: 3003
		public const int PROCESS_QUERY_LIMITED_INFORMATION = 4096;

		// Token: 0x04000BBC RID: 3004
		public const int STANDARD_RIGHTS_REQUIRED = 983040;

		// Token: 0x04000BBD RID: 3005
		public const int SYNCHRONIZE = 1048576;

		// Token: 0x04000BBE RID: 3006
		public const int PROCESS_ALL_ACCESS = 2035711;

		// Token: 0x04000BBF RID: 3007
		public const int DUPLICATE_CLOSE_SOURCE = 1;

		// Token: 0x04000BC0 RID: 3008
		public const int DUPLICATE_SAME_ACCESS = 2;

		// Token: 0x04000BC1 RID: 3009
		public const int STILL_ACTIVE = 259;

		// Token: 0x04000BC2 RID: 3010
		public const int WAIT_OBJECT_0 = 0;

		// Token: 0x04000BC3 RID: 3011
		public const int WAIT_FAILED = -1;

		// Token: 0x04000BC4 RID: 3012
		public const int WAIT_TIMEOUT = 258;

		// Token: 0x04000BC5 RID: 3013
		public const int WAIT_ABANDONED = 128;

		// Token: 0x04000BC6 RID: 3014
		public const int WAIT_ABANDONED_0 = 128;

		// Token: 0x04000BC7 RID: 3015
		public const int ERROR_FILE_NOT_FOUND = 2;

		// Token: 0x04000BC8 RID: 3016
		public const int ERROR_PATH_NOT_FOUND = 3;

		// Token: 0x04000BC9 RID: 3017
		public const int ERROR_ACCESS_DENIED = 5;

		// Token: 0x04000BCA RID: 3018
		public const int ERROR_INVALID_HANDLE = 6;

		// Token: 0x04000BCB RID: 3019
		public const int ERROR_SHARING_VIOLATION = 32;

		// Token: 0x04000BCC RID: 3020
		public const int ERROR_INVALID_NAME = 123;

		// Token: 0x04000BCD RID: 3021
		public const int ERROR_ALREADY_EXISTS = 183;

		// Token: 0x04000BCE RID: 3022
		public const int ERROR_FILENAME_EXCED_RANGE = 206;
	}
}
