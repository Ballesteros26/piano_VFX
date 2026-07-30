using System;

namespace System.Threading.Tasks
{
	// Token: 0x020004C6 RID: 1222
	internal class SystemThreadingTasks_FutureDebugView<TResult>
	{
		// Token: 0x060038FF RID: 14591 RVA: 0x000CDBB8 File Offset: 0x000CBDB8
		public SystemThreadingTasks_FutureDebugView(Task<TResult> task)
		{
			this.m_task = task;
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06003900 RID: 14592 RVA: 0x000CDBC8 File Offset: 0x000CBDC8
		public TResult Result
		{
			get
			{
				if (this.m_task.Status != TaskStatus.RanToCompletion)
				{
					return default(TResult);
				}
				return this.m_task.Result;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06003901 RID: 14593 RVA: 0x000CDBF8 File Offset: 0x000CBDF8
		public object AsyncState
		{
			get
			{
				return this.m_task.AsyncState;
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06003902 RID: 14594 RVA: 0x000CDC05 File Offset: 0x000CBE05
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.m_task.CreationOptions;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06003903 RID: 14595 RVA: 0x000CDC12 File Offset: 0x000CBE12
		public Exception Exception
		{
			get
			{
				return this.m_task.Exception;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06003904 RID: 14596 RVA: 0x000CDC1F File Offset: 0x000CBE1F
		public int Id
		{
			get
			{
				return this.m_task.Id;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06003905 RID: 14597 RVA: 0x000CDC2C File Offset: 0x000CBE2C
		public bool CancellationPending
		{
			get
			{
				return this.m_task.Status == TaskStatus.WaitingToRun && this.m_task.CancellationToken.IsCancellationRequested;
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06003906 RID: 14598 RVA: 0x000CDC5C File Offset: 0x000CBE5C
		public TaskStatus Status
		{
			get
			{
				return this.m_task.Status;
			}
		}

		// Token: 0x04001DCC RID: 7628
		private Task<TResult> m_task;
	}
}
