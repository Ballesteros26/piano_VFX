using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Displays editable text information in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000138 RID: 312
	public class DataGridViewTextBoxCell : DataGridViewCell
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewTextBoxCell" /> class.</summary>
		// Token: 0x060015DA RID: 5594 RVA: 0x00051170 File Offset: 0x0004F370
		public DataGridViewTextBoxCell()
		{
			base.ValueType = typeof(object);
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x00051194 File Offset: 0x0004F394
		static DataGridViewTextBoxCell()
		{
			DataGridViewTextBoxCell.editingControl.Multiline = false;
			DataGridViewTextBoxCell.editingControl.BorderStyle = BorderStyle.None;
		}

		/// <summary>Gets the type of the formatted value associated with the cell.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the <see cref="T:System.String" /> type in all cases.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x000511C4 File Offset: 0x0004F3C4
		public override Type FormattedValueType
		{
			get
			{
				return typeof(string);
			}
		}

		/// <summary>Gets or sets the maximum number of characters that can be entered into the text box.</summary>
		/// <returns>The maximum number of characters that can be entered into the text box; the default value is 32767.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 0.</exception>
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x000511D0 File Offset: 0x0004F3D0
		// (set) Token: 0x060015DE RID: 5598 RVA: 0x000511D8 File Offset: 0x0004F3D8
		[DefaultValue(32767)]
		public virtual int MaxInputLength
		{
			get
			{
				return this.maxInputLength;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("MaxInputLength coudn't be less than 0.");
				}
				this.maxInputLength = value;
			}
		}

		/// <returns>A <see cref="T:System.Type" /> representing the data type of the value in the cell.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x000511F4 File Offset: 0x0004F3F4
		public override Type ValueType
		{
			get
			{
				return base.ValueType;
			}
		}

		/// <summary>Creates an exact copy of this cell.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewTextBoxCell" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060015E0 RID: 5600 RVA: 0x000511FC File Offset: 0x0004F3FC
		public override object Clone()
		{
			DataGridViewTextBoxCell dataGridViewTextBoxCell = (DataGridViewTextBoxCell)base.Clone();
			dataGridViewTextBoxCell.maxInputLength = this.maxInputLength;
			return dataGridViewTextBoxCell;
		}

		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060015E1 RID: 5601 RVA: 0x00051224 File Offset: 0x0004F424
		[EditorBrowsable(2)]
		public override void DetachEditingControl()
		{
			if (base.DataGridView == null)
			{
				throw new InvalidOperationException("There is no associated DataGridView.");
			}
			base.DataGridView.EditingControlInternal = null;
		}

		/// <summary>Attaches and initializes the hosted editing control.</summary>
		/// <param name="rowIndex">The index of the row being edited.</param>
		/// <param name="initialFormattedValue">The initial value to be displayed in the control.</param>
		/// <param name="dataGridViewCellStyle">A cell style that is used to determine the appearance of the hosted control.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060015E2 RID: 5602 RVA: 0x00051254 File Offset: 0x0004F454
		public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			if (base.DataGridView == null)
			{
				throw new InvalidOperationException("There is no associated DataGridView.");
			}
			base.DataGridView.EditingControlInternal = DataGridViewTextBoxCell.editingControl;
			DataGridViewTextBoxCell.editingControl.EditingControlDataGridView = base.DataGridView;
			DataGridViewTextBoxCell.editingControl.MaxLength = this.maxInputLength;
			if (initialFormattedValue == null || initialFormattedValue.ToString() == string.Empty)
			{
				DataGridViewTextBoxCell.editingControl.Text = string.Empty;
			}
			else
			{
				DataGridViewTextBoxCell.editingControl.Text = initialFormattedValue.ToString();
			}
			DataGridViewTextBoxCell.editingControl.ApplyCellStyleToEditingControl(dataGridViewCellStyle);
			DataGridViewTextBoxCell.editingControl.PrepareEditingControlForEdit(true);
		}

		/// <summary>Determines if edit mode should be started based on the given key.</summary>
		/// <returns>true if edit mode should be started; otherwise, false. </returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that represents the key that was pressed.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015E3 RID: 5603 RVA: 0x000512FC File Offset: 0x0004F4FC
		public override bool KeyEntersEditMode(KeyEventArgs e)
		{
			return e.KeyCode == Keys.Space || (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.Z) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.Divide) || (e.KeyCode == Keys.BrowserSearch || e.KeyCode == Keys.SelectMedia) || (e.KeyCode >= Keys.OemSemicolon && e.KeyCode <= Keys.ProcessKey) || (e.KeyCode == Keys.Attn || e.KeyCode == Keys.Packet) || (e.KeyCode >= Keys.Exsel && e.KeyCode <= Keys.OemClear);
		}

		/// <param name="setLocation">true to have the control placed as specified by the other arguments; false to allow the control to place itself.</param>
		/// <param name="setSize">true to specify the size; false to allow the control to size itself. </param>
		/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that defines the cell bounds. </param>
		/// <param name="cellClip">The area that will be used to paint the editing control.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell being edited.</param>
		/// <param name="singleVerticalBorderAdded">true to add a vertical border to the cell; otherwise, false.</param>
		/// <param name="singleHorizontalBorderAdded">true to add a horizontal border to the cell; otherwise, false.</param>
		/// <param name="isFirstDisplayedColumn">true if the hosting cell is in the first visible column; otherwise, false.</param>
		/// <param name="isFirstDisplayedRow">true if the hosting cell is in the first visible row; otherwise, false.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060015E4 RID: 5604 RVA: 0x000513DC File Offset: 0x0004F5DC
		public override void PositionEditingControl(bool setLocation, bool setSize, Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
		{
			cellBounds.Size = new Size(cellBounds.Width - 5, cellBounds.Height + 2);
			cellBounds.Location = new Point(cellBounds.X + 3, (cellBounds.Height - DataGridViewTextBoxCell.editingControl.Height) / 2 + cellBounds.Y - 1);
			base.PositionEditingControl(setLocation, setSize, cellBounds, cellClip, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
			DataGridViewTextBoxCell.editingControl.Invalidate();
		}

		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015E5 RID: 5605 RVA: 0x0005145C File Offset: 0x0004F65C
		public override string ToString()
		{
			return string.Format("DataGridViewTextBoxCell {{ ColumnIndex={0}, RowIndex={1} }}", base.ColumnIndex, base.RowIndex);
		}

		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x060015E6 RID: 5606 RVA: 0x0005148C File Offset: 0x0004F68C
		protected override Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return Rectangle.Empty;
			}
			object formattedValue = base.FormattedValue;
			Size size = Size.Empty;
			if (formattedValue != null)
			{
				size = DataGridViewCell.MeasureTextSize(graphics, formattedValue.ToString(), cellStyle.Font, TextFormatFlags.Left);
				size.Height += 2;
			}
			return new Rectangle(0, (base.OwningRow.Height - size.Height) / 2, size.Width, size.Height);
		}

		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x060015E7 RID: 5607 RVA: 0x0005150C File Offset: 0x0004F70C
		protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (base.DataGridView == null || string.IsNullOrEmpty(base.ErrorText))
			{
				return Rectangle.Empty;
			}
			Size size;
			size..ctor(12, 11);
			return new Rectangle(new Point(base.Size.Width - size.Width - 5, (base.Size.Height - size.Height) / 2), size);
		}

		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the preferred size, in pixels, of the cell.</returns>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to draw the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
		/// <param name="rowIndex">The zero-based row index of the cell.</param>
		/// <param name="constraintSize">The cell's maximum allowable size.</param>
		// Token: 0x060015E8 RID: 5608 RVA: 0x00051580 File Offset: 0x0004F780
		protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
		{
			object formattedValue = base.FormattedValue;
			if (formattedValue != null)
			{
				Size size = DataGridViewCell.MeasureTextSize(graphics, formattedValue.ToString(), cellStyle.Font, TextFormatFlags.Left);
				size.Height = Math.Max(size.Height, 20);
				size.Width += 2;
				return size;
			}
			return new Size(21, 20);
		}

		/// <summary>Called by <see cref="T:System.Windows.Forms.DataGridView" /> when the selection cursor moves onto a cell.</summary>
		/// <param name="rowIndex">The index of the row entered by the mouse.</param>
		/// <param name="throughMouseClick">true if the cell was entered as a result of a mouse click; otherwise, false.</param>
		// Token: 0x060015E9 RID: 5609 RVA: 0x000515E0 File Offset: 0x0004F7E0
		protected override void OnEnter(int rowIndex, bool throughMouseClick)
		{
		}

		/// <summary>Called by the <see cref="T:System.Windows.Forms.DataGridView" /> when the mouse leaves a cell.</summary>
		/// <param name="rowIndex">The index of the row the mouse has left.</param>
		/// <param name="throughMouseClick">true if the cell was left as a result of a mouse click; otherwise, false.</param>
		// Token: 0x060015EA RID: 5610 RVA: 0x000515E4 File Offset: 0x0004F7E4
		protected override void OnLeave(int rowIndex, bool throughMouseClick)
		{
		}

		/// <summary>Called by <see cref="T:System.Windows.Forms.DataGridView" /> when the mouse leaves a cell.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x060015EB RID: 5611 RVA: 0x000515E8 File Offset: 0x0004F7E8
		protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
		{
		}

		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
		/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="rowIndex">The row index of the cell that is being painted.</param>
		/// <param name="cellState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the cell.</param>
		/// <param name="value">The data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="formattedValue">The formatted data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="errorText">An error message that is associated with the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the cell.</param>
		/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles for the cell that is being painted.</param>
		/// <param name="paintParts">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values that specifies which parts of the cell need to be painted.</param>
		// Token: 0x060015EC RID: 5612 RVA: 0x000515EC File Offset: 0x0004F7EC
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			DataGridViewPaintParts dataGridViewPaintParts = DataGridViewPaintParts.Background | DataGridViewPaintParts.SelectionBackground;
			dataGridViewPaintParts &= paintParts;
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, dataGridViewPaintParts);
			if (!base.IsInEditMode && (paintParts & DataGridViewPaintParts.ContentForeground) == DataGridViewPaintParts.ContentForeground)
			{
				Color color = ((!this.Selected) ? cellStyle.ForeColor : cellStyle.SelectionForeColor);
				TextFormatFlags textFormatFlags = TextFormatFlags.TextBoxControl | TextFormatFlags.EndEllipsis;
				textFormatFlags |= base.AlignmentToFlags(cellStyle.Alignment);
				Rectangle rectangle = cellBounds;
				rectangle.Height -= 2;
				rectangle.Width -= 2;
				if ((cellStyle.Alignment & (DataGridViewContentAlignment)7) > DataGridViewContentAlignment.NotSet)
				{
					rectangle.Offset(0, 2);
					rectangle.Height -= 2;
				}
				if (formattedValue != null)
				{
					TextRenderer.DrawText(graphics, formattedValue.ToString(), cellStyle.Font, rectangle, color, textFormatFlags);
				}
			}
			DataGridViewPaintParts dataGridViewPaintParts2 = DataGridViewPaintParts.Border | DataGridViewPaintParts.ErrorIcon | DataGridViewPaintParts.Focus;
			dataGridViewPaintParts2 &= paintParts;
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, dataGridViewPaintParts2);
		}

		// Token: 0x04000C31 RID: 3121
		private int maxInputLength = 32767;

		// Token: 0x04000C32 RID: 3122
		private static DataGridViewTextBoxEditingControl editingControl = new DataGridViewTextBoxEditingControl();
	}
}
