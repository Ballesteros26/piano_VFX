using System;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Represents a lock that is used to manage access to a resource, allowing multiple threads for reading or exclusive access for writing.</summary>
	// Token: 0x02000025 RID: 37
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class ReaderWriterLockSlim : IDisposable
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00002328 File Offset: 0x00000528
		private void InitializeThreadCounts()
		{
			this.upgradeLockOwnerId = -1;
			this.writeLockOwnerId = -1;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ReaderWriterLockSlim" /> class with default property values.</summary>
		// Token: 0x06000072 RID: 114 RVA: 0x00002338 File Offset: 0x00000538
		public ReaderWriterLockSlim()
			: this(LockRecursionPolicy.NoRecursion)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ReaderWriterLockSlim" /> class, specifying the lock recursion policy.</summary>
		/// <param name="recursionPolicy">One of the enumeration values that specifies the lock recursion policy. </param>
		// Token: 0x06000073 RID: 115 RVA: 0x00002341 File Offset: 0x00000541
		public ReaderWriterLockSlim(LockRecursionPolicy recursionPolicy)
		{
			if (recursionPolicy == LockRecursionPolicy.SupportsRecursion)
			{
				this.fIsReentrant = true;
			}
			this.InitializeThreadCounts();
			this.fNoWaiters = true;
			this.lockID = Interlocked.Increment(ref ReaderWriterLockSlim.s_nextLockID);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002371 File Offset: 0x00000571
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsRWEntryEmpty(ReaderWriterCount rwc)
		{
			return rwc.lockID == 0L || (rwc.readercount == 0 && rwc.writercount == 0 && rwc.upgradecount == 0);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002398 File Offset: 0x00000598
		private bool IsRwHashEntryChanged(ReaderWriterCount lrwc)
		{
			return lrwc.lockID != this.lockID;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000023AC File Offset: 0x000005AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ReaderWriterCount GetThreadRWCount(bool dontAllocate)
		{
			ReaderWriterCount next = ReaderWriterLockSlim.t_rwc;
			ReaderWriterCount readerWriterCount = null;
			while (next != null)
			{
				if (next.lockID == this.lockID)
				{
					return next;
				}
				if (!dontAllocate && readerWriterCount == null && ReaderWriterLockSlim.IsRWEntryEmpty(next))
				{
					readerWriterCount = next;
				}
				next = next.next;
			}
			if (dontAllocate)
			{
				return null;
			}
			if (readerWriterCount == null)
			{
				readerWriterCount = new ReaderWriterCount();
				readerWriterCount.next = ReaderWriterLockSlim.t_rwc;
				ReaderWriterLockSlim.t_rwc = readerWriterCount;
			}
			readerWriterCount.lockID = this.lockID;
			return readerWriterCount;
		}

		/// <summary>Tries to enter the lock in read mode.</summary>
		/// <exception cref="T:System.Threading.LockRecursionException">The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" /> and the current thread has already entered read mode. -or-The recursion number would exceed the capacity of the counter. This limit is so large that applications should never encounter it.</exception>
		// Token: 0x06000077 RID: 119 RVA: 0x00002419 File Offset: 0x00000619
		public void EnterReadLock()
		{
			this.TryEnterReadLock(-1);
		}

		/// <summary>Tries to enter the lock in read mode, with an optional time-out.</summary>
		/// <returns>true if the calling thread entered read mode, otherwise, false.</returns>
		/// <param name="timeout">The interval to wait, or -1 milliseconds to wait indefinitely. </param>
		/// <exception cref="T:System.Threading.LockRecursionException">The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" /> and the current thread has already entered the lock. -or-The recursion number would exceed the capacity of the counter. The limit is so large that applications should never encounter it.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="timeout" /> is negative, but it is not equal to -1 milliseconds, which is the only negative value allowed.-or-The value of <paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" /> milliseconds. </exception>
		// Token: 0x06000078 RID: 120 RVA: 0x00002423 File Offset: 0x00000623
		public bool TryEnterReadLock(TimeSpan timeout)
		{
			return this.TryEnterReadLock(new ReaderWriterLockSlim.TimeoutTracker(timeout));
		}

		/// <summary>Tries to enter the lock in read mode, with an optional integer time-out.</summary>
		/// <returns>true if the calling thread entered read mode, otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or -1 (<see cref="F:System.Threading.Timeout.Infinite" />) to wait indefinitely.</param>
		/// <exception cref="T:System.Threading.LockRecursionException">The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" /> and the current thread has already entered the lock. -or-The recursion number would exceed the capacity of the counter. The limit is so large that applications should never encounter it.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="millisecondsTimeout" /> is negative, but it is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> (-1), which is the only negative value allowed. </exception>
		// Token: 0x06000079 RID: 121 RVA: 0x00002431 File Offset: 0x00000631
		public bool TryEnterReadLock(int millisecondsTimeout)
		{
			return this.TryEnterReadLock(new ReaderWriterLockSlim.TimeoutTracker(millisecondsTimeout));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000243F File Offset: 0x0000063F
		private bool TryEnterReadLock(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			return this.TryEnterReadLockCore(timeout);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002448 File Offset: 0x00000648
		private bool TryEnterReadLockCore(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			if (this.fDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			ReaderWriterCount readerWriterCount;
			if (!this.fIsReentrant)
			{
				if (managedThreadId == this.writeLockOwnerId)
				{
					throw new LockRecursionException(global::SR.GetString("A read lock may not be acquired with the write lock held in this mode."));
				}
				this.EnterMyLock();
				readerWriterCount = this.GetThreadRWCount(false);
				if (readerWriterCount.readercount > 0)
				{
					this.ExitMyLock();
					throw new LockRecursionException(global::SR.GetString("Recursive read lock acquisitions not allowed in this mode."));
				}
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					readerWriterCount.readercount++;
					this.owners += 1U;
					this.ExitMyLock();
					return true;
				}
			}
			else
			{
				this.EnterMyLock();
				readerWriterCount = this.GetThreadRWCount(false);
				if (readerWriterCount.readercount > 0)
				{
					readerWriterCount.readercount++;
					this.ExitMyLock();
					return true;
				}
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					readerWriterCount.readercount++;
					this.owners += 1U;
					this.ExitMyLock();
					this.fUpgradeThreadHoldingRead = true;
					return true;
				}
				if (managedThreadId == this.writeLockOwnerId)
				{
					readerWriterCount.readercount++;
					this.owners += 1U;
					this.ExitMyLock();
					return true;
				}
			}
			bool flag = true;
			int num = 0;
			while (this.owners >= 268435454U)
			{
				if (num < 20)
				{
					this.ExitMyLock();
					if (timeout.IsExpired)
					{
						return false;
					}
					num++;
					ReaderWriterLockSlim.SpinWait(num);
					this.EnterMyLock();
					if (this.IsRwHashEntryChanged(readerWriterCount))
					{
						readerWriterCount = this.GetThreadRWCount(false);
					}
				}
				else if (this.readEvent == null)
				{
					this.LazyCreateEvent(ref this.readEvent, false);
					if (this.IsRwHashEntryChanged(readerWriterCount))
					{
						readerWriterCount = this.GetThreadRWCount(false);
					}
				}
				else
				{
					flag = this.WaitOnEvent(this.readEvent, ref this.numReadWaiters, timeout, false);
					if (!flag)
					{
						return false;
					}
					if (this.IsRwHashEntryChanged(readerWriterCount))
					{
						readerWriterCount = this.GetThreadRWCount(false);
					}
				}
			}
			this.owners += 1U;
			readerWriterCount.readercount++;
			this.ExitMyLock();
			return flag;
		}

		/// <summary>Tries to enter the lock in write mode.</summary>
		/// <exception cref="T:System.Threading.LockRecursionException">The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" /> and the current thread has already entered the lock in any mode. -or-The current thread has entered read mode, so trying to enter the lock in write mode would create the possibility of a deadlock. -or-The recursion number would exceed the capacity of the counter. The limit is so large that applications should never encounter it.</exception>
		// Token: 0x0600007C RID: 124 RVA: 0x00002650 File Offset: 0x00000850
		public void EnterWriteLock()
		{
			this.TryEnterWriteLock(-1);
		}

		/// <summary>Tries to enter the lock in write mode, with an optional time-out.</summary>
		/// <returns>true if the calling thread entered write mode, otherwise, false.</returns>
		/// <param name="timeout">The interval to wait, or -1 milliseconds to wait indefinitely.</param>
		/// <exception cref="T:System.Threading.LockRecursionException">The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" /> and the current thread has already entered the lock. -or-The current thread initially entered the lock in read mode, and therefore trying to enter write mode would create the possibility of a deadlock. -or-The recursion number would exceed the capacity of the counter. The limit is so large that applications should never encounter it.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="timeout" /> is negative, but it is not equal to -1 milliseconds, which is the only negative value allowed.-or-The value of <paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" /> milliseconds. </exception>
		// Token: 0x0600007D RID: 125 RVA: 0x0000265A File Offset: 0x0000085A
		public bool TryEnterWriteLock(TimeSpan timeout)
		{
			return this.TryEnterWriteLock(new ReaderWriterLockSlim.TimeoutTracker(timeout));
		}

		/// <summary>Tries to enter the lock in write mode, with an optional time-out.</summary>
		/// <returns>true if the calling thread entered write mode, otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or -1 (<see cref="F:System.Threading.Timeout.Infinite" />) to wait indefinitely.</param>
		/// <exception cref="T:System.Threading.LockRecursionException">The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" /> and the current thread has already entered the lock. -or-The current thread initially entered the lock in read mode, and therefore trying to enter write mode would create the possibility of a deadlock. -or-The recursion number would exceed the capacity of the counter. The limit is so large that applications should never encounter it.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="millisecondsTimeout" /> is negative, but it is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> (-1), which is the only negative value allowed. </exception>
		// Token: 0x0600007E RID: 126 RVA: 0x00002668 File Offset: 0x00000868
		public bool TryEnterWriteLock(int millisecondsTimeout)
		{
			return this.TryEnterWriteLock(new ReaderWriterLockSlim.TimeoutTracker(millisecondsTimeout));
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002676 File Offset: 0x00000876
		private bool TryEnterWriteLock(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			return this.TryEnterWriteLockCore(timeout);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002680 File Offset: 0x00000880
		private bool TryEnterWriteLockCore(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			if (this.fDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			bool flag = false;
			ReaderWriterCount readerWriterCount;
			if (!this.fIsReentrant)
			{
				if (managedThreadId == this.writeLockOwnerId)
				{
					throw new LockRecursionException(global::SR.GetString("Recursive write lock acquisitions not allowed in this mode."));
				}
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					flag = true;
				}
				this.EnterMyLock();
				readerWriterCount = this.GetThreadRWCount(true);
				if (readerWriterCount != null && readerWriterCount.readercount > 0)
				{
					this.ExitMyLock();
					throw new LockRecursionException(global::SR.GetString("Write lock may not be acquired with read lock held. This pattern is prone to deadlocks. Please ensure that read locks are released before taking a write lock. If an upgrade is necessary, use an upgrade lock in place of the read lock."));
				}
			}
			else
			{
				this.EnterMyLock();
				readerWriterCount = this.GetThreadRWCount(false);
				if (managedThreadId == this.writeLockOwnerId)
				{
					readerWriterCount.writercount++;
					this.ExitMyLock();
					return true;
				}
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					flag = true;
				}
				else if (readerWriterCount.readercount > 0)
				{
					this.ExitMyLock();
					throw new LockRecursionException(global::SR.GetString("Write lock may not be acquired with read lock held. This pattern is prone to deadlocks. Please ensure that read locks are released before taking a write lock. If an upgrade is necessary, use an upgrade lock in place of the read lock."));
				}
			}
			int num = 0;
			while (!this.IsWriterAcquired())
			{
				if (flag)
				{
					uint numReaders = this.GetNumReaders();
					if (numReaders == 1U)
					{
						this.SetWriterAcquired();
					}
					else
					{
						if (numReaders != 2U || readerWriterCount == null)
						{
							goto IL_012E;
						}
						if (this.IsRwHashEntryChanged(readerWriterCount))
						{
							readerWriterCount = this.GetThreadRWCount(false);
						}
						if (readerWriterCount.readercount <= 0)
						{
							goto IL_012E;
						}
						this.SetWriterAcquired();
					}
					IL_01C6:
					if (this.fIsReentrant)
					{
						if (this.IsRwHashEntryChanged(readerWriterCount))
						{
							readerWriterCount = this.GetThreadRWCount(false);
						}
						readerWriterCount.writercount++;
					}
					this.ExitMyLock();
					this.writeLockOwnerId = managedThreadId;
					return true;
				}
				IL_012E:
				if (num < 20)
				{
					this.ExitMyLock();
					if (timeout.IsExpired)
					{
						return false;
					}
					num++;
					ReaderWriterLockSlim.SpinWait(num);
					this.EnterMyLock();
				}
				else if (flag)
				{
					if (this.waitUpgradeEvent == null)
					{
						this.LazyCreateEvent(ref this.waitUpgradeEvent, true);
					}
					else if (!this.WaitOnEvent(this.waitUpgradeEvent, ref this.numWriteUpgradeWaiters, timeout, true))
					{
						return false;
					}
				}
				else if (this.writeEvent == null)
				{
					this.LazyCreateEvent(ref this.writeEvent, true);
				}
				else if (!this.WaitOnEvent(this.writeEvent, ref this.numWriteWaiters, timeout, true))
				{
					return false;
				}
			}
			this.SetWriterAcquired();
			goto IL_01C6;
		}

		/// <summary>Tries to enter the lock in upgradeable mode.</summary>
		/// <exception cref="T:System.Threading.LockRecursionException">The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" /> and the current thread has already entered the lock in any mode. -or-The current thread has entered read mode, so trying to enter upgradeable mode would create the possibility of a deadlock. -or-The recursion number would exceed the capacity of the counter. The limit is so large that applications should never encounter it.</exception>
		// Token: 0x06000081 RID: 129 RVA: 0x00002888 File Offset: 0x00000A88
		public void EnterUpgradeableReadLock()
		{
			this.TryEnterUpgradeableReadLock(-1);
		}

		/// <summary>Tries to enter the lock in upgradeable mode, with an optional time-out.</summary>
		/// <returns>true if the calling thread entered upgradeable mode, otherwise, false.</returns>
		/// <param name="timeout">The interval to wait, or -1 milliseconds to wait indefinitely.</param>
		/// <exception cref="T:System.Threading.LockRecursionException">The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" /> and the current thread has already entered the lock. -or-The current thread initially entered the lock in read mode, and therefore trying to enter upgradeable mode would create the possibility of a deadlock. -or-The recursion number would exceed the capacity of the counter. The limit is so large that applications should never encounter it.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="timeout" /> is negative, but it is not equal to -1 milliseconds, which is the only negative value allowed.-or-The value of <paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" /> milliseconds. </exception>
		// Token: 0x06000082 RID: 130 RVA: 0x00002892 File Offset: 0x00000A92
		public bool TryEnterUpgradeableReadLock(TimeSpan timeout)
		{
			return this.TryEnterUpgradeableReadLock(new ReaderWriterLockSlim.TimeoutTracker(timeout));
		}

		/// <summary>Tries to enter the lock in upgradeable mode, with an optional time-out.</summary>
		/// <returns>true if the calling thread entered upgradeable mode, otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or -1 (<see cref="F:System.Threading.Timeout.Infinite" />) to wait indefinitely.</param>
		/// <exception cref="T:System.Threading.LockRecursionException">The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" /> and the current thread has already entered the lock. -or-The current thread initially entered the lock in read mode, and therefore trying to enter upgradeable mode would create the possibility of a deadlock. -or-The recursion number would exceed the capacity of the counter. The limit is so large that applications should never encounter it.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="millisecondsTimeout" /> is negative, but it is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> (-1), which is the only negative value allowed. </exception>
		// Token: 0x06000083 RID: 131 RVA: 0x000028A0 File Offset: 0x00000AA0
		public bool TryEnterUpgradeableReadLock(int millisecondsTimeout)
		{
			return this.TryEnterUpgradeableReadLock(new ReaderWriterLockSlim.TimeoutTracker(millisecondsTimeout));
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000028AE File Offset: 0x00000AAE
		private bool TryEnterUpgradeableReadLock(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			return this.TryEnterUpgradeableReadLockCore(timeout);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000028B8 File Offset: 0x00000AB8
		private bool TryEnterUpgradeableReadLockCore(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			if (this.fDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			ReaderWriterCount readerWriterCount;
			if (!this.fIsReentrant)
			{
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					throw new LockRecursionException(global::SR.GetString("Recursive upgradeable lock acquisitions not allowed in this mode."));
				}
				if (managedThreadId == this.writeLockOwnerId)
				{
					throw new LockRecursionException(global::SR.GetString("Upgradeable lock may not be acquired with write lock held in this mode. Acquiring Upgradeable lock gives the ability to read along with an option to upgrade to a writer."));
				}
				this.EnterMyLock();
				readerWriterCount = this.GetThreadRWCount(true);
				if (readerWriterCount != null && readerWriterCount.readercount > 0)
				{
					this.ExitMyLock();
					throw new LockRecursionException(global::SR.GetString("Upgradeable lock may not be acquired with read lock held."));
				}
			}
			else
			{
				this.EnterMyLock();
				readerWriterCount = this.GetThreadRWCount(false);
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					readerWriterCount.upgradecount++;
					this.ExitMyLock();
					return true;
				}
				if (managedThreadId == this.writeLockOwnerId)
				{
					this.owners += 1U;
					this.upgradeLockOwnerId = managedThreadId;
					readerWriterCount.upgradecount++;
					if (readerWriterCount.readercount > 0)
					{
						this.fUpgradeThreadHoldingRead = true;
					}
					this.ExitMyLock();
					return true;
				}
				if (readerWriterCount.readercount > 0)
				{
					this.ExitMyLock();
					throw new LockRecursionException(global::SR.GetString("Upgradeable lock may not be acquired with read lock held."));
				}
			}
			int num = 0;
			while (this.upgradeLockOwnerId != -1 || this.owners >= 268435454U)
			{
				if (num < 20)
				{
					this.ExitMyLock();
					if (timeout.IsExpired)
					{
						return false;
					}
					num++;
					ReaderWriterLockSlim.SpinWait(num);
					this.EnterMyLock();
				}
				else if (this.upgradeEvent == null)
				{
					this.LazyCreateEvent(ref this.upgradeEvent, true);
				}
				else if (!this.WaitOnEvent(this.upgradeEvent, ref this.numUpgradeWaiters, timeout, false))
				{
					return false;
				}
			}
			this.owners += 1U;
			this.upgradeLockOwnerId = managedThreadId;
			if (this.fIsReentrant)
			{
				if (this.IsRwHashEntryChanged(readerWriterCount))
				{
					readerWriterCount = this.GetThreadRWCount(false);
				}
				readerWriterCount.upgradecount++;
			}
			this.ExitMyLock();
			return true;
		}

		/// <summary>Reduces the recursion count for read mode, and exits read mode if the resulting count is 0 (zero).</summary>
		/// <exception cref="T:System.Threading.SynchronizationLockException">The current thread has not entered the lock in read mode.</exception>
		// Token: 0x06000086 RID: 134 RVA: 0x00002A98 File Offset: 0x00000C98
		public void ExitReadLock()
		{
			this.EnterMyLock();
			ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
			if (threadRWCount == null || threadRWCount.readercount < 1)
			{
				this.ExitMyLock();
				throw new SynchronizationLockException(global::SR.GetString("The read lock is being released without being held."));
			}
			if (this.fIsReentrant)
			{
				if (threadRWCount.readercount > 1)
				{
					threadRWCount.readercount--;
					this.ExitMyLock();
					return;
				}
				if (Thread.CurrentThread.ManagedThreadId == this.upgradeLockOwnerId)
				{
					this.fUpgradeThreadHoldingRead = false;
				}
			}
			this.owners -= 1U;
			threadRWCount.readercount--;
			this.ExitAndWakeUpAppropriateWaiters();
		}

		/// <summary>Reduces the recursion count for write mode, and exits write mode if the resulting count is 0 (zero).</summary>
		/// <exception cref="T:System.Threading.SynchronizationLockException">The current thread has not entered the lock in write mode.</exception>
		// Token: 0x06000087 RID: 135 RVA: 0x00002B38 File Offset: 0x00000D38
		public void ExitWriteLock()
		{
			if (!this.fIsReentrant)
			{
				if (Thread.CurrentThread.ManagedThreadId != this.writeLockOwnerId)
				{
					throw new SynchronizationLockException(global::SR.GetString("The write lock is being released without being held."));
				}
				this.EnterMyLock();
			}
			else
			{
				this.EnterMyLock();
				ReaderWriterCount threadRWCount = this.GetThreadRWCount(false);
				if (threadRWCount == null)
				{
					this.ExitMyLock();
					throw new SynchronizationLockException(global::SR.GetString("The write lock is being released without being held."));
				}
				if (threadRWCount.writercount < 1)
				{
					this.ExitMyLock();
					throw new SynchronizationLockException(global::SR.GetString("The write lock is being released without being held."));
				}
				threadRWCount.writercount--;
				if (threadRWCount.writercount > 0)
				{
					this.ExitMyLock();
					return;
				}
			}
			this.ClearWriterAcquired();
			this.writeLockOwnerId = -1;
			this.ExitAndWakeUpAppropriateWaiters();
		}

		/// <summary>Reduces the recursion count for upgradeable mode, and exits upgradeable mode if the resulting count is 0 (zero).</summary>
		/// <exception cref="T:System.Threading.SynchronizationLockException">The current thread has not entered the lock in upgradeable mode.</exception>
		// Token: 0x06000088 RID: 136 RVA: 0x00002BEC File Offset: 0x00000DEC
		public void ExitUpgradeableReadLock()
		{
			if (!this.fIsReentrant)
			{
				if (Thread.CurrentThread.ManagedThreadId != this.upgradeLockOwnerId)
				{
					throw new SynchronizationLockException(global::SR.GetString("The upgradeable lock is being released without being held."));
				}
				this.EnterMyLock();
			}
			else
			{
				this.EnterMyLock();
				ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
				if (threadRWCount == null)
				{
					this.ExitMyLock();
					throw new SynchronizationLockException(global::SR.GetString("The upgradeable lock is being released without being held."));
				}
				if (threadRWCount.upgradecount < 1)
				{
					this.ExitMyLock();
					throw new SynchronizationLockException(global::SR.GetString("The upgradeable lock is being released without being held."));
				}
				threadRWCount.upgradecount--;
				if (threadRWCount.upgradecount > 0)
				{
					this.ExitMyLock();
					return;
				}
				this.fUpgradeThreadHoldingRead = false;
			}
			this.owners -= 1U;
			this.upgradeLockOwnerId = -1;
			this.ExitAndWakeUpAppropriateWaiters();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002CB0 File Offset: 0x00000EB0
		private void LazyCreateEvent(ref EventWaitHandle waitEvent, bool makeAutoResetEvent)
		{
			this.ExitMyLock();
			EventWaitHandle eventWaitHandle;
			if (makeAutoResetEvent)
			{
				eventWaitHandle = new AutoResetEvent(false);
			}
			else
			{
				eventWaitHandle = new ManualResetEvent(false);
			}
			this.EnterMyLock();
			if (waitEvent == null)
			{
				waitEvent = eventWaitHandle;
				return;
			}
			eventWaitHandle.Close();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002CEC File Offset: 0x00000EEC
		private bool WaitOnEvent(EventWaitHandle waitEvent, ref uint numWaiters, ReaderWriterLockSlim.TimeoutTracker timeout, bool isWriteWaiter)
		{
			waitEvent.Reset();
			numWaiters += 1U;
			this.fNoWaiters = false;
			if (this.numWriteWaiters == 1U)
			{
				this.SetWritersWaiting();
			}
			if (this.numWriteUpgradeWaiters == 1U)
			{
				this.SetUpgraderWaiting();
			}
			bool flag = false;
			this.ExitMyLock();
			try
			{
				flag = waitEvent.WaitOne(timeout.RemainingMilliseconds);
			}
			finally
			{
				this.EnterMyLock();
				numWaiters -= 1U;
				if (this.numWriteWaiters == 0U && this.numWriteUpgradeWaiters == 0U && this.numUpgradeWaiters == 0U && this.numReadWaiters == 0U)
				{
					this.fNoWaiters = true;
				}
				if (this.numWriteWaiters == 0U)
				{
					this.ClearWritersWaiting();
				}
				if (this.numWriteUpgradeWaiters == 0U)
				{
					this.ClearUpgraderWaiting();
				}
				if (!flag)
				{
					if (isWriteWaiter)
					{
						this.ExitAndWakeUpAppropriateReadWaiters();
					}
					else
					{
						this.ExitMyLock();
					}
				}
			}
			return flag;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002DBC File Offset: 0x00000FBC
		private void ExitAndWakeUpAppropriateWaiters()
		{
			if (this.fNoWaiters)
			{
				this.ExitMyLock();
				return;
			}
			this.ExitAndWakeUpAppropriateWaitersPreferringWriters();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002DD4 File Offset: 0x00000FD4
		private void ExitAndWakeUpAppropriateWaitersPreferringWriters()
		{
			uint numReaders = this.GetNumReaders();
			if (this.fIsReentrant && this.numWriteUpgradeWaiters > 0U && this.fUpgradeThreadHoldingRead && numReaders == 2U)
			{
				this.ExitMyLock();
				this.waitUpgradeEvent.Set();
				return;
			}
			if (numReaders == 1U && this.numWriteUpgradeWaiters > 0U)
			{
				this.ExitMyLock();
				this.waitUpgradeEvent.Set();
				return;
			}
			if (numReaders == 0U && this.numWriteWaiters > 0U)
			{
				this.ExitMyLock();
				this.writeEvent.Set();
				return;
			}
			this.ExitAndWakeUpAppropriateReadWaiters();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002E60 File Offset: 0x00001060
		private void ExitAndWakeUpAppropriateReadWaiters()
		{
			if (this.numWriteWaiters != 0U || this.numWriteUpgradeWaiters != 0U || this.fNoWaiters)
			{
				this.ExitMyLock();
				return;
			}
			bool flag = this.numReadWaiters > 0U;
			bool flag2 = this.numUpgradeWaiters != 0U && this.upgradeLockOwnerId == -1;
			this.ExitMyLock();
			if (flag)
			{
				this.readEvent.Set();
			}
			if (flag2)
			{
				this.upgradeEvent.Set();
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002ECD File Offset: 0x000010CD
		private bool IsWriterAcquired()
		{
			return (this.owners & 3221225471U) == 0U;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002EDE File Offset: 0x000010DE
		private void SetWriterAcquired()
		{
			this.owners |= 2147483648U;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002EF2 File Offset: 0x000010F2
		private void ClearWriterAcquired()
		{
			this.owners &= 2147483647U;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002F06 File Offset: 0x00001106
		private void SetWritersWaiting()
		{
			this.owners |= 1073741824U;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002F1A File Offset: 0x0000111A
		private void ClearWritersWaiting()
		{
			this.owners &= 3221225471U;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002F2E File Offset: 0x0000112E
		private void SetUpgraderWaiting()
		{
			this.owners |= 536870912U;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002F42 File Offset: 0x00001142
		private void ClearUpgraderWaiting()
		{
			this.owners &= 3758096383U;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002F56 File Offset: 0x00001156
		private uint GetNumReaders()
		{
			return this.owners & 268435455U;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002F64 File Offset: 0x00001164
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void EnterMyLock()
		{
			if (Interlocked.CompareExchange(ref this.myLock, 1, 0) != 0)
			{
				this.EnterMyLockSpin();
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002F7C File Offset: 0x0000117C
		private void EnterMyLockSpin()
		{
			int processorCount = Environment.ProcessorCount;
			int num = 0;
			for (;;)
			{
				if (num < 10 && processorCount > 1)
				{
					Thread.SpinWait(20 * (num + 1));
				}
				else if (num < 15)
				{
					Thread.Sleep(0);
				}
				else
				{
					Thread.Sleep(1);
				}
				if (this.myLock == 0 && Interlocked.CompareExchange(ref this.myLock, 1, 0) == 0)
				{
					break;
				}
				num++;
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002FD7 File Offset: 0x000011D7
		private void ExitMyLock()
		{
			Volatile.Write(ref this.myLock, 0);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002FE5 File Offset: 0x000011E5
		private static void SpinWait(int SpinCount)
		{
			if (SpinCount < 5 && Environment.ProcessorCount > 1)
			{
				Thread.SpinWait(20 * SpinCount);
				return;
			}
			if (SpinCount < 17)
			{
				Thread.Sleep(0);
				return;
			}
			Thread.Sleep(1);
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.ReaderWriterLockSlim" /> class.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600009A RID: 154 RVA: 0x0000300F File Offset: 0x0000120F
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003018 File Offset: 0x00001218
		private void Dispose(bool disposing)
		{
			if (disposing && !this.fDisposed)
			{
				if (this.WaitingReadCount > 0 || this.WaitingUpgradeCount > 0 || this.WaitingWriteCount > 0)
				{
					throw new SynchronizationLockException(global::SR.GetString("The lock is being disposed while still being used. It either is being held by a thread and/or has active waiters waiting to acquire the lock."));
				}
				if (this.IsReadLockHeld || this.IsUpgradeableReadLockHeld || this.IsWriteLockHeld)
				{
					throw new SynchronizationLockException(global::SR.GetString("The lock is being disposed while still being used. It either is being held by a thread and/or has active waiters waiting to acquire the lock."));
				}
				if (this.writeEvent != null)
				{
					this.writeEvent.Close();
					this.writeEvent = null;
				}
				if (this.readEvent != null)
				{
					this.readEvent.Close();
					this.readEvent = null;
				}
				if (this.upgradeEvent != null)
				{
					this.upgradeEvent.Close();
					this.upgradeEvent = null;
				}
				if (this.waitUpgradeEvent != null)
				{
					this.waitUpgradeEvent.Close();
					this.waitUpgradeEvent = null;
				}
				this.fDisposed = true;
			}
		}

		/// <summary>Gets a value that indicates whether the current thread has entered the lock in read mode.</summary>
		/// <returns>true if the current thread has entered read mode; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000030F8 File Offset: 0x000012F8
		public bool IsReadLockHeld
		{
			get
			{
				return this.RecursiveReadCount > 0;
			}
		}

		/// <summary>Gets a value that indicates whether the current thread has entered the lock in upgradeable mode. </summary>
		/// <returns>true if the current thread has entered upgradeable mode; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003106 File Offset: 0x00001306
		public bool IsUpgradeableReadLockHeld
		{
			get
			{
				return this.RecursiveUpgradeCount > 0;
			}
		}

		/// <summary>Gets a value that indicates whether the current thread has entered the lock in write mode.</summary>
		/// <returns>true if the current thread has entered write mode; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003114 File Offset: 0x00001314
		public bool IsWriteLockHeld
		{
			get
			{
				return this.RecursiveWriteCount > 0;
			}
		}

		/// <summary>Gets a value that indicates the recursion policy for the current <see cref="T:System.Threading.ReaderWriterLockSlim" /> object.</summary>
		/// <returns>One of the enumeration values that specifies the lock recursion policy.</returns>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003122 File Offset: 0x00001322
		public LockRecursionPolicy RecursionPolicy
		{
			get
			{
				if (this.fIsReentrant)
				{
					return LockRecursionPolicy.SupportsRecursion;
				}
				return LockRecursionPolicy.NoRecursion;
			}
		}

		/// <summary>Gets the total number of unique threads that have entered the lock in read mode.</summary>
		/// <returns>The number of unique threads that have entered the lock in read mode.</returns>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003130 File Offset: 0x00001330
		public int CurrentReadCount
		{
			get
			{
				int numReaders = (int)this.GetNumReaders();
				if (this.upgradeLockOwnerId != -1)
				{
					return numReaders - 1;
				}
				return numReaders;
			}
		}

		/// <summary>Gets the number of times the current thread has entered the lock in read mode, as an indication of recursion.</summary>
		/// <returns>0 (zero) if the current thread has not entered read mode, 1 if the thread has entered read mode but has not entered it recursively, or n if the thread has entered the lock recursively n - 1 times.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003154 File Offset: 0x00001354
		public int RecursiveReadCount
		{
			get
			{
				int num = 0;
				ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
				if (threadRWCount != null)
				{
					num = threadRWCount.readercount;
				}
				return num;
			}
		}

		/// <summary>Gets the number of times the current thread has entered the lock in upgradeable mode, as an indication of recursion.</summary>
		/// <returns>0 if the current thread has not entered upgradeable mode, 1 if the thread has entered upgradeable mode but has not entered it recursively, or n if the thread has entered upgradeable mode recursively n - 1 times.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003178 File Offset: 0x00001378
		public int RecursiveUpgradeCount
		{
			get
			{
				if (this.fIsReentrant)
				{
					int num = 0;
					ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
					if (threadRWCount != null)
					{
						num = threadRWCount.upgradecount;
					}
					return num;
				}
				if (Thread.CurrentThread.ManagedThreadId == this.upgradeLockOwnerId)
				{
					return 1;
				}
				return 0;
			}
		}

		/// <summary>Gets the number of times the current thread has entered the lock in write mode, as an indication of recursion.</summary>
		/// <returns>0 if the current thread has not entered write mode, 1 if the thread has entered write mode but has not entered it recursively, or n if the thread has entered write mode recursively n - 1 times.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000031B8 File Offset: 0x000013B8
		public int RecursiveWriteCount
		{
			get
			{
				if (this.fIsReentrant)
				{
					int num = 0;
					ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
					if (threadRWCount != null)
					{
						num = threadRWCount.writercount;
					}
					return num;
				}
				if (Thread.CurrentThread.ManagedThreadId == this.writeLockOwnerId)
				{
					return 1;
				}
				return 0;
			}
		}

		/// <summary>Gets the total number of threads that are waiting to enter the lock in read mode.</summary>
		/// <returns>The total number of threads that are waiting to enter read mode.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000031F8 File Offset: 0x000013F8
		public int WaitingReadCount
		{
			get
			{
				return (int)this.numReadWaiters;
			}
		}

		/// <summary>Gets the total number of threads that are waiting to enter the lock in upgradeable mode.</summary>
		/// <returns>The total number of threads that are waiting to enter upgradeable mode.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003200 File Offset: 0x00001400
		public int WaitingUpgradeCount
		{
			get
			{
				return (int)this.numUpgradeWaiters;
			}
		}

		/// <summary>Gets the total number of threads that are waiting to enter the lock in write mode.</summary>
		/// <returns>The total number of threads that are waiting to enter write mode.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003208 File Offset: 0x00001408
		public int WaitingWriteCount
		{
			get
			{
				return (int)this.numWriteWaiters;
			}
		}

		// Token: 0x040001D5 RID: 469
		private bool fIsReentrant;

		// Token: 0x040001D6 RID: 470
		private int myLock;

		// Token: 0x040001D7 RID: 471
		private const int LockSpinCycles = 20;

		// Token: 0x040001D8 RID: 472
		private const int LockSpinCount = 10;

		// Token: 0x040001D9 RID: 473
		private const int LockSleep0Count = 5;

		// Token: 0x040001DA RID: 474
		private uint numWriteWaiters;

		// Token: 0x040001DB RID: 475
		private uint numReadWaiters;

		// Token: 0x040001DC RID: 476
		private uint numWriteUpgradeWaiters;

		// Token: 0x040001DD RID: 477
		private uint numUpgradeWaiters;

		// Token: 0x040001DE RID: 478
		private bool fNoWaiters;

		// Token: 0x040001DF RID: 479
		private int upgradeLockOwnerId;

		// Token: 0x040001E0 RID: 480
		private int writeLockOwnerId;

		// Token: 0x040001E1 RID: 481
		private EventWaitHandle writeEvent;

		// Token: 0x040001E2 RID: 482
		private EventWaitHandle readEvent;

		// Token: 0x040001E3 RID: 483
		private EventWaitHandle upgradeEvent;

		// Token: 0x040001E4 RID: 484
		private EventWaitHandle waitUpgradeEvent;

		// Token: 0x040001E5 RID: 485
		private static long s_nextLockID;

		// Token: 0x040001E6 RID: 486
		private long lockID;

		// Token: 0x040001E7 RID: 487
		[ThreadStatic]
		private static ReaderWriterCount t_rwc;

		// Token: 0x040001E8 RID: 488
		private bool fUpgradeThreadHoldingRead;

		// Token: 0x040001E9 RID: 489
		private const int MaxSpinCount = 20;

		// Token: 0x040001EA RID: 490
		private uint owners;

		// Token: 0x040001EB RID: 491
		private const uint WRITER_HELD = 2147483648U;

		// Token: 0x040001EC RID: 492
		private const uint WAITING_WRITERS = 1073741824U;

		// Token: 0x040001ED RID: 493
		private const uint WAITING_UPGRADER = 536870912U;

		// Token: 0x040001EE RID: 494
		private const uint MAX_READER = 268435454U;

		// Token: 0x040001EF RID: 495
		private const uint READER_MASK = 268435455U;

		// Token: 0x040001F0 RID: 496
		private bool fDisposed;

		// Token: 0x02000026 RID: 38
		private struct TimeoutTracker
		{
			// Token: 0x060000A7 RID: 167 RVA: 0x00003210 File Offset: 0x00001410
			public TimeoutTracker(TimeSpan timeout)
			{
				long num = (long)timeout.TotalMilliseconds;
				if (num < -1L || num > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("timeout");
				}
				this.m_total = (int)num;
				if (this.m_total != -1 && this.m_total != 0)
				{
					this.m_start = Environment.TickCount;
					return;
				}
				this.m_start = 0;
			}

			// Token: 0x060000A8 RID: 168 RVA: 0x0000326B File Offset: 0x0000146B
			public TimeoutTracker(int millisecondsTimeout)
			{
				if (millisecondsTimeout < -1)
				{
					throw new ArgumentOutOfRangeException("millisecondsTimeout");
				}
				this.m_total = millisecondsTimeout;
				if (this.m_total != -1 && this.m_total != 0)
				{
					this.m_start = Environment.TickCount;
					return;
				}
				this.m_start = 0;
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x060000A9 RID: 169 RVA: 0x000032A8 File Offset: 0x000014A8
			public int RemainingMilliseconds
			{
				get
				{
					if (this.m_total == -1 || this.m_total == 0)
					{
						return this.m_total;
					}
					int num = Environment.TickCount - this.m_start;
					if (num < 0 || num >= this.m_total)
					{
						return 0;
					}
					return this.m_total - num;
				}
			}

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x060000AA RID: 170 RVA: 0x000032F1 File Offset: 0x000014F1
			public bool IsExpired
			{
				get
				{
					return this.RemainingMilliseconds == 0;
				}
			}

			// Token: 0x040001F1 RID: 497
			private int m_total;

			// Token: 0x040001F2 RID: 498
			private int m_start;
		}
	}
}
