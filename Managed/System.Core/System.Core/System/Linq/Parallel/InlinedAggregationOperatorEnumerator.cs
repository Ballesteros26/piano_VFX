using System;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000167 RID: 359
	internal abstract class InlinedAggregationOperatorEnumerator<TIntermediate> : QueryOperatorEnumerator<TIntermediate, int>
	{
		// Token: 0x06000A6C RID: 2668 RVA: 0x00022D2F File Offset: 0x00020F2F
		internal InlinedAggregationOperatorEnumerator(int partitionIndex, CancellationToken cancellationToken)
		{
			this._partitionIndex = partitionIndex;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00022D45 File Offset: 0x00020F45
		internal sealed override bool MoveNext(ref TIntermediate currentElement, ref int currentKey)
		{
			if (!this._done && this.MoveNextCore(ref currentElement))
			{
				currentKey = this._partitionIndex;
				this._done = true;
				return true;
			}
			return false;
		}

		// Token: 0x06000A6E RID: 2670
		protected abstract bool MoveNextCore(ref TIntermediate currentElement);

		// Token: 0x0400066D RID: 1645
		private int _partitionIndex;

		// Token: 0x0400066E RID: 1646
		private bool _done;

		// Token: 0x0400066F RID: 1647
		protected CancellationToken _cancellationToken;
	}
}
