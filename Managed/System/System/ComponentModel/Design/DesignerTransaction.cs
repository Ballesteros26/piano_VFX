using System;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a way to group a series of design-time actions to improve performance and enable most types of changes to be undone.</summary>
	// Token: 0x02000316 RID: 790
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class DesignerTransaction : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> class with no description.</summary>
		// Token: 0x06001931 RID: 6449 RVA: 0x00069BBA File Offset: 0x00067DBA
		protected DesignerTransaction()
			: this("")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> class using the specified transaction description.</summary>
		/// <param name="description">A description for this transaction. </param>
		// Token: 0x06001932 RID: 6450 RVA: 0x00069BC7 File Offset: 0x00067DC7
		protected DesignerTransaction(string description)
		{
			this.desc = description;
		}

		/// <summary>Gets a value indicating whether the transaction was canceled.</summary>
		/// <returns>true if the transaction was canceled; otherwise, false.</returns>
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x00069BD6 File Offset: 0x00067DD6
		public bool Canceled
		{
			get
			{
				return this.canceled;
			}
		}

		/// <summary>Gets a value indicating whether the transaction was committed.</summary>
		/// <returns>true if the transaction was committed; otherwise, false.</returns>
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001934 RID: 6452 RVA: 0x00069BDE File Offset: 0x00067DDE
		public bool Committed
		{
			get
			{
				return this.committed;
			}
		}

		/// <summary>Gets a description for the transaction.</summary>
		/// <returns>A description for the transaction.</returns>
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x00069BE6 File Offset: 0x00067DE6
		public string Description
		{
			get
			{
				return this.desc;
			}
		}

		/// <summary>Cancels the transaction and attempts to roll back the changes made by the events of the transaction.</summary>
		// Token: 0x06001936 RID: 6454 RVA: 0x00069BEE File Offset: 0x00067DEE
		public void Cancel()
		{
			if (!this.canceled && !this.committed)
			{
				this.canceled = true;
				GC.SuppressFinalize(this);
				this.suppressedFinalization = true;
				this.OnCancel();
			}
		}

		/// <summary>Commits this transaction.</summary>
		// Token: 0x06001937 RID: 6455 RVA: 0x00069C1A File Offset: 0x00067E1A
		public void Commit()
		{
			if (!this.committed && !this.canceled)
			{
				this.committed = true;
				GC.SuppressFinalize(this);
				this.suppressedFinalization = true;
				this.OnCommit();
			}
		}

		/// <summary>Raises the Cancel event.</summary>
		// Token: 0x06001938 RID: 6456
		protected abstract void OnCancel();

		/// <summary>Performs the actual work of committing a transaction.</summary>
		// Token: 0x06001939 RID: 6457
		protected abstract void OnCommit();

		// Token: 0x0600193A RID: 6458 RVA: 0x00069C48 File Offset: 0x00067E48
		~DesignerTransaction()
		{
			this.Dispose(false);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.DesignerTransaction" />. </summary>
		// Token: 0x0600193B RID: 6459 RVA: 0x00069C78 File Offset: 0x00067E78
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			if (!this.suppressedFinalization)
			{
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600193C RID: 6460 RVA: 0x00069C8F File Offset: 0x00067E8F
		protected virtual void Dispose(bool disposing)
		{
			this.Cancel();
		}

		// Token: 0x04001466 RID: 5222
		private bool committed;

		// Token: 0x04001467 RID: 5223
		private bool canceled;

		// Token: 0x04001468 RID: 5224
		private bool suppressedFinalization;

		// Token: 0x04001469 RID: 5225
		private string desc;
	}
}
