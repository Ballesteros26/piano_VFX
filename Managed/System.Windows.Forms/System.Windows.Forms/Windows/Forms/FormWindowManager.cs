using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x0200019D RID: 413
	internal class FormWindowManager : InternalWindowManager
	{
		// Token: 0x06001B07 RID: 6919 RVA: 0x00069668 File Offset: 0x00067868
		public FormWindowManager(Form form)
			: base(form)
		{
			form.MouseCaptureChanged += new EventHandler(this.HandleCaptureChanged);
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x00069684 File Offset: 0x00067884
		private void HandleCaptureChanged(object sender, EventArgs e)
		{
			if (this.pending_activation && !this.form.Capture)
			{
				this.form.BringToFront();
				this.pending_activation = false;
			}
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x000696B4 File Offset: 0x000678B4
		public override void PointToClient(ref int x, ref int y)
		{
			XplatUI.ScreenToClient(base.Form.Parent.Handle, ref x, ref y);
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x000696D8 File Offset: 0x000678D8
		protected override bool HandleNCLButtonDown(ref Message m)
		{
			this.pending_activation = true;
			return base.HandleNCLButtonDown(ref m);
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x000696E8 File Offset: 0x000678E8
		protected override void HandleTitleBarDoubleClick(int x, int y)
		{
			if (base.IconRectangleContains(x, y))
			{
				this.form.Close();
			}
			else if (this.form.WindowState == FormWindowState.Maximized)
			{
				this.form.WindowState = FormWindowState.Normal;
			}
			else
			{
				this.form.WindowState = FormWindowState.Maximized;
			}
			base.HandleTitleBarDoubleClick(x, y);
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x00069748 File Offset: 0x00067948
		internal override Rectangle MaximizedBounds
		{
			get
			{
				Rectangle maximizedBounds = base.MaximizedBounds;
				int num = ThemeEngine.Current.ManagedWindowBorderWidth(this);
				maximizedBounds.Inflate(num, num);
				return maximizedBounds;
			}
		}

		// Token: 0x04000EF9 RID: 3833
		private bool pending_activation;
	}
}
