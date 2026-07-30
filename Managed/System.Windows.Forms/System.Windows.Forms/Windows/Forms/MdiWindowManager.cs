using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000247 RID: 583
	internal class MdiWindowManager : InternalWindowManager
	{
		// Token: 0x06002607 RID: 9735 RVA: 0x0008FFEC File Offset: 0x0008E1EC
		public MdiWindowManager(Form form, MdiClient mdi_container)
			: base(form)
		{
			this.mdi_container = mdi_container;
			if (form.WindowState == FormWindowState.Normal)
			{
				base.NormalBounds = form.Bounds;
			}
			this.form_closed_handler = new EventHandler(this.FormClosed);
			form.Closed += this.form_closed_handler;
			form.TextChanged += new EventHandler(this.FormTextChangedHandler);
			form.SizeChanged += new EventHandler(this.FormSizeChangedHandler);
			form.LocationChanged += new EventHandler(this.FormLocationChangedHandler);
			form.VisibleChanged += new EventHandler(this.FormVisibleChangedHandler);
			this.draw_maximized_buttons = new PaintEventHandler(this.DrawMaximizedButtons);
			this.CreateIconMenus();
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x000900A0 File Offset: 0x0008E2A0
		public void RaiseActivated()
		{
			if (this.last_activation_event == 1)
			{
				return;
			}
			this.last_activation_event = 1;
			this.form.OnActivatedInternal();
			this.form.SelectActiveControl();
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x000900D8 File Offset: 0x0008E2D8
		public void RaiseDeactivate()
		{
			if (this.last_activation_event != 1)
			{
				return;
			}
			this.last_activation_event = 2;
			this.form.OnDeactivateInternal();
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x0600260A RID: 9738 RVA: 0x000900FC File Offset: 0x0008E2FC
		public override int MenuHeight
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x0600260B RID: 9739 RVA: 0x00090100 File Offset: 0x0008E300
		// (set) Token: 0x0600260C RID: 9740 RVA: 0x00090108 File Offset: 0x0008E308
		internal bool IsVisiblePending
		{
			get
			{
				return this.is_visible_pending;
			}
			set
			{
				this.is_visible_pending = value;
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x0600260D RID: 9741 RVA: 0x00090114 File Offset: 0x0008E314
		private TitleButtons MaximizedTitleButtons
		{
			get
			{
				if (this.maximized_title_buttons == null)
				{
					this.maximized_title_buttons = new TitleButtons(base.Form);
					this.maximized_title_buttons.CloseButton.Visible = true;
					this.maximized_title_buttons.RestoreButton.Visible = true;
					this.maximized_title_buttons.MinimizeButton.Visible = true;
				}
				return this.maximized_title_buttons;
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x0600260E RID: 9742 RVA: 0x00090178 File Offset: 0x0008E378
		internal override Rectangle MaximizedBounds
		{
			get
			{
				Rectangle clientRectangle = this.mdi_container.ClientRectangle;
				int num = ThemeEngine.Current.ManagedWindowBorderWidth(this);
				int titleBarHeight = base.TitleBarHeight;
				Rectangle rectangle;
				rectangle..ctor(clientRectangle.Left - num, clientRectangle.Top - titleBarHeight - num, clientRectangle.Width + num * 2, clientRectangle.Height + titleBarHeight + num * 2);
				return rectangle;
			}
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x000901D8 File Offset: 0x0008E3D8
		private void FormVisibleChangedHandler(object sender, EventArgs e)
		{
			if (this.mdi_container == null)
			{
				return;
			}
			if (this.form.Visible)
			{
				this.mdi_container.ActivateChild(this.form);
			}
			else if (this.mdi_container.Controls.Count > 1)
			{
				this.mdi_container.ActivateActiveMdiChild();
			}
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x00090238 File Offset: 0x0008E438
		private void FormTextChangedHandler(object sender, EventArgs e)
		{
			this.mdi_container.SetParentText(false);
			if (this.form.MdiParent.MainMenuStrip != null)
			{
				this.form.MdiParent.MainMenuStrip.RefreshMdiItems();
			}
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x0009027C File Offset: 0x0008E47C
		private void FormLocationChangedHandler(object sender, EventArgs e)
		{
			if (this.form.window_state == FormWindowState.Minimized)
			{
				base.IconicBounds = this.form.Bounds;
			}
			this.form.MdiParent.MdiContainer.SizeScrollBars();
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x000902C0 File Offset: 0x0008E4C0
		private void FormSizeChangedHandler(object sender, EventArgs e)
		{
			if (this.form.window_state == FormWindowState.Maximized && this.form.Bounds != this.MaximizedBounds)
			{
				this.form.Bounds = this.MaximizedBounds;
			}
			this.form.MdiParent.MdiContainer.SizeScrollBars();
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06002613 RID: 9747 RVA: 0x00090320 File Offset: 0x0008E520
		public MainMenu MergedMenu
		{
			get
			{
				if (this.merged_menu == null)
				{
					this.merged_menu = this.CreateMergedMenu();
				}
				return this.merged_menu;
			}
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x00090340 File Offset: 0x0008E540
		private MainMenu CreateMergedMenu()
		{
			Form form = (Form)this.mdi_container.Parent;
			MainMenu mainMenu;
			if (form.Menu != null)
			{
				mainMenu = form.Menu.CloneMenu();
			}
			else
			{
				mainMenu = new MainMenu();
			}
			if (this.form.WindowState == FormWindowState.Maximized)
			{
			}
			mainMenu.MergeMenu(this.form.Menu);
			mainMenu.MenuChanged += new EventHandler(this.MenuChangedHandler);
			mainMenu.SetForm(form);
			return mainMenu;
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06002615 RID: 9749 RVA: 0x000903BC File Offset: 0x0008E5BC
		public MainMenu MaximizedMenu
		{
			get
			{
				if (this.maximized_menu == null)
				{
					this.maximized_menu = this.CreateMaximizedMenu();
				}
				return this.maximized_menu;
			}
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x000903DC File Offset: 0x0008E5DC
		private MainMenu CreateMaximizedMenu()
		{
			Form form = (Form)this.mdi_container.Parent;
			if (this.form.MainMenuStrip != null || form.MainMenuStrip != null)
			{
				return null;
			}
			MainMenu mainMenu = new MainMenu();
			if (form.Menu != null)
			{
				MainMenu mainMenu2 = form.Menu.CloneMenu();
				mainMenu.MergeMenu(mainMenu2);
			}
			if (this.form.Menu != null)
			{
				MainMenu mainMenu3 = this.form.Menu.CloneMenu();
				mainMenu.MergeMenu(mainMenu3);
			}
			if (mainMenu.MenuItems.Count == 0)
			{
				mainMenu.MenuItems.Add(new MenuItem());
			}
			mainMenu.MenuItems.Insert(0, this.icon_menu);
			mainMenu.SetForm(form);
			return mainMenu;
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x000904A0 File Offset: 0x0008E6A0
		private void CreateIconMenus()
		{
			this.icon_menu = new MenuItem();
			this.icon_popup_menu = new ContextMenu();
			this.icon_menu.OwnerDraw = true;
			this.icon_menu.MeasureItem += this.MeasureIconMenuItem;
			this.icon_menu.DrawItem += this.DrawIconMenuItem;
			this.icon_menu.Click += new EventHandler(this.ClickIconMenuItem);
			MenuItem menuItem = new MenuItem("&Restore", new EventHandler(this.RestoreItemHandler));
			MenuItem menuItem2 = new MenuItem("&Move", new EventHandler(this.MoveItemHandler));
			MenuItem menuItem3 = new MenuItem("&Size", new EventHandler(this.SizeItemHandler));
			MenuItem menuItem4 = new MenuItem("Mi&nimize", new EventHandler(this.MinimizeItemHandler));
			MenuItem menuItem5 = new MenuItem("Ma&ximize", new EventHandler(this.MaximizeItemHandler));
			MenuItem menuItem6 = new MenuItem("&Close", new EventHandler(this.CloseItemHandler));
			MenuItem menuItem7 = new MenuItem("Nex&t", new EventHandler(this.NextItemHandler));
			this.icon_menu.MenuItems.AddRange(new MenuItem[] { menuItem, menuItem2, menuItem3, menuItem4, menuItem5, menuItem6, menuItem7 });
			this.icon_popup_menu.MenuItems.AddRange(new MenuItem[] { menuItem, menuItem2, menuItem3, menuItem4, menuItem5, menuItem6, menuItem7 });
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x00090624 File Offset: 0x0008E824
		private void ClickIconMenuItem(object sender, EventArgs e)
		{
			if ((DateTime.Now - this.icon_clicked_time).TotalMilliseconds <= (double)SystemInformation.DoubleClickTime)
			{
				this.form.Close();
				return;
			}
			this.icon_clicked_time = DateTime.Now;
			Point point = Point.Empty;
			point = this.form.MdiParent.PointToScreen(point);
			point = this.form.PointToClient(point);
			this.ShowPopup(point);
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x00090698 File Offset: 0x0008E898
		internal void ShowPopup(Point pnt)
		{
			if (this.form.WindowState == FormWindowState.Maximized && this.form.MdiParent.MainMenuStrip != null && this.form.MdiParent.MainMenuStrip.Items.Count > 0)
			{
				ToolStripItem toolStripItem = this.form.MdiParent.MainMenuStrip.Items[0];
				if (toolStripItem is MdiControlStrip.SystemMenuItem)
				{
					(toolStripItem as MdiControlStrip.SystemMenuItem).ShowDropDown();
					return;
				}
			}
			this.icon_popup_menu.MenuItems[0].Enabled = this.form.window_state != FormWindowState.Normal;
			this.icon_popup_menu.MenuItems[1].Enabled = this.form.window_state != FormWindowState.Maximized;
			this.icon_popup_menu.MenuItems[2].Enabled = this.form.window_state != FormWindowState.Maximized;
			this.icon_popup_menu.MenuItems[3].Enabled = this.form.window_state != FormWindowState.Minimized;
			this.icon_popup_menu.MenuItems[4].Enabled = this.form.window_state != FormWindowState.Maximized;
			this.icon_popup_menu.MenuItems[5].Enabled = true;
			this.icon_popup_menu.MenuItems[6].Enabled = true;
			this.icon_popup_menu.Show(this.form, pnt);
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x00090824 File Offset: 0x0008EA24
		private void RestoreItemHandler(object sender, EventArgs e)
		{
			this.form.WindowState = FormWindowState.Normal;
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x00090834 File Offset: 0x0008EA34
		private void MoveItemHandler(object sender, EventArgs e)
		{
			int num = 0;
			int num2 = 0;
			this.PointToScreen(ref num, ref num2);
			Cursor.Position = new Point(num, num2);
			this.form.Cursor = Cursors.Cross;
			this.state = InternalWindowManager.State.Moving;
			this.form.Capture = true;
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x00090880 File Offset: 0x0008EA80
		private void SizeItemHandler(object sender, EventArgs e)
		{
			int num = 0;
			int num2 = 0;
			this.PointToScreen(ref num, ref num2);
			Cursor.Position = new Point(num, num2);
			this.form.Cursor = Cursors.Cross;
			this.state = InternalWindowManager.State.Sizing;
			this.form.Capture = true;
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x000908CC File Offset: 0x0008EACC
		private void MinimizeItemHandler(object sender, EventArgs e)
		{
			this.form.WindowState = FormWindowState.Minimized;
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x000908DC File Offset: 0x0008EADC
		private void MaximizeItemHandler(object sender, EventArgs e)
		{
			if (this.form.WindowState != FormWindowState.Maximized)
			{
				this.form.WindowState = FormWindowState.Maximized;
			}
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x000908FC File Offset: 0x0008EAFC
		private void CloseItemHandler(object sender, EventArgs e)
		{
			this.form.Close();
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x0009090C File Offset: 0x0008EB0C
		private void NextItemHandler(object sender, EventArgs e)
		{
			this.mdi_container.ActivateNextChild();
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x0009091C File Offset: 0x0008EB1C
		private void DrawIconMenuItem(object sender, DrawItemEventArgs de)
		{
			de.Graphics.DrawIcon(this.form.Icon, new Rectangle(de.Bounds.X + 2, de.Bounds.Y + 2, de.Bounds.Height - 4, de.Bounds.Height - 4));
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x00090984 File Offset: 0x0008EB84
		private void MeasureIconMenuItem(object sender, MeasureItemEventArgs me)
		{
			int menuHeight = SystemInformation.MenuHeight;
			me.ItemHeight = menuHeight;
			me.ItemWidth = menuHeight + 2;
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x000909A8 File Offset: 0x0008EBA8
		private void MenuChangedHandler(object sender, EventArgs e)
		{
			this.CreateMergedMenu();
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x000909B4 File Offset: 0x0008EBB4
		public override void PointToClient(ref int x, ref int y)
		{
			XplatUI.ScreenToClient(this.mdi_container.Handle, ref x, ref y);
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x000909C8 File Offset: 0x0008EBC8
		public override void PointToScreen(ref int x, ref int y)
		{
			XplatUI.ClientToScreen(this.mdi_container.Handle, ref x, ref y);
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x000909DC File Offset: 0x0008EBDC
		public override void UpdateWindowDecorations(FormWindowState window_state)
		{
			if (this.MaximizedMenu != null)
			{
				switch (window_state)
				{
				case FormWindowState.Normal:
				case FormWindowState.Minimized:
					this.MaximizedMenu.Paint -= this.draw_maximized_buttons;
					this.MaximizedTitleButtons.Visible = false;
					base.TitleButtons.Visible = true;
					break;
				case FormWindowState.Maximized:
					this.MaximizedMenu.Paint += this.draw_maximized_buttons;
					this.MaximizedTitleButtons.Visible = true;
					base.TitleButtons.Visible = false;
					break;
				}
			}
			base.UpdateWindowDecorations(window_state);
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x00090A70 File Offset: 0x0008EC70
		public override void SetWindowState(FormWindowState old_state, FormWindowState window_state)
		{
			this.mdi_container.SetWindowState(this.form, old_state, window_state, false);
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x00090A88 File Offset: 0x0008EC88
		private void FormClosed(object sender, EventArgs e)
		{
			this.mdi_container.ChildFormClosed(this.form);
			if (this.form.MdiParent.MainMenuStrip != null)
			{
				this.form.MdiParent.MainMenuStrip.RefreshMdiItems();
			}
			this.mdi_container.RemoveControlMenuItems(this);
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x00090ADC File Offset: 0x0008ECDC
		public override void DrawMaximizedButtons(object sender, PaintEventArgs pe)
		{
			Size size = ThemeEngine.Current.ManagedWindowGetMenuButtonSize(this);
			Point menuOrigin = XplatUI.GetMenuOrigin(this.mdi_container.ParentForm.Handle);
			int num = ThemeEngine.Current.ManagedWindowBorderWidth(this);
			TitleButtons maximizedTitleButtons = this.MaximizedTitleButtons;
			maximizedTitleButtons.Visible = true;
			base.TitleButtons.Visible = false;
			maximizedTitleButtons.CloseButton.Rectangle = new Rectangle(this.mdi_container.ParentForm.Size.Width - 1 - num - size.Width - 2, menuOrigin.Y + 2, size.Width, size.Height);
			maximizedTitleButtons.RestoreButton.Rectangle = new Rectangle(maximizedTitleButtons.CloseButton.Rectangle.Left - 2 - size.Width, menuOrigin.Y + 2, size.Width, size.Height);
			maximizedTitleButtons.MinimizeButton.Rectangle = new Rectangle(maximizedTitleButtons.RestoreButton.Rectangle.Left - size.Width, menuOrigin.Y + 2, size.Width, size.Height);
			base.DrawTitleButton(pe.Graphics, maximizedTitleButtons.MinimizeButton, pe.ClipRectangle);
			base.DrawTitleButton(pe.Graphics, maximizedTitleButtons.RestoreButton, pe.ClipRectangle);
			base.DrawTitleButton(pe.Graphics, maximizedTitleButtons.CloseButton, pe.ClipRectangle);
			TitleButton minimizeButton = maximizedTitleButtons.MinimizeButton;
			minimizeButton.Rectangle.Y = minimizeButton.Rectangle.Y - menuOrigin.Y;
			TitleButton restoreButton = maximizedTitleButtons.RestoreButton;
			restoreButton.Rectangle.Y = restoreButton.Rectangle.Y - menuOrigin.Y;
			TitleButton closeButton = maximizedTitleButtons.CloseButton;
			closeButton.Rectangle.Y = closeButton.Rectangle.Y - menuOrigin.Y;
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x00090CA4 File Offset: 0x0008EEA4
		public bool HandleMenuMouseDown(MainMenu menu, int x, int y)
		{
			Point point = MenuTracker.ScreenToMenu(menu, new Point(x, y));
			this.HandleTitleBarDown(point.X, point.Y);
			return base.TitleButtons.AnyPushedTitleButtons;
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x00090CE0 File Offset: 0x0008EEE0
		public void HandleMenuMouseUp(MainMenu menu, int x, int y)
		{
			Point point = MenuTracker.ScreenToMenu(menu, new Point(x, y));
			this.HandleTitleBarUp(point.X, point.Y);
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x00090D10 File Offset: 0x0008EF10
		public void HandleMenuMouseLeave(MainMenu menu, int x, int y)
		{
			Point point = MenuTracker.ScreenToMenu(menu, new Point(x, y));
			this.HandleTitleBarLeave(point.X, point.Y);
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x00090D40 File Offset: 0x0008EF40
		public void HandleMenuMouseMove(MainMenu menu, int x, int y)
		{
			Point point = MenuTracker.ScreenToMenu(menu, new Point(x, y));
			this.HandleTitleBarMouseMove(point.X, point.Y);
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x00090D70 File Offset: 0x0008EF70
		protected override void HandleTitleBarLeave(int x, int y)
		{
			base.HandleTitleBarLeave(x, y);
			if (this.maximized_title_buttons != null)
			{
				this.maximized_title_buttons.MouseLeave(x, y);
			}
			if (base.IsMaximized)
			{
				XplatUI.InvalidateNC(this.form.MdiParent.Handle);
			}
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x00090DC0 File Offset: 0x0008EFC0
		protected override void HandleTitleBarUp(int x, int y)
		{
			if (base.IconRectangleContains(x, y))
			{
				if (!this.icon_dont_show_popup)
				{
					if (base.IsMaximized)
					{
						this.ClickIconMenuItem(null, null);
					}
					else
					{
						this.ShowPopup(Point.Empty);
					}
				}
				else
				{
					this.icon_dont_show_popup = false;
				}
				return;
			}
			bool isMaximized = base.IsMaximized;
			base.HandleTitleBarUp(x, y);
			if (this.maximized_title_buttons != null && isMaximized)
			{
				this.maximized_title_buttons.MouseUp(x, y);
			}
			if (base.IsMaximized)
			{
				XplatUI.InvalidateNC(this.mdi_container.Parent.Handle);
			}
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x00090E64 File Offset: 0x0008F064
		protected override void HandleTitleBarDoubleClick(int x, int y)
		{
			if (base.IconRectangleContains(x, y))
			{
				this.form.Close();
			}
			else if (this.form.MaximizeBox)
			{
				this.form.WindowState = FormWindowState.Maximized;
			}
			base.HandleTitleBarDoubleClick(x, y);
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x00090EB4 File Offset: 0x0008F0B4
		protected override void HandleTitleBarDown(int x, int y)
		{
			if (base.IconRectangleContains(x, y))
			{
				if ((DateTime.Now - this.icon_clicked_time).TotalMilliseconds <= (double)SystemInformation.DoubleClickTime && this.icon_clicked.X == x && this.icon_clicked.Y == y)
				{
					this.form.Close();
				}
				else
				{
					this.icon_clicked_time = DateTime.Now;
					this.icon_clicked.X = x;
					this.icon_clicked.Y = y;
				}
				return;
			}
			base.HandleTitleBarDown(x, y);
			if (this.maximized_title_buttons != null)
			{
				this.maximized_title_buttons.MouseDown(x, y);
			}
			if (base.IsMaximized)
			{
				XplatUI.InvalidateNC(this.mdi_container.Parent.Handle);
			}
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x00090F88 File Offset: 0x0008F188
		protected override void HandleTitleBarMouseMove(int x, int y)
		{
			base.HandleTitleBarMouseMove(x, y);
			if (this.maximized_title_buttons != null && this.maximized_title_buttons.MouseMove(x, y))
			{
				XplatUI.InvalidateNC(this.form.MdiParent.Handle);
			}
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x00090FD0 File Offset: 0x0008F1D0
		protected override bool HandleLButtonDblClick(ref Message m)
		{
			int num = Control.LowOrder(m.LParam.ToInt32());
			int num2 = Control.HighOrder((long)m.LParam.ToInt32());
			this.NCClientToNC(ref num, ref num2);
			if (base.IconRectangleContains(num, num2))
			{
				this.icon_popup_menu.Wnd.Hide();
				this.form.Close();
				return true;
			}
			return base.HandleLButtonDblClick(ref m);
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x00091044 File Offset: 0x0008F244
		protected override bool HandleLButtonDown(ref Message m)
		{
			int num = Control.LowOrder(m.LParam.ToInt32());
			int num2 = Control.HighOrder((long)m.LParam.ToInt32());
			this.NCClientToNC(ref num, ref num2);
			if (base.IconRectangleContains(num, num2))
			{
				if ((DateTime.Now - this.icon_clicked_time).TotalMilliseconds <= (double)SystemInformation.DoubleClickTime)
				{
					if (this.icon_popup_menu != null && this.icon_popup_menu.Wnd != null)
					{
						this.icon_popup_menu.Wnd.Hide();
					}
					this.form.Close();
					return true;
				}
				if (this.form.Capture)
				{
					this.icon_dont_show_popup = true;
				}
			}
			return base.HandleLButtonDown(ref m);
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x0009110C File Offset: 0x0008F30C
		protected override bool ShouldRemoveWindowManager(FormBorderStyle style)
		{
			return false;
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x00091110 File Offset: 0x0008F310
		protected override void HandleWindowMove(Message m)
		{
			Point position = Cursor.Position;
			Point point = base.MouseMove(position);
			if (point.X == 0 && point.Y == 0)
			{
				return;
			}
			int num = this.virtual_position.X + point.X;
			int num2 = this.virtual_position.Y + point.Y;
			Rectangle clientRectangle = this.mdi_container.ClientRectangle;
			if (this.mdi_container.VerticalScrollbarVisible)
			{
				clientRectangle.Width -= SystemInformation.VerticalScrollBarWidth;
			}
			if (this.mdi_container.HorizontalScrollbarVisible)
			{
				clientRectangle.Height -= SystemInformation.HorizontalScrollBarHeight;
			}
			base.UpdateVP(num, num2, this.form.Width, this.form.Height);
			this.start = position;
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x000911E4 File Offset: 0x0008F3E4
		protected override bool HandleNCMouseMove(ref Message m)
		{
			XplatUI.RequestAdditionalWM_NCMessages(this.form.Handle, true, true);
			return base.HandleNCMouseMove(ref m);
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x00091200 File Offset: 0x0008F400
		protected override void DrawVirtualPosition(Rectangle virtual_position)
		{
			this.ClearVirtualPosition();
			if (this.form.Parent != null)
			{
				XplatUI.DrawReversibleRectangle(this.form.Parent.Handle, virtual_position, 2);
			}
			this.prev_virtual_position = virtual_position;
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x00091244 File Offset: 0x0008F444
		protected override void ClearVirtualPosition()
		{
			if (this.prev_virtual_position != Rectangle.Empty && this.form.Parent != null)
			{
				XplatUI.DrawReversibleRectangle(this.form.Parent.Handle, this.prev_virtual_position, 2);
			}
			this.prev_virtual_position = Rectangle.Empty;
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x000912A0 File Offset: 0x0008F4A0
		protected override void OnWindowFinishedMoving()
		{
			this.form.Refresh();
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x0600263B RID: 9787 RVA: 0x000912B0 File Offset: 0x0008F4B0
		public override bool IsActive
		{
			get
			{
				return this.mdi_container != null && this.mdi_container.ActiveMdiChild == this.form;
			}
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x000912E0 File Offset: 0x0008F4E0
		protected override void Activate()
		{
			if (this.mdi_container.ActiveMdiChild != this.form)
			{
				this.mdi_container.ActivateChild(this.form);
			}
			base.Activate();
		}

		// Token: 0x0400131F RID: 4895
		private MainMenu merged_menu;

		// Token: 0x04001320 RID: 4896
		private MainMenu maximized_menu;

		// Token: 0x04001321 RID: 4897
		private MenuItem icon_menu;

		// Token: 0x04001322 RID: 4898
		private ContextMenu icon_popup_menu;

		// Token: 0x04001323 RID: 4899
		internal bool was_minimized;

		// Token: 0x04001324 RID: 4900
		private PaintEventHandler draw_maximized_buttons;

		// Token: 0x04001325 RID: 4901
		internal EventHandler form_closed_handler;

		// Token: 0x04001326 RID: 4902
		private MdiClient mdi_container;

		// Token: 0x04001327 RID: 4903
		private Rectangle prev_virtual_position;

		// Token: 0x04001328 RID: 4904
		private Point icon_clicked;

		// Token: 0x04001329 RID: 4905
		private DateTime icon_clicked_time;

		// Token: 0x0400132A RID: 4906
		private bool icon_dont_show_popup;

		// Token: 0x0400132B RID: 4907
		private TitleButtons maximized_title_buttons;

		// Token: 0x0400132C RID: 4908
		private bool is_visible_pending;

		// Token: 0x0400132D RID: 4909
		private byte last_activation_event;
	}
}
