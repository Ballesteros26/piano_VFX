using System;
using System.Drawing;

namespace System.Windows.Forms.Theming.Default
{
	// Token: 0x020004C9 RID: 1225
	internal class CheckBoxPainter
	{
		// Token: 0x17001333 RID: 4915
		// (get) Token: 0x06004C45 RID: 19525 RVA: 0x00130118 File Offset: 0x0012E318
		protected SystemResPool ResPool
		{
			get
			{
				return ThemeEngine.Current.ResPool;
			}
		}

		// Token: 0x06004C46 RID: 19526 RVA: 0x00130124 File Offset: 0x0012E324
		public void PaintCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, ElementState state, FlatStyle style, CheckState checkState)
		{
			switch (style)
			{
			case FlatStyle.Flat:
				switch (state)
				{
				case ElementState.Normal:
					this.DrawFlatNormalCheckBox(g, bounds, backColor, foreColor, checkState);
					break;
				case ElementState.Hot:
					this.DrawFlatHotCheckBox(g, bounds, backColor, foreColor, checkState);
					break;
				case ElementState.Pressed:
					this.DrawFlatPressedCheckBox(g, bounds, backColor, foreColor, checkState);
					break;
				case ElementState.Disabled:
					this.DrawFlatDisabledCheckBox(g, bounds, backColor, foreColor, checkState);
					break;
				}
				break;
			case FlatStyle.Popup:
				switch (state)
				{
				case ElementState.Normal:
					this.DrawPopupNormalCheckBox(g, bounds, backColor, foreColor, checkState);
					break;
				case ElementState.Hot:
					this.DrawPopupHotCheckBox(g, bounds, backColor, foreColor, checkState);
					break;
				case ElementState.Pressed:
					this.DrawPopupPressedCheckBox(g, bounds, backColor, foreColor, checkState);
					break;
				case ElementState.Disabled:
					this.DrawPopupDisabledCheckBox(g, bounds, backColor, foreColor, checkState);
					break;
				}
				break;
			case FlatStyle.Standard:
			case FlatStyle.System:
				switch (state)
				{
				case ElementState.Normal:
					this.DrawNormalCheckBox(g, bounds, backColor, foreColor, checkState);
					break;
				case ElementState.Hot:
					this.DrawHotCheckBox(g, bounds, backColor, foreColor, checkState);
					break;
				case ElementState.Pressed:
					this.DrawPressedCheckBox(g, bounds, backColor, foreColor, checkState);
					break;
				case ElementState.Disabled:
					this.DrawDisabledCheckBox(g, bounds, backColor, foreColor, checkState);
					break;
				}
				break;
			}
		}

