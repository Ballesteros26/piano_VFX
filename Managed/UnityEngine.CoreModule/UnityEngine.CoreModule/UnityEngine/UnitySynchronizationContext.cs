using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001C6 RID: 454
	internal sealed class UnitySynchronizationContext : SynchronizationContext
	{
		// Token: 0x06001455 RID: 5205 RVA: 0x00021529 File Offset: 0x0001F729
		private UnitySynchronizationContext(int mainThreadID)
		{
			this.m_AsyncWorkQueue = new List<UnitySynchronizationContext.WorkRequest>(20);
			this.m_MainThreadID = mainThreadID;
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x0002155B File Offset: 0x0001F75B
		private UnitySynchronizationContext(List<UnitySynchronizationContext.WorkRequest> queue, int mainThreadID)
		{
			this.m_AsyncWorkQueue = queue;
			this.m_MainThreadID = mainThreadID;
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x00021588 File Offset: 0x0001F788
		public override void Send(SendOrPostCallback callback, object state)
		{
			bool flag = this.m_MainThreadID == Thread.CurrentThread.ManagedThreadId;
			if (flag)
			{
				callback.Invoke(state);
			}
			else
			{
				using (ManualResetEvent manualResetEvent = new ManualResetEvent(false))
				{
					List<UnitySynchronizationContext.WorkRequest> asyncWorkQueue = this.m_AsyncWorkQueue;
					lock (asyncWorkQueue)
					{
						this.m_AsyncWorkQueue.Add(new UnitySynchronizationContext.WorkRequest(callback, state, manualResetEvent));
					}
					manualResetEvent.WaitOne();
				}
			}
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x00021624 File Offset: 0x0001F824
		public override void OperationStarted()
		{
			Interlocked.Increment(ref this.m_TrackedCount);
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x00021633 File Offset: 0x0001F833
		public override void OperationCompleted()
		{
			Interlocked.Decrement(ref this.m_TrackedCount);
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x00021644 File Offset: 0x0001F844
		public override void Post(SendOrPostCallback callback, object state)
		{
			List<UnitySynchronizationContext.WorkRequest> asyncWorkQueue = this.m_AsyncWorkQueue;
			lock (asyncWorkQueue)
			{
				this.m_AsyncWorkQueue.Add(new UnitySynchronizationContext.WorkRequest(callback, state, null));
			}
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x00021690 File Offset: 0x0001F890
		public override SynchronizationContext CreateCopy()
		{
			return new UnitySynchronizationContext(this.m_AsyncWorkQueue, this.m_MainThreadID);
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x000216B4 File Offset: 0x0001F8B4
		private void Exec()
		{
			List<UnitySynchronizationContext.WorkRequest> asyncWorkQueue = this.m_AsyncWorkQueue;
			lock (asyncWorkQueue)
			{
				this.m_CurrentFrameWork.AddRange(this.m_AsyncWorkQueue);
				this.m_AsyncWorkQueue.Clear();
			}
			while (this.m_CurrentFrameWork.Count > 0)
			{
				UnitySynchronizationContext.WorkRequest workRequest = this.m_CurrentFrameWork[0];
				this.m_CurrentFrameWork.Remove(workRequest);
				workRequest.Invoke();
			}
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x00021744 File Offset: 0x0001F944
		private bool HasPendingTasks()
		{
			return this.m_AsyncWorkQueue.Count != 0 || this.m_TrackedCount != 0;
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0002176F File Offset: 0x0001F96F
		[RequiredByNativeCode]
		private static void InitializeSynchronizationContext()
		{
			SynchronizationContext.SetSynchronizationContext(new UnitySynchronizationContext(Thread.CurrentThread.ManagedThreadId));
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x00021788 File Offset: 0x0001F988
		[RequiredByNativeCode]
		private static void ExecuteTasks()
		{
			UnitySynchronizationContext unitySynchronizationContext = SynchronizationContext.Current as UnitySynchronizationContext;
			bool flag = unitySynchronizationContext != null;
			if (flag)
			{
				unitySynchronizationContext.Exec();
			}
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x000217B0 File Offset: 0x0001F9B0
		[RequiredByNativeCode]
		private static bool ExecutePendingTasks(long millisecondsTimeout)
		{
			UnitySynchronizationContext unitySynchronizationContext = SynchronizationContext.Current as UnitySynchronizationContext;
			bool flag = unitySynchronizationContext == null;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				while (unitySynchronizationContext.HasPendingTasks())
				{
					bool flag3 = stopwatch.ElapsedMilliseconds > millisecondsTimeout;
					if (flag3)
					{
						break;
					}
					unitySynchronizationContext.Exec();
					Thread.Sleep(1);
				}
				flag2 = !unitySynchronizationContext.HasPendingTasks();
			}
			return flag2;
		}

		// Token: 0x0400067A RID: 1658
		private const int kAwqInitialCapacity = 20;

		// Token: 0x0400067B RID: 1659
		private readonly List<UnitySynchronizationContext.WorkRequest> m_AsyncWorkQueue;

		// Token: 0x0400067C RID: 1660
		private readonly List<UnitySynchronizationContext.WorkRequest> m_CurrentFrameWork = new List<UnitySynchronizationContext.WorkRequest>(20);

		// Token: 0x0400067D RID: 1661
		private readonly int m_MainThreadID;

		// Token: 0x0400067E RID: 1662
		private int m_TrackedCount = 0;

		// Token: 0x020001C7 RID: 455
		private struct WorkRequest
		{
			// Token: 0x06001461 RID: 5217 RVA: 0x00021820 File Offset: 0x0001FA20
			public WorkRequest(SendOrPostCallback callback, object state, ManualResetEvent waitHandle = null)
			{
				this.m_DelagateCallback = callback;
				this.m_DelagateState = state;
				this.m_WaitHandle = waitHandle;
			}

			// Token: 0x06001462 RID: 5218 RVA: 0x00021838 File Offset: 0x0001FA38
			public void Invoke()
			{
				try
				{
					this.m_DelagateCallback.Invoke(this.m_DelagateState);
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
				bool flag = this.m_WaitHandle != null;
				if (flag)
				{
					this.m_WaitHandle.Set();
				}
			}

			// Token: 0x0400067F RID: 1663
			private readonly SendOrPostCallback m_DelagateCallback;

			// Token: 0x04000680 RID: 1664
			private readonly object m_DelagateState;

			// Token: 0x04000681 RID: 1665
			private readonly ManualResetEvent m_WaitHandle;
		}
	}
}
