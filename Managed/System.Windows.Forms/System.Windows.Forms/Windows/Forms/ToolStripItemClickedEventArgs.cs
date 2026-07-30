using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStrip.ItemClicked" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000358 RID: 856
	public class ToolStripItemClickedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemClickedEventArgs" /> class, specifying the <see cref="T:System.Windows.Forms.ToolStripItem" /> that was clicked. </summary>
		/// <param name="clickedItem">The <see cref="T:System.Windows.Forms.ToolStripItem" /> that was clicked.</param>
		// Token: 0x06003DFC RID: 15868 RVA: 0x000F7848 File Offset: 0x000F5A48
		public ToolStripItemClickedEventArgs(ToolStripItem clickedItem)
		{
			this.clicked_item = clickedItem;
		}

		/// <summary>Gets the item that was clicked on the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> that was clicked.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x06003DFD RID: 15869 RVA: 0x000F7858 File Offset: 0x000F5A58
		public ToolStripItem ClickedItem
		{
			get
			{
				return this.clicked_item;
			}
		}

		// Token: 0x04001AE3 RID: 6883
		private ToolStripItem clicked_item;
	}
}
