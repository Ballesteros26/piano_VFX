using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x0200019F RID: 415
	internal abstract class QueryOperator<TOutput> : ParallelQuery<TOutput>
	{
		// Token: 0x06000B26 RID: 2854 RVA: 0x000255B4 File Offset: 0x000237B4
		internal QueryOperator(QuerySettings settings)
			: this(false, settings)
		{
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x000255BE File Offset: 0x000237BE
		internal QueryOperator(bool isOrdered, QuerySettings settings)
			: base(settings)
		{
			this._outputOrdered = isOrdered;
		}

		// Token: 0x06000B28 RID: 2856
		internal abstract QueryResults<TOutput> Open(QuerySettings settings, bool preferStriping);

		// Token: 0x06000B29 RID: 2857 RVA: 0x000255D0 File Offset: 0x000237D0
		public override IEnumerator<TOutput> GetEnumerator()
		{
			return this.GetEnumerator(null, false);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x000255ED File Offset: 0x000237ED
		public IEnumerator<TOutput> GetEnumerator(ParallelMergeOptions? mergeOptions)
		{
			return this.GetEnumerator(mergeOptions, false);
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x000255F7 File Offset: 0x000237F7
		internal bool OutputOrdered
		{
			get
			{
				return this._outputOrdered;
			}
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x000255FF File Offset: 0x000237FF
		internal virtual IEnumerator<TOutput> GetEnumerator(ParallelMergeOptions? mergeOptions, bool suppressOrderPreservation)
		{
			return new QueryOpeningEnumerator<TOutput>(this, mergeOptions, suppressOrderPreservation);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002560C File Offset: 0x0002380C
		internal IEnumerator<TOutput> GetOpenedEnumerator(ParallelMergeOptions? mergeOptions, bool suppressOrder, bool forEffect, QuerySettings querySettings)
		{
			if (querySettings.ExecutionMode.Value == ParallelExecutionMode.Default && this.LimitsParallelism)
			{
				return ExceptionAggregator.WrapEnumerable<TOutput>(this.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState).GetEnumerator();
			}
			QueryResults<TOutput> queryResults = this.GetQueryResults(querySettings);
			if (mergeOptions == null)
			{
				mergeOptions = querySettings.MergeOptions;
			}
			if (querySettings.CancellationState.MergedCancellationToken.IsCancellationRequested)
			{
				if (querySettings.CancellationState.ExternalCancellationToken.IsCancellationRequested)
				{
					throw new OperationCanceledException(querySettings.CancellationState.ExternalCancellationToken);
				}
				throw new OperationCanceledException();
			}
			else
			{
				bool flag = this.OutputOrdered && !suppressOrder;
				PartitionedStreamMerger<TOutput> partitionedStreamMerger = new PartitionedStreamMerger<TOutput>(forEffect, mergeOptions.GetValueOrDefault(), querySettings.TaskScheduler, flag, querySettings.CancellationState, querySettings.QueryId);
				queryResults.GivePartitionedStream(partitionedStreamMerger);
				if (forEffect)
				{
					return null;
				}
				return partitionedStreamMerger.MergeExecutor.GetEnumerator();
			}
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x000256FB File Offset: 0x000238FB
		private QueryResults<TOutput> GetQueryResults(QuerySettings querySettings)
		{
			return this.Open(querySettings, false);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00025708 File Offset: 0x00023908
		internal TOutput[] ExecuteAndGetResultsAsArray()
		{
			QuerySettings querySettings = base.SpecifiedQuerySettings.WithPerExecutionSettings().WithDefaults();
			QueryLifecycle.LogicalQueryExecutionBegin(querySettings.QueryId);
			TOutput[] array;
			try
			{
				if (querySettings.ExecutionMode.Value == ParallelExecutionMode.Default && this.LimitsParallelism)
				{
					array = ExceptionAggregator.WrapEnumerable<TOutput>(CancellableEnumerable.Wrap<TOutput>(this.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState).ToArray<TOutput>();
				}
				else
				{
					QueryResults<TOutput> queryResults = this.GetQueryResults(querySettings);
					if (querySettings.CancellationState.MergedCancellationToken.IsCancellationRequested)
					{
						if (querySettings.CancellationState.ExternalCancellationToken.IsCancellationRequested)
						{
							throw new OperationCanceledException(querySettings.CancellationState.ExternalCancellationToken);
						}
						throw new OperationCanceledException();
					}
					else if (queryResults.IsIndexible && this.OutputOrdered)
					{
						ArrayMergeHelper<TOutput> arrayMergeHelper = new ArrayMergeHelper<TOutput>(base.SpecifiedQuerySettings, queryResults);
						arrayMergeHelper.Execute();
						TOutput[] resultsAsArray = arrayMergeHelper.GetResultsAsArray();
						querySettings.CleanStateAtQueryEnd();
						array = resultsAsArray;
					}
					else
					{
						PartitionedStreamMerger<TOutput> partitionedStreamMerger = new PartitionedStreamMerger<TOutput>(false, ParallelMergeOptions.FullyBuffered, querySettings.TaskScheduler, this.OutputOrdered, querySettings.CancellationState, querySettings.QueryId);
						queryResults.GivePartitionedStream(partitionedStreamMerger);
						TOutput[] resultsAsArray2 = partitionedStreamMerger.MergeExecutor.GetResultsAsArray();
						querySettings.CleanStateAtQueryEnd();
						array = resultsAsArray2;
					}
				}
			}
			finally
			{
				QueryLifecycle.LogicalQueryExecutionEnd(querySettings.QueryId);
			}
			return array;
		}

		// Token: 0x06000B30 RID: 2864
		internal abstract IEnumerable<TOutput> AsSequentialQuery(CancellationToken token);

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000B31 RID: 2865
		internal abstract bool LimitsParallelism { get; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000B32 RID: 2866
		internal abstract OrdinalIndexState OrdinalIndexState { get; }

		// Token: 0x06000B33 RID: 2867 RVA: 0x00025878 File Offset: 0x00023A78
		internal static ListQueryResults<TOutput> ExecuteAndCollectResults<TKey>(PartitionedStream<TOutput, TKey> openedChild, int partitionCount, bool outputOrdered, bool useStriping, QuerySettings settings)
		{
			TaskScheduler taskScheduler = settings.TaskScheduler;
			return new ListQueryResults<TOutput>(MergeExecutor<TOutput>.Execute<TKey>(openedChild, false, ParallelMergeOptions.FullyBuffered, taskScheduler, outputOrdered, settings.CancellationState, settings.QueryId).GetResultsAsArray(), partitionCount, useStriping);
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x000258B4 File Offset: 0x00023AB4
		internal static QueryOperator<TOutput> AsQueryOperator(IEnumerable<TOutput> source)
		{
			QueryOperator<TOutput> queryOperator = source as QueryOperator<TOutput>;
			if (queryOperator == null)
			{
				OrderedParallelQuery<TOutput> orderedParallelQuery = source as OrderedParallelQuery<TOutput>;
				if (orderedParallelQuery != null)
				{
					queryOperator = orderedParallelQuery.SortOperator;
				}
				else
				{
					queryOperator = new ScanQueryOperator<TOutput>(source);
				}
			}
			return queryOperator;
		}

		// Token: 0x040006BA RID: 1722
		protected bool _outputOrdered;
	}
}
