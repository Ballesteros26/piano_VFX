using System;
using System.Collections;

namespace System.Web.Services.Protocols
{
	// Token: 0x0200001B RID: 27
	internal class ClientTypeCache
	{
		// Token: 0x17000024 RID: 36
		internal object this[Type key]
		{
			get
			{
				return this.cache[key];
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002CEC File Offset: 0x00000EEC
		internal void Add(Type key, object value)
		{
			lock (this)
			{
				if (this.cache[key] != value)
				{
					Hashtable hashtable = new Hashtable();
					foreach (object obj in this.cache.Keys)
					{
						hashtable.Add(obj, this.cache[obj]);
					}
					this.cache = hashtable;
					this.cache[key] = value;
				}
			}
		}

		// Token: 0x040001A6 RID: 422
		private Hashtable cache = new Hashtable();
	}
}
