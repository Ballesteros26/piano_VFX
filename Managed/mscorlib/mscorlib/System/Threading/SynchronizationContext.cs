using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Provides the basic functionality for propagating a synchronization context in various synchronization models. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200047F RID: 1151
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)]
	public class SynchronizationContext
	{
		/// <summary>Sets notification that wait notification is required and prepares the callback method so it can be called more reliably when a wait occurs.</summary>
		// Token: 0x06003645 RID: 13893 RVA: 0x000C7E58 File Offset: 0x000C6058
		[SecuritySafeCritical]
		protected void SetWaitNotificationRequired()
		{
			Type type = base.GetType();
			if (SynchronizationContext.s_cachedPreparedType1 != type && SynchronizationContext.s_cachedPreparedType2 != type && SynchronizationContext.s_cachedPreparedType3 != type && SynchronizationContext.s_cachedPreparedType4 != type && SynchronizationContext.s_cachedPreparedType5 != type)
			{
				RuntimeHelpers.PrepareDelegate(new SynchronizationContext.WaitDelegate(this.Wait));
				if (SynchronizationContext.s_cachedPreparedType1 == null)
				{
					SynchronizationContext.s_cachedPreparedType1 = type;
				}
				else if (SynchronizationContext.s_cachedPreparedType2 == null)
				{
					SynchronizationContext.s_cachedPreparedType2 = type;
				}
				else if (SynchronizationContext.s_cachedPreparedType3 == null)
				{
					SynchronizationContext.s_cachedPreparedType3 = type;
				}
				else if (SynchronizationContext.s_cachedPreparedType4 == null)
				{
					SynchronizationContext.s_cachedPreparedType4 = type;
				}
				else if (SynchronizationContext.s_cachedPreparedType5 == null)
				{
					SynchronizationContext.s_cachedPreparedType5 = type;
				}
			}
			this._props |= SynchronizationContextProperties.RequireWaitNotification;
		}

		/// <summary>Determines if wait notification is required.</summary>
		/// <returns>true if wait notification is required; otherwise, false. </returns>
		// Token: 0x06003646 RID: 13894 RVA: 0x000C7F40 File Offset: 0x000C6140
		public bool IsWaitNotificationRequired()
		{
			return (this._props & SynchronizationContextProperties.RequireWaitNotification) > SynchronizationContextProperties.None;
		}

		/// <summary>When overridden in a derived class, dispatches a synchronous message to a synchronization context.</summary>
		/// <param name="d">The <see cref="T:System.Threading.SendOrPostCallback" /> delegate to call.</param>
		/// <param name="state">The object passed to the delegate. </param>
		/// <exception cref="T:System.NotSupportedException">The method was called in a Windows Store app. The implementation of <see cref="T:System.Threading.SynchronizationContext" /> for Windows Store apps does not support the <see cref="M:System.Threading.SynchronizationContext.Send(System.Threading.SendOrPostCallback,System.Object)" /> method. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003647 RID: 13895 RVA: 0x000C7F4D File Offset: 0x000C614D
		public virtual void Send(SendOrPostCallback d, object state)
		{
			d(state);
		}

		/// <summary>When overridden in a derived class, dispatches an asynchronous message to a synchronization context.</summary>
		/// <param name="d">The <see cref="T:System.Threading.SendOrPostCallback" /> delegate to call.</param>
		/// <param name="state">The object passed to the delegate.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003648 RID: 13896 RVA: 0x000C7F56 File Offset: 0x000C6156
		public virtual void Post(SendOrPostCallback d, object state)
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(d.Invoke), state);
		}

		/// <summary>When overridden in a derived class, responds to the notification that an operation has started.</summary>
		// Token: 0x06003649 RID: 13897 RVA: 0x00002194 File Offset: 0x00000394
		public virtual void OperationStarted()
		{
		}

		/// <summary>When overridden in a derived class, responds to the notification that an operation has completed.</summary>
		// Token: 0x0600364A RID: 13898 RVA: 0x00002194 File Offset: 0x00000394
		public virtual void OperationCompleted()
		{
		}

		/// <summary>Waits for any or all the elements in the specified array to receive a signal.</summary>
		/// <returns>The array index of the object that satisfied the wait.</returns>
		/// <param name="waitHandles">An array of type <see cref="T:System.IntPtr" /> that contains the native operating system handles.</param>
		/// <param name="waitAll">true to wait for all handles; false to wait for any handle. </param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="waitHandles" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x0600364B RID: 13899 RVA: 0x000C7F6B File Offset: 0x000C616B
		[PrePrepareMethod]
		[SecurityCritical]
		[CLSCompliant(false)]
		public virtual int Wait(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
		{
			if (waitHandles == null)
			{
				throw new ArgumentNullException("waitHandles");
			}
			return SynchronizationContext.WaitHelper(waitHandles, waitAll, millisecondsTimeout);
		}

		/// <summary>Helper function that waits for any or all the elements in the specified array to receive a signal.</summary>
		/// <returns>The array index of the object that satisfied the wait.</returns>
		/// <param name="waitHandles">An array of type <see cref="T:System.IntPtr" /> that contains the native operating system handles.</param>
		/// <param name="waitAll">true to wait for all handles;  false to wait for any handle. </param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		// Token: 0x0600364C RID: 13900 RVA: 0x0002126B File Offset: 0x0001F46B
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[PrePrepareMethod]
		[SecurityCritical]
		protected static int WaitHelper(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the current synchronization context.</summary>
		/// <param name="syncContext">The <see cref="T:System.Threading.SynchronizationContext" /> object to be set.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x0600364D RID: 13901 RVA: 0x000C7F83 File Offset: 0x000C6183
		[SecurityCritical]
		public static void SetSynchronizationContext(SynchronizationContext syncContext)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			mutableExecutionContext.SynchronizationContext = syncContext;
			mutableExecutionContext.SynchronizationContextNoFlow = syncContext;
		}

		/// <summary>Gets the synchronization context for the current thread.</summary>
		/// <returns>A <see cref="T:System.Threading.SynchronizationContext" /> object representing the current synchronization context.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x0600364E RID: 13902 RVA: 0x000C7F9C File Offset: 0x000C619C
		public static SynchronizationContext Current
		{
			get
			{
				return Thread.CurrentThread.GetExecutionContextReader().SynchronizationContext ?? SynchronizationContext.GetThreadLocalContext();
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x0600364F RID: 13903 RVA: 0x000C7FC4 File Offset: 0x000C61C4
		internal static SynchronizationContext CurrentNoFlow
		{
			[FriendAccessAllowed]
			get
			{
				return Thread.CurrentThread.GetExecutionContextReader().SynchronizationContextNoFlow ?? SynchronizationContext.GetThreadLocalContext();
			}
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x0000A42E File Offset: 0x0000862E
		private static SynchronizationContext GetThreadLocalContext()
		{
			return null;
		}

		/// <summary>When overridden in a derived class, creates a copy of the synchronization context.  </summary>
		/// <returns>A new <see cref="T:System.Threading.SynchronizationContext" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003651 RID: 13905 RVA: 0x000C7FEC File Offset: 0x000C61EC
		public virtual SynchronizationContext CreateCopy()
		{
			return new SynchronizationContext();
		}

		// Token: 0x06003652 RID: 13906 RVA: 0x000C7FF3 File Offset: 0x000C61F3
		[SecurityCritical]
		private static int InvokeWaitMethodHelper(SynchronizationContext syncContext, IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
		{
			return syncContext.Wait(waitHandles, waitAll, millisecondsTimeout);
		}

		// Token: 0x04001CC8 RID: 7368
		private SynchronizationContextProperties _props;

		// Token: 0x04001CC9 RID: 7369
		private static Type s_cachedPreparedType1;

		// Token: 0x04001CCA RID: 7370
		private static Type s_cachedPreparedType2;

		// Token: 0x04001CCB RID: 7371
		private static Type s_cachedPreparedType3;

		// Token: 0x04001CCC RID: 7372
		private static Type s_cachedPreparedType4;

		// Token: 0x04001CCD RID: 7373
		private static Type s_cachedPreparedType5;

		// Token: 0x02000480 RID: 1152
		// (Invoke) Token: 0x06003654 RID: 13908
		private delegate int WaitDelegate(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout);
	}
}
