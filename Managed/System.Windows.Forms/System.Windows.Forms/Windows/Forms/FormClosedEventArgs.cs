using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Form.FormClosed" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000199 RID: 409
	public class FormClosedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FormClosedEventArgs" /> class.</summary>
		/// <param name="closeReason">A <see cref="T:System.Windows.Forms.CloseReason" /> value that represents the reason why the form was closed.</param>
		// Token: 0x06001AFE RID: 6910 RVA: 0x0006955C File Offset: 0x0006775C
		public FormClosedEventArgs(CloseReason closeReason)
		{
			this.close_reason = closeReason;
		}

		/// <summary>Gets a value that indicates why the form was closed. </summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.CloseReason" /> enumerated values. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001AFF RID: 6911 RVA: 0x0006956C File Offset: 0x0006776C
		public CloseReason CloseReason
		{
			get
			{
				return this.close_reason;
			}
		}

		// Token: 0x04000EF1 RID: 3825
		private CloseReason close_reason;
	}
}
