using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000FF RID: 255
	internal sealed class OrderedPartition<TElement> : IPartition<TElement>, IIListProvider<TElement>, IEnumerable<TElement>, IEnumerable
	{
		// Token: 0x060008E8 RID: 2280 RVA: 0x0001CAA3 File Offset: 0x0001ACA3
		public OrderedPartition(OrderedEnumerable<TElement> source, int minIdxInclusive, int maxIdxInclusive)
		{
			this._source = source;
			this._minIndexInclusive = minIdxInclusive;
			this._maxIndexInclusive = maxIdxInclusive;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0001CAC0 File Offset: 0x0001ACC0
		public IEnumerator<TElement> GetEnumerator()
		{
			return this._source.GetEnumerator(this._minIndexInclusive, this._maxIndexInclusive);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001CAD9 File Offset: 0x0001ACD9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0001CAE4 File Offset: 0x0001ACE4
		public IPartition<TElement> Skip(int count)
		{
			int num = this._minIndexInclusive + count;
			if (num <= this._maxIndexInclusive)
			{
				return new OrderedPartition<TElement>(this._source, num, this._maxIndexInclusive);
			}
			return EmptyPartition<TElement>.Instance;
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0001CB20 File Offset: 0x0001AD20
		public IPartition<TElement> Take(int count)
		{
			int num = this._minIndexInclusive + count - 1;
			if (num >= this._maxIndexInclusive)
			{
				return this;
			}
			return new OrderedPartition<TElement>(this._source, this._minIndexInclusive, num);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0001CB58 File Offset: 0x0001AD58
		public TElement TryGetElementAt(int index, out bool found)
		{
			if (index <= this._maxIndexInclusive - this._minIndexInclusive)
			{
				return this._source.TryGetElementAt(index + this._minIndexInclusive, out found);
			}
			found = false;
			return default(TElement);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0001CB96 File Offset: 0x0001AD96
		public TElement TryGetFirst(out bool found)
		{
			return this._source.TryGetElementAt(this._minIndexInclusive, out found);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0001CBAA File Offset: 0x0001ADAA
		public TElement TryGetLast(out bool found)
		{
			return this._source.TryGetLast(this._minIndexInclusive, this._maxIndexInclusive, out found);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0001CBC4 File Offset: 0x0001ADC4
		public TElement[] ToArray()
		{
			return this._source.ToArray(this._minIndexInclusive, this._maxIndexInclusive);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001CBDD File Offset: 0x0001ADDD
		public List<TElement> ToList()
		{
			return this._source.ToList(this._minIndexInclusive, this._maxIndexInclusive);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0001CBF6 File Offset: 0x0001ADF6
		public int GetCount(bool onlyIfCheap)
		{
			return this._source.GetCount(this._minIndexInclusive, this._maxIndexInclusive, onlyIfCheap);
		}

		// Token: 0x04000526 RID: 1318
		private readonly OrderedEnumerable<TElement> _source;

		// Token: 0x04000527 RID: 1319
		private readonly int _minIndexInclusive;

		// Token: 0x04000528 RID: 1320
		private readonly int _maxIndexInclusive;
	}
}
