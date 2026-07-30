using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.ModelBinding
{
	// Token: 0x02000519 RID: 1305
	internal static class DictionaryHelpers
	{
		// Token: 0x060039D5 RID: 14805 RVA: 0x0009CCF2 File Offset: 0x0009AEF2
		public static IEnumerable<KeyValuePair<string, TValue>> FindKeysWithPrefix<TValue>(IDictionary<string, TValue> dictionary, string prefix)
		{
			TValue tvalue;
			if (dictionary.TryGetValue(prefix, out tvalue))
			{
				yield return new KeyValuePair<string, TValue>(prefix, tvalue);
			}
			foreach (KeyValuePair<string, TValue> keyValuePair in dictionary)
			{
				string key = keyValuePair.Key;
				if (key.Length > prefix.Length && key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
				{
					char c = key[prefix.Length];
					if (c == '.' || c == '[')
					{
						yield return keyValuePair;
					}
				}
			}
			IEnumerator<KeyValuePair<string, TValue>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060039D6 RID: 14806 RVA: 0x0009CD09 File Offset: 0x0009AF09
		public static bool DoesAnyKeyHavePrefix<TValue>(IDictionary<string, TValue> dictionary, string prefix)
		{
			return DictionaryHelpers.FindKeysWithPrefix<TValue>(dictionary, prefix).Any<KeyValuePair<string, TValue>>();
		}
	}
}
