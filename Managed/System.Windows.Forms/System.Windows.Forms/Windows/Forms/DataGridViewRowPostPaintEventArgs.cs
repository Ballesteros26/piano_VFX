using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowPostPaint" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200012E RID: 302
	public class DataGridViewRowPostPaintEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowPostPaintEventArgs" /> class. </summary>
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
		// Token: 0x06001565 RID: 5477 RVA: 0x00050784 File Offset: 0x0004E984
		public DataGridViewRowPostPaintEventArgs(DataGridView dataGridView, Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, string errorText, DataGridViewCellStyle inheritedRowStyle, bool isFirstDisplayedRow, bool isLastVisibleRow)
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
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x000507E4 File Offset: 0x0004E9E4
		// (set) Token: 0x06001567 RID: 5479 RVA: 0x000507EC File Offset: 0x0004E9EC
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
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x000507F8 File Offset: 0x0004E9F8
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
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x00050800 File Offset: 0x0004EA00
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the cell style applied to the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains the cell style applied to the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x0600156A RID: 5482 RVA: 0x00050808 File Offset: 0x0004EA08
		public DataGridViewCellStyle InheritedRowStyle
		{
			get
			{
				return this.inheritedRowStyle;
			}
		}

		/// <summary>Gets a value indicating whether the current row is the first row displayed in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>true if the row being painted is currently the first row displayed in the <see cref="T:System.Windows.Forms.DataGridView" />; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x00050810 File Offset: 0x0004EA10
		public bool IsFirstDisplayedRow
		{
			get
			{
				return this.isFirstDisplayedRow;
			}
		}

		/// <summary>Gets a value indicating whether the current row is the last visible row displayed in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>true if the current row is the last row in the <see cref="T:System.Windows.Forms.DataGridView" /> that has the <see cref="P:System.Windows.Forms.DataGridViewRow.Visible" /> property set to true; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x00050818 File Offset: 0x0004EA18
		public bool IsLastVisibleRow
		{
			get
			{
				return this.isLastVisibleRow;
			}
		}

		/// <summary>Get the bounds of the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x00050820 File Offset: 0x0004EA20
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
		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x00050828 File Offset: 0x0004EA28
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets the state of the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the row.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x00050830 File Offset: 0x0004EA30
		public DataGridViewElementStates State
		{
			get
			{
				return this.rowState;
			}
		}

		/// <summary>Draws the focus rectangle around the specified bounds.</summary>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the focus area.</param>
		/// <param name="cellsPaintSelectionBackground">true to use the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.SelectionBackColor" /> property of the <see cref="P:System.Windows.Forms.DataGridViewRow.InheritedStyle" /> property to determine the color of the focus rectangle; false to use the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.BackColor" /> property of the <see cref="P:System.Windows.Forms.DataGridViewRow.InheritedStyle" /> property.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewRowPostPaintEventArgs.RowIndex" /> is less than zero or greater than the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control minus one.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001570 RID: 5488 RVA: 0x00050838 File Offset: 0x0004EA38
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
		// Token: 0x06001571 RID: 5489 RVA: 0x000508B8 File Offset: 0x0004EAB8
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
		// Token: 0x06001572 RID: 5490 RVA: 0x00050938 File Offset: 0x0004EB38
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
		// Token: 0x06001573 RID: 5491 RVA: 0x00050958 File Offset: 0x0004EB58
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
		// Token: 0x06001574 RID: 5492 RVA: 0x00050964 File Offset: 0x0004EB64
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
		// Token: 0x06001575 RID: 5493 RVA: 0x00050984 File Offset: 0x0004EB84
		public void PaintHeader(DataGridViewPaintParts paintParts)
		{
			if (this.rowIndex < 0 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException("Invalid RowIndex.");
			}
			DataGridViewRow rowInternal = this.dataGridView.GetRowInternal(this.rowIndex);
			rowInternal.PaintHeader(this.graphics, this.clipBounds, this.rowBounds, this.rowIndex, this.rowState, this.isFirstDisplayedRow, this.isLastVisibleRow, paintParts);
		}

		// Token: 0x04000C09 RID: 3081
		private DataGridView dataGridView;

		// Token: 0x04000C0A RID: 3082
		private Graphics graphics;

		// Token: 0x04000C0B RID: 3083
		private Rectangle clipBounds;

		// Token: 0x04000C0C RID: 3084
		private Rectangle rowBounds;

		// Token: 0x04000C0D RID: 3085
		private int rowIndex;

		// Token: 0x04000C0E RID: 3086
		private DataGridViewElementStates rowState;

		// Token: 0x04000C0F RID: 3087
		private string errorText;

		// Token: 0x04000C10 RID: 3088
		private DataGridViewCellStyle inheritedRowStyle;

		// Token: 0x04000C11 RID: 3089
		private bool isFirstDisplayedRow;

		// Token: 0x04000C12 RID: 3090
		private bool isLastVisibleRow;
	}
}
