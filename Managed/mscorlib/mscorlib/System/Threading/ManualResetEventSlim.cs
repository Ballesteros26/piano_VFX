using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Provides a slimmed down version of <see cref="T:System.Threading.ManualResetEvent" />.</summary>
	// Token: 0x0200045A RID: 1114
	[ComVisible(false)]
	[DebuggerDisplay("Set = {IsSet}")]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class ManualResetEventSlim : IDisposable
	{
		/// <summary>Gets the underlying <see cref="T:System.Threading.WaitHandle" /> object for this <see cref="T:System.Threading.ManualResetEventSlim" />.</summary>
		/// <returns>The underlying <see cref="T:System.Threading.WaitHandle" /> event object fore this <see cref="T:System.Threading.ManualResetEventSlim" />.</returns>
		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x0600352E RID: 13614 RVA: 0x000C482F File Offset: 0x000C2A2F
		public WaitHandle WaitHandle
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.m_eventObj == null)
				{
					this.LazyInitializeEvent();
				}
				return this.m_eventObj;
			}
		}

		/// <summary>Gets whether the event is set.</summary>
		/// <returns>true if the event has is set; otherwise, false.</returns>
		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x0600352F RID: 13615 RVA: 0x000C4850 File Offset: 0x000C2A50
		// (set) Token: 0x06003530 RID: 13616 RVA: 0x000C4867 File Offset: 0x000C2A67
		public bool IsSet
		{
			get
			{
				return ManualResetEventSlim.ExtractStatePortion(this.m_combinedState, int.MinValue) != 0;
			}
			private set
			{
				this.UpdateStateAtomically((value ? 1 : 0) << 31, int.MinValue);
			}
		}

		/// <summary>Gets the number of spin waits that will be occur before falling back to a kernel-based wait operation.</summary>
		/// <returns>Returns the number of spin waits that will be occur before falling back to a kernel-based wait operation.</returns>
		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06003531 RID: 13617 RVA: 0x000C487E File Offset: 0x000C2A7E
		// (set) Token: 0x06003532 RID: 13618 RVA: 0x000C4894 File Offset: 0x000C2A94
		public int SpinCount
		{
			get
			{
				return ManualResetEventSlim.ExtractStatePortionAndShiftRight(this.m_combinedState, 1073217536, 19);
			}
			private set
			{
				this.m_combinedState = (this.m_combinedState & -1073217537) | (value << 19);
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06003533 RID: 13619 RVA: 0x000C48B1 File Offset: 0x000C2AB1
		// (set) Token: 0x06003534 RID: 13620 RVA: 0x000C48C6 File Offset: 0x000C2AC6
		private int Waiters
		{
			get
			{
				return ManualResetEventSlim.ExtractStatePortionAndShiftRight(this.m_combinedState, 524287, 0);
			}
			set
			{
				if (value >= 524287)
				{
					throw new InvalidOperationException(string.Format(Environment.GetResourceString("There are too many threads currently waiting on the event. A maximum of {0} waiting threads are supported."), 524287));
				}
				this.UpdateStateAtomically(value, 524287);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ManualResetEventSlim" /> class with an initial state of nonsignaled.</summary>
		// Token: 0x06003535 RID: 13621 RVA: 0x000C48FB File Offset: 0x000C2AFB
		public ManualResetEventSlim()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ManualResetEventSlim" /> class with a Boolean value indicating whether to set the intial state to signaled.</summary>
		/// <param name="initialState">true to set the initial state signaled; false to set the initial state to nonsignaled.</param>
		// Token: 0x06003536 RID: 13622 RVA: 0x000C4904 File Offset: 0x000C2B04
		public ManualResetEventSlim(bool initialState)
		{
			this.Initialize(initialState, 10);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ManualResetEventSlim" /> class with a Boolean value indicating whether to set the intial state to signaled and a specified spin count.</summary>
		/// <param name="initialState">true to set the initial state to signaled; false to set the initial state to nonsignaled.</param>
		/// <param name="spinCount">The number of spin waits that will occur before falling back to a kernel-based wait operation.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="spinCount" /> is less than 0 or greater than the maximum allowed value.</exception>
		// Token: 0x06003537 RID: 13623 RVA: 0x000C4918 File Offset: 0x000C2B18
		public ManualResetEventSlim(bool initialState, int spinCount)
		{
			if (spinCount < 0)
			{
				throw new ArgumentOutOfRangeException("spinCount");
			}
			if (spinCount > 2047)
			{
				throw new ArgumentOutOfRangeException("spinCount", string.Format(Environment.GetResourceString("The spinCount argument must be in the range 0 to {0}, inclusive."), 2047));
			}
			this.Initialize(initialState, spinCount);
		}

		// Token: 0x06003538 RID: 13624 RVA: 0x000C496E File Offset: 0x000C2B6E
		private void Initialize(bool initialState, int spinCount)
		{
			this.m_combinedState = (initialState ? int.MinValue : 0);
			this.SpinCount = (PlatformHelper.IsSingleProcessor ? 1 : spinCount);
		}

		// Token: 0x06003539 RID: 13625 RVA: 0x000C4994 File Offset: 0x000C2B94
		private void EnsureLockObjectCreated()
		{
			if (this.m_lock != null)
			{
				return;
			}
			object obj = new object();
			Interlocked.CompareExchange(ref this.m_lock, obj, null);
		}

		// Token: 0x0600353A RID: 13626 RVA: 0x000C49C0 File Offset: 0x000C2BC0
		private bool LazyInitializeEvent()
		{
			bool isSet = this.IsSet;
			ManualResetEvent manualResetEvent = new ManualResetEvent(isSet);
			if (Interlocked.CompareExchange<ManualResetEvent>(ref this.m_eventObj, manualResetEvent, null) != null)
			{
				manualResetEvent.Close();
				return false;
			}
			if (this.IsSet != isSet)
			{
				ManualResetEvent manualResetEvent2 = manualResetEvent;
				lock (manualResetEvent2)
				{
					if (this.m_eventObj == manualResetEvent)
					{
						manualResetEvent.Set();
					}
				}
			}
			return true;
		}

		/// <summary>Sets the state of the event to signaled, which allows one or more threads waiting on the event to proceed.</summary>
		// Token: 0x0600353B RID: 13627 RVA: 0x000C4A38 File Offset: 0x000C2C38
		public void Set()
		{
			this.Set(false);
		}

		// Token: 0x0600353C RID: 13628 RVA: 0x000C4A44 File Offset: 0x000C2C44
		private void Set(bool duringCancellation)
		{
			this.IsSet = true;
			if (this.Waiters > 0)
			{
				object @lock = this.m_lock;
				lock (@lock)
				{
					Monitor.PulseAll(this.m_lock);
				}
			}
			ManualResetEvent eventObj = this.m_eventObj;
			if (eventObj != null && !duringCancellation)
			{
				ManualResetEvent manualResetEvent = eventObj;
				lock (manualResetEvent)
				{
					if (this.m_eventObj != null)
					{
						this.m_eventObj.Set();
					}
				}
			}
		}

		/// <summary>Sets the state of the event to nonsignaled, which causes threads to block.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x0600353D RID: 13629 RVA: 0x000C4AE8 File Offset: 0x000C2CE8
		public void Reset()
		{
			this.ThrowIfDisposed();
			if (this.m_eventObj != null)
			{
				this.m_eventObj.Reset();
			}
			this.IsSet = false;
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> is set.</summary>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x0600353E RID: 13630 RVA: 0x000C4B10 File Offset: 0x000C2D10
		public void Wait()
		{
			this.Wait(-1, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> receives a signal, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed or the <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has been disposed.</exception>
		// Token: 0x0600353F RID: 13631 RVA: 0x000C4B2E File Offset: 0x000C2D2E
		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> is set, using a <see cref="T:System.TimeSpan" /> to measure the time interval.</summary>
		/// <returns>true if the <see cref="T:System.Threading.ManualResetEventSlim" /> was set; otherwise, false.</returns>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06003540 RID: 13632 RVA: 0x000C4B3C File Offset: 0x000C2D3C
		public bool Wait(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> is set, using a <see cref="T:System.TimeSpan" /> to measure the time interval, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <returns>true if the <see cref="T:System.Threading.ManualResetEventSlim" /> was set; otherwise, false.</returns>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed or the <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has been disposed.</exception>
		// Token: 0x06003541 RID: 13633 RVA: 0x000C4B7C File Offset: 0x000C2D7C
		public bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, cancellationToken);
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> is set, using a 32-bit signed integer to measure the time interval.</summary>
		/// <returns>true if the <see cref="T:System.Threading.ManualResetEventSlim" /> was set; otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06003542 RID: 13634 RVA: 0x000C4BB4 File Offset: 0x000C2DB4
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> is set, using a 32-bit signed integer to measure the time interval, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <returns>true if the <see cref="T:System.Threading.ManualResetEventSlim" /> was set; otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed or the <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has been disposed.</exception>
		// Token: 0x06003543 RID: 13635 RVA: 0x000C4BD4 File Offset: 0x000C2DD4
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			this.ThrowIfDisposed();
			cancellationToken.ThrowIfCancellationRequested();
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			if (!this.IsSet)
			{
				if (millisecondsTimeout == 0)
				{
					return false;
				}
				uint num = 0U;
				bool flag = false;
				int num2 = millisecondsTimeout;
				if (millisecondsTimeout != -1)
				{
					num = TimeoutHelper.GetTime();
					flag = true;
				}
				int num3 = 10;
				int num4 = 5;
				int num5 = 20;
				int spinCount = this.SpinCount;
				for (int i = 0; i < spinCount; i++)
				{
					if (this.IsSet)
					{
						return true;
					}
					if (i < num3)
					{
						if (i == num3 / 2)
						{
							Thread.Yield();
						}
						else
						{
							Thread.SpinWait(PlatformHelper.ProcessorCount * (4 << i));
						}
					}
					else if (i % num5 == 0)
					{
						Thread.Sleep(1);
					}
					else if (i % num4 == 0)
					{
						Thread.Sleep(0);
					}
					else
					{
						Thread.Yield();
					}
					if (i >= 100 && i % 10 == 0)
					{
						cancellationToken.ThrowIfCancellationRequested();
					}
				}
				this.EnsureLockObjectCreated();
				using (cancellationToken.InternalRegisterWithoutEC(ManualResetEventSlim.s_cancellationTokenCallback, this))
				{
					object @lock = this.m_lock;
					lock (@lock)
					{
						while (!this.IsSet)
						{
							cancellationToken.ThrowIfCancellationRequested();
							if (flag)
							{
								num2 = TimeoutHelper.UpdateTimeOut(num, millisecondsTimeout);
								if (num2 <= 0)
								{
									return false;
								}
							}
							this.Waiters++;
							if (this.IsSet)
							{
								int waiters = this.Waiters;
								this.Waiters = waiters - 1;
								return true;
							}
							try
							{
								if (!Monitor.Wait(this.m_lock, num2))
								{
									return false;
								}
							}
							finally
							{
								this.Waiters--;
							}
						}
					}
				}
				return true;
			}
			return true;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.ManualResetEventSlim" /> class.</summary>
		// Token: 0x06003544 RID: 13636 RVA: 0x000C4D98 File Offset: 0x000C2F98
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Threading.ManualResetEventSlim" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06003545 RID: 13637 RVA: 0x000C4DA8 File Offset: 0x000C2FA8
		protected virtual void Dispose(bool disposing)
		{
			if ((this.m_combinedState & 1073741824) != 0)
			{
				return;
			}
			this.m_combinedState |= 1073741824;
			if (disposing)
			{
				ManualResetEvent eventObj = this.m_eventObj;
				if (eventObj != null)
				{
					ManualResetEvent manualResetEvent = eventObj;
					lock (manualResetEvent)
					{
						eventObj.Close();
						this.m_eventObj = null;
					}
				}
			}
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x000C4E24 File Offset: 0x000C3024
		private void ThrowIfDisposed()
		{
			if ((this.m_combinedState & 1073741824) != 0)
			{
				throw new ObjectDisposedException(Environment.GetResourceString("The event has been disposed."));
			}
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x000C4E48 File Offset: 0x000C3048
		private static void CancellationTokenCallback(object obj)
		{
			ManualResetEventSlim manualResetEventSlim = obj as ManualResetEventSlim;
			object @lock = manualResetEventSlim.m_lock;
			lock (@lock)
			{
				Monitor.PulseAll(manualResetEventSlim.m_lock);
			}
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x000C4E98 File Offset: 0x000C3098
		private void UpdateStateAtomically(int newBits, int updateBitsMask)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int combinedState = this.m_combinedState;
				int num = (combinedState & ~updateBitsMask) | newBits;
				if (Interlocked.CompareExchange(ref this.m_combinedState, num, combinedState) == combinedState)
				{
					break;
				}
				spinWait.SpinOnce();
			}
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x000C4ED6 File Offset: 0x000C30D6
		private static int ExtractStatePortionAndShiftRight(int state, int mask, int rightBitShiftCount)
		{
			return (int)((uint)(state & mask) >> rightBitShiftCount);
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x000C4EE0 File Offset: 0x000C30E0
		private static int ExtractStatePortion(int state, int mask)
		{
			return state & mask;
		}

		// Token: 0x04001C55 RID: 7253
		private const int DEFAULT_SPIN_SP = 1;

		// Token: 0x04001C56 RID: 7254
		private const int DEFAULT_SPIN_MP = 10;

		// Token: 0x04001C57 RID: 7255
		private volatile object m_lock;

		// Token: 0x04001C58 RID: 7256
		private volatile ManualResetEvent m_eventObj;

		// Token: 0x04001C59 RID: 7257
		private volatile int m_combinedState;

		// Token: 0x04001C5A RID: 7258
		private const int SignalledState_BitMask = -2147483648;

		// Token: 0x04001C5B RID: 7259
		private const int SignalledState_ShiftCount = 31;

		// Token: 0x04001C5C RID: 7260
		private const int Dispose_BitMask = 1073741824;

		// Token: 0x04001C5D RID: 7261
		private const int SpinCountState_BitMask = 1073217536;

		// Token: 0x04001C5E RID: 7262
		private const int SpinCountState_ShiftCount = 19;

		// Token: 0x04001C5F RID: 7263
		private const int SpinCountState_MaxValue = 2047;

		// Token: 0x04001C60 RID: 7264
		private const int NumWaitersState_BitMask = 524287;

		// Token: 0x04001C61 RID: 7265
		private const int NumWaitersState_ShiftCount = 0;

		// Token: 0x04001C62 RID: 7266
		private const int NumWaitersState_MaxValue = 524287;

		// Token: 0x04001C63 RID: 7267
		private static Action<object> s_cancellationTokenCallback = new Action<object>(ManualResetEventSlim.CancellationTokenCallback);
	}
}
