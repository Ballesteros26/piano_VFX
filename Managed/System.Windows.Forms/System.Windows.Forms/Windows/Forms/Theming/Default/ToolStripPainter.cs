using System;
using System.Drawing;

namespace System.Windows.Forms.Theming.Default
{
	// Token: 0x020004CC RID: 1228
	internal class ToolStripPainter
	{
		// Token: 0x17001348 RID: 4936
		// (get) Token: 0x06004C96 RID: 19606 RVA: 0x001325A4 File Offset: 0x001307A4
		protected SystemResPool ResPool
		{
			get
			{
				return ThemeEngine.Current.ResPool;
			}
		}

		// Token: 0x06004C97 RID: 19607 RVA: 0x001325B0 File Offset: 0x001307B0
		public virtual void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (!e.Item.Enabled)
			{
				return;
			}
			Rectangle rectangle;
			rectangle..ctor(0, 0, e.Item.Width, e.Item.Height);
			ToolStripButton toolStripButton = e.Item as ToolStripButton;
			if (e.Item.Pressed || (toolStripButton != null && toolStripButton.Checked))
			{
				ControlPaint.DrawBorder3D(e.Graphics, rectangle, Border3DStyle.SunkenOuter);
			}
			else if (e.Item.Selected)
			{
				ControlPaint.DrawBorder3D(e.Graphics, rectangle, Border3DStyle.RaisedInner);
			}
			else if (e.Item.BackColor != Control.DefaultBackColor && e.Item.BackColor != Color.Empty)
			{
				e.Graphics.FillRectangle(this.ResPool.GetSolidBrush(e.Item.BackColor), rectangle);
			}
		}

