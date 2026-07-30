using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001A0 RID: 416
	internal abstract class QueryOperatorEnumerator<TElement, TKey>
	{
		// Token: 0x06000B35 RID: 2869
		internal abstract bool MoveNext(ref TElement currentElement, ref TKey currentKey);

		// Token: 0x06000B36 RID: 2870 RVA: 0x000258E6 File Offset: 0x00023AE6
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00003C4C File Offset: 0x00001E4C
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00003C4C File Offset: 0x00001E4C
		internal virtual void Reset()
		{
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x000258EF File Offset: 0x00023AEF
		internal IEnumerator<TElement> AsClassicEnumerator()
		{
			return new QueryOperatorEnumerator<TElement, TKey>.QueryOperatorClassicEnumerator(this);
		}

		// Token: 0x020001A1 RID: 417
		private class QueryOperatorClassicEnumerator : IEnumerator<TElement>, IDisposable, IEnumerator
		{
			// Token: 0x06000B3B RID: 2875 RVA: 0x000258F7 File Offset: 0x00023AF7
			internal QueryOperatorClassicEnumerator(QueryOperatorEnumerator<TElement, TKey> operatorEnumerator)
			{
				this._operatorEnumerator = operatorEnumerator;
			}

			// Token: 0x06000B3C RID: 2876 RVA: 0x00025908 File Offset: 0x00023B08
			public bool MoveNext()
			{
				TKey tkey = default(TKey);
				return this._operatorEnumerator.MoveNext(ref this._current, ref tkey);
			}

			// Token: 0x1700015F RID: 351
			// (get) Token: 0x06000B3D RID: 2877 RVA: 0x00025930 File Offset: 0x00023B30
			public TElement Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x17000160 RID: 352
			// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00025938 File Offset: 0x00023B38
			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x06000B3F RID: 2879 RVA: 0x00025945 File Offset: 0x00023B45
			public void Dispose()
			{
				this._operatorEnumerator.Dispose();
				this._operatorEnumerator = null;
			}

			// Token: 0x06000B40 RID: 2880 RVA: 0x00025959 File Offset: 0x00023B59
			public void Reset()
			{
				this._operatorEnumerator.Reset();
			}

			// Token: 0x040006BB RID: 1723
			private QueryOperatorEnumerator<TElement, TKey> _operatorEnumerator;

			// Token: 0x040006BC RID: 1724
			private TElement _current;
		}
	}
}
