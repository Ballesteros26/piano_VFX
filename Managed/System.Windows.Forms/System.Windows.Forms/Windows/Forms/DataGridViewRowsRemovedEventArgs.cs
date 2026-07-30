using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowsRemoved" /> event.</summary>
	// Token: 0x02000132 RID: 306
	public class DataGridViewRowsRemovedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowsRemovedEventArgs" /> class.</summary>
		/// <param name="rowIndex">The zero-based index of the row that was deleted, or the first deleted row if multiple rows were deleted. </param>
		/// <param name="rowCount">The number of rows that were deleted.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than 0.-or-<paramref name="rowCount" /> is less than 1.</exception>
		// Token: 0x0600158F RID: 5519 RVA: 0x00050CF0 File Offset: 0x0004EEF0
		public DataGridViewRowsRemovedEventArgs(int rowIndex, int rowCount)
		{
			this.rowIndex = rowIndex;
			this.rowCount = rowCount;
		}

		/// <summary>Gets the number of rows that were deleted.</summary>
		/// <returns>The number of deleted rows.</returns>
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x00050D08 File Offset: 0x0004EF08
		public int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		/// <summary>Gets the zero-based index of the row deleted, or the first deleted row if multiple rows were deleted.</summary>
		/// <returns>The zero-based index of the row that was deleted, or the first deleted row if multiple rows were deleted.</returns>
		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x00050D10 File Offset: 0x0004EF10
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000C22 RID: 3106
		private int rowIndex;

		// Token: 0x04000C23 RID: 3107
		private int rowCount;
	}
}
