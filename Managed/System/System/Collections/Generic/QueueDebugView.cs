using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000722 RID: 1826
	internal sealed class QueueDebugView<T>
	{
		// Token: 0x060039A0 RID: 14752 RVA: 0x000D294A File Offset: 0x000D0B4A
		public QueueDebugView(Queue<T> queue)
		{
			if (queue == null)
			{
				throw new ArgumentNullException("queue");
			}
			this._queue = queue;
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x060039A1 RID: 14753 RVA: 0x000D2967 File Offset: 0x000D0B67
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._queue.ToArray();
			}
		}

		// Token: 0x04002CC6 RID: 11462
		private readonly Queue<T> _queue;
	}
}
