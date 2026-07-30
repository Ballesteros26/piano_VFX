using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001E7 RID: 487
	internal abstract class UnaryQueryOperator<TInput, TOutput> : QueryOperator<TOutput>
	{
		// Token: 0x06000C68 RID: 3176 RVA: 0x0002986B File Offset: 0x00027A6B
		internal UnaryQueryOperator(IEnumerable<TInput> child)
			: this(QueryOperator<TInput>.AsQueryOperator(child))
		{
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00029879 File Offset: 0x00027A79
		internal UnaryQueryOperator(IEnumerable<TInput> child, bool outputOrdered)
			: this(QueryOperator<TInput>.AsQueryOperator(child), outputOrdered)
		{
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00029888 File Offset: 0x00027A88
		private UnaryQueryOperator(QueryOperator<TInput> child)
			: this(child, child.OutputOrdered, child.SpecifiedQuerySettings)
		{
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0002989D File Offset: 0x00027A9D
		internal UnaryQueryOperator(QueryOperator<TInput> child, bool outputOrdered)
			: this(child, outputOrdered, child.SpecifiedQuerySettings)
		{
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x000298AD File Offset: 0x00027AAD
		private UnaryQueryOperator(QueryOperator<TInput> child, bool outputOrdered, QuerySettings settings)
			: base(outputOrdered, settings)
		{
			this._child = child;
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x000298C5 File Offset: 0x00027AC5
		internal QueryOperator<TInput> Child
		{
			get
			{
				return this._child;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x000298CD File Offset: 0x00027ACD
		internal sealed override OrdinalIndexState OrdinalIndexState
		{
			get
			{
				return this._indexState;
			}
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x000298D5 File Offset: 0x00027AD5
		protected void SetOrdinalIndexState(OrdinalIndexState indexState)
		{
			this._indexState = indexState;
		}

		// Token: 0x06000C70 RID: 3184
		internal abstract void WrapPartitionedStream<TKey>(PartitionedStream<TInput, TKey> inputStream, IPartitionedStreamRecipient<TOutput> recipient, bool preferStriping, QuerySettings settings);

		// Token: 0x0400079B RID: 1947
		private readonly QueryOperator<TInput> _child;

		// Token: 0x0400079C RID: 1948
		private OrdinalIndexState _indexState = OrdinalIndexState.Shuffled;

		// Token: 0x020001E8 RID: 488
		internal class UnaryQueryOperatorResults : QueryResults<TOutput>
		{
			// Token: 0x06000C71 RID: 3185 RVA: 0x000298DE File Offset: 0x00027ADE
			internal UnaryQueryOperatorResults(QueryResults<TInput> childQueryResults, UnaryQueryOperator<TInput, TOutput> op, QuerySettings settings, bool preferStriping)
			{
				this._childQueryResults = childQueryResults;
				this._op = op;
				this._settings = settings;
				this._preferStriping = preferStriping;
			}

			// Token: 0x06000C72 RID: 3186 RVA: 0x00029904 File Offset: 0x00027B04
			internal override void GivePartitionedStream(IPartitionedStreamRecipient<TOutput> recipient)
			{
				if (this._settings.ExecutionMode.Value == ParallelExecutionMode.Default && this._op.LimitsParallelism)
				{
					PartitionedStream<TOutput, int> partitionedStream = ExchangeUtilities.PartitionDataSource<TOutput>(this._op.AsSequentialQuery(this._settings.CancellationState.ExternalCancellationToken), this._settings.DegreeOfParallelism.Value, this._preferStriping);
					recipient.Receive<int>(partitionedStream);
					return;
				}
				if (this.IsIndexible)
				{
					PartitionedStream<TOutput, int> partitionedStream2 = ExchangeUtilities.PartitionDataSource<TOutput>(this, this._settings.DegreeOfParallelism.Value, this._preferStriping);
					recipient.Receive<int>(partitionedStream2);
					return;
				}
				this._childQueryResults.GivePartitionedStream(new UnaryQueryOperator<TInput, TOutput>.UnaryQueryOperatorResults.ChildResultsRecipient(recipient, this._op, this._preferStriping, this._settings));
			}

			// Token: 0x0400079D RID: 1949
			protected QueryResults<TInput> _childQueryResults;

			// Token: 0x0400079E RID: 1950
			private UnaryQueryOperator<TInput, TOutput> _op;

			// Token: 0x0400079F RID: 1951
			private QuerySettings _settings;

			// Token: 0x040007A0 RID: 1952
			private bool _preferStriping;

			// Token: 0x020001E9 RID: 489
			private class ChildResultsRecipient : IPartitionedStreamRecipient<TInput>
			{
				// Token: 0x06000C73 RID: 3187 RVA: 0x000299C8 File Offset: 0x00027BC8
				internal ChildResultsRecipient(IPartitionedStreamRecipient<TOutput> outputRecipient, UnaryQueryOperator<TInput, TOutput> op, bool preferStriping, QuerySettings settings)
				{
					this._outputRecipient = outputRecipient;
					this._op = op;
					this._preferStriping = preferStriping;
					this._settings = settings;
				}

				// Token: 0x06000C74 RID: 3188 RVA: 0x000299ED File Offset: 0x00027BED
				public void Receive<TKey>(PartitionedStream<TInput, TKey> inputStream)
				{
					this._op.WrapPartitionedStream<TKey>(inputStream, this._outputRecipient, this._preferStriping, this._settings);
				}

				// Token: 0x040007A1 RID: 1953
				private IPartitionedStreamRecipient<TOutput> _outputRecipient;

				// Token: 0x040007A2 RID: 1954
				private UnaryQueryOperator<TInput, TOutput> _op;

				// Token: 0x040007A3 RID: 1955
				private bool _preferStriping;

				// Token: 0x040007A4 RID: 1956
				private QuerySettings _settings;
			}
		}
	}
}
