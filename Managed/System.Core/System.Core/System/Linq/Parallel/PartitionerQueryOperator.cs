using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000199 RID: 409
	internal class PartitionerQueryOperator<TElement> : QueryOperator<TElement>
	{
		// Token: 0x06000B07 RID: 2823 RVA: 0x0002500B File Offset: 0x0002320B
		internal PartitionerQueryOperator(Partitioner<TElement> partitioner)
			: base(false, QuerySettings.Empty)
		{
			this._partitioner = partitioner;
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x00025020 File Offset: 0x00023220
		internal bool Orderable
		{
			get
			{
				return this._partitioner is OrderablePartitioner<TElement>;
			}
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00025030 File Offset: 0x00023230
		internal override QueryResults<TElement> Open(QuerySettings settings, bool preferStriping)
		{
			return new PartitionerQueryOperator<TElement>.PartitionerQueryOperatorResults(this._partitioner, settings);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0002503E File Offset: 0x0002323E
		internal override IEnumerable<TElement> AsSequentialQuery(CancellationToken token)
		{
			using (IEnumerator<TElement> enumerator = this._partitioner.GetPartitions(1)[0])
			{
				while (enumerator.MoveNext())
				{
					TElement telement = enumerator.Current;
					yield return telement;
				}
			}
			IEnumerator<TElement> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0002504E File Offset: 0x0002324E
		internal override OrdinalIndexState OrdinalIndexState
		{
			get
			{
				return PartitionerQueryOperator<TElement>.GetOrdinalIndexState(this._partitioner);
			}
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0002505C File Offset: 0x0002325C
		internal static OrdinalIndexState GetOrdinalIndexState(Partitioner<TElement> partitioner)
		{
			OrderablePartitioner<TElement> orderablePartitioner = partitioner as OrderablePartitioner<TElement>;
			if (orderablePartitioner == null)
			{
				return OrdinalIndexState.Shuffled;
			}
			if (!orderablePartitioner.KeysOrderedInEachPartition)
			{
				return OrdinalIndexState.Shuffled;
			}
			if (orderablePartitioner.KeysNormalized)
			{
				return OrdinalIndexState.Correct;
			}
			return OrdinalIndexState.Increasing;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040006A7 RID: 1703
		private Partitioner<TElement> _partitioner;

		// Token: 0x0200019A RID: 410
		private class PartitionerQueryOperatorResults : QueryResults<TElement>
		{
			// Token: 0x06000B0E RID: 2830 RVA: 0x0002508A File Offset: 0x0002328A
			internal PartitionerQueryOperatorResults(Partitioner<TElement> partitioner, QuerySettings settings)
			{
				this._partitioner = partitioner;
				this._settings = settings;
			}

			// Token: 0x06000B0F RID: 2831 RVA: 0x000250A0 File Offset: 0x000232A0
			internal override void GivePartitionedStream(IPartitionedStreamRecipient<TElement> recipient)
			{
				int value = this._settings.DegreeOfParallelism.Value;
				OrderablePartitioner<TElement> orderablePartitioner = this._partitioner as OrderablePartitioner<TElement>;
				OrdinalIndexState ordinalIndexState = ((orderablePartitioner != null) ? PartitionerQueryOperator<TElement>.GetOrdinalIndexState(orderablePartitioner) : OrdinalIndexState.Shuffled);
				PartitionedStream<TElement, int> partitionedStream = new PartitionedStream<TElement, int>(value, Util.GetDefaultComparer<int>(), ordinalIndexState);
				if (orderablePartitioner != null)
				{
					IList<IEnumerator<KeyValuePair<long, TElement>>> orderablePartitions = orderablePartitioner.GetOrderablePartitions(value);
					if (orderablePartitions == null)
					{
						throw new InvalidOperationException("Partitioner returned null instead of a list of partitions.");
					}
					if (orderablePartitions.Count != value)
					{
						throw new InvalidOperationException("Partitioner returned a wrong number of partitions.");
					}
					for (int i = 0; i < value; i++)
					{
						IEnumerator<KeyValuePair<long, TElement>> enumerator = orderablePartitions[i];
						if (enumerator == null)
						{
							throw new InvalidOperationException("Partitioner returned a null partition.");
						}
						partitionedStream[i] = new PartitionerQueryOperator<TElement>.OrderablePartitionerEnumerator(enumerator);
					}
				}
				else
				{
					IList<IEnumerator<TElement>> partitions = this._partitioner.GetPartitions(value);
					if (partitions == null)
					{
						throw new InvalidOperationException("Partitioner returned null instead of a list of partitions.");
					}
					if (partitions.Count != value)
					{
						throw new InvalidOperationException("Partitioner returned a wrong number of partitions.");
					}
					for (int j = 0; j < value; j++)
					{
						IEnumerator<TElement> enumerator2 = partitions[j];
						if (enumerator2 == null)
						{
							throw new InvalidOperationException("Partitioner returned a null partition.");
						}
						partitionedStream[j] = new PartitionerQueryOperator<TElement>.PartitionerEnumerator(enumerator2);
					}
				}
				recipient.Receive<int>(partitionedStream);
			}

			// Token: 0x040006A8 RID: 1704
			private Partitioner<TElement> _partitioner;

			// Token: 0x040006A9 RID: 1705
			private QuerySettings _settings;
		}

		// Token: 0x0200019B RID: 411
		private class OrderablePartitionerEnumerator : QueryOperatorEnumerator<TElement, int>
		{
			// Token: 0x06000B10 RID: 2832 RVA: 0x000251C5 File Offset: 0x000233C5
			internal OrderablePartitionerEnumerator(IEnumerator<KeyValuePair<long, TElement>> sourceEnumerator)
			{
				this._sourceEnumerator = sourceEnumerator;
			}

			// Token: 0x06000B11 RID: 2833 RVA: 0x000251D4 File Offset: 0x000233D4
			internal override bool MoveNext(ref TElement currentElement, ref int currentKey)
			{
				if (!this._sourceEnumerator.MoveNext())
				{
					return false;
				}
				KeyValuePair<long, TElement> keyValuePair = this._sourceEnumerator.Current;
				currentElement = keyValuePair.Value;
				currentKey = checked((int)keyValuePair.Key);
				return true;
			}

			// Token: 0x06000B12 RID: 2834 RVA: 0x00025214 File Offset: 0x00023414
			protected override void Dispose(bool disposing)
			{
				this._sourceEnumerator.Dispose();
			}

			// Token: 0x040006AA RID: 1706
			private IEnumerator<KeyValuePair<long, TElement>> _sourceEnumerator;
		}

		// Token: 0x0200019C RID: 412
		private class PartitionerEnumerator : QueryOperatorEnumerator<TElement, int>
		{
			// Token: 0x06000B13 RID: 2835 RVA: 0x00025221 File Offset: 0x00023421
			internal PartitionerEnumerator(IEnumerator<TElement> sourceEnumerator)
			{
				this._sourceEnumerator = sourceEnumerator;
			}

			// Token: 0x06000B14 RID: 2836 RVA: 0x00025230 File Offset: 0x00023430
			internal override bool MoveNext(ref TElement currentElement, ref int currentKey)
			{
				if (!this._sourceEnumerator.MoveNext())
				{
					return false;
				}
				currentElement = this._sourceEnumerator.Current;
				currentKey = 0;
				return true;
			}

			// Token: 0x06000B15 RID: 2837 RVA: 0x00025256 File Offset: 0x00023456
			protected override void Dispose(bool disposing)
			{
				this._sourceEnumerator.Dispose();
			}

			// Token: 0x040006AB RID: 1707
			private IEnumerator<TElement> _sourceEnumerator;
		}
	}
}
