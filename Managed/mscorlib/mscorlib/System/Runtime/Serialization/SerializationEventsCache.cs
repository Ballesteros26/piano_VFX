using System;
using System.Collections;

namespace System.Runtime.Serialization
{
	// Token: 0x020006EC RID: 1772
	internal static class SerializationEventsCache
	{
		// Token: 0x06004AA2 RID: 19106 RVA: 0x0010B46C File Offset: 0x0010966C
		internal static SerializationEvents GetSerializationEventsForType(Type t)
		{
			SerializationEvents serializationEvents;
			if ((serializationEvents = (SerializationEvents)SerializationEventsCache.cache[t]) == null)
			{
				object syncRoot = SerializationEventsCache.cache.SyncRoot;
				lock (syncRoot)
				{
					if ((serializationEvents = (SerializationEvents)SerializationEventsCache.cache[t]) == null)
					{
						serializationEvents = new SerializationEvents(t);
						SerializationEventsCache.cache[t] = serializationEvents;
					}
				}
			}
			return serializationEvents;
		}

		// Token: 0x04002702 RID: 9986
		private static Hashtable cache = new Hashtable();
	}
}
