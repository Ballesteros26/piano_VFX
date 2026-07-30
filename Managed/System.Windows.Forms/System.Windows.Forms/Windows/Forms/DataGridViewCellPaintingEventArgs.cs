using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellPainting" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000EE RID: 238
	public class DataGridViewCellPaintingEventArgs : HandledEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellPaintingEventArgs" /> class. </summary>
		/// <param name="dataGridView">The <see cref="T:System.Windows.Forms.DataGridView" /> that contains the cell to be painted.</param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
		/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="rowIndex">The row index of the cell that is being painted.</param>
		/// <param name="columnIndex">The column index of the cell that is being painted.</param>
		/// <param name="cellState">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the cell.</param>
		/// <param name="value">The data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="formattedValue">The formatted data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="errorText">An error message that is associated with the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the cell.</param>
		/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles for the cell that is being painted.</param>
		/// <param name="paintParts">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values specifying the parts to paint.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridView" /> is null.-or-<paramref name="graphics" /> is null.-or-<paramref name="cellStyle" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="paintParts" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values.</exception>
		// Token: 0x06001261 RID: 4705 RVA: 0x000483A4 File Offset: 0x000465A4
		public DataGridViewCellPaintingEventArgs(DataGridView dataGridView, Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, int columnIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			this.dataGridView = dataGridView;
			this.graphics = graphics;
			this.clipBounds = clipBounds;
			this.cellBounds = cellBounds;
			this.rowIndex = rowIndex;
			this.columnIndex = columnIndex;
			this.cellState = cellState;
			this.cellValue = value;
			this.formattedValue = formattedValue;
			this.errorText = errorText;
			this.cellStyle = cellStyle;
			this.advancedBorderStyle = advancedBorderStyle;
			this.paintParts = paintParts;
		}

		/// <summary>Gets the border style of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that represents the border style of the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x0004841C File Offset: 0x0004661C
		public DataGridViewAdvancedBorderStyle AdvancedBorderStyle
		{
			get
			{
				return this.advancedBorderStyle;
			}
		}

		/// <summary>Get the bounds of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x00048424 File Offset: 0x00046624
		public Rectangle CellBounds
		{
			get
			{
				return this.cellBounds;
			}
		}

		/// <summary>Gets the cell style of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains the cell style of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x0004842C File Offset: 0x0004662C
		public DataGridViewCellStyle CellStyle
		{
			get
			{
				return this.cellStyle;
			}
		}

		/// <summary>Gets the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x00048434 File Offset: 0x00046634
		public Rectangle ClipBounds
		{
			get
			{
				return this.clipBounds;
			}
		}

		/// <summary>Gets the column index of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>The column index of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x0004843C File Offset: 0x0004663C
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		/// <summary>Gets a string that represents an error message for the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>A string that represents an error message for the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x00048444 File Offset: 0x00046644
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
		}

		/// <summary>Gets the formatted value of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>The formatted value of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x0004844C File Offset: 0x0004664C
		public object FormattedValue
		{
			get
			{
				return this.formattedValue;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> used to paint the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x00048454 File Offset: 0x00046654
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>The cell parts that are to be painted.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values specifying the parts to be painted.</returns>
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x0004845C File Offset: 0x0004665C
		public DataGridViewPaintParts PaintParts
		{
			get
			{
				return this.paintParts;
			}
		}

		/// <summary>Gets the row index of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>The row index of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x00048464 File Offset: 0x00046664
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets the state of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the cell.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x0004846C File Offset: 0x0004666C
		public DataGridViewElementStates State
		{
			get
			{
				return this.cellState;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>The value of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x00048474 File Offset: 0x00046674
		public object Value
		{
			get
			{
				return this.cellValue;
			}
		}

		/// <summary>Paints the specified parts of the cell for the area in the specified bounds.</summary>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area of the <see cref="T:System.Windows.Forms.DataGridView" /> to be painted.</param>
		/// <param name="paintParts">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values specifying the parts to paint.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.RowIndex" /> is less than -1 or greater than or equal to the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-<see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.ColumnIndex" /> is less than -1 or greater than or equal to the number of columns in the <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x0600126E RID: 4718 RVA: 0x0004847C File Offset: 0x0004667C
		public void Paint(Rectangle clipBounds, DataGridViewPaintParts paintParts)
		{
			if (this.rowIndex < -1 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException("Invalid \"RowIndex.\"");
			}
			if (this.columnIndex < -1 || this.columnIndex >= this.dataGridView.Columns.Count)
			{
				throw new InvalidOperationException("Invalid \"ColumnIndex.\"");
			}
			DataGridViewCell dataGridViewCell;
			if (this.rowIndex == -1 && this.columnIndex == -1)
			{
				dataGridViewCell = this.dataGridView.TopLeftHeaderCell;
			}
			else if (this.rowIndex == -1)
			{
				dataGridViewCell = this.dataGridView.Columns[this.columnIndex].HeaderCell;
			}
			else if (this.columnIndex == -1)
			{
				dataGridViewCell = this.dataGridView.Rows[this.rowIndex].HeaderCell;
			}
			else
			{
				dataGridViewCell = this.dataGridView.Rows[this.rowIndex].Cells[this.columnIndex];
			}
			dataGridViewCell.PaintInternal(this.graphics, clipBounds, this.cellBounds, this.rowIndex, this.cellState, this.Value, this.formattedValue, this.errorText, this.cellStyle, this.advancedBorderStyle, paintParts);
		}

		/// <summary>Paints the cell background for the area in the specified bounds.</summary>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area of the <see cref="T:System.Windows.Forms.DataGridView" /> to be painted.</param>
		/// <param name="cellsPaintSelectionBackground">true to paint the background of the specified bounds with the color of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.SelectionBackColor" /> property of the <see cref="P:System.Windows.Forms.DataGridViewCell.InheritedStyle" />; false to paint the background of the specified bounds with the color of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.BackColor" /> property of the <see cref="P:System.Windows.Forms.DataGridViewCell.InheritedStyle" />.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.RowIndex" /> is less than -1 or greater than or equal to the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-<see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.ColumnIndex" /> is less than -1 or greater than or equal to the number of columns in the <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x0600126F RID: 4719 RVA: 0x000485D8 File Offset: 0x000467D8
		public void PaintBackground(Rectangle clipBounds, bool cellsPaintSelectionBackground)
		{
			this.Paint(clipBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);
		}

		/// <summary>Paints the cell content for the area in the specified bounds.</summary>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area of the <see cref="T:System.Windows.Forms.DataGridView" /> to be painted.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.RowIndex" /> is less than -1 or greater than or equal to the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-<see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.ColumnIndex" /> is less than -1 or greater than or equal to the number of columns in the <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x06001270 RID: 4720 RVA: 0x000485E4 File Offset: 0x000467E4
		[MonoInternalNote("Needs row header cell edit pencil glyph")]
		public void PaintContent(Rectangle clipBounds)
		{
			this.Paint(clipBounds, DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.ContentForeground);
		}

		// Token: 0x04000B03 RID: 2819
		private DataGridView dataGridView;

		// Token: 0x04000B04 RID: 2820
		private Graphics graphics;

		// Token: 0x04000B05 RID: 2821
		private Rectangle clipBounds;

		// Token: 0x04000B06 RID: 2822
		private Rectangle cellBounds;

		// Token: 0x04000B07 RID: 2823
		private int rowIndex;

		// Token: 0x04000B08 RID: 2824
		private int columnIndex;

		// Token: 0x04000B09 RID: 2825
		private DataGridViewElementStates cellState;

		// Token: 0x04000B0A RID: 2826
		private object cellValue;

		// Token: 0x04000B0B RID: 2827
		private object formattedValue;

		// Token: 0x04000B0C RID: 2828
		private string errorText;

		// Token: 0x04000B0D RID: 2829
		private DataGridViewCellStyle cellStyle;

		// Token: 0x04000B0E RID: 2830
		private DataGridViewAdvancedBorderStyle advancedBorderStyle;

		// Token: 0x04000B0F RID: 2831
		private DataGridViewPaintParts paintParts;
	}
}
