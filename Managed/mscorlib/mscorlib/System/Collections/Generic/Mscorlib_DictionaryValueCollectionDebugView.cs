using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000A40 RID: 2624
	internal sealed class Mscorlib_DictionaryValueCollectionDebugView<TKey, TValue>
	{
		// Token: 0x06006093 RID: 24723 RVA: 0x0013E07C File Offset: 0x0013C27C
		public Mscorlib_DictionaryValueCollectionDebugView(ICollection<TValue> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			this.collection = collection;
		}

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x06006094 RID: 24724 RVA: 0x0013E094 File Offset: 0x0013C294
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TValue[] Items
		{
			get
			{
				TValue[] array = new TValue[this.collection.Count];
				this.collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x0400308B RID: 12427
		private ICollection<TValue> collection;
	}
}
