using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosed" /> and <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosing" /> events.</summary>
	// Token: 0x02000317 RID: 791
	[ComVisible(true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class DesignerTransactionCloseEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransactionCloseEventArgs" /> class, using the specified value that indicates whether the designer called <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on the transaction.</summary>
		/// <param name="commit">A value indicating whether the transaction was committed.</param>
		// Token: 0x0600193D RID: 6461 RVA: 0x00069C97 File Offset: 0x00067E97
		[Obsolete("This constructor is obsolete. Use DesignerTransactionCloseEventArgs(bool, bool) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public DesignerTransactionCloseEventArgs(bool commit)
			: this(commit, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransactionCloseEventArgs" /> class. </summary>
		/// <param name="commit">A value indicating whether the transaction was committed.</param>
		/// <param name="lastTransaction">true if this is the last transaction to close; otherwise, false.</param>
		// Token: 0x0600193E RID: 6462 RVA: 0x00069CA1 File Offset: 0x00067EA1
		public DesignerTransactionCloseEventArgs(bool commit, bool lastTransaction)
		{
			this.commit = commit;
			this.lastTransaction = lastTransaction;
		}

		/// <summary>Indicates whether the designer called <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on the transaction.</summary>
		/// <returns>true if the designer called <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on the transaction; otherwise, false.</returns>
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x0600193F RID: 6463 RVA: 0x00069CB7 File Offset: 0x00067EB7
		public bool TransactionCommitted
		{
			get
			{
				return this.commit;
			}
		}

		/// <summary>Gets a value indicating whether this is the last transaction to close.</summary>
		/// <returns>true, if this is the last transaction to close; otherwise, false. </returns>
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001940 RID: 6464 RVA: 0x00069CBF File Offset: 0x00067EBF
		public bool LastTransaction
		{
			get
			{
				return this.lastTransaction;
			}
		}

		// Token: 0x0400146A RID: 5226
		private bool commit;

		// Token: 0x0400146B RID: 5227
		private bool lastTransaction;
	}
}
