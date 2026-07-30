using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x020004AE RID: 1198
	internal sealed class InterlockedStack
	{
		// Token: 0x06002345 RID: 9029 RVA: 0x00088972 File Offset: 0x00086B72
		internal InterlockedStack()
		{
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x00088988 File Offset: 0x00086B88
		internal void Push(object pooledStream)
		{
			if (pooledStream == null)
			{
				throw new ArgumentNullException("pooledStream");
			}
			object syncRoot = this._stack.SyncRoot;
			lock (syncRoot)
			{
				this._stack.Push(pooledStream);
			}
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x000889E4 File Offset: 0x00086BE4
		internal object Pop()
		{
			object syncRoot = this._stack.SyncRoot;
			object obj2;
			lock (syncRoot)
			{
				object obj = null;
				if (0 < this._stack.Count)
				{
					obj = this._stack.Pop();
				}
				obj2 = obj;
			}
			return obj2;
		}

		// Token: 0x04001F7C RID: 8060
		private readonly Stack _stack = new Stack();
	}
}
