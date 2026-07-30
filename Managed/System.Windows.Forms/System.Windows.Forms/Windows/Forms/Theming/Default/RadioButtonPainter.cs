using System;
using System.Drawing;

namespace System.Windows.Forms.Theming.Default
{
	// Token: 0x020004CA RID: 1226
	internal class RadioButtonPainter
	{
		// Token: 0x17001335 RID: 4917
		// (get) Token: 0x06004C57 RID: 19543 RVA: 0x00130ED0 File Offset: 0x0012F0D0
		protected SystemResPool ResPool
		{
			get
			{
				return ThemeEngine.Current.ResPool;
			}
		}

		// Token: 0x06004C58 RID: 19544 RVA: 0x00130EDC File Offset: 0x0012F0DC
		public void PaintRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, ElementState state, FlatStyle style, bool isChecked)
		{
			switch (style)
			{
			case FlatStyle.Flat:
				switch (state)
				{
				case ElementState.Normal:
					this.DrawFlatNormalRadioButton(g, bounds, backColor, foreColor, isChecked);
					break;
				case ElementState.Hot:
					this.DrawFlatHotRadioButton(g, bounds, backColor, foreColor, isChecked);
					break;
				case ElementState.Pressed:
					this.DrawFlatPressedRadioButton(g, bounds, backColor, foreColor, isChecked);
					break;
				case ElementState.Disabled:
					this.DrawFlatDisabledRadioButton(g, bounds, backColor, foreColor, isChecked);
					break;
				}
				break;
			case FlatStyle.Popup:
				switch (state)
				{
				case ElementState.Normal:
					this.DrawPopupNormalRadioButton(g, bounds, backColor, foreColor, isChecked);
					break;
				case ElementState.Hot:
					this.DrawPopupHotRadioButton(g, bounds, backColor, foreColor, isChecked);
					break;
				case ElementState.Pressed:
					this.DrawPopupPressedRadioButton(g, bounds, backColor, foreColor, isChecked);
					break;
				case ElementState.Disabled:
					this.DrawPopupDisabledRadioButton(g, bounds, backColor, foreColor, isChecked);
					break;
				}
				break;
			case FlatStyle.Standard:
			case FlatStyle.System:
				switch (state)
				{
				case ElementState.Normal:
					this.DrawNormalRadioButton(g, bounds, backColor, foreColor, isChecked);
					break;
				case ElementState.Hot:
					this.DrawHotRadioButton(g, bounds, backColor, foreColor, isChecked);
					break;
				case ElementState.Pressed:
					this.DrawPressedRadioButton(g, bounds, backColor, foreColor, isChecked);
					break;
				case ElementState.Disabled:
					this.DrawDisabledRadioButton(g, bounds, backColor, foreColor, isChecked);
					break;
				}
				break;
			}
		}

		// Token: 0x06004C59 RID: 19545 RVA: 0x00131050 File Offset: 0x0012F250
		public virtual void DrawNormalRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
		{
			ButtonState buttonState = ButtonState.Normal;
			if (isChecked)
			{
				buttonState |= ButtonState.Checked;
			}
			ControlPaint.DrawRadioButton(g, bounds, buttonState);
		}

		// Token: 0x06004C5A RID: 19546 RVA: 0x00131078 File Offset: 0x0012F278
		public virtual void DrawHotRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
		{
			this.DrawNormalRadioButton(g, bounds, backColor, foreColor, isChecked);
		}

		// Token: 0x06004C5B RID: 19547 RVA: 0x00131088 File Offset: 0x0012F288
		public virtual void DrawPressedRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
		{
			ButtonState buttonState = ButtonState.Pushed;
			if (isChecked)
			{
				buttonState |= ButtonState.Checked;
			}
			ControlPaint.DrawRadioButton(g, bounds, buttonState);
		}

		// Token: 0x06004C5C RID: 19548 RVA: 0x001310B4 File Offset: 0x0012F2B4
		public virtual void DrawDisabledRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
		{
			ButtonState buttonState = ButtonState.Inactive;
			if (isChecked)
			{
				buttonState |= ButtonState.Checked;
			}
			ControlPaint.DrawRadioButton(g, bounds, buttonState);
		}

		// Token: 0x06004C5D RID: 19549 RVA: 0x001310E0 File Offset: 0x0012F2E0
		public virtual void DrawFlatNormalRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
		{
			g.DrawArc(SystemPens.ControlDarkDark, bounds, 0f, 359f);
			g.FillPie(SystemBrushes.ControlLightLight, bounds.X + 1, bounds.Y + 1, bounds.Width - 2, bounds.Height - 2, 0, 359);
			if (isChecked)
			{
				this.DrawFlatRadioGlyphDot(g, bounds, SystemColors.ControlDarkDark);
			}
		}

		// Token: 0x06004C5E RID: 19550 RVA: 0x0013114C File Offset: 0x0012F34C
		public virtual void DrawFlatHotRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
		{
			g.DrawArc(SystemPens.ControlDarkDark, bounds, 0f, 359f);
			g.FillPie(SystemBrushes.ControlLight, bounds.X + 1, bounds.Y + 1, bounds.Width - 2, bounds.Height - 2, 0, 359);
			if (isChecked)
			{
				this.DrawFlatRadioGlyphDot(g, bounds, SystemColors.ControlDarkDark);
			}
		}

