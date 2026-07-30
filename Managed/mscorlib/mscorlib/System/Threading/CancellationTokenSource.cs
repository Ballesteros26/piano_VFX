using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Signals to a <see cref="T:System.Threading.CancellationToken" /> that it should be canceled.</summary>
	// Token: 0x02000450 RID: 1104
	[ComVisible(false)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class CancellationTokenSource : IDisposable
	{
		// Token: 0x060034DE RID: 13534 RVA: 0x000C37DE File Offset: 0x000C19DE
		private static void LinkedTokenCancelDelegate(object source)
		{
			(source as CancellationTokenSource).Cancel();
		}

		/// <summary>Gets whether cancellation has been requested for this <see cref="T:System.Threading.CancellationTokenSource" />.</summary>
		/// <returns>Whether cancellation has been requested for this <see cref="T:System.Threading.CancellationTokenSource" />.</returns>
		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x060034DF RID: 13535 RVA: 0x000C37EB File Offset: 0x000C19EB
		public bool IsCancellationRequested
		{
			get
			{
				return this.m_state >= 2;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x060034E0 RID: 13536 RVA: 0x000C37FB File Offset: 0x000C19FB
		internal bool IsCancellationCompleted
		{
			get
			{
				return this.m_state == 3;
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x060034E1 RID: 13537 RVA: 0x000C3808 File Offset: 0x000C1A08
		internal bool IsDisposed
		{
			get
			{
				return this.m_disposed;
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x060034E3 RID: 13539 RVA: 0x000C381B File Offset: 0x000C1A1B
		// (set) Token: 0x060034E2 RID: 13538 RVA: 0x000C3810 File Offset: 0x000C1A10
		internal int ThreadIDExecutingCallbacks
		{
			get
			{
				return this.m_threadIDExecutingCallbacks;
			}
			set
			{
				this.m_threadIDExecutingCallbacks = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Threading.CancellationToken" /> associated with this <see cref="T:System.Threading.CancellationTokenSource" />.</summary>
		/// <returns>The <see cref="T:System.Threading.CancellationToken" /> associated with this <see cref="T:System.Threading.CancellationTokenSource" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The token source has been disposed.</exception>
		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x060034E4 RID: 13540 RVA: 0x000C3825 File Offset: 0x000C1A25
		public CancellationToken Token
		{
			get
			{
				this.ThrowIfDisposed();
				return new CancellationToken(this);
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x060034E5 RID: 13541 RVA: 0x000C3833 File Offset: 0x000C1A33
		internal bool CanBeCanceled
		{
			get
			{
				return this.m_state != 0;
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x060034E6 RID: 13542 RVA: 0x000C3840 File Offset: 0x000C1A40
		internal WaitHandle WaitHandle
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.m_kernelEvent != null)
				{
					return this.m_kernelEvent;
				}
				ManualResetEvent manualResetEvent = new ManualResetEvent(false);
				if (Interlocked.CompareExchange<ManualResetEvent>(ref this.m_kernelEvent, manualResetEvent, null) != null)
				{
					((IDisposable)manualResetEvent).Dispose();
				}
				if (this.IsCancellationRequested)
				{
					this.m_kernelEvent.Set();
				}
				return this.m_kernelEvent;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x060034E7 RID: 13543 RVA: 0x000C38A0 File Offset: 0x000C1AA0
		internal CancellationCallbackInfo ExecutingCallback
		{
			get
			{
				return this.m_executingCallback;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.CancellationTokenSource" /> class.</summary>
		// Token: 0x060034E8 RID: 13544 RVA: 0x000C38AA File Offset: 0x000C1AAA
		public CancellationTokenSource()
		{
			this.m_state = 1;
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x000C38C4 File Offset: 0x000C1AC4
		private CancellationTokenSource(bool set)
		{
			this.m_state = (set ? 3 : 0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.CancellationTokenSource" /> class that will be canceled after the specified time span.</summary>
		/// <param name="delay">The time span to wait before canceling this <see cref="T:System.Threading.CancellationTokenSource" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="delay" />.<see cref="P:System.TimeSpan.TotalMilliseconds" /> is less than -1 or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x060034EA RID: 13546 RVA: 0x000C38E4 File Offset: 0x000C1AE4
		public CancellationTokenSource(TimeSpan delay)
		{
			long num = (long)delay.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("delay");
			}
			this.InitializeWithTimer((int)num);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.CancellationTokenSource" /> class that will be canceled after the specified delay in milliseconds.</summary>
		/// <param name="millisecondsDelay">The time span to wait before canceling this <see cref="T:System.Threading.CancellationTokenSource" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when <paramref name="millisecondsDelay" /> is less than -1.</exception>
		// Token: 0x060034EB RID: 13547 RVA: 0x000C392A File Offset: 0x000C1B2A
		public CancellationTokenSource(int millisecondsDelay)
		{
			if (millisecondsDelay < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsDelay");
			}
			this.InitializeWithTimer(millisecondsDelay);
		}

		// Token: 0x060034EC RID: 13548 RVA: 0x000C3951 File Offset: 0x000C1B51
		private void InitializeWithTimer(int millisecondsDelay)
		{
			this.m_state = 1;
			this.m_timer = new Timer(CancellationTokenSource.s_timerCallback, this, millisecondsDelay, -1);
		}

		/// <summary>Communicates a request for cancellation.</summary>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.AggregateException">An aggregate exception containing all the exceptions thrown by the registered callbacks on the associated <see cref="T:System.Threading.CancellationToken" />.</exception>
		// Token: 0x060034ED RID: 13549 RVA: 0x000C3971 File Offset: 0x000C1B71
		public void Cancel()
		{
			this.Cancel(false);
		}

		/// <summary>Communicates a request for cancellation, and specifies whether remaining callbacks and cancelable operations should be processed.</summary>
		/// <param name="throwOnFirstException">true if exceptions should immediately propagate; otherwise, false.</param>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.AggregateException">An aggregate exception containing all the exceptions thrown by the registered callbacks on the associated <see cref="T:System.Threading.CancellationToken" />.</exception>
		// Token: 0x060034EE RID: 13550 RVA: 0x000C397A File Offset: 0x000C1B7A
		public void Cancel(bool throwOnFirstException)
		{
			this.ThrowIfDisposed();
			this.NotifyCancellation(throwOnFirstException);
		}

		/// <summary>Schedules a cancel operation on this <see cref="T:System.Threading.CancellationTokenSource" /> after the specified time span.</summary>
		/// <param name="delay">The time span to wait before canceling this <see cref="T:System.Threading.CancellationTokenSource" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The exception thrown when this <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when <paramref name="delay" /> is less than -1 or greater than Int32.MaxValue.</exception>
		// Token: 0x060034EF RID: 13551 RVA: 0x000C398C File Offset: 0x000C1B8C
		public void CancelAfter(TimeSpan delay)
		{
			long num = (long)delay.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("delay");
			}
			this.CancelAfter((int)num);
		}

		/// <summary>Schedules a cancel operation on this <see cref="T:System.Threading.CancellationTokenSource" /> after the specified number of milliseconds.</summary>
		/// <param name="millisecondsDelay">The time span to wait before canceling this <see cref="T:System.Threading.CancellationTokenSource" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The exception thrown when this <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception thrown when <paramref name="millisecondsDelay" /> is less than -1.</exception>
		// Token: 0x060034F0 RID: 13552 RVA: 0x000C39C4 File Offset: 0x000C1BC4
		public void CancelAfter(int millisecondsDelay)
		{
			this.ThrowIfDisposed();
			if (millisecondsDelay < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsDelay");
			}
			if (this.IsCancellationRequested)
			{
				return;
			}
			if (this.m_timer == null)
			{
				Timer timer = new Timer(CancellationTokenSource.s_timerCallback, this, -1, -1);
				if (Interlocked.CompareExchange<Timer>(ref this.m_timer, timer, null) != null)
				{
					timer.Dispose();
				}
			}
			try
			{
				this.m_timer.Change(millisecondsDelay, -1);
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x000C3A44 File Offset: 0x000C1C44
		private static void TimerCallbackLogic(object obj)
		{
			CancellationTokenSource cancellationTokenSource = (CancellationTokenSource)obj;
			if (!cancellationTokenSource.IsDisposed)
			{
				try
				{
					cancellationTokenSource.Cancel();
				}
				catch (ObjectDisposedException)
				{
					if (!cancellationTokenSource.IsDisposed)
					{
						throw;
					}
				}
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.CancellationTokenSource" /> class.</summary>
		/// <exception cref="T:System.ObjectDisposedException">A linked <see cref="T:System.Threading.CancellationTokenSource" /> has already been disposed.</exception>
		// Token: 0x060034F2 RID: 13554 RVA: 0x000C3A88 File Offset: 0x000C1C88
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Threading.CancellationTokenSource" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x060034F3 RID: 13555 RVA: 0x000C3A98 File Offset: 0x000C1C98
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_disposed)
				{
					return;
				}
				if (this.m_timer != null)
				{
					this.m_timer.Dispose();
				}
				CancellationTokenRegistration[] linkingRegistrations = this.m_linkingRegistrations;
				if (linkingRegistrations != null)
				{
					this.m_linkingRegistrations = null;
					for (int i = 0; i < linkingRegistrations.Length; i++)
					{
						linkingRegistrations[i].Dispose();
					}
				}
				this.m_registeredCallbacksLists = null;
				ManualResetEvent kernelEvent = this.m_kernelEvent;
				if (kernelEvent != null)
				{
					this.m_kernelEvent = null;
					kernelEvent.Close();
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x060034F4 RID: 13556 RVA: 0x000C3B1E File Offset: 0x000C1D1E
		internal void ThrowIfDisposed()
		{
			if (this.m_disposed)
			{
				CancellationTokenSource.ThrowObjectDisposedException();
			}
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x000C3B2D File Offset: 0x000C1D2D
		private static void ThrowObjectDisposedException()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("The CancellationTokenSource has been disposed."));
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x000C3B3F File Offset: 0x000C1D3F
		internal static CancellationTokenSource InternalGetStaticSource(bool set)
		{
			if (!set)
			{
				return CancellationTokenSource._staticSource_NotCancelable;
			}
			return CancellationTokenSource._staticSource_Set;
		}

		// Token: 0x060034F7 RID: 13559 RVA: 0x000C3B50 File Offset: 0x000C1D50
		internal CancellationTokenRegistration InternalRegister(Action<object> callback, object stateForCallback, SynchronizationContext targetSyncContext, ExecutionContext executionContext)
		{
			if (AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
			{
				this.ThrowIfDisposed();
			}
			if (!this.IsCancellationRequested)
			{
				if (this.m_disposed && !AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
				{
					return default(CancellationTokenRegistration);
				}
				int num = Thread.CurrentThread.ManagedThreadId % CancellationTokenSource.s_nLists;
				CancellationCallbackInfo cancellationCallbackInfo = new CancellationCallbackInfo(callback, stateForCallback, targetSyncContext, executionContext, this);
				SparselyPopulatedArray<CancellationCallbackInfo>[] array = this.m_registeredCallbacksLists;
				if (array == null)
				{
					SparselyPopulatedArray<CancellationCallbackInfo>[] array2 = new SparselyPopulatedArray<CancellationCallbackInfo>[CancellationTokenSource.s_nLists];
					array = Interlocked.CompareExchange<SparselyPopulatedArray<CancellationCallbackInfo>[]>(ref this.m_registeredCallbacksLists, array2, null);
					if (array == null)
					{
						array = array2;
					}
				}
				SparselyPopulatedArray<CancellationCallbackInfo> sparselyPopulatedArray = Volatile.Read<SparselyPopulatedArray<CancellationCallbackInfo>>(ref array[num]);
				if (sparselyPopulatedArray == null)
				{
					SparselyPopulatedArray<CancellationCallbackInfo> sparselyPopulatedArray2 = new SparselyPopulatedArray<CancellationCallbackInfo>(4);
					Interlocked.CompareExchange<SparselyPopulatedArray<CancellationCallbackInfo>>(ref array[num], sparselyPopulatedArray2, null);
					sparselyPopulatedArray = array[num];
				}
				SparselyPopulatedArrayAddInfo<CancellationCallbackInfo> sparselyPopulatedArrayAddInfo = sparselyPopulatedArray.Add(cancellationCallbackInfo);
				CancellationTokenRegistration cancellationTokenRegistration = new CancellationTokenRegistration(cancellationCallbackInfo, sparselyPopulatedArrayAddInfo);
				if (!this.IsCancellationRequested)
				{
					return cancellationTokenRegistration;
				}
				if (!cancellationTokenRegistration.TryDeregister())
				{
					return cancellationTokenRegistration;
				}
			}
			callback(stateForCallback);
			return default(CancellationTokenRegistration);
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x000C3C40 File Offset: 0x000C1E40
		private void NotifyCancellation(bool throwOnFirstException)
		{
			if (this.IsCancellationRequested)
			{
				return;
			}
			if (Interlocked.CompareExchange(ref this.m_state, 2, 1) == 1)
			{
				Timer timer = this.m_timer;
				if (timer != null)
				{
					timer.Dispose();
				}
				this.ThreadIDExecutingCallbacks = Thread.CurrentThread.ManagedThreadId;
				ManualResetEvent kernelEvent = this.m_kernelEvent;
				if (kernelEvent != null)
				{
					try
					{
						kernelEvent.Set();
					}
					catch (ObjectDisposedException)
					{
						if (this.m_kernelEvent != null)
						{
							throw;
						}
					}
				}
				this.ExecuteCallbackHandlers(throwOnFirstException);
			}
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x000C3CC4 File Offset: 0x000C1EC4
		private void ExecuteCallbackHandlers(bool throwOnFirstException)
		{
			List<Exception> list = null;
			SparselyPopulatedArray<CancellationCallbackInfo>[] registeredCallbacksLists = this.m_registeredCallbacksLists;
			if (registeredCallbacksLists == null)
			{
				Interlocked.Exchange(ref this.m_state, 3);
				return;
			}
			try
			{
				for (int i = 0; i < registeredCallbacksLists.Length; i++)
				{
					SparselyPopulatedArray<CancellationCallbackInfo> sparselyPopulatedArray = Volatile.Read<SparselyPopulatedArray<CancellationCallbackInfo>>(ref registeredCallbacksLists[i]);
					if (sparselyPopulatedArray != null)
					{
						for (SparselyPopulatedArrayFragment<CancellationCallbackInfo> sparselyPopulatedArrayFragment = sparselyPopulatedArray.Tail; sparselyPopulatedArrayFragment != null; sparselyPopulatedArrayFragment = sparselyPopulatedArrayFragment.Prev)
						{
							for (int j = sparselyPopulatedArrayFragment.Length - 1; j >= 0; j--)
							{
								this.m_executingCallback = sparselyPopulatedArrayFragment[j];
								if (this.m_executingCallback != null)
								{
									CancellationCallbackCoreWorkArguments cancellationCallbackCoreWorkArguments = new CancellationCallbackCoreWorkArguments(sparselyPopulatedArrayFragment, j);
									try
									{
										if (this.m_executingCallback.TargetSyncContext != null)
										{
											this.m_executingCallback.TargetSyncContext.Send(new SendOrPostCallback(this.CancellationCallbackCoreWork_OnSyncContext), cancellationCallbackCoreWorkArguments);
											this.ThreadIDExecutingCallbacks = Thread.CurrentThread.ManagedThreadId;
										}
										else
										{
											this.CancellationCallbackCoreWork(cancellationCallbackCoreWorkArguments);
										}
									}
									catch (Exception ex)
									{
										if (throwOnFirstException)
										{
											throw;
										}
										if (list == null)
										{
											list = new List<Exception>();
										}
										list.Add(ex);
									}
								}
							}
						}
					}
				}
			}
			finally
			{
				this.m_state = 3;
				this.m_executingCallback = null;
				Thread.MemoryBarrier();
			}
			if (list != null)
			{
				throw new AggregateException(list);
			}
		}

		// Token: 0x060034FA RID: 13562 RVA: 0x000C3E20 File Offset: 0x000C2020
		private void CancellationCallbackCoreWork_OnSyncContext(object obj)
		{
			this.CancellationCallbackCoreWork((CancellationCallbackCoreWorkArguments)obj);
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x000C3E30 File Offset: 0x000C2030
		private void CancellationCallbackCoreWork(CancellationCallbackCoreWorkArguments args)
		{
			CancellationCallbackInfo cancellationCallbackInfo = args.m_currArrayFragment.SafeAtomicRemove(args.m_currArrayIndex, this.m_executingCallback);
			if (cancellationCallbackInfo == this.m_executingCallback)
			{
				if (cancellationCallbackInfo.TargetExecutionContext != null)
				{
					cancellationCallbackInfo.CancellationTokenSource.ThreadIDExecutingCallbacks = Thread.CurrentThread.ManagedThreadId;
				}
				cancellationCallbackInfo.ExecuteCallback();
			}
		}

		/// <summary>Creates a <see cref="T:System.Threading.CancellationTokenSource" /> that will be in the canceled state when any of the source tokens are in the canceled state.</summary>
		/// <returns>A <see cref="T:System.Threading.CancellationTokenSource" /> that is linked to the source tokens.</returns>
		/// <param name="token1">The first cancellation token to observe.</param>
		/// <param name="token2">The second cancellation token to observe.</param>
		/// <exception cref="T:System.ObjectDisposedException">A <see cref="T:System.Threading.CancellationTokenSource" /> associated with one of the source tokens has been disposed.</exception>
		// Token: 0x060034FC RID: 13564 RVA: 0x000C3E88 File Offset: 0x000C2088
		public static CancellationTokenSource CreateLinkedTokenSource(CancellationToken token1, CancellationToken token2)
		{
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			bool canBeCanceled = token2.CanBeCanceled;
			if (token1.CanBeCanceled)
			{
				cancellationTokenSource.m_linkingRegistrations = new CancellationTokenRegistration[canBeCanceled ? 2 : 1];
				cancellationTokenSource.m_linkingRegistrations[0] = token1.InternalRegisterWithoutEC(CancellationTokenSource.s_LinkedTokenCancelDelegate, cancellationTokenSource);
			}
			if (canBeCanceled)
			{
				int num = 1;
				if (cancellationTokenSource.m_linkingRegistrations == null)
				{
					cancellationTokenSource.m_linkingRegistrations = new CancellationTokenRegistration[1];
					num = 0;
				}
				cancellationTokenSource.m_linkingRegistrations[num] = token2.InternalRegisterWithoutEC(CancellationTokenSource.s_LinkedTokenCancelDelegate, cancellationTokenSource);
			}
			return cancellationTokenSource;
		}

		/// <summary>Creates a <see cref="T:System.Threading.CancellationTokenSource" /> that will be in the canceled state when any of the source tokens in the specified array are in the canceled state.</summary>
		/// <returns>A <see cref="T:System.Threading.CancellationTokenSource" /> that is linked to the source tokens.</returns>
		/// <param name="tokens">An array that contains the cancellation token instances to observe.</param>
		/// <exception cref="T:System.ObjectDisposedException">A <see cref="T:System.Threading.CancellationTokenSource" /> associated with one of the source tokens has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="tokens" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="tokens" /> is empty.</exception>
		// Token: 0x060034FD RID: 13565 RVA: 0x000C3F0C File Offset: 0x000C210C
		public static CancellationTokenSource CreateLinkedTokenSource(params CancellationToken[] tokens)
		{
			if (tokens == null)
			{
				throw new ArgumentNullException("tokens");
			}
			if (tokens.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("No tokens were supplied."));
			}
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			cancellationTokenSource.m_linkingRegistrations = new CancellationTokenRegistration[tokens.Length];
			for (int i = 0; i < tokens.Length; i++)
			{
				if (tokens[i].CanBeCanceled)
				{
					cancellationTokenSource.m_linkingRegistrations[i] = tokens[i].InternalRegisterWithoutEC(CancellationTokenSource.s_LinkedTokenCancelDelegate, cancellationTokenSource);
				}
			}
			return cancellationTokenSource;
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x000C3F8C File Offset: 0x000C218C
		internal void WaitForCallbackToComplete(CancellationCallbackInfo callbackInfo)
		{
			SpinWait spinWait = default(SpinWait);
			while (this.ExecutingCallback == callbackInfo)
			{
				spinWait.SpinOnce();
			}
		}

		// Token: 0x04001C2C RID: 7212
		private static readonly CancellationTokenSource _staticSource_Set = new CancellationTokenSource(true);

		// Token: 0x04001C2D RID: 7213
		private static readonly CancellationTokenSource _staticSource_NotCancelable = new CancellationTokenSource(false);

		// Token: 0x04001C2E RID: 7214
		private static readonly int s_nLists = ((PlatformHelper.ProcessorCount > 24) ? 24 : PlatformHelper.ProcessorCount);

		// Token: 0x04001C2F RID: 7215
		private volatile ManualResetEvent m_kernelEvent;

		// Token: 0x04001C30 RID: 7216
		private volatile SparselyPopulatedArray<CancellationCallbackInfo>[] m_registeredCallbacksLists;

		// Token: 0x04001C31 RID: 7217
		private const int CANNOT_BE_CANCELED = 0;

		// Token: 0x04001C32 RID: 7218
		private const int NOT_CANCELED = 1;

		// Token: 0x04001C33 RID: 7219
		private const int NOTIFYING = 2;

		// Token: 0x04001C34 RID: 7220
		private const int NOTIFYINGCOMPLETE = 3;

		// Token: 0x04001C35 RID: 7221
		private volatile int m_state;

		// Token: 0x04001C36 RID: 7222
		private volatile int m_threadIDExecutingCallbacks = -1;

		// Token: 0x04001C37 RID: 7223
		private bool m_disposed;

		// Token: 0x04001C38 RID: 7224
		private CancellationTokenRegistration[] m_linkingRegistrations;

		// Token: 0x04001C39 RID: 7225
		private static readonly Action<object> s_LinkedTokenCancelDelegate = new Action<object>(CancellationTokenSource.LinkedTokenCancelDelegate);

		// Token: 0x04001C3A RID: 7226
		private volatile CancellationCallbackInfo m_executingCallback;

		// Token: 0x04001C3B RID: 7227
		private volatile Timer m_timer;

		// Token: 0x04001C3C RID: 7228
		private static readonly TimerCallback s_timerCallback = new TimerCallback(CancellationTokenSource.TimerCallbackLogic);
	}
}
