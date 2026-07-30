using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UnityEngine.Serialization
{
	// Token: 0x02000268 RID: 616
	internal class DictionarySerializationSurrogate<TKey, TValue> : ISerializationSurrogate
	{
		// Token: 0x060019DF RID: 6623 RVA: 0x0002A4F8 File Offset: 0x000286F8
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			Dictionary<TKey, TValue> dictionary = (Dictionary<TKey, TValue>)obj;
			dictionary.GetObjectData(info, context);
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x0002A518 File Offset: 0x00028718
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			IEqualityComparer<TKey> equalityComparer = (IEqualityComparer<TKey>)info.GetValue("Comparer", typeof(IEqualityComparer<TKey>));
			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(equalityComparer);
			bool flag = info.MemberCount > 3;
			if (flag)
			{
				KeyValuePair<TKey, TValue>[] array = (KeyValuePair<TKey, TValue>[])info.GetValue("KeyValuePairs", typeof(KeyValuePair<TKey, TValue>[]));
				bool flag2 = array != null;
				if (flag2)
				{
					foreach (KeyValuePair<TKey, TValue> keyValuePair in array)
					{
						dictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			return dictionary;
		}
	}
}
