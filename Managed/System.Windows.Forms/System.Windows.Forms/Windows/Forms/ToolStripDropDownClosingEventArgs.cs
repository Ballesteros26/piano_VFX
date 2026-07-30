using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripDropDown.Closing" /> event.</summary>
	// Token: 0x0200034C RID: 844
	public class ToolStripDropDownClosingEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownClosingEventArgs" /> class with the specified reason for closing. </summary>
		/// <param name="reason">One of the <see cref="T:System.Windows.Forms.ToolStripDropDownCloseReason" /> values.</param>
		// Token: 0x06003C9E RID: 15518 RVA: 0x000F3D1C File Offset: 0x000F1F1C
		public ToolStripDropDownClosingEventArgs(ToolStripDropDownCloseReason reason)
		{
			this.close_reason = reason;
		}

		/// <summary>Gets the reason that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is closing.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripDropDownCloseReason" /> values.</returns>
		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06003C9F RID: 15519 RVA: 0x000F3D2C File Offset: 0x000F1F2C
		public ToolStripDropDownCloseReason CloseReason
		{
			get
			{
				return this.close_reason;
			}
		}

		// Token: 0x04001A77 RID: 6775
		private ToolStripDropDownCloseReason close_reason;
	}
}
