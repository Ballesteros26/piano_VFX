using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000502 RID: 1282
	internal class ParallelForReplicaTask : Task
	{
		// Token: 0x06003B07 RID: 15111 RVA: 0x000D6144 File Offset: 0x000D4344
		internal ParallelForReplicaTask(Action<object> taskReplicaDelegate, object stateObject, Task parentTask, TaskScheduler taskScheduler, TaskCreationOptions creationOptionsForReplica, InternalTaskOptions internalOptionsForReplica)
			: base(taskReplicaDelegate, stateObject, parentTask, default(CancellationToken), creationOptionsForReplica, internalOptionsForReplica, taskScheduler)
		{
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06003B08 RID: 15112 RVA: 0x000D6169 File Offset: 0x000D4369
		// (set) Token: 0x06003B09 RID: 15113 RVA: 0x000D6171 File Offset: 0x000D4371
		internal override object SavedStateForNextReplica
		{
			get
			{
				return this.m_stateForNextReplica;
			}
			set
			{
				this.m_stateForNextReplica = value;
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06003B0A RID: 15114 RVA: 0x000D617A File Offset: 0x000D437A
		// (set) Token: 0x06003B0B RID: 15115 RVA: 0x000D6182 File Offset: 0x000D4382
		internal override object SavedStateFromPreviousReplica
		{
			get
			{
				return this.m_stateFromPreviousReplica;
			}
			set
			{
				this.m_stateFromPreviousReplica = value;
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06003B0C RID: 15116 RVA: 0x000D618B File Offset: 0x000D438B
		// (set) Token: 0x06003B0D RID: 15117 RVA: 0x000D6193 File Offset: 0x000D4393
		internal override Task HandedOverChildReplica
		{
			get
			{
				return this.m_handedOverChildReplica;
			}
			set
			{
				this.m_handedOverChildReplica = value;
			}
		}

		// Token: 0x04001EB8 RID: 7864
		internal object m_stateForNextReplica;

		// Token: 0x04001EB9 RID: 7865
		internal object m_stateFromPreviousReplica;

		// Token: 0x04001EBA RID: 7866
		internal Task m_handedOverChildReplica;
	}
}
