using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001E5 RID: 485
	internal sealed class WhereQueryOperator<TInputOutput> : UnaryQueryOperator<TInputOutput, TInputOutput>
	{
		// Token: 0x06000C60 RID: 3168 RVA: 0x00029729 File Offset: 0x00027929
		internal WhereQueryOperator(IEnumerable<TInputOutput> child, Func<TInputOutput, bool> predicate)
			: base(child)
		{
			base.SetOrdinalIndexState(base.Child.OrdinalIndexState.Worse(OrdinalIndexState.Increasing));
			this._predicate = predicate;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00029750 File Offset: 0x00027950
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInputOutput, TKey> inputStream, IPartitionedStreamRecipient<TInputOutput> recipient, bool preferStriping, QuerySettings settings)
		{
			PartitionedStream<TInputOutput, TKey> partitionedStream = new PartitionedStream<TInputOutput, TKey>(inputStream.PartitionCount, inputStream.KeyComparer, this.OrdinalIndexState);
			for (int i = 0; i < inputStream.PartitionCount; i++)
			{
				partitionedStream[i] = new WhereQueryOperator<TInputOutput>.WhereQueryOperatorEnumerator<TKey>(inputStream[i], this._predicate, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x000262B7 File Offset: 0x000244B7
		internal override QueryResults<TInputOutput> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TInputOutput, TInputOutput>.UnaryQueryOperatorResults(base.Child.Open(settings, preferStriping), this, settings, preferStriping);
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x000297B2 File Offset: 0x000279B2
		internal override IEnumerable<TInputOutput> AsSequentialQuery(CancellationToken token)
		{
			return CancellableEnumerable.Wrap<TInputOutput>(base.Child.AsSequentialQuery(token), token).Where(this._predicate);
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000796 RID: 1942
		private Func<TInputOutput, bool> _predicate;

		// Token: 0x020001E6 RID: 486
		private class WhereQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TInputOutput, TKey>
		{
			// Token: 0x06000C65 RID: 3173 RVA: 0x000297D1 File Offset: 0x000279D1
			internal WhereQueryOperatorEnumerator(QueryOperatorEnumerator<TInputOutput, TKey> source, Func<TInputOutput, bool> predicate, CancellationToken cancellationToken)
			{
				this._source = source;
				this._predicate = predicate;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000C66 RID: 3174 RVA: 0x000297F0 File Offset: 0x000279F0
			internal override bool MoveNext(ref TInputOutput currentElement, ref TKey currentKey)
			{
				if (this._outputLoopCount == null)
				{
					this._outputLoopCount = new Shared<int>(0);
				}
				while (this._source.MoveNext(ref currentElement, ref currentKey))
				{
					Shared<int> outputLoopCount = this._outputLoopCount;
					int value = outputLoopCount.Value;
					outputLoopCount.Value = value + 1;
					if ((value & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this._cancellationToken);
					}
					if (this._predicate(currentElement))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000C67 RID: 3175 RVA: 0x0002985E File Offset: 0x00027A5E
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000797 RID: 1943
			private readonly QueryOperatorEnumerator<TInputOutput, TKey> _source;

			// Token: 0x04000798 RID: 1944
			private readonly Func<TInputOutput, bool> _predicate;

			// Token: 0x04000799 RID: 1945
			private CancellationToken _cancellationToken;

			// Token: 0x0400079A RID: 1946
			private Shared<int> _outputLoopCount;
		}
	}
}
