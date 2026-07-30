using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Threading
{
	/// <summary>A lightweight alternative to <see cref="T:System.Threading.Semaphore" /> that limits the number of threads that can access a resource or pool of resources concurrently.</summary>
	// Token: 0x0200045B RID: 1115
	[DebuggerDisplay("Current Count = {m_currentCount}")]
	[ComVisible(false)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class SemaphoreSlim : IDisposable
	{
		/// <summary>Gets the number of threads that will be allowed to enter the <see cref="T:System.Threading.SemaphoreSlim" />.</summary>
		/// <returns>The current count of the <see cref="T:System.Threading.SemaphoreSlim" />.</returns>
		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x0600354C RID: 13644 RVA: 0x000C4EF8 File Offset: 0x000C30F8
		public int CurrentCount
		{
			get
			{
				return this.m_currentCount;
			}
		}

		/// <summary>Returns a <see cref="T:System.Threading.WaitHandle" /> that can be used to wait on the semaphore.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that can be used to wait on the semaphore.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.SemaphoreSlim" /> has been disposed.</exception>
		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x0600354D RID: 13645 RVA: 0x000C4F04 File Offset: 0x000C3104
		public WaitHandle AvailableWaitHandle
		{
			get
			{
				this.CheckDispose();
				if (this.m_waitHandle != null)
				{
					return this.m_waitHandle;
				}
				object lockObj = this.m_lockObj;
				lock (lockObj)
				{
					if (this.m_waitHandle == null)
					{
						this.m_waitHandle = new ManualResetEvent(this.m_currentCount != 0);
					}
				}
				return this.m_waitHandle;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SemaphoreSlim" /> class, specifying the initial number of requests that can be granted concurrently.</summary>
		/// <param name="initialCount">The initial number of requests for the semaphore that can be granted concurrently.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="initialCount" /> is less than 0.</exception>
		// Token: 0x0600354E RID: 13646 RVA: 0x000C4F84 File Offset: 0x000C3184
		public SemaphoreSlim(int initialCount)
			: this(initialCount, int.MaxValue)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SemaphoreSlim" /> class, specifying the initial and maximum number of requests that can be granted concurrently.</summary>
		/// <param name="initialCount">The initial number of requests for the semaphore that can be granted concurrently.</param>
		/// <param name="maxCount">The maximum number of requests for the semaphore that can be granted concurrently.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="initialCount" /> is less than 0, or <paramref name="initialCount" /> is greater than <paramref name="maxCount" />, or <paramref name="maxCount" /> is equal to or less than 0.</exception>
		// Token: 0x0600354F RID: 13647 RVA: 0x000C4F94 File Offset: 0x000C3194
		public SemaphoreSlim(int initialCount, int maxCount)
		{
			if (initialCount < 0 || initialCount > maxCount)
			{
				throw new ArgumentOutOfRangeException("initialCount", initialCount, SemaphoreSlim.GetResourceString("The initialCount argument must be non-negative and less than or equal to the maximumCount."));
			}
			if (maxCount <= 0)
			{
				throw new ArgumentOutOfRangeException("maxCount", maxCount, SemaphoreSlim.GetResourceString("The maximumCount argument must be a positive number. If a maximum is not required, use the constructor without a maxCount parameter."));
			}
			this.m_maxCount = maxCount;
			this.m_lockObj = new object();
			this.m_currentCount = initialCount;
		}

		/// <summary>Blocks the current thread until it can enter the <see cref="T:System.Threading.SemaphoreSlim" />.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		// Token: 0x06003550 RID: 13648 RVA: 0x000C5004 File Offset: 0x000C3204
		public void Wait()
		{
			this.Wait(-1, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until it can enter the <see cref="T:System.Threading.SemaphoreSlim" />, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> token to observe.</param>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.-or-The <see cref="T:System.Threading.CancellationTokenSource" /> that created<paramref name=" cancellationToken" /> has already been disposed.</exception>
		// Token: 0x06003551 RID: 13649 RVA: 0x000C5022 File Offset: 0x000C3222
		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		/// <summary>Blocks the current thread until it can enter the <see cref="T:System.Threading.SemaphoreSlim" />, using a <see cref="T:System.TimeSpan" /> to specify the timeout.</summary>
		/// <returns>true if the current thread successfully entered the <see cref="T:System.Threading.SemaphoreSlim" />; otherwise, false.</returns>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The semaphoreSlim instance has been disposed<paramref name="." /></exception>
		// Token: 0x06003552 RID: 13650 RVA: 0x000C5030 File Offset: 0x000C3230
		public bool Wait(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, SemaphoreSlim.GetResourceString("The timeout must represent a value between -1 and Int32.MaxValue, inclusive."));
			}
			return this.Wait((int)timeout.TotalMilliseconds, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until it can enter the <see cref="T:System.Threading.SemaphoreSlim" />, using a <see cref="T:System.TimeSpan" /> that specifies the timeout, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <returns>true if the current thread successfully entered the <see cref="T:System.Threading.SemaphoreSlim" />; otherwise, false.</returns>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The semaphoreSlim instance has been disposed<paramref name="." /><paramref name="-or-" />The <see cref="T:System.Threading.CancellationTokenSource" /> that created<paramref name=" cancellationToken" /> has already been disposed.</exception>
		// Token: 0x06003553 RID: 13651 RVA: 0x000C5088 File Offset: 0x000C3288
		public bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, SemaphoreSlim.GetResourceString("The timeout must represent a value between -1 and Int32.MaxValue, inclusive."));
			}
			return this.Wait((int)timeout.TotalMilliseconds, cancellationToken);
		}

		/// <summary>Blocks the current thread until it can enter the <see cref="T:System.Threading.SemaphoreSlim" />, using a 32-bit signed integer that specifies the timeout.</summary>
		/// <returns>true if the current thread successfully entered the <see cref="T:System.Threading.SemaphoreSlim" />; otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		// Token: 0x06003554 RID: 13652 RVA: 0x000C50D8 File Offset: 0x000C32D8
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until it can enter the <see cref="T:System.Threading.SemaphoreSlim" />, using a 32-bit signed integer that specifies the timeout, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <returns>true if the current thread successfully entered the <see cref="T:System.Threading.SemaphoreSlim" />; otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.SemaphoreSlim" /> instance has been disposed, or the <see cref="T:System.Threading.CancellationTokenSource" />that created<paramref name=" cancellationToken" /> has been disposed.</exception>
		// Token: 0x06003555 RID: 13653 RVA: 0x000C50F8 File Offset: 0x000C32F8
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			this.CheckDispose();
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("totalMilliSeconds", millisecondsTimeout, SemaphoreSlim.GetResourceString("The timeout must represent a value between -1 and Int32.MaxValue, inclusive."));
			}
			cancellationToken.ThrowIfCancellationRequested();
			uint num = 0U;
			if (millisecondsTimeout != -1 && millisecondsTimeout > 0)
			{
				num = TimeoutHelper.GetTime();
			}
			bool flag = false;
			Task<bool> task = null;
			bool flag2 = false;
			CancellationTokenRegistration cancellationTokenRegistration = cancellationToken.InternalRegisterWithoutEC(SemaphoreSlim.s_cancellationTokenCanceledEventHandler, this);
			try
			{
				SpinWait spinWait = default(SpinWait);
				while (this.m_currentCount == 0 && !spinWait.NextSpinWillYield)
				{
					spinWait.SpinOnce();
				}
				try
				{
				}
				finally
				{
					Monitor.Enter(this.m_lockObj, ref flag2);
					if (flag2)
					{
						this.m_waitCount++;
					}
				}
				if (this.m_asyncHead != null)
				{
					task = this.WaitAsync(millisecondsTimeout, cancellationToken);
				}
				else
				{
					OperationCanceledException ex = null;
					if (this.m_currentCount == 0)
					{
						if (millisecondsTimeout == 0)
						{
							return false;
						}
						try
						{
							flag = this.WaitUntilCountOrTimeout(millisecondsTimeout, num, cancellationToken);
						}
						catch (OperationCanceledException ex)
						{
						}
					}
					if (this.m_currentCount > 0)
					{
						flag = true;
						this.m_currentCount--;
					}
					else if (ex != null)
					{
						throw ex;
					}
					if (this.m_waitHandle != null && this.m_currentCount == 0)
					{
						this.m_waitHandle.Reset();
					}
				}
			}
			finally
			{
				if (flag2)
				{
					this.m_waitCount--;
					Monitor.Exit(this.m_lockObj);
				}
				cancellationTokenRegistration.Dispose();
			}
			if (task == null)
			{
				return flag;
			}
			return task.GetAwaiter().GetResult();
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x000C5294 File Offset: 0x000C3494
		private bool WaitUntilCountOrTimeout(int millisecondsTimeout, uint startTime, CancellationToken cancellationToken)
		{
			int num = -1;
			while (this.m_currentCount == 0)
			{
				cancellationToken.ThrowIfCancellationRequested();
				if (millisecondsTimeout != -1)
				{
					num = TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout);
					if (num <= 0)
					{
						return false;
					}
				}
				if (!Monitor.Wait(this.m_lockObj, num))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Asynchronously waits to enter the <see cref="T:System.Threading.SemaphoreSlim" />.</summary>
		/// <returns>A task that will complete when the semaphore has been entered.</returns>
		// Token: 0x06003557 RID: 13655 RVA: 0x000C52DC File Offset: 0x000C34DC
		public Task WaitAsync()
		{
			return this.WaitAsync(-1, default(CancellationToken));
		}

		/// <summary>Asynchronously waits to enter the <see cref="T:System.Threading.SemaphoreSlim" />, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <returns>A task that will complete when the semaphore has been entered.</returns>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> token to observe.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		// Token: 0x06003558 RID: 13656 RVA: 0x000C52F9 File Offset: 0x000C34F9
		public Task WaitAsync(CancellationToken cancellationToken)
		{
			return this.WaitAsync(-1, cancellationToken);
		}

		/// <summary>Asynchronously waits to enter the <see cref="T:System.Threading.SemaphoreSlim" />, using a 32-bit signed integer to measure the time interval.</summary>
		/// <returns>A task that will complete with a result of true if the current thread successfully entered the <see cref="T:System.Threading.SemaphoreSlim" />, otherwise with a result of false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		// Token: 0x06003559 RID: 13657 RVA: 0x000C5304 File Offset: 0x000C3504
		public Task<bool> WaitAsync(int millisecondsTimeout)
		{
			return this.WaitAsync(millisecondsTimeout, default(CancellationToken));
		}

		/// <summary>Asynchronously waits to enter the <see cref="T:System.Threading.SemaphoreSlim" />, using a <see cref="T:System.TimeSpan" /> to measure the time interval.</summary>
		/// <returns>A task that will complete with a result of true if the current thread successfully entered the <see cref="T:System.Threading.SemaphoreSlim" />, otherwise with a result of false.</returns>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x0600355A RID: 13658 RVA: 0x000C5324 File Offset: 0x000C3524
		public Task<bool> WaitAsync(TimeSpan timeout)
		{
			return this.WaitAsync(timeout, default(CancellationToken));
		}

		/// <summary>Asynchronously waits to enter the <see cref="T:System.Threading.SemaphoreSlim" />, using a <see cref="T:System.TimeSpan" /> to measure the time interval, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <returns>A task that will complete with a result of true if the current thread successfully entered the <see cref="T:System.Threading.SemaphoreSlim" />, otherwise with a result of false.</returns>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> token to observe.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out-or-timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x0600355B RID: 13659 RVA: 0x000C5344 File Offset: 0x000C3544
		public Task<bool> WaitAsync(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, SemaphoreSlim.GetResourceString("The timeout must represent a value between -1 and Int32.MaxValue, inclusive."));
			}
			return this.WaitAsync((int)timeout.TotalMilliseconds, cancellationToken);
		}

		/// <summary>Asynchronously waits to enter the <see cref="T:System.Threading.SemaphoreSlim" />, using a 32-bit signed integer to measure the time interval, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <returns>A task that will complete with a result of true if the current thread successfully entered the <see cref="T:System.Threading.SemaphoreSlim" />, otherwise with a result of false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		// Token: 0x0600355C RID: 13660 RVA: 0x000C5394 File Offset: 0x000C3594
		public Task<bool> WaitAsync(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			this.CheckDispose();
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("totalMilliSeconds", millisecondsTimeout, SemaphoreSlim.GetResourceString("The timeout must represent a value between -1 and Int32.MaxValue, inclusive."));
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation<bool>(cancellationToken);
			}
			object lockObj = this.m_lockObj;
			Task<bool> task;
			lock (lockObj)
			{
				if (this.m_currentCount > 0)
				{
					this.m_currentCount--;
					if (this.m_waitHandle != null && this.m_currentCount == 0)
					{
						this.m_waitHandle.Reset();
					}
					task = SemaphoreSlim.s_trueTask;
				}
				else
				{
					SemaphoreSlim.TaskNode taskNode = this.CreateAndAddAsyncWaiter();
					task = ((millisecondsTimeout == -1 && !cancellationToken.CanBeCanceled) ? taskNode : this.WaitUntilCountOrTimeoutAsync(taskNode, millisecondsTimeout, cancellationToken));
				}
			}
			return task;
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x000C546C File Offset: 0x000C366C
		private SemaphoreSlim.TaskNode CreateAndAddAsyncWaiter()
		{
			SemaphoreSlim.TaskNode taskNode = new SemaphoreSlim.TaskNode();
			if (this.m_asyncHead == null)
			{
				this.m_asyncHead = taskNode;
				this.m_asyncTail = taskNode;
			}
			else
			{
				this.m_asyncTail.Next = taskNode;
				taskNode.Prev = this.m_asyncTail;
				this.m_asyncTail = taskNode;
			}
			return taskNode;
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x000C54B8 File Offset: 0x000C36B8
		private bool RemoveAsyncWaiter(SemaphoreSlim.TaskNode task)
		{
			bool flag = this.m_asyncHead == task || task.Prev != null;
			if (task.Next != null)
			{
				task.Next.Prev = task.Prev;
			}
			if (task.Prev != null)
			{
				task.Prev.Next = task.Next;
			}
			if (this.m_asyncHead == task)
			{
				this.m_asyncHead = task.Next;
			}
			if (this.m_asyncTail == task)
			{
				this.m_asyncTail = task.Prev;
			}
			task.Next = (task.Prev = null);
			return flag;
		}

		// Token: 0x0600355F RID: 13663 RVA: 0x000C5548 File Offset: 0x000C3748
		private async Task<bool> WaitUntilCountOrTimeoutAsync(SemaphoreSlim.TaskNode asyncWaiter, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			using (CancellationTokenSource cts = (cancellationToken.CanBeCanceled ? CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, default(CancellationToken)) : new CancellationTokenSource()))
			{
				Task<Task> task = Task.WhenAny(new Task[]
				{
					asyncWaiter,
					Task.Delay(millisecondsTimeout, cts.Token)
				});
				object obj = asyncWaiter;
				Task task2 = await task.ConfigureAwait(false);
				if (obj == task2)
				{
					obj = null;
					cts.Cancel();
					return true;
				}
			}
			CancellationTokenSource cts = null;
			object lockObj = this.m_lockObj;
			lock (lockObj)
			{
				if (this.RemoveAsyncWaiter(asyncWaiter))
				{
					cancellationToken.ThrowIfCancellationRequested();
					return false;
				}
			}
			return await asyncWaiter.ConfigureAwait(false);
		}

		/// <summary>Exits the <see cref="T:System.Threading.SemaphoreSlim" /> once.</summary>
		/// <returns>The previous count of the <see cref="T:System.Threading.SemaphoreSlim" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.Threading.SemaphoreFullException">The <see cref="T:System.Threading.SemaphoreSlim" /> has already reached its maximum size.</exception>
		// Token: 0x06003560 RID: 13664 RVA: 0x000C55A5 File Offset: 0x000C37A5
		public int Release()
		{
			return this.Release(1);
		}

		/// <summary>Exits the <see cref="T:System.Threading.SemaphoreSlim" /> a specified number of times.</summary>
		/// <returns>The previous count of the <see cref="T:System.Threading.SemaphoreSlim" />.</returns>
		/// <param name="releaseCount">The number of times to exit the semaphore.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="releaseCount" /> is less than 1.</exception>
		/// <exception cref="T:System.Threading.SemaphoreFullException">The <see cref="T:System.Threading.SemaphoreSlim" /> has already reached its maximum size.</exception>
		// Token: 0x06003561 RID: 13665 RVA: 0x000C55B0 File Offset: 0x000C37B0
		public int Release(int releaseCount)
		{
			this.CheckDispose();
			if (releaseCount < 1)
			{
				throw new ArgumentOutOfRangeException("releaseCount", releaseCount, SemaphoreSlim.GetResourceString("The releaseCount argument must be greater than zero."));
			}
			object lockObj = this.m_lockObj;
			int num2;
			lock (lockObj)
			{
				int num = this.m_currentCount;
				num2 = num;
				if (this.m_maxCount - num < releaseCount)
				{
					throw new SemaphoreFullException();
				}
				num += releaseCount;
				int waitCount = this.m_waitCount;
				if (num == 1 || waitCount == 1)
				{
					Monitor.Pulse(this.m_lockObj);
				}
				else if (waitCount > 1)
				{
					Monitor.PulseAll(this.m_lockObj);
				}
				if (this.m_asyncHead != null)
				{
					int num3 = num - waitCount;
					while (num3 > 0 && this.m_asyncHead != null)
					{
						num--;
						num3--;
						SemaphoreSlim.TaskNode asyncHead = this.m_asyncHead;
						this.RemoveAsyncWaiter(asyncHead);
						SemaphoreSlim.QueueWaiterTask(asyncHead);
					}
				}
				this.m_currentCount = num;
				if (this.m_waitHandle != null && num2 == 0 && num > 0)
				{
					this.m_waitHandle.Set();
				}
			}
			return num2;
		}

		// Token: 0x06003562 RID: 13666 RVA: 0x000C56C8 File Offset: 0x000C38C8
		[SecuritySafeCritical]
		private static void QueueWaiterTask(SemaphoreSlim.TaskNode waiterTask)
		{
			ThreadPool.UnsafeQueueCustomWorkItem(waiterTask, false);
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.SemaphoreSlim" /> class.</summary>
		// Token: 0x06003563 RID: 13667 RVA: 0x000C56D1 File Offset: 0x000C38D1
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Threading.SemaphoreSlim" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06003564 RID: 13668 RVA: 0x000C56E0 File Offset: 0x000C38E0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_waitHandle != null)
				{
					this.m_waitHandle.Close();
					this.m_waitHandle = null;
				}
				this.m_lockObj = null;
				this.m_asyncHead = null;
				this.m_asyncTail = null;
			}
		}

		// Token: 0x06003565 RID: 13669 RVA: 0x000C571C File Offset: 0x000C391C
		private static void CancellationTokenCanceledEventHandler(object obj)
		{
			SemaphoreSlim semaphoreSlim = obj as SemaphoreSlim;
			object lockObj = semaphoreSlim.m_lockObj;
			lock (lockObj)
			{
				Monitor.PulseAll(semaphoreSlim.m_lockObj);
			}
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x000C5768 File Offset: 0x000C3968
		private void CheckDispose()
		{
			if (this.m_lockObj == null)
			{
				throw new ObjectDisposedException(null, SemaphoreSlim.GetResourceString("The semaphore has been disposed."));
			}
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x000C5783 File Offset: 0x000C3983
		private static string GetResourceString(string str)
		{
			return Environment.GetResourceString(str);
		}

		// Token: 0x04001C64 RID: 7268
		private volatile int m_currentCount;

		// Token: 0x04001C65 RID: 7269
		private readonly int m_maxCount;

		// Token: 0x04001C66 RID: 7270
		private volatile int m_waitCount;

		// Token: 0x04001C67 RID: 7271
		private object m_lockObj;

		// Token: 0x04001C68 RID: 7272
		private volatile ManualResetEvent m_waitHandle;

		// Token: 0x04001C69 RID: 7273
		private SemaphoreSlim.TaskNode m_asyncHead;

		// Token: 0x04001C6A RID: 7274
		private SemaphoreSlim.TaskNode m_asyncTail;

		// Token: 0x04001C6B RID: 7275
		private static readonly Task<bool> s_trueTask = new Task<bool>(false, true, (TaskCreationOptions)16384, default(CancellationToken));

		// Token: 0x04001C6C RID: 7276
		private const int NO_MAXIMUM = 2147483647;

		// Token: 0x04001C6D RID: 7277
		private static Action<object> s_cancellationTokenCanceledEventHandler = new Action<object>(SemaphoreSlim.CancellationTokenCanceledEventHandler);

		// Token: 0x0200045C RID: 1116
		private sealed class TaskNode : Task<bool>, IThreadPoolWorkItem
		{
			// Token: 0x06003569 RID: 13673 RVA: 0x000C57C4 File Offset: 0x000C39C4
			internal TaskNode()
			{
			}

			// Token: 0x0600356A RID: 13674 RVA: 0x000C57CC File Offset: 0x000C39CC
			[SecurityCritical]
			void IThreadPoolWorkItem.ExecuteWorkItem()
			{
				base.TrySetResult(true);
			}

			// Token: 0x0600356B RID: 13675 RVA: 0x00002194 File Offset: 0x00000394
			[SecurityCritical]
			void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
			{
			}

			// Token: 0x04001C6E RID: 7278
			internal SemaphoreSlim.TaskNode Prev;

			// Token: 0x04001C6F RID: 7279
			internal SemaphoreSlim.TaskNode Next;
		}
	}
}
