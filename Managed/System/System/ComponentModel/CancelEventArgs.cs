using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for a cancelable event.</summary>
	// Token: 0x0200023B RID: 571
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class CancelEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CancelEventArgs" /> class with the <see cref="P:System.ComponentModel.CancelEventArgs.Cancel" /> property set to false.</summary>
		// Token: 0x0600128C RID: 4748 RVA: 0x0004DED5 File Offset: 0x0004C0D5
		public CancelEventArgs()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CancelEventArgs" /> class with the <see cref="P:System.ComponentModel.CancelEventArgs.Cancel" /> property set to the given value.</summary>
		/// <param name="cancel">true to cancel the event; otherwise, false. </param>
		// Token: 0x0600128D RID: 4749 RVA: 0x0004DEDE File Offset: 0x0004C0DE
		public CancelEventArgs(bool cancel)
		{
			this.cancel = cancel;
		}

		/// <summary>Gets or sets a value indicating whether the event should be canceled.</summary>
		/// <returns>true if the event should be canceled; otherwise, false.</returns>
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x0004DEED File Offset: 0x0004C0ED
		// (set) Token: 0x0600128F RID: 4751 RVA: 0x0004DEF5 File Offset: 0x0004C0F5
		public bool Cancel
		{
			get
			{
				return this.cancel;
			}
			set
			{
				this.cancel = value;
			}
		}

		// Token: 0x04001267 RID: 4711
		private bool cancel;
	}
}
