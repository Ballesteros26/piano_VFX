using System;
using System.Collections;
using System.Drawing;

namespace System.Windows.Forms.Layout
{
	// Token: 0x0200049F RID: 1183
	internal class TableLayout : LayoutEngine
	{
		// Token: 0x06004B88 RID: 19336 RVA: 0x0012AC68 File Offset: 0x00128E68
		public override void InitLayout(object child, BoundsSpecified specified)
		{
			base.InitLayout(child, specified);
		}

		// Token: 0x06004B89 RID: 19337 RVA: 0x0012AC74 File Offset: 0x00128E74
		public override bool Layout(object container, LayoutEventArgs args)
		{
			TableLayoutPanel tableLayoutPanel = container as TableLayoutPanel;
			TableLayoutSettings layoutSettings = tableLayoutPanel.LayoutSettings;
			tableLayoutPanel.actual_positions = this.CalculateControlPositions(tableLayoutPanel, Math.Max(layoutSettings.ColumnCount, 1), Math.Max(layoutSettings.RowCount, 1));
			this.CalculateColumnRowSizes(tableLayoutPanel, tableLayoutPanel.actual_positions.GetLength(0), tableLayoutPanel.actual_positions.GetLength(1));
			this.LayoutControls(tableLayoutPanel);
			return false;
		}

		// Token: 0x06004B8A RID: 19338 RVA: 0x0012ACDC File Offset: 0x00128EDC
		internal Control[,] CalculateControlPositions(TableLayoutPanel panel, int columns, int rows)
		{
			Control[,] array = new Control[columns, rows];
			TableLayoutSettings layoutSettings = panel.LayoutSettings;
			foreach (object obj in panel.Controls)
			{
				Control control = (Control)obj;
				int num = layoutSettings.GetColumn(control);
				int num2 = layoutSettings.GetRow(control);
				if (num >= 0 && num2 >= 0)
				{
					if (num >= columns)
					{
						return this.CalculateControlPositions(panel, num + 1, rows);
					}
					if (num2 >= rows)
					{
						return this.CalculateControlPositions(panel, columns, num2 + 1);
					}
					if (array[num, num2] == null)
					{
						int num3 = Math.Min(layoutSettings.GetColumnSpan(control), columns);
						int num4 = Math.Min(layoutSettings.GetRowSpan(control), rows);
						if (num + num3 > columns)
						{
							if (num2 + 1 < rows)
							{
								array[num, num2] = TableLayout.dummy_control;
								num2++;
								num = 0;
							}
							else
							{
								if (layoutSettings.GrowStyle == TableLayoutPanelGrowStyle.AddColumns)
								{
									return this.CalculateControlPositions(panel, columns + 1, rows);
								}
								throw new ArgumentException();
							}
						}
						if (num2 + num4 > rows)
						{
							if (layoutSettings.GrowStyle == TableLayoutPanelGrowStyle.AddRows)
							{
								return this.CalculateControlPositions(panel, columns, rows + 1);
							}
							throw new ArgumentException();
						}
						else
						{
							array[num, num2] = control;
							for (int i = 1; i < num3; i++)
							{
								array[num + i, num2] = TableLayout.dummy_control;
							}
							for (int j = 1; j < num4; j++)
							{
								array[num, num2 + j] = TableLayout.dummy_control;
							}
						}
					}
				}
			}
			int num5 = 0;
			using (IEnumerator enumerator2 = panel.Controls.GetEnumerator())
			{
				IL_0407:
				while (enumerator2.MoveNext())
				{
					object obj2 = enumerator2.Current;
					Control control2 = (Control)obj2;
					int column = layoutSettings.GetColumn(control2);
					int row = layoutSettings.GetRow(control2);
					if (column < 0 || column >= columns || row < 0 || row >= rows || (array[column, row] != control2 && array[column, row] != TableLayout.dummy_control))
					{
						for (int k = num5; k < rows; k++)
						{
							num5 = k;
							int num6 = 0;
							int l = num6;
							while (l < columns)
							{
								if (array[l, k] == null)
								{
									int num7 = Math.Min(layoutSettings.GetColumnSpan(control2), columns);
									int num8 = Math.Min(layoutSettings.GetRowSpan(control2), rows);
									if (l + num7 > columns)
									{
										if (k + 1 < rows)
										{
											break;
										}
										if (layoutSettings.GrowStyle == TableLayoutPanelGrowStyle.AddColumns)
										{
											return this.CalculateControlPositions(panel, columns + 1, rows);
										}
										throw new ArgumentException();
									}
									else
									{
										if (k + num8 <= rows)
										{
											array[l, k] = control2;
											for (int m = 1; m < num7; m++)
											{
												array[l + m, k] = TableLayout.dummy_control;
											}
											for (int n = 1; n < num8; n++)
											{
												array[l, k + n] = TableLayout.dummy_control;
											}
											goto IL_0407;
										}
										if (l + 1 < columns)
										{
											break;
										}
										if (layoutSettings.GrowStyle == TableLayoutPanelGrowStyle.AddRows)
										{
											return this.CalculateControlPositions(panel, columns, rows + 1);
										}
										throw new ArgumentException();
									}
								}
								else
								{
									if (layoutSettings.GrowStyle == TableLayoutPanelGrowStyle.AddColumns && layoutSettings.RowCount == 0)
									{
										break;
									}
									l++;
								}
							}
						}
						TableLayoutPanelGrowStyle tableLayoutPanelGrowStyle = layoutSettings.GrowStyle;
						if (layoutSettings.GrowStyle == TableLayoutPanelGrowStyle.AddColumns && layoutSettings.RowCount == 0)
						{
							tableLayoutPanelGrowStyle = TableLayoutPanelGrowStyle.AddRows;
						}
						switch (tableLayoutPanelGrowStyle)
						{
						case TableLayoutPanelGrowStyle.FixedSize:
							throw new ArgumentException();
						case TableLayoutPanelGrowStyle.AddColumns:
							return this.CalculateControlPositions(panel, columns + 1, rows);
						}
						return this.CalculateControlPositions(panel, columns, rows + 1);
					}
				}
			}
			return array;
		}

