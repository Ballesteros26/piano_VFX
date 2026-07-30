using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Propagates notification that operations should be canceled.</summary>
	// Token: 0x0200044E RID: 1102
	[ComVisible(false)]
	[DebuggerDisplay("IsCancellationRequested = {IsCancellationRequested}")]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct CancellationToken
	{
		/// <summary>Returns an empty CancellationToken value.</summary>
		/// <returns>Returns an empty CancellationToken value.</returns>
		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x060034BE RID: 13502 RVA: 0x000C33B8 File Offset: 0x000C15B8
		public static CancellationToken None
		{
			get
			{
				return default(CancellationToken);
			}
		}

		/// <summary>Gets whether cancellation has been requested for this token.</summary>
		/// <returns>true if cancellation has been requested for this token; otherwise false.</returns>
		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x060034BF RID: 13503 RVA: 0x000C33CE File Offset: 0x000C15CE
		public bool IsCancellationRequested
		{
			get
			{
				return this.m_source != null && this.m_source.IsCancellationRequested;
			}
		}

		/// <summary>Gets whether this token is capable of being in the canceled state.</summary>
		/// <returns>true if this token is capable of being in the canceled state; otherwise false.</returns>
		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x060034C0 RID: 13504 RVA: 0x000C33E5 File Offset: 0x000C15E5
		public bool CanBeCanceled
		{
			get
			{
				return this.m_source != null && this.m_source.CanBeCanceled;
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that is signaled when the token is canceled.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that is signaled when the token is canceled.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x060034C1 RID: 13505 RVA: 0x000C33FC File Offset: 0x000C15FC
		public WaitHandle WaitHandle
		{
			get
			{
				if (this.m_source == null)
				{
					this.InitializeDefaultSource();
				}
				return this.m_source.WaitHandle;
			}
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x000C3417 File Offset: 0x000C1617
		internal CancellationToken(CancellationTokenSource source)
		{
			this.m_source = source;
		}

		/// <summary>Initializes the <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="canceled">The canceled state for the token.</param>
		// Token: 0x060034C3 RID: 13507 RVA: 0x000C3420 File Offset: 0x000C1620
		public CancellationToken(bool canceled)
		{
			this = default(CancellationToken);
			if (canceled)
			{
				this.m_source = CancellationTokenSource.InternalGetStaticSource(canceled);
			}
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x000C3438 File Offset: 0x000C1638
		private static void ActionToActionObjShunt(object obj)
		{
			(obj as Action)();
		}

		/// <summary>Registers a delegate that will be called when this <see cref="T:System.Threading.CancellationToken" /> is canceled.</summary>
		/// <returns>The <see cref="T:System.Threading.CancellationTokenRegistration" /> instance that can be used to deregister the callback.</returns>
		/// <param name="callback">The delegate to be executed when the <see cref="T:System.Threading.CancellationToken" /> is canceled.</param>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> is null.</exception>
		// Token: 0x060034C5 RID: 13509 RVA: 0x000C3445 File Offset: 0x000C1645
		public CancellationTokenRegistration Register(Action callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			return this.Register(CancellationToken.s_ActionToActionObjShunt, callback, false, true);
		}

		/// <summary>Registers a delegate that will be called when this <see cref="T:System.Threading.CancellationToken" /> is canceled.</summary>
		/// <returns>The <see cref="T:System.Threading.CancellationTokenRegistration" /> instance that can be used to deregister the callback.</returns>
		/// <param name="callback">The delegate to be executed when the <see cref="T:System.Threading.CancellationToken" /> is canceled.</param>
		/// <param name="useSynchronizationContext">A Boolean value that indicates whether to capture the current <see cref="T:System.Threading.SynchronizationContext" /> and use it when invoking the <paramref name="callback" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> is null.</exception>
		// Token: 0x060034C6 RID: 13510 RVA: 0x000C3463 File Offset: 0x000C1663
		public CancellationTokenRegistration Register(Action callback, bool useSynchronizationContext)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			return this.Register(CancellationToken.s_ActionToActionObjShunt, callback, useSynchronizationContext, true);
		}

		/// <summary>Registers a delegate that will be called when this <see cref="T:System.Threading.CancellationToken" /> is canceled.</summary>
		/// <returns>The <see cref="T:System.Threading.CancellationTokenRegistration" /> instance that can be used to deregister the callback.</returns>
		/// <param name="callback">The delegate to be executed when the <see cref="T:System.Threading.CancellationToken" /> is canceled.</param>
		/// <param name="state">The state to pass to the <paramref name="callback" /> when the delegate is invoked. This may be null.</param>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> is null.</exception>
		// Token: 0x060034C7 RID: 13511 RVA: 0x000C3481 File Offset: 0x000C1681
		public CancellationTokenRegistration Register(Action<object> callback, object state)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			return this.Register(callback, state, false, true);
		}

		/// <summary>Registers a delegate that will be called when this <see cref="T:System.Threading.CancellationToken" /> is canceled.</summary>
		/// <returns>The <see cref="T:System.Threading.CancellationTokenRegistration" /> instance that can be used to deregister the callback.</returns>
		/// <param name="callback">The delegate to be executed when the <see cref="T:System.Threading.CancellationToken" /> is canceled.</param>
		/// <param name="state">The state to pass to the <paramref name="callback" /> when the delegate is invoked. This may be null.</param>
		/// <param name="useSynchronizationContext">A Boolean value that indicates whether to capture the current <see cref="T:System.Threading.SynchronizationContext" /> and use it when invoking the <paramref name="callback" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> is null.</exception>
		// Token: 0x060034C8 RID: 13512 RVA: 0x000C349B File Offset: 0x000C169B
		public CancellationTokenRegistration Register(Action<object> callback, object state, bool useSynchronizationContext)
		{
			return this.Register(callback, state, useSynchronizationContext, true);
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x000C34A7 File Offset: 0x000C16A7
		internal CancellationTokenRegistration InternalRegisterWithoutEC(Action<object> callback, object state)
		{
			return this.Register(callback, state, false, false);
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x000C34B4 File Offset: 0x000C16B4
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private CancellationTokenRegistration Register(Action<object> callback, object state, bool useSynchronizationContext, bool useExecutionContext)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			if (!this.CanBeCanceled)
			{
				return default(CancellationTokenRegistration);
			}
			SynchronizationContext synchronizationContext = null;
			ExecutionContext executionContext = null;
			if (!this.IsCancellationRequested)
			{
				if (useSynchronizationContext)
				{
					synchronizationContext = SynchronizationContext.Current;
				}
				if (useExecutionContext)
				{
					executionContext = ExecutionContext.Capture(ref stackCrawlMark, ExecutionContext.CaptureOptions.OptimizeDefaultCase);
				}
			}
			return this.m_source.InternalRegister(callback, state, synchronizationContext, executionContext);
		}

		/// <summary>Determines whether the current <see cref="T:System.Threading.CancellationToken" /> instance is equal to the specified token.</summary>
		/// <returns>True if the instances are equal; otherwise, false. Two tokens are equal if they are associated with the same <see cref="T:System.Threading.CancellationTokenSource" /> or if they were both constructed from public CancellationToken constructors and their <see cref="P:System.Threading.CancellationToken.IsCancellationRequested" /> values are equal.</returns>
		/// <param name="other">The other <see cref="T:System.Threading.CancellationToken" /> to which to compare this instance.</param>
		// Token: 0x060034CB RID: 13515 RVA: 0x000C3514 File Offset: 0x000C1714
		public bool Equals(CancellationToken other)
		{
			if (this.m_source == null && other.m_source == null)
			{
				return true;
			}
			if (this.m_source == null)
			{
				return other.m_source == CancellationTokenSource.InternalGetStaticSource(false);
			}
			if (other.m_source == null)
			{
				return this.m_source == CancellationTokenSource.InternalGetStaticSource(false);
			}
			return this.m_source == other.m_source;
		}

		/// <summary>Determines whether the current <see cref="T:System.Threading.CancellationToken" /> instance is equal to the specified <see cref="T:System.Object" />.</summary>
		/// <returns>True if <paramref name="other" /> is a <see cref="T:System.Threading.CancellationToken" /> and if the two instances are equal; otherwise, false. Two tokens are equal if they are associated with the same <see cref="T:System.Threading.CancellationTokenSource" /> or if they were both constructed from public CancellationToken constructors and their <see cref="P:System.Threading.CancellationToken.IsCancellationRequested" /> values are equal.</returns>
		/// <param name="other">The other object to which to compare this instance.</param>
		/// <exception cref="T:System.ObjectDisposedException">An associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		// Token: 0x060034CC RID: 13516 RVA: 0x000C356F File Offset: 0x000C176F
		public override bool Equals(object other)
		{
			return other is CancellationToken && this.Equals((CancellationToken)other);
		}

		/// <summary>Serves as a hash function for a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Threading.CancellationToken" /> instance.</returns>
		// Token: 0x060034CD RID: 13517 RVA: 0x000C3587 File Offset: 0x000C1787
		public override int GetHashCode()
		{
			if (this.m_source == null)
			{
				return CancellationTokenSource.InternalGetStaticSource(false).GetHashCode();
			}
			return this.m_source.GetHashCode();
		}

		/// <summary>Determines whether two <see cref="T:System.Threading.CancellationToken" /> instances are equal.</summary>
		/// <returns>True if the instances are equal; otherwise, false.</returns>
		/// <param name="left">The first instance.</param>
		/// <param name="right">The second instance.</param>
		/// <exception cref="T:System.ObjectDisposedException">An associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		// Token: 0x060034CE RID: 13518 RVA: 0x000C35A8 File Offset: 0x000C17A8
		public static bool operator ==(CancellationToken left, CancellationToken right)
		{
			return left.Equals(right);
		}

		/// <summary>Determines whether two <see cref="T:System.Threading.CancellationToken" /> instances are not equal.</summary>
		/// <returns>True if the instances are not equal; otherwise, false.</returns>
		/// <param name="left">The first instance.</param>
		/// <param name="right">The second instance.</param>
		/// <exception cref="T:System.ObjectDisposedException">An associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		// Token: 0x060034CF RID: 13519 RVA: 0x000C35B2 File Offset: 0x000C17B2
		public static bool operator !=(CancellationToken left, CancellationToken right)
		{
			return !left.Equals(right);
		}

		/// <summary>Throws a <see cref="T:System.OperationCanceledException" /> if this token has had cancellation requested.</summary>
		/// <exception cref="T:System.OperationCanceledException">The token has had cancellation requested.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		// Token: 0x060034D0 RID: 13520 RVA: 0x000C35BF File Offset: 0x000C17BF
		public void ThrowIfCancellationRequested()
		{
			if (this.IsCancellationRequested)
			{
				this.ThrowOperationCanceledException();
			}
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000C35CF File Offset: 0x000C17CF
		internal void ThrowIfSourceDisposed()
		{
			if (this.m_source != null && this.m_source.IsDisposed)
			{
				CancellationToken.ThrowObjectDisposedException();
			}
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x000C35EB File Offset: 0x000C17EB
		private void ThrowOperationCanceledException()
		{
			throw new OperationCanceledException(Environment.GetResourceString("The operation was canceled."), this);
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x000C3602 File Offset: 0x000C1802
		private static void ThrowObjectDisposedException()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("The CancellationTokenSource associated with this CancellationToken has been disposed."));
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x000C3614 File Offset: 0x000C1814
		private void InitializeDefaultSource()
		{
			this.m_source = CancellationTokenSource.InternalGetStaticSource(false);
		}

		// Token: 0x04001C28 RID: 7208
		private CancellationTokenSource m_source;

		// Token: 0x04001C29 RID: 7209
		private static readonly Action<object> s_ActionToActionObjShunt = new Action<object>(CancellationToken.ActionToActionObjShunt);
	}
}
