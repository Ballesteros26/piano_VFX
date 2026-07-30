using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.ControlAdded" /> and <see cref="E:System.Windows.Forms.Control.ControlRemoved" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000AD RID: 173
	public class ControlEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ControlEventArgs" /> class for the specified control.</summary>
		/// <param name="control">The <see cref="T:System.Windows.Forms.Control" /> to store in this event. </param>
		// Token: 0x06000ABA RID: 2746 RVA: 0x0002C51C File Offset: 0x0002A71C
		public ControlEventArgs(Control control)
		{
			this.control = control;
		}

		/// <summary>Gets the control object used by this event.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> used by this event.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0002C52C File Offset: 0x0002A72C
		public Control Control
		{
			get
			{
				return this.control;
			}
		}

		// Token: 0x0400083B RID: 2107
		private Control control;
	}
}
