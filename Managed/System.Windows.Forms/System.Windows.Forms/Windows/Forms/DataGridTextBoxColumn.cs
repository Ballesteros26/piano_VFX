using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System.Windows.Forms
{
	/// <summary>Hosts a <see cref="T:System.Windows.Forms.TextBox" /> control in a cell of a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> for editing strings.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000CE RID: 206
	public class DataGridTextBoxColumn : DataGridColumnStyle
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> class.</summary>
		// Token: 0x06000E0B RID: 3595 RVA: 0x00037FA0 File Offset: 0x000361A0
		public DataGridTextBoxColumn()
			: this(null, string.Empty, false)
		{
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> with a specified <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> for the column with which the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> will be associated. </param>
		// Token: 0x06000E0C RID: 3596 RVA: 0x00037FB0 File Offset: 0x000361B0
		public DataGridTextBoxColumn(PropertyDescriptor prop)
			: this(prop, string.Empty, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> class using the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />. Specifies whether the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> is a default column.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to be associated with the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" />. </param>
		/// <param name="isDefault">Specifies whether the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> is a default column. </param>
		// Token: 0x06000E0D RID: 3597 RVA: 0x00037FC0 File Offset: 0x000361C0
		public DataGridTextBoxColumn(PropertyDescriptor prop, bool isDefault)
			: this(prop, string.Empty, isDefault)
		{
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> with the specified <see cref="T:System.ComponentModel.PropertyDescriptor" /> and format.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> for the column with which the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> will be associated. </param>
		/// <param name="format">The format used to format the column values. </param>
		// Token: 0x06000E0E RID: 3598 RVA: 0x00037FD0 File Offset: 0x000361D0
		public DataGridTextBoxColumn(PropertyDescriptor prop, string format)
			: this(prop, format, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> class with a specified <see cref="T:System.ComponentModel.PropertyDescriptor" /> and format. Specifies whether the column is the default column.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to be associated with the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" />. </param>
		/// <param name="format">The format used. </param>
		/// <param name="isDefault">Specifies whether the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> is the default column. </param>
		// Token: 0x06000E0F RID: 3599 RVA: 0x00037FDC File Offset: 0x000361DC
		public DataGridTextBoxColumn(PropertyDescriptor prop, string format, bool isDefault)
			: base(prop)
		{
			this.Format = format;
			this.is_default = isDefault;
			this.textbox = new DataGridTextBox();
			this.textbox.Multiline = true;
			this.textbox.WordWrap = false;
			this.textbox.BorderStyle = BorderStyle.None;
			this.textbox.Visible = false;
		}

		/// <summary>Gets or sets the character(s) that specify how text is formatted.</summary>
		/// <returns>The character or characters that specify how text is formatted.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x00038054 File Offset: 0x00036254
		// (set) Token: 0x06000E12 RID: 3602 RVA: 0x0003805C File Offset: 0x0003625C
		[Editor("System.Windows.Forms.Design.DataGridColumnStyleFormatEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue(null)]
		public string Format
		{
			get
			{
				return this.format;
			}
			set
			{
				if (value != this.format)
				{
					this.format = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the culture specific information used to determine how values are formatted.</summary>
		/// <returns>An object that implements the <see cref="T:System.IFormatProvider" /> interface, such as the <see cref="T:System.Globalization.CultureInfo" /> class.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x0003807C File Offset: 0x0003627C
		// (set) Token: 0x06000E14 RID: 3604 RVA: 0x00038084 File Offset: 0x00036284
		[Browsable(false)]
		[EditorBrowsable(2)]
		public IFormatProvider FormatInfo
		{
			get
			{
				return this.format_provider;
			}
			set
			{
				if (value != this.format_provider)
				{
					this.format_provider = value;
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> for the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" />.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that formats the values displayed in the column.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000329 RID: 809
		// (set) Token: 0x06000E15 RID: 3605 RVA: 0x0003809C File Offset: 0x0003629C
		[DefaultValue(null)]
		public override PropertyDescriptor PropertyDescriptor
		{
			set
			{
				base.PropertyDescriptor = value;
			}
		}

		/// <summary>Sets a value indicating whether the text box column is read-only.</summary>
		/// <returns>true if the text box column is read-only; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x000380A8 File Offset: 0x000362A8
		// (set) Token: 0x06000E17 RID: 3607 RVA: 0x000380B0 File Offset: 0x000362B0
		public override bool ReadOnly
		{
			get
			{
				return base.ReadOnly;
			}
			set
			{
				base.ReadOnly = value;
			}
		}

		/// <summary>Gets the hosted <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TextBox" /> control hosted by the column.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x000380BC File Offset: 0x000362BC
		[Browsable(false)]
		public virtual TextBox TextBox
		{
			get
			{
				return this.textbox;
			}
		}

		/// <summary>Initiates a request to interrupt an edit procedure.</summary>
		/// <param name="rowNum">The number of the row in which an edit operation is being interrupted. </param>
		// Token: 0x06000E19 RID: 3609 RVA: 0x000380C4 File Offset: 0x000362C4
		protected internal override void Abort(int rowNum)
		{
			this.EndEdit();
		}

		/// <summary>Inititates a request to complete an editing procedure.</summary>
		/// <returns>true if the value was successfully committed; otherwise, false.</returns>
		/// <param name="dataSource">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> control the column belongs to. </param>
		/// <param name="rowNum">The number of the edited row. </param>
		// Token: 0x06000E1A RID: 3610 RVA: 0x000380CC File Offset: 0x000362CC
		protected internal override bool Commit(CurrencyManager dataSource, int rowNum)
		{
			this.textbox.Bounds = Rectangle.Empty;
			if (this.textbox.IsInEditOrNavigateMode)
			{
				return true;
			}
			try
			{
				string formattedValue = this.GetFormattedValue(dataSource, rowNum);
				if (formattedValue != this.textbox.Text)
				{
					if (this.textbox.Text == this.NullText)
					{
						this.SetColumnValueAtRow(dataSource, rowNum, DBNull.Value);
					}
					else
					{
						object obj = this.textbox.Text;
						TypeConverter converter = TypeDescriptor.GetConverter(this.PropertyDescriptor.PropertyType);
						if (converter != null && converter.CanConvertFrom(typeof(string)))
						{
							obj = converter.ConvertFrom(null, CultureInfo.CurrentCulture, this.textbox.Text);
							if (converter.CanConvertTo(typeof(string)))
							{
								this.textbox.Text = (string)converter.ConvertTo(null, CultureInfo.CurrentCulture, obj, typeof(string));
							}
						}
						this.SetColumnValueAtRow(dataSource, rowNum, obj);
					}
				}
			}
			catch
			{
				return false;
			}
			this.EndEdit();
			return true;
		}

		/// <summary>Informs the column that the focus is being conceded.</summary>
		// Token: 0x06000E1B RID: 3611 RVA: 0x00038214 File Offset: 0x00036414
		protected internal override void ConcedeFocus()
		{
			this.HideEditBox();
		}

		/// <summary>Prepares a cell for editing.</summary>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> control the column belongs to. </param>
		/// <param name="rowNum">The row number in this column being edited. </param>
		/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> in which the control is to be sited. </param>
		/// <param name="readOnly">A value indicating whether the column is a read-only. true if the value is read-only; otherwise, false. </param>
		/// <param name="displayText">The text to display in the control. </param>
		/// <param name="cellIsVisible">A value indicating whether the cell is visible. true if the cell is visible; otherwise, false. </param>
		// Token: 0x06000E1C RID: 3612 RVA: 0x0003821C File Offset: 0x0003641C
		protected internal override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string displayText, bool cellIsVisible)
		{
			this.grid.SuspendLayout();
			this.textbox.TextChanged -= new EventHandler(this.textbox_TextChanged);
			this.textbox.TextAlign = this.alignment;
			bool flag = base.TableStyleReadOnly || this.ReadOnly || readOnly;
			if (!flag && displayText != null)
			{
				this.textbox.Text = displayText;
				this.textbox.IsInEditOrNavigateMode = false;
			}
			else
			{
				this.textbox.Text = this.GetFormattedValue(source, rowNum);
			}
			this.textbox.TextChanged += new EventHandler(this.textbox_TextChanged);
			this.textbox.ReadOnly = flag;
			this.textbox.Bounds = new Rectangle(new Point(bounds.X + DataGridTextBoxColumn.offset_x, bounds.Y + DataGridTextBoxColumn.offset_y), new Size(bounds.Width - DataGridTextBoxColumn.offset_x - 1, bounds.Height - DataGridTextBoxColumn.offset_y - 1));
			this.textbox.Visible = cellIsVisible;
			this.textbox.SelectAll();
			this.textbox.Focus();
			this.grid.ResumeLayout(false);
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00038360 File Offset: 0x00036560
		private void textbox_TextChanged(object o, EventArgs e)
		{
			this.textbox.IsInEditOrNavigateMode = false;
			this.grid.EditRowChanged(this);
		}

		/// <summary>Ends an edit operation on the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</summary>
		// Token: 0x06000E1E RID: 3614 RVA: 0x0003837C File Offset: 0x0003657C
		protected void EndEdit()
		{
			this.textbox.TextChanged -= new EventHandler(this.textbox_TextChanged);
			this.HideEditBox();
		}

		/// <summary>Enters a <see cref="F:System.DBNull.Value" /> in the column.</summary>
		// Token: 0x06000E1F RID: 3615 RVA: 0x0003839C File Offset: 0x0003659C
		protected internal override void EnterNullValue()
		{
			this.textbox.Text = this.NullText;
		}

		/// <summary>Gets the height of a cell in a <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</summary>
		/// <returns>The height of a cell.</returns>
		// Token: 0x06000E20 RID: 3616 RVA: 0x000383B0 File Offset: 0x000365B0
		protected internal override int GetMinimumHeight()
		{
			return base.FontHeight + 3;
		}

		/// <summary>Gets the height to be used in for automatically resizing columns.</summary>
		/// <returns>The height the cells automatically resize to.</returns>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> object used to draw shapes on the screen. </param>
		/// <param name="value">The value to draw. </param>
		// Token: 0x06000E21 RID: 3617 RVA: 0x000383BC File Offset: 0x000365BC
		protected internal override int GetPreferredHeight(Graphics g, object value)
		{
			string formattedValue = this.GetFormattedValue(value);
			Regex regex = new Regex("/\r\n/");
			int count = regex.Matches(formattedValue).Count;
			return this.DataGridTableStyle.DataGrid.Font.Height * (count + 1) + 1;
		}

		/// <summary>Returns the optimum width and height of the cell in a specified row relative to the specified value.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that contains the dimensions of the cell.</returns>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> object used to draw shapes on the screen. </param>
		/// <param name="value">The value to draw. </param>
		// Token: 0x06000E22 RID: 3618 RVA: 0x00038404 File Offset: 0x00036604
		protected internal override Size GetPreferredSize(Graphics g, object value)
		{
			string formattedValue = this.GetFormattedValue(value);
			Size size = Size.Ceiling(g.MeasureString(formattedValue, this.DataGridTableStyle.DataGrid.Font));
			size.Width += 4;
			return size;
		}

		/// <summary>Hides the <see cref="T:System.Windows.Forms.DataGridTextBox" /> control and moves the focus to the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
		// Token: 0x06000E23 RID: 3619 RVA: 0x00038448 File Offset: 0x00036648
		protected void HideEditBox()
		{
			if (!this.textbox.Visible)
			{
				return;
			}
			this.grid.SuspendLayout();
			this.textbox.Bounds = Rectangle.Empty;
			this.textbox.Visible = false;
			this.textbox.IsInEditOrNavigateMode = true;
			this.grid.ResumeLayout(false);
		}

		/// <summary>Paints the a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, <see cref="T:System.Windows.Forms.CurrencyManager" />, and row number.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> object to draw to. </param>
		/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into. </param>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> the that contains the column. </param>
		/// <param name="rowNum">The number of the row in the underlying data table. </param>
		// Token: 0x06000E24 RID: 3620 RVA: 0x000384A8 File Offset: 0x000366A8
		protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum)
		{
			this.Paint(g, bounds, source, rowNum, false);
		}

		/// <summary>Paints a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, <see cref="T:System.Windows.Forms.CurrencyManager" />, row number, and alignment.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> object to draw to. </param>
		/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into. </param>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> the that contains the column. </param>
		/// <param name="rowNum">The number of the row in the underlying data table. </param>
		/// <param name="alignToRight">A value indicating whether to align the column's content to the right. true if the content should be aligned to the right; otherwise, false. </param>
		// Token: 0x06000E25 RID: 3621 RVA: 0x000384B8 File Offset: 0x000366B8
		protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, bool alignToRight)
		{
			this.Paint(g, bounds, source, rowNum, ThemeEngine.Current.ResPool.GetSolidBrush(this.DataGridTableStyle.BackColor), ThemeEngine.Current.ResPool.GetSolidBrush(this.DataGridTableStyle.ForeColor), alignToRight);
		}

		/// <summary>Paints a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, <see cref="T:System.Windows.Forms.CurrencyManager" />, row number, <see cref="T:System.Drawing.Brush" />, and foreground color.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> object to draw to. </param>
		/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into. </param>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> the that contains the column. </param>
		/// <param name="rowNum">The number of the row in the underlying data table. </param>
		/// <param name="backBrush">A <see cref="T:System.Drawing.Brush" /> that paints the background. </param>
		/// <param name="foreBrush">A <see cref="T:System.Drawing.Brush" /> that paints the foreground color. </param>
		/// <param name="alignToRight">A value indicating whether to align the column's content to the right. true if the content should be aligned to the right; otherwise, false. </param>
		// Token: 0x06000E26 RID: 3622 RVA: 0x00038508 File Offset: 0x00036708
		protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			this.PaintText(g, bounds, this.GetFormattedValue(source, rowNum), backBrush, foreBrush, alignToRight);
		}

		/// <summary>Draws the text and rectangle at the given location with the specified alignment.</summary>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> object used to draw the string. </param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> which contains the boundary data of the rectangle. </param>
		/// <param name="text">The string to be drawn to the screen. </param>
		/// <param name="alignToRight">A value indicating whether the text is right-aligned. </param>
		// Token: 0x06000E27 RID: 3623 RVA: 0x0003852C File Offset: 0x0003672C
		protected void PaintText(Graphics g, Rectangle bounds, string text, bool alignToRight)
		{
			this.PaintText(g, bounds, text, ThemeEngine.Current.ResPool.GetSolidBrush(this.DataGridTableStyle.BackColor), ThemeEngine.Current.ResPool.GetSolidBrush(this.DataGridTableStyle.ForeColor), alignToRight);
		}

		/// <summary>Draws the text and rectangle at the specified location with the specified colors and alignment.</summary>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> object used to draw the string. </param>
		/// <param name="textBounds">A <see cref="T:System.Drawing.Rectangle" /> which contains the boundary data of the rectangle. </param>
		/// <param name="text">The string to be drawn to the screen. </param>
		/// <param name="backBrush">A <see cref="T:System.Drawing.Brush" /> that determines the rectangle's background color </param>
		/// <param name="foreBrush">A <see cref="T:System.Drawing.Brush" /> that determines the rectangles foreground color. </param>
		/// <param name="alignToRight">A value indicating whether the text is right-aligned. </param>
		// Token: 0x06000E28 RID: 3624 RVA: 0x00038578 File Offset: 0x00036778
		protected void PaintText(Graphics g, Rectangle textBounds, string text, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			if (alignToRight)
			{
				this.string_format.FormatFlags |= 1;
			}
			else
			{
				this.string_format.FormatFlags &= -2;
			}
			HorizontalAlignment alignment = this.alignment;
			if (alignment != HorizontalAlignment.Right)
			{
				if (alignment != HorizontalAlignment.Center)
				{
					this.string_format.Alignment = 0;
				}
				else
				{
					this.string_format.Alignment = 1;
				}
			}
			else
			{
				this.string_format.Alignment = 2;
			}
			g.FillRectangle(backBrush, textBounds);
			base.PaintGridLine(g, textBounds);
			textBounds.X += DataGridTextBoxColumn.offset_x;
			textBounds.Width -= DataGridTextBoxColumn.offset_x;
			textBounds.Y += DataGridTextBoxColumn.offset_y;
			textBounds.Height -= DataGridTextBoxColumn.offset_y;
			this.string_format.FormatFlags |= 4096;
			g.DrawString(text, this.DataGridTableStyle.DataGrid.Font, foreBrush, textBounds, this.string_format);
		}

		/// <summary>Removes the reference that the <see cref="T:System.Windows.Forms.DataGrid" /> holds to the control used to edit data.</summary>
		// Token: 0x06000E29 RID: 3625 RVA: 0x000386A0 File Offset: 0x000368A0
		protected internal override void ReleaseHostedControl()
		{
			if (this.textbox == null)
			{
				return;
			}
			this.grid.SuspendLayout();
			this.grid.Controls.Remove(this.textbox);
			this.grid.Invalidate(new Rectangle(this.textbox.Location, this.textbox.Size));
			this.textbox.Dispose();
			this.textbox = null;
			this.grid.ResumeLayout(false);
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.TextBox" /> control to the <see cref="T:System.Windows.Forms.DataGrid" /> control's <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.DataGrid" /> control the <see cref="T:System.Windows.Forms.TextBox" /> control is added to. </param>
		// Token: 0x06000E2A RID: 3626 RVA: 0x00038720 File Offset: 0x00036920
		protected override void SetDataGridInColumn(DataGrid value)
		{
			base.SetDataGridInColumn(value);
			if (value == null)
			{
				return;
			}
			this.textbox.SetDataGrid(this.grid);
			this.grid.SuspendLayout();
			this.grid.Controls.Add(this.textbox);
			this.grid.ResumeLayout(false);
		}

		/// <summary>Updates the user interface.</summary>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> that supplies the data. </param>
		/// <param name="rowNum">The index of the row to update. </param>
		/// <param name="displayText">The text that will be displayed in the cell. </param>
		// Token: 0x06000E2B RID: 3627 RVA: 0x0003877C File Offset: 0x0003697C
		protected internal override void UpdateUI(CurrencyManager source, int rowNum, string displayText)
		{
			if (this.textbox.Visible && this.textbox.IsInEditOrNavigateMode)
			{
				this.textbox.Text = this.GetFormattedValue(source, rowNum);
			}
			else
			{
				this.textbox.Text = displayText;
			}
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x000387D0 File Offset: 0x000369D0
		private string GetFormattedValue(CurrencyManager source, int rowNum)
		{
			object columnValueAtRow = this.GetColumnValueAtRow(source, rowNum);
			return this.GetFormattedValue(columnValueAtRow);
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x000387F0 File Offset: 0x000369F0
		private string GetFormattedValue(object obj)
		{
			if (DBNull.Value.Equals(obj) || obj == null)
			{
				return this.NullText;
			}
			if (this.format != null && this.format != string.Empty && obj is IFormattable)
			{
				return ((IFormattable)obj).ToString(this.format, this.format_provider);
			}
			TypeConverter converter = TypeDescriptor.GetConverter(this.PropertyDescriptor.PropertyType);
			if (converter != null && converter.CanConvertTo(typeof(string)))
			{
				return (string)converter.ConvertTo(null, CultureInfo.CurrentCulture, obj, typeof(string));
			}
			return obj.ToString();
		}

		// Token: 0x040009B8 RID: 2488
		private string format;

		// Token: 0x040009B9 RID: 2489
		private IFormatProvider format_provider;

		// Token: 0x040009BA RID: 2490
		private StringFormat string_format = new StringFormat();

		// Token: 0x040009BB RID: 2491
		private DataGridTextBox textbox;

		// Token: 0x040009BC RID: 2492
		private static readonly int offset_x = 2;

		// Token: 0x040009BD RID: 2493
		private static readonly int offset_y = 2;
	}
}
