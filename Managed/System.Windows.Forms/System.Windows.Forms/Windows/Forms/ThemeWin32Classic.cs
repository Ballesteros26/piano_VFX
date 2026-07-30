using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms.Theming;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x0200032D RID: 813
	internal class ThemeWin32Classic : Theme
	{
		// Token: 0x06003797 RID: 14231 RVA: 0x000D7850 File Offset: 0x000D5A50
		public ThemeWin32Classic()
		{
			this.defaultWindowBackColor = this.ColorWindow;
			this.defaultWindowForeColor = this.ColorControlText;
			this.window_border_font = new Font(FontFamily.GenericSansSerif, 8.25f, 1);
			ThemeWin32Classic.string_format_menu_text = new StringFormat();
			ThemeWin32Classic.string_format_menu_text.LineAlignment = 1;
			ThemeWin32Classic.string_format_menu_text.Alignment = 0;
			ThemeWin32Classic.string_format_menu_text.HotkeyPrefix = 1;
			ThemeWin32Classic.string_format_menu_text.SetTabStops(0f, new float[] { 50f });
			ThemeWin32Classic.string_format_menu_text.FormatFlags |= 4096;
			ThemeWin32Classic.string_format_menu_shortcut = new StringFormat();
			ThemeWin32Classic.string_format_menu_shortcut.LineAlignment = 1;
			ThemeWin32Classic.string_format_menu_shortcut.Alignment = 2;
			ThemeWin32Classic.string_format_menu_menubar_text = new StringFormat();
			ThemeWin32Classic.string_format_menu_menubar_text.LineAlignment = 1;
			ThemeWin32Classic.string_format_menu_menubar_text.Alignment = 1;
			ThemeWin32Classic.string_format_menu_menubar_text.HotkeyPrefix = 1;
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06003799 RID: 14233 RVA: 0x000D7958 File Offset: 0x000D5B58
		public override Version Version
		{
			get
			{
				return new Version(0, 1, 0, 0);
			}
		}

		// Token: 0x0600379A RID: 14234 RVA: 0x000D7964 File Offset: 0x000D5B64
		public override void ResetDefaults()
		{
			Console.WriteLine("NOT IMPLEMENTED: ResetDefault()");
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x0600379B RID: 14235 RVA: 0x000D7970 File Offset: 0x000D5B70
		public override bool DoubleBufferingSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x0600379C RID: 14236 RVA: 0x000D7974 File Offset: 0x000D5B74
		public override int HorizontalScrollBarHeight
		{
			get
			{
				return XplatUI.HorizontalScrollBarHeight;
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x0600379D RID: 14237 RVA: 0x000D797C File Offset: 0x000D5B7C
		public override int VerticalScrollBarWidth
		{
			get
			{
				return XplatUI.VerticalScrollBarWidth;
			}
		}

		// Token: 0x0600379E RID: 14238 RVA: 0x000D7984 File Offset: 0x000D5B84
		protected Brush GetControlBackBrush(Color c)
		{
			if (c.ToArgb() == this.DefaultControlBackColor.ToArgb())
			{
				return SystemBrushes.Control;
			}
			return this.ResPool.GetSolidBrush(c);
		}

		// Token: 0x0600379F RID: 14239 RVA: 0x000D79C0 File Offset: 0x000D5BC0
		protected Brush GetControlForeBrush(Color c)
		{
			if (c.ToArgb() == this.DefaultControlForeColor.ToArgb())
			{
				return SystemBrushes.ControlText;
			}
			return this.ResPool.GetSolidBrush(c);
		}

		// Token: 0x060037A0 RID: 14240 RVA: 0x000D79FC File Offset: 0x000D5BFC
		public override Font GetLinkFont(Control control)
		{
			return new Font(control.Font.FontFamily, control.Font.Size, control.Font.Style | 4, control.Font.Unit);
		}

		// Token: 0x060037A1 RID: 14241 RVA: 0x000D7A3C File Offset: 0x000D5C3C
		public override void DrawOwnerDrawBackground(DrawItemEventArgs e)
		{
			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
			{
				e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
				return;
			}
			e.Graphics.FillRectangle(this.ResPool.GetSolidBrush(e.BackColor), e.Bounds);
		}

		// Token: 0x060037A2 RID: 14242 RVA: 0x000D7A90 File Offset: 0x000D5C90
		public override void DrawOwnerDrawFocusRectangle(DrawItemEventArgs e)
		{
			if (e.State == DrawItemState.Focus)
			{
				this.CPDrawFocusRectangle(e.Graphics, e.Bounds, e.ForeColor, e.BackColor);
			}
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x000D7AC8 File Offset: 0x000D5CC8
		public override void DrawButton(Graphics g, Button b, Rectangle textBounds, Rectangle imageBounds, Rectangle clipRectangle)
		{
			this.DrawButtonBackground(g, b, clipRectangle);
			if (imageBounds.Size != Size.Empty)
			{
				this.DrawButtonImage(g, b, imageBounds);
			}
			if (b.Focused && b.Enabled && b.ShowFocusCues)
			{
				this.DrawButtonFocus(g, b);
			}
			if (textBounds != Rectangle.Empty)
			{
				this.DrawButtonText(g, b, textBounds);
			}
		}

		// Token: 0x060037A4 RID: 14244 RVA: 0x000D7B44 File Offset: 0x000D5D44
		public virtual void DrawButtonBackground(Graphics g, Button button, Rectangle clipArea)
		{
			if (button.Pressed)
			{
				ThemeElements.DrawButton(g, button.ClientRectangle, ButtonThemeState.Pressed, button.BackColor, button.ForeColor);
			}
			else if (button.InternalSelected)
			{
				ThemeElements.DrawButton(g, button.ClientRectangle, ButtonThemeState.Default, button.BackColor, button.ForeColor);
			}
			else if (button.Entered)
			{
				ThemeElements.DrawButton(g, button.ClientRectangle, ButtonThemeState.Entered, button.BackColor, button.ForeColor);
			}
			else if (!button.Enabled)
			{
				ThemeElements.DrawButton(g, button.ClientRectangle, ButtonThemeState.Disabled, button.BackColor, button.ForeColor);
			}
			else
			{
				ThemeElements.DrawButton(g, button.ClientRectangle, ButtonThemeState.Normal, button.BackColor, button.ForeColor);
			}
		}

		// Token: 0x060037A5 RID: 14245 RVA: 0x000D7C10 File Offset: 0x000D5E10
		public virtual void DrawButtonFocus(Graphics g, Button button)
		{
			ControlPaint.DrawFocusRectangle(g, Rectangle.Inflate(button.ClientRectangle, -4, -4));
		}

		// Token: 0x060037A6 RID: 14246 RVA: 0x000D7C28 File Offset: 0x000D5E28
		public virtual void DrawButtonImage(Graphics g, ButtonBase button, Rectangle imageBounds)
		{
			if (button.Enabled)
			{
				g.DrawImage(button.Image, imageBounds);
			}
			else
			{
				this.CPDrawImageDisabled(g, button.Image, imageBounds.Left, imageBounds.Top, this.ColorControl);
			}
		}

		// Token: 0x060037A7 RID: 14247 RVA: 0x000D7C74 File Offset: 0x000D5E74
		public virtual void DrawButtonText(Graphics g, ButtonBase button, Rectangle textBounds)
		{
			textBounds.Height = Math.Max(textBounds.Height, button.Font.Height);
			if (button.Enabled)
			{
				TextRenderer.DrawTextInternal(g, button.Text, button.Font, textBounds, button.ForeColor, button.TextFormatFlags, button.UseCompatibleTextRendering);
			}
			else
			{
				this.DrawStringDisabled20(g, button.Text, button.Font, textBounds, button.BackColor, button.TextFormatFlags, button.UseCompatibleTextRendering);
			}
		}

		// Token: 0x060037A8 RID: 14248 RVA: 0x000D7CFC File Offset: 0x000D5EFC
		public override void DrawFlatButton(Graphics g, ButtonBase b, Rectangle textBounds, Rectangle imageBounds, Rectangle clipRectangle)
		{
			if (b.BackgroundImage == null)
			{
				this.DrawFlatButtonBackground(g, b, clipRectangle);
			}
			if (imageBounds.Size != Size.Empty)
			{
				this.DrawFlatButtonImage(g, b, imageBounds);
			}
			if (b.Focused && b.Enabled && b.ShowFocusCues)
			{
				this.DrawFlatButtonFocus(g, b);
			}
			if (textBounds != Rectangle.Empty)
			{
				this.DrawFlatButtonText(g, b, textBounds);
			}
		}

		// Token: 0x060037A9 RID: 14249 RVA: 0x000D7D80 File Offset: 0x000D5F80
		public virtual void DrawFlatButtonBackground(Graphics g, ButtonBase button, Rectangle clipArea)
		{
			if (button.Pressed)
			{
				ThemeElements.DrawFlatButton(g, button.ClientRectangle, ButtonThemeState.Pressed, button.BackColor, button.ForeColor, button.FlatAppearance);
			}
			else if (button.InternalSelected)
			{
				if (button.Entered)
				{
					ThemeElements.DrawFlatButton(g, button.ClientRectangle, ButtonThemeState.Entered | ButtonThemeState.Default, button.BackColor, button.ForeColor, button.FlatAppearance);
				}
				else
				{
					ThemeElements.DrawFlatButton(g, button.ClientRectangle, ButtonThemeState.Default, button.BackColor, button.ForeColor, button.FlatAppearance);
				}
			}
			else if (button.Entered)
			{
				ThemeElements.DrawFlatButton(g, button.ClientRectangle, ButtonThemeState.Entered, button.BackColor, button.ForeColor, button.FlatAppearance);
			}
			else if (!button.Enabled)
			{
				ThemeElements.DrawFlatButton(g, button.ClientRectangle, ButtonThemeState.Disabled, button.BackColor, button.ForeColor, button.FlatAppearance);
			}
			else
			{
				ThemeElements.DrawFlatButton(g, button.ClientRectangle, ButtonThemeState.Normal, button.BackColor, button.ForeColor, button.FlatAppearance);
			}
		}

		// Token: 0x060037AA RID: 14250 RVA: 0x000D7E9C File Offset: 0x000D609C
		public virtual void DrawFlatButtonFocus(Graphics g, ButtonBase button)
		{
			if (!button.Pressed)
			{
				Color color = ControlPaint.Dark(button.BackColor);
				g.DrawRectangle(this.ResPool.GetPen(color), new Rectangle(button.ClientRectangle.Left + 4, button.ClientRectangle.Top + 4, button.ClientRectangle.Width - 9, button.ClientRectangle.Height - 9));
			}
		}

		// Token: 0x060037AB RID: 14251 RVA: 0x000D7F1C File Offset: 0x000D611C
		public virtual void DrawFlatButtonImage(Graphics g, ButtonBase button, Rectangle imageBounds)
		{
			this.DrawButtonImage(g, button, imageBounds);
		}

		// Token: 0x060037AC RID: 14252 RVA: 0x000D7F28 File Offset: 0x000D6128
		public virtual void DrawFlatButtonText(Graphics g, ButtonBase button, Rectangle textBounds)
		{
			this.DrawButtonText(g, button, textBounds);
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x000D7F34 File Offset: 0x000D6134
		public override void DrawPopupButton(Graphics g, Button b, Rectangle textBounds, Rectangle imageBounds, Rectangle clipRectangle)
		{
			this.DrawPopupButtonBackground(g, b, clipRectangle);
			if (imageBounds.Size != Size.Empty)
			{
				this.DrawPopupButtonImage(g, b, imageBounds);
			}
			if (b.Focused && b.Enabled && b.ShowFocusCues)
			{
				this.DrawPopupButtonFocus(g, b);
			}
			if (textBounds != Rectangle.Empty)
			{
				this.DrawPopupButtonText(g, b, textBounds);
			}
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x000D7FB0 File Offset: 0x000D61B0
		public virtual void DrawPopupButtonBackground(Graphics g, Button button, Rectangle clipArea)
		{
			if (button.Pressed)
			{
				ThemeElements.DrawPopupButton(g, button.ClientRectangle, ButtonThemeState.Pressed, button.BackColor, button.ForeColor);
			}
			else if (button.Entered)
			{
				ThemeElements.DrawPopupButton(g, button.ClientRectangle, ButtonThemeState.Entered, button.BackColor, button.ForeColor);
			}
			else if (button.InternalSelected)
			{
				ThemeElements.DrawPopupButton(g, button.ClientRectangle, ButtonThemeState.Default, button.BackColor, button.ForeColor);
			}
			else if (!button.Enabled)
			{
				ThemeElements.DrawPopupButton(g, button.ClientRectangle, ButtonThemeState.Disabled, button.BackColor, button.ForeColor);
			}
			else
			{
				ThemeElements.DrawPopupButton(g, button.ClientRectangle, ButtonThemeState.Normal, button.BackColor, button.ForeColor);
			}
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x000D807C File Offset: 0x000D627C
		public virtual void DrawPopupButtonFocus(Graphics g, Button button)
		{
			this.DrawButtonFocus(g, button);
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x000D8088 File Offset: 0x000D6288
		public virtual void DrawPopupButtonImage(Graphics g, Button button, Rectangle imageBounds)
		{
			this.DrawButtonImage(g, button, imageBounds);
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x000D8094 File Offset: 0x000D6294
		public virtual void DrawPopupButtonText(Graphics g, Button button, Rectangle textBounds)
		{
			this.DrawButtonText(g, button, textBounds);
		}

		// Token: 0x060037B2 RID: 14258 RVA: 0x000D80A0 File Offset: 0x000D62A0
		public override Size CalculateButtonAutoSize(Button button)
		{
			Size empty = Size.Empty;
			Size size = TextRenderer.MeasureTextInternal(button.Text, button.Font, button.UseCompatibleTextRendering);
			Size size2 = ((button.Image != null) ? button.Image.Size : Size.Empty);
			if (button.Text.Length != 0)
			{
				size.Height += 4;
				size.Width += 4;
			}
			switch (button.TextImageRelation)
			{
			case TextImageRelation.Overlay:
				empty.Height = Math.Max((button.Text.Length != 0) ? size.Height : 0, size2.Height);
				empty.Width = Math.Max(size.Width, size2.Width);
				break;
			case TextImageRelation.ImageAboveText:
			case TextImageRelation.TextAboveImage:
				empty.Height = size.Height + size2.Height;
				empty.Width = Math.Max(size.Width, size2.Width);
				break;
			case TextImageRelation.ImageBeforeText:
			case TextImageRelation.TextBeforeImage:
				empty.Height = Math.Max(size.Height, size2.Height);
				empty.Width = size.Width + size2.Width;
				break;
			}
			empty.Height += button.Padding.Vertical + 6;
			empty.Width += button.Padding.Horizontal + 6;
			return empty;
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x000D8248 File Offset: 0x000D6448
		public override void CalculateButtonTextAndImageLayout(ButtonBase button, out Rectangle textRectangle, out Rectangle imageRectangle)
		{
			Image image = button.Image;
			string text = button.Text;
			Rectangle clientRectangle = button.ClientRectangle;
			Size size = TextRenderer.MeasureTextInternal(text, button.Font, clientRectangle.Size, button.TextFormatFlags, button.UseCompatibleTextRendering);
			Size size2 = ((image != null) ? image.Size : Size.Empty);
			textRectangle = Rectangle.Empty;
			imageRectangle = Rectangle.Empty;
			switch (button.TextImageRelation)
			{
			case TextImageRelation.Overlay:
			{
				textRectangle = Rectangle.Inflate(clientRectangle, -4, -4);
				if (button.Pressed)
				{
					textRectangle.Offset(1, 1);
				}
				if (image == null)
				{
					return;
				}
				int height = image.Height;
				int width = image.Width;
				ContentAlignment imageAlign = button.ImageAlign;
				int num;
				int num2;
				switch (imageAlign)
				{
				case 1:
					num = 5;
					num2 = 5;
					break;
				case 2:
					num = (clientRectangle.Width - width) / 2;
					num2 = 5;
					break;
				default:
					if (imageAlign != 16)
					{
						if (imageAlign != 32)
						{
							if (imageAlign != 64)
							{
								if (imageAlign != 256)
								{
									if (imageAlign != 512)
									{
										if (imageAlign != 1024)
										{
											num = 5;
											num2 = 5;
										}
										else
										{
											num = clientRectangle.Width - width - 4;
											num2 = clientRectangle.Height - height - 4;
										}
									}
									else
									{
										num = (clientRectangle.Width - width) / 2;
										num2 = clientRectangle.Height - height - 4;
									}
								}
								else
								{
									num = 5;
									num2 = clientRectangle.Height - height - 4;
								}
							}
							else
							{
								num = clientRectangle.Width - width - 4;
								num2 = (clientRectangle.Height - height) / 2;
							}
						}
						else
						{
							num = (clientRectangle.Width - width) / 2;
							num2 = (clientRectangle.Height - height) / 2;
						}
					}
					else
					{
						num = 5;
						num2 = (clientRectangle.Height - height) / 2;
					}
					break;
				case 4:
					num = clientRectangle.Width - width - 5;
					num2 = 5;
					break;
				}
				imageRectangle..ctor(num, num2, width, height);
				break;
			}
			case TextImageRelation.ImageAboveText:
				clientRectangle.Inflate(-4, -4);
				this.LayoutTextAboveOrBelowImage(clientRectangle, false, size, size2, button.TextAlign, button.ImageAlign, out textRectangle, out imageRectangle);
				break;
			case TextImageRelation.TextAboveImage:
				clientRectangle.Inflate(-4, -4);
				this.LayoutTextAboveOrBelowImage(clientRectangle, true, size, size2, button.TextAlign, button.ImageAlign, out textRectangle, out imageRectangle);
				break;
			case TextImageRelation.ImageBeforeText:
				clientRectangle.Inflate(-4, -4);
				this.LayoutTextBeforeOrAfterImage(clientRectangle, false, size, size2, button.TextAlign, button.ImageAlign, out textRectangle, out imageRectangle);
				break;
			case TextImageRelation.TextBeforeImage:
				clientRectangle.Inflate(-4, -4);
				this.LayoutTextBeforeOrAfterImage(clientRectangle, true, size, size2, button.TextAlign, button.ImageAlign, out textRectangle, out imageRectangle);
				break;
			}
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x000D8540 File Offset: 0x000D6740
		private void LayoutTextBeforeOrAfterImage(Rectangle totalArea, bool textFirst, Size textSize, Size imageSize, ContentAlignment textAlign, ContentAlignment imageAlign, out Rectangle textRect, out Rectangle imageRect)
		{
			int num = 0;
			int num2 = textSize.Width + num + imageSize.Width;
			if (!textFirst)
			{
				num += 2;
			}
			if (num2 > totalArea.Width)
			{
				textSize.Width = totalArea.Width - num - imageSize.Width;
				num2 = totalArea.Width;
			}
			int num3 = totalArea.Width - num2;
			int num4 = 0;
			HorizontalAlignment horizontalAlignment = this.GetHorizontalAlignment(textAlign);
			HorizontalAlignment horizontalAlignment2 = this.GetHorizontalAlignment(imageAlign);
			if (horizontalAlignment2 == HorizontalAlignment.Left)
			{
				num4 = 0;
			}
			else if (horizontalAlignment2 == HorizontalAlignment.Right && horizontalAlignment == HorizontalAlignment.Right)
			{
				num4 = num3;
			}
			else if (horizontalAlignment2 == HorizontalAlignment.Center && (horizontalAlignment == HorizontalAlignment.Left || horizontalAlignment == HorizontalAlignment.Center))
			{
				num4 += num3 / 3;
			}
			else
			{
				num4 += 2 * (num3 / 3);
			}
			Rectangle rectangle;
			Rectangle rectangle2;
			if (textFirst)
			{
				rectangle..ctor(totalArea.Left + num4, this.AlignInRectangle(totalArea, textSize, textAlign).Top, textSize.Width, textSize.Height);
				rectangle2..ctor(rectangle.Right + num, this.AlignInRectangle(totalArea, imageSize, imageAlign).Top, imageSize.Width, imageSize.Height);
			}
			else
			{
				rectangle2..ctor(totalArea.Left + num4, this.AlignInRectangle(totalArea, imageSize, imageAlign).Top, imageSize.Width, imageSize.Height);
				rectangle..ctor(rectangle2.Right + num, this.AlignInRectangle(totalArea, textSize, textAlign).Top, textSize.Width, textSize.Height);
			}
			textRect = rectangle;
			imageRect = rectangle2;
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x000D86EC File Offset: 0x000D68EC
		private void LayoutTextAboveOrBelowImage(Rectangle totalArea, bool textFirst, Size textSize, Size imageSize, ContentAlignment textAlign, ContentAlignment imageAlign, out Rectangle textRect, out Rectangle imageRect)
		{
			int num = 0;
			int num2 = textSize.Height + num + imageSize.Height;
			if (textFirst)
			{
				num += 2;
			}
			if (textSize.Width > totalArea.Width)
			{
				textSize.Width = totalArea.Width;
			}
			if (num2 > totalArea.Height && textFirst)
			{
				imageSize = Size.Empty;
				num2 = totalArea.Height;
			}
			int num3 = totalArea.Height - num2;
			int num4 = 0;
			ThemeWin32Classic.VerticalAlignment verticalAlignment = this.GetVerticalAlignment(textAlign);
			ThemeWin32Classic.VerticalAlignment verticalAlignment2 = this.GetVerticalAlignment(imageAlign);
			if (verticalAlignment2 == ThemeWin32Classic.VerticalAlignment.Top)
			{
				num4 = 0;
			}
			else if (verticalAlignment2 == ThemeWin32Classic.VerticalAlignment.Bottom && verticalAlignment == ThemeWin32Classic.VerticalAlignment.Bottom)
			{
				num4 = num3;
			}
			else if (verticalAlignment2 == ThemeWin32Classic.VerticalAlignment.Center && (verticalAlignment == ThemeWin32Classic.VerticalAlignment.Top || verticalAlignment == ThemeWin32Classic.VerticalAlignment.Center))
			{
				num4 += num3 / 3;
			}
			else
			{
				num4 += 2 * (num3 / 3);
			}
			Rectangle rectangle;
			Rectangle rectangle2;
			if (textFirst)
			{
				rectangle..ctor(this.AlignInRectangle(totalArea, textSize, textAlign).Left, totalArea.Top + num4, textSize.Width, textSize.Height);
				rectangle2..ctor(this.AlignInRectangle(totalArea, imageSize, imageAlign).Left, rectangle.Bottom + num, imageSize.Width, imageSize.Height);
			}
			else
			{
				rectangle2..ctor(this.AlignInRectangle(totalArea, imageSize, imageAlign).Left, totalArea.Top + num4, imageSize.Width, imageSize.Height);
				rectangle..ctor(this.AlignInRectangle(totalArea, textSize, textAlign).Left, rectangle2.Bottom + num, textSize.Width, textSize.Height);
				if (rectangle.Bottom > totalArea.Bottom)
				{
					rectangle.Y = totalArea.Top;
				}
			}
			textRect = rectangle;
			imageRect = rectangle2;
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x000D88D0 File Offset: 0x000D6AD0
		private HorizontalAlignment GetHorizontalAlignment(ContentAlignment align)
		{
			switch (align)
			{
			case 1:
				break;
			case 2:
				return HorizontalAlignment.Center;
			default:
				if (align != 16)
				{
					if (align == 32)
					{
						return HorizontalAlignment.Center;
					}
					if (align == 64)
					{
						return HorizontalAlignment.Right;
					}
					if (align != 256)
					{
						if (align == 512)
						{
							return HorizontalAlignment.Center;
						}
						if (align != 1024)
						{
							return HorizontalAlignment.Left;
						}
						return HorizontalAlignment.Right;
					}
				}
				break;
			case 4:
				return HorizontalAlignment.Right;
			}
			return HorizontalAlignment.Left;
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x000D893C File Offset: 0x000D6B3C
		private ThemeWin32Classic.VerticalAlignment GetVerticalAlignment(ContentAlignment align)
		{
			switch (align)
			{
			case 1:
			case 2:
			case 4:
				return ThemeWin32Classic.VerticalAlignment.Top;
			default:
				if (align == 16 || align == 32 || align == 64)
				{
					return ThemeWin32Classic.VerticalAlignment.Center;
				}
				if (align != 256 && align != 512 && align != 1024)
				{
					return ThemeWin32Classic.VerticalAlignment.Top;
				}
				return ThemeWin32Classic.VerticalAlignment.Bottom;
			}
		}

		// Token: 0x060037B8 RID: 14264 RVA: 0x000D89A8 File Offset: 0x000D6BA8
		internal Rectangle AlignInRectangle(Rectangle outer, Size inner, ContentAlignment align)
		{
			int num = 0;
			int num2 = 0;
			if (align == 256 || align == 16 || align == 1)
			{
				num = outer.X;
			}
			else if (align == 512 || align == 32 || align == 2)
			{
				num = Math.Max(outer.X + (outer.Width - inner.Width) / 2, outer.Left);
			}
			else if (align == 1024 || align == 64 || align == 4)
			{
				num = outer.Right - inner.Width;
			}
			if (align == 2 || align == 1 || align == 4)
			{
				num2 = outer.Y;
			}
			else if (align == 32 || align == 16 || align == 64)
			{
				num2 = outer.Y + (outer.Height - inner.Height) / 2;
			}
			else if (align == 512 || align == 1024 || align == 256)
			{
				num2 = outer.Bottom - inner.Height;
			}
			return new Rectangle(num, num2, Math.Min(inner.Width, outer.Width), Math.Min(inner.Height, outer.Height));
		}

		// Token: 0x060037B9 RID: 14265 RVA: 0x000D8B08 File Offset: 0x000D6D08
		public override void DrawButtonBase(Graphics dc, Rectangle clip_area, ButtonBase button)
		{
			this.ButtonBase_DrawButton(button, dc);
			if (button.FlatStyle != FlatStyle.System && (button.image != null || button.image_list != null))
			{
				this.ButtonBase_DrawImage(button, dc);
			}
			if (ThemeWin32Classic.ShouldPaintFocusRectagle(button))
			{
				this.ButtonBase_DrawFocus(button, dc);
			}
			if (button.Text != null && button.Text != string.Empty)
			{
				this.ButtonBase_DrawText(button, dc);
			}
		}

		// Token: 0x060037BA RID: 14266 RVA: 0x000D8B84 File Offset: 0x000D6D84
		protected static bool ShouldPaintFocusRectagle(ButtonBase button)
		{
			return (button.Focused || button.paint_as_acceptbutton) && button.Enabled && button.ShowFocusCues;
		}

		// Token: 0x060037BB RID: 14267 RVA: 0x000D8BBC File Offset: 0x000D6DBC
		protected virtual void ButtonBase_DrawButton(ButtonBase button, Graphics dc)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = button.BackColor.ToArgb() == this.ColorControl.ToArgb();
			CPColor cpcolor = ((!flag3) ? this.ResPool.GetCPColor(button.BackColor) : CPColor.Empty);
			if (button is CheckBox)
			{
				flag = true;
				flag2 = ((CheckBox)button).Checked;
			}
			else if (button is RadioButton)
			{
				flag = true;
				flag2 = ((RadioButton)button).Checked;
			}
			Rectangle rectangle;
			if (button.Focused && button.Enabled && !flag)
			{
				rectangle = Rectangle.Inflate(button.ClientRectangle, -1, -1);
			}
			else
			{
				rectangle = button.ClientRectangle;
			}
			if (button.FlatStyle == FlatStyle.Popup)
			{
				if (!button.is_pressed && !button.is_entered && !flag2)
				{
					this.Internal_DrawButton(dc, rectangle, 1, cpcolor, flag3, button.BackColor);
				}
				else if (!button.is_pressed && button.is_entered && !flag2)
				{
					this.Internal_DrawButton(dc, rectangle, 2, cpcolor, flag3, button.BackColor);
				}
				else if (button.is_pressed || flag2)
				{
					this.Internal_DrawButton(dc, rectangle, 1, cpcolor, flag3, button.BackColor);
				}
			}
			else if (button.FlatStyle == FlatStyle.Flat)
			{
				if (button.is_entered && !button.is_pressed && !flag2)
				{
					if (button.image == null && button.image_list == null)
					{
						Brush brush = ((!flag3) ? this.ResPool.GetSolidBrush(cpcolor.Dark) : SystemBrushes.ControlDark);
						dc.FillRectangle(brush, rectangle);
					}
				}
				else if (button.is_pressed || flag2)
				{
					if (button.image == null && button.image_list == null)
					{
						Brush brush2 = ((!flag3) ? this.ResPool.GetSolidBrush(cpcolor.LightLight) : SystemBrushes.ControlLightLight);
						dc.FillRectangle(brush2, rectangle);
					}
					Pen pen = ((!flag3) ? this.ResPool.GetPen(cpcolor.Dark) : SystemPens.ControlDark);
					dc.DrawRectangle(pen, rectangle.X + 4, rectangle.Y + 4, rectangle.Width - 9, rectangle.Height - 9);
				}
				this.Internal_DrawButton(dc, rectangle, 3, cpcolor, flag3, button.BackColor);
			}
			else if ((!button.is_pressed || !button.Enabled) && !flag2)
			{
				this.Internal_DrawButton(dc, rectangle, 0, cpcolor, flag3, button.BackColor);
			}
			else
			{
				this.Internal_DrawButton(dc, rectangle, 1, cpcolor, flag3, button.BackColor);
			}
		}

		// Token: 0x060037BC RID: 14268 RVA: 0x000D8E94 File Offset: 0x000D7094
		private void Internal_DrawButton(Graphics dc, Rectangle rect, int state, CPColor cpcolor, bool is_ColorControl, Color backcolor)
		{
			switch (state)
			{
			case 0:
			{
				Pen pen = ((!is_ColorControl) ? this.ResPool.GetPen(cpcolor.LightLight) : SystemPens.ControlLightLight);
				dc.DrawLine(pen, rect.X, rect.Y, rect.X, rect.Bottom - 2);
				dc.DrawLine(pen, rect.X + 1, rect.Y, rect.Right - 2, rect.Y);
				pen = ((!is_ColorControl) ? this.ResPool.GetPen(backcolor) : SystemPens.Control);
				dc.DrawLine(pen, rect.X + 1, rect.Y + 1, rect.X + 1, rect.Bottom - 3);
				dc.DrawLine(pen, rect.X + 2, rect.Y + 1, rect.Right - 3, rect.Y + 1);
				pen = ((!is_ColorControl) ? this.ResPool.GetPen(cpcolor.Dark) : SystemPens.ControlDark);
				dc.DrawLine(pen, rect.X + 1, rect.Bottom - 2, rect.Right - 2, rect.Bottom - 2);
				dc.DrawLine(pen, rect.Right - 2, rect.Y + 1, rect.Right - 2, rect.Bottom - 3);
				pen = ((!is_ColorControl) ? this.ResPool.GetPen(cpcolor.DarkDark) : SystemPens.ControlDarkDark);
				dc.DrawLine(pen, rect.X, rect.Bottom - 1, rect.Right - 1, rect.Bottom - 1);
				dc.DrawLine(pen, rect.Right - 1, rect.Y, rect.Right - 1, rect.Bottom - 2);
				break;
			}
			case 1:
			{
				Pen pen = ((!is_ColorControl) ? this.ResPool.GetPen(cpcolor.Dark) : SystemPens.ControlDark);
				dc.DrawRectangle(pen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
				break;
			}
			case 2:
			{
				Pen pen = ((!is_ColorControl) ? this.ResPool.GetPen(cpcolor.LightLight) : SystemPens.ControlLightLight);
				dc.DrawLine(pen, rect.X, rect.Y, rect.X, rect.Bottom - 2);
				dc.DrawLine(pen, rect.X + 1, rect.Y, rect.Right - 2, rect.Y);
				pen = ((!is_ColorControl) ? this.ResPool.GetPen(cpcolor.Dark) : SystemPens.ControlDark);
				dc.DrawLine(pen, rect.X, rect.Bottom - 1, rect.Right - 1, rect.Bottom - 1);
				dc.DrawLine(pen, rect.Right - 1, rect.Y, rect.Right - 1, rect.Bottom - 2);
				break;
			}
			case 3:
			{
				Pen pen = ((!is_ColorControl) ? this.ResPool.GetPen(cpcolor.DarkDark) : SystemPens.ControlDarkDark);
				dc.DrawRectangle(pen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
				break;
			}
			}
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x000D9228 File Offset: 0x000D7428
		protected virtual void ButtonBase_DrawImage(ButtonBase button, Graphics dc)
		{
			int width = button.ClientSize.Width;
			int height = button.ClientSize.Height;
			Image image;
			if (button.ImageIndex != -1)
			{
				image = button.image_list.Images[button.ImageIndex];
			}
			else
			{
				image = button.image;
			}
			int width2 = image.Width;
			int height2 = image.Height;
			ContentAlignment imageAlign = button.ImageAlign;
			int num;
			int num2;
			switch (imageAlign)
			{
			case 1:
				num = 5;
				num2 = 5;
				break;
			case 2:
				num = (width - width2) / 2;
				num2 = 5;
				break;
			default:
				if (imageAlign != 16)
				{
					if (imageAlign != 32)
					{
						if (imageAlign != 64)
						{
							if (imageAlign != 256)
							{
								if (imageAlign != 512)
								{
									if (imageAlign != 1024)
									{
										num = 5;
										num2 = 5;
									}
									else
									{
										num = width - width2 - 4;
										num2 = height - height2 - 4;
									}
								}
								else
								{
									num = (width - width2) / 2;
									num2 = height - height2 - 4;
								}
							}
							else
							{
								num = 5;
								num2 = height - height2 - 4;
							}
						}
						else
						{
							num = width - width2 - 4;
							num2 = (height - height2) / 2;
						}
					}
					else
					{
						num = (width - width2) / 2;
						num2 = (height - height2) / 2;
					}
				}
				else
				{
					num = 5;
					num2 = (height - height2) / 2;
				}
				break;
			case 4:
				num = width - width2 - 5;
				num2 = 5;
				break;
			}
			dc.SetClip(new Rectangle(3, 3, width - 5, height - 5));
			if (button.Enabled)
			{
				dc.DrawImage(image, num, num2, width2, height2);
			}
			else
			{
				this.CPDrawImageDisabled(dc, image, num, num2, this.ColorControl);
			}
			dc.ResetClip();
		}

		// Token: 0x060037BE RID: 14270 RVA: 0x000D93E0 File Offset: 0x000D75E0
		protected virtual void ButtonBase_DrawFocus(ButtonBase button, Graphics dc)
		{
			Color color = button.ForeColor;
			int num = -3;
			if (!(button is CheckBox) && !(button is RadioButton))
			{
				num = -4;
				if (button.FlatStyle == FlatStyle.Popup && !button.is_pressed)
				{
					color = ControlPaint.Dark(button.BackColor);
				}
				dc.DrawRectangle(this.ResPool.GetPen(color), button.ClientRectangle.X, button.ClientRectangle.Y, button.ClientRectangle.Width - 1, button.ClientRectangle.Height - 1);
			}
			if (button.Focused)
			{
				Rectangle rectangle = Rectangle.Inflate(button.ClientRectangle, num, num);
				ControlPaint.DrawFocusRectangle(dc, rectangle);
			}
		}

		// Token: 0x060037BF RID: 14271 RVA: 0x000D94A4 File Offset: 0x000D76A4
		protected virtual void ButtonBase_DrawText(ButtonBase button, Graphics dc)
		{
			Rectangle clientRectangle = button.ClientRectangle;
			Rectangle rectangle = Rectangle.Inflate(clientRectangle, -4, -4);
			if (button.is_pressed)
			{
				rectangle.X++;
				rectangle.Y++;
			}
			rectangle.Height = Math.Max(button.Font.Height, rectangle.Height);
			if (button.Enabled)
			{
				dc.DrawString(button.Text, button.Font, this.ResPool.GetSolidBrush(button.ForeColor), rectangle, button.text_format);
			}
			else if (button.FlatStyle == FlatStyle.Flat || button.FlatStyle == FlatStyle.Popup)
			{
				dc.DrawString(button.Text, button.Font, this.ResPool.GetSolidBrush(this.ColorGrayText), rectangle, button.text_format);
			}
			else
			{
				this.CPDrawStringDisabled(dc, button.Text, button.Font, button.BackColor, rectangle, button.text_format);
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x060037C0 RID: 14272 RVA: 0x000D95BC File Offset: 0x000D77BC
		public override Size ButtonBaseDefaultSize
		{
			get
			{
				return new Size(75, 23);
			}
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x000D95C8 File Offset: 0x000D77C8
		public override void DrawCheckBox(Graphics g, CheckBox cb, Rectangle glyphArea, Rectangle textBounds, Rectangle imageBounds, Rectangle clipRectangle)
		{
			if (cb.Appearance == Appearance.Button && cb.FlatStyle != FlatStyle.Flat)
			{
				this.ButtonBase_DrawButton(cb, g);
			}
			else if (cb.Appearance != Appearance.Button)
			{
				this.DrawCheckBoxGlyph(g, cb, glyphArea);
			}
			if (cb.Appearance == Appearance.Button && cb.FlatStyle == FlatStyle.Flat)
			{
				this.DrawFlatButton(g, cb, textBounds, imageBounds, clipRectangle);
			}
			if (imageBounds.Size != Size.Empty)
			{
				this.DrawCheckBoxImage(g, cb, imageBounds);
			}
			if (cb.Focused && cb.Enabled && cb.ShowFocusCues && textBounds != Rectangle.Empty)
			{
				this.DrawCheckBoxFocus(g, cb, textBounds);
			}
			if (textBounds != Rectangle.Empty)
			{
				this.DrawCheckBoxText(g, cb, textBounds);
			}
		}

		// Token: 0x060037C2 RID: 14274 RVA: 0x000D96AC File Offset: 0x000D78AC
		public virtual void DrawCheckBoxGlyph(Graphics g, CheckBox cb, Rectangle glyphArea)
		{
			if (cb.Pressed)
			{
				ThemeElements.CurrentTheme.CheckBoxPainter.PaintCheckBox(g, glyphArea, cb.BackColor, cb.ForeColor, ElementState.Pressed, cb.FlatStyle, cb.CheckState);
			}
			else if (cb.InternalSelected)
			{
				ThemeElements.CurrentTheme.CheckBoxPainter.PaintCheckBox(g, glyphArea, cb.BackColor, cb.ForeColor, ElementState.Normal, cb.FlatStyle, cb.CheckState);
			}
			else if (cb.Entered)
			{
				ThemeElements.CurrentTheme.CheckBoxPainter.PaintCheckBox(g, glyphArea, cb.BackColor, cb.ForeColor, ElementState.Hot, cb.FlatStyle, cb.CheckState);
			}
			else if (!cb.Enabled)
			{
				ThemeElements.CurrentTheme.CheckBoxPainter.PaintCheckBox(g, glyphArea, cb.BackColor, cb.ForeColor, ElementState.Disabled, cb.FlatStyle, cb.CheckState);
			}
			else
			{
				ThemeElements.CurrentTheme.CheckBoxPainter.PaintCheckBox(g, glyphArea, cb.BackColor, cb.ForeColor, ElementState.Normal, cb.FlatStyle, cb.CheckState);
			}
		}

		// Token: 0x060037C3 RID: 14275 RVA: 0x000D97CC File Offset: 0x000D79CC
		public virtual void DrawCheckBoxFocus(Graphics g, CheckBox cb, Rectangle focusArea)
		{
			ControlPaint.DrawFocusRectangle(g, focusArea);
		}

		// Token: 0x060037C4 RID: 14276 RVA: 0x000D97D8 File Offset: 0x000D79D8
		public virtual void DrawCheckBoxImage(Graphics g, CheckBox cb, Rectangle imageBounds)
		{
			if (cb.Enabled)
			{
				g.DrawImage(cb.Image, imageBounds);
			}
			else
			{
				this.CPDrawImageDisabled(g, cb.Image, imageBounds.Left, imageBounds.Top, this.ColorControl);
			}
		}

		// Token: 0x060037C5 RID: 14277 RVA: 0x000D9824 File Offset: 0x000D7A24
		public virtual void DrawCheckBoxText(Graphics g, CheckBox cb, Rectangle textBounds)
		{
			if (cb.Enabled)
			{
				TextRenderer.DrawTextInternal(g, cb.Text, cb.Font, textBounds, cb.ForeColor, cb.TextFormatFlags, cb.UseCompatibleTextRendering);
			}
			else
			{
				this.DrawStringDisabled20(g, cb.Text, cb.Font, textBounds, cb.BackColor, cb.TextFormatFlags, cb.UseCompatibleTextRendering);
			}
		}

		// Token: 0x060037C6 RID: 14278 RVA: 0x000D988C File Offset: 0x000D7A8C
		public override void CalculateCheckBoxTextAndImageLayout(ButtonBase button, Point p, out Rectangle glyphArea, out Rectangle textRectangle, out Rectangle imageRectangle)
		{
			int num = 13;
			if (button is CheckBox)
			{
				num = (((button as CheckBox).Appearance != Appearance.Normal) ? 0 : 13);
			}
			glyphArea..ctor(0, 2, num, num);
			Rectangle clientRectangle = button.ClientRectangle;
			ContentAlignment contentAlignment = 1;
			if (button is CheckBox)
			{
				contentAlignment = (button as CheckBox).CheckAlign;
			}
			else if (button is RadioButton)
			{
				contentAlignment = (button as RadioButton).CheckAlign;
			}
			ContentAlignment contentAlignment2 = contentAlignment;
			switch (contentAlignment2)
			{
			case 1:
				clientRectangle.Width -= num;
				clientRectangle.Offset(num, 0);
				break;
			case 2:
				glyphArea.X = (button.Width - num) / 2;
				break;
			default:
				if (contentAlignment2 != 16)
				{
					if (contentAlignment2 != 32)
					{
						if (contentAlignment2 != 64)
						{
							if (contentAlignment2 != 256)
							{
								if (contentAlignment2 != 512)
								{
									if (contentAlignment2 == 1024)
									{
										glyphArea.Y = button.Height - num - 2;
										glyphArea.X = button.Width - num;
										clientRectangle.Width -= num;
									}
								}
								else
								{
									glyphArea.Y = button.Height - num;
									glyphArea.X = (button.Width - num) / 2 - 2;
								}
							}
							else
							{
								glyphArea.Y = button.Height - num - 2;
								clientRectangle.Width -= num;
								clientRectangle.Offset(num, 0);
							}
						}
						else
						{
							glyphArea.Y = (button.Height - num) / 2;
							glyphArea.X = button.Width - num;
							clientRectangle.Width -= num;
						}
					}
					else
					{
						glyphArea.Y = (button.Height - num) / 2;
						glyphArea.X = (button.Width - num) / 2;
					}
				}
				else
				{
					glyphArea.Y = (button.Height - num) / 2;
					clientRectangle.Width -= num;
					clientRectangle.Offset(num, 0);
				}
				break;
			case 4:
				glyphArea.X = button.Width - num;
				clientRectangle.Width -= num;
				break;
			}
			Image image = button.Image;
			string text = button.Text;
			Size empty = Size.Empty;
			if (!button.AutoSize)
			{
				empty.Width = button.Width - glyphArea.Width - 2;
			}
			Size size = TextRenderer.MeasureTextInternal(text, button.Font, empty, button.TextFormatFlags, button.UseCompatibleTextRendering);
			size.Height = Math.Min(size.Height, clientRectangle.Height);
			size.Width = Math.Min(size.Width, clientRectangle.Width);
			Size size2 = ((image != null) ? image.Size : Size.Empty);
			textRectangle = Rectangle.Empty;
			imageRectangle = Rectangle.Empty;
			switch (button.TextImageRelation)
			{
			case TextImageRelation.Overlay:
			{
				textRectangle.X = clientRectangle.Left + 2;
				textRectangle.Y = (clientRectangle.Height - size.Height) / 2 - 1;
				textRectangle.Size = size;
				if (image == null)
				{
					return;
				}
				int height = image.Height;
				int width = image.Width;
				contentAlignment2 = button.ImageAlign;
				int num2;
				int num3;
				switch (contentAlignment2)
				{
				case 1:
					num2 = 5;
					num3 = 5;
					break;
				case 2:
					num2 = (clientRectangle.Width - width) / 2;
					num3 = 5;
					break;
				default:
					if (contentAlignment2 != 16)
					{
						if (contentAlignment2 != 32)
						{
							if (contentAlignment2 != 64)
							{
								if (contentAlignment2 != 256)
								{
									if (contentAlignment2 != 512)
									{
										if (contentAlignment2 != 1024)
										{
											num2 = 5;
											num3 = 5;
										}
										else
										{
											num2 = clientRectangle.Width - width - 4;
											num3 = clientRectangle.Height - height - 4;
										}
									}
									else
									{
										num2 = (clientRectangle.Width - width) / 2;
										num3 = clientRectangle.Height - height - 4;
									}
								}
								else
								{
									num2 = 5;
									num3 = clientRectangle.Height - height - 4;
								}
							}
							else
							{
								num2 = clientRectangle.Width - width - 4;
								num3 = (clientRectangle.Height - height) / 2;
							}
						}
						else
						{
							num2 = (clientRectangle.Width - width) / 2;
							num3 = (clientRectangle.Height - height) / 2;
						}
					}
					else
					{
						num2 = 5;
						num3 = (clientRectangle.Height - height) / 2;
					}
					break;
				case 4:
					num2 = clientRectangle.Width - width - 5;
					num3 = 5;
					break;
				}
				imageRectangle..ctor(num2 + num, num3, width, height);
				break;
			}
			case TextImageRelation.ImageAboveText:
				clientRectangle.Inflate(-4, -4);
				this.LayoutTextAboveOrBelowImage(clientRectangle, false, size, size2, button.TextAlign, button.ImageAlign, out textRectangle, out imageRectangle);
				break;
			case TextImageRelation.TextAboveImage:
				clientRectangle.Inflate(-4, -4);
				this.LayoutTextAboveOrBelowImage(clientRectangle, true, size, size2, button.TextAlign, button.ImageAlign, out textRectangle, out imageRectangle);
				break;
			case TextImageRelation.ImageBeforeText:
				clientRectangle.Inflate(-4, -4);
				this.LayoutTextBeforeOrAfterImage(clientRectangle, false, size, size2, button.TextAlign, button.ImageAlign, out textRectangle, out imageRectangle);
				break;
			case TextImageRelation.TextBeforeImage:
				clientRectangle.Inflate(-4, -4);
				this.LayoutTextBeforeOrAfterImage(clientRectangle, true, size, size2, button.TextAlign, button.ImageAlign, out textRectangle, out imageRectangle);
				break;
			}
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x000D9E20 File Offset: 0x000D8020
		public override Size CalculateCheckBoxAutoSize(CheckBox checkBox)
		{
			Size empty = Size.Empty;
			Size size = TextRenderer.MeasureTextInternal(checkBox.Text, checkBox.Font, checkBox.UseCompatibleTextRendering);
			Size size2 = ((checkBox.Image != null) ? checkBox.Image.Size : Size.Empty);
			if (checkBox.Text.Length != 0)
			{
				size.Height += 4;
				size.Width += 4;
			}
			switch (checkBox.TextImageRelation)
			{
			case TextImageRelation.Overlay:
				empty.Height = Math.Max((checkBox.Text.Length != 0) ? size.Height : 0, size2.Height);
				empty.Width = Math.Max(size.Width, size2.Width);
				break;
			case TextImageRelation.ImageAboveText:
			case TextImageRelation.TextAboveImage:
				empty.Height = size.Height + size2.Height;
				empty.Width = Math.Max(size.Width, size2.Width);
				break;
			case TextImageRelation.ImageBeforeText:
			case TextImageRelation.TextBeforeImage:
				empty.Height = Math.Max(size.Height, size2.Height);
				empty.Width = size.Width + size2.Width;
				break;
			}
			empty.Height += checkBox.Padding.Vertical;
			empty.Width += checkBox.Padding.Horizontal + 15;
			if (empty.Height == checkBox.Padding.Vertical)
			{
				empty.Height += 14;
			}
			return empty;
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x000D9FF4 File Offset: 0x000D81F4
		public override void DrawCheckBox(Graphics dc, Rectangle clip_area, CheckBox checkbox)
		{
			int num = 13;
			int num2 = 4;
			Rectangle clientRectangle = checkbox.ClientRectangle;
			Rectangle rectangle = clientRectangle;
			Rectangle rectangle2;
			rectangle2..ctor(rectangle.X, rectangle.Y, num, num);
			StringFormat stringFormat = new StringFormat();
			stringFormat.Alignment = 0;
			stringFormat.LineAlignment = 1;
			if (checkbox.ShowKeyboardCuesInternal)
			{
				stringFormat.HotkeyPrefix = 1;
			}
			else
			{
				stringFormat.HotkeyPrefix = 2;
			}
			ContentAlignment contentAlignment;
			if (checkbox.appearance != Appearance.Button)
			{
				contentAlignment = checkbox.check_alignment;
				switch (contentAlignment)
				{
				case 1:
					rectangle2.X = clientRectangle.Left;
					rectangle.X = clientRectangle.X + num + num2;
					rectangle.Width = clientRectangle.Width - num - num2;
					break;
				case 2:
					rectangle2.X = (clientRectangle.Right - clientRectangle.Left) / 2 - num / 2;
					rectangle2.Y = clientRectangle.Top;
					rectangle.X = clientRectangle.X;
					rectangle.Width = clientRectangle.Width;
					rectangle.Y = num + num2;
					rectangle.Height = clientRectangle.Height - num - num2;
					break;
				default:
					if (contentAlignment != 16)
					{
						if (contentAlignment == 32)
						{
							rectangle2.X = (clientRectangle.Right - clientRectangle.Left) / 2 - num / 2;
							rectangle2.Y = (clientRectangle.Bottom - clientRectangle.Top) / 2 - num / 2;
							rectangle.X = clientRectangle.X;
							rectangle.Width = clientRectangle.Width;
							break;
						}
						if (contentAlignment == 64)
						{
							rectangle2.X = clientRectangle.Right - num;
							rectangle2.Y = (clientRectangle.Bottom - clientRectangle.Top) / 2 - num / 2;
							rectangle.X = clientRectangle.X;
							rectangle.Width = clientRectangle.Width - num - num2;
							break;
						}
						if (contentAlignment == 256)
						{
							rectangle2.X = clientRectangle.Left;
							rectangle2.Y = clientRectangle.Bottom - num;
							rectangle.X = clientRectangle.X + num + num2;
							rectangle.Width = clientRectangle.Width - num - num2;
							break;
						}
						if (contentAlignment == 512)
						{
							rectangle2.X = (clientRectangle.Right - clientRectangle.Left) / 2 - num / 2;
							rectangle2.Y = clientRectangle.Bottom - num;
							rectangle.X = clientRectangle.X;
							rectangle.Width = clientRectangle.Width;
							rectangle.Height = clientRectangle.Height - rectangle2.Y - num2;
							break;
						}
						if (contentAlignment == 1024)
						{
							rectangle2.X = clientRectangle.Right - num;
							rectangle2.Y = clientRectangle.Bottom - num;
							rectangle.X = clientRectangle.X;
							rectangle.Width = clientRectangle.Width - num - num2;
							break;
						}
					}
					rectangle2.X = clientRectangle.Left;
					rectangle2.Y = (clientRectangle.Bottom - clientRectangle.Top) / 2 - num / 2;
					rectangle.X = clientRectangle.X + num + num2;
					rectangle.Width = clientRectangle.Width - num - num2;
					break;
				case 4:
					rectangle2.X = clientRectangle.Right - num;
					rectangle.X = clientRectangle.X;
					rectangle.Width = clientRectangle.Width - num - num2;
					break;
				}
			}
			else
			{
				rectangle.X = clientRectangle.X;
				rectangle.Width = clientRectangle.Width;
			}
			contentAlignment = checkbox.text_alignment;
			switch (contentAlignment)
			{
			case 1:
				break;
			case 2:
				goto IL_0442;
			default:
				if (contentAlignment != 16)
				{
					if (contentAlignment == 32)
					{
						goto IL_0442;
					}
					if (contentAlignment == 64)
					{
						goto IL_044E;
					}
					if (contentAlignment != 256)
					{
						if (contentAlignment == 512)
						{
							goto IL_0442;
						}
						if (contentAlignment != 1024)
						{
							goto IL_045A;
						}
						goto IL_044E;
					}
				}
				break;
			case 4:
				goto IL_044E;
			}
			stringFormat.Alignment = 0;
			goto IL_045A;
			IL_0442:
			stringFormat.Alignment = 1;
			goto IL_045A;
			IL_044E:
			stringFormat.Alignment = 2;
			IL_045A:
			contentAlignment = checkbox.text_alignment;
			switch (contentAlignment)
			{
			case 1:
			case 2:
			case 4:
				stringFormat.LineAlignment = 0;
				break;
			default:
				if (contentAlignment != 16 && contentAlignment != 32 && contentAlignment != 64)
				{
					if (contentAlignment == 256 || contentAlignment == 512 || contentAlignment == 1024)
					{
						stringFormat.LineAlignment = 2;
					}
				}
				else
				{
					stringFormat.LineAlignment = 1;
				}
				break;
			}
			ButtonState buttonState = ButtonState.Normal;
			if (checkbox.FlatStyle == FlatStyle.Flat)
			{
				buttonState |= ButtonState.Flat;
			}
			if (checkbox.Checked)
			{
				buttonState |= ButtonState.Checked;
			}
			if (checkbox.ThreeState && checkbox.CheckState == CheckState.Indeterminate)
			{
				buttonState |= ButtonState.Checked;
				buttonState |= ButtonState.Pushed;
			}
			if (!checkbox.Enabled)
			{
				buttonState |= ButtonState.Inactive;
			}
			else if (checkbox.is_pressed)
			{
				buttonState |= ButtonState.Pushed;
			}
			this.CheckBox_DrawCheckBox(dc, checkbox, buttonState, rectangle2);
			if (checkbox.image != null || checkbox.image_list != null)
			{
				this.ButtonBase_DrawImage(checkbox, dc);
			}
			this.CheckBox_DrawText(checkbox, rectangle, dc, stringFormat);
			if (checkbox.Focused && checkbox.Enabled && checkbox.appearance != Appearance.Button && checkbox.Text != string.Empty && checkbox.ShowFocusCues)
			{
				SizeF sizeF = dc.MeasureString(checkbox.Text, checkbox.Font);
				Rectangle empty = Rectangle.Empty;
				empty.X = rectangle.X;
				empty.Y = (int)(((float)rectangle.Height - sizeF.Height) / 2f);
				empty.Size = sizeF.ToSize();
				this.CheckBox_DrawFocus(checkbox, dc, empty);
			}
			stringFormat.Dispose();
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x000DA648 File Offset: 0x000D8848
		protected virtual void CheckBox_DrawCheckBox(Graphics dc, CheckBox checkbox, ButtonState state, Rectangle checkbox_rectangle)
		{
			Brush brush = ((checkbox.BackColor.ToArgb() != this.ColorControl.ToArgb()) ? this.ResPool.GetSolidBrush(checkbox.BackColor) : SystemBrushes.Control);
			dc.FillRectangle(brush, checkbox.ClientRectangle);
			if (checkbox.appearance == Appearance.Button)
			{
				this.ButtonBase_DrawButton(checkbox, dc);
				if (checkbox.Focused && checkbox.Enabled)
				{
					this.ButtonBase_DrawFocus(checkbox, dc);
				}
			}
			else if (checkbox.FlatStyle == FlatStyle.Flat || checkbox.FlatStyle == FlatStyle.Popup)
			{
				this.DrawFlatStyleCheckBox(dc, checkbox_rectangle, checkbox);
			}
			else
			{
				this.CPDrawCheckBox(dc, checkbox_rectangle, state);
			}
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x000DA708 File Offset: 0x000D8908
		protected virtual void CheckBox_DrawText(CheckBox checkbox, Rectangle text_rectangle, Graphics dc, StringFormat text_format)
		{
			this.DrawCheckBox_and_RadioButtonText(checkbox, text_rectangle, dc, text_format, checkbox.Appearance, checkbox.Checked);
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x000DA72C File Offset: 0x000D892C
		protected virtual void CheckBox_DrawFocus(CheckBox checkbox, Graphics dc, Rectangle text_rectangle)
		{
			this.DrawInnerFocusRectangle(dc, text_rectangle, checkbox.BackColor);
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x000DA73C File Offset: 0x000D893C
		protected virtual void DrawFlatStyleCheckBox(Graphics graphics, Rectangle rectangle, CheckBox checkbox)
		{
			Rectangle rectangle2;
			Rectangle rectangle3;
			if (checkbox.FlatStyle == FlatStyle.Popup && checkbox.is_entered)
			{
				rectangle2..ctor(rectangle.X, rectangle.Y, Math.Max(rectangle.Width - 1, 0), Math.Max(rectangle.Height - 1, 0));
				rectangle3..ctor(rectangle2.X + 1, rectangle2.Y + 1, Math.Max(rectangle2.Width - 3, 0), Math.Max(rectangle2.Height - 3, 0));
			}
			else
			{
				rectangle2..ctor(rectangle.X, rectangle.Y, Math.Max(rectangle.Width - 2, 0), Math.Max(rectangle.Height - 2, 0));
				rectangle3..ctor(rectangle2.X + 1, rectangle2.Y + 1, Math.Max(rectangle2.Width - 2, 0), Math.Max(rectangle2.Height - 2, 0));
			}
			if (checkbox.Enabled)
			{
				if (checkbox.is_entered || checkbox.Capture)
				{
					if (checkbox.FlatStyle == FlatStyle.Popup && checkbox.is_entered && checkbox.Capture)
					{
						graphics.FillRectangle(this.ResPool.GetSolidBrush(checkbox.BackColor), rectangle3);
					}
					else if (checkbox.FlatStyle == FlatStyle.Flat)
					{
						if (!checkbox.is_pressed)
						{
							graphics.FillRectangle(this.ResPool.GetSolidBrush(checkbox.BackColor), rectangle3);
						}
						else
						{
							graphics.FillRectangle(this.ResPool.GetSolidBrush(ControlPaint.LightLight(checkbox.BackColor)), rectangle3);
						}
					}
					else
					{
						graphics.FillRectangle(this.ResPool.GetSolidBrush(ControlPaint.LightLight(checkbox.BackColor)), rectangle3);
					}
					if (checkbox.FlatStyle == FlatStyle.Flat)
					{
						ControlPaint.DrawBorder(graphics, rectangle2, checkbox.ForeColor, ButtonBorderStyle.Solid);
					}
					else
					{
						this.CPDrawBorder3D(graphics, rectangle2, Border3DStyle.SunkenInner, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom, checkbox.BackColor);
					}
				}
				else
				{
					graphics.FillRectangle(this.ResPool.GetSolidBrush(ControlPaint.LightLight(checkbox.BackColor)), rectangle3);
					if (checkbox.FlatStyle == FlatStyle.Flat)
					{
						ControlPaint.DrawBorder(graphics, rectangle2, checkbox.ForeColor, ButtonBorderStyle.Solid);
					}
					else
					{
						ControlPaint.DrawBorder(graphics, rectangle2, ControlPaint.DarkDark(checkbox.BackColor), ButtonBorderStyle.Solid);
					}
				}
			}
			else
			{
				if (checkbox.FlatStyle == FlatStyle.Popup)
				{
					graphics.FillRectangle(SystemBrushes.Control, rectangle3);
				}
				ControlPaint.DrawBorder(graphics, rectangle2, this.ColorControlDark, ButtonBorderStyle.Solid);
			}
			if (checkbox.Checked)
			{
				int num = Math.Max(3, rectangle3.Width / 3);
				int num2 = Math.Max(1, rectangle3.Width / 9);
				Rectangle rectangle4;
				rectangle4..ctor(rectangle3.X, rectangle3.Y + 1, rectangle3.Width, rectangle3.Height);
				Pen pen;
				if (checkbox.Enabled)
				{
					pen = this.ResPool.GetPen(checkbox.ForeColor);
				}
				else
				{
					pen = SystemPens.ControlDark;
				}
				for (int i = 0; i < num; i++)
				{
					graphics.DrawLine(pen, rectangle4.Left + num / 2, rectangle4.Top + num + i, rectangle4.Left + num / 2 + 2 * num2, rectangle4.Top + num + 2 * num2 + i);
					graphics.DrawLine(pen, rectangle4.Left + num / 2 + 2 * num2, rectangle4.Top + num + 2 * num2 + i, rectangle4.Left + num / 2 + 6 * num2, rectangle4.Top + num - 2 * num2 + i);
				}
			}
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x000DAADC File Offset: 0x000D8CDC
		private void DrawCheckBox_and_RadioButtonText(ButtonBase button_base, Rectangle text_rectangle, Graphics dc, StringFormat text_format, Appearance appearance, bool ischecked)
		{
			if (appearance == Appearance.Button)
			{
				if (ischecked || (button_base.Capture && button_base.FlatStyle != FlatStyle.Flat))
				{
					text_rectangle.X++;
					text_rectangle.Y++;
				}
				text_rectangle.Inflate(-4, -4);
			}
			if ((float)button_base.Font.Height * 1.5f > (float)text_rectangle.Height)
			{
				text_format.FormatFlags |= 4096;
			}
			if (button_base.Enabled)
			{
				dc.DrawString(button_base.Text, button_base.Font, this.ResPool.GetSolidBrush(button_base.ForeColor), text_rectangle, text_format);
			}
			else if (button_base.FlatStyle == FlatStyle.Flat || button_base.FlatStyle == FlatStyle.Popup)
			{
				dc.DrawString(button_base.Text, button_base.Font, SystemBrushes.ControlDarkDark, text_rectangle, text_format);
			}
			else
			{
				this.CPDrawStringDisabled(dc, button_base.Text, button_base.Font, button_base.BackColor, text_rectangle, text_format);
			}
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x000DAC04 File Offset: 0x000D8E04
		public override void DrawCheckedListBoxItem(CheckedListBox ctrl, DrawItemEventArgs e)
		{
			Rectangle bounds = e.Bounds;
			ButtonState buttonState;
			if ((e.State & DrawItemState.Checked) == DrawItemState.Checked)
			{
				buttonState = ButtonState.Checked;
				if ((e.State & DrawItemState.Inactive) == DrawItemState.Inactive)
				{
					buttonState |= ButtonState.Inactive;
				}
			}
			else
			{
				buttonState = ButtonState.Normal;
			}
			if (!ctrl.ThreeDCheckBoxes)
			{
				buttonState |= ButtonState.Flat;
			}
			Rectangle rectangle;
			rectangle..ctor(2, (bounds.Height - 11) / 2, 13, 13);
			ControlPaint.DrawCheckBox(e.Graphics, bounds.X + rectangle.X, bounds.Y + rectangle.Y, rectangle.Width, rectangle.Height, buttonState);
			bounds.X += rectangle.Right;
			bounds.Width -= rectangle.Right;
			Color color;
			Color color2;
			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
			{
				color = this.ColorHighlight;
				color2 = this.ColorHighlightText;
			}
			else
			{
				color = e.BackColor;
				color2 = e.ForeColor;
			}
			e.Graphics.FillRectangle(this.ResPool.GetSolidBrush(color), bounds);
			e.Graphics.DrawString(ctrl.GetItemText(ctrl.Items[e.Index]), e.Font, this.ResPool.GetSolidBrush(color2), bounds, ctrl.StringFormat);
			if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
			{
				this.CPDrawFocusRectangle(e.Graphics, bounds, color2, color);
			}
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x000DAD84 File Offset: 0x000D8F84
		public override void DrawComboBoxItem(ComboBox ctrl, DrawItemEventArgs e)
		{
			Rectangle bounds = e.Bounds;
			StringFormat stringFormat = new StringFormat();
			stringFormat.FormatFlags = 8192;
			Color color;
			Color color2;
			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
			{
				color = this.ColorHighlight;
				color2 = this.ColorHighlightText;
			}
			else
			{
				color = e.BackColor;
				color2 = e.ForeColor;
			}
			if (!ctrl.Enabled)
			{
				color2 = this.ColorInactiveCaptionText;
			}
			e.Graphics.FillRectangle(this.ResPool.GetSolidBrush(color), e.Bounds);
			if (e.Index != -1)
			{
				e.Graphics.DrawString(ctrl.GetItemText(ctrl.Items[e.Index]), e.Font, this.ResPool.GetSolidBrush(color2), bounds, stringFormat);
			}
			if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
			{
				this.CPDrawFocusRectangle(e.Graphics, e.Bounds, color2, color);
			}
			stringFormat.Dispose();
		}

		// Token: 0x060037D0 RID: 14288 RVA: 0x000DAE78 File Offset: 0x000D9078
		public override void DrawFlatStyleComboButton(Graphics graphics, Rectangle rectangle, ButtonState state)
		{
			Point[] array = new Point[3];
			Rectangle rectangle2;
			rectangle2..ctor(rectangle.X + rectangle.Width / 4, rectangle.Y + rectangle.Height / 4, rectangle.Width / 2, rectangle.Height / 2);
			int num = rectangle2.Left + rectangle2.Width / 2;
			int num2 = rectangle2.Top + rectangle2.Height / 2;
			int num3 = Math.Max(1, rectangle2.Width / 8);
			int num4 = Math.Max(1, rectangle2.Height / 8);
			if ((state & ButtonState.Pushed) != ButtonState.Normal)
			{
				num3++;
				num4++;
			}
			rectangle2.Y -= num4;
			num2 -= num4;
			Point point;
			point..ctor(rectangle2.Left + 1, num2);
			Point point2;
			point2..ctor(rectangle2.Right - 1, num2);
			Point point3;
			point3..ctor(num, rectangle2.Bottom - 1);
			array[0] = point;
			array[1] = point2;
			array[2] = point3;
			if ((state & ButtonState.Inactive) != ButtonState.Normal)
			{
				Point[] array2 = array;
				int num5 = 0;
				array2[num5].X = array2[num5].X + 1;
				Point[] array3 = array;
				int num6 = 0;
				array3[num6].Y = array3[num6].Y + 1;
				Point[] array4 = array;
				int num7 = 1;
				array4[num7].X = array4[num7].X + 1;
				Point[] array5 = array;
				int num8 = 1;
				array5[num8].Y = array5[num8].Y + 1;
				Point[] array6 = array;
				int num9 = 2;
				array6[num9].X = array6[num9].X + 1;
				Point[] array7 = array;
				int num10 = 2;
				array7[num10].Y = array7[num10].Y + 1;
				graphics.FillPolygon(SystemBrushes.ControlLightLight, array, 1);
				array[0] = point;
				array[1] = point2;
				array[2] = point3;
				graphics.FillPolygon(SystemBrushes.ControlDark, array, 1);
			}
			else
			{
				graphics.FillPolygon(SystemBrushes.ControlText, array, 1);
			}
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x000DB074 File Offset: 0x000D9274
		public override void ComboBoxDrawNormalDropDownButton(ComboBox comboBox, Graphics g, Rectangle clippingArea, Rectangle area, ButtonState state)
		{
			this.CPDrawComboButton(g, area, state);
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x000DB084 File Offset: 0x000D9284
		public override bool ComboBoxNormalDropDownButtonHasTransparentBackground(ComboBox comboBox, ButtonState state)
		{
			return true;
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x000DB088 File Offset: 0x000D9288
		public override bool ComboBoxDropDownButtonHasHotElementStyle(ComboBox comboBox)
		{
			return false;
		}

		// Token: 0x060037D4 RID: 14292 RVA: 0x000DB08C File Offset: 0x000D928C
		public override void ComboBoxDrawBackground(ComboBox comboBox, Graphics g, Rectangle clippingArea, FlatStyle style)
		{
			if (!comboBox.Enabled)
			{
				g.FillRectangle(this.ResPool.GetSolidBrush(this.ColorControl), comboBox.ClientRectangle);
			}
			if (comboBox.DropDownStyle == ComboBoxStyle.Simple)
			{
				g.FillRectangle(this.ResPool.GetSolidBrush(comboBox.Parent.BackColor), comboBox.ClientRectangle);
			}
			if (style == FlatStyle.Popup && (comboBox.Entered || comboBox.Focused))
			{
				Rectangle textArea = comboBox.TextArea;
				textArea.Height--;
				textArea.Width--;
				g.DrawRectangle(this.ResPool.GetPen(SystemColors.ControlDark), textArea);
				g.DrawLine(this.ResPool.GetPen(SystemColors.ControlDark), comboBox.ButtonArea.X - 1, comboBox.ButtonArea.Top, comboBox.ButtonArea.X - 1, comboBox.ButtonArea.Bottom);
			}
			if (style != FlatStyle.Flat && style != FlatStyle.Popup && clippingArea.IntersectsWith(comboBox.TextArea))
			{
				ControlPaint.DrawBorder3D(g, comboBox.TextArea, Border3DStyle.Sunken);
			}
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x000DB1D4 File Offset: 0x000D93D4
		public override bool CombBoxBackgroundHasHotElementStyle(ComboBox comboBox)
		{
			return false;
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x060037D6 RID: 14294 RVA: 0x000DB1D8 File Offset: 0x000D93D8
		public override int DataGridPreferredColumnWidth
		{
			get
			{
				return 75;
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x060037D7 RID: 14295 RVA: 0x000DB1DC File Offset: 0x000D93DC
		public override int DataGridMinimumColumnCheckBoxHeight
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x060037D8 RID: 14296 RVA: 0x000DB1E0 File Offset: 0x000D93E0
		public override int DataGridMinimumColumnCheckBoxWidth
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x060037D9 RID: 14297 RVA: 0x000DB1E4 File Offset: 0x000D93E4
		public override Color DataGridAlternatingBackColor
		{
			get
			{
				return this.ColorWindow;
			}
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x060037DA RID: 14298 RVA: 0x000DB1EC File Offset: 0x000D93EC
		public override Color DataGridBackColor
		{
			get
			{
				return this.ColorWindow;
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x060037DB RID: 14299 RVA: 0x000DB1F4 File Offset: 0x000D93F4
		public override Color DataGridBackgroundColor
		{
			get
			{
				return this.ColorAppWorkspace;
			}
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x060037DC RID: 14300 RVA: 0x000DB1FC File Offset: 0x000D93FC
		public override Color DataGridCaptionBackColor
		{
			get
			{
				return this.ColorActiveCaption;
			}
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x060037DD RID: 14301 RVA: 0x000DB204 File Offset: 0x000D9404
		public override Color DataGridCaptionForeColor
		{
			get
			{
				return this.ColorActiveCaptionText;
			}
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x060037DE RID: 14302 RVA: 0x000DB20C File Offset: 0x000D940C
		public override Color DataGridGridLineColor
		{
			get
			{
				return this.ColorControl;
			}
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x060037DF RID: 14303 RVA: 0x000DB214 File Offset: 0x000D9414
		public override Color DataGridHeaderBackColor
		{
			get
			{
				return this.ColorControl;
			}
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x060037E0 RID: 14304 RVA: 0x000DB21C File Offset: 0x000D941C
		public override Color DataGridHeaderForeColor
		{
			get
			{
				return this.ColorControlText;
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x060037E1 RID: 14305 RVA: 0x000DB224 File Offset: 0x000D9424
		public override Color DataGridLinkColor
		{
			get
			{
				return this.ColorHotTrack;
			}
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x060037E2 RID: 14306 RVA: 0x000DB22C File Offset: 0x000D942C
		public override Color DataGridLinkHoverColor
		{
			get
			{
				return this.ColorHotTrack;
			}
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x060037E3 RID: 14307 RVA: 0x000DB234 File Offset: 0x000D9434
		public override Color DataGridParentRowsBackColor
		{
			get
			{
				return this.ColorControl;
			}
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x060037E4 RID: 14308 RVA: 0x000DB23C File Offset: 0x000D943C
		public override Color DataGridParentRowsForeColor
		{
			get
			{
				return this.ColorWindowText;
			}
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x060037E5 RID: 14309 RVA: 0x000DB244 File Offset: 0x000D9444
		public override Color DataGridSelectionBackColor
		{
			get
			{
				return this.ColorActiveCaption;
			}
		}

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x060037E6 RID: 14310 RVA: 0x000DB24C File Offset: 0x000D944C
		public override Color DataGridSelectionForeColor
		{
			get
			{
				return this.ColorActiveCaptionText;
			}
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x000DB254 File Offset: 0x000D9454
		public override void DataGridPaint(PaintEventArgs pe, DataGrid grid)
		{
			this.DataGridPaintCaption(pe.Graphics, pe.ClipRectangle, grid);
			this.DataGridPaintParentRows(pe.Graphics, pe.ClipRectangle, grid);
			this.DataGridPaintColumnHeaders(pe.Graphics, pe.ClipRectangle, grid);
			this.DataGridPaintRows(pe.Graphics, grid.cells_area, pe.ClipRectangle, grid);
			if (grid.VScrollBar.Visible && grid.HScrollBar.Visible)
			{
				Rectangle rectangle;
				rectangle..ctor(grid.ClientRectangle.X + grid.ClientRectangle.Width - grid.VScrollBar.Width, grid.ClientRectangle.Y + grid.ClientRectangle.Height - grid.HScrollBar.Height, grid.VScrollBar.Width, grid.HScrollBar.Height);
				if (pe.ClipRectangle.IntersectsWith(rectangle))
				{
					pe.Graphics.FillRectangle(this.ResPool.GetSolidBrush(grid.ParentRowsBackColor), rectangle);
				}
			}
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x000DB378 File Offset: 0x000D9578
		public override void DataGridPaintCaption(Graphics g, Rectangle clip, DataGrid grid)
		{
			Rectangle rectangle = clip;
			rectangle.Intersect(grid.caption_area);
			g.FillRectangle(this.ResPool.GetSolidBrush(grid.CaptionBackColor), rectangle);
			g.DrawLine(this.ResPool.GetPen(grid.CurrentTableStyle.CurrentHeaderForeColor), rectangle.X, rectangle.Y + rectangle.Height - 1, rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height - 1);
			if (grid.CaptionText != string.Empty)
			{
				Rectangle caption_area = grid.caption_area;
				caption_area.Y += caption_area.Height / 2 - grid.CaptionFont.Height / 2;
				caption_area.Height = grid.CaptionFont.Height;
				g.DrawString(grid.CaptionText, grid.CaptionFont, this.ResPool.GetSolidBrush(grid.CaptionForeColor), caption_area);
			}
			if (rectangle.IntersectsWith(grid.back_button_rect))
			{
				g.DrawImage(grid.back_button_image, grid.back_button_rect);
				if (grid.back_button_mouseover)
				{
					this.CPDrawBorder3D(g, grid.back_button_rect, (!grid.back_button_active) ? Border3DStyle.Raised : Border3DStyle.Sunken, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
				}
			}
			if (rectangle.IntersectsWith(grid.parent_rows_button_rect))
			{
				g.DrawImage(grid.parent_rows_button_image, grid.parent_rows_button_rect);
				if (grid.parent_rows_button_mouseover)
				{
					this.CPDrawBorder3D(g, grid.parent_rows_button_rect, (!grid.parent_rows_button_active) ? Border3DStyle.Raised : Border3DStyle.Sunken, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
				}
			}
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x000DB520 File Offset: 0x000D9720
		public override void DataGridPaintColumnHeaders(Graphics g, Rectangle clip, DataGrid grid)
		{
			if (!grid.CurrentTableStyle.ColumnHeadersVisible)
			{
				return;
			}
			Rectangle column_headers_area = grid.column_headers_area;
			if (grid.CurrentTableStyle.CurrentRowHeadersVisible)
			{
				Rectangle column_headers_area2 = grid.column_headers_area;
				column_headers_area2.Width = grid.RowHeaderWidth;
				if (clip.IntersectsWith(column_headers_area2))
				{
					if (grid.FlatMode)
					{
						g.FillRectangle(this.ResPool.GetSolidBrush(grid.CurrentTableStyle.CurrentHeaderBackColor), column_headers_area2);
					}
					else
					{
						this.CPDrawBorder3D(g, column_headers_area2, Border3DStyle.RaisedInner, Border3DSide.All, grid.CurrentTableStyle.CurrentHeaderBackColor);
					}
				}
				column_headers_area.X += grid.RowHeaderWidth;
				column_headers_area.Width -= grid.RowHeaderWidth;
			}
			Rectangle rectangle = default(Rectangle);
			Region clip2 = g.Clip;
			rectangle.Y = column_headers_area.Y;
			rectangle.Height = column_headers_area.Height;
			int num = grid.FirstVisibleColumn + grid.VisibleColumnCount;
			for (int i = grid.FirstVisibleColumn; i < num; i++)
			{
				if (grid.CurrentTableStyle.GridColumnStyles[i].bound)
				{
					int columnStartingPixel = grid.GetColumnStartingPixel(i);
					rectangle.X = column_headers_area.X + columnStartingPixel - grid.HorizPixelOffset;
					rectangle.Width = grid.CurrentTableStyle.GridColumnStyles[i].Width;
					if (clip.IntersectsWith(rectangle))
					{
						Region region = new Region(rectangle);
						region.Intersect(column_headers_area);
						region.Intersect(clip2);
						g.Clip = region;
						this.DataGridPaintColumnHeader(g, rectangle, grid, i);
						region.Dispose();
					}
				}
			}
			g.Clip = clip2;
			Rectangle column_headers_area3 = grid.column_headers_area;
			column_headers_area3.X = ((num != 0) ? (rectangle.X + rectangle.Width) : grid.RowHeaderWidth);
			column_headers_area3.Width = grid.ClientRectangle.X + grid.ClientRectangle.Width - column_headers_area3.X;
			g.FillRectangle(this.ResPool.GetSolidBrush(grid.BackgroundColor), column_headers_area3);
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x000DB760 File Offset: 0x000D9960
		public override void DataGridPaintColumnHeader(Graphics g, Rectangle bounds, DataGrid grid, int col)
		{
			g.FillRectangle(this.ResPool.GetSolidBrush(grid.CurrentTableStyle.HeaderBackColor), bounds);
			if (!grid.FlatMode)
			{
				g.DrawLine(this.ResPool.GetPen(this.ColorControlLightLight), bounds.X, bounds.Y, bounds.X + bounds.Width, bounds.Y);
				if (col == 0)
				{
					g.DrawLine(this.ResPool.GetPen(this.ColorControlLightLight), bounds.X, bounds.Y, bounds.X, bounds.Y + bounds.Height);
				}
				else
				{
					g.DrawLine(this.ResPool.GetPen(this.ColorControlLightLight), bounds.X, bounds.Y + 2, bounds.X, bounds.Y + bounds.Height - 3);
				}
				if (col == grid.VisibleColumnCount - 1)
				{
					g.DrawLine(this.ResPool.GetPen(this.ColorControlDark), bounds.X + bounds.Width - 1, bounds.Y, bounds.X + bounds.Width - 1, bounds.Y + bounds.Height);
				}
				else
				{
					g.DrawLine(this.ResPool.GetPen(this.ColorControlDark), bounds.X + bounds.Width - 1, bounds.Y + 2, bounds.X + bounds.Width - 1, bounds.Y + bounds.Height - 3);
				}
				g.DrawLine(this.ResPool.GetPen(this.ColorControlDark), bounds.X, bounds.Y + bounds.Height - 1, bounds.X + bounds.Width, bounds.Y + bounds.Height - 1);
			}
			bounds.X += 2;
			bounds.Width -= 2;
			DataGridColumnStyle dataGridColumnStyle = grid.CurrentTableStyle.GridColumnStyles[col];
			if (dataGridColumnStyle.ArrowDrawingMode != DataGridColumnStyle.ArrowDrawing.No)
			{
				bounds.Width -= 16;
			}
			StringFormat stringFormat = new StringFormat();
			stringFormat.FormatFlags |= 4096;
			stringFormat.LineAlignment = 1;
			stringFormat.Trimming = 1;
			g.DrawString(dataGridColumnStyle.HeaderText, grid.CurrentTableStyle.HeaderFont, this.ResPool.GetSolidBrush(grid.CurrentTableStyle.CurrentHeaderForeColor), bounds, stringFormat);
			if (dataGridColumnStyle.ArrowDrawingMode != DataGridColumnStyle.ArrowDrawing.No)
			{
				Point point;
				point..ctor(bounds.X + bounds.Width + 4, bounds.Y + (bounds.Height - 6) / 2);
				if (dataGridColumnStyle.ArrowDrawingMode == DataGridColumnStyle.ArrowDrawing.Ascending)
				{
					g.DrawLine(SystemPens.ControlLightLight, point.X + 6, point.Y + 6, point.X + 3, point.Y);
					g.DrawLine(SystemPens.ControlDark, point.X, point.Y + 6, point.X + 6, point.Y + 6);
					g.DrawLine(SystemPens.ControlDark, point.X, point.Y + 6, point.X + 3, point.Y);
				}
				else
				{
					g.DrawLine(SystemPens.ControlLightLight, point.X + 6, point.Y, point.X + 3, point.Y + 6);
					g.DrawLine(SystemPens.ControlDark, point.X, point.Y, point.X + 6, point.Y);
					g.DrawLine(SystemPens.ControlDark, point.X, point.Y, point.X + 3, point.Y + 6);
				}
			}
		}

		// Token: 0x060037EB RID: 14315 RVA: 0x000DBB50 File Offset: 0x000D9D50
		public override void DataGridPaintParentRows(Graphics g, Rectangle clip, DataGrid grid)
		{
			Rectangle rectangle = default(Rectangle);
			rectangle.X = grid.ParentRowsArea.X;
			rectangle.Width = grid.ParentRowsArea.Width;
			rectangle.Height = grid.CaptionFont.Height + 3;
			object[] array = grid.data_source_stack.ToArray();
			Region clip2 = g.Clip;
			for (int i = 0; i < array.Length; i++)
			{
				rectangle.Y = grid.ParentRowsArea.Y + i * rectangle.Height;
				if (clip.IntersectsWith(rectangle))
				{
					Region region = new Region(rectangle);
					region.Intersect(clip2);
					g.Clip = region;
					this.DataGridPaintParentRow(g, rectangle, (DataGridDataSource)array[array.Length - i - 1], grid);
					region.Dispose();
				}
			}
			g.Clip = clip2;
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x000DBC40 File Offset: 0x000D9E40
		public override void DataGridPaintParentRow(Graphics g, Rectangle bounds, DataGridDataSource row, DataGrid grid)
		{
			g.FillRectangle(this.ResPool.GetSolidBrush(grid.ParentRowsBackColor), bounds);
			Font font = new Font(grid.Font.FontFamily, grid.Font.Size, grid.Font.Style | 1);
			StringFormat stringFormat = new StringFormat();
			stringFormat.LineAlignment = 1;
			stringFormat.Alignment = 0;
			string text = string.Empty;
			if (row.view is DataRowView)
			{
				text = ((DataRowView)row.view).DataView.GetListName(null) + ": ";
			}
			Size size = g.MeasureString(text, font).ToSize();
			Rectangle rectangle;
			rectangle..ctor(new Point(bounds.X + 3, bounds.Y + bounds.Height - size.Height), size);
			g.DrawString(text, font, this.ResPool.GetSolidBrush(grid.ParentRowsForeColor), rectangle, stringFormat);
			foreach (object obj in ((ICustomTypeDescriptor)row.view).GetProperties())
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (!typeof(IBindingList).IsAssignableFrom(propertyDescriptor.PropertyType))
				{
					rectangle.X += rectangle.Size.Width + 5;
					string text2 = string.Format("{0}: {1}", propertyDescriptor.Name, propertyDescriptor.GetValue(row.view));
					rectangle.Size = g.MeasureString(text2, grid.Font).ToSize();
					rectangle.Y = bounds.Y + bounds.Height - rectangle.Height;
					g.DrawString(text2, grid.Font, this.ResPool.GetSolidBrush(grid.ParentRowsForeColor), rectangle, stringFormat);
				}
			}
			if (!grid.FlatMode)
			{
				this.CPDrawBorder3D(g, bounds, Border3DStyle.RaisedInner, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
			}
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x000DBE88 File Offset: 0x000DA088
		public override void DataGridPaintRowHeaderArrow(Graphics g, Rectangle bounds, DataGrid grid)
		{
			Point[] array = new Point[3];
			Rectangle rectangle;
			rectangle..ctor(bounds.X + bounds.Width / 4, bounds.Y + bounds.Height / 4, bounds.Width / 2, bounds.Height / 2);
			int num = rectangle.Left + rectangle.Width / 2;
			int num2 = rectangle.Top + rectangle.Height / 2;
			int num3 = Math.Max(1, rectangle.Width / 8);
			rectangle.X -= num3;
			num -= num3;
			Point point;
			point..ctor(num, rectangle.Top - 1);
			Point point2;
			point2..ctor(num, rectangle.Bottom);
			Point point3;
			point3..ctor(rectangle.Right, num2);
			array[0] = point;
			array[1] = point2;
			array[2] = point3;
			g.FillPolygon(this.ResPool.GetSolidBrush(grid.CurrentTableStyle.CurrentHeaderForeColor), array, 1);
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x000DBF9C File Offset: 0x000DA19C
		public override void DataGridPaintRowHeaderStar(Graphics g, Rectangle bounds, DataGrid grid)
		{
			int num = bounds.X + 4;
			int num2 = bounds.Y + 3;
			Pen pen = this.ResPool.GetPen(grid.CurrentTableStyle.CurrentHeaderForeColor);
			g.DrawLine(pen, num + 4, num2, num + 4, num2 + 8);
			g.DrawLine(pen, num, num2 + 4, num + 8, num2 + 4);
			g.DrawLine(pen, num + 1, num2 + 1, num + 7, num2 + 7);
			g.DrawLine(pen, num + 7, num2 + 1, num + 1, num2 + 7);
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x000DC01C File Offset: 0x000DA21C
		public override void DataGridPaintRowHeader(Graphics g, Rectangle bounds, int row, DataGrid grid)
		{
			bool flag = grid.ShowEditRow && row == grid.DataGridRows.Length - 1;
			bool flag2 = row == grid.CurrentCell.RowNumber;
			g.FillRectangle(this.ResPool.GetSolidBrush(grid.CurrentTableStyle.CurrentHeaderBackColor), bounds);
			if (flag2)
			{
				if (grid.IsChanging)
				{
					g.DrawString("...", grid.Font, this.ResPool.GetSolidBrush(grid.CurrentTableStyle.CurrentHeaderForeColor), bounds);
				}
				else
				{
					Rectangle rectangle;
					rectangle..ctor(bounds.X - 2, bounds.Y, 18, 18);
					this.DataGridPaintRowHeaderArrow(g, rectangle, grid);
				}
			}
			else if (flag)
			{
				this.DataGridPaintRowHeaderStar(g, bounds, grid);
			}
			if (!grid.FlatMode && !flag)
			{
				this.CPDrawBorder3D(g, bounds, Border3DStyle.RaisedInner, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
			}
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x000DC118 File Offset: 0x000DA318
		public override void DataGridPaintRows(Graphics g, Rectangle cells, Rectangle clip, DataGrid grid)
		{
			Rectangle rectangle = default(Rectangle);
			Rectangle rectangle2 = default(Rectangle);
			int visibleRowCount = grid.VisibleRowCount;
			bool flag = false;
			if (grid.RowsCount < grid.DataGridRows.Length && grid.FirstVisibleRow + grid.VisibleRowCount >= grid.DataGridRows.Length)
			{
				flag = true;
			}
			rectangle.Width = cells.Width + grid.RowHeadersArea.Width;
			for (int i = 0; i < visibleRowCount; i++)
			{
				int num = grid.FirstVisibleRow + i;
				if (num == grid.DataGridRows.Length - 1)
				{
					rectangle.Height = grid.DataGridRows[num].Height;
				}
				else
				{
					rectangle.Height = grid.DataGridRows[num + 1].VerticalOffset - grid.DataGridRows[num].VerticalOffset;
				}
				rectangle.Y = cells.Y + grid.DataGridRows[num].VerticalOffset - grid.DataGridRows[grid.FirstVisibleRow].VerticalOffset;
				if (clip.IntersectsWith(rectangle))
				{
					if (grid.CurrentTableStyle.HasRelations && (!flag || num != grid.DataGridRows.Length - 1))
					{
						this.DataGridPaintRelationRow(g, num, rectangle, false, clip, grid);
					}
					else
					{
						this.DataGridPaintRow(g, num, rectangle, flag && num == grid.DataGridRows.Length - 1, clip, grid);
					}
				}
			}
			rectangle2.X = 0;
			if (visibleRowCount == 0)
			{
				rectangle2.Y = cells.Y;
			}
			else
			{
				rectangle2.Y = rectangle.Y + rectangle.Height;
			}
			rectangle2.Height = cells.Y + cells.Height - rectangle.Y - rectangle.Height;
			rectangle2.Width = cells.Width + grid.RowHeadersArea.Width;
			g.FillRectangle(this.ResPool.GetSolidBrush(grid.BackgroundColor), rectangle2);
		}

		// Token: 0x060037F1 RID: 14321 RVA: 0x000DC340 File Offset: 0x000DA540
		public override void DataGridPaintRelationRow(Graphics g, int row, Rectangle row_rect, bool is_newrow, Rectangle clip, DataGrid grid)
		{
			Rectangle rectangle = default(Rectangle);
			Pen pen = ThemeEngine.Current.ResPool.GetPen(grid.CurrentTableStyle.ForeColor);
			if (grid.CurrentTableStyle.CurrentRowHeadersVisible)
			{
				Rectangle rectangle2 = row_rect;
				rectangle2.Width = grid.RowHeaderWidth;
				row_rect.X += grid.RowHeaderWidth;
				if (clip.IntersectsWith(rectangle2))
				{
					this.DataGridPaintRowHeader(g, rectangle2, row, grid);
				}
				rectangle = rectangle2;
				rectangle.X += rectangle.Width / 2;
				rectangle.Y += 3;
				rectangle.Width = 8;
				rectangle.Height = 8;
				g.DrawRectangle(pen, rectangle);
				g.DrawLine(pen, rectangle.X + 2, rectangle.Y + rectangle.Height / 2, rectangle.X + rectangle.Width - 2, rectangle.Y + rectangle.Height / 2);
				if (!grid.IsExpanded(row))
				{
					g.DrawLine(pen, rectangle.X + rectangle.Width / 2, rectangle.Y + 2, rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height - 2);
				}
			}
			Rectangle rectangle3 = row_rect;
			if (grid.DataGridRows[row].IsExpanded)
			{
				rectangle3.Height -= grid.DataGridRows[row].RelationHeight;
			}
			this.DataGridPaintRowContents(g, row, rectangle3, is_newrow, clip, grid);
			if (grid.DataGridRows[row].IsExpanded)
			{
				string[] relations = grid.CurrentTableStyle.Relations;
				StringBuilder stringBuilder = new StringBuilder(string.Empty);
				for (int i = 0; i < relations.Length; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append("\n");
					}
					stringBuilder.Append(relations[i]);
				}
				string text = stringBuilder.ToString();
				StringFormat stringFormat = new StringFormat();
				stringFormat.FormatFlags |= 4096;
				Rectangle rectangle4 = row_rect;
				rectangle4.X = rectangle3.X + grid.GetColumnStartingPixel(grid.FirstVisibleColumn) - grid.HorizPixelOffset;
				rectangle4.Y += rectangle3.Height;
				rectangle4.Height = grid.DataGridRows[row].RelationHeight;
				rectangle4.Width = 0;
				int num = grid.FirstVisibleColumn + grid.VisibleColumnCount;
				for (int j = grid.FirstVisibleColumn; j < num; j++)
				{
					if (grid.CurrentTableStyle.GridColumnStyles[j].bound)
					{
						rectangle4.Width += grid.CurrentTableStyle.GridColumnStyles[j].Width;
					}
				}
				rectangle4.Width = Math.Max(rectangle4.Width, grid.DataGridRows[row].relation_area.Width);
				g.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(grid.CurrentTableStyle.BackColor), rectangle4);
				Rectangle relation_area = grid.DataGridRows[row].relation_area;
				relation_area.Y = rectangle4.Y;
				relation_area.Height--;
				g.DrawLine(pen, rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height, rectangle.X + rectangle.Width / 2, relation_area.Y + relation_area.Height / 2);
				g.DrawLine(pen, rectangle.X + rectangle.Width / 2, relation_area.Y + relation_area.Height / 2, relation_area.X, relation_area.Y + relation_area.Height / 2);
				g.DrawRectangle(pen, relation_area);
				g.DrawString(text, grid.LinkFont, this.ResPool.GetSolidBrush(grid.LinkColor), relation_area, stringFormat);
				if (row_rect.X + row_rect.Width > rectangle4.X + rectangle4.Width)
				{
					Rectangle rectangle5 = default(Rectangle);
					rectangle5.X = rectangle4.X + rectangle4.Width;
					rectangle5.Width = row_rect.X + row_rect.Width - rectangle4.X - rectangle4.Width;
					rectangle5.Y = row_rect.Y;
					rectangle5.Height = row_rect.Height;
					if (clip.IntersectsWith(rectangle5))
					{
						g.FillRectangle(this.ResPool.GetSolidBrush(grid.BackgroundColor), rectangle5);
					}
				}
			}
		}

		// Token: 0x060037F2 RID: 14322 RVA: 0x000DC80C File Offset: 0x000DAA0C
		public override void DataGridPaintRowContents(Graphics g, int row, Rectangle row_rect, bool is_newrow, Rectangle clip, DataGrid grid)
		{
			Rectangle rectangle = default(Rectangle);
			Rectangle rectangle2 = Rectangle.Empty;
			rectangle.Y = row_rect.Y;
			rectangle.Height = row_rect.Height;
			Color color;
			Color color2;
			if (grid.IsSelected(row))
			{
				color = grid.SelectionBackColor;
				color2 = grid.SelectionForeColor;
			}
			else
			{
				if (row % 2 == 0)
				{
					color = grid.BackColor;
				}
				else
				{
					color = grid.AlternatingBackColor;
				}
				color2 = grid.ForeColor;
			}
			Brush solidBrush = this.ResPool.GetSolidBrush(color);
			Brush solidBrush2 = this.ResPool.GetSolidBrush(color2);
			int num = grid.FirstVisibleColumn + grid.VisibleColumnCount;
			DataGridCell currentCell = grid.CurrentCell;
			if (num > 0)
			{
				Region clip2 = g.Clip;
				for (int i = grid.FirstVisibleColumn; i < num; i++)
				{
					if (grid.CurrentTableStyle.GridColumnStyles[i].bound)
					{
						int columnStartingPixel = grid.GetColumnStartingPixel(i);
						rectangle.X = row_rect.X + columnStartingPixel - grid.HorizPixelOffset;
						rectangle.Width = grid.CurrentTableStyle.GridColumnStyles[i].Width;
						if (clip.IntersectsWith(rectangle))
						{
							Region region = new Region(rectangle);
							region.Intersect(row_rect);
							region.Intersect(clip2);
							g.Clip = region;
							Brush brush = solidBrush;
							Brush brush2 = solidBrush2;
							if (grid.is_editing && i == currentCell.ColumnNumber && row == currentCell.RowNumber)
							{
								brush = this.ResPool.GetSolidBrush(grid.BackColor);
								brush2 = this.ResPool.GetSolidBrush(grid.ForeColor);
							}
							if (is_newrow)
							{
								grid.CurrentTableStyle.GridColumnStyles[i].PaintNewRow(g, rectangle, brush, brush2);
							}
							else
							{
								grid.CurrentTableStyle.GridColumnStyles[i].Paint(g, rectangle, grid.ListManager, row, brush, brush2, grid.RightToLeft == RightToLeft.Yes);
							}
							region.Dispose();
						}
					}
				}
				g.Clip = clip2;
				if (row_rect.X + row_rect.Width > rectangle.X + rectangle.Width)
				{
					rectangle2.X = rectangle.X + rectangle.Width;
					rectangle2.Width = row_rect.X + row_rect.Width - rectangle.X - rectangle.Width;
					rectangle2.Y = row_rect.Y;
					rectangle2.Height = row_rect.Height;
				}
			}
			else
			{
				rectangle2 = row_rect;
			}
			if (!rectangle2.IsEmpty && clip.IntersectsWith(rectangle2))
			{
				g.FillRectangle(this.ResPool.GetSolidBrush(grid.BackgroundColor), rectangle2);
			}
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x000DCAF8 File Offset: 0x000DACF8
		public override void DataGridPaintRow(Graphics g, int row, Rectangle row_rect, bool is_newrow, Rectangle clip, DataGrid grid)
		{
			if (grid.CurrentTableStyle.CurrentRowHeadersVisible)
			{
				Rectangle rectangle = row_rect;
				rectangle.Width = grid.RowHeaderWidth;
				row_rect.X += grid.RowHeaderWidth;
				if (clip.IntersectsWith(rectangle))
				{
					this.DataGridPaintRowHeader(g, rectangle, row, grid);
				}
			}
			this.DataGridPaintRowContents(g, row, row_rect, is_newrow, clip, grid);
		}

		// Token: 0x060037F4 RID: 14324 RVA: 0x000DCB64 File Offset: 0x000DAD64
		public override bool DataGridViewRowHeaderCellDrawBackground(DataGridViewRowHeaderCell cell, Graphics g, Rectangle bounds)
		{
			return false;
		}

		// Token: 0x060037F5 RID: 14325 RVA: 0x000DCB68 File Offset: 0x000DAD68
		public override bool DataGridViewRowHeaderCellDrawSelectionBackground(DataGridViewRowHeaderCell cell)
		{
			return false;
		}

		// Token: 0x060037F6 RID: 14326 RVA: 0x000DCB6C File Offset: 0x000DAD6C
		public override bool DataGridViewRowHeaderCellDrawBorder(DataGridViewRowHeaderCell cell, Graphics g, Rectangle bounds)
		{
			return false;
		}

		// Token: 0x060037F7 RID: 14327 RVA: 0x000DCB70 File Offset: 0x000DAD70
		public override bool DataGridViewColumnHeaderCellDrawBackground(DataGridViewColumnHeaderCell cell, Graphics g, Rectangle bounds)
		{
			return false;
		}

		// Token: 0x060037F8 RID: 14328 RVA: 0x000DCB74 File Offset: 0x000DAD74
		public override bool DataGridViewColumnHeaderCellDrawBorder(DataGridViewColumnHeaderCell cell, Graphics g, Rectangle bounds)
		{
			return false;
		}

		// Token: 0x060037F9 RID: 14329 RVA: 0x000DCB78 File Offset: 0x000DAD78
		public override bool DataGridViewHeaderCellHasPressedStyle(DataGridView dataGridView)
		{
			return false;
		}

		// Token: 0x060037FA RID: 14330 RVA: 0x000DCB7C File Offset: 0x000DAD7C
		public override bool DataGridViewHeaderCellHasHotStyle(DataGridView dataGridView)
		{
			return false;
		}

		// Token: 0x060037FB RID: 14331 RVA: 0x000DCB80 File Offset: 0x000DAD80
		protected virtual void DateTimePickerDrawBorder(DateTimePicker dateTimePicker, Graphics g, Rectangle clippingArea)
		{
			this.CPDrawBorder3D(g, dateTimePicker.ClientRectangle, Border3DStyle.Sunken, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom, dateTimePicker.BackColor);
		}

		// Token: 0x060037FC RID: 14332 RVA: 0x000DCBA4 File Offset: 0x000DADA4
		protected virtual void DateTimePickerDrawDropDownButton(DateTimePicker dateTimePicker, Graphics g, Rectangle clippingArea)
		{
			ButtonState buttonState = ((!dateTimePicker.is_drop_down_visible) ? ButtonState.Normal : ButtonState.Pushed);
			g.FillRectangle(this.ResPool.GetSolidBrush(this.ColorControl), dateTimePicker.drop_down_arrow_rect);
			this.CPDrawComboButton(g, dateTimePicker.drop_down_arrow_rect, buttonState);
		}

		// Token: 0x060037FD RID: 14333 RVA: 0x000DCBF4 File Offset: 0x000DADF4
		public override void DrawDateTimePicker(Graphics dc, Rectangle clip_rectangle, DateTimePicker dtp)
		{
			if (!clip_rectangle.IntersectsWith(dtp.ClientRectangle))
			{
				return;
			}
			Rectangle clientRectangle = dtp.ClientRectangle;
			this.DateTimePickerDrawBorder(dtp, dc, clip_rectangle);
			if (clip_rectangle.IntersectsWith(dtp.drop_down_arrow_rect))
			{
				clientRectangle.Inflate(-2, -2);
				if (!dtp.ShowUpDown)
				{
					this.DateTimePickerDrawDropDownButton(dtp, dc, clip_rectangle);
				}
				else
				{
					ButtonState buttonState = ((!dtp.is_up_pressed) ? ButtonState.Normal : ButtonState.Pushed);
					ButtonState buttonState2 = ((!dtp.is_down_pressed) ? ButtonState.Normal : ButtonState.Pushed);
					Rectangle drop_down_arrow_rect = dtp.drop_down_arrow_rect;
					Rectangle drop_down_arrow_rect2 = dtp.drop_down_arrow_rect;
					drop_down_arrow_rect.Height /= 2;
					drop_down_arrow_rect2.Y = drop_down_arrow_rect.Height;
					drop_down_arrow_rect2.Height = dtp.Height - drop_down_arrow_rect.Height;
					if (drop_down_arrow_rect2.Height > drop_down_arrow_rect.Height)
					{
						drop_down_arrow_rect2.Y++;
						drop_down_arrow_rect2.Height--;
					}
					drop_down_arrow_rect.Inflate(-1, -1);
					drop_down_arrow_rect2.Inflate(-1, -1);
					ControlPaint.DrawScrollButton(dc, drop_down_arrow_rect, ScrollButton.Min, buttonState);
					ControlPaint.DrawScrollButton(dc, drop_down_arrow_rect2, ScrollButton.Down, buttonState2);
				}
			}
			if (!clip_rectangle.IntersectsWith(dtp.date_area_rect))
			{
				return;
			}
			dc.FillRectangle(SystemBrushes.Window, dtp.date_area_rect);
			Rectangle date_area_rect = dtp.date_area_rect;
			if (dtp.ShowCheckBox)
			{
				Rectangle checkBoxRect = dtp.CheckBoxRect;
				date_area_rect.X = date_area_rect.X + checkBoxRect.Width + 8;
				date_area_rect.Width = date_area_rect.Width - checkBoxRect.Width - 8;
				ButtonState buttonState3 = ((!dtp.Checked) ? ButtonState.Normal : ButtonState.Checked);
				this.CPDrawCheckBox(dc, checkBoxRect, buttonState3);
				if (dtp.is_checkbox_selected)
				{
					this.CPDrawFocusRectangle(dc, checkBoxRect, dtp.foreground_color, dtp.background_color);
				}
			}
			using (StringFormat genericTypographic = StringFormat.GenericTypographic)
			{
				genericTypographic.LineAlignment = 0;
				genericTypographic.Alignment = 0;
				genericTypographic.FormatFlags = genericTypographic.FormatFlags | 2048 | 4096 | 4;
				genericTypographic.FormatFlags &= -16385;
				if (dtp.part_data.Length > 0 && dtp.part_data[0].drawing_rectangle.IsEmpty)
				{
					for (int i = 0; i < dtp.part_data.Length; i++)
					{
						DateTimePicker.PartData partData = dtp.part_data[i];
						RectangleF rectangleF = default(RectangleF);
						string text = partData.GetText(dtp.Value);
						rectangleF.Size = dc.MeasureString(text, dtp.Font, 250, genericTypographic);
						if (!partData.is_literal)
						{
							rectangleF.Width = Math.Max(dtp.CalculateMaxWidth(partData.value, dc, genericTypographic), rectangleF.Width);
						}
						if (i > 0)
						{
							rectangleF.X = dtp.part_data[i - 1].drawing_rectangle.Right;
						}
						else
						{
							rectangleF.X = (float)date_area_rect.X;
						}
						rectangleF.Y = 2f;
						rectangleF.Inflate(1f, 0f);
						partData.drawing_rectangle = rectangleF;
					}
				}
				Brush solidBrush = this.ResPool.GetSolidBrush((!dtp.ShowCheckBox || dtp.Checked) ? dtp.ForeColor : SystemColors.GrayText);
				RectangleF rectangleF2 = clip_rectangle;
				for (int j = 0; j < dtp.part_data.Length; j++)
				{
					DateTimePicker.PartData partData2 = dtp.part_data[j];
					if (rectangleF2.IntersectsWith(partData2.drawing_rectangle))
					{
						string text2 = ((dtp.editing_part_index != j) ? partData2.GetText(dtp.Value) : dtp.editing_text);
						PointF pointF = default(PointF);
						SizeF sizeF = dc.MeasureString(text2, dtp.Font, 250, genericTypographic);
						pointF.X = partData2.drawing_rectangle.Left + partData2.drawing_rectangle.Width / 2f - sizeF.Width / 2f;
						pointF.Y = partData2.drawing_rectangle.Top + partData2.drawing_rectangle.Height / 2f - sizeF.Height / 2f;
						RectangleF rectangleF3;
						rectangleF3..ctor(pointF, sizeF);
						rectangleF3 = RectangleF.Intersect(rectangleF3, date_area_rect);
						if (rectangleF3.IsEmpty)
						{
							break;
						}
						if (rectangleF3.Right >= (float)date_area_rect.Right)
						{
							genericTypographic.FormatFlags &= -16385;
						}
						else
						{
							genericTypographic.FormatFlags |= 16384;
						}
						if (partData2.Selected)
						{
							dc.FillRectangle(SystemBrushes.Highlight, rectangleF3);
							dc.DrawString(text2, dtp.Font, SystemBrushes.HighlightText, rectangleF3, genericTypographic);
						}
						else
						{
							dc.DrawString(text2, dtp.Font, solidBrush, rectangleF3, genericTypographic);
						}
						if (partData2.drawing_rectangle.Right > (float)date_area_rect.Right)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x060037FE RID: 14334 RVA: 0x000DD158 File Offset: 0x000DB358
		public override bool DateTimePickerBorderHasHotElementStyle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060037FF RID: 14335 RVA: 0x000DD15C File Offset: 0x000DB35C
		public override Rectangle DateTimePickerGetDropDownButtonArea(DateTimePicker dateTimePicker)
		{
			Rectangle clientRectangle = dateTimePicker.ClientRectangle;
			clientRectangle.X = clientRectangle.Right - SystemInformation.VerticalScrollBarWidth - 2;
			if (clientRectangle.Width > SystemInformation.VerticalScrollBarWidth + 2)
			{
				clientRectangle.Width = SystemInformation.VerticalScrollBarWidth;
			}
			else
			{
				clientRectangle.Width = Math.Max(clientRectangle.Width - 2, 0);
			}
			clientRectangle.Inflate(0, -2);
			return clientRectangle;
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x000DD1CC File Offset: 0x000DB3CC
		public override Rectangle DateTimePickerGetDateArea(DateTimePicker dateTimePicker)
		{
			Rectangle clientRectangle = dateTimePicker.ClientRectangle;
			if (dateTimePicker.ShowUpDown)
			{
				if (clientRectangle.Width > 17)
				{
					clientRectangle.Width -= 17;
				}
				else
				{
					clientRectangle.Width = 0;
				}
			}
			else if (clientRectangle.Width > SystemInformation.VerticalScrollBarWidth + 4)
			{
				clientRectangle.Width -= SystemInformation.VerticalScrollBarWidth;
			}
			else
			{
				clientRectangle.Width = 0;
			}
			clientRectangle.Inflate(-2, -2);
			return clientRectangle;
		}

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06003801 RID: 14337 RVA: 0x000DD25C File Offset: 0x000DB45C
		public override bool DateTimePickerDropDownButtonHasHotElementStyle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x000DD260 File Offset: 0x000DB460
		public override void DrawGroupBox(Graphics dc, Rectangle area, GroupBox box)
		{
			dc.FillRectangle(this.GetControlBackBrush(box.BackColor), box.ClientRectangle);
			StringFormat stringFormat = new StringFormat();
			stringFormat.HotkeyPrefix = 1;
			SizeF sizeF = dc.MeasureString(box.Text, box.Font);
			int num = 0;
			if (sizeF.Width > 0f)
			{
				num = (int)sizeF.Width + 7;
				if (num > box.Width - 16)
				{
					num = box.Width - 16;
				}
			}
			int num2 = box.Font.Height / 2;
			Region clip = dc.Clip;
			dc.SetClip(new Rectangle(10, 0, num, box.Font.Height), 4);
			this.CPDrawBorder3D(dc, new Rectangle(0, num2, box.Width, box.Height - num2), Border3DStyle.Etched, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom, box.BackColor);
			dc.Clip = clip;
			if (box.Text.Length != 0)
			{
				if (box.Enabled)
				{
					dc.DrawString(box.Text, box.Font, this.ResPool.GetSolidBrush(box.ForeColor), 10f, 0f, stringFormat);
				}
				else
				{
					this.CPDrawStringDisabled(dc, box.Text, box.Font, box.BackColor, new RectangleF(10f, 0f, (float)num, (float)box.Font.Height), stringFormat);
				}
			}
			stringFormat.Dispose();
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06003803 RID: 14339 RVA: 0x000DD3C8 File Offset: 0x000DB5C8
		public override Size GroupBoxDefaultSize
		{
			get
			{
				return new Size(200, 100);
			}
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06003804 RID: 14340 RVA: 0x000DD3D8 File Offset: 0x000DB5D8
		public override Size HScrollBarDefaultSize
		{
			get
			{
				return new Size(80, this.ScrollBarButtonSize);
			}
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x000DD3E8 File Offset: 0x000DB5E8
		public override void DrawListBoxItem(ListBox ctrl, DrawItemEventArgs e)
		{
			Color color;
			Color color2;
			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
			{
				color = this.ColorHighlight;
				color2 = this.ColorHighlightText;
			}
			else
			{
				color = e.BackColor;
				color2 = e.ForeColor;
			}
			e.Graphics.FillRectangle(this.ResPool.GetSolidBrush(color), e.Bounds);
			e.Graphics.DrawString(ctrl.GetItemText(ctrl.Items[e.Index]), e.Font, this.ResPool.GetSolidBrush(color2), e.Bounds, ctrl.StringFormat);
			if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
			{
				this.CPDrawFocusRectangle(e.Graphics, e.Bounds, color2, color);
			}
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x000DD4AC File Offset: 0x000DB6AC
		public override void DrawListViewItems(Graphics dc, Rectangle clip, ListView control)
		{
			bool flag = control.View == View.Details;
			int firstVisibleIndex = control.FirstVisibleIndex;
			int lastVisibleIndex = control.LastVisibleIndex;
			if (control.VirtualMode)
			{
				control.OnCacheVirtualItems(new CacheVirtualItemsEventArgs(firstVisibleIndex, lastVisibleIndex));
			}
			for (int i = firstVisibleIndex; i <= lastVisibleIndex; i++)
			{
				ListViewItem itemAtDisplayIndex = control.GetItemAtDisplayIndex(i);
				if (clip.IntersectsWith(itemAtDisplayIndex.Bounds))
				{
					bool flag2 = false;
					if (control.OwnerDraw)
					{
						flag2 = this.DrawListViewItemOwnerDraw(dc, itemAtDisplayIndex, i);
					}
					if (!flag2)
					{
						this.DrawListViewItem(dc, control, itemAtDisplayIndex);
						if (control.View == View.Details)
						{
							this.DrawListViewSubItems(dc, control, itemAtDisplayIndex);
						}
					}
				}
			}
			if (control.UsingGroups)
			{
				for (int j = 0; j < control.Groups.InternalCount; j++)
				{
					ListViewGroup internalGroup = control.Groups.GetInternalGroup(j);
					if (internalGroup.ItemCount > 0 && clip.IntersectsWith(internalGroup.HeaderBounds))
					{
						this.DrawListViewGroupHeader(dc, control, internalGroup);
					}
				}
			}
			ListViewInsertionMark insertionMark = control.InsertionMark;
			int index = insertionMark.Index;
			if (Application.VisualStylesEnabled && insertionMark.Bounds != Rectangle.Empty && control.View != View.Details && control.View != View.List && index > -1 && index < control.Items.Count)
			{
				Brush solidBrush = this.ResPool.GetSolidBrush(insertionMark.Color);
				dc.FillRectangle(solidBrush, insertionMark.Line);
				dc.FillPolygon(solidBrush, insertionMark.TopTriangle);
				dc.FillPolygon(solidBrush, insertionMark.BottomTriangle);
			}
			if (flag && control.GridLines && !control.UsingGroups)
			{
				Size clientSize = control.ClientSize;
				int num = ((control.HeaderStyle != ColumnHeaderStyle.None) ? control.header_control.Height : 0);
				foreach (object obj in control.Columns)
				{
					ColumnHeader columnHeader = (ColumnHeader)obj;
					int num2 = columnHeader.Rect.Right - control.h_marker;
					dc.DrawLine(SystemPens.Control, num2, num, num2, clientSize.Height);
				}
				int num3 = control.ItemSize.Height;
				if (num3 == 0)
				{
					num3 = control.Font.Height + 2;
				}
				for (int k = num + num3 - control.v_marker % num3; k < clientSize.Height; k += num3)
				{
					dc.DrawLine(SystemPens.Control, 0, k, clientSize.Width, k);
				}
			}
			if (control.h_scroll.Visible && control.v_scroll.Visible)
			{
				Rectangle rectangle = default(Rectangle);
				rectangle.X = control.h_scroll.Location.X + control.h_scroll.Width;
				rectangle.Width = control.v_scroll.Width;
				rectangle.Y = control.v_scroll.Location.Y + control.v_scroll.Height;
				rectangle.Height = control.h_scroll.Height;
				dc.FillRectangle(SystemBrushes.Control, rectangle);
			}
			Rectangle boxSelectRectangle = control.item_control.BoxSelectRectangle;
			if (!boxSelectRectangle.Size.IsEmpty)
			{
				dc.DrawRectangle(this.ResPool.GetDashPen(this.ColorControlText, 2), boxSelectRectangle);
			}
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x000DD884 File Offset: 0x000DBA84
		public override void DrawListViewHeader(Graphics dc, Rectangle clip, ListView control)
		{
			bool flag = control.View == View.Details;
			if (flag && control.HeaderStyle != ColumnHeaderStyle.None)
			{
				dc.FillRectangle(SystemBrushes.Control, 0, 0, control.TotalWidth, control.Font.Height + 5);
				if (control.Columns.Count > 0)
				{
					foreach (object obj in control.Columns)
					{
						ColumnHeader columnHeader = (ColumnHeader)obj;
						Rectangle rect = columnHeader.Rect;
						rect.X -= control.h_marker;
						bool flag2 = false;
						if (control.OwnerDraw)
						{
							flag2 = this.DrawListViewColumnHeaderOwnerDraw(dc, control, columnHeader, rect);
						}
						if (!flag2)
						{
							this.ListViewDrawColumnHeaderBackground(control, columnHeader, dc, rect, clip);
							rect.X += 5;
							rect.Width -= 10;
							if (rect.Width > 0)
							{
								int num;
								if (control.SmallImageList == null)
								{
									num = -1;
								}
								else
								{
									num = ((!(columnHeader.ImageKey == string.Empty)) ? control.SmallImageList.Images.IndexOfKey(columnHeader.ImageKey) : columnHeader.ImageIndex);
								}
								if (num > -1 && num < control.SmallImageList.Images.Count)
								{
									int num2 = control.SmallImageList.ImageSize.Width + 5;
									int num3 = (int)dc.MeasureString(columnHeader.Text, control.Font).Width;
									int num4 = rect.X;
									switch (columnHeader.TextAlign)
									{
									case HorizontalAlignment.Right:
										num4 = rect.Right - (num3 + num2);
										break;
									case HorizontalAlignment.Center:
										num4 = (rect.Width - (num3 + num2)) / 2 + rect.X;
										break;
									}
									if (num4 < rect.X)
									{
										num4 = rect.X;
									}
									control.SmallImageList.Draw(dc, new Point(num4, rect.Y), num);
									rect.X += num2;
									rect.Width -= num2;
								}
								dc.DrawString(columnHeader.Text, control.Font, SystemBrushes.ControlText, rect, columnHeader.Format);
							}
						}
					}
					int num5 = control.GetReorderedColumn(control.Columns.Count - 1).Rect.Right - control.h_marker;
					if (num5 < control.Right)
					{
						Rectangle rect2 = control.Columns[0].Rect;
						rect2.X = num5;
						rect2.Width = control.Right - num5;
						this.ListViewDrawUnusedHeaderBackground(control, dc, rect2, clip);
					}
				}
			}
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x000DDB9C File Offset: 0x000DBD9C
		protected virtual void ListViewDrawColumnHeaderBackground(ListView listView, ColumnHeader columnHeader, Graphics g, Rectangle area, Rectangle clippingArea)
		{
			ButtonState buttonState;
			if (listView.HeaderStyle == ColumnHeaderStyle.Clickable)
			{
				buttonState = ((!columnHeader.Pressed) ? ButtonState.Normal : ButtonState.Pushed);
			}
			else
			{
				buttonState = ButtonState.Flat;
			}
			this.CPDrawButton(g, area, buttonState);
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x000DDBE4 File Offset: 0x000DBDE4
		protected virtual void ListViewDrawUnusedHeaderBackground(ListView listView, Graphics g, Rectangle area, Rectangle clippingArea)
		{
			ButtonState buttonState;
			if (listView.HeaderStyle == ColumnHeaderStyle.Clickable)
			{
				buttonState = ButtonState.Normal;
			}
			else
			{
				buttonState = ButtonState.Flat;
			}
			this.CPDrawButton(g, area, buttonState);
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x000DDC14 File Offset: 0x000DBE14
		public override void DrawListViewHeaderDragDetails(Graphics dc, ListView view, ColumnHeader col, int target_x)
		{
			Rectangle rect = col.Rect;
			rect.X -= view.h_marker;
			Color color = Color.FromArgb(127, (int)this.ColorControlDark.R, (int)this.ColorControlDark.G, (int)this.ColorControlDark.B);
			dc.FillRectangle(this.ResPool.GetSolidBrush(color), rect);
			rect.X += 3;
			rect.Width -= 8;
			if (rect.Width <= 0)
			{
				return;
			}
			color = Color.FromArgb(127, (int)this.ColorControlText.R, (int)this.ColorControlText.G, (int)this.ColorControlText.B);
			dc.DrawString(col.Text, view.Font, this.ResPool.GetSolidBrush(color), rect, col.Format);
			dc.DrawLine(this.ResPool.GetSizedPen(this.ColorHighlight, 2), target_x, 0, target_x, col.Rect.Height);
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x000DDD3C File Offset: 0x000DBF3C
		protected virtual bool DrawListViewColumnHeaderOwnerDraw(Graphics dc, ListView control, ColumnHeader column, Rectangle bounds)
		{
			ListViewItemStates listViewItemStates = ListViewItemStates.ShowKeyboardCues;
			if (column.Pressed)
			{
				listViewItemStates |= ListViewItemStates.Selected;
			}
			DrawListViewColumnHeaderEventArgs drawListViewColumnHeaderEventArgs = new DrawListViewColumnHeaderEventArgs(dc, bounds, column.Index, column, listViewItemStates, SystemColors.ControlText, ThemeEngine.Current.ColorControl, this.DefaultFont);
			control.OnDrawColumnHeader(drawListViewColumnHeaderEventArgs);
			return !drawListViewColumnHeaderEventArgs.DrawDefault;
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x000DDD94 File Offset: 0x000DBF94
		protected virtual bool DrawListViewItemOwnerDraw(Graphics dc, ListViewItem item, int index)
		{
			ListViewItemStates listViewItemStates = ListViewItemStates.ShowKeyboardCues;
			if (item.Selected)
			{
				listViewItemStates |= ListViewItemStates.Selected;
			}
			if (item.Focused)
			{
				listViewItemStates |= ListViewItemStates.Focused;
			}
			DrawListViewItemEventArgs drawListViewItemEventArgs = new DrawListViewItemEventArgs(dc, item, item.Bounds, index, listViewItemStates);
			item.ListView.OnDrawItem(drawListViewItemEventArgs);
			if (drawListViewItemEventArgs.DrawDefault)
			{
				return false;
			}
			if (item.ListView.View == View.Details)
			{
				int num = Math.Min(item.ListView.Columns.Count, item.SubItems.Count);
				for (int i = 0; i < num; i++)
				{
					if (!this.DrawListViewSubItemOwnerDraw(dc, item, listViewItemStates, i))
					{
						if (i == 0)
						{
							this.DrawListViewItem(dc, item.ListView, item);
						}
						else
						{
							this.DrawListViewSubItem(dc, item.ListView, item, i);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x000DDE6C File Offset: 0x000DC06C
		protected virtual void DrawListViewItem(Graphics dc, ListView control, ListViewItem item)
		{
			Rectangle checkRectReal = item.CheckRectReal;
			Rectangle bounds = item.GetBounds(ItemBoundsPortion.Icon);
			Rectangle bounds2 = item.GetBounds(ItemBoundsPortion.Entire);
			Rectangle bounds3 = item.GetBounds(ItemBoundsPortion.Label);
			if (control.CheckBoxes && control.View != View.Tile)
			{
				if (control.StateImageList == null)
				{
					int num = Math.Max(3, checkRectReal.Width / 6);
					int num2 = Math.Max(1, checkRectReal.Width / 12);
					dc.FillRectangle(SystemBrushes.Window, checkRectReal);
					Rectangle rectangle;
					rectangle..ctor(checkRectReal.X + 2, checkRectReal.Y + 2, checkRectReal.Width - 4, checkRectReal.Height - 4);
					Pen sizedPen = this.ResPool.GetSizedPen(this.ColorWindowText, 2);
					dc.DrawRectangle(sizedPen, rectangle);
					if (item.Checked)
					{
						Pen sizedPen2 = this.ResPool.GetSizedPen(this.ColorWindowText, 1);
						rectangle.X++;
						rectangle.Y++;
						int num3 = rectangle.Width / 5;
						int num4 = rectangle.Height / 3;
						for (int i = 0; i < num; i++)
						{
							dc.DrawLine(sizedPen2, rectangle.Left + num3, rectangle.Top + num4 + i, rectangle.Left + num3 + 2 * num2, rectangle.Top + num4 + 2 * num2 + i);
							dc.DrawLine(sizedPen2, rectangle.Left + num3 + 2 * num2, rectangle.Top + num4 + 2 * num2 + i, rectangle.Left + num3 + 6 * num2, rectangle.Top + num4 - 2 * num2 + i);
						}
					}
				}
				else
				{
					int num5;
					if (item.Checked)
					{
						num5 = ((control.StateImageList.Images.Count <= 1) ? (-1) : 1);
					}
					else
					{
						num5 = ((control.StateImageList.Images.Count <= 0) ? (-1) : 0);
					}
					if (num5 > -1)
					{
						control.StateImageList.Draw(dc, checkRectReal.Location, num5);
					}
				}
			}
			ImageList imageList = ((control.View != View.LargeIcon && control.View != View.Tile) ? control.SmallImageList : control.LargeImageList);
			if (imageList != null)
			{
				int num6;
				if (item.ImageKey != string.Empty)
				{
					num6 = imageList.Images.IndexOfKey(item.ImageKey);
				}
				else
				{
					num6 = item.ImageIndex;
				}
				if (num6 > -1 && num6 < imageList.Images.Count)
				{
					imageList.Draw(dc, bounds.Location, num6);
				}
			}
			StringFormat stringFormat = new StringFormat();
			if (control.View == View.SmallIcon || control.View == View.LargeIcon)
			{
				stringFormat.LineAlignment = 0;
			}
			else
			{
				stringFormat.LineAlignment = 1;
			}
			if (control.View == View.LargeIcon)
			{
				stringFormat.Alignment = 1;
			}
			else
			{
				stringFormat.Alignment = 0;
			}
			if (control.LabelWrap && control.View != View.Details && control.View != View.Tile)
			{
				stringFormat.FormatFlags = 8192;
			}
			else
			{
				stringFormat.FormatFlags = 4096;
			}
			if ((control.View == View.LargeIcon && !item.Focused) || control.View == View.Details || control.View == View.Tile)
			{
				stringFormat.Trimming = 3;
			}
			Rectangle rectangle2 = bounds3;
			if (control.View == View.Details)
			{
				Size size = Size.Ceiling(dc.MeasureString(item.Text, item.Font));
				if (!control.FullRowSelect)
				{
					rectangle2.Width = Math.Min(size.Width + 4, bounds3.Width);
				}
			}
			if (item.Selected && control.Focused)
			{
				dc.FillRectangle(SystemBrushes.Highlight, rectangle2);
			}
			else if (item.Selected && !control.HideSelection)
			{
				dc.FillRectangle(SystemBrushes.Control, rectangle2);
			}
			else
			{
				dc.FillRectangle(this.ResPool.GetSolidBrush(item.BackColor), bounds3);
			}
			Brush brush = (control.Enabled ? ((!item.Selected || !control.Focused) ? this.ResPool.GetSolidBrush(item.ForeColor) : SystemBrushes.HighlightText) : SystemBrushes.ControlLight);
			if (control.View == View.Tile && Application.VisualStylesEnabled)
			{
				dc.DrawString(item.Text, item.Font, brush, item.SubItems[0].Bounds, stringFormat);
				int num7 = Math.Min(control.Columns.Count, item.SubItems.Count);
				for (int j = 1; j < num7; j++)
				{
					ListViewItem.ListViewSubItem listViewSubItem = item.SubItems[j];
					if (listViewSubItem.Text != null && listViewSubItem.Text.Length != 0)
					{
						Brush brush2 = ((!item.Selected || !control.Focused) ? this.GetControlForeBrush(listViewSubItem.ForeColor) : SystemBrushes.HighlightText);
						dc.DrawString(listViewSubItem.Text, listViewSubItem.Font, brush2, listViewSubItem.Bounds, stringFormat);
					}
				}
			}
			else if (item.Text != null && item.Text.Length > 0)
			{
				Font font = item.Font;
				if (control.HotTracking && item.Hot)
				{
					font = item.HotFont;
				}
				if (item.Selected && control.Focused)
				{
					dc.DrawString(item.Text, font, brush, rectangle2, stringFormat);
				}
				else
				{
					dc.DrawString(item.Text, font, brush, bounds3, stringFormat);
				}
			}
			if (item.Focused && control.Focused)
			{
				Rectangle rectangle3 = rectangle2;
				if (control.FullRowSelect && control.View == View.Details)
				{
					int num8 = 0;
					foreach (object obj in control.Columns)
					{
						ColumnHeader columnHeader = (ColumnHeader)obj;
						num8 += columnHeader.Width;
					}
					rectangle3..ctor(0, bounds2.Y, num8, bounds2.Height);
				}
				if (control.ShowFocusCues)
				{
					if (item.Selected)
					{
						this.CPDrawFocusRectangle(dc, rectangle3, this.ColorHighlightText, this.ColorHighlight);
					}
					else
					{
						this.CPDrawFocusRectangle(dc, rectangle3, control.ForeColor, control.BackColor);
					}
				}
			}
			stringFormat.Dispose();
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x000DE588 File Offset: 0x000DC788
		protected virtual void DrawListViewSubItems(Graphics dc, ListView control, ListViewItem item)
		{
			int count = control.Columns.Count;
			int num = Math.Min(item.SubItems.Count, count);
			for (int i = 1; i < num; i++)
			{
				this.DrawListViewSubItem(dc, control, item, i);
			}
			Rectangle bounds = item.GetBounds(ItemBoundsPortion.Label);
			if (item.Selected && (control.Focused || !control.HideSelection) && control.FullRowSelect)
			{
				for (int j = num; j < count; j++)
				{
					ColumnHeader columnHeader = control.Columns[j];
					bounds.X = columnHeader.Rect.X - control.h_marker;
					bounds.Width = columnHeader.Wd;
					dc.FillRectangle((!control.Focused) ? SystemBrushes.Control : SystemBrushes.Highlight, bounds);
				}
			}
		}

		// Token: 0x0600380F RID: 14351 RVA: 0x000DE678 File Offset: 0x000DC878
		protected virtual void DrawListViewSubItem(Graphics dc, ListView control, ListViewItem item, int index)
		{
			ListViewItem.ListViewSubItem listViewSubItem = item.SubItems[index];
			ColumnHeader columnHeader = control.Columns[index];
			StringFormat stringFormat = new StringFormat();
			stringFormat.Alignment = columnHeader.Format.Alignment;
			stringFormat.LineAlignment = 1;
			stringFormat.FormatFlags = 4096;
			stringFormat.Trimming = 3;
			Rectangle bounds = listViewSubItem.Bounds;
			Rectangle rectangle = bounds;
			rectangle.X += 3;
			rectangle.Width -= this.ListViewItemPaddingWidth;
			SolidBrush solidBrush;
			SolidBrush solidBrush2;
			Font font;
			if (item.UseItemStyleForSubItems)
			{
				solidBrush = this.ResPool.GetSolidBrush(item.BackColor);
				solidBrush2 = this.ResPool.GetSolidBrush(item.ForeColor);
				if (control.HotTracking && item.Hot)
				{
					font = item.HotFont;
				}
				else
				{
					font = item.Font;
				}
			}
			else
			{
				solidBrush = this.ResPool.GetSolidBrush(listViewSubItem.BackColor);
				solidBrush2 = this.ResPool.GetSolidBrush(listViewSubItem.ForeColor);
				font = listViewSubItem.Font;
			}
			if (item.Selected && (control.Focused || !control.HideSelection) && control.FullRowSelect)
			{
				Brush brush;
				Brush brush2;
				if (control.Focused)
				{
					brush = SystemBrushes.Highlight;
					brush2 = SystemBrushes.HighlightText;
				}
				else
				{
					brush = SystemBrushes.Control;
					brush2 = solidBrush2;
				}
				dc.FillRectangle(brush, bounds);
				if (listViewSubItem.Text != null && listViewSubItem.Text.Length > 0)
				{
					dc.DrawString(listViewSubItem.Text, font, brush2, rectangle, stringFormat);
				}
			}
			else
			{
				dc.FillRectangle(solidBrush, bounds);
				if (listViewSubItem.Text != null && listViewSubItem.Text.Length > 0)
				{
					dc.DrawString(listViewSubItem.Text, font, solidBrush2, rectangle, stringFormat);
				}
			}
			stringFormat.Dispose();
		}

		// Token: 0x06003810 RID: 14352 RVA: 0x000DE870 File Offset: 0x000DCA70
		protected virtual bool DrawListViewSubItemOwnerDraw(Graphics dc, ListViewItem item, ListViewItemStates state, int index)
		{
			ListView listView = item.ListView;
			ListViewItem.ListViewSubItem listViewSubItem = item.SubItems[index];
			DrawListViewSubItemEventArgs drawListViewSubItemEventArgs = new DrawListViewSubItemEventArgs(dc, listViewSubItem.Bounds, item, listViewSubItem, item.Index, index, listView.Columns[index], state);
			listView.OnDrawSubItem(drawListViewSubItemEventArgs);
			return !drawListViewSubItemEventArgs.DrawDefault;
		}

		// Token: 0x06003811 RID: 14353 RVA: 0x000DE8C8 File Offset: 0x000DCAC8
		protected virtual void DrawListViewGroupHeader(Graphics dc, ListView control, ListViewGroup group)
		{
			Rectangle headerBounds = group.HeaderBounds;
			Rectangle headerBounds2 = group.HeaderBounds;
			headerBounds.Offset(8, 0);
			headerBounds.Inflate(-8, 0);
			int num = control.Font.Height + 2;
			Font font = new Font(control.Font, control.Font.Style | 1);
			Brush brush = new LinearGradientBrush(new Point(headerBounds2.Left, 0), new Point(headerBounds2.Left + this.ListViewGroupLineWidth, 0), SystemColors.Desktop, Color.White);
			Pen pen = new Pen(brush);
			StringFormat stringFormat = new StringFormat();
			switch (group.HeaderAlignment)
			{
			case HorizontalAlignment.Left:
				stringFormat.Alignment = 0;
				break;
			case HorizontalAlignment.Right:
				stringFormat.Alignment = 2;
				break;
			case HorizontalAlignment.Center:
				stringFormat.Alignment = 1;
				break;
			}
			stringFormat.LineAlignment = 0;
			dc.DrawString(group.Header, font, SystemBrushes.ControlText, headerBounds, stringFormat);
			dc.DrawLine(pen, headerBounds2.Left, headerBounds2.Top + num, headerBounds2.Left + this.ListViewGroupLineWidth, headerBounds2.Top + num);
			stringFormat.Dispose();
			font.Dispose();
			pen.Dispose();
			brush.Dispose();
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06003812 RID: 14354 RVA: 0x000DEA14 File Offset: 0x000DCC14
		public override bool ListViewHasHotHeaderStyle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x000DEA18 File Offset: 0x000DCC18
		public override int ListViewGetHeaderHeight(ListView listView, Font font)
		{
			return ThemeWin32Classic.ListViewGetHeaderHeight(font);
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x000DEA20 File Offset: 0x000DCC20
		private static int ListViewGetHeaderHeight(Font font)
		{
			return font.Height + 5;
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x000DEA2C File Offset: 0x000DCC2C
		public static int ListViewGetHeaderHeight()
		{
			return ThemeWin32Classic.ListViewGetHeaderHeight(ThemeEngine.Current.DefaultFont);
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06003816 RID: 14358 RVA: 0x000DEA40 File Offset: 0x000DCC40
		public override Size ListViewCheckBoxSize
		{
			get
			{
				return new Size(16, 16);
			}
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06003817 RID: 14359 RVA: 0x000DEA4C File Offset: 0x000DCC4C
		public override int ListViewColumnHeaderHeight
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06003818 RID: 14360 RVA: 0x000DEA50 File Offset: 0x000DCC50
		public override int ListViewDefaultColumnWidth
		{
			get
			{
				return 60;
			}
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06003819 RID: 14361 RVA: 0x000DEA54 File Offset: 0x000DCC54
		public override int ListViewVerticalSpacing
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x0600381A RID: 14362 RVA: 0x000DEA58 File Offset: 0x000DCC58
		public override int ListViewEmptyColumnWidth
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x0600381B RID: 14363 RVA: 0x000DEA5C File Offset: 0x000DCC5C
		public override int ListViewHorizontalSpacing
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x0600381C RID: 14364 RVA: 0x000DEA60 File Offset: 0x000DCC60
		public override int ListViewItemPaddingWidth
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x0600381D RID: 14365 RVA: 0x000DEA64 File Offset: 0x000DCC64
		public override Size ListViewDefaultSize
		{
			get
			{
				return new Size(121, 97);
			}
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x0600381E RID: 14366 RVA: 0x000DEA70 File Offset: 0x000DCC70
		public override int ListViewGroupHeight
		{
			get
			{
				return 20;
			}
		}

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x0600381F RID: 14367 RVA: 0x000DEA74 File Offset: 0x000DCC74
		public int ListViewGroupLineWidth
		{
			get
			{
				return 200;
			}
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06003820 RID: 14368 RVA: 0x000DEA7C File Offset: 0x000DCC7C
		public override int ListViewTileWidthFactor
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06003821 RID: 14369 RVA: 0x000DEA80 File Offset: 0x000DCC80
		public override int ListViewTileHeightFactor
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x000DEA84 File Offset: 0x000DCC84
		public override void CalcItemSize(Graphics dc, MenuItem item, int y, int x, bool menuBar)
		{
			item.X = x;
			item.Y = y;
			if (!item.Visible)
			{
				item.Width = 0;
				item.Height = 0;
				return;
			}
			if (item.Separator)
			{
				item.Height = 6;
				item.Width = 20;
				return;
			}
			if (item.MeasureEventDefined)
			{
				MeasureItemEventArgs measureItemEventArgs = new MeasureItemEventArgs(dc, item.Index);
				item.PerformMeasureItem(measureItemEventArgs);
				item.Height = measureItemEventArgs.ItemHeight;
				item.Width = measureItemEventArgs.ItemWidth;
				return;
			}
			SizeF sizeF = dc.MeasureString(item.Text, this.MenuFont, int.MaxValue, ThemeWin32Classic.string_format_menu_text);
			item.Width = (int)sizeF.Width;
			item.Height = (int)sizeF.Height;
			if (!menuBar)
			{
				if (item.Shortcut != Shortcut.None && item.ShowShortcut)
				{
					item.XTab = this.MenuCheckSize.Width + 8 + (int)sizeF.Width;
					sizeF = dc.MeasureString(" " + item.GetShortCutText(), this.MenuFont);
					item.Width += 8 + (int)sizeF.Width;
				}
				item.Width += 4 + this.MenuCheckSize.Width * 2;
			}
			else
			{
				item.Width += 8;
				x += item.Width;
			}
			if (item.Height < this.MenuHeight)
			{
				item.Height = this.MenuHeight;
			}
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x000DEC10 File Offset: 0x000DCE10
		public override int CalcMenuBarSize(Graphics dc, Menu menu, int width)
		{
			int num = 0;
			int num2 = 0;
			menu.Height = 0;
			foreach (object obj in menu.MenuItems)
			{
				MenuItem menuItem = (MenuItem)obj;
				this.CalcItemSize(dc, menuItem, num2, num, true);
				if (num + menuItem.Width > width)
				{
					menuItem.X = 0;
					num2 += menuItem.Height;
					menuItem.Y = num2;
					num = 0;
				}
				num += menuItem.Width;
				menuItem.MenuBar = true;
				if (num2 + menuItem.Height > menu.Height)
				{
					menu.Height = menuItem.Height + num2;
				}
			}
			menu.Width = width;
			return menu.Height;
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x000DECF8 File Offset: 0x000DCEF8
		public override void CalcPopupMenuSize(Graphics dc, Menu menu)
		{
			int num = 3;
			int i = 0;
			menu.Height = 0;
			while (i < menu.MenuItems.Count)
			{
				int num2 = 3;
				int num3 = 0;
				int j;
				for (j = i; j < menu.MenuItems.Count; j++)
				{
					MenuItem menuItem = menu.MenuItems[j];
					if (j != i && (menuItem.Break || menuItem.BarBreak))
					{
						break;
					}
					this.CalcItemSize(dc, menuItem, num2, num, false);
					num2 += menuItem.Height;
					if (menuItem.Width > num3)
					{
						num3 = menuItem.Width;
					}
				}
				int k = i;
				while (k < j)
				{
					menu.MenuItems[k].Width = num3;
					k++;
					i++;
				}
				if (num2 > menu.Height)
				{
					menu.Height = num2;
				}
				num += num3;
			}
			menu.Width = num;
			menu.Width += 2;
			menu.Height += 2;
			menu.Width++;
			menu.Height++;
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x000DEE2C File Offset: 0x000DD02C
		public override void DrawMenuBar(Graphics dc, Menu menu, Rectangle rect)
		{
			if (menu.Height == 0)
			{
				this.CalcMenuBarSize(dc, menu, rect.Width);
			}
			bool hotkey_active = (menu as MainMenu).tracker.hotkey_active;
			HotkeyPrefix hotkeyPrefix = ((!this.MenuAccessKeysUnderlined && !hotkey_active) ? 2 : 1);
			ThemeWin32Classic.string_format_menu_menubar_text.HotkeyPrefix = hotkeyPrefix;
			ThemeWin32Classic.string_format_menu_text.HotkeyPrefix = hotkeyPrefix;
			rect.Height = menu.Height;
			dc.FillRectangle(SystemBrushes.Menu, rect);
			for (int i = 0; i < menu.MenuItems.Count; i++)
			{
				MenuItem menuItem = menu.MenuItems[i];
				Rectangle bounds = menuItem.bounds;
				bounds.X += rect.X;
				bounds.Y += rect.Y;
				menuItem.MenuHeight = menu.Height;
				menuItem.PerformDrawItem(new DrawItemEventArgs(dc, this.MenuFont, bounds, i, menuItem.Status));
			}
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x000DEF30 File Offset: 0x000DD130
		protected Bitmap CreateGlyphBitmap(Size size, MenuGlyph glyph, Color color)
		{
			Color color2;
			if (color.R == 0 && color.G == 0 && color.B == 0)
			{
				color2 = Color.White;
			}
			else
			{
				color2 = Color.Black;
			}
			Bitmap bitmap = new Bitmap(size.Width, size.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			Rectangle rectangle;
			rectangle..ctor(Point.Empty, size);
			graphics.FillRectangle(this.ResPool.GetSolidBrush(color2), rectangle);
			this.CPDrawMenuGlyph(graphics, rectangle, glyph, color, Color.Empty);
			bitmap.MakeTransparent(color2);
			graphics.Dispose();
			return bitmap;
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x000DEFCC File Offset: 0x000DD1CC
		public override void DrawMenuItem(MenuItem item, DrawItemEventArgs e)
		{
			Rectangle bounds = e.Bounds;
			if (!item.Visible)
			{
				return;
			}
			StringFormat stringFormat;
			if (item.MenuBar)
			{
				stringFormat = ThemeWin32Classic.string_format_menu_menubar_text;
			}
			else
			{
				stringFormat = ThemeWin32Classic.string_format_menu_text;
			}
			if (item.Separator)
			{
				int num = e.Bounds.Y + e.Bounds.Height / 2;
				e.Graphics.DrawLine(SystemPens.ControlDark, e.Bounds.X, num, e.Bounds.X + e.Bounds.Width, num);
				e.Graphics.DrawLine(SystemPens.ControlLight, e.Bounds.X, num + 1, e.Bounds.X + e.Bounds.Width, num + 1);
				return;
			}
			if (!item.MenuBar)
			{
				bounds.X += this.MenuCheckSize.Width;
			}
			if (item.BarBreak)
			{
				Rectangle bounds2 = e.Bounds;
				bounds2.Y++;
				bounds2.Width = 3;
				bounds2.Height = item.MenuHeight - 6;
				e.Graphics.DrawLine(SystemPens.ControlDark, bounds2.X, bounds2.Y, bounds2.X, bounds2.Y + bounds2.Height);
				e.Graphics.DrawLine(SystemPens.ControlLight, bounds2.X + 1, bounds2.Y, bounds2.X + 1, bounds2.Y + bounds2.Height);
			}
			Color color;
			Color color2;
			Brush brush;
			Brush brush2;
			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected && !item.MenuBar)
			{
				color = this.ColorHighlightText;
				color2 = this.ColorHighlight;
				brush = SystemBrushes.HighlightText;
				brush2 = SystemBrushes.Highlight;
			}
			else
			{
				color = this.ColorMenuText;
				color2 = this.ColorMenu;
				brush = this.ResPool.GetSolidBrush(this.ColorMenuText);
				brush2 = SystemBrushes.Menu;
			}
			if (!item.MenuBar)
			{
				e.Graphics.FillRectangle(brush2, e.Bounds);
			}
			if (item.Enabled)
			{
				e.Graphics.DrawString(item.Text, e.Font, brush, bounds, stringFormat);
				if (item.MenuBar)
				{
					Border3DStyle border3DStyle = Border3DStyle.Adjust;
					if ((item.Status & DrawItemState.HotLight) != DrawItemState.None)
					{
						border3DStyle = Border3DStyle.RaisedInner;
					}
					else if ((item.Status & DrawItemState.Selected) != DrawItemState.None)
					{
						border3DStyle = Border3DStyle.SunkenOuter;
					}
					if (border3DStyle != Border3DStyle.Adjust)
					{
						this.CPDrawBorder3D(e.Graphics, e.Bounds, border3DStyle, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom, this.ColorMenu);
					}
				}
			}
			else
			{
				if ((item.Status & DrawItemState.Selected) != DrawItemState.Selected)
				{
					e.Graphics.DrawString(item.Text, e.Font, Brushes.White, new RectangleF((float)(bounds.X + 1), (float)(bounds.Y + 1), (float)bounds.Width, (float)bounds.Height), stringFormat);
				}
				e.Graphics.DrawString(item.Text, e.Font, this.ResPool.GetSolidBrush(this.ColorGrayText), bounds, stringFormat);
			}
			if (!item.MenuBar && item.Shortcut != Shortcut.None && item.ShowShortcut)
			{
				string shortCutText = item.GetShortCutText();
				Rectangle rectangle = bounds;
				rectangle.X = item.XTab;
				rectangle.Width -= item.XTab;
				if (item.Enabled)
				{
					e.Graphics.DrawString(shortCutText, e.Font, brush, rectangle, ThemeWin32Classic.string_format_menu_shortcut);
				}
				else
				{
					if ((item.Status & DrawItemState.Selected) != DrawItemState.Selected)
					{
						e.Graphics.DrawString(shortCutText, e.Font, Brushes.White, new RectangleF((float)(rectangle.X + 1), (float)(rectangle.Y + 1), (float)rectangle.Width, (float)bounds.Height), ThemeWin32Classic.string_format_menu_shortcut);
					}
					e.Graphics.DrawString(shortCutText, e.Font, this.ResPool.GetSolidBrush(this.ColorGrayText), rectangle, ThemeWin32Classic.string_format_menu_shortcut);
				}
			}
			if (!item.MenuBar && (item.IsPopup || item.MdiList))
			{
				int width = this.MenuCheckSize.Width;
				int height = this.MenuCheckSize.Height;
				Bitmap bitmap = this.CreateGlyphBitmap(new Size(width, height), MenuGlyph.Arrow, color);
				if (item.Enabled)
				{
					e.Graphics.DrawImage(bitmap, e.Bounds.X + e.Bounds.Width - width, e.Bounds.Y + (e.Bounds.Height - height) / 2);
				}
				else
				{
					ControlPaint.DrawImageDisabled(e.Graphics, bitmap, e.Bounds.X + e.Bounds.Width - width, e.Bounds.Y + (e.Bounds.Height - height) / 2, color2);
				}
				bitmap.Dispose();
			}
			if (!item.MenuBar && item.Checked)
			{
				Rectangle bounds3 = e.Bounds;
				int width2 = this.MenuCheckSize.Width;
				int height2 = this.MenuCheckSize.Height;
				Bitmap bitmap2 = this.CreateGlyphBitmap(new Size(width2, height2), (!item.RadioCheck) ? MenuGlyph.Checkmark : MenuGlyph.Bullet, color);
				e.Graphics.DrawImage(bitmap2, bounds3.X, e.Bounds.Y + (e.Bounds.Height - height2) / 2);
				bitmap2.Dispose();
			}
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x000DF5E0 File Offset: 0x000DD7E0
		public override void DrawPopupMenu(Graphics dc, Menu menu, Rectangle cliparea, Rectangle rect)
		{
			dc.FillRectangle(SystemBrushes.Menu, cliparea);
			this.CPDrawBorder3D(dc, rect, Border3DStyle.Raised, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
			for (int i = 0; i < menu.MenuItems.Count; i++)
			{
				if (cliparea.IntersectsWith(menu.MenuItems[i].bounds))
				{
					MenuItem menuItem = menu.MenuItems[i];
					menuItem.MenuHeight = menu.Height;
					menuItem.PerformDrawItem(new DrawItemEventArgs(dc, this.MenuFont, menuItem.bounds, i, menuItem.Status));
				}
			}
		}

		// Token: 0x06003829 RID: 14377 RVA: 0x000DF678 File Offset: 0x000DD878
		public override void DrawMonthCalendar(Graphics dc, Rectangle clip_rectangle, MonthCalendar mc)
		{
			Rectangle clientRectangle = mc.ClientRectangle;
			Size singleMonthSize = mc.SingleMonthSize;
			Size size = (Size)mc.calendar_spacing;
			Size size2 = (Size)mc.date_cell_size;
			int num = 1;
			int num2 = 1;
			for (int i = 0; i < mc.CalendarDimensions.Height; i++)
			{
				if (i > 0)
				{
					num2 += singleMonthSize.Height + size.Height;
				}
				for (int j = 0; j < mc.CalendarDimensions.Width; j++)
				{
					if (j > 0)
					{
						num += singleMonthSize.Width + size.Width;
					}
					else
					{
						num = 1;
					}
					Rectangle rectangle;
					rectangle..ctor(num, num2, singleMonthSize.Width, singleMonthSize.Height);
					if (rectangle.IntersectsWith(clip_rectangle))
					{
						this.DrawSingleMonth(dc, clip_rectangle, rectangle, mc, i, j);
					}
				}
			}
			Rectangle rectangle2;
			rectangle2..ctor(clientRectangle.X, Math.Max(clientRectangle.Bottom - size2.Height - 3, 0), clientRectangle.Width, size2.Height + 2);
			if (mc.ShowToday && rectangle2.IntersectsWith(clip_rectangle))
			{
				dc.FillRectangle(this.GetControlBackBrush(mc.BackColor), rectangle2);
				if (mc.ShowToday)
				{
					int num3 = 5;
					if (mc.ShowTodayCircle)
					{
						Rectangle rectangle3;
						rectangle3..ctor(clientRectangle.X + 5, Math.Max(clientRectangle.Bottom - size2.Height - 2, 0), size2.Width, size2.Height);
						this.DrawTodayCircle(dc, rectangle3);
						num3 += size2.Width + 5;
					}
					StringFormat stringFormat = new StringFormat();
					stringFormat.LineAlignment = 1;
					stringFormat.Alignment = 0;
					Rectangle rectangle4;
					rectangle4..ctor(num3 + clientRectangle.X, Math.Max(clientRectangle.Bottom - size2.Height, 0), Math.Max(clientRectangle.Width - num3, 0), size2.Height);
					dc.DrawString("Today: " + DateTime.Now.ToShortDateString(), mc.bold_font, this.GetControlForeBrush(mc.ForeColor), rectangle4, stringFormat);
					stringFormat.Dispose();
				}
			}
			Brush brush;
			if (mc.owner == null)
			{
				brush = this.GetControlBackBrush(mc.BackColor);
			}
			else
			{
				brush = SystemBrushes.ControlDarkDark;
			}
			for (int k = 0; k <= mc.CalendarDimensions.Width; k++)
			{
				if (k == 0 && clip_rectangle.X == clientRectangle.X)
				{
					dc.FillRectangle(brush, clientRectangle.X, clientRectangle.Y, 1, clientRectangle.Height);
				}
				else if (k == mc.CalendarDimensions.Width && clip_rectangle.Right == clientRectangle.Right)
				{
					dc.FillRectangle(brush, clientRectangle.Right - 1, clientRectangle.Y, 1, clientRectangle.Height);
				}
				else
				{
					Rectangle rectangle5;
					rectangle5..ctor(clientRectangle.X + singleMonthSize.Width * k + size.Width * (k - 1) + 1, clientRectangle.Y, size.Width, clientRectangle.Height);
					if (k < mc.CalendarDimensions.Width && k > 0 && clip_rectangle.IntersectsWith(rectangle5))
					{
						dc.FillRectangle(brush, rectangle5);
					}
				}
			}
			for (int l = 0; l <= mc.CalendarDimensions.Height; l++)
			{
				if (l == 0 && clip_rectangle.Y == clientRectangle.Y)
				{
					dc.FillRectangle(brush, clientRectangle.X, clientRectangle.Y, clientRectangle.Width, 1);
				}
				else if (l == mc.CalendarDimensions.Height && clip_rectangle.Bottom == clientRectangle.Bottom)
				{
					dc.FillRectangle(brush, clientRectangle.X, clientRectangle.Bottom - 1, clientRectangle.Width, 1);
				}
				else
				{
					Rectangle rectangle6;
					rectangle6..ctor(clientRectangle.X, clientRectangle.Y + singleMonthSize.Height * l + size.Height * (l - 1) + 1, clientRectangle.Width, size.Height);
					if (l < mc.CalendarDimensions.Height && l > 0 && clip_rectangle.IntersectsWith(rectangle6))
					{
						dc.FillRectangle(brush, rectangle6);
					}
				}
			}
			if (mc.owner != null)
			{
				Rectangle clientRectangle2 = mc.ClientRectangle;
				if (clip_rectangle.Contains(mc.Location))
				{
					if (clip_rectangle.Contains(new Point(clientRectangle2.Left, clientRectangle2.Bottom)))
					{
						dc.DrawLine(SystemPens.ControlText, clientRectangle2.X, clientRectangle2.Y, clientRectangle2.X, clientRectangle2.Bottom - 1);
					}
					if (clip_rectangle.Contains(new Point(clientRectangle2.Right, clientRectangle2.Y)))
					{
						dc.DrawLine(SystemPens.ControlText, clientRectangle2.X, clientRectangle2.Y, clientRectangle2.Right - 1, clientRectangle2.Y);
					}
				}
				if (clip_rectangle.Contains(new Point(clientRectangle2.Right, clientRectangle2.Bottom)))
				{
					if (clip_rectangle.Contains(new Point(clientRectangle2.Left, clientRectangle2.Bottom)))
					{
						dc.DrawLine(SystemPens.ControlText, clientRectangle2.X, clientRectangle2.Bottom - 1, clientRectangle2.Right - 1, clientRectangle2.Bottom - 1);
					}
					if (clip_rectangle.Contains(new Point(clientRectangle2.Right, clientRectangle2.Y)))
					{
						dc.DrawLine(SystemPens.ControlText, clientRectangle2.Right - 1, clientRectangle2.Y, clientRectangle2.Right - 1, clientRectangle2.Bottom - 1);
					}
				}
			}
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x000DFCA8 File Offset: 0x000DDEA8
		private void DrawSingleMonth(Graphics dc, Rectangle clip_rectangle, Rectangle rectangle, MonthCalendar mc, int row, int col)
		{
			Size size = (Size)mc.title_size;
			Size size2 = (Size)mc.date_cell_size;
			DateTime dateTime = (DateTime)mc.current_month;
			DateTime dateTime2;
			dateTime2..ctor(2006, 10, 1);
			DateTime dateTime3 = dateTime.AddMonths(row * mc.CalendarDimensions.Width + col);
			Rectangle rectangle2;
			rectangle2..ctor(rectangle.X, rectangle.Y, size.Width, size.Height);
			if (rectangle2.IntersectsWith(clip_rectangle))
			{
				dc.FillRectangle(this.ResPool.GetSolidBrush(mc.TitleBackColor), rectangle2);
				string text = dateTime3.ToString("MMMM yyyy");
				dc.DrawString(text, mc.bold_font, this.ResPool.GetSolidBrush(mc.TitleForeColor), rectangle2, mc.centered_format);
				if (mc.ShowYearUpDown)
				{
					Rectangle rectangle3;
					Rectangle rectangle4;
					Rectangle rectangle5;
					mc.GetYearNameRectangles(rectangle2, row * mc.CalendarDimensions.Width + col, out rectangle3, out rectangle4, out rectangle5);
					dc.FillRectangle(this.ResPool.GetSolidBrush(SystemColors.Control), rectangle3);
					dc.DrawString(dateTime3.ToString("yyyy"), mc.bold_font, this.ResPool.GetSolidBrush(Color.Black), rectangle3, mc.centered_format);
					ButtonState buttonState = ((!mc.IsYearGoingUp) ? ButtonState.Normal : ButtonState.Pushed);
					ButtonState buttonState2 = ((!mc.IsYearGoingDown) ? ButtonState.Normal : ButtonState.Pushed);
					ControlPaint.DrawScrollButton(dc, rectangle4, ScrollButton.Min, buttonState);
					ControlPaint.DrawScrollButton(dc, rectangle5, ScrollButton.Down, buttonState2);
				}
				if (row == 0 && col == 0)
				{
					this.DrawMonthCalendarButton(dc, rectangle, mc, size, mc.button_x_offset, (Size)mc.button_size, true);
				}
				if (row == 0 && col == mc.CalendarDimensions.Width - 1)
				{
					this.DrawMonthCalendarButton(dc, rectangle, mc, size, mc.button_x_offset, (Size)mc.button_size, false);
				}
			}
			int num = ((!mc.ShowWeekNumbers) ? 0 : 1);
			Rectangle rectangle6;
			rectangle6..ctor(rectangle.X, rectangle.Y + size.Height, (7 + num) * size2.Width, size2.Height);
			if (rectangle6.IntersectsWith(clip_rectangle))
			{
				dc.FillRectangle(this.GetControlBackBrush(mc.BackColor), rectangle6);
				DayOfWeek dayOfWeek = mc.GetDayOfWeek(mc.FirstDayOfWeek);
				for (int i = 0; i < 7; i++)
				{
					int num2 = i - dayOfWeek;
					if (num2 < 0)
					{
						num2 = 7 + num2;
					}
					Rectangle rectangle7;
					rectangle7..ctor(rectangle6.X + (i + num) * size2.Width, rectangle6.Y, size2.Width, size2.Height);
					dc.DrawString(dateTime2.AddDays(i + dayOfWeek).ToString("ddd"), mc.Font, this.ResPool.GetSolidBrush(mc.TitleBackColor), rectangle7, mc.centered_format);
				}
				int num3 = Math.Max(size.Height + size2.Height - 1, 0);
				dc.DrawLine(this.ResPool.GetPen(mc.ForeColor), rectangle.X + num * size2.Width + mc.divider_line_offset, rectangle.Y + num3, rectangle.Right - mc.divider_line_offset, rectangle.Y + num3);
			}
			Rectangle rectangle8;
			rectangle8..ctor(rectangle.X, rectangle.Y + size.Height + size2.Height, size2.Width, size2.Height);
			int num4 = 0;
			bool flag = false;
			DateTime dateTime4 = mc.GetFirstDateInMonthGrid(new DateTime(dateTime3.Year, dateTime3.Month, 1));
			for (int j = 0; j < 6; j++)
			{
				Rectangle rectangle9;
				rectangle9..ctor(rectangle.X, rectangle.Y + size.Height + size2.Height * (j + 1), size2.Width * 7, size2.Height);
				if (mc.ShowWeekNumbers)
				{
					rectangle9.Width += size2.Width;
				}
				bool flag2 = rectangle9.IntersectsWith(clip_rectangle);
				if (flag2)
				{
					dc.FillRectangle(this.GetControlBackBrush(mc.BackColor), rectangle9);
				}
				if (mc.IsValidWeekToDraw(dateTime3, dateTime4, row, col))
				{
					num4 = j;
				}
				if (mc.ShowWeekNumbers && num4 == j)
				{
					if (!flag)
					{
						flag = flag2;
					}
					int weekOfYear = mc.GetWeekOfYear(dateTime4);
					if (flag2)
					{
						dc.DrawString(weekOfYear.ToString(), mc.Font, this.ResPool.GetSolidBrush(mc.TitleBackColor), rectangle8, mc.centered_format);
					}
					rectangle8.Offset(size2.Width, 0);
				}
				if (num4 == j)
				{
					for (int k = 0; k < 7; k++)
					{
						if (flag2)
						{
							this.DrawMonthCalendarDate(dc, rectangle8, mc, dateTime4, dateTime3, row, col);
						}
						dateTime4 = dateTime4.AddDays(1.0);
						rectangle8.Offset(size2.Width, 0);
					}
					int num5 = ((!mc.ShowWeekNumbers) ? (-7) : (-8));
					rectangle8.Offset(num5 * size2.Width, size2.Height);
				}
			}
			num4++;
			if (flag)
			{
				dc.DrawLine(this.ResPool.GetPen(mc.ForeColor), rectangle.X + size2.Width - 1, rectangle.Y + size.Height + size2.Height + mc.divider_line_offset, rectangle.X + size2.Width - 1, rectangle.Y + size.Height + size2.Height + num4 * size2.Height - mc.divider_line_offset);
			}
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x000E0300 File Offset: 0x000DE500
		private void DrawMonthCalendarButton(Graphics dc, Rectangle rectangle, MonthCalendar mc, Size title_size, int x_offset, Size button_size, bool is_previous)
		{
			PointF[] array = new PointF[3];
			bool flag;
			Rectangle rectangle2;
			if (is_previous)
			{
				flag = mc.is_previous_clicked;
				rectangle2..ctor(rectangle.X + 1 + x_offset, rectangle.Y + 1 + (title_size.Height - button_size.Height) / 2, Math.Max(button_size.Width - 1, 0), Math.Max(button_size.Height - 1, 0));
				PointF pointF;
				pointF..ctor((float)rectangle2.X + (float)(rectangle2.Width + 4) / 2f, (float)(rectangle.Y + (rectangle2.Height + 7) / 2 + 1));
				if (flag)
				{
					pointF.X += 1f;
					pointF.Y += 1f;
				}
				array[0].X = pointF.X;
				array[0].Y = pointF.Y - 3.5f + 0.5f;
				array[1].X = pointF.X;
				array[1].Y = pointF.Y + 3.5f + 0.5f;
				array[2].X = pointF.X - 4f;
				array[2].Y = pointF.Y + 0.5f;
			}
			else
			{
				flag = mc.is_next_clicked;
				rectangle2..ctor(rectangle.Right - 1 - x_offset - button_size.Width, rectangle.Y + 1 + (title_size.Height - button_size.Height) / 2, Math.Max(button_size.Width - 1, 0), Math.Max(button_size.Height - 1, 0));
				PointF pointF;
				pointF..ctor((float)rectangle2.X + (float)(rectangle2.Width + 4) / 2f, (float)(rectangle.Y + (rectangle2.Height + 7) / 2 + 1));
				if (flag)
				{
					pointF.X += 1f;
					pointF.Y += 1f;
				}
				array[0].X = pointF.X - 4f;
				array[0].Y = pointF.Y - 3.5f + 0.5f;
				array[1].X = pointF.X - 4f;
				array[1].Y = pointF.Y + 3.5f + 0.5f;
				array[2].X = pointF.X;
				array[2].Y = pointF.Y + 0.5f;
			}
			dc.FillRectangle(SystemBrushes.Control, rectangle2);
			if (flag)
			{
				dc.DrawRectangle(SystemPens.ControlDark, rectangle2);
			}
			else
			{
				this.CPDrawBorder3D(dc, rectangle2, Border3DStyle.Raised, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
			}
			dc.FillPolygon(SystemBrushes.ControlText, array);
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x000E0610 File Offset: 0x000DE810
		private void DrawMonthCalendarDate(Graphics dc, Rectangle rectangle, MonthCalendar mc, DateTime date, DateTime month, int row, int col)
		{
			Color color = mc.ForeColor;
			Rectangle rectangle2;
			rectangle2..ctor(rectangle.X, rectangle.Y, Math.Max(rectangle.Width - 1, 0), Math.Max(rectangle.Height - 1, 0));
			if (date.Year != month.Year || date.Month != month.Month)
			{
				DateTime dateTime = month.AddMonths(-1);
				if (dateTime.Year == date.Year && dateTime.Month == date.Month && row == 0 && col == 0)
				{
					color = mc.TrailingForeColor;
				}
				else
				{
					dateTime = month.AddMonths(1);
					if (dateTime.Year != date.Year || dateTime.Month != date.Month || row != mc.CalendarDimensions.Height - 1 || col != mc.CalendarDimensions.Width - 1)
					{
						return;
					}
					color = mc.TrailingForeColor;
				}
			}
			else
			{
				color = mc.ForeColor;
			}
			if (date == mc.SelectionStart.Date && date == mc.SelectionEnd.Date)
			{
				color = mc.BackColor;
				Rectangle rectangle3 = Rectangle.Inflate(rectangle, -1, -1);
				dc.FillPie(this.ResPool.GetSolidBrush(mc.TitleBackColor), rectangle3, 0f, 360f);
			}
			else if (date == mc.SelectionStart.Date)
			{
				color = mc.BackColor;
				Rectangle rectangle4 = Rectangle.Inflate(rectangle, -1, -1);
				dc.FillPie(this.ResPool.GetSolidBrush(mc.TitleBackColor), rectangle4, 90f, 180f);
				if (date < mc.SelectionEnd.Date)
				{
					rectangle4.X = (int)Math.Floor((double)(rectangle.X + rectangle.Width / 2));
					rectangle4.Width = Math.Max(rectangle.Right - rectangle4.X, 0);
					dc.FillRectangle(this.ResPool.GetSolidBrush(mc.TitleBackColor), rectangle4);
				}
			}
			else if (date == mc.SelectionEnd.Date)
			{
				color = mc.BackColor;
				Rectangle rectangle5 = Rectangle.Inflate(rectangle, -1, -1);
				dc.FillPie(this.ResPool.GetSolidBrush(mc.TitleBackColor), rectangle5, 270f, 180f);
				if (date > mc.SelectionStart.Date)
				{
					rectangle5.X = rectangle.X;
					rectangle5.Width = rectangle.Width - rectangle.Width / 2;
					dc.FillRectangle(this.ResPool.GetSolidBrush(mc.TitleBackColor), rectangle5);
				}
			}
			else if (date > mc.SelectionStart.Date && date < mc.SelectionEnd.Date)
			{
				color = mc.BackColor;
				Rectangle rectangle6 = Rectangle.Inflate(rectangle, 0, -1);
				dc.FillRectangle(this.ResPool.GetSolidBrush(mc.TitleBackColor), rectangle6);
			}
			Font font = ((!mc.IsBoldedDate(date)) ? mc.Font : mc.bold_font);
			dc.DrawString(date.Day.ToString(), font, this.ResPool.GetSolidBrush(color), rectangle, mc.centered_format);
			if (mc.ShowTodayCircle && date == DateTime.Now.Date)
			{
				this.DrawTodayCircle(dc, rectangle2);
			}
			if (mc.is_date_clicked && mc.clicked_date == date)
			{
				Pen dashPen = this.ResPool.GetDashPen(Color.Black, 2);
				dc.DrawRectangle(dashPen, rectangle2);
			}
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x000E0A34 File Offset: 0x000DEC34
		private void DrawTodayCircle(Graphics dc, Rectangle rectangle)
		{
			Color color = Color.FromArgb(248, 0, 0);
			Rectangle rectangle2;
			rectangle2..ctor(rectangle.X + 1, rectangle.Y + 4, Math.Max(rectangle.Width - 2, 0), Math.Max(rectangle.Height - 5, 0));
			Rectangle rectangle3;
			rectangle3..ctor(rectangle.X + 1, rectangle.Y + 1, Math.Max(rectangle.Width - 2, 0), Math.Max(rectangle.Height - 2, 0));
			Point[] array = new Point[]
			{
				new Point(rectangle2.X, rectangle3.Y + rectangle3.Height / 12),
				new Point(rectangle2.X + rectangle2.Width / 9, rectangle3.Y),
				new Point(rectangle2.X + rectangle2.Width / 2 + 1, rectangle3.Y)
			};
			Pen sizedPen = this.ResPool.GetSizedPen(color, 2);
			dc.DrawArc(sizedPen, rectangle2, 90f, 180f);
			dc.DrawArc(sizedPen, rectangle3, 270f, 180f);
			dc.DrawCurve(sizedPen, array);
			dc.DrawLine(this.ResPool.GetPen(color), array[2], new Point(array[2].X, rectangle2.Y));
		}

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x0600382E RID: 14382 RVA: 0x000E0BB8 File Offset: 0x000DEDB8
		public override Size PanelDefaultSize
		{
			get
			{
				return new Size(200, 100);
			}
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x000E0BC8 File Offset: 0x000DEDC8
		public override void DrawPictureBox(Graphics dc, Rectangle clip, PictureBox pb)
		{
			Rectangle clientRectangle = pb.ClientRectangle;
			clientRectangle..ctor(clientRectangle.Left + pb.Padding.Left, clientRectangle.Top + pb.Padding.Top, clientRectangle.Width - pb.Padding.Horizontal, clientRectangle.Height - pb.Padding.Vertical);
			if (pb.Image != null)
			{
				switch (pb.SizeMode)
				{
				case PictureBoxSizeMode.StretchImage:
					dc.DrawImage(pb.Image, clientRectangle.Left, clientRectangle.Top, clientRectangle.Width, clientRectangle.Height);
					return;
				case PictureBoxSizeMode.CenterImage:
					dc.DrawImage(pb.Image, clientRectangle.Width / 2 - pb.Image.Width / 2, clientRectangle.Height / 2 - pb.Image.Height / 2);
					return;
				case PictureBoxSizeMode.Zoom:
				{
					Size size;
					if ((float)pb.Image.Width / (float)pb.Image.Height >= (float)clientRectangle.Width / (float)clientRectangle.Height)
					{
						size..ctor(clientRectangle.Width, pb.Image.Height * clientRectangle.Width / pb.Image.Width);
					}
					else
					{
						size..ctor(pb.Image.Width * clientRectangle.Height / pb.Image.Height, clientRectangle.Height);
					}
					dc.DrawImage(pb.Image, clientRectangle.Width / 2 - size.Width / 2, clientRectangle.Height / 2 - size.Height / 2, size.Width, size.Height);
					return;
				}
				}
				dc.DrawImage(pb.Image, clientRectangle.Left, clientRectangle.Top, pb.Image.Width, pb.Image.Height);
				return;
			}
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06003830 RID: 14384 RVA: 0x000E0DE4 File Offset: 0x000DEFE4
		public override Size PictureBoxDefaultSize
		{
			get
			{
				return new Size(100, 50);
			}
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06003831 RID: 14385 RVA: 0x000E0DF0 File Offset: 0x000DEFF0
		public override int PrintPreviewControlPadding
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x000E0DF4 File Offset: 0x000DEFF4
		public override Size PrintPreviewControlGetPageSize(PrintPreviewControl preview)
		{
			int printPreviewControlPadding = this.PrintPreviewControlPadding;
			PreviewPageInfo[] page_infos = preview.page_infos;
			int num4;
			int num5;
			if (preview.AutoZoom)
			{
				int num = preview.ClientRectangle.Height - preview.Rows * printPreviewControlPadding - 2 * printPreviewControlPadding;
				int num2 = preview.ClientRectangle.Width - (preview.Columns - 1) * printPreviewControlPadding - 2 * printPreviewControlPadding;
				float num3 = (float)page_infos[0].Image.Width / (float)page_infos[0].Image.Height;
				num4 = num2 / preview.Columns;
				num5 = (int)((float)num4 / num3);
				if (num5 * (preview.Rows + 1) > num)
				{
					num5 = num / (preview.Rows + 1);
					num4 = (int)((float)num5 * num3);
				}
			}
			else
			{
				num4 = (int)((double)page_infos[0].Image.Width * preview.Zoom);
				num5 = (int)((double)page_infos[0].Image.Height * preview.Zoom);
			}
			return new Size(num4, num5);
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x000E0EE8 File Offset: 0x000DF0E8
		public override void PrintPreviewControlPaint(PaintEventArgs pe, PrintPreviewControl preview, Size page_size)
		{
			int num = 8;
			PreviewPageInfo[] page_infos = preview.page_infos;
			if (page_infos == null)
			{
				return;
			}
			int num2 = page_size.Width * preview.Columns + num * (preview.Columns - 1) + 2 * num;
			int num3 = page_size.Height * (preview.Rows + 1) + num * preview.Rows + 2 * num;
			Rectangle viewPort = preview.ViewPort;
			pe.Graphics.Clip = new Region(viewPort);
			int num4 = viewPort.Width / 2 - num2 / 2;
			if (num4 < 0)
			{
				num4 = 0;
			}
			int num5 = viewPort.Height / 2 - num3 / 2;
			if (num5 < 0)
			{
				num5 = 0;
			}
			int num6 = num5 + num - preview.vbar_value;
			if (preview.StartPage > 0)
			{
				int num7 = preview.StartPage - 1;
				for (int i = 0; i < preview.Rows + 1; i++)
				{
					int num8 = num4 + num - preview.hbar_value;
					for (int j = 0; j < preview.Columns; j++)
					{
						if (num7 < page_infos.Length)
						{
							Image image = preview.image_cache[num7];
							if (image == null)
							{
								image = page_infos[num7].Image;
							}
							Rectangle rectangle;
							rectangle..ctor(new Point(num8, num6), page_size);
							pe.Graphics.DrawImage(image, rectangle, 0, 0, image.Width, image.Height, 2);
							num8 += num + page_size.Width;
							num7++;
						}
					}
					num6 += num + page_size.Height;
				}
			}
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x000E1074 File Offset: 0x000DF274
		public override void DrawProgressBar(Graphics dc, Rectangle clip_rect, ProgressBar ctrl)
		{
			Rectangle client_area = ctrl.client_area;
			this.CPDrawBorder3D(dc, ctrl.ClientRectangle, Border3DStyle.SunkenOuter, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom, this.ColorControl);
			int num = int.MaxValue;
			int num2 = client_area.X;
			switch (ctrl.Style)
			{
			case ProgressBarStyle.Continuous:
			{
				int num3 = (int)((double)client_area.Width * ((double)(ctrl.Value - ctrl.Minimum) / (double)Math.Max(ctrl.Maximum - ctrl.Minimum, 1)));
				dc.FillRectangle(this.ResPool.GetSolidBrush(ctrl.ForeColor), new Rectangle(client_area.X, client_area.Y, num3, client_area.Height));
				return;
			}
			case ProgressBarStyle.Marquee:
				if (XplatUI.ThemesEnabled)
				{
					int num4 = (int)(DateTime.Now - ctrl.start).TotalMilliseconds;
					double num5 = (double)num4 % (double)ctrl.MarqueeAnimationSpeed / (double)ctrl.MarqueeAnimationSpeed;
					num = 5;
					num2 = client_area.X + (int)((double)client_area.Width * num5);
				}
				break;
			}
			int num6 = 2;
			int num7 = 0;
			int num8 = ThemeWin32Classic.ProgressBarGetChunkSize(client_area.Height);
			num8 = Math.Max(num8, 0);
			int num9 = (int)((double)(ctrl.Value - ctrl.Minimum) * (double)client_area.Width / (double)Math.Max(ctrl.Maximum - ctrl.Minimum, 1));
			int num10 = num8 + num6;
			Rectangle rectangle;
			rectangle..ctor(num2, client_area.Y, num8, client_area.Height);
			for (;;)
			{
				if (num != 2147483647)
				{
					if (num7 >= num)
					{
						break;
					}
					if (rectangle.X > client_area.Width)
					{
						rectangle.X -= client_area.Width;
					}
				}
				else if (rectangle.X - client_area.X >= num9)
				{
					break;
				}
				if (clip_rect.IntersectsWith(rectangle))
				{
					dc.FillRectangle(this.ResPool.GetSolidBrush(ctrl.ForeColor), rectangle);
				}
				rectangle.X += num10;
				num7++;
			}
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x000E12A4 File Offset: 0x000DF4A4
		public static int ProgressBarGetChunkSize()
		{
			return ThemeWin32Classic.ProgressBarGetChunkSize(23);
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x000E12B0 File Offset: 0x000DF4B0
		private static int ProgressBarGetChunkSize(int progressBarClientAreaHeight)
		{
			return progressBarClientAreaHeight * 2 / 3;
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06003837 RID: 14391 RVA: 0x000E12C4 File Offset: 0x000DF4C4
		public override Size ProgressBarDefaultSize
		{
			get
			{
				return new Size(100, 23);
			}
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x000E12D0 File Offset: 0x000DF4D0
		public override void DrawRadioButton(Graphics dc, Rectangle clip_rectangle, RadioButton radio_button)
		{
			int num = 13;
			int num2 = 4;
			Rectangle clientRectangle = radio_button.ClientRectangle;
			Rectangle rectangle = clientRectangle;
			Rectangle rectangle2;
			rectangle2..ctor(rectangle.X, rectangle.Y, num, num);
			StringFormat stringFormat = new StringFormat();
			stringFormat.Alignment = 0;
			stringFormat.LineAlignment = 1;
			stringFormat.HotkeyPrefix = 1;
			ContentAlignment contentAlignment;
			if (radio_button.appearance != Appearance.Button)
			{
				contentAlignment = radio_button.radiobutton_alignment;
				switch (contentAlignment)
				{
				case 1:
					rectangle2.X = clientRectangle.Left;
					rectangle2.Y = clientRectangle.Top;
					rectangle.X = clientRectangle.X + num + num2;
					rectangle.Width = clientRectangle.Width - num - num2;
					break;
				case 2:
					rectangle2.X = (clientRectangle.Right - clientRectangle.Left) / 2 - num / 2;
					rectangle2.Y = clientRectangle.Top;
					rectangle.X = clientRectangle.X;
					rectangle.Y = num + num2;
					rectangle.Width = clientRectangle.Width;
					rectangle.Height = clientRectangle.Height - num - num2;
					break;
				default:
					if (contentAlignment != 16)
					{
						if (contentAlignment == 32)
						{
							rectangle2.X = (clientRectangle.Right - clientRectangle.Left) / 2 - num / 2;
							rectangle2.Y = (clientRectangle.Bottom - clientRectangle.Top) / 2 - num / 2;
							rectangle.X = clientRectangle.X;
							rectangle.Width = clientRectangle.Width;
							break;
						}
						if (contentAlignment == 64)
						{
							rectangle2.X = clientRectangle.Right - num;
							rectangle2.Y = (clientRectangle.Bottom - clientRectangle.Top) / 2 - num / 2;
							rectangle.X = clientRectangle.X;
							rectangle.Width = clientRectangle.Width - num - num2;
							break;
						}
						if (contentAlignment == 256)
						{
							rectangle2.X = clientRectangle.Left;
							rectangle2.Y = clientRectangle.Bottom - num;
							rectangle.X = clientRectangle.X + num + num2;
							rectangle.Width = clientRectangle.Width - num - num2;
							break;
						}
						if (contentAlignment == 512)
						{
							rectangle2.X = (clientRectangle.Right - clientRectangle.Left) / 2 - num / 2;
							rectangle2.Y = clientRectangle.Bottom - num;
							rectangle.X = clientRectangle.X;
							rectangle.Width = clientRectangle.Width;
							rectangle.Height = clientRectangle.Height - num - num2;
							break;
						}
						if (contentAlignment == 1024)
						{
							rectangle2.X = clientRectangle.Right - num;
							rectangle2.Y = clientRectangle.Bottom - num;
							rectangle.X = clientRectangle.X;
							rectangle.Width = clientRectangle.Width - num - num2;
							break;
						}
					}
					rectangle2.X = clientRectangle.Left;
					rectangle2.Y = (clientRectangle.Bottom - clientRectangle.Top) / 2 - num / 2;
					rectangle.X = clientRectangle.X + num + num2;
					rectangle.Width = clientRectangle.Width - num - num2;
					break;
				case 4:
					rectangle2.X = clientRectangle.Right - num;
					rectangle2.Y = clientRectangle.Top;
					rectangle.X = clientRectangle.X;
					rectangle.Width = clientRectangle.Width - num - num2;
					break;
				}
			}
			else
			{
				rectangle.X = clientRectangle.X;
				rectangle.Width = clientRectangle.Width;
			}
			contentAlignment = radio_button.text_alignment;
			switch (contentAlignment)
			{
			case 1:
				break;
			case 2:
				goto IL_0442;
			default:
				if (contentAlignment != 16)
				{
					if (contentAlignment == 32)
					{
						goto IL_0442;
					}
					if (contentAlignment == 64)
					{
						goto IL_044E;
					}
					if (contentAlignment != 256)
					{
						if (contentAlignment == 512)
						{
							goto IL_0442;
						}
						if (contentAlignment != 1024)
						{
							goto IL_045A;
						}
						goto IL_044E;
					}
				}
				break;
			case 4:
				goto IL_044E;
			}
			stringFormat.Alignment = 0;
			goto IL_045A;
			IL_0442:
			stringFormat.Alignment = 1;
			goto IL_045A;
			IL_044E:
			stringFormat.Alignment = 2;
			IL_045A:
			contentAlignment = radio_button.text_alignment;
			switch (contentAlignment)
			{
			case 1:
			case 2:
			case 4:
				stringFormat.LineAlignment = 0;
				break;
			default:
				if (contentAlignment != 16 && contentAlignment != 32 && contentAlignment != 64)
				{
					if (contentAlignment == 256 || contentAlignment == 512 || contentAlignment == 1024)
					{
						stringFormat.LineAlignment = 2;
					}
				}
				else
				{
					stringFormat.LineAlignment = 1;
				}
				break;
			}
			ButtonState buttonState = ButtonState.Normal;
			if (radio_button.FlatStyle == FlatStyle.Flat)
			{
				buttonState |= ButtonState.Flat;
			}
			if (radio_button.Checked)
			{
				buttonState |= ButtonState.Checked;
			}
			if (!radio_button.Enabled)
			{
				buttonState |= ButtonState.Inactive;
			}
			this.RadioButton_DrawButton(radio_button, dc, buttonState, rectangle2);
			if (radio_button.image != null || radio_button.image_list != null)
			{
				this.ButtonBase_DrawImage(radio_button, dc);
			}
			this.RadioButton_DrawText(radio_button, rectangle, dc, stringFormat);
			if (radio_button.Focused && radio_button.Enabled && radio_button.appearance != Appearance.Button && radio_button.Text != string.Empty && radio_button.ShowFocusCues)
			{
				SizeF sizeF = dc.MeasureString(radio_button.Text, radio_button.Font);
				Rectangle empty = Rectangle.Empty;
				empty.X = rectangle.X;
				empty.Y = (int)(((float)rectangle.Height - sizeF.Height) / 2f);
				empty.Size = sizeF.ToSize();
				this.RadioButton_DrawFocus(radio_button, dc, empty);
			}
			stringFormat.Dispose();
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x000E18DC File Offset: 0x000DFADC
		protected virtual void RadioButton_DrawButton(RadioButton radio_button, Graphics dc, ButtonState state, Rectangle radiobutton_rectangle)
		{
			dc.FillRectangle(this.GetControlBackBrush(radio_button.BackColor), radio_button.ClientRectangle);
			if (radio_button.appearance == Appearance.Button)
			{
				this.ButtonBase_DrawButton(radio_button, dc);
				if (radio_button.Focused && radio_button.Enabled)
				{
					this.ButtonBase_DrawFocus(radio_button, dc);
				}
			}
			else if (radio_button.FlatStyle == FlatStyle.Flat || radio_button.FlatStyle == FlatStyle.Popup)
			{
				this.DrawFlatStyleRadioButton(dc, radiobutton_rectangle, radio_button);
			}
			else
			{
				this.CPDrawRadioButton(dc, radiobutton_rectangle, state);
			}
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x000E1968 File Offset: 0x000DFB68
		protected virtual void RadioButton_DrawText(RadioButton radio_button, Rectangle text_rectangle, Graphics dc, StringFormat text_format)
		{
			this.DrawCheckBox_and_RadioButtonText(radio_button, text_rectangle, dc, text_format, radio_button.Appearance, radio_button.Checked);
		}

		// Token: 0x0600383B RID: 14395 RVA: 0x000E198C File Offset: 0x000DFB8C
		protected virtual void RadioButton_DrawFocus(RadioButton radio_button, Graphics dc, Rectangle text_rectangle)
		{
			this.DrawInnerFocusRectangle(dc, text_rectangle, radio_button.BackColor);
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x000E199C File Offset: 0x000DFB9C
		protected virtual void DrawFlatStyleRadioButton(Graphics graphics, Rectangle rectangle, RadioButton radio_button)
		{
			if (radio_button.Enabled)
			{
				if (radio_button.FlatStyle == FlatStyle.Flat)
				{
					graphics.DrawArc(SystemPens.ControlDarkDark, rectangle, 0f, 359f);
					if ((radio_button.is_entered || radio_button.Capture) && !radio_button.is_pressed)
					{
						graphics.FillPie(SystemBrushes.ControlLight, rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height - 2, 0, 359);
					}
					else
					{
						graphics.FillPie(SystemBrushes.ControlLightLight, rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height - 2, 0, 359);
					}
				}
				else
				{
					graphics.FillPie(SystemBrushes.ControlLightLight, rectangle, 0f, 359f);
					if (radio_button.is_entered || radio_button.Capture)
					{
						graphics.DrawArc(SystemPens.ControlLight, rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height - 2, 0, 359);
						graphics.DrawArc(SystemPens.ControlDark, rectangle, 135f, 180f);
						graphics.DrawArc(SystemPens.ControlLightLight, rectangle, 315f, 180f);
					}
					else
					{
						graphics.DrawArc(SystemPens.ControlDark, rectangle, 0f, 359f);
					}
				}
			}
			else
			{
				graphics.FillPie(SystemBrushes.Control, rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height - 2, 0, 359);
				graphics.DrawArc(SystemPens.ControlDark, rectangle, 0f, 359f);
			}
			if (radio_button.Checked)
			{
				int num = Math.Max(1, Math.Min(rectangle.Width, rectangle.Height) / 3);
				Pen pen = SystemPens.ControlDarkDark;
				Brush brush = SystemBrushes.ControlDarkDark;
				if (!radio_button.Enabled || (radio_button.FlatStyle == FlatStyle.Popup && radio_button.is_pressed))
				{
					pen = SystemPens.ControlDark;
					brush = SystemBrushes.ControlDark;
				}
				if (rectangle.Height > 13)
				{
					graphics.FillPie(brush, rectangle.X + num, rectangle.Y + num, rectangle.Width - num * 2, rectangle.Height - num * 2, 0, 359);
				}
				else
				{
					int num2 = rectangle.Width / 2 + rectangle.X;
					int num3 = rectangle.Height / 2 + rectangle.Y;
					graphics.DrawLine(pen, num2 - 1, num3, num2 + 2, num3);
					graphics.DrawLine(pen, num2 - 1, num3 + 1, num2 + 2, num3 + 1);
					graphics.DrawLine(pen, num2, num3 - 1, num2, num3 + 2);
					graphics.DrawLine(pen, num2 + 1, num3 - 1, num2 + 1, num3 + 2);
				}
			}
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x0600383D RID: 14397 RVA: 0x000E1C7C File Offset: 0x000DFE7C
		public override Size RadioButtonDefaultSize
		{
			get
			{
				return new Size(104, 24);
			}
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x000E1C88 File Offset: 0x000DFE88
		public override void DrawRadioButton(Graphics g, RadioButton rb, Rectangle glyphArea, Rectangle textBounds, Rectangle imageBounds, Rectangle clipRectangle)
		{
			if (rb.FlatStyle == FlatStyle.Flat || rb.FlatStyle == FlatStyle.Popup)
			{
				glyphArea.Height -= 2;
				glyphArea.Width -= 2;
			}
			this.DrawRadioButtonGlyph(g, rb, glyphArea);
			if (imageBounds.Size != Size.Empty)
			{
				this.DrawRadioButtonImage(g, rb, imageBounds);
			}
			if (rb.Focused && rb.Enabled && rb.ShowFocusCues && textBounds.Size != Size.Empty)
			{
				this.DrawRadioButtonFocus(g, rb, textBounds);
			}
			if (textBounds != Rectangle.Empty)
			{
				this.DrawRadioButtonText(g, rb, textBounds);
			}
		}

		// Token: 0x0600383F RID: 14399 RVA: 0x000E1D50 File Offset: 0x000DFF50
		public virtual void DrawRadioButtonGlyph(Graphics g, RadioButton rb, Rectangle glyphArea)
		{
			if (rb.Pressed)
			{
				ThemeElements.CurrentTheme.RadioButtonPainter.PaintRadioButton(g, glyphArea, rb.BackColor, rb.ForeColor, ElementState.Pressed, rb.FlatStyle, rb.Checked);
			}
			else if (rb.InternalSelected)
			{
				ThemeElements.CurrentTheme.RadioButtonPainter.PaintRadioButton(g, glyphArea, rb.BackColor, rb.ForeColor, ElementState.Normal, rb.FlatStyle, rb.Checked);
			}
			else if (rb.Entered)
			{
				ThemeElements.CurrentTheme.RadioButtonPainter.PaintRadioButton(g, glyphArea, rb.BackColor, rb.ForeColor, ElementState.Hot, rb.FlatStyle, rb.Checked);
			}
			else if (!rb.Enabled)
			{
				ThemeElements.CurrentTheme.RadioButtonPainter.PaintRadioButton(g, glyphArea, rb.BackColor, rb.ForeColor, ElementState.Disabled, rb.FlatStyle, rb.Checked);
			}
			else
			{
				ThemeElements.CurrentTheme.RadioButtonPainter.PaintRadioButton(g, glyphArea, rb.BackColor, rb.ForeColor, ElementState.Normal, rb.FlatStyle, rb.Checked);
			}
		}

		// Token: 0x06003840 RID: 14400 RVA: 0x000E1E70 File Offset: 0x000E0070
		public virtual void DrawRadioButtonFocus(Graphics g, RadioButton rb, Rectangle focusArea)
		{
			ControlPaint.DrawFocusRectangle(g, focusArea);
		}

		// Token: 0x06003841 RID: 14401 RVA: 0x000E1E7C File Offset: 0x000E007C
		public virtual void DrawRadioButtonImage(Graphics g, RadioButton rb, Rectangle imageBounds)
		{
			if (rb.Enabled)
			{
				g.DrawImage(rb.Image, imageBounds);
			}
			else
			{
				this.CPDrawImageDisabled(g, rb.Image, imageBounds.Left, imageBounds.Top, this.ColorControl);
			}
		}

		// Token: 0x06003842 RID: 14402 RVA: 0x000E1EC8 File Offset: 0x000E00C8
		public virtual void DrawRadioButtonText(Graphics g, RadioButton rb, Rectangle textBounds)
		{
			if (rb.Enabled)
			{
				TextRenderer.DrawTextInternal(g, rb.Text, rb.Font, textBounds, rb.ForeColor, rb.TextFormatFlags, rb.UseCompatibleTextRendering);
			}
			else
			{
				this.DrawStringDisabled20(g, rb.Text, rb.Font, textBounds, rb.BackColor, rb.TextFormatFlags, rb.UseCompatibleTextRendering);
			}
		}

		// Token: 0x06003843 RID: 14403 RVA: 0x000E1F30 File Offset: 0x000E0130
		public override Size CalculateRadioButtonAutoSize(RadioButton rb)
		{
			Size empty = Size.Empty;
			Size size = TextRenderer.MeasureTextInternal(rb.Text, rb.Font, rb.UseCompatibleTextRendering);
			Size size2 = ((rb.Image != null) ? rb.Image.Size : Size.Empty);
			if (rb.Text.Length != 0)
			{
				size.Height += 4;
				size.Width += 4;
			}
			switch (rb.TextImageRelation)
			{
			case TextImageRelation.Overlay:
				empty.Height = Math.Max((rb.Text.Length != 0) ? size.Height : 0, size2.Height);
				empty.Width = Math.Max(size.Width, size2.Width);
				break;
			case TextImageRelation.ImageAboveText:
			case TextImageRelation.TextAboveImage:
				empty.Height = size.Height + size2.Height;
				empty.Width = Math.Max(size.Width, size2.Width);
				break;
			case TextImageRelation.ImageBeforeText:
			case TextImageRelation.TextBeforeImage:
				empty.Height = Math.Max(size.Height, size2.Height);
				empty.Width = size.Width + size2.Width;
				break;
			}
			empty.Height += rb.Padding.Vertical;
			empty.Width += rb.Padding.Horizontal + 15;
			if (empty.Height == rb.Padding.Vertical)
			{
				empty.Height += 14;
			}
			return empty;
		}

		// Token: 0x06003844 RID: 14404 RVA: 0x000E2104 File Offset: 0x000E0304
		public override void CalculateRadioButtonTextAndImageLayout(ButtonBase b, Point offset, out Rectangle glyphArea, out Rectangle textRectangle, out Rectangle imageRectangle)
		{
			this.CalculateCheckBoxTextAndImageLayout(b, offset, out glyphArea, out textRectangle, out imageRectangle);
		}

		// Token: 0x06003845 RID: 14405 RVA: 0x000E2114 File Offset: 0x000E0314
		public override void DrawScrollBar(Graphics dc, Rectangle clip, ScrollBar bar)
		{
			int scrollbutton_width = bar.scrollbutton_width;
			int scrollbutton_height = bar.scrollbutton_height;
			Rectangle thumbPos = bar.ThumbPos;
			if (bar.vert)
			{
				Rectangle rectangle;
				rectangle..ctor(0, 0, bar.Width, scrollbutton_height);
				bar.FirstArrowArea = rectangle;
				Rectangle rectangle2;
				rectangle2..ctor(0, bar.ClientRectangle.Height - scrollbutton_height, bar.Width, scrollbutton_height);
				bar.SecondArrowArea = rectangle2;
				thumbPos.Width = bar.Width;
				bar.ThumbPos = thumbPos;
				Brush brush;
				if (bar.thumb_moving == ScrollBar.ThumbMoving.Backwards)
				{
					brush = this.ResPool.GetHatchBrush(12, Color.FromArgb(255, 63, 63, 63), Color.Black);
				}
				else
				{
					brush = this.ResPool.GetHatchBrush(12, this.ColorScrollBar, Color.White);
				}
				Rectangle rectangle3;
				rectangle3..ctor(0, 0, bar.ClientRectangle.Width, bar.ThumbPos.Bottom);
				if (clip.IntersectsWith(rectangle3))
				{
					dc.FillRectangle(brush, rectangle3);
				}
				if (bar.thumb_moving == ScrollBar.ThumbMoving.Forward)
				{
					brush = this.ResPool.GetHatchBrush(12, Color.FromArgb(255, 63, 63, 63), Color.Black);
				}
				else
				{
					brush = this.ResPool.GetHatchBrush(12, this.ColorScrollBar, Color.White);
				}
				Rectangle rectangle4;
				rectangle4..ctor(0, bar.ThumbPos.Bottom, bar.ClientRectangle.Width, bar.ClientRectangle.Height - bar.ThumbPos.Bottom);
				if (clip.IntersectsWith(rectangle4))
				{
					dc.FillRectangle(brush, rectangle4);
				}
				if (clip.IntersectsWith(rectangle))
				{
					this.CPDrawScrollButton(dc, rectangle, ScrollButton.Min, bar.firstbutton_state);
				}
				if (clip.IntersectsWith(rectangle2))
				{
					this.CPDrawScrollButton(dc, rectangle2, ScrollButton.Down, bar.secondbutton_state);
				}
			}
			else
			{
				Rectangle rectangle;
				rectangle..ctor(0, 0, scrollbutton_width, bar.Height);
				bar.FirstArrowArea = rectangle;
				Rectangle rectangle2;
				rectangle2..ctor(bar.ClientRectangle.Width - scrollbutton_width, 0, scrollbutton_width, bar.Height);
				bar.SecondArrowArea = rectangle2;
				thumbPos.Height = bar.Height;
				bar.ThumbPos = thumbPos;
				Brush brush2;
				if (bar.thumb_moving == ScrollBar.ThumbMoving.Backwards)
				{
					brush2 = this.ResPool.GetHatchBrush(12, Color.FromArgb(255, 63, 63, 63), Color.Black);
				}
				else
				{
					brush2 = this.ResPool.GetHatchBrush(12, this.ColorScrollBar, Color.White);
				}
				Rectangle rectangle5;
				rectangle5..ctor(0, 0, bar.ThumbPos.Right, bar.ClientRectangle.Height);
				if (clip.IntersectsWith(rectangle5))
				{
					dc.FillRectangle(brush2, rectangle5);
				}
				if (bar.thumb_moving == ScrollBar.ThumbMoving.Forward)
				{
					brush2 = this.ResPool.GetHatchBrush(12, Color.FromArgb(255, 63, 63, 63), Color.Black);
				}
				else
				{
					brush2 = this.ResPool.GetHatchBrush(12, this.ColorScrollBar, Color.White);
				}
				Rectangle rectangle6;
				rectangle6..ctor(bar.ThumbPos.Right, 0, bar.ClientRectangle.Width - bar.ThumbPos.Right, bar.ClientRectangle.Height);
				if (clip.IntersectsWith(rectangle6))
				{
					dc.FillRectangle(brush2, rectangle6);
				}
				if (clip.IntersectsWith(rectangle))
				{
					this.CPDrawScrollButton(dc, rectangle, ScrollButton.Left, bar.firstbutton_state);
				}
				if (clip.IntersectsWith(rectangle2))
				{
					this.CPDrawScrollButton(dc, rectangle2, ScrollButton.Right, bar.secondbutton_state);
				}
			}
			this.ScrollBar_DrawThumb(bar, thumbPos, clip, dc);
		}

		// Token: 0x06003846 RID: 14406 RVA: 0x000E24DC File Offset: 0x000E06DC
		protected virtual void ScrollBar_DrawThumb(ScrollBar bar, Rectangle thumb_pos, Rectangle clip, Graphics dc)
		{
			if (bar.Enabled && thumb_pos.Width > 0 && thumb_pos.Height > 0 && clip.IntersectsWith(thumb_pos))
			{
				this.DrawScrollButtonPrimitive(dc, thumb_pos, ButtonState.Normal);
			}
		}

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06003847 RID: 14407 RVA: 0x000E2528 File Offset: 0x000E0728
		public override int ScrollBarButtonSize
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06003848 RID: 14408 RVA: 0x000E252C File Offset: 0x000E072C
		public override bool ScrollBarHasHotElementStyles
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06003849 RID: 14409 RVA: 0x000E2530 File Offset: 0x000E0730
		public override bool ScrollBarHasPressedThumbStyle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x0600384A RID: 14410 RVA: 0x000E2534 File Offset: 0x000E0734
		public override bool ScrollBarHasHoverArrowButtonStyle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600384B RID: 14411 RVA: 0x000E2538 File Offset: 0x000E0738
		public override void DrawStatusBar(Graphics real_dc, Rectangle clip, StatusBar sb)
		{
			Rectangle clientRectangle = sb.ClientRectangle;
			int num = 2;
			int num2 = 2;
			Image image = new Bitmap(sb.ClientSize.Width, sb.ClientSize.Height, real_dc);
			Graphics graphics = Graphics.FromImage(image);
			this.DrawStatusBarBackground(graphics, clip, sb);
			if (!sb.ShowPanels && sb.Text != string.Empty)
			{
				string text = sb.Text;
				StringFormat stringFormat = new StringFormat();
				stringFormat.Trimming = 1;
				stringFormat.FormatFlags = 4096;
				if (text.Length > 127)
				{
					text = text.Substring(0, 127);
				}
				if (text.get_Chars(0) == '\t')
				{
					stringFormat.Alignment = 1;
					text = text.Substring(1);
					if (text.get_Chars(0) == '\t')
					{
						stringFormat.Alignment = 2;
						text = text.Substring(1);
					}
				}
				graphics.DrawString(text, sb.Font, this.ResPool.GetSolidBrush(sb.ForeColor), new Rectangle(clientRectangle.X + 2, clientRectangle.Y + 2, clientRectangle.Width - 4, clientRectangle.Height - 4), stringFormat);
				stringFormat.Dispose();
			}
			else if (sb.ShowPanels)
			{
				Brush controlForeBrush = this.GetControlForeBrush(sb.ForeColor);
				int num3 = clientRectangle.X + num;
				int num4 = clientRectangle.Y + num2;
				for (int i = 0; i < sb.Panels.Count; i++)
				{
					Rectangle rectangle;
					rectangle..ctor(num3, num4, sb.Panels[i].Width, clientRectangle.Height);
					num3 += rectangle.Width + this.StatusBarHorzGapWidth;
					if (rectangle.IntersectsWith(clip))
					{
						this.DrawStatusBarPanel(graphics, rectangle, i, controlForeBrush, sb.Panels[i]);
					}
				}
			}
			if (sb.SizingGrip)
			{
				this.DrawStatusBarSizingGrip(graphics, clip, sb, clientRectangle);
			}
			real_dc.DrawImage(image, 0, 0);
			graphics.Dispose();
			image.Dispose();
		}

		// Token: 0x0600384C RID: 14412 RVA: 0x000E275C File Offset: 0x000E095C
		protected virtual void DrawStatusBarBackground(Graphics dc, Rectangle clip, StatusBar sb)
		{
			bool flag = sb.BackColor.ToArgb() == this.ColorControl.ToArgb();
			Brush brush = ((!flag) ? this.ResPool.GetSolidBrush(sb.BackColor) : SystemBrushes.Control);
			dc.FillRectangle(brush, clip);
		}

		// Token: 0x0600384D RID: 14413 RVA: 0x000E27B4 File Offset: 0x000E09B4
		protected virtual void DrawStatusBarSizingGrip(Graphics dc, Rectangle clip, StatusBar sb, Rectangle area)
		{
			area..ctor(area.Right - 16 - 2, area.Bottom - 12 - 1, 16, 16);
			this.CPDrawSizeGrip(dc, this.ColorControl, area);
		}

		// Token: 0x0600384E RID: 14414 RVA: 0x000E27F4 File Offset: 0x000E09F4
		protected virtual void DrawStatusBarPanel(Graphics dc, Rectangle area, int index, Brush br_forecolor, StatusBarPanel panel)
		{
			int num = 3;
			int num2 = 16;
			area.Height -= num;
			this.DrawStatusBarPanelBackground(dc, area, panel);
			if (panel.Style == StatusBarPanelStyle.OwnerDraw)
			{
				StatusBarDrawItemEventArgs statusBarDrawItemEventArgs = new StatusBarDrawItemEventArgs(dc, panel.Parent.Font, area, index, DrawItemState.Default, panel, panel.Parent.ForeColor, panel.Parent.BackColor);
				panel.Parent.OnDrawItemInternal(statusBarDrawItemEventArgs);
				return;
			}
			string text = panel.Text;
			StringFormat stringFormat = new StringFormat();
			stringFormat.Trimming = 1;
			stringFormat.FormatFlags = 4096;
			if (text != null && text.Length > 0 && text.get_Chars(0) == '\t')
			{
				stringFormat.Alignment = 1;
				text = text.Substring(1);
				if (text.get_Chars(0) == '\t')
				{
					stringFormat.Alignment = 2;
					text = text.Substring(1);
				}
			}
			Rectangle empty = Rectangle.Empty;
			int num3 = 0;
			int num4 = area.Height / 2 - (int)panel.Parent.Font.Size / 2 - 1;
			HorizontalAlignment alignment = panel.Alignment;
			if (alignment != HorizontalAlignment.Right)
			{
				if (alignment != HorizontalAlignment.Center)
				{
					int num5 = area.Left + num;
					if (panel.Icon != null)
					{
						num3 = area.Left + 2;
						num5 = num3 + num2 + 2;
					}
					int num6 = num5;
					empty..ctor(num6, num4, area.Right - num6 - num, area.Bottom - num4 - num);
				}
				else
				{
					int num7 = (int)dc.MeasureString(text, panel.Parent.Font).Width;
					int num6 = area.Left + (panel.Width - num7) / 2;
					empty..ctor(num6, num4, area.Right - num6 - num, area.Bottom - num4 - num);
					if (panel.Icon != null)
					{
						num3 = num6 - num2 - 2;
					}
				}
			}
			else
			{
				int num7 = (int)dc.MeasureString(text, panel.Parent.Font).Width;
				int num6 = area.Right - num7 - 4;
				empty..ctor(num6, num4, area.Right - num6 - num, area.Bottom - num4 - num);
				if (panel.Icon != null)
				{
					num3 = num6 - num2 - 2;
				}
			}
			RectangleF clipBounds = dc.ClipBounds;
			dc.SetClip(area);
			dc.DrawString(text, panel.Parent.Font, br_forecolor, empty, stringFormat);
			dc.SetClip(clipBounds);
			if (panel.Icon != null)
			{
				dc.DrawIcon(panel.Icon, new Rectangle(num3, num4, num2, num2));
			}
		}

		// Token: 0x0600384F RID: 14415 RVA: 0x000E2AB0 File Offset: 0x000E0CB0
		protected virtual void DrawStatusBarPanelBackground(Graphics dc, Rectangle area, StatusBarPanel panel)
		{
			if (panel.BorderStyle != StatusBarPanelBorderStyle.None)
			{
				Border3DStyle border3DStyle = Border3DStyle.SunkenOuter;
				if (panel.BorderStyle == StatusBarPanelBorderStyle.Raised)
				{
					border3DStyle = Border3DStyle.RaisedInner;
				}
				this.CPDrawBorder3D(dc, area, border3DStyle, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom, panel.Parent.BackColor);
			}
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06003850 RID: 14416 RVA: 0x000E2AF0 File Offset: 0x000E0CF0
		public override int StatusBarSizeGripWidth
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x06003851 RID: 14417 RVA: 0x000E2AF4 File Offset: 0x000E0CF4
		public override int StatusBarHorzGapWidth
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x06003852 RID: 14418 RVA: 0x000E2AF8 File Offset: 0x000E0CF8
		public override Size StatusBarDefaultSize
		{
			get
			{
				return new Size(100, 22);
			}
		}

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06003853 RID: 14419 RVA: 0x000E2B04 File Offset: 0x000E0D04
		public override Size TabControlDefaultItemSize
		{
			get
			{
				return ThemeElements.CurrentTheme.TabControlPainter.DefaultItemSize;
			}
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06003854 RID: 14420 RVA: 0x000E2B18 File Offset: 0x000E0D18
		public override Point TabControlDefaultPadding
		{
			get
			{
				return ThemeElements.CurrentTheme.TabControlPainter.DefaultPadding;
			}
		}

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06003855 RID: 14421 RVA: 0x000E2B2C File Offset: 0x000E0D2C
		public override int TabControlMinimumTabWidth
		{
			get
			{
				return ThemeElements.CurrentTheme.TabControlPainter.MinimumTabWidth;
			}
		}

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06003856 RID: 14422 RVA: 0x000E2B40 File Offset: 0x000E0D40
		public override Rectangle TabControlSelectedDelta
		{
			get
			{
				return ThemeElements.CurrentTheme.TabControlPainter.SelectedTabDelta;
			}
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06003857 RID: 14423 RVA: 0x000E2B54 File Offset: 0x000E0D54
		public override int TabControlSelectedSpacing
		{
			get
			{
				return ThemeElements.CurrentTheme.TabControlPainter.SelectedSpacing;
			}
		}

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06003858 RID: 14424 RVA: 0x000E2B68 File Offset: 0x000E0D68
		public override int TabPanelOffsetX
		{
			get
			{
				return ThemeElements.CurrentTheme.TabControlPainter.TabPanelOffset.X;
			}
		}

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06003859 RID: 14425 RVA: 0x000E2B8C File Offset: 0x000E0D8C
		public override int TabPanelOffsetY
		{
			get
			{
				return ThemeElements.CurrentTheme.TabControlPainter.TabPanelOffset.Y;
			}
		}

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x0600385A RID: 14426 RVA: 0x000E2BB0 File Offset: 0x000E0DB0
		public override int TabControlColSpacing
		{
			get
			{
				return ThemeElements.CurrentTheme.TabControlPainter.ColSpacing;
			}
		}

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x0600385B RID: 14427 RVA: 0x000E2BC4 File Offset: 0x000E0DC4
		public override Point TabControlImagePadding
		{
			get
			{
				return ThemeElements.CurrentTheme.TabControlPainter.ImagePadding;
			}
		}

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x0600385C RID: 14428 RVA: 0x000E2BD8 File Offset: 0x000E0DD8
		public override int TabControlScrollerWidth
		{
			get
			{
				return ThemeElements.CurrentTheme.TabControlPainter.ScrollerWidth;
			}
		}

		// Token: 0x0600385D RID: 14429 RVA: 0x000E2BEC File Offset: 0x000E0DEC
		public override Size TabControlGetSpacing(TabControl tab)
		{
			Size size;
			try
			{
				size = ThemeElements.CurrentTheme.TabControlPainter.RowSpacing(tab);
			}
			catch
			{
				throw new Exception("Invalid Appearance value: " + tab.Appearance);
			}
			return size;
		}

		// Token: 0x0600385E RID: 14430 RVA: 0x000E2C54 File Offset: 0x000E0E54
		public override void DrawTabControl(Graphics dc, Rectangle area, TabControl tab)
		{
			ThemeElements.CurrentTheme.TabControlPainter.Draw(dc, area, tab);
		}

		// Token: 0x0600385F RID: 14431 RVA: 0x000E2C68 File Offset: 0x000E0E68
		public override Rectangle TabControlGetDisplayRectangle(TabControl tab)
		{
			return ThemeElements.CurrentTheme.TabControlPainter.GetDisplayRectangle(tab);
		}

		// Token: 0x06003860 RID: 14432 RVA: 0x000E2C7C File Offset: 0x000E0E7C
		public override Rectangle TabControlGetPanelRect(TabControl tab)
		{
			return ThemeElements.CurrentTheme.TabControlPainter.GetTabPanelRect(tab);
		}

		// Token: 0x06003861 RID: 14433 RVA: 0x000E2C90 File Offset: 0x000E0E90
		public override void TextBoxBaseFillBackground(TextBoxBase textBoxBase, Graphics g, Rectangle clippingArea)
		{
			if (textBoxBase.backcolor_set || (textBoxBase.Enabled && !textBoxBase.read_only))
			{
				g.FillRectangle(this.ResPool.GetSolidBrush(textBoxBase.BackColor), clippingArea);
			}
			else
			{
				g.FillRectangle(this.ResPool.GetSolidBrush(this.ColorControl), clippingArea);
			}
		}

		// Token: 0x06003862 RID: 14434 RVA: 0x000E2CF4 File Offset: 0x000E0EF4
		public override bool TextBoxBaseHandleWmNcPaint(TextBoxBase textBoxBase, ref Message m)
		{
			return false;
		}

		// Token: 0x06003863 RID: 14435 RVA: 0x000E2CF8 File Offset: 0x000E0EF8
		public override bool TextBoxBaseShouldPaintBackground(TextBoxBase textBoxBase)
		{
			return true;
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x000E2CFC File Offset: 0x000E0EFC
		public override void DrawToolBar(Graphics dc, Rectangle clip_rectangle, ToolBar control)
		{
			StringFormat stringFormat = new StringFormat();
			stringFormat.Trimming = 3;
			stringFormat.LineAlignment = 1;
			if (control.ShowKeyboardCuesInternal)
			{
				stringFormat.HotkeyPrefix = 1;
			}
			else
			{
				stringFormat.HotkeyPrefix = 2;
			}
			if (control.TextAlign == ToolBarTextAlign.Underneath)
			{
				stringFormat.Alignment = 1;
			}
			else
			{
				stringFormat.Alignment = 0;
			}
			if (control.Appearance != ToolBarAppearance.Flat || control.Parent == null)
			{
				dc.FillRectangle(SystemBrushes.Control, clip_rectangle);
			}
			if (control.Divider && clip_rectangle.Y < 2)
			{
				if (clip_rectangle.Y < 1)
				{
					dc.DrawLine(SystemPens.ControlDark, clip_rectangle.X, 0, clip_rectangle.Right, 0);
				}
				dc.DrawLine(SystemPens.ControlLightLight, clip_rectangle.X, 1, clip_rectangle.Right, 1);
			}
			foreach (ToolBarItem toolBarItem in control.items)
			{
				if (toolBarItem.Button.Visible && clip_rectangle.IntersectsWith(toolBarItem.Rectangle))
				{
					this.DrawToolBarButton(dc, control, toolBarItem, stringFormat);
				}
			}
			stringFormat.Dispose();
		}

		// Token: 0x06003865 RID: 14437 RVA: 0x000E2E28 File Offset: 0x000E1028
		protected virtual void DrawToolBarButton(Graphics dc, ToolBar control, ToolBarItem item, StringFormat format)
		{
			bool flag = control.Appearance == ToolBarAppearance.Flat;
			this.DrawToolBarButtonBorder(dc, item, flag);
			switch (item.Button.Style)
			{
			case ToolBarButtonStyle.ToggleButton:
				this.DrawToolBarToggleButtonBackground(dc, item);
				this.DrawToolBarButtonContents(dc, control, item, format);
				break;
			case ToolBarButtonStyle.Separator:
				if (flag)
				{
					this.DrawToolBarSeparator(dc, item);
				}
				break;
			case ToolBarButtonStyle.DropDownButton:
				if (control.DropDownArrows)
				{
					this.DrawToolBarDropDownArrow(dc, item, flag);
				}
				this.DrawToolBarButtonContents(dc, control, item, format);
				break;
			default:
				this.DrawToolBarButtonContents(dc, control, item, format);
				break;
			}
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x000E2ECC File Offset: 0x000E10CC
		protected virtual void DrawToolBarButtonBorder(Graphics dc, ToolBarItem item, bool is_flat)
		{
			if (item.Button.Style == ToolBarButtonStyle.Separator)
			{
				return;
			}
			Border3DStyle border3DStyle;
			if (is_flat)
			{
				if (item.Button.Pushed || item.Pressed)
				{
					border3DStyle = Border3DStyle.SunkenOuter;
				}
				else
				{
					if (!item.Hilight)
					{
						return;
					}
					border3DStyle = Border3DStyle.RaisedInner;
				}
			}
			else if (item.Button.Pushed || item.Pressed)
			{
				border3DStyle = Border3DStyle.Sunken;
			}
			else
			{
				border3DStyle = Border3DStyle.Raised;
			}
			Rectangle rectangle = item.Rectangle;
			if (item.Button.Style == ToolBarButtonStyle.DropDownButton && item.Button.Parent.DropDownArrows && is_flat)
			{
				rectangle.Width -= this.ToolBarDropDownWidth;
			}
			this.CPDrawBorder3D(dc, rectangle, border3DStyle, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
		}

		// Token: 0x06003867 RID: 14439 RVA: 0x000E2FA4 File Offset: 0x000E11A4
		protected virtual void DrawToolBarSeparator(Graphics dc, ToolBarItem item)
		{
			Rectangle rectangle = item.Rectangle;
			int num = (int)SystemPens.Control.Width + 1;
			dc.DrawLine(SystemPens.ControlDark, rectangle.X + 1, rectangle.Y, rectangle.X + 1, rectangle.Bottom);
			dc.DrawLine(SystemPens.ControlLight, rectangle.X + num, rectangle.Y, rectangle.X + num, rectangle.Bottom);
		}

		// Token: 0x06003868 RID: 14440 RVA: 0x000E301C File Offset: 0x000E121C
		protected virtual void DrawToolBarToggleButtonBackground(Graphics dc, ToolBarItem item)
		{
			Rectangle rectangle = item.Rectangle;
			rectangle.X += this.ToolBarImageGripWidth;
			rectangle.Y += this.ToolBarImageGripWidth;
			rectangle.Width -= 2 * this.ToolBarImageGripWidth;
			rectangle.Height -= 2 * this.ToolBarImageGripWidth;
			Brush brush;
			if (item.Button.Pushed)
			{
				brush = this.ResPool.GetHatchBrush(12, this.ColorScrollBar, this.ColorControlLightLight);
			}
			else if (item.Button.PartialPush)
			{
				brush = SystemBrushes.ControlLight;
			}
			else
			{
				brush = SystemBrushes.Control;
			}
			dc.FillRectangle(brush, rectangle);
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x000E30DC File Offset: 0x000E12DC
		protected virtual void DrawToolBarDropDownArrow(Graphics dc, ToolBarItem item, bool is_flat)
		{
			Rectangle rectangle = item.Rectangle;
			rectangle.X = item.Rectangle.Right - this.ToolBarDropDownWidth;
			rectangle.Width = this.ToolBarDropDownWidth;
			if (is_flat)
			{
				if (item.DDPressed)
				{
					this.CPDrawBorder3D(dc, rectangle, Border3DStyle.SunkenOuter, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
				}
				else if (item.Button.Pushed || item.Pressed)
				{
					this.CPDrawBorder3D(dc, rectangle, Border3DStyle.SunkenOuter, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
				}
				else if (item.Hilight)
				{
					this.CPDrawBorder3D(dc, rectangle, Border3DStyle.RaisedInner, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
				}
			}
			else if (item.DDPressed)
			{
				this.CPDrawBorder3D(dc, rectangle, Border3DStyle.Flat, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
			}
			else if (item.Button.Pushed || item.Pressed)
			{
				this.CPDrawBorder3D(dc, Rectangle.Inflate(rectangle, -1, -1), Border3DStyle.SunkenOuter, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
			}
			else
			{
				this.CPDrawBorder3D(dc, rectangle, Border3DStyle.Raised, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
			}
			PointF[] array = new PointF[3];
			PointF pointF;
			pointF..ctor((float)rectangle.X + (float)rectangle.Width / 2f, (float)(rectangle.Y + rectangle.Height / 2));
			if (item.Pressed || item.Button.Pushed || item.DDPressed)
			{
				pointF.X += 1f;
				pointF.Y += 1f;
			}
			array[0].X = pointF.X - (float)this.ToolBarDropDownArrowWidth / 2f + 0.5f;
			array[0].Y = pointF.Y;
			array[1].X = pointF.X + (float)this.ToolBarDropDownArrowWidth / 2f + 0.5f;
			array[1].Y = pointF.Y;
			array[2].X = pointF.X + 0.5f;
			array[2].Y = pointF.Y + (float)this.ToolBarDropDownArrowHeight;
			dc.FillPolygon(SystemBrushes.ControlText, array);
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x000E3314 File Offset: 0x000E1514
		protected virtual void DrawToolBarButtonContents(Graphics dc, ToolBar control, ToolBarItem item, StringFormat format)
		{
			if (item.Button.Image != null)
			{
				int num = item.ImageRectangle.X + this.ToolBarImageGripWidth;
				int num2 = item.ImageRectangle.Y + this.ToolBarImageGripWidth;
				if (item.Pressed || item.Button.Pushed)
				{
					num++;
					num2++;
				}
				if (item.Button.Enabled)
				{
					dc.DrawImage(item.Button.Image, num, num2);
				}
				else
				{
					this.CPDrawImageDisabled(dc, item.Button.Image, num, num2, this.ColorControl);
				}
			}
			Rectangle textRectangle = item.TextRectangle;
			if (textRectangle.Width <= 0 || textRectangle.Height <= 0)
			{
				return;
			}
			if (item.Pressed || item.Button.Pushed)
			{
				textRectangle.X++;
				textRectangle.Y++;
			}
			if (item.Button.Enabled)
			{
				dc.DrawString(item.Button.Text, control.Font, SystemBrushes.ControlText, textRectangle, format);
			}
			else
			{
				this.CPDrawStringDisabled(dc, item.Button.Text, control.Font, control.BackColor, textRectangle, format);
			}
		}

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x0600386B RID: 14443 RVA: 0x000E3480 File Offset: 0x000E1680
		public override int ToolBarGripWidth
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x0600386C RID: 14444 RVA: 0x000E3484 File Offset: 0x000E1684
		public override int ToolBarImageGripWidth
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x0600386D RID: 14445 RVA: 0x000E3488 File Offset: 0x000E1688
		public override int ToolBarSeparatorWidth
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x0600386E RID: 14446 RVA: 0x000E348C File Offset: 0x000E168C
		public override int ToolBarDropDownWidth
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x0600386F RID: 14447 RVA: 0x000E3490 File Offset: 0x000E1690
		public override int ToolBarDropDownArrowWidth
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06003870 RID: 14448 RVA: 0x000E3494 File Offset: 0x000E1694
		public override int ToolBarDropDownArrowHeight
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06003871 RID: 14449 RVA: 0x000E3498 File Offset: 0x000E1698
		public override Size ToolBarDefaultSize
		{
			get
			{
				return new Size(100, 42);
			}
		}

		// Token: 0x06003872 RID: 14450 RVA: 0x000E34A4 File Offset: 0x000E16A4
		public override bool ToolBarHasHotElementStyles(ToolBar toolBar)
		{
			return toolBar.Appearance == ToolBarAppearance.Flat;
		}

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06003873 RID: 14451 RVA: 0x000E34B0 File Offset: 0x000E16B0
		public override bool ToolBarHasHotCheckedElementStyles
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003874 RID: 14452 RVA: 0x000E34B4 File Offset: 0x000E16B4
		public override void DrawToolTip(Graphics dc, Rectangle clip_rectangle, ToolTip.ToolTipWindow control)
		{
			this.ToolTipDrawBackground(dc, clip_rectangle, control);
			TextFormatFlags textFormatFlags = TextFormatFlags.HidePrefix;
			Color foreColor = control.ForeColor;
			if (control.title.Length > 0)
			{
				Font font = new Font(control.Font, control.Font.Style | 1);
				TextRenderer.DrawTextInternal(dc, control.title, font, control.title_rect, foreColor, textFormatFlags, false);
				font.Dispose();
			}
			if (control.icon != null)
			{
				dc.DrawIcon(control.icon, control.icon_rect);
			}
			TextRenderer.DrawTextInternal(dc, control.Text, control.Font, control.text_rect, foreColor, textFormatFlags, false);
		}

		// Token: 0x06003875 RID: 14453 RVA: 0x000E3558 File Offset: 0x000E1758
		protected virtual void ToolTipDrawBackground(Graphics dc, Rectangle clip_rectangle, ToolTip.ToolTipWindow control)
		{
			Brush solidBrush = this.ResPool.GetSolidBrush(control.BackColor);
			dc.FillRectangle(solidBrush, control.ClientRectangle);
			dc.DrawRectangle(SystemPens.WindowFrame, 0, 0, control.Width - 1, control.Height - 1);
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x000E35A4 File Offset: 0x000E17A4
		public override Size ToolTipSize(ToolTip.ToolTipWindow tt, string text)
		{
			Size size = TextRenderer.MeasureTextInternal(text, tt.Font, false);
			size.Width += 4;
			size.Height += 3;
			Rectangle rectangle;
			rectangle..ctor(Point.Empty, size);
			rectangle.Inflate(-2, -1);
			tt.text_rect = rectangle;
			tt.icon_rect = (tt.title_rect = Rectangle.Empty);
			Size size2 = Size.Empty;
			if (tt.title.Length > 0)
			{
				Font font = new Font(tt.Font, tt.Font.Style | 1);
				size2 = TextRenderer.MeasureTextInternal(tt.title, font, false);
				font.Dispose();
			}
			Size empty = Size.Empty;
			if (tt.icon != null)
			{
				empty..ctor(size.Height, size.Height);
			}
			if (empty != Size.Empty || size2 != Size.Empty)
			{
				int num = 8;
				int num2 = 0;
				int num3 = ((empty.Height <= size2.Height) ? size2.Height : empty.Height);
				Size size3 = size;
				Point point;
				point..ctor(num, num);
				if (empty != Size.Empty)
				{
					tt.icon_rect = new Rectangle(point, empty);
					num2 = empty.Width + num;
				}
				if (size2 != Size.Empty)
				{
					Rectangle rectangle2;
					rectangle2..ctor(point, new Size(size2.Width, num3));
					if (empty != Size.Empty)
					{
						rectangle2.X += empty.Width + num;
					}
					tt.title_rect = rectangle2;
					num2 += size2.Width;
				}
				tt.text_rect = new Rectangle(new Point(point.X, point.Y + num3 + num), size3);
				size.Height += num + num3;
				if (num2 > size.Width)
				{
					size.Width = num2;
				}
				size.Width += num * 2;
				size.Height += num * 2;
			}
			return size;
		}

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06003877 RID: 14455 RVA: 0x000E37E0 File Offset: 0x000E19E0
		public override bool ToolTipTransparentBackground
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003878 RID: 14456 RVA: 0x000E37E4 File Offset: 0x000E19E4
		public override void ShowBalloonWindow(IntPtr handle, int timeout, string title, string text, ToolTipIcon icon)
		{
			if (Control.FromHandle(handle) == null)
			{
				return;
			}
			if (this.balloon_window != null)
			{
				this.balloon_window.Close();
				this.balloon_window.Dispose();
			}
			this.balloon_window = new NotifyIcon.BalloonWindow(handle);
			this.balloon_window.Title = title;
			this.balloon_window.Text = text;
			this.balloon_window.Icon = icon;
			this.balloon_window.Timeout = timeout;
			this.balloon_window.Show();
		}

		// Token: 0x06003879 RID: 14457 RVA: 0x000E386C File Offset: 0x000E1A6C
		public override void DrawBalloonWindow(Graphics dc, Rectangle clip, NotifyIcon.BalloonWindow control)
		{
			Brush solidBrush = this.ResPool.GetSolidBrush(this.ColorInfoText);
			Rectangle clientRectangle = control.ClientRectangle;
			int num = ((control.Icon != ToolTipIcon.None) ? 16 : 0);
			dc.FillRectangle(this.ResPool.GetSolidBrush(this.ColorInfo), clientRectangle);
			dc.DrawRectangle(this.ResPool.GetPen(this.ColorWindowFrame), 0, 0, clientRectangle.Width - 1, clientRectangle.Height - 1);
			Image image;
			switch (control.Icon)
			{
			case ToolTipIcon.Info:
				image = ThemeEngine.Current.Images(UIIcon.MessageBoxInfo, 16);
				break;
			case ToolTipIcon.Warning:
				image = ThemeEngine.Current.Images(UIIcon.MessageBoxError, 16);
				break;
			case ToolTipIcon.Error:
				image = ThemeEngine.Current.Images(UIIcon.MessageBoxWarning, 16);
				break;
			default:
				image = null;
				break;
			}
			if (control.Icon != ToolTipIcon.None)
			{
				dc.DrawImage(image, new Rectangle(8, 8, num, num));
			}
			Rectangle rectangle;
			rectangle..ctor(clientRectangle.X + 8 + num + ((num <= 0) ? 0 : 8), clientRectangle.Y + 8, clientRectangle.Width - (24 + num), clientRectangle.Height - 16);
			Font font = new Font(control.Font.FontFamily, control.Font.Size, control.Font.Style | 1, control.Font.Unit);
			dc.DrawString(control.Title, font, solidBrush, rectangle, control.Format);
			Rectangle rectangle2;
			rectangle2..ctor(clientRectangle.X + 8, clientRectangle.Y + 8, clientRectangle.Width - 16, clientRectangle.Height - 16);
			StringFormat format = control.Format;
			format.LineAlignment = 2;
			dc.DrawString(control.Text, control.Font, solidBrush, rectangle2, format);
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x000E3A50 File Offset: 0x000E1C50
		public override Rectangle BalloonWindowRect(NotifyIcon.BalloonWindow control)
		{
			Rectangle workingArea = Screen.GetWorkingArea(control);
			SizeF sizeF;
			sizeF..ctor(250f, 200f);
			SizeF sizeF2 = TextRenderer.MeasureString(control.Title, control.Font, sizeF, control.Format);
			SizeF sizeF3 = TextRenderer.MeasureString(control.Text, control.Font, sizeF, control.Format);
			if (sizeF2.Height < 16f)
			{
				sizeF2.Height = 16f;
			}
			Rectangle rectangle = default(Rectangle);
			rectangle.Height = (int)(sizeF2.Height + sizeF3.Height + 24f);
			rectangle.Width = (int)((sizeF2.Width <= sizeF3.Width) ? sizeF3.Width : sizeF2.Width) + 16;
			rectangle.X = workingArea.Width - rectangle.Width - 2;
			rectangle.Y = workingArea.Height - rectangle.Height - 2;
			return rectangle;
		}

		// Token: 0x0600387B RID: 14459 RVA: 0x000E3B4C File Offset: 0x000E1D4C
		public override int TrackBarValueFromMousePosition(int x, int y, TrackBar tb)
		{
			int num = tb.Value;
			int num2 = tb.Value;
			Rectangle empty = Rectangle.Empty;
			Rectangle empty2 = Rectangle.Empty;
			Point empty3 = Point.Empty;
			Point empty4 = Point.Empty;
			float num3;
			this.GetTrackBarDrawingInfo(tb, out num3, out empty2, out empty, out empty3, out empty4, out empty4);
			if (tb.Orientation == Orientation.Vertical)
			{
				num2 = (int)Math.Round((double)(((float)(empty2.Bottom - y) - (float)empty.Height / 2f) / num3), 0);
				if (num2 + tb.Minimum > tb.Maximum)
				{
					num2 = tb.Maximum - tb.Minimum;
				}
				else if (num2 + tb.Minimum < tb.Minimum)
				{
					num2 = 0;
				}
				num = num2 + tb.Minimum;
			}
			else
			{
				num2 = (int)Math.Round((double)(((float)(x - empty3.X) - (float)empty.Width / 2f) / num3), 0);
				if (num2 + tb.Minimum > tb.Maximum)
				{
					num2 = tb.Maximum - tb.Minimum;
				}
				else if (num2 + tb.Minimum < tb.Minimum)
				{
					num2 = 0;
				}
				num = num2 + tb.Minimum;
			}
			return num;
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x000E3C7C File Offset: 0x000E1E7C
		private void GetTrackBarDrawingInfo(TrackBar tb, out float pixels_betweenticks, out Rectangle thumb_area, out Rectangle thumb_pos, out Point channel_startpoint, out Point bottomtick_startpoint, out Point toptick_startpoint)
		{
			thumb_area = Rectangle.Empty;
			thumb_pos = Rectangle.Empty;
			if (tb.Orientation == Orientation.Vertical)
			{
				toptick_startpoint = default(Point);
				bottomtick_startpoint = default(Point);
				channel_startpoint = default(Point);
				Rectangle clientRectangle = tb.ClientRectangle;
				switch (tb.TickStyle)
				{
				case TickStyle.None:
				case TickStyle.BottomRight:
					channel_startpoint.Y = 8;
					channel_startpoint.X = 9;
					bottomtick_startpoint.Y = 13;
					bottomtick_startpoint.X = 24;
					break;
				case TickStyle.TopLeft:
					channel_startpoint.Y = 8;
					channel_startpoint.X = 19;
					toptick_startpoint.Y = 13;
					toptick_startpoint.X = 8;
					break;
				case TickStyle.Both:
					channel_startpoint.Y = 8;
					channel_startpoint.X = 18;
					bottomtick_startpoint.Y = 13;
					bottomtick_startpoint.X = 32;
					toptick_startpoint.Y = 13;
					toptick_startpoint.X = 8;
					break;
				}
				thumb_area.X = clientRectangle.X + channel_startpoint.X;
				thumb_area.Y = clientRectangle.Y + channel_startpoint.Y;
				thumb_area.Height = clientRectangle.Height - 8 - 8;
				thumb_area.Width = 4;
				float num = (float)(thumb_area.Height - 11);
				if (tb.Maximum == tb.Minimum)
				{
					pixels_betweenticks = 0f;
				}
				else
				{
					pixels_betweenticks = num / (float)(tb.Maximum - tb.Minimum);
				}
				thumb_pos.Y = thumb_area.Bottom - 11 - (int)(pixels_betweenticks * (float)(tb.Value - tb.Minimum));
			}
			else
			{
				toptick_startpoint = default(Point);
				bottomtick_startpoint = default(Point);
				channel_startpoint = default(Point);
				Rectangle clientRectangle2 = tb.ClientRectangle;
				switch (tb.TickStyle)
				{
				case TickStyle.None:
				case TickStyle.BottomRight:
					channel_startpoint.X = 8;
					channel_startpoint.Y = 9;
					bottomtick_startpoint.X = 13;
					bottomtick_startpoint.Y = 24;
					break;
				case TickStyle.TopLeft:
					channel_startpoint.X = 8;
					channel_startpoint.Y = 19;
					toptick_startpoint.X = 13;
					toptick_startpoint.Y = 8;
					break;
				case TickStyle.Both:
					channel_startpoint.X = 8;
					channel_startpoint.Y = 18;
					bottomtick_startpoint.X = 13;
					bottomtick_startpoint.Y = 32;
					toptick_startpoint.X = 13;
					toptick_startpoint.Y = 8;
					break;
				}
				thumb_area.X = clientRectangle2.X + channel_startpoint.X;
				thumb_area.Y = clientRectangle2.Y + channel_startpoint.Y;
				thumb_area.Width = clientRectangle2.Width - 8 - 8;
				thumb_area.Height = 4;
				float num2 = (float)(thumb_area.Width - 11);
				if (tb.Maximum == tb.Minimum)
				{
					pixels_betweenticks = 0f;
				}
				else
				{
					pixels_betweenticks = num2 / (float)(tb.Maximum - tb.Minimum);
				}
				thumb_pos.X = channel_startpoint.X + (int)(pixels_betweenticks * (float)(tb.Value - tb.Minimum));
			}
			thumb_pos.Size = this.TrackBarGetThumbSize(tb);
		}

		// Token: 0x0600387D RID: 14461 RVA: 0x000E3FA0 File Offset: 0x000E21A0
		protected virtual Size TrackBarGetThumbSize(TrackBar trackBar)
		{
			return ThemeWin32Classic.TrackBarGetThumbSize();
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x000E3FA8 File Offset: 0x000E21A8
		public static Size TrackBarGetThumbSize()
		{
			return new Size(10, 22);
		}

		// Token: 0x0600387F RID: 14463 RVA: 0x000E3FB4 File Offset: 0x000E21B4
		protected virtual ThemeWin32Classic.ITrackBarTickPainter GetTrackBarTickPainter(Graphics g)
		{
			return new ThemeWin32Classic.TrackBarTickPainter(g, this.ResPool.GetPen(ThemeWin32Classic.pen_ticks_color));
		}

		// Token: 0x06003880 RID: 14464 RVA: 0x000E3FCC File Offset: 0x000E21CC
		private void DrawTrackBar_Vertical(Graphics dc, Rectangle clip_rectangle, TrackBar tb, ref Rectangle thumb_pos, ref Rectangle thumb_area, Brush br_thumb, float ticks, int value_pos, bool mouse_value)
		{
			Point point = default(Point);
			Point point2 = default(Point);
			Point point3 = default(Point);
			Rectangle clientRectangle = tb.ClientRectangle;
			float num;
			this.GetTrackBarDrawingInfo(tb, out num, out thumb_area, out thumb_pos, out point3, out point2, out point);
			this.TrackBarDrawVerticalTrack(dc, thumb_area, point3, clip_rectangle);
			switch (tb.TickStyle)
			{
			case TickStyle.None:
			case TickStyle.BottomRight:
				thumb_pos.X = point3.X - 8;
				this.TrackBarDrawVerticalThumbRight(dc, thumb_pos, br_thumb, clip_rectangle, tb);
				break;
			case TickStyle.TopLeft:
				thumb_pos.X = point3.X - 10;
				this.TrackBarDrawVerticalThumbLeft(dc, thumb_pos, br_thumb, clip_rectangle, tb);
				break;
			default:
				thumb_pos.X = clientRectangle.X + 10;
				this.TrackBarDrawVerticalThumb(dc, thumb_pos, br_thumb, clip_rectangle, tb);
				break;
			}
			float num2 = (float)(thumb_area.Height - 11);
			num = num2 / ticks;
			thumb_area.X = thumb_pos.X;
			thumb_area.Y = point3.Y;
			thumb_area.Width = thumb_pos.Height;
			if (num <= 0f)
			{
				return;
			}
			if (tb.TickStyle == TickStyle.None)
			{
				return;
			}
			Region region = new Region(clientRectangle);
			region.Exclude(thumb_area);
			if (region.IsVisible(clip_rectangle))
			{
				ThemeWin32Classic.ITrackBarTickPainter trackBarTickPainter = this.TrackBarGetVerticalTickPainter(dc);
				if ((tb.TickStyle & TickStyle.BottomRight) == TickStyle.BottomRight)
				{
					float num3 = (float)(clientRectangle.X + point2.X);
					for (float num4 = 0f; num4 < num2 + 1f; num4 += num)
					{
						float num5 = (float)(clientRectangle.Y + point2.Y) + num4;
						trackBarTickPainter.Paint(num3, num5, num3 + (float)((num4 != 0f && num4 + num < num2 + 1f) ? 2 : 3), num5);
					}
				}
				if ((tb.TickStyle & TickStyle.TopLeft) == TickStyle.TopLeft)
				{
					float num6 = (float)(clientRectangle.X + point.X);
					for (float num7 = 0f; num7 < num2 + 1f; num7 += num)
					{
						float num8 = (float)(clientRectangle.Y + point.Y) + num7;
						trackBarTickPainter.Paint(num6 - (float)((num7 != 0f && num7 + num < num2 + 1f) ? 2 : 3), num8, num6, num8);
					}
				}
			}
			region.Dispose();
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x000E4254 File Offset: 0x000E2454
		protected virtual void TrackBarDrawVerticalTrack(Graphics dc, Rectangle thumb_area, Point channel_startpoint, Rectangle clippingArea)
		{
			dc.FillRectangle(SystemBrushes.ControlDark, channel_startpoint.X, channel_startpoint.Y, 1, thumb_area.Height);
			dc.FillRectangle(SystemBrushes.ControlDarkDark, channel_startpoint.X + 1, channel_startpoint.Y, 1, thumb_area.Height);
			dc.FillRectangle(SystemBrushes.ControlLight, channel_startpoint.X + 3, channel_startpoint.Y, 1, thumb_area.Height);
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x000E42C8 File Offset: 0x000E24C8
		protected virtual void TrackBarDrawVerticalThumbRight(Graphics dc, Rectangle thumb_pos, Brush br_thumb, Rectangle clippingArea, TrackBar trackBar)
		{
			Pen pen = SystemPens.ControlLightLight;
			dc.DrawLine(pen, thumb_pos.X, thumb_pos.Y, thumb_pos.X, thumb_pos.Y + 10);
			dc.DrawLine(pen, thumb_pos.X, thumb_pos.Y, thumb_pos.X + 16, thumb_pos.Y);
			dc.DrawLine(pen, thumb_pos.X + 16, thumb_pos.Y, thumb_pos.X + 16 + 4, thumb_pos.Y + 4);
			pen = SystemPens.ControlDark;
			dc.DrawLine(pen, thumb_pos.X + 1, thumb_pos.Y + 9, thumb_pos.X + 15, thumb_pos.Y + 9);
			dc.DrawLine(pen, thumb_pos.X + 16, thumb_pos.Y + 9, thumb_pos.X + 16 + 4, thumb_pos.Y + 9 - 4);
			pen = SystemPens.ControlDarkDark;
			dc.DrawLine(pen, thumb_pos.X, thumb_pos.Y + 10, thumb_pos.X + 16, thumb_pos.Y + 10);
			dc.DrawLine(pen, thumb_pos.X + 16, thumb_pos.Y + 10, thumb_pos.X + 16 + 5, thumb_pos.Y + 10 - 5);
			dc.FillRectangle(br_thumb, thumb_pos.X + 1, thumb_pos.Y + 1, 16, 8);
			dc.FillRectangle(br_thumb, thumb_pos.X + 17, thumb_pos.Y + 2, 1, 6);
			dc.FillRectangle(br_thumb, thumb_pos.X + 18, thumb_pos.Y + 3, 1, 4);
			dc.FillRectangle(br_thumb, thumb_pos.X + 19, thumb_pos.Y + 4, 1, 2);
		}

		// Token: 0x06003883 RID: 14467 RVA: 0x000E4490 File Offset: 0x000E2690
		protected virtual void TrackBarDrawVerticalThumbLeft(Graphics dc, Rectangle thumb_pos, Brush br_thumb, Rectangle clippingArea, TrackBar trackBar)
		{
			Pen pen = SystemPens.ControlLightLight;
			dc.DrawLine(pen, thumb_pos.X + 4, thumb_pos.Y, thumb_pos.X + 4 + 16, thumb_pos.Y);
			dc.DrawLine(pen, thumb_pos.X + 4, thumb_pos.Y, thumb_pos.X, thumb_pos.Y + 4);
			pen = SystemPens.ControlDark;
			dc.DrawLine(pen, thumb_pos.X + 4, thumb_pos.Y + 9, thumb_pos.X + 4 + 16, thumb_pos.Y + 9);
			dc.DrawLine(pen, thumb_pos.X + 4, thumb_pos.Y + 9, thumb_pos.X, thumb_pos.Y + 5);
			dc.DrawLine(pen, thumb_pos.X + 19, thumb_pos.Y + 9, thumb_pos.X + 19, thumb_pos.Y + 1);
			pen = SystemPens.ControlDarkDark;
			dc.DrawLine(pen, thumb_pos.X + 4, thumb_pos.Y + 10, thumb_pos.X + 4 + 16, thumb_pos.Y + 10);
			dc.DrawLine(pen, thumb_pos.X + 4, thumb_pos.Y + 10, thumb_pos.X - 1, thumb_pos.Y + 5);
			dc.DrawLine(pen, thumb_pos.X + 20, thumb_pos.Y, thumb_pos.X + 20, thumb_pos.Y + 10);
			dc.FillRectangle(br_thumb, thumb_pos.X + 4, thumb_pos.Y + 1, 15, 8);
			dc.FillRectangle(br_thumb, thumb_pos.X + 3, thumb_pos.Y + 2, 1, 6);
			dc.FillRectangle(br_thumb, thumb_pos.X + 2, thumb_pos.Y + 3, 1, 4);
			dc.FillRectangle(br_thumb, thumb_pos.X + 1, thumb_pos.Y + 4, 1, 2);
		}

		// Token: 0x06003884 RID: 14468 RVA: 0x000E4680 File Offset: 0x000E2880
		protected virtual void TrackBarDrawVerticalThumb(Graphics dc, Rectangle thumb_pos, Brush br_thumb, Rectangle clippingArea, TrackBar trackBar)
		{
			Pen pen = SystemPens.ControlLightLight;
			dc.DrawLine(pen, thumb_pos.X, thumb_pos.Y, thumb_pos.X, thumb_pos.Y + 9);
			dc.DrawLine(pen, thumb_pos.X, thumb_pos.Y, thumb_pos.X + 19, thumb_pos.Y);
			pen = SystemPens.ControlDark;
			dc.DrawLine(pen, thumb_pos.X + 1, thumb_pos.Y + 9, thumb_pos.X + 19, thumb_pos.Y + 9);
			dc.DrawLine(pen, thumb_pos.X + 10, thumb_pos.Y + 1, thumb_pos.X + 19, thumb_pos.Y + 8);
			pen = SystemPens.ControlDarkDark;
			dc.DrawLine(pen, thumb_pos.X, thumb_pos.Y + 10, thumb_pos.X + 20, thumb_pos.Y + 10);
			dc.DrawLine(pen, thumb_pos.X + 20, thumb_pos.Y, thumb_pos.X + 20, thumb_pos.Y + 9);
			dc.FillRectangle(br_thumb, thumb_pos.X + 1, thumb_pos.Y + 1, 18, 8);
		}

		// Token: 0x06003885 RID: 14469 RVA: 0x000E47BC File Offset: 0x000E29BC
		protected virtual ThemeWin32Classic.ITrackBarTickPainter TrackBarGetVerticalTickPainter(Graphics g)
		{
			return this.GetTrackBarTickPainter(g);
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x000E47C8 File Offset: 0x000E29C8
		private void DrawTrackBar_Horizontal(Graphics dc, Rectangle clip_rectangle, TrackBar tb, ref Rectangle thumb_pos, ref Rectangle thumb_area, Brush br_thumb, float ticks, int value_pos, bool mouse_value)
		{
			Point point = default(Point);
			Point point2 = default(Point);
			Point point3 = default(Point);
			Rectangle clientRectangle = tb.ClientRectangle;
			float num;
			this.GetTrackBarDrawingInfo(tb, out num, out thumb_area, out thumb_pos, out point3, out point2, out point);
			this.TrackBarDrawHorizontalTrack(dc, thumb_area, point3, clip_rectangle);
			switch (tb.TickStyle)
			{
			case TickStyle.None:
			case TickStyle.BottomRight:
				thumb_pos.Y = point3.Y - 8;
				this.TrackBarDrawHorizontalThumbBottom(dc, thumb_pos, br_thumb, clip_rectangle, tb);
				break;
			case TickStyle.TopLeft:
				thumb_pos.Y = point3.Y - 10;
				this.TrackBarDrawHorizontalThumbTop(dc, thumb_pos, br_thumb, clip_rectangle, tb);
				break;
			default:
				thumb_pos.Y = clientRectangle.Y + 10;
				this.TrackBarDrawHorizontalThumb(dc, thumb_pos, br_thumb, clip_rectangle, tb);
				break;
			}
			float num2 = (float)(thumb_area.Width - 11);
			num = num2 / ticks;
			thumb_area.Y = thumb_pos.Y;
			thumb_area.X = point3.X;
			thumb_area.Height = thumb_pos.Height;
			if (num <= 0f)
			{
				return;
			}
			if (tb.TickStyle == TickStyle.None)
			{
				return;
			}
			Region region = new Region(clientRectangle);
			region.Exclude(thumb_area);
			if (region.IsVisible(clip_rectangle))
			{
				ThemeWin32Classic.ITrackBarTickPainter trackBarTickPainter = this.TrackBarGetHorizontalTickPainter(dc);
				if ((tb.TickStyle & TickStyle.BottomRight) == TickStyle.BottomRight)
				{
					float num3 = (float)(clientRectangle.Y + point2.Y);
					for (float num4 = 0f; num4 < num2 + 1f; num4 += num)
					{
						float num5 = (float)(clientRectangle.X + point2.X) + num4;
						trackBarTickPainter.Paint(num5, num3, num5, num3 + (float)((num4 != 0f && num4 + num < num2 + 1f) ? 2 : 3));
					}
				}
				if ((tb.TickStyle & TickStyle.TopLeft) == TickStyle.TopLeft)
				{
					float num6 = (float)(clientRectangle.Y + point.Y);
					for (float num7 = 0f; num7 < num2 + 1f; num7 += num)
					{
						float num8 = (float)(clientRectangle.X + point.X) + num7;
						trackBarTickPainter.Paint(num8, num6 - (float)((num7 != 0f && num7 + num < num2 + 1f) ? 2 : 3), num8, num6);
					}
				}
			}
			region.Dispose();
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x000E4A50 File Offset: 0x000E2C50
		protected virtual void TrackBarDrawHorizontalTrack(Graphics dc, Rectangle thumb_area, Point channel_startpoint, Rectangle clippingArea)
		{
			dc.FillRectangle(SystemBrushes.ControlDark, channel_startpoint.X, channel_startpoint.Y, thumb_area.Width, 1);
			dc.FillRectangle(SystemBrushes.ControlDarkDark, channel_startpoint.X, channel_startpoint.Y + 1, thumb_area.Width, 1);
			dc.FillRectangle(SystemBrushes.ControlLight, channel_startpoint.X, channel_startpoint.Y + 3, thumb_area.Width, 1);
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x000E4AC4 File Offset: 0x000E2CC4
		protected virtual void TrackBarDrawHorizontalThumbBottom(Graphics dc, Rectangle thumb_pos, Brush br_thumb, Rectangle clippingArea, TrackBar trackBar)
		{
			Pen pen = SystemPens.ControlLightLight;
			dc.DrawLine(pen, thumb_pos.X, thumb_pos.Y, thumb_pos.X + 10, thumb_pos.Y);
			dc.DrawLine(pen, thumb_pos.X, thumb_pos.Y, thumb_pos.X, thumb_pos.Y + 16);
			dc.DrawLine(pen, thumb_pos.X, thumb_pos.Y + 16, thumb_pos.X + 4, thumb_pos.Y + 16 + 4);
			pen = SystemPens.ControlDark;
			dc.DrawLine(pen, thumb_pos.X + 9, thumb_pos.Y + 1, thumb_pos.X + 9, thumb_pos.Y + 15);
			dc.DrawLine(pen, thumb_pos.X + 9, thumb_pos.Y + 16, thumb_pos.X + 9 - 4, thumb_pos.Y + 16 + 4);
			pen = SystemPens.ControlDarkDark;
			dc.DrawLine(pen, thumb_pos.X + 10, thumb_pos.Y, thumb_pos.X + 10, thumb_pos.Y + 16);
			dc.DrawLine(pen, thumb_pos.X + 10, thumb_pos.Y + 16, thumb_pos.X + 10 - 5, thumb_pos.Y + 16 + 5);
			dc.FillRectangle(br_thumb, thumb_pos.X + 1, thumb_pos.Y + 1, 8, 16);
			dc.FillRectangle(br_thumb, thumb_pos.X + 2, thumb_pos.Y + 17, 6, 1);
			dc.FillRectangle(br_thumb, thumb_pos.X + 3, thumb_pos.Y + 18, 4, 1);
			dc.FillRectangle(br_thumb, thumb_pos.X + 4, thumb_pos.Y + 19, 2, 1);
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x000E4C8C File Offset: 0x000E2E8C
		protected virtual void TrackBarDrawHorizontalThumbTop(Graphics dc, Rectangle thumb_pos, Brush br_thumb, Rectangle clippingArea, TrackBar trackBar)
		{
			Pen pen = SystemPens.ControlLightLight;
			dc.DrawLine(pen, thumb_pos.X, thumb_pos.Y + 4, thumb_pos.X, thumb_pos.Y + 4 + 16);
			dc.DrawLine(pen, thumb_pos.X, thumb_pos.Y + 4, thumb_pos.X + 4, thumb_pos.Y);
			pen = SystemPens.ControlDark;
			dc.DrawLine(pen, thumb_pos.X + 9, thumb_pos.Y + 4, thumb_pos.X + 9, thumb_pos.Y + 4 + 16);
			dc.DrawLine(pen, thumb_pos.X + 9, thumb_pos.Y + 4, thumb_pos.X + 5, thumb_pos.Y);
			dc.DrawLine(pen, thumb_pos.X + 9, thumb_pos.Y + 19, thumb_pos.X + 1, thumb_pos.Y + 19);
			pen = SystemPens.ControlDarkDark;
			dc.DrawLine(pen, thumb_pos.X + 10, thumb_pos.Y + 4, thumb_pos.X + 10, thumb_pos.Y + 4 + 16);
			dc.DrawLine(pen, thumb_pos.X + 10, thumb_pos.Y + 4, thumb_pos.X + 5, thumb_pos.Y - 1);
			dc.DrawLine(pen, thumb_pos.X, thumb_pos.Y + 20, thumb_pos.X + 10, thumb_pos.Y + 20);
			dc.FillRectangle(br_thumb, thumb_pos.X + 1, thumb_pos.Y + 4, 8, 15);
			dc.FillRectangle(br_thumb, thumb_pos.X + 2, thumb_pos.Y + 3, 6, 1);
			dc.FillRectangle(br_thumb, thumb_pos.X + 3, thumb_pos.Y + 2, 4, 1);
			dc.FillRectangle(br_thumb, thumb_pos.X + 4, thumb_pos.Y + 1, 2, 1);
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x000E4E7C File Offset: 0x000E307C
		protected virtual void TrackBarDrawHorizontalThumb(Graphics dc, Rectangle thumb_pos, Brush br_thumb, Rectangle clippingArea, TrackBar trackBar)
		{
			Pen pen = SystemPens.ControlLightLight;
			dc.DrawLine(pen, thumb_pos.X, thumb_pos.Y, thumb_pos.X + 9, thumb_pos.Y);
			dc.DrawLine(pen, thumb_pos.X, thumb_pos.Y, thumb_pos.X, thumb_pos.Y + 19);
			pen = SystemPens.ControlDark;
			dc.DrawLine(pen, thumb_pos.X + 9, thumb_pos.Y + 1, thumb_pos.X + 9, thumb_pos.Y + 19);
			dc.DrawLine(pen, thumb_pos.X + 1, thumb_pos.Y + 10, thumb_pos.X + 8, thumb_pos.Y + 19);
			pen = SystemPens.ControlDarkDark;
			dc.DrawLine(pen, thumb_pos.X + 10, thumb_pos.Y, thumb_pos.X + 10, thumb_pos.Y + 20);
			dc.DrawLine(pen, thumb_pos.X, thumb_pos.Y + 20, thumb_pos.X + 9, thumb_pos.Y + 20);
			dc.FillRectangle(br_thumb, thumb_pos.X + 1, thumb_pos.Y + 1, 8, 18);
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x000E4FB8 File Offset: 0x000E31B8
		protected virtual ThemeWin32Classic.ITrackBarTickPainter TrackBarGetHorizontalTickPainter(Graphics g)
		{
			return this.GetTrackBarTickPainter(g);
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x000E4FC4 File Offset: 0x000E31C4
		public override void DrawTrackBar(Graphics dc, Rectangle clip_rectangle, TrackBar tb)
		{
			float num = (float)((tb.Maximum - tb.Minimum) / tb.tickFrequency);
			Rectangle thumbPos = tb.ThumbPos;
			Rectangle thumbArea = tb.ThumbArea;
			int num2;
			bool flag;
			if (tb.thumb_pressed)
			{
				num2 = tb.thumb_mouseclick;
				flag = true;
			}
			else
			{
				num2 = tb.Value - tb.Minimum;
				flag = false;
			}
			Rectangle clientRectangle = tb.ClientRectangle;
			Brush brush;
			if (!tb.Enabled)
			{
				brush = this.ResPool.GetHatchBrush(12, this.ColorControlLightLight, this.ColorControlLight);
			}
			else if (tb.thumb_pressed)
			{
				brush = this.ResPool.GetHatchBrush(12, this.ColorControlLight, this.ColorControl);
			}
			else
			{
				brush = SystemBrushes.Control;
			}
			if (tb.BackColor.ToArgb() == this.DefaultControlBackColor.ToArgb())
			{
				dc.FillRectangle(SystemBrushes.Control, clip_rectangle);
			}
			else
			{
				dc.FillRectangle(this.ResPool.GetSolidBrush(tb.BackColor), clip_rectangle);
			}
			if (tb.Focused)
			{
				this.CPDrawFocusRectangle(dc, clientRectangle, tb.ForeColor, tb.BackColor);
			}
			if (tb.Orientation == Orientation.Vertical)
			{
				this.DrawTrackBar_Vertical(dc, clip_rectangle, tb, ref thumbPos, ref thumbArea, brush, num, num2, flag);
			}
			else
			{
				this.DrawTrackBar_Horizontal(dc, clip_rectangle, tb, ref thumbPos, ref thumbArea, brush, num, num2, flag);
			}
			tb.ThumbPos = thumbPos;
			tb.ThumbArea = thumbArea;
		}

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x0600388D RID: 14477 RVA: 0x000E5134 File Offset: 0x000E3334
		public override Size TrackBarDefaultSize
		{
			get
			{
				return new Size(104, 42);
			}
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x0600388E RID: 14478 RVA: 0x000E5140 File Offset: 0x000E3340
		public override bool TrackBarHasHotThumbStyle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x000E5144 File Offset: 0x000E3344
		public override void UpDownBaseDrawButton(Graphics g, Rectangle bounds, bool top, PushButtonState state)
		{
			ControlPaint.DrawScrollButton(g, bounds, (!top) ? ScrollButton.Down : ScrollButton.Min, (state != PushButtonState.Pressed) ? ButtonState.Normal : ButtonState.Pushed);
		}

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06003890 RID: 14480 RVA: 0x000E5170 File Offset: 0x000E3370
		public override bool UpDownBaseHasHotButtonStyle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06003891 RID: 14481 RVA: 0x000E5174 File Offset: 0x000E3374
		public override Size VScrollBarDefaultSize
		{
			get
			{
				return new Size(this.ScrollBarButtonSize, 80);
			}
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06003892 RID: 14482 RVA: 0x000E5184 File Offset: 0x000E3384
		public override Size TreeViewDefaultSize
		{
			get
			{
				return new Size(121, 97);
			}
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x000E5190 File Offset: 0x000E3390
		public override void TreeViewDrawNodePlusMinus(TreeView treeView, TreeNode node, Graphics dc, int x, int middle)
		{
			int num = treeView.ActualItemHeight - 2;
			dc.FillRectangle(this.ResPool.GetSolidBrush(treeView.BackColor), x + 4 - num / 2, node.GetY() + 1, num, num);
			dc.DrawRectangle(SystemPens.ControlDarkDark, x, middle - 4, 8, 8);
			if (node.IsExpanded)
			{
				dc.DrawLine(SystemPens.ControlDarkDark, x + 2, middle, x + 6, middle);
			}
			else
			{
				dc.DrawLine(SystemPens.ControlDarkDark, x + 2, middle, x + 6, middle);
				dc.DrawLine(SystemPens.ControlDarkDark, x + 4, middle - 2, x + 4, middle + 2);
			}
		}

		// Token: 0x06003894 RID: 14484 RVA: 0x000E523C File Offset: 0x000E343C
		public override int ManagedWindowTitleBarHeight(InternalWindowManager wm)
		{
			if (wm.IsToolWindow && !wm.IsMinimized)
			{
				return SystemInformation.ToolWindowCaptionHeight;
			}
			if (wm.Form.FormBorderStyle == FormBorderStyle.None)
			{
				return 0;
			}
			return SystemInformation.CaptionHeight;
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x000E527C File Offset: 0x000E347C
		public override int ManagedWindowBorderWidth(InternalWindowManager wm)
		{
			if ((wm.IsToolWindow && wm.form.FormBorderStyle == FormBorderStyle.FixedToolWindow) || wm.IsMinimized)
			{
				return 3;
			}
			return 4;
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x000E52B4 File Offset: 0x000E34B4
		public override int ManagedWindowIconWidth(InternalWindowManager wm)
		{
			return this.ManagedWindowTitleBarHeight(wm) - 5;
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x000E52C0 File Offset: 0x000E34C0
		public override void ManagedWindowSetButtonLocations(InternalWindowManager wm)
		{
			TitleButtons titleButtons = wm.TitleButtons;
			Form form = wm.form;
			titleButtons.HelpButton.Visible = form.HelpButton;
			foreach (object obj in titleButtons)
			{
				TitleButton titleButton = (TitleButton)obj;
				titleButton.Visible = false;
			}
			switch (form.FormBorderStyle)
			{
			case FormBorderStyle.None:
				if (form.WindowState == FormWindowState.Normal)
				{
					goto IL_0175;
				}
				break;
			case FormBorderStyle.FixedSingle:
			case FormBorderStyle.Fixed3D:
			case FormBorderStyle.FixedDialog:
			case FormBorderStyle.Sizable:
				break;
			case FormBorderStyle.FixedToolWindow:
			case FormBorderStyle.SizableToolWindow:
				titleButtons.CloseButton.Visible = true;
				if (form.WindowState == FormWindowState.Normal)
				{
					goto IL_0175;
				}
				break;
			default:
				goto IL_0175;
			}
			switch (form.WindowState)
			{
			case FormWindowState.Normal:
				titleButtons.MinimizeButton.Visible = true;
				titleButtons.MaximizeButton.Visible = true;
				titleButtons.RestoreButton.Visible = false;
				break;
			case FormWindowState.Minimized:
				titleButtons.MinimizeButton.Visible = false;
				titleButtons.MaximizeButton.Visible = true;
				titleButtons.RestoreButton.Visible = true;
				break;
			case FormWindowState.Maximized:
				titleButtons.MinimizeButton.Visible = true;
				titleButtons.MaximizeButton.Visible = false;
				titleButtons.RestoreButton.Visible = true;
				break;
			}
			titleButtons.CloseButton.Visible = true;
			IL_0175:
			if (!form.MinimizeBox && !form.MaximizeBox)
			{
				titleButtons.MinimizeButton.Visible = false;
				titleButtons.MaximizeButton.Visible = false;
			}
			else if (!form.MinimizeBox)
			{
				titleButtons.MinimizeButton.State = ButtonState.Inactive;
			}
			else if (!form.MaximizeBox)
			{
				titleButtons.MaximizeButton.State = ButtonState.Inactive;
			}
			int num = this.ManagedWindowBorderWidth(wm);
			Size size = this.ManagedWindowButtonSize(wm);
			int width = size.Width;
			int height = size.Height;
			int num2 = num + 2;
			int num3 = form.Width - num - width - 2;
			if ((!wm.IsToolWindow || wm.IsMinimized) && wm.HasBorders)
			{
				titleButtons.CloseButton.Rectangle = new Rectangle(num3, num2, width, height);
				num3 -= 2 + width;
				if (titleButtons.MaximizeButton.Visible)
				{
					titleButtons.MaximizeButton.Rectangle = new Rectangle(num3, num2, width, height);
					num3 -= 2 + width;
				}
				if (titleButtons.RestoreButton.Visible)
				{
					titleButtons.RestoreButton.Rectangle = new Rectangle(num3, num2, width, height);
					num3 -= 2 + width;
				}
				titleButtons.MinimizeButton.Rectangle = new Rectangle(num3, num2, width, height);
				num3 -= 2 + width;
			}
			else if (wm.IsToolWindow)
			{
				titleButtons.CloseButton.Rectangle = new Rectangle(num3, num2, width, height);
				num3 -= 2 + width;
			}
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x000E55FC File Offset: 0x000E37FC
		protected virtual Rectangle ManagedWindowDrawTitleBarAndBorders(Graphics dc, Rectangle clip, InternalWindowManager wm)
		{
			Form form = wm.Form;
			int num = this.ManagedWindowTitleBarHeight(wm);
			int num2 = this.ManagedWindowBorderWidth(wm);
			Color color = Color.FromArgb(255, 10, 36, 106);
			Color color2 = Color.FromArgb(255, 166, 202, 240);
			Color color3 = ThemeEngine.Current.ColorControlDark;
			Color color4 = Color.FromArgb(255, 192, 192, 192);
			Pen pen = this.ResPool.GetPen(this.ColorControl);
			Rectangle rectangle;
			rectangle..ctor(0, 0, form.Width, form.Height);
			ControlPaint.DrawBorder3D(dc, rectangle, Border3DStyle.Raised);
			rectangle..ctor(2, 2, form.Width - 5, form.Height - 5);
			for (int i = 2; i < num2; i++)
			{
				dc.DrawRectangle(pen, rectangle);
				rectangle.Inflate(-1, -1);
			}
			bool flag = false;
			if (wm.Form.Parent != null && wm.Form.Parent is Form)
			{
				flag = false;
			}
			else if (wm.IsActive && !wm.IsMaximized)
			{
				flag = true;
			}
			if (flag)
			{
				color3 = color;
				color4 = color2;
			}
			Rectangle rectangle2;
			rectangle2..ctor(num2, num2, form.Width - num2 * 2, num - 1);
			if (rectangle2.Width > 0 && rectangle2.Height > 0)
			{
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle2, color3, color4, 0))
				{
					dc.FillRectangle(linearGradientBrush, rectangle2);
				}
			}
			if (!wm.IsMinimized)
			{
				dc.DrawLine(this.ResPool.GetPen(SystemColors.Control), num2, num + num2 - 1, form.Width - num2 - 1, num + num2 - 1);
			}
			return rectangle2;
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x000E57F0 File Offset: 0x000E39F0
		public override void DrawManagedWindowDecorations(Graphics dc, Rectangle clip, InternalWindowManager wm)
		{
			Rectangle rectangle = this.ManagedWindowDrawTitleBarAndBorders(dc, clip, wm);
			Form form = wm.Form;
			if (wm.ShowIcon)
			{
				Rectangle rectangle2 = this.ManagedWindowGetTitleBarIconArea(wm);
				if (rectangle2.IntersectsWith(clip))
				{
					dc.DrawIcon(form.Icon, rectangle2);
				}
				rectangle.Width -= rectangle2.Right + 2 - rectangle.X;
				rectangle.X = rectangle2.Right + 2;
			}
			foreach (TitleButton titleButton in wm.TitleButtons.AllButtons)
			{
				rectangle.Width -= Math.Max(0, rectangle.Right - this.DrawTitleButton(dc, titleButton, clip, form));
			}
			rectangle.Width -= 3;
			string text = form.Text;
			text = text.Replace(Environment.NewLine, string.Empty);
			if (text != null && text != string.Empty)
			{
				StringFormat stringFormat = new StringFormat();
				stringFormat.FormatFlags = 4096;
				stringFormat.Trimming = 3;
				stringFormat.LineAlignment = 1;
				if (rectangle.IntersectsWith(clip))
				{
					dc.DrawString(text, this.WindowBorderFont, ThemeEngine.Current.ResPool.GetSolidBrush(Color.White), rectangle, stringFormat);
				}
			}
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x000E595C File Offset: 0x000E3B5C
		public override Size ManagedWindowButtonSize(InternalWindowManager wm)
		{
			int num = this.ManagedWindowTitleBarHeight(wm);
			if (!wm.IsMaximized && !wm.IsMinimized)
			{
				if (wm.IsToolWindow)
				{
					return new Size(SystemInformation.ToolWindowCaptionButtonSize.Width - 2, num - 5);
				}
				if (wm.Form.FormBorderStyle == FormBorderStyle.None)
				{
					return Size.Empty;
				}
			}
			else
			{
				num = SystemInformation.CaptionHeight;
			}
			return new Size(SystemInformation.CaptionButtonSize.Width - 2, num - 5);
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x000E59E4 File Offset: 0x000E3BE4
		private int DrawTitleButton(Graphics dc, TitleButton button, Rectangle clip, Form form)
		{
			if (!button.Visible)
			{
				return int.MaxValue;
			}
			if (button.Rectangle.IntersectsWith(clip))
			{
				this.ManagedWindowDrawTitleButton(dc, button, clip, form);
			}
			return button.Rectangle.Left;
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x000E5A2C File Offset: 0x000E3C2C
		protected virtual void ManagedWindowDrawTitleButton(Graphics dc, TitleButton button, Rectangle clip, Form form)
		{
			dc.FillRectangle(SystemBrushes.Control, button.Rectangle);
			ControlPaint.DrawCaptionButton(dc, button.Rectangle, button.Caption, button.State);
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x000E5A58 File Offset: 0x000E3C58
		public override Rectangle ManagedWindowGetTitleBarIconArea(InternalWindowManager wm)
		{
			int num = this.ManagedWindowBorderWidth(wm);
			return new Rectangle(num + 3, num + 2, wm.IconWidth, wm.IconWidth);
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x000E5A84 File Offset: 0x000E3C84
		public override Size ManagedWindowGetMenuButtonSize(InternalWindowManager wm)
		{
			Size menuButtonSize = SystemInformation.MenuButtonSize;
			menuButtonSize.Width -= 2;
			menuButtonSize.Height -= 4;
			return menuButtonSize;
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x000E5AB8 File Offset: 0x000E3CB8
		public override bool ManagedWindowTitleButtonHasHotElementStyle(TitleButton button, Form form)
		{
			return false;
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x000E5ABC File Offset: 0x000E3CBC
		public override void ManagedWindowDrawMenuButton(Graphics dc, TitleButton button, Rectangle clip, InternalWindowManager wm)
		{
			dc.FillRectangle(SystemBrushes.Control, button.Rectangle);
			ControlPaint.DrawCaptionButton(dc, button.Rectangle, button.Caption, button.State);
		}

		// Token: 0x060038A1 RID: 14497 RVA: 0x000E5AE8 File Offset: 0x000E3CE8
		public override void ManagedWindowOnSizeInitializedOrChanged(Form form)
		{
		}

		// Token: 0x060038A2 RID: 14498 RVA: 0x000E5AEC File Offset: 0x000E3CEC
		public override void CPDrawBorder(Graphics graphics, Rectangle bounds, Color leftColor, int leftWidth, ButtonBorderStyle leftStyle, Color topColor, int topWidth, ButtonBorderStyle topStyle, Color rightColor, int rightWidth, ButtonBorderStyle rightStyle, Color bottomColor, int bottomWidth, ButtonBorderStyle bottomStyle)
		{
			ThemeWin32Classic.DrawBorderInternal(graphics, bounds.Left, bounds.Top, bounds.Left, bounds.Bottom - 1, leftWidth, leftColor, leftStyle, Border3DSide.Left);
			ThemeWin32Classic.DrawBorderInternal(graphics, bounds.Left, bounds.Top, bounds.Right - 1, bounds.Top, topWidth, topColor, topStyle, Border3DSide.Top);
			ThemeWin32Classic.DrawBorderInternal(graphics, bounds.Right - 1, bounds.Top, bounds.Right - 1, bounds.Bottom - 1, rightWidth, rightColor, rightStyle, Border3DSide.Right);
			ThemeWin32Classic.DrawBorderInternal(graphics, bounds.Left, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1, bottomWidth, bottomColor, bottomStyle, Border3DSide.Bottom);
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x000E5BAC File Offset: 0x000E3DAC
		public override void CPDrawBorder(Graphics graphics, RectangleF bounds, Color leftColor, int leftWidth, ButtonBorderStyle leftStyle, Color topColor, int topWidth, ButtonBorderStyle topStyle, Color rightColor, int rightWidth, ButtonBorderStyle rightStyle, Color bottomColor, int bottomWidth, ButtonBorderStyle bottomStyle)
		{
			ThemeWin32Classic.DrawBorderInternal(graphics, bounds.Left, bounds.Top, bounds.Left, bounds.Bottom - 1f, leftWidth, leftColor, leftStyle, Border3DSide.Left);
			ThemeWin32Classic.DrawBorderInternal(graphics, bounds.Left, bounds.Top, bounds.Right - 1f, bounds.Top, topWidth, topColor, topStyle, Border3DSide.Top);
			ThemeWin32Classic.DrawBorderInternal(graphics, bounds.Right - 1f, bounds.Top, bounds.Right - 1f, bounds.Bottom - 1f, rightWidth, rightColor, rightStyle, Border3DSide.Right);
			ThemeWin32Classic.DrawBorderInternal(graphics, bounds.Left, bounds.Bottom - 1f, bounds.Right - 1f, bounds.Bottom - 1f, bottomWidth, bottomColor, bottomStyle, Border3DSide.Bottom);
		}

		// Token: 0x060038A4 RID: 14500 RVA: 0x000E5C8C File Offset: 0x000E3E8C
		public override void CPDrawBorder3D(Graphics graphics, Rectangle rectangle, Border3DStyle style, Border3DSide sides)
		{
			this.CPDrawBorder3D(graphics, rectangle, style, sides, this.ColorControl);
		}

		// Token: 0x060038A5 RID: 14501 RVA: 0x000E5CAC File Offset: 0x000E3EAC
		public override void CPDrawBorder3D(Graphics graphics, Rectangle rectangle, Border3DStyle style, Border3DSide sides, Color control_color)
		{
			Rectangle rectangle2;
			rectangle2..ctor(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
			bool flag = control_color.ToArgb() == this.ColorControl.ToArgb();
			if ((style & Border3DStyle.Adjust) != (Border3DStyle)0)
			{
				rectangle2.Y -= 2;
				rectangle2.X -= 2;
				rectangle2.Width += 4;
				rectangle2.Height += 4;
			}
			Pen pen4;
			Pen pen3;
			Pen pen2;
			Pen pen = (pen2 = (pen3 = (pen4 = ((!flag) ? this.ResPool.GetPen(control_color) : SystemPens.Control))));
			CPColor cpcolor = CPColor.Empty;
			if (!flag)
			{
				cpcolor = this.ResPool.GetCPColor(control_color);
			}
			switch (style)
			{
			case Border3DStyle.RaisedOuter:
				pen3 = ((!flag) ? this.ResPool.GetPen(cpcolor.DarkDark) : SystemPens.ControlDarkDark);
				break;
			case Border3DStyle.SunkenOuter:
				pen2 = ((!flag) ? this.ResPool.GetPen(cpcolor.Dark) : SystemPens.ControlDark);
				pen3 = ((!flag) ? this.ResPool.GetPen(cpcolor.LightLight) : SystemPens.ControlLightLight);
				break;
			default:
				if (style == Border3DStyle.Flat)
				{
					pen3 = (pen2 = ((!flag) ? this.ResPool.GetPen(cpcolor.Dark) : SystemPens.ControlDark));
				}
				break;
			case Border3DStyle.RaisedInner:
				pen2 = ((!flag) ? this.ResPool.GetPen(cpcolor.LightLight) : SystemPens.ControlLightLight);
				pen3 = ((!flag) ? this.ResPool.GetPen(cpcolor.Dark) : SystemPens.ControlDark);
				break;
			case Border3DStyle.Raised:
				pen = ((!flag) ? this.ResPool.GetPen(cpcolor.LightLight) : SystemPens.ControlLightLight);
				pen3 = ((!flag) ? this.ResPool.GetPen(cpcolor.DarkDark) : SystemPens.ControlDarkDark);
				pen4 = ((!flag) ? this.ResPool.GetPen(cpcolor.Dark) : SystemPens.ControlDark);
				break;
			case Border3DStyle.Etched:
				pen4 = (pen2 = ((!flag) ? this.ResPool.GetPen(cpcolor.Dark) : SystemPens.ControlDark));
				pen3 = (pen = ((!flag) ? this.ResPool.GetPen(cpcolor.LightLight) : SystemPens.ControlLightLight));
				break;
			case Border3DStyle.SunkenInner:
				pen2 = ((!flag) ? this.ResPool.GetPen(cpcolor.DarkDark) : SystemPens.ControlDarkDark);
				break;
			case Border3DStyle.Bump:
				pen3 = (pen = ((!flag) ? this.ResPool.GetPen(cpcolor.DarkDark) : SystemPens.ControlDarkDark));
				break;
			case Border3DStyle.Sunken:
				pen2 = ((!flag) ? this.ResPool.GetPen(cpcolor.Dark) : SystemPens.ControlDark);
				pen = ((!flag) ? this.ResPool.GetPen(cpcolor.DarkDark) : SystemPens.ControlDarkDark);
				pen3 = ((!flag) ? this.ResPool.GetPen(cpcolor.LightLight) : SystemPens.ControlLightLight);
				break;
			}
			bool flag2 = style != Border3DStyle.RaisedOuter && style != Border3DStyle.SunkenOuter;
			if ((sides & Border3DSide.Middle) != (Border3DSide)0)
			{
				Brush brush = ((!flag) ? this.ResPool.GetSolidBrush(control_color) : SystemBrushes.Control);
				graphics.FillRectangle(brush, rectangle2);
			}
			if ((sides & Border3DSide.Left) != (Border3DSide)0)
			{
				graphics.DrawLine(pen2, rectangle2.Left, rectangle2.Bottom - 2, rectangle2.Left, rectangle2.Top);
				if (rectangle2.Width > 2 && flag2)
				{
					graphics.DrawLine(pen, rectangle2.Left + 1, rectangle2.Bottom - 2, rectangle2.Left + 1, rectangle2.Top);
				}
			}
			if ((sides & Border3DSide.Top) != (Border3DSide)0)
			{
				graphics.DrawLine(pen2, rectangle2.Left, rectangle2.Top, rectangle2.Right - 2, rectangle2.Top);
				if (rectangle2.Height > 2 && flag2)
				{
					graphics.DrawLine(pen, rectangle2.Left + 1, rectangle2.Top + 1, rectangle2.Right - 3, rectangle2.Top + 1);
				}
			}
			if ((sides & Border3DSide.Right) != (Border3DSide)0)
			{
				graphics.DrawLine(pen3, rectangle2.Right - 1, rectangle2.Top, rectangle2.Right - 1, rectangle2.Bottom - 1);
				if (rectangle2.Width > 3 && flag2)
				{
					graphics.DrawLine(pen4, rectangle2.Right - 2, rectangle2.Top + 1, rectangle2.Right - 2, rectangle2.Bottom - 2);
				}
			}
			if ((sides & Border3DSide.Bottom) != (Border3DSide)0)
			{
				graphics.DrawLine(pen3, rectangle2.Left, rectangle2.Bottom - 1, rectangle2.Right - 1, rectangle2.Bottom - 1);
				if (rectangle2.Height > 3 && flag2)
				{
					graphics.DrawLine(pen4, rectangle2.Left + 1, rectangle2.Bottom - 2, rectangle2.Right - 2, rectangle2.Bottom - 2);
				}
			}
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x000E6250 File Offset: 0x000E4450
		public override void CPDrawButton(Graphics dc, Rectangle rectangle, ButtonState state)
		{
			this.CPDrawButtonInternal(dc, rectangle, state, SystemPens.ControlDarkDark, SystemPens.ControlDark, SystemPens.ControlLight);
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x000E6278 File Offset: 0x000E4478
		private void CPDrawButtonInternal(Graphics dc, Rectangle rectangle, ButtonState state, Pen DarkPen, Pen NormalPen, Pen LightPen)
		{
			dc.FillRectangle(this.ResPool.GetHatchBrush(12, Color.FromArgb(base.Clamp((int)(this.ColorControl.R + 3), 0, 255), (int)this.ColorControl.G, (int)this.ColorControl.B), this.ColorControl), rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height - 2);
			if ((state & ButtonState.All) == ButtonState.All || ((state & ButtonState.Checked) == ButtonState.Checked && (state & ButtonState.Flat) == ButtonState.Flat))
			{
				dc.FillRectangle(this.ResPool.GetHatchBrush(12, this.ColorControlLight, this.ColorControl), rectangle.X + 2, rectangle.Y + 2, rectangle.Width - 4, rectangle.Height - 4);
				dc.DrawRectangle(SystemPens.ControlDark, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
			}
			else if ((state & ButtonState.Flat) == ButtonState.Flat)
			{
				dc.DrawRectangle(SystemPens.ControlDark, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
			}
			else if ((state & ButtonState.Checked) == ButtonState.Checked)
			{
				dc.FillRectangle(this.ResPool.GetHatchBrush(12, this.ColorControlLight, this.ColorControl), rectangle.X + 2, rectangle.Y + 2, rectangle.Width - 4, rectangle.Height - 4);
				dc.DrawLine(DarkPen, rectangle.X, rectangle.Y, rectangle.X, rectangle.Bottom - 2);
				dc.DrawLine(DarkPen, rectangle.X + 1, rectangle.Y, rectangle.Right - 2, rectangle.Y);
				dc.DrawLine(NormalPen, rectangle.X + 1, rectangle.Y + 1, rectangle.X + 1, rectangle.Bottom - 3);
				dc.DrawLine(NormalPen, rectangle.X + 2, rectangle.Y + 1, rectangle.Right - 3, rectangle.Y + 1);
				dc.DrawLine(LightPen, rectangle.X, rectangle.Bottom - 1, rectangle.Right - 2, rectangle.Bottom - 1);
				dc.DrawLine(LightPen, rectangle.Right - 1, rectangle.Y, rectangle.Right - 1, rectangle.Bottom - 1);
			}
			else if ((state & ButtonState.Pushed) == ButtonState.Pushed)
			{
				dc.DrawLine(DarkPen, rectangle.X, rectangle.Y, rectangle.X, rectangle.Bottom - 2);
				dc.DrawLine(DarkPen, rectangle.X + 1, rectangle.Y, rectangle.Right - 2, rectangle.Y);
				dc.DrawLine(NormalPen, rectangle.X + 1, rectangle.Y + 1, rectangle.X + 1, rectangle.Bottom - 3);
				dc.DrawLine(NormalPen, rectangle.X + 2, rectangle.Y + 1, rectangle.Right - 3, rectangle.Y + 1);
				dc.DrawLine(LightPen, rectangle.X, rectangle.Bottom - 1, rectangle.Right - 2, rectangle.Bottom - 1);
				dc.DrawLine(LightPen, rectangle.Right - 1, rectangle.Y, rectangle.Right - 1, rectangle.Bottom - 1);
			}
			else if ((state & ButtonState.Inactive) == ButtonState.Inactive || true)
			{
				dc.DrawLine(LightPen, rectangle.X, rectangle.Y, rectangle.Right - 2, rectangle.Y);
				dc.DrawLine(LightPen, rectangle.X, rectangle.Y, rectangle.X, rectangle.Bottom - 2);
				dc.DrawLine(NormalPen, rectangle.X + 1, rectangle.Bottom - 2, rectangle.Right - 2, rectangle.Bottom - 2);
				dc.DrawLine(NormalPen, rectangle.Right - 2, rectangle.Y + 1, rectangle.Right - 2, rectangle.Bottom - 3);
				dc.DrawLine(DarkPen, rectangle.X, rectangle.Bottom - 1, rectangle.Right - 1, rectangle.Bottom - 1);
				dc.DrawLine(DarkPen, rectangle.Right - 1, rectangle.Y, rectangle.Right - 1, rectangle.Bottom - 2);
			}
		}

		// Token: 0x060038A8 RID: 14504 RVA: 0x000E6764 File Offset: 0x000E4964
		public override void CPDrawCaptionButton(Graphics graphics, Rectangle rectangle, CaptionButton button, ButtonState state)
		{
			this.CPDrawButtonInternal(graphics, rectangle, state, SystemPens.ControlDarkDark, SystemPens.ControlDark, SystemPens.ControlLightLight);
			Rectangle rectangle2;
			if (rectangle.Width < rectangle.Height)
			{
				rectangle2..ctor(rectangle.X + 1, rectangle.Y + rectangle.Height / 2 - rectangle.Width / 2 + 1, rectangle.Width - 4, rectangle.Width - 4);
			}
			else
			{
				rectangle2..ctor(rectangle.X + rectangle.Width / 2 - rectangle.Height / 2 + 1, rectangle.Y + 1, rectangle.Height - 4, rectangle.Height - 4);
			}
			if ((state & ButtonState.Pushed) != ButtonState.Normal)
			{
				rectangle2..ctor(rectangle.X + 2, rectangle.Y + 2, rectangle.Width - 3, rectangle.Height - 3);
			}
			int num = Math.Max(1, rectangle2.Width / 7);
			switch (button)
			{
			case CaptionButton.Close:
			{
				Pen pen;
				if ((state & ButtonState.Inactive) != ButtonState.Normal)
				{
					pen = this.ResPool.GetSizedPen(this.ColorControlLight, num);
					this.DrawCaptionHelper(graphics, this.ColorControlLight, pen, num, 1, rectangle2, button);
					pen = this.ResPool.GetSizedPen(this.ColorControlDark, num);
					this.DrawCaptionHelper(graphics, this.ColorControlDark, pen, num, 0, rectangle2, button);
					return;
				}
				pen = this.ResPool.GetSizedPen(this.ColorControlText, num);
				this.DrawCaptionHelper(graphics, this.ColorControlText, pen, num, 0, rectangle2, button);
				return;
			}
			case CaptionButton.Minimize:
			case CaptionButton.Maximize:
			case CaptionButton.Restore:
			case CaptionButton.Help:
				if ((state & ButtonState.Inactive) != ButtonState.Normal)
				{
					this.DrawCaptionHelper(graphics, this.ColorControlLight, SystemPens.ControlLightLight, num, 1, rectangle2, button);
					this.DrawCaptionHelper(graphics, this.ColorControlDark, SystemPens.ControlDark, num, 0, rectangle2, button);
					return;
				}
				this.DrawCaptionHelper(graphics, this.ColorControlText, SystemPens.ControlText, num, 0, rectangle2, button);
				return;
			default:
				return;
			}
		}

		// Token: 0x060038A9 RID: 14505 RVA: 0x000E6958 File Offset: 0x000E4B58
		public override void CPDrawCheckBox(Graphics dc, Rectangle rectangle, ButtonState state)
		{
			Pen pen = Pens.Black;
			Rectangle rectangle2;
			rectangle2..ctor(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
			if ((state & ButtonState.All) == ButtonState.All)
			{
				rectangle2.Width -= 2;
				rectangle2.Height -= 2;
				dc.FillRectangle(SystemBrushes.Control, rectangle2.X, rectangle2.Y, rectangle2.Width - 1, rectangle2.Height - 1);
				dc.DrawRectangle(SystemPens.ControlDark, rectangle2.X, rectangle2.Y, rectangle2.Width - 1, rectangle2.Height - 1);
				pen = SystemPens.ControlDark;
			}
			else if ((state & ButtonState.Flat) == ButtonState.Flat)
			{
				rectangle2.Width -= 2;
				rectangle2.Height -= 2;
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					dc.FillRectangle(SystemBrushes.ControlLight, rectangle2.X, rectangle2.Y, rectangle2.Width - 1, rectangle2.Height - 1);
				}
				else
				{
					dc.FillRectangle(Brushes.White, rectangle2.X, rectangle2.Y, rectangle2.Width - 1, rectangle2.Height - 1);
				}
				dc.DrawRectangle(SystemPens.ControlDark, rectangle2.X, rectangle2.Y, rectangle2.Width - 1, rectangle2.Height - 1);
			}
			else
			{
				rectangle2.Width--;
				rectangle2.Height--;
				int num = ((rectangle2.Height <= rectangle2.Width) ? rectangle2.Height : rectangle2.Width);
				int num2 = Math.Max(0, rectangle2.X + rectangle2.Width / 2 - num / 2);
				int num3 = Math.Max(0, rectangle2.Y + rectangle2.Height / 2 - num / 2);
				Rectangle rectangle3;
				rectangle3..ctor(num2, num3, num, num);
				if ((state & ButtonState.Pushed) == ButtonState.Pushed || (state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					dc.FillRectangle(this.ResPool.GetHatchBrush(12, Color.FromArgb(base.Clamp((int)(this.ColorControl.R + 3), 0, 255), (int)this.ColorControl.G, (int)this.ColorControl.B), this.ColorControl), rectangle3.X + 2, rectangle3.Y + 2, rectangle3.Width - 3, rectangle3.Height - 3);
				}
				else
				{
					dc.FillRectangle(SystemBrushes.ControlLightLight, rectangle3.X + 2, rectangle3.Y + 2, rectangle3.Width - 3, rectangle3.Height - 3);
				}
				Pen pen2 = SystemPens.ControlDark;
				dc.DrawLine(pen2, rectangle3.X, rectangle3.Y, rectangle3.X, rectangle3.Bottom - 1);
				dc.DrawLine(pen2, rectangle3.X + 1, rectangle3.Y, rectangle3.Right - 1, rectangle3.Y);
				pen2 = SystemPens.ControlDarkDark;
				dc.DrawLine(pen2, rectangle3.X + 1, rectangle3.Y + 1, rectangle3.X + 1, rectangle3.Bottom - 2);
				dc.DrawLine(pen2, rectangle3.X + 2, rectangle3.Y + 1, rectangle3.Right - 2, rectangle3.Y + 1);
				pen2 = SystemPens.ControlLightLight;
				dc.DrawLine(pen2, rectangle3.Right, rectangle3.Y, rectangle3.Right, rectangle3.Bottom);
				dc.DrawLine(pen2, rectangle3.X, rectangle3.Bottom, rectangle3.Right, rectangle3.Bottom);
				using (Pen pen3 = new Pen(this.ResPool.GetHatchBrush(12, Color.FromArgb(base.Clamp((int)(this.ColorControl.R + 3), 0, 255), (int)this.ColorControl.G, (int)this.ColorControl.B), this.ColorControl)))
				{
					dc.DrawLine(pen3, rectangle3.X + 1, rectangle3.Bottom - 1, rectangle3.Right - 1, rectangle3.Bottom - 1);
					dc.DrawLine(pen3, rectangle3.Right - 1, rectangle3.Y + 1, rectangle3.Right - 1, rectangle3.Bottom - 1);
				}
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					pen = SystemPens.ControlDark;
				}
			}
			if ((state & ButtonState.Checked) == ButtonState.Checked)
			{
				int num4 = ((rectangle2.Height <= rectangle2.Width) ? (rectangle2.Height / 2) : (rectangle2.Width / 2));
				if (num4 < 7)
				{
					int num5 = Math.Max(3, num4 / 3);
					int num6 = Math.Max(1, num4 / 9);
					Rectangle rectangle4;
					rectangle4..ctor(rectangle2.X + rectangle2.Width / 2 - (int)Math.Ceiling((double)((float)num4 / 2f)) - 1, rectangle2.Y + rectangle2.Height / 2 - num4 / 2 - 1, num4, num4);
					for (int i = 0; i < num5; i++)
					{
						dc.DrawLine(pen, rectangle4.Left + num5 / 2, rectangle4.Top + num5 + i, rectangle4.Left + num5 / 2 + 2 * num6, rectangle4.Top + num5 + 2 * num6 + i);
						dc.DrawLine(pen, rectangle4.Left + num5 / 2 + 2 * num6, rectangle4.Top + num5 + 2 * num6 + i, rectangle4.Left + num5 / 2 + 6 * num6, rectangle4.Top + num5 - 2 * num6 + i);
					}
				}
				else
				{
					int num7 = Math.Max(3, num4 / 3) + 1;
					int num8 = rectangle2.Width / 2;
					int num9 = rectangle2.Height / 2;
					Rectangle rectangle5;
					rectangle5..ctor(rectangle2.X + num8 - num4 / 2 - 1, rectangle2.Y + num9 - num4 / 2, num4, num4);
					int num10 = num4 / 3;
					int num11 = num4 - num10 - 1;
					for (int j = 0; j < num7; j++)
					{
						dc.DrawLine(pen, rectangle5.X, rectangle5.Bottom - 1 - num10 - j, rectangle5.X + num10, rectangle5.Bottom - 1 - j);
						dc.DrawLine(pen, rectangle5.X + num10, rectangle5.Bottom - 1 - j, rectangle5.Right - 1, rectangle5.Bottom - j - 1 - num11);
					}
				}
			}
		}

		// Token: 0x060038AA RID: 14506 RVA: 0x000E7070 File Offset: 0x000E5270
		public override void CPDrawComboButton(Graphics graphics, Rectangle rectangle, ButtonState state)
		{
			Point[] array = new Point[3];
			if ((state & ButtonState.Checked) != ButtonState.Normal)
			{
				graphics.FillRectangle(this.ResPool.GetHatchBrush(12, this.ColorControlLightLight, this.ColorControlLight), rectangle);
			}
			if ((state & ButtonState.Flat) != ButtonState.Normal)
			{
				ControlPaint.DrawBorder(graphics, rectangle, this.ColorControlDark, ButtonBorderStyle.Solid);
			}
			else if ((state & (ButtonState.Pushed | ButtonState.Checked)) != ButtonState.Normal)
			{
				Rectangle rectangle2;
				rectangle2..ctor(rectangle.X, rectangle.Y, Math.Max(rectangle.Width - 1, 0), Math.Max(rectangle.Height - 1, 0));
				graphics.DrawRectangle(SystemPens.ControlDark, rectangle2);
			}
			else
			{
				this.CPDrawBorder3D(graphics, rectangle, Border3DStyle.Raised, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom, this.ColorControl);
			}
			Rectangle rectangle3;
			rectangle3..ctor(rectangle.X + rectangle.Width / 4, rectangle.Y + rectangle.Height / 4, rectangle.Width / 2, rectangle.Height / 2);
			int num = rectangle3.Left + rectangle3.Width / 2;
			int num2 = rectangle3.Top + rectangle3.Height / 2;
			int num3 = Math.Max(1, rectangle3.Width / 8);
			int num4 = Math.Max(1, rectangle3.Height / 8);
			if ((state & ButtonState.Pushed) != ButtonState.Normal)
			{
				num3++;
				num4++;
			}
			rectangle3.Y -= num4;
			num2 -= num4;
			Point point;
			point..ctor(rectangle3.Left, num2);
			Point point2;
			point2..ctor(rectangle3.Right, num2);
			Point point3;
			point3..ctor(num, rectangle3.Bottom);
			array[0] = point;
			array[1] = point2;
			array[2] = point3;
			if ((state & ButtonState.Inactive) != ButtonState.Normal)
			{
				Point[] array2 = array;
				int num5 = 0;
				array2[num5].X = array2[num5].X + 1;
				Point[] array3 = array;
				int num6 = 0;
				array3[num6].Y = array3[num6].Y + 1;
				Point[] array4 = array;
				int num7 = 1;
				array4[num7].X = array4[num7].X + 1;
				Point[] array5 = array;
				int num8 = 1;
				array5[num8].Y = array5[num8].Y + 1;
				Point[] array6 = array;
				int num9 = 2;
				array6[num9].X = array6[num9].X + 1;
				Point[] array7 = array;
				int num10 = 2;
				array7[num10].Y = array7[num10].Y + 1;
				graphics.FillPolygon(SystemBrushes.ControlLightLight, array, 1);
				array[0] = point;
				array[1] = point2;
				array[2] = point3;
				graphics.FillPolygon(SystemBrushes.ControlDark, array, 1);
			}
			else
			{
				graphics.FillPolygon(SystemBrushes.ControlText, array, 1);
			}
		}

		// Token: 0x060038AB RID: 14507 RVA: 0x000E7314 File Offset: 0x000E5514
		public override void CPDrawContainerGrabHandle(Graphics graphics, Rectangle bounds)
		{
			Pen black = Pens.Black;
			Rectangle rectangle;
			rectangle..ctor(bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
			graphics.FillRectangle(SystemBrushes.ControlLightLight, rectangle);
			graphics.DrawRectangle(black, rectangle);
			int num = rectangle.X + rectangle.Width / 2;
			int num2 = rectangle.Y + rectangle.Height / 2;
			graphics.DrawLine(black, num, rectangle.Y + 2, num, rectangle.Bottom - 2);
			graphics.DrawLine(black, rectangle.X + 2, num2, rectangle.Right - 2, num2);
			graphics.DrawLine(black, num - 1, rectangle.Y + 3, num + 1, rectangle.Y + 3);
			graphics.DrawLine(black, num - 1, rectangle.Bottom - 3, num + 1, rectangle.Bottom - 3);
			graphics.DrawLine(black, rectangle.X + 3, num2 - 1, rectangle.X + 3, num2 + 1);
			graphics.DrawLine(black, rectangle.Right - 3, num2 - 1, rectangle.Right - 3, num2 + 1);
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x000E7438 File Offset: 0x000E5638
		public virtual void DrawFlatStyleFocusRectangle(Graphics graphics, Rectangle rectangle, ButtonBase button, Color foreColor, Color backColor)
		{
			Rectangle rectangle2;
			rectangle2..ctor(rectangle.X, rectangle.Y, Math.Max(rectangle.Width - 1, 0), Math.Max(rectangle.Height - 1, 0));
			Color color = foreColor;
			if (button.FlatStyle == FlatStyle.Popup && !button.is_pressed)
			{
				color = ((backColor.ToArgb() != this.ColorControl.ToArgb()) ? this.ColorControlText : ControlPaint.Dark(this.ColorControl));
			}
			graphics.DrawRectangle(this.ResPool.GetPen(color), rectangle2);
			if (button.FlatStyle == FlatStyle.Popup)
			{
				this.DrawInnerFocusRectangle(graphics, Rectangle.Inflate(rectangle, -4, -4), backColor);
			}
			else
			{
				Pen pen = this.ResPool.GetPen(ControlPaint.LightLight(backColor));
				graphics.DrawRectangle(pen, Rectangle.Inflate(rectangle2, -4, -4));
			}
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x000E7520 File Offset: 0x000E5720
		public virtual void DrawInnerFocusRectangle(Graphics graphics, Rectangle rectangle, Color backColor)
		{
			Rectangle rectangle2;
			rectangle2..ctor(rectangle.X, rectangle.Y, Math.Max(rectangle.Width - 1, 0), Math.Max(rectangle.Height - 1, 0));
			this.CPDrawFocusRectangle(graphics, rectangle2, Color.Wheat, backColor);
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x000E7570 File Offset: 0x000E5770
		public override void CPDrawFocusRectangle(Graphics graphics, Rectangle rectangle, Color foreColor, Color backColor)
		{
			Rectangle rectangle2 = rectangle;
			if ((double)backColor.GetBrightness() >= 0.5)
			{
				foreColor = Color.Transparent;
				backColor = Color.Black;
			}
			else
			{
				backColor = Color.FromArgb(Math.Abs((int)(backColor.R - byte.MaxValue)), Math.Abs((int)(backColor.G - byte.MaxValue)), Math.Abs((int)(backColor.B - byte.MaxValue)));
				foreColor = Color.Black;
			}
			HatchBrush hatchBrush = this.ResPool.GetHatchBrush(12, backColor, foreColor);
			Pen pen = new Pen(hatchBrush, 1f);
			rectangle2.Width--;
			rectangle2.Height--;
			graphics.DrawRectangle(pen, rectangle2);
			pen.Dispose();
		}

		// Token: 0x060038AF RID: 14511 RVA: 0x000E7638 File Offset: 0x000E5838
		public override void CPDrawGrabHandle(Graphics graphics, Rectangle rectangle, bool primary, bool enabled)
		{
			Pen pen;
			Brush brush;
			if (primary)
			{
				pen = Pens.Black;
				if (enabled)
				{
					brush = Brushes.White;
				}
				else
				{
					brush = SystemBrushes.Control;
				}
			}
			else
			{
				pen = Pens.White;
				if (enabled)
				{
					brush = Brushes.Black;
				}
				else
				{
					brush = SystemBrushes.Control;
				}
			}
			graphics.FillRectangle(brush, rectangle);
			graphics.DrawRectangle(pen, rectangle);
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x000E769C File Offset: 0x000E589C
		public override void CPDrawGrid(Graphics graphics, Rectangle area, Size pixelsBetweenDots, Color backColor)
		{
			int num;
			int num2;
			int num3;
			ControlPaint.Color2HBS(backColor, out num, out num2, out num3);
			Color color;
			if (num2 > 127)
			{
				color = Color.Black;
			}
			else
			{
				color = Color.White;
			}
			using (Pen pen = new Pen(color))
			{
				pen.DashPattern = new float[]
				{
					1f,
					(float)(pixelsBetweenDots.Width - 1)
				};
				for (int i = area.Top; i < area.Bottom; i += pixelsBetweenDots.Height)
				{
					graphics.DrawLine(pen, area.X, i, area.Right - 1, i);
				}
			}
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x000E7770 File Offset: 0x000E5970
		public override void CPDrawImageDisabled(Graphics graphics, Image image, int x, int y, Color background)
		{
			if (ThemeWin32Classic.imagedisabled_attributes == null)
			{
				ThemeWin32Classic.imagedisabled_attributes = new ImageAttributes();
				float[][] array = new float[6][];
				int num = 0;
				float[] array2 = new float[5];
				array2[0] = 0.2f;
				array2[1] = 0.2f;
				array2[2] = 0.2f;
				array[num] = array2;
				int num2 = 1;
				float[] array3 = new float[5];
				array3[0] = 0.41f;
				array3[1] = 0.41f;
				array3[2] = 0.41f;
				array[num2] = array3;
				int num3 = 2;
				float[] array4 = new float[5];
				array4[0] = 0.11f;
				array4[1] = 0.11f;
				array4[2] = 0.11f;
				array[num3] = array4;
				array[3] = new float[] { 0.15f, 0.15f, 0.15f, 1f, 0f, 0f };
				array[4] = new float[] { 0.15f, 0.15f, 0.15f, 0f, 1f, 0f };
				array[5] = new float[] { 0.15f, 0.15f, 0.15f, 0f, 0f, 1f };
				ColorMatrix colorMatrix = new ColorMatrix(array);
				ThemeWin32Classic.imagedisabled_attributes.SetColorMatrix(colorMatrix);
			}
			graphics.DrawImage(image, new Rectangle(x, y, image.Width, image.Height), 0, 0, image.Width, image.Height, 2, ThemeWin32Classic.imagedisabled_attributes);
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x000E7878 File Offset: 0x000E5A78
		public override void CPDrawLockedFrame(Graphics graphics, Rectangle rectangle, bool primary)
		{
			Pen pen;
			Pen pen2;
			if (primary)
			{
				pen = this.ResPool.GetSizedPen(Color.White, 2);
				pen2 = this.ResPool.GetPen(Color.Black);
			}
			else
			{
				pen = this.ResPool.GetSizedPen(Color.Black, 2);
				pen2 = this.ResPool.GetPen(Color.White);
			}
			pen.Alignment = 1;
			pen2.Alignment = 1;
			graphics.DrawRectangle(pen, rectangle);
			graphics.DrawRectangle(pen2, rectangle.X + 2, rectangle.Y + 2, rectangle.Width - 5, rectangle.Height - 5);
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x000E7918 File Offset: 0x000E5B18
		public override void CPDrawMenuGlyph(Graphics graphics, Rectangle rectangle, MenuGlyph glyph, Color color, Color backColor)
		{
			if (backColor != Color.Empty)
			{
				graphics.FillRectangle(this.ResPool.GetSolidBrush(backColor), rectangle);
			}
			Brush solidBrush = this.ResPool.GetSolidBrush(color);
			switch (glyph)
			{
			case MenuGlyph.Arrow:
			{
				float num = (float)rectangle.Height * 0.7f;
				float num2 = num / 2f;
				PointF pointF;
				pointF..ctor((float)rectangle.X + ((float)rectangle.Width - num2) / 2f, (float)rectangle.Y + (float)rectangle.Height / 2f);
				PointF[] array = new PointF[3];
				array[0].X = pointF.X;
				array[0].Y = pointF.Y - num / 2f;
				array[1].X = pointF.X;
				array[1].Y = pointF.Y + num / 2f;
				array[2].X = pointF.X + num2 + 0.1f;
				array[2].Y = pointF.Y;
				graphics.FillPolygon(solidBrush, array);
				return;
			}
			case MenuGlyph.Checkmark:
			{
				Pen pen = this.ResPool.GetPen(color);
				int num3 = Math.Max(2, rectangle.Width / 6);
				Rectangle rectangle2;
				rectangle2..ctor(rectangle.X + num3, rectangle.Y + num3, rectangle.Width - num3 * 2, rectangle.Height - num3 * 2);
				int num4 = Math.Max(1, rectangle.Width / 12);
				int num5 = rectangle2.Y + num3 + (rectangle2.Height - (2 * num4 + num3)) / 2;
				for (int i = 0; i < num3; i++)
				{
					graphics.DrawLine(pen, rectangle2.Left + num3 / 2, num5 + i, rectangle2.Left + num3 / 2 + 2 * num4, num5 + 2 * num4 + i);
					graphics.DrawLine(pen, rectangle2.Left + num3 / 2 + 2 * num4, num5 + 2 * num4 + i, rectangle2.Left + num3 / 2 + 6 * num4, num5 - 2 * num4 + i);
				}
				return;
			}
			case MenuGlyph.Bullet:
			{
				int num3 = Math.Max(2, rectangle.Width / 3);
				Rectangle rectangle2;
				rectangle2..ctor(rectangle.X + num3, rectangle.Y + num3, rectangle.Width - num3 * 2, rectangle.Height - num3 * 2);
				graphics.FillEllipse(solidBrush, rectangle2);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060038B4 RID: 14516 RVA: 0x000E7BB0 File Offset: 0x000E5DB0
		[MonoInternalNote("Does not respect Mixed")]
		public override void CPDrawMixedCheckBox(Graphics graphics, Rectangle rectangle, ButtonState state)
		{
			this.CPDrawCheckBox(graphics, rectangle, state);
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x000E7BBC File Offset: 0x000E5DBC
		public override void CPDrawRadioButton(Graphics dc, Rectangle rectangle, ButtonState state)
		{
			CPColor cpcolor = this.ResPool.GetCPColor(this.ColorControl);
			Color color = Color.Black;
			Color color2 = Color.Black;
			Color color3 = Color.Black;
			Color color4 = Color.Black;
			Color color5 = Color.Black;
			int num = ((rectangle.Width <= rectangle.Height) ? ((int)((float)rectangle.Width * 0.9f)) : ((int)((float)rectangle.Height * 0.9f)));
			int num2 = num / 2;
			Rectangle rectangle2;
			rectangle2..ctor(rectangle.X + rectangle.Width / 2 - num2, rectangle.Y + rectangle.Height / 2 - num2, num, num);
			Brush brush;
			if ((state & ButtonState.All) == ButtonState.All)
			{
				brush = this.ResPool.GetHatchBrush(12, Color.FromArgb(base.Clamp((int)(this.ColorControl.R + 3), 0, 255), (int)this.ColorControl.G, (int)this.ColorControl.B), this.ColorControl);
				color = cpcolor.Dark;
			}
			else if ((state & ButtonState.Flat) == ButtonState.Flat)
			{
				if ((state & ButtonState.Inactive) == ButtonState.Inactive || (state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					brush = this.ResPool.GetHatchBrush(12, Color.FromArgb(base.Clamp((int)(this.ColorControl.R + 3), 0, 255), (int)this.ColorControl.G, (int)this.ColorControl.B), this.ColorControl);
				}
				else
				{
					brush = SystemBrushes.ControlLightLight;
				}
			}
			else
			{
				if ((state & ButtonState.Inactive) == ButtonState.Inactive || (state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					brush = this.ResPool.GetHatchBrush(12, Color.FromArgb(base.Clamp((int)(this.ColorControl.R + 3), 0, 255), (int)this.ColorControl.G, (int)this.ColorControl.B), this.ColorControl);
				}
				else
				{
					brush = SystemBrushes.ControlLightLight;
				}
				color2 = cpcolor.Dark;
				color3 = cpcolor.DarkDark;
				color4 = cpcolor.Light;
				color5 = Color.Transparent;
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					color = cpcolor.Dark;
				}
			}
			dc.FillEllipse(brush, rectangle2.X + 1, rectangle2.Y + 1, num - 1, num - 1);
			int num3 = Math.Max(1, (int)((float)num * 0.08f));
			dc.DrawArc(this.ResPool.GetSizedPen(color2, num3), rectangle2, 135f, 180f);
			dc.DrawArc(this.ResPool.GetSizedPen(color3, num3), Rectangle.Inflate(rectangle2, -num3, -num3), 135f, 180f);
			dc.DrawArc(this.ResPool.GetSizedPen(color4, num3), rectangle2, 315f, 180f);
			if (color5 != Color.Transparent)
			{
				dc.DrawArc(this.ResPool.GetSizedPen(color5, num3), Rectangle.Inflate(rectangle2, -num3, -num3), 315f, 180f);
			}
			else
			{
				using (Pen pen = new Pen(this.ResPool.GetHatchBrush(12, Color.FromArgb(base.Clamp((int)(this.ColorControl.R + 3), 0, 255), (int)this.ColorControl.G, (int)this.ColorControl.B), this.ColorControl), (float)num3))
				{
					dc.DrawArc(pen, Rectangle.Inflate(rectangle2, -num3, -num3), 315f, 180f);
				}
			}
			if ((state & ButtonState.Checked) == ButtonState.Checked)
			{
				int num4 = num3 * 4;
				Rectangle rectangle3 = Rectangle.Inflate(rectangle2, -num4, -num4);
				if (rectangle.Height > 13)
				{
					rectangle3.X++;
					rectangle3.Y++;
					rectangle3.Height--;
					dc.FillEllipse(this.ResPool.GetSolidBrush(color), rectangle3);
				}
				else
				{
					Pen pen2 = this.ResPool.GetPen(color);
					dc.DrawLine(pen2, rectangle3.X, rectangle3.Y + rectangle3.Height / 2, rectangle3.Right, rectangle3.Y + rectangle3.Height / 2);
					dc.DrawLine(pen2, rectangle3.X, rectangle3.Y + rectangle3.Height / 2 + 1, rectangle3.Right, rectangle3.Y + rectangle3.Height / 2 + 1);
					dc.DrawLine(pen2, rectangle3.X + rectangle3.Width / 2, rectangle3.Y, rectangle3.X + rectangle3.Width / 2, rectangle3.Bottom);
					dc.DrawLine(pen2, rectangle3.X + rectangle3.Width / 2 + 1, rectangle3.Y, rectangle3.X + rectangle3.Width / 2 + 1, rectangle3.Bottom);
				}
			}
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x000E8138 File Offset: 0x000E6338
		public override void CPDrawReversibleFrame(Rectangle rectangle, Color backColor, FrameStyle style)
		{
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x000E813C File Offset: 0x000E633C
		public override void CPDrawReversibleLine(Point start, Point end, Color backColor)
		{
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x000E8140 File Offset: 0x000E6340
		public override void CPDrawScrollButton(Graphics dc, Rectangle area, ScrollButton type, ButtonState state)
		{
			this.DrawScrollButtonPrimitive(dc, area, state);
			bool flag = true;
			int num = 0;
			if ((state & ButtonState.Pushed) != ButtonState.Normal)
			{
				num = 1;
			}
			Rectangle rectangle;
			rectangle..ctor(area.X + 2 + num, area.Y + 2 + num, area.Width - 4, area.Height - 4);
			Point[] array = new Point[3];
			for (int i = 0; i < 3; i++)
			{
				array[i] = default(Point);
			}
			Pen pen = SystemPens.ControlText;
			if ((state & ButtonState.Inactive) != ButtonState.Normal)
			{
				pen = SystemPens.ControlDark;
			}
			switch (type)
			{
			case ScrollButton.Min:
			{
				int num2 = (int)Math.Round((double)((float)rectangle.Width / 2f)) - 1;
				int num3 = (int)Math.Round((double)((float)rectangle.Height / 2f));
				if (num2 == 1)
				{
					num2 = 2;
				}
				if (num3 == 1)
				{
					num3 = 2;
				}
				int num4;
				if (rectangle.Height < 8)
				{
					num4 = 2;
					flag = false;
				}
				else if (rectangle.Height == 11)
				{
					num4 = 3;
				}
				else
				{
					num4 = (int)Math.Round((double)((float)rectangle.Height / 3f));
				}
				array[0].X = rectangle.X + num2;
				array[0].Y = rectangle.Y + num3 - num4 / 2;
				array[1].X = array[0].X + num4 - 1;
				array[1].Y = array[0].Y + num4 - 1;
				array[2].X = array[0].X - num4 + 1;
				array[2].Y = array[1].Y;
				dc.DrawPolygon(pen, array);
				if ((state & ButtonState.Inactive) != ButtonState.Normal)
				{
					dc.DrawLine(SystemPens.ControlLightLight, array[1].X + 1, array[1].Y + 1, array[2].X + 1, array[1].Y + 1);
				}
				if (flag)
				{
					for (int j = 0; j < array[1].Y - array[0].Y; j++)
					{
						dc.DrawLine(pen, array[2].X, array[1].Y - j, array[1].X, array[1].Y - j);
						Point[] array2 = array;
						int num5 = 1;
						array2[num5].X = array2[num5].X - 1;
						Point[] array3 = array;
						int num6 = 2;
						array3[num6].X = array3[num6].X + 1;
					}
				}
				break;
			}
			default:
			{
				int num2 = (int)Math.Round((double)((float)rectangle.Width / 2f)) - 1;
				int num3 = (int)Math.Round((double)((float)rectangle.Height / 2f)) - 1;
				if (num2 == 1)
				{
					num2 = 2;
				}
				int num4;
				if (rectangle.Height < 8)
				{
					num4 = 2;
					flag = false;
				}
				else if (rectangle.Height == 11)
				{
					num4 = 3;
				}
				else
				{
					num4 = (int)Math.Round((double)((float)rectangle.Height / 3f));
				}
				array[0].X = rectangle.X + num2;
				array[0].Y = rectangle.Y + num3 + num4 / 2;
				array[1].X = array[0].X + num4 - 1;
				array[1].Y = array[0].Y - num4 + 1;
				array[2].X = array[0].X - num4 + 1;
				array[2].Y = array[1].Y;
				dc.DrawPolygon(pen, array);
				if ((state & ButtonState.Inactive) != ButtonState.Normal)
				{
					dc.DrawLine(SystemPens.ControlLightLight, array[1].X + 1, array[1].Y + 1, array[0].X + 1, array[0].Y + 1);
					dc.DrawLine(SystemPens.ControlLightLight, array[1].X, array[1].Y + 1, array[0].X + 1, array[0].Y);
				}
				if (flag)
				{
					for (int k = 0; k < array[0].Y - array[1].Y; k++)
					{
						dc.DrawLine(pen, array[1].X, array[1].Y + k, array[2].X, array[1].Y + k);
						Point[] array4 = array;
						int num7 = 1;
						array4[num7].X = array4[num7].X - 1;
						Point[] array5 = array;
						int num8 = 2;
						array5[num8].X = array5[num8].X + 1;
					}
				}
				break;
			}
			case ScrollButton.Left:
			{
				int num3 = (int)Math.Round((double)((float)rectangle.Height / 2f)) - 1;
				if (num3 == 1)
				{
					num3 = 2;
				}
				int num9;
				if (rectangle.Width < 8)
				{
					num9 = 2;
					flag = false;
				}
				else if (rectangle.Width == 11)
				{
					num9 = 3;
				}
				else
				{
					num9 = (int)Math.Round((double)((float)rectangle.Width / 3f));
				}
				array[0].X = rectangle.Left + num9 - 1;
				array[0].Y = rectangle.Y + num3;
				if (array[0].X - 1 == rectangle.X)
				{
					Point[] array6 = array;
					int num10 = 0;
					array6[num10].X = array6[num10].X + 1;
				}
				array[1].X = array[0].X + num9 - 1;
				array[1].Y = array[0].Y - num9 + 1;
				array[2].X = array[1].X;
				array[2].Y = array[0].Y + num9 - 1;
				dc.DrawPolygon(pen, array);
				if ((state & ButtonState.Inactive) != ButtonState.Normal)
				{
					dc.DrawLine(SystemPens.ControlLightLight, array[1].X + 1, array[1].Y + 1, array[2].X + 1, array[2].Y + 1);
				}
				if (flag)
				{
					for (int l = 0; l < array[2].X - array[0].X; l++)
					{
						dc.DrawLine(pen, array[2].X - l, array[1].Y, array[2].X - l, array[2].Y);
						Point[] array7 = array;
						int num11 = 1;
						array7[num11].Y = array7[num11].Y + 1;
						Point[] array8 = array;
						int num12 = 2;
						array8[num12].Y = array8[num12].Y - 1;
					}
				}
				break;
			}
			case ScrollButton.Right:
			{
				int num3 = (int)Math.Round((double)((float)rectangle.Height / 2f)) - 1;
				if (num3 == 1)
				{
					num3 = 2;
				}
				int num9;
				if (rectangle.Width < 8)
				{
					num9 = 2;
					flag = false;
				}
				else if (rectangle.Width == 11)
				{
					num9 = 3;
				}
				else
				{
					num9 = (int)Math.Round((double)((float)rectangle.Width / 3f));
				}
				array[0].X = rectangle.Right - num9 - 1;
				array[0].Y = rectangle.Y + num3;
				if (array[0].X - 1 == rectangle.X)
				{
					Point[] array9 = array;
					int num13 = 0;
					array9[num13].X = array9[num13].X + 1;
				}
				array[1].X = array[0].X - num9 + 1;
				array[1].Y = array[0].Y - num9 + 1;
				array[2].X = array[1].X;
				array[2].Y = array[0].Y + num9 - 1;
				dc.DrawPolygon(pen, array);
				if ((state & ButtonState.Inactive) != ButtonState.Normal)
				{
					dc.DrawLine(SystemPens.ControlLightLight, array[0].X + 1, array[0].Y + 1, array[2].X + 1, array[2].Y + 1);
					dc.DrawLine(SystemPens.ControlLightLight, array[0].X, array[0].Y + 1, array[2].X + 1, array[2].Y);
				}
				if (flag)
				{
					for (int m = 0; m < array[0].X - array[1].X; m++)
					{
						dc.DrawLine(pen, array[2].X + m, array[1].Y, array[2].X + m, array[2].Y);
						Point[] array10 = array;
						int num14 = 1;
						array10[num14].Y = array10[num14].Y + 1;
						Point[] array11 = array;
						int num15 = 2;
						array11[num15].Y = array11[num15].Y - 1;
					}
				}
				break;
			}
			}
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x000E8B18 File Offset: 0x000E6D18
		public override void CPDrawSelectionFrame(Graphics graphics, bool active, Rectangle outsideRect, Rectangle insideRect, Color backColor)
		{
		}

		// Token: 0x060038BA RID: 14522 RVA: 0x000E8B1C File Offset: 0x000E6D1C
		public override void CPDrawSizeGrip(Graphics dc, Color backColor, Rectangle bounds)
		{
			Pen pen = this.ResPool.GetPen(ControlPaint.Dark(backColor));
			Pen pen2 = this.ResPool.GetPen(ControlPaint.LightLight(backColor));
			for (int i = 2; i < bounds.Width - 2; i += 4)
			{
				dc.DrawLine(pen2, bounds.X + i, bounds.Bottom - 2, bounds.Right - 1, bounds.Y + i - 1);
				dc.DrawLine(pen, bounds.X + i + 1, bounds.Bottom - 2, bounds.Right - 1, bounds.Y + i);
				dc.DrawLine(pen, bounds.X + i + 2, bounds.Bottom - 2, bounds.Right - 1, bounds.Y + i + 1);
			}
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x000E8BF0 File Offset: 0x000E6DF0
		private void DrawStringDisabled20(Graphics g, string s, Font font, Rectangle layoutRectangle, Color color, TextFormatFlags flags, bool useDrawString)
		{
			CPColor cpcolor = this.ResPool.GetCPColor(color);
			layoutRectangle.Offset(1, 1);
			TextRenderer.DrawTextInternal(g, s, font, layoutRectangle, cpcolor.LightLight, flags, useDrawString);
			layoutRectangle.Offset(-1, -1);
			TextRenderer.DrawTextInternal(g, s, font, layoutRectangle, cpcolor.Dark, flags, useDrawString);
		}

		// Token: 0x060038BC RID: 14524 RVA: 0x000E8C48 File Offset: 0x000E6E48
		public override void CPDrawStringDisabled(Graphics dc, string s, Font font, Color color, RectangleF layoutRectangle, StringFormat format)
		{
			CPColor cpcolor = this.ResPool.GetCPColor(color);
			dc.DrawString(s, font, this.ResPool.GetSolidBrush(cpcolor.LightLight), new RectangleF(layoutRectangle.X + 1f, layoutRectangle.Y + 1f, layoutRectangle.Width, layoutRectangle.Height), format);
			dc.DrawString(s, font, this.ResPool.GetSolidBrush(cpcolor.Dark), layoutRectangle, format);
		}

		// Token: 0x060038BD RID: 14525 RVA: 0x000E8CCC File Offset: 0x000E6ECC
		public override void CPDrawStringDisabled(IDeviceContext dc, string s, Font font, Color color, Rectangle layoutRectangle, TextFormatFlags format)
		{
			CPColor cpcolor = this.ResPool.GetCPColor(color);
			layoutRectangle.Offset(1, 1);
			TextRenderer.DrawText(dc, s, font, layoutRectangle, cpcolor.LightLight, format);
			layoutRectangle.Offset(-1, -1);
			TextRenderer.DrawText(dc, s, font, layoutRectangle, cpcolor.Dark, format);
		}

		// Token: 0x060038BE RID: 14526 RVA: 0x000E8D20 File Offset: 0x000E6F20
		public override void CPDrawVisualStyleBorder(Graphics graphics, Rectangle bounds)
		{
			graphics.DrawRectangle(SystemPens.ControlDarkDark, bounds);
		}

		// Token: 0x060038BF RID: 14527 RVA: 0x000E8D30 File Offset: 0x000E6F30
		private static void DrawBorderInternal(Graphics graphics, int startX, int startY, int endX, int endY, int width, Color color, ButtonBorderStyle style, Border3DSide side)
		{
			ThemeWin32Classic.DrawBorderInternal(graphics, (float)startX, (float)startY, (float)endX, (float)endY, width, color, style, side);
		}

		// Token: 0x060038C0 RID: 14528 RVA: 0x000E8D54 File Offset: 0x000E6F54
		private static void DrawBorderInternal(Graphics graphics, float startX, float startY, float endX, float endY, int width, Color color, ButtonBorderStyle style, Border3DSide side)
		{
			switch (style)
			{
			case ButtonBorderStyle.Dotted:
			{
				Pen pen = ThemeEngine.Current.ResPool.GetDashPen(color, 2);
				goto IL_0073;
			}
			case ButtonBorderStyle.Dashed:
			{
				Pen pen = ThemeEngine.Current.ResPool.GetDashPen(color, 1);
				goto IL_0073;
			}
			case ButtonBorderStyle.Solid:
			case ButtonBorderStyle.Inset:
			case ButtonBorderStyle.Outset:
			{
				Pen pen = ThemeEngine.Current.ResPool.GetDashPen(color, 0);
				goto IL_0073;
			}
			}
			return;
			IL_0073:
			if (style != ButtonBorderStyle.Inset)
			{
				if (style != ButtonBorderStyle.Outset)
				{
					switch (side)
					{
					case Border3DSide.Left:
					{
						for (int i = 0; i < width; i++)
						{
							Pen pen;
							graphics.DrawLine(pen, startX + (float)i, startY + (float)i, endX + (float)i, endY - (float)i);
						}
						break;
					}
					case Border3DSide.Top:
					{
						for (int j = 0; j < width; j++)
						{
							Pen pen;
							graphics.DrawLine(pen, startX + (float)j, startY + (float)j, endX - (float)j, endY + (float)j);
						}
						break;
					}
					case Border3DSide.Right:
					{
						for (int k = 0; k < width; k++)
						{
							Pen pen;
							graphics.DrawLine(pen, startX - (float)k, startY + (float)k, endX - (float)k, endY - (float)k);
						}
						break;
					}
					case Border3DSide.Bottom:
					{
						for (int l = 0; l < width; l++)
						{
							Pen pen;
							graphics.DrawLine(pen, startX + (float)l, startY - (float)l, endX - (float)l, endY - (float)l);
						}
						break;
					}
					}
				}
				else
				{
					int num;
					int num2;
					int num3;
					ControlPaint.Color2HBS(color, out num, out num2, out num3);
					int num4 = num2 / width;
					int num5;
					if (num2 > 127)
					{
						num5 = Math.Max(6, (160 - num2) / width);
					}
					else
					{
						num5 = (127 - num2) / width;
					}
					for (int m = 0; m < width; m++)
					{
						switch (side)
						{
						case Border3DSide.Left:
						{
							Color color2 = ControlPaint.HBS2Color(num, Math.Min(255, num2 + num5 * (width - m)), num3);
							Pen pen = ThemeEngine.Current.ResPool.GetPen(color2);
							graphics.DrawLine(pen, startX + (float)m, startY + (float)m, endX + (float)m, endY - (float)m);
							break;
						}
						case Border3DSide.Top:
						{
							Color color2 = ControlPaint.HBS2Color(num, Math.Min(255, num2 + num5 * (width - m)), num3);
							Pen pen = ThemeEngine.Current.ResPool.GetPen(color2);
							graphics.DrawLine(pen, startX + (float)m, startY + (float)m, endX - (float)m, endY + (float)m);
							break;
						}
						case Border3DSide.Right:
						{
							Color color2 = ControlPaint.HBS2Color(num, Math.Max(0, num2 - num4 * (width - m)), num3);
							Pen pen = ThemeEngine.Current.ResPool.GetPen(color2);
							graphics.DrawLine(pen, startX - (float)m, startY + (float)m, endX - (float)m, endY - (float)m);
							break;
						}
						case Border3DSide.Bottom:
						{
							Color color2 = ControlPaint.HBS2Color(num, Math.Max(0, num2 - num4 * (width - m)), num3);
							Pen pen = ThemeEngine.Current.ResPool.GetPen(color2);
							graphics.DrawLine(pen, startX + (float)m, startY - (float)m, endX - (float)m, endY - (float)m);
							break;
						}
						}
					}
				}
			}
			else
			{
				int num6;
				int num7;
				int num8;
				ControlPaint.Color2HBS(color, out num6, out num7, out num8);
				int num9 = num7 / width;
				int num10;
				if (num7 > 127)
				{
					num10 = Math.Max(6, (160 - num7) / width);
				}
				else
				{
					num10 = (127 - num7) / width;
				}
				for (int n = 0; n < width; n++)
				{
					switch (side)
					{
					case Border3DSide.Left:
					{
						Color color3 = ControlPaint.HBS2Color(num6, Math.Max(0, num7 - num9 * (width - n)), num8);
						Pen pen = ThemeEngine.Current.ResPool.GetPen(color3);
						graphics.DrawLine(pen, startX + (float)n, startY + (float)n, endX + (float)n, endY - (float)n);
						break;
					}
					case Border3DSide.Top:
					{
						Color color3 = ControlPaint.HBS2Color(num6, Math.Max(0, num7 - num9 * (width - n)), num8);
						Pen pen = ThemeEngine.Current.ResPool.GetPen(color3);
						graphics.DrawLine(pen, startX + (float)n, startY + (float)n, endX - (float)n, endY + (float)n);
						break;
					}
					case Border3DSide.Right:
					{
						Color color3 = ControlPaint.HBS2Color(num6, Math.Min(255, num7 + num10 * (width - n)), num8);
						Pen pen = ThemeEngine.Current.ResPool.GetPen(color3);
						graphics.DrawLine(pen, startX - (float)n, startY + (float)n, endX - (float)n, endY - (float)n);
						break;
					}
					case Border3DSide.Bottom:
					{
						Color color3 = ControlPaint.HBS2Color(num6, Math.Min(255, num7 + num10 * (width - n)), num8);
						Pen pen = ThemeEngine.Current.ResPool.GetPen(color3);
						graphics.DrawLine(pen, startX + (float)n, startY - (float)n, endX - (float)n, endY - (float)n);
						break;
					}
					}
				}
			}
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x000E9294 File Offset: 0x000E7494
		private void DrawCaptionHelper(Graphics graphics, Color color, Pen pen, int lineWidth, int shift, Rectangle captionRect, CaptionButton button)
		{
			switch (button)
			{
			case CaptionButton.Close:
				if (lineWidth < 2)
				{
					graphics.DrawLine(pen, captionRect.Left + 2 * lineWidth + 1 + shift, captionRect.Top + 2 * lineWidth + shift, captionRect.Right - 2 * lineWidth + 1 + shift, captionRect.Bottom - 2 * lineWidth + shift);
					graphics.DrawLine(pen, captionRect.Right - 2 * lineWidth + 1 + shift, captionRect.Top + 2 * lineWidth + shift, captionRect.Left + 2 * lineWidth + 1 + shift, captionRect.Bottom - 2 * lineWidth + shift);
				}
				graphics.DrawLine(pen, captionRect.Left + 2 * lineWidth + shift, captionRect.Top + 2 * lineWidth + shift, captionRect.Right - 2 * lineWidth + shift, captionRect.Bottom - 2 * lineWidth + shift);
				graphics.DrawLine(pen, captionRect.Right - 2 * lineWidth + shift, captionRect.Top + 2 * lineWidth + shift, captionRect.Left + 2 * lineWidth + shift, captionRect.Bottom - 2 * lineWidth + shift);
				return;
			case CaptionButton.Minimize:
			{
				for (int i = 0; i < Math.Max(2, lineWidth); i++)
				{
					graphics.DrawLine(pen, captionRect.Left + lineWidth + shift, captionRect.Bottom - lineWidth + shift - i, captionRect.Right - 3 * lineWidth + shift, captionRect.Bottom - lineWidth + shift - i);
				}
				return;
			}
			case CaptionButton.Maximize:
			{
				for (int j = 0; j < Math.Max(2, lineWidth); j++)
				{
					graphics.DrawLine(pen, captionRect.Left + lineWidth + shift, captionRect.Top + 2 * lineWidth + shift + j, captionRect.Right - lineWidth - lineWidth / 2 + shift, captionRect.Top + 2 * lineWidth + shift + j);
				}
				for (int k = 0; k < Math.Max(1, lineWidth / 2); k++)
				{
					graphics.DrawLine(pen, captionRect.Left + lineWidth + shift + k, captionRect.Top + 2 * lineWidth + shift, captionRect.Left + lineWidth + shift + k, captionRect.Bottom - lineWidth + shift);
				}
				for (int l = 0; l < Math.Max(1, lineWidth / 2); l++)
				{
					graphics.DrawLine(pen, captionRect.Right - lineWidth - lineWidth / 2 + shift + l, captionRect.Top + 2 * lineWidth + shift, captionRect.Right - lineWidth - lineWidth / 2 + shift + l, captionRect.Bottom - lineWidth + shift);
				}
				for (int m = 0; m < Math.Max(1, lineWidth / 2); m++)
				{
					graphics.DrawLine(pen, captionRect.Left + lineWidth + shift, captionRect.Bottom - lineWidth + shift - m, captionRect.Right - lineWidth - lineWidth / 2 + shift, captionRect.Bottom - lineWidth + shift - m);
				}
				return;
			}
			case CaptionButton.Restore:
			{
				for (int n = 0; n < Math.Max(2, lineWidth); n++)
				{
					graphics.DrawLine(pen, captionRect.Left + 3 * lineWidth + shift, captionRect.Top + 2 * lineWidth + shift - n, captionRect.Right - lineWidth - lineWidth / 2 + shift, captionRect.Top + 2 * lineWidth + shift - n);
				}
				for (int num = 0; num < Math.Max(1, lineWidth / 2); num++)
				{
					graphics.DrawLine(pen, captionRect.Left + 3 * lineWidth + shift + num, captionRect.Top + 2 * lineWidth + shift, captionRect.Left + 3 * lineWidth + shift + num, captionRect.Top + 4 * lineWidth + shift);
				}
				for (int num2 = 0; num2 < Math.Max(1, lineWidth / 2); num2++)
				{
					graphics.DrawLine(pen, captionRect.Right - lineWidth - lineWidth / 2 + shift - num2, captionRect.Top + 2 * lineWidth + shift, captionRect.Right - lineWidth - lineWidth / 2 + shift - num2, captionRect.Top + 5 * lineWidth - lineWidth / 2 + shift);
				}
				for (int num3 = 0; num3 < Math.Max(1, lineWidth / 2); num3++)
				{
					graphics.DrawLine(pen, captionRect.Right - 3 * lineWidth - lineWidth / 2 + shift, captionRect.Top + 5 * lineWidth - lineWidth / 2 + shift + 1 + num3, captionRect.Right - lineWidth - lineWidth / 2 + shift, captionRect.Top + 5 * lineWidth - lineWidth / 2 + shift + 1 + num3);
				}
				for (int num4 = 0; num4 < Math.Max(2, lineWidth); num4++)
				{
					graphics.DrawLine(pen, captionRect.Left + lineWidth + shift, captionRect.Top + 4 * lineWidth + shift + 1 - num4, captionRect.Right - 3 * lineWidth - lineWidth / 2 + shift, captionRect.Top + 4 * lineWidth + shift + 1 - num4);
				}
				for (int num5 = 0; num5 < Math.Max(1, lineWidth / 2); num5++)
				{
					graphics.DrawLine(pen, captionRect.Left + lineWidth + shift + num5, captionRect.Top + 4 * lineWidth + shift + 1, captionRect.Left + lineWidth + shift + num5, captionRect.Bottom - lineWidth + shift);
				}
				for (int num6 = 0; num6 < Math.Max(1, lineWidth / 2); num6++)
				{
					graphics.DrawLine(pen, captionRect.Right - 3 * lineWidth - lineWidth / 2 + shift - num6, captionRect.Top + 4 * lineWidth + shift + 1, captionRect.Right - 3 * lineWidth - lineWidth / 2 + shift - num6, captionRect.Bottom - lineWidth + shift);
				}
				for (int num7 = 0; num7 < Math.Max(1, lineWidth / 2); num7++)
				{
					graphics.DrawLine(pen, captionRect.Left + lineWidth + shift, captionRect.Bottom - lineWidth + shift - num7, captionRect.Right - 3 * lineWidth - lineWidth / 2 + shift, captionRect.Bottom - lineWidth + shift - num7);
				}
				return;
			}
			case CaptionButton.Help:
			{
				StringFormat stringFormat = new StringFormat();
				Font font = new Font("Microsoft Sans Serif", (float)captionRect.Height, 1, 2);
				stringFormat.Alignment = 1;
				stringFormat.LineAlignment = 1;
				graphics.DrawString("?", font, this.ResPool.GetSolidBrush(color), (float)(captionRect.X + captionRect.Width / 2 + shift), (float)(captionRect.Y + captionRect.Height / 2 + shift + lineWidth / 2), stringFormat);
				stringFormat.Dispose();
				font.Dispose();
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x000E99C0 File Offset: 0x000E7BC0
		public void DrawScrollButtonPrimitive(Graphics dc, Rectangle area, ButtonState state)
		{
			if ((state & ButtonState.Pushed) == ButtonState.Pushed)
			{
				dc.FillRectangle(SystemBrushes.Control, area.X + 1, area.Y + 1, area.Width - 2, area.Height - 2);
				dc.DrawRectangle(SystemPens.ControlDark, area.X, area.Y, area.Width, area.Height);
				return;
			}
			Brush control = SystemBrushes.Control;
			Brush controlLightLight = SystemBrushes.ControlLightLight;
			Brush controlDark = SystemBrushes.ControlDark;
			Brush controlDarkDark = SystemBrushes.ControlDarkDark;
			dc.FillRectangle(control, area.X, area.Y, area.Width, 1);
			dc.FillRectangle(control, area.X, area.Y, 1, area.Height);
			dc.FillRectangle(controlLightLight, area.X + 1, area.Y + 1, area.Width - 1, 1);
			dc.FillRectangle(controlLightLight, area.X + 1, area.Y + 2, 1, area.Height - 4);
			dc.FillRectangle(controlDark, area.X + 1, area.Y + area.Height - 2, area.Width - 2, 1);
			dc.FillRectangle(controlDarkDark, area.X, area.Y + area.Height - 1, area.Width, 1);
			dc.FillRectangle(controlDark, area.X + area.Width - 2, area.Y + 1, 1, area.Height - 3);
			dc.FillRectangle(controlDarkDark, area.X + area.Width - 1, area.Y, 1, area.Height - 1);
			dc.FillRectangle(control, area.X + 2, area.Y + 2, area.Width - 4, area.Height - 4);
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x000E9BA0 File Offset: 0x000E7DA0
		public override void CPDrawBorderStyle(Graphics dc, Rectangle area, BorderStyle border_style)
		{
			switch (border_style)
			{
			case BorderStyle.FixedSingle:
				dc.DrawRectangle(this.ResPool.GetPen(this.ColorWindowFrame), area.X, area.Y, area.Width - 1, area.Height - 1);
				break;
			case BorderStyle.Fixed3D:
				dc.DrawLine(this.ResPool.GetPen(this.ColorControlDark), area.X, area.Y, area.X + area.Width, area.Y);
				dc.DrawLine(this.ResPool.GetPen(this.ColorControlDark), area.X, area.Y, area.X, area.Y + area.Height);
				dc.DrawLine(this.ResPool.GetPen(this.ColorControlLight), area.X, area.Y + area.Height - 1, area.X + area.Width, area.Y + area.Height - 1);
				dc.DrawLine(this.ResPool.GetPen(this.ColorControlLight), area.X + area.Width - 1, area.Y, area.X + area.Width - 1, area.Y + area.Height);
				dc.DrawLine(this.ResPool.GetPen(this.ColorActiveBorder), area.X + 1, area.Bottom - 2, area.Right - 2, area.Bottom - 2);
				dc.DrawLine(this.ResPool.GetPen(this.ColorActiveBorder), area.Right - 2, area.Top + 1, area.Right - 2, area.Bottom - 2);
				dc.DrawLine(this.ResPool.GetPen(this.ColorControlDarkDark), area.X + 1, area.Top + 1, area.X + 1, area.Bottom - 3);
				dc.DrawLine(this.ResPool.GetPen(this.ColorControlDarkDark), area.X + 1, area.Top + 1, area.Right - 3, area.Top + 1);
				break;
			}
		}

		// Token: 0x040019A0 RID: 6560
		private const int SEPARATOR_HEIGHT = 6;

		// Token: 0x040019A1 RID: 6561
		private const int SEPARATOR_MIN_WIDTH = 20;

		// Token: 0x040019A2 RID: 6562
		private const int SM_CXBORDER = 1;

		// Token: 0x040019A3 RID: 6563
		private const int SM_CYBORDER = 1;

		// Token: 0x040019A4 RID: 6564
		private const int MENU_TAB_SPACE = 8;

		// Token: 0x040019A5 RID: 6565
		private const int MENU_BAR_ITEMS_SPACE = 8;

		// Token: 0x040019A6 RID: 6566
		public const int ProgressBarChunkSpacing = 2;

		// Token: 0x040019A7 RID: 6567
		private const int ProgressBarDefaultHeight = 23;

		// Token: 0x040019A8 RID: 6568
		private const Border3DSide all_sides = Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom;

		// Token: 0x040019A9 RID: 6569
		private const int balloon_iconsize = 16;

		// Token: 0x040019AA RID: 6570
		private const int balloon_bordersize = 8;

		// Token: 0x040019AB RID: 6571
		public const int TrackBarVerticalTrackWidth = 4;

		// Token: 0x040019AC RID: 6572
		public const int TrackBarHorizontalTrackHeight = 4;

		// Token: 0x040019AD RID: 6573
		protected static readonly Color arrow_color = Color.Black;

		// Token: 0x040019AE RID: 6574
		protected static readonly Color pen_ticks_color = Color.Black;

		// Token: 0x040019AF RID: 6575
		protected static StringFormat string_format_menu_text;

		// Token: 0x040019B0 RID: 6576
		protected static StringFormat string_format_menu_shortcut;

		// Token: 0x040019B1 RID: 6577
		protected static StringFormat string_format_menu_menubar_text;

		// Token: 0x040019B2 RID: 6578
		private static ImageAttributes imagedisabled_attributes = null;

		// Token: 0x040019B3 RID: 6579
		private NotifyIcon.BalloonWindow balloon_window;

		// Token: 0x0200032E RID: 814
		private enum VerticalAlignment
		{
			// Token: 0x040019B5 RID: 6581
			Top,
			// Token: 0x040019B6 RID: 6582
			Center,
			// Token: 0x040019B7 RID: 6583
			Bottom
		}

		// Token: 0x0200032F RID: 815
		protected interface ITrackBarTickPainter
		{
			// Token: 0x060038C4 RID: 14532
			void Paint(float x1, float y1, float x2, float y2);
		}

		// Token: 0x02000330 RID: 816
		private class TrackBarTickPainter : ThemeWin32Classic.ITrackBarTickPainter
		{
			// Token: 0x060038C5 RID: 14533 RVA: 0x000E9E0C File Offset: 0x000E800C
			public TrackBarTickPainter(Graphics g, Pen pen)
			{
				this.g = g;
				this.pen = pen;
			}

			// Token: 0x060038C6 RID: 14534 RVA: 0x000E9E24 File Offset: 0x000E8024
			public void Paint(float x1, float y1, float x2, float y2)
			{
				this.g.DrawLine(this.pen, x1, y1, x2, y2);
			}

			// Token: 0x040019B8 RID: 6584
			private readonly Graphics g;

			// Token: 0x040019B9 RID: 6585
			private readonly Pen pen;
		}
	}
}
