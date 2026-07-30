using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellParsing" /> event of a <see cref="T:System.Windows.Forms.DataGridView" /> control. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000EF RID: 239
	public class DataGridViewCellParsingEventArgs : ConvertEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs" /> class. </summary>
		/// <param name="rowIndex">The row index of the cell that was changed.</param>
		/// <param name="columnIndex">The column index of the cell that was changed.</param>
		/// <param name="value">The new value.</param>
		/// <param name="desiredType">The type of the new value.</param>
		/// <param name="inheritedCellStyle">The style applied to the cell that was changed.</param>
		// Token: 0x06001271 RID: 4721 RVA: 0x000485F0 File Offset: 0x000467F0
		public DataGridViewCellParsingEventArgs(int rowIndex, int columnIndex, object value, Type desiredType, DataGridViewCellStyle inheritedCellStyle)
			: base(value, desiredType)
		{
			this.columnIndex = columnIndex;
			this.rowIndex = rowIndex;
			this.inheritedCellStyle = inheritedCellStyle;
		}

		/// <summary>Gets or sets the style applied to the edited cell.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the current style of the cell being edited. The default value is the value of the cell <see cref="P:System.Windows.Forms.DataGridViewCell.InheritedStyle" /> property.</returns>
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x00048614 File Offset: 0x00046814
		// (set) Token: 0x06001273 RID: 4723 RVA: 0x0004861C File Offset: 0x0004681C
		public DataGridViewCellStyle InheritedCellStyle
		{
			get
			{
				return this.inheritedCellStyle;
			}
			set
			{
				this.inheritedCellStyle = value;
			}
		}

		/// <summary>Gets the column index of the cell data that requires parsing.</summary>
		/// <returns>The column index of the cell that was changed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x00048628 File Offset: 0x00046828
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		/// <summary>Gets or sets a value indicating whether a cell's value has been successfully parsed.</summary>
		/// <returns>true if the cell's value has been successfully parsed; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001275 RID: 4725 RVA: 0x00048630 File Offset: 0x00046830
		// (set) Token: 0x06001276 RID: 4726 RVA: 0x00048638 File Offset: 0x00046838
		public bool ParsingApplied
		{
			get
			{
				return this.parsingApplied;
			}
			set
			{
				this.parsingApplied = value;
			}
		}

		/// <summary>Gets the row index of the cell that requires parsing.</summary>
		/// <returns>The row index of the cell that was changed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001277 RID: 4727 RVA: 0x00048644 File Offset: 0x00046844
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000B10 RID: 2832
		private int columnIndex;

		// Token: 0x04000B11 RID: 2833
		private DataGridViewCellStyle inheritedCellStyle;

		// Token: 0x04000B12 RID: 2834
		private bool parsingApplied;

		// Token: 0x04000B13 RID: 2835
		private int rowIndex;
	}
}