		// Token: 0x06004C5F RID: 19551 RVA: 0x001311B8 File Offset: 0x0012F3B8
		public virtual void DrawFlatPressedRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
		{
			g.DrawArc(SystemPens.ControlDarkDark, bounds, 0f, 359f);
			g.FillPie(SystemBrushes.ControlLightLight, bounds.X + 1, bounds.Y + 1, bounds.Width - 2, bounds.Height - 2, 0, 359);
			if (isChecked)
			{
				this.DrawFlatRadioGlyphDot(g, bounds, SystemColors.ControlDarkDark);
			}
		}

		// Token: 0x06004C60 RID: 19552 RVA: 0x00131224 File Offset: 0x0012F424
		public virtual void DrawFlatDisabledRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
		{
			g.FillPie(SystemBrushes.Control, bounds.X + 1, bounds.Y + 1, bounds.Width - 2, bounds.Height - 2, 0, 359);
			g.DrawArc(SystemPens.ControlDark, bounds, 0f, 359f);
			if (isChecked)
			{
				this.DrawFlatRadioGlyphDot(g, bounds, SystemColors.ControlDark);
			}
		}

		// Token: 0x06004C61 RID: 19553 RVA: 0x00131290 File Offset: 0x0012F490
		public virtual void DrawPopupNormalRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
		{
			g.FillPie(SystemBrushes.ControlLightLight, bounds, 0f, 359f);
			g.DrawArc(SystemPens.ControlDark, bounds, 0f, 359f);
			if (isChecked)
			{
				this.DrawFlatRadioGlyphDot(g, bounds, SystemColors.ControlDarkDark);
			}
		}

		// Token: 0x06004C62 RID: 19554 RVA: 0x001312E0 File Offset: 0x0012F4E0
		public virtual void DrawPopupHotRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
		{
			g.FillPie(SystemBrushes.ControlLightLight, bounds, 0f, 359f);
			g.DrawArc(SystemPens.ControlLight, bounds.X + 1, bounds.Y + 1, bounds.Width - 2, bounds.Height - 2, 0, 359);
			g.DrawArc(SystemPens.ControlDark, bounds, 135f, 180f);
			g.DrawArc(SystemPens.ControlLightLight, bounds, 315f, 180f);
			if (isChecked)
			{
				this.DrawFlatRadioGlyphDot(g, bounds, SystemColors.ControlDarkDark);
			}
		}

		// Token: 0x06004C63 RID: 19555 RVA: 0x00131378 File Offset: 0x0012F578
		public virtual void DrawPopupPressedRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
		{
			g.FillPie(SystemBrushes.ControlLightLight, bounds, 0f, 359f);
			g.DrawArc(SystemPens.ControlLight, bounds.X + 1, bounds.Y + 1, bounds.Width - 2, bounds.Height - 2, 0, 359);
			g.DrawArc(SystemPens.ControlDark, bounds, 135f, 180f);
			g.DrawArc(SystemPens.ControlLightLight, bounds, 315f, 180f);
			if (isChecked)
			{
				this.DrawFlatRadioGlyphDot(g, bounds, SystemColors.ControlDarkDark);
			}
		}

		// Token: 0x06004C64 RID: 19556 RVA: 0x00131410 File Offset: 0x0012F610
		public virtual void DrawPopupDisabledRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
		{
			g.FillPie(SystemBrushes.Control, bounds.X + 1, bounds.Y + 1, bounds.Width - 2, bounds.Height - 2, 0, 359);
			g.DrawArc(SystemPens.ControlDark, bounds, 0f, 359f);
			if (isChecked)
			{
				this.DrawFlatRadioGlyphDot(g, bounds, SystemColors.ControlDarkDark);
			}
		}

		// Token: 0x06004C65 RID: 19557 RVA: 0x0013147C File Offset: 0x0012F67C
		protected void DrawFlatRadioGlyphDot(Graphics g, Rectangle bounds, Color dotColor)
		{
			int num = Math.Max(1, Math.Min(bounds.Width, bounds.Height) / 3);
			Pen pen = this.ResPool.GetPen(dotColor);
			Brush solidBrush = this.ResPool.GetSolidBrush(dotColor);
			if (bounds.Height > 13)
			{
				g.FillPie(solidBrush, bounds.X + num, bounds.Y + num, bounds.Width - num * 2, bounds.Height - num * 2, 0, 359);
			}
			else
			{
				int num2 = bounds.Width / 2 + bounds.X;
				int num3 = bounds.Height / 2 + bounds.Y;
				g.DrawLine(pen, num2 - 1, num3, num2 + 2, num3);
				g.DrawLine(pen, num2 - 1, num3 + 1, num2 + 2, num3 + 1);
				g.DrawLine(pen, num2, num3 - 1, num2, num3 + 2);
				g.DrawLine(pen, num2 + 1, num3 - 1, num2 + 1, num3 + 2);
			}
		}
	}
}
