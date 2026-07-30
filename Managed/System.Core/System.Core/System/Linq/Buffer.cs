using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000E1 RID: 225
	internal struct Buffer<TElement>
	{
		// Token: 0x0600082A RID: 2090 RVA: 0x0001AF18 File Offset: 0x00019118
		internal Buffer(IEnumerable<TElement> source)
		{
			IIListProvider<TElement> iilistProvider;
			if ((iilistProvider = source as IIListProvider<TElement>) != null)
			{
				TElement[] array = iilistProvider.ToArray();
				this._items = array;
				this._count = array.Length;
				return;
			}
			this._items = EnumerableHelpers.ToArray<TElement>(source, out this._count);
		}

		// Token: 0x040004D8 RID: 1240
		internal readonly TElement[] _items;

		// Token: 0x040004D9 RID: 1241
		internal readonly int _count;
	}
}
