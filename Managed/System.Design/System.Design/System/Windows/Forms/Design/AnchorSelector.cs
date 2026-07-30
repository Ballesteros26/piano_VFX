using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000003 RID: 3
	internal class AnchorSelector : UserControl
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020A8 File Offset: 0x000002A8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020C7 File Offset: 0x000002C7
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.Name = "AnchorSelector";
			base.Size = new Size(150, 120);
			base.ResumeLayout(false);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020F3 File Offset: 0x000002F3
		public AnchorStyles AnchorStyles
		{
			get
			{
				return this.styles;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020FB File Offset: 0x000002FB
		public AnchorSelector(IWindowsFormsEditorService editor_service, AnchorStyles startup)
		{
			this.styles = startup;
			this.InitializeComponent();
			this.BackColor = Color.White;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000211C File Offset: 0x0000031C
		private void PaintState(Graphics g, int x1, int y1, int x2, int y2, AnchorStyles v)
		{
			if ((this.styles & v) != null)
			{
				g.DrawLine(SystemPens.MenuText, x1, y1, x2, y2);
				return;
			}
			int num = ((x1 == x2) ? 10 : 0);
			int num2 = ((y1 == y2) ? 10 : 0);
			g.DrawBezier(SystemPens.MenuText, new Point(x1, y1), new Point((x1 + x2) / 2 + num, (y1 + y2) / 2 - num2), new Point((x1 + x2) / 2 - num, (y1 + y2) / 2 + num2), new Point(x2, y2));
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021A4 File Offset: 0x000003A4
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			int num = base.Width / 3;
			int num2 = base.Height / 3;
			int num3 = base.Width / 2;
			int num4 = base.Height / 2;
			graphics.FillRectangle(Brushes.Black, new Rectangle(num, num2, num, num2));
			this.PaintState(graphics, 0, num4, num, num4, 4);
			this.PaintState(graphics, num * 2, num4, base.Width, num4, 8);
			this.PaintState(graphics, num3, 0, num3, num2, 1);
			this.PaintState(graphics, num3, num2 * 2, num3, base.Height, 2);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002234 File Offset: 0x00000434
		protected override void OnClick(EventArgs ee)
		{
			Point point = base.PointToClient(Control.MousePosition);
			int num = base.Width / 3;
			int num2 = base.Height / 3;
			if (point.X <= num && point.Y > num2 && point.Y < num2 * 2)
			{
				this.styles ^= 4;
			}
			else if (point.Y < num2 && point.X > num && point.X < num * 2)
			{
				this.styles ^= 1;
			}
			else if (point.X > num * 2 && point.Y > num2 && point.Y < num2 * 2)
			{
				this.styles ^= 8;
			}
			else if (point.Y > num2 * 2 && point.X > num && point.X < num * 2)
			{
				this.styles ^= 2;
			}
			else
			{
				base.OnClick(ee);
			}
			base.Invalidate();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002333 File Offset: 0x00000533
		protected override void OnDoubleClick(EventArgs ee)
		{
			this.OnClick(ee);
		}

		// Token: 0x04000001 RID: 1
		private IContainer components;

		// Token: 0x04000002 RID: 2
		private AnchorStyles styles;
	}
}
