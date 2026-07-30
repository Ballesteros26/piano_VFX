using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001C1 RID: 449
	internal class GroupByGrouping<TGroupKey, TElement> : IGrouping<TGroupKey, TElement>, IEnumerable<TElement>, IEnumerable
	{
		// Token: 0x06000BD1 RID: 3025 RVA: 0x00027564 File Offset: 0x00025764
		internal GroupByGrouping(KeyValuePair<Wrapper<TGroupKey>, ListChunk<TElement>> keyValues)
		{
			this._keyValues = keyValues;
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x00027573 File Offset: 0x00025773
		TGroupKey IGrouping<TGroupKey, TElement>.Key
		{
			get
			{
				return this._keyValues.Key.Value;
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00027585 File Offset: 0x00025785
		IEnumerator<TElement> IEnumerable<TElement>.GetEnumerator()
		{
			return this._keyValues.Value.GetEnumerator();
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00027597 File Offset: 0x00025797
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<TElement>)this).GetEnumerator();
		}

		// Token: 0x04000719 RID: 1817
		private KeyValuePair<Wrapper<TGroupKey>, ListChunk<TElement>> _keyValues;
	}
}
