using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripDropDown.Closed" /> event. </summary>
	// Token: 0x0200034B RID: 843
	public class ToolStripDropDownClosedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownClosedEventArgs" /> class. </summary>
		/// <param name="reason">One of the <see cref="T:System.Windows.Forms.ToolStripDropDownCloseReason" /> values.</param>
		// Token: 0x06003C9C RID: 15516 RVA: 0x000F3D04 File Offset: 0x000F1F04
		public ToolStripDropDownClosedEventArgs(ToolStripDropDownCloseReason reason)
		{
			this.close_reason = reason;
		}

		/// <summary>Gets the reason that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> closed.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripDropDownCloseReason" /> values.</returns>
		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06003C9D RID: 15517 RVA: 0x000F3D14 File Offset: 0x000F1F14
		public ToolStripDropDownCloseReason CloseReason
		{
			get
			{
				return this.close_reason;
			}
		}

		// Token: 0x04001A76 RID: 6774
		private ToolStripDropDownCloseReason close_reason;
	}
}
