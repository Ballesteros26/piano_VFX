using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms
{
	/// <summary>Handles the painting functionality for <see cref="T:System.Windows.Forms.ToolStrip" /> objects, applying a custom palette and a streamlined style.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000373 RID: 883
	public class ToolStripProfessionalRenderer : ToolStripRenderer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripProfessionalRenderer" /> class. </summary>
		// Token: 0x06003F4B RID: 16203 RVA: 0x000FBDC8 File Offset: 0x000F9FC8
		public ToolStripProfessionalRenderer()
			: this(new ProfessionalColorTable())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripProfessionalRenderer" /> class. </summary>
		/// <param name="professionalColorTable">A <see cref="T:System.Windows.Forms.ProfessionalColorTable" /> to be used for painting.</param>
		// Token: 0x06003F4C RID: 16204 RVA: 0x000FBDD8 File Offset: 0x000F9FD8
		public ToolStripProfessionalRenderer(ProfessionalColorTable professionalColorTable)
		{
			this.color_table = professionalColorTable;
			this.rounded_edges = true;
		}

		/// <summary>Gets the color palette used for painting.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ProfessionalColorTable" /> used for painting.</returns>
		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x06003F4D RID: 16205 RVA: 0x000FBDF0 File Offset: 0x000F9FF0
		public ProfessionalColorTable ColorTable
		{
			get
			{
				return this.color_table;
			}
		}

		/// <summary>Gets or sets a value indicating whether edges of controls have a rounded rather than a square or sharp appearance.</summary>
		/// <returns>true to round off control edges; otherwise, false.</returns>
		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x06003F4E RID: 16206 RVA: 0x000FBDF8 File Offset: 0x000F9FF8
		// (set) Token: 0x06003F4F RID: 16207 RVA: 0x000FBE00 File Offset: 0x000FA000
		public bool RoundedEdges
		{
			get
			{
				return this.rounded_edges;
			}
			set
			{
				this.rounded_edges = value;
			}
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripArrowRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003F50 RID: 16208 RVA: 0x000FBE0C File Offset: 0x000FA00C
		protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
		{
			base.OnRenderArrow(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06003F51 RID: 16209 RVA: 0x000FBE18 File Offset: 0x000FA018
		protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (!e.Item.Enabled)
			{
				return;
			}
			Rectangle rectangle;
			rectangle..ctor(0, 0, e.Item.Width, e.Item.Height);
			if (e.Item is ToolStripButton && (e.Item as ToolStripButton).Checked && !e.Item.Selected)
			{
				if (this.ColorTable.UseSystemColors)
				{
					e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.ButtonCheckedHighlight), rectangle);
				}
				else
				{
					using (Brush brush = new LinearGradientBrush(rectangle, this.ColorTable.ButtonCheckedGradientBegin, this.ColorTable.ButtonCheckedGradientEnd, 1))
					{
						e.Graphics.FillRectangle(brush, rectangle);
					}
				}
			}
			else if (e.Item is ToolStripDropDownItem && e.Item.Pressed)
			{
				using (Brush brush2 = new LinearGradientBrush(rectangle, this.ColorTable.ToolStripGradientBegin, this.ColorTable.ToolStripGradientEnd, 1))
				{
					e.Graphics.FillRectangle(brush2, rectangle);
				}
			}
			else if (e.Item.Pressed || (e.Item is ToolStripButton && (e.Item as ToolStripButton).Checked))
			{
				using (Brush brush3 = new LinearGradientBrush(rectangle, this.ColorTable.ButtonPressedGradientBegin, this.ColorTable.ButtonPressedGradientEnd, 1))
				{
					e.Graphics.FillRectangle(brush3, rectangle);
				}
			}
			else if (e.Item.Selected)
			{
				using (Brush brush4 = new LinearGradientBrush(rectangle, this.ColorTable.ButtonSelectedGradientBegin, this.ColorTable.ButtonSelectedGradientEnd, 1))
				{
					e.Graphics.FillRectangle(brush4, rectangle);
				}
			}
			else if (e.Item.BackColor != Control.DefaultBackColor && e.Item.BackColor != Color.Empty)
			{
				using (Brush brush5 = new SolidBrush(e.Item.BackColor))
				{
					e.Graphics.FillRectangle(brush5, rectangle);
				}
			}
			rectangle.Width--;
			rectangle.Height--;
			if (e.Item.Selected && !e.Item.Pressed)
			{
				using (Pen pen = new Pen(this.ColorTable.ButtonSelectedBorder))
				{
					e.Graphics.DrawRectangle(pen, rectangle);
				}
			}
			else if (e.Item.Pressed)
			{
				using (Pen pen2 = new Pen(this.ColorTable.ButtonPressedBorder))
				{
					e.Graphics.DrawRectangle(pen2, rectangle);
				}
			}
			else if (e.Item is ToolStripButton && (e.Item as ToolStripButton).Checked)
			{
				using (Pen pen3 = new Pen(this.ColorTable.ButtonPressedBorder))
				{
					e.Graphics.DrawRectangle(pen3, rectangle);
				}
			}
			base.OnRenderButtonBackground(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06003F52 RID: 16210 RVA: 0x000FC284 File Offset: 0x000FA484
		protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
		{
			Rectangle rectangle;
			rectangle..ctor(0, 0, e.Item.Width, e.Item.Height);
			if (e.Item.Selected && !e.Item.Pressed)
			{
				using (Brush brush = new LinearGradientBrush(rectangle, this.ColorTable.ButtonSelectedGradientBegin, this.ColorTable.ButtonSelectedGradientEnd, 1))
				{
					e.Graphics.FillRectangle(brush, rectangle);
				}
			}
			else if (e.Item.Pressed)
			{
				using (Brush brush2 = new LinearGradientBrush(rectangle, this.ColorTable.ImageMarginGradientMiddle, this.ColorTable.ImageMarginGradientEnd, 1))
				{
					e.Graphics.FillRectangle(brush2, rectangle);
				}
			}
			rectangle.Width--;
			rectangle.Height--;
			if (e.Item.Selected && !e.Item.Pressed)
			{
				using (Pen pen = new Pen(this.ColorTable.ButtonSelectedBorder))
				{
					e.Graphics.DrawRectangle(pen, rectangle);
				}
			}
			else if (e.Item.Pressed)
			{
				using (Pen pen2 = new Pen(this.ColorTable.MenuBorder))
				{
					e.Graphics.DrawRectangle(pen2, rectangle);
				}
			}
			base.OnRenderDropDownButtonBackground(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripGripRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06003F53 RID: 16211 RVA: 0x000FC484 File Offset: 0x000FA684
		protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
		{
			if (e.GripStyle == ToolStripGripStyle.Hidden)
			{
				return;
			}
			if (e.GripDisplayStyle == ToolStripGripDisplayStyle.Vertical)
			{
				Rectangle rectangle;
				rectangle..ctor(e.GripBounds.Left, e.GripBounds.Top + 5, 2, 2);
				for (int i = 0; i < e.GripBounds.Height - 12; i += 4)
				{
					e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.GripLight), rectangle);
					rectangle.Offset(0, 4);
				}
				Rectangle rectangle2;
				rectangle2..ctor(e.GripBounds.Left - 1, e.GripBounds.Top + 4, 2, 2);
				for (int j = 0; j < e.GripBounds.Height - 12; j += 4)
				{
					e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.GripDark), rectangle2);
					rectangle2.Offset(0, 4);
				}
			}
			else
			{
				Rectangle rectangle3;
				rectangle3..ctor(e.GripBounds.Left + 5, e.GripBounds.Top, 2, 2);
				for (int k = 0; k < e.GripBounds.Width - 11; k += 4)
				{
					e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.GripLight), rectangle3);
					rectangle3.Offset(4, 0);
				}
				Rectangle rectangle4;
				rectangle4..ctor(e.GripBounds.Left + 4, e.GripBounds.Top - 1, 2, 2);
				for (int l = 0; l < e.GripBounds.Width - 11; l += 4)
				{
					e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.GripDark), rectangle4);
					rectangle4.Offset(4, 0);
				}
			}
			base.OnRenderGrip(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06003F54 RID: 16212 RVA: 0x000FC6B0 File Offset: 0x000FA8B0
		protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
		{
			if (!(e.ToolStrip is ToolStripOverflow))
			{
				Rectangle rectangle;
				rectangle..ctor(1, 2, 24, e.ToolStrip.Height - 3);
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, this.ColorTable.ToolStripGradientBegin, this.ColorTable.ToolStripGradientEnd, 0))
				{
					e.Graphics.FillRectangle(linearGradientBrush, rectangle);
				}
			}
			base.OnRenderImageMargin(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemImageRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003F55 RID: 16213 RVA: 0x000FC748 File Offset: 0x000FA948
		protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
		{
			if (e.Item.Selected)
			{
				e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.CheckPressedBackground), e.ImageRectangle);
				e.Graphics.DrawRectangle(ThemeEngine.Current.ResPool.GetPen(this.ColorTable.ButtonPressedBorder), e.ImageRectangle);
			}
			else if (e.Item.Pressed)
			{
				e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.CheckSelectedBackground), e.ImageRectangle);
				e.Graphics.DrawRectangle(ThemeEngine.Current.ResPool.GetPen(this.ColorTable.ButtonSelectedBorder), e.ImageRectangle);
			}
			else
			{
				e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.CheckSelectedBackground), e.ImageRectangle);
				e.Graphics.DrawRectangle(ThemeEngine.Current.ResPool.GetPen(this.ColorTable.ButtonSelectedBorder), e.ImageRectangle);
			}
			if (e.Item.Image == null)
			{
				ControlPaint.DrawMenuGlyph(e.Graphics, new Rectangle(6, 5, 7, 6), MenuGlyph.Checkmark);
			}
			base.OnRenderItemCheck(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemImageRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06003F56 RID: 16214 RVA: 0x000FC8B0 File Offset: 0x000FAAB0
		protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
		{
			base.OnRenderItemImage(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemTextRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06003F57 RID: 16215 RVA: 0x000FC8BC File Offset: 0x000FAABC
		protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
		{
			base.OnRenderItemText(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003F58 RID: 16216 RVA: 0x000FC8C8 File Offset: 0x000FAAC8
		protected override void OnRenderLabelBackground(ToolStripItemRenderEventArgs e)
		{
			base.OnRenderLabelBackground(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003F59 RID: 16217 RVA: 0x000FC8D4 File Offset: 0x000FAAD4
		protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)e.Item;
			if (toolStripMenuItem.IsOnDropDown)
			{
				Rectangle rectangle;
				rectangle..ctor(1, 0, e.Item.Bounds.Width - 3, e.Item.Bounds.Height - 1);
				if ((e.Item.Selected || e.Item.Pressed) && e.Item.Enabled)
				{
					e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.MenuItemSelectedGradientEnd), rectangle);
				}
				if (toolStripMenuItem.Selected || toolStripMenuItem.Pressed)
				{
					using (Pen pen = new Pen(this.ColorTable.MenuItemBorder))
					{
						e.Graphics.DrawRectangle(pen, rectangle);
					}
				}
			}
			else
			{
				Rectangle rectangle;
				rectangle..ctor(0, 0, e.Item.Width, e.Item.Height);
				if (e.Item.Pressed)
				{
					using (Brush brush = new LinearGradientBrush(rectangle, this.ColorTable.ToolStripGradientBegin, this.ColorTable.ToolStripGradientEnd, 1))
					{
						e.Graphics.FillRectangle(brush, rectangle);
					}
				}
				else if (e.Item.Selected)
				{
					using (Brush brush2 = new LinearGradientBrush(rectangle, this.ColorTable.ButtonSelectedGradientBegin, this.ColorTable.ButtonSelectedGradientEnd, 1))
					{
						e.Graphics.FillRectangle(brush2, rectangle);
					}
				}
				else if (e.Item.BackColor != Control.DefaultBackColor && e.Item.BackColor != Color.Empty)
				{
					using (Brush brush3 = new SolidBrush(e.Item.BackColor))
					{
						e.Graphics.FillRectangle(brush3, rectangle);
					}
				}
				rectangle.Width--;
				rectangle.Height--;
				if (toolStripMenuItem.Selected || toolStripMenuItem.Pressed)
				{
					if (toolStripMenuItem.HasDropDownItems && toolStripMenuItem.DropDown.Visible)
					{
						using (Pen pen2 = new Pen(this.ColorTable.MenuBorder))
						{
							e.Graphics.DrawRectangle(pen2, rectangle);
						}
					}
					else
					{
						using (Pen pen3 = new Pen(this.ColorTable.MenuItemBorder))
						{
							e.Graphics.DrawRectangle(pen3, rectangle);
						}
					}
				}
			}
			base.OnRenderMenuItemBackground(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06003F5A RID: 16218 RVA: 0x000FCC54 File Offset: 0x000FAE54
		protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
		{
			LinearGradientMode linearGradientMode = ((e.ToolStrip.Orientation != Orientation.Vertical) ? 1 : 0);
			Rectangle rectangle;
			if (e.ToolStrip.Orientation == Orientation.Horizontal)
			{
				rectangle..ctor(e.Item.Width - 11, 0, 11, e.Item.Height - 1);
			}
			else
			{
				rectangle..ctor(0, e.Item.Height - 11, e.Item.Width - 1, 11);
			}
			if (e.Item.Selected && !e.Item.Pressed)
			{
				using (Brush brush = new LinearGradientBrush(rectangle, this.ColorTable.ButtonSelectedGradientBegin, this.ColorTable.ButtonSelectedGradientEnd, linearGradientMode))
				{
					e.Graphics.FillRectangle(brush, rectangle);
				}
			}
			else if (e.Item.Pressed)
			{
				using (Brush brush2 = new LinearGradientBrush(rectangle, this.ColorTable.ButtonPressedGradientBegin, this.ColorTable.ButtonPressedGradientEnd, linearGradientMode))
				{
					e.Graphics.FillRectangle(brush2, rectangle);
				}
			}
			else
			{
				using (Brush brush3 = new LinearGradientBrush(rectangle, this.ColorTable.OverflowButtonGradientBegin, this.ColorTable.OverflowButtonGradientEnd, linearGradientMode))
				{
					e.Graphics.FillRectangle(brush3, rectangle);
				}
			}
			ToolStripProfessionalRenderer.PaintOverflowArrow(e, rectangle);
			base.OnRenderOverflowButtonBackground(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripSeparatorRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06003F5B RID: 16219 RVA: 0x000FCE28 File Offset: 0x000FB028
		protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
		{
			if (e.Vertical)
			{
				Rectangle rectangle;
				rectangle..ctor(4, 6, 1, e.Item.Height - 10);
				e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.SeparatorLight), rectangle);
				Rectangle rectangle2;
				rectangle2..ctor(3, 5, 1, e.Item.Height - 10);
				e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.SeparatorDark), rectangle2);
			}
			else
			{
				if (!e.Item.IsOnDropDown)
				{
					Rectangle rectangle3;
					rectangle3..ctor(6, 4, e.Item.Width - 10, 1);
					e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.SeparatorLight), rectangle3);
				}
				Rectangle rectangle4;
				if (e.Item.IsOnDropDown)
				{
					if (e.Item.UseImageMargin)
					{
						rectangle4..ctor(35, 3, e.Item.Width - 36, 1);
					}
					else
					{
						rectangle4..ctor(7, 3, e.Item.Width - 7, 1);
					}
				}
				else
				{
					rectangle4..ctor(5, 3, e.Item.Width - 10, 1);
				}
				e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.SeparatorDark), rectangle4);
			}
			base.OnRenderSeparator(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003F5C RID: 16220 RVA: 0x000FCFB0 File Offset: 0x000FB1B0
		protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripSplitButton toolStripSplitButton = (ToolStripSplitButton)e.Item;
			Rectangle rectangle;
			rectangle..ctor(0, 0, toolStripSplitButton.Width, toolStripSplitButton.Height);
			Rectangle rectangle2;
			rectangle2..ctor(0, 0, toolStripSplitButton.ButtonBounds.Width, toolStripSplitButton.ButtonBounds.Height);
			if (toolStripSplitButton.ButtonSelected && !toolStripSplitButton.DropDownButtonPressed)
			{
				using (Brush brush = new LinearGradientBrush(rectangle, this.ColorTable.ButtonSelectedGradientBegin, this.ColorTable.ButtonSelectedGradientEnd, 1))
				{
					e.Graphics.FillRectangle(brush, rectangle);
				}
			}
			if (toolStripSplitButton.ButtonPressed)
			{
				using (Brush brush2 = new LinearGradientBrush(rectangle2, this.ColorTable.ButtonPressedGradientBegin, this.ColorTable.ButtonPressedGradientEnd, 1))
				{
					e.Graphics.FillRectangle(brush2, rectangle2);
				}
			}
			rectangle.Width--;
			rectangle.Height--;
			if (e.Item.Selected && !toolStripSplitButton.DropDownButtonPressed)
			{
				using (Pen pen = new Pen(this.ColorTable.ButtonSelectedBorder))
				{
					e.Graphics.DrawRectangle(pen, rectangle);
					e.Graphics.DrawLine(pen, rectangle2.Right, 0, rectangle2.Right, rectangle2.Height);
				}
			}
			else if (e.Item.Pressed)
			{
				using (Pen pen2 = new Pen(this.ColorTable.MenuBorder))
				{
					e.Graphics.DrawRectangle(pen2, rectangle);
				}
			}
			base.OnRenderSplitButtonBackground(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06003F5D RID: 16221 RVA: 0x000FD1E8 File Offset: 0x000FB3E8
		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			if (e.ToolStrip.BackgroundImage != null)
			{
				if (e.ToolStrip is StatusStrip)
				{
					e.Graphics.DrawLine(Pens.White, e.AffectedBounds.Left, e.AffectedBounds.Top, e.AffectedBounds.Right, e.AffectedBounds.Top);
				}
				return;
			}
			if (e.ToolStrip is ToolStripDropDown)
			{
				e.Graphics.Clear(this.ColorTable.ToolStripDropDownBackground);
				return;
			}
			if (e.ToolStrip is MenuStrip || e.ToolStrip is StatusStrip)
			{
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(e.AffectedBounds, this.ColorTable.MenuStripGradientBegin, this.ColorTable.MenuStripGradientEnd, (e.ToolStrip.Orientation != Orientation.Horizontal) ? 1 : 0))
				{
					e.Graphics.FillRectangle(linearGradientBrush, e.AffectedBounds);
				}
			}
			else
			{
				using (LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(e.AffectedBounds, this.ColorTable.ToolStripGradientBegin, this.ColorTable.ToolStripGradientEnd, (e.ToolStrip.Orientation != Orientation.Vertical) ? 1 : 0))
				{
					e.Graphics.FillRectangle(linearGradientBrush2, e.AffectedBounds);
				}
			}
			if (e.ToolStrip is StatusStrip)
			{
				e.Graphics.DrawLine(Pens.White, e.AffectedBounds.Left, e.AffectedBounds.Top, e.AffectedBounds.Right, e.AffectedBounds.Top);
			}
			base.OnRenderToolStripBackground(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003F5E RID: 16222 RVA: 0x000FD400 File Offset: 0x000FB600
		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			if (e.ToolStrip is ToolStripDropDown)
			{
				if (e.ToolStrip is ToolStripOverflow)
				{
					e.Graphics.DrawLines(ThemeEngine.Current.ResPool.GetPen(this.ColorTable.MenuBorder), new Point[]
					{
						e.AffectedBounds.Location,
						new Point(e.AffectedBounds.Left, e.AffectedBounds.Bottom - 1),
						new Point(e.AffectedBounds.Right - 1, e.AffectedBounds.Bottom - 1),
						new Point(e.AffectedBounds.Right - 1, e.AffectedBounds.Top),
						new Point(e.AffectedBounds.Left, e.AffectedBounds.Top)
					});
				}
				else
				{
					e.Graphics.DrawLines(ThemeEngine.Current.ResPool.GetPen(this.ColorTable.MenuBorder), new Point[]
					{
						new Point(e.AffectedBounds.Left + e.ConnectedArea.Left, e.AffectedBounds.Top),
						e.AffectedBounds.Location,
						new Point(e.AffectedBounds.Left, e.AffectedBounds.Bottom - 1),
						new Point(e.AffectedBounds.Right - 1, e.AffectedBounds.Bottom - 1),
						new Point(e.AffectedBounds.Right - 1, e.AffectedBounds.Top),
						new Point(e.AffectedBounds.Left + e.ConnectedArea.Right, e.AffectedBounds.Top)
					});
				}
				return;
			}
			if (e.ToolStrip is MenuStrip || e.ToolStrip is StatusStrip)
			{
				return;
			}
			using (Pen pen = new Pen(this.ColorTable.ToolStripBorder))
			{
				if (this.RoundedEdges)
				{
					e.Graphics.DrawLine(pen, new Point(2, e.ToolStrip.Height - 1), new Point(e.ToolStrip.Width - 3, e.ToolStrip.Height - 1));
					e.Graphics.DrawLine(pen, new Point(e.ToolStrip.Width - 2, e.ToolStrip.Height - 2), new Point(e.ToolStrip.Width - 1, e.ToolStrip.Height - 2));
					e.Graphics.DrawLine(pen, new Point(e.ToolStrip.Width - 1, 2), new Point(e.ToolStrip.Width - 1, e.ToolStrip.Height - 3));
				}
				else
				{
					e.Graphics.DrawLine(pen, new Point(e.ToolStrip.Left, e.ToolStrip.Bottom - 1), new Point(e.ToolStrip.Width, e.ToolStrip.Bottom - 1));
				}
			}
			base.OnRenderToolStripBorder(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderToolStripContentPanelBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripContentPanelRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003F5F RID: 16223 RVA: 0x000FD818 File Offset: 0x000FBA18
		protected override void OnRenderToolStripContentPanelBackground(ToolStripContentPanelRenderEventArgs e)
		{
			base.OnRenderToolStripContentPanelBackground(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderToolStripPanelBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripPanelRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003F60 RID: 16224 RVA: 0x000FD824 File Offset: 0x000FBA24
		protected override void OnRenderToolStripPanelBackground(ToolStripPanelRenderEventArgs e)
		{
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(e.ToolStripPanel.Bounds, this.ColorTable.MenuStripGradientBegin, this.ColorTable.MenuStripGradientEnd, (e.ToolStripPanel.Orientation != Orientation.Horizontal) ? 1 : 0))
			{
				e.Graphics.FillRectangle(linearGradientBrush, e.ToolStripPanel.Bounds);
			}
			base.OnRenderToolStripPanelBackground(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderToolStripStatusLabelBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003F61 RID: 16225 RVA: 0x000FD8BC File Offset: 0x000FBABC
		protected override void OnRenderToolStripStatusLabelBackground(ToolStripItemRenderEventArgs e)
		{
			base.OnRenderToolStripStatusLabelBackground(e);
		}

		// Token: 0x06003F62 RID: 16226 RVA: 0x000FD8C8 File Offset: 0x000FBAC8
		private static void PaintOverflowArrow(ToolStripItemRenderEventArgs e, Rectangle paint_here)
		{
			if (e.ToolStrip.Orientation == Orientation.Horizontal)
			{
				Point point;
				point..ctor(paint_here.X + 2, paint_here.Bottom - 9);
				e.Graphics.DrawLine(Pens.White, point.X + 1, point.Y + 1, point.X + 5, point.Y + 1);
				e.Graphics.DrawLine(Pens.Black, point.X, point.Y, point.X + 4, point.Y);
				e.Graphics.DrawLine(Pens.White, point.X + 3, point.Y + 4, point.X + 5, point.Y + 4);
				e.Graphics.DrawLine(Pens.White, point.X + 3, point.Y + 5, point.X + 4, point.Y + 5);
				e.Graphics.DrawLine(Pens.White, point.X + 3, point.Y + 4, point.X + 3, point.Y + 6);
				e.Graphics.DrawLine(Pens.Black, point.X, point.Y + 3, point.X + 4, point.Y + 3);
				e.Graphics.DrawLine(Pens.Black, point.X + 1, point.Y + 4, point.X + 3, point.Y + 4);
				e.Graphics.DrawLine(Pens.Black, point.X + 2, point.Y + 4, point.X + 2, point.Y + 5);
			}
			else
			{
				Point point2;
				point2..ctor(paint_here.Right - 9, paint_here.Y + 2);
				e.Graphics.DrawLine(Pens.White, point2.X + 1, point2.Y + 1, point2.X + 1, point2.Y + 5);
				e.Graphics.DrawLine(Pens.Black, point2.X, point2.Y, point2.X, point2.Y + 4);
				e.Graphics.DrawLine(Pens.White, point2.X + 4, point2.Y + 3, point2.X + 4, point2.Y + 5);
				e.Graphics.DrawLine(Pens.White, point2.X + 5, point2.Y + 3, point2.X + 5, point2.Y + 4);
				e.Graphics.DrawLine(Pens.White, point2.X + 4, point2.Y + 3, point2.X + 6, point2.Y + 3);
				e.Graphics.DrawLine(Pens.Black, point2.X + 3, point2.Y, point2.X + 3, point2.Y + 4);
				e.Graphics.DrawLine(Pens.Black, point2.X + 4, point2.Y + 1, point2.X + 4, point2.Y + 3);
				e.Graphics.DrawLine(Pens.Black, point2.X + 4, point2.Y + 2, point2.X + 5, point2.Y + 2);
			}
		}

		// Token: 0x04001B41 RID: 6977
		private ProfessionalColorTable color_table;

		// Token: 0x04001B42 RID: 6978
		private bool rounded_edges;
	}
}
