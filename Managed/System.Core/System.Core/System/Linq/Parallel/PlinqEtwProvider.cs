using System;
using System.Diagnostics.Tracing;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x0200020D RID: 525
	[EventSource(Name = "System.Linq.Parallel.PlinqEventSource", Guid = "159eeeec-4a14-4418-a8fe-faabcd987887")]
	internal sealed class PlinqEtwProvider : EventSource
	{
		// Token: 0x06000D10 RID: 3344 RVA: 0x0002B81C File Offset: 0x00029A1C
		private PlinqEtwProvider()
		{
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0002B824 File Offset: 0x00029A24
		[NonEvent]
		internal static int NextQueryId()
		{
			return Interlocked.Increment(ref PlinqEtwProvider.s_queryId);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0002B830 File Offset: 0x00029A30
		[NonEvent]
		internal void ParallelQueryBegin(int queryId)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				int num = Task.CurrentId ?? 0;
				this.ParallelQueryBegin(PlinqEtwProvider.s_defaultSchedulerId, num, queryId);
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0002B86F File Offset: 0x00029A6F
		[Event(1, Level = EventLevel.Informational, Task = (EventTask)1, Opcode = EventOpcode.Start)]
		private void ParallelQueryBegin(int taskSchedulerId, int taskId, int queryId)
		{
			base.WriteEvent(1, taskSchedulerId, taskId, queryId);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0002B87C File Offset: 0x00029A7C
		[NonEvent]
		internal void ParallelQueryEnd(int queryId)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				int num = Task.CurrentId ?? 0;
				this.ParallelQueryEnd(PlinqEtwProvider.s_defaultSchedulerId, num, queryId);
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0002B8BB File Offset: 0x00029ABB
		[Event(2, Level = EventLevel.Informational, Task = (EventTask)1, Opcode = EventOpcode.Stop)]
		private void ParallelQueryEnd(int taskSchedulerId, int taskId, int queryId)
		{
			base.WriteEvent(2, taskSchedulerId, taskId, queryId);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0002B8C8 File Offset: 0x00029AC8
		[NonEvent]
		internal void ParallelQueryFork(int queryId)
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				int num = Task.CurrentId ?? 0;
				this.ParallelQueryFork(PlinqEtwProvider.s_defaultSchedulerId, num, queryId);
			}
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0002B907 File Offset: 0x00029B07
		[Event(3, Level = EventLevel.Verbose, Task = (EventTask)2, Opcode = EventOpcode.Start)]
		private void ParallelQueryFork(int taskSchedulerId, int taskId, int queryId)
		{
			base.WriteEvent(3, taskSchedulerId, taskId, queryId);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0002B914 File Offset: 0x00029B14
		[NonEvent]
		internal void ParallelQueryJoin(int queryId)
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				int num = Task.CurrentId ?? 0;
				this.ParallelQueryJoin(PlinqEtwProvider.s_defaultSchedulerId, num, queryId);
			}
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0002B953 File Offset: 0x00029B53
		[Event(4, Level = EventLevel.Verbose, Task = (EventTask)2, Opcode = EventOpcode.Stop)]
		private void ParallelQueryJoin(int taskSchedulerId, int taskId, int queryId)
		{
			base.WriteEvent(4, taskSchedulerId, taskId, queryId);
		}

		// Token: 0x04000821 RID: 2081
		internal static PlinqEtwProvider Log = new PlinqEtwProvider();

		// Token: 0x04000822 RID: 2082
		private static readonly int s_defaultSchedulerId = TaskScheduler.Default.Id;

		// Token: 0x04000823 RID: 2083
		private static int s_queryId = 0;

		// Token: 0x04000824 RID: 2084
		private const EventKeywords ALL_KEYWORDS = EventKeywords.All;

		// Token: 0x04000825 RID: 2085
		private const int PARALLELQUERYBEGIN_EVENTID = 1;

		// Token: 0x04000826 RID: 2086
		private const int PARALLELQUERYEND_EVENTID = 2;

		// Token: 0x04000827 RID: 2087
		private const int PARALLELQUERYFORK_EVENTID = 3;

		// Token: 0x04000828 RID: 2088
		private const int PARALLELQUERYJOIN_EVENTID = 4;

		// Token: 0x0200020E RID: 526
		public class Tasks
		{
			// Token: 0x04000829 RID: 2089
			public const EventTask Query = (EventTask)1;

			// Token: 0x0400082A RID: 2090
			public const EventTask ForkJoin = (EventTask)2;
		}
	}
}
