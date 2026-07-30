using System;

namespace System.Windows.Forms
{
	/// <summary>Defines values for specifying how the width of a column is adjusted. </summary>
	// Token: 0x020000D7 RID: 215
	public enum DataGridViewAutoSizeColumnMode
	{
		/// <summary>The sizing behavior of the column is inherited from the <see cref="P:System.Windows.Forms.DataGridView.AutoSizeColumnsMode" /> property.</summary>
		// Token: 0x04000AA5 RID: 2725
		NotSet,
		/// <summary>The column width does not automatically adjust.</summary>
		// Token: 0x04000AA6 RID: 2726
		None,
		/// <summary>The column width adjusts to fit the contents of the column header cell. </summary>
		// Token: 0x04000AA7 RID: 2727
		ColumnHeader,
		/// <summary>The column width adjusts to fit the contents of all cells in the column, excluding the header cell. </summary>
		// Token: 0x04000AA8 RID: 2728
		AllCellsExceptHeader = 4,
		/// <summary>The column width adjusts to fit the contents of all cells in the column, including the header cell. </summary>
		// Token: 0x04000AA9 RID: 2729
		AllCells = 6,
		/// <summary>The column width adjusts to fit the contents of all cells in the column that are in rows currently displayed onscreen, excluding the header cell. </summary>
		// Token: 0x04000AAA RID: 2730
		DisplayedCellsExceptHeader = 8,
		/// <summary>The column width adjusts to fit the contents of all cells in the column that are in rows currently displayed onscreen, including the header cell. </summary>
		// Token: 0x04000AAB RID: 2731
		DisplayedCells = 10,
		/// <summary>The column width adjusts so that the widths of all columns exactly fills the display area of the control, requiring horizontal scrolling only to keep column widths above the <see cref="P:System.Windows.Forms.DataGridViewColumn.MinimumWidth" /> property values. Relative column widths are determined by the relative <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> property values.</summary>
		// Token: 0x04000AAC RID: 2732
		Fill = 16
	}
}
