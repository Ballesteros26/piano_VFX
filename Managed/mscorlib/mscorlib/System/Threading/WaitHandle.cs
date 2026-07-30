using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Contexts;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	/// <summary>Encapsulates operating system–specific objects that wait for exclusive access to shared resources.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200049D RID: 1181
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class WaitHandle : MarshalByRefObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.WaitHandle" /> class.</summary>
		// Token: 0x06003775 RID: 14197 RVA: 0x000CA3C5 File Offset: 0x000C85C5
		protected WaitHandle()
		{
			this.Init();
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x000CA3D3 File Offset: 0x000C85D3
		[SecuritySafeCritical]
		private void Init()
		{
			this.safeWaitHandle = null;
			this.waitHandle = WaitHandle.InvalidHandle;
			this.hasThreadAffinity = false;
		}

		/// <summary>Gets or sets the native operating system handle.</summary>
		/// <returns>An IntPtr representing the native operating system handle. The default is the value of the <see cref="F:System.Threading.WaitHandle.InvalidHandle" /> field.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06003777 RID: 14199 RVA: 0x000CA3F0 File Offset: 0x000C85F0
		// (set) Token: 0x06003778 RID: 14200 RVA: 0x000CA410 File Offset: 0x000C8610
		[Obsolete("Use the SafeWaitHandle property instead.")]
		public virtual IntPtr Handle
		{
			[SecuritySafeCritical]
			get
			{
				if (this.safeWaitHandle != null)
				{
					return this.safeWaitHandle.DangerousGetHandle();
				}
				return WaitHandle.InvalidHandle;
			}
			[SecurityCritical]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				if (value == WaitHandle.InvalidHandle)
				{
					if (this.safeWaitHandle != null)
					{
						this.safeWaitHandle.SetHandleAsInvalid();
						this.safeWaitHandle = null;
					}
				}
				else
				{
					this.safeWaitHandle = new SafeWaitHandle(value, true);
				}
				this.waitHandle = value;
			}
		}

		/// <summary>Gets or sets the native operating system handle.</summary>
		/// <returns>A <see cref="T:Microsoft.Win32.SafeHandles.SafeWaitHandle" /> representing the native operating system handle.</returns>
		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06003779 RID: 14201 RVA: 0x000CA462 File Offset: 0x000C8662
		// (set) Token: 0x0600377A RID: 14202 RVA: 0x000CA48C File Offset: 0x000C868C
		public SafeWaitHandle SafeWaitHandle
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[SecurityCritical]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				if (this.safeWaitHandle == null)
				{
					this.safeWaitHandle = new SafeWaitHandle(WaitHandle.InvalidHandle, false);
				}
				return this.safeWaitHandle;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[SecurityCritical]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					if (value == null)
					{
						this.safeWaitHandle = null;
						this.waitHandle = WaitHandle.InvalidHandle;
					}
					else
					{
						this.safeWaitHandle = value;
						this.waitHandle = this.safeWaitHandle.DangerousGetHandle();
					}
				}
			}
		}

		// Token: 0x0600377B RID: 14203 RVA: 0x000CA4E8 File Offset: 0x000C86E8
		[SecurityCritical]
		internal void SetHandleInternal(SafeWaitHandle handle)
		{
			this.safeWaitHandle = handle;
			this.waitHandle = handle.DangerousGetHandle();
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.WaitHandle" /> receives a signal, using a 32-bit signed integer to specify the time interval and specifying whether to exit the synchronization domain before the wait.</summary>
		/// <returns>true if the current instance receives a signal; otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely. </param>
		/// <param name="exitContext">true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, false. </param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600377C RID: 14204 RVA: 0x000CA4FF File Offset: 0x000C86FF
		public virtual bool WaitOne(int millisecondsTimeout, bool exitContext)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return this.WaitOne((long)millisecondsTimeout, exitContext);
		}

		/// <summary>Blocks the current thread until the current instance receives a signal, using a <see cref="T:System.TimeSpan" /> to specify the time interval and specifying whether to exit the synchronization domain before the wait.</summary>
		/// <returns>true if the current instance receives a signal; otherwise, false.</returns>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely. </param>
		/// <param name="exitContext">true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, false. </param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out.-or-<paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600377D RID: 14205 RVA: 0x000CA524 File Offset: 0x000C8724
		public virtual bool WaitOne(TimeSpan timeout, bool exitContext)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (-1L > num || 2147483647L < num)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return this.WaitOne(num, exitContext);
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.WaitHandle" /> receives a signal.</summary>
		/// <returns>true if the current instance receives a signal. If the current instance is never signaled, <see cref="M:System.Threading.WaitHandle.WaitOne(System.Int32,System.Boolean)" /> never returns.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600377E RID: 14206 RVA: 0x000CA565 File Offset: 0x000C8765
		public virtual bool WaitOne()
		{
			return this.WaitOne(-1, false);
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.WaitHandle" /> receives a signal, using a 32-bit signed integer to specify the time interval.</summary>
		/// <returns>true if the current instance receives a signal; otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely. </param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x0600377F RID: 14207 RVA: 0x000CA56F File Offset: 0x000C876F
		public virtual bool WaitOne(int millisecondsTimeout)
		{
			return this.WaitOne(millisecondsTimeout, false);
		}

		/// <summary>Blocks the current thread until the current instance receives a signal, using a <see cref="T:System.TimeSpan" /> to specify the time interval.</summary>
		/// <returns>true if the current instance receives a signal; otherwise, false.</returns>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely. </param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out.-or-<paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x06003780 RID: 14208 RVA: 0x000CA579 File Offset: 0x000C8779
		public virtual bool WaitOne(TimeSpan timeout)
		{
			return this.WaitOne(timeout, false);
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x000CA583 File Offset: 0x000C8783
		[SecuritySafeCritical]
		private bool WaitOne(long timeout, bool exitContext)
		{
			return WaitHandle.InternalWaitOne(this.safeWaitHandle, timeout, this.hasThreadAffinity, exitContext);
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x000CA59C File Offset: 0x000C879C
		[SecurityCritical]
		internal static bool InternalWaitOne(SafeHandle waitableSafeHandle, long millisecondsTimeout, bool hasThreadAffinity, bool exitContext)
		{
			if (waitableSafeHandle == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a disposed object."));
			}
			int num = WaitHandle.WaitOneNative(waitableSafeHandle, (uint)millisecondsTimeout, hasThreadAffinity, exitContext);
			if (num == 128)
			{
				WaitHandle.ThrowAbandonedMutexException();
			}
			return num != 258 && num != int.MaxValue;
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x000CA5EC File Offset: 0x000C87EC
		[SecurityCritical]
		internal bool WaitOneWithoutFAS()
		{
			if (this.safeWaitHandle == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a disposed object."));
			}
			long num = -1L;
			int num2 = WaitHandle.WaitOneNative(this.safeWaitHandle, (uint)num, this.hasThreadAffinity, false);
			if (num2 == 128)
			{
				WaitHandle.ThrowAbandonedMutexException();
			}
			return num2 != 258 && num2 != int.MaxValue;
		}

		/// <summary>Waits for all the elements in the specified array to receive a signal, using an <see cref="T:System.Int32" /> value to specify the time interval and specifying whether to exit the synchronization domain before the wait.</summary>
		/// <returns>true when every element in <paramref name="waitHandles" /> has received a signal; otherwise, false.</returns>
		/// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait. This array cannot contain multiple references to the same object (duplicates). </param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely. </param>
		/// <param name="exitContext">true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null.-or- One or more of the objects in the <paramref name="waitHandles" /> array is null. -or-<paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 2.0 or later. </exception>
		/// <exception cref="T:System.DuplicateWaitObjectException">The <paramref name="waitHandles" /> array contains elements that are duplicates. </exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.-or- The <see cref="T:System.STAThreadAttribute" /> attribute is applied to the thread procedure for the current thread, and <paramref name="waitHandles" /> contains more than one element. </exception>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 1.0 or 1.1. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003784 RID: 14212 RVA: 0x000CA650 File Offset: 0x000C8850
		[SecuritySafeCritical]
		public static bool WaitAll(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext)
		{
			if (waitHandles == null)
			{
				throw new ArgumentNullException(Environment.GetResourceString("The waitHandles parameter cannot be null."));
			}
			if (waitHandles.Length == 0)
			{
				throw new ArgumentNullException(Environment.GetResourceString("Waithandle array may not be empty."));
			}
			if (waitHandles.Length > 64)
			{
				throw new NotSupportedException(Environment.GetResourceString("The number of WaitHandles must be less than or equal to 64."));
			}
			if (-1 > millisecondsTimeout)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			WaitHandle[] array = new WaitHandle[waitHandles.Length];
			for (int i = 0; i < waitHandles.Length; i++)
			{
				WaitHandle waitHandle = waitHandles[i];
				if (waitHandle == null)
				{
					throw new ArgumentNullException(Environment.GetResourceString("At least one element in the specified array was null."));
				}
				if (RemotingServices.IsTransparentProxy(waitHandle))
				{
					throw new InvalidOperationException(Environment.GetResourceString("Cannot wait on a transparent proxy."));
				}
				array[i] = waitHandle;
			}
			int num = WaitHandle.WaitMultiple(array, millisecondsTimeout, exitContext, true);
			if (128 <= num && 128 + array.Length > num)
			{
				WaitHandle.ThrowAbandonedMutexException();
			}
			GC.KeepAlive(array);
			return num != 258 && num != int.MaxValue;
		}

		/// <summary>Waits for all the elements in the specified array to receive a signal, using a <see cref="T:System.TimeSpan" /> value to specify the time interval, and specifying whether to exit the synchronization domain before the wait.</summary>
		/// <returns>true when every element in <paramref name="waitHandles" /> has received a signal; otherwise false.</returns>
		/// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait. This array cannot contain multiple references to the same object. </param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds, to wait indefinitely. </param>
		/// <param name="exitContext">true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null. -or- One or more of the objects in the <paramref name="waitHandles" /> array is null. -or-<paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 2.0 or later. </exception>
		/// <exception cref="T:System.DuplicateWaitObjectException">The <paramref name="waitHandles" /> array contains elements that are duplicates. </exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.-or- The <see cref="T:System.STAThreadAttribute" /> attribute is applied to the thread procedure for the current thread, and <paramref name="waitHandles" /> contains more than one element. </exception>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 1.0 or 1.1. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out. -or-<paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait terminated because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003785 RID: 14213 RVA: 0x000CA73C File Offset: 0x000C893C
		public static bool WaitAll(WaitHandle[] waitHandles, TimeSpan timeout, bool exitContext)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (-1L > num || 2147483647L < num)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return WaitHandle.WaitAll(waitHandles, (int)num, exitContext);
		}

		/// <summary>Waits for all the elements in the specified array to receive a signal.</summary>
		/// <returns>true when every element in <paramref name="waitHandles" /> has received a signal; otherwise the method never returns.</returns>
		/// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait. This array cannot contain multiple references to the same object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null. -or- One or more of the objects in the <paramref name="waitHandles" /> array are null. -or-<paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 2.0 or later.</exception>
		/// <exception cref="T:System.DuplicateWaitObjectException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.ArgumentException" />, instead.The <paramref name="waitHandles" /> array contains elements that are duplicates. </exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.-or- The <see cref="T:System.STAThreadAttribute" /> attribute is applied to the thread procedure for the current thread, and <paramref name="waitHandles" /> contains more than one element. </exception>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 1.0 or 1.1. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait terminated because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003786 RID: 14214 RVA: 0x000CA77E File Offset: 0x000C897E
		public static bool WaitAll(WaitHandle[] waitHandles)
		{
			return WaitHandle.WaitAll(waitHandles, -1, true);
		}

		/// <summary>Waits for all the elements in the specified array to receive a signal, using an <see cref="T:System.Int32" /> value to specify the time interval.</summary>
		/// <returns>true when every element in <paramref name="waitHandles" /> has received a signal; otherwise, false.</returns>
		/// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait. This array cannot contain multiple references to the same object (duplicates). </param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null.-or- One or more of the objects in the <paramref name="waitHandles" /> array is null. -or-<paramref name="waitHandles" /> is an array with no elements. </exception>
		/// <exception cref="T:System.DuplicateWaitObjectException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.ArgumentException" />, instead.The <paramref name="waitHandles" /> array contains elements that are duplicates. </exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.-or- The <see cref="T:System.STAThreadAttribute" /> attribute is applied to the thread procedure for the current thread, and <paramref name="waitHandles" /> contains more than one element. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x06003787 RID: 14215 RVA: 0x000CA788 File Offset: 0x000C8988
		public static bool WaitAll(WaitHandle[] waitHandles, int millisecondsTimeout)
		{
			return WaitHandle.WaitAll(waitHandles, millisecondsTimeout, true);
		}

		/// <summary>Waits for all the elements in the specified array to receive a signal, using a <see cref="T:System.TimeSpan" /> value to specify the time interval.</summary>
		/// <returns>true when every element in <paramref name="waitHandles" /> has received a signal; otherwise, false.</returns>
		/// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait. This array cannot contain multiple references to the same object. </param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds, to wait indefinitely. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null. -or- One or more of the objects in the <paramref name="waitHandles" /> array is null. -or-<paramref name="waitHandles" /> is an array with no elements. </exception>
		/// <exception cref="T:System.DuplicateWaitObjectException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.ArgumentException" />, instead.The <paramref name="waitHandles" /> array contains elements that are duplicates. </exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.-or- The <see cref="T:System.STAThreadAttribute" /> attribute is applied to the thread procedure for the current thread, and <paramref name="waitHandles" /> contains more than one element. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out. -or-<paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait terminated because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x06003788 RID: 14216 RVA: 0x000CA792 File Offset: 0x000C8992
		public static bool WaitAll(WaitHandle[] waitHandles, TimeSpan timeout)
		{
			return WaitHandle.WaitAll(waitHandles, timeout, true);
		}

		/// <summary>Waits for any of the elements in the specified array to receive a signal, using a 32-bit signed integer to specify the time interval, and specifying whether to exit the synchronization domain before the wait.</summary>
		/// <returns>The array index of the object that satisfied the wait, or <see cref="F:System.Threading.WaitHandle.WaitTimeout" /> if no object satisfied the wait and a time interval equivalent to <paramref name="millisecondsTimeout" /> has passed.</returns>
		/// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait. </param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely. </param>
		/// <param name="exitContext">true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null.-or-One or more of the objects in the <paramref name="waitHandles" /> array is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits. </exception>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 1.0 or 1.1. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 2.0 or later. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003789 RID: 14217 RVA: 0x000CA79C File Offset: 0x000C899C
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int WaitAny(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext)
		{
			if (waitHandles == null)
			{
				throw new ArgumentNullException(Environment.GetResourceString("The waitHandles parameter cannot be null."));
			}
			if (waitHandles.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Waithandle array may not be empty."));
			}
			if (64 < waitHandles.Length)
			{
				throw new NotSupportedException(Environment.GetResourceString("The number of WaitHandles must be less than or equal to 64."));
			}
			if (-1 > millisecondsTimeout)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			WaitHandle[] array = new WaitHandle[waitHandles.Length];
			for (int i = 0; i < waitHandles.Length; i++)
			{
				WaitHandle waitHandle = waitHandles[i];
				if (waitHandle == null)
				{
					throw new ArgumentNullException(Environment.GetResourceString("At least one element in the specified array was null."));
				}
				if (RemotingServices.IsTransparentProxy(waitHandle))
				{
					throw new InvalidOperationException(Environment.GetResourceString("Cannot wait on a transparent proxy."));
				}
				array[i] = waitHandle;
			}
			int num = WaitHandle.WaitMultiple(array, millisecondsTimeout, exitContext, false);
			if (128 <= num && 128 + array.Length > num)
			{
				int num2 = num - 128;
				if (0 <= num2 && num2 < array.Length)
				{
					WaitHandle.ThrowAbandonedMutexException(num2, array[num2]);
				}
				else
				{
					WaitHandle.ThrowAbandonedMutexException();
				}
			}
			GC.KeepAlive(array);
			return num;
		}

		/// <summary>Waits for any of the elements in the specified array to receive a signal, using a <see cref="T:System.TimeSpan" /> to specify the time interval and specifying whether to exit the synchronization domain before the wait.</summary>
		/// <returns>The array index of the object that satisfied the wait, or <see cref="F:System.Threading.WaitHandle.WaitTimeout" /> if no object satisfied the wait and a time interval equivalent to <paramref name="timeout" /> has passed.</returns>
		/// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait. </param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely. </param>
		/// <param name="exitContext">true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null.-or-One or more of the objects in the <paramref name="waitHandles" /> array is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits. </exception>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 1.0 or 1.1. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out. -or-<paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 2.0 or later. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600378A RID: 14218 RVA: 0x000CA898 File Offset: 0x000C8A98
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int WaitAny(WaitHandle[] waitHandles, TimeSpan timeout, bool exitContext)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (-1L > num || 2147483647L < num)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return WaitHandle.WaitAny(waitHandles, (int)num, exitContext);
		}

		/// <summary>Waits for any of the elements in the specified array to receive a signal, using a <see cref="T:System.TimeSpan" /> to specify the time interval.</summary>
		/// <returns>The array index of the object that satisfied the wait, or <see cref="F:System.Threading.WaitHandle.WaitTimeout" /> if no object satisfied the wait and a time interval equivalent to <paramref name="timeout" /> has passed.</returns>
		/// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait. </param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null.-or-One or more of the objects in the <paramref name="waitHandles" /> array is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out. -or-<paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="waitHandles" /> is an array with no elements. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x0600378B RID: 14219 RVA: 0x000CA8DA File Offset: 0x000C8ADA
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int WaitAny(WaitHandle[] waitHandles, TimeSpan timeout)
		{
			return WaitHandle.WaitAny(waitHandles, timeout, true);
		}

		/// <summary>Waits for any of the elements in the specified array to receive a signal.</summary>
		/// <returns>The array index of the object that satisfied the wait.</returns>
		/// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null.-or-One or more of the objects in the <paramref name="waitHandles" /> array is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits. </exception>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 1.0 or 1.1. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 2.0 or later. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600378C RID: 14220 RVA: 0x000CA8E4 File Offset: 0x000C8AE4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int WaitAny(WaitHandle[] waitHandles)
		{
			return WaitHandle.WaitAny(waitHandles, -1, true);
		}

		/// <summary>Waits for any of the elements in the specified array to receive a signal, using a 32-bit signed integer to specify the time interval.</summary>
		/// <returns>The array index of the object that satisfied the wait, or <see cref="F:System.Threading.WaitHandle.WaitTimeout" /> if no object satisfied the wait and a time interval equivalent to <paramref name="millisecondsTimeout" /> has passed.</returns>
		/// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait. </param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null.-or-One or more of the objects in the <paramref name="waitHandles" /> array is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="waitHandles" /> is an array with no elements. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x0600378D RID: 14221 RVA: 0x000CA8EE File Offset: 0x000C8AEE
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int WaitAny(WaitHandle[] waitHandles, int millisecondsTimeout)
		{
			return WaitHandle.WaitAny(waitHandles, millisecondsTimeout, true);
		}

		/// <summary>Signals one <see cref="T:System.Threading.WaitHandle" /> and waits on another.</summary>
		/// <returns>true if both the signal and the wait complete successfully; if the wait does not complete, the method does not return.</returns>
		/// <param name="toSignal">The <see cref="T:System.Threading.WaitHandle" /> to signal.</param>
		/// <param name="toWaitOn">The <see cref="T:System.Threading.WaitHandle" /> to wait on.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="toSignal" /> is null.-or-<paramref name="toWaitOn" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The method was called on a thread that has <see cref="T:System.STAThreadAttribute" />. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">This method is not supported on Windows 98 or Windows Millennium Edition. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="toSignal" /> is a semaphore, and it already has a full count. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600378E RID: 14222 RVA: 0x000CA8F8 File Offset: 0x000C8AF8
		public static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn)
		{
			return WaitHandle.SignalAndWait(toSignal, toWaitOn, -1, false);
		}

		/// <summary>Signals one <see cref="T:System.Threading.WaitHandle" /> and waits on another, specifying the time-out interval as a <see cref="T:System.TimeSpan" /> and specifying whether to exit the synchronization domain for the context before entering the wait.</summary>
		/// <returns>true if both the signal and the wait completed successfully, or false if the signal completed but the wait timed out.</returns>
		/// <param name="toSignal">The <see cref="T:System.Threading.WaitHandle" /> to signal.</param>
		/// <param name="toWaitOn">The <see cref="T:System.Threading.WaitHandle" /> to wait on.</param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the interval to wait. If the value is -1, the wait is infinite.</param>
		/// <param name="exitContext">true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="toSignal" /> is null.-or-<paramref name="toWaitOn" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The method was called on a thread that has <see cref="T:System.STAThreadAttribute" />. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">This method is not supported on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="toSignal" /> is a semaphore, and it already has a full count. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> evaluates to a negative number of milliseconds other than -1. -or-<paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600378F RID: 14223 RVA: 0x000CA904 File Offset: 0x000C8B04
		public static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn, TimeSpan timeout, bool exitContext)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (-1L > num || 2147483647L < num)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return WaitHandle.SignalAndWait(toSignal, toWaitOn, (int)num, exitContext);
		}

		/// <summary>Signals one <see cref="T:System.Threading.WaitHandle" /> and waits on another, specifying a time-out interval as a 32-bit signed integer and specifying whether to exit the synchronization domain for the context before entering the wait.</summary>
		/// <returns>true if both the signal and the wait completed successfully, or false if the signal completed but the wait timed out.</returns>
		/// <param name="toSignal">The <see cref="T:System.Threading.WaitHandle" /> to signal.</param>
		/// <param name="toWaitOn">The <see cref="T:System.Threading.WaitHandle" /> to wait on.</param>
		/// <param name="millisecondsTimeout">An integer that represents the interval to wait. If the value is <see cref="F:System.Threading.Timeout.Infinite" />, that is, -1, the wait is infinite.</param>
		/// <param name="exitContext">true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="toSignal" /> is null.-or-<paramref name="toWaitOn" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The method is called on a thread that has <see cref="T:System.STAThreadAttribute" />. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">This method is not supported on Windows 98 or Windows Millennium Edition. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="toSignal" /> is a semaphore, and it already has a full count. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Threading.WaitHandle" /> cannot be signaled because it would exceed its maximum count.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003790 RID: 14224 RVA: 0x000CA948 File Offset: 0x000C8B48
		[SecuritySafeCritical]
		public static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn, int millisecondsTimeout, bool exitContext)
		{
			if (toSignal == null)
			{
				throw new ArgumentNullException("toSignal");
			}
			if (toWaitOn == null)
			{
				throw new ArgumentNullException("toWaitOn");
			}
			if (-1 > millisecondsTimeout)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			int num = WaitHandle.SignalAndWaitOne(toSignal.safeWaitHandle, toWaitOn.safeWaitHandle, millisecondsTimeout, toWaitOn.hasThreadAffinity, exitContext);
			if (2147483647 != num && toSignal.hasThreadAffinity)
			{
				Thread.EndCriticalRegion();
				Thread.EndThreadAffinity();
			}
			if (128 == num)
			{
				WaitHandle.ThrowAbandonedMutexException();
			}
			if (298 == num)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The WaitHandle cannot be signaled because it would exceed its maximum count."));
			}
			return num == 0;
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x000CA9ED File Offset: 0x000C8BED
		private static void ThrowAbandonedMutexException()
		{
			throw new AbandonedMutexException();
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x000CA9F4 File Offset: 0x000C8BF4
		private static void ThrowAbandonedMutexException(int location, WaitHandle handle)
		{
			throw new AbandonedMutexException(location, handle);
		}

		/// <summary>When overridden in a derived class, releases all resources held by the current <see cref="T:System.Threading.WaitHandle" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003793 RID: 14227 RVA: 0x000CA9FD File Offset: 0x000C8BFD
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>When overridden in a derived class, releases the unmanaged resources used by the <see cref="T:System.Threading.WaitHandle" />, and optionally releases the managed resources.</summary>
		/// <param name="explicitDisposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003794 RID: 14228 RVA: 0x000CAA0C File Offset: 0x000C8C0C
		[SecuritySafeCritical]
		protected virtual void Dispose(bool explicitDisposing)
		{
			if (this.safeWaitHandle != null)
			{
				this.safeWaitHandle.Close();
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.WaitHandle" /> class.</summary>
		// Token: 0x06003795 RID: 14229 RVA: 0x000CA9FD File Offset: 0x000C8BFD
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x000CAA28 File Offset: 0x000C8C28
		private unsafe static int WaitMultiple(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext, bool WaitAll)
		{
			if (waitHandles.Length > 64)
			{
				return int.MaxValue;
			}
			int num = -1;
			int num2;
			try
			{
				if (exitContext)
				{
					SynchronizationAttribute.ExitContext();
				}
				for (int i = 0; i < waitHandles.Length; i++)
				{
					try
					{
					}
					finally
					{
						bool flag = false;
						waitHandles[i].SafeWaitHandle.DangerousAddRef(ref flag);
						num = i;
					}
				}
				IntPtr* ptr;
				checked
				{
					ptr = stackalloc IntPtr[unchecked((UIntPtr)waitHandles.Length) * (UIntPtr)sizeof(IntPtr)];
				}
				for (int j = 0; j < waitHandles.Length; j++)
				{
					ptr[j] = waitHandles[j].SafeWaitHandle.DangerousGetHandle();
				}
				num2 = WaitHandle.Wait_internal(ptr, waitHandles.Length, WaitAll, millisecondsTimeout);
			}
			finally
			{
				for (int k = num; k >= 0; k--)
				{
					waitHandles[k].SafeWaitHandle.DangerousRelease();
				}
				if (exitContext)
				{
					SynchronizationAttribute.EnterContext();
				}
			}
			return num2;
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x000CAB00 File Offset: 0x000C8D00
		private unsafe static int WaitOneNative(SafeHandle waitableSafeHandle, uint millisecondsTimeout, bool hasThreadAffinity, bool exitContext)
		{
			bool flag = false;
			int num;
			try
			{
				if (exitContext)
				{
					SynchronizationAttribute.ExitContext();
				}
				waitableSafeHandle.DangerousAddRef(ref flag);
				IntPtr intPtr = waitableSafeHandle.DangerousGetHandle();
				num = WaitHandle.Wait_internal(&intPtr, 1, false, (int)millisecondsTimeout);
			}
			finally
			{
				if (flag)
				{
					waitableSafeHandle.DangerousRelease();
				}
				if (exitContext)
				{
					SynchronizationAttribute.EnterContext();
				}
			}
			return num;
		}

		// Token: 0x06003798 RID: 14232
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int Wait_internal(IntPtr* handles, int numHandles, bool waitAll, int ms);

		// Token: 0x06003799 RID: 14233 RVA: 0x000CAB58 File Offset: 0x000C8D58
		private static int SignalAndWaitOne(SafeWaitHandle waitHandleToSignal, SafeWaitHandle waitHandleToWaitOn, int millisecondsTimeout, bool hasThreadAffinity, bool exitContext)
		{
			bool flag = false;
			bool flag2 = false;
			int num;
			try
			{
				waitHandleToSignal.DangerousAddRef(ref flag);
				waitHandleToWaitOn.DangerousAddRef(ref flag2);
				num = WaitHandle.SignalAndWait_Internal(waitHandleToSignal.DangerousGetHandle(), waitHandleToWaitOn.DangerousGetHandle(), millisecondsTimeout);
			}
			finally
			{
				if (flag)
				{
					waitHandleToSignal.DangerousRelease();
				}
				if (flag2)
				{
					waitHandleToWaitOn.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x0600379A RID: 14234
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SignalAndWait_Internal(IntPtr toSignal, IntPtr toWaitOn, int ms);

		/// <summary>Indicates that a <see cref="M:System.Threading.WaitHandle.WaitAny(System.Threading.WaitHandle[],System.Int32,System.Boolean)" /> operation timed out before any of the wait handles were signaled. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04001D2C RID: 7468
		public const int WaitTimeout = 258;

		// Token: 0x04001D2D RID: 7469
		private const int MAX_WAITHANDLES = 64;

		// Token: 0x04001D2E RID: 7470
		private IntPtr waitHandle;

		// Token: 0x04001D2F RID: 7471
		[SecurityCritical]
		internal volatile SafeWaitHandle safeWaitHandle;

		// Token: 0x04001D30 RID: 7472
		internal bool hasThreadAffinity;

		// Token: 0x04001D31 RID: 7473
		private const int WAIT_OBJECT_0 = 0;

		// Token: 0x04001D32 RID: 7474
		private const int WAIT_ABANDONED = 128;

		// Token: 0x04001D33 RID: 7475
		private const int WAIT_FAILED = 2147483647;

		// Token: 0x04001D34 RID: 7476
		private const int ERROR_TOO_MANY_POSTS = 298;

		/// <summary>Represents an invalid native operating system handle. This field is read-only.</summary>
		// Token: 0x04001D35 RID: 7477
		protected static readonly IntPtr InvalidHandle = (IntPtr)(-1);

		// Token: 0x04001D36 RID: 7478
		internal const int MaxWaitHandles = 64;

		// Token: 0x0200049E RID: 1182
		internal enum OpenExistingResult
		{
			// Token: 0x04001D38 RID: 7480
			Success,
			// Token: 0x04001D39 RID: 7481
			NameNotFound,
			// Token: 0x04001D3A RID: 7482
			PathNotFound,
			// Token: 0x04001D3B RID: 7483
			NameInvalid
		}
	}
}
