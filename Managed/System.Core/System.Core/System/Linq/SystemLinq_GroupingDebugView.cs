using System;
using System.Diagnostics;

namespace System.Linq
{
	// Token: 0x020000E5 RID: 229
	internal sealed class SystemLinq_GroupingDebugView<TKey, TElement>
	{
		// Token: 0x06000831 RID: 2097 RVA: 0x0001B02C File Offset: 0x0001922C
		public SystemLinq_GroupingDebugView(Grouping<TKey, TElement> grouping)
		{
			this._grouping = grouping;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x0001B03B File Offset: 0x0001923B
		public TKey Key
		{
			get
			{
				return this._grouping.Key;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x0001B048 File Offset: 0x00019248
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TElement[] Values
		{
			get
			{
				TElement[] array;
				if ((array = this._cachedValues) == null)
				{
					array = (this._cachedValues = this._grouping.ToArray<TElement>());
				}
				return array;
			}
		}

		// Token: 0x040004DC RID: 1244
		private readonly Grouping<TKey, TElement> _grouping;

		// Token: 0x040004DD RID: 1245
		private TElement[] _cachedValues;
	}
}
