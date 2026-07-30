using System;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x02000198 RID: 408
	internal class PartitionedStreamMerger<TOutput> : IPartitionedStreamRecipient<TOutput>
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x00024F9C File Offset: 0x0002319C
		internal MergeExecutor<TOutput> MergeExecutor
		{
			get
			{
				return this._mergeExecutor;
			}
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00024FA4 File Offset: 0x000231A4
		internal PartitionedStreamMerger(bool forEffectMerge, ParallelMergeOptions mergeOptions, TaskScheduler taskScheduler, bool outputOrdered, CancellationState cancellationState, int queryId)
		{
			this._forEffectMerge = forEffectMerge;
			this._mergeOptions = mergeOptions;
			this._isOrdered = outputOrdered;
			this._taskScheduler = taskScheduler;
			this._cancellationState = cancellationState;
			this._queryId = queryId;
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00024FD9 File Offset: 0x000231D9
		public void Receive<TKey>(PartitionedStream<TOutput, TKey> partitionedStream)
		{
			this._mergeExecutor = MergeExecutor<TOutput>.Execute<TKey>(partitionedStream, this._forEffectMerge, this._mergeOptions, this._taskScheduler, this._isOrdered, this._cancellationState, this._queryId);
		}

		// Token: 0x040006A0 RID: 1696
		private bool _forEffectMerge;

		// Token: 0x040006A1 RID: 1697
		private ParallelMergeOptions _mergeOptions;

		// Token: 0x040006A2 RID: 1698
		private bool _isOrdered;

		// Token: 0x040006A3 RID: 1699
		private MergeExecutor<TOutput> _mergeExecutor;

		// Token: 0x040006A4 RID: 1700
		private TaskScheduler _taskScheduler;

		// Token: 0x040006A5 RID: 1701
		private int _queryId;

		// Token: 0x040006A6 RID: 1702
		private CancellationState _cancellationState;
	}
}
