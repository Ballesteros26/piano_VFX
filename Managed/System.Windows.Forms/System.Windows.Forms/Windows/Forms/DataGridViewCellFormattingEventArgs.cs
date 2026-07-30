using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellFormatting" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000EC RID: 236
	public class DataGridViewCellFormattingEventArgs : ConvertEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs" /> class.</summary>
		/// <param name="columnIndex">The column index of the cell that caused the event.</param>
		/// <param name="rowIndex">The row index of the cell that caused the event.</param>
		/// <param name="value">The cell's contents.</param>
		/// <param name="desiredType">The type to convert <paramref name="value" /> to. </param>
		/// <param name="cellStyle">The style of the cell that caused the event.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is less than -1-or-<paramref name="rowIndex" /> is less than -1.</exception>
		// Token: 0x06001257 RID: 4695 RVA: 0x000482FC File Offset: 0x000464FC
		public DataGridViewCellFormattingEventArgs(int columnIndex, int rowIndex, object value, Type desiredType, DataGridViewCellStyle cellStyle)
			: base(value, desiredType)
		{
			this.columnIndex = columnIndex;
			this.rowIndex = rowIndex;
			this.cellStyle = cellStyle;
		}

		/// <summary>Gets or sets the style of the cell that is being formatted.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the display style of the cell being formatted. The default is the value of the cell's <see cref="P:System.Windows.Forms.DataGridViewCell.InheritedStyle" /> property. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x00048320 File Offset: 0x00046520
		// (set) Token: 0x06001259 RID: 4697 RVA: 0x00048328 File Offset: 0x00046528
		public DataGridViewCellStyle CellStyle
		{
			get
			{
				return this.cellStyle;
			}
			set
			{
				this.cellStyle = value;
			}
		}

		/// <summary>Gets the column index of the cell that is being formatted.</summary>
		/// <returns>The column index of the cell that is being formatted.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x00048334 File Offset: 0x00046534
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		/// <summary>Gets or sets a value indicating whether the cell value has been successfully formatted.</summary>
		/// <returns>true if the formatting for the cell value has been handled; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x0004833C File Offset: 0x0004653C
		// (set) Token: 0x0600125C RID: 4700 RVA: 0x00048344 File Offset: 0x00046544
		public bool FormattingApplied
		{
			get
			{
				return this.formattingApplied;
			}
			set
			{
				this.formattingApplied = value;
			}
		}

		/// <summary>Gets the row index of the cell that is being formatted.</summary>
		/// <returns>The row index of the cell that is being formatted.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x00048350 File Offset: 0x00046550
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000AFD RID: 2813
		private int columnIndex;

		// Token: 0x04000AFE RID: 2814
		private DataGridViewCellStyle cellStyle;

		// Token: 0x04000AFF RID: 2815
		private bool formattingApplied;

		// Token: 0x04000B00 RID: 2816
		private int rowIndex;
	}
}
