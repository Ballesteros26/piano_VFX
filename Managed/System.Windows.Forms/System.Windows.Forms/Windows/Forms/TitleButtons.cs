using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x020001EB RID: 491
	internal class TitleButtons : IEnumerable
	{
		// Token: 0x06001EE7 RID: 7911 RVA: 0x00074604 File Offset: 0x00072804
		public TitleButtons(Form frm)
		{
			this.form = frm;
			this.Visible = true;
			this.MinimizeButton = new TitleButton(CaptionButton.Minimize, new EventHandler(this.ClickHandler));
			this.MaximizeButton = new TitleButton(CaptionButton.Maximize, new EventHandler(this.ClickHandler));
			this.RestoreButton = new TitleButton(CaptionButton.Restore, new EventHandler(this.ClickHandler));
			this.CloseButton = new TitleButton(CaptionButton.Close, new EventHandler(this.ClickHandler));
			this.HelpButton = new TitleButton(CaptionButton.Help, new EventHandler(this.ClickHandler));
			this.AllButtons = new TitleButton[] { this.MinimizeButton, this.MaximizeButton, this.RestoreButton, this.CloseButton, this.HelpButton };
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x000746D8 File Offset: 0x000728D8
		private void ClickHandler(object sender, EventArgs e)
		{
			if (!this.Visible)
			{
				return;
			}
			TitleButton titleButton = (TitleButton)sender;
			switch (titleButton.Caption)
			{
			case CaptionButton.Close:
				this.form.Close();
				break;
			case CaptionButton.Minimize:
				this.form.WindowState = FormWindowState.Minimized;
				break;
			case CaptionButton.Maximize:
				this.form.WindowState = FormWindowState.Maximized;
				break;
			case CaptionButton.Restore:
				this.form.WindowState = FormWindowState.Normal;
				break;
			case CaptionButton.Help:
				Console.WriteLine("Help not implemented.");
				break;
			}
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x00074770 File Offset: 0x00072970
		public TitleButton FindButton(int x, int y)
		{
			if (!this.Visible)
			{
				return null;
			}
			foreach (TitleButton titleButton in this.AllButtons)
			{
				if (titleButton.Visible && titleButton.Rectangle.Contains(x, y))
				{
					return titleButton;
				}
			}
			return null;
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x000747CC File Offset: 0x000729CC
		public bool AnyPushedTitleButtons
		{
			get
			{
				if (!this.Visible)
				{
					return false;
				}
				foreach (TitleButton titleButton in this.AllButtons)
				{
					if (titleButton.Visible && titleButton.State == ButtonState.Pushed)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x00074824 File Offset: 0x00072A24
		public IEnumerator GetEnumerator()
		{
			return this.AllButtons.GetEnumerator();
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x00074834 File Offset: 0x00072A34
		public void ToolTipStart(TitleButton button)
		{
			this.tooltip_hovered_button = button;
			if (this.tooltip_hovered_button == this.tooltip_hidden_button)
			{
				return;
			}
			this.tooltip_hidden_button = null;
			if (this.tooltip != null && this.tooltip.Visible)
			{
				this.ToolTipShow(true);
			}
			if (this.tooltip_timer == null)
			{
				this.tooltip_timer = new Timer();
				this.tooltip_timer.Tick += new EventHandler(this.ToolTipTimerTick);
			}
			this.tooltip_timer.Interval = 1000;
			this.tooltip_timer.Start();
			this.tooltip_hovered_button = button;
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x000748D4 File Offset: 0x00072AD4
		public void ToolTipTimerTick(object sender, EventArgs e)
		{
			if (this.tooltip_timer.Interval == 3000)
			{
				this.tooltip_hidden_button = this.tooltip_hovered_button;
				this.ToolTipHide(false);
			}
			else
			{
				this.ToolTipShow(false);
			}
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x00074918 File Offset: 0x00072B18
		public void ToolTipShow(bool only_refresh)
		{
			if (!this.form.Visible)
			{
				return;
			}
			string text = Locale.GetText(this.tooltip_hovered_button.Caption.ToString());
			this.tooltip_timer.Interval = 3000;
			this.tooltip_timer.Enabled = true;
			if (only_refresh && (this.tooltip == null || !this.tooltip.Visible))
			{
				return;
			}
			if (this.tooltip == null)
			{
				this.tooltip = new ToolTip.ToolTipWindow();
			}
			else
			{
				if (this.tooltip.Text == text && this.tooltip.Visible)
				{
					return;
				}
				if (this.tooltip.Visible)
				{
					this.tooltip.Visible = false;
				}
			}
			if (this.form.WindowState == FormWindowState.Maximized && this.form.MdiParent != null)
			{
				this.tooltip.Present(this.form.MdiParent, text);
			}
			else
			{
				this.tooltip.Present(this.form, text);
			}
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x00074A3C File Offset: 0x00072C3C
		public void ToolTipHide(bool reset_hidden_button)
		{
			if (this.tooltip_timer != null)
			{
				this.tooltip_timer.Enabled = false;
			}
			if (this.tooltip != null && this.tooltip.Visible)
			{
				this.tooltip.Visible = false;
			}
			if (reset_hidden_button)
			{
				this.tooltip_hidden_button = null;
			}
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x00074A94 File Offset: 0x00072C94
		public bool MouseMove(int x, int y)
		{
			if (!this.Visible)
			{
				return false;
			}
			bool flag = false;
			bool anyPushedTitleButtons = this.AnyPushedTitleButtons;
			bool flag2 = false;
			TitleButton titleButton = this.FindButton(x, y);
			foreach (object obj in this)
			{
				TitleButton titleButton2 = (TitleButton)obj;
				if (titleButton2 != null)
				{
					if (titleButton2.State != ButtonState.Inactive)
					{
						if (titleButton2 == titleButton)
						{
							if (anyPushedTitleButtons)
							{
								flag |= titleButton2.State != ButtonState.Pushed;
								titleButton2.State = ButtonState.Pushed;
							}
							this.ToolTipStart(titleButton2);
							flag2 = true;
							if (!titleButton2.Entered)
							{
								titleButton2.Entered = true;
								if (ThemeEngine.Current.ManagedWindowTitleButtonHasHotElementStyle(titleButton2, this.form))
								{
									flag = true;
								}
							}
						}
						else
						{
							if (anyPushedTitleButtons)
							{
								flag |= titleButton2.State != ButtonState.Normal;
								titleButton2.State = ButtonState.Normal;
							}
							if (titleButton2.Entered)
							{
								titleButton2.Entered = false;
								if (ThemeEngine.Current.ManagedWindowTitleButtonHasHotElementStyle(titleButton2, this.form))
								{
									flag = true;
								}
							}
						}
					}
				}
			}
			if (!flag2)
			{
				this.ToolTipHide(false);
			}
			return flag;
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x00074C08 File Offset: 0x00072E08
		public void MouseDown(int x, int y)
		{
			if (!this.Visible)
			{
				return;
			}
			this.ToolTipHide(false);
			foreach (object obj in this)
			{
				TitleButton titleButton = (TitleButton)obj;
				if (titleButton != null && titleButton.State != ButtonState.Inactive)
				{
					titleButton.State = ButtonState.Normal;
				}
			}
			TitleButton titleButton2 = this.FindButton(x, y);
			if (titleButton2 != null && titleButton2.State != ButtonState.Inactive)
			{
				titleButton2.State = ButtonState.Pushed;
			}
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x00074CC8 File Offset: 0x00072EC8
		public void MouseUp(int x, int y)
		{
			if (!this.Visible)
			{
				return;
			}
			TitleButton titleButton = this.FindButton(x, y);
			if (titleButton != null && titleButton.State != ButtonState.Inactive)
			{
				titleButton.OnClick();
			}
			foreach (object obj in this)
			{
				TitleButton titleButton2 = (TitleButton)obj;
				if (titleButton2 != null && titleButton2.State != ButtonState.Inactive)
				{
					titleButton2.State = ButtonState.Normal;
				}
			}
			if (titleButton == this.CloseButton && !this.form.closing)
			{
				XplatUI.InvalidateNC(this.form.Handle);
			}
			this.ToolTipHide(true);
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x00074DB4 File Offset: 0x00072FB4
		internal void MouseLeave(int x, int y)
		{
			if (!this.Visible)
			{
				return;
			}
			foreach (object obj in this)
			{
				TitleButton titleButton = (TitleButton)obj;
				if (titleButton != null && titleButton.State != ButtonState.Inactive)
				{
					titleButton.State = ButtonState.Normal;
				}
			}
			this.ToolTipHide(true);
		}

		// Token: 0x0400102D RID: 4141
		private const int tooltip_hide_interval = 3000;

		// Token: 0x0400102E RID: 4142
		private const int tooltip_show_interval = 1000;

		// Token: 0x0400102F RID: 4143
		public TitleButton MinimizeButton;

		// Token: 0x04001030 RID: 4144
		public TitleButton MaximizeButton;

		// Token: 0x04001031 RID: 4145
		public TitleButton RestoreButton;

		// Token: 0x04001032 RID: 4146
		public TitleButton CloseButton;

		// Token: 0x04001033 RID: 4147
		public TitleButton HelpButton;

		// Token: 0x04001034 RID: 4148
		public TitleButton[] AllButtons;

		// Token: 0x04001035 RID: 4149
		public bool Visible;

		// Token: 0x04001036 RID: 4150
		private ToolTip.ToolTipWindow tooltip;

		// Token: 0x04001037 RID: 4151
		private Timer tooltip_timer;

		// Token: 0x04001038 RID: 4152
		private TitleButton tooltip_hovered_button;

		// Token: 0x04001039 RID: 4153
		private TitleButton tooltip_hidden_button;

		// Token: 0x0400103A RID: 4154
		private Form form;
	}
}
