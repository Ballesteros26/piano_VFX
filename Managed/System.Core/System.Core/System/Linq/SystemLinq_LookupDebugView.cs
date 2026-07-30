using System;
using System.Diagnostics;

namespace System.Linq
{
	// Token: 0x020000E6 RID: 230
	internal sealed class SystemLinq_LookupDebugView<TKey, TElement>
	{
		// Token: 0x06000834 RID: 2100 RVA: 0x0001B073 File Offset: 0x00019273
		public SystemLinq_LookupDebugView(Lookup<TKey, TElement> lookup)
		{
			this._lookup = lookup;
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x0001B084 File Offset: 0x00019284
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public IGrouping<TKey, TElement>[] Groupings
		{
			get
			{
				IGrouping<TKey, TElement>[] array;
				if ((array = this._cachedGroupings) == null)
				{
					array = (this._cachedGroupings = this._lookup.ToArray<IGrouping<TKey, TElement>>());
				}
				return array;
			}
		}

		// Token: 0x040004DE RID: 1246
		private readonly Lookup<TKey, TElement> _lookup;

		// Token: 0x040004DF RID: 1247
		private IGrouping<TKey, TElement>[] _cachedGroupings;
	}
}
