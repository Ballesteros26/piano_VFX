using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x0200071A RID: 1818
	internal sealed class IDictionaryDebugView<K, V>
	{
		// Token: 0x06003945 RID: 14661 RVA: 0x000D15D8 File Offset: 0x000CF7D8
		public IDictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._dict = dictionary;
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06003946 RID: 14662 RVA: 0x000D15F8 File Offset: 0x000CF7F8
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public KeyValuePair<K, V>[] Items
		{
			get
			{
				KeyValuePair<K, V>[] array = new KeyValuePair<K, V>[this._dict.Count];
				this._dict.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04002CA2 RID: 11426
		private readonly IDictionary<K, V> _dict;
	}
}
