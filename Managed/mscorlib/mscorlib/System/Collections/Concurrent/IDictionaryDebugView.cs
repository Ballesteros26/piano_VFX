using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Collections.Concurrent
{
	// Token: 0x020009FC RID: 2556
	internal sealed class IDictionaryDebugView<K, V>
	{
		// Token: 0x06005EFE RID: 24318 RVA: 0x00138F74 File Offset: 0x00137174
		public IDictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._dictionary = dictionary;
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x06005EFF RID: 24319 RVA: 0x00138F94 File Offset: 0x00137194
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public KeyValuePair<K, V>[] Items
		{
			get
			{
				KeyValuePair<K, V>[] array = new KeyValuePair<K, V>[this._dictionary.Count];
				this._dictionary.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04002FE3 RID: 12259
		private readonly IDictionary<K, V> _dictionary;
	}
}
