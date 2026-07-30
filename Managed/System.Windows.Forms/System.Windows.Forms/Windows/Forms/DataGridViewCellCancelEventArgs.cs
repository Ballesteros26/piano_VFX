using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for <see cref="E:System.Windows.Forms.DataGridView.CellBeginEdit" /> and <see cref="E:System.Windows.Forms.DataGridView.RowValidating" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000E7 RID: 231
	public class DataGridViewCellCancelEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs" /> class. </summary>
		/// <param name="columnIndex">The index of the column containing the cell that the event occurs for.</param>
		/// <param name="rowIndex">The index of the row containing the cell that the event occurs for.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is less than -1.-or-<paramref name="rowIndex" /> is less than -1.</exception>
		// Token: 0x0600122D RID: 4653 RVA: 0x00047DCC File Offset: 0x00045FCC
		public DataGridViewCellCancelEventArgs(int columnIndex, int rowIndex)
		{
			this.columnIndex = columnIndex;
			this.rowIndex = rowIndex;
		}

		/// <summary>Gets the column index of the cell that the event occurs for.</summary>
		/// <returns>The column index of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that the event occurs for.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x00047DE4 File Offset: 0x00045FE4
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		/// <summary>Gets the row index of the cell that the event occurs for.</summary>
		/// <returns>The row index of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that the event occurs for.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x00047DEC File Offset: 0x00045FEC
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000AF5 RID: 2805
		private int columnIndex;

		// Token: 0x04000AF6 RID: 2806
		private int rowIndex;
	}
}
