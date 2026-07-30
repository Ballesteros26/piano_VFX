using System;
using System.Diagnostics;

namespace System.Collections.Concurrent
{
	// Token: 0x020006EB RID: 1771
	internal sealed class BlockingCollectionDebugView<T>
	{
		// Token: 0x0600377B RID: 14203 RVA: 0x000CC5CF File Offset: 0x000CA7CF
		public BlockingCollectionDebugView(BlockingCollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._blockingCollection = collection;
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x0600377C RID: 14204 RVA: 0x000CC5EC File Offset: 0x000CA7EC
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._blockingCollection.ToArray();
			}
		}

		// Token: 0x04002C02 RID: 11266
		private readonly BlockingCollection<T> _blockingCollection;
	}
}
