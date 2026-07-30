using System;
using System.Security.Permissions;
using System.Threading;

namespace System.ComponentModel
{
	/// <summary>Executes an operation on a separate thread.</summary>
	// Token: 0x02000231 RID: 561
	[DefaultEvent("DoWork")]
	[SRDescription("Executes an operation on a separate thread.")]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class BackgroundWorker : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BackgroundWorker" /> class.</summary>
		// Token: 0x06001215 RID: 4629 RVA: 0x0004D2AB File Offset: 0x0004B4AB
		public BackgroundWorker()
		{
			this.threadStart = new BackgroundWorker.WorkerThreadStartDelegate(this.WorkerThreadStart);
			this.operationCompleted = new SendOrPostCallback(this.AsyncOperationCompleted);
			this.progressReporter = new SendOrPostCallback(this.ProgressReporter);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0004D2E9 File Offset: 0x0004B4E9
		private void AsyncOperationCompleted(object arg)
		{
			this.isRunning = false;
			this.cancellationPending = false;
			this.OnRunWorkerCompleted((RunWorkerCompletedEventArgs)arg);
		}

		/// <summary>Gets a value indicating whether the application has requested cancellation of a background operation.</summary>
		/// <returns>true if the application has requested cancellation of a background operation; otherwise, false. The default is false.</returns>
		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x0004D305 File Offset: 0x0004B505
		[SRDescription("Has the user attempted to cancel the operation? To be accessed from DoWork event handler.")]
		[Browsable(false)]
		public bool CancellationPending
		{
			get
			{
				return this.cancellationPending;
			}
		}

		/// <summary>Requests cancellation of a pending background operation.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.BackgroundWorker.WorkerSupportsCancellation" /> is false. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001218 RID: 4632 RVA: 0x0004D30D File Offset: 0x0004B50D
		public void CancelAsync()
		{
			if (!this.WorkerSupportsCancellation)
			{
				throw new InvalidOperationException(global::SR.GetString("This BackgroundWorker states that it doesn't support cancellation. Modify WorkerSupportsCancellation to state that it does support cancellation."));
			}
			this.cancellationPending = true;
		}

		/// <summary>Occurs when <see cref="M:System.ComponentModel.BackgroundWorker.RunWorkerAsync" /> is called.</summary>
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06001219 RID: 4633 RVA: 0x0004D32E File Offset: 0x0004B52E
		// (remove) Token: 0x0600121A RID: 4634 RVA: 0x0004D341 File Offset: 0x0004B541
		[SRDescription("Event handler to be run on a different thread when the operation begins.")]
		[SRCategory("Asynchronous")]
		public event DoWorkEventHandler DoWork
		{
			add
			{
				base.Events.AddHandler(BackgroundWorker.doWorkKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(BackgroundWorker.doWorkKey, value);
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.ComponentModel.BackgroundWorker" /> is running an asynchronous operation.</summary>
		/// <returns>true, if the <see cref="T:System.ComponentModel.BackgroundWorker" /> is running an asynchronous operation; otherwise, false.</returns>
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x0004D354 File Offset: 0x0004B554
		[Browsable(false)]
		[SRDescription("Is the worker still currently working on a background operation?")]
		public bool IsBusy
		{
			get
			{
				return this.isRunning;
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600121C RID: 4636 RVA: 0x0004D35C File Offset: 0x0004B55C
		protected virtual void OnDoWork(DoWorkEventArgs e)
		{
			DoWorkEventHandler doWorkEventHandler = (DoWorkEventHandler)base.Events[BackgroundWorker.doWorkKey];
			if (doWorkEventHandler != null)
			{
				doWorkEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.RunWorkerCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600121D RID: 4637 RVA: 0x0004D38C File Offset: 0x0004B58C
		protected virtual void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e)
		{
			RunWorkerCompletedEventHandler runWorkerCompletedEventHandler = (RunWorkerCompletedEventHandler)base.Events[BackgroundWorker.runWorkerCompletedKey];
			if (runWorkerCompletedEventHandler != null)
			{
				runWorkerCompletedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600121E RID: 4638 RVA: 0x0004D3BC File Offset: 0x0004B5BC
		protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
		{
			ProgressChangedEventHandler progressChangedEventHandler = (ProgressChangedEventHandler)base.Events[BackgroundWorker.progressChangedKey];
			if (progressChangedEventHandler != null)
			{
				progressChangedEventHandler(this, e);
			}
		}

		/// <summary>Occurs when <see cref="M:System.ComponentModel.BackgroundWorker.ReportProgress(System.Int32)" /> is called.</summary>
		// Token: 0x1400001E RID: 30
		// (add) Token: 0x0600121F RID: 4639 RVA: 0x0004D3EA File Offset: 0x0004B5EA
		// (remove) Token: 0x06001220 RID: 4640 RVA: 0x0004D3FD File Offset: 0x0004B5FD
		[SRCategory("Asynchronous")]
		[SRDescription("Raised when the worker thread indicates that some progress has been made.")]
		public event ProgressChangedEventHandler ProgressChanged
		{
			add
			{
				base.Events.AddHandler(BackgroundWorker.progressChangedKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(BackgroundWorker.progressChangedKey, value);
			}
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0004D410 File Offset: 0x0004B610
		private void ProgressReporter(object arg)
		{
			this.OnProgressChanged((ProgressChangedEventArgs)arg);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
		/// <param name="percentProgress">The percentage, from 0 to 100, of the background operation that is complete. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.BackgroundWorker.WorkerReportsProgress" /> property is set to false. </exception>
		// Token: 0x06001222 RID: 4642 RVA: 0x0004D41E File Offset: 0x0004B61E
		public void ReportProgress(int percentProgress)
		{
			this.ReportProgress(percentProgress, null);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
		/// <param name="percentProgress">The percentage, from 0 to 100, of the background operation that is complete.</param>
		/// <param name="userState">The state object passed to <see cref="M:System.ComponentModel.BackgroundWorker.RunWorkerAsync(System.Object)" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.BackgroundWorker.WorkerReportsProgress" /> property is set to false. </exception>
		// Token: 0x06001223 RID: 4643 RVA: 0x0004D428 File Offset: 0x0004B628
		public void ReportProgress(int percentProgress, object userState)
		{
			if (!this.WorkerReportsProgress)
			{
				throw new InvalidOperationException(global::SR.GetString("This BackgroundWorker states that it doesn't report progress. Modify WorkerReportsProgress to state that it does report progress."));
			}
			ProgressChangedEventArgs progressChangedEventArgs = new ProgressChangedEventArgs(percentProgress, userState);
			if (this.asyncOperation != null)
			{
				this.asyncOperation.Post(this.progressReporter, progressChangedEventArgs);
				return;
			}
			this.progressReporter(progressChangedEventArgs);
		}

		/// <summary>Starts execution of a background operation.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.BackgroundWorker.IsBusy" /> is true.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001224 RID: 4644 RVA: 0x0004D47C File Offset: 0x0004B67C
		public void RunWorkerAsync()
		{
			this.RunWorkerAsync(null);
		}

		/// <summary>Starts execution of a background operation.</summary>
		/// <param name="argument">A parameter for use by the background operation to be executed in the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event handler. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.BackgroundWorker.IsBusy" /> is true. </exception>
		// Token: 0x06001225 RID: 4645 RVA: 0x0004D488 File Offset: 0x0004B688
		public void RunWorkerAsync(object argument)
		{
			if (this.isRunning)
			{
				throw new InvalidOperationException(global::SR.GetString("This BackgroundWorker is currently busy and cannot run multiple tasks concurrently."));
			}
			this.isRunning = true;
			this.cancellationPending = false;
			this.asyncOperation = AsyncOperationManager.CreateOperation(null);
			this.threadStart.BeginInvoke(argument, null, null);
		}

		/// <summary>Occurs when the background operation has completed, has been canceled, or has raised an exception.</summary>
		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06001226 RID: 4646 RVA: 0x0004D4D6 File Offset: 0x0004B6D6
		// (remove) Token: 0x06001227 RID: 4647 RVA: 0x0004D4E9 File Offset: 0x0004B6E9
		[SRDescription("Raised when the worker has completed (either through success, failure, or cancellation).")]
		[SRCategory("Asynchronous")]
		public event RunWorkerCompletedEventHandler RunWorkerCompleted
		{
			add
			{
				base.Events.AddHandler(BackgroundWorker.runWorkerCompletedKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(BackgroundWorker.runWorkerCompletedKey, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.ComponentModel.BackgroundWorker" /> can report progress updates.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.BackgroundWorker" /> supports progress updates; otherwise false. The default is false.</returns>
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x0004D4FC File Offset: 0x0004B6FC
		// (set) Token: 0x06001229 RID: 4649 RVA: 0x0004D504 File Offset: 0x0004B704
		[SRCategory("Asynchronous")]
		[SRDescription("Whether the worker will report progress.")]
		[DefaultValue(false)]
		public bool WorkerReportsProgress
		{
			get
			{
				return this.workerReportsProgress;
			}
			set
			{
				this.workerReportsProgress = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.ComponentModel.BackgroundWorker" /> supports asynchronous cancellation.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.BackgroundWorker" /> supports cancellation; otherwise false. The default is false.</returns>
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x0004D50D File Offset: 0x0004B70D
		// (set) Token: 0x0600122B RID: 4651 RVA: 0x0004D515 File Offset: 0x0004B715
		[SRDescription("Whether the worker supports cancellation.")]
		[DefaultValue(false)]
		[SRCategory("Asynchronous")]
		public bool WorkerSupportsCancellation
		{
			get
			{
				return this.canCancelWorker;
			}
			set
			{
				this.canCancelWorker = value;
			}
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0004D520 File Offset: 0x0004B720
		private void WorkerThreadStart(object argument)
		{
			object obj = null;
			Exception ex = null;
			bool flag = false;
			try
			{
				DoWorkEventArgs doWorkEventArgs = new DoWorkEventArgs(argument);
				this.OnDoWork(doWorkEventArgs);
				if (doWorkEventArgs.Cancel)
				{
					flag = true;
				}
				else
				{
					obj = doWorkEventArgs.Result;
				}
			}
			catch (Exception ex)
			{
			}
			RunWorkerCompletedEventArgs runWorkerCompletedEventArgs = new RunWorkerCompletedEventArgs(obj, ex, flag);
			this.asyncOperation.PostOperationCompleted(this.operationCompleted, runWorkerCompletedEventArgs);
		}

		// Token: 0x0400123E RID: 4670
		private static readonly object doWorkKey = new object();

		// Token: 0x0400123F RID: 4671
		private static readonly object runWorkerCompletedKey = new object();

		// Token: 0x04001240 RID: 4672
		private static readonly object progressChangedKey = new object();

		// Token: 0x04001241 RID: 4673
		private bool canCancelWorker;

		// Token: 0x04001242 RID: 4674
		private bool workerReportsProgress;

		// Token: 0x04001243 RID: 4675
		private bool cancellationPending;

		// Token: 0x04001244 RID: 4676
		private bool isRunning;

		// Token: 0x04001245 RID: 4677
		private AsyncOperation asyncOperation;

		// Token: 0x04001246 RID: 4678
		private readonly BackgroundWorker.WorkerThreadStartDelegate threadStart;

		// Token: 0x04001247 RID: 4679
		private readonly SendOrPostCallback operationCompleted;

		// Token: 0x04001248 RID: 4680
		private readonly SendOrPostCallback progressReporter;

		// Token: 0x02000232 RID: 562
		// (Invoke) Token: 0x0600122F RID: 4655
		private delegate void WorkerThreadStartDelegate(object argument);
	}
}
