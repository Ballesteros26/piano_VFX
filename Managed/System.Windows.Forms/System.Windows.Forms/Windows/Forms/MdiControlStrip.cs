using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000242 RID: 578
	internal class MdiControlStrip
	{
		// Token: 0x02000243 RID: 579
		public class SystemMenuItem : ToolStripMenuItem
		{
			// Token: 0x060025F8 RID: 9720 RVA: 0x0008FB30 File Offset: 0x0008DD30
			public SystemMenuItem(Form ownerForm)
			{
				this.form = ownerForm;
				base.AutoSize = false;
				base.Size = new Size(20, 20);
				base.Image = ownerForm.Icon.ToBitmap();
				base.MergeIndex = int.MinValue;
				base.DisplayStyle = ToolStripItemDisplayStyle.Image;
				base.DropDownItems.Add("&Restore", null, new EventHandler(this.RestoreItemHandler));
				ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)base.DropDownItems.Add("&Move");
				toolStripMenuItem.Enabled = false;
				ToolStripMenuItem toolStripMenuItem2 = (ToolStripMenuItem)base.DropDownItems.Add("&Size");
				toolStripMenuItem2.Enabled = false;
				base.DropDownItems.Add("Mi&nimize", null, new EventHandler(this.MinimizeItemHandler));
				ToolStripMenuItem toolStripMenuItem3 = (ToolStripMenuItem)base.DropDownItems.Add("Ma&ximize");
				toolStripMenuItem3.Enabled = false;
				base.DropDownItems.Add("-");
				ToolStripMenuItem toolStripMenuItem4 = (ToolStripMenuItem)base.DropDownItems.Add("&Close", null, new EventHandler(this.CloseItemHandler));
				toolStripMenuItem4.ShortcutKeys = (Keys)131187;
				base.DropDownItems.Add("-");
				ToolStripMenuItem toolStripMenuItem5 = (ToolStripMenuItem)base.DropDownItems.Add("Nex&t", null, new EventHandler(this.NextItemHandler));
				toolStripMenuItem5.ShortcutKeys = (Keys)131189;
			}

			// Token: 0x060025F9 RID: 9721 RVA: 0x0008FC98 File Offset: 0x0008DE98
			protected override void OnPaint(PaintEventArgs e)
			{
				if (base.Owner == null)
				{
					return;
				}
				Image image = this.Image;
				Rectangle rectangle;
				Rectangle rectangle2;
				base.CalculateTextAndImageRectangles(out rectangle, out rectangle2);
				if (rectangle2 != Rectangle.Empty)
				{
					base.Owner.Renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(e.Graphics, this, image, rectangle2));
				}
			}

			// Token: 0x17000962 RID: 2402
			// (get) Token: 0x060025FA RID: 9722 RVA: 0x0008FCF0 File Offset: 0x0008DEF0
			// (set) Token: 0x060025FB RID: 9723 RVA: 0x0008FCF8 File Offset: 0x0008DEF8
			public Form MdiForm
			{
				get
				{
					return this.form;
				}
				set
				{
					this.form = value;
				}
			}

			// Token: 0x060025FC RID: 9724 RVA: 0x0008FD04 File Offset: 0x0008DF04
			private void RestoreItemHandler(object sender, EventArgs e)
			{
				this.form.WindowState = FormWindowState.Normal;
			}

			// Token: 0x060025FD RID: 9725 RVA: 0x0008FD14 File Offset: 0x0008DF14
			private void MinimizeItemHandler(object sender, EventArgs e)
			{
				this.form.WindowState = FormWindowState.Minimized;
			}

			// Token: 0x060025FE RID: 9726 RVA: 0x0008FD24 File Offset: 0x0008DF24
			private void CloseItemHandler(object sender, EventArgs e)
			{
				this.form.Close();
			}

			// Token: 0x060025FF RID: 9727 RVA: 0x0008FD34 File Offset: 0x0008DF34
			private void NextItemHandler(object sender, EventArgs e)
			{
				this.form.MdiParent.MdiContainer.ActivateNextChild();
			}

			// Token: 0x04001313 RID: 4883
			private Form form;
		}

		// Token: 0x02000244 RID: 580
		public class ControlBoxMenuItem : ToolStripMenuItem
		{
			// Token: 0x06002600 RID: 9728 RVA: 0x0008FD4C File Offset: 0x0008DF4C
			public ControlBoxMenuItem(Form ownerForm, MdiControlStrip.ControlBoxType type)
			{
				this.form = ownerForm;
				this.type = type;
				base.AutoSize = false;
				base.Alignment = ToolStripItemAlignment.Right;
				base.Size = new Size(20, 20);
				base.MergeIndex = int.MaxValue;
				base.DisplayStyle = ToolStripItemDisplayStyle.None;
				switch (type)
				{
				case MdiControlStrip.ControlBoxType.Close:
					base.Click += new EventHandler(this.CloseItemHandler);
					break;
				case MdiControlStrip.ControlBoxType.Min:
					base.Click += new EventHandler(this.MinimizeItemHandler);
					break;
				case MdiControlStrip.ControlBoxType.Max:
					base.Click += new EventHandler(this.RestoreItemHandler);
					break;
				}
			}

			// Token: 0x06002601 RID: 9729 RVA: 0x0008FDFC File Offset: 0x0008DFFC
			protected override void OnPaint(PaintEventArgs e)
			{
				base.OnPaint(e);
				Graphics graphics = e.Graphics;
				switch (this.type)
				{
				case MdiControlStrip.ControlBoxType.Close:
					graphics.FillRectangle(Brushes.Black, 8, 8, 4, 4);
					graphics.FillRectangle(Brushes.Black, 6, 6, 2, 2);
					graphics.FillRectangle(Brushes.Black, 6, 12, 2, 2);
					graphics.FillRectangle(Brushes.Black, 12, 6, 2, 2);
					graphics.FillRectangle(Brushes.Black, 12, 12, 2, 2);
					graphics.DrawLine(Pens.Black, 8, 7, 8, 12);
					graphics.DrawLine(Pens.Black, 7, 8, 12, 8);
					graphics.DrawLine(Pens.Black, 11, 7, 11, 12);
					graphics.DrawLine(Pens.Black, 7, 11, 12, 11);
					break;
				case MdiControlStrip.ControlBoxType.Min:
					graphics.DrawLine(Pens.Black, 6, 12, 11, 12);
					graphics.DrawLine(Pens.Black, 6, 13, 11, 13);
					break;
				case MdiControlStrip.ControlBoxType.Max:
					graphics.DrawLines(Pens.Black, new Point[]
					{
						new Point(7, 8),
						new Point(7, 5),
						new Point(13, 5),
						new Point(13, 10),
						new Point(11, 10)
					});
					graphics.DrawLine(Pens.Black, 7, 6, 12, 6);
					graphics.DrawRectangle(Pens.Black, new Rectangle(5, 8, 6, 5));
					graphics.DrawLine(Pens.Black, 5, 9, 11, 9);
					break;
				}
			}

			// Token: 0x17000963 RID: 2403
			// (get) Token: 0x06002602 RID: 9730 RVA: 0x0008FFA8 File Offset: 0x0008E1A8
			// (set) Token: 0x06002603 RID: 9731 RVA: 0x0008FFB0 File Offset: 0x0008E1B0
			public Form MdiForm
			{
				get
				{
					return this.form;
				}
				set
				{
					this.form = value;
				}
			}

			// Token: 0x06002604 RID: 9732 RVA: 0x0008FFBC File Offset: 0x0008E1BC
			private void RestoreItemHandler(object sender, EventArgs e)
			{
				this.form.WindowState = FormWindowState.Normal;
			}

			// Token: 0x06002605 RID: 9733 RVA: 0x0008FFCC File Offset: 0x0008E1CC
			private void MinimizeItemHandler(object sender, EventArgs e)
			{
				this.form.WindowState = FormWindowState.Minimized;
			}

			// Token: 0x06002606 RID: 9734 RVA: 0x0008FFDC File Offset: 0x0008E1DC
			private void CloseItemHandler(object sender, EventArgs e)
			{
				this.form.Close();
			}

			// Token: 0x04001314 RID: 4884
			private Form form;

			// Token: 0x04001315 RID: 4885
			private MdiControlStrip.ControlBoxType type;
		}

		// Token: 0x02000245 RID: 581
		public enum ControlBoxType
		{
			// Token: 0x04001317 RID: 4887
			Close,
			// Token: 0x04001318 RID: 4888
			Min,
			// Token: 0x04001319 RID: 4889
			Max
		}
	}
}
