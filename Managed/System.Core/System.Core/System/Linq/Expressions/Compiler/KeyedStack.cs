using System;
using System.Collections.Generic;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002CE RID: 718
	internal sealed class KeyedStack<TKey, TValue> where TValue : class
	{
		// Token: 0x06001579 RID: 5497 RVA: 0x0004143C File Offset: 0x0003F63C
		internal void Push(TKey key, TValue value)
		{
			Stack<TValue> stack;
			if (!this._data.TryGetValue(key, out stack))
			{
				this._data.Add(key, stack = new Stack<TValue>());
			}
			stack.Push(value);
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x00041474 File Offset: 0x0003F674
		internal TValue TryPop(TKey key)
		{
			Stack<TValue> stack;
			TValue tvalue;
			if (!this._data.TryGetValue(key, out stack) || !stack.TryPop(out tvalue))
			{
				return default(TValue);
			}
			return tvalue;
		}

		// Token: 0x04000A32 RID: 2610
		private readonly Dictionary<TKey, Stack<TValue>> _data = new Dictionary<TKey, Stack<TValue>>();
	}
}