		// Token: 0x06004B8B RID: 19339 RVA: 0x0012B150 File Offset: 0x00129350
		private void CalculateColumnRowSizes(TableLayoutPanel panel, int columns, int rows)
		{
			TableLayoutSettings layoutSettings = panel.LayoutSettings;
			panel.column_widths = new int[panel.actual_positions.GetLength(0)];
			panel.row_heights = new int[panel.actual_positions.GetLength(1)];
			int cellBorderWidth = TableLayoutPanel.GetCellBorderWidth(panel.CellBorderStyle);
			Rectangle displayRectangle = panel.DisplayRectangle;
			TableLayoutColumnStyleCollection tableLayoutColumnStyleCollection = new TableLayoutColumnStyleCollection(panel);
			foreach (object obj in layoutSettings.ColumnStyles)
			{
				ColumnStyle columnStyle = (ColumnStyle)obj;
				tableLayoutColumnStyleCollection.Add(new ColumnStyle(columnStyle.SizeType, columnStyle.Width));
			}
			TableLayoutRowStyleCollection tableLayoutRowStyleCollection = new TableLayoutRowStyleCollection(panel);
			foreach (object obj2 in layoutSettings.RowStyles)
			{
				RowStyle rowStyle = (RowStyle)obj2;
				tableLayoutRowStyleCollection.Add(new RowStyle(rowStyle.SizeType, rowStyle.Height));
			}
			if (columns > tableLayoutColumnStyleCollection.Count)
			{
				for (int i = tableLayoutColumnStyleCollection.Count; i < columns; i++)
				{
					tableLayoutColumnStyleCollection.Add(new ColumnStyle());
				}
			}
			if (rows > tableLayoutRowStyleCollection.Count)
			{
				for (int j = tableLayoutRowStyleCollection.Count; j < rows; j++)
				{
					tableLayoutRowStyleCollection.Add(new RowStyle());
				}
			}
			while (tableLayoutRowStyleCollection.Count > rows)
			{
				tableLayoutRowStyleCollection.RemoveAt(tableLayoutRowStyleCollection.Count - 1);
			}
			while (tableLayoutColumnStyleCollection.Count > columns)
			{
				tableLayoutColumnStyleCollection.RemoveAt(tableLayoutColumnStyleCollection.Count - 1);
			}
			int num = displayRectangle.Width - cellBorderWidth * (columns + 1);
			int num2 = 0;
			foreach (object obj3 in tableLayoutColumnStyleCollection)
			{
				ColumnStyle columnStyle2 = (ColumnStyle)obj3;
				if (columnStyle2.SizeType == SizeType.Absolute)
				{
					panel.column_widths[num2] = (int)columnStyle2.Width;
					num -= (int)columnStyle2.Width;
				}
				num2++;
			}
			num2 = 0;
			foreach (object obj4 in tableLayoutColumnStyleCollection)
			{
				ColumnStyle columnStyle3 = (ColumnStyle)obj4;
				if (columnStyle3.SizeType == SizeType.AutoSize)
				{
					int num3 = 0;
					for (int k = 0; k < rows; k++)
					{
						Control control = panel.actual_positions[num2, k];
						if (control != null && control != TableLayout.dummy_control && control.VisibleInternal)
						{
							if (layoutSettings.GetColumnSpan(control) <= 1)
							{
								if (control.AutoSize)
								{
									num3 = Math.Max(num3, control.PreferredSize.Width + control.Margin.Horizontal);
								}
								else
								{
									num3 = Math.Max(num3, control.ExplicitBounds.Width + control.Margin.Horizontal);
								}
								if (control.Width + control.Margin.Left + control.Margin.Right > num3)
								{
									num3 = control.Width + control.Margin.Left + control.Margin.Right;
								}
							}
						}
					}
					panel.column_widths[num2] = num3;
					num -= num3;
				}
				num2++;
			}
			num2 = 0;
			float num4 = 0f;
			if (num > 0)
			{
				int num5 = num;
				foreach (object obj5 in tableLayoutColumnStyleCollection)
				{
					ColumnStyle columnStyle4 = (ColumnStyle)obj5;
					if (columnStyle4.SizeType == SizeType.Percent)
					{
						num4 += columnStyle4.Width;
					}
				}
				foreach (object obj6 in tableLayoutColumnStyleCollection)
				{
					ColumnStyle columnStyle5 = (ColumnStyle)obj6;
					if (columnStyle5.SizeType == SizeType.Percent)
					{
						panel.column_widths[num2] = (int)(columnStyle5.Width / num4 * (float)num5);
						num -= panel.column_widths[num2];
					}
					num2++;
				}
			}
			if (num > 0)
			{
				panel.column_widths[tableLayoutColumnStyleCollection.Count - 1] += num;
			}
			int num6 = displayRectangle.Height - cellBorderWidth * (rows + 1);
			num2 = 0;
			foreach (object obj7 in tableLayoutRowStyleCollection)
			{
				RowStyle rowStyle2 = (RowStyle)obj7;
				if (rowStyle2.SizeType == SizeType.Absolute)
				{
					panel.row_heights[num2] = (int)rowStyle2.Height;
					num6 -= (int)rowStyle2.Height;
				}
				num2++;
			}
			num2 = 0;
			foreach (object obj8 in tableLayoutRowStyleCollection)
			{
				RowStyle rowStyle3 = (RowStyle)obj8;
				if (rowStyle3.SizeType == SizeType.AutoSize)
				{
					int num7 = 0;
					for (int l = 0; l < columns; l++)
					{
						Control control2 = panel.actual_positions[l, num2];
						if (control2 != null && control2 != TableLayout.dummy_control && control2.VisibleInternal)
						{
							if (layoutSettings.GetRowSpan(control2) <= 1)
							{
								if (control2.AutoSize)
								{
									num7 = Math.Max(num7, control2.PreferredSize.Height + control2.Margin.Vertical);
								}
								else
								{
									num7 = Math.Max(num7, control2.ExplicitBounds.Height + control2.Margin.Vertical);
								}
								if (control2.Height + control2.Margin.Top + control2.Margin.Bottom > num7)
								{
									num7 = control2.Height + control2.Margin.Top + control2.Margin.Bottom;
								}
							}
						}
					}
					panel.row_heights[num2] = num7;
					num6 -= num7;
				}
				num2++;
			}
			num2 = 0;
			num4 = 0f;
			if (num6 > 0)
			{
				int num8 = num6;
				foreach (object obj9 in tableLayoutRowStyleCollection)
				{
					RowStyle rowStyle4 = (RowStyle)obj9;
					if (rowStyle4.SizeType == SizeType.Percent)
					{
						num4 += rowStyle4.Height;
					}
				}
				foreach (object obj10 in tableLayoutRowStyleCollection)
				{
					RowStyle rowStyle5 = (RowStyle)obj10;
					if (rowStyle5.SizeType == SizeType.Percent)
					{
						panel.row_heights[num2] = (int)(rowStyle5.Height / num4 * (float)num8);
						num6 -= panel.row_heights[num2];
					}
					num2++;
				}
			}
			if (num6 > 0)
			{
				panel.row_heights[tableLayoutRowStyleCollection.Count - 1] += num6;
			}
		}

