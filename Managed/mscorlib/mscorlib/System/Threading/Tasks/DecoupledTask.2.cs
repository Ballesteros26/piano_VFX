using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200052A RID: 1322
	internal sealed class DecoupledTask<T> : IDecoupledTask
	{
		// Token: 0x06003C22 RID: 15394 RVA: 0x000D8ECC File Offset: 0x000D70CC
		public DecoupledTask(Task<T> task)
		{
			this.Task = task;
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06003C23 RID: 15395 RVA: 0x000D8EDB File Offset: 0x000D70DB
		public bool IsCompleted
		{
			get
			{
				return this.Task.IsCompleted;
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06003C24 RID: 15396 RVA: 0x000D8EE8 File Offset: 0x000D70E8
		// (set) Token: 0x06003C25 RID: 15397 RVA: 0x000D8EF0 File Offset: 0x000D70F0
		public Task<T> Task { get; private set; }
	}
}
