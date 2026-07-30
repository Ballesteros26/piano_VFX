using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000355 RID: 853
	internal sealed class ICollectionDebugView<T>
	{
		// Token: 0x06001A01 RID: 6657 RVA: 0x00056068 File Offset: 0x00054268
		public ICollectionDebugView(ICollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x00056088 File Offset: 0x00054288
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				T[] array = new T[this._collection.Count];
				this._collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04000B8C RID: 2956
		private readonly ICollection<T> _collection;
	}
}
