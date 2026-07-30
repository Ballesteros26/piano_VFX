using System;

namespace System.Collections.Generic
{
	// Token: 0x02000A25 RID: 2597
	public static class CollectionExtensions
	{
		// Token: 0x06005FDA RID: 24538 RVA: 0x0013B6B8 File Offset: 0x001398B8
		public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
		{
			return dictionary.GetValueOrDefault(key, default(TValue));
		}

		// Token: 0x06005FDB RID: 24539 RVA: 0x0013B6D8 File Offset: 0x001398D8
		public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			TValue tvalue;
			if (!dictionary.TryGetValue(key, out tvalue))
			{
				return defaultValue;
			}
			return tvalue;
		}

		// Token: 0x06005FDC RID: 24540 RVA: 0x0013B701 File Offset: 0x00139901
		public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			if (!dictionary.ContainsKey(key))
			{
				dictionary.Add(key, value);
				return true;
			}
			return false;
		}

		// Token: 0x06005FDD RID: 24541 RVA: 0x0013B725 File Offset: 0x00139925
		public static bool Remove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			if (dictionary.TryGetValue(key, out value))
			{
				dictionary.Remove(key);
				return true;
			}
			value = default(TValue);
			return false;
		}
	}
}
