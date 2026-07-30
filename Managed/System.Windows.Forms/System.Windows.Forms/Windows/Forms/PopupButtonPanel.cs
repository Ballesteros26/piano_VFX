using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000167 RID: 359
	internal class PopupButtonPanel : Control, IUpdateFolder
	{
		// Token: 0x06001832 RID: 6194 RVA: 0x00059A2C File Offset: 0x00057C2C
		public PopupButtonPanel()
		{
			base.SuspendLayout();
			this.BackColor = Color.FromArgb(128, 128, 128);
			base.Size = new Size(85, 336);
			base.InternalBorderStyle = BorderStyle.Fixed3D;
			this.recentlyusedButton = new PopupButtonPanel.PopupButton();
			this.desktopButton = new PopupButtonPanel.PopupButton();
			this.personalButton = new PopupButtonPanel.PopupButton();
			this.mycomputerButton = new PopupButtonPanel.PopupButton();
			this.networkButton = new PopupButtonPanel.PopupButton();
			this.recentlyusedButton.Size = new Size(81, 64);
			this.recentlyusedButton.Image = ThemeEngine.Current.Images(UIIcon.PlacesRecentDocuments, 32);
			this.recentlyusedButton.BackColor = this.BackColor;
			this.recentlyusedButton.ForeColor = Color.Black;
			this.recentlyusedButton.Location = new Point(2, 2);
			this.recentlyusedButton.Text = "Recently\nused";
			this.recentlyusedButton.Click += new EventHandler(this.OnClickButton);
			this.desktopButton.Image = ThemeEngine.Current.Images(UIIcon.PlacesDesktop, 32);
			this.desktopButton.BackColor = this.BackColor;
			this.desktopButton.ForeColor = Color.Black;
			this.desktopButton.Size = new Size(81, 64);
			this.desktopButton.Location = new Point(2, 66);
			this.desktopButton.Text = "Desktop";
			this.desktopButton.Click += new EventHandler(this.OnClickButton);
			this.personalButton.Image = ThemeEngine.Current.Images(UIIcon.PlacesPersonal, 32);
			this.personalButton.BackColor = this.BackColor;
			this.personalButton.ForeColor = Color.Black;
			this.personalButton.Size = new Size(81, 64);
			this.personalButton.Location = new Point(2, 130);
			this.personalButton.Text = "Personal";
			this.personalButton.Click += new EventHandler(this.OnClickButton);
			this.mycomputerButton.Image = ThemeEngine.Current.Images(UIIcon.PlacesMyComputer, 32);
			this.mycomputerButton.BackColor = this.BackColor;
			this.mycomputerButton.ForeColor = Color.Black;
			this.mycomputerButton.Size = new Size(81, 64);
			this.mycomputerButton.Location = new Point(2, 194);
			this.mycomputerButton.Text = "My Computer";
			this.mycomputerButton.Click += new EventHandler(this.OnClickButton);
			this.networkButton.Image = ThemeEngine.Current.Images(UIIcon.PlacesMyNetwork, 32);
			this.networkButton.BackColor = this.BackColor;
			this.networkButton.ForeColor = Color.Black;
			this.networkButton.Size = new Size(81, 64);
			this.networkButton.Location = new Point(2, 258);
			this.networkButton.Text = "My Network";
			this.networkButton.Click += new EventHandler(this.OnClickButton);
			base.Controls.Add(this.recentlyusedButton);
			base.Controls.Add(this.desktopButton);
			base.Controls.Add(this.personalButton);
			base.Controls.Add(this.mycomputerButton);
			base.Controls.Add(this.networkButton);
			base.ResumeLayout(false);
			base.KeyDown += this.Key_Down;
			base.SetStyle(ControlStyles.StandardClick, false);
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x00059DD4 File Offset: 0x00057FD4
		// Note: this type is marked as 'beforefieldinit'.
		static PopupButtonPanel()
		{
			PopupButtonPanel.UIAFocusedItemChangedEvent = new object();
			PopupButtonPanel.PDirectoryChangedEvent = new object();
		}

		// Token: 0x1400018F RID: 399
		// (add) Token: 0x06001834 RID: 6196 RVA: 0x00059DEC File Offset: 0x00057FEC
		// (remove) Token: 0x06001835 RID: 6197 RVA: 0x00059E00 File Offset: 0x00058000
		internal event EventHandler UIAFocusedItemChanged
		{
			add
			{
				base.Events.AddHandler(PopupButtonPanel.UIAFocusedItemChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(PopupButtonPanel.UIAFocusedItemChangedEvent, value);
			}
		}

		// Token: 0x14000190 RID: 400
		// (add) Token: 0x06001836 RID: 6198 RVA: 0x00059E14 File Offset: 0x00058014
		// (remove) Token: 0x06001837 RID: 6199 RVA: 0x00059E28 File Offset: 0x00058028
		public event EventHandler DirectoryChanged
		{
			add
			{
				base.Events.AddHandler(PopupButtonPanel.PDirectoryChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(PopupButtonPanel.PDirectoryChangedEvent, value);
			}
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x00059E3C File Offset: 0x0005803C
		private void OnClickButton(object sender, EventArgs e)
		{
			if (this.lastPopupButton != null && this.lastPopupButton != sender as PopupButtonPanel.PopupButton)
			{
				this.lastPopupButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Normal;
			}
			this.lastPopupButton = sender as PopupButtonPanel.PopupButton;
			if (sender == this.recentlyusedButton)
			{
				this.currentPath = MWFVFS.RecentlyUsedPrefix;
			}
			else if (sender == this.desktopButton)
			{
				this.currentPath = MWFVFS.DesktopPrefix;
			}
			else if (sender == this.personalButton)
			{
				this.currentPath = MWFVFS.PersonalPrefix;
			}
			else if (sender == this.mycomputerButton)
			{
				this.currentPath = MWFVFS.MyComputerPrefix;
			}
			else if (sender == this.networkButton)
			{
				this.currentPath = MWFVFS.MyNetworkPrefix;
			}
			EventHandler eventHandler = (EventHandler)base.Events[PopupButtonPanel.PDirectoryChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x00059F2C File Offset: 0x0005812C
		internal void OnUIAFocusedItemChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[PopupButtonPanel.UIAFocusedItemChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x00059F64 File Offset: 0x00058164
		internal PopupButtonPanel.PopupButton UIAFocusButton
		{
			get
			{
				return this.focusButton;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x0600183C RID: 6204 RVA: 0x0005A144 File Offset: 0x00058344
		// (set) Token: 0x0600183B RID: 6203 RVA: 0x00059F6C File Offset: 0x0005816C
		public string CurrentFolder
		{
			get
			{
				return this.currentPath;
			}
			set
			{
				if (value == MWFVFS.RecentlyUsedPrefix)
				{
					if (this.lastPopupButton != this.recentlyusedButton)
					{
						if (this.lastPopupButton != null)
						{
							this.lastPopupButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Normal;
						}
						this.recentlyusedButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Down;
						this.lastPopupButton = this.recentlyusedButton;
					}
				}
				else if (value == MWFVFS.DesktopPrefix)
				{
					if (this.lastPopupButton != this.desktopButton)
					{
						if (this.lastPopupButton != null)
						{
							this.lastPopupButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Normal;
						}
						this.desktopButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Down;
						this.lastPopupButton = this.desktopButton;
					}
				}
				else if (value == MWFVFS.PersonalPrefix)
				{
					if (this.lastPopupButton != this.personalButton)
					{
						if (this.lastPopupButton != null)
						{
							this.lastPopupButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Normal;
						}
						this.personalButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Down;
						this.lastPopupButton = this.personalButton;
					}
				}
				else if (value == MWFVFS.MyComputerPrefix)
				{
					if (this.lastPopupButton != this.mycomputerButton)
					{
						if (this.lastPopupButton != null)
						{
							this.lastPopupButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Normal;
						}
						this.mycomputerButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Down;
						this.lastPopupButton = this.mycomputerButton;
					}
				}
				else if (value == MWFVFS.MyNetworkPrefix)
				{
					if (this.lastPopupButton != this.networkButton)
					{
						if (this.lastPopupButton != null)
						{
							this.lastPopupButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Normal;
						}
						this.networkButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Down;
						this.lastPopupButton = this.networkButton;
					}
				}
				else if (this.lastPopupButton != null)
				{
					this.lastPopupButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Normal;
					this.lastPopupButton = null;
				}
			}
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x0005A14C File Offset: 0x0005834C
		protected override void OnGotFocus(EventArgs e)
		{
			if (this.lastPopupButton != this.recentlyusedButton)
			{
				this.recentlyusedButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Up;
				this.SetFocusButton(this.recentlyusedButton);
			}
			this.currentFocusIndex = 0;
			base.OnGotFocus(e);
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x0005A188 File Offset: 0x00058388
		protected override void OnLostFocus(EventArgs e)
		{
			if (this.focusButton != null && this.focusButton.ButtonState != PopupButtonPanel.PopupButton.PopupButtonState.Down)
			{
				this.focusButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Normal;
			}
			base.OnLostFocus(e);
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x0005A1BC File Offset: 0x000583BC
		protected override bool IsInputKey(Keys key)
		{
			switch (key)
			{
			case Keys.Left:
			case Keys.Up:
			case Keys.Right:
			case Keys.Down:
				break;
			default:
				if (key != Keys.Return)
				{
					return base.IsInputKey(key);
				}
				break;
			}
			return true;
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x0005A1FC File Offset: 0x000583FC
		private void Key_Down(object sender, KeyEventArgs e)
		{
			bool flag = false;
			if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
			{
				this.currentFocusIndex--;
				if (this.currentFocusIndex < 0)
				{
					this.currentFocusIndex = base.Controls.Count - 1;
				}
				flag = true;
			}
			else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
			{
				this.currentFocusIndex++;
				if (this.currentFocusIndex == base.Controls.Count)
				{
					this.currentFocusIndex = 0;
				}
				flag = true;
			}
			else if (e.KeyCode == Keys.Return)
			{
				this.focusButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Down;
				this.OnClickButton(this.focusButton, EventArgs.Empty);
			}
			if (flag)
			{
				PopupButtonPanel.PopupButton popupButton = base.Controls[this.currentFocusIndex] as PopupButtonPanel.PopupButton;
				if (this.focusButton != null && this.focusButton.ButtonState != PopupButtonPanel.PopupButton.PopupButtonState.Down)
				{
					this.focusButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Normal;
				}
				if (popupButton.ButtonState != PopupButtonPanel.PopupButton.PopupButtonState.Down)
				{
					popupButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Up;
				}
				this.SetFocusButton(popupButton);
			}
			e.Handled = true;
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0005A338 File Offset: 0x00058538
		internal void SetFocusButton(PopupButtonPanel.PopupButton button)
		{
			if (button == this.focusButton)
			{
				return;
			}
			this.focusButton = button;
			this.OnUIAFocusedItemChanged();
		}

		// Token: 0x04000D73 RID: 3443
		private PopupButtonPanel.PopupButton recentlyusedButton;

		// Token: 0x04000D74 RID: 3444
		private PopupButtonPanel.PopupButton desktopButton;

		// Token: 0x04000D75 RID: 3445
		private PopupButtonPanel.PopupButton personalButton;

		// Token: 0x04000D76 RID: 3446
		private PopupButtonPanel.PopupButton mycomputerButton;

		// Token: 0x04000D77 RID: 3447
		private PopupButtonPanel.PopupButton networkButton;

		// Token: 0x04000D78 RID: 3448
		private PopupButtonPanel.PopupButton lastPopupButton;

		// Token: 0x04000D79 RID: 3449
		private PopupButtonPanel.PopupButton focusButton;

		// Token: 0x04000D7A RID: 3450
		private string currentPath;

		// Token: 0x04000D7B RID: 3451
		private int currentFocusIndex;

		// Token: 0x04000D7D RID: 3453
		private static object PDirectoryChangedEvent;

		// Token: 0x02000168 RID: 360
		internal class PopupButton : Control
		{
			// Token: 0x06001842 RID: 6210 RVA: 0x0005A354 File Offset: 0x00058554
			public PopupButton()
			{
				this.text_format.Alignment = 1;
				this.text_format.LineAlignment = 0;
				base.SetStyle(ControlStyles.DoubleBuffer, true);
				base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
				base.SetStyle(ControlStyles.UserPaint, true);
				base.SetStyle(ControlStyles.Selectable, false);
			}

			// Token: 0x170005D6 RID: 1494
			// (get) Token: 0x06001844 RID: 6212 RVA: 0x0005A3D4 File Offset: 0x000585D4
			// (set) Token: 0x06001843 RID: 6211 RVA: 0x0005A3C4 File Offset: 0x000585C4
			public Image Image
			{
				get
				{
					return this.image;
				}
				set
				{
					this.image = value;
					base.Invalidate();
				}
			}

			// Token: 0x170005D7 RID: 1495
			// (get) Token: 0x06001846 RID: 6214 RVA: 0x0005A3EC File Offset: 0x000585EC
			// (set) Token: 0x06001845 RID: 6213 RVA: 0x0005A3DC File Offset: 0x000585DC
			public PopupButtonPanel.PopupButton.PopupButtonState ButtonState
			{
				get
				{
					return this.popupButtonState;
				}
				set
				{
					this.popupButtonState = value;
					base.Invalidate();
				}
			}

			// Token: 0x06001847 RID: 6215 RVA: 0x0005A3F4 File Offset: 0x000585F4
			internal void PerformClick()
			{
				this.OnClick(EventArgs.Empty);
			}

			// Token: 0x06001848 RID: 6216 RVA: 0x0005A404 File Offset: 0x00058604
			protected override void OnPaint(PaintEventArgs pe)
			{
				this.Draw(pe);
				base.OnPaint(pe);
			}

			// Token: 0x06001849 RID: 6217 RVA: 0x0005A414 File Offset: 0x00058614
			private void Draw(PaintEventArgs pe)
			{
				Graphics graphics = pe.Graphics;
				graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.BackColor), base.ClientRectangle);
				if (this.image != null)
				{
					int num = (base.ClientSize.Width - this.image.Width) / 2;
					int num2 = 4;
					graphics.DrawImage(this.image, num, num2);
				}
				if (this.Text != string.Empty)
				{
					if (this.text_rect == Rectangle.Empty)
					{
						this.text_rect = new Rectangle(0, base.Height - 30, base.Width, base.Height - 30);
					}
					graphics.DrawString(this.Text, this.Font, Brushes.White, this.text_rect, this.text_format);
				}
				PopupButtonPanel.PopupButton.PopupButtonState popupButtonState = this.popupButtonState;
				if (popupButtonState != PopupButtonPanel.PopupButton.PopupButtonState.Down)
				{
					if (popupButtonState == PopupButtonPanel.PopupButton.PopupButtonState.Up)
					{
						graphics.DrawLine(ThemeEngine.Current.ResPool.GetPen(Color.White), 0, 0, base.ClientSize.Width - 1, 0);
						graphics.DrawLine(ThemeEngine.Current.ResPool.GetPen(Color.White), 0, 0, 0, base.ClientSize.Height - 1);
						graphics.DrawLine(ThemeEngine.Current.ResPool.GetPen(Color.Black), base.ClientSize.Width - 1, 0, base.ClientSize.Width - 1, base.ClientSize.Height - 1);
						graphics.DrawLine(ThemeEngine.Current.ResPool.GetPen(Color.Black), 0, base.ClientSize.Height - 1, base.ClientSize.Width - 1, base.ClientSize.Height - 1);
					}
				}
				else
				{
					graphics.DrawLine(ThemeEngine.Current.ResPool.GetPen(Color.Black), 0, 0, base.ClientSize.Width - 1, 0);
					graphics.DrawLine(ThemeEngine.Current.ResPool.GetPen(Color.Black), 0, 0, 0, base.ClientSize.Height - 1);
					graphics.DrawLine(ThemeEngine.Current.ResPool.GetPen(Color.White), base.ClientSize.Width - 1, 0, base.ClientSize.Width - 1, base.ClientSize.Height - 1);
					graphics.DrawLine(ThemeEngine.Current.ResPool.GetPen(Color.White), 0, base.ClientSize.Height - 1, base.ClientSize.Width - 1, base.ClientSize.Height - 1);
				}
			}

			// Token: 0x0600184A RID: 6218 RVA: 0x0005A710 File Offset: 0x00058910
			protected override void OnMouseEnter(EventArgs e)
			{
				if (this.popupButtonState != PopupButtonPanel.PopupButton.PopupButtonState.Down)
				{
					this.popupButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Up;
				}
				PopupButtonPanel popupButtonPanel = base.Parent as PopupButtonPanel;
				if (popupButtonPanel.focusButton != null && popupButtonPanel.focusButton.ButtonState == PopupButtonPanel.PopupButton.PopupButtonState.Up)
				{
					popupButtonPanel.focusButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Normal;
					popupButtonPanel.SetFocusButton(null);
				}
				base.Invalidate();
				base.OnMouseEnter(e);
			}

			// Token: 0x0600184B RID: 6219 RVA: 0x0005A778 File Offset: 0x00058978
			protected override void OnMouseLeave(EventArgs e)
			{
				if (this.popupButtonState == PopupButtonPanel.PopupButton.PopupButtonState.Up)
				{
					this.popupButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Normal;
				}
				base.Invalidate();
				base.OnMouseLeave(e);
			}

			// Token: 0x0600184C RID: 6220 RVA: 0x0005A7A8 File Offset: 0x000589A8
			protected override void OnClick(EventArgs e)
			{
				this.popupButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Down;
				base.Invalidate();
				base.OnClick(e);
			}

			// Token: 0x04000D7E RID: 3454
			private Image image;

			// Token: 0x04000D7F RID: 3455
			private PopupButtonPanel.PopupButton.PopupButtonState popupButtonState;

			// Token: 0x04000D80 RID: 3456
			private StringFormat text_format = new StringFormat();

			// Token: 0x04000D81 RID: 3457
			private Rectangle text_rect = Rectangle.Empty;

			// Token: 0x02000169 RID: 361
			internal enum PopupButtonState
			{
				// Token: 0x04000D83 RID: 3459
				Normal,
				// Token: 0x04000D84 RID: 3460
				Down,
				// Token: 0x04000D85 RID: 3461
				Up
			}
		}
	}
}