		// Token: 0x06004C98 RID: 19608 RVA: 0x001326A4 File Offset: 0x001308A4
		public virtual void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (!e.Item.Enabled)
			{
				return;
			}
			Rectangle rectangle;
			rectangle..ctor(0, 0, e.Item.Width, e.Item.Height);
			if (e.Item.Pressed)
			{
				ControlPaint.DrawBorder3D(e.Graphics, rectangle, Border3DStyle.SunkenOuter);
			}
			else if (e.Item.Selected)
			{
				ControlPaint.DrawBorder3D(e.Graphics, rectangle, Border3DStyle.RaisedInner);
			}
			else if (e.Item.BackColor != Control.DefaultBackColor && e.Item.BackColor != Color.Empty)
			{
				e.Graphics.FillRectangle(this.ResPool.GetSolidBrush(e.Item.BackColor), rectangle);
			}
		}

		// Token: 0x06004C99 RID: 19609 RVA: 0x0013277C File Offset: 0x0013097C
		public virtual void OnRenderGrip(ToolStripGripRenderEventArgs e)
		{
			if (e.GripStyle == ToolStripGripStyle.Hidden)
			{
				return;
			}
			if (e.GripDisplayStyle == ToolStripGripDisplayStyle.Vertical)
			{
				e.Graphics.DrawLine(Pens.White, 0, 2, 1, 2);
				e.Graphics.DrawLine(Pens.White, 0, 2, 0, e.GripBounds.Height - 3);
				e.Graphics.DrawLine(SystemPens.ControlDark, 2, 2, 2, e.GripBounds.Height - 3);
				e.Graphics.DrawLine(SystemPens.ControlDark, 2, e.GripBounds.Height - 3, 0, e.GripBounds.Height - 3);
			}
			else
			{
				e.Graphics.DrawLine(Pens.White, 2, 0, e.GripBounds.Width - 3, 0);
				e.Graphics.DrawLine(Pens.White, 2, 0, 2, 1);
				e.Graphics.DrawLine(SystemPens.ControlDark, e.GripBounds.Width - 3, 0, e.GripBounds.Width - 3, 2);
				e.Graphics.DrawLine(SystemPens.ControlDark, 2, 2, e.GripBounds.Width - 3, 2);
			}
		}

		// Token: 0x06004C9A RID: 19610 RVA: 0x001328C4 File Offset: 0x00130AC4
		public virtual void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)e.Item;
			Rectangle rectangle;
			rectangle..ctor(Point.Empty, toolStripMenuItem.Size);
			if (toolStripMenuItem.IsOnDropDown)
			{
				if (e.Item.Selected || e.Item.Pressed)
				{
					e.Graphics.FillRectangle(SystemBrushes.Highlight, rectangle);
				}
			}
			else if (e.Item.Pressed)
			{
				ControlPaint.DrawBorder3D(e.Graphics, rectangle, Border3DStyle.SunkenOuter);
			}
			else if (e.Item.Selected)
			{
				ControlPaint.DrawBorder3D(e.Graphics, rectangle, Border3DStyle.RaisedInner);
			}
			else if (e.Item.BackColor != Control.DefaultBackColor && e.Item.BackColor != Color.Empty)
			{
				e.Graphics.FillRectangle(this.ResPool.GetSolidBrush(e.Item.BackColor), rectangle);
			}
		}

		// Token: 0x06004C9B RID: 19611 RVA: 0x001329CC File Offset: 0x00130BCC
		public virtual void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
		{
			Rectangle rectangle;
			rectangle..ctor(Point.Empty, e.Item.Size);
			if (e.Item.Pressed)
			{
				ControlPaint.DrawBorder3D(e.Graphics, rectangle, Border3DStyle.SunkenOuter);
			}
			else if (e.Item.Selected)
			{
				ControlPaint.DrawBorder3D(e.Graphics, rectangle, Border3DStyle.RaisedInner);
			}
			else if (e.Item.BackColor != Control.DefaultBackColor && e.Item.BackColor != Color.Empty)
			{
				e.Graphics.FillRectangle(this.ResPool.GetSolidBrush(e.Item.BackColor), rectangle);
			}
			ToolStripRenderer.DrawDownArrow(e.Graphics, SystemPens.ControlText, e.Item.Width / 2 - 3, e.Item.Height / 2 - 1);
		}

		// Token: 0x06004C9C RID: 19612 RVA: 0x00132AB8 File Offset: 0x00130CB8
		public virtual void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
		{
			if (e.Vertical)
			{
				e.Graphics.DrawLine(Pens.White, 4, 3, 4, e.Item.Height - 1);
				e.Graphics.DrawLine(SystemPens.ControlDark, 3, 3, 3, e.Item.Height - 1);
			}
			else if (!e.Item.IsOnDropDown)
			{
				e.Graphics.DrawLine(Pens.White, 2, 4, e.Item.Right - 1, 4);
				e.Graphics.DrawLine(SystemPens.ControlDark, 2, 3, e.Item.Right - 1, 3);
			}
			else
			{
				e.Graphics.DrawLine(Pens.White, 3, 4, e.Item.Right - 4, 4);
				e.Graphics.DrawLine(SystemPens.ControlDark, 3, 3, e.Item.Right - 4, 3);
			}
		}

		// Token: 0x06004C9D RID: 19613 RVA: 0x00132BAC File Offset: 0x00130DAC
		public virtual void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripSplitButton toolStripSplitButton = (ToolStripSplitButton)e.Item;
			Rectangle rectangle;
			rectangle..ctor(Point.Empty, toolStripSplitButton.ButtonBounds.Size);
			Point point;
			point..ctor(toolStripSplitButton.Width - toolStripSplitButton.DropDownButtonBounds.Width, 0);
			Rectangle rectangle2;
			rectangle2..ctor(point, toolStripSplitButton.DropDownButtonBounds.Size);
			if (toolStripSplitButton.ButtonPressed)
			{
				ControlPaint.DrawBorder3D(e.Graphics, rectangle, Border3DStyle.SunkenOuter);
			}
			else if (toolStripSplitButton.ButtonSelected)
			{
				ControlPaint.DrawBorder3D(e.Graphics, rectangle, Border3DStyle.RaisedInner);
			}
			else if (e.Item.BackColor != Control.DefaultBackColor && e.Item.BackColor != Color.Empty)
			{
				e.Graphics.FillRectangle(this.ResPool.GetSolidBrush(e.Item.BackColor), rectangle);
			}
			if (toolStripSplitButton.DropDownButtonPressed || toolStripSplitButton.ButtonPressed)
			{
				ControlPaint.DrawBorder3D(e.Graphics, rectangle2, Border3DStyle.SunkenOuter);
			}
			else if (toolStripSplitButton.DropDownButtonSelected || toolStripSplitButton.ButtonSelected)
			{
				ControlPaint.DrawBorder3D(e.Graphics, rectangle2, Border3DStyle.RaisedInner);
			}
			else if (e.Item.BackColor != Control.DefaultBackColor && e.Item.BackColor != Color.Empty)
			{
				e.Graphics.FillRectangle(this.ResPool.GetSolidBrush(e.Item.BackColor), rectangle2);
			}
		}

		// Token: 0x06004C9E RID: 19614 RVA: 0x00132D4C File Offset: 0x00130F4C
		public virtual void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			if (e.ToolStrip.BackgroundImage == null)
			{
				e.Graphics.Clear(e.BackColor);
			}
			if (e.ToolStrip is StatusStrip)
			{
				e.Graphics.DrawLine(Pens.White, e.AffectedBounds.Left, e.AffectedBounds.Top, e.AffectedBounds.Right, e.AffectedBounds.Top);
			}
		}

		// Token: 0x06004C9F RID: 19615 RVA: 0x00132DD4 File Offset: 0x00130FD4
		public virtual void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			if (e.ToolStrip is StatusStrip)
			{
				return;
			}
			if (e.ToolStrip is ToolStripDropDown)
			{
				ControlPaint.DrawBorder3D(e.Graphics, e.AffectedBounds, Border3DStyle.Raised);
			}
			else
			{
				e.Graphics.DrawLine(SystemPens.ControlDark, new Point(e.ToolStrip.Left, e.ToolStrip.Height - 2), new Point(e.ToolStrip.Right, e.ToolStrip.Height - 2));
				e.Graphics.DrawLine(Pens.White, new Point(e.ToolStrip.Left, e.ToolStrip.Height - 1), new Point(e.ToolStrip.Right, e.ToolStrip.Height - 1));
			}
		}
	}
}
