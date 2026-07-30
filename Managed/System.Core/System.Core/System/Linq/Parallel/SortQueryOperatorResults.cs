using System;

namespace System.Linq.Parallel
{
	// Token: 0x020001DC RID: 476
	internal class SortQueryOperatorResults<TInputOutput, TSortKey> : QueryResults<TInputOutput>
	{
		// Token: 0x06000C3E RID: 3134 RVA: 0x00028A6F File Offset: 0x00026C6F
		internal SortQueryOperatorResults(QueryResults<TInputOutput> childQueryResults, SortQueryOperator<TInputOutput, TSortKey> op, QuerySettings settings)
		{
			this._childQueryResults = childQueryResults;
			this._op = op;
			this._settings = settings;
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool IsIndexible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00028A8C File Offset: 0x00026C8C
		internal override void GivePartitionedStream(IPartitionedStreamRecipient<TInputOutput> recipient)
		{
			this._childQueryResults.GivePartitionedStream(new SortQueryOperatorResults<TInputOutput, TSortKey>.ChildResultsRecipient(recipient, this._op, this._settings));
		}

		// Token: 0x0400076D RID: 1901
		protected QueryResults<TInputOutput> _childQueryResults;

		// Token: 0x0400076E RID: 1902
		private SortQueryOperator<TInputOutput, TSortKey> _op;

		// Token: 0x0400076F RID: 1903
		private QuerySettings _settings;

		// Token: 0x020001DD RID: 477
		private class ChildResultsRecipient : IPartitionedStreamRecipient<TInputOutput>
		{
			// Token: 0x06000C41 RID: 3137 RVA: 0x00028AAB File Offset: 0x00026CAB
			internal ChildResultsRecipient(IPartitionedStreamRecipient<TInputOutput> outputRecipient, SortQueryOperator<TInputOutput, TSortKey> op, QuerySettings settings)
			{
				this._outputRecipient = outputRecipient;
				this._op = op;
				this._settings = settings;
			}

			// Token: 0x06000C42 RID: 3138 RVA: 0x00028AC8 File Offset: 0x00026CC8
			public void Receive<TKey>(PartitionedStream<TInputOutput, TKey> childPartitionedStream)
			{
				this._op.WrapPartitionedStream<TKey>(childPartitionedStream, this._outputRecipient, false, this._settings);
			}

			// Token: 0x04000770 RID: 1904
			private IPartitionedStreamRecipient<TInputOutput> _outputRecipient;

			// Token: 0x04000771 RID: 1905
			private SortQueryOperator<TInputOutput, TSortKey> _op;

			// Token: 0x04000772 RID: 1906
			private QuerySettings _settings;
		}
	}
}
