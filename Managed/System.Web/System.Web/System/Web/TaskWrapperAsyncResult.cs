using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web
{
	// Token: 0x02000057 RID: 87
	internal sealed class TaskWrapperAsyncResult : IAsyncResult
	{
		// Token: 0x060003DA RID: 986 RVA: 0x00007329 File Offset: 0x00005529
		internal TaskWrapperAsyncResult(Task task, object asyncState)
		{
			this.Task = task;
			this.AsyncState = asyncState;
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000733F File Offset: 0x0000553F
		// (set) Token: 0x060003DC RID: 988 RVA: 0x00007347 File Offset: 0x00005547
		public object AsyncState { get; private set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00007350 File Offset: 0x00005550
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return ((IAsyncResult)this.Task).AsyncWaitHandle;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000735D File Offset: 0x0000555D
		public bool CompletedSynchronously
		{
			get
			{
				return this._forceCompletedSynchronously || ((IAsyncResult)this.Task).CompletedSynchronously;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00007374 File Offset: 0x00005574
		public bool IsCompleted
		{
			get
			{
				return ((IAsyncResult)this.Task).IsCompleted;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00007381 File Offset: 0x00005581
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x00007389 File Offset: 0x00005589
		internal Task Task { get; private set; }

		// Token: 0x060003E2 RID: 994 RVA: 0x00007392 File Offset: 0x00005592
		internal void ForceCompletedSynchronously()
		{
			this._forceCompletedSynchronously = true;
		}

		// Token: 0x04000E2B RID: 3627
		private bool _forceCompletedSynchronously;
	}
}
