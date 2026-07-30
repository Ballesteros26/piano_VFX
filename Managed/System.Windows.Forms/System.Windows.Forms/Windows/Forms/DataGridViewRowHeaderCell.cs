using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Represents a row header of a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000129 RID: 297
	public class DataGridViewRowHeaderCell : DataGridViewHeaderCell
	{
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewHeaderCell" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600153D RID: 5437 RVA: 0x0004FEC8 File Offset: 0x0004E0C8
		public override object Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Retrieves the inherited shortcut menu for the specified row.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> of the row if one exists; otherwise, the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> inherited from <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		/// <param name="rowIndex">The index of the row to get the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> of. The index must be -1 to indicate the row of column headers.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the cell is not null and the specified <paramref name="rowIndex" /> is less than 0 or greater than the number of rows in the control minus 1.</exception>
		// Token: 0x0600153E RID: 5438 RVA: 0x0004FED0 File Offset: 0x0004E0D0
		public override ContextMenuStrip GetInheritedContextMenuStrip(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return null;
			}
			if (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			if (this.ContextMenuStrip != null)
			{
				return this.ContextMenuStrip;
			}
			return base.DataGridView.ContextMenuStrip;
		}

		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that includes the style settings of the cell inherited from the cell's parent row, column, and <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		/// <param name="inheritedCellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be populated with the inherited cell style. </param>
		/// <param name="rowIndex">The index of the cell's parent row. </param>
		/// <param name="includeColors">true to include inherited colors in the returned cell style; otherwise, false. </param>
		// Token: 0x0600153F RID: 5439 RVA: 0x0004FF30 File Offset: 0x0004E130
		public override DataGridViewCellStyle GetInheritedStyle(DataGridViewCellStyle inheritedCellStyle, int rowIndex, bool includeColors)
		{
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle(base.DataGridView.DefaultCellStyle);
			dataGridViewCellStyle.ApplyStyle(base.DataGridView.RowHeadersDefaultCellStyle);
			if (base.HasStyle)
			{
				dataGridViewCellStyle.ApplyStyle(base.Style);
			}
			return dataGridViewCellStyle;
		}

		/// <filterpriority>1</filterpriority>
		// Token: 0x06001540 RID: 5440 RVA: 0x0004FF78 File Offset: 0x0004E178
		public override string ToString()
		{
			return base.ToString();
		}

		/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell" />. </summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell.DataGridViewRowHeaderCellAccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell" />. </returns>
		// Token: 0x06001541 RID: 5441 RVA: 0x0004FF80 File Offset: 0x0004E180
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewRowHeaderCell.DataGridViewRowHeaderCellAccessibleObject(this);
		}

		/// <summary>Retrieves the formatted value of the cell to copy to the <see cref="T:System.Windows.Forms.Clipboard" />.</summary>
		/// <returns>A <see cref="T:System.Object" /> that represents the value of the cell to copy to the <see cref="T:System.Windows.Forms.Clipboard" />.</returns>
		/// <param name="rowIndex">The zero-based index of the row containing the cell.</param>
		/// <param name="firstCell">true to indicate that the cell is in the first column of the region defined by the selected cells; otherwise, false.</param>
		/// <param name="lastCell">true to indicate that the cell is the last column of the region defined by the selected cells; otherwise, false.</param>
		/// <param name="inFirstRow">true to indicate that the cell is in the first row of the region defined by the selected cells; otherwise, false.</param>
		/// <param name="inLastRow">true to indicate that the cell is in the last row of the region defined by the selected cells; otherwise, false.</param>
		/// <param name="format">The current format string of the cell.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than zero or greater than or equal to the number of rows in the control.</exception>
		// Token: 0x06001542 RID: 5442 RVA: 0x0004FF88 File Offset: 0x0004E188
		protected override object GetClipboardContent(int rowIndex, bool firstCell, bool lastCell, bool inFirstRow, bool inLastRow, string format)
		{
			if (base.DataGridView == null)
			{
				return null;
			}
			if (rowIndex < 0 || rowIndex >= base.DataGridView.RowCount)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			string text = this.GetValue(rowIndex) as string;
			string text2 = string.Empty;
			string text3 = string.Empty;
			string text4 = string.Empty;
			string text5 = string.Empty;
			string text6 = string.Empty;
			string text7 = string.Empty;
			if (format == DataFormats.UnicodeText || format == DataFormats.Text)
			{
				if (lastCell && !inLastRow)
				{
					text6 = Environment.NewLine;
				}
				else if (!lastCell)
				{
					text6 = "\t";
				}
			}
			else if (format == DataFormats.CommaSeparatedValue)
			{
				if (lastCell && !inLastRow)
				{
					text6 = Environment.NewLine;
				}
				else if (!lastCell)
				{
					text6 = ",";
				}
			}
			else
			{
				if (!(format == DataFormats.Html))
				{
					return text;
				}
				if (inFirstRow)
				{
					text2 = "<TABLE>";
				}
				text4 = "<TR>";
				if (lastCell)
				{
					text7 = "</TR>";
					if (inLastRow)
					{
						text5 = "</TABLE>";
					}
				}
				text3 = "<TD ALIGN=\"center\">";
				text6 = "</TD>";
				if (text == null)
				{
					text = "&nbsp;";
				}
				else
				{
					text = "<B>" + text + "</B>";
				}
			}
			if (text == null)
			{
				text = string.Empty;
			}
			return string.Concat(new string[] { text2, text4, text3, text, text6, text7, text5 });
		}

		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x06001543 RID: 5443 RVA: 0x00050130 File Offset: 0x0004E330
		protected override Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return Rectangle.Empty;
			}
			Size size;
			size..ctor(11, 18);
			return new Rectangle(24, (base.OwningRow.Height - size.Height) / 2, size.Width, size.Height);
		}

		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x06001544 RID: 5444 RVA: 0x00050184 File Offset: 0x0004E384
		protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (base.DataGridView == null || string.IsNullOrEmpty(base.DataGridView.GetRowInternal(rowIndex).ErrorText))
			{
				return Rectangle.Empty;
			}
			Size size;
			size..ctor(12, 11);
			return new Rectangle(new Point(base.Size.Width - size.Width - 5, (base.Size.Height - size.Height) / 2), size);
		}

		/// <returns>A string that describes the error for the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <param name="rowIndex">The row index of the cell.</param>
		// Token: 0x06001545 RID: 5445 RVA: 0x00050204 File Offset: 0x0004E404
		protected internal override string GetErrorText(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return string.Empty;
			}
			return base.DataGridView.GetRowInternal(rowIndex).ErrorText;
		}

		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the preferred size, in pixels, of the cell.</returns>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to draw the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
		/// <param name="rowIndex">The zero-based row index of the cell.</param>
		/// <param name="constraintSize">The cell's maximum allowable size.</param>
		// Token: 0x06001546 RID: 5446 RVA: 0x00050234 File Offset: 0x0004E434
		protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
		{
			object formattedValue = base.FormattedValue;
			if (formattedValue != null)
			{
				Size size = DataGridViewCell.MeasureTextSize(graphics, formattedValue.ToString(), cellStyle.Font, TextFormatFlags.Left);
				size.Height = Math.Max(size.Height, 17);
				size.Width += 48;
				return size;
			}
			return new Size(39, 17);
		}

		/// <summary>Gets the value of the cell. </summary>
		/// <returns>The value contained in the <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell" />.</returns>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the cell is not null and <paramref name="rowIndex" /> is less than -1 or greater than or equal to the number of rows in the parent <see cref="T:System.Windows.Forms.DataGridView" />.</exception>
		// Token: 0x06001547 RID: 5447 RVA: 0x00050294 File Offset: 0x0004E494
		protected override object GetValue(int rowIndex)
		{
			if (this.headerText != null)
			{
				return this.headerText;
			}
			return null;
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x000502AC File Offset: 0x0004E4AC
		[MonoInternalNote("Needs row header cell selected/edit pencil glyphs")]
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			DataGridViewPaintParts dataGridViewPaintParts = DataGridViewPaintParts.Background | DataGridViewPaintParts.SelectionBackground;
			dataGridViewPaintParts &= paintParts;
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, dataGridViewPaintParts);
			if ((paintParts & DataGridViewPaintParts.ContentBackground) == DataGridViewPaintParts.ContentBackground)
			{
				Color color = ((!this.Selected) ? cellStyle.ForeColor : cellStyle.SelectionForeColor);
				Pen pen = ThemeEngine.Current.ResPool.GetPen(color);
				int num = cellBounds.Left + 6;
				if (base.DataGridView.CurrentRow != null && base.DataGridView.CurrentRow.Index == rowIndex)
				{
					this.DrawRightArrowGlyph(graphics, pen, num, cellBounds.Top + cellBounds.Height / 2 - 4);
					num += 7;
				}
				if (base.DataGridView.Rows[rowIndex].IsNewRow)
				{
					this.DrawNewRowGlyph(graphics, pen, num, cellBounds.Top + cellBounds.Height / 2 - 4);
				}
			}
			if ((paintParts & DataGridViewPaintParts.ContentForeground) == DataGridViewPaintParts.ContentForeground)
			{
				Color color2 = ((!this.Selected) ? cellStyle.ForeColor : cellStyle.SelectionForeColor);
				TextFormatFlags textFormatFlags = TextFormatFlags.VerticalCenter | TextFormatFlags.TextBoxControl | TextFormatFlags.EndEllipsis;
				Rectangle rectangle = cellBounds;
				rectangle.Height -= 2;
				rectangle.Width -= 2;
				if (formattedValue != null)
				{
					TextRenderer.DrawText(graphics, formattedValue.ToString(), cellStyle.Font, rectangle, color2, textFormatFlags);
				}
			}
			DataGridViewPaintParts dataGridViewPaintParts2 = DataGridViewPaintParts.Border | DataGridViewPaintParts.ErrorIcon;
			dataGridViewPaintParts2 &= paintParts;
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, dataGridViewPaintParts2);
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x00050434 File Offset: 0x0004E634
		protected override void PaintBorder(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle)
		{
			if (ThemeEngine.Current.DataGridViewRowHeaderCellDrawBorder(this, graphics, cellBounds))
			{
				return;
			}
			Pen borderPen = base.GetBorderPen();
			graphics.DrawLine(borderPen, cellBounds.Left, cellBounds.Top, cellBounds.Left, cellBounds.Bottom - 1);
			graphics.DrawLine(borderPen, cellBounds.Right - 1, cellBounds.Top, cellBounds.Right - 1, cellBounds.Bottom - 1);
			if (base.RowIndex == base.DataGridView.Rows.Count - 1 || base.RowIndex == -1)
			{
				graphics.DrawLine(borderPen, cellBounds.Left, cellBounds.Bottom - 1, cellBounds.Right - 1, cellBounds.Bottom - 1);
			}
			else
			{
				graphics.DrawLine(borderPen, cellBounds.Left + 3, cellBounds.Bottom - 1, cellBounds.Right - 3, cellBounds.Bottom - 1);
			}
			if (base.RowIndex == -1)
			{
				graphics.DrawLine(borderPen, cellBounds.Left, cellBounds.Top, cellBounds.Right - 1, cellBounds.Top);
			}
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0005055C File Offset: 0x0004E75C
		internal override void PaintPartBackground(Graphics graphics, Rectangle cellBounds, DataGridViewCellStyle style)
		{
			if (ThemeEngine.Current.DataGridViewRowHeaderCellDrawBackground(this, graphics, cellBounds))
			{
				return;
			}
			base.PaintPartBackground(graphics, cellBounds, style);
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0005057C File Offset: 0x0004E77C
		internal override void PaintPartSelectionBackground(Graphics graphics, Rectangle cellBounds, DataGridViewElementStates cellState, DataGridViewCellStyle cellStyle)
		{
			if (ThemeEngine.Current.DataGridViewRowHeaderCellDrawSelectionBackground(this))
			{
				return;
			}
			base.PaintPartSelectionBackground(graphics, cellBounds, cellState, cellStyle);
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x000505A8 File Offset: 0x0004E7A8
		private void DrawRightArrowGlyph(Graphics g, Pen p, int x, int y)
		{
			g.DrawLine(p, x, y, x, y + 8);
			g.DrawLine(p, x + 1, y + 1, x + 1, y + 7);
			g.DrawLine(p, x + 2, y + 2, x + 2, y + 6);
			g.DrawLine(p, x + 3, y + 3, x + 3, y + 5);
			g.DrawLine(p, x + 3, y + 4, x + 4, y + 4);
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x00050618 File Offset: 0x0004E818
		private void DrawNewRowGlyph(Graphics g, Pen p, int x, int y)
		{
			g.DrawLine(p, x, y + 4, x + 8, y + 4);
			g.DrawLine(p, x + 4, y, x + 4, y + 8);
			g.DrawLine(p, x + 1, y + 1, x + 7, y + 7);
			g.DrawLine(p, x + 7, y + 1, x + 1, y + 7);
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x00050678 File Offset: 0x0004E878
		internal override Rectangle InternalErrorIconsBounds
		{
			get
			{
				return this.GetErrorIconBounds(null, null, base.RowIndex);
			}
		}

		/// <returns>true if the value has been set; otherwise, false.</returns>
		/// <param name="rowIndex">The index of the cell's parent row. </param>
		/// <param name="value">The cell value to set. </param>
		// Token: 0x0600154F RID: 5455 RVA: 0x00050688 File Offset: 0x0004E888
		protected override bool SetValue(int rowIndex, object value)
		{
			this.headerText = (string)value;
			return true;
		}

		// Token: 0x04000BFC RID: 3068
		private string headerText;

		/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell" /> to accessibility client applications.</summary>
		// Token: 0x0200012A RID: 298
		protected class DataGridViewRowHeaderCellAccessibleObject : DataGridViewCell.DataGridViewCellAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell.DataGridViewRowHeaderCellAccessibleObject" /> class. </summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell" /> that owns this accessible object.</param>
			// Token: 0x06001550 RID: 5456 RVA: 0x00050698 File Offset: 0x0004E898
			public DataGridViewRowHeaderCellAccessibleObject(DataGridViewRowHeaderCell owner)
				: base(owner)
			{
			}

			/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the accessible object.</returns>
			// Token: 0x170004F2 RID: 1266
			// (get) Token: 0x06001551 RID: 5457 RVA: 0x000506A4 File Offset: 0x0004E8A4
			public override Rectangle Bounds
			{
				get
				{
					return base.Bounds;
				}
			}

			/// <summary>Gets a description of the default action of the <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell.DataGridViewRowHeaderCellAccessibleObject" />.</summary>
			/// <returns>An empty string ("").</returns>
			// Token: 0x170004F3 RID: 1267
			// (get) Token: 0x06001552 RID: 5458 RVA: 0x000506AC File Offset: 0x0004E8AC
			public override string DefaultAction
			{
				get
				{
					return base.DefaultAction;
				}
			}

			/// <summary>Gets the name of the <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell.DataGridViewRowHeaderCellAccessibleObject" />.</summary>
			/// <returns>The name of the <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell.DataGridViewRowHeaderCellAccessibleObject" />.</returns>
			// Token: 0x170004F4 RID: 1268
			// (get) Token: 0x06001553 RID: 5459 RVA: 0x000506B4 File Offset: 0x0004E8B4
			public override string Name
			{
				get
				{
					return base.Name;
				}
			}

			/// <summary>Gets the parent of the <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell.DataGridViewRowHeaderCellAccessibleObject" />.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /> that belongs to the <see cref="T:System.Windows.Forms.DataGridViewRow" /> of the current <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell.DataGridViewRowHeaderCellAccessibleObject" />.</returns>
			// Token: 0x170004F5 RID: 1269
			// (get) Token: 0x06001554 RID: 5460 RVA: 0x000506BC File Offset: 0x0004E8BC
			public override AccessibleObject Parent
			{
				get
				{
					return base.Parent;
				}
			}

			/// <summary>Gets the role of the <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell.DataGridViewRowHeaderCellAccessibleObject" />.</summary>
			/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.RowHeader" /> value.</returns>
			// Token: 0x170004F6 RID: 1270
			// (get) Token: 0x06001555 RID: 5461 RVA: 0x000506C4 File Offset: 0x0004E8C4
			public override AccessibleRole Role
			{
				get
				{
					return base.Role;
				}
			}

			/// <summary>Gets the state of the <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell.DataGridViewRowHeaderCellAccessibleObject" />.</summary>
			/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.AccessibleStates" /> values. </returns>
			// Token: 0x170004F7 RID: 1271
			// (get) Token: 0x06001556 RID: 5462 RVA: 0x000506CC File Offset: 0x0004E8CC
			public override AccessibleStates State
			{
				get
				{
					return base.State;
				}
			}

			/// <summary>Gets the value of the <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell.DataGridViewRowHeaderCellAccessibleObject" />.</summary>
			/// <returns>An empty string ("").</returns>
			// Token: 0x170004F8 RID: 1272
			// (get) Token: 0x06001557 RID: 5463 RVA: 0x000506D4 File Offset: 0x0004E8D4
			public override string Value
			{
				get
				{
					return base.Value;
				}
			}

			/// <summary>Performs the default action of the <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell.DataGridViewRowHeaderCellAccessibleObject" />.</summary>
			// Token: 0x06001558 RID: 5464 RVA: 0x000506DC File Offset: 0x0004E8DC
			public override void DoDefaultAction()
			{
				base.DoDefaultAction();
			}

			/// <summary>Navigates to another accessible object.</summary>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents an object in the specified direction.</returns>
			/// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</param>
			// Token: 0x06001559 RID: 5465 RVA: 0x000506E4 File Offset: 0x0004E8E4
			public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
			{
				return base.Navigate(navigationDirection);
			}

			/// <summary>Modifies the row selection depending on the selection mode.</summary>
			/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.AccessibleSelection" /> values.</param>
			/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property value is null.</exception>
			// Token: 0x0600155A RID: 5466 RVA: 0x000506F0 File Offset: 0x0004E8F0
			public override void Select(AccessibleSelection flags)
			{
				base.Select(flags);
			}
		}
	}
}
