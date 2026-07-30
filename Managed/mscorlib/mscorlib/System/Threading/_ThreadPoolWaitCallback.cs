using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000492 RID: 1170
	internal static class _ThreadPoolWaitCallback
	{
		// Token: 0x06003726 RID: 14118 RVA: 0x000C99EC File Offset: 0x000C7BEC
		[SecurityCritical]
		internal static bool PerformWaitCallback()
		{
			return ThreadPoolWorkQueue.Dispatch();
		}
	}
}
