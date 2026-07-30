using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000A34 RID: 2612
	internal sealed class IDictionaryDebugView<K, V>
	{
		// Token: 0x0600606D RID: 24685 RVA: 0x0013DCA6 File Offset: 0x0013BEA6
		public IDictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._dict = dictionary;
		}

		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x0600606E RID: 24686 RVA: 0x0013DCC4 File Offset: 0x0013BEC4
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

		// Token: 0x04003082 RID: 12418
		private readonly IDictionary<K, V> _dict;
	}
}
