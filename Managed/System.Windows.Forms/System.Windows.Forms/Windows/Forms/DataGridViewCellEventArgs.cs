using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for <see cref="T:System.Windows.Forms.DataGridView" /> events related to cell and row operations.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000EB RID: 235
	public class DataGridViewCellEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> class. </summary>
		/// <param name="columnIndex">The index of the column containing the cell that the event occurs for.</param>
		/// <param name="rowIndex">The index of the row containing the cell that the event occurs for.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is less than -1.-or-<paramref name="rowIndex" /> is less than -1.</exception>
		// Token: 0x06001254 RID: 4692 RVA: 0x000482D4 File Offset: 0x000464D4
		public DataGridViewCellEventArgs(int columnIndex, int rowIndex)
		{
			this.columnIndex = columnIndex;
			this.rowIndex = rowIndex;
		}

		/// <summary>Gets a value indicating the column index of the cell that the event occurs for.</summary>
		/// <returns>The index of the column containing the cell that the event occurs for.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x000482EC File Offset: 0x000464EC
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
		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x000482F4 File Offset: 0x000464F4
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000AFB RID: 2811
		private int columnIndex;

		// Token: 0x04000AFC RID: 2812
		private int rowIndex;
	}
}
