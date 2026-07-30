using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ColumnWidthChanged" /> event. </summary>
	// Token: 0x0200008F RID: 143
	public class ColumnWidthChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnWidthChangedEventArgs" /> class. </summary>
		/// <param name="columnIndex">The index of the column whose width is being changed.</param>
		// Token: 0x06000667 RID: 1639 RVA: 0x0001D844 File Offset: 0x0001BA44
		public ColumnWidthChangedEventArgs(int columnIndex)
		{
			this.column_index = columnIndex;
		}

		/// <summary>Gets the column index for the column whose width is being changed.</summary>
		/// <returns>The index of the column whose width is being changed.</returns>
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0001D854 File Offset: 0x0001BA54
		public int ColumnIndex
		{
			get
			{
				return this.column_index;
			}
		}

		// Token: 0x0400073E RID: 1854
		private int column_index;
	}
}
