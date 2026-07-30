using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000A3E RID: 2622
	internal sealed class Mscorlib_CollectionDebugView<T>
	{
		// Token: 0x0600608F RID: 24719 RVA: 0x0013DFF3 File Offset: 0x0013C1F3
		public Mscorlib_CollectionDebugView(ICollection<T> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			this.collection = collection;
		}

		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x06006090 RID: 24720 RVA: 0x0013E00C File Offset: 0x0013C20C
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				T[] array = new T[this.collection.Count];
				this.collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04003089 RID: 12425
		private ICollection<T> collection;
	}
}
