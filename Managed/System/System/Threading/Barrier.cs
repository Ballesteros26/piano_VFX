using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Enables multiple tasks to cooperatively work on an algorithm in parallel through multiple phases.</summary>
	// Token: 0x0200012A RID: 298
	[ComVisible(false)]
	[DebuggerDisplay("Participant Count={ParticipantCount},Participants Remaining={ParticipantsRemaining}")]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class Barrier : IDisposable
	{
		/// <summary>Gets the number of participants in the barrier that haven’t yet signaled in the current phase.</summary>
		/// <returns>Returns the number of participants in the barrier that haven’t yet signaled in the current phase.</returns>
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x000275F8 File Offset: 0x000257F8
		public int ParticipantsRemaining
		{
			get
			{
				int currentTotalCount = this.m_currentTotalCount;
				int num = currentTotalCount & 32767;
				int num2 = (currentTotalCount & 2147418112) >> 16;
				return num - num2;
			}
		}

		/// <summary>Gets the total number of participants in the barrier.</summary>
		/// <returns>Returns the total number of participants in the barrier.</returns>
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x00027622 File Offset: 0x00025822
		public int ParticipantCount
		{
			get
			{
				return this.m_currentTotalCount & 32767;
			}
		}

		/// <summary>Gets the number of the barrier's current phase.</summary>
		/// <returns>Returns the number of the barrier's current phase.</returns>
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x00027632 File Offset: 0x00025832
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x0002763F File Offset: 0x0002583F
		public long CurrentPhaseNumber
		{
			get
			{
				return Volatile.Read(ref this.m_currentPhase);
			}
			internal set
			{
				Volatile.Write(ref this.m_currentPhase, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Barrier" /> class.</summary>
		/// <param name="participantCount">The number of participating threads.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="participantCount" /> is less than 0 or greater than 32,767.</exception>
		// Token: 0x0600080A RID: 2058 RVA: 0x0002764D File Offset: 0x0002584D
		public Barrier(int participantCount)
			: this(participantCount, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Barrier" /> class.</summary>
		/// <param name="participantCount">The number of participating threads.</param>
		/// <param name="postPhaseAction">The <see cref="T:System.Action`1" /> to be executed after each phase. null (Nothing in Visual Basic) may be passed to indicate no action is taken.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="participantCount" /> is less than 0 or greater than 32,767.</exception>
		// Token: 0x0600080B RID: 2059 RVA: 0x00027658 File Offset: 0x00025858
		public Barrier(int participantCount, Action<Barrier> postPhaseAction)
		{
			if (participantCount < 0 || participantCount > 32767)
			{
				throw new ArgumentOutOfRangeException("participantCount", participantCount, global::SR.GetString("The participantCount argument must be non-negative and less than or equal to 32767."));
			}
			this.m_currentTotalCount = participantCount;
			this.m_postPhaseAction = postPhaseAction;
			this.m_oddEvent = new ManualResetEventSlim(true);
			this.m_evenEvent = new ManualResetEventSlim(false);
			if (postPhaseAction != null && !ExecutionContext.IsFlowSuppressed())
			{
				this.m_ownerThreadContext = ExecutionContext.Capture();
			}
			this.m_actionCallerID = 0;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x000276D6 File Offset: 0x000258D6
		private void GetCurrentTotal(int currentTotal, out int current, out int total, out bool sense)
		{
			total = currentTotal & 32767;
			current = (currentTotal & 2147418112) >> 16;
			sense = (currentTotal & int.MinValue) == 0;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00027700 File Offset: 0x00025900
		private bool SetCurrentTotal(int currentTotal, int current, int total, bool sense)
		{
			int num = (current << 16) | total;
			if (!sense)
			{
				num |= int.MinValue;
			}
			return Interlocked.CompareExchange(ref this.m_currentTotalCount, num, currentTotal) == currentTotal;
		}

		/// <summary>Notifies the <see cref="T:System.Threading.Barrier" /> that there will be an additional participant.</summary>
		/// <returns>The phase number of the barrier in which the new participants will first participate.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Adding a participant would cause the barrier's participant count to exceed 32,767.-or-The method was invoked from within a post-phase action.</exception>
		// Token: 0x0600080E RID: 2062 RVA: 0x00027730 File Offset: 0x00025930
		public long AddParticipant()
		{
			long num;
			try
			{
				num = this.AddParticipants(1);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new InvalidOperationException(global::SR.GetString("Adding participantCount participants would result in the number of participants exceeding the maximum number allowed."));
			}
			return num;
		}

		/// <summary>Notifies the <see cref="T:System.Threading.Barrier" /> that there will be additional participants.</summary>
		/// <returns>The phase number of the barrier in which the new participants will first participate.</returns>
		/// <param name="participantCount">The number of additional participants to add to the barrier.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="participantCount" /> is less than 0.-or-Adding <paramref name="participantCount" /> participants would cause the barrier's participant count to exceed 32,767.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method was invoked from within a post-phase action.</exception>
		// Token: 0x0600080F RID: 2063 RVA: 0x0002776C File Offset: 0x0002596C
		public long AddParticipants(int participantCount)
		{
			this.ThrowIfDisposed();
			if (participantCount < 1)
			{
				throw new ArgumentOutOfRangeException("participantCount", participantCount, global::SR.GetString("The participantCount argument must be a positive value."));
			}
			if (participantCount > 32767)
			{
				throw new ArgumentOutOfRangeException("participantCount", global::SR.GetString("Adding participantCount participants would result in the number of participants exceeding the maximum number allowed."));
			}
			if (this.m_actionCallerID != 0 && Thread.CurrentThread.ManagedThreadId == this.m_actionCallerID)
			{
				throw new InvalidOperationException(global::SR.GetString("This method may not be called from within the postPhaseAction."));
			}
			SpinWait spinWait = default(SpinWait);
			bool flag;
			for (;;)
			{
				int currentTotalCount = this.m_currentTotalCount;
				int num;
				int num2;
				this.GetCurrentTotal(currentTotalCount, out num, out num2, out flag);
				if (participantCount + num2 > 32767)
				{
					break;
				}
				if (this.SetCurrentTotal(currentTotalCount, num, num2 + participantCount, flag))
				{
					goto Block_6;
				}
				spinWait.SpinOnce();
			}
			throw new ArgumentOutOfRangeException("participantCount", global::SR.GetString("Adding participantCount participants would result in the number of participants exceeding the maximum number allowed."));
			Block_6:
			long currentPhaseNumber = this.CurrentPhaseNumber;
			long num3 = ((flag != (currentPhaseNumber % 2L == 0L)) ? (currentPhaseNumber + 1L) : currentPhaseNumber);
			if (num3 != currentPhaseNumber)
			{
				if (flag)
				{
					this.m_oddEvent.Wait();
				}
				else
				{
					this.m_evenEvent.Wait();
				}
			}
			else if (flag && this.m_evenEvent.IsSet)
			{
				this.m_evenEvent.Reset();
			}
			else if (!flag && this.m_oddEvent.IsSet)
			{
				this.m_oddEvent.Reset();
			}
			return num3;
		}

		/// <summary>Notifies the <see cref="T:System.Threading.Barrier" /> that there will be one less participant.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The barrier already has 0 participants.-or-The method was invoked from within a post-phase action.</exception>
		// Token: 0x06000810 RID: 2064 RVA: 0x000278C0 File Offset: 0x00025AC0
		public void RemoveParticipant()
		{
			this.RemoveParticipants(1);
		}

		/// <summary>Notifies the <see cref="T:System.Threading.Barrier" /> that there will be fewer participants.</summary>
		/// <param name="participantCount">The number of additional participants to remove from the barrier.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="participantCount" /> is less than 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">The barrier already has 0 participants.-or-The method was invoked from within a post-phase action. -or-current participant count is less than the specified participantCount</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The total participant count is less than the specified<paramref name=" participantCount" /></exception>
		// Token: 0x06000811 RID: 2065 RVA: 0x000278CC File Offset: 0x00025ACC
		public void RemoveParticipants(int participantCount)
		{
			this.ThrowIfDisposed();
			if (participantCount < 1)
			{
				throw new ArgumentOutOfRangeException("participantCount", participantCount, global::SR.GetString("The participantCount argument must be a positive value."));
			}
			if (this.m_actionCallerID != 0 && Thread.CurrentThread.ManagedThreadId == this.m_actionCallerID)
			{
				throw new InvalidOperationException(global::SR.GetString("This method may not be called from within the postPhaseAction."));
			}
			SpinWait spinWait = default(SpinWait);
			bool flag;
			for (;;)
			{
				int currentTotalCount = this.m_currentTotalCount;
				int num;
				int num2;
				this.GetCurrentTotal(currentTotalCount, out num, out num2, out flag);
				if (num2 < participantCount)
				{
					break;
				}
				if (num2 - participantCount < num)
				{
					goto Block_5;
				}
				int num3 = num2 - participantCount;
				if (num3 > 0 && num == num3)
				{
					if (this.SetCurrentTotal(currentTotalCount, 0, num2 - participantCount, !flag))
					{
						goto Block_8;
					}
				}
				else if (this.SetCurrentTotal(currentTotalCount, num, num2 - participantCount, flag))
				{
					return;
				}
				spinWait.SpinOnce();
			}
			throw new ArgumentOutOfRangeException("participantCount", global::SR.GetString("The participantCount argument must be less than or equal the number of participants."));
			Block_5:
			throw new InvalidOperationException(global::SR.GetString("The participantCount argument is greater than the number of participants that haven't yet arrived at the barrier in this phase."));
			Block_8:
			this.FinishPhase(flag);
		}

		/// <summary>Signals that a participant has reached the barrier and waits for all other participants to reach the barrier as well.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method was invoked from within a post-phase action, the barrier currently has 0 participants, or the barrier is signaled by more threads than are registered as participants.</exception>
		/// <exception cref="T:System.Threading.BarrierPostPhaseException">If an exception is thrown from the post phase action of a Barrier after all participating threads have called SignalAndWait, the exception will be wrapped in a BarrierPostPhaseException and be thrown on all participating threads.</exception>
		// Token: 0x06000812 RID: 2066 RVA: 0x000279BC File Offset: 0x00025BBC
		public void SignalAndWait()
		{
			this.SignalAndWait(default(CancellationToken));
		}

		/// <summary>Signals that a participant has reached the barrier and waits for all other participants to reach the barrier, while observing a cancellation token.</summary>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method was invoked from within a post-phase action, the barrier currently has 0 participants, or the barrier is signaled by more threads than are registered as participants.</exception>
		// Token: 0x06000813 RID: 2067 RVA: 0x000279D8 File Offset: 0x00025BD8
		public void SignalAndWait(CancellationToken cancellationToken)
		{
			this.SignalAndWait(-1, cancellationToken);
		}

		/// <summary>Signals that a participant has reached the barrier and waits for all other participants to reach the barrier as well, using a <see cref="T:System.TimeSpan" /> object to measure the time interval.</summary>
		/// <returns>true if all other participants reached the barrier; otherwise, false.</returns>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" />is a negative number other than -1 milliseconds, which represents an infinite time-out, or it is greater than 32,767.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method was invoked from within a post-phase action, the barrier currently has 0 participants, or the barrier is signaled by more threads than are registered as participants.</exception>
		// Token: 0x06000814 RID: 2068 RVA: 0x000279E4 File Offset: 0x00025BE4
		public bool SignalAndWait(TimeSpan timeout)
		{
			return this.SignalAndWait(timeout, default(CancellationToken));
		}

		/// <summary>Signals that a participant has reached the barrier and waits for all other participants to reach the barrier as well, using a <see cref="T:System.TimeSpan" /> object to measure the time interval, while observing a cancellation token.</summary>
		/// <returns>true if all other participants reached the barrier; otherwise, false.</returns>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" />is a negative number other than -1 milliseconds, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method was invoked from within a post-phase action, the barrier currently has 0 participants, or the barrier is signaled by more threads than are registered as participants.</exception>
		// Token: 0x06000815 RID: 2069 RVA: 0x00027A04 File Offset: 0x00025C04
		public bool SignalAndWait(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, global::SR.GetString("The specified timeout must represent a value between -1 and Int32.MaxValue, inclusive."));
			}
			return this.SignalAndWait((int)timeout.TotalMilliseconds, cancellationToken);
		}

		/// <summary>Signals that a participant has reached the barrier and waits for all other participants to reach the barrier as well, using a 32-bit signed integer to measure the timeout.</summary>
		/// <returns>if all participants reached the barrier within the specified time; otherwise false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method was invoked from within a post-phase action, the barrier currently has 0 participants, or the barrier is signaled by more threads than are registered as participants.</exception>
		/// <exception cref="T:System.Threading.BarrierPostPhaseException">If an exception is thrown from the post phase action of a Barrier after all participating threads have called SignalAndWait, the exception will be wrapped in a BarrierPostPhaseException and be thrown on all participating threads.</exception>
		// Token: 0x06000816 RID: 2070 RVA: 0x00027A54 File Offset: 0x00025C54
		public bool SignalAndWait(int millisecondsTimeout)
		{
			return this.SignalAndWait(millisecondsTimeout, default(CancellationToken));
		}

		/// <summary>Signals that a participant has reached the barrier and waits for all other participants to reach the barrier as well, using a 32-bit signed integer to measure the timeout, while observing a cancellation token.</summary>
		/// <returns>if all participants reached the barrier within the specified time; otherwise false</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method was invoked from within a post-phase action, the barrier currently has 0 participants, or the barrier is signaled by more threads than are registered as participants.</exception>
		// Token: 0x06000817 RID: 2071 RVA: 0x00027A74 File Offset: 0x00025C74
		public bool SignalAndWait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			this.ThrowIfDisposed();
			cancellationToken.ThrowIfCancellationRequested();
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", millisecondsTimeout, global::SR.GetString("The specified timeout must represent a value between -1 and Int32.MaxValue, inclusive."));
			}
			if (this.m_actionCallerID != 0 && Thread.CurrentThread.ManagedThreadId == this.m_actionCallerID)
			{
				throw new InvalidOperationException(global::SR.GetString("This method may not be called from within the postPhaseAction."));
			}
			SpinWait spinWait = default(SpinWait);
			bool flag;
			long currentPhaseNumber;
			for (;;)
			{
				int num = this.m_currentTotalCount;
				int num2;
				int num3;
				this.GetCurrentTotal(num, out num2, out num3, out flag);
				currentPhaseNumber = this.CurrentPhaseNumber;
				if (num3 == 0)
				{
					break;
				}
				if (num2 == 0 && flag != (this.CurrentPhaseNumber % 2L == 0L))
				{
					goto Block_6;
				}
				if (num2 + 1 == num3)
				{
					if (this.SetCurrentTotal(num, 0, num3, !flag))
					{
						goto Block_8;
					}
				}
				else if (this.SetCurrentTotal(num, num2 + 1, num3, flag))
				{
					goto IL_00EA;
				}
				spinWait.SpinOnce();
			}
			throw new InvalidOperationException(global::SR.GetString("The barrier has no registered participants."));
			Block_6:
			throw new InvalidOperationException(global::SR.GetString("The number of threads using the barrier exceeded the total number of registered participants."));
			Block_8:
			this.FinishPhase(flag);
			return true;
			IL_00EA:
			ManualResetEventSlim manualResetEventSlim = (flag ? this.m_evenEvent : this.m_oddEvent);
			bool flag2 = false;
			bool flag3 = false;
			try
			{
				flag3 = this.DiscontinuousWait(manualResetEventSlim, millisecondsTimeout, cancellationToken, currentPhaseNumber);
			}
			catch (OperationCanceledException)
			{
				flag2 = true;
			}
			catch (ObjectDisposedException)
			{
				if (currentPhaseNumber >= this.CurrentPhaseNumber)
				{
					throw;
				}
				flag3 = true;
			}
			if (!flag3)
			{
				spinWait.Reset();
				for (;;)
				{
					int num = this.m_currentTotalCount;
					int num2;
					int num3;
					bool flag4;
					this.GetCurrentTotal(num, out num2, out num3, out flag4);
					if (currentPhaseNumber < this.CurrentPhaseNumber || flag != flag4)
					{
						break;
					}
					if (this.SetCurrentTotal(num, num2 - 1, num3, flag))
					{
						goto Block_13;
					}
					spinWait.SpinOnce();
				}
				this.WaitCurrentPhase(manualResetEventSlim, currentPhaseNumber);
				goto IL_0197;
				Block_13:
				if (flag2)
				{
					throw new OperationCanceledException(global::SR.GetString("The operation was canceled."), cancellationToken);
				}
				return false;
			}
			IL_0197:
			if (this.m_exception != null)
			{
				throw new BarrierPostPhaseException(this.m_exception);
			}
			return true;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00027C4C File Offset: 0x00025E4C
		[SecuritySafeCritical]
		private void FinishPhase(bool observedSense)
		{
			if (this.m_postPhaseAction != null)
			{
				try
				{
					this.m_actionCallerID = Thread.CurrentThread.ManagedThreadId;
					if (this.m_ownerThreadContext != null)
					{
						ExecutionContext ownerThreadContext = this.m_ownerThreadContext;
						this.m_ownerThreadContext = this.m_ownerThreadContext.CreateCopy();
						ContextCallback contextCallback = Barrier.s_invokePostPhaseAction;
						if (contextCallback == null)
						{
							contextCallback = (Barrier.s_invokePostPhaseAction = new ContextCallback(Barrier.InvokePostPhaseAction));
						}
						ExecutionContext.Run(ownerThreadContext, contextCallback, this);
						ownerThreadContext.Dispose();
					}
					else
					{
						this.m_postPhaseAction(this);
					}
					this.m_exception = null;
					return;
				}
				catch (Exception ex)
				{
					this.m_exception = ex;
					return;
				}
				finally
				{
					this.m_actionCallerID = 0;
					this.SetResetEvents(observedSense);
					if (this.m_exception != null)
					{
						throw new BarrierPostPhaseException(this.m_exception);
					}
				}
			}
			this.SetResetEvents(observedSense);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00027D24 File Offset: 0x00025F24
		[SecurityCritical]
		private static void InvokePostPhaseAction(object obj)
		{
			Barrier barrier = (Barrier)obj;
			barrier.m_postPhaseAction(barrier);
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00027D44 File Offset: 0x00025F44
		private void SetResetEvents(bool observedSense)
		{
			this.CurrentPhaseNumber += 1L;
			if (observedSense)
			{
				this.m_oddEvent.Reset();
				this.m_evenEvent.Set();
				return;
			}
			this.m_evenEvent.Reset();
			this.m_oddEvent.Set();
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00027D90 File Offset: 0x00025F90
		private void WaitCurrentPhase(ManualResetEventSlim currentPhaseEvent, long observedPhase)
		{
			SpinWait spinWait = default(SpinWait);
			while (!currentPhaseEvent.IsSet && this.CurrentPhaseNumber - observedPhase <= 1L)
			{
				spinWait.SpinOnce();
			}
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00027DC4 File Offset: 0x00025FC4
		private bool DiscontinuousWait(ManualResetEventSlim currentPhaseEvent, int totalTimeout, CancellationToken token, long observedPhase)
		{
			int num = 100;
			int num2 = 10000;
			while (observedPhase == this.CurrentPhaseNumber)
			{
				int num3 = ((totalTimeout == -1) ? num : Math.Min(num, totalTimeout));
				if (currentPhaseEvent.Wait(num3, token))
				{
					return true;
				}
				if (totalTimeout != -1)
				{
					totalTimeout -= num3;
					if (totalTimeout <= 0)
					{
						return false;
					}
				}
				num = ((num >= num2) ? num2 : Math.Min(num << 1, num2));
			}
			this.WaitCurrentPhase(currentPhaseEvent, observedPhase);
			return true;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.Barrier" /> class.</summary>
		/// <exception cref="T:System.InvalidOperationException">The method was invoked from within a post-phase action.</exception>
		// Token: 0x0600081D RID: 2077 RVA: 0x00027E2B File Offset: 0x0002602B
		public void Dispose()
		{
			if (this.m_actionCallerID != 0 && Thread.CurrentThread.ManagedThreadId == this.m_actionCallerID)
			{
				throw new InvalidOperationException(global::SR.GetString("This method may not be called from within the postPhaseAction."));
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Threading.Barrier" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x0600081E RID: 2078 RVA: 0x00027E64 File Offset: 0x00026064
		protected virtual void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (disposing)
				{
					this.m_oddEvent.Dispose();
					this.m_evenEvent.Dispose();
					if (this.m_ownerThreadContext != null)
					{
						this.m_ownerThreadContext.Dispose();
						this.m_ownerThreadContext = null;
					}
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00027EB3 File Offset: 0x000260B3
		private void ThrowIfDisposed()
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("Barrier", global::SR.GetString("The barrier has been disposed."));
			}
		}

		// Token: 0x04000D88 RID: 3464
		private volatile int m_currentTotalCount;

		// Token: 0x04000D89 RID: 3465
		private const int CURRENT_MASK = 2147418112;

		// Token: 0x04000D8A RID: 3466
		private const int TOTAL_MASK = 32767;

		// Token: 0x04000D8B RID: 3467
		private const int SENSE_MASK = -2147483648;

		// Token: 0x04000D8C RID: 3468
		private const int MAX_PARTICIPANTS = 32767;

		// Token: 0x04000D8D RID: 3469
		private long m_currentPhase;

		// Token: 0x04000D8E RID: 3470
		private bool m_disposed;

		// Token: 0x04000D8F RID: 3471
		private ManualResetEventSlim m_oddEvent;

		// Token: 0x04000D90 RID: 3472
		private ManualResetEventSlim m_evenEvent;

		// Token: 0x04000D91 RID: 3473
		private ExecutionContext m_ownerThreadContext;

		// Token: 0x04000D92 RID: 3474
		[SecurityCritical]
		private static ContextCallback s_invokePostPhaseAction;

		// Token: 0x04000D93 RID: 3475
		private Action<Barrier> m_postPhaseAction;

		// Token: 0x04000D94 RID: 3476
		private Exception m_exception;

		// Token: 0x04000D95 RID: 3477
		private int m_actionCallerID;
	}
}
