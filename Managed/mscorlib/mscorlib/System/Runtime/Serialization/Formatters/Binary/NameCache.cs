using System;
using System.Collections.Concurrent;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000741 RID: 1857
	internal sealed class NameCache
	{
		// Token: 0x06004D13 RID: 19731 RVA: 0x0011661C File Offset: 0x0011481C
		internal object GetCachedValue(string name)
		{
			this.name = name;
			object obj;
			if (!NameCache.ht.TryGetValue(name, out obj))
			{
				return null;
			}
			return obj;
		}

		// Token: 0x06004D14 RID: 19732 RVA: 0x00116642 File Offset: 0x00114842
		internal void SetCachedValue(object value)
		{
			NameCache.ht[this.name] = value;
		}

		// Token: 0x04002961 RID: 10593
		private static ConcurrentDictionary<string, object> ht = new ConcurrentDictionary<string, object>();

		// Token: 0x04002962 RID: 10594
		private string name;
	}
}
