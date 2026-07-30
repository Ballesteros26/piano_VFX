using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000529 RID: 1321
	internal sealed class DecoupledTask : IDecoupledTask
	{
		// Token: 0x06003C1E RID: 15390 RVA: 0x000D8E9F File Offset: 0x000D709F
		public DecoupledTask(Task task)
		{
			this.Task = task;
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06003C1F RID: 15391 RVA: 0x000D8EAE File Offset: 0x000D70AE
		public bool IsCompleted
		{
			get
			{
				return this.Task.IsCompleted;
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06003C20 RID: 15392 RVA: 0x000D8EBB File Offset: 0x000D70BB
		// (set) Token: 0x06003C21 RID: 15393 RVA: 0x000D8EC3 File Offset: 0x000D70C3
		public Task Task { get; private set; }
	}
}
