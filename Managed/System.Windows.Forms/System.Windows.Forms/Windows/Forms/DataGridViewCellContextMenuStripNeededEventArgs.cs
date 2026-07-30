using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellContextMenuStripNeeded" /> event. </summary>
	// Token: 0x020000E9 RID: 233
	public class DataGridViewCellContextMenuStripNeededEventArgs : DataGridViewCellEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventArgs" /> class. </summary>
		/// <param name="columnIndex">The column index of cell that the event occurred for.</param>
		/// <param name="rowIndex">The row index of the cell that the event occurred for.</param>
		// Token: 0x0600124E RID: 4686 RVA: 0x0004828C File Offset: 0x0004648C
		public DataGridViewCellContextMenuStripNeededEventArgs(int columnIndex, int rowIndex)
			: base(columnIndex, rowIndex)
		{
		}

		/// <summary>Gets or sets the shortcut menu for the cell that raised the <see cref="E:System.Windows.Forms.DataGridView.CellContextMenuStripNeeded" /> event.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> for the cell. </returns>
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x00048298 File Offset: 0x00046498
		// (set) Token: 0x06001250 RID: 4688 RVA: 0x000482A0 File Offset: 0x000464A0
		public ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return this.contextMenuStrip;
			}
			set
			{
				this.contextMenuStrip = value;
			}
		}

		// Token: 0x04000AF9 RID: 2809
		private ContextMenuStrip contextMenuStrip;
	}
}
