using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000491 RID: 1169
	internal sealed class ThreadPoolWorkQueueThreadLocals
	{
		// Token: 0x06003723 RID: 14115 RVA: 0x000C98FF File Offset: 0x000C7AFF
		public ThreadPoolWorkQueueThreadLocals(ThreadPoolWorkQueue tpq)
		{
			this.workQueue = tpq;
			this.workStealingQueue = new ThreadPoolWorkQueue.WorkStealingQueue();
			ThreadPoolWorkQueue.allThreadQueues.Add(this.workStealingQueue);
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x000C9940 File Offset: 0x000C7B40
		[SecurityCritical]
		private void CleanUp()
		{
			if (this.workStealingQueue != null)
			{
				if (this.workQueue != null)
				{
					bool flag = false;
					while (!flag)
					{
						try
						{
						}
						finally
						{
							IThreadPoolWorkItem threadPoolWorkItem = null;
							if (this.workStealingQueue.LocalPop(out threadPoolWorkItem))
							{
								this.workQueue.Enqueue(threadPoolWorkItem, true);
							}
							else
							{
								flag = true;
							}
						}
					}
				}
				ThreadPoolWorkQueue.allThreadQueues.Remove(this.workStealingQueue);
			}
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x000C99AC File Offset: 0x000C7BAC
		[SecuritySafeCritical]
		~ThreadPoolWorkQueueThreadLocals()
		{
			if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
			{
				this.CleanUp();
			}
		}

		// Token: 0x04001CFD RID: 7421
		[ThreadStatic]
		[SecurityCritical]
		public static ThreadPoolWorkQueueThreadLocals threadLocals;

		// Token: 0x04001CFE RID: 7422
		public readonly ThreadPoolWorkQueue workQueue;

		// Token: 0x04001CFF RID: 7423
		public readonly ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue;

		// Token: 0x04001D00 RID: 7424
		public readonly Random random = new Random(Thread.CurrentThread.ManagedThreadId);
	}
}
