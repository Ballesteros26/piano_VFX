using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001A5 RID: 421
	internal sealed class ScanQueryOperator<TElement> : QueryOperator<TElement>
	{
		// Token: 0x06000B6C RID: 2924 RVA: 0x00025DEC File Offset: 0x00023FEC
		internal ScanQueryOperator(IEnumerable<TElement> data)
			: base(false, QuerySettings.Empty)
		{
			ParallelEnumerableWrapper<TElement> parallelEnumerableWrapper = data as ParallelEnumerableWrapper<TElement>;
			if (parallelEnumerableWrapper != null)
			{
				data = parallelEnumerableWrapper.WrappedEnumerable;
			}
			this._data = data;
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x00025E1E File Offset: 0x0002401E
		public IEnumerable<TElement> Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00025E28 File Offset: 0x00024028
		internal override QueryResults<TElement> Open(QuerySettings settings, bool preferStriping)
		{
			IList<TElement> list = this._data as IList<TElement>;
			if (list != null)
			{
				return new ListQueryResults<TElement>(list, settings.DegreeOfParallelism.GetValueOrDefault(), preferStriping);
			}
			return new ScanQueryOperator<TElement>.ScanEnumerableQueryOperatorResults(this._data, settings);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00025E67 File Offset: 0x00024067
		internal override IEnumerator<TElement> GetEnumerator(ParallelMergeOptions? mergeOptions, bool suppressOrderPreservation)
		{
			return this._data.GetEnumerator();
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00025E1E File Offset: 0x0002401E
		internal override IEnumerable<TElement> AsSequentialQuery(CancellationToken token)
		{
			return this._data;
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x00025E74 File Offset: 0x00024074
		internal override OrdinalIndexState OrdinalIndexState
		{
			get
			{
				if (!(this._data is IList<TElement>))
				{
					return OrdinalIndexState.Correct;
				}
				return OrdinalIndexState.Indexable;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040006C7 RID: 1735
		private readonly IEnumerable<TElement> _data;

		// Token: 0x020001A6 RID: 422
		private class ScanEnumerableQueryOperatorResults : QueryResults<TElement>
		{
			// Token: 0x06000B73 RID: 2931 RVA: 0x00025E86 File Offset: 0x00024086
			internal ScanEnumerableQueryOperatorResults(IEnumerable<TElement> data, QuerySettings settings)
			{
				this._data = data;
				this._settings = settings;
			}

			// Token: 0x06000B74 RID: 2932 RVA: 0x00025E9C File Offset: 0x0002409C
			internal override void GivePartitionedStream(IPartitionedStreamRecipient<TElement> recipient)
			{
				PartitionedStream<TElement, int> partitionedStream = ExchangeUtilities.PartitionDataSource<TElement>(this._data, this._settings.DegreeOfParallelism.Value, false);
				recipient.Receive<int>(partitionedStream);
			}

			// Token: 0x040006C8 RID: 1736
			private IEnumerable<TElement> _data;

			// Token: 0x040006C9 RID: 1737
			private QuerySettings _settings;
		}
	}
}
