using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x020000F2 RID: 242
	internal sealed class Listeners<TElem> where TElem : class
	{
		// Token: 0x06000CC6 RID: 3270 RVA: 0x0003B7FD File Offset: 0x000399FD
		internal Listeners(int ObjectID, Listeners<TElem>.Func<TElem, bool> notifyFilter)
		{
			this._listeners = new List<TElem>();
			this._filter = notifyFilter;
			this._objectID = ObjectID;
			this._listenerReaderCount = 0;
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0003B825 File Offset: 0x00039A25
		internal bool HasListeners
		{
			get
			{
				return 0 < this._listeners.Count;
			}
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0003B835 File Offset: 0x00039A35
		internal void Add(TElem listener)
		{
			this._listeners.Add(listener);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0003B843 File Offset: 0x00039A43
		internal int IndexOfReference(TElem listener)
		{
			return Index.IndexOfReference<TElem>(this._listeners, listener);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0003B854 File Offset: 0x00039A54
		internal void Remove(TElem listener)
		{
			int num = this.IndexOfReference(listener);
			this._listeners[num] = default(TElem);
			if (this._listenerReaderCount == 0)
			{
				this._listeners.RemoveAt(num);
				this._listeners.TrimExcess();
			}
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0003B8A0 File Offset: 0x00039AA0
		internal void Notify<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3, Listeners<TElem>.Action<TElem, T1, T2, T3> action)
		{
			int count = this._listeners.Count;
			if (0 < count)
			{
				int num = -1;
				this._listenerReaderCount++;
				try
				{
					for (int i = 0; i < count; i++)
					{
						TElem telem = this._listeners[i];
						if (this._filter(telem))
						{
							action(telem, arg1, arg2, arg3);
						}
						else
						{
							this._listeners[i] = default(TElem);
							num = i;
						}
					}
				}
				finally
				{
					this._listenerReaderCount--;
				}
				if (this._listenerReaderCount == 0)
				{
					this.RemoveNullListeners(num);
				}
			}
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0003B94C File Offset: 0x00039B4C
		private void RemoveNullListeners(int nullIndex)
		{
			int num = nullIndex;
			while (0 <= num)
			{
				if (this._listeners[num] == null)
				{
					this._listeners.RemoveAt(num);
				}
				num--;
			}
		}

		// Token: 0x0400086D RID: 2157
		private readonly List<TElem> _listeners;

		// Token: 0x0400086E RID: 2158
		private readonly Listeners<TElem>.Func<TElem, bool> _filter;

		// Token: 0x0400086F RID: 2159
		private readonly int _objectID;

		// Token: 0x04000870 RID: 2160
		private int _listenerReaderCount;

		// Token: 0x020000F3 RID: 243
		// (Invoke) Token: 0x06000CCE RID: 3278
		internal delegate void Action<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

		// Token: 0x020000F4 RID: 244
		// (Invoke) Token: 0x06000CD2 RID: 3282
		internal delegate TResult Func<T1, TResult>(T1 arg1);
	}
}
