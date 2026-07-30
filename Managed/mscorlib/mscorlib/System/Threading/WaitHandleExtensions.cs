using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	// Token: 0x0200049F RID: 1183
	public static class WaitHandleExtensions
	{
		// Token: 0x0600379C RID: 14236 RVA: 0x000CABC1 File Offset: 0x000C8DC1
		[SecurityCritical]
		public static SafeWaitHandle GetSafeWaitHandle(this WaitHandle waitHandle)
		{
			if (waitHandle == null)
			{
				throw new ArgumentNullException("waitHandle");
			}
			return waitHandle.SafeWaitHandle;
		}

		// Token: 0x0600379D RID: 14237 RVA: 0x000CABD7 File Offset: 0x000C8DD7
		[SecurityCritical]
		public static void SetSafeWaitHandle(this WaitHandle waitHandle, SafeWaitHandle value)
		{
			if (waitHandle == null)
			{
				throw new ArgumentNullException("waitHandle");
			}
			waitHandle.SafeWaitHandle = value;
		}
	}
}
