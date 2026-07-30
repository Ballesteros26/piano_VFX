using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x0200048C RID: 1164
	internal static class ThreadPoolGlobals
	{
		// Token: 0x04001CE6 RID: 7398
		public static uint tpQuantum = 30U;

		// Token: 0x04001CE7 RID: 7399
		public static int processorCount = Environment.ProcessorCount;

		// Token: 0x04001CE8 RID: 7400
		public static bool tpHosted = ThreadPool.IsThreadPoolHosted();

		// Token: 0x04001CE9 RID: 7401
		public static volatile bool vmTpInitialized;

		// Token: 0x04001CEA RID: 7402
		public static bool enableWorkerTracking;

		// Token: 0x04001CEB RID: 7403
		[SecurityCritical]
		public static ThreadPoolWorkQueue workQueue = new ThreadPoolWorkQueue();
	}
}
