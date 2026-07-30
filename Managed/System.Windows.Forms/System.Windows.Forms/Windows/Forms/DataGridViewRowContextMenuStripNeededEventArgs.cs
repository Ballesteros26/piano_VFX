using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowContextMenuStripNeeded" /> event. </summary>
	// Token: 0x02000125 RID: 293
	public class DataGridViewRowContextMenuStripNeededEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventArgs" /> class. </summary>
		/// <param name="rowIndex">The index of the row.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than -1.</exception>
		// Token: 0x06001530 RID: 5424 RVA: 0x0004FE08 File Offset: 0x0004E008
		public DataGridViewRowContextMenuStripNeededEventArgs(int rowIndex)
		{
			this.rowIndex = rowIndex;
		}

		/// <summary>Gets or sets the shortcut menu for the row that raised the <see cref="E:System.Windows.Forms.DataGridView.RowContextMenuStripNeeded" /> event.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> in use.</returns>
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x0004FE18 File Offset: 0x0004E018
		// (set) Token: 0x06001532 RID: 5426 RVA: 0x0004FE20 File Offset: 0x0004E020
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

		/// <summary>Gets the index of the row that is requesting a shortcut menu.</summary>
		/// <returns>The zero-based index of the row that is requesting a shortcut menu.</returns>
		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x0004FE2C File Offset: 0x0004E02C
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000BF6 RID: 3062
		private int rowIndex;

		// Token: 0x04000BF7 RID: 3063
		private ContextMenuStrip contextMenuStrip;
	}
}