		// Token: 0x06004C47 RID: 19527 RVA: 0x00130298 File Offset: 0x0012E498
		public virtual void DrawNormalCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			int num = ((bounds.Height <= bounds.Width) ? bounds.Height : bounds.Width);
			int num2 = Math.Max(0, bounds.X + bounds.Width / 2 - num / 2);
			int num3 = Math.Max(0, bounds.Y + bounds.Height / 2 - num / 2);
			Rectangle rectangle;
			rectangle..ctor(num2, num3, num, num);
			g.FillRectangle(SystemBrushes.ControlLightLight, rectangle.X + 2, rectangle.Y + 2, rectangle.Width - 3, rectangle.Height - 3);
			Pen pen = SystemPens.ControlDark;
			g.DrawLine(pen, rectangle.X, rectangle.Y, rectangle.X, rectangle.Bottom - 2);
			g.DrawLine(pen, rectangle.X + 1, rectangle.Y, rectangle.Right - 2, rectangle.Y);
			pen = SystemPens.ControlDarkDark;
			g.DrawLine(pen, rectangle.X + 1, rectangle.Y + 1, rectangle.X + 1, rectangle.Bottom - 3);
			g.DrawLine(pen, rectangle.X + 2, rectangle.Y + 1, rectangle.Right - 3, rectangle.Y + 1);
			pen = SystemPens.ControlLightLight;
			g.DrawLine(pen, rectangle.Right - 1, rectangle.Y, rectangle.Right - 1, rectangle.Bottom - 1);
			g.DrawLine(pen, rectangle.X, rectangle.Bottom - 1, rectangle.Right - 1, rectangle.Bottom - 1);
			using (Pen pen2 = new Pen(this.ResPool.GetHatchBrush(12, Color.FromArgb(this.Clamp((int)(this.ColorControl.R + 3), 0, 255), (int)this.ColorControl.G, (int)this.ColorControl.B), this.ColorControl)))
			{
				g.DrawLine(pen2, rectangle.X + 1, rectangle.Bottom - 2, rectangle.Right - 2, rectangle.Bottom - 2);
				g.DrawLine(pen2, rectangle.Right - 2, rectangle.Y + 1, rectangle.Right - 2, rectangle.Bottom - 2);
			}
			if (state == CheckState.Checked)
			{
				this.DrawCheck(g, bounds, Color.Black);
			}
			else if (state == CheckState.Indeterminate)
			{
				this.DrawCheck(g, bounds, SystemColors.ControlDarkDark);
			}
		}

		// Token: 0x06004C48 RID: 19528 RVA: 0x00130560 File Offset: 0x0012E760
		public virtual void DrawHotCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			this.DrawNormalCheckBox(g, bounds, backColor, foreColor, state);
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x00130570 File Offset: 0x0012E770
		public virtual void DrawPressedCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			int num = ((bounds.Height <= bounds.Width) ? bounds.Height : bounds.Width);
			int num2 = Math.Max(0, bounds.X + bounds.Width / 2 - num / 2);
			int num3 = Math.Max(0, bounds.Y + bounds.Height / 2 - num / 2);
			Rectangle rectangle;
			rectangle..ctor(num2, num3, num, num);
			g.FillRectangle(this.ResPool.GetHatchBrush(12, Color.FromArgb(this.Clamp((int)(this.ColorControl.R + 3), 0, 255), (int)this.ColorControl.G, (int)this.ColorControl.B), this.ColorControl), rectangle.X + 2, rectangle.Y + 2, rectangle.Width - 3, rectangle.Height - 3);
			Pen pen = SystemPens.ControlDark;
			g.DrawLine(pen, rectangle.X, rectangle.Y, rectangle.X, rectangle.Bottom - 2);
			g.DrawLine(pen, rectangle.X + 1, rectangle.Y, rectangle.Right - 2, rectangle.Y);
			pen = SystemPens.ControlDarkDark;
			g.DrawLine(pen, rectangle.X + 1, rectangle.Y + 1, rectangle.X + 1, rectangle.Bottom - 3);
			g.DrawLine(pen, rectangle.X + 2, rectangle.Y + 1, rectangle.Right - 3, rectangle.Y + 1);
			pen = SystemPens.ControlLightLight;
			g.DrawLine(pen, rectangle.Right - 1, rectangle.Y, rectangle.Right - 1, rectangle.Bottom - 1);
			g.DrawLine(pen, rectangle.X, rectangle.Bottom - 1, rectangle.Right - 1, rectangle.Bottom - 1);
			using (Pen pen2 = new Pen(this.ResPool.GetHatchBrush(12, Color.FromArgb(this.Clamp((int)(this.ColorControl.R + 3), 0, 255), (int)this.ColorControl.G, (int)this.ColorControl.B), this.ColorControl)))
			{
				g.DrawLine(pen2, rectangle.X + 1, rectangle.Bottom - 2, rectangle.Right - 2, rectangle.Bottom - 2);
				g.DrawLine(pen2, rectangle.Right - 2, rectangle.Y + 1, rectangle.Right - 2, rectangle.Bottom - 2);
			}
			if (state == CheckState.Checked)
			{
				this.DrawCheck(g, bounds, Color.Black);
			}
			else if (state == CheckState.Indeterminate)
			{
				this.DrawCheck(g, bounds, SystemColors.ControlDarkDark);
			}
		}

		// Token: 0x06004C4A RID: 19530 RVA: 0x00130884 File Offset: 0x0012EA84
		public virtual void DrawDisabledCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			this.DrawPressedCheckBox(g, bounds, backColor, foreColor, CheckState.Unchecked);
			if (state == CheckState.Checked || state == CheckState.Indeterminate)
			{
				this.DrawCheck(g, bounds, SystemColors.ControlDark);
			}
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x001308B0 File Offset: 0x0012EAB0
		public virtual void DrawFlatNormalCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			Rectangle rectangle;
			rectangle..ctor(bounds.X, bounds.Y, Math.Max(bounds.Width - 2, 0), Math.Max(bounds.Height - 2, 0));
			Rectangle rectangle2;
			rectangle2..ctor(rectangle.X + 1, rectangle.Y + 1, Math.Max(rectangle.Width - 2, 0), Math.Max(rectangle.Height - 2, 0));
			g.FillRectangle(this.ResPool.GetSolidBrush(ControlPaint.LightLight(backColor)), rectangle2);
			ControlPaint.DrawBorder(g, rectangle, foreColor, ButtonBorderStyle.Solid);
			bounds.Offset(-1, 0);
			if (state == CheckState.Checked)
			{
				this.DrawCheck(g, bounds, Color.Black);
			}
			else if (state == CheckState.Indeterminate)
			{
				this.DrawCheck(g, bounds, SystemColors.ControlDarkDark);
			}
		}

		// Token: 0x06004C4C RID: 19532 RVA: 0x00130984 File Offset: 0x0012EB84
		public virtual void DrawFlatHotCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			Rectangle rectangle;
			rectangle..ctor(bounds.X, bounds.Y, Math.Max(bounds.Width - 2, 0), Math.Max(bounds.Height - 2, 0));
			Rectangle rectangle2;
			rectangle2..ctor(rectangle.X + 1, rectangle.Y + 1, Math.Max(rectangle.Width - 2, 0), Math.Max(rectangle.Height - 2, 0));
			g.FillRectangle(this.ResPool.GetSolidBrush(backColor), rectangle2);
			ControlPaint.DrawBorder(g, rectangle, foreColor, ButtonBorderStyle.Solid);
			bounds.Offset(-1, 0);
			if (state == CheckState.Checked)
			{
				this.DrawCheck(g, bounds, Color.Black);
			}
			else if (state == CheckState.Indeterminate)
			{
				this.DrawCheck(g, bounds, SystemColors.ControlDarkDark);
			}
		}

		// Token: 0x06004C4D RID: 19533 RVA: 0x00130A50 File Offset: 0x0012EC50
		public virtual void DrawFlatPressedCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			this.DrawFlatNormalCheckBox(g, bounds, backColor, foreColor, state);
		}

		// Token: 0x06004C4E RID: 19534 RVA: 0x00130A60 File Offset: 0x0012EC60
		public virtual void DrawFlatDisabledCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			Rectangle rectangle;
			rectangle..ctor(bounds.X, bounds.Y, Math.Max(bounds.Width - 2, 0), Math.Max(bounds.Height - 2, 0));
			ControlPaint.DrawBorder(g, rectangle, foreColor, ButtonBorderStyle.Solid);
			bounds.Offset(-1, 0);
			if (state == CheckState.Checked || state == CheckState.Indeterminate)
			{
				this.DrawCheck(g, bounds, SystemColors.ControlDarkDark);
			}
		}

		// Token: 0x06004C4F RID: 19535 RVA: 0x00130AD0 File Offset: 0x0012ECD0
		public virtual void DrawPopupNormalCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			this.DrawFlatNormalCheckBox(g, bounds, backColor, foreColor, state);
		}

		// Token: 0x06004C50 RID: 19536 RVA: 0x00130AE0 File Offset: 0x0012ECE0
		public virtual void DrawPopupHotCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			Rectangle rectangle;
			rectangle..ctor(bounds.X, bounds.Y, Math.Max(bounds.Width - 1, 0), Math.Max(bounds.Height - 1, 0));
			Rectangle rectangle2;
			rectangle2..ctor(rectangle.X + 1, rectangle.Y + 1, Math.Max(rectangle.Width - 3, 0), Math.Max(rectangle.Height - 3, 0));
			g.FillRectangle(this.ResPool.GetSolidBrush(ControlPaint.LightLight(backColor)), rectangle2);
			ThemeEngine.Current.CPDrawBorder3D(g, rectangle, Border3DStyle.SunkenInner, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom, backColor);
			bounds.Offset(-1, 0);
			if (state == CheckState.Checked)
			{
				this.DrawCheck(g, bounds, Color.Black);
			}
			else if (state == CheckState.Indeterminate)
			{
				this.DrawCheck(g, bounds, SystemColors.ControlDarkDark);
			}
		}

		// Token: 0x06004C51 RID: 19537 RVA: 0x00130BB8 File Offset: 0x0012EDB8
		public virtual void DrawPopupPressedCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			Rectangle rectangle;
			rectangle..ctor(bounds.X, bounds.Y, Math.Max(bounds.Width - 1, 0), Math.Max(bounds.Height - 1, 0));
			Rectangle rectangle2;
			rectangle2..ctor(rectangle.X + 1, rectangle.Y + 1, Math.Max(rectangle.Width - 3, 0), Math.Max(rectangle.Height - 3, 0));
			g.FillRectangle(this.ResPool.GetSolidBrush(backColor), rectangle2);
			ThemeEngine.Current.CPDrawBorder3D(g, rectangle, Border3DStyle.SunkenInner, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom, backColor);
			bounds.Offset(-1, 0);
			if (state == CheckState.Checked)
			{
				this.DrawCheck(g, bounds, Color.Black);
			}
			else if (state == CheckState.Indeterminate)
			{
				this.DrawCheck(g, bounds, SystemColors.ControlDarkDark);
			}
		}

		// Token: 0x06004C52 RID: 19538 RVA: 0x00130C8C File Offset: 0x0012EE8C
		public virtual void DrawPopupDisabledCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			this.DrawFlatDisabledCheckBox(g, bounds, backColor, foreColor, state);
		}

		// Token: 0x06004C53 RID: 19539 RVA: 0x00130C9C File Offset: 0x0012EE9C
		public virtual void DrawCheck(Graphics g, Rectangle bounds, Color checkColor)
		{
			int num = ((bounds.Height <= bounds.Width) ? (bounds.Height / 2) : (bounds.Width / 2));
			Pen pen = this.ResPool.GetPen(checkColor);
			if (num < 7)
			{
				int num2 = Math.Max(3, num / 3);
				int num3 = Math.Max(1, num / 9);
				Rectangle rectangle;
				rectangle..ctor(bounds.X + bounds.Width / 2 - num / 2 - 1, bounds.Y + bounds.Height / 2 - num / 2 - 1, num, num);
				for (int i = 0; i < num2; i++)
				{
					g.DrawLine(pen, rectangle.Left + num2 / 2, rectangle.Top + num2 + i, rectangle.Left + num2 / 2 + 2 * num3, rectangle.Top + num2 + 2 * num3 + i);
					g.DrawLine(pen, rectangle.Left + num2 / 2 + 2 * num3, rectangle.Top + num2 + 2 * num3 + i, rectangle.Left + num2 / 2 + 6 * num3, rectangle.Top + num2 - 2 * num3 + i);
				}
			}
			else
			{
				int num4 = Math.Max(3, num / 3) + 1;
				int num5 = bounds.Width / 2;
				int num6 = bounds.Height / 2;
				Rectangle rectangle2;
				rectangle2..ctor(bounds.X + num5 - num / 2 - 1, bounds.Y + num6 - num / 2, num, num);
				int num7 = num / 3;
				int num8 = num - num7 - 1;
				for (int j = 0; j < num4; j++)
				{
					g.DrawLine(pen, rectangle2.X, rectangle2.Bottom - 1 - num7 - j, rectangle2.X + num7, rectangle2.Bottom - 1 - j);
					g.DrawLine(pen, rectangle2.X + num7, rectangle2.Bottom - 1 - j, rectangle2.Right - 1, rectangle2.Bottom - j - 1 - num8);
				}
			}
		}

		// Token: 0x06004C54 RID: 19540 RVA: 0x00130EA8 File Offset: 0x0012F0A8
		private int Clamp(int value, int lower, int upper)
		{
			if (value < lower)
			{
				return lower;
			}
			if (value > upper)
			{
				return upper;
			}
			return value;
		}

		// Token: 0x17001334 RID: 4916
		// (get) Token: 0x06004C55 RID: 19541 RVA: 0x00130EC0 File Offset: 0x0012F0C0
		private Color ColorControl
		{
			get
			{
				return SystemColors.Control;
			}
		}
	}
}
