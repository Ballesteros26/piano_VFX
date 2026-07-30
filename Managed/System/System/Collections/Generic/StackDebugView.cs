using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x0200074A RID: 1866
	internal sealed class StackDebugView<T>
	{
		// Token: 0x06003B42 RID: 15170 RVA: 0x000D7B17 File Offset: 0x000D5D17
		public StackDebugView(Stack<T> stack)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack");
			}
			this._stack = stack;
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06003B43 RID: 15171 RVA: 0x000D7B34 File Offset: 0x000D5D34
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._stack.ToArray();
			}
		}

		// Token: 0x04002D43 RID: 11587
		private readonly Stack<T> _stack;
	}
}
