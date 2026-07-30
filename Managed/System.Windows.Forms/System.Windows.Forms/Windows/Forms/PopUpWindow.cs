using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x0200024E RID: 590
	internal class PopUpWindow : Control
	{
		// Token: 0x060026A5 RID: 9893 RVA: 0x00093968 File Offset: 0x00091B68
		public PopUpWindow(Control form, Menu menu)
		{
			this.menu = menu;
			this.form = form;
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.Opaque | ControlStyles.ResizeRedraw, true);
			this.is_visible = false;
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x060026A6 RID: 9894 RVA: 0x000939A8 File Offset: 0x00091BA8
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Caption = "Menu PopUp";
				createParams.Style = int.MinValue;
				createParams.ExStyle |= 136;
				return createParams;
			}
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x000939E8 File Offset: 0x00091BE8
		public void ShowWindow()
		{
			XplatUI.SetCursor(this.form.Handle, Cursors.Default.handle);
			this.RefreshItems();
			base.Show();
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x00093A1C File Offset: 0x00091C1C
		internal override void OnPaintInternal(PaintEventArgs args)
		{
			ThemeEngine.Current.DrawPopupMenu(args.Graphics, this.menu, args.ClipRectangle, base.ClientRectangle);
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x00093A4C File Offset: 0x00091C4C
		public void HideWindow()
		{
			XplatUI.SetCursor(this.form.Handle, this.form.Cursor.handle);
			MenuTracker.HideSubPopups(this.menu, null);
			base.Hide();
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x00093A8C File Offset: 0x00091C8C
		protected override void CreateHandle()
		{
			base.CreateHandle();
			this.RefreshItems();
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x00093A9C File Offset: 0x00091C9C
		internal void RefreshItems()
		{
			Point point;
			point..ctor(base.Location.X, base.Location.Y);
			ThemeEngine.Current.CalcPopupMenuSize(base.DeviceContext, this.menu);
			if (point.X + this.menu.Rect.Width > SystemInformation.VirtualScreen.Width)
			{
				if (point.X - this.menu.Rect.Width > 0 && !(this.menu.parent_menu is MainMenu))
				{
					point.X -= this.menu.Rect.Width;
				}
				else
				{
					point.X = SystemInformation.VirtualScreen.Width - this.menu.Rect.Width;
				}
				if (point.X < 0)
				{
					point.X = 0;
				}
			}
			if (point.Y + this.menu.Rect.Height > SystemInformation.VirtualScreen.Height)
			{
				if (point.Y - this.menu.Rect.Height > 0)
				{
					point.Y -= this.menu.Rect.Height;
				}
				else
				{
					point.Y = SystemInformation.VirtualScreen.Height - this.menu.Rect.Height;
				}
				if (point.Y < 0)
				{
					point.Y = 0;
				}
			}
			base.Location = point;
			base.Width = this.menu.Rect.Width;
			base.Height = this.menu.Rect.Height;
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x060026AC RID: 9900 RVA: 0x00093CA4 File Offset: 0x00091EA4
		internal override bool ActivateOnShow
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001354 RID: 4948
		private Menu menu;

		// Token: 0x04001355 RID: 4949
		private Control form;
	}
}
