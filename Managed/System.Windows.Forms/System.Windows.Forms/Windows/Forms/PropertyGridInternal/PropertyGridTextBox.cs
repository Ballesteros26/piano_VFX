using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x020002A6 RID: 678
	internal class PropertyGridTextBox : UserControl, IMessageFilter
	{
		// Token: 0x06002D4E RID: 11598 RVA: 0x000AE544 File Offset: 0x000AC744
		public PropertyGridTextBox()
		{
			this.dialog_button = new Button();
			this.dropdown_button = new Button();
			this.textbox = new PGTextBox();
			base.SuspendLayout();
			this.dialog_button.Dock = DockStyle.Right;
			this.dialog_button.BackColor = SystemColors.Control;
			this.dialog_button.Size = new Size(16, 16);
			this.dialog_button.TabIndex = 1;
			this.dialog_button.Visible = false;
			this.dialog_button.Click += new EventHandler(this.dialog_button_Click);
			this.dropdown_button.Dock = DockStyle.Right;
			this.dropdown_button.BackColor = SystemColors.Control;
			this.dropdown_button.Size = new Size(16, 16);
			this.dropdown_button.TabIndex = 2;
			this.dropdown_button.Visible = false;
			this.dropdown_button.Click += new EventHandler(this.dropdown_button_Click);
			this.textbox.AutoSize = false;
			this.textbox.BorderStyle = BorderStyle.None;
			this.textbox.Dock = DockStyle.Fill;
			this.textbox.TabIndex = 3;
			base.Controls.Add(this.textbox);
			base.Controls.Add(this.dropdown_button);
			base.Controls.Add(this.dialog_button);
			base.SetStyle(ControlStyles.Selectable, true);
			base.ResumeLayout(false);
			this.dropdown_button.Paint += this.dropdown_button_Paint;
			this.dialog_button.Paint += this.dialog_button_Paint;
			this.textbox.DoubleClick += new EventHandler(this.textbox_DoubleClick);
			this.textbox.KeyDown += this.textbox_KeyDown;
			this.textbox.GotFocus += new EventHandler(this.textbox_GotFocus);
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x000AE728 File Offset: 0x000AC928
		// Note: this type is marked as 'beforefieldinit'.
		static PropertyGridTextBox()
		{
			PropertyGridTextBox.DropDownButtonClickedEvent = new object();
			PropertyGridTextBox.DialogButtonClickedEvent = new object();
			PropertyGridTextBox.ToggleValueEvent = new object();
			PropertyGridTextBox.KeyDownEvent = new object();
			PropertyGridTextBox.ValidateEvent = new object();
		}

		// Token: 0x140002BF RID: 703
		// (add) Token: 0x06002D50 RID: 11600 RVA: 0x000AE768 File Offset: 0x000AC968
		// (remove) Token: 0x06002D51 RID: 11601 RVA: 0x000AE77C File Offset: 0x000AC97C
		public event EventHandler DropDownButtonClicked
		{
			add
			{
				base.Events.AddHandler(PropertyGridTextBox.DropDownButtonClickedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(PropertyGridTextBox.DropDownButtonClickedEvent, value);
			}
		}

		// Token: 0x140002C0 RID: 704
		// (add) Token: 0x06002D52 RID: 11602 RVA: 0x000AE790 File Offset: 0x000AC990
		// (remove) Token: 0x06002D53 RID: 11603 RVA: 0x000AE7A4 File Offset: 0x000AC9A4
		public event EventHandler DialogButtonClicked
		{
			add
			{
				base.Events.AddHandler(PropertyGridTextBox.DialogButtonClickedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(PropertyGridTextBox.DialogButtonClickedEvent, value);
			}
		}

		// Token: 0x140002C1 RID: 705
		// (add) Token: 0x06002D54 RID: 11604 RVA: 0x000AE7B8 File Offset: 0x000AC9B8
		// (remove) Token: 0x06002D55 RID: 11605 RVA: 0x000AE7CC File Offset: 0x000AC9CC
		public event EventHandler ToggleValue
		{
			add
			{
				base.Events.AddHandler(PropertyGridTextBox.ToggleValueEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(PropertyGridTextBox.ToggleValueEvent, value);
			}
		}

		// Token: 0x140002C2 RID: 706
		// (add) Token: 0x06002D56 RID: 11606 RVA: 0x000AE7E0 File Offset: 0x000AC9E0
		// (remove) Token: 0x06002D57 RID: 11607 RVA: 0x000AE7F4 File Offset: 0x000AC9F4
		public new event KeyEventHandler KeyDown
		{
			add
			{
				base.Events.AddHandler(PropertyGridTextBox.KeyDownEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(PropertyGridTextBox.KeyDownEvent, value);
			}
		}

		// Token: 0x140002C3 RID: 707
		// (add) Token: 0x06002D58 RID: 11608 RVA: 0x000AE808 File Offset: 0x000ACA08
		// (remove) Token: 0x06002D59 RID: 11609 RVA: 0x000AE81C File Offset: 0x000ACA1C
		public new event CancelEventHandler Validate
		{
			add
			{
				base.Events.AddHandler(PropertyGridTextBox.ValidateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(PropertyGridTextBox.ValidateEvent, value);
			}
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x000AE830 File Offset: 0x000ACA30
		bool IMessageFilter.PreFilterMessage(ref Message m)
		{
			if (!this.validating && m.HWnd != this.textbox.Handle && this.textbox.Focused && (m.Msg == 513 || m.Msg == 519 || m.Msg == 516 || m.Msg == 161 || m.Msg == 167 || m.Msg == 164))
			{
				CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[PropertyGridTextBox.ValidateEvent];
				if (cancelEventHandler != null)
				{
					CancelEventArgs cancelEventArgs = new CancelEventArgs();
					this.validating = true;
					cancelEventHandler.Invoke(this, cancelEventArgs);
					this.validating = false;
					if (!cancelEventArgs.Cancel)
					{
						Application.RemoveMessageFilter(this);
						this.filtering = false;
					}
					return cancelEventArgs.Cancel;
				}
			}
			return false;
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x000AE92C File Offset: 0x000ACB2C
		protected override void OnGotFocus(EventArgs args)
		{
			base.OnGotFocus(args);
			this.textbox.has_been_focused = true;
			this.textbox.Focus();
			this.textbox.SelectionLength = 0;
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06002D5C RID: 11612 RVA: 0x000AE95C File Offset: 0x000ACB5C
		// (set) Token: 0x06002D5D RID: 11613 RVA: 0x000AE96C File Offset: 0x000ACB6C
		public bool DialogButtonVisible
		{
			get
			{
				return this.dialog_button.Visible;
			}
			set
			{
				this.dialog_button.Visible = value;
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06002D5E RID: 11614 RVA: 0x000AE97C File Offset: 0x000ACB7C
		// (set) Token: 0x06002D5F RID: 11615 RVA: 0x000AE98C File Offset: 0x000ACB8C
		public bool DropDownButtonVisible
		{
			get
			{
				return this.dropdown_button.Visible;
			}
			set
			{
				this.dropdown_button.Visible = value;
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06002D60 RID: 11616 RVA: 0x000AE99C File Offset: 0x000ACB9C
		// (set) Token: 0x06002D61 RID: 11617 RVA: 0x000AE9A4 File Offset: 0x000ACBA4
		public new Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				this.textbox.ForeColor = value;
				this.dropdown_button.ForeColor = value;
				this.dialog_button.ForeColor = value;
				base.ForeColor = value;
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06002D62 RID: 11618 RVA: 0x000AE9DC File Offset: 0x000ACBDC
		// (set) Token: 0x06002D63 RID: 11619 RVA: 0x000AE9E4 File Offset: 0x000ACBE4
		public new Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				this.textbox.BackColor = value;
				base.BackColor = value;
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06002D64 RID: 11620 RVA: 0x000AE9FC File Offset: 0x000ACBFC
		// (set) Token: 0x06002D65 RID: 11621 RVA: 0x000AEA0C File Offset: 0x000ACC0C
		public bool ReadOnly
		{
			get
			{
				return this.textbox.ReadOnly;
			}
			set
			{
				this.textbox.ReadOnly = value;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06002D66 RID: 11622 RVA: 0x000AEA1C File Offset: 0x000ACC1C
		// (set) Token: 0x06002D67 RID: 11623 RVA: 0x000AEA2C File Offset: 0x000ACC2C
		public new string Text
		{
			get
			{
				return this.textbox.Text;
			}
			set
			{
				this.textbox.Text = value;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (set) Token: 0x06002D68 RID: 11624 RVA: 0x000AEA3C File Offset: 0x000ACC3C
		public char PasswordChar
		{
			set
			{
				this.textbox.PasswordChar = value;
			}
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x000AEA4C File Offset: 0x000ACC4C
		private void dropdown_button_Paint(object sender, PaintEventArgs e)
		{
			ThemeEngine.Current.CPDrawComboButton(e.Graphics, this.dropdown_button.ClientRectangle, this.dropdown_button.ButtonState);
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x000AEA80 File Offset: 0x000ACC80
		private void dialog_button_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawString("...", new Font(this.Font, 1), Brushes.Black, 0f, 0f);
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x000AEAB8 File Offset: 0x000ACCB8
		private void dropdown_button_Click(object sender, EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[PropertyGridTextBox.DropDownButtonClickedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x000AEAEC File Offset: 0x000ACCEC
		private void dialog_button_Click(object sender, EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[PropertyGridTextBox.DialogButtonClickedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x000AEB20 File Offset: 0x000ACD20
		internal void SendMouseDown(Point screenLocation)
		{
			Point point = base.PointToClient(screenLocation);
			XplatUI.SendMessage(this.Handle, Msg.WM_LBUTTONDOWN, new IntPtr(1), Control.MakeParam(point.X, point.Y));
			this.textbox.FocusAt(screenLocation);
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x000AEB6C File Offset: 0x000ACD6C
		private void textbox_DoubleClick(object sender, EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[PropertyGridTextBox.ToggleValueEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x000AEBA0 File Offset: 0x000ACDA0
		private void textbox_KeyDown(object sender, KeyEventArgs e)
		{
			KeyEventHandler keyEventHandler = (KeyEventHandler)base.Events[PropertyGridTextBox.KeyDownEvent];
			if (keyEventHandler != null)
			{
				keyEventHandler(this, e);
			}
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x000AEBD4 File Offset: 0x000ACDD4
		private void textbox_GotFocus(object sender, EventArgs e)
		{
			if (!this.filtering)
			{
				this.filtering = true;
				Application.AddMessageFilter(this);
			}
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x000AEBF0 File Offset: 0x000ACDF0
		protected override void DestroyHandle()
		{
			Application.RemoveMessageFilter(this);
			this.filtering = false;
			base.DestroyHandle();
		}

		// Token: 0x040015DF RID: 5599
		private PGTextBox textbox;

		// Token: 0x040015E0 RID: 5600
		private Button dialog_button;

		// Token: 0x040015E1 RID: 5601
		private Button dropdown_button;

		// Token: 0x040015E2 RID: 5602
		private bool validating;

		// Token: 0x040015E3 RID: 5603
		private bool filtering;
	}
}
