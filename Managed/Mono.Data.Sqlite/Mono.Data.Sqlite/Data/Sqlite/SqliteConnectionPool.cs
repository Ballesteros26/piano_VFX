using System;
using System.Collections.Generic;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000016 RID: 22
	internal static class SqliteConnectionPool
	{
		// Token: 0x06000155 RID: 341 RVA: 0x000082B8 File Offset: 0x000064B8
		internal static SqliteConnectionHandle Remove(string fileName, int maxPoolSize, out int version)
		{
			SortedList<string, SqliteConnectionPool.Pool> connections = SqliteConnectionPool._connections;
			SqliteConnectionHandle sqliteConnectionHandle;
			lock (connections)
			{
				version = SqliteConnectionPool._poolVersion;
				SqliteConnectionPool.Pool pool;
				if (!SqliteConnectionPool._connections.TryGetValue(fileName, out pool))
				{
					pool = new SqliteConnectionPool.Pool(SqliteConnectionPool._poolVersion, maxPoolSize);
					SqliteConnectionPool._connections.Add(fileName, pool);
					sqliteConnectionHandle = null;
				}
				else
				{
					version = pool.PoolVersion;
					pool.MaxPoolSize = maxPoolSize;
					SqliteConnectionPool.ResizePool(pool, false);
					while (pool.Queue.Count > 0)
					{
						SqliteConnectionHandle sqliteConnectionHandle2 = pool.Queue.Dequeue().Target as SqliteConnectionHandle;
						if (sqliteConnectionHandle2 != null)
						{
							return sqliteConnectionHandle2;
						}
					}
					sqliteConnectionHandle = null;
				}
			}
			return sqliteConnectionHandle;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000836C File Offset: 0x0000656C
		internal static void ClearAllPools()
		{
			SortedList<string, SqliteConnectionPool.Pool> connections = SqliteConnectionPool._connections;
			lock (connections)
			{
				foreach (KeyValuePair<string, SqliteConnectionPool.Pool> keyValuePair in SqliteConnectionPool._connections)
				{
					while (keyValuePair.Value.Queue.Count > 0)
					{
						SqliteConnectionHandle sqliteConnectionHandle = keyValuePair.Value.Queue.Dequeue().Target as SqliteConnectionHandle;
						if (sqliteConnectionHandle != null)
						{
							sqliteConnectionHandle.Dispose();
						}
					}
					if (SqliteConnectionPool._poolVersion <= keyValuePair.Value.PoolVersion)
					{
						SqliteConnectionPool._poolVersion = keyValuePair.Value.PoolVersion + 1;
					}
				}
				SqliteConnectionPool._connections.Clear();
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00008448 File Offset: 0x00006648
		internal static void ClearPool(string fileName)
		{
			SortedList<string, SqliteConnectionPool.Pool> connections = SqliteConnectionPool._connections;
			lock (connections)
			{
				SqliteConnectionPool.Pool pool;
				if (SqliteConnectionPool._connections.TryGetValue(fileName, out pool))
				{
					pool.PoolVersion++;
					while (pool.Queue.Count > 0)
					{
						SqliteConnectionHandle sqliteConnectionHandle = pool.Queue.Dequeue().Target as SqliteConnectionHandle;
						if (sqliteConnectionHandle != null)
						{
							sqliteConnectionHandle.Dispose();
						}
					}
				}
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000084D0 File Offset: 0x000066D0
		internal static void Add(string fileName, SqliteConnectionHandle hdl, int version)
		{
			SortedList<string, SqliteConnectionPool.Pool> connections = SqliteConnectionPool._connections;
			lock (connections)
			{
				SqliteConnectionPool.Pool pool;
				if (SqliteConnectionPool._connections.TryGetValue(fileName, out pool) && version == pool.PoolVersion)
				{
					SqliteConnectionPool.ResizePool(pool, true);
					pool.Queue.Enqueue(new WeakReference(hdl, false));
					GC.KeepAlive(hdl);
				}
				else
				{
					hdl.Close();
				}
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008548 File Offset: 0x00006748
		private static void ResizePool(SqliteConnectionPool.Pool queue, bool forAdding)
		{
			int num = queue.MaxPoolSize;
			if (forAdding && num > 0)
			{
				num--;
			}
			while (queue.Queue.Count > num)
			{
				SqliteConnectionHandle sqliteConnectionHandle = queue.Queue.Dequeue().Target as SqliteConnectionHandle;
				if (sqliteConnectionHandle != null)
				{
					sqliteConnectionHandle.Dispose();
				}
			}
		}

		// Token: 0x0400006F RID: 111
		private static SortedList<string, SqliteConnectionPool.Pool> _connections = new SortedList<string, SqliteConnectionPool.Pool>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000070 RID: 112
		private static int _poolVersion = 1;

		// Token: 0x0200003A RID: 58
		internal class Pool
		{
			// Token: 0x060002F2 RID: 754 RVA: 0x0000E5F8 File Offset: 0x0000C7F8
			internal Pool(int version, int maxSize)
			{
				this.PoolVersion = version;
				this.MaxPoolSize = maxSize;
			}

			// Token: 0x04000110 RID: 272
			internal readonly Queue<WeakReference> Queue = new Queue<WeakReference>();

			// Token: 0x04000111 RID: 273
			internal int PoolVersion;

			// Token: 0x04000112 RID: 274
			internal int MaxPoolSize;
		}
	}
}
