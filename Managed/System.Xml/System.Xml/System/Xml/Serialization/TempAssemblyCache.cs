using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x020002D9 RID: 729
	internal class TempAssemblyCache
	{
		// Token: 0x17000533 RID: 1331
		internal TempAssembly this[string ns, object o]
		{
			get
			{
				return (TempAssembly)this.cache[new TempAssemblyCacheKey(ns, o)];
			}
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x00098600 File Offset: 0x00096800
		internal void Add(string ns, object o, TempAssembly assembly)
		{
			TempAssemblyCacheKey tempAssemblyCacheKey = new TempAssemblyCacheKey(ns, o);
			lock (this)
			{
				if (this.cache[tempAssemblyCacheKey] != assembly)
				{
					Hashtable hashtable = new Hashtable();
					foreach (object obj in this.cache.Keys)
					{
						hashtable.Add(obj, this.cache[obj]);
					}
					this.cache = hashtable;
					this.cache[tempAssemblyCacheKey] = assembly;
				}
			}
		}

		// Token: 0x040015E5 RID: 5605
		private Hashtable cache = new Hashtable();
	}
}
