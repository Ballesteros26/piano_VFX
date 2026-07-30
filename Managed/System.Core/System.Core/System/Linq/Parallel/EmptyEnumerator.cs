using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000109 RID: 265
	internal class EmptyEnumerator<T> : QueryOperatorEnumerator<T, int>, IEnumerator<T>, IDisposable, IEnumerator
	{
		// Token: 0x06000928 RID: 2344 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool MoveNext(ref T currentElement, ref int currentKey)
		{
			return false;
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x0001D694 File Offset: 0x0001B894
		public T Current
		{
			get
			{
				return default(T);
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x00005E51 File Offset: 0x00004051
		object IEnumerator.Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00002285 File Offset: 0x00000485
		public bool MoveNext()
		{
			return false;
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00003C4C File Offset: 0x00001E4C
		void IEnumerator.Reset()
		{
		}
	}
}
