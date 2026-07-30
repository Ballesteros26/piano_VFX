using System;
using System.Collections;
using System.Threading;

namespace System.Net
{
	// Token: 0x020004AB RID: 1195
	internal class ConnectionPool
	{
		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06002327 RID: 8999 RVA: 0x00087F11 File Offset: 0x00086111
		private Mutex CreationMutex
		{
			get
			{
				return (Mutex)this.m_WaitHandles[2];
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06002328 RID: 9000 RVA: 0x00087F20 File Offset: 0x00086120
		private ManualResetEvent ErrorEvent
		{
			get
			{
				return (ManualResetEvent)this.m_WaitHandles[1];
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06002329 RID: 9001 RVA: 0x00087F2F File Offset: 0x0008612F
		private Semaphore Semaphore
		{
			get
			{
				return (Semaphore)this.m_WaitHandles[0];
			}
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x00087F40 File Offset: 0x00086140
		internal ConnectionPool(ServicePoint servicePoint, int maxPoolSize, int minPoolSize, int idleTimeout, CreateConnectionDelegate createConnectionCallback)
		{
			this.m_State = ConnectionPool.State.Initializing;
			this.m_CreateConnectionCallback = createConnectionCallback;
			this.m_MaxPoolSize = maxPoolSize;
			this.m_MinPoolSize = minPoolSize;
			this.m_ServicePoint = servicePoint;
			this.Initialize();
			if (idleTimeout > 0)
			{
				this.m_CleanupQueue = TimerThread.GetOrCreateQueue((idleTimeout == 1) ? 1 : (idleTimeout / 2));
				this.m_CleanupQueue.CreateTimer(ConnectionPool.s_CleanupCallback, this);
			}
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x00087FAC File Offset: 0x000861AC
		private void Initialize()
		{
			this.m_StackOld = new InterlockedStack();
			this.m_StackNew = new InterlockedStack();
			this.m_QueuedRequests = new Queue();
			this.m_WaitHandles = new WaitHandle[3];
			this.m_WaitHandles[0] = new Semaphore(0, 1048576);
			this.m_WaitHandles[1] = new ManualResetEvent(false);
			this.m_WaitHandles[2] = new Mutex();
			this.m_ErrorTimer = null;
			this.m_ObjectList = new ArrayList();
			this.m_State = ConnectionPool.State.Running;
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x00088030 File Offset: 0x00086230
		private void QueueRequest(ConnectionPool.AsyncConnectionPoolRequest asyncRequest)
		{
			Queue queuedRequests = this.m_QueuedRequests;
			lock (queuedRequests)
			{
				this.m_QueuedRequests.Enqueue(asyncRequest);
				if (this.m_AsyncThread == null)
				{
					this.m_AsyncThread = new Thread(new ThreadStart(this.AsyncThread));
					this.m_AsyncThread.IsBackground = true;
					this.m_AsyncThread.Start();
				}
			}
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x000880AC File Offset: 0x000862AC
		private void AsyncThread()
		{
			for (;;)
			{
				Queue queue;
				if (this.m_QueuedRequests.Count <= 0)
				{
					Thread.Sleep(500);
					queue = this.m_QueuedRequests;
					lock (queue)
					{
						if (this.m_QueuedRequests.Count != 0)
						{
							continue;
						}
						this.m_AsyncThread = null;
					}
					break;
				}
				bool flag2 = true;
				ConnectionPool.AsyncConnectionPoolRequest asyncConnectionPoolRequest = null;
				queue = this.m_QueuedRequests;
				lock (queue)
				{
					asyncConnectionPoolRequest = (ConnectionPool.AsyncConnectionPoolRequest)this.m_QueuedRequests.Dequeue();
				}
				WaitHandle[] waitHandles = this.m_WaitHandles;
				PooledStream pooledStream = null;
				try
				{
					while (pooledStream == null && flag2)
					{
						int num = WaitHandle.WaitAny(waitHandles, asyncConnectionPoolRequest.CreationTimeout, false);
						pooledStream = this.Get(asyncConnectionPoolRequest.OwningObject, num, ref flag2, ref waitHandles);
					}
					pooledStream.Activate(asyncConnectionPoolRequest.OwningObject, asyncConnectionPoolRequest.AsyncCallback);
				}
				catch (Exception ex)
				{
					if (pooledStream != null)
					{
						this.PutConnection(pooledStream, asyncConnectionPoolRequest.OwningObject, asyncConnectionPoolRequest.CreationTimeout, false);
					}
					asyncConnectionPoolRequest.AsyncCallback(asyncConnectionPoolRequest.OwningObject, ex);
				}
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x0600232E RID: 9006 RVA: 0x000881E8 File Offset: 0x000863E8
		internal int Count
		{
			get
			{
				return this.m_TotalObjects;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x0600232F RID: 9007 RVA: 0x000881F0 File Offset: 0x000863F0
		internal ServicePoint ServicePoint
		{
			get
			{
				return this.m_ServicePoint;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06002330 RID: 9008 RVA: 0x000881F8 File Offset: 0x000863F8
		internal int MaxPoolSize
		{
			get
			{
				return this.m_MaxPoolSize;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06002331 RID: 9009 RVA: 0x00088200 File Offset: 0x00086400
		internal int MinPoolSize
		{
			get
			{
				return this.m_MinPoolSize;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06002332 RID: 9010 RVA: 0x00088208 File Offset: 0x00086408
		private bool ErrorOccurred
		{
			get
			{
				return this.m_ErrorOccured;
			}
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x00088214 File Offset: 0x00086414
		private static void CleanupCallbackWrapper(TimerThread.Timer timer, int timeNoticed, object context)
		{
			ConnectionPool connectionPool = (ConnectionPool)context;
			try
			{
				connectionPool.CleanupCallback();
			}
			finally
			{
				connectionPool.m_CleanupQueue.CreateTimer(ConnectionPool.s_CleanupCallback, context);
			}
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x00088254 File Offset: 0x00086454
		internal void ForceCleanup()
		{
			if (Logging.On)
			{
			}
			while (this.Count > 0 && this.Semaphore.WaitOne(0, false))
			{
				PooledStream pooledStream = (PooledStream)this.m_StackNew.Pop();
				if (pooledStream == null)
				{
					pooledStream = (PooledStream)this.m_StackOld.Pop();
				}
				this.Destroy(pooledStream);
			}
			bool on = Logging.On;
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x000882B4 File Offset: 0x000864B4
		private void CleanupCallback()
		{
			while (this.Count > this.MinPoolSize && this.Semaphore.WaitOne(0, false))
			{
				PooledStream pooledStream = (PooledStream)this.m_StackOld.Pop();
				if (pooledStream == null)
				{
					this.Semaphore.ReleaseSemaphore();
					break;
				}
				this.Destroy(pooledStream);
			}
			if (this.Semaphore.WaitOne(0, false))
			{
				for (;;)
				{
					PooledStream pooledStream2 = (PooledStream)this.m_StackNew.Pop();
					if (pooledStream2 == null)
					{
						break;
					}
					this.m_StackOld.Push(pooledStream2);
				}
				this.Semaphore.ReleaseSemaphore();
			}
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x00088348 File Offset: 0x00086548
		private PooledStream Create(CreateConnectionDelegate createConnectionCallback)
		{
			PooledStream pooledStream = null;
			try
			{
				pooledStream = createConnectionCallback(this);
				if (pooledStream == null)
				{
					throw new InternalException();
				}
				if (!pooledStream.CanBePooled)
				{
					throw new InternalException();
				}
				pooledStream.PrePush(null);
				object syncRoot = this.m_ObjectList.SyncRoot;
				lock (syncRoot)
				{
					this.m_ObjectList.Add(pooledStream);
					this.m_TotalObjects = this.m_ObjectList.Count;
				}
			}
			catch (Exception ex)
			{
				pooledStream = null;
				this.m_ResError = ex;
				this.Abort();
			}
			return pooledStream;
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x000883F0 File Offset: 0x000865F0
		private void Destroy(PooledStream pooledStream)
		{
			if (pooledStream != null)
			{
				try
				{
					object syncRoot = this.m_ObjectList.SyncRoot;
					lock (syncRoot)
					{
						this.m_ObjectList.Remove(pooledStream);
						this.m_TotalObjects = this.m_ObjectList.Count;
					}
				}
				finally
				{
					pooledStream.Dispose();
				}
			}
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x00088464 File Offset: 0x00086664
		private static void CancelErrorCallbackWrapper(TimerThread.Timer timer, int timeNoticed, object context)
		{
			((ConnectionPool)context).CancelErrorCallback();
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x00088474 File Offset: 0x00086674
		private void CancelErrorCallback()
		{
			TimerThread.Timer errorTimer = this.m_ErrorTimer;
			if (errorTimer != null && errorTimer.Cancel())
			{
				this.m_ErrorOccured = false;
				this.ErrorEvent.Reset();
				this.m_ErrorTimer = null;
				this.m_ResError = null;
			}
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x000884B8 File Offset: 0x000866B8
		private PooledStream GetFromPool(object owningObject)
		{
			PooledStream pooledStream = (PooledStream)this.m_StackNew.Pop();
			if (pooledStream == null)
			{
				pooledStream = (PooledStream)this.m_StackOld.Pop();
			}
			if (pooledStream != null)
			{
				pooledStream.PostPop(owningObject);
			}
			return pooledStream;
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x000884F8 File Offset: 0x000866F8
		private PooledStream Get(object owningObject, int result, ref bool continueLoop, ref WaitHandle[] waitHandles)
		{
			PooledStream pooledStream = null;
			if (result != 1)
			{
				if (result != 2)
				{
					if (result == 258)
					{
						Interlocked.Decrement(ref this.m_WaitCount);
						continueLoop = false;
						throw new WebException(NetRes.GetWebStatusString("net_timeout", WebExceptionStatus.ConnectFailure), WebExceptionStatus.Timeout);
					}
				}
				else
				{
					try
					{
						continueLoop = true;
						pooledStream = this.UserCreateRequest();
						if (pooledStream != null)
						{
							pooledStream.PostPop(owningObject);
							Interlocked.Decrement(ref this.m_WaitCount);
							continueLoop = false;
							return pooledStream;
						}
						if (this.Count >= this.MaxPoolSize && this.MaxPoolSize != 0 && !this.ReclaimEmancipatedObjects())
						{
							waitHandles = new WaitHandle[2];
							waitHandles[0] = this.m_WaitHandles[0];
							waitHandles[1] = this.m_WaitHandles[1];
						}
						return pooledStream;
					}
					finally
					{
						this.CreationMutex.ReleaseMutex();
					}
				}
				Interlocked.Decrement(ref this.m_WaitCount);
				pooledStream = this.GetFromPool(owningObject);
				continueLoop = false;
				return pooledStream;
			}
			bool flag = Interlocked.Decrement(ref this.m_WaitCount) != 0;
			continueLoop = false;
			Exception resError = this.m_ResError;
			if (!flag)
			{
				this.CancelErrorCallback();
			}
			throw resError;
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x000885F8 File Offset: 0x000867F8
		internal void Abort()
		{
			if (this.m_ResError == null)
			{
				this.m_ResError = new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
			}
			this.ErrorEvent.Set();
			this.m_ErrorOccured = true;
			this.m_ErrorTimer = ConnectionPool.s_CancelErrorQueue.CreateTimer(ConnectionPool.s_CancelErrorCallback, this);
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x00088650 File Offset: 0x00086850
		internal PooledStream GetConnection(object owningObject, GeneralAsyncDelegate asyncCallback, int creationTimeout)
		{
			PooledStream pooledStream = null;
			bool flag = true;
			bool flag2 = asyncCallback != null;
			if (this.m_State != ConnectionPool.State.Running)
			{
				throw new InternalException();
			}
			Interlocked.Increment(ref this.m_WaitCount);
			WaitHandle[] waitHandles = this.m_WaitHandles;
			if (flag2)
			{
				int num = WaitHandle.WaitAny(waitHandles, 0, false);
				if (num != 258)
				{
					pooledStream = this.Get(owningObject, num, ref flag, ref waitHandles);
				}
				if (pooledStream == null)
				{
					ConnectionPool.AsyncConnectionPoolRequest asyncConnectionPoolRequest = new ConnectionPool.AsyncConnectionPoolRequest(this, owningObject, asyncCallback, creationTimeout);
					this.QueueRequest(asyncConnectionPoolRequest);
				}
			}
			else
			{
				while (pooledStream == null && flag)
				{
					int num = WaitHandle.WaitAny(waitHandles, creationTimeout, false);
					pooledStream = this.Get(owningObject, num, ref flag, ref waitHandles);
				}
			}
			if (pooledStream != null)
			{
				if (!pooledStream.IsInitalizing)
				{
					asyncCallback = null;
				}
				try
				{
					if (!pooledStream.Activate(owningObject, asyncCallback))
					{
						pooledStream = null;
					}
					return pooledStream;
				}
				catch
				{
					this.PutConnection(pooledStream, owningObject, creationTimeout, false);
					throw;
				}
			}
			if (!flag2)
			{
				throw new InternalException();
			}
			return pooledStream;
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x0008872C File Offset: 0x0008692C
		internal void PutConnection(PooledStream pooledStream, object owningObject, int creationTimeout)
		{
			this.PutConnection(pooledStream, owningObject, creationTimeout, true);
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x00088738 File Offset: 0x00086938
		internal void PutConnection(PooledStream pooledStream, object owningObject, int creationTimeout, bool canReuse)
		{
			if (pooledStream == null)
			{
				throw new ArgumentNullException("pooledStream");
			}
			pooledStream.PrePush(owningObject);
			if (this.m_State != ConnectionPool.State.ShuttingDown)
			{
				pooledStream.Deactivate();
				if (this.m_WaitCount == 0)
				{
					this.CancelErrorCallback();
				}
				if (canReuse && pooledStream.CanBePooled)
				{
					this.PutNew(pooledStream);
					return;
				}
				try
				{
					this.Destroy(pooledStream);
					return;
				}
				finally
				{
					if (this.m_WaitCount > 0)
					{
						if (!this.CreationMutex.WaitOne(creationTimeout, false))
						{
							this.Abort();
						}
						else
						{
							try
							{
								pooledStream = this.UserCreateRequest();
								if (pooledStream != null)
								{
									this.PutNew(pooledStream);
								}
							}
							finally
							{
								this.CreationMutex.ReleaseMutex();
							}
						}
					}
				}
			}
			this.Destroy(pooledStream);
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x000887FC File Offset: 0x000869FC
		private void PutNew(PooledStream pooledStream)
		{
			this.m_StackNew.Push(pooledStream);
			this.Semaphore.ReleaseSemaphore();
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x00088818 File Offset: 0x00086A18
		private bool ReclaimEmancipatedObjects()
		{
			bool flag = false;
			object syncRoot = this.m_ObjectList.SyncRoot;
			lock (syncRoot)
			{
				object[] array = this.m_ObjectList.ToArray();
				if (array != null)
				{
					foreach (PooledStream pooledStream in array)
					{
						if (pooledStream != null)
						{
							bool flag3 = false;
							try
							{
								Monitor.TryEnter(pooledStream, ref flag3);
								if (flag3 && pooledStream.IsEmancipated)
								{
									this.PutConnection(pooledStream, null, -1);
									flag = true;
								}
							}
							finally
							{
								if (flag3)
								{
									Monitor.Exit(pooledStream);
								}
							}
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x000888CC File Offset: 0x00086ACC
		private PooledStream UserCreateRequest()
		{
			PooledStream pooledStream = null;
			if (!this.ErrorOccurred && (this.Count < this.MaxPoolSize || this.MaxPoolSize == 0) && ((this.Count & 1) == 1 || !this.ReclaimEmancipatedObjects()))
			{
				pooledStream = this.Create(this.m_CreateConnectionCallback);
			}
			return pooledStream;
		}

		// Token: 0x04001F59 RID: 8025
		private static TimerThread.Callback s_CleanupCallback = new TimerThread.Callback(ConnectionPool.CleanupCallbackWrapper);

		// Token: 0x04001F5A RID: 8026
		private static TimerThread.Callback s_CancelErrorCallback = new TimerThread.Callback(ConnectionPool.CancelErrorCallbackWrapper);

		// Token: 0x04001F5B RID: 8027
		private static TimerThread.Queue s_CancelErrorQueue = TimerThread.GetOrCreateQueue(5000);

		// Token: 0x04001F5C RID: 8028
		private const int MaxQueueSize = 1048576;

		// Token: 0x04001F5D RID: 8029
		private const int SemaphoreHandleIndex = 0;

		// Token: 0x04001F5E RID: 8030
		private const int ErrorHandleIndex = 1;

		// Token: 0x04001F5F RID: 8031
		private const int CreationHandleIndex = 2;

		// Token: 0x04001F60 RID: 8032
		private const int WaitTimeout = 258;

		// Token: 0x04001F61 RID: 8033
		private const int WaitAbandoned = 128;

		// Token: 0x04001F62 RID: 8034
		private const int ErrorWait = 5000;

		// Token: 0x04001F63 RID: 8035
		private readonly TimerThread.Queue m_CleanupQueue;

		// Token: 0x04001F64 RID: 8036
		private ConnectionPool.State m_State;

		// Token: 0x04001F65 RID: 8037
		private InterlockedStack m_StackOld;

		// Token: 0x04001F66 RID: 8038
		private InterlockedStack m_StackNew;

		// Token: 0x04001F67 RID: 8039
		private int m_WaitCount;

		// Token: 0x04001F68 RID: 8040
		private WaitHandle[] m_WaitHandles;

		// Token: 0x04001F69 RID: 8041
		private Exception m_ResError;

		// Token: 0x04001F6A RID: 8042
		private volatile bool m_ErrorOccured;

		// Token: 0x04001F6B RID: 8043
		private TimerThread.Timer m_ErrorTimer;

		// Token: 0x04001F6C RID: 8044
		private ArrayList m_ObjectList;

		// Token: 0x04001F6D RID: 8045
		private int m_TotalObjects;

		// Token: 0x04001F6E RID: 8046
		private Queue m_QueuedRequests;

		// Token: 0x04001F6F RID: 8047
		private Thread m_AsyncThread;

		// Token: 0x04001F70 RID: 8048
		private int m_MaxPoolSize;

		// Token: 0x04001F71 RID: 8049
		private int m_MinPoolSize;

		// Token: 0x04001F72 RID: 8050
		private ServicePoint m_ServicePoint;

		// Token: 0x04001F73 RID: 8051
		private CreateConnectionDelegate m_CreateConnectionCallback;

		// Token: 0x020004AC RID: 1196
		private enum State
		{
			// Token: 0x04001F75 RID: 8053
			Initializing,
			// Token: 0x04001F76 RID: 8054
			Running,
			// Token: 0x04001F77 RID: 8055
			ShuttingDown
		}

		// Token: 0x020004AD RID: 1197
		private class AsyncConnectionPoolRequest
		{
			// Token: 0x06002344 RID: 9028 RVA: 0x0008894D File Offset: 0x00086B4D
			public AsyncConnectionPoolRequest(ConnectionPool pool, object owningObject, GeneralAsyncDelegate asyncCallback, int creationTimeout)
			{
				this.Pool = pool;
				this.OwningObject = owningObject;
				this.AsyncCallback = asyncCallback;
				this.CreationTimeout = creationTimeout;
			}

			// Token: 0x04001F78 RID: 8056
			public object OwningObject;

			// Token: 0x04001F79 RID: 8057
			public GeneralAsyncDelegate AsyncCallback;

			// Token: 0x04001F7A RID: 8058
			public ConnectionPool Pool;

			// Token: 0x04001F7B RID: 8059
			public int CreationTimeout;
		}
	}
}
