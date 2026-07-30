using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000A36 RID: 2614
	internal sealed class DictionaryValueCollectionDebugView<TKey, TValue>
	{
		// Token: 0x06006071 RID: 24689 RVA: 0x0013DD3C File Offset: 0x0013BF3C
		public DictionaryValueCollectionDebugView(ICollection<TValue> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x06006072 RID: 24690 RVA: 0x0013DD5C File Offset: 0x0013BF5C
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

		// Token: 0x04003084 RID: 12420
		private readonly ICollection<TValue> _collection;
	}
}
