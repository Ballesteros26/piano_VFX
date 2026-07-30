using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000A41 RID: 2625
	internal sealed class Mscorlib_DictionaryDebugView<K, V>
	{
		// Token: 0x06006095 RID: 24725 RVA: 0x0013E0C0 File Offset: 0x0013C2C0
		public Mscorlib_DictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
			}
			this.dict = dictionary;
		}

		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x06006096 RID: 24726 RVA: 0x0013E0D8 File Offset: 0x0013C2D8
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public KeyValuePair<K, V>[] Items
		{
			get
			{
				KeyValuePair<K, V>[] array = new KeyValuePair<K, V>[this.dict.Count];
				this.dict.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x0400308C RID: 12428
		private IDictionary<K, V> dict;
	}
}
