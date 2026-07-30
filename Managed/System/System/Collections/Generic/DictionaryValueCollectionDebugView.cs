using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x0200071C RID: 1820
	internal sealed class DictionaryValueCollectionDebugView<TKey, TValue>
	{
		// Token: 0x06003949 RID: 14665 RVA: 0x000D1670 File Offset: 0x000CF870
		public DictionaryValueCollectionDebugView(ICollection<TValue> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x0600394A RID: 14666 RVA: 0x000D1690 File Offset: 0x000CF890
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TValue[] Items
		{
			get
			{
				TValue[] array = new TValue[this._collection.Count];
				this._collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04002CA4 RID: 11428
		private readonly ICollection<TValue> _collection;
	}
}
