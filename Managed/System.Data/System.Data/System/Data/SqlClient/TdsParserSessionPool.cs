using System;
using System.Collections.Generic;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x02000225 RID: 549
	internal class TdsParserSessionPool
	{
		// Token: 0x0600189D RID: 6301 RVA: 0x0007D72A File Offset: 0x0007B92A
		internal TdsParserSessionPool(TdsParser parser)
		{
			this._parser = parser;
			this._cache = new List<TdsParserStateObject>();
			this._freeStateObjects = new TdsParserStateObject[10];
			this._freeStateObjectCount = 0;
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x0007D758 File Offset: 0x0007B958
		private bool IsDisposed
		{
			get
			{
				return this._freeStateObjects == null;
			}
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x0007D764 File Offset: 0x0007B964
		internal void Deactivate()
		{
			List<TdsParserStateObject> cache = this._cache;
			lock (cache)
			{
				for (int i = this._cache.Count - 1; i >= 0; i--)
				{
					TdsParserStateObject tdsParserStateObject = this._cache[i];
					if (tdsParserStateObject != null && tdsParserStateObject.IsOrphaned)
					{
						this.PutSession(tdsParserStateObject);
					}
				}
			}
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x0007D7D8 File Offset: 0x0007B9D8
		internal void Dispose()
		{
			List<TdsParserStateObject> cache = this._cache;
			lock (cache)
			{
				for (int i = 0; i < this._freeStateObjectCount; i++)
				{
					if (this._freeStateObjects[i] != null)
					{
						this._freeStateObjects[i].Dispose();
					}
				}
				this._freeStateObjects = null;
				this._freeStateObjectCount = 0;
				for (int j = 0; j < this._cache.Count; j++)
				{
					if (this._cache[j] != null)
					{
						if (this._cache[j].IsOrphaned)
						{
							this._cache[j].Dispose();
						}
						else
						{
							this._cache[j].DecrementPendingCallbacks(false);
						}
					}
				}
				this._cache.Clear();
				this._cachedCount = 0;
			}
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x0007D8B8 File Offset: 0x0007BAB8
		internal TdsParserStateObject GetSession(object owner)
		{
			List<TdsParserStateObject> cache = this._cache;
			TdsParserStateObject tdsParserStateObject;
			lock (cache)
			{
				if (this.IsDisposed)
				{
					throw ADP.ClosedConnectionError();
				}
				if (this._freeStateObjectCount > 0)
				{
					this._freeStateObjectCount--;
					tdsParserStateObject = this._freeStateObjects[this._freeStateObjectCount];
					this._freeStateObjects[this._freeStateObjectCount] = null;
				}
				else
				{
					tdsParserStateObject = this._parser.CreateSession();
					this._cache.Add(tdsParserStateObject);
					this._cachedCount = this._cache.Count;
				}
				tdsParserStateObject.Activate(owner);
			}
			return tdsParserStateObject;
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x0007D968 File Offset: 0x0007BB68
		internal void PutSession(TdsParserStateObject session)
		{
			bool flag = session.Deactivate();
			List<TdsParserStateObject> cache = this._cache;
			lock (cache)
			{
				if (this.IsDisposed)
				{
					session.Dispose();
				}
				else if (flag && this._freeStateObjectCount < 10)
				{
					this._freeStateObjects[this._freeStateObjectCount] = session;
					this._freeStateObjectCount++;
				}
				else
				{
					this._cache.Remove(session);
					this._cachedCount = this._cache.Count;
					session.Dispose();
				}
				session.RemoveOwner();
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x0007DA10 File Offset: 0x0007BC10
		internal int ActiveSessionsCount
		{
			get
			{
				return this._cachedCount - this._freeStateObjectCount;
			}
		}

		// Token: 0x040011BA RID: 4538
		private const int MaxInactiveCount = 10;

		// Token: 0x040011BB RID: 4539
		private readonly TdsParser _parser;

		// Token: 0x040011BC RID: 4540
		private readonly List<TdsParserStateObject> _cache;

		// Token: 0x040011BD RID: 4541
		private int _cachedCount;

		// Token: 0x040011BE RID: 4542
		private TdsParserStateObject[] _freeStateObjects;

		// Token: 0x040011BF RID: 4543
		private int _freeStateObjectCount;
	}
}
