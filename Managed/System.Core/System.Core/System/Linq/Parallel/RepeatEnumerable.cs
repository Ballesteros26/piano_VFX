using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000113 RID: 275
	internal class RepeatEnumerable<TResult> : ParallelQuery<TResult>, IParallelPartitionable<TResult>
	{
		// Token: 0x06000945 RID: 2373 RVA: 0x0001D8BC File Offset: 0x0001BABC
		internal RepeatEnumerable(TResult element, int count)
			: base(QuerySettings.Empty)
		{
			this._element = element;
			this._count = count;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0001D8D8 File Offset: 0x0001BAD8
		public QueryOperatorEnumerator<TResult, int>[] GetPartitions(int partitionCount)
		{
			int num = (this._count + partitionCount - 1) / partitionCount;
			QueryOperatorEnumerator<TResult, int>[] array = new QueryOperatorEnumerator<TResult, int>[partitionCount];
			int i = 0;
			int num2 = 0;
			while (i < partitionCount)
			{
				if (num2 + num > this._count)
				{
					array[i] = new RepeatEnumerable<TResult>.RepeatEnumerator(this._element, (num2 < this._count) ? (this._count - num2) : 0, num2);
				}
				else
				{
					array[i] = new RepeatEnumerable<TResult>.RepeatEnumerator(this._element, num, num2);
				}
				i++;
				num2 += num;
			}
			return array;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0001D94C File Offset: 0x0001BB4C
		public override IEnumerator<TResult> GetEnumerator()
		{
			return new RepeatEnumerable<TResult>.RepeatEnumerator(this._element, this._count, 0).AsClassicEnumerator();
		}

		// Token: 0x04000557 RID: 1367
		private TResult _element;

		// Token: 0x04000558 RID: 1368
		private int _count;

		// Token: 0x02000114 RID: 276
		private class RepeatEnumerator : QueryOperatorEnumerator<TResult, int>
		{
			// Token: 0x06000948 RID: 2376 RVA: 0x0001D965 File Offset: 0x0001BB65
			internal RepeatEnumerator(TResult element, int count, int indexOffset)
			{
				this._element = element;
				this._count = count;
				this._indexOffset = indexOffset;
			}

			// Token: 0x06000949 RID: 2377 RVA: 0x0001D984 File Offset: 0x0001BB84
			internal override bool MoveNext(ref TResult currentElement, ref int currentKey)
			{
				if (this._currentIndex == null)
				{
					this._currentIndex = new Shared<int>(-1);
				}
				if (this._currentIndex.Value < this._count - 1)
				{
					this._currentIndex.Value++;
					currentElement = this._element;
					currentKey = this._currentIndex.Value + this._indexOffset;
					return true;
				}
				return false;
			}

			// Token: 0x0600094A RID: 2378 RVA: 0x0001D9F0 File Offset: 0x0001BBF0
			internal override void Reset()
			{
				this._currentIndex = null;
			}

			// Token: 0x04000559 RID: 1369
			private readonly TResult _element;

			// Token: 0x0400055A RID: 1370
			private readonly int _count;

			// Token: 0x0400055B RID: 1371
			private readonly int _indexOffset;

			// Token: 0x0400055C RID: 1372
			private Shared<int> _currentIndex;
		}
	}
}
