using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x0200010E RID: 270
	internal class ParallelEnumerableWrapper<T> : ParallelQuery<T>
	{
		// Token: 0x0600093B RID: 2363 RVA: 0x0001D767 File Offset: 0x0001B967
		internal ParallelEnumerableWrapper(IEnumerable<T> wrappedEnumerable)
			: base(QuerySettings.Empty)
		{
			this._wrappedEnumerable = wrappedEnumerable;
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x0001D77B File Offset: 0x0001B97B
		internal IEnumerable<T> WrappedEnumerable
		{
			get
			{
				return this._wrappedEnumerable;
			}
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0001D783 File Offset: 0x0001B983
		public override IEnumerator<T> GetEnumerator()
		{
			return this._wrappedEnumerable.GetEnumerator();
		}

		// Token: 0x0400054B RID: 1355
		private readonly IEnumerable<T> _wrappedEnumerable;
	}
}
