using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellValidating" /> event of a <see cref="T:System.Windows.Forms.DataGridView" /> control. </summary>
	// Token: 0x020000F6 RID: 246
	public class DataGridViewCellValidatingEventArgs : CancelEventArgs
	{
		// Token: 0x060012AD RID: 4781 RVA: 0x00048EDC File Offset: 0x000470DC
		internal DataGridViewCellValidatingEventArgs(int columnIndex, int rowIndex, object formattedValue)
		{
			this.columnIndex = columnIndex;
			this.rowIndex = rowIndex;
			this.formattedValue = formattedValue;
		}

		/// <summary>Gets the column index of the cell that needs to be validated.</summary>
		/// <returns>A zero-based integer that specifies the column index of the cell that needs to be validated.</returns>
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x00048EFC File Offset: 0x000470FC
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		/// <summary>Gets the formatted contents of the cell that needs to be validated.</summary>
		/// <returns>A reference to the formatted value.</returns>
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x00048F04 File Offset: 0x00047104
		public object FormattedValue
		{
			get
			{
				return this.formattedValue;
			}
		}

		/// <summary>Gets the row index of the cell that needs to be validated.</summary>
		/// <returns>A zero-based integer that specifies the row index of the cell that needs to be validated.</returns>
		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x00048F0C File Offset: 0x0004710C
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000B31 RID: 2865
		private int columnIndex;

		// Token: 0x04000B32 RID: 2866
		private object formattedValue;

		// Token: 0x04000B33 RID: 2867
		private int rowIndex;
	}
}
