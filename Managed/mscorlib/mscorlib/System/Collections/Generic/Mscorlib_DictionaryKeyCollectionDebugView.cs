using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000A3F RID: 2623
	internal sealed class Mscorlib_DictionaryKeyCollectionDebugView<TKey, TValue>
	{
		// Token: 0x06006091 RID: 24721 RVA: 0x0013E038 File Offset: 0x0013C238
		public Mscorlib_DictionaryKeyCollectionDebugView(ICollection<TKey> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			this.collection = collection;
		}

		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x06006092 RID: 24722 RVA: 0x0013E050 File Offset: 0x0013C250
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TKey[] Items
		{
			get
			{
				TKey[] array = new TKey[this.collection.Count];
				this.collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x0400308A RID: 12426
		private ICollection<TKey> collection;
	}
}
