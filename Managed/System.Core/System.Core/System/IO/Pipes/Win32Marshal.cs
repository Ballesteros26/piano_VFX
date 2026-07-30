using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x0200004F RID: 79
	internal static class Win32Marshal
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000045DC File Offset: 0x000027DC
		internal static bool IsWindows
		{
			get
			{
				PlatformID platform = Environment.OSVersion.Platform;
				return platform <= PlatformID.WinCE;
			}
		}

		// Token: 0x0600017E RID: 382
		[DllImport("kernel32", SetLastError = true)]
		internal static extern bool CreatePipe(out IntPtr readHandle, out IntPtr writeHandle, ref SecurityAttributes pipeAtts, int size);

		// Token: 0x0600017F RID: 383
		[DllImport("kernel32", SetLastError = true)]
		internal static extern IntPtr CreateNamedPipe(string name, uint openMode, int pipeMode, int maxInstances, int outBufferSize, int inBufferSize, int defaultTimeout, ref SecurityAttributes securityAttributes, IntPtr atts);

		// Token: 0x06000180 RID: 384
		[DllImport("kernel32", SetLastError = true)]
		internal static extern bool ConnectNamedPipe(SafePipeHandle handle, IntPtr overlapped);

		// Token: 0x06000181 RID: 385
		[DllImport("kernel32", SetLastError = true)]
		internal static extern bool DisconnectNamedPipe(SafePipeHandle handle);

		// Token: 0x06000182 RID: 386
		[DllImport("kernel32", SetLastError = true)]
		internal static extern bool GetNamedPipeHandleState(SafePipeHandle handle, out int state, out int curInstances, out int maxCollectionCount, out int collectDateTimeout, byte[] userName, int maxUserNameSize);

		// Token: 0x06000183 RID: 387
		[DllImport("kernel32", SetLastError = true)]
		internal static extern bool WaitNamedPipe(string name, int timeout);

		// Token: 0x06000184 RID: 388
		[DllImport("kernel32", SetLastError = true)]
		internal static extern IntPtr CreateFile(string name, PipeAccessRights desiredAccess, FileShare fileShare, ref SecurityAttributes atts, int creationDisposition, int flags, IntPtr templateHandle);
	}
}
