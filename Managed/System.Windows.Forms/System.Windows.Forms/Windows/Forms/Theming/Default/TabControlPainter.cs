using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms.Theming.Default
{
	// Token: 0x020004CB RID: 1227
	internal class TabControlPainter
	{
		// Token: 0x06004C66 RID: 19558 RVA: 0x00131578 File Offset: 0x0012F778
		public TabControlPainter()
		{
			this.defaultItemSize = new Size(42, 21);
			this.defaultPadding = new Point(6, 3);
			this.selectedTabDelta = new Rectangle(2, 2, 4, 3);
			this.selectedSpacing = 0;
			this.rowSpacingNormal = new Size(0, 0);
			this.rowSpacingButtons = new Size(3, 3);
			this.rowSpacingFlatButtons = new Size(9, 3);
			this.colSpacing = 0;
			this.minimumTabWidth = 42;
			this.scrollerWidth = 17;
			this.focusRectSpacing = new Point(2, 2);
			this.tabPanelOffset = new Point(4, 0);
			this.flatButtonSpacing = 8;
			this.tabPageSpacing = new Rectangle(4, 2, 3, 4);
			this.imagePadding = new Point(2, 3);
			this.defaultFormatting = new StringFormat();
			this.defaultFormatting.Alignment = 0;
			this.defaultFormatting.LineAlignment = 0;
			this.defaultFormatting.FormatFlags = 20480;
			this.defaultFormatting.HotkeyPrefix = 1;
			this.borderThickness = new Rectangle(1, 1, 2, 2);
		}

		// Token: 0x17001336 RID: 4918
		// (get) Token: 0x06004C67 RID: 19559 RVA: 0x00131688 File Offset: 0x0012F888
		protected SystemResPool ResPool
		{
			get
			{
				return ThemeEngine.Current.ResPool;
			}
		}

		// Token: 0x17001337 RID: 4919
		// (get) Token: 0x06004C68 RID: 19560 RVA: 0x00131694 File Offset: 0x0012F894
		// (set) Token: 0x06004C69 RID: 19561 RVA: 0x0013169C File Offset: 0x0012F89C
		public virtual Size DefaultItemSize
		{
			get
			{
				return this.defaultItemSize;
			}
			set
			{
				this.defaultItemSize = value;
			}
		}

		// Token: 0x17001338 RID: 4920
		// (get) Token: 0x06004C6A RID: 19562 RVA: 0x001316A8 File Offset: 0x0012F8A8
		// (set) Token: 0x06004C6B RID: 19563 RVA: 0x001316B0 File Offset: 0x0012F8B0
		public virtual Point DefaultPadding
		{
			get
			{
				return this.defaultPadding;
			}
			set
			{
				this.defaultPadding = value;
			}
		}

		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x06004C6C RID: 19564 RVA: 0x001316BC File Offset: 0x0012F8BC
		// (set) Token: 0x06004C6D RID: 19565 RVA: 0x001316C4 File Offset: 0x0012F8C4
		public virtual int MinimumTabWidth
		{
			get
			{
				return this.minimumTabWidth;
			}
			set
			{
				this.minimumTabWidth = value;
			}
		}

		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x06004C6E RID: 19566 RVA: 0x001316D0 File Offset: 0x0012F8D0
		// (set) Token: 0x06004C6F RID: 19567 RVA: 0x001316D8 File Offset: 0x0012F8D8
		public virtual Rectangle SelectedTabDelta
		{
			get
			{
				return this.selectedTabDelta;
			}
			set
			{
				this.selectedTabDelta = value;
			}
		}

		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x06004C70 RID: 19568 RVA: 0x001316E4 File Offset: 0x0012F8E4
		// (set) Token: 0x06004C71 RID: 19569 RVA: 0x001316EC File Offset: 0x0012F8EC
		public virtual Point TabPanelOffset
		{
			get
			{
				return this.tabPanelOffset;
			}
			set
			{
				this.tabPanelOffset = value;
			}
		}

		// Token: 0x1700133C RID: 4924
		// (get) Token: 0x06004C72 RID: 19570 RVA: 0x001316F8 File Offset: 0x0012F8F8
		// (set) Token: 0x06004C73 RID: 19571 RVA: 0x00131700 File Offset: 0x0012F900
		public virtual int SelectedSpacing
		{
			get
			{
				return this.selectedSpacing;
			}
			set
			{
				this.selectedSpacing = value;
			}
		}

		// Token: 0x1700133D RID: 4925
		// (get) Token: 0x06004C74 RID: 19572 RVA: 0x0013170C File Offset: 0x0012F90C
		// (set) Token: 0x06004C75 RID: 19573 RVA: 0x00131714 File Offset: 0x0012F914
		public virtual Size RowSpacingNormal
		{
			get
			{
				return this.rowSpacingNormal;
			}
			set
			{
				this.rowSpacingNormal = value;
			}
		}

		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x06004C76 RID: 19574 RVA: 0x00131720 File Offset: 0x0012F920
		// (set) Token: 0x06004C77 RID: 19575 RVA: 0x00131728 File Offset: 0x0012F928
		public virtual Size RowSpacingButtons
		{
			get
			{
				return this.rowSpacingButtons;
			}
			set
			{
				this.rowSpacingButtons = value;
			}
		}

		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x06004C78 RID: 19576 RVA: 0x00131734 File Offset: 0x0012F934
		// (set) Token: 0x06004C79 RID: 19577 RVA: 0x0013173C File Offset: 0x0012F93C
		public virtual Size RowSpacingFlatButtons
		{
			get
			{
				return this.rowSpacingFlatButtons;
			}
			set
			{
				this.rowSpacingFlatButtons = value;
			}
		}

		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x06004C7A RID: 19578 RVA: 0x00131748 File Offset: 0x0012F948
		// (set) Token: 0x06004C7B RID: 19579 RVA: 0x00131750 File Offset: 0x0012F950
		public virtual Point FocusRectSpacing
		{
			get
			{
				return this.focusRectSpacing;
			}
			set
			{
				this.focusRectSpacing = value;
			}
		}

		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x06004C7C RID: 19580 RVA: 0x0013175C File Offset: 0x0012F95C
		// (set) Token: 0x06004C7D RID: 19581 RVA: 0x00131764 File Offset: 0x0012F964
		public virtual int ColSpacing
		{
			get
			{
				return this.colSpacing;
			}
			set
			{
				this.colSpacing = value;
			}
		}

		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x06004C7E RID: 19582 RVA: 0x00131770 File Offset: 0x0012F970
		// (set) Token: 0x06004C7F RID: 19583 RVA: 0x00131778 File Offset: 0x0012F978
		public virtual int FlatButtonSpacing
		{
			get
			{
				return this.flatButtonSpacing;
			}
			set
			{
				this.flatButtonSpacing = value;
			}
		}

		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x06004C80 RID: 19584 RVA: 0x00131784 File Offset: 0x0012F984
		// (set) Token: 0x06004C81 RID: 19585 RVA: 0x0013178C File Offset: 0x0012F98C
		public virtual Rectangle TabPageSpacing
		{
			get
			{
				return this.tabPageSpacing;
			}
			set
			{
				this.tabPageSpacing = value;
			}
		}

		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x06004C82 RID: 19586 RVA: 0x00131798 File Offset: 0x0012F998
		// (set) Token: 0x06004C83 RID: 19587 RVA: 0x001317A0 File Offset: 0x0012F9A0
		public virtual Point ImagePadding
		{
			get
			{
				return this.imagePadding;
			}
			set
			{
				this.imagePadding = value;
			}
		}

		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x06004C84 RID: 19588 RVA: 0x001317AC File Offset: 0x0012F9AC
		// (set) Token: 0x06004C85 RID: 19589 RVA: 0x001317B4 File Offset: 0x0012F9B4
		public virtual StringFormat DefaultFormatting
		{
			get
			{
				return this.defaultFormatting;
			}
			set
			{
				this.defaultFormatting = value;
			}
		}

		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x06004C86 RID: 19590 RVA: 0x001317C0 File Offset: 0x0012F9C0
		// (set) Token: 0x06004C87 RID: 19591 RVA: 0x001317C8 File Offset: 0x0012F9C8
		public virtual Rectangle BorderThickness
		{
			get
			{
				return this.borderThickness;
			}
			set
			{
				this.borderThickness = value;
			}
		}

		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x06004C88 RID: 19592 RVA: 0x001317D4 File Offset: 0x0012F9D4
		// (set) Token: 0x06004C89 RID: 19593 RVA: 0x001317DC File Offset: 0x0012F9DC
		public virtual int ScrollerWidth
		{
			get
			{
				return this.scrollerWidth;
			}
			set
			{
				this.scrollerWidth = value;
			}
		}

		// Token: 0x06004C8A RID: 19594 RVA: 0x001317E8 File Offset: 0x0012F9E8
		public virtual Size RowSpacing(TabControl tab)
		{
			switch (tab.Appearance)
			{
			case TabAppearance.Normal:
				return this.rowSpacingNormal;
			case TabAppearance.Buttons:
				return this.rowSpacingButtons;
			case TabAppearance.FlatButtons:
				return this.rowSpacingFlatButtons;
			default:
				throw new Exception("Invalid Appearance value: " + tab.Appearance);
			}
		}

		// Token: 0x06004C8B RID: 19595 RVA: 0x00131844 File Offset: 0x0012FA44
		public virtual Rectangle GetLeftScrollRect(TabControl tab)
		{
			TabAlignment alignment = tab.Alignment;
			if (alignment != TabAlignment.Top)
			{
				Rectangle tabPanelRect = this.GetTabPanelRect(tab);
				return new Rectangle(tab.ClientRectangle.Right - this.scrollerWidth * 2, tabPanelRect.Bottom + 2, this.scrollerWidth, this.scrollerWidth);
			}
			return new Rectangle(tab.ClientRectangle.Right - this.scrollerWidth * 2, tab.ClientRectangle.Top + 1, this.scrollerWidth, this.scrollerWidth);
		}

		// Token: 0x06004C8C RID: 19596 RVA: 0x001318D8 File Offset: 0x0012FAD8
		public virtual Rectangle GetRightScrollRect(TabControl tab)
		{
			TabAlignment alignment = tab.Alignment;
			if (alignment != TabAlignment.Top)
			{
				Rectangle tabPanelRect = this.GetTabPanelRect(tab);
				return new Rectangle(tab.ClientRectangle.Right - this.scrollerWidth, tabPanelRect.Bottom + 2, this.scrollerWidth, this.scrollerWidth);
			}
			return new Rectangle(tab.ClientRectangle.Right - this.scrollerWidth, tab.ClientRectangle.Top + 1, this.scrollerWidth, this.scrollerWidth);
		}

		// Token: 0x06004C8D RID: 19597 RVA: 0x00131968 File Offset: 0x0012FB68
		public Rectangle GetDisplayRectangle(TabControl tab)
		{
			Rectangle tabPanelRect = this.GetTabPanelRect(tab);
			return new Rectangle(tabPanelRect.Left + this.tabPageSpacing.X, tabPanelRect.Top + this.tabPageSpacing.Y, tabPanelRect.Width - this.tabPageSpacing.X - this.tabPageSpacing.Width, tabPanelRect.Height - this.tabPageSpacing.Y - this.tabPageSpacing.Height);
		}

		// Token: 0x06004C8E RID: 19598 RVA: 0x001319E8 File Offset: 0x0012FBE8
		public Rectangle GetTabPanelRect(TabControl tab)
		{
			Rectangle rectangle;
			rectangle..ctor(tab.ClientRectangle.X, tab.ClientRectangle.Y, tab.ClientRectangle.Width, tab.ClientRectangle.Height);
			if (tab.TabCount == 0)
			{
				return rectangle;
			}
			int height = this.RowSpacing(tab).Height;
			int num = (tab.ItemSize.Height + height - this.selectedTabDelta.Height) * tab.RowCount + this.selectedTabDelta.Y;
			switch (tab.Alignment)
			{
			case TabAlignment.Top:
				rectangle.Y += num;
				rectangle.Height -= num;
				break;
			case TabAlignment.Bottom:
				rectangle.Height -= num;
				break;
			case TabAlignment.Left:
				rectangle.X += num;
				rectangle.Width -= num;
				break;
			case TabAlignment.Right:
				rectangle.Width -= num;
				break;
			}
			return rectangle;
		}

		// Token: 0x06004C8F RID: 19599 RVA: 0x00131B1C File Offset: 0x0012FD1C
		public virtual void Draw(Graphics dc, Rectangle area, TabControl tab)
		{
			this.DrawBackground(dc, area, tab);
			int num = 0;
			int num2 = tab.TabPages.Count;
			int num3 = 1;
			if (tab.Alignment == TabAlignment.Top)
			{
				num = num2;
				num2 = 0;
				num3 = -1;
			}
			for (int num4 = num; num4 != num2; num4 += num3)
			{
				for (int i = tab.SliderPos; i < tab.TabPages.Count; i++)
				{
					if (i != tab.SelectedIndex)
					{
						if (num4 == tab.TabPages[i].Row)
						{
							Rectangle tabRect = tab.GetTabRect(i);
							if (tabRect.IntersectsWith(area))
							{
								this.DrawTab(dc, tab.TabPages[i], tab, tabRect, false);
							}
						}
					}
				}
			}
			if (tab.SelectedIndex != -1 && tab.SelectedIndex >= tab.SliderPos)
			{
				Rectangle tabRect2 = tab.GetTabRect(tab.SelectedIndex);
				if (tabRect2.IntersectsWith(area))
				{
					this.DrawTab(dc, tab.TabPages[tab.SelectedIndex], tab, tabRect2, true);
				}
			}
			if (tab.ShowSlider)
			{
				Rectangle rightScrollRect = this.GetRightScrollRect(tab);
				Rectangle leftScrollRect = this.GetLeftScrollRect(tab);
				this.DrawScrollButton(dc, rightScrollRect, area, ScrollButton.Right, tab.RightSliderState);
				this.DrawScrollButton(dc, leftScrollRect, area, ScrollButton.Left, tab.LeftSliderState);
			}
		}

		// Token: 0x06004C90 RID: 19600 RVA: 0x00131C84 File Offset: 0x0012FE84
		protected virtual void DrawScrollButton(Graphics dc, Rectangle bounds, Rectangle clippingArea, ScrollButton button, PushButtonState state)
		{
			ControlPaint.DrawScrollButton(dc, bounds, button, TabControlPainter.GetButtonState(state));
		}

		// Token: 0x06004C91 RID: 19601 RVA: 0x00131C98 File Offset: 0x0012FE98
		private static ButtonState GetButtonState(PushButtonState state)
		{
			if (state != PushButtonState.Pressed)
			{
				return ButtonState.Normal;
			}
			return ButtonState.Pushed;
		}

		// Token: 0x06004C92 RID: 19602 RVA: 0x00131CBC File Offset: 0x0012FEBC
		protected virtual void DrawBackground(Graphics dc, Rectangle area, TabControl tab)
		{
			Brush control = SystemBrushes.Control;
			dc.FillRectangle(control, area);
			Rectangle tabPanelRect = this.GetTabPanelRect(tab);
			if (tab.Appearance == TabAppearance.Normal)
			{
				ControlPaint.DrawBorder3D(dc, tabPanelRect, Border3DStyle.RaisedInner, Border3DSide.Left | Border3DSide.Top);
				ControlPaint.DrawBorder3D(dc, tabPanelRect, Border3DStyle.Raised, Border3DSide.Right | Border3DSide.Bottom);
			}
		}

		// Token: 0x06004C93 RID: 19603 RVA: 0x00131D00 File Offset: 0x0012FF00
		protected virtual int DrawTab(Graphics dc, TabPage page, TabControl tab, Rectangle bounds, bool is_selected)
		{
			int num = bounds.Width;
			dc.FillRectangle(this.ResPool.GetSolidBrush(tab.BackColor), bounds);
			if (tab.Appearance == TabAppearance.Buttons || tab.Appearance == TabAppearance.FlatButtons)
			{
				if (tab.Appearance == TabAppearance.FlatButtons)
				{
					int width = bounds.Width;
					bounds.Width += this.flatButtonSpacing - 2;
					num = bounds.Width;
					if (tab.Alignment == TabAlignment.Top || tab.Alignment == TabAlignment.Bottom)
					{
						ThemeEngine.Current.CPDrawBorder3D(dc, bounds, Border3DStyle.Etched, Border3DSide.Right);
					}
					else
					{
						ThemeEngine.Current.CPDrawBorder3D(dc, bounds, Border3DStyle.Etched, Border3DSide.Top);
					}
					bounds.Width = width;
				}
				if (is_selected)
				{
					ThemeEngine.Current.CPDrawBorder3D(dc, bounds, Border3DStyle.Sunken, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
				}
				else if (tab.Appearance != TabAppearance.FlatButtons)
				{
					ThemeEngine.Current.CPDrawBorder3D(dc, bounds, Border3DStyle.Raised, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
				}
			}
			else
			{
				CPColor cpcolor = this.ResPool.GetCPColor(tab.BackColor);
				Pen pen = this.ResPool.GetPen(cpcolor.LightLight);
				switch (tab.Alignment)
				{
				case TabAlignment.Top:
					dc.DrawLine(pen, bounds.Left, bounds.Bottom - 1, bounds.Left, bounds.Top + 3);
					dc.DrawLine(pen, bounds.Left, bounds.Top + 3, bounds.Left + 2, bounds.Top);
					dc.DrawLine(pen, bounds.Left + 2, bounds.Top, bounds.Right - 3, bounds.Top);
					dc.DrawLine(SystemPens.ControlDark, bounds.Right - 2, bounds.Top + 1, bounds.Right - 2, bounds.Bottom - 1);
					dc.DrawLine(SystemPens.ControlDarkDark, bounds.Right - 2, bounds.Top + 1, bounds.Right - 1, bounds.Top + 2);
					dc.DrawLine(SystemPens.ControlDarkDark, bounds.Right - 1, bounds.Top + 2, bounds.Right - 1, bounds.Bottom - 1);
					break;
				case TabAlignment.Bottom:
					dc.DrawLine(pen, bounds.Left, bounds.Top, bounds.Left, bounds.Bottom - 2);
					dc.DrawLine(pen, bounds.Left, bounds.Bottom - 2, bounds.Left + 3, bounds.Bottom);
					dc.DrawLine(SystemPens.ControlDarkDark, bounds.Left + 3, bounds.Bottom, bounds.Right - 3, bounds.Bottom);
					dc.DrawLine(SystemPens.ControlDark, bounds.Left + 3, bounds.Bottom - 1, bounds.Right - 3, bounds.Bottom - 1);
					dc.DrawLine(SystemPens.ControlDark, bounds.Right - 2, bounds.Bottom - 1, bounds.Right - 2, bounds.Top + 1);
					dc.DrawLine(SystemPens.ControlDarkDark, bounds.Right - 2, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 2);
					dc.DrawLine(SystemPens.ControlDarkDark, bounds.Right - 1, bounds.Bottom - 2, bounds.Right - 1, bounds.Top + 1);
					break;
				case TabAlignment.Left:
					dc.DrawLine(pen, bounds.Left - 2, bounds.Top, bounds.Right, bounds.Top);
					dc.DrawLine(pen, bounds.Left, bounds.Top + 2, bounds.Left - 2, bounds.Top);
					dc.DrawLine(pen, bounds.Left, bounds.Top + 2, bounds.Left, bounds.Bottom - 2);
					dc.DrawLine(SystemPens.ControlDark, bounds.Left, bounds.Bottom - 2, bounds.Left + 2, bounds.Bottom - 1);
					dc.DrawLine(SystemPens.ControlDark, bounds.Left + 2, bounds.Bottom - 1, bounds.Right, bounds.Bottom - 1);
					dc.DrawLine(SystemPens.ControlDarkDark, bounds.Left + 2, bounds.Bottom, bounds.Right, bounds.Bottom);
					break;
				default:
					dc.DrawLine(pen, bounds.Left, bounds.Top, bounds.Right - 3, bounds.Top);
					dc.DrawLine(pen, bounds.Right - 3, bounds.Top, bounds.Right, bounds.Top + 3);
					dc.DrawLine(SystemPens.ControlDark, bounds.Right - 1, bounds.Top + 1, bounds.Right - 1, bounds.Bottom - 1);
					dc.DrawLine(SystemPens.ControlDark, bounds.Left, bounds.Bottom - 1, bounds.Right - 2, bounds.Bottom - 1);
					dc.DrawLine(SystemPens.ControlDarkDark, bounds.Right, bounds.Top + 3, bounds.Right, bounds.Bottom - 3);
					dc.DrawLine(SystemPens.ControlDarkDark, bounds.Left, bounds.Bottom, bounds.Right - 3, bounds.Bottom);
					break;
				}
			}
			Rectangle rectangle;
			rectangle..ctor(bounds.Left + this.focusRectSpacing.X + this.borderThickness.Left, bounds.Top + this.focusRectSpacing.Y + this.borderThickness.Top, bounds.Width - this.focusRectSpacing.X * 2 - this.borderThickness.Width + 1, bounds.Height - this.focusRectSpacing.Y * 2 - this.borderThickness.Height);
			if (tab.DrawMode == TabDrawMode.Normal && page.Text != null)
			{
				if (tab.Alignment == TabAlignment.Left)
				{
					dc.TranslateTransform((float)bounds.Left, (float)bounds.Bottom);
					dc.RotateTransform(-90f);
					dc.DrawString(page.Text, page.Font, SystemBrushes.ControlText, (float)(tab.Padding.X - 2), (float)tab.Padding.Y, this.defaultFormatting);
					dc.ResetTransform();
				}
				else if (tab.Alignment == TabAlignment.Right)
				{
					dc.TranslateTransform((float)bounds.Right, (float)bounds.Top);
					dc.RotateTransform(90f);
					dc.DrawString(page.Text, page.Font, SystemBrushes.ControlText, (float)(tab.Padding.X - 2), (float)tab.Padding.Y, this.defaultFormatting);
					dc.ResetTransform();
				}
				else
				{
					Rectangle rectangle2 = rectangle;
					if (tab.ImageList != null && page.ImageIndex >= 0 && page.ImageIndex < tab.ImageList.Images.Count)
					{
						int num2 = rectangle.Y + (rectangle.Height - tab.ImageList.ImageSize.Height) / 2;
						tab.ImageList.Draw(dc, new Point(rectangle.X, num2), page.ImageIndex);
						rectangle2.X += tab.ImageList.ImageSize.Width + 2;
						rectangle2.Width -= tab.ImageList.ImageSize.Width + 2;
					}
					dc.DrawString(page.Text, page.Font, SystemBrushes.ControlText, rectangle2, this.defaultFormatting);
				}
			}
			else if (page.Text != null)
			{
				DrawItemState drawItemState = DrawItemState.None;
				if (page == tab.SelectedTab)
				{
					drawItemState |= DrawItemState.Selected;
				}
				DrawItemEventArgs drawItemEventArgs = new DrawItemEventArgs(dc, tab.Font, bounds, tab.IndexForTabPage(page), drawItemState, page.ForeColor, page.BackColor);
				tab.OnDrawItemInternal(drawItemEventArgs);
				return num;
			}
			if (page.Parent.Focused && is_selected && tab.ShowFocusCues)
			{
				rectangle.Width--;
				ThemeEngine.Current.CPDrawFocusRectangle(dc, rectangle, tab.ForeColor, tab.BackColor);
			}
			return num;
		}

		// Token: 0x06004C94 RID: 19604 RVA: 0x00132598 File Offset: 0x00130798
		public virtual bool HasHotElementStyles(TabControl tabControl)
		{
			return false;
		}

		// Token: 0x040029F2 RID: 10738
		private Size defaultItemSize;

		// Token: 0x040029F3 RID: 10739
		private Point defaultPadding;

		// Token: 0x040029F4 RID: 10740
		private int minimumTabWidth;

		// Token: 0x040029F5 RID: 10741
		private Rectangle selectedTabDelta;

		// Token: 0x040029F6 RID: 10742
		private Point tabPanelOffset;

		// Token: 0x040029F7 RID: 10743
		private int selectedSpacing;

		// Token: 0x040029F8 RID: 10744
		private Size rowSpacingNormal;

		// Token: 0x040029F9 RID: 10745
		private Size rowSpacingButtons;

		// Token: 0x040029FA RID: 10746
		private Size rowSpacingFlatButtons;

		// Token: 0x040029FB RID: 10747
		private int scrollerWidth;

		// Token: 0x040029FC RID: 10748
		private Point focusRectSpacing;

		// Token: 0x040029FD RID: 10749
		private Rectangle tabPageSpacing;

		// Token: 0x040029FE RID: 10750
		private int colSpacing;

		// Token: 0x040029FF RID: 10751
		private int flatButtonSpacing;

		// Token: 0x04002A00 RID: 10752
		private Point imagePadding;

		// Token: 0x04002A01 RID: 10753
		private StringFormat defaultFormatting;

		// Token: 0x04002A02 RID: 10754
		private Rectangle borderThickness;
	}
}
