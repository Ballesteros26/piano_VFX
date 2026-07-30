using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.ProviderBase
{
	// Token: 0x0200030B RID: 779
	internal sealed class DbConnectionPool
	{
		// Token: 0x060022AD RID: 8877 RVA: 0x000A1370 File Offset: 0x0009F570
		internal DbConnectionPool(DbConnectionFactory connectionFactory, DbConnectionPoolGroup connectionPoolGroup, DbConnectionPoolIdentity identity, DbConnectionPoolProviderInfo connectionPoolProviderInfo)
		{
			if (identity != null && identity.IsRestricted)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.AttemptingToPoolOnRestrictedToken);
			}
			this._state = DbConnectionPool.State.Initializing;
			Random random = DbConnectionPool.s_random;
			lock (random)
			{
				this._cleanupWait = DbConnectionPool.s_random.Next(12, 24) * 10 * 1000;
			}
			this._connectionFactory = connectionFactory;
			this._connectionPoolGroup = connectionPoolGroup;
			this._connectionPoolGroupOptions = connectionPoolGroup.PoolGroupOptions;
			this._connectionPoolProviderInfo = connectionPoolProviderInfo;
			this._identity = identity;
			this._waitHandles = new DbConnectionPool.PoolWaitHandles();
			this._errorWait = 5000;
			this._errorTimer = null;
			this._objectList = new List<DbConnectionInternal>(this.MaxPoolSize);
			if (ADP.IsPlatformNT5)
			{
				this._transactedConnectionPool = new DbConnectionPool.TransactedConnectionPool(this);
			}
			this._poolCreateRequest = new WaitCallback(this.PoolCreateRequest);
			this._state = DbConnectionPool.State.Running;
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060022AE RID: 8878 RVA: 0x000A148C File Offset: 0x0009F68C
		private int CreationTimeout
		{
			get
			{
				return this.PoolGroupOptions.CreationTimeout;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060022AF RID: 8879 RVA: 0x000A1499 File Offset: 0x0009F699
		internal int Count
		{
			get
			{
				return this._totalObjects;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x000A14A1 File Offset: 0x0009F6A1
		internal DbConnectionFactory ConnectionFactory
		{
			get
			{
				return this._connectionFactory;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060022B1 RID: 8881 RVA: 0x000A14A9 File Offset: 0x0009F6A9
		internal bool ErrorOccurred
		{
			get
			{
				return this._errorOccurred;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060022B2 RID: 8882 RVA: 0x000A14B3 File Offset: 0x0009F6B3
		private bool HasTransactionAffinity
		{
			get
			{
				return this.PoolGroupOptions.HasTransactionAffinity;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060022B3 RID: 8883 RVA: 0x000A14C0 File Offset: 0x0009F6C0
		internal TimeSpan LoadBalanceTimeout
		{
			get
			{
				return this.PoolGroupOptions.LoadBalanceTimeout;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060022B4 RID: 8884 RVA: 0x000A14D0 File Offset: 0x0009F6D0
		private bool NeedToReplenish
		{
			get
			{
				if (DbConnectionPool.State.Running != this._state)
				{
					return false;
				}
				int count = this.Count;
				if (count >= this.MaxPoolSize)
				{
					return false;
				}
				if (count < this.MinPoolSize)
				{
					return true;
				}
				int num = this._stackNew.Count + this._stackOld.Count;
				int waitCount = this._waitCount;
				return num < waitCount || (num == waitCount && count > 1);
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060022B5 RID: 8885 RVA: 0x000A1534 File Offset: 0x0009F734
		internal DbConnectionPoolIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060022B6 RID: 8886 RVA: 0x000A153C File Offset: 0x0009F73C
		internal bool IsRunning
		{
			get
			{
				return DbConnectionPool.State.Running == this._state;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x060022B7 RID: 8887 RVA: 0x000A1547 File Offset: 0x0009F747
		private int MaxPoolSize
		{
			get
			{
				return this.PoolGroupOptions.MaxPoolSize;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x060022B8 RID: 8888 RVA: 0x000A1554 File Offset: 0x0009F754
		private int MinPoolSize
		{
			get
			{
				return this.PoolGroupOptions.MinPoolSize;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060022B9 RID: 8889 RVA: 0x000A1561 File Offset: 0x0009F761
		internal DbConnectionPoolGroup PoolGroup
		{
			get
			{
				return this._connectionPoolGroup;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x060022BA RID: 8890 RVA: 0x000A1569 File Offset: 0x0009F769
		internal DbConnectionPoolGroupOptions PoolGroupOptions
		{
			get
			{
				return this._connectionPoolGroupOptions;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x060022BB RID: 8891 RVA: 0x000A1571 File Offset: 0x0009F771
		internal DbConnectionPoolProviderInfo ProviderInfo
		{
			get
			{
				return this._connectionPoolProviderInfo;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x060022BC RID: 8892 RVA: 0x000A1579 File Offset: 0x0009F779
		internal bool UseLoadBalancing
		{
			get
			{
				return this.PoolGroupOptions.UseLoadBalancing;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x060022BD RID: 8893 RVA: 0x000A1586 File Offset: 0x0009F786
		private bool UsingIntegrateSecurity
		{
			get
			{
				return this._identity != null && DbConnectionPoolIdentity.NoIdentity != this._identity;
			}
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x000A15A4 File Offset: 0x0009F7A4
		private void CleanupCallback(object state)
		{
			while (this.Count > this.MinPoolSize && this._waitHandles.PoolSemaphore.WaitOne(0))
			{
				DbConnectionInternal dbConnectionInternal;
				if (!this._stackOld.TryPop(out dbConnectionInternal))
				{
					this._waitHandles.PoolSemaphore.Release(1);
					break;
				}
				bool flag = true;
				DbConnectionInternal dbConnectionInternal2 = dbConnectionInternal;
				lock (dbConnectionInternal2)
				{
					if (dbConnectionInternal.IsTransactionRoot)
					{
						flag = false;
					}
				}
				if (flag)
				{
					this.DestroyObject(dbConnectionInternal);
				}
				else
				{
					dbConnectionInternal.SetInStasis();
				}
			}
			if (this._waitHandles.PoolSemaphore.WaitOne(0))
			{
				DbConnectionInternal dbConnectionInternal3;
				while (this._stackNew.TryPop(out dbConnectionInternal3))
				{
					this._stackOld.Push(dbConnectionInternal3);
				}
				this._waitHandles.PoolSemaphore.Release(1);
			}
			this.QueuePoolCreateRequest();
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x000A1688 File Offset: 0x0009F888
		internal void Clear()
		{
			List<DbConnectionInternal> objectList = this._objectList;
			DbConnectionInternal dbConnectionInternal;
			lock (objectList)
			{
				int count = this._objectList.Count;
				for (int i = 0; i < count; i++)
				{
					dbConnectionInternal = this._objectList[i];
					if (dbConnectionInternal != null)
					{
						dbConnectionInternal.DoNotPoolThisConnection();
					}
				}
				goto IL_0057;
			}
			IL_0050:
			this.DestroyObject(dbConnectionInternal);
			IL_0057:
			if (!this._stackNew.TryPop(out dbConnectionInternal))
			{
				while (this._stackOld.TryPop(out dbConnectionInternal))
				{
					this.DestroyObject(dbConnectionInternal);
				}
				this.ReclaimEmancipatedObjects();
				return;
			}
			goto IL_0050;
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x000A172C File Offset: 0x0009F92C
		private Timer CreateCleanupTimer()
		{
			return new Timer(new TimerCallback(this.CleanupCallback), null, this._cleanupWait, this._cleanupWait);
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x000A174C File Offset: 0x0009F94C
		private DbConnectionInternal CreateObject(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection)
		{
			DbConnectionInternal dbConnectionInternal = null;
			try
			{
				dbConnectionInternal = this._connectionFactory.CreatePooledConnection(this, owningObject, this._connectionPoolGroup.ConnectionOptions, this._connectionPoolGroup.PoolKey, userOptions);
				if (dbConnectionInternal == null)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.CreateObjectReturnedNull);
				}
				if (!dbConnectionInternal.CanBePooled)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.NewObjectCannotBePooled);
				}
				dbConnectionInternal.PrePush(null);
				List<DbConnectionInternal> list = this._objectList;
				lock (list)
				{
					if (oldConnection != null && oldConnection.Pool == this)
					{
						this._objectList.Remove(oldConnection);
					}
					this._objectList.Add(dbConnectionInternal);
					this._totalObjects = this._objectList.Count;
				}
				if (oldConnection != null)
				{
					DbConnectionPool pool = oldConnection.Pool;
					if (pool != null && pool != this)
					{
						list = pool._objectList;
						lock (list)
						{
							pool._objectList.Remove(oldConnection);
							pool._totalObjects = pool._objectList.Count;
						}
					}
				}
				this._errorWait = 5000;
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				dbConnectionInternal = null;
				this._resError = ex;
				Timer timer = new Timer(new TimerCallback(this.ErrorCallback), null, -1, -1);
				try
				{
				}
				finally
				{
					this._waitHandles.ErrorEvent.Set();
					this._errorOccurred = true;
					this._errorTimer = timer;
					timer.Change(this._errorWait, this._errorWait);
				}
				if (30000 < this._errorWait)
				{
					this._errorWait = 60000;
				}
				else
				{
					this._errorWait *= 2;
				}
				throw;
			}
			return dbConnectionInternal;
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x000A1914 File Offset: 0x0009FB14
		private void DeactivateObject(DbConnectionInternal obj)
		{
			obj.DeactivateConnection();
			bool flag = false;
			bool flag2 = false;
			if (obj.IsConnectionDoomed)
			{
				flag2 = true;
			}
			else
			{
				lock (obj)
				{
					if (this._state == DbConnectionPool.State.ShuttingDown)
					{
						if (obj.IsTransactionRoot)
						{
							obj.SetInStasis();
						}
						else
						{
							flag2 = true;
						}
					}
					else if (obj.IsNonPoolableTransactionRoot)
					{
						obj.SetInStasis();
					}
					else if (obj.CanBePooled)
					{
						Transaction enlistedTransaction = obj.EnlistedTransaction;
						if (null != enlistedTransaction)
						{
							this._transactedConnectionPool.PutTransactedObject(enlistedTransaction, obj);
						}
						else
						{
							flag = true;
						}
					}
					else if (obj.IsTransactionRoot && !obj.IsConnectionDoomed)
					{
						obj.SetInStasis();
					}
					else
					{
						flag2 = true;
					}
				}
			}
			if (flag)
			{
				this.PutNewObject(obj);
				return;
			}
			if (flag2)
			{
				this.DestroyObject(obj);
				this.QueuePoolCreateRequest();
			}
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x000A19F4 File Offset: 0x0009FBF4
		internal void DestroyObject(DbConnectionInternal obj)
		{
			if (!obj.IsTxRootWaitingForTxEnd)
			{
				List<DbConnectionInternal> objectList = this._objectList;
				lock (objectList)
				{
					this._objectList.Remove(obj);
					this._totalObjects = this._objectList.Count;
				}
				obj.Dispose();
			}
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x000A1A5C File Offset: 0x0009FC5C
		private void ErrorCallback(object state)
		{
			this._errorOccurred = false;
			this._waitHandles.ErrorEvent.Reset();
			Timer errorTimer = this._errorTimer;
			this._errorTimer = null;
			if (errorTimer != null)
			{
				errorTimer.Dispose();
			}
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x000A1A9C File Offset: 0x0009FC9C
		private Exception TryCloneCachedException()
		{
			if (this._resError == null)
			{
				return null;
			}
			SqlException ex = this._resError as SqlException;
			if (ex != null)
			{
				return ex.InternalClone();
			}
			return this._resError;
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x000A1AD0 File Offset: 0x0009FCD0
		private void WaitForPendingOpen()
		{
			DbConnectionPool.PendingGetConnection pendingGetConnection;
			do
			{
				bool flag = false;
				try
				{
					try
					{
					}
					finally
					{
						flag = Interlocked.CompareExchange(ref this._pendingOpensWaiting, 1, 0) == 0;
					}
					if (!flag)
					{
						break;
					}
					while (this._pendingOpens.TryDequeue(out pendingGetConnection))
					{
						if (!pendingGetConnection.Completion.Task.IsCompleted)
						{
							uint num;
							if (pendingGetConnection.DueTime == -1L)
							{
								num = uint.MaxValue;
							}
							else
							{
								num = (uint)Math.Max(ADP.TimerRemainingMilliseconds(pendingGetConnection.DueTime), 0L);
							}
							DbConnectionInternal dbConnectionInternal = null;
							bool flag2 = false;
							Exception ex = null;
							try
							{
								bool flag3 = true;
								bool flag4 = false;
								ADP.SetCurrentTransaction(pendingGetConnection.Completion.Task.AsyncState as Transaction);
								flag2 = !this.TryGetConnection(pendingGetConnection.Owner, num, flag3, flag4, pendingGetConnection.UserOptions, out dbConnectionInternal);
							}
							catch (Exception ex)
							{
							}
							if (ex != null)
							{
								pendingGetConnection.Completion.TrySetException(ex);
							}
							else if (flag2)
							{
								pendingGetConnection.Completion.TrySetException(ADP.ExceptionWithStackTrace(ADP.PooledOpenTimeout()));
							}
							else if (!pendingGetConnection.Completion.TrySetResult(dbConnectionInternal))
							{
								this.PutObject(dbConnectionInternal, pendingGetConnection.Owner);
							}
						}
					}
				}
				finally
				{
					if (flag)
					{
						Interlocked.Exchange(ref this._pendingOpensWaiting, 0);
					}
				}
			}
			while (this._pendingOpens.TryPeek(out pendingGetConnection));
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x000A1C50 File Offset: 0x0009FE50
		internal bool TryGetConnection(DbConnection owningObject, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions, out DbConnectionInternal connection)
		{
			uint num = 0U;
			bool flag = false;
			if (retry == null)
			{
				num = (uint)this.CreationTimeout;
				if (num == 0U)
				{
					num = uint.MaxValue;
				}
				flag = true;
			}
			if (this._state != DbConnectionPool.State.Running)
			{
				connection = null;
				return true;
			}
			bool flag2 = true;
			if (this.TryGetConnection(owningObject, num, flag, flag2, userOptions, out connection))
			{
				return true;
			}
			if (retry == null)
			{
				return true;
			}
			DbConnectionPool.PendingGetConnection pendingGetConnection = new DbConnectionPool.PendingGetConnection((this.CreationTimeout == 0) ? (-1L) : (ADP.TimerCurrent() + ADP.TimerFromSeconds(this.CreationTimeout / 1000)), owningObject, retry, userOptions);
			this._pendingOpens.Enqueue(pendingGetConnection);
			if (this._pendingOpensWaiting == 0)
			{
				new Thread(new ThreadStart(this.WaitForPendingOpen))
				{
					IsBackground = true
				}.Start();
			}
			connection = null;
			return false;
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x000A1CFC File Offset: 0x0009FEFC
		private bool TryGetConnection(DbConnection owningObject, uint waitForMultipleObjectsTimeout, bool allowCreate, bool onlyOneCheckConnection, DbConnectionOptions userOptions, out DbConnectionInternal connection)
		{
			DbConnectionInternal dbConnectionInternal = null;
			Transaction transaction = null;
			if (this.HasTransactionAffinity)
			{
				dbConnectionInternal = this.GetFromTransactedPool(out transaction);
			}
			if (dbConnectionInternal == null)
			{
				Interlocked.Increment(ref this._waitCount);
				for (;;)
				{
					int num = 3;
					try
					{
						try
						{
						}
						finally
						{
							num = WaitHandle.WaitAny(this._waitHandles.GetHandles(allowCreate), (int)waitForMultipleObjectsTimeout);
						}
						switch (num)
						{
						case 0:
							Interlocked.Decrement(ref this._waitCount);
							dbConnectionInternal = this.GetFromGeneralPool();
							if (dbConnectionInternal != null && !dbConnectionInternal.IsConnectionAlive(false))
							{
								this.DestroyObject(dbConnectionInternal);
								dbConnectionInternal = null;
								if (onlyOneCheckConnection)
								{
									if (this._waitHandles.CreationSemaphore.WaitOne((int)waitForMultipleObjectsTimeout))
									{
										try
										{
											dbConnectionInternal = this.UserCreateRequest(owningObject, userOptions, null);
											break;
										}
										finally
										{
											this._waitHandles.CreationSemaphore.Release(1);
										}
									}
									connection = null;
									return false;
								}
							}
							break;
						case 1:
							Interlocked.Decrement(ref this._waitCount);
							throw this.TryCloneCachedException();
						case 2:
							try
							{
								dbConnectionInternal = this.UserCreateRequest(owningObject, userOptions, null);
							}
							catch
							{
								if (dbConnectionInternal == null)
								{
									Interlocked.Decrement(ref this._waitCount);
								}
								throw;
							}
							finally
							{
								if (dbConnectionInternal != null)
								{
									Interlocked.Decrement(ref this._waitCount);
								}
							}
							if (dbConnectionInternal == null && this.Count >= this.MaxPoolSize && this.MaxPoolSize != 0 && !this.ReclaimEmancipatedObjects())
							{
								allowCreate = false;
							}
							break;
						default:
							if (num == 258)
							{
								Interlocked.Decrement(ref this._waitCount);
								connection = null;
								return false;
							}
							Interlocked.Decrement(ref this._waitCount);
							throw ADP.InternalError(ADP.InternalErrorCode.UnexpectedWaitAnyResult);
						}
					}
					finally
					{
						if (2 == num)
						{
							this._waitHandles.CreationSemaphore.Release(1);
						}
					}
					if (dbConnectionInternal != null)
					{
						goto IL_0185;
					}
				}
				bool flag;
				return flag;
			}
			IL_0185:
			if (dbConnectionInternal != null)
			{
				this.PrepareConnection(owningObject, dbConnectionInternal, transaction);
			}
			connection = dbConnectionInternal;
			return true;
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x000A1F20 File Offset: 0x000A0120
		private void PrepareConnection(DbConnection owningObject, DbConnectionInternal obj, Transaction transaction)
		{
			lock (obj)
			{
				obj.PostPop(owningObject);
			}
			try
			{
				obj.ActivateConnection(transaction);
			}
			catch
			{
				this.PutObject(obj, owningObject);
				throw;
			}
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x000A1F80 File Offset: 0x000A0180
		internal DbConnectionInternal ReplaceConnection(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection)
		{
			DbConnectionInternal dbConnectionInternal = this.UserCreateRequest(owningObject, userOptions, oldConnection);
			if (dbConnectionInternal != null)
			{
				this.PrepareConnection(owningObject, dbConnectionInternal, oldConnection.EnlistedTransaction);
				oldConnection.PrepareForReplaceConnection();
				oldConnection.DeactivateConnection();
				oldConnection.Dispose();
			}
			return dbConnectionInternal;
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x000A1FBC File Offset: 0x000A01BC
		private DbConnectionInternal GetFromGeneralPool()
		{
			DbConnectionInternal dbConnectionInternal = null;
			if (!this._stackNew.TryPop(out dbConnectionInternal) && !this._stackOld.TryPop(out dbConnectionInternal))
			{
				dbConnectionInternal = null;
			}
			return dbConnectionInternal;
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x000A1FF0 File Offset: 0x000A01F0
		private DbConnectionInternal GetFromTransactedPool(out Transaction transaction)
		{
			transaction = ADP.GetCurrentTransaction();
			DbConnectionInternal dbConnectionInternal = null;
			if (null != transaction && this._transactedConnectionPool != null)
			{
				dbConnectionInternal = this._transactedConnectionPool.GetTransactedObject(transaction);
				if (dbConnectionInternal != null)
				{
					if (dbConnectionInternal.IsTransactionRoot)
					{
						try
						{
							dbConnectionInternal.IsConnectionAlive(true);
							return dbConnectionInternal;
						}
						catch
						{
							this.DestroyObject(dbConnectionInternal);
							throw;
						}
					}
					if (!dbConnectionInternal.IsConnectionAlive(false))
					{
						this.DestroyObject(dbConnectionInternal);
						dbConnectionInternal = null;
					}
				}
			}
			return dbConnectionInternal;
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x000A2068 File Offset: 0x000A0268
		private void PoolCreateRequest(object state)
		{
			if (DbConnectionPool.State.Running == this._state)
			{
				if (!this._pendingOpens.IsEmpty && this._pendingOpensWaiting == 0)
				{
					new Thread(new ThreadStart(this.WaitForPendingOpen))
					{
						IsBackground = true
					}.Start();
				}
				this.ReclaimEmancipatedObjects();
				if (!this.ErrorOccurred && this.NeedToReplenish)
				{
					if (this.UsingIntegrateSecurity && !this._identity.Equals(DbConnectionPoolIdentity.GetCurrent()))
					{
						return;
					}
					int num = 3;
					try
					{
						try
						{
						}
						finally
						{
							num = WaitHandle.WaitAny(this._waitHandles.GetHandles(true), this.CreationTimeout);
						}
						if (2 == num)
						{
							if (!this.ErrorOccurred)
							{
								while (this.NeedToReplenish)
								{
									DbConnectionInternal dbConnectionInternal = this.CreateObject(null, null, null);
									if (dbConnectionInternal == null)
									{
										break;
									}
									this.PutNewObject(dbConnectionInternal);
								}
							}
						}
						else if (258 == num)
						{
							this.QueuePoolCreateRequest();
						}
					}
					finally
					{
						if (2 == num)
						{
							this._waitHandles.CreationSemaphore.Release(1);
						}
					}
				}
			}
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x000A2178 File Offset: 0x000A0378
		internal void PutNewObject(DbConnectionInternal obj)
		{
			this._stackNew.Push(obj);
			this._waitHandles.PoolSemaphore.Release(1);
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x000A2198 File Offset: 0x000A0398
		internal void PutObject(DbConnectionInternal obj, object owningObject)
		{
			lock (obj)
			{
				obj.PrePush(owningObject);
			}
			this.DeactivateObject(obj);
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x000A21DC File Offset: 0x000A03DC
		internal void PutObjectFromTransactedPool(DbConnectionInternal obj)
		{
			if (this._state == DbConnectionPool.State.Running && obj.CanBePooled)
			{
				this.PutNewObject(obj);
				return;
			}
			this.DestroyObject(obj);
			this.QueuePoolCreateRequest();
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x000A2204 File Offset: 0x000A0404
		private void QueuePoolCreateRequest()
		{
			if (DbConnectionPool.State.Running == this._state)
			{
				ThreadPool.QueueUserWorkItem(this._poolCreateRequest);
			}
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x000A221C File Offset: 0x000A041C
		private bool ReclaimEmancipatedObjects()
		{
			bool flag = false;
			List<DbConnectionInternal> list = new List<DbConnectionInternal>();
			List<DbConnectionInternal> objectList = this._objectList;
			int num;
			lock (objectList)
			{
				num = this._objectList.Count;
				for (int i = 0; i < num; i++)
				{
					DbConnectionInternal dbConnectionInternal = this._objectList[i];
					if (dbConnectionInternal != null)
					{
						bool flag3 = false;
						try
						{
							Monitor.TryEnter(dbConnectionInternal, ref flag3);
							if (flag3 && dbConnectionInternal.IsEmancipated)
							{
								dbConnectionInternal.PrePush(null);
								list.Add(dbConnectionInternal);
							}
						}
						finally
						{
							if (flag3)
							{
								Monitor.Exit(dbConnectionInternal);
							}
						}
					}
				}
			}
			num = list.Count;
			for (int j = 0; j < num; j++)
			{
				DbConnectionInternal dbConnectionInternal2 = list[j];
				flag = true;
				dbConnectionInternal2.DetachCurrentTransactionIfEnded();
				this.DeactivateObject(dbConnectionInternal2);
			}
			return flag;
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x000A2308 File Offset: 0x000A0508
		internal void Startup()
		{
			this._cleanupTimer = this.CreateCleanupTimer();
			if (this.NeedToReplenish)
			{
				this.QueuePoolCreateRequest();
			}
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x000A2324 File Offset: 0x000A0524
		internal void Shutdown()
		{
			this._state = DbConnectionPool.State.ShuttingDown;
			Timer cleanupTimer = this._cleanupTimer;
			this._cleanupTimer = null;
			if (cleanupTimer != null)
			{
				cleanupTimer.Dispose();
			}
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x000A2350 File Offset: 0x000A0550
		internal void TransactionEnded(Transaction transaction, DbConnectionInternal transactedObject)
		{
			DbConnectionPool.TransactedConnectionPool transactedConnectionPool = this._transactedConnectionPool;
			if (transactedConnectionPool != null)
			{
				transactedConnectionPool.TransactionEnded(transaction, transactedObject);
			}
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x000A2370 File Offset: 0x000A0570
		private DbConnectionInternal UserCreateRequest(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection = null)
		{
			DbConnectionInternal dbConnectionInternal = null;
			if (this.ErrorOccurred)
			{
				throw this.TryCloneCachedException();
			}
			if ((oldConnection != null || this.Count < this.MaxPoolSize || this.MaxPoolSize == 0) && (oldConnection != null || (this.Count & 1) == 1 || !this.ReclaimEmancipatedObjects()))
			{
				dbConnectionInternal = this.CreateObject(owningObject, userOptions, oldConnection);
			}
			return dbConnectionInternal;
		}

		// Token: 0x040016F3 RID: 5875
		private const int MAX_Q_SIZE = 1048576;

		// Token: 0x040016F4 RID: 5876
		private const int SEMAPHORE_HANDLE = 0;

		// Token: 0x040016F5 RID: 5877
		private const int ERROR_HANDLE = 1;

		// Token: 0x040016F6 RID: 5878
		private const int CREATION_HANDLE = 2;

		// Token: 0x040016F7 RID: 5879
		private const int BOGUS_HANDLE = 3;

		// Token: 0x040016F8 RID: 5880
		private const int ERROR_WAIT_DEFAULT = 5000;

		// Token: 0x040016F9 RID: 5881
		private static readonly Random s_random = new Random(5101977);

		// Token: 0x040016FA RID: 5882
		private readonly int _cleanupWait;

		// Token: 0x040016FB RID: 5883
		private readonly DbConnectionPoolIdentity _identity;

		// Token: 0x040016FC RID: 5884
		private readonly DbConnectionFactory _connectionFactory;

		// Token: 0x040016FD RID: 5885
		private readonly DbConnectionPoolGroup _connectionPoolGroup;

		// Token: 0x040016FE RID: 5886
		private readonly DbConnectionPoolGroupOptions _connectionPoolGroupOptions;

		// Token: 0x040016FF RID: 5887
		private DbConnectionPoolProviderInfo _connectionPoolProviderInfo;

		// Token: 0x04001700 RID: 5888
		private DbConnectionPool.State _state;

		// Token: 0x04001701 RID: 5889
		private readonly ConcurrentStack<DbConnectionInternal> _stackOld = new ConcurrentStack<DbConnectionInternal>();

		// Token: 0x04001702 RID: 5890
		private readonly ConcurrentStack<DbConnectionInternal> _stackNew = new ConcurrentStack<DbConnectionInternal>();

		// Token: 0x04001703 RID: 5891
		private readonly ConcurrentQueue<DbConnectionPool.PendingGetConnection> _pendingOpens = new ConcurrentQueue<DbConnectionPool.PendingGetConnection>();

		// Token: 0x04001704 RID: 5892
		private int _pendingOpensWaiting;

		// Token: 0x04001705 RID: 5893
		private readonly WaitCallback _poolCreateRequest;

		// Token: 0x04001706 RID: 5894
		private int _waitCount;

		// Token: 0x04001707 RID: 5895
		private readonly DbConnectionPool.PoolWaitHandles _waitHandles;

		// Token: 0x04001708 RID: 5896
		private Exception _resError;

		// Token: 0x04001709 RID: 5897
		private volatile bool _errorOccurred;

		// Token: 0x0400170A RID: 5898
		private int _errorWait;

		// Token: 0x0400170B RID: 5899
		private Timer _errorTimer;

		// Token: 0x0400170C RID: 5900
		private Timer _cleanupTimer;

		// Token: 0x0400170D RID: 5901
		private readonly DbConnectionPool.TransactedConnectionPool _transactedConnectionPool;

		// Token: 0x0400170E RID: 5902
		private readonly List<DbConnectionInternal> _objectList;

		// Token: 0x0400170F RID: 5903
		private int _totalObjects;

		// Token: 0x0200030C RID: 780
		private enum State
		{
			// Token: 0x04001711 RID: 5905
			Initializing,
			// Token: 0x04001712 RID: 5906
			Running,
			// Token: 0x04001713 RID: 5907
			ShuttingDown
		}

		// Token: 0x0200030D RID: 781
		private sealed class TransactedConnectionList : List<DbConnectionInternal>
		{
			// Token: 0x060022D8 RID: 8920 RVA: 0x000A23D9 File Offset: 0x000A05D9
			internal TransactedConnectionList(int initialAllocation, Transaction tx)
				: base(initialAllocation)
			{
				this._transaction = tx;
			}

			// Token: 0x060022D9 RID: 8921 RVA: 0x000A23E9 File Offset: 0x000A05E9
			internal void Dispose()
			{
				if (null != this._transaction)
				{
					this._transaction.Dispose();
				}
			}

			// Token: 0x04001714 RID: 5908
			private Transaction _transaction;
		}

		// Token: 0x0200030E RID: 782
		private sealed class PendingGetConnection
		{
			// Token: 0x060022DA RID: 8922 RVA: 0x000A2404 File Offset: 0x000A0604
			public PendingGetConnection(long dueTime, DbConnection owner, TaskCompletionSource<DbConnectionInternal> completion, DbConnectionOptions userOptions)
			{
				this.DueTime = dueTime;
				this.Owner = owner;
				this.Completion = completion;
			}

			// Token: 0x17000605 RID: 1541
			// (get) Token: 0x060022DB RID: 8923 RVA: 0x000A2421 File Offset: 0x000A0621
			// (set) Token: 0x060022DC RID: 8924 RVA: 0x000A2429 File Offset: 0x000A0629
			public long DueTime { get; private set; }

			// Token: 0x17000606 RID: 1542
			// (get) Token: 0x060022DD RID: 8925 RVA: 0x000A2432 File Offset: 0x000A0632
			// (set) Token: 0x060022DE RID: 8926 RVA: 0x000A243A File Offset: 0x000A063A
			public DbConnection Owner { get; private set; }

			// Token: 0x17000607 RID: 1543
			// (get) Token: 0x060022DF RID: 8927 RVA: 0x000A2443 File Offset: 0x000A0643
			// (set) Token: 0x060022E0 RID: 8928 RVA: 0x000A244B File Offset: 0x000A064B
			public TaskCompletionSource<DbConnectionInternal> Completion { get; private set; }

			// Token: 0x17000608 RID: 1544
			// (get) Token: 0x060022E1 RID: 8929 RVA: 0x000A2454 File Offset: 0x000A0654
			// (set) Token: 0x060022E2 RID: 8930 RVA: 0x000A245C File Offset: 0x000A065C
			public DbConnectionOptions UserOptions { get; private set; }
		}

		// Token: 0x0200030F RID: 783
		private sealed class TransactedConnectionPool
		{
			// Token: 0x060022E3 RID: 8931 RVA: 0x000A2465 File Offset: 0x000A0665
			internal TransactedConnectionPool(DbConnectionPool pool)
			{
				this._pool = pool;
				this._transactedCxns = new Dictionary<Transaction, DbConnectionPool.TransactedConnectionList>();
			}

			// Token: 0x17000609 RID: 1545
			// (get) Token: 0x060022E4 RID: 8932 RVA: 0x000A248F File Offset: 0x000A068F
			internal int ObjectID
			{
				get
				{
					return this._objectID;
				}
			}

			// Token: 0x1700060A RID: 1546
			// (get) Token: 0x060022E5 RID: 8933 RVA: 0x000A2497 File Offset: 0x000A0697
			internal DbConnectionPool Pool
			{
				get
				{
					return this._pool;
				}
			}

			// Token: 0x060022E6 RID: 8934 RVA: 0x000A24A0 File Offset: 0x000A06A0
			internal DbConnectionInternal GetTransactedObject(Transaction transaction)
			{
				DbConnectionInternal dbConnectionInternal = null;
				bool flag = false;
				Dictionary<Transaction, DbConnectionPool.TransactedConnectionList> transactedCxns = this._transactedCxns;
				DbConnectionPool.TransactedConnectionList transactedConnectionList;
				lock (transactedCxns)
				{
					flag = this._transactedCxns.TryGetValue(transaction, out transactedConnectionList);
				}
				if (flag)
				{
					DbConnectionPool.TransactedConnectionList transactedConnectionList2 = transactedConnectionList;
					lock (transactedConnectionList2)
					{
						int num = transactedConnectionList.Count - 1;
						if (0 <= num)
						{
							dbConnectionInternal = transactedConnectionList[num];
							transactedConnectionList.RemoveAt(num);
						}
					}
				}
				return dbConnectionInternal;
			}

			// Token: 0x060022E7 RID: 8935 RVA: 0x000A253C File Offset: 0x000A073C
			internal void PutTransactedObject(Transaction transaction, DbConnectionInternal transactedObject)
			{
				bool flag = false;
				Dictionary<Transaction, DbConnectionPool.TransactedConnectionList> dictionary = this._transactedCxns;
				lock (dictionary)
				{
					DbConnectionPool.TransactedConnectionList transactedConnectionList;
					if (flag = this._transactedCxns.TryGetValue(transaction, out transactedConnectionList))
					{
						DbConnectionPool.TransactedConnectionList transactedConnectionList2 = transactedConnectionList;
						lock (transactedConnectionList2)
						{
							transactedConnectionList.Add(transactedObject);
						}
					}
				}
				if (!flag)
				{
					Transaction transaction2 = null;
					DbConnectionPool.TransactedConnectionList transactedConnectionList3 = null;
					try
					{
						transaction2 = transaction.Clone();
						transactedConnectionList3 = new DbConnectionPool.TransactedConnectionList(2, transaction2);
						dictionary = this._transactedCxns;
						lock (dictionary)
						{
							DbConnectionPool.TransactedConnectionList transactedConnectionList;
							if (flag = this._transactedCxns.TryGetValue(transaction, out transactedConnectionList))
							{
								DbConnectionPool.TransactedConnectionList transactedConnectionList2 = transactedConnectionList;
								lock (transactedConnectionList2)
								{
									transactedConnectionList.Add(transactedObject);
									return;
								}
							}
							transactedConnectionList3.Add(transactedObject);
							this._transactedCxns.Add(transaction2, transactedConnectionList3);
							transaction2 = null;
						}
					}
					finally
					{
						if (null != transaction2)
						{
							if (transactedConnectionList3 != null)
							{
								transactedConnectionList3.Dispose();
							}
							else
							{
								transaction2.Dispose();
							}
						}
					}
				}
			}

			// Token: 0x060022E8 RID: 8936 RVA: 0x000A2688 File Offset: 0x000A0888
			internal void TransactionEnded(Transaction transaction, DbConnectionInternal transactedObject)
			{
				int num = -1;
				Dictionary<Transaction, DbConnectionPool.TransactedConnectionList> transactedCxns = this._transactedCxns;
				lock (transactedCxns)
				{
					DbConnectionPool.TransactedConnectionList transactedConnectionList;
					if (this._transactedCxns.TryGetValue(transaction, out transactedConnectionList))
					{
						bool flag2 = false;
						DbConnectionPool.TransactedConnectionList transactedConnectionList2 = transactedConnectionList;
						lock (transactedConnectionList2)
						{
							num = transactedConnectionList.IndexOf(transactedObject);
							if (num >= 0)
							{
								transactedConnectionList.RemoveAt(num);
							}
							if (0 >= transactedConnectionList.Count)
							{
								this._transactedCxns.Remove(transaction);
								flag2 = true;
							}
						}
						if (flag2)
						{
							transactedConnectionList.Dispose();
						}
					}
				}
				if (0 <= num)
				{
					this.Pool.PutObjectFromTransactedPool(transactedObject);
				}
			}

			// Token: 0x04001719 RID: 5913
			private Dictionary<Transaction, DbConnectionPool.TransactedConnectionList> _transactedCxns;

			// Token: 0x0400171A RID: 5914
			private DbConnectionPool _pool;

			// Token: 0x0400171B RID: 5915
			private static int _objectTypeCount;

			// Token: 0x0400171C RID: 5916
			internal readonly int _objectID = Interlocked.Increment(ref DbConnectionPool.TransactedConnectionPool._objectTypeCount);
		}

		// Token: 0x02000310 RID: 784
		private sealed class PoolWaitHandles
		{
			// Token: 0x060022E9 RID: 8937 RVA: 0x000A2748 File Offset: 0x000A0948
			internal PoolWaitHandles()
			{
				this._poolSemaphore = new Semaphore(0, 1048576);
				this._errorEvent = new ManualResetEvent(false);
				this._creationSemaphore = new Semaphore(1, 1);
				this._handlesWithCreate = new WaitHandle[] { this._poolSemaphore, this._errorEvent, this._creationSemaphore };
				this._handlesWithoutCreate = new WaitHandle[] { this._poolSemaphore, this._errorEvent };
			}

			// Token: 0x1700060B RID: 1547
			// (get) Token: 0x060022EA RID: 8938 RVA: 0x000A27CA File Offset: 0x000A09CA
			internal Semaphore CreationSemaphore
			{
				get
				{
					return this._creationSemaphore;
				}
			}

			// Token: 0x1700060C RID: 1548
			// (get) Token: 0x060022EB RID: 8939 RVA: 0x000A27D2 File Offset: 0x000A09D2
			internal ManualResetEvent ErrorEvent
			{
				get
				{
					return this._errorEvent;
				}
			}

			// Token: 0x1700060D RID: 1549
			// (get) Token: 0x060022EC RID: 8940 RVA: 0x000A27DA File Offset: 0x000A09DA
			internal Semaphore PoolSemaphore
			{
				get
				{
					return this._poolSemaphore;
				}
			}

			// Token: 0x060022ED RID: 8941 RVA: 0x000A27E2 File Offset: 0x000A09E2
			internal WaitHandle[] GetHandles(bool withCreate)
			{
				if (!withCreate)
				{
					return this._handlesWithoutCreate;
				}
				return this._handlesWithCreate;
			}

			// Token: 0x0400171D RID: 5917
			private readonly Semaphore _poolSemaphore;

			// Token: 0x0400171E RID: 5918
			private readonly ManualResetEvent _errorEvent;

			// Token: 0x0400171F RID: 5919
			private readonly Semaphore _creationSemaphore;

			// Token: 0x04001720 RID: 5920
			private readonly WaitHandle[] _handlesWithCreate;

			// Token: 0x04001721 RID: 5921
			private readonly WaitHandle[] _handlesWithoutCreate;
		}
	}
}
