using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	// Token: 0x020001DE RID: 478
	internal class ProcessWaitHandle : WaitHandle
	{
		// Token: 0x06000F31 RID: 3889 RVA: 0x00046948 File Offset: 0x00044B48
		internal ProcessWaitHandle(SafeProcessHandle processHandle)
		{
			SafeWaitHandle safeWaitHandle = null;
			if (!Microsoft.Win32.NativeMethods.DuplicateHandle(new HandleRef(this, Microsoft.Win32.NativeMethods.GetCurrentProcess()), processHandle, new HandleRef(this, Microsoft.Win32.NativeMethods.GetCurrentProcess()), out safeWaitHandle, 0, false, 2))
			{
				throw new SystemException("Unknown error in DuplicateHandle");
			}
			base.SafeWaitHandle = safeWaitHandle;
		}
	}
}
