using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowPrePaint" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200012F RID: 303
	public class DataGridViewRowPrePaintEventArgs : HandledEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowPrePaintEventArgs" /> class. </summary>
		/// <param name="dataGridView">The <see cref="T:System.Windows.Forms.DataGridView" /> that owns the row that is being painted.</param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be painted.</param>
		/// <param name="rowBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> that is being painted.</param>
		/// <param name="rowIndex">The row index of the cell that is being painted.</param>
		/// <param name="rowState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the row.</param>
		/// <param name="errorText">An error message that is associated with the row.</param>
		/// <param name="inheritedRowStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the row.</param>
		/// <param name="isFirstDisplayedRow">true to indicate whether the current row is the first row currently displayed in the <see cref="T:System.Windows.Forms.DataGridView" />; otherwise, false.</param>
		/// <param name="isLastVisibleRow">true to indicate whether the current row is the last row in the <see cref="T:System.Windows.Forms.DataGridView" /> that has the <see cref="P:System.Windows.Forms.DataGridViewRow.Visible" /> property set to true; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridView" /> is null.-or-<paramref name="graphics" /> is null.-or-<paramref name="inheritedRowStyle" /> is null.</exception>
		// Token: 0x06001576 RID: 5494 RVA: 0x00050A08 File Offset: 0x0004EC08
		public DataGridViewRowPrePaintEventArgs(DataGridView dataGridView, Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, string errorText, DataGridViewCellStyle inheritedRowStyle, bool isFirstDisplayedRow, bool isLastVisibleRow)
		{
			this.dataGridView = dataGridView;
			this.graphics = graphics;
			this.clipBounds = clipBounds;
			this.rowBounds = rowBounds;
			this.rowIndex = rowIndex;
			this.rowState = rowState;
			this.errorText = errorText;
			this.inheritedRowStyle = inheritedRowStyle;
			this.isFirstDisplayedRow = isFirstDisplayedRow;
			this.isLastVisibleRow = isLastVisibleRow;
		}

		/// <summary>Gets or sets the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x00050A68 File Offset: 0x0004EC68
		// (set) Token: 0x06001578 RID: 5496 RVA: 0x00050A70 File Offset: 0x0004EC70
		public Rectangle ClipBounds
		{
			get
			{
				return this.clipBounds;
			}
			set
			{
				this.clipBounds = value;
			}
		}

		/// <summary>Gets a string that represents an error message for the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
		/// <returns>A string that represents an error message for the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x00050A7C File Offset: 0x0004EC7C
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> used to paint the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x00050A84 File Offset: 0x0004EC84
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the cell style applied to the row.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains the cell style currently applied to the row.</returns>
		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x00050A8C File Offset: 0x0004EC8C
		public DataGridViewCellStyle InheritedRowStyle
		{
			get
			{
				return this.inheritedRowStyle;
			}
		}

		/// <summary>Gets a value indicating whether the current row is the first row currently displayed in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>true if the row being painted is currently the first row displayed in the <see cref="T:System.Windows.Forms.DataGridView" />; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x00050A94 File Offset: 0x0004EC94
		public bool IsFirstDisplayedRow
		{
			get
			{
				return this.isFirstDisplayedRow;
			}
		}

		/// <summary>Gets a value indicating whether the current row is the last visible row in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>true if the row being painted is currently the last row in the <see cref="T:System.Windows.Forms.DataGridView" /> that has the <see cref="P:System.Windows.Forms.DataGridViewRow.Visible" /> property set to true; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x00050A9C File Offset: 0x0004EC9C
		public bool IsLastVisibleRow
		{
			get
			{
				return this.isLastVisibleRow;
			}
		}

		/// <summary>The cell parts that are to be painted.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values specifying the parts to be painted.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values.</exception>
		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x00050AA4 File Offset: 0x0004ECA4
		// (set) Token: 0x0600157F RID: 5503 RVA: 0x00050AAC File Offset: 0x0004ECAC
		public DataGridViewPaintParts PaintParts
		{
			get
			{
				return this.paintParts;
			}
			set
			{
				this.paintParts = value;
			}
		}

		/// <summary>Get the bounds of the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x00050AB8 File Offset: 0x0004ECB8
		public Rectangle RowBounds
		{
			get
			{
				return this.rowBounds;
			}
		}

		/// <summary>Gets the index of the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
		/// <returns>The index of the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001581 RID: 5505 RVA: 0x00050AC0 File Offset: 0x0004ECC0
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets the state of the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the row.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001582 RID: 5506 RVA: 0x00050AC8 File Offset: 0x0004ECC8
		public DataGridViewElementStates State
		{
			get
			{
				return this.rowState;
			}
		}

		/// <summary>Draws the focus rectangle around the specified bounds.</summary>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the focus area.</param>
		/// <param name="cellsPaintSelectionBackground">true to use the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.SelectionBackColor" /> property of the <see cref="P:System.Windows.Forms.DataGridViewRow.InheritedStyle" /> property to determine the color of the focus rectangle; false to use the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.BackColor" /> property of the <see cref="P:System.Windows.Forms.DataGridViewRow.InheritedStyle" />.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewRowPrePaintEventArgs.RowIndex" /> is less than zero or greater than the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control minus one.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001583 RID: 5507 RVA: 0x00050AD0 File Offset: 0x0004ECD0
		public void DrawFocus(Rectangle bounds, bool cellsPaintSelectionBackground)
		{
			if (this.rowIndex < 0 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException("Invalid RowIndex.");
			}
			DataGridViewRow rowInternal = this.dataGridView.GetRowInternal(this.rowIndex);
			rowInternal.PaintCells(this.graphics, this.clipBounds, bounds, this.rowIndex, this.rowState, this.isFirstDisplayedRow, this.isLastVisibleRow, DataGridViewPaintParts.Focus);
		}

		/// <summary>Paints the specified cell parts for the area in the specified bounds.</summary>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area of the <see cref="T:System.Windows.Forms.DataGridView" /> to be painted.</param>
		/// <param name="paintParts">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values specifying the parts to paint.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewRowPostPaintEventArgs.RowIndex" /> is less than zero or greater than the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control minus one.</exception>
		// Token: 0x06001584 RID: 5508 RVA: 0x00050B50 File Offset: 0x0004ED50
		public void PaintCells(Rectangle clipBounds, DataGridViewPaintParts paintParts)
		{
			if (this.rowIndex < 0 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException("Invalid RowIndex.");
			}
			DataGridViewRow rowInternal = this.dataGridView.GetRowInternal(this.rowIndex);
			rowInternal.PaintCells(this.graphics, clipBounds, this.rowBounds, this.rowIndex, this.rowState, this.isFirstDisplayedRow, this.isLastVisibleRow, paintParts);
		}

		/// <summary>Paints the cell backgrounds for the area in the specified bounds.</summary>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area of the <see cref="T:System.Windows.Forms.DataGridView" /> to be painted.</param>
		/// <param name="cellsPaintSelectionBackground">true to paint the background of the specified bounds with the color of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.SelectionBackColor" /> property of the <see cref="P:System.Windows.Forms.DataGridViewRow.InheritedStyle" />; false to paint the background of the specified bounds with the color of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.BackColor" /> property of the <see cref="P:System.Windows.Forms.DataGridViewRow.InheritedStyle" />.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewRowPostPaintEventArgs.RowIndex" /> is less than zero or greater than the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control minus one.</exception>
		// Token: 0x06001585 RID: 5509 RVA: 0x00050BD0 File Offset: 0x0004EDD0
		public void PaintCellsBackground(Rectangle clipBounds, bool cellsPaintSelectionBackground)
		{
			if (cellsPaintSelectionBackground)
			{
				this.PaintCells(clipBounds, DataGridViewPaintParts.All);
			}
			else
			{
				this.PaintCells(clipBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border | DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.ContentForeground | DataGridViewPaintParts.ErrorIcon | DataGridViewPaintParts.Focus);
			}
		}

		/// <summary>Paints the cell contents for the area in the specified bounds.</summary>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area of the <see cref="T:System.Windows.Forms.DataGridView" /> to be painted.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewRowPostPaintEventArgs.RowIndex" /> is less than zero or greater than the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control minus one.</exception>
		// Token: 0x06001586 RID: 5510 RVA: 0x00050BF0 File Offset: 0x0004EDF0
		public void PaintCellsContent(Rectangle clipBounds)
		{
			this.PaintCells(clipBounds, DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.ContentForeground);
		}

		/// <summary>Paints the entire row header of the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
		/// <param name="paintSelectionBackground">true to paint the row header with the color of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.SelectionBackColor" /> property of the <see cref="P:System.Windows.Forms.DataGridViewRow.InheritedStyle" />; false to paint the row header with the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.BackColor" /> of the <see cref="P:System.Windows.Forms.DataGridView.RowHeadersDefaultCellStyle" /> property.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewRowPostPaintEventArgs.RowIndex" /> is less than zero or greater than the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control minus one.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001587 RID: 5511 RVA: 0x00050BFC File Offset: 0x0004EDFC
		public void PaintHeader(bool paintSelectionBackground)
		{
			if (paintSelectionBackground)
			{
				this.PaintHeader(DataGridViewPaintParts.All);
			}
			else
			{
				this.PaintHeader(DataGridViewPaintParts.Background | DataGridViewPaintParts.Border | DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.ContentForeground | DataGridViewPaintParts.ErrorIcon | DataGridViewPaintParts.Focus);
			}
		}

		/// <summary>Paints the specified parts of the row header of the current row.</summary>
		/// <param name="paintParts">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values specifying the parts to paint.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewRowPostPaintEventArgs.RowIndex" /> is less than zero or greater than the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control minus one.</exception>
		// Token: 0x06001588 RID: 5512 RVA: 0x00050C1C File Offset: 0x0004EE1C
		public void PaintHeader(DataGridViewPaintParts paintParts)
		{
			if (this.rowIndex < 0 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException("Invalid RowIndex.");
			}
			DataGridViewRow rowInternal = this.dataGridView.GetRowInternal(this.rowIndex);
			rowInternal.PaintHeader(this.graphics, this.clipBounds, this.rowBounds, this.rowIndex, this.rowState, this.isFirstDisplayedRow, this.isLastVisibleRow, paintParts);
		}

		// Token: 0x04000C13 RID: 3091
		private DataGridView dataGridView;

		// Token: 0x04000C14 RID: 3092
		private Graphics graphics;

		// Token: 0x04000C15 RID: 3093
		private Rectangle clipBounds;

		// Token: 0x04000C16 RID: 3094
		private Rectangle rowBounds;

		// Token: 0x04000C17 RID: 3095
		private int rowIndex;

		// Token: 0x04000C18 RID: 3096
		private DataGridViewElementStates rowState;

		// Token: 0x04000C19 RID: 3097
		private string errorText;

		// Token: 0x04000C1A RID: 3098
		private DataGridViewCellStyle inheritedRowStyle;

		// Token: 0x04000C1B RID: 3099
		private bool isFirstDisplayedRow;

		// Token: 0x04000C1C RID: 3100
		private bool isLastVisibleRow;

		// Token: 0x04000C1D RID: 3101
		private DataGridViewPaintParts paintParts;
	}
}
