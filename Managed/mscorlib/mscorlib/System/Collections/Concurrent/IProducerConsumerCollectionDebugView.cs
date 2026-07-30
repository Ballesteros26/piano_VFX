using System;
using System.Diagnostics;

namespace System.Collections.Concurrent
{
	// Token: 0x02000A06 RID: 2566
	internal sealed class IProducerConsumerCollectionDebugView<T>
	{
		// Token: 0x06005F51 RID: 24401 RVA: 0x0013A492 File Offset: 0x00138692
		public IProducerConsumerCollectionDebugView(IProducerConsumerCollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x06005F52 RID: 24402 RVA: 0x0013A4AF File Offset: 0x001386AF
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._collection.ToArray();
			}
		}

		// Token: 0x0400300A RID: 12298
		private readonly IProducerConsumerCollection<T> _collection;
	}
}
