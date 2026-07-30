using System;
using System.Drawing;

namespace System.Windows.Forms.Theming.Default
{
	// Token: 0x020004C8 RID: 1224
	internal class ButtonPainter
	{
		// Token: 0x17001332 RID: 4914
		// (get) Token: 0x06004C3F RID: 19519 RVA: 0x0012F7A8 File Offset: 0x0012D9A8
		protected SystemResPool ResPool
		{
			get
			{
				return ThemeEngine.Current.ResPool;
			}
		}

		// Token: 0x06004C40 RID: 19520 RVA: 0x0012F7B4 File Offset: 0x0012D9B4
		public virtual void Draw(Graphics g, Rectangle bounds, ButtonThemeState state, Color backColor, Color foreColor)
		{
			bool flag = backColor.ToArgb() == ThemeEngine.Current.ColorControl.ToArgb() || backColor == Color.Empty;
			CPColor cpcolor = ((!flag) ? this.ResPool.GetCPColor(backColor) : CPColor.Empty);
			switch (state)
			{
			case ButtonThemeState.Normal:
			case ButtonThemeState.Entered:
			case ButtonThemeState.Disabled:
			{
				Pen pen = ((!flag) ? this.ResPool.GetPen(cpcolor.LightLight) : SystemPens.ControlLightLight);
				g.DrawLine(pen, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 2);
				g.DrawLine(pen, bounds.X + 1, bounds.Y, bounds.Right - 2, bounds.Y);
				pen = ((!flag) ? this.ResPool.GetPen(backColor) : SystemPens.Control);
				g.DrawLine(pen, bounds.X + 1, bounds.Y + 1, bounds.X + 1, bounds.Bottom - 3);
				g.DrawLine(pen, bounds.X + 2, bounds.Y + 1, bounds.Right - 3, bounds.Y + 1);
				pen = ((!flag) ? this.ResPool.GetPen(cpcolor.Dark) : SystemPens.ControlDark);
				g.DrawLine(pen, bounds.X + 1, bounds.Bottom - 2, bounds.Right - 2, bounds.Bottom - 2);
				g.DrawLine(pen, bounds.Right - 2, bounds.Y + 1, bounds.Right - 2, bounds.Bottom - 3);
				pen = ((!flag) ? this.ResPool.GetPen(cpcolor.DarkDark) : SystemPens.ControlDarkDark);
				g.DrawLine(pen, bounds.X, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1);
				g.DrawLine(pen, bounds.Right - 1, bounds.Y, bounds.Right - 1, bounds.Bottom - 2);
				break;
			}
			default:
				if (state == ButtonThemeState.Default)
				{
					g.DrawRectangle(this.ResPool.GetPen(foreColor), bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
					bounds.Inflate(-1, -1);
					Pen pen = ((!flag) ? this.ResPool.GetPen(cpcolor.LightLight) : SystemPens.ControlLightLight);
					g.DrawLine(pen, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 2);
					g.DrawLine(pen, bounds.X + 1, bounds.Y, bounds.Right - 2, bounds.Y);
					pen = ((!flag) ? this.ResPool.GetPen(backColor) : SystemPens.Control);
					g.DrawLine(pen, bounds.X + 1, bounds.Y + 1, bounds.X + 1, bounds.Bottom - 3);
					g.DrawLine(pen, bounds.X + 2, bounds.Y + 1, bounds.Right - 3, bounds.Y + 1);
					pen = ((!flag) ? this.ResPool.GetPen(cpcolor.Dark) : SystemPens.ControlDark);
					g.DrawLine(pen, bounds.X + 1, bounds.Bottom - 2, bounds.Right - 2, bounds.Bottom - 2);
					g.DrawLine(pen, bounds.Right - 2, bounds.Y + 1, bounds.Right - 2, bounds.Bottom - 3);
					pen = ((!flag) ? this.ResPool.GetPen(cpcolor.DarkDark) : SystemPens.ControlDarkDark);
					g.DrawLine(pen, bounds.X, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1);
					g.DrawLine(pen, bounds.Right - 1, bounds.Y, bounds.Right - 1, bounds.Bottom - 2);
				}
				break;
			case ButtonThemeState.Pressed:
			{
				g.DrawRectangle(this.ResPool.GetPen(foreColor), bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
				bounds.Inflate(-1, -1);
				Pen pen = ((!flag) ? this.ResPool.GetPen(cpcolor.Dark) : SystemPens.ControlDark);
				g.DrawRectangle(pen, bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
				break;
			}
			}
		}

		// Token: 0x06004C41 RID: 19521 RVA: 0x0012FCC4 File Offset: 0x0012DEC4
		public virtual void DrawFlat(Graphics g, Rectangle bounds, ButtonThemeState state, Color backColor, Color foreColor, FlatButtonAppearance appearance)
		{
			bool flag = backColor.ToArgb() == ThemeEngine.Current.ColorControl.ToArgb() || backColor == Color.Empty;
			CPColor cpcolor = ((!flag) ? this.ResPool.GetCPColor(backColor) : CPColor.Empty);
			switch (state)
			{
			case ButtonThemeState.Normal:
			case ButtonThemeState.Disabled:
				goto IL_0183;
			case ButtonThemeState.Entered:
				break;
			default:
				switch (state)
				{
				case ButtonThemeState.Default:
					if (appearance.CheckedBackColor != Color.Empty)
					{
						g.FillRectangle(this.ResPool.GetSolidBrush(appearance.CheckedBackColor), bounds);
					}
					goto IL_0183;
				case ButtonThemeState.Normal | ButtonThemeState.Default:
					goto IL_0183;
				case ButtonThemeState.Entered | ButtonThemeState.Default:
					break;
				default:
					goto IL_0183;
				}
				break;
			case ButtonThemeState.Pressed:
				if (appearance.MouseDownBackColor != Color.Empty)
				{
					g.FillRectangle(this.ResPool.GetSolidBrush(appearance.MouseDownBackColor), bounds);
				}
				else
				{
					g.FillRectangle(this.ResPool.GetSolidBrush(ButtonPainter.ChangeIntensity(backColor, 0.95f)), bounds);
				}
				goto IL_0183;
			}
			if (appearance.MouseOverBackColor != Color.Empty)
			{
				g.FillRectangle(this.ResPool.GetSolidBrush(appearance.MouseOverBackColor), bounds);
			}
			else
			{
				g.FillRectangle(this.ResPool.GetSolidBrush(ButtonPainter.ChangeIntensity(backColor, 0.9f)), bounds);
			}
			IL_0183:
			Pen pen;
			if (appearance.BorderColor == Color.Empty)
			{
				pen = ((!flag) ? this.ResPool.GetSizedPen(cpcolor.DarkDark, appearance.BorderSize) : SystemPens.ControlDarkDark);
			}
			else
			{
				pen = this.ResPool.GetSizedPen(appearance.BorderColor, appearance.BorderSize);
			}
			bounds.Width--;
			bounds.Height--;
			if (appearance.BorderSize > 0)
			{
				g.DrawRectangle(pen, bounds);
			}
		}

		// Token: 0x06004C42 RID: 19522 RVA: 0x0012FEE8 File Offset: 0x0012E0E8
		public virtual void DrawPopup(Graphics g, Rectangle bounds, ButtonThemeState state, Color backColor, Color foreColor)
		{
			bool flag = backColor.ToArgb() == ThemeEngine.Current.ColorControl.ToArgb() || backColor == Color.Empty;
			CPColor cpcolor = ((!flag) ? this.ResPool.GetCPColor(backColor) : CPColor.Empty);
			Pen pen;
			switch (state)
			{
			case ButtonThemeState.Normal:
			case ButtonThemeState.Pressed:
			case ButtonThemeState.Disabled:
				break;
			case ButtonThemeState.Entered:
				pen = ((!flag) ? this.ResPool.GetPen(cpcolor.LightLight) : SystemPens.ControlLightLight);
				g.DrawLine(pen, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 2);
				g.DrawLine(pen, bounds.X + 1, bounds.Y, bounds.Right - 2, bounds.Y);
				pen = ((!flag) ? this.ResPool.GetPen(cpcolor.Dark) : SystemPens.ControlDark);
				g.DrawLine(pen, bounds.X, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1);
				g.DrawLine(pen, bounds.Right - 1, bounds.Y, bounds.Right - 1, bounds.Bottom - 2);
				return;
			default:
				if (state != ButtonThemeState.Default)
				{
					return;
				}
				break;
			}
			pen = ((!flag) ? this.ResPool.GetPen(cpcolor.DarkDark) : SystemPens.ControlDarkDark);
			bounds.Width--;
			bounds.Height--;
			g.DrawRectangle(pen, bounds);
			if (state == ButtonThemeState.Default || state == ButtonThemeState.Pressed)
			{
				bounds.Inflate(-1, -1);
				g.DrawRectangle(pen, bounds);
			}
		}

		// Token: 0x06004C43 RID: 19523 RVA: 0x001300DC File Offset: 0x0012E2DC
		private static Color ChangeIntensity(Color baseColor, float percent)
		{
			int num;
			int num2;
			int num3;
			ControlPaint.Color2HBS(baseColor, out num, out num2, out num3);
			int num4 = Math.Min(255, (int)((float)num2 * percent));
			return ControlPaint.HBS2Color(num, num4, num3);
		}
	}
}
