using System;

namespace System.Windows.Forms
{
	/// <summary>Identifies a cell in the grid.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C4 RID: 196
	public struct DataGridCell
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridCell" /> class.</summary>
		/// <param name="r">The number of a row in the <see cref="T:System.Windows.Forms.DataGrid" />. </param>
		/// <param name="c">The number of a column in the <see cref="T:System.Windows.Forms.DataGrid" />. </param>
		// Token: 0x06000D13 RID: 3347 RVA: 0x00035E1C File Offset: 0x0003401C
		public DataGridCell(int r, int c)
		{
			this.row = r;
			this.column = c;
		}

		/// <summary>Gets or sets the number of a column in the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
		/// <returns>The number of the column.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x00035E2C File Offset: 0x0003402C
		// (set) Token: 0x06000D15 RID: 3349 RVA: 0x00035E34 File Offset: 0x00034034
		public int ColumnNumber
		{
			get
			{
				return this.column;
			}
			set
			{
				this.column = value;
			}
		}

		/// <summary>Gets or sets the number of a row in the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
		/// <returns>The number of the row.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x00035E40 File Offset: 0x00034040
		// (set) Token: 0x06000D17 RID: 3351 RVA: 0x00035E48 File Offset: 0x00034048
		public int RowNumber
		{
			get
			{
				return this.row;
			}
			set
			{
				this.row = value;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.DataGridCell" /> is identical to a second <see cref="T:System.Windows.Forms.DataGridCell" />.</summary>
		/// <returns>true if the second object is identical to the first; otherwise, false.</returns>
		/// <param name="o">An object you are to comparing. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D18 RID: 3352 RVA: 0x00035E54 File Offset: 0x00034054
		public override bool Equals(object o)
		{
			if (!(o is DataGridCell))
			{
				return false;
			}
			DataGridCell dataGridCell = (DataGridCell)o;
			return dataGridCell.ColumnNumber == this.column && dataGridCell.RowNumber == this.row;
		}

		/// <summary>Gets a hash value that can be added to a <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>A number that uniquely identifies the <see cref="T:System.Windows.Forms.DataGridCell" /> in a <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D19 RID: 3353 RVA: 0x00035E9C File Offset: 0x0003409C
		public override int GetHashCode()
		{
			return this.row ^ this.column;
		}

		/// <summary>Gets the row number and column number of the cell.</summary>
		/// <returns>A string containing the row number and column number.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D1A RID: 3354 RVA: 0x00035EAC File Offset: 0x000340AC
		public override string ToString()
		{
			return string.Concat(new object[] { "DataGridCell {RowNumber = ", this.row, ", ColumnNumber = ", this.column, "}" });
		}

		// Token: 0x04000956 RID: 2390
		private int row;

		// Token: 0x04000957 RID: 2391
		private int column;
	}
}
