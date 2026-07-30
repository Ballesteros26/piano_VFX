using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000135 RID: 309
	internal class PartitionedStream<TElement, TKey>
	{
		// Token: 0x060009AC RID: 2476 RVA: 0x0001FB3A File Offset: 0x0001DD3A
		internal PartitionedStream(int partitionCount, IComparer<TKey> keyComparer, OrdinalIndexState indexState)
		{
			this._partitions = new QueryOperatorEnumerator<TElement, TKey>[partitionCount];
			this._keyComparer = keyComparer;
			this._indexState = indexState;
		}

		// Token: 0x17000137 RID: 311
		internal QueryOperatorEnumerator<TElement, TKey> this[int index]
		{
			get
			{
				return this._partitions[index];
			}
			set
			{
				this._partitions[index] = value;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0001FB71 File Offset: 0x0001DD71
		public int PartitionCount
		{
			get
			{
				return this._partitions.Length;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0001FB7B File Offset: 0x0001DD7B
		internal IComparer<TKey> KeyComparer
		{
			get
			{
				return this._keyComparer;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0001FB83 File Offset: 0x0001DD83
		internal OrdinalIndexState OrdinalIndexState
		{
			get
			{
				return this._indexState;
			}
		}

		// Token: 0x040005DF RID: 1503
		protected QueryOperatorEnumerator<TElement, TKey>[] _partitions;

		// Token: 0x040005E0 RID: 1504
		private readonly IComparer<TKey> _keyComparer;

		// Token: 0x040005E1 RID: 1505
		private readonly OrdinalIndexState _indexState;
	}
}
