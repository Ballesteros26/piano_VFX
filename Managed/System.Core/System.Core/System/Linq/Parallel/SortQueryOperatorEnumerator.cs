using System;

namespace System.Linq.Parallel
{
	// Token: 0x020001DE RID: 478
	internal class SortQueryOperatorEnumerator<TInputOutput, TKey, TSortKey> : QueryOperatorEnumerator<TInputOutput, TSortKey>
	{
		// Token: 0x06000C43 RID: 3139 RVA: 0x00028AE3 File Offset: 0x00026CE3
		internal SortQueryOperatorEnumerator(QueryOperatorEnumerator<TInputOutput, TKey> source, Func<TInputOutput, TSortKey> keySelector)
		{
			this._source = source;
			this._keySelector = keySelector;
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00028AFC File Offset: 0x00026CFC
		internal override bool MoveNext(ref TInputOutput currentElement, ref TSortKey currentKey)
		{
			TKey tkey = default(TKey);
			if (!this._source.MoveNext(ref currentElement, ref tkey))
			{
				return false;
			}
			currentKey = this._keySelector(currentElement);
			return true;
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00028B3B File Offset: 0x00026D3B
		protected override void Dispose(bool disposing)
		{
			this._source.Dispose();
		}

		// Token: 0x04000773 RID: 1907
		private readonly QueryOperatorEnumerator<TInputOutput, TKey> _source;

		// Token: 0x04000774 RID: 1908
		private readonly Func<TInputOutput, TSortKey> _keySelector;
	}
}
