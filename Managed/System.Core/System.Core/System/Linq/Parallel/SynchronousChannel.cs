using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000107 RID: 263
	internal sealed class SynchronousChannel<T>
	{
		// Token: 0x0600091E RID: 2334 RVA: 0x00002320 File Offset: 0x00000520
		internal SynchronousChannel()
		{
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0001D604 File Offset: 0x0001B804
		internal void Init()
		{
			this._queue = new Queue<T>();
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0001D611 File Offset: 0x0001B811
		internal void Enqueue(T item)
		{
			this._queue.Enqueue(item);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0001D61F File Offset: 0x0001B81F
		internal T Dequeue()
		{
			return this._queue.Dequeue();
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00003C4C File Offset: 0x00001E4C
		internal void SetDone()
		{
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0001D62C File Offset: 0x0001B82C
		internal void CopyTo(T[] array, int arrayIndex)
		{
			this._queue.CopyTo(array, arrayIndex);
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x0001D63B File Offset: 0x0001B83B
		internal int Count
		{
			get
			{
				return this._queue.Count;
			}
		}

		// Token: 0x04000545 RID: 1349
		private Queue<T> _queue;
	}
}
