using System;

namespace System.Linq.Parallel
{
	// Token: 0x0200014E RID: 334
	internal abstract class BinaryQueryOperator<TLeftInput, TRightInput, TOutput> : QueryOperator<TOutput>
	{
		// Token: 0x06000A1B RID: 2587 RVA: 0x00021D17 File Offset: 0x0001FF17
		internal BinaryQueryOperator(ParallelQuery<TLeftInput> leftChild, ParallelQuery<TRightInput> rightChild)
			: this(QueryOperator<TLeftInput>.AsQueryOperator(leftChild), QueryOperator<TRightInput>.AsQueryOperator(rightChild))
		{
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00021D2C File Offset: 0x0001FF2C
		internal BinaryQueryOperator(QueryOperator<TLeftInput> leftChild, QueryOperator<TRightInput> rightChild)
			: base(false, leftChild.SpecifiedQuerySettings.Merge(rightChild.SpecifiedQuerySettings))
		{
			this._leftChild = leftChild;
			this._rightChild = rightChild;
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x00021D69 File Offset: 0x0001FF69
		internal QueryOperator<TLeftInput> LeftChild
		{
			get
			{
				return this._leftChild;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x00021D71 File Offset: 0x0001FF71
		internal QueryOperator<TRightInput> RightChild
		{
			get
			{
				return this._rightChild;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x00021D79 File Offset: 0x0001FF79
		internal sealed override OrdinalIndexState OrdinalIndexState
		{
			get
			{
				return this._indexState;
			}
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00021D81 File Offset: 0x0001FF81
		protected void SetOrdinalIndex(OrdinalIndexState indexState)
		{
			this._indexState = indexState;
		}

		// Token: 0x06000A21 RID: 2593
		public abstract void WrapPartitionedStream<TLeftKey, TRightKey>(PartitionedStream<TLeftInput, TLeftKey> leftPartitionedStream, PartitionedStream<TRightInput, TRightKey> rightPartitionedStream, IPartitionedStreamRecipient<TOutput> outputRecipient, bool preferStriping, QuerySettings settings);

		// Token: 0x0400064C RID: 1612
		private readonly QueryOperator<TLeftInput> _leftChild;

		// Token: 0x0400064D RID: 1613
		private readonly QueryOperator<TRightInput> _rightChild;

		// Token: 0x0400064E RID: 1614
		private OrdinalIndexState _indexState = OrdinalIndexState.Shuffled;

		// Token: 0x0200014F RID: 335
		internal class BinaryQueryOperatorResults : QueryResults<TOutput>
		{
			// Token: 0x06000A22 RID: 2594 RVA: 0x00021D8A File Offset: 0x0001FF8A
			internal BinaryQueryOperatorResults(QueryResults<TLeftInput> leftChildQueryResults, QueryResults<TRightInput> rightChildQueryResults, BinaryQueryOperator<TLeftInput, TRightInput, TOutput> op, QuerySettings settings, bool preferStriping)
			{
				this._leftChildQueryResults = leftChildQueryResults;
				this._rightChildQueryResults = rightChildQueryResults;
				this._op = op;
				this._settings = settings;
				this._preferStriping = preferStriping;
			}

			// Token: 0x06000A23 RID: 2595 RVA: 0x00021DB8 File Offset: 0x0001FFB8
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
				this._leftChildQueryResults.GivePartitionedStream(new BinaryQueryOperator<TLeftInput, TRightInput, TOutput>.BinaryQueryOperatorResults.LeftChildResultsRecipient(recipient, this, this._preferStriping, this._settings));
			}

			// Token: 0x0400064F RID: 1615
			protected QueryResults<TLeftInput> _leftChildQueryResults;

			// Token: 0x04000650 RID: 1616
			protected QueryResults<TRightInput> _rightChildQueryResults;

			// Token: 0x04000651 RID: 1617
			private BinaryQueryOperator<TLeftInput, TRightInput, TOutput> _op;

			// Token: 0x04000652 RID: 1618
			private QuerySettings _settings;

			// Token: 0x04000653 RID: 1619
			private bool _preferStriping;

			// Token: 0x02000150 RID: 336
			private class LeftChildResultsRecipient : IPartitionedStreamRecipient<TLeftInput>
			{
				// Token: 0x06000A24 RID: 2596 RVA: 0x00021E77 File Offset: 0x00020077
				internal LeftChildResultsRecipient(IPartitionedStreamRecipient<TOutput> outputRecipient, BinaryQueryOperator<TLeftInput, TRightInput, TOutput>.BinaryQueryOperatorResults results, bool preferStriping, QuerySettings settings)
				{
					this._outputRecipient = outputRecipient;
					this._results = results;
					this._preferStriping = preferStriping;
					this._settings = settings;
				}

				// Token: 0x06000A25 RID: 2597 RVA: 0x00021E9C File Offset: 0x0002009C
				public void Receive<TLeftKey>(PartitionedStream<TLeftInput, TLeftKey> source)
				{
					BinaryQueryOperator<TLeftInput, TRightInput, TOutput>.BinaryQueryOperatorResults.RightChildResultsRecipient<TLeftKey> rightChildResultsRecipient = new BinaryQueryOperator<TLeftInput, TRightInput, TOutput>.BinaryQueryOperatorResults.RightChildResultsRecipient<TLeftKey>(this._outputRecipient, this._results._op, source, this._preferStriping, this._settings);
					this._results._rightChildQueryResults.GivePartitionedStream(rightChildResultsRecipient);
				}

				// Token: 0x04000654 RID: 1620
				private IPartitionedStreamRecipient<TOutput> _outputRecipient;

				// Token: 0x04000655 RID: 1621
				private BinaryQueryOperator<TLeftInput, TRightInput, TOutput>.BinaryQueryOperatorResults _results;

				// Token: 0x04000656 RID: 1622
				private bool _preferStriping;

				// Token: 0x04000657 RID: 1623
				private QuerySettings _settings;
			}

			// Token: 0x02000151 RID: 337
			private class RightChildResultsRecipient<TLeftKey> : IPartitionedStreamRecipient<TRightInput>
			{
				// Token: 0x06000A26 RID: 2598 RVA: 0x00021EDE File Offset: 0x000200DE
				internal RightChildResultsRecipient(IPartitionedStreamRecipient<TOutput> outputRecipient, BinaryQueryOperator<TLeftInput, TRightInput, TOutput> op, PartitionedStream<TLeftInput, TLeftKey> leftPartitionedStream, bool preferStriping, QuerySettings settings)
				{
					this._outputRecipient = outputRecipient;
					this._op = op;
					this._preferStriping = preferStriping;
					this._leftPartitionedStream = leftPartitionedStream;
					this._settings = settings;
				}

				// Token: 0x06000A27 RID: 2599 RVA: 0x00021F0B File Offset: 0x0002010B
				public void Receive<TRightKey>(PartitionedStream<TRightInput, TRightKey> rightPartitionedStream)
				{
					this._op.WrapPartitionedStream<TLeftKey, TRightKey>(this._leftPartitionedStream, rightPartitionedStream, this._outputRecipient, this._preferStriping, this._settings);
				}

				// Token: 0x04000658 RID: 1624
				private IPartitionedStreamRecipient<TOutput> _outputRecipient;

				// Token: 0x04000659 RID: 1625
				private PartitionedStream<TLeftInput, TLeftKey> _leftPartitionedStream;

				// Token: 0x0400065A RID: 1626
				private BinaryQueryOperator<TLeftInput, TRightInput, TOutput> _op;

				// Token: 0x0400065B RID: 1627
				private bool _preferStriping;

				// Token: 0x0400065C RID: 1628
				private QuerySettings _settings;
			}
		}
	}
}
