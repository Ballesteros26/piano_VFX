using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000719 RID: 1817
	internal sealed class ICollectionDebugView<T>
	{
		// Token: 0x06003943 RID: 14659 RVA: 0x000D158E File Offset: 0x000CF78E
		public ICollectionDebugView(ICollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06003944 RID: 14660 RVA: 0x000D15AC File Offset: 0x000CF7AC
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

		// Token: 0x04002CA1 RID: 11425
		private readonly ICollection<T> _collection;
	}
}
