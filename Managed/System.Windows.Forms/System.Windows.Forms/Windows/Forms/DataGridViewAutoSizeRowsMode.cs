using System;

namespace System.Windows.Forms
{
	/// <summary>Defines values for specifying how the heights of rows are adjusted. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000DD RID: 221
	public enum DataGridViewAutoSizeRowsMode
	{
		/// <summary>The row heights do not automatically adjust.</summary>
		// Token: 0x04000ABE RID: 2750
		None,
		/// <summary>The row heights adjust to fit the contents of the row header. </summary>
		// Token: 0x04000ABF RID: 2751
		AllHeaders = 5,
		/// <summary>The row heights adjust to fit the contents of all cells in the rows, excluding header cells. </summary>
		// Token: 0x04000AC0 RID: 2752
		AllCellsExceptHeaders,
		/// <summary>The row heights adjust to fit the contents of all cells in the rows, including header cells. </summary>
		// Token: 0x04000AC1 RID: 2753
		AllCells,
		/// <summary>The row heights adjust to fit the contents of the row headers currently displayed onscreen.</summary>
		// Token: 0x04000AC2 RID: 2754
		DisplayedHeaders = 9,
		/// <summary>The row heights adjust to fit the contents of all cells in rows currently displayed onscreen, excluding header cells. </summary>
		// Token: 0x04000AC3 RID: 2755
		DisplayedCellsExceptHeaders,
		/// <summary>The row heights adjust to fit the contents of all cells in rows currently displayed onscreen, including header cells. </summary>
		// Token: 0x04000AC4 RID: 2756
		DisplayedCells
	}
}
