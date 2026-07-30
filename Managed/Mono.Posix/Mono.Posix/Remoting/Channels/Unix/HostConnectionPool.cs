using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Threading;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x0200008C RID: 140
	internal class HostConnectionPool
	{
		// Token: 0x060006C4 RID: 1732 RVA: 0x0000F152 File Offset: 0x0000D352
		public HostConnectionPool(string path)
		{
			this._path = path;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0000F16C File Offset: 0x0000D36C
		public UnixConnection GetConnection()
		{
			UnixConnection unixConnection = null;
			ArrayList pool = this._pool;
			lock (pool)
			{
				for (;;)
				{
					if (this._pool.Count <= 0)
					{
						goto IL_006A;
					}
					unixConnection = (UnixConnection)this._pool[this._pool.Count - 1];
					this._pool.RemoveAt(this._pool.Count - 1);
					if (unixConnection.IsAlive)
					{
						goto IL_006A;
					}
					this.CancelConnection(unixConnection);
					unixConnection = null;
					IL_008B:
					if (unixConnection != null)
					{
						break;
					}
					continue;
					IL_006A:
					if (unixConnection == null && this._activeConnections < UnixConnectionPool.MaxOpenConnections)
					{
						break;
					}
					if (unixConnection == null)
					{
						Monitor.Wait(this._pool);
						goto IL_008B;
					}
					goto IL_008B;
				}
			}
			if (unixConnection == null)
			{
				return this.CreateConnection();
			}
			return unixConnection;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0000F230 File Offset: 0x0000D430
		private UnixConnection CreateConnection()
		{
			UnixConnection unixConnection2;
			try
			{
				ReusableUnixClient reusableUnixClient = new ReusableUnixClient(this._path);
				UnixConnection unixConnection = new UnixConnection(this, reusableUnixClient);
				this._activeConnections++;
				unixConnection2 = unixConnection;
			}
			catch (Exception ex)
			{
				throw new RemotingException(ex.Message);
			}
			return unixConnection2;
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0000F280 File Offset: 0x0000D480
		public void ReleaseConnection(UnixConnection entry)
		{
			ArrayList pool = this._pool;
			lock (pool)
			{
				entry.ControlTime = DateTime.UtcNow;
				this._pool.Add(entry);
				Monitor.Pulse(this._pool);
			}
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0000F2E0 File Offset: 0x0000D4E0
		private void CancelConnection(UnixConnection entry)
		{
			try
			{
				entry.Stream.Close();
				this._activeConnections--;
			}
			catch
			{
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0000F31C File Offset: 0x0000D51C
		public void PurgeConnections()
		{
			ArrayList pool = this._pool;
			lock (pool)
			{
				for (int i = 0; i < this._pool.Count; i++)
				{
					UnixConnection unixConnection = (UnixConnection)this._pool[i];
					if ((DateTime.UtcNow - unixConnection.ControlTime).TotalSeconds > (double)UnixConnectionPool.KeepAliveSeconds)
					{
						this.CancelConnection(unixConnection);
						this._pool.RemoveAt(i);
						i--;
					}
				}
			}
		}

		// Token: 0x040004BD RID: 1213
		private ArrayList _pool = new ArrayList();

		// Token: 0x040004BE RID: 1214
		private int _activeConnections;

		// Token: 0x040004BF RID: 1215
		private string _path;
	}
}
