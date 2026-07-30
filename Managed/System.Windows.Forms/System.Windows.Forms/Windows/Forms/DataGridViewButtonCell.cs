using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Displays a button-like user interface (UI) for use in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000E0 RID: 224
	public class DataGridViewButtonCell : DataGridViewCell
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewButtonCell" /> class.</summary>
		// Token: 0x06001151 RID: 4433 RVA: 0x00045238 File Offset: 0x00043438
		public DataGridViewButtonCell()
		{
			this.useColumnTextForButtonValue = false;
			this.button_state = PushButtonState.Normal;
		}

		/// <summary>Gets the type of the cell's hosted editing control.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the underlying editing control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x00045250 File Offset: 0x00043450
		public override Type EditType
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets or sets the style determining the button's appearance.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.FlatStyle" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x00045254 File Offset: 0x00043454
		// (set) Token: 0x06001154 RID: 4436 RVA: 0x0004525C File Offset: 0x0004345C
		[DefaultValue(FlatStyle.Standard)]
		public FlatStyle FlatStyle
		{
			get
			{
				return this.flatStyle;
			}
			set
			{
				if (!Enum.IsDefined(typeof(FlatStyle), value))
				{
					throw new InvalidEnumArgumentException("Value is not valid FlatStyle.");
				}
				if (value == FlatStyle.Popup)
				{
					throw new Exception("FlatStyle cannot be set to Popup in this control.");
				}
			}
		}

		/// <returns>A <see cref="T:System.Type" /> representing the type of the cell's formatted value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x00045298 File Offset: 0x00043498
		public override Type FormattedValueType
		{
			get
			{
				return typeof(string);
			}
		}

		/// <summary>Gets or sets a value indicating whether the owning column's text will appear on the button displayed by the cell.</summary>
		/// <returns>true if the value of the <see cref="P:System.Windows.Forms.DataGridViewCell.Value" /> property will automatically match the value of the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.Text" /> property of the owning column; otherwise, false. The default is false.</returns>
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x000452A4 File Offset: 0x000434A4
		// (set) Token: 0x06001157 RID: 4439 RVA: 0x000452AC File Offset: 0x000434AC
		[DefaultValue(false)]
		public bool UseColumnTextForButtonValue
		{
			get
			{
				return this.useColumnTextForButtonValue;
			}
			set
			{
				this.useColumnTextForButtonValue = value;
			}
		}

		/// <returns>A <see cref="T:System.Type" /> representing the data type of the value in the cell.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x000452B8 File Offset: 0x000434B8
		public override Type ValueType
		{
			get
			{
				return (base.ValueType != null) ? base.ValueType : typeof(object);
			}
		}

		/// <summary>Creates an exact copy of this cell.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewButtonCell" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001159 RID: 4441 RVA: 0x000452E8 File Offset: 0x000434E8
		public override object Clone()
		{
			DataGridViewButtonCell dataGridViewButtonCell = (DataGridViewButtonCell)base.Clone();
			dataGridViewButtonCell.flatStyle = this.flatStyle;
			dataGridViewButtonCell.useColumnTextForButtonValue = this.useColumnTextForButtonValue;
			return dataGridViewButtonCell;
		}

		/// <summary>Returns the string representation of the cell.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current cell.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600115A RID: 4442 RVA: 0x0004531C File Offset: 0x0004351C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				base.GetType().Name,
				": RowIndex: ",
				base.RowIndex.ToString(),
				"; ColumnIndex: ",
				base.ColumnIndex.ToString(),
				";"
			});
		}

		/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewButtonCell" />. </summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.DataGridViewButtonCell.DataGridViewButtonCellAccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewButtonCell" />. </returns>
		// Token: 0x0600115B RID: 4443 RVA: 0x0004537C File Offset: 0x0004357C
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewButtonCell.DataGridViewButtonCellAccessibleObject(this);
		}

		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x0600115C RID: 4444 RVA: 0x00045384 File Offset: 0x00043584
		protected override Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return Rectangle.Empty;
			}
			Rectangle empty = Rectangle.Empty;
			empty.Height = base.OwningRow.Height - 1;
			empty.Width = base.OwningColumn.Width - 1;
			return empty;
		}

		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x0600115D RID: 4445 RVA: 0x000453D4 File Offset: 0x000435D4
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
		// Token: 0x0600115E RID: 4446 RVA: 0x00045448 File Offset: 0x00043648
		protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
		{
			object formattedValue = base.FormattedValue;
			if (formattedValue != null)
			{
				Size size = DataGridViewCell.MeasureTextSize(graphics, formattedValue.ToString(), cellStyle.Font, TextFormatFlags.Left);
				size.Height = Math.Max(size.Height, 21);
				size.Width += 10;
				return size;
			}
			return new Size(21, 21);
		}

		/// <summary>Retrieves the text associated with the button.</summary>
		/// <returns>The value of the <see cref="T:System.Windows.Forms.DataGridViewButtonCell" /> or the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.Text" /> value of the owning column if <see cref="P:System.Windows.Forms.DataGridViewButtonCell.UseColumnTextForButtonValue" /> is true. </returns>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x0600115F RID: 4447 RVA: 0x000454A8 File Offset: 0x000436A8
		protected override object GetValue(int rowIndex)
		{
			if (this.useColumnTextForButtonValue)
			{
				return (base.OwningColumn as DataGridViewButtonColumn).Text;
			}
			return base.GetValue(rowIndex);
		}

		/// <summary>Indicates whether a row is unshared if a key is pressed while the focus is on a cell in the row.</summary>
		/// <returns>true if the user pressed the SPACE key without modifier keys; otherwise, false.</returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		/// <param name="rowIndex">The index of the cell's parent row. </param>
		// Token: 0x06001160 RID: 4448 RVA: 0x000454D0 File Offset: 0x000436D0
		protected override bool KeyDownUnsharesRow(KeyEventArgs e, int rowIndex)
		{
			return e.KeyData == Keys.Space;
		}

		/// <summary>Indicates whether a row is unshared when a key is released while the focus is on a cell in the row.</summary>
		/// <returns>true if the user released the SPACE key; otherwise, false.</returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		/// <param name="rowIndex">The index of the cell's parent row. </param>
		// Token: 0x06001161 RID: 4449 RVA: 0x000454DC File Offset: 0x000436DC
		protected override bool KeyUpUnsharesRow(KeyEventArgs e, int rowIndex)
		{
			return e.KeyData == Keys.Space;
		}

		/// <summary>Indicates whether a row will be unshared when the mouse button is held down while the pointer is on a cell in the row.</summary>
		/// <returns>true if the user pressed the left mouse button; otherwise, false.</returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06001162 RID: 4450 RVA: 0x000454E8 File Offset: 0x000436E8
		protected override bool MouseDownUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return e.Button == MouseButtons.Left;
		}

		/// <summary>Indicates whether a row will be unshared when the mouse pointer moves over a cell in the row.</summary>
		/// <returns>true if the cell was the last cell receiving a mouse click; otherwise, false.</returns>
		/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
		// Token: 0x06001163 RID: 4451 RVA: 0x000454F8 File Offset: 0x000436F8
		protected override bool MouseEnterUnsharesRow(int rowIndex)
		{
			return false;
		}

		/// <summary>Indicates whether a row will be unshared when the mouse pointer leaves the row.</summary>
		/// <returns>true if the button displayed by the cell is in the pressed state; otherwise, false.</returns>
		/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
		// Token: 0x06001164 RID: 4452 RVA: 0x000454FC File Offset: 0x000436FC
		protected override bool MouseLeaveUnsharesRow(int rowIndex)
		{
			return this.button_state == PushButtonState.Pressed;
		}

		/// <summary>Indicates whether a row will be unshared when the mouse button is released while the pointer is on a cell in the row. </summary>
		/// <returns>true if the left mouse button was released; otherwise, false.</returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06001165 RID: 4453 RVA: 0x00045508 File Offset: 0x00043708
		protected override bool MouseUpUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return e.Button == MouseButtons.Left;
		}

		/// <summary>Called when a character key is pressed while the focus is on the cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
		// Token: 0x06001166 RID: 4454 RVA: 0x00045518 File Offset: 0x00043718
		protected override void OnKeyDown(KeyEventArgs e, int rowIndex)
		{
			if ((e.KeyData & Keys.Space) == Keys.Space)
			{
				this.button_state = PushButtonState.Pressed;
				base.DataGridView.InvalidateCell(this);
			}
		}

		/// <summary>Called when a character key is released while the focus is on the cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data</param>
		/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
		// Token: 0x06001167 RID: 4455 RVA: 0x00045540 File Offset: 0x00043740
		protected override void OnKeyUp(KeyEventArgs e, int rowIndex)
		{
			if ((e.KeyData & Keys.Space) == Keys.Space)
			{
				this.button_state = PushButtonState.Normal;
				base.DataGridView.InvalidateCell(this);
			}
		}

		/// <summary>Called when the focus moves from the cell.</summary>
		/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
		/// <param name="throughMouseClick">true if focus left the cell as a result of user mouse click; false if focus left due to a programmatic cell change.</param>
		// Token: 0x06001168 RID: 4456 RVA: 0x00045568 File Offset: 0x00043768
		protected override void OnLeave(int rowIndex, bool throughMouseClick)
		{
			if (this.button_state != PushButtonState.Normal)
			{
				this.button_state = PushButtonState.Normal;
				base.DataGridView.InvalidateCell(this);
			}
		}

		/// <summary>Called when the mouse button is held down while the pointer is on the cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06001169 RID: 4457 RVA: 0x0004558C File Offset: 0x0004378C
		protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				this.button_state = PushButtonState.Pressed;
				base.DataGridView.InvalidateCell(this);
			}
		}

		/// <summary>Called when the mouse pointer moves out of the cell.</summary>
		/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
		// Token: 0x0600116A RID: 4458 RVA: 0x000455B8 File Offset: 0x000437B8
		protected override void OnMouseLeave(int rowIndex)
		{
			if (this.button_state != PushButtonState.Normal)
			{
				this.button_state = PushButtonState.Normal;
				base.DataGridView.InvalidateCell(this);
			}
		}

		/// <summary>Called when the mouse pointer moves while it is over the cell. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x0600116B RID: 4459 RVA: 0x000455DC File Offset: 0x000437DC
		protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
		{
			if (this.button_state != PushButtonState.Normal && this.button_state != PushButtonState.Hot)
			{
				this.button_state = PushButtonState.Hot;
				base.DataGridView.InvalidateCell(this);
			}
		}

		/// <summary>Called when the mouse button is released while the pointer is on the cell. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x0600116C RID: 4460 RVA: 0x0004560C File Offset: 0x0004380C
		protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				this.button_state = PushButtonState.Normal;
				base.DataGridView.InvalidateCell(this);
			}
		}

		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
		/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="rowIndex">The row index of the cell that is being painted.</param>
		/// <param name="elementState"></param>
		/// <param name="value">The data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="formattedValue">The formatted data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="errorText">An error message that is associated with the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the cell.</param>
		/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles for the cell that is being painted.</param>
		/// <param name="paintParts">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values that specifies which parts of the cell need to be painted.</param>
		// Token: 0x0600116D RID: 4461 RVA: 0x00045638 File Offset: 0x00043838
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00045660 File Offset: 0x00043860
		internal override void PaintPartBackground(Graphics graphics, Rectangle cellBounds, DataGridViewCellStyle style)
		{
			ButtonRenderer.DrawButton(graphics, cellBounds, this.button_state);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00045670 File Offset: 0x00043870
		internal override void PaintPartSelectionBackground(Graphics graphics, Rectangle cellBounds, DataGridViewElementStates cellState, DataGridViewCellStyle cellStyle)
		{
			cellBounds.Inflate(-2, -2);
			base.PaintPartSelectionBackground(graphics, cellBounds, cellState, cellStyle);
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00045688 File Offset: 0x00043888
		internal override void PaintPartContent(Graphics graphics, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, DataGridViewCellStyle cellStyle, object formattedValue)
		{
			Color color = ((!this.Selected) ? cellStyle.ForeColor : cellStyle.SelectionForeColor);
			TextFormatFlags textFormatFlags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.TextBoxControl | TextFormatFlags.EndEllipsis;
			cellBounds.Height -= 2;
			cellBounds.Width -= 2;
			if (formattedValue != null)
			{
				TextRenderer.DrawText(graphics, formattedValue.ToString(), cellStyle.Font, cellBounds, color, textFormatFlags);
			}
		}

		// Token: 0x04000AD4 RID: 2772
		private FlatStyle flatStyle;

		// Token: 0x04000AD5 RID: 2773
		private bool useColumnTextForButtonValue;

		// Token: 0x04000AD6 RID: 2774
		private PushButtonState button_state;

		/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewButtonCell" /> to accessibility client applications.</summary>
		// Token: 0x020000E1 RID: 225
		protected class DataGridViewButtonCellAccessibleObject : DataGridViewCell.DataGridViewCellAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewButtonCell.DataGridViewButtonCellAccessibleObject" /> class. </summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewButtonCell.DataGridViewButtonCellAccessibleObject" />.</param>
			// Token: 0x06001171 RID: 4465 RVA: 0x000456F8 File Offset: 0x000438F8
			public DataGridViewButtonCellAccessibleObject(DataGridViewCell owner)
				: base(owner)
			{
			}

			/// <summary>Gets a <see cref="T:System.String" /> that represents the default action of the <see cref="T:System.Windows.Forms.DataGridViewButtonCell.DataGridViewButtonCellAccessibleObject" />.</summary>
			/// <returns>The <see cref="T:System.String" /> "Press" if the <see cref="P:System.Windows.Forms.DataGridViewCell.ReadOnly" /> property is set to false; otherwise, an empty <see cref="T:System.String" /> ("").</returns>
			// Token: 0x170003B9 RID: 953
			// (get) Token: 0x06001172 RID: 4466 RVA: 0x00045704 File Offset: 0x00043904
			public override string DefaultAction
			{
				get
				{
					if (base.Owner.ReadOnly)
					{
						return "Press";
					}
					return string.Empty;
				}
			}

			/// <summary>Performs the default action of the <see cref="T:System.Windows.Forms.DataGridViewButtonCell.DataGridViewButtonCellAccessibleObject" /></summary>
			/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Windows.Forms.DataGridViewButtonCell" /> returned by the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property does not belong to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-The <see cref="T:System.Windows.Forms.DataGridViewButtonCell" /> returned by the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property belongs to a shared row.</exception>
			// Token: 0x06001173 RID: 4467 RVA: 0x00045724 File Offset: 0x00043924
			public override void DoDefaultAction()
			{
			}

			/// <summary>Gets the number of child accessible objects that belong to the <see cref="T:System.Windows.Forms.DataGridViewButtonCell.DataGridViewButtonCellAccessibleObject" />.</summary>
			/// <returns>The value –1.</returns>
			// Token: 0x06001174 RID: 4468 RVA: 0x00045728 File Offset: 0x00043928
			public override int GetChildCount()
			{
				return -1;
			}
		}
	}
}
