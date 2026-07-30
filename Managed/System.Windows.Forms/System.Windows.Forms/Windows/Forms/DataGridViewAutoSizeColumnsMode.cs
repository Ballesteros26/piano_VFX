using System;

namespace System.Windows.Forms
{
	/// <summary>Defines values for specifying how the widths of columns are adjusted. </summary>
	// Token: 0x020000D9 RID: 217
	public enum DataGridViewAutoSizeColumnsMode
	{
		/// <summary>The column widths do not automatically adjust. </summary>
		// Token: 0x04000AB0 RID: 2736
		None = 1,
		/// <summary>The column widths adjust to fit the contents of the column header cells. </summary>
		// Token: 0x04000AB1 RID: 2737
		ColumnHeader,
		/// <summary>The column widths adjust to fit the contents of all cells in the columns, excluding header cells. </summary>
		// Token: 0x04000AB2 RID: 2738
		AllCellsExceptHeader = 4,
		/// <summary>The column widths adjust to fit the contents of all cells in the columns, including header cells. </summary>
		// Token: 0x04000AB3 RID: 2739
		AllCells = 6,
		/// <summary>The column widths adjust to fit the contents of all cells in the columns that are in rows currently displayed onscreen, excluding header cells. </summary>
		// Token: 0x04000AB4 RID: 2740
		DisplayedCellsExceptHeader = 8,
		/// <summary>The column widths adjust to fit the contents of all cells in the columns that are in rows currently displayed onscreen, including header cells. </summary>
		// Token: 0x04000AB5 RID: 2741
		DisplayedCells = 10,
		/// <summary>The column widths adjust so that the widths of all columns exactly fill the display area of the control, requiring horizontal scrolling only to keep column widths above the <see cref="P:System.Windows.Forms.DataGridViewColumn.MinimumWidth" /> property values. Relative column widths are determined by the relative <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> property values.</summary>
		// Token: 0x04000AB6 RID: 2742
		Fill = 16
	}
}