		// Token: 0x06004B8C RID: 19340 RVA: 0x0012BA48 File Offset: 0x00129C48
		private void LayoutControls(TableLayoutPanel panel)
		{
			TableLayoutSettings layoutSettings = panel.LayoutSettings;
			int cellBorderWidth = TableLayoutPanel.GetCellBorderWidth(panel.CellBorderStyle);
			int length = panel.actual_positions.GetLength(0);
			int length2 = panel.actual_positions.GetLength(1);
			Point point;
			point..ctor(panel.DisplayRectangle.Left + cellBorderWidth, panel.DisplayRectangle.Top + cellBorderWidth);
			for (int i = 0; i < length2; i++)
			{
				for (int j = 0; j < length; j++)
				{
					Control control = panel.actual_positions[j, i];
					if (control != null && control != TableLayout.dummy_control)
					{
						Size size;
						if (control.AutoSize)
						{
							size = control.PreferredSize;
						}
						else
						{
							size = control.ExplicitBounds.Size;
						}
						int num = panel.column_widths[j];
						for (int k = 1; k < Math.Min(layoutSettings.GetColumnSpan(control), panel.column_widths.Length); k++)
						{
							num += panel.column_widths[j + k];
						}
						int num2;
						if (control.Dock == DockStyle.Fill || control.Dock == DockStyle.Top || control.Dock == DockStyle.Bottom || ((control.Anchor & AnchorStyles.Left) == AnchorStyles.Left && (control.Anchor & AnchorStyles.Right) == AnchorStyles.Right))
						{
							num2 = num - control.Margin.Left - control.Margin.Right;
						}
						else
						{
							num2 = Math.Min(size.Width, num - control.Margin.Left - control.Margin.Right);
						}
						int num3 = panel.row_heights[i];
						for (int l = 1; l < Math.Min(layoutSettings.GetRowSpan(control), panel.row_heights.Length); l++)
						{
							num3 += panel.row_heights[i + l];
						}
						int num4;
						if (control.Dock == DockStyle.Fill || control.Dock == DockStyle.Left || control.Dock == DockStyle.Right || ((control.Anchor & AnchorStyles.Top) == AnchorStyles.Top && (control.Anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom))
						{
							num4 = num3 - control.Margin.Top - control.Margin.Bottom;
						}
						else
						{
							num4 = Math.Min(size.Height, num3 - control.Margin.Top - control.Margin.Bottom);
						}
						int num5;
						if (control.Dock == DockStyle.Left || control.Dock == DockStyle.Fill || (control.Anchor & AnchorStyles.Left) == AnchorStyles.Left)
						{
							num5 = point.X + control.Margin.Left;
						}
						else if (control.Dock == DockStyle.Right || (control.Anchor & AnchorStyles.Right) == AnchorStyles.Right)
						{
							num5 = point.X + num - num2 - control.Margin.Right;
						}
						else
						{
							num5 = point.X + (num - control.Margin.Left - control.Margin.Right) / 2 + control.Margin.Left - num2 / 2;
						}
						int num6;
						if (control.Dock == DockStyle.Top || control.Dock == DockStyle.Fill || (control.Anchor & AnchorStyles.Top) == AnchorStyles.Top)
						{
							num6 = point.Y + control.Margin.Top;
						}
						else if (control.Dock == DockStyle.Bottom || (control.Anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom)
						{
							num6 = point.Y + num3 - num4 - control.Margin.Bottom;
						}
						else
						{
							num6 = point.Y + (num3 - control.Margin.Top - control.Margin.Bottom) / 2 + control.Margin.Top - num4 / 2;
						}
						control.SetBoundsInternal(num5, num6, num2, num4, BoundsSpecified.None);
					}
					point.Offset(panel.column_widths[j] + cellBorderWidth, 0);
				}
				point.Offset(-1 * point.X + cellBorderWidth + panel.DisplayRectangle.Left, panel.row_heights[i] + cellBorderWidth);
			}
		}

		// Token: 0x0400285A RID: 10330
		private static Control dummy_control = new Control("Dummy");
	}
}
