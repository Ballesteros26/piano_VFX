using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x0200010D RID: 269
	internal class ParallelEnumerableWrapper : ParallelQuery<object>
	{
		// Token: 0x06000938 RID: 2360 RVA: 0x0001D734 File Offset: 0x0001B934
		internal ParallelEnumerableWrapper(IEnumerable source)
			: base(QuerySettings.Empty)
		{
			this._source = source;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0001D748 File Offset: 0x0001B948
		internal override IEnumerator GetEnumeratorUntyped()
		{
			return this._source.GetEnumerator();
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0001D755 File Offset: 0x0001B955
		public override IEnumerator<object> GetEnumerator()
		{
			return new EnumerableWrapperWeakToStrong(this._source).GetEnumerator();
		}

		// Token: 0x0400054A RID: 1354
		private readonly IEnumerable _source;
	}
}
