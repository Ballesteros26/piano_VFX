using System;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the MethodNameCompleted event.</summary>
	// Token: 0x0200022A RID: 554
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class AsyncCompletedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> class.</summary>
		// Token: 0x060011E4 RID: 4580 RVA: 0x0000BE61 File Offset: 0x0000A061
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		public AsyncCompletedEventArgs()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> class. </summary>
		/// <param name="error">Any error that occurred during the asynchronous operation.</param>
		/// <param name="cancelled">A value indicating whether the asynchronous operation was canceled.</param>
		/// <param name="userState">The optional user-supplied state object passed to the <see cref="M:System.ComponentModel.BackgroundWorker.RunWorkerAsync(System.Object)" /> method.</param>
		// Token: 0x060011E5 RID: 4581 RVA: 0x0004CB77 File Offset: 0x0004AD77
		public AsyncCompletedEventArgs(Exception error, bool cancelled, object userState)
		{
			this.error = error;
			this.cancelled = cancelled;
			this.userState = userState;
		}

		/// <summary>Gets a value indicating whether an asynchronous operation has been canceled.</summary>
		/// <returns>true if the background operation has been canceled; otherwise false. The default is false.</returns>
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x0004CB94 File Offset: 0x0004AD94
		[SRDescription("True if operation was cancelled.")]
		public bool Cancelled
		{
			get
			{
				return this.cancelled;
			}
		}

		/// <summary>Gets a value indicating which error occurred during an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Exception" /> instance, if an error occurred during an asynchronous operation; otherwise null.</returns>
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x0004CB9C File Offset: 0x0004AD9C
		[SRDescription("Exception that occurred during operation.  Null if no error.")]
		public Exception Error
		{
			get
			{
				return this.error;
			}
		}

		/// <summary>Gets the unique identifier for the asynchronous task.</summary>
		/// <returns>An object reference that uniquely identifies the asynchronous task; otherwise, null if no value has been set.</returns>
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x0004CBA4 File Offset: 0x0004ADA4
		[SRDescription("User-supplied state to identify operation.")]
		public object UserState
		{
			get
			{
				return this.userState;
			}
		}

		/// <summary>Raises a user-supplied exception if an asynchronous operation failed.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Cancelled" /> property is true. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Error" /> property has been set by the asynchronous operation. The <see cref="P:System.Exception.InnerException" /> property holds a reference to <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Error" />. </exception>
		// Token: 0x060011E9 RID: 4585 RVA: 0x0004CBAC File Offset: 0x0004ADAC
		protected void RaiseExceptionIfNecessary()
		{
			if (this.Error != null)
			{
				throw new TargetInvocationException(global::SR.GetString("An exception occurred during the operation, making the result invalid.  Check InnerException for exception details."), this.Error);
			}
			if (this.Cancelled)
			{
				throw new InvalidOperationException(global::SR.GetString("Operation has been cancelled."));
			}
		}

		// Token: 0x0400122D RID: 4653
		private readonly Exception error;

		// Token: 0x0400122E RID: 4654
		private readonly bool cancelled;

		// Token: 0x0400122F RID: 4655
		private readonly object userState;
	}
}
