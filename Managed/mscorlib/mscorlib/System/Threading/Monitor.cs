using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Provides a mechanism that synchronizes access to objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200047A RID: 1146
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public static class Monitor
	{
		/// <summary>Acquires an exclusive lock on the specified object.</summary>
		/// <param name="obj">The object on which to acquire the monitor lock. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003619 RID: 13849
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Enter(object obj);

		/// <summary>Acquires an exclusive lock on the specified object, and atomically sets a value that indicates whether the lock was taken.</summary>
		/// <param name="obj">The object on which to wait. </param>
		/// <param name="lockTaken">The result of the attempt to acquire the lock, passed by reference. The input must be false. The output is true if the lock is acquired; otherwise, the output is false. The output is set even if an exception occurs during the attempt to acquire the lock. Note   If no exception occurs, the output of this method is always true.</param>
		/// <exception cref="T:System.ArgumentException">The input to <paramref name="lockTaken" /> is true.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null. </exception>
		// Token: 0x0600361A RID: 13850 RVA: 0x000C7BEB File Offset: 0x000C5DEB
		public static void Enter(object obj, ref bool lockTaken)
		{
			if (lockTaken)
			{
				Monitor.ThrowLockTakenException();
			}
			Monitor.ReliableEnter(obj, ref lockTaken);
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x000C7BFD File Offset: 0x000C5DFD
		private static void ThrowLockTakenException()
		{
			throw new ArgumentException(Environment.GetResourceString("Argument must be initialized to false"), "lockTaken");
		}

		/// <summary>Releases an exclusive lock on the specified object.</summary>
		/// <param name="obj">The object on which to release the lock. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null. </exception>
		/// <exception cref="T:System.Threading.SynchronizationLockException">The current thread does not own the lock for the specified object. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600361C RID: 13852
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Exit(object obj);

		/// <summary>Attempts to acquire an exclusive lock on the specified object.</summary>
		/// <returns>true if the current thread acquires the lock; otherwise, false.</returns>
		/// <param name="obj">The object on which to acquire the lock. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600361D RID: 13853 RVA: 0x000C7C14 File Offset: 0x000C5E14
		public static bool TryEnter(object obj)
		{
			bool flag = false;
			Monitor.TryEnter(obj, 0, ref flag);
			return flag;
		}

		/// <summary>Attempts to acquire an exclusive lock on the specified object, and atomically sets a value that indicates whether the lock was taken.</summary>
		/// <param name="obj">The object on which to acquire the lock. </param>
		/// <param name="lockTaken">The result of the attempt to acquire the lock, passed by reference. The input must be false. The output is true if the lock is acquired; otherwise, the output is false. The output is set even if an exception occurs during the attempt to acquire the lock.</param>
		/// <exception cref="T:System.ArgumentException">The input to <paramref name="lockTaken" /> is true.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null. </exception>
		// Token: 0x0600361E RID: 13854 RVA: 0x000C7C2D File Offset: 0x000C5E2D
		public static void TryEnter(object obj, ref bool lockTaken)
		{
			if (lockTaken)
			{
				Monitor.ThrowLockTakenException();
			}
			Monitor.ReliableEnterTimeout(obj, 0, ref lockTaken);
		}

		/// <summary>Attempts, for the specified number of milliseconds, to acquire an exclusive lock on the specified object.</summary>
		/// <returns>true if the current thread acquires the lock; otherwise, false.</returns>
		/// <param name="obj">The object on which to acquire the lock. </param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait for the lock. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is negative, and not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600361F RID: 13855 RVA: 0x000C7C40 File Offset: 0x000C5E40
		public static bool TryEnter(object obj, int millisecondsTimeout)
		{
			bool flag = false;
			Monitor.TryEnter(obj, millisecondsTimeout, ref flag);
			return flag;
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x000C7C5C File Offset: 0x000C5E5C
		private static int MillisecondsTimeoutFromTimeSpan(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return (int)num;
		}

		/// <summary>Attempts, for the specified amount of time, to acquire an exclusive lock on the specified object.</summary>
		/// <returns>true if the current thread acquires the lock; otherwise, false.</returns>
		/// <param name="obj">The object on which to acquire the lock. </param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> representing the amount of time to wait for the lock. A value of –1 millisecond specifies an infinite wait.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="timeout" /> in milliseconds is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> (–1 millisecond), or is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003621 RID: 13857 RVA: 0x000C7C97 File Offset: 0x000C5E97
		public static bool TryEnter(object obj, TimeSpan timeout)
		{
			return Monitor.TryEnter(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout));
		}

		/// <summary>Attempts, for the specified number of milliseconds, to acquire an exclusive lock on the specified object, and atomically sets a value that indicates whether the lock was taken.</summary>
		/// <param name="obj">The object on which to acquire the lock. </param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait for the lock. </param>
		/// <param name="lockTaken">The result of the attempt to acquire the lock, passed by reference. The input must be false. The output is true if the lock is acquired; otherwise, the output is false. The output is set even if an exception occurs during the attempt to acquire the lock.</param>
		/// <exception cref="T:System.ArgumentException">The input to <paramref name="lockTaken" /> is true.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is negative, and not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		// Token: 0x06003622 RID: 13858 RVA: 0x000C7CA5 File Offset: 0x000C5EA5
		public static void TryEnter(object obj, int millisecondsTimeout, ref bool lockTaken)
		{
			if (lockTaken)
			{
				Monitor.ThrowLockTakenException();
			}
			Monitor.ReliableEnterTimeout(obj, millisecondsTimeout, ref lockTaken);
		}

		/// <summary>Attempts, for the specified amount of time, to acquire an exclusive lock on the specified object, and atomically sets a value that indicates whether the lock was taken.</summary>
		/// <param name="obj">The object on which to acquire the lock. </param>
		/// <param name="timeout">The amount of time to wait for the lock. A value of –1 millisecond specifies an infinite wait.</param>
		/// <param name="lockTaken">The result of the attempt to acquire the lock, passed by reference. The input must be false. The output is true if the lock is acquired; otherwise, the output is false. The output is set even if an exception occurs during the attempt to acquire the lock.</param>
		/// <exception cref="T:System.ArgumentException">The input to <paramref name="lockTaken" /> is true.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="timeout" /> in milliseconds is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> (–1 millisecond), or is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		// Token: 0x06003623 RID: 13859 RVA: 0x000C7CB8 File Offset: 0x000C5EB8
		public static void TryEnter(object obj, TimeSpan timeout, ref bool lockTaken)
		{
			if (lockTaken)
			{
				Monitor.ThrowLockTakenException();
			}
			Monitor.ReliableEnterTimeout(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout), ref lockTaken);
		}

		/// <summary>Determines whether the current thread holds the lock on the specified object. </summary>
		/// <returns>true if the current thread holds the lock on <paramref name="obj" />; otherwise, false.</returns>
		/// <param name="obj">The object to test. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is null. </exception>
		// Token: 0x06003624 RID: 13860 RVA: 0x000C7CD0 File Offset: 0x000C5ED0
		[SecuritySafeCritical]
		public static bool IsEntered(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			return Monitor.IsEnteredNative(obj);
		}

		/// <summary>Releases the lock on an object and blocks the current thread until it reacquires the lock. If the specified time-out interval elapses, the thread enters the ready queue. This method also specifies whether the synchronization domain for the context (if in a synchronized context) is exited before the wait and reacquired afterward.</summary>
		/// <returns>true if the lock was reacquired before the specified time elapsed; false if the lock was reacquired after the specified time elapsed. The method does not return until the lock is reacquired.</returns>
		/// <param name="obj">The object on which to wait. </param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait before the thread enters the ready queue. </param>
		/// <param name="exitContext">true to exit and reacquire the synchronization domain for the context (if in a synchronized context) before the wait; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null. </exception>
		/// <exception cref="T:System.Threading.SynchronizationLockException">Wait is not invoked from within a synchronized block of code. </exception>
		/// <exception cref="T:System.Threading.ThreadInterruptedException">The thread that invokes Wait is later interrupted from the waiting state. This happens when another thread calls this thread's <see cref="M:System.Threading.Thread.Interrupt" /> method. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="millisecondsTimeout" /> parameter is negative, and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003625 RID: 13861 RVA: 0x000C7CE6 File Offset: 0x000C5EE6
		[SecuritySafeCritical]
		public static bool Wait(object obj, int millisecondsTimeout, bool exitContext)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			return Monitor.ObjWait(exitContext, millisecondsTimeout, obj);
		}

		/// <summary>Releases the lock on an object and blocks the current thread until it reacquires the lock. If the specified time-out interval elapses, the thread enters the ready queue. Optionally exits the synchronization domain for the synchronized context before the wait and reacquires the domain afterward.</summary>
		/// <returns>true if the lock was reacquired before the specified time elapsed; false if the lock was reacquired after the specified time elapsed. The method does not return until the lock is reacquired.</returns>
		/// <param name="obj">The object on which to wait. </param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> representing the amount of time to wait before the thread enters the ready queue. </param>
		/// <param name="exitContext">true to exit and reacquire the synchronization domain for the context (if in a synchronized context) before the wait; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null. </exception>
		/// <exception cref="T:System.Threading.SynchronizationLockException">Wait is not invoked from within a synchronized block of code. </exception>
		/// <exception cref="T:System.Threading.ThreadInterruptedException">The thread that invokes Wait is later interrupted from the waiting state. This happens when another thread calls this thread's <see cref="M:System.Threading.Thread.Interrupt" /> method. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="timeout" /> parameter is negative and does not represent <see cref="F:System.Threading.Timeout.Infinite" /> (–1 millisecond), or is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003626 RID: 13862 RVA: 0x000C7CFE File Offset: 0x000C5EFE
		public static bool Wait(object obj, TimeSpan timeout, bool exitContext)
		{
			return Monitor.Wait(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout), exitContext);
		}

		/// <summary>Releases the lock on an object and blocks the current thread until it reacquires the lock. If the specified time-out interval elapses, the thread enters the ready queue.</summary>
		/// <returns>true if the lock was reacquired before the specified time elapsed; false if the lock was reacquired after the specified time elapsed. The method does not return until the lock is reacquired.</returns>
		/// <param name="obj">The object on which to wait. </param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait before the thread enters the ready queue. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null. </exception>
		/// <exception cref="T:System.Threading.SynchronizationLockException">The calling thread does not own the lock for the specified object. </exception>
		/// <exception cref="T:System.Threading.ThreadInterruptedException">The thread that invokes Wait is later interrupted from the waiting state. This happens when another thread calls this thread's <see cref="M:System.Threading.Thread.Interrupt" /> method. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="millisecondsTimeout" /> parameter is negative, and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003627 RID: 13863 RVA: 0x000C7D0D File Offset: 0x000C5F0D
		public static bool Wait(object obj, int millisecondsTimeout)
		{
			return Monitor.Wait(obj, millisecondsTimeout, false);
		}

		/// <summary>Releases the lock on an object and blocks the current thread until it reacquires the lock. If the specified time-out interval elapses, the thread enters the ready queue.</summary>
		/// <returns>true if the lock was reacquired before the specified time elapsed; false if the lock was reacquired after the specified time elapsed. The method does not return until the lock is reacquired.</returns>
		/// <param name="obj">The object on which to wait. </param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> representing the amount of time to wait before the thread enters the ready queue. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null. </exception>
		/// <exception cref="T:System.Threading.SynchronizationLockException">The calling thread does not own the lock for the specified object. </exception>
		/// <exception cref="T:System.Threading.ThreadInterruptedException">The thread that invokes Wait is later interrupted from the waiting state. This happens when another thread calls this thread's <see cref="M:System.Threading.Thread.Interrupt" /> method. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="timeout" /> parameter in milliseconds is negative and does not represent <see cref="F:System.Threading.Timeout.Infinite" /> (–1 millisecond), or is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003628 RID: 13864 RVA: 0x000C7D17 File Offset: 0x000C5F17
		public static bool Wait(object obj, TimeSpan timeout)
		{
			return Monitor.Wait(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout), false);
		}

		/// <summary>Releases the lock on an object and blocks the current thread until it reacquires the lock.</summary>
		/// <returns>true if the call returned because the caller reacquired the lock for the specified object. This method does not return if the lock is not reacquired.</returns>
		/// <param name="obj">The object on which to wait. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null. </exception>
		/// <exception cref="T:System.Threading.SynchronizationLockException">The calling thread does not own the lock for the specified object. </exception>
		/// <exception cref="T:System.Threading.ThreadInterruptedException">The thread that invokes Wait is later interrupted from the waiting state. This happens when another thread calls this thread's <see cref="M:System.Threading.Thread.Interrupt" /> method. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003629 RID: 13865 RVA: 0x000C7D26 File Offset: 0x000C5F26
		public static bool Wait(object obj)
		{
			return Monitor.Wait(obj, -1, false);
		}

		/// <summary>Notifies a thread in the waiting queue of a change in the locked object's state.</summary>
		/// <param name="obj">The object a thread is waiting for. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null. </exception>
		/// <exception cref="T:System.Threading.SynchronizationLockException">The calling thread does not own the lock for the specified object. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600362A RID: 13866 RVA: 0x000C7D30 File Offset: 0x000C5F30
		[SecuritySafeCritical]
		public static void Pulse(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			Monitor.ObjPulse(obj);
		}

		/// <summary>Notifies all waiting threads of a change in the object's state.</summary>
		/// <param name="obj">The object that sends the pulse. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null. </exception>
		/// <exception cref="T:System.Threading.SynchronizationLockException">The calling thread does not own the lock for the specified object. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600362B RID: 13867 RVA: 0x000C7D46 File Offset: 0x000C5F46
		[SecuritySafeCritical]
		public static void PulseAll(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			Monitor.ObjPulseAll(obj);
		}

		// Token: 0x0600362C RID: 13868
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Monitor_test_synchronised(object obj);

		// Token: 0x0600362D RID: 13869
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Monitor_pulse(object obj);

		// Token: 0x0600362E RID: 13870 RVA: 0x000C7D5C File Offset: 0x000C5F5C
		private static void ObjPulse(object obj)
		{
			if (!Monitor.Monitor_test_synchronised(obj))
			{
				throw new SynchronizationLockException("Object is not synchronized");
			}
			Monitor.Monitor_pulse(obj);
		}

		// Token: 0x0600362F RID: 13871
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Monitor_pulse_all(object obj);

		// Token: 0x06003630 RID: 13872 RVA: 0x000C7D77 File Offset: 0x000C5F77
		private static void ObjPulseAll(object obj)
		{
			if (!Monitor.Monitor_test_synchronised(obj))
			{
				throw new SynchronizationLockException("Object is not synchronized");
			}
			Monitor.Monitor_pulse_all(obj);
		}

		// Token: 0x06003631 RID: 13873
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Monitor_wait(object obj, int ms);

		// Token: 0x06003632 RID: 13874 RVA: 0x000C7D94 File Offset: 0x000C5F94
		private static bool ObjWait(bool exitContext, int millisecondsTimeout, object obj)
		{
			if (millisecondsTimeout < 0 && millisecondsTimeout != -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			if (!Monitor.Monitor_test_synchronised(obj))
			{
				throw new SynchronizationLockException("Object is not synchronized");
			}
			bool flag;
			try
			{
				if (exitContext)
				{
					SynchronizationAttribute.ExitContext();
				}
				flag = Monitor.Monitor_wait(obj, millisecondsTimeout);
			}
			finally
			{
				if (exitContext)
				{
					SynchronizationAttribute.EnterContext();
				}
			}
			return flag;
		}

		// Token: 0x06003633 RID: 13875
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void try_enter_with_atomic_var(object obj, int millisecondsTimeout, ref bool lockTaken);

		// Token: 0x06003634 RID: 13876 RVA: 0x000C7DF4 File Offset: 0x000C5FF4
		private static void ReliableEnterTimeout(object obj, int timeout, ref bool lockTaken)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (timeout < 0 && timeout != -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			Monitor.try_enter_with_atomic_var(obj, timeout, ref lockTaken);
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x000C7E1F File Offset: 0x000C601F
		private static void ReliableEnter(object obj, ref bool lockTaken)
		{
			Monitor.ReliableEnterTimeout(obj, -1, ref lockTaken);
		}

		// Token: 0x06003636 RID: 13878
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Monitor_test_owner(object obj);

		// Token: 0x06003637 RID: 13879 RVA: 0x000C7E29 File Offset: 0x000C6029
		private static bool IsEnteredNative(object obj)
		{
			return Monitor.Monitor_test_owner(obj);
		}
	}
}
