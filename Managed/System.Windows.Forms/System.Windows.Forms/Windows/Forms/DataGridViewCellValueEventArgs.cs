using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellValueNeeded" /> and <see cref="E:System.Windows.Forms.DataGridView.CellValuePushed" /> events of the <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000F7 RID: 247
	public class DataGridViewCellValueEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellValueEventArgs" /> class. </summary>
		/// <param name="columnIndex">The index of the column containing the cell that the event occurs for.</param>
		/// <param name="rowIndex">The index of the row containing the cell that the event occurs for.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is less than 0.-or-<paramref name="rowIndex" /> is less than 0.</exception>
		// Token: 0x060012B1 RID: 4785 RVA: 0x00048F14 File Offset: 0x00047114
		public DataGridViewCellValueEventArgs(int columnIndex, int rowIndex)
		{
			this.columnIndex = columnIndex;
			this.rowIndex = rowIndex;
		}

		/// <summary>Gets a value indicating the column index of the cell that the event occurs for.</summary>
		/// <returns>The index of the column containing the cell that the event occurs for.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x00048F2C File Offset: 0x0004712C
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		/// <summary>Gets a value indicating the row index of the cell that the event occurs for.</summary>
		/// <returns>The index of the row containing the cell that the event occurs for.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x00048F34 File Offset: 0x00047134
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets or sets the value of the cell that the event occurs for.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the cell's value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x00048F3C File Offset: 0x0004713C
		// (set) Token: 0x060012B5 RID: 4789 RVA: 0x00048F44 File Offset: 0x00047144
		public object Value
		{
			get
			{
				return this.cellValue;
			}
			set
			{
				this.cellValue = value;
			}
		}

		// Token: 0x04000B34 RID: 2868
		private int columnIndex;

		// Token: 0x04000B35 RID: 2869
		private int rowIndex;

		// Token: 0x04000B36 RID: 2870
		private object cellValue;
	}
}
