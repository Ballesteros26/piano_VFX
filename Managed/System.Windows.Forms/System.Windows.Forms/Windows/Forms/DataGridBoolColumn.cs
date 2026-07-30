using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Specifies a column in which each cell contains a check box for representing a Boolean value.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C2 RID: 194
	public class DataGridBoolColumn : DataGridColumnStyle
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridBoolColumn" /> class.</summary>
		// Token: 0x06000CED RID: 3309 RVA: 0x0003574C File Offset: 0x0003394C
		public DataGridBoolColumn()
			: this(null, false)
		{
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Windows.Forms.DataGridBoolColumn" /> with the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> associated with the column. </param>
		// Token: 0x06000CEE RID: 3310 RVA: 0x00035758 File Offset: 0x00033958
		public DataGridBoolColumn(PropertyDescriptor prop)
			: this(prop, false)
		{
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Windows.Forms.DataGridBoolColumn" /> with the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />, and specifying whether the column style is a default column.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> associated with the column. </param>
		/// <param name="isDefault">true to specify the column as the default; otherwise, false. </param>
		// Token: 0x06000CEF RID: 3311 RVA: 0x00035764 File Offset: 0x00033964
		public DataGridBoolColumn(PropertyDescriptor prop, bool isDefault)
			: base(prop)
		{
			this.false_value = false;
			this.null_value = null;
			this.true_value = true;
			this.allow_null = true;
			this.is_default = isDefault;
			this.checkbox_size = new Size(ThemeEngine.Current.DataGridMinimumColumnCheckBoxWidth, ThemeEngine.Current.DataGridMinimumColumnCheckBoxHeight);
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x000357C4 File Offset: 0x000339C4
		// Note: this type is marked as 'beforefieldinit'.
		static DataGridBoolColumn()
		{
			DataGridBoolColumn.AllowNullChangedEvent = new object();
			DataGridBoolColumn.FalseValueChangedEvent = new object();
			DataGridBoolColumn.TrueValueChangedEvent = new object();
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridBoolColumn.AllowNull" /> property is changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000CC RID: 204
		// (add) Token: 0x06000CF1 RID: 3313 RVA: 0x000357E4 File Offset: 0x000339E4
		// (remove) Token: 0x06000CF2 RID: 3314 RVA: 0x000357F8 File Offset: 0x000339F8
		public event EventHandler AllowNullChanged
		{
			add
			{
				base.Events.AddHandler(DataGridBoolColumn.AllowNullChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridBoolColumn.AllowNullChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridBoolColumn.FalseValue" /> property is changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000CD RID: 205
		// (add) Token: 0x06000CF3 RID: 3315 RVA: 0x0003580C File Offset: 0x00033A0C
		// (remove) Token: 0x06000CF4 RID: 3316 RVA: 0x00035820 File Offset: 0x00033A20
		public event EventHandler FalseValueChanged
		{
			add
			{
				base.Events.AddHandler(DataGridBoolColumn.FalseValueChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridBoolColumn.FalseValueChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridBoolColumn.TrueValue" /> property value is changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000CE RID: 206
		// (add) Token: 0x06000CF5 RID: 3317 RVA: 0x00035834 File Offset: 0x00033A34
		// (remove) Token: 0x06000CF6 RID: 3318 RVA: 0x00035848 File Offset: 0x00033A48
		public event EventHandler TrueValueChanged
		{
			add
			{
				base.Events.AddHandler(DataGridBoolColumn.TrueValueChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridBoolColumn.TrueValueChangedEvent, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether null values are allowed.</summary>
		/// <returns>true if null values are allowed, otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x0003585C File Offset: 0x00033A5C
		// (set) Token: 0x06000CF8 RID: 3320 RVA: 0x00035864 File Offset: 0x00033A64
		[DefaultValue(true)]
		public bool AllowNull
		{
			get
			{
				return this.allow_null;
			}
			set
			{
				if (value != this.allow_null)
				{
					this.allow_null = value;
					EventHandler eventHandler = (EventHandler)base.Events[DataGridBoolColumn.AllowNullChangedEvent];
					if (eventHandler != null)
					{
						eventHandler.Invoke(this, EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Gets or sets the actual value used when setting the value of the column to false.</summary>
		/// <returns>The value, typed as <see cref="T:System.Object" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x000358AC File Offset: 0x00033AAC
		// (set) Token: 0x06000CFA RID: 3322 RVA: 0x000358B4 File Offset: 0x00033AB4
		[DefaultValue(false)]
		[TypeConverter(typeof(StringConverter))]
		public object FalseValue
		{
			get
			{
				return this.false_value;
			}
			set
			{
				if (value != this.false_value)
				{
					this.false_value = value;
					EventHandler eventHandler = (EventHandler)base.Events[DataGridBoolColumn.FalseValueChangedEvent];
					if (eventHandler != null)
					{
						eventHandler.Invoke(this, EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Gets or sets the actual value used when setting the value of the column to <see cref="F:System.DBNull.Value" />.</summary>
		/// <returns>The value, typed as <see cref="T:System.Object" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x000358FC File Offset: 0x00033AFC
		// (set) Token: 0x06000CFC RID: 3324 RVA: 0x00035904 File Offset: 0x00033B04
		[TypeConverter(typeof(StringConverter))]
		public object NullValue
		{
			get
			{
				return this.null_value;
			}
			set
			{
				if (value != this.null_value)
				{
					this.null_value = value;
				}
			}
		}

		/// <summary>Gets or sets the actual value used when setting the value of the column to true.</summary>
		/// <returns>The value, typed as <see cref="T:System.Object" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x0003591C File Offset: 0x00033B1C
		// (set) Token: 0x06000CFE RID: 3326 RVA: 0x00035924 File Offset: 0x00033B24
		[DefaultValue(true)]
		[TypeConverter(typeof(StringConverter))]
		public object TrueValue
		{
			get
			{
				return this.true_value;
			}
			set
			{
				if (value != this.true_value)
				{
					this.true_value = value;
					EventHandler eventHandler = (EventHandler)base.Events[DataGridBoolColumn.TrueValueChangedEvent];
					if (eventHandler != null)
					{
						eventHandler.Invoke(this, EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Initiates a request to interrupt an edit procedure.</summary>
		/// <param name="rowNum">The number of the row in which an operation is being interrupted. </param>
		// Token: 0x06000CFF RID: 3327 RVA: 0x0003596C File Offset: 0x00033B6C
		protected internal override void Abort(int rowNum)
		{
			if (rowNum == this.editing_row)
			{
				this.grid.Invalidate(this.grid.GetCurrentCellBounds());
				this.editing_row = -1;
			}
		}

		/// <summary>Initiates a request to complete an editing procedure.</summary>
		/// <returns>true if the editing procedure committed successfully; otherwise, false.</returns>
		/// <param name="dataSource">The <see cref="T:System.Data.DataView" /> of the edited column. </param>
		/// <param name="rowNum">The number of the edited row. </param>
		// Token: 0x06000D00 RID: 3328 RVA: 0x00035998 File Offset: 0x00033B98
		protected internal override bool Commit(CurrencyManager dataSource, int rowNum)
		{
			if (rowNum == this.editing_row)
			{
				this.SetColumnValueAtRow(dataSource, rowNum, this.FromStateToValue(this.editing_state));
				this.grid.Invalidate(this.grid.GetCurrentCellBounds());
				this.editing_row = -1;
			}
			return true;
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x000359E4 File Offset: 0x00033BE4
		[MonoTODO("Stub, does nothing")]
		protected internal override void ConcedeFocus()
		{
			base.ConcedeFocus();
		}

		/// <summary>Prepares the cell for editing a value.</summary>
		/// <param name="source">The <see cref="T:System.Data.DataView" /> of the edited cell. </param>
		/// <param name="rowNum">The row number of the edited cell. </param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> in which the control is to be sited. </param>
		/// <param name="readOnly">true if the value is read only; otherwise, false. </param>
		/// <param name="displayText">The text to display in the cell. </param>
		/// <param name="cellIsVisible">true to show the cell; otherwise, false. </param>
		// Token: 0x06000D02 RID: 3330 RVA: 0x000359EC File Offset: 0x00033BEC
		protected internal override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string displayText, bool cellIsVisible)
		{
			this.editing_row = rowNum;
			this.model_state = this.FromValueToState(this.GetColumnValueAtRow(source, rowNum));
			this.editing_state = this.model_state | DataGridBoolColumn.CheckState.Selected;
			this.grid.Invalidate(this.grid.GetCurrentCellBounds());
		}

		/// <summary>Enters a <see cref="F:System.DBNull.Value" /> into the column.</summary>
		/// <exception cref="T:System.Exception">The <see cref="P:System.Windows.Forms.DataGridBoolColumn.AllowNull" /> property is set to false. </exception>
		// Token: 0x06000D03 RID: 3331 RVA: 0x00035A38 File Offset: 0x00033C38
		[MonoTODO("Stub, does nothing")]
		protected internal override void EnterNullValue()
		{
			base.EnterNullValue();
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00035A40 File Offset: 0x00033C40
		private bool ValueEquals(object value, object obj)
		{
			return (value != null) ? value.Equals(obj) : (obj == null);
		}

		/// <summary>Gets the value at the specified row.</summary>
		/// <returns>The value, typed as <see cref="T:System.Object" />.</returns>
		/// <param name="lm">The <see cref="T:System.Windows.Forms.CurrencyManager" /> for the column. </param>
		/// <param name="row">The row number. </param>
		// Token: 0x06000D05 RID: 3333 RVA: 0x00035A58 File Offset: 0x00033C58
		protected internal override object GetColumnValueAtRow(CurrencyManager lm, int row)
		{
			object columnValueAtRow = base.GetColumnValueAtRow(lm, row);
			if (this.ValueEquals(DBNull.Value, columnValueAtRow))
			{
				return this.null_value;
			}
			if (this.ValueEquals(true, columnValueAtRow))
			{
				return this.true_value;
			}
			return this.false_value;
		}

		/// <summary>Gets the height of a cell in a column.</summary>
		/// <returns>The height of the column. The default is 16.</returns>
		// Token: 0x06000D06 RID: 3334 RVA: 0x00035AA8 File Offset: 0x00033CA8
		protected internal override int GetMinimumHeight()
		{
			return this.checkbox_size.Height;
		}

		/// <summary>Gets the height used when resizing columns.</summary>
		/// <returns>The height used to automatically resize cells in a column.</returns>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that draws on the screen. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that contains the value to be drawn to the screen. </param>
		// Token: 0x06000D07 RID: 3335 RVA: 0x00035AB8 File Offset: 0x00033CB8
		protected internal override int GetPreferredHeight(Graphics g, object value)
		{
			return this.checkbox_size.Height;
		}

		/// <summary>Gets the optimum width and height of a cell given a specific value to contain.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that contains the drawing information for the cell.</returns>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that draws the cell. </param>
		/// <param name="value">The value that must fit in the cell. </param>
		// Token: 0x06000D08 RID: 3336 RVA: 0x00035AC8 File Offset: 0x00033CC8
		protected internal override Size GetPreferredSize(Graphics g, object value)
		{
			return this.checkbox_size;
		}

		/// <summary>Draws the <see cref="T:System.Windows.Forms.DataGridBoolColumn" /> with the given <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" /> and row number.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw to. </param>
		/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into. </param>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the column. </param>
		/// <param name="rowNum">The number of the row referred to in the underlying data. </param>
		// Token: 0x06000D09 RID: 3337 RVA: 0x00035AD0 File Offset: 0x00033CD0
		protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum)
		{
			this.Paint(g, bounds, source, rowNum, false);
		}

		/// <summary>Draws the <see cref="T:System.Windows.Forms.DataGridBoolColumn" /> with the given <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, row number, and alignment settings.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw to. </param>
		/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into. </param>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the column. </param>
		/// <param name="rowNum">The number of the row in the underlying data table being referred to. </param>
		/// <param name="alignToRight">A value indicating whether to align the content to the right. true if the content is aligned to the right, otherwise, false. </param>
		// Token: 0x06000D0A RID: 3338 RVA: 0x00035AE0 File Offset: 0x00033CE0
		protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, bool alignToRight)
		{
			this.Paint(g, bounds, source, rowNum, ThemeEngine.Current.ResPool.GetSolidBrush(this.DataGridTableStyle.BackColor), ThemeEngine.Current.ResPool.GetSolidBrush(this.DataGridTableStyle.ForeColor), alignToRight);
		}

		/// <summary>Draws the <see cref="T:System.Windows.Forms.DataGridBoolColumn" /> with the given <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, row number, <see cref="T:System.Drawing.Brush" />, and <see cref="T:System.Drawing.Color" />.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw to. </param>
		/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into. </param>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the column. </param>
		/// <param name="rowNum">The number of the row in the underlying data table being referred to. </param>
		/// <param name="backBrush">A <see cref="T:System.Drawing.Brush" /> used to paint the background color. </param>
		/// <param name="foreBrush">A <see cref="T:System.Drawing.Color" /> used to paint the foreground color. </param>
		/// <param name="alignToRight">A value indicating whether to align the content to the right. true if the content is aligned to the right, otherwise, false. </param>
		// Token: 0x06000D0B RID: 3339 RVA: 0x00035B30 File Offset: 0x00033D30
		protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			Rectangle rectangle = default(Rectangle);
			DataGridBoolColumn.CheckState checkState;
			if (rowNum == this.editing_row)
			{
				checkState = this.editing_state;
			}
			else
			{
				checkState = this.FromValueToState(this.GetColumnValueAtRow(source, rowNum));
			}
			rectangle.X = bounds.X + (bounds.Width - this.checkbox_size.Width - 2) / 2;
			rectangle.Y = bounds.Y + (bounds.Height - this.checkbox_size.Height - 2) / 2;
			rectangle.Width = this.checkbox_size.Width - 2;
			rectangle.Height = this.checkbox_size.Height - 2;
			if ((checkState & DataGridBoolColumn.CheckState.Selected) == DataGridBoolColumn.CheckState.Selected)
			{
				backBrush = ThemeEngine.Current.ResPool.GetSolidBrush(this.grid.SelectionBackColor);
				checkState &= ~DataGridBoolColumn.CheckState.Selected;
			}
			g.FillRectangle(backBrush, bounds);
			ButtonState buttonState;
			switch (checkState)
			{
			case DataGridBoolColumn.CheckState.Checked:
				buttonState = ButtonState.Checked;
				goto IL_0114;
			case DataGridBoolColumn.CheckState.Null:
				buttonState = ButtonState.Inactive | ButtonState.Checked;
				goto IL_0114;
			}
			buttonState = ButtonState.Normal;
			IL_0114:
			ThemeEngine.Current.CPDrawCheckBox(g, rectangle, buttonState);
			base.PaintGridLine(g, bounds);
		}

		/// <summary>Sets the value of a specified row.</summary>
		/// <param name="lm">The <see cref="T:System.Windows.Forms.CurrencyManager" /> for the column. </param>
		/// <param name="row">The row number. </param>
		/// <param name="value">The value to set, typed as <see cref="T:System.Object" />. </param>
		// Token: 0x06000D0C RID: 3340 RVA: 0x00035C68 File Offset: 0x00033E68
		protected internal override void SetColumnValueAtRow(CurrencyManager lm, int row, object value)
		{
			object obj = null;
			if (this.ValueEquals(this.null_value, value))
			{
				obj = DBNull.Value;
			}
			else if (this.ValueEquals(this.true_value, value))
			{
				obj = true;
			}
			else if (this.ValueEquals(this.false_value, value))
			{
				obj = false;
			}
			base.SetColumnValueAtRow(lm, row, obj);
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00035CD4 File Offset: 0x00033ED4
		private object FromStateToValue(DataGridBoolColumn.CheckState state)
		{
			if ((state & DataGridBoolColumn.CheckState.Checked) == DataGridBoolColumn.CheckState.Checked)
			{
				return this.true_value;
			}
			if ((state & DataGridBoolColumn.CheckState.Null) == DataGridBoolColumn.CheckState.Null)
			{
				return this.null_value;
			}
			return this.false_value;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00035D08 File Offset: 0x00033F08
		private DataGridBoolColumn.CheckState FromValueToState(object obj)
		{
			if (this.ValueEquals(this.true_value, obj))
			{
				return DataGridBoolColumn.CheckState.Checked;
			}
			if (this.ValueEquals(this.null_value, obj))
			{
				return DataGridBoolColumn.CheckState.Null;
			}
			return DataGridBoolColumn.CheckState.UnChecked;
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00035D34 File Offset: 0x00033F34
		private DataGridBoolColumn.CheckState GetNextState(DataGridBoolColumn.CheckState state)
		{
			DataGridBoolColumn.CheckState checkState;
			switch (state & ~DataGridBoolColumn.CheckState.Selected)
			{
			case DataGridBoolColumn.CheckState.Checked:
				if (this.AllowNull)
				{
					checkState = DataGridBoolColumn.CheckState.Null;
				}
				else
				{
					checkState = DataGridBoolColumn.CheckState.UnChecked;
				}
				goto IL_0049;
			case DataGridBoolColumn.CheckState.Null:
				checkState = DataGridBoolColumn.CheckState.UnChecked;
				goto IL_0049;
			}
			checkState = DataGridBoolColumn.CheckState.Checked;
			IL_0049:
			return checkState | (state & DataGridBoolColumn.CheckState.Selected);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00035D94 File Offset: 0x00033F94
		internal override void OnKeyDown(KeyEventArgs ke, int row, int column)
		{
			Keys keyCode = ke.KeyCode;
			if (keyCode == Keys.Space)
			{
				this.NextState(row, column);
			}
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00035DC4 File Offset: 0x00033FC4
		internal override void OnMouseDown(MouseEventArgs e, int row, int column)
		{
			this.NextState(row, column);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00035DD0 File Offset: 0x00033FD0
		private void NextState(int row, int column)
		{
			this.grid.ColumnStartedEditing(default(Rectangle));
			this.editing_state = this.GetNextState(this.editing_state);
			this.grid.Invalidate(this.grid.GetCellBounds(row, column));
		}

		// Token: 0x04000946 RID: 2374
		private bool allow_null;

		// Token: 0x04000947 RID: 2375
		private object false_value;

		// Token: 0x04000948 RID: 2376
		private object null_value;

		// Token: 0x04000949 RID: 2377
		private object true_value;

		// Token: 0x0400094A RID: 2378
		private int editing_row;

		// Token: 0x0400094B RID: 2379
		private DataGridBoolColumn.CheckState editing_state;

		// Token: 0x0400094C RID: 2380
		private DataGridBoolColumn.CheckState model_state;

		// Token: 0x0400094D RID: 2381
		private Size checkbox_size;

		// Token: 0x020000C3 RID: 195
		[Flags]
		private enum CheckState
		{
			// Token: 0x04000952 RID: 2386
			Checked = 1,
			// Token: 0x04000953 RID: 2387
			UnChecked = 2,
			// Token: 0x04000954 RID: 2388
			Null = 4,
			// Token: 0x04000955 RID: 2389
			Selected = 8
		}
	}
}
