using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Form.FormClosing" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200019A RID: 410
	public class FormClosingEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FormClosingEventArgs" /> class.</summary>
		/// <param name="closeReason">A <see cref="T:System.Windows.Forms.CloseReason" /> value that represents the reason why the form is being closed.</param>
		/// <param name="cancel">true to cancel the event; otherwise, false.</param>
		// Token: 0x06001B00 RID: 6912 RVA: 0x00069574 File Offset: 0x00067774
		public FormClosingEventArgs(CloseReason closeReason, bool cancel)
			: base(cancel)
		{
			this.close_reason = closeReason;
		}

		/// <summary>Gets a value that indicates why the form is being closed.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.CloseReason" /> enumerated values. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001B01 RID: 6913 RVA: 0x00069584 File Offset: 0x00067784
		public CloseReason CloseReason
		{
			get
			{
				return this.close_reason;
			}
		}

		// Token: 0x04000EF2 RID: 3826
		private CloseReason close_reason;
	}
}
