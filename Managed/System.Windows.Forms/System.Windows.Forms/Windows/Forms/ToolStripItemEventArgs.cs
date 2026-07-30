using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for <see cref="T:System.Windows.Forms.ToolStripItem" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200035B RID: 859
	public class ToolStripItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemEventArgs" /> class, specifying a <see cref="T:System.Windows.Forms.ToolStripItem" />. </summary>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" /> for which to specify events.</param>
		// Token: 0x06003E22 RID: 15906 RVA: 0x000F7FB4 File Offset: 0x000F61B4
		public ToolStripItemEventArgs(ToolStripItem item)
		{
			this.item = item;
		}

		/// <summary>Gets a <see cref="T:System.Windows.Forms.ToolStripItem" /> for which to handle events.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x06003E23 RID: 15907 RVA: 0x000F7FC4 File Offset: 0x000F61C4
		public ToolStripItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x04001AEB RID: 6891
		private ToolStripItem item;
	}
}
