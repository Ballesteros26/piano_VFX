using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms.Design;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x020002A7 RID: 679
	internal class PropertyGridView : ScrollableControl, IWindowsFormsEditorService
	{
		// Token: 0x06002D72 RID: 11634 RVA: 0x000AEC08 File Offset: 0x000ACE08
		public PropertyGridView(PropertyGrid propertyGrid)
		{
			this.property_grid = propertyGrid;
			this.string_format = new StringFormat();
			this.string_format.FormatFlags = 4096;
			this.string_format.Trimming = 0;
			this.grid_textbox = new PropertyGridTextBox();
			this.grid_textbox.DropDownButtonClicked += new EventHandler(this.DropDownButtonClicked);
			this.grid_textbox.DialogButtonClicked += new EventHandler(this.DialogButtonClicked);
			this.dropdown_form = new PropertyGridView.PropertyGridDropDown();
			this.dropdown_form.FormBorderStyle = FormBorderStyle.None;
			this.dropdown_form.StartPosition = FormStartPosition.Manual;
			this.dropdown_form.ShowInTaskbar = false;
			this.dialog_form = new Form();
			this.dialog_form.StartPosition = FormStartPosition.Manual;
			this.dialog_form.FormBorderStyle = FormBorderStyle.None;
			this.dialog_form.ShowInTaskbar = false;
			this.dropdown_form_padding = new Padding(0, 0, 2, 2);
			this.row_height = this.Font.Height + this.font_height_padding;
			this.grid_textbox.Visible = false;
			this.grid_textbox.Font = this.Font;
			this.grid_textbox.BackColor = SystemColors.Window;
			this.grid_textbox.Validate += new CancelEventHandler(this.grid_textbox_Validate);
			this.grid_textbox.ToggleValue += new EventHandler(this.grid_textbox_ToggleValue);
			this.grid_textbox.KeyDown += this.grid_textbox_KeyDown;
			base.Controls.Add(this.grid_textbox);
			this.vbar = new ImplicitVScrollBar();
			this.vbar.Visible = false;
			this.vbar.Value = 0;
			this.vbar.ValueChanged += new EventHandler(this.VScrollBar_HandleValueChanged);
			this.vbar.Dock = DockStyle.Right;
			base.Controls.AddImplicit(this.vbar);
			this.resizing_grid = false;
			this.bold_font = new Font(this.Font, 1);
			this.inactive_text_brush = new SolidBrush(ThemeEngine.Current.ColorGrayText);
			base.ForeColorChanged += new EventHandler(this.RedrawEvent);
			base.BackColorChanged += new EventHandler(this.RedrawEvent);
			base.FontChanged += new EventHandler(this.RedrawEvent);
			base.SetStyle(ControlStyles.Selectable, true);
			base.SetStyle(ControlStyles.DoubleBuffer, true);
			base.SetStyle(ControlStyles.UserPaint, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.ResizeRedraw, true);
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06002D73 RID: 11635 RVA: 0x000AEE90 File Offset: 0x000AD090
		private GridEntry RootGridItem
		{
			get
			{
				return (GridEntry)this.property_grid.RootGridItem;
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06002D74 RID: 11636 RVA: 0x000AEEA4 File Offset: 0x000AD0A4
		// (set) Token: 0x06002D75 RID: 11637 RVA: 0x000AEEB8 File Offset: 0x000AD0B8
		private GridEntry SelectedGridItem
		{
			get
			{
				return (GridEntry)this.property_grid.SelectedGridItem;
			}
			set
			{
				this.property_grid.SelectedGridItem = value;
			}
		}

		// Token: 0x06002D76 RID: 11638 RVA: 0x000AEEC8 File Offset: 0x000AD0C8
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.bold_font = new Font(this.Font, 1);
			this.row_height = this.Font.Height + this.font_height_padding;
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x000AEF08 File Offset: 0x000AD108
		private void InvalidateItemLabel(GridEntry item)
		{
			base.Invalidate(new Rectangle(0, item.Top, this.SplitterLocation, this.row_height));
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x000AEF34 File Offset: 0x000AD134
		private void InvalidateItem(GridEntry item)
		{
			if (item == null)
			{
				return;
			}
			Rectangle rectangle;
			rectangle..ctor(0, item.Top, base.Width, this.row_height);
			base.Invalidate(rectangle);
			if (item.Expanded)
			{
				rectangle..ctor(0, item.Top + this.row_height, base.Width, base.Height - (item.Top + this.row_height));
				base.Invalidate(rectangle);
			}
		}

		// Token: 0x06002D79 RID: 11641 RVA: 0x000AEFAC File Offset: 0x000AD1AC
		protected override void OnDoubleClick(EventArgs e)
		{
			if (this.SelectedGridItem != null && this.SelectedGridItem.Expandable && !this.SelectedGridItem.PlusMinusBounds.Contains(this.last_click))
			{
				this.SelectedGridItem.Expanded = !this.SelectedGridItem.Expanded;
			}
			else
			{
				this.ToggleValue(this.SelectedGridItem);
			}
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x000AF01C File Offset: 0x000AD21C
		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.BackColor), base.ClientRectangle);
			int num = -this.vbar.Value * this.row_height;
			if (this.RootGridItem != null)
			{
				this.DrawGridItems(this.RootGridItem.GridItems, e, 1, ref num);
			}
			this.UpdateScrollBar();
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x000AF08C File Offset: 0x000AD28C
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if (this.vbar == null || !this.vbar.Visible)
			{
				return;
			}
			if (e.Delta < 0)
			{
				this.vbar.Value = Math.Min(this.vbar.Maximum - this.GetVisibleRowsCount() + 1, this.vbar.Value + SystemInformation.MouseWheelScrollLines);
			}
			else
			{
				this.vbar.Value = Math.Max(0, this.vbar.Value - SystemInformation.MouseWheelScrollLines);
			}
			base.OnMouseWheel(e);
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x000AF124 File Offset: 0x000AD324
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (this.RootGridItem == null)
			{
				return;
			}
			if (this.resizing_grid)
			{
				int num = Math.Max(e.X, 32);
				this.SplitterPercent = 1.0 * (double)num / (double)base.Width;
			}
			if (e.X > this.SplitterLocation - 3 && e.X < this.SplitterLocation + 3)
			{
				this.Cursor = Cursors.SizeWE;
			}
			else
			{
				this.Cursor = Cursors.Default;
			}
			base.OnMouseMove(e);
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x000AF1B8 File Offset: 0x000AD3B8
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			this.last_click = e.Location;
			if (this.RootGridItem == null)
			{
				return;
			}
			if (e.X > this.SplitterLocation - 3 && e.X < this.SplitterLocation + 3)
			{
				this.resizing_grid = true;
			}
			else
			{
				int num = -this.vbar.Value * this.row_height;
				GridItem selectedGridItem = this.GetSelectedGridItem(this.RootGridItem.GridItems, e.Y, ref num);
				if (selectedGridItem != null)
				{
					if (selectedGridItem.Expandable && ((GridEntry)selectedGridItem).PlusMinusBounds.Contains(e.X, e.Y))
					{
						selectedGridItem.Expanded = !selectedGridItem.Expanded;
					}
					this.SelectedGridItem = (GridEntry)selectedGridItem;
					if (!this.GridLabelHitTest(e.X))
					{
						this.grid_textbox.SendMouseDown(base.PointToScreen(e.Location));
					}
				}
			}
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x000AF2BC File Offset: 0x000AD4BC
		protected override void OnMouseUp(MouseEventArgs e)
		{
			this.resizing_grid = false;
			base.OnMouseUp(e);
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x000AF2CC File Offset: 0x000AD4CC
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (this.SelectedGridItem != null)
			{
				this.UpdateView();
			}
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x000AF2E8 File Offset: 0x000AD4E8
		private void UnfocusSelection()
		{
			base.Select(this);
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x000AF2F4 File Offset: 0x000AD4F4
		private void FocusSelection()
		{
			base.Select(this.grid_textbox);
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x000AF304 File Offset: 0x000AD504
		protected override bool ProcessDialogKey(Keys keyData)
		{
			GridEntry selectedGridItem = this.SelectedGridItem;
			if (selectedGridItem == null || !this.grid_textbox.Visible)
			{
				return base.ProcessDialogKey(keyData);
			}
			if (keyData == Keys.Tab)
			{
				this.FocusSelection();
				return true;
			}
			if (keyData == Keys.Return)
			{
				if (this.TrySetEntry(selectedGridItem, this.grid_textbox.Text))
				{
					this.UnfocusSelection();
				}
				return true;
			}
			if (keyData != Keys.Escape)
			{
				return false;
			}
			if (selectedGridItem.IsEditable)
			{
				this.UpdateItem(selectedGridItem);
			}
			this.UnfocusSelection();
			return true;
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x000AF398 File Offset: 0x000AD598
		private bool TrySetEntry(GridEntry entry, object value)
		{
			if (entry == null || this.grid_textbox.Text.Equals(entry.ValueText))
			{
				return true;
			}
			if (entry.IsEditable || (!entry.IsEditable && (entry.HasCustomEditor || entry.AcceptedValues != null)) || !entry.IsMerged || entry.HasMergedValue || (!entry.HasMergedValue && this.grid_textbox.Text != string.Empty))
			{
				string text = null;
				if (!entry.SetValue(value, out text) && text != null)
				{
					if (this.property_grid.ShowError(text, MessageBoxButtons.OKCancel) == DialogResult.Cancel)
					{
						this.UpdateItem(entry);
						this.UnfocusSelection();
					}
					return false;
				}
			}
			this.UpdateItem(entry);
			return true;
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x000AF474 File Offset: 0x000AD674
		protected override bool IsInputKey(Keys keyData)
		{
			switch (keyData)
			{
			case Keys.Escape:
			case Keys.PageUp:
			case Keys.PageDown:
			case Keys.End:
			case Keys.Home:
			case Keys.Left:
			case Keys.Up:
			case Keys.Right:
			case Keys.Down:
				break;
			default:
				if (keyData != Keys.Return)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x000AF4D4 File Offset: 0x000AD6D4
		private GridEntry MoveUpFromItem(GridEntry item, int up_count)
		{
			while (up_count > 0)
			{
				GridItemCollection gridItemCollection = ((item.Parent == null) ? this.RootGridItem.GridItems : item.Parent.GridItems);
				int num = gridItemCollection.IndexOf(item);
				if (num == 0)
				{
					if (item.Parent.GridItemType == GridItemType.Root)
					{
						return item;
					}
					item = (GridEntry)item.Parent;
					up_count--;
				}
				else
				{
					GridEntry gridEntry = (GridEntry)gridItemCollection[num - 1];
					if (gridEntry.Expandable && gridEntry.Expanded)
					{
						item = (GridEntry)gridEntry.GridItems[gridEntry.GridItems.Count - 1];
					}
					else
					{
						item = gridEntry;
					}
					up_count--;
				}
			}
			return item;
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x000AF5A0 File Offset: 0x000AD7A0
		private GridEntry MoveDownFromItem(GridEntry item, int down_count)
		{
			while (down_count > 0)
			{
				if (item.Expandable && item.Expanded)
				{
					item = (GridEntry)item.GridItems[0];
					down_count--;
				}
				else
				{
					GridItem gridItem = item;
					GridItemCollection gridItemCollection = gridItem.Parent.GridItems;
					int num;
					for (num = gridItemCollection.IndexOf(gridItem); num == gridItemCollection.Count - 1; num = gridItemCollection.IndexOf(gridItem))
					{
						gridItem = gridItem.Parent;
						if (gridItem == null || gridItem.Parent == null)
						{
							break;
						}
						gridItemCollection = gridItem.Parent.GridItems;
					}
					if (num == gridItemCollection.Count - 1)
					{
						return item;
					}
					item = (GridEntry)gridItemCollection[num + 1];
					down_count--;
				}
			}
			return item;
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x000AF66C File Offset: 0x000AD86C
		protected override void OnKeyDown(KeyEventArgs e)
		{
			GridEntry selectedGridItem = this.SelectedGridItem;
			if (selectedGridItem == null)
			{
				base.OnKeyDown(e);
				return;
			}
			Keys keys = e.KeyData & Keys.KeyCode;
			switch (keys)
			{
			case Keys.PageUp:
				this.SelectedGridItem = this.MoveUpFromItem(selectedGridItem, this.vbar.LargeChange);
				e.Handled = true;
				goto IL_026A;
			case Keys.PageDown:
				this.SelectedGridItem = this.MoveDownFromItem(selectedGridItem, this.vbar.LargeChange);
				e.Handled = true;
				goto IL_026A;
			case Keys.End:
			{
				GridEntry gridEntry = (GridEntry)this.RootGridItem.GridItems[this.RootGridItem.GridItems.Count - 1];
				while (gridEntry.Expandable && gridEntry.Expanded)
				{
					gridEntry = (GridEntry)gridEntry.GridItems[gridEntry.GridItems.Count - 1];
				}
				this.SelectedGridItem = gridEntry;
				e.Handled = true;
				goto IL_026A;
			}
			case Keys.Home:
				this.SelectedGridItem = (GridEntry)this.RootGridItem.GridItems[0];
				e.Handled = true;
				goto IL_026A;
			case Keys.Left:
				if (e.Control)
				{
					if (this.SplitterLocation > 32)
					{
						this.SplitterPercent -= 0.01;
					}
					e.Handled = true;
					goto IL_026A;
				}
				if (selectedGridItem.Expandable && selectedGridItem.Expanded)
				{
					selectedGridItem.Expanded = false;
					e.Handled = true;
					goto IL_026A;
				}
				break;
			case Keys.Up:
				break;
			case Keys.Right:
				if (e.Control)
				{
					if (this.SplitterLocation < base.Width)
					{
						this.SplitterPercent += 0.01;
					}
					e.Handled = true;
					goto IL_026A;
				}
				if (selectedGridItem.Expandable && !selectedGridItem.Expanded)
				{
					selectedGridItem.Expanded = true;
					e.Handled = true;
					goto IL_026A;
				}
				goto IL_016C;
			case Keys.Down:
				goto IL_016C;
			default:
				if (keys != Keys.Return)
				{
					goto IL_026A;
				}
				if (selectedGridItem.Expandable)
				{
					selectedGridItem.Expanded = !selectedGridItem.Expanded;
				}
				e.Handled = true;
				goto IL_026A;
			}
			this.SelectedGridItem = this.MoveUpFromItem(selectedGridItem, 1);
			e.Handled = true;
			goto IL_026A;
			IL_016C:
			this.SelectedGridItem = this.MoveDownFromItem(selectedGridItem, 1);
			e.Handled = true;
			IL_026A:
			base.OnKeyDown(e);
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06002D88 RID: 11656 RVA: 0x000AF8EC File Offset: 0x000ADAEC
		private int SplitterLocation
		{
			get
			{
				return (int)(this.splitter_percent * (double)base.Width);
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06002D8A RID: 11658 RVA: 0x000AF9A8 File Offset: 0x000ADBA8
		// (set) Token: 0x06002D89 RID: 11657 RVA: 0x000AF900 File Offset: 0x000ADB00
		private double SplitterPercent
		{
			get
			{
				return this.splitter_percent;
			}
			set
			{
				int splitterLocation = this.SplitterLocation;
				this.splitter_percent = Math.Max(Math.Min(value, 0.9), 0.1);
				if (splitterLocation != this.SplitterLocation)
				{
					int num = ((splitterLocation <= this.SplitterLocation) ? splitterLocation : this.SplitterLocation);
					base.Invalidate(new Rectangle(num, 0, base.Width - num - ((!this.vbar.Visible) ? 0 : this.vbar.Width), base.Height));
					this.UpdateItem(this.SelectedGridItem);
				}
			}
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x000AF9B0 File Offset: 0x000ADBB0
		private bool GridLabelHitTest(int x)
		{
			return 0 <= x && (double)x <= this.splitter_percent * (double)base.Width;
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x000AF9D4 File Offset: 0x000ADBD4
		private GridItem GetSelectedGridItem(GridItemCollection grid_items, int y, ref int current)
		{
			foreach (object obj in grid_items)
			{
				GridItem gridItem = (GridItem)obj;
				if (y > current && y < current + this.row_height)
				{
					return gridItem;
				}
				current += this.row_height;
				if (gridItem.Expanded)
				{
					GridItem selectedGridItem = this.GetSelectedGridItem(gridItem.GridItems, y, ref current);
					if (selectedGridItem != null)
					{
						return selectedGridItem;
					}
				}
			}
			return null;
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x000AFA90 File Offset: 0x000ADC90
		private int GetVisibleItemsCount(GridEntry entry)
		{
			if (entry == null)
			{
				return 0;
			}
			int num = 0;
			foreach (object obj in entry.GridItems)
			{
				GridEntry gridEntry = (GridEntry)obj;
				num++;
				if (gridEntry.Expandable && gridEntry.Expanded)
				{
					num += this.GetVisibleItemsCount(gridEntry);
				}
			}
			return num;
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x000AFB28 File Offset: 0x000ADD28
		private int GetVisibleRowsCount()
		{
			return base.Height / this.row_height;
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x000AFB38 File Offset: 0x000ADD38
		private void UpdateScrollBar()
		{
			if (this.RootGridItem == null)
			{
				return;
			}
			int visibleRowsCount = this.GetVisibleRowsCount();
			int visibleItemsCount = this.GetVisibleItemsCount(this.RootGridItem);
			if (visibleItemsCount > visibleRowsCount)
			{
				this.vbar.Visible = true;
				this.vbar.SmallChange = 1;
				this.vbar.LargeChange = visibleRowsCount;
				this.vbar.Maximum = Math.Max(0, visibleItemsCount - 1);
			}
			else
			{
				this.vbar.Value = 0;
				this.vbar.Visible = false;
			}
			this.UpdateGridTextBoxBounds(this.SelectedGridItem);
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x000AFBD0 File Offset: 0x000ADDD0
		private void DrawGridItems(GridItemCollection grid_items, PaintEventArgs pevent, int depth, ref int yLoc)
		{
			foreach (object obj in grid_items)
			{
				GridItem gridItem = (GridItem)obj;
				this.DrawGridItem((GridEntry)gridItem, pevent, depth, ref yLoc);
				if (gridItem.Expanded)
				{
					this.DrawGridItems(gridItem.GridItems, pevent, (gridItem.GridItemType != GridItemType.Category) ? (depth + 1) : depth, ref yLoc);
				}
			}
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x000AFC74 File Offset: 0x000ADE74
		private void DrawGridItemLabel(GridEntry grid_item, PaintEventArgs pevent, int depth, Rectangle rect)
		{
			Font font = this.Font;
			Brush brush;
			if (grid_item.GridItemType == GridItemType.Category)
			{
				font = this.bold_font;
				brush = SystemBrushes.ControlText;
				pevent.Graphics.DrawString(grid_item.Label, font, brush, (float)(rect.X + 1), (float)(rect.Y + 2));
				if (grid_item == this.SelectedGridItem)
				{
					SizeF sizeF = pevent.Graphics.MeasureString(grid_item.Label, font);
					ControlPaint.DrawFocusRectangle(pevent.Graphics, new Rectangle(rect.X + 1, rect.Y + 2, (int)sizeF.Width, (int)sizeF.Height));
				}
			}
			else if (grid_item == this.SelectedGridItem)
			{
				Rectangle rectangle = rect;
				if (depth > 1)
				{
					rectangle.X -= 16;
					rectangle.Width += 16;
				}
				pevent.Graphics.FillRectangle(SystemBrushes.Highlight, rectangle);
				brush = SystemBrushes.HighlightText;
			}
			else
			{
				brush = ((!grid_item.IsReadOnly) ? SystemBrushes.ControlText : this.inactive_text_brush);
			}
			pevent.Graphics.DrawString(grid_item.Label, font, brush, new Rectangle(rect.X + 1, rect.Y + 2, rect.Width - 2, rect.Height - 2), this.string_format);
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x000AFDD4 File Offset: 0x000ADFD4
		private void DrawGridItemValue(GridEntry grid_item, PaintEventArgs pevent, int depth, Rectangle rect)
		{
			if (grid_item.PropertyDescriptor == null)
			{
				return;
			}
			int num = this.SplitterLocation + 2;
			if (grid_item.PaintValueSupported)
			{
				pevent.Graphics.DrawRectangle(Pens.Black, this.SplitterLocation + 2, rect.Y + 2, 20, this.row_height - 4);
				grid_item.PaintValue(pevent.Graphics, new Rectangle(this.SplitterLocation + 2 + 1, rect.Y + 2 + 1, 19, this.row_height - 5));
				num += 27;
			}
			Font font = this.Font;
			if (grid_item.IsResetable || !grid_item.HasDefaultValue)
			{
				font = this.bold_font;
			}
			Brush brush = ((!grid_item.IsReadOnly) ? SystemBrushes.ControlText : this.inactive_text_brush);
			string text = string.Empty;
			if (!grid_item.IsMerged || (grid_item.IsMerged && grid_item.HasMergedValue))
			{
				if (grid_item.IsPassword)
				{
					text = new string('●', grid_item.ValueText.Length);
				}
				else
				{
					text = grid_item.ValueText;
				}
			}
			pevent.Graphics.DrawString(text, font, brush, new RectangleF((float)(num + 2), (float)(rect.Y + 2), (float)(base.ClientRectangle.Width - num), (float)(this.row_height - 4)), this.string_format);
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x000AFF38 File Offset: 0x000AE138
		private void DrawGridItem(GridEntry grid_item, PaintEventArgs pevent, int depth, ref int yLoc)
		{
			if (yLoc > -this.row_height && yLoc < base.ClientRectangle.Height)
			{
				pevent.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.property_grid.LineColor), 0, yLoc, 16, this.row_height);
				if (grid_item.GridItemType == GridItemType.Category)
				{
					pevent.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.property_grid.CategoryForeColor), depth * 16, yLoc, base.ClientRectangle.Width - depth * 16, this.row_height);
				}
				this.DrawGridItemLabel(grid_item, pevent, depth, new Rectangle(depth * 16, yLoc, this.SplitterLocation - depth * 16, this.row_height));
				this.DrawGridItemValue(grid_item, pevent, depth, new Rectangle(this.SplitterLocation + 2, yLoc, base.ClientRectangle.Width - this.SplitterLocation - 2 - ((!this.vbar.Visible) ? 0 : this.vbar.Width), this.row_height));
				if (grid_item.GridItemType != GridItemType.Category)
				{
					Pen pen = ThemeEngine.Current.ResPool.GetPen(this.property_grid.LineColor);
					pevent.Graphics.DrawLine(pen, this.SplitterLocation, yLoc, this.SplitterLocation, yLoc + this.row_height);
					pevent.Graphics.DrawLine(pen, 0, yLoc + this.row_height, base.ClientRectangle.Width, yLoc + this.row_height);
				}
				if (grid_item.Expandable)
				{
					int num = yLoc + this.row_height / 2 - 2 + 1;
					grid_item.PlusMinusBounds = this.DrawPlusMinus(pevent.Graphics, (depth - 1) * 16 + 2 + 1, num, grid_item.Expanded, grid_item.GridItemType == GridItemType.Category);
				}
			}
			grid_item.Top = yLoc;
			yLoc += this.row_height;
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x000B0148 File Offset: 0x000AE348
		private Rectangle DrawPlusMinus(Graphics g, int x, int y, bool expanded, bool category)
		{
			Rectangle rectangle;
			rectangle..ctor(x, y, 8, 8);
			if (!category)
			{
				g.FillRectangle(Brushes.White, rectangle);
			}
			Pen pen = ThemeEngine.Current.ResPool.GetPen(this.property_grid.ViewForeColor);
			g.DrawRectangle(pen, rectangle);
			g.DrawLine(pen, x + 2, y + 4, x + 6, y + 4);
			if (!expanded)
			{
				g.DrawLine(pen, x + 4, y + 2, x + 4, y + 6);
			}
			return rectangle;
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x000B01C4 File Offset: 0x000AE3C4
		private void RedrawEvent(object sender, EventArgs e)
		{
			this.Refresh();
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x000B01CC File Offset: 0x000AE3CC
		private void listBox_MouseUp(object sender, MouseEventArgs e)
		{
			this.AcceptListBoxSelection(sender);
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x000B01D8 File Offset: 0x000AE3D8
		private void listBox_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keys = e.KeyData & Keys.KeyCode;
			if (keys == Keys.Return)
			{
				this.AcceptListBoxSelection(sender);
				return;
			}
			if (keys != Keys.Escape)
			{
				return;
			}
			this.CloseDropDown();
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x000B0218 File Offset: 0x000AE418
		private void AcceptListBoxSelection(object sender)
		{
			GridEntry selectedGridItem = this.SelectedGridItem;
			if (selectedGridItem != null)
			{
				this.grid_textbox.Text = (string)((ListBox)sender).SelectedItem;
				this.CloseDropDown();
				if (this.TrySetEntry(selectedGridItem, this.grid_textbox.Text))
				{
					this.UnfocusSelection();
				}
			}
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x000B0270 File Offset: 0x000AE470
		private void DropDownButtonClicked(object sender, EventArgs e)
		{
			this.DropDownEdit();
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x000B0278 File Offset: 0x000AE478
		private void DropDownEdit()
		{
			GridEntry selectedGridItem = this.SelectedGridItem;
			if (selectedGridItem == null)
			{
				return;
			}
			if (selectedGridItem.HasCustomEditor)
			{
				selectedGridItem.EditValue(this);
			}
			else if (this.dropdown_form.Visible)
			{
				this.CloseDropDown();
			}
			else
			{
				ICollection acceptedValues = selectedGridItem.AcceptedValues;
				if (acceptedValues != null)
				{
					if (this.dropdown_list == null)
					{
						this.dropdown_list = new ListBox();
						this.dropdown_list.KeyDown += this.listBox_KeyDown;
						this.dropdown_list.MouseUp += this.listBox_MouseUp;
					}
					this.dropdown_list.Items.Clear();
					this.dropdown_list.BorderStyle = BorderStyle.FixedSingle;
					int num = 0;
					int num2 = 0;
					string valueText = selectedGridItem.ValueText;
					foreach (object obj in acceptedValues)
					{
						this.dropdown_list.Items.Add(obj);
						if (valueText != null && valueText.Equals(obj))
						{
							num = num2;
						}
						num2++;
					}
					this.dropdown_list.Height = this.row_height * Math.Min(this.dropdown_list.Items.Count, 15);
					this.dropdown_list.Width = base.ClientRectangle.Width - this.SplitterLocation - ((!this.vbar.Visible) ? 0 : this.vbar.Width);
					if (acceptedValues.Count > 0)
					{
						this.dropdown_list.SelectedIndex = num;
					}
					this.DropDownControl(this.dropdown_list);
				}
			}
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x000B0458 File Offset: 0x000AE658
		private void DialogButtonClicked(object sender, EventArgs e)
		{
			GridEntry selectedGridItem = this.SelectedGridItem;
			if (selectedGridItem != null && selectedGridItem.HasCustomEditor)
			{
				selectedGridItem.EditValue(this);
			}
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x000B0488 File Offset: 0x000AE688
		private void VScrollBar_HandleValueChanged(object sender, EventArgs e)
		{
			this.UpdateView();
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x000B0490 File Offset: 0x000AE690
		private void grid_textbox_ToggleValue(object sender, EventArgs args)
		{
			this.ToggleValue(this.SelectedGridItem);
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x000B04A0 File Offset: 0x000AE6A0
		private void grid_textbox_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keys = e.KeyData & Keys.KeyCode;
			if (keys == Keys.Down)
			{
				if (e.Alt)
				{
					this.DropDownEdit();
					e.Handled = true;
				}
			}
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x000B04E4 File Offset: 0x000AE6E4
		private void grid_textbox_Validate(object sender, CancelEventArgs args)
		{
			if (!this.TrySetEntry(this.SelectedGridItem, this.grid_textbox.Text))
			{
				args.Cancel = true;
			}
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x000B0514 File Offset: 0x000AE714
		private void ToggleValue(GridEntry entry)
		{
			if (entry != null && !entry.IsReadOnly && entry.GridItemType == GridItemType.Property)
			{
				entry.ToggleValue();
			}
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x000B053C File Offset: 0x000AE73C
		internal void UpdateItem(GridEntry entry)
		{
			if (entry == null || entry.GridItemType == GridItemType.Category || entry.GridItemType == GridItemType.Root)
			{
				this.grid_textbox.Visible = false;
				this.InvalidateItem(entry);
				return;
			}
			if (this.SelectedGridItem == entry)
			{
				base.SuspendLayout();
				this.grid_textbox.Visible = false;
				if (entry.IsResetable || !entry.HasDefaultValue)
				{
					this.grid_textbox.Font = this.bold_font;
				}
				else
				{
					this.grid_textbox.Font = this.Font;
				}
				if (entry.IsReadOnly)
				{
					this.grid_textbox.DropDownButtonVisible = false;
					this.grid_textbox.DialogButtonVisible = false;
					this.grid_textbox.ReadOnly = true;
					this.grid_textbox.ForeColor = SystemColors.GrayText;
				}
				else
				{
					this.grid_textbox.DropDownButtonVisible = entry.AcceptedValues != null || entry.EditorStyle == 3;
					this.grid_textbox.DialogButtonVisible = entry.EditorStyle == 2;
					this.grid_textbox.ForeColor = SystemColors.ControlText;
					this.grid_textbox.ReadOnly = !entry.IsEditable;
				}
				this.UpdateGridTextBoxBounds(entry);
				this.grid_textbox.PasswordChar = ((!entry.IsPassword) ? '\0' : '*');
				this.grid_textbox.Text = ((!entry.IsMerged || entry.HasMergedValue) ? entry.ValueText : string.Empty);
				this.grid_textbox.Visible = true;
				this.InvalidateItem(entry);
				base.ResumeLayout(false);
			}
			else
			{
				this.grid_textbox.Visible = false;
			}
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x000B06F8 File Offset: 0x000AE8F8
		private void UpdateGridTextBoxBounds(GridEntry entry)
		{
			if (entry == null || this.RootGridItem == null)
			{
				return;
			}
			int num = -this.vbar.Value * this.row_height;
			this.CalculateItemY(entry, this.RootGridItem.GridItems, ref num);
			int num2 = this.SplitterLocation + 2 + ((!entry.PaintValueSupported) ? 0 : 27);
			this.grid_textbox.SetBounds(num2 + 2, num + 2, base.ClientRectangle.Width - 2 - num2 - ((!this.vbar.Visible) ? 0 : this.vbar.Width), this.row_height - 2);
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x000B07AC File Offset: 0x000AE9AC
		private bool CalculateItemY(GridEntry entry, GridItemCollection items, ref int y)
		{
			foreach (object obj in items)
			{
				GridItem gridItem = (GridItem)obj;
				if (gridItem == entry)
				{
					return true;
				}
				y += this.row_height;
				if (gridItem.Expandable && gridItem.Expanded && this.CalculateItemY(entry, gridItem.GridItems, ref y))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x000B085C File Offset: 0x000AEA5C
		private void ScrollToItem(GridEntry item)
		{
			if (item == null || this.RootGridItem == null)
			{
				return;
			}
			int num = -this.vbar.Value * this.row_height;
			int num2 = this.vbar.Value;
			this.CalculateItemY(item, this.RootGridItem.GridItems, ref num);
			if (num < 0)
			{
				num2 += num / this.row_height;
			}
			else if (num + this.row_height > base.Height)
			{
				num2 += (num + this.row_height - base.Height) / this.row_height + 1;
			}
			if (num2 >= this.vbar.Minimum && num2 <= this.vbar.Maximum)
			{
				this.vbar.Value = num2;
			}
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x000B0924 File Offset: 0x000AEB24
		internal void SelectItem(GridEntry oldItem, GridEntry newItem)
		{
			if (oldItem != null)
			{
				this.InvalidateItemLabel(oldItem);
			}
			if (newItem != null)
			{
				this.UpdateItem(newItem);
				this.ScrollToItem(newItem);
			}
			else
			{
				this.grid_textbox.Visible = false;
				this.vbar.Visible = false;
			}
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x000B0970 File Offset: 0x000AEB70
		internal void UpdateView()
		{
			this.UpdateScrollBar();
			base.Invalidate();
			base.Update();
			this.UpdateItem(this.SelectedGridItem);
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x000B099C File Offset: 0x000AEB9C
		internal void ExpandItem(GridEntry item)
		{
			this.UpdateItem(this.SelectedGridItem);
			base.Invalidate(new Rectangle(0, item.Top, base.Width, base.Height - item.Top));
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x000B09DC File Offset: 0x000AEBDC
		internal void CollapseItem(GridEntry item)
		{
			this.UpdateItem(this.SelectedGridItem);
			base.Invalidate(new Rectangle(0, item.Top, base.Width, base.Height - item.Top));
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x000B0A1C File Offset: 0x000AEC1C
		private void ShowDropDownControl(Control control, bool resizeable)
		{
			this.dropdown_form.Size = control.Size;
			control.Dock = DockStyle.Fill;
			if (resizeable)
			{
				this.dropdown_form.Padding = this.dropdown_form_padding;
				this.dropdown_form.Width += this.dropdown_form_padding.Right;
				this.dropdown_form.Height += this.dropdown_form_padding.Bottom;
				this.dropdown_form.FormBorderStyle = FormBorderStyle.Sizable;
				this.dropdown_form.SizeGripStyle = SizeGripStyle.Show;
			}
			else
			{
				this.dropdown_form.FormBorderStyle = FormBorderStyle.None;
				this.dropdown_form.SizeGripStyle = SizeGripStyle.Hide;
				this.dropdown_form.Padding = Padding.Empty;
			}
			this.dropdown_form.Controls.Add(control);
			this.dropdown_form.Width = Math.Max(base.ClientRectangle.Width - this.SplitterLocation - ((!this.vbar.Visible) ? 0 : this.vbar.Width), control.Width);
			this.dropdown_form.Location = base.PointToScreen(new Point(this.grid_textbox.Right - this.dropdown_form.Width, this.grid_textbox.Location.Y + this.row_height));
			this.RepositionInScreenWorkingArea(this.dropdown_form);
			Point location = this.dropdown_form.Location;
			Form form = base.FindForm();
			form.AddOwnedForm(this.dropdown_form);
			this.dropdown_form.Show();
			if (this.dropdown_form.Location != location)
			{
				this.dropdown_form.Location = location;
			}
			MSG msg = default(MSG);
			object obj = XplatUI.StartLoop(Thread.CurrentThread);
			control.Focus();
			while (this.dropdown_form.Visible && XplatUI.GetMessage(obj, ref msg, IntPtr.Zero, 0, 0))
			{
				Msg message = msg.message;
				switch (message)
				{
				case Msg.WM_NCLBUTTONDOWN:
				case Msg.WM_NCRBUTTONDOWN:
					goto IL_0240;
				default:
					switch (message)
					{
					case Msg.WM_LBUTTONDOWN:
					case Msg.WM_RBUTTONDOWN:
						goto IL_0240;
					default:
						if (message != Msg.WM_ACTIVATE && message != Msg.WM_NCPAINT)
						{
							if (message == Msg.WM_NCMBUTTONDOWN || message == Msg.WM_MBUTTONDOWN)
							{
								goto IL_0240;
							}
						}
						else if (form.window.Handle == msg.hwnd)
						{
							this.CloseDropDown();
						}
						break;
					}
					break;
				}
				IL_028A:
				XplatUI.TranslateMessage(ref msg);
				XplatUI.DispatchMessage(ref msg);
				continue;
				IL_0240:
				if (!this.HwndInControl(this.dropdown_form, msg.hwnd))
				{
					this.CloseDropDown();
				}
				goto IL_028A;
			}
			XplatUI.EndLoop(Thread.CurrentThread);
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x000B0CF4 File Offset: 0x000AEEF4
		private void RepositionInScreenWorkingArea(Form form)
		{
			Rectangle workingArea = Screen.FromControl(form).WorkingArea;
			if (!workingArea.Contains(form.Bounds))
			{
				int num = form.Location.X;
				int num2 = form.Location.Y;
				if (form.Location.X < workingArea.X)
				{
					num = workingArea.X;
				}
				if (form.Location.Y + form.Size.Height > workingArea.Height)
				{
					num2 = base.PointToScreen(new Point(this.grid_textbox.Right - form.Width, this.grid_textbox.Location.Y)).Y - form.Size.Height;
				}
				form.Location = new Point(num, num2);
			}
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x000B0DE4 File Offset: 0x000AEFE4
		private bool HwndInControl(Control c, IntPtr hwnd)
		{
			if (hwnd == c.window.Handle)
			{
				return true;
			}
			foreach (Control control in c.Controls.GetAllControls())
			{
				if (this.HwndInControl(control, hwnd))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x000B0E40 File Offset: 0x000AF040
		public void CloseDropDown()
		{
			this.dropdown_form.Hide();
			this.dropdown_form.Controls.Clear();
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x000B0E60 File Offset: 0x000AF060
		public void DropDownControl(Control control)
		{
			bool flag = this.SelectedGridItem != null && this.SelectedGridItem.EditorResizeable;
			this.ShowDropDownControl(control, flag);
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x000B0E94 File Offset: 0x000AF094
		public DialogResult ShowDialog(Form dialog)
		{
			return dialog.ShowDialog(this);
		}

		// Token: 0x040015E9 RID: 5609
		private const char PASSWORD_PAINT_CHAR = '●';

		// Token: 0x040015EA RID: 5610
		private const char PASSWORD_TEXT_CHAR = '*';

		// Token: 0x040015EB RID: 5611
		private const int V_INDENT = 16;

		// Token: 0x040015EC RID: 5612
		private const int ENTRY_SPACING = 2;

		// Token: 0x040015ED RID: 5613
		private const int RESIZE_WIDTH = 3;

		// Token: 0x040015EE RID: 5614
		private const int BUTTON_WIDTH = 25;

		// Token: 0x040015EF RID: 5615
		private const int VALUE_PAINT_WIDTH = 19;

		// Token: 0x040015F0 RID: 5616
		private const int VALUE_PAINT_INDENT = 27;

		// Token: 0x040015F1 RID: 5617
		private double splitter_percent = 0.5;

		// Token: 0x040015F2 RID: 5618
		private int row_height;

		// Token: 0x040015F3 RID: 5619
		private int font_height_padding = 3;

		// Token: 0x040015F4 RID: 5620
		private PropertyGridTextBox grid_textbox;

		// Token: 0x040015F5 RID: 5621
		private PropertyGrid property_grid;

		// Token: 0x040015F6 RID: 5622
		private bool resizing_grid;

		// Token: 0x040015F7 RID: 5623
		private PropertyGridView.PropertyGridDropDown dropdown_form;

		// Token: 0x040015F8 RID: 5624
		private Form dialog_form;

		// Token: 0x040015F9 RID: 5625
		private ImplicitVScrollBar vbar;

		// Token: 0x040015FA RID: 5626
		private StringFormat string_format;

		// Token: 0x040015FB RID: 5627
		private Font bold_font;

		// Token: 0x040015FC RID: 5628
		private Brush inactive_text_brush;

		// Token: 0x040015FD RID: 5629
		private ListBox dropdown_list;

		// Token: 0x040015FE RID: 5630
		private Point last_click;

		// Token: 0x040015FF RID: 5631
		private Padding dropdown_form_padding;

		// Token: 0x020002A8 RID: 680
		internal class PropertyGridDropDown : Form
		{
			// Token: 0x17000B92 RID: 2962
			// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x000B0EA8 File Offset: 0x000AF0A8
			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					createParams.Style = -2046820352;
					createParams.ExStyle |= 8;
					return createParams;
				}
			}
		}
	}
}
