using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000111 RID: 273
	internal class RangeEnumerable : ParallelQuery<int>, IParallelPartitionable<int>
	{
		// Token: 0x0600093F RID: 2367 RVA: 0x0001D7A3 File Offset: 0x0001B9A3
		internal RangeEnumerable(int from, int count)
			: base(QuerySettings.Empty)
		{
			this._from = from;
			this._count = count;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0001D7C0 File Offset: 0x0001B9C0
		public QueryOperatorEnumerator<int, int>[] GetPartitions(int partitionCount)
		{
			int num = this._count / partitionCount;
			int num2 = this._count % partitionCount;
			int num3 = 0;
			QueryOperatorEnumerator<int, int>[] array = new QueryOperatorEnumerator<int, int>[partitionCount];
			for (int i = 0; i < partitionCount; i++)
			{
				int num4 = ((i < num2) ? (num + 1) : num);
				array[i] = new RangeEnumerable.RangeEnumerator(this._from + num3, num4, num3);
				num3 += num4;
			}
			return array;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0001D81F File Offset: 0x0001BA1F
		public override IEnumerator<int> GetEnumerator()
		{
			return new RangeEnumerable.RangeEnumerator(this._from, this._count, 0).AsClassicEnumerator();
		}

		// Token: 0x04000551 RID: 1361
		private int _from;

		// Token: 0x04000552 RID: 1362
		private int _count;

		// Token: 0x02000112 RID: 274
		private class RangeEnumerator : QueryOperatorEnumerator<int, int>
		{
			// Token: 0x06000942 RID: 2370 RVA: 0x0001D838 File Offset: 0x0001BA38
			internal RangeEnumerator(int from, int count, int initialIndex)
			{
				this._from = from;
				this._count = count;
				this._initialIndex = initialIndex;
			}

			// Token: 0x06000943 RID: 2371 RVA: 0x0001D858 File Offset: 0x0001BA58
			internal override bool MoveNext(ref int currentElement, ref int currentKey)
			{
				if (this._currentCount == null)
				{
					this._currentCount = new Shared<int>(-1);
				}
				int num = this._currentCount.Value + 1;
				if (num < this._count)
				{
					this._currentCount.Value = num;
					currentElement = num + this._from;
					currentKey = num + this._initialIndex;
					return true;
				}
				return false;
			}

			// Token: 0x06000944 RID: 2372 RVA: 0x0001D8B3 File Offset: 0x0001BAB3
			internal override void Reset()
			{
				this._currentCount = null;
			}

			// Token: 0x04000553 RID: 1363
			private readonly int _from;

			// Token: 0x04000554 RID: 1364
			private readonly int _count;

			// Token: 0x04000555 RID: 1365
			private readonly int _initialIndex;

			// Token: 0x04000556 RID: 1366
			private Shared<int> _currentCount;
		}
	}
}
