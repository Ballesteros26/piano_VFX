using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000A35 RID: 2613
	internal sealed class DictionaryKeyCollectionDebugView<TKey, TValue>
	{
		// Token: 0x0600606F RID: 24687 RVA: 0x0013DCF0 File Offset: 0x0013BEF0
		public DictionaryKeyCollectionDebugView(ICollection<TKey> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x06006070 RID: 24688 RVA: 0x0013DD10 File Offset: 0x0013BF10
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

		// Token: 0x04003083 RID: 12419
		private readonly ICollection<TKey> _collection;
	}
}
