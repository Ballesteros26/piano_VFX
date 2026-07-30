using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000A42 RID: 2626
	internal sealed class Mscorlib_KeyedCollectionDebugView<K, T>
	{
		// Token: 0x06006097 RID: 24727 RVA: 0x0013E104 File Offset: 0x0013C304
		public Mscorlib_KeyedCollectionDebugView(KeyedCollection<K, T> keyedCollection)
		{
			if (keyedCollection == null)
			{
				throw new ArgumentNullException("keyedCollection");
			}
			this.kc = keyedCollection;
		}

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x06006098 RID: 24728 RVA: 0x0013E124 File Offset: 0x0013C324
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				T[] array = new T[this.kc.Count];
				this.kc.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x0400308D RID: 12429
		private KeyedCollection<K, T> kc;
	}
}
