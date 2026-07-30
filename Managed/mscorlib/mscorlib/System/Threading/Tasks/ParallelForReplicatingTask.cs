using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000501 RID: 1281
	internal class ParallelForReplicatingTask : Task
	{
		// Token: 0x06003B04 RID: 15108 RVA: 0x000D60C8 File Offset: 0x000D42C8
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal ParallelForReplicatingTask(ParallelOptions parallelOptions, Action action, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions)
			: base(action, null, Task.InternalCurrent, default(CancellationToken), creationOptions, internalOptions | InternalTaskOptions.SelfReplicating, null)
		{
			this.m_replicationDownCount = parallelOptions.EffectiveMaxConcurrencyLevel;
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			base.PossiblyCaptureContext(ref stackCrawlMark);
		}

		// Token: 0x06003B05 RID: 15109 RVA: 0x000D610B File Offset: 0x000D430B
		internal override bool ShouldReplicate()
		{
			if (this.m_replicationDownCount == -1)
			{
				return true;
			}
			if (this.m_replicationDownCount > 0)
			{
				this.m_replicationDownCount--;
				return true;
			}
			return false;
		}

		// Token: 0x06003B06 RID: 15110 RVA: 0x000D6132 File Offset: 0x000D4332
		internal override Task CreateReplicaTask(Action<object> taskReplicaDelegate, object stateObject, Task parentTask, TaskScheduler taskScheduler, TaskCreationOptions creationOptionsForReplica, InternalTaskOptions internalOptionsForReplica)
		{
			return new ParallelForReplicaTask(taskReplicaDelegate, stateObject, parentTask, taskScheduler, creationOptionsForReplica, internalOptionsForReplica);
		}

		// Token: 0x04001EB7 RID: 7863
		private int m_replicationDownCount;
	}
}
