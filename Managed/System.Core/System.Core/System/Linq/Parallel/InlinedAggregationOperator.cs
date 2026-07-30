using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000166 RID: 358
	internal abstract class InlinedAggregationOperator<TSource, TIntermediate, TResult> : UnaryQueryOperator<TSource, TIntermediate>
	{
		// Token: 0x06000A64 RID: 2660 RVA: 0x00022C3A File Offset: 0x00020E3A
		internal InlinedAggregationOperator(IEnumerable<TSource> child)
			: base(child)
		{
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00022C44 File Offset: 0x00020E44
		internal TResult Aggregate()
		{
			Exception ex = null;
			TResult tresult;
			try
			{
				tresult = this.InternalAggregate(ref ex);
			}
			catch (Exception ex2)
			{
				if (ex2 is AggregateException)
				{
					throw;
				}
				OperationCanceledException ex3 = ex2 as OperationCanceledException;
				if (ex3 != null && ex3.CancellationToken == base.SpecifiedQuerySettings.CancellationState.ExternalCancellationToken && base.SpecifiedQuerySettings.CancellationState.ExternalCancellationToken.IsCancellationRequested)
				{
					throw;
				}
				throw new AggregateException(new Exception[] { ex2 });
			}
			if (ex != null)
			{
				throw ex;
			}
			return tresult;
		}

		// Token: 0x06000A66 RID: 2662
		protected abstract TResult InternalAggregate(ref Exception singularExceptionToThrow);

		// Token: 0x06000A67 RID: 2663 RVA: 0x0001FD40 File Offset: 0x0001DF40
		internal override QueryResults<TIntermediate> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TSource, TIntermediate>.UnaryQueryOperatorResults(base.Child.Open(settings, preferStriping), this, settings, preferStriping);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00022CD8 File Offset: 0x00020ED8
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TIntermediate> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TIntermediate, int> partitionedStream = new PartitionedStream<TIntermediate, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Correct);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = this.CreateEnumerator<TKey>(i, partitionCount, inputStream[i], null, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x06000A69 RID: 2665
		protected abstract QueryOperatorEnumerator<TIntermediate, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<TSource, TKey> source, object sharedData, CancellationToken cancellationToken);

		// Token: 0x06000A6A RID: 2666 RVA: 0x00003CCF File Offset: 0x00001ECF
		[ExcludeFromCodeCoverage]
		internal override IEnumerable<TIntermediate> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}
	}
}
