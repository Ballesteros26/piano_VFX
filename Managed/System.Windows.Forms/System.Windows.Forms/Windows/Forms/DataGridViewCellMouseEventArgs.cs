using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for mouse events raised by a <see cref="T:System.Windows.Forms.DataGridView" /> whenever the mouse is moved within a <see cref="T:System.Windows.Forms.DataGridViewCell" />. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000ED RID: 237
	public class DataGridViewCellMouseEventArgs : MouseEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> class.</summary>
		/// <param name="columnIndex">The cell's zero-based column index.</param>
		/// <param name="rowIndex">The cell's zero-based row index.</param>
		/// <param name="localX">The x-coordinate of the mouse, in pixels.</param>
		/// <param name="localY">The y-coordinate of the mouse, in pixels.</param>
		/// <param name="e">The originating <see cref="T:System.Windows.Forms.MouseEventArgs" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is less than -1.-or-<paramref name="rowIndex" /> is less than -1.</exception>
		// Token: 0x0600125E RID: 4702 RVA: 0x00048358 File Offset: 0x00046558
		public DataGridViewCellMouseEventArgs(int columnIndex, int rowIndex, int localX, int localY, MouseEventArgs e)
			: base(e.Button, e.Clicks, localX, localY, e.Delta)
		{
			this.columnIndex = columnIndex;
			this.rowIndex = rowIndex;
		}

		/// <summary>Gets the zero-based column index of the cell.</summary>
		/// <returns>An integer specifying the column index.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x00048394 File Offset: 0x00046594
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		/// <summary>Gets the zero-based row index of the cell.</summary>
		/// <returns>An integer specifying the row index.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x0004839C File Offset: 0x0004659C
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000B01 RID: 2817
		private int columnIndex;

		// Token: 0x04000B02 RID: 2818
		private int rowIndex;
	}
}
