using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000336 RID: 822
	internal class ToolBarItem : Component
	{
		// Token: 0x06003974 RID: 14708 RVA: 0x000EC83C File Offset: 0x000EAA3C
		public ToolBarItem(ToolBarButton button)
		{
			this.toolbar = button.Parent;
			this.button = button;
		}

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06003975 RID: 14709 RVA: 0x000EC858 File Offset: 0x000EAA58
		public ToolBarButton Button
		{
			get
			{
				return this.button;
			}
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06003976 RID: 14710 RVA: 0x000EC860 File Offset: 0x000EAA60
		// (set) Token: 0x06003977 RID: 14711 RVA: 0x000EC8D8 File Offset: 0x000EAAD8
		public Rectangle Rectangle
		{
			get
			{
				if (!this.button.Visible || this.toolbar == null)
				{
					return Rectangle.Empty;
				}
				if (this.button.Style == ToolBarButtonStyle.DropDownButton && this.toolbar.DropDownArrows)
				{
					Rectangle rectangle = this.bounds;
					rectangle.Width += ThemeEngine.Current.ToolBarDropDownWidth;
					return rectangle;
				}
				return this.bounds;
			}
			set
			{
				this.bounds = value;
			}
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06003978 RID: 14712 RVA: 0x000EC8E4 File Offset: 0x000EAAE4
		// (set) Token: 0x06003979 RID: 14713 RVA: 0x000EC8F4 File Offset: 0x000EAAF4
		public Point Location
		{
			get
			{
				return this.bounds.Location;
			}
			set
			{
				this.bounds.Location = value;
			}
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x0600397A RID: 14714 RVA: 0x000EC904 File Offset: 0x000EAB04
		public Rectangle ImageRectangle
		{
			get
			{
				Rectangle rectangle = this.image_rect;
				rectangle.X += this.bounds.X;
				rectangle.Y += this.bounds.Y;
				return rectangle;
			}
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x0600397B RID: 14715 RVA: 0x000EC94C File Offset: 0x000EAB4C
		public Rectangle TextRectangle
		{
			get
			{
				Rectangle rectangle = this.text_rect;
				rectangle.X += this.bounds.X;
				rectangle.Y += this.bounds.Y;
				return rectangle;
			}
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x0600397C RID: 14716 RVA: 0x000EC994 File Offset: 0x000EAB94
		private Size TextSize
		{
			get
			{
				StringFormat stringFormat = new StringFormat();
				stringFormat.HotkeyPrefix = 2;
				SizeF sizeF = TextRenderer.MeasureString(this.button.Text, this.toolbar.Font, SizeF.Empty, stringFormat);
				if (sizeF == SizeF.Empty)
				{
					return Size.Empty;
				}
				return new Size((int)Math.Ceiling((double)sizeF.Width) + 6, (int)Math.Ceiling((double)sizeF.Height));
			}
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x0600397D RID: 14717 RVA: 0x000ECA0C File Offset: 0x000EAC0C
		// (set) Token: 0x0600397E RID: 14718 RVA: 0x000ECA24 File Offset: 0x000EAC24
		public bool Pressed
		{
			get
			{
				return this.pressed && this.inside;
			}
			set
			{
				this.pressed = value;
			}
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x0600397F RID: 14719 RVA: 0x000ECA30 File Offset: 0x000EAC30
		// (set) Token: 0x06003980 RID: 14720 RVA: 0x000ECA38 File Offset: 0x000EAC38
		public bool DDPressed
		{
			get
			{
				return this.dd_pressed;
			}
			set
			{
				this.dd_pressed = value;
			}
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06003981 RID: 14721 RVA: 0x000ECA44 File Offset: 0x000EAC44
		// (set) Token: 0x06003982 RID: 14722 RVA: 0x000ECA4C File Offset: 0x000EAC4C
		public bool Inside
		{
			get
			{
				return this.inside;
			}
			set
			{
				this.inside = value;
			}
		}

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06003983 RID: 14723 RVA: 0x000ECA58 File Offset: 0x000EAC58
		// (set) Token: 0x06003984 RID: 14724 RVA: 0x000ECA60 File Offset: 0x000EAC60
		public bool Hilight
		{
			get
			{
				return this.hilight;
			}
			set
			{
				if (this.hilight == value)
				{
					return;
				}
				this.hilight = value;
				this.Invalidate();
			}
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x000ECA7C File Offset: 0x000EAC7C
		public Size CalculateSize()
		{
			Theme theme = ThemeEngine.Current;
			int num = this.toolbar.ButtonSize.Height + 2 * theme.ToolBarGripWidth;
			if (this.button.Style == ToolBarButtonStyle.Separator)
			{
				return new Size(theme.ToolBarSeparatorWidth, num);
			}
			Size size;
			if (this.TextSize.IsEmpty && this.button.Image == null)
			{
				size = this.toolbar.default_size;
			}
			else
			{
				size = this.TextSize;
			}
			Size size2 = ((!(this.toolbar.ImageSize == Size.Empty)) ? this.toolbar.ImageSize : new Size(16, 16));
			int num2 = size2.Width + 2 * theme.ToolBarImageGripWidth;
			int num3 = size2.Height + 2 * theme.ToolBarImageGripWidth;
			if (this.toolbar.TextAlign == ToolBarTextAlign.Right)
			{
				size.Width = num2 + size.Width;
				size.Height = ((size.Height <= num3) ? num3 : size.Height);
			}
			else
			{
				size.Height = num3 + size.Height;
				size.Width = ((size.Width <= num2) ? num2 : size.Width);
			}
			size.Width += theme.ToolBarGripWidth;
			size.Height += theme.ToolBarGripWidth;
			return size;
		}

		// Token: 0x06003986 RID: 14726 RVA: 0x000ECC08 File Offset: 0x000EAE08
		public bool Layout(bool vertical, int calculated_size)
		{
			if (this.toolbar == null || !this.button.Visible)
			{
				return false;
			}
			Size buttonSize = this.toolbar.ButtonSize;
			Size size = buttonSize;
			if (!this.toolbar.SizeSpecified || this.button.Style == ToolBarButtonStyle.Separator)
			{
				size = this.CalculateSize();
				if (size.Width == 0 || size.Height == 0)
				{
					size = buttonSize;
				}
				if (vertical)
				{
					size.Width = calculated_size;
				}
				else
				{
					size.Height = calculated_size;
				}
			}
			return this.Layout(size);
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x000ECCA4 File Offset: 0x000EAEA4
		public bool Layout(Size size)
		{
			if (this.toolbar == null || !this.button.Visible)
			{
				return false;
			}
			this.bounds.Size = size;
			Size size2 = ((!(this.toolbar.ImageSize == Size.Empty)) ? this.toolbar.ImageSize : new Size(16, 16));
			int toolBarImageGripWidth = ThemeEngine.Current.ToolBarImageGripWidth;
			Rectangle rectangle;
			Rectangle rectangle2;
			if (this.toolbar.TextAlign == ToolBarTextAlign.Underneath)
			{
				rectangle..ctor((this.bounds.Size.Width - size2.Width) / 2 - toolBarImageGripWidth, 0, size2.Width + 2 + toolBarImageGripWidth, size2.Height + 2 * toolBarImageGripWidth);
				rectangle2..ctor(0, rectangle.Height, this.bounds.Size.Width, this.bounds.Size.Height - rectangle.Height - 2 * toolBarImageGripWidth);
			}
			else
			{
				rectangle..ctor(0, 0, size2.Width + 2 * toolBarImageGripWidth, size2.Height + 2 * toolBarImageGripWidth);
				rectangle2..ctor(rectangle.Width, 0, this.bounds.Size.Width - rectangle.Width, this.bounds.Size.Height - 2 * toolBarImageGripWidth);
			}
			bool flag = false;
			if (rectangle != this.image_rect || rectangle2 != this.text_rect)
			{
				flag = true;
			}
			this.image_rect = rectangle;
			this.text_rect = rectangle2;
			return flag;
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x000ECE4C File Offset: 0x000EB04C
		public void Invalidate()
		{
			if (this.toolbar != null)
			{
				this.toolbar.Invalidate(this.Rectangle);
			}
		}

		// Token: 0x040019EB RID: 6635
		private ToolBar toolbar;

		// Token: 0x040019EC RID: 6636
		private ToolBarButton button;

		// Token: 0x040019ED RID: 6637
		private Rectangle bounds;

		// Token: 0x040019EE RID: 6638
		private Rectangle image_rect;

		// Token: 0x040019EF RID: 6639
		private Rectangle text_rect;

		// Token: 0x040019F0 RID: 6640
		private bool dd_pressed;

		// Token: 0x040019F1 RID: 6641
		private bool inside;

		// Token: 0x040019F2 RID: 6642
		private bool hilight;

		// Token: 0x040019F3 RID: 6643
		private bool pressed;
	}
}
