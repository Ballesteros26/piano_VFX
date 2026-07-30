using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000500 RID: 1280
	internal class SystemThreadingTasks_TaskDebugView
	{
		// Token: 0x06003AFD RID: 15101 RVA: 0x000D6043 File Offset: 0x000D4243
		public SystemThreadingTasks_TaskDebugView(Task task)
		{
			this.m_task = task;
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06003AFE RID: 15102 RVA: 0x000D6052 File Offset: 0x000D4252
		public object AsyncState
		{
			get
			{
				return this.m_task.AsyncState;
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06003AFF RID: 15103 RVA: 0x000D605F File Offset: 0x000D425F
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.m_task.CreationOptions;
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06003B00 RID: 15104 RVA: 0x000D606C File Offset: 0x000D426C
		public Exception Exception
		{
			get
			{
				return this.m_task.Exception;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06003B01 RID: 15105 RVA: 0x000D6079 File Offset: 0x000D4279
		public int Id
		{
			get
			{
				return this.m_task.Id;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06003B02 RID: 15106 RVA: 0x000D6088 File Offset: 0x000D4288
		public bool CancellationPending
		{
			get
			{
				return this.m_task.Status == TaskStatus.WaitingToRun && this.m_task.CancellationToken.IsCancellationRequested;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06003B03 RID: 15107 RVA: 0x000D60B8 File Offset: 0x000D42B8
		public TaskStatus Status
		{
			get
			{
				return this.m_task.Status;
			}
		}

		// Token: 0x04001EB6 RID: 7862
		private Task m_task;
	}
}
