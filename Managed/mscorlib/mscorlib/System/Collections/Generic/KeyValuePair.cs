using System;
using System.Text;

namespace System.Collections.Generic
{
	// Token: 0x02000A26 RID: 2598
	public static class KeyValuePair
	{
		// Token: 0x06005FDE RID: 24542 RVA: 0x0013B751 File Offset: 0x00139951
		public static KeyValuePair<TKey, TValue> Create<TKey, TValue>(TKey key, TValue value)
		{
			return new KeyValuePair<TKey, TValue>(key, value);
		}

		// Token: 0x06005FDF RID: 24543 RVA: 0x0013B75C File Offset: 0x0013995C
		internal static string PairToString(object key, object value)
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			stringBuilder.Append('[');
			if (key != null)
			{
				stringBuilder.Append(key);
			}
			stringBuilder.Append(", ");
			if (value != null)
			{
				stringBuilder.Append(value);
			}
			stringBuilder.Append(']');
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}
	}
}
