using System;
using System.Collections;
using System.Drawing;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x0200024B RID: 587
	internal class MenuTracker
	{
		// Token: 0x0600268C RID: 9868 RVA: 0x0009205C File Offset: 0x0009025C
		public MenuTracker(Menu top_menu)
		{
			this.CurrentMenu = top_menu;
			this.TopMenu = top_menu;
			foreach (object obj in this.TopMenu.MenuItems)
			{
				MenuItem menuItem = (MenuItem)obj;
				this.AddShortcuts(menuItem);
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x0600268D RID: 9869 RVA: 0x000920FC File Offset: 0x000902FC
		public bool Navigating
		{
			get
			{
				return this.keynav_state != MenuTracker.KeyNavState.Idle || this.active;
			}
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x00092114 File Offset: 0x00090314
		internal static Point ScreenToMenu(Menu menu, Point pnt)
		{
			int x = pnt.X;
			int y = pnt.Y;
			XplatUI.ScreenToMenu(menu.Wnd.window.Handle, ref x, ref y);
			return new Point(x, y);
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x00092154 File Offset: 0x00090354
		private void UpdateCursor()
		{
			Control realChildAtPoint = this.GrabControl.GetRealChildAtPoint(Cursor.Position);
			if (realChildAtPoint != null)
			{
				if (this.active)
				{
					XplatUI.SetCursor(realChildAtPoint.Handle, Cursors.Default.handle);
				}
				else
				{
					XplatUI.SetCursor(realChildAtPoint.Handle, realChildAtPoint.Cursor.handle);
				}
			}
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x000921B4 File Offset: 0x000903B4
		internal void Deactivate()
		{
			bool flag = this.keynav_state != MenuTracker.KeyNavState.Idle && this.TopMenu is MainMenu;
			this.active = false;
			this.popup_active = false;
			this.hotkey_active = false;
			if (this.GrabControl != null)
			{
				this.GrabControl.ActiveTracker = null;
			}
			this.keynav_state = MenuTracker.KeyNavState.Idle;
			if (this.TopMenu is ContextMenu)
			{
				PopUpWindow popUpWindow = this.TopMenu.Wnd as PopUpWindow;
				this.DeselectItem(this.TopMenu.SelectedItem);
				if (popUpWindow != null)
				{
					popUpWindow.HideWindow();
				}
			}
			else
			{
				this.DeselectItem(this.TopMenu.SelectedItem);
			}
			this.CurrentMenu = this.TopMenu;
			if (flag)
			{
				(this.TopMenu as MainMenu).Draw();
			}
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x00092288 File Offset: 0x00090488
		private MenuItem FindItemByCoords(Menu menu, Point pt)
		{
			if (menu is MainMenu)
			{
				pt = MenuTracker.ScreenToMenu(menu, pt);
			}
			else
			{
				pt = menu.Wnd.PointToClient(pt);
			}
			foreach (object obj in menu.MenuItems)
			{
				MenuItem menuItem = (MenuItem)obj;
				Rectangle bounds = menuItem.bounds;
				if (bounds.Contains(pt))
				{
					return menuItem;
				}
			}
			return null;
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x00092338 File Offset: 0x00090538
		private MenuItem GetItemAtXY(int x, int y)
		{
			Point point;
			point..ctor(x, y);
			MenuItem menuItem = null;
			if (this.TopMenu.SelectedItem != null)
			{
				menuItem = this.FindSubItemByCoord(this.TopMenu.SelectedItem, Control.MousePosition);
			}
			if (menuItem == null)
			{
				menuItem = this.FindItemByCoords(this.TopMenu, point);
			}
			return menuItem;
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x0009238C File Offset: 0x0009058C
		public bool OnMouseDown(MouseEventArgs args)
		{
			MenuItem itemAtXY = this.GetItemAtXY(args.X, args.Y);
			this.mouse_down = true;
			if (itemAtXY == null)
			{
				this.Deactivate();
				return false;
			}
			if ((args.Button & MouseButtons.Left) == MouseButtons.None)
			{
				return true;
			}
			if (!itemAtXY.Enabled)
			{
				return true;
			}
			this.popdown_menu = this.active && itemAtXY.VisibleItems;
			if (itemAtXY.IsPopup || itemAtXY.Parent is MainMenu)
			{
				this.active = true;
				itemAtXY.Parent.InvalidateItem(itemAtXY);
			}
			if (this.CurrentMenu == this.TopMenu && !this.popdown_menu)
			{
				this.SelectItem(itemAtXY.Parent, itemAtXY, itemAtXY.IsPopup);
			}
			this.GrabControl.ActiveTracker = this;
			return true;
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x00092468 File Offset: 0x00090668
		public void OnMotion(MouseEventArgs args)
		{
			if (args.Location == this.last_motion)
			{
				return;
			}
			this.last_motion = args.Location;
			MenuItem itemAtXY = this.GetItemAtXY(args.X, args.Y);
			this.UpdateCursor();
			if (this.CurrentMenu.SelectedItem == itemAtXY)
			{
				return;
			}
			this.GrabControl.ActiveTracker = ((!this.active && itemAtXY == null) ? null : this);
			if (itemAtXY == null)
			{
				MenuItem selectedItem = this.CurrentMenu.SelectedItem;
				if (this.active && selectedItem.VisibleItems && selectedItem.IsPopup && this.CurrentMenu is MainMenu)
				{
					return;
				}
				if (this.keynav_state == MenuTracker.KeyNavState.Navigating)
				{
					return;
				}
				if (selectedItem.Parent is MenuItem)
				{
					MenuItem menuItem = selectedItem.Parent as MenuItem;
					if (menuItem.IsPopup)
					{
						this.SelectItem(menuItem.Parent, menuItem, false);
						return;
					}
				}
				if (this.CurrentMenu != this.TopMenu)
				{
					this.CurrentMenu = this.CurrentMenu.parent_menu;
				}
				this.DeselectItem(selectedItem);
			}
			else
			{
				this.keynav_state = MenuTracker.KeyNavState.Idle;
				this.SelectItem(itemAtXY.Parent, itemAtXY, this.active && itemAtXY.IsPopup && this.popup_active && this.CurrentMenu.SelectedItem != itemAtXY);
			}
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x000925E4 File Offset: 0x000907E4
		public void OnMouseUp(MouseEventArgs args)
		{
			if (!this.mouse_down)
			{
				return;
			}
			this.mouse_down = false;
			if ((args.Button & MouseButtons.Left) == MouseButtons.None)
			{
				return;
			}
			MenuItem itemAtXY = this.GetItemAtXY(args.X, args.Y);
			if (itemAtXY == null)
			{
				this.Deactivate();
				return;
			}
			if (!itemAtXY.Enabled)
			{
				return;
			}
			if ((this.CurrentMenu == this.TopMenu && !(this.CurrentMenu is ContextMenu) && this.popdown_menu) || !itemAtXY.IsPopup)
			{
				this.Deactivate();
				this.UpdateCursor();
			}
			if (!itemAtXY.IsPopup)
			{
				this.DeselectItem(itemAtXY);
				if (this.TopMenu != null && this.TopMenu.Wnd != null)
				{
					Form form = this.TopMenu.Wnd.FindForm();
					if (form != null)
					{
						form.OnMenuComplete(EventArgs.Empty);
					}
				}
				itemAtXY.PerformClick();
			}
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x000926DC File Offset: 0x000908DC
		public static bool TrackPopupMenu(Menu menu, Point pnt)
		{
			if (menu.MenuItems.Count <= 0)
			{
				return true;
			}
			MenuTracker tracker = menu.tracker;
			tracker.active = true;
			tracker.popup_active = true;
			Control sourceControl = (tracker.TopMenu as ContextMenu).SourceControl;
			tracker.GrabControl = sourceControl.FindForm();
			if (tracker.GrabControl == null)
			{
				tracker.GrabControl = sourceControl.FindRootParent();
			}
			tracker.GrabControl.ActiveTracker = tracker;
			menu.Wnd = new PopUpWindow(tracker.GrabControl, menu);
			menu.Wnd.Location = menu.Wnd.PointToClient(pnt);
			((PopUpWindow)menu.Wnd).ShowWindow();
			bool flag = true;
			object obj = XplatUI.StartLoop(Thread.CurrentThread);
			while (menu.Wnd != null && menu.Wnd.Visible && flag)
			{
				MSG msg = default(MSG);
				flag = XplatUI.GetMessage(obj, ref msg, IntPtr.Zero, 0, 0);
				switch (msg.message)
				{
				case Msg.WM_KEYDOWN:
				case Msg.WM_KEYUP:
				case Msg.WM_CHAR:
				case Msg.WM_SYSKEYDOWN:
				case Msg.WM_SYSKEYUP:
				case Msg.WM_SYSCHAR:
				{
					Control control = Control.FromHandle(msg.hwnd);
					if (control != null)
					{
						Message message = Message.Create(msg.hwnd, (int)msg.message, msg.wParam, msg.lParam);
						control.PreProcessControlMessageInternal(ref message);
					}
					continue;
				}
				}
				XplatUI.TranslateMessage(ref msg);
				XplatUI.DispatchMessage(ref msg);
			}
			if (tracker.GrabControl.IsDisposed)
			{
				return true;
			}
			if (!flag)
			{
				XplatUI.PostQuitMessage(0);
			}
			if (menu.Wnd != null)
			{
				menu.Wnd.Dispose();
				menu.Wnd = null;
			}
			return true;
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x000928A4 File Offset: 0x00090AA4
		private void DeselectItem(MenuItem item)
		{
			if (item == null)
			{
				return;
			}
			item.Selected = false;
			if (item.IsPopup)
			{
				MenuTracker.HideSubPopups(item, this.TopMenu);
				foreach (object obj in item.MenuItems)
				{
					MenuItem menuItem = (MenuItem)obj;
					if (menuItem.Selected)
					{
						this.DeselectItem(menuItem);
					}
				}
			}
			Menu parent = item.Parent;
			parent.InvalidateItem(item);
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x00092954 File Offset: 0x00090B54
		private void SelectItem(Menu menu, MenuItem item, bool execute)
		{
			MenuItem selectedItem = this.CurrentMenu.SelectedItem;
			if (selectedItem != item.Parent)
			{
				this.DeselectItem(selectedItem);
				if (this.CurrentMenu != menu && selectedItem.Parent != item && selectedItem.Parent is MenuItem)
				{
					this.DeselectItem(selectedItem.Parent as MenuItem);
				}
			}
			if (this.CurrentMenu != menu)
			{
				this.CurrentMenu = menu;
			}
			item.Selected = true;
			menu.InvalidateItem(item);
			if ((this.CurrentMenu == this.TopMenu && execute) || (this.CurrentMenu != this.TopMenu && this.popup_active))
			{
				item.PerformSelect();
			}
			if (execute && (selectedItem == null || item != selectedItem.Parent))
			{
				this.ExecFocusedItem(menu, item);
			}
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x00092A34 File Offset: 0x00090C34
		private void ExecFocusedItem(Menu menu, MenuItem item)
		{
			if (item == null)
			{
				return;
			}
			if (!item.Enabled)
			{
				return;
			}
			if (item.IsPopup)
			{
				this.ShowSubPopup(menu, item);
			}
			else
			{
				this.Deactivate();
				item.PerformClick();
			}
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x00092A78 File Offset: 0x00090C78
		private void ShowSubPopup(Menu menu, MenuItem item)
		{
			if (!item.Enabled)
			{
				return;
			}
			if (!this.popdown_menu || !item.VisibleItems)
			{
				item.PerformPopup();
			}
			if (!item.VisibleItems)
			{
				return;
			}
			if (item.Wnd != null)
			{
				item.Wnd.Dispose();
			}
			this.popup_active = true;
			PopUpWindow popUpWindow = new PopUpWindow(this.GrabControl, item);
			Point point;
			if (menu is MainMenu)
			{
				point..ctor(item.X, item.Y + item.Height - 2 - menu.Height);
			}
			else
			{
				point..ctor(item.X + item.Width - 3, item.Y - 3);
			}
			point = menu.Wnd.PointToScreen(point);
			popUpWindow.Location = point;
			item.Wnd = popUpWindow;
			popUpWindow.ShowWindow();
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x00092B58 File Offset: 0x00090D58
		public static void HideSubPopups(Menu menu, Menu topmenu)
		{
			foreach (object obj in menu.MenuItems)
			{
				MenuItem menuItem = (MenuItem)obj;
				if (menuItem.IsPopup)
				{
					MenuTracker.HideSubPopups(menuItem, null);
				}
			}
			if (menu.Wnd == null)
			{
				return;
			}
			PopUpWindow popUpWindow = menu.Wnd as PopUpWindow;
			if (popUpWindow != null)
			{
				popUpWindow.Hide();
				popUpWindow.Dispose();
			}
			menu.Wnd = null;
			if (topmenu != null && topmenu is MainMenu)
			{
				((MainMenu)topmenu).OnCollapse(EventArgs.Empty);
			}
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x00092C24 File Offset: 0x00090E24
		private MenuItem FindSubItemByCoord(Menu menu, Point pnt)
		{
			foreach (object obj in menu.MenuItems)
			{
				MenuItem menuItem = (MenuItem)obj;
				if (menuItem.IsPopup && menuItem.Wnd != null && menuItem.Wnd.Visible && menuItem == menu.SelectedItem)
				{
					MenuItem menuItem2 = this.FindSubItemByCoord(menuItem, pnt);
					if (menuItem2 != null)
					{
						return menuItem2;
					}
				}
				if (menu.Wnd != null && menu.Wnd.Visible)
				{
					Rectangle bounds = menuItem.bounds;
					Point point = menu.Wnd.PointToScreen(new Point(menuItem.X, menuItem.Y));
					bounds.X = point.X;
					bounds.Y = point.Y;
					if (bounds.Contains(pnt))
					{
						return menuItem;
					}
				}
			}
			return null;
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x00092D54 File Offset: 0x00090F54
		private static MenuItem FindItemByKey(Menu menu, IntPtr key)
		{
			char c = char.ToUpper((char)(key.ToInt32() & 255));
			foreach (object obj in menu.MenuItems)
			{
				MenuItem menuItem = (MenuItem)obj;
				if (menuItem.Mnemonic == c)
				{
					return menuItem;
				}
			}
			string text = c.ToString();
			foreach (object obj2 in menu.MenuItems)
			{
				MenuItem menuItem2 = (MenuItem)obj2;
				if (menuItem2.Text.StartsWith(text))
				{
					return menuItem2;
				}
			}
			return null;
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x00092E70 File Offset: 0x00091070
		private static MenuItem GetNextItem(Menu menu, MenuTracker.ItemNavigation navigation)
		{
			int i = 0;
			bool flag = false;
			for (int j = 0; j < menu.MenuItems.Count; j++)
			{
				MenuItem menuItem = menu.MenuItems[j];
				if (!menuItem.Separator && menuItem.Visible)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return null;
			}
			switch (navigation)
			{
			case MenuTracker.ItemNavigation.First:
				for (i = 0; i < menu.MenuItems.Count; i++)
				{
					MenuItem menuItem = menu.MenuItems[i];
					if (!menuItem.Separator && menuItem.Visible)
					{
						break;
					}
				}
				break;
			case MenuTracker.ItemNavigation.Next:
				i = ((menu.SelectedItem != null) ? menu.SelectedItem.Index : (-1));
				for (i++; i < menu.MenuItems.Count; i++)
				{
					MenuItem menuItem = menu.MenuItems[i];
					if (!menuItem.Separator && menuItem.Visible)
					{
						break;
					}
				}
				if (i >= menu.MenuItems.Count)
				{
					for (i = 0; i < menu.MenuItems.Count; i++)
					{
						MenuItem menuItem = menu.MenuItems[i];
						if (!menuItem.Separator && menuItem.Visible)
						{
							break;
						}
					}
				}
				break;
			case MenuTracker.ItemNavigation.Previous:
				if (menu.SelectedItem != null)
				{
					i = menu.SelectedItem.Index;
				}
				for (i--; i >= 0; i--)
				{
					MenuItem menuItem = menu.MenuItems[i];
					if (!menuItem.Separator && menuItem.Visible)
					{
						break;
					}
				}
				if (i < 0)
				{
					for (i = menu.MenuItems.Count - 1; i >= 0; i--)
					{
						MenuItem menuItem = menu.MenuItems[i];
						if (!menuItem.Separator && menuItem.Visible)
						{
							break;
						}
					}
				}
				break;
			}
			return menu.MenuItems[i];
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x000930B0 File Offset: 0x000912B0
		private void ProcessMenuKey(Msg msg_type)
		{
			if (this.TopMenu.MenuItems.Count == 0)
			{
				return;
			}
			MainMenu mainMenu = this.TopMenu as MainMenu;
			if (msg_type != Msg.WM_SYSKEYDOWN)
			{
				if (msg_type == Msg.WM_SYSKEYUP)
				{
					switch (this.keynav_state)
					{
					case MenuTracker.KeyNavState.Idle:
					case MenuTracker.KeyNavState.Navigating:
						goto IL_0106;
					case MenuTracker.KeyNavState.Startup:
						this.keynav_state = MenuTracker.KeyNavState.NoPopups;
						this.SelectItem(this.TopMenu, this.TopMenu.MenuItems[0], false);
						goto IL_0106;
					}
					this.Deactivate();
					mainMenu.Draw();
					IL_0106:;
				}
			}
			else
			{
				MenuTracker.KeyNavState keyNavState = this.keynav_state;
				if (keyNavState != MenuTracker.KeyNavState.Idle)
				{
					if (keyNavState != MenuTracker.KeyNavState.Startup)
					{
						this.Deactivate();
						mainMenu.Draw();
					}
				}
				else
				{
					this.keynav_state = MenuTracker.KeyNavState.Startup;
					this.hotkey_active = true;
					this.GrabControl.ActiveTracker = this;
					this.CurrentMenu = this.TopMenu;
					mainMenu.Draw();
				}
			}
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x000931C8 File Offset: 0x000913C8
		private bool ProcessMnemonic(Message msg, Keys key_data)
		{
			this.keynav_state = MenuTracker.KeyNavState.Navigating;
			MenuItem menuItem = MenuTracker.FindItemByKey(this.CurrentMenu, msg.WParam);
			if (menuItem == null || this.GrabControl == null)
			{
				return false;
			}
			this.active = true;
			this.GrabControl.ActiveTracker = this;
			this.SelectItem(this.CurrentMenu, menuItem, true);
			if (menuItem.IsPopup)
			{
				this.CurrentMenu = menuItem;
				this.SelectItem(menuItem, menuItem.MenuItems[0], false);
			}
			return true;
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x0009324C File Offset: 0x0009144C
		public void AddShortcuts(MenuItem item)
		{
			foreach (object obj in item.MenuItems)
			{
				MenuItem menuItem = (MenuItem)obj;
				this.AddShortcuts(menuItem);
				if (menuItem.Shortcut != Shortcut.None)
				{
					this.shortcuts[(int)menuItem.Shortcut] = menuItem;
				}
			}
			if (item.Shortcut != Shortcut.None)
			{
				this.shortcuts[(int)item.Shortcut] = item;
			}
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x00093300 File Offset: 0x00091500
		public void RemoveShortcuts(MenuItem item)
		{
			foreach (object obj in item.MenuItems)
			{
				MenuItem menuItem = (MenuItem)obj;
				this.RemoveShortcuts(menuItem);
				if (menuItem.Shortcut != Shortcut.None)
				{
					this.shortcuts.Remove((int)menuItem.Shortcut);
				}
			}
			if (item.Shortcut != Shortcut.None)
			{
				this.shortcuts.Remove((int)item.Shortcut);
			}
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x000933B4 File Offset: 0x000915B4
		private bool ProcessShortcut(Keys keyData)
		{
			MenuItem menuItem = this.shortcuts[(int)keyData] as MenuItem;
			if (menuItem == null || !menuItem.Enabled)
			{
				return false;
			}
			if (this.active)
			{
				this.Deactivate();
			}
			menuItem.PerformClick();
			return true;
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x00093404 File Offset: 0x00091604
		public bool ProcessKeys(ref Message msg, Keys keyData)
		{
			if ((keyData & Keys.Alt) == Keys.Alt && this.active)
			{
				this.Deactivate();
				return false;
			}
			if ((keyData & Keys.Alt) == Keys.Alt && (keyData & Keys.F4) == Keys.F4)
			{
				if (this.GrabControl != null)
				{
					this.GrabControl.ActiveTracker = null;
				}
				return false;
			}
			if (msg.Msg != 261 && this.ProcessShortcut(keyData))
			{
				return true;
			}
			if ((keyData & Keys.KeyCode) == Keys.Menu && this.TopMenu is MainMenu)
			{
				this.ProcessMenuKey((Msg)msg.Msg);
				return true;
			}
			if ((keyData & Keys.Alt) == Keys.Alt)
			{
				return this.ProcessMnemonic(msg, keyData);
			}
			if (msg.Msg == 261)
			{
				return false;
			}
			if (!this.Navigating)
			{
				return false;
			}
			switch (keyData)
			{
			case Keys.Left:
				if (this.CurrentMenu is MainMenu)
				{
					MenuItem menuItem = MenuTracker.GetNextItem(this.CurrentMenu, MenuTracker.ItemNavigation.Previous);
					bool flag = menuItem.IsPopup && this.keynav_state != MenuTracker.KeyNavState.NoPopups;
					this.SelectItem(this.CurrentMenu, menuItem, flag);
					if (flag)
					{
						this.SelectItem(menuItem, menuItem.MenuItems[0], false);
						this.CurrentMenu = menuItem;
					}
				}
				else if (this.CurrentMenu.parent_menu is MainMenu)
				{
					MenuItem menuItem = MenuTracker.GetNextItem(this.CurrentMenu.parent_menu, MenuTracker.ItemNavigation.Previous);
					this.SelectItem(this.CurrentMenu.parent_menu, menuItem, menuItem.IsPopup);
					if (menuItem.IsPopup)
					{
						this.SelectItem(menuItem, menuItem.MenuItems[0], false);
						this.CurrentMenu = menuItem;
					}
				}
				else if (!(this.CurrentMenu is ContextMenu))
				{
					MenuTracker.HideSubPopups(this.CurrentMenu, this.TopMenu);
					if (this.CurrentMenu.parent_menu != null)
					{
						this.CurrentMenu = this.CurrentMenu.parent_menu;
					}
				}
				break;
			case Keys.Up:
			{
				if (this.CurrentMenu is MainMenu)
				{
					return true;
				}
				if (this.CurrentMenu.MenuItems.Count == 1 && this.CurrentMenu.parent_menu == this.TopMenu)
				{
					this.DeselectItem(this.CurrentMenu.SelectedItem);
					this.CurrentMenu = this.TopMenu;
					return true;
				}
				MenuItem menuItem = MenuTracker.GetNextItem(this.CurrentMenu, MenuTracker.ItemNavigation.Previous);
				if (menuItem != null)
				{
					this.SelectItem(this.CurrentMenu, menuItem, false);
				}
				break;
			}
			case Keys.Right:
				if (this.CurrentMenu is MainMenu)
				{
					MenuItem menuItem = MenuTracker.GetNextItem(this.CurrentMenu, MenuTracker.ItemNavigation.Next);
					bool flag2 = menuItem.IsPopup && this.keynav_state != MenuTracker.KeyNavState.NoPopups;
					this.SelectItem(this.CurrentMenu, menuItem, flag2);
					if (flag2)
					{
						this.SelectItem(menuItem, menuItem.MenuItems[0], false);
						this.CurrentMenu = menuItem;
					}
				}
				else if (this.CurrentMenu.SelectedItem != null && this.CurrentMenu.SelectedItem.IsPopup)
				{
					MenuItem menuItem = this.CurrentMenu.SelectedItem;
					this.ShowSubPopup(this.CurrentMenu, menuItem);
					this.SelectItem(menuItem, menuItem.MenuItems[0], false);
					this.CurrentMenu = menuItem;
				}
				else
				{
					Menu menu = this.CurrentMenu.parent_menu;
					while (menu != null && !(menu is MainMenu))
					{
						menu = menu.parent_menu;
					}
					if (menu is MainMenu)
					{
						MenuItem menuItem = MenuTracker.GetNextItem(menu, MenuTracker.ItemNavigation.Next);
						this.SelectItem(menu, menuItem, menuItem.IsPopup);
						if (menuItem.IsPopup)
						{
							this.SelectItem(menuItem, menuItem.MenuItems[0], false);
							this.CurrentMenu = menuItem;
						}
					}
				}
				break;
			case Keys.Down:
			{
				MenuItem menuItem;
				if (this.CurrentMenu is MainMenu)
				{
					if (this.CurrentMenu.SelectedItem != null && this.CurrentMenu.SelectedItem.IsPopup)
					{
						this.keynav_state = MenuTracker.KeyNavState.Navigating;
						menuItem = this.CurrentMenu.SelectedItem;
						this.ShowSubPopup(this.CurrentMenu, menuItem);
						this.SelectItem(menuItem, menuItem.MenuItems[0], false);
						this.CurrentMenu = menuItem;
						this.active = true;
						this.GrabControl.ActiveTracker = this;
					}
					return true;
				}
				menuItem = MenuTracker.GetNextItem(this.CurrentMenu, MenuTracker.ItemNavigation.Next);
				if (menuItem != null)
				{
					this.SelectItem(this.CurrentMenu, menuItem, false);
				}
				break;
			}
			default:
				if (keyData != Keys.Return)
				{
					if (keyData != Keys.Escape)
					{
						this.ProcessMnemonic(msg, keyData);
					}
					else
					{
						this.Deactivate();
					}
				}
				else if (this.CurrentMenu.SelectedItem != null && this.CurrentMenu.SelectedItem.IsPopup)
				{
					this.keynav_state = MenuTracker.KeyNavState.Navigating;
					MenuItem menuItem = this.CurrentMenu.SelectedItem;
					this.ShowSubPopup(this.CurrentMenu, menuItem);
					this.SelectItem(menuItem, menuItem.MenuItems[0], false);
					this.CurrentMenu = menuItem;
					this.active = true;
					this.GrabControl.ActiveTracker = this;
				}
				else
				{
					this.ExecFocusedItem(this.CurrentMenu, this.CurrentMenu.SelectedItem);
				}
				break;
			}
			return this.active;
		}

		// Token: 0x0400133F RID: 4927
		internal bool active;

		// Token: 0x04001340 RID: 4928
		internal bool popup_active;

		// Token: 0x04001341 RID: 4929
		internal bool popdown_menu;

		// Token: 0x04001342 RID: 4930
		internal bool hotkey_active;

		// Token: 0x04001343 RID: 4931
		private bool mouse_down;

		// Token: 0x04001344 RID: 4932
		public Menu CurrentMenu;

		// Token: 0x04001345 RID: 4933
		public Menu TopMenu;

		// Token: 0x04001346 RID: 4934
		public Control GrabControl;

		// Token: 0x04001347 RID: 4935
		private Point last_motion = Point.Empty;

		// Token: 0x04001348 RID: 4936
		private MenuTracker.KeyNavState keynav_state;

		// Token: 0x04001349 RID: 4937
		private Hashtable shortcuts = new Hashtable();

		// Token: 0x0200024C RID: 588
		private enum KeyNavState
		{
			// Token: 0x0400134B RID: 4939
			Idle,
			// Token: 0x0400134C RID: 4940
			Startup,
			// Token: 0x0400134D RID: 4941
			NoPopups,
			// Token: 0x0400134E RID: 4942
			Navigating
		}

		// Token: 0x0200024D RID: 589
		private enum ItemNavigation
		{
			// Token: 0x04001350 RID: 4944
			First,
			// Token: 0x04001351 RID: 4945
			Last,
			// Token: 0x04001352 RID: 4946
			Next,
			// Token: 0x04001353 RID: 4947
			Previous
		}
	}
}
