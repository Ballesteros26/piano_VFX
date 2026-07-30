using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Threading;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000089 RID: 137
	internal class UnixConnectionPool
	{
		// Token: 0x060006B1 RID: 1713 RVA: 0x0000EF10 File Offset: 0x0000D110
		static UnixConnectionPool()
		{
			UnixConnectionPool._poolThread.Start();
			UnixConnectionPool._poolThread.IsBackground = true;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0000EF63 File Offset: 0x0000D163
		public static void Shutdown()
		{
			if (UnixConnectionPool._poolThread != null)
			{
				UnixConnectionPool._poolThread.Abort();
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0000EF76 File Offset: 0x0000D176
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x0000EF7D File Offset: 0x0000D17D
		public static int MaxOpenConnections
		{
			get
			{
				return UnixConnectionPool._maxOpenConnections;
			}
			set
			{
				if (value < 1)
				{
					throw new RemotingException("MaxOpenConnections must be greater than zero");
				}
				UnixConnectionPool._maxOpenConnections = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0000EF94 File Offset: 0x0000D194
		// (set) Token: 0x060006B6 RID: 1718 RVA: 0x0000EF9B File Offset: 0x0000D19B
		public static int KeepAliveSeconds
		{
			get
			{
				return UnixConnectionPool._keepAliveSeconds;
			}
			set
			{
				UnixConnectionPool._keepAliveSeconds = value;
			}
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0000EFA4 File Offset: 0x0000D1A4
		public static UnixConnection GetConnection(string path)
		{
			Hashtable pools = UnixConnectionPool._pools;
			HostConnectionPool hostConnectionPool;
			lock (pools)
			{
				hostConnectionPool = (HostConnectionPool)UnixConnectionPool._pools[path];
				if (hostConnectionPool == null)
				{
					hostConnectionPool = new HostConnectionPool(path);
					UnixConnectionPool._pools[path] = hostConnectionPool;
				}
			}
			return hostConnectionPool.GetConnection();
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0000F00C File Offset: 0x0000D20C
		private static void ConnectionCollector()
		{
			for (;;)
			{
				Thread.Sleep(3000);
				Hashtable pools = UnixConnectionPool._pools;
				lock (pools)
				{
					foreach (object obj in UnixConnectionPool._pools.Values)
					{
						((HostConnectionPool)obj).PurgeConnections();
					}
				}
			}
		}

		// Token: 0x040004B4 RID: 1204
		private static Hashtable _pools = new Hashtable();

		// Token: 0x040004B5 RID: 1205
		private static int _maxOpenConnections = int.MaxValue;

		// Token: 0x040004B6 RID: 1206
		private static int _keepAliveSeconds = 15;

		// Token: 0x040004B7 RID: 1207
		private static Thread _poolThread = new Thread(new ThreadStart(UnixConnectionPool.ConnectionCollector));
	}
}
