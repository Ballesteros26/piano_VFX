using System;

namespace System.Windows.Forms
{
	/// <summary>Defines values for specifying how the height of a row is adjusted. </summary>
	// Token: 0x020000DC RID: 220
	public enum DataGridViewAutoSizeRowMode
	{
		/// <summary>The row height adjusts to fit the contents of the row header. </summary>
		// Token: 0x04000ABA RID: 2746
		RowHeader = 1,
		/// <summary>The row height adjusts to fit the contents of all cells in the row, excluding the header cell. </summary>
		// Token: 0x04000ABB RID: 2747
		AllCellsExceptHeader,
		/// <summary>The row height adjusts to fit the contents of all cells in the row, including the header cell. </summary>
		// Token: 0x04000ABC RID: 2748
		AllCells
	}
}
