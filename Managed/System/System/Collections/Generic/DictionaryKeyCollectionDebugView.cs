using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x0200071B RID: 1819
	internal sealed class DictionaryKeyCollectionDebugView<TKey, TValue>
	{
		// Token: 0x06003947 RID: 14663 RVA: 0x000D1624 File Offset: 0x000CF824
		public DictionaryKeyCollectionDebugView(ICollection<TKey> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06003948 RID: 14664 RVA: 0x000D1644 File Offset: 0x000CF844
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TKey[] Items
		{
			get
			{
				TKey[] array = new TKey[this._collection.Count];
				this._collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04002CA3 RID: 11427
		private readonly ICollection<TKey> _collection;
	}
}
