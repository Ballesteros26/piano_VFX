using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Threading
{
	/// <summary>Creates and controls a thread, sets its priority, and gets its status. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000484 RID: 1156
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_Thread))]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class Thread : CriticalFinalizerObject, _Thread
	{
		// Token: 0x06003665 RID: 13925 RVA: 0x000C8118 File Offset: 0x000C6318
		private static void AsyncLocalSetCurrentCulture(AsyncLocalValueChangedArgs<CultureInfo> args)
		{
			Thread.m_CurrentCulture = args.CurrentValue;
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x000C8126 File Offset: 0x000C6326
		private static void AsyncLocalSetCurrentUICulture(AsyncLocalValueChangedArgs<CultureInfo> args)
		{
			Thread.m_CurrentUICulture = args.CurrentValue;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Thread" /> class.</summary>
		/// <param name="start">A <see cref="T:System.Threading.ThreadStart" /> delegate that represents the methods to be invoked when this thread begins executing. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="start" /> parameter is null. </exception>
		// Token: 0x06003667 RID: 13927 RVA: 0x000C8134 File Offset: 0x000C6334
		[SecuritySafeCritical]
		public Thread(ThreadStart start)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			this.SetStartHelper(start, 0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Thread" /> class, specifying the maximum stack size for the thread.</summary>
		/// <param name="start">A <see cref="T:System.Threading.ThreadStart" /> delegate that represents the methods to be invoked when this thread begins executing.</param>
		/// <param name="maxStackSize">The maximum stack size, in bytes, to be used by the thread, or 0 to use the default maximum stack size specified in the header for the executable.Important   For partially trusted code, <paramref name="maxStackSize" /> is ignored if it is greater than the default stack size. No exception is thrown. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="start" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxStackSize" /> is less than zero.</exception>
		// Token: 0x06003668 RID: 13928 RVA: 0x000C8152 File Offset: 0x000C6352
		[SecuritySafeCritical]
		public Thread(ThreadStart start, int maxStackSize)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			if (0 > maxStackSize)
			{
				throw new ArgumentOutOfRangeException("maxStackSize", Environment.GetResourceString("Non-negative number required."));
			}
			this.SetStartHelper(start, maxStackSize);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Thread" /> class, specifying a delegate that allows an object to be passed to the thread when the thread is started.</summary>
		/// <param name="start">A <see cref="T:System.Threading.ParameterizedThreadStart" /> delegate that represents the methods to be invoked when this thread begins executing.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="start" /> is null. </exception>
		// Token: 0x06003669 RID: 13929 RVA: 0x000C8134 File Offset: 0x000C6334
		[SecuritySafeCritical]
		public Thread(ParameterizedThreadStart start)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			this.SetStartHelper(start, 0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Thread" /> class, specifying a delegate that allows an object to be passed to the thread when the thread is started and specifying the maximum stack size for the thread.</summary>
		/// <param name="start">A <see cref="T:System.Threading.ParameterizedThreadStart" /> delegate that represents the methods to be invoked when this thread begins executing.</param>
		/// <param name="maxStackSize">The maximum stack size, in bytes, to be used by the thread, or 0 to use the default maximum stack size specified in the header for the executable.Important   For partially trusted code, <paramref name="maxStackSize" /> is ignored if it is greater than the default stack size. No exception is thrown.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="start" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxStackSize" /> is less than zero.</exception>
		// Token: 0x0600366A RID: 13930 RVA: 0x000C8152 File Offset: 0x000C6352
		[SecuritySafeCritical]
		public Thread(ParameterizedThreadStart start, int maxStackSize)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			if (0 > maxStackSize)
			{
				throw new ArgumentOutOfRangeException("maxStackSize", Environment.GetResourceString("Non-negative number required."));
			}
			this.SetStartHelper(start, maxStackSize);
		}

		/// <summary>Causes the operating system to change the state of the current instance to <see cref="F:System.Threading.ThreadState.Running" />.</summary>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has already been started. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available to start this thread. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600366B RID: 13931 RVA: 0x000C818C File Offset: 0x000C638C
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Start()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.Start(ref stackCrawlMark);
		}

		/// <summary>Causes the operating system to change the state of the current instance to <see cref="F:System.Threading.ThreadState.Running" />, and optionally supplies an object containing data to be used by the method the thread executes.</summary>
		/// <param name="parameter">An object that contains data to be used by the method the thread executes.</param>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has already been started. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available to start this thread. </exception>
		/// <exception cref="T:System.InvalidOperationException">This thread was created using a <see cref="T:System.Threading.ThreadStart" /> delegate instead of a <see cref="T:System.Threading.ParameterizedThreadStart" /> delegate.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600366C RID: 13932 RVA: 0x000C81A4 File Offset: 0x000C63A4
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Start(object parameter)
		{
			if (this.m_Delegate is ThreadStart)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The thread was created with a ThreadStart delegate that does not accept a parameter."));
			}
			this.m_ThreadStartArg = parameter;
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.Start(ref stackCrawlMark);
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x000C81E0 File Offset: 0x000C63E0
		[SecuritySafeCritical]
		private void Start(ref StackCrawlMark stackMark)
		{
			if (this.m_Delegate != null)
			{
				ThreadHelper threadHelper = (ThreadHelper)this.m_Delegate.Target;
				ExecutionContext executionContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx);
				threadHelper.SetExecutionContextHelper(executionContext);
			}
			IPrincipal principal = null;
			this.StartInternal(principal, ref stackMark);
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x000C821D File Offset: 0x000C641D
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal ExecutionContext.Reader GetExecutionContextReader()
		{
			return new ExecutionContext.Reader(this.m_ExecutionContext);
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x0600366F RID: 13935 RVA: 0x000C822A File Offset: 0x000C642A
		// (set) Token: 0x06003670 RID: 13936 RVA: 0x000C8235 File Offset: 0x000C6435
		internal bool ExecutionContextBelongsToCurrentScope
		{
			get
			{
				return !this.m_ExecutionContextBelongsToOuterScope;
			}
			set
			{
				this.m_ExecutionContextBelongsToOuterScope = !value;
			}
		}

		/// <summary>Gets an <see cref="T:System.Threading.ExecutionContext" /> object that contains information about the various contexts of the current thread. </summary>
		/// <returns>An <see cref="T:System.Threading.ExecutionContext" /> object that consolidates context information for the current thread.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06003671 RID: 13937 RVA: 0x000C8244 File Offset: 0x000C6444
		public ExecutionContext ExecutionContext
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			get
			{
				ExecutionContext executionContext;
				if (this == Thread.CurrentThread)
				{
					executionContext = this.GetMutableExecutionContext();
				}
				else
				{
					executionContext = this.m_ExecutionContext;
				}
				return executionContext;
			}
		}

		// Token: 0x06003672 RID: 13938 RVA: 0x000C826C File Offset: 0x000C646C
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal ExecutionContext GetMutableExecutionContext()
		{
			if (this.m_ExecutionContext == null)
			{
				this.m_ExecutionContext = new ExecutionContext();
			}
			else if (!this.ExecutionContextBelongsToCurrentScope)
			{
				ExecutionContext executionContext = this.m_ExecutionContext.CreateMutableCopy();
				this.m_ExecutionContext = executionContext;
			}
			this.ExecutionContextBelongsToCurrentScope = true;
			return this.m_ExecutionContext;
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x000C82B6 File Offset: 0x000C64B6
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecurityCritical]
		internal void SetExecutionContext(ExecutionContext value, bool belongsToCurrentScope)
		{
			this.m_ExecutionContext = value;
			this.ExecutionContextBelongsToCurrentScope = belongsToCurrentScope;
		}

		// Token: 0x06003674 RID: 13940 RVA: 0x000C82C6 File Offset: 0x000C64C6
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void SetExecutionContext(ExecutionContext.Reader value, bool belongsToCurrentScope)
		{
			this.m_ExecutionContext = value.DangerousGetRawExecutionContext();
			this.ExecutionContextBelongsToCurrentScope = belongsToCurrentScope;
		}

		/// <summary>Applies a captured <see cref="T:System.Threading.CompressedStack" /> to the current thread. </summary>
		/// <param name="stack">The <see cref="T:System.Threading.CompressedStack" /> object to be applied to the current thread.</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="00000000000000000400000000000000" />
		/// </PermissionSet>
		// Token: 0x06003675 RID: 13941 RVA: 0x000C82DC File Offset: 0x000C64DC
		[Obsolete("Thread.SetCompressedStack is no longer supported. Please use the System.Threading.CompressedStack class")]
		[SecurityCritical]
		public void SetCompressedStack(CompressedStack stack)
		{
			throw new InvalidOperationException(Environment.GetResourceString("Use CompressedStack.(Capture/Run) or ExecutionContext.(Capture/Run) APIs instead."));
		}

		/// <summary>Returns a <see cref="T:System.Threading.CompressedStack" /> object that can be used to capture the stack for the current thread. </summary>
		/// <returns>None. </returns>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="00000000000000000400000000000000" />
		/// </PermissionSet>
		// Token: 0x06003676 RID: 13942 RVA: 0x000C82DC File Offset: 0x000C64DC
		[SecurityCritical]
		[Obsolete("Thread.GetCompressedStack is no longer supported. Please use the System.Threading.CompressedStack class")]
		public CompressedStack GetCompressedStack()
		{
			throw new InvalidOperationException(Environment.GetResourceString("Use CompressedStack.(Capture/Run) or ExecutionContext.(Capture/Run) APIs instead."));
		}

		/// <summary>Cancels an <see cref="M:System.Threading.Thread.Abort(System.Object)" /> requested for the current thread.</summary>
		/// <exception cref="T:System.Threading.ThreadStateException">Abort was not invoked on the current thread. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required security permission for the current thread. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x06003677 RID: 13943 RVA: 0x000C82ED File Offset: 0x000C64ED
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		public static void ResetAbort()
		{
			Thread currentThread = Thread.CurrentThread;
			if ((currentThread.ThreadState & ThreadState.AbortRequested) == ThreadState.Running)
			{
				throw new ThreadStateException(Environment.GetResourceString("Unable to reset abort because no abort was requested."));
			}
			currentThread.ResetAbortNative();
			currentThread.ClearAbortReason();
		}

		// Token: 0x06003678 RID: 13944
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResetAbortNative();

		/// <summary>Either suspends the thread, or if the thread is already suspended, has no effect.</summary>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has not been started or is dead. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the appropriate <see cref="T:System.Security.Permissions.SecurityPermission" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x06003679 RID: 13945 RVA: 0x000C831D File Offset: 0x000C651D
		[Obsolete("Thread.Suspend has been deprecated.  Please use other classes in System.Threading, such as Monitor, Mutex, Event, and Semaphore, to synchronize Threads or protect resources.  http://go.microsoft.com/fwlink/?linkid=14202", false)]
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		public void Suspend()
		{
			this.SuspendInternal();
		}

		// Token: 0x0600367A RID: 13946
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SuspendInternal();

		/// <summary>Resumes a thread that has been suspended.</summary>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has not been started, is dead, or is not in the suspended state. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the appropriate <see cref="T:System.Security.Permissions.SecurityPermission" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x0600367B RID: 13947 RVA: 0x000C8325 File Offset: 0x000C6525
		[Obsolete("Thread.Resume has been deprecated.  Please use other classes in System.Threading, such as Monitor, Mutex, Event, and Semaphore, to synchronize Threads or protect resources.  http://go.microsoft.com/fwlink/?linkid=14202", false)]
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		public void Resume()
		{
			this.ResumeInternal();
		}

		// Token: 0x0600367C RID: 13948
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResumeInternal();

		/// <summary>Interrupts a thread that is in the WaitSleepJoin thread state.</summary>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the appropriate <see cref="T:System.Security.Permissions.SecurityPermission" />. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x0600367D RID: 13949 RVA: 0x000C832D File Offset: 0x000C652D
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		public void Interrupt()
		{
			this.InterruptInternal();
		}

		// Token: 0x0600367E RID: 13950
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InterruptInternal();

		/// <summary>Gets or sets a value indicating the scheduling priority of a thread.</summary>
		/// <returns>One of the <see cref="T:System.Threading.ThreadPriority" /> values. The default value is Normal.</returns>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has reached a final state, such as <see cref="F:System.Threading.ThreadState.Aborted" />. </exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is not a valid ThreadPriority value. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x0600367F RID: 13951 RVA: 0x000C8335 File Offset: 0x000C6535
		// (set) Token: 0x06003680 RID: 13952 RVA: 0x000C833D File Offset: 0x000C653D
		public ThreadPriority Priority
		{
			[SecuritySafeCritical]
			get
			{
				return (ThreadPriority)this.GetPriorityNative();
			}
			[SecuritySafeCritical]
			[HostProtection(SecurityAction.LinkDemand, SelfAffectingThreading = true)]
			set
			{
				this.SetPriorityNative((int)value);
			}
		}

		// Token: 0x06003681 RID: 13953
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetPriorityNative();

		// Token: 0x06003682 RID: 13954
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPriorityNative(int priority);

		// Token: 0x06003683 RID: 13955
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool JoinInternal(int millisecondsTimeout);

		/// <summary>Blocks the calling thread until a thread terminates, while continuing to perform standard COM and SendMessage pumping.</summary>
		/// <exception cref="T:System.Threading.ThreadStateException">The caller attempted to join a thread that is in the <see cref="F:System.Threading.ThreadState.Unstarted" /> state. </exception>
		/// <exception cref="T:System.Threading.ThreadInterruptedException">The thread is interrupted while waiting. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003684 RID: 13956 RVA: 0x000C8346 File Offset: 0x000C6546
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public void Join()
		{
			this.JoinInternal(-1);
		}

		/// <summary>Blocks the calling thread until a thread terminates or the specified time elapses, while continuing to perform standard COM and SendMessage pumping.</summary>
		/// <returns>true if the thread has terminated; false if the thread has not terminated after the amount of time specified by the <paramref name="millisecondsTimeout" /> parameter has elapsed.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait for the thread to terminate. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="millisecondsTimeout" /> is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> in milliseconds. </exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has not been started. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003685 RID: 13957 RVA: 0x000C8350 File Offset: 0x000C6550
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public bool Join(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return this.JoinInternal(millisecondsTimeout);
		}

		/// <summary>Blocks the calling thread until a thread terminates or the specified time elapses, while continuing to perform standard COM and SendMessage pumping.</summary>
		/// <returns>true if the thread terminated; false if the thread has not terminated after the amount of time specified by the <paramref name="timeout" /> parameter has elapsed.</returns>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> set to the amount of time to wait for the thread to terminate. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="timeout" /> is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> in milliseconds, or is greater than <see cref="F:System.Int32.MaxValue" /> milliseconds. </exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The caller attempted to join a thread that is in the <see cref="F:System.Threading.ThreadState.Unstarted" /> state. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003686 RID: 13958 RVA: 0x000C8374 File Offset: 0x000C6574
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public bool Join(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return this.Join((int)num);
		}

		// Token: 0x06003687 RID: 13959
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SleepInternal(int millisecondsTimeout);

		/// <summary>Suspends the current thread for a specified time.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds for which the thread is blocked. Specify zero (0) to indicate that this thread should be suspended to allow other waiting threads to execute. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to block the thread indefinitely. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The time-out value is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003688 RID: 13960 RVA: 0x000C83B5 File Offset: 0x000C65B5
		[SecuritySafeCritical]
		public static void Sleep(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			Thread.SleepInternal(millisecondsTimeout);
		}

		/// <summary>Blocks the current thread for a specified time.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> set to the amount of time for which the thread is blocked. Specify zero to indicate that this thread should be suspended to allow other waiting threads to execute. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to block the thread indefinitely. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="timeout" /> is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> in milliseconds, or is greater than <see cref="F:System.Int32.MaxValue" /> milliseconds. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003689 RID: 13961 RVA: 0x000C83D8 File Offset: 0x000C65D8
		public static void Sleep(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			Thread.Sleep((int)num);
		}

		// Token: 0x0600368A RID: 13962
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool YieldInternal();

		/// <summary>Causes the calling thread to yield execution to another thread that is ready to run on the current processor. The operating system selects the thread to yield to.</summary>
		/// <returns>true if the operating system switched execution to another thread; otherwise, false.</returns>
		// Token: 0x0600368B RID: 13963 RVA: 0x000C8418 File Offset: 0x000C6618
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public static bool Yield()
		{
			return Thread.YieldInternal();
		}

		// Token: 0x0600368C RID: 13964 RVA: 0x000C8420 File Offset: 0x000C6620
		[SecurityCritical]
		private void SetStartHelper(Delegate start, int maxStackSize)
		{
			maxStackSize = Thread.GetProcessDefaultStackSize(maxStackSize);
			ThreadHelper threadHelper = new ThreadHelper(start);
			if (start is ThreadStart)
			{
				this.SetStart(new ThreadStart(threadHelper.ThreadStart), maxStackSize);
				return;
			}
			this.SetStart(new ParameterizedThreadStart(threadHelper.ThreadStart), maxStackSize);
		}

		/// <summary>Allocates an unnamed data slot on all the threads. For better performance, use fields that are marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <returns>The allocated named data slot on all threads.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600368D RID: 13965 RVA: 0x000C846B File Offset: 0x000C666B
		[HostProtection(SecurityAction.LinkDemand, SharedState = true, ExternalThreading = true)]
		public static LocalDataStoreSlot AllocateDataSlot()
		{
			return Thread.LocalDataStoreManager.AllocateDataSlot();
		}

		/// <summary>Allocates a named data slot on all threads. For better performance, use fields that are marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <returns>The allocated named data slot on all threads.</returns>
		/// <param name="name">The name of the data slot to be allocated. </param>
		/// <exception cref="T:System.ArgumentException">A named data slot with the specified name already exists.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600368E RID: 13966 RVA: 0x000C8477 File Offset: 0x000C6677
		[HostProtection(SecurityAction.LinkDemand, SharedState = true, ExternalThreading = true)]
		public static LocalDataStoreSlot AllocateNamedDataSlot(string name)
		{
			return Thread.LocalDataStoreManager.AllocateNamedDataSlot(name);
		}

		/// <summary>Looks up a named data slot. For better performance, use fields that are marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <returns>A <see cref="T:System.LocalDataStoreSlot" /> allocated for this thread.</returns>
		/// <param name="name">The name of the local data slot. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600368F RID: 13967 RVA: 0x000C8484 File Offset: 0x000C6684
		[HostProtection(SecurityAction.LinkDemand, SharedState = true, ExternalThreading = true)]
		public static LocalDataStoreSlot GetNamedDataSlot(string name)
		{
			return Thread.LocalDataStoreManager.GetNamedDataSlot(name);
		}

		/// <summary>Eliminates the association between a name and a slot, for all threads in the process. For better performance, use fields that are marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <param name="name">The name of the data slot to be freed. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003690 RID: 13968 RVA: 0x000C8491 File Offset: 0x000C6691
		[HostProtection(SecurityAction.LinkDemand, SharedState = true, ExternalThreading = true)]
		public static void FreeNamedDataSlot(string name)
		{
			Thread.LocalDataStoreManager.FreeNamedDataSlot(name);
		}

		/// <summary>Retrieves the value from the specified slot on the current thread, within the current thread's current domain. For better performance, use fields that are marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <returns>The retrieved value.</returns>
		/// <param name="slot">The <see cref="T:System.LocalDataStoreSlot" /> from which to get the value. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003691 RID: 13969 RVA: 0x000C84A0 File Offset: 0x000C66A0
		[HostProtection(SecurityAction.LinkDemand, SharedState = true, ExternalThreading = true)]
		public static object GetData(LocalDataStoreSlot slot)
		{
			LocalDataStoreHolder localDataStoreHolder = Thread.s_LocalDataStore;
			if (localDataStoreHolder == null)
			{
				Thread.LocalDataStoreManager.ValidateSlot(slot);
				return null;
			}
			return localDataStoreHolder.Store.GetData(slot);
		}

		/// <summary>Sets the data in the specified slot on the currently running thread, for that thread's current domain. For better performance, use fields marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <param name="slot">The <see cref="T:System.LocalDataStoreSlot" /> in which to set the value. </param>
		/// <param name="data">The value to be set. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003692 RID: 13970 RVA: 0x000C84D0 File Offset: 0x000C66D0
		[HostProtection(SecurityAction.LinkDemand, SharedState = true, ExternalThreading = true)]
		public static void SetData(LocalDataStoreSlot slot, object data)
		{
			LocalDataStoreHolder localDataStoreHolder = Thread.s_LocalDataStore;
			if (localDataStoreHolder == null)
			{
				localDataStoreHolder = Thread.LocalDataStoreManager.CreateLocalDataStore();
				Thread.s_LocalDataStore = localDataStoreHolder;
			}
			localDataStoreHolder.Store.SetData(slot, data);
		}

		/// <summary>Gets or sets the current culture used by the Resource Manager to look up culture-specific resources at run time.</summary>
		/// <returns>An object that represents the current culture.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is set to a culture name that cannot be used to locate a resource file. Resource filenames must include only letters, numbers, hyphens or underscores.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06003693 RID: 13971 RVA: 0x000C8504 File Offset: 0x000C6704
		// (set) Token: 0x06003694 RID: 13972 RVA: 0x000C850C File Offset: 0x000C670C
		public CultureInfo CurrentUICulture
		{
			get
			{
				return this.GetCurrentUICultureNoAppX();
			}
			[SecuritySafeCritical]
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				CultureInfo.VerifyCultureName(value, true);
				if (Thread.m_CurrentUICulture == null && Thread.m_CurrentCulture == null)
				{
					Thread.nativeInitCultureAccessors();
				}
				if (!AppContextSwitches.NoAsyncCurrentCulture)
				{
					if (Thread.s_asyncLocalCurrentUICulture == null)
					{
						Interlocked.CompareExchange<AsyncLocal<CultureInfo>>(ref Thread.s_asyncLocalCurrentUICulture, new AsyncLocal<CultureInfo>(new Action<AsyncLocalValueChangedArgs<CultureInfo>>(Thread.AsyncLocalSetCurrentUICulture)), null);
					}
					Thread.s_asyncLocalCurrentUICulture.Value = value;
					return;
				}
				Thread.m_CurrentUICulture = value;
			}
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x000C8580 File Offset: 0x000C6780
		internal CultureInfo GetCurrentUICultureNoAppX()
		{
			if (Thread.m_CurrentUICulture != null)
			{
				return Thread.m_CurrentUICulture;
			}
			CultureInfo defaultThreadCurrentUICulture = CultureInfo.DefaultThreadCurrentUICulture;
			if (defaultThreadCurrentUICulture == null)
			{
				return CultureInfo.UserDefaultUICulture;
			}
			return defaultThreadCurrentUICulture;
		}

		/// <summary>Gets or sets the culture for the current thread.</summary>
		/// <returns>An object that represents the culture for the current thread.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is set to null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06003696 RID: 13974 RVA: 0x000C85AA File Offset: 0x000C67AA
		// (set) Token: 0x06003697 RID: 13975 RVA: 0x000C85B4 File Offset: 0x000C67B4
		public CultureInfo CurrentCulture
		{
			get
			{
				return this.GetCurrentCultureNoAppX();
			}
			[SecuritySafeCritical]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (Thread.m_CurrentCulture == null && Thread.m_CurrentUICulture == null)
				{
					Thread.nativeInitCultureAccessors();
				}
				if (!AppContextSwitches.NoAsyncCurrentCulture)
				{
					if (Thread.s_asyncLocalCurrentCulture == null)
					{
						Interlocked.CompareExchange<AsyncLocal<CultureInfo>>(ref Thread.s_asyncLocalCurrentCulture, new AsyncLocal<CultureInfo>(new Action<AsyncLocalValueChangedArgs<CultureInfo>>(Thread.AsyncLocalSetCurrentCulture)), null);
					}
					Thread.s_asyncLocalCurrentCulture.Value = value;
					return;
				}
				Thread.m_CurrentCulture = value;
			}
		}

		// Token: 0x06003698 RID: 13976 RVA: 0x000C8620 File Offset: 0x000C6820
		private CultureInfo GetCurrentCultureNoAppX()
		{
			if (Thread.m_CurrentCulture != null)
			{
				return Thread.m_CurrentCulture;
			}
			CultureInfo defaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentCulture;
			if (defaultThreadCurrentCulture == null)
			{
				return CultureInfo.UserDefaultCulture;
			}
			return defaultThreadCurrentCulture;
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x000C864A File Offset: 0x000C684A
		private static void nativeInitCultureAccessors()
		{
			Thread.m_CurrentCulture = CultureInfo.ConstructCurrentCulture();
			Thread.m_CurrentUICulture = CultureInfo.ConstructCurrentUICulture();
		}

		/// <summary>Synchronizes memory access as follows: The processor executing the current thread cannot reorder instructions in such a way that memory accesses prior to the call to <see cref="M:System.Threading.Thread.MemoryBarrier" /> execute after memory accesses that follow the call to <see cref="M:System.Threading.Thread.MemoryBarrier" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600369A RID: 13978
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void MemoryBarrier();

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x0600369B RID: 13979 RVA: 0x000C8660 File Offset: 0x000C6860
		private static LocalDataStoreMgr LocalDataStoreManager
		{
			get
			{
				if (Thread.s_LocalDataStoreMgr == null)
				{
					Interlocked.CompareExchange<LocalDataStoreMgr>(ref Thread.s_LocalDataStoreMgr, new LocalDataStoreMgr(), null);
				}
				return Thread.s_LocalDataStoreMgr;
			}
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600369C RID: 13980 RVA: 0x0002126B File Offset: 0x0001F46B
		void _Thread.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600369D RID: 13981 RVA: 0x0002126B File Offset: 0x0001F46B
		void _Thread.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600369E RID: 13982 RVA: 0x0002126B File Offset: 0x0001F46B
		void _Thread.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600369F RID: 13983 RVA: 0x0002126B File Offset: 0x0001F46B
		void _Thread.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060036A0 RID: 13984
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ConstructInternalThread();

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x060036A1 RID: 13985 RVA: 0x000C867F File Offset: 0x000C687F
		private InternalThread Internal
		{
			get
			{
				if (this.internal_thread == null)
				{
					this.ConstructInternalThread();
				}
				return this.internal_thread;
			}
		}

		/// <summary>Gets the current context in which the thread is executing.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Contexts.Context" /> representing the current thread context.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x060036A2 RID: 13986 RVA: 0x000C8695 File Offset: 0x000C6895
		public static Context CurrentContext
		{
			[SecurityPermission(SecurityAction.LinkDemand, Infrastructure = true)]
			get
			{
				return AppDomain.InternalGetContext();
			}
		}

		// Token: 0x060036A3 RID: 13987
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern byte[] ByteArrayToRootDomain(byte[] arr);

		// Token: 0x060036A4 RID: 13988
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern byte[] ByteArrayToCurrentDomain(byte[] arr);

		// Token: 0x060036A5 RID: 13989 RVA: 0x000C869C File Offset: 0x000C689C
		private static void DeserializePrincipal(Thread th)
		{
			MemoryStream memoryStream = new MemoryStream(Thread.ByteArrayToCurrentDomain(th.Internal._serialized_principal));
			int num = memoryStream.ReadByte();
			if (num == 0)
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				th.principal = (IPrincipal)binaryFormatter.Deserialize(memoryStream);
				th.principal_version = th.Internal._serialized_principal_version;
				return;
			}
			if (num == 1)
			{
				BinaryReader binaryReader = new BinaryReader(memoryStream);
				string text = binaryReader.ReadString();
				string text2 = binaryReader.ReadString();
				int num2 = binaryReader.ReadInt32();
				string[] array = null;
				if (num2 >= 0)
				{
					array = new string[num2];
					for (int i = 0; i < num2; i++)
					{
						array[i] = binaryReader.ReadString();
					}
				}
				th.principal = new GenericPrincipal(new GenericIdentity(text, text2), array);
				return;
			}
			if (num == 2 || num == 3)
			{
				string[] array2 = ((num == 2) ? null : new string[0]);
				th.principal = new GenericPrincipal(new GenericIdentity("", ""), array2);
			}
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x000C8790 File Offset: 0x000C6990
		private static void SerializePrincipal(Thread th, IPrincipal value)
		{
			MemoryStream memoryStream = new MemoryStream();
			bool flag = false;
			if (value.GetType() == typeof(GenericPrincipal))
			{
				GenericPrincipal genericPrincipal = (GenericPrincipal)value;
				if (genericPrincipal.Identity != null && genericPrincipal.Identity.GetType() == typeof(GenericIdentity))
				{
					GenericIdentity genericIdentity = (GenericIdentity)genericPrincipal.Identity;
					if (genericIdentity.Name == "" && genericIdentity.AuthenticationType == "")
					{
						if (genericPrincipal.Roles == null)
						{
							memoryStream.WriteByte(2);
							flag = true;
						}
						else if (genericPrincipal.Roles.Length == 0)
						{
							memoryStream.WriteByte(3);
							flag = true;
						}
					}
					else
					{
						memoryStream.WriteByte(1);
						BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
						binaryWriter.Write(genericPrincipal.Identity.Name);
						binaryWriter.Write(genericPrincipal.Identity.AuthenticationType);
						string[] roles = genericPrincipal.Roles;
						if (roles == null)
						{
							binaryWriter.Write(-1);
						}
						else
						{
							binaryWriter.Write(roles.Length);
							foreach (string text in roles)
							{
								binaryWriter.Write(text);
							}
						}
						binaryWriter.Flush();
						flag = true;
					}
				}
			}
			if (!flag)
			{
				memoryStream.WriteByte(0);
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				try
				{
					binaryFormatter.Serialize(memoryStream, value);
				}
				catch
				{
				}
			}
			th.Internal._serialized_principal = Thread.ByteArrayToRootDomain(memoryStream.ToArray());
		}

		/// <summary>Gets or sets the thread's current principal (for role-based security).</summary>
		/// <returns>An <see cref="T:System.Security.Principal.IPrincipal" /> value representing the security context.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the permission required to set the principal. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x060036A7 RID: 13991 RVA: 0x000C891C File Offset: 0x000C6B1C
		// (set) Token: 0x060036A8 RID: 13992 RVA: 0x000C89B0 File Offset: 0x000C6BB0
		public static IPrincipal CurrentPrincipal
		{
			get
			{
				Thread currentThread = Thread.CurrentThread;
				if (currentThread.principal_version != currentThread.Internal._serialized_principal_version)
				{
					currentThread.principal = null;
				}
				if (currentThread.principal != null)
				{
					return currentThread.principal;
				}
				if (currentThread.Internal._serialized_principal != null)
				{
					try
					{
						Thread.DeserializePrincipal(currentThread);
						return currentThread.principal;
					}
					catch
					{
					}
				}
				currentThread.principal = Thread.GetDomain().DefaultPrincipal;
				currentThread.principal_version = currentThread.Internal._serialized_principal_version;
				return currentThread.principal;
			}
			[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
			set
			{
				Thread currentThread = Thread.CurrentThread;
				if (value != Thread.GetDomain().DefaultPrincipal)
				{
					currentThread.Internal._serialized_principal_version++;
					try
					{
						Thread.SerializePrincipal(currentThread, value);
					}
					catch (Exception)
					{
						currentThread.Internal._serialized_principal = null;
					}
					currentThread.principal_version = currentThread.Internal._serialized_principal_version;
				}
				else
				{
					currentThread.Internal._serialized_principal = null;
				}
				currentThread.principal = value;
			}
		}

		// Token: 0x060036A9 RID: 13993
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Thread GetCurrentThread();

		/// <summary>Gets the currently running thread.</summary>
		/// <returns>A <see cref="T:System.Threading.Thread" /> that is the representation of the currently running thread.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060036AA RID: 13994 RVA: 0x000C8A34 File Offset: 0x000C6C34
		public static Thread CurrentThread
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			get
			{
				Thread thread = Thread.current_thread;
				if (thread != null)
				{
					return thread;
				}
				return Thread.GetCurrentThread();
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x060036AB RID: 13995 RVA: 0x000C8A51 File Offset: 0x000C6C51
		internal static int CurrentThreadId
		{
			get
			{
				return (int)Thread.CurrentThread.internal_thread.thread_id;
			}
		}

		/// <summary>Returns the current domain in which the current thread is running.</summary>
		/// <returns>An <see cref="T:System.AppDomain" /> representing the current application domain of the running thread.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060036AC RID: 13996 RVA: 0x000C8A63 File Offset: 0x000C6C63
		public static AppDomain GetDomain()
		{
			return AppDomain.CurrentDomain;
		}

		/// <summary>Returns a unique application domain identifier.</summary>
		/// <returns>A 32-bit signed integer uniquely identifying the application domain.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060036AD RID: 13997
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetDomainID();

		// Token: 0x060036AE RID: 13998
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr Thread_internal(MulticastDelegate start);

		// Token: 0x060036AF RID: 13999 RVA: 0x000C8A6A File Offset: 0x000C6C6A
		private Thread(InternalThread it)
		{
			this.internal_thread = it;
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x000C8A7C File Offset: 0x000C6C7C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		~Thread()
		{
		}

		/// <summary>Gets or sets the apartment state of this thread.</summary>
		/// <returns>One of the <see cref="T:System.Threading.ApartmentState" /> values. The initial value is Unknown.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property to a state that is not a valid apartment state (a state other than single-threaded apartment (STA) or multithreaded apartment (MTA)). </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x060036B1 RID: 14001 RVA: 0x000C8AA4 File Offset: 0x000C6CA4
		// (set) Token: 0x060036B2 RID: 14002 RVA: 0x000C8AB8 File Offset: 0x000C6CB8
		[Obsolete("Deprecated in favor of GetApartmentState, SetApartmentState and TrySetApartmentState.")]
		public ApartmentState ApartmentState
		{
			get
			{
				this.ValidateThreadState();
				return (ApartmentState)this.Internal.apartment_state;
			}
			set
			{
				this.ValidateThreadState();
				this.TrySetApartmentState(value);
			}
		}

		/// <summary>Gets a value indicating whether or not a thread belongs to the managed thread pool.</summary>
		/// <returns>true if this thread belongs to the managed thread pool; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x060036B3 RID: 14003 RVA: 0x000C8AC9 File Offset: 0x000C6CC9
		public bool IsThreadPoolThread
		{
			get
			{
				return this.IsThreadPoolThreadInternal;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x060036B4 RID: 14004 RVA: 0x000C8AD1 File Offset: 0x000C6CD1
		// (set) Token: 0x060036B5 RID: 14005 RVA: 0x000C8ADE File Offset: 0x000C6CDE
		internal bool IsThreadPoolThreadInternal
		{
			get
			{
				return this.Internal.threadpool_thread;
			}
			set
			{
				this.Internal.threadpool_thread = value;
			}
		}

		/// <summary>Gets a value indicating the execution status of the current thread.</summary>
		/// <returns>true if this thread has been started and has not terminated normally or aborted; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x060036B6 RID: 14006 RVA: 0x000C8AEC File Offset: 0x000C6CEC
		public bool IsAlive
		{
			get
			{
				ThreadState state = Thread.GetState(this.Internal);
				return (state & ThreadState.Aborted) == ThreadState.Running && (state & ThreadState.Stopped) == ThreadState.Running && (state & ThreadState.Unstarted) == ThreadState.Running;
			}
		}

		/// <summary>Gets or sets a value indicating whether or not a thread is a background thread.</summary>
		/// <returns>true if this thread is or is to become a background thread; otherwise, false.</returns>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread is dead. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060036B7 RID: 14007 RVA: 0x000C8B1C File Offset: 0x000C6D1C
		// (set) Token: 0x060036B8 RID: 14008 RVA: 0x000C8B29 File Offset: 0x000C6D29
		public bool IsBackground
		{
			get
			{
				return (this.ValidateThreadState() & ThreadState.Background) > ThreadState.Running;
			}
			set
			{
				this.ValidateThreadState();
				if (value)
				{
					Thread.SetState(this.Internal, ThreadState.Background);
					return;
				}
				Thread.ClrState(this.Internal, ThreadState.Background);
			}
		}

		// Token: 0x060036B9 RID: 14009
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetName_internal(InternalThread thread);

		// Token: 0x060036BA RID: 14010
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetName_internal(InternalThread thread, string name);

		/// <summary>Gets or sets the name of the thread.</summary>
		/// <returns>A string containing the name of the thread, or null if no name was set.</returns>
		/// <exception cref="T:System.InvalidOperationException">A set operation was requested, and the Name property has already been set. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060036BB RID: 14011 RVA: 0x000C8B4E File Offset: 0x000C6D4E
		// (set) Token: 0x060036BC RID: 14012 RVA: 0x000C8B5B File Offset: 0x000C6D5B
		public string Name
		{
			get
			{
				return Thread.GetName_internal(this.Internal);
			}
			set
			{
				Thread.SetName_internal(this.Internal, value);
			}
		}

		/// <summary>Gets a value containing the states of the current thread.</summary>
		/// <returns>One of the <see cref="T:System.Threading.ThreadState" /> values indicating the state of the current thread. The initial value is Unstarted.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060036BD RID: 14013 RVA: 0x000C8B69 File Offset: 0x000C6D69
		public ThreadState ThreadState
		{
			get
			{
				return Thread.GetState(this.Internal);
			}
		}

		// Token: 0x060036BE RID: 14014
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Abort_internal(InternalThread thread, object stateInfo);

		/// <summary>Raises a <see cref="T:System.Threading.ThreadAbortException" /> in the thread on which it is invoked, to begin the process of terminating the thread. Calling this method usually terminates the thread.</summary>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread that is being aborted is currently suspended.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x060036BF RID: 14015 RVA: 0x000C8B76 File Offset: 0x000C6D76
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		public void Abort()
		{
			Thread.Abort_internal(this.Internal, null);
		}

		/// <summary>Raises a <see cref="T:System.Threading.ThreadAbortException" /> in the thread on which it is invoked, to begin the process of terminating the thread while also providing exception information about the thread termination. Calling this method usually terminates the thread.</summary>
		/// <param name="stateInfo">An object that contains application-specific information, such as state, which can be used by the thread being aborted. </param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread that is being aborted is currently suspended.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x060036C0 RID: 14016 RVA: 0x000C8B84 File Offset: 0x000C6D84
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		public void Abort(object stateInfo)
		{
			Thread.Abort_internal(this.Internal, stateInfo);
		}

		// Token: 0x060036C1 RID: 14017
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object GetAbortExceptionState();

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x060036C2 RID: 14018 RVA: 0x000C8B92 File Offset: 0x000C6D92
		internal object AbortReason
		{
			get
			{
				return this.GetAbortExceptionState();
			}
		}

		// Token: 0x060036C3 RID: 14019 RVA: 0x00002194 File Offset: 0x00000394
		private void ClearAbortReason()
		{
		}

		// Token: 0x060036C4 RID: 14020
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SpinWait_nop();

		/// <summary>Causes a thread to wait the number of times defined by the <paramref name="iterations" /> parameter.</summary>
		/// <param name="iterations">A 32-bit signed integer that defines how long a thread is to wait. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036C5 RID: 14021 RVA: 0x000C8B9A File Offset: 0x000C6D9A
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void SpinWait(int iterations)
		{
			if (iterations < 0)
			{
				return;
			}
			while (iterations-- > 0)
			{
				Thread.SpinWait_nop();
			}
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x000C8BB0 File Offset: 0x000C6DB0
		private void StartInternal(IPrincipal principal, ref StackCrawlMark stackMark)
		{
			this.Internal._serialized_principal = Thread.CurrentThread.Internal._serialized_principal;
			if (this.Thread_internal(this.m_Delegate) == IntPtr.Zero)
			{
				throw new SystemException("Thread creation failed.");
			}
			this.m_ThreadStartArg = null;
		}

		// Token: 0x060036C7 RID: 14023
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetState(InternalThread thread, ThreadState set);

		// Token: 0x060036C8 RID: 14024
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClrState(InternalThread thread, ThreadState clr);

		// Token: 0x060036C9 RID: 14025
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ThreadState GetState(InternalThread thread);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036CA RID: 14026
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte VolatileRead(ref byte address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036CB RID: 14027
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double VolatileRead(ref double address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036CC RID: 14028
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short VolatileRead(ref short address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036CD RID: 14029
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int VolatileRead(ref int address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036CE RID: 14030
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long VolatileRead(ref long address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036CF RID: 14031
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr VolatileRead(ref IntPtr address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036D0 RID: 14032
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object VolatileRead(ref object address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036D1 RID: 14033
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern sbyte VolatileRead(ref sbyte address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036D2 RID: 14034
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float VolatileRead(ref float address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036D3 RID: 14035
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ushort VolatileRead(ref ushort address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036D4 RID: 14036
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint VolatileRead(ref uint address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036D5 RID: 14037
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ulong VolatileRead(ref ulong address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036D6 RID: 14038
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern UIntPtr VolatileRead(ref UIntPtr address);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036D7 RID: 14039
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref byte address, byte value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036D8 RID: 14040
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref double address, double value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036D9 RID: 14041
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref short address, short value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036DA RID: 14042
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref int address, int value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036DB RID: 14043
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref long address, long value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036DC RID: 14044
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref IntPtr address, IntPtr value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036DD RID: 14045
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref object address, object value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036DE RID: 14046
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref sbyte address, sbyte value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036DF RID: 14047
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref float address, float value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036E0 RID: 14048
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref ushort address, ushort value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036E1 RID: 14049
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref uint address, uint value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036E2 RID: 14050
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref ulong address, ulong value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036E3 RID: 14051
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref UIntPtr address, UIntPtr value);

		// Token: 0x060036E4 RID: 14052
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SystemMaxStackStize();

		// Token: 0x060036E5 RID: 14053 RVA: 0x000C8C04 File Offset: 0x000C6E04
		private static int GetProcessDefaultStackSize(int maxStackSize)
		{
			if (maxStackSize == 0)
			{
				return 0;
			}
			if (maxStackSize < 131072)
			{
				return 131072;
			}
			int pageSize = Environment.GetPageSize();
			if (maxStackSize % pageSize != 0)
			{
				maxStackSize = maxStackSize / (pageSize - 1) * pageSize;
			}
			return Math.Min(maxStackSize, Thread.SystemMaxStackStize());
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x000C8C43 File Offset: 0x000C6E43
		private void SetStart(MulticastDelegate start, int maxStackSize)
		{
			this.m_Delegate = start;
			this.Internal.stack_size = maxStackSize;
		}

		/// <summary>Gets a unique identifier for the current managed thread.</summary>
		/// <returns>An integer that represents a unique identifier for this managed thread.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x060036E7 RID: 14055 RVA: 0x000C8C58 File Offset: 0x000C6E58
		public int ManagedThreadId
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.Internal.managed_id;
			}
		}

		/// <summary>Notifies a host that execution is about to enter a region of code in which the effects of a thread abort or unhandled exception might jeopardize other tasks in the application domain.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060036E8 RID: 14056 RVA: 0x000C8C65 File Offset: 0x000C6E65
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void BeginCriticalRegion()
		{
			Thread.CurrentThread.Internal.critical_region_level++;
		}

		/// <summary>Notifies a host that execution is about to enter a region of code in which the effects of a thread abort or unhandled exception are limited to the current task.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060036E9 RID: 14057 RVA: 0x000C8C82 File Offset: 0x000C6E82
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void EndCriticalRegion()
		{
			Thread.CurrentThread.Internal.critical_region_level--;
		}

		/// <summary>Notifies a host that managed code is about to execute instructions that depend on the identity of the current physical operating system thread.</summary>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x060036EA RID: 14058 RVA: 0x00002194 File Offset: 0x00000394
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void BeginThreadAffinity()
		{
		}

		/// <summary>Notifies a host that managed code has finished executing instructions that depend on the identity of the current physical operating system thread.</summary>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x060036EB RID: 14059 RVA: 0x00002194 File Offset: 0x00000394
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void EndThreadAffinity()
		{
		}

		/// <summary>Returns an <see cref="T:System.Threading.ApartmentState" /> value indicating the apartment state.</summary>
		/// <returns>One of the <see cref="T:System.Threading.ApartmentState" /> values indicating the apartment state of the managed thread. The default is <see cref="F:System.Threading.ApartmentState.Unknown" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036EC RID: 14060 RVA: 0x000C8AA4 File Offset: 0x000C6CA4
		public ApartmentState GetApartmentState()
		{
			this.ValidateThreadState();
			return (ApartmentState)this.Internal.apartment_state;
		}

		/// <summary>Sets the apartment state of a thread before it is started.</summary>
		/// <param name="state">The new apartment state.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid apartment state.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has already been started.</exception>
		/// <exception cref="T:System.InvalidOperationException">The apartment state has already been initialized.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036ED RID: 14061 RVA: 0x000C8C9F File Offset: 0x000C6E9F
		public void SetApartmentState(ApartmentState state)
		{
			if (!this.TrySetApartmentState(state))
			{
				throw new InvalidOperationException("Failed to set the specified COM apartment state.");
			}
		}

		/// <summary>Sets the apartment state of a thread before it is started.</summary>
		/// <returns>true if the apartment state is set; otherwise, false.</returns>
		/// <param name="state">The new apartment state.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid apartment state.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has already been started.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060036EE RID: 14062 RVA: 0x000C8CB8 File Offset: 0x000C6EB8
		public bool TrySetApartmentState(ApartmentState state)
		{
			if ((this.ThreadState & ThreadState.Unstarted) == ThreadState.Running)
			{
				throw new ThreadStateException("Thread was in an invalid state for the operation being executed.");
			}
			if (this.Internal.apartment_state != 2 && (ApartmentState)this.Internal.apartment_state != state)
			{
				return false;
			}
			this.Internal.apartment_state = (byte)state;
			return true;
		}

		/// <summary>Returns a hash code for the current thread.</summary>
		/// <returns>An integer hash code value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060036EF RID: 14063 RVA: 0x000C8D06 File Offset: 0x000C6F06
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return this.ManagedThreadId;
		}

		// Token: 0x060036F0 RID: 14064
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetStackTraces(out Thread[] threads, out object[] stack_frames);

		// Token: 0x060036F1 RID: 14065 RVA: 0x000C8D10 File Offset: 0x000C6F10
		internal static Dictionary<Thread, StackTrace> Mono_GetStackTraces()
		{
			Thread[] array;
			object[] array2;
			Thread.GetStackTraces(out array, out array2);
			Dictionary<Thread, StackTrace> dictionary = new Dictionary<Thread, StackTrace>();
			for (int i = 0; i < array.Length; i++)
			{
				dictionary[array[i]] = new StackTrace((StackFrame[])array2[i]);
			}
			return dictionary;
		}

		/// <summary>Turns off automatic cleanup of runtime callable wrappers (RCW) for the current thread. </summary>
		// Token: 0x060036F2 RID: 14066 RVA: 0x000C8D51 File Offset: 0x000C6F51
		public void DisableComObjectEagerCleanup()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x000C8D58 File Offset: 0x000C6F58
		private ThreadState ValidateThreadState()
		{
			ThreadState state = Thread.GetState(this.Internal);
			if ((state & ThreadState.Stopped) != ThreadState.Running)
			{
				throw new ThreadStateException("Thread is dead; state can not be accessed.");
			}
			return state;
		}

		// Token: 0x04001CD2 RID: 7378
		private static LocalDataStoreMgr s_LocalDataStoreMgr;

		// Token: 0x04001CD3 RID: 7379
		[ThreadStatic]
		private static LocalDataStoreHolder s_LocalDataStore;

		// Token: 0x04001CD4 RID: 7380
		[ThreadStatic]
		internal static CultureInfo m_CurrentCulture;

		// Token: 0x04001CD5 RID: 7381
		[ThreadStatic]
		internal static CultureInfo m_CurrentUICulture;

		// Token: 0x04001CD6 RID: 7382
		private static AsyncLocal<CultureInfo> s_asyncLocalCurrentCulture;

		// Token: 0x04001CD7 RID: 7383
		private static AsyncLocal<CultureInfo> s_asyncLocalCurrentUICulture;

		// Token: 0x04001CD8 RID: 7384
		private InternalThread internal_thread;

		// Token: 0x04001CD9 RID: 7385
		private object m_ThreadStartArg;

		// Token: 0x04001CDA RID: 7386
		private object pending_exception;

		// Token: 0x04001CDB RID: 7387
		private IPrincipal principal;

		// Token: 0x04001CDC RID: 7388
		private int principal_version;

		// Token: 0x04001CDD RID: 7389
		[ThreadStatic]
		private static Thread current_thread;

		// Token: 0x04001CDE RID: 7390
		private MulticastDelegate m_Delegate;

		// Token: 0x04001CDF RID: 7391
		private ExecutionContext m_ExecutionContext;

		// Token: 0x04001CE0 RID: 7392
		private bool m_ExecutionContextBelongsToOuterScope;
	}
}
