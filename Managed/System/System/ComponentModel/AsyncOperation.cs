using System;
using System.Security.Permissions;
using System.Threading;
using Unity;

namespace System.ComponentModel
{
	/// <summary>Tracks the lifetime of an asynchronous operation.</summary>
	// Token: 0x0200022C RID: 556
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public sealed class AsyncOperation
	{
		// Token: 0x060011EE RID: 4590 RVA: 0x0004CBE4 File Offset: 0x0004ADE4
		private AsyncOperation(object userSuppliedState, SynchronizationContext syncContext)
		{
			this.userSuppliedState = userSuppliedState;
			this.syncContext = syncContext;
			this.alreadyCompleted = false;
			this.syncContext.OperationStarted();
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0004CC0C File Offset: 0x0004AE0C
		~AsyncOperation()
		{
			if (!this.alreadyCompleted && this.syncContext != null)
			{
				this.syncContext.OperationCompleted();
			}
		}

		/// <summary>Gets or sets an object used to uniquely identify an asynchronous operation.</summary>
		/// <returns>The state object passed to the asynchronous method invocation.</returns>
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x0004CC50 File Offset: 0x0004AE50
		public object UserSuppliedState
		{
			get
			{
				return this.userSuppliedState;
			}
		}

		/// <summary>Gets the <see cref="T:System.Threading.SynchronizationContext" /> object that was passed to the constructor.</summary>
		/// <returns>The <see cref="T:System.Threading.SynchronizationContext" /> object that was passed to the constructor.</returns>
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x0004CC58 File Offset: 0x0004AE58
		public SynchronizationContext SynchronizationContext
		{
			get
			{
				return this.syncContext;
			}
		}

		/// <summary>Invokes a delegate on the thread or context appropriate for the application model.</summary>
		/// <param name="d">A <see cref="T:System.Threading.SendOrPostCallback" /> object that wraps the delegate to be called when the operation ends. </param>
		/// <param name="arg">An argument for the delegate contained in the <paramref name="d" /> parameter. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.ComponentModel.AsyncOperation.PostOperationCompleted(System.Threading.SendOrPostCallback,System.Object)" /> method has been called previously for this task. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		// Token: 0x060011F2 RID: 4594 RVA: 0x0004CC60 File Offset: 0x0004AE60
		public void Post(SendOrPostCallback d, object arg)
		{
			this.VerifyNotCompleted();
			this.VerifyDelegateNotNull(d);
			this.syncContext.Post(d, arg);
		}

		/// <summary>Ends the lifetime of an asynchronous operation.</summary>
		/// <param name="d">A <see cref="T:System.Threading.SendOrPostCallback" /> object that wraps the delegate to be called when the operation ends. </param>
		/// <param name="arg">An argument for the delegate contained in the <paramref name="d" /> parameter. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.ComponentModel.AsyncOperation.OperationCompleted" /> has been called previously for this task. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		// Token: 0x060011F3 RID: 4595 RVA: 0x0004CC7C File Offset: 0x0004AE7C
		public void PostOperationCompleted(SendOrPostCallback d, object arg)
		{
			this.Post(d, arg);
			this.OperationCompletedCore();
		}

		/// <summary>Ends the lifetime of an asynchronous operation.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.ComponentModel.AsyncOperation.OperationCompleted" /> has been called previously for this task. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060011F4 RID: 4596 RVA: 0x0004CC8C File Offset: 0x0004AE8C
		public void OperationCompleted()
		{
			this.VerifyNotCompleted();
			this.OperationCompletedCore();
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0004CC9C File Offset: 0x0004AE9C
		private void OperationCompletedCore()
		{
			try
			{
				this.syncContext.OperationCompleted();
			}
			finally
			{
				this.alreadyCompleted = true;
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0004CCD4 File Offset: 0x0004AED4
		private void VerifyNotCompleted()
		{
			if (this.alreadyCompleted)
			{
				throw new InvalidOperationException(global::SR.GetString("This operation has already had OperationCompleted called on it and further calls are illegal."));
			}
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0004CCEE File Offset: 0x0004AEEE
		private void VerifyDelegateNotNull(SendOrPostCallback d)
		{
			if (d == null)
			{
				throw new ArgumentNullException(global::SR.GetString("A non-null SendOrPostCallback must be supplied."), "d");
			}
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0004CD08 File Offset: 0x0004AF08
		internal static AsyncOperation CreateOperation(object userSuppliedState, SynchronizationContext syncContext)
		{
			return new AsyncOperation(userSuppliedState, syncContext);
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal AsyncOperation()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001230 RID: 4656
		private SynchronizationContext syncContext;

		// Token: 0x04001231 RID: 4657
		private object userSuppliedState;

		// Token: 0x04001232 RID: 4658
		private bool alreadyCompleted;
	}
}
