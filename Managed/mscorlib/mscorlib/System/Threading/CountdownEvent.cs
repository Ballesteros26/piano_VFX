using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Represents a synchronization primitive that is signaled when its count reaches zero.</summary>
	// Token: 0x02000456 RID: 1110
	[ComVisible(false)]
	[DebuggerDisplay("Initial Count={InitialCount}, Current Count={CurrentCount}")]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class CountdownEvent : IDisposable
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Threading.CountdownEvent" /> class with the specified count.</summary>
		/// <param name="initialCount">The number of signals initially required to set the <see cref="T:System.Threading.CountdownEvent" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="initialCount" /> is less than 0.</exception>
		// Token: 0x06003510 RID: 13584 RVA: 0x000C430A File Offset: 0x000C250A
		public CountdownEvent(int initialCount)
		{
			if (initialCount < 0)
			{
				throw new ArgumentOutOfRangeException("initialCount");
			}
			this.m_initialCount = initialCount;
			this.m_currentCount = initialCount;
			this.m_event = new ManualResetEventSlim();
			if (initialCount == 0)
			{
				this.m_event.Set();
			}
		}

		/// <summary>Gets the number of remaining signals required to set the event.</summary>
		/// <returns> The number of remaining signals required to set the event.</returns>
		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06003511 RID: 13585 RVA: 0x000C434C File Offset: 0x000C254C
		public int CurrentCount
		{
			get
			{
				int currentCount = this.m_currentCount;
				if (currentCount >= 0)
				{
					return currentCount;
				}
				return 0;
			}
		}

		/// <summary>Gets the numbers of signals initially required to set the event.</summary>
		/// <returns> The number of signals initially required to set the event.</returns>
		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06003512 RID: 13586 RVA: 0x000C4369 File Offset: 0x000C2569
		public int InitialCount
		{
			get
			{
				return this.m_initialCount;
			}
		}

		/// <summary>Determines whether the event is set.</summary>
		/// <returns>true if the event is set; otherwise, false.</returns>
		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06003513 RID: 13587 RVA: 0x000C4371 File Offset: 0x000C2571
		public bool IsSet
		{
			get
			{
				return this.m_currentCount <= 0;
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that is used to wait for the event to be set.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that is used to wait for the event to be set.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06003514 RID: 13588 RVA: 0x000C4381 File Offset: 0x000C2581
		public WaitHandle WaitHandle
		{
			get
			{
				this.ThrowIfDisposed();
				return this.m_event.WaitHandle;
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.CountdownEvent" /> class.</summary>
		// Token: 0x06003515 RID: 13589 RVA: 0x000C4394 File Offset: 0x000C2594
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Threading.CountdownEvent" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06003516 RID: 13590 RVA: 0x000C43A3 File Offset: 0x000C25A3
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.m_event.Dispose();
				this.m_disposed = true;
			}
		}

		/// <summary>Registers a signal with the <see cref="T:System.Threading.CountdownEvent" />, decrementing the value of <see cref="P:System.Threading.CountdownEvent.CurrentCount" />.</summary>
		/// <returns>true if the signal caused the count to reach zero and the event was set; otherwise, false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is already set.</exception>
		// Token: 0x06003517 RID: 13591 RVA: 0x000C43BC File Offset: 0x000C25BC
		public bool Signal()
		{
			this.ThrowIfDisposed();
			if (this.m_currentCount <= 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Invalid attempt made to decrement the event's count below zero."));
			}
			int num = Interlocked.Decrement(ref this.m_currentCount);
			if (num == 0)
			{
				this.m_event.Set();
				return true;
			}
			if (num < 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Invalid attempt made to decrement the event's count below zero."));
			}
			return false;
		}

		/// <summary>Registers multiple signals with the <see cref="T:System.Threading.CountdownEvent" />, decrementing the value of <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> by the specified amount.</summary>
		/// <returns>true if the signals caused the count to reach zero and the event was set; otherwise, false.</returns>
		/// <param name="signalCount">The number of signals to register.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="signalCount" /> is less than 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is already set. -or- Or <paramref name="signalCount" /> is greater than <see cref="P:System.Threading.CountdownEvent.CurrentCount" />.</exception>
		// Token: 0x06003518 RID: 13592 RVA: 0x000C441C File Offset: 0x000C261C
		public bool Signal(int signalCount)
		{
			if (signalCount <= 0)
			{
				throw new ArgumentOutOfRangeException("signalCount");
			}
			this.ThrowIfDisposed();
			SpinWait spinWait = default(SpinWait);
			int currentCount;
			for (;;)
			{
				currentCount = this.m_currentCount;
				if (currentCount < signalCount)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.m_currentCount, currentCount - signalCount, currentCount) == currentCount)
				{
					goto IL_0055;
				}
				spinWait.SpinOnce();
			}
			throw new InvalidOperationException(Environment.GetResourceString("Invalid attempt made to decrement the event's count below zero."));
			IL_0055:
			if (currentCount == signalCount)
			{
				this.m_event.Set();
				return true;
			}
			return false;
		}

		/// <summary>Increments the <see cref="T:System.Threading.CountdownEvent" />'s current count by one.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is already set.-or-<see cref="P:System.Threading.CountdownEvent.CurrentCount" /> is equal to or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06003519 RID: 13593 RVA: 0x000C4490 File Offset: 0x000C2690
		public void AddCount()
		{
			this.AddCount(1);
		}

		/// <summary>Attempts to increment <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> by one.</summary>
		/// <returns>true if the increment succeeded; otherwise, false. If <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> is already at zero, this method will return false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> is equal to <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x0600351A RID: 13594 RVA: 0x000C4499 File Offset: 0x000C2699
		public bool TryAddCount()
		{
			return this.TryAddCount(1);
		}

		/// <summary>Increments the <see cref="T:System.Threading.CountdownEvent" />'s current count by a specified value.</summary>
		/// <param name="signalCount">The value by which to increase <see cref="P:System.Threading.CountdownEvent.CurrentCount" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="signalCount" /> is less than or equal to 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is already set.-or-<see cref="P:System.Threading.CountdownEvent.CurrentCount" /> is equal to or greater than <see cref="F:System.Int32.MaxValue" /> after count is incremented by <paramref name="signalCount." /></exception>
		// Token: 0x0600351B RID: 13595 RVA: 0x000C44A2 File Offset: 0x000C26A2
		public void AddCount(int signalCount)
		{
			if (!this.TryAddCount(signalCount))
			{
				throw new InvalidOperationException(Environment.GetResourceString("The event is already signaled and cannot be incremented."));
			}
		}

		/// <summary>Attempts to increment <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> by a specified value.</summary>
		/// <returns>true if the increment succeeded; otherwise, false. If <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> is already at zero this will return false.</returns>
		/// <param name="signalCount">The value by which to increase <see cref="P:System.Threading.CountdownEvent.CurrentCount" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="signalCount" /> is less than or equal to 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is already set.-or-<see cref="P:System.Threading.CountdownEvent.CurrentCount" /> + <paramref name="signalCount" /> is equal to or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x0600351C RID: 13596 RVA: 0x000C44C0 File Offset: 0x000C26C0
		public bool TryAddCount(int signalCount)
		{
			if (signalCount <= 0)
			{
				throw new ArgumentOutOfRangeException("signalCount");
			}
			this.ThrowIfDisposed();
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int currentCount = this.m_currentCount;
				if (currentCount <= 0)
				{
					break;
				}
				if (currentCount > 2147483647 - signalCount)
				{
					goto Block_3;
				}
				if (Interlocked.CompareExchange(ref this.m_currentCount, currentCount + signalCount, currentCount) == currentCount)
				{
					return true;
				}
				spinWait.SpinOnce();
			}
			return false;
			Block_3:
			throw new InvalidOperationException(Environment.GetResourceString("The increment operation would cause the CurrentCount to overflow."));
		}

		/// <summary>Resets the <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> to the value of <see cref="P:System.Threading.CountdownEvent.InitialCount" />.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed..</exception>
		// Token: 0x0600351D RID: 13597 RVA: 0x000C452F File Offset: 0x000C272F
		public void Reset()
		{
			this.Reset(this.m_initialCount);
		}

		/// <summary>Resets the <see cref="P:System.Threading.CountdownEvent.InitialCount" /> property to a specified value.</summary>
		/// <param name="count">The number of signals required to set the <see cref="T:System.Threading.CountdownEvent" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has alread been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than 0.</exception>
		// Token: 0x0600351E RID: 13598 RVA: 0x000C4540 File Offset: 0x000C2740
		public void Reset(int count)
		{
			this.ThrowIfDisposed();
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.m_currentCount = count;
			this.m_initialCount = count;
			if (count == 0)
			{
				this.m_event.Set();
				return;
			}
			this.m_event.Reset();
		}

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		// Token: 0x0600351F RID: 13599 RVA: 0x000C458C File Offset: 0x000C278C
		public void Wait()
		{
			this.Wait(-1, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. -or- The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		// Token: 0x06003520 RID: 13600 RVA: 0x000C45AA File Offset: 0x000C27AA
		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set, using a <see cref="T:System.TimeSpan" /> to measure the timeout.</summary>
		/// <returns>true if the <see cref="T:System.Threading.CountdownEvent" /> was set; otherwise, false.</returns>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06003521 RID: 13601 RVA: 0x000C45B8 File Offset: 0x000C27B8
		public bool Wait(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set, using a <see cref="T:System.TimeSpan" /> to measure the timeout, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <returns>true if the <see cref="T:System.Threading.CountdownEvent" /> was set; otherwise, false.</returns>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. -or- The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06003522 RID: 13602 RVA: 0x000C45F8 File Offset: 0x000C27F8
		public bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, cancellationToken);
		}

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set, using a 32-bit signed integer to measure the timeout.</summary>
		/// <returns>true if the <see cref="T:System.Threading.CountdownEvent" /> was set; otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		// Token: 0x06003523 RID: 13603 RVA: 0x000C4630 File Offset: 0x000C2830
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set, using a 32-bit signed integer to measure the timeout, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <returns>true if the <see cref="T:System.Threading.CountdownEvent" /> was set; otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. -or- The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		// Token: 0x06003524 RID: 13604 RVA: 0x000C4650 File Offset: 0x000C2850
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			this.ThrowIfDisposed();
			cancellationToken.ThrowIfCancellationRequested();
			bool flag = this.IsSet;
			if (!flag)
			{
				flag = this.m_event.Wait(millisecondsTimeout, cancellationToken);
			}
			return flag;
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x000C4692 File Offset: 0x000C2892
		private void ThrowIfDisposed()
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("CountdownEvent");
			}
		}

		// Token: 0x04001C4C RID: 7244
		private int m_initialCount;

		// Token: 0x04001C4D RID: 7245
		private volatile int m_currentCount;

		// Token: 0x04001C4E RID: 7246
		private ManualResetEventSlim m_event;

		// Token: 0x04001C4F RID: 7247
		private volatile bool m_disposed;
	}
}
