using System;
using System.Collections;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Defines a lock that supports single writers and multiple readers. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020004AB RID: 1195
	[ComVisible(true)]
	public sealed class ReaderWriterLock : CriticalFinalizerObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ReaderWriterLock" /> class.</summary>
		// Token: 0x06003804 RID: 14340 RVA: 0x000CB338 File Offset: 0x000C9538
		public ReaderWriterLock()
		{
			this.writer_queue = new LockQueue(this);
			this.reader_locks = new Hashtable();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x000CB364 File Offset: 0x000C9564
		~ReaderWriterLock()
		{
		}

		/// <summary>Gets a value indicating whether the current thread holds a reader lock.</summary>
		/// <returns>true if the current thread holds a reader lock; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06003806 RID: 14342 RVA: 0x000CB38C File Offset: 0x000C958C
		public bool IsReaderLockHeld
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				bool flag2;
				lock (this)
				{
					flag2 = this.reader_locks.ContainsKey(Thread.CurrentThreadId);
				}
				return flag2;
			}
		}

		/// <summary>Gets a value indicating whether the current thread holds the writer lock.</summary>
		/// <returns>true if the current thread holds the writer lock; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06003807 RID: 14343 RVA: 0x000CB3D8 File Offset: 0x000C95D8
		public bool IsWriterLockHeld
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				bool flag2;
				lock (this)
				{
					flag2 = this.state < 0 && Thread.CurrentThreadId == this.writer_lock_owner;
				}
				return flag2;
			}
		}

		/// <summary>Gets the current sequence number.</summary>
		/// <returns>The current sequence number.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06003808 RID: 14344 RVA: 0x000CB428 File Offset: 0x000C9628
		public int WriterSeqNum
		{
			get
			{
				int num;
				lock (this)
				{
					num = this.seq_num;
				}
				return num;
			}
		}

		/// <summary>Acquires a reader lock, using an <see cref="T:System.Int32" /> value for the time-out.</summary>
		/// <param name="millisecondsTimeout">The time-out in milliseconds. </param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="millisecondsTimeout" /> expires before the lock request is granted. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003809 RID: 14345 RVA: 0x000CB468 File Offset: 0x000C9668
		public void AcquireReaderLock(int millisecondsTimeout)
		{
			this.AcquireReaderLock(millisecondsTimeout, 1);
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x000CB474 File Offset: 0x000C9674
		private void AcquireReaderLock(int millisecondsTimeout, int initialLockCount)
		{
			lock (this)
			{
				if (this.HasWriterLock())
				{
					this.AcquireWriterLock(millisecondsTimeout, initialLockCount);
				}
				else
				{
					object obj = this.reader_locks[Thread.CurrentThreadId];
					if (obj == null)
					{
						this.readers++;
						try
						{
							if (this.state < 0 || !this.writer_queue.IsEmpty)
							{
								while (Monitor.Wait(this, millisecondsTimeout))
								{
									if (this.state >= 0)
									{
										goto IL_007B;
									}
								}
								throw new ApplicationException("Timeout expired");
							}
							IL_007B:;
						}
						finally
						{
							this.readers--;
						}
						this.reader_locks[Thread.CurrentThreadId] = initialLockCount;
						this.state += initialLockCount;
					}
					else
					{
						this.reader_locks[Thread.CurrentThreadId] = (int)obj + 1;
						this.state++;
					}
				}
			}
		}

		/// <summary>Acquires a reader lock, using a <see cref="T:System.TimeSpan" /> value for the time-out.</summary>
		/// <param name="timeout">A TimeSpan specifying the time-out period. </param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="timeout" /> expires before the lock request is granted. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> specifies a negative value other than -1 milliseconds. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600380B RID: 14347 RVA: 0x000CB590 File Offset: 0x000C9790
		public void AcquireReaderLock(TimeSpan timeout)
		{
			int num = this.CheckTimeout(timeout);
			this.AcquireReaderLock(num, 1);
		}

		/// <summary>Acquires the writer lock, using an <see cref="T:System.Int32" /> value for the time-out.</summary>
		/// <param name="millisecondsTimeout">The time-out in milliseconds. </param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="timeout" /> expires before the lock request is granted. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600380C RID: 14348 RVA: 0x000CB5AD File Offset: 0x000C97AD
		public void AcquireWriterLock(int millisecondsTimeout)
		{
			this.AcquireWriterLock(millisecondsTimeout, 1);
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x000CB5B8 File Offset: 0x000C97B8
		private void AcquireWriterLock(int millisecondsTimeout, int initialLockCount)
		{
			lock (this)
			{
				if (this.HasWriterLock())
				{
					this.state--;
				}
				else
				{
					if (this.state != 0 || !this.writer_queue.IsEmpty)
					{
						while (this.writer_queue.Wait(millisecondsTimeout))
						{
							if (this.state == 0)
							{
								goto IL_005A;
							}
						}
						throw new ApplicationException("Timeout expired");
					}
					IL_005A:
					this.state = -initialLockCount;
					this.writer_lock_owner = Thread.CurrentThreadId;
					this.seq_num++;
				}
			}
		}

		/// <summary>Acquires the writer lock, using a <see cref="T:System.TimeSpan" /> value for the time-out.</summary>
		/// <param name="timeout">The TimeSpan specifying the time-out period. </param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="timeout" /> expires before the lock request is granted. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> specifies a negative value other than -1 milliseconds. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600380E RID: 14350 RVA: 0x000CB65C File Offset: 0x000C985C
		public void AcquireWriterLock(TimeSpan timeout)
		{
			int num = this.CheckTimeout(timeout);
			this.AcquireWriterLock(num, 1);
		}

		/// <summary>Indicates whether the writer lock has been granted to any thread since the sequence number was obtained.</summary>
		/// <returns>true if the writer lock has been granted to any thread since the sequence number was obtained; otherwise, false.</returns>
		/// <param name="seqNum">The sequence number. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600380F RID: 14351 RVA: 0x000CB67C File Offset: 0x000C987C
		public bool AnyWritersSince(int seqNum)
		{
			bool flag2;
			lock (this)
			{
				flag2 = this.seq_num > seqNum;
			}
			return flag2;
		}

		/// <summary>Restores the lock status of the thread to what it was before <see cref="M:System.Threading.ReaderWriterLock.UpgradeToWriterLock(System.Int32)" /> was called.</summary>
		/// <param name="lockCookie">A <see cref="T:System.Threading.LockCookie" /> returned by <see cref="M:System.Threading.ReaderWriterLock.UpgradeToWriterLock(System.Int32)" />. </param>
		/// <exception cref="T:System.ApplicationException">The thread does not have the writer lock. </exception>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="lockCookie" /> is a null pointer. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003810 RID: 14352 RVA: 0x000CB6BC File Offset: 0x000C98BC
		public void DowngradeFromWriterLock(ref LockCookie lockCookie)
		{
			lock (this)
			{
				if (!this.HasWriterLock())
				{
					throw new ApplicationException("The thread does not have the writer lock.");
				}
				if (lockCookie.WriterLocks != 0)
				{
					this.state++;
				}
				else
				{
					this.state = lockCookie.ReaderLocks;
					this.reader_locks[Thread.CurrentThreadId] = this.state;
					if (this.readers > 0)
					{
						Monitor.PulseAll(this);
					}
				}
			}
		}

		/// <summary>Releases the lock, regardless of the number of times the thread acquired the lock.</summary>
		/// <returns>A <see cref="T:System.Threading.LockCookie" /> value representing the released lock.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003811 RID: 14353 RVA: 0x000CB758 File Offset: 0x000C9958
		public LockCookie ReleaseLock()
		{
			LockCookie lockCookie;
			lock (this)
			{
				lockCookie = this.GetLockCookie();
				if (lockCookie.WriterLocks != 0)
				{
					this.ReleaseWriterLock(lockCookie.WriterLocks);
				}
				else if (lockCookie.ReaderLocks != 0)
				{
					this.ReleaseReaderLock(lockCookie.ReaderLocks, lockCookie.ReaderLocks);
				}
			}
			return lockCookie;
		}

		/// <summary>Decrements the lock count.</summary>
		/// <exception cref="T:System.ApplicationException">The thread does not have any reader or writer locks. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003812 RID: 14354 RVA: 0x000CB7C8 File Offset: 0x000C99C8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void ReleaseReaderLock()
		{
			lock (this)
			{
				if (!this.HasWriterLock())
				{
					if (this.state > 0)
					{
						object obj = this.reader_locks[Thread.CurrentThreadId];
						if (obj != null)
						{
							this.ReleaseReaderLock((int)obj, 1);
							return;
						}
					}
					throw new ApplicationException("The thread does not have any reader or writer locks.");
				}
				this.ReleaseWriterLock();
			}
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x000CB848 File Offset: 0x000C9A48
		private void ReleaseReaderLock(int currentCount, int releaseCount)
		{
			int num = currentCount - releaseCount;
			if (num == 0)
			{
				this.reader_locks.Remove(Thread.CurrentThreadId);
			}
			else
			{
				this.reader_locks[Thread.CurrentThreadId] = num;
			}
			this.state -= releaseCount;
			if (this.state == 0 && !this.writer_queue.IsEmpty)
			{
				this.writer_queue.Pulse();
			}
		}

		/// <summary>Decrements the lock count on the writer lock.</summary>
		/// <exception cref="T:System.ApplicationException">The thread does not have the writer lock. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003814 RID: 14356 RVA: 0x000CB8BC File Offset: 0x000C9ABC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void ReleaseWriterLock()
		{
			lock (this)
			{
				if (!this.HasWriterLock())
				{
					throw new ApplicationException("The thread does not have the writer lock.");
				}
				this.ReleaseWriterLock(1);
			}
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x000CB90C File Offset: 0x000C9B0C
		private void ReleaseWriterLock(int releaseCount)
		{
			this.state += releaseCount;
			if (this.state == 0)
			{
				if (this.readers > 0)
				{
					Monitor.PulseAll(this);
					return;
				}
				if (!this.writer_queue.IsEmpty)
				{
					this.writer_queue.Pulse();
				}
			}
		}

		/// <summary>Restores the lock status of the thread to what it was before calling <see cref="M:System.Threading.ReaderWriterLock.ReleaseLock" />.</summary>
		/// <param name="lockCookie">A <see cref="T:System.Threading.LockCookie" /> returned by <see cref="M:System.Threading.ReaderWriterLock.ReleaseLock" />. </param>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="lockCookie" /> is a null pointer. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003816 RID: 14358 RVA: 0x000CB94C File Offset: 0x000C9B4C
		public void RestoreLock(ref LockCookie lockCookie)
		{
			lock (this)
			{
				if (lockCookie.WriterLocks != 0)
				{
					this.AcquireWriterLock(-1, lockCookie.WriterLocks);
				}
				else if (lockCookie.ReaderLocks != 0)
				{
					this.AcquireReaderLock(-1, lockCookie.ReaderLocks);
				}
			}
		}

		/// <summary>Upgrades a reader lock to the writer lock, using an Int32 value for the time-out.</summary>
		/// <returns>A <see cref="T:System.Threading.LockCookie" /> value.</returns>
		/// <param name="millisecondsTimeout">The time-out in milliseconds. </param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="millisecondsTimeout" /> expires before the lock request is granted. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003817 RID: 14359 RVA: 0x000CB9B0 File Offset: 0x000C9BB0
		public LockCookie UpgradeToWriterLock(int millisecondsTimeout)
		{
			LockCookie lockCookie;
			lock (this)
			{
				lockCookie = this.GetLockCookie();
				if (lockCookie.WriterLocks != 0)
				{
					this.state--;
					return lockCookie;
				}
				if (lockCookie.ReaderLocks != 0)
				{
					this.ReleaseReaderLock(lockCookie.ReaderLocks, lockCookie.ReaderLocks);
				}
			}
			this.AcquireWriterLock(millisecondsTimeout);
			return lockCookie;
		}

		/// <summary>Upgrades a reader lock to the writer lock, using a TimeSpan value for the time-out.</summary>
		/// <returns>A <see cref="T:System.Threading.LockCookie" /> value.</returns>
		/// <param name="timeout">The TimeSpan specifying the time-out period. </param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="timeout" /> expires before the lock request is granted. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> specifies a negative value other than -1 milliseconds. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003818 RID: 14360 RVA: 0x000CBA2C File Offset: 0x000C9C2C
		public LockCookie UpgradeToWriterLock(TimeSpan timeout)
		{
			int num = this.CheckTimeout(timeout);
			return this.UpgradeToWriterLock(num);
		}

		// Token: 0x06003819 RID: 14361 RVA: 0x000CBA48 File Offset: 0x000C9C48
		private LockCookie GetLockCookie()
		{
			LockCookie lockCookie = new LockCookie(Thread.CurrentThreadId);
			if (this.HasWriterLock())
			{
				lockCookie.WriterLocks = -this.state;
			}
			else
			{
				object obj = this.reader_locks[Thread.CurrentThreadId];
				if (obj != null)
				{
					lockCookie.ReaderLocks = (int)obj;
				}
			}
			return lockCookie;
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x000CBAA0 File Offset: 0x000C9CA0
		private bool HasWriterLock()
		{
			return this.state < 0 && Thread.CurrentThreadId == this.writer_lock_owner;
		}

		// Token: 0x0600381B RID: 14363 RVA: 0x000CBABA File Offset: 0x000C9CBA
		private int CheckTimeout(TimeSpan timeout)
		{
			int num = (int)timeout.TotalMilliseconds;
			if (num < -1)
			{
				throw new ArgumentOutOfRangeException("timeout", "Number must be either non-negative or -1");
			}
			return num;
		}

		// Token: 0x04001D4D RID: 7501
		private int seq_num = 1;

		// Token: 0x04001D4E RID: 7502
		private int state;

		// Token: 0x04001D4F RID: 7503
		private int readers;

		// Token: 0x04001D50 RID: 7504
		private int writer_lock_owner;

		// Token: 0x04001D51 RID: 7505
		private LockQueue writer_queue;

		// Token: 0x04001D52 RID: 7506
		private Hashtable reader_locks;
	}
}
