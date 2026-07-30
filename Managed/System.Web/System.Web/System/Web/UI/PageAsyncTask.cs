using System;
using System.Threading;
using System.Threading.Tasks;
using Unity;

namespace System.Web.UI
{
	/// <summary>Contains information about an asynchronous task registered to a page. This class cannot be inherited.</summary>
	// Token: 0x0200020F RID: 527
	public sealed class PageAsyncTask
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PageAsyncTask" /> class using the default value for executing in parallel. </summary>
		/// <param name="beginHandler">The handler to call when beginning an asynchronous task.</param>
		/// <param name="endHandler">The handler to call when the task is completed successfully within the time-out period.</param>
		/// <param name="timeoutHandler">The handler to call when the task is not completed successfully within the time-out period.</param>
		/// <param name="state">The object that represents the state of the task.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginHandler" /> parameter or <paramref name="endHandler" /> parameter is not specified.</exception>
		// Token: 0x06001582 RID: 5506 RVA: 0x0003A478 File Offset: 0x00038678
		public PageAsyncTask(BeginEventHandler beginHandler, EndEventHandler endHandler, EndEventHandler timeoutHandler, object state)
			: this(beginHandler, endHandler, timeoutHandler, state, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PageAsyncTask" /> class using the specified value for executing in parallel. </summary>
		/// <param name="beginHandler">The handler to call when beginning an asynchronous task.</param>
		/// <param name="endHandler">The handler to call when the task is completed successfully within the time-out period.</param>
		/// <param name="timeoutHandler">The handler to call when the task is not completed successfully within the time-out period.</param>
		/// <param name="state">The object that represents the state of the task.</param>
		/// <param name="executeInParallel">The value that indicates whether the task can be processed in parallel with other tasks.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginHandler" /> parameter or <paramref name="endHandler" /> parameter is not specified.</exception>
		// Token: 0x06001583 RID: 5507 RVA: 0x0003A486 File Offset: 0x00038686
		public PageAsyncTask(BeginEventHandler beginHandler, EndEventHandler endHandler, EndEventHandler timeoutHandler, object state, bool executeInParallel)
		{
			this.beginHandler = beginHandler;
			this.endHandler = endHandler;
			this.timeoutHandler = timeoutHandler;
			this.state = state;
			this.executeInParallel = executeInParallel;
		}

		/// <summary>Gets the method to call when beginning an asynchronous task.</summary>
		/// <returns>A <see cref="T:System.Web.BeginEventHandler" /> delegate that represents the method to call when beginning the asynchronous task. </returns>
		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x0003A4B3 File Offset: 0x000386B3
		public BeginEventHandler BeginHandler
		{
			get
			{
				return this.beginHandler;
			}
		}

		/// <summary>Gets the method to call when the task completes successfully within the time-out period.</summary>
		/// <returns>An <see cref="T:System.Web.EndEventHandler" /> delegate that represents the method to call when the task completes successfully within the time-out period.</returns>
		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x0003A4BB File Offset: 0x000386BB
		public EndEventHandler EndHandler
		{
			get
			{
				return this.endHandler;
			}
		}

		/// <summary>Gets the method to call when the task does not complete successfully within the time-out period.</summary>
		/// <returns>An <see cref="T:System.Web.EndEventHandler" /> delegate that represents the method to call when the task does not complete successfully within the time-out period.</returns>
		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x0003A4C3 File Offset: 0x000386C3
		public EndEventHandler TimeoutHandler
		{
			get
			{
				return this.timeoutHandler;
			}
		}

		/// <summary>Gets a value that indicates whether the task can be processed in parallel with other tasks.</summary>
		/// <returns>true if the task should be processed in parallel with other tasks; otherwise, false.</returns>
		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x0003A4CB File Offset: 0x000386CB
		public bool ExecuteInParallel
		{
			get
			{
				return this.executeInParallel;
			}
		}

		/// <summary>Gets an object that represents the state of the task.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the state of the task.</returns>
		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x0003A4D3 File Offset: 0x000386D3
		public object State
		{
			get
			{
				return this.state;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PageAsyncTask" /> class using an event handler that enables the task to be canceled.</summary>
		/// <param name="handler">An event handler.</param>
		// Token: 0x06001589 RID: 5513 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public PageAsyncTask(Func<CancellationToken, Task> handler)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PageAsyncTask" /> class using an event handler that enables the task to be handled.</summary>
		/// <param name="handler">An event handler.</param>
		// Token: 0x0600158A RID: 5514 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public PageAsyncTask(Func<Task> handler)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400150F RID: 5391
		private BeginEventHandler beginHandler;

		// Token: 0x04001510 RID: 5392
		private EndEventHandler endHandler;

		// Token: 0x04001511 RID: 5393
		private EndEventHandler timeoutHandler;

		// Token: 0x04001512 RID: 5394
		private bool executeInParallel;

		// Token: 0x04001513 RID: 5395
		private object state;
	}
}
